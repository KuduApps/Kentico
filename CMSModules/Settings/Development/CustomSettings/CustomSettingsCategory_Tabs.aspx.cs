using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Settings_Development_CustomSettings_CustomSettingsCategory_Tabs : SiteManagerPage
{
    #region "Variables"

    protected int mCategoryId = 0;
    protected bool mWholeSettings = false;
    protected string mTreeRoot = "customsettings";
    protected string mRootName = "CMS.CustomSettings";
    protected SettingsCategoryInfo mCategoryInfo = null;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        mCategoryId = QueryHelper.GetInteger("categoryid", 0);
        mTreeRoot = QueryHelper.GetText("treeroot", "customsettings");
        mWholeSettings = mTreeRoot == "settings";
        mRootName = mWholeSettings ? "CMS.Settings" : "CMS.CustomSettings";

        if (mCategoryId > 0)
        {
            mCategoryInfo = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(mCategoryId);
        }
        else
        {
            mCategoryInfo = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(mRootName);
        }

        if (mCategoryInfo != null)
        {
            mCategoryId = mCategoryInfo.CategoryID;
        }

        // Intialize the control
        SetupControl();
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        if (mCategoryInfo != null)
        {
            string[,] breadcrumbs = new string[2, 4];
            // Show title, if root category is chosen, else show bradcrumbs.
            if(mCategoryInfo.CategoryName == mRootName)
            {
                breadcrumbs[0, 0] = GetString("Development.Settings");
                breadcrumbs[0, 1] = "";
                breadcrumbs[0, 2] = "";
                breadcrumbs[1, 0] = ResHelper.LocalizeString(mCategoryInfo.CategoryDisplayName);
                breadcrumbs[1, 1] = "";
                breadcrumbs[1, 2] = "";
            } 
            else
            {
                breadcrumbs[0, 0] = (mWholeSettings ? GetString("Development.Settings") : GetString("Development.CustomSettings"));
                breadcrumbs[0, 1] = ResolveUrl("~/CMSModules/Settings/Development/CustomSettings/Default.aspx?treeroot=" + mTreeRoot);
                breadcrumbs[0, 2] = "frameMain";
                breadcrumbs[1, 0] = ResHelper.LocalizeString(mCategoryInfo.CategoryDisplayName);
                breadcrumbs[1, 1] = "";
                breadcrumbs[1, 2] = "_parent";
                
            }
            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        }
 
        InitalizeTabs();
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitalizeTabs()
    {
        // Collect tabs data
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("Development.CustomSettings.KeysTab");
        tabs[0, 2] = "CustomSettings_List.aspx?treeroot=" + mTreeRoot + "&categoryid=" + mCategoryId;

        tabs[1, 0] = GetString("Development.CustomSettings.GeneralTab");
        tabs[1, 2] = "CustomSettingsCategory_Edit.aspx?treeroot=" + mTreeRoot + "&categoryid=" + mCategoryId;

        // Set the target iFrame
        this.CurrentMaster.Tabs.UrlTarget = "customsettingscategorycontent";

        // Assign tabs data
        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.SelectedTab = QueryHelper.GetString("selectedtab", "") == "general" ? 1 : 0;
    }

    #endregion
}
