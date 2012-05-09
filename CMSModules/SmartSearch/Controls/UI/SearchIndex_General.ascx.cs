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

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.SettingsProvider;
using CMS.IO;

using Lucene.Net.Search;
using Lucene.Net.Index;

public partial class CMSModules_SmartSearch_Controls_UI_SearchIndex_General : CMSAdminEditControl, IPostBackEventHandler
{
    #region "Variables"

    protected SearchIndexInfo sii = null;
    int codeNameLength = 0;

    #endregion


    #region "Methods and events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Show panel with message how to enable indexing
        if (!SettingsKeyProvider.GetBoolValue("CMSSearchIndexingEnabled"))
        {
            pnlDisabled.Visible = true;
        }

        // Get file size and document count informations resource strings        
        pnlInfo.GroupingText = GetString("srch.index.info");

        // Action buttons
        imgOptimize.ImageUrl = GetImageUrl("CMSModules/CMS_SMartSearch/optimize.png");
        btnOptimize.Text = GetString("srch.index.optimize");
        btnOptimize.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("srch.index.confirmoptimize")) + ");";

        imgRebuild.ImageUrl = GetImageUrl("CMSModules/CMS_SMartSearch/rebuild.png");
        btnRebuild.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("srch.index.confirmrebuild")) + ");";
        btnRebuild.Text = GetString("srch.index.rebuild");

        // Init controls
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvBatchSize.ErrorMessage = GetString("general.requiresvalue");
        btnOk.Text = GetString("General.OK");

        // Possible length of path - already taken, +1 because in MAX_INDEX PATH is count codename of length 1
        string indexPath = Path.Combine(SettingsKeyProvider.WebApplicationPhysicalPath, "App_Data\\CMSModules\\SmartSearch\\");
        codeNameLength = SearchHelper.MAX_INDEX_PATH - indexPath.Length + 1;
        pnlContent.Visible = true;
        txtCodeName.MaxLength = codeNameLength;

        // Get search index info
        sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);

        // Set edited object
        if (this.ItemID > 0)
        {
            EditedObject = sii;
        }

        if (sii != null)
        {
            string indexTypeStr = String.Empty;
            switch (sii.IndexType)
            {
                // Documents
                case PredefinedObjectType.DOCUMENT:
                    indexTypeStr = GetString("srch.index.doctype");
                    break;

                // Forums
                case PredefinedObjectType.FORUM:
                    indexTypeStr = GetString("srch.index.forumtype");
                    break;

                // Users
                case PredefinedObjectType.USER:
                    indexTypeStr = GetString("srch.index.usertype");
                    break;

                // Custom tables
                case SettingsObjectType.CUSTOMTABLE:
                    indexTypeStr = GetString("srch.index.customtabletype");
                    break;

                // General index
                case SearchHelper.GENERALINDEX:
                    indexTypeStr = GetString("srch.index.general");
                    break;

                // Custom search index
                case SearchHelper.CUSTOM_SEARCH_INDEX:
                    indexTypeStr = GetString("srch.index.customsearch");
                    break;

                // Documents crawler
                case SearchHelper.DOCUMENTS_CRAWLER_INDEX:
                    indexTypeStr = GetString("srch.index.doctypecrawler");
                    break;
            }
            lblTypeValue.Text = indexTypeStr;

            stopCustomControl.IndexInfo = sii;
            stopCustomControl.AnalyzerDropDown = drpAnalyzer;
        }

        if (!RequestHelper.IsPostBack())
        {
            this.LoadControls();
        }

        // Reload info panel
        ReloadInfoPanel();
    }

    /// <summary>
    /// Resets all boxes.
    /// </summary>
    public void LoadControls()
    {
        //Fill drop down list
        DataHelper.FillWithEnum<AnalyzerTypeEnum>(drpAnalyzer, "srch.index.", SearchIndexInfoProvider.AnalyzerEnumToString, true);

        // Fill textboxes        
        if (sii != null)
        {
            txtCodeName.Text = sii.IndexName;
            txtDisplayName.Text = sii.IndexDisplayName;
            drpAnalyzer.SelectedValue = SearchIndexInfoProvider.AnalyzerEnumToString(sii.IndexAnalyzerType);
            txtBatchSize.Text = sii.IndexBatchSize.ToString();

            // Community indexing is not yet supported
            //chkCommunity.Checked = sii.IndexIsCommunityGroup;

        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Trim blank space and too long string
        string codeName = txtCodeName.Text.Trim();
        if (codeName.Length > 200)
        {
            codeName = codeName.Substring(0, 200);
        }

        // Get code name
        codeName = ValidationHelper.GetCodeName(codeName, null, null);
        txtCodeName.Text = codeName;

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
            // Check code name
            SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(codeName);

            // Get current item
            SearchIndexInfo current = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);


            // Check if code name is unique
            if ((sii == null) || (sii == current))
            {
                // Get original index path
                string originalPath = current.CurrentIndexPath;

                // Set the fields
                current.IndexName = codeName;

                // Trim blank space and too long string
                txtDisplayName.Text = txtDisplayName.Text.Trim();
                if (txtDisplayName.Text.Length > 200)
                {
                    txtDisplayName.Text = txtDisplayName.Text.Substring(0, 200);
                }

                if (current.IndexDisplayName != txtDisplayName.Text)
                {
                    // Refresh a breadcrumb if used in the tabs layout
                    ScriptHelper.RefreshTabHeader(Page, string.Empty);
                }

                current.IndexDisplayName = txtDisplayName.Text;

                // Check if analyzer type is changed
                bool analyzerTypeChanged = false;
                if ((current != null) &&
                    ((current.IndexAnalyzerType != SearchIndexInfoProvider.AnalyzerCodenameToEnum(drpAnalyzer.SelectedValue)) ||
                    (String.Compare(current.StopWordsFile, stopCustomControl.StopWordsFile, true) != 0) ||
                    (String.Compare(current.CustomAnalyzerAssemblyName, stopCustomControl.CustomAnalyzerAssemblyName) != 0) ||
                    (String.Compare(current.CustomAnalyzerClassName, stopCustomControl.CustomAnalyzerClassName) != 0)))
                {
                    analyzerTypeChanged = true;
                }
                current.IndexAnalyzerType = SearchIndexInfoProvider.AnalyzerCodenameToEnum(drpAnalyzer.SelectedValue);

                // Community indexing is not yet supported
                //current.IndexIsCommunityGroup = chkCommunity.Checked;
                current.IndexIsCommunityGroup = false;
                current.CustomAnalyzerAssemblyName = stopCustomControl.CustomAnalyzerAssemblyName;
                current.CustomAnalyzerClassName = stopCustomControl.CustomAnalyzerClassName;
                current.StopWordsFile = stopCustomControl.StopWordsFile;
                current.IndexBatchSize = ValidationHelper.GetInteger(txtBatchSize.Text, 10);

                // Save the object
                SearchIndexInfoProvider.SetSearchIndexInfo(current);

                // Codename changed
                bool codenameChanged = false;
                if (sii == null)
                {
                    try
                    {
                        DirectoryHelper.MoveDirectory(originalPath, current.CurrentIndexPath);
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider ep = new EventLogProvider();
                        ep.LogEvent("SmartSearch", "Rename", ex);
                    }

                    codenameChanged = true;
                }

                if (codenameChanged || analyzerTypeChanged)
                {
                    lblInfo.Text = String.Format(GetString("general.changessaved") + " " + GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
                }
                else
                {
                    lblInfo.Text = GetString("General.ChangesSaved");
                }

                // Redirect to edit mode
                RaiseOnSaved();
                lblInfo.Visible = true;

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


    /// <summary>
    ///  Rebuild click.
    /// </summary>    
    protected void btnRebuild_Click(object sender, EventArgs e)
    {
        if (sii != null)
        {
            if ((sii.IndexType.ToLower() == PredefinedObjectType.DOCUMENT)|| (sii.IndexType == SearchHelper.DOCUMENTS_CRAWLER_INDEX))
            {
                DataSet ds = SearchIndexCultureInfoProvider.GetSearchIndexCultures("IndexID = " + sii.IndexID, null, 0, "IndexID, IndexCultureID");                
                if (SqlHelperClass.DataSourceIsEmpty(ds))
                {
                    lblInfo.Text = GetString("index.noculture");
                    lblInfo.Visible = true;
                    return;
                }

                // Test content
                DataSet result = sii.IndexSettings.GetAll();
                if (DataHelper.DataSourceIsEmpty(result))
                {                
                    lblInfo.Text = GetString("index.nocontent");
                    lblInfo.Visible = true;
                    return;
                }
            }

            SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Rebuild, sii.IndexType, null, sii.IndexName);
        }

        lblInfo.Text = GetString("srch.index.rebuildstarted");
        lblInfo.Visible = true;

        // Reload info panel
        System.Threading.Thread.Sleep(100);
        ReloadInfoPanel();
    }


    /// <summary>
    ///  Optimize click.
    /// </summary>    
    protected void btnOptimize_Click(object sender, EventArgs e)
    {
        // Rebuild search index info
        if (sii != null)
        {
            SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Optimize, sii.IndexType, null, sii.IndexName);
        }

        lblInfo.Text = GetString("srch.index.optimizestarted");
        lblInfo.Visible = true;

        // Reload info panel
        System.Threading.Thread.Sleep(100);
        ReloadInfoPanel();
    }


    /// <summary>
    /// Reloads info panel.
    /// </summary>
    protected void ReloadInfoPanel()
    {
        if (sii != null)
        {
            // Keep flag if is in action status
            bool isInAction = (sii.IndexStatus == IndexStatusEnum.REBUILDING || sii.IndexStatus == IndexStatusEnum.OPTIMIZING);

            // Keep flag if index is not usable
            bool isNotReady = (!isInAction && sii.IndexStatus != IndexStatusEnum.READY);

            // get status name
            string statusName = GetString("srch.status." + sii.IndexStatus.ToString());

            // Set progress if is action status
            ltrProgress.Text = String.Empty;
            if (isInAction)
            {
                ltrProgress.Text = "<img style=\"width:12px;height:12px;\" src=\"" + UIHelper.GetImageUrl(this.Page, "Design/Preloaders/preload16.gif") + "\" alt=\"" + statusName + "\" tooltip=\"" + statusName + "\"  />";
            }

            // Fill panel info with informations about index
            lblNumberOfItemsValue.Text = ValidationHelper.GetString(sii.NumberOfIndexedItems, "0");
            lblIndexFileSizeValue.Text = DataHelper.GetSizeString(sii.IndexFileSize);
            lblIndexStatusValue.Text = statusName;

            // use coloring for status name
            if (isNotReady)
            {
                lblIndexStatusValue.Text = "<span class=\"StatusDisabled\">" + statusName + "</span>";
            }
            else if (sii.IndexStatus == IndexStatusEnum.READY)
            {
                lblIndexStatusValue.Text = "<span class=\"StatusEnabled\">" + statusName + "</span>";
            }

            lblLastRebuildTimeValue.Text = GetString("general.notavailable");
            lblLastUpdateValue.Text = sii.IndexLastUpdate.ToString();

            if (sii.IndexLastRebuildTime != DateTimeHelper.ZERO_TIME)
            {
                lblLastRebuildTimeValue.Text = ValidationHelper.GetString(sii.IndexLastRebuildTime, "");
            }
            lblIndexIsOptimizedValue.Text = UniGridFunctions.ColoredSpanYesNo(false);

            if (sii.IndexStatus == IndexStatusEnum.READY)
            {
                IndexSearcher searcher = sii.GetSearcher();
                if (searcher != null)
                {
                    IndexReader reader = searcher.GetIndexReader();
                    if (reader != null)
                    {
                        if (reader.IsOptimized())
                        {
                            lblIndexIsOptimizedValue.Text = UniGridFunctions.ColoredSpanYesNo(true);
                        }
                    }
                }
            }
        }
    }

    #endregion


    #region "IPostBackEventHandler Members"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument == "saved")
        {
            // Get search index info
            SearchIndexInfo sii = null;
            if (this.ItemID > 0)
            {
                sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);
            }

            // Create rebuild task
            if (sii != null)
            {
                SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Rebuild, sii.IndexType, null, sii.IndexName);
            }

            lblInfo.Text = GetString("srch.index.rebuildstarted");
            lblInfo.Visible = true;

            // Reload info panel
            System.Threading.Thread.Sleep(100);
            ReloadInfoPanel();
        }
    }

    #endregion

}
