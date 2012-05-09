using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_Controls_UI_SearchIndex_User : CMSAdminControl, IPostBackEventHandler
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
        // Disable textbpx editing
        selectInRole.UseFriendlyMode = true;
        selectNotInRole.UseFriendlyMode = true;

        if (!RequestHelper.IsPostBack())
        {
            SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);
            if (sii != null)
            {

                SearchIndexSettings sis = sii.IndexSettings;
                SearchIndexSettingsInfo sisi = sis.GetSearchIndexSettingsInfo(SearchHelper.SIMPLE_ITEM_ID);

                // Create new
                if (sisi != null)
                {
                    chkHidden.Checked = ValidationHelper.GetBoolean(sisi.GetValue("UserHidden"), false);
                    chkOnlyEnabled.Checked = ValidationHelper.GetBoolean(sisi.GetValue("UserEnabled"), true);
                    chkSite.Checked = ValidationHelper.GetBoolean(sisi.GetValue("UserAllSites"), false);
                    selectInRole.Value = ValidationHelper.GetString(sisi.GetValue("UserInRoles"), String.Empty);
                    selectNotInRole.Value = ValidationHelper.GetString(sisi.GetValue("UserNotInRoles"), String.Empty);
                    txtWhere.TextArea.Text = sisi.WhereCondition;
                }
            }
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
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

            sisi.SetValue("UserHidden", chkHidden.Checked);
            sisi.SetValue("UserEnabled", chkOnlyEnabled.Checked );
            sisi.SetValue("UserAllSites", chkSite.Checked);
            sisi.SetValue("UserInRoles", selectInRole.Value);
            sisi.SetValue("UserNotInRoles", selectNotInRole.Value);
            sisi.WhereCondition = txtWhere.TextArea.Text.Trim();

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
                
                lblInfo.Text = GetString("srch.index.rebuildstarted");
                lblInfo.Visible = true;
            }
        }
    }

    #endregion
}
