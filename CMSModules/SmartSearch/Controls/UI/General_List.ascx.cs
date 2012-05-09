using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_Controls_UI_General_List : CMSAdminControl, IPostBackEventHandler
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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);
            if (sii != null)
            {
                SearchIndexSettings sis = sii.IndexSettings;
                SearchIndexSettingsInfo sisi = sis.GetSearchIndexSettingsInfo(SearchHelper.SIMPLE_ITEM_ID);

                if (sisi != null)
                {
                    selObjects.Value = sisi.ClassNames;
                    txtWhere.TextArea.Text = sisi.WhereCondition;
                }
            }
        }


        // Setup the UniSelector - exclude classes which have their special type of a search index
        selObjects.ExcludeSmartSearchClassNames = true;
        selObjects.ShowPleaseSelect = String.IsNullOrEmpty(ValidationHelper.GetString(selObjects.Value, String.Empty));
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Validate - check for selected object name
        if (string.IsNullOrEmpty(selObjects.Value.ToString()))
        {
            lblError.Visible = true;
            return;
        }

        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);
        if (sii != null)
        {
            SearchIndexSettings sis = sii.IndexSettings;
            SearchIndexSettingsInfo sisi = sis.GetSearchIndexSettingsInfo(SearchHelper.SIMPLE_ITEM_ID);

            // Create new
            if (sisi == null)
            {
                sisi = new SearchIndexSettingsInfo();
                sisi.ID = SearchHelper.SIMPLE_ITEM_ID;
            }

            sisi.ClassNames = ValidationHelper.GetString(selObjects.Value, String.Empty);
            sisi.WhereCondition = txtWhere.TextArea.Text.Trim();

            // Update settings item
            sis.SetSearchIndexSettingsInfo(sisi);
            // Update xml value
            sii.IndexSettings = sis;

            SearchIndexInfoProvider.SetSearchIndexInfo(sii);

            selObjects.ShowPleaseSelect = false;
            selObjects.Reload(true);

            // Display a message
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");

            if (smartSearchEnabled)
            {
                lblInfo.Text += " " + String.Format(GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
            }
        }
    }


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

                lblInfo.Text = GetString("srch.index.rebuildstarted");
                lblInfo.Visible = true;
            }
        }
    }

    #endregion
}
