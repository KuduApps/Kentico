using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSModules_Settings_Development_CustomSettings_CustomSettingsCategory_Edit : SiteManagerPage
{
    #region "Variables"

    private string mTreeRoot = "customsettings";
    private int mLinkCatId = -1;
    private int mParentId = -1;
    private int mCategoryId = -1;
    private bool mShowTitle = false;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Id of parent if creating new category.
        mParentId = QueryHelper.GetInteger("parentid", -1);
        // Id of category if editing existing category.
        mCategoryId = QueryHelper.GetInteger("categoryid", -1);
        // Enable showing of title.
        mShowTitle = QueryHelper.GetBoolean("showtitle", false);
        // Set isGroup flag.
        catEdit.IsGroupEdit = QueryHelper.GetBoolean("isgroup", false);
        // Root of tree
        mTreeRoot = QueryHelper.GetText("treeroot", "customsettings");
        if (mTreeRoot == "customsettings")
        {
            catEdit.IsCustom = true;
            catEdit.IncludeRootCategory = true;
        }
        else
        {
            catEdit.IncludeRootCategory = !catEdit.IsGroupEdit;
        }

        if (this.ViewState["newId"] != null)
        {
            mCategoryId = ValidationHelper.GetInteger(this.ViewState["newId"], 0);
        }


        catEdit.OnSaved += new EventHandler(catEdit_OnSaved);

        // Set up export links
        lnkExportCategories.NavigateUrl = ResolveUrl("~/CMSModules/Settings/Development/Export_Settings.aspx?type=categories");
        lnkExportCategories.Text = GetString("settingsedit.category_list.exportcategories");
        lnkExportSettings.NavigateUrl = ResolveUrl("~/CMSModules/Settings/Development/Export_Settings.aspx?type=settings");
        lnkExportSettings.Text = GetString("exportobjects.settings");

        // Set up of root category in parent selector and refreshing
        catEdit.DisplayOnlyCategories = true;
        catEdit.SettingsCategoryID = mCategoryId;
        // Set tree refreshing
        catEdit.TreeRefreshUrl = "~/CMSModules/Settings/Development/CustomSettings/CustomSettings_Menu.aspx?treeroot=" + mTreeRoot + "&";

        // Redirect to editing form only if creating new, else refresh header
        if (mCategoryId <= 0)
        {
            if (catEdit.IsGroupEdit)
            {
                catEdit.ContentRefreshUrl = null;
            }
            else
            {
                catEdit.ContentRefreshUrl = "~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Default.aspx?isediting=1&treeroot=" + mTreeRoot + "&";
            }
        }
        else
        {
            catEdit.HeaderRefreshUrl = "~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Tabs.aspx?selectedtab=" + (catEdit.IsGroupEdit ? "keys" : "general") + "&treeroot=" + mTreeRoot + "&";
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        string[,] breadcrumbs = new string[2, 4];
        breadcrumbs[0, 2] = "customsettingsmain";

        // Get root category: Settings or CustomSettings
        SettingsCategoryInfo customSettingsRoot = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(mTreeRoot == "settings" ? "CMS.Settings" : "CMS.CustomSettings");
        // Parent category info for level up link
        SettingsCategoryInfo parentCategoryInfo = null;

        if (mCategoryId <= 0)
        {
            // Set new title
            string title = GetString(catEdit.IsGroupEdit ? "settings.group.new" : "settingsedit.category_list.newitemcaption");
            if (!catEdit.IsGroupEdit && mShowTitle)
            {
                this.CurrentMaster.Title.TitleText = title;
            }
            catEdit.ShowParentSelector = false;

            if (catEdit.SettingsCategoryObj == null)
            {
                catEdit.RootCategoryID = mParentId;
                parentCategoryInfo = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(mParentId);

                breadcrumbs[1, 0] = title;
            }
            else
            {
                parentCategoryInfo = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(catEdit.SettingsCategoryObj.CategoryParentID);
                breadcrumbs[1, 0] = catEdit.SettingsCategoryObj.CategoryDisplayName;
            }
        }
        else
        {
            SetEditEnabled(false);

            // One level up link category Id, but maximaly to current root
            mLinkCatId = customSettingsRoot.CategoryID == catEdit.SettingsCategoryObj.CategoryParentID ? customSettingsRoot.CategoryID : (catEdit.IsGroupEdit ? catEdit.SettingsCategoryObj.CategoryParentID : catEdit.SettingsCategoryObj.CategoryID);
            parentCategoryInfo = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(mLinkCatId);

            breadcrumbs[1, 0] = GetString(catEdit.IsGroupEdit ? catEdit.SettingsCategoryObj.CategoryDisplayName : "settingsedit.settingscategory.edit.headercaption");

            if (mShowTitle && !catEdit.IsGroupEdit)
            {
                this.CurrentMaster.Title.TitleText = GetString("settingsedit.settingscategory.edit.headercaption");
            }

            if (catEdit.SettingsCategoryObj.CategoryID != customSettingsRoot.CategoryID)
            {
                SetEditEnabled(true);
                // Allow assigning of all categories in edit mode
                catEdit.RootCategoryID = customSettingsRoot != null ? customSettingsRoot.CategoryID : catEdit.SettingsCategoryObj.CategoryParentID;
                catEdit.IsGroupEdit = catEdit.SettingsCategoryObj.CategoryIsGroup;
            }
        }

        // Set up title and breadcrumbs
        if (parentCategoryInfo != null)
        {
            breadcrumbs[0, 0] = ResHelper.LocalizeString(parentCategoryInfo.CategoryDisplayName);
            breadcrumbs[0, 1] = ResolveUrl("~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Default.aspx") + "?treeroot=" + mTreeRoot + "&categoryid=" + parentCategoryInfo.CategoryID;
        }

        if (mCategoryId <= 0 || catEdit.IsGroupEdit)
        {
            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        }
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CustomSettings/object.png");
    }

    #endregion


    #region "Protected methods"

    protected void catEdit_OnSaved(object sender, EventArgs e)
    {
        // Save id of newly created item for edit mode
        if (catEdit.SettingsCategoryObj != null)
        {
            this.ViewState.Add("newId", catEdit.SettingsCategoryObj.CategoryID);
        }
    }


    /// <summary>
    /// Sets visibility of export links and group properties.
    /// </summary>
    protected void SetEditEnabled(bool enabled)
    {
        // Set visibility of editing control
        if (mTreeRoot == "customsettings")
        {
            catEdit.Enabled = enabled;
        }
        else
        {
            // Set visibility of export links
            lnkExportCategories.Visible = !enabled;
            lnkExportSettings.Visible = !enabled;
            catEdit.Visible = enabled;
        }
    }

    #endregion
}

