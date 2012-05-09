using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.IO;

public partial class CMSModules_SmartSearch_Controls_UI_SearchIndex_New : CMSAdminEditControl
{
    #region "Variables"

    int codeNameLength = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        string indexPath = Path.Combine(SettingsKeyProvider.WebApplicationPhysicalPath, "App_Data\\CMSModules\\SmartSearch\\");
        if (indexPath.Length > SearchHelper.MAX_INDEX_PATH)
        {
            pnlPathTooLong.Visible = true;
            return;
        }
        else
        {
            // Possible length of path - already taken, +1 because in MAX_INDEX PATH is count codename of length 1
            codeNameLength = SearchHelper.MAX_INDEX_PATH - indexPath.Length + 1;
            pnlContent.Visible = true;
            txtCodeName.MaxLength = codeNameLength;
        }

        // Init controls
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        btnOk.Text = GetString("General.OK");
        if (CMSContext.CurrentSite != null)
        {
            chkAddIndexToCurrentSite.Text = GetString("srch.newindex.addindextocurrentsite") + " " + CMSContext.CurrentSite.DisplayName;
        }
        else
        {
            chkAddIndexToCurrentSite.Visible = false;
        }

        if (!RequestHelper.IsPostBack())
        {
            this.ResetControls();
        }

        stopCustomControl.AnalyzerDropDown = drpAnalyzer;
    }


    /// <summary>
    /// Resets all boxes.
    /// </summary>
    public void ResetControls()
    {
        txtCodeName.Text = null;
        txtDisplayName.Text = null;

        //Fill drop down list
        DataHelper.FillWithEnum<AnalyzerTypeEnum>(drpAnalyzer, "srch.index.", SearchIndexInfoProvider.AnalyzerEnumToString, true);

        drpAnalyzer.SelectedValue = SearchIndexInfoProvider.AnalyzerEnumToString(AnalyzerTypeEnum.StandardAnalyzer);
        chkAddIndexToCurrentSite.Checked = true;

        // Create sorted list for drop down values
        SortedList sortedList = new SortedList();
        
        sortedList.Add(GetString("srch.index.doctype"), PredefinedObjectType.DOCUMENT);
        // Allow forum only if module is available
        if ((ModuleEntry.IsModuleRegistered(ModuleEntry.FORUMS) && ModuleEntry.IsModuleLoaded(ModuleEntry.FORUMS)))
        {
            sortedList.Add(GetString("srch.index.forumtype"), PredefinedObjectType.FORUM);
        }
        sortedList.Add(GetString("srch.index.usertype"), PredefinedObjectType.USER);
        sortedList.Add(GetString("srch.index.customtabletype"), SettingsObjectType.CUSTOMTABLE);
        sortedList.Add(GetString("srch.index.customsearch"), SearchHelper.CUSTOM_SEARCH_INDEX);
        sortedList.Add(GetString("srch.index.doctypecrawler"), SearchHelper.DOCUMENTS_CRAWLER_INDEX);
        sortedList.Add(GetString("srch.index.general"), SearchHelper.GENERALINDEX);

        drpType.DataValueField = "value";
        drpType.DataTextField = "key";
        drpType.DataSource = sortedList;
        drpType.DataBind();

        // Pre-select documents index
        drpType.SelectedValue = PredefinedObjectType.DOCUMENT;

    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Get code name
        string codeName = ValidationHelper.GetCodeName(txtCodeName.Text, null, null);

        // Perform validation
        string errorMessage = new Validator().NotEmpty(codeName, rfvCodeName.ErrorMessage)
            .NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage).Result;

        // Check CodeName for identificator format
        if (!ValidationHelper.IsCodeName(codeName))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }

        // Check length of code name
        if (codeName.Length > codeNameLength)
        {
            errorMessage = GetString("srch.codenameexceeded");
        }

        if (errorMessage == "")
        {
            // Create new 
            SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(codeName);

            if (sii == null)
            {
                sii = new SearchIndexInfo();

                // Set the fields
                sii.IndexName = codeName;
                sii.IndexDisplayName = txtDisplayName.Text.Trim();
                sii.IndexAnalyzerType = SearchIndexInfoProvider.AnalyzerCodenameToEnum(drpAnalyzer.SelectedValue);
                // Community indexing is not yet supported
                //sii.IndexIsCommunityGroup = chkCommunity.Checked;
                sii.IndexIsCommunityGroup = false;
                sii.IndexType = drpType.SelectedValue;
                sii.CustomAnalyzerAssemblyName = stopCustomControl.CustomAnalyzerAssemblyName;
                sii.CustomAnalyzerClassName = stopCustomControl.CustomAnalyzerClassName;
                sii.StopWordsFile = stopCustomControl.StopWordsFile;

                // Save the object
                SearchIndexInfoProvider.SetSearchIndexInfo(sii);
                this.ItemID = sii.IndexID;

                // Assing to current website
                if (chkAddIndexToCurrentSite.Checked)
                {
                    SearchIndexSiteInfoProvider.AddSearchIndexToSite(sii.IndexID, CMSContext.CurrentSiteID);
                }

                // Redirect to edit mode
                RaiseOnSaved();
            }
            else
            {
                // Error message - code name already exists
                lblError.Visible = true;
                lblError.Text = GetString("srch.index.codenameexists");
            }
        }
        else
        {
            // Error message - validation
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
