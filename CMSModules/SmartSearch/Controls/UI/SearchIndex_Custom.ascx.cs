using System;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_Controls_UI_SearchIndex_Custom : CMSAdminControl, IPostBackEventHandler
{
    #region "Variables"

    private bool smartSearchEnabled = SettingsKeyProvider.GetBoolValue("CMSSearchIndexingEnabled");
    private int mItemId = QueryHelper.GetInteger("indexid", 0);

    #endregion


    #region "Properties"

    /// <summary>
    /// Item ID.
    /// </summary>
    public int ItemID
    {
        get
        {
            return mItemId;
        }
        set
        {
            mItemId = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(ItemID);
            if (sii != null)
            {
                SearchIndexSettings sis = sii.IndexSettings;
                SearchIndexSettingsInfo sisi = sis.GetSearchIndexSettingsInfo(SearchHelper.CUSTOM_INDEX_DATA);

                // Create new
                if (sisi != null)
                {
                    txtAssembly.Text = ValidationHelper.GetString(sisi.GetValue("AssemblyName"), String.Empty);
                    txtClassName.Text = ValidationHelper.GetString(sisi.GetValue("ClassName"), String.Empty);
                    txtData.TextArea.Text = ValidationHelper.GetString(sisi.GetValue("CustomData"), String.Empty);
                }
            }
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        string errorMessage = new Validator().NotEmpty(txtAssembly.Text.Trim(), GetString("srch.index.assemblyempty")).NotEmpty(txtClassName.Text.Trim(), GetString("srch.index.classnameempty")).Result;

        if (String.IsNullOrEmpty(errorMessage))
        {
            SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(ItemID);
            if (sii != null)
            {
                SearchIndexSettings sis = sii.IndexSettings;
                SearchIndexSettingsInfo sisi = sis.GetSearchIndexSettingsInfo(SearchHelper.CUSTOM_INDEX_DATA);

                // Create new
                if (sisi == null)
                {
                    sisi = new SearchIndexSettingsInfo();
                    sisi.ID = SearchHelper.CUSTOM_INDEX_DATA;
                }

                sisi.SetValue("AssemblyName", txtAssembly.Text.Trim());
                sisi.SetValue("ClassName", txtClassName.Text.Trim());
                sisi.SetValue("CustomData", txtData.TextArea.Text);

                // Update settings item
                sis.SetSearchIndexSettingsInfo(sisi);
                // Update xml value
                sii.IndexSettings = sis;

                SearchIndexInfoProvider.SetSearchIndexInfo(sii);

                // Redirect to edit mode
                lblInfo.Visible = true;
                lblInfo.Text = GetString("general.changessaved");

                if (smartSearchEnabled)
                {
                    lblInfo.Text += " " + String.Format(GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
                }
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return;
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
            if (ItemID > 0)
            {
                sii = SearchIndexInfoProvider.GetSearchIndexInfo(ItemID);
            }

            // Create rebuild task
            if (sii != null)
            {
                SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Rebuild, sii.IndexType, null, sii.IndexName);

                lblInfo.Text = GetString("srch.index.rebuildstarted");
                lblInfo.Visible = true;
            }
        }
    }

    #endregion
}