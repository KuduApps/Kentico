using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_Development_CustomSettings_SettingsKey_Edit : SiteManagerPage
{
    #region "Variables"

    private string mKeyName = "";
    private int mSiteId = -1;
    private string mParentGroup = "";
    private SettingsKeyInfo mEditedKey = null;
    private string mTreeRoot = "customsettings";

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get parameters
        mKeyName = QueryHelper.GetString("keyname", "");
        mSiteId = QueryHelper.GetInteger("siteid", -1);
        mParentGroup = QueryHelper.GetString("parentgroup", null);
        mTreeRoot = QueryHelper.GetText("treeroot", "customsettings");

        skeEditKey.IsCustomSetting = mTreeRoot.Equals("customsettings");
        skeEditKey.OnSaved += new EventHandler(skeEditKey_OnSaved);

        if (skeEditKey.IsCustomSetting)
        {
            SettingsCategoryInfo root = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("CMS.CustomSettings");
            if (root != null)
            {
                skeEditKey.RootCategoryID = root.CategoryID;
            }
        }

        // Set up editing mode
        if ((mSiteId >= 0) && !string.IsNullOrEmpty(mKeyName))
        {
            mEditedKey = SettingsKeyProvider.GetSettingsKeyInfo(mKeyName, mSiteId);

            // Set id of key
            if (mEditedKey != null)
            {
                skeEditKey.SettingsKeyID = mEditedKey.KeyID;
            }
        }
        // Set up creating mode
        else
        {
            if (mParentGroup != null)
            {
                SettingsCategoryInfo parentCat = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(mParentGroup);
                if (parentCat != null)
                {
                    skeEditKey.SelectedGroupID = parentCat.CategoryID;
                }
            }
        }

        // Check if there is something right created to edit.
        if (this.ViewState["newId"] != null)
        {
            skeEditKey.SettingsKeyID = ValidationHelper.GetInteger(this.ViewState["newId"], 0);
        }

        skeEditKey.TreeRefreshUrl = "~/CMSModules/Settings/Development/CustomSettings/CustomSettings_Menu.aspx?treeroot=" + mTreeRoot + "&";
        skeEditKey.HeaderRefreshUrl = "~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Tabs.aspx?selectedtab=keys&treeroot=" + mTreeRoot + "&";
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        string[,] breadcrumbs = new string[2, 4];
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[1, 1] = "";

        // Set bradcrumbs for editing
        if (skeEditKey.SettingsKeyObj != null)
        {
            SettingsCategoryInfo sci = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(skeEditKey.SettingsKeyObj.KeyCategoryID);

            breadcrumbs[0, 0] = sci.CategoryDisplayName;
            breadcrumbs[0, 1] = ResolveUrl("CustomSettingsCategory_Default.aspx?treeroot=" + mTreeRoot + "&categoryid=" + sci.CategoryParentID + "&siteid=" + mSiteId);
            breadcrumbs[1, 0] = skeEditKey.SettingsKeyObj.KeyDisplayName;
        }
        // Set bradcrumbs for creating new key
        else
        {
            if (mParentGroup != null)
            {
                SettingsCategoryInfo parentCat = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(mParentGroup);
                if (parentCat != null)
                {
                    breadcrumbs[0, 0] = parentCat.CategoryDisplayName;
                    breadcrumbs[0, 1] = ResolveUrl("CustomSettingsCategory_Default.aspx?treeroot=" + mTreeRoot + "&categoryid=" + parentCat.CategoryParentID + "&siteid=" + mSiteId);
                    breadcrumbs[1, 0] = GetString("Development.CustomSettings.NewKey");
                }
            }
        }
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }

    #endregion


    #region "Event handler"

    void skeEditKey_OnSaved(object sender, EventArgs e)
    {
        // Store new keyId for use in edit mode.
        this.ViewState.Add("newId", skeEditKey.SettingsKeyObj.KeyID);
    }

    #endregion
}
