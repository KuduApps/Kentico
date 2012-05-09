using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Settings_Development_CustomSettings_CustomSettingsCategory_Default : SiteManagerPage
{
    protected string mContentLink = "";
    protected string mSelectedTab = "keys";
    protected string mTabsCategoryId = "0";

    protected void Page_Load(object sender, EventArgs e)
    {
        // Colect parameters
        int categoryId = QueryHelper.GetInteger("categoryid", 0);
        int siteId = QueryHelper.GetInteger("siteid", 0);
        string treeRoot = QueryHelper.GetText("treeroot", "customsettings");
        string rootName = treeRoot == "settings" ? "CMS.Settings" : "CMS.CustomSettings";
        mSelectedTab = QueryHelper.GetString("selectedtab", "keys");
        bool isEditing = QueryHelper.GetBoolean("isediting", false);

        if (categoryId > 0)
        {
            DataSet ds = SettingsCategoryInfoProvider.GetSettingsCategories("CategoryID = " + categoryId, null, 1, "CategoryName, CategoryIsGroup, CategoryParentID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                bool isGroup = ValidationHelper.GetBoolean(ds.Tables[0].Rows[0]["CategoryIsGroup"], false);
                // CategoryId for use in tabs
                int tabCatId = categoryId;

                // Use paprentID when viewing group
                if (isGroup)
                {
                    tabCatId = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["CategoryParentID"], 0);
                }
                mTabsCategoryId = tabCatId.ToString();

                // Resolve content page link: edit or list
                if (isEditing)
                {
                    if (!isGroup)
                    {
                        mContentLink = "CustomSettingsCategory_Edit.aspx?treeroot=" + treeRoot + "&categoryid=" + categoryId + "&siteid=" + siteId;
                        mSelectedTab = "general";
                    }
                    else
                    {
                        mContentLink = "CustomSettingsCategory_Edit.aspx?isgroup=1&treeroot=" + treeRoot + "&categoryid=" + categoryId + "&siteid=" + siteId;
                        mSelectedTab = "keys";
                    }
                }
                else
                {
                    mContentLink = "CustomSettings_List.aspx?treeroot=" + treeRoot + "&categoryid=" + categoryId + "&siteid=" + siteId;
                }
            }
        }
        // Default link, when no category specified
        else
        {
            mContentLink = "CustomSettings_List.aspx?treeroot=" + treeRoot + "&siteid=" + siteId;
        }
    }
}

