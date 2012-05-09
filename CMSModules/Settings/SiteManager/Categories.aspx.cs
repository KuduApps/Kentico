using System;
using System.Data;

using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Settings_SiteManager_Categories : SiteManagerPage
{
    protected int mSiteId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(Page);

        // URL for tree selection
        string script = "var categoryURL = '" + ResolveUrl("keys.aspx") + "';\n";
        script += "var doNotReloadContent = false;\n";

        // Get selected site id
        mSiteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        TreeViewCategories.SiteID = mSiteId;
        TreeViewCategories.RootIsClickable = true;

        bool searchMode = false;
        int categoryId = 0;
        if (Request.Params["selectedCategoryId"] != null)
        {
            // Selected category
            categoryId = ValidationHelper.GetInteger(Request.Params["selectedCategoryId"], 0);
            searchMode = true;

        }
        else
        {
            // First request to Settings
            categoryId = SettingsCategoryInfoProvider.GetRootSettingsCategoryInfo().CategoryID;
            searchMode = SettingsKeyProvider.DevelopmentMode;
        }

        TreeViewCategories.RootIsClickable = SettingsKeyProvider.DevelopmentMode;
        bool reload = QueryHelper.GetBoolean("reload", true);

        // Select category if set
        if ((categoryId > 0) && (searchMode))
        {
            SettingsCategoryInfo sci = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(categoryId);
            if (sci != null)
            {
                // Stop reloading of right frame, if explicitly set
                if (!reload)
                {
                    script += "doNotReloadContent = true;";
                }
                script += SelectAtferLoad(sci.CategoryIDPath, sci.CategoryName, sci.CategoryID, sci.CategoryParentID);
            }
        }
        // If no category specified, select the first category under root by default
        else
        {
            SettingsCategoryInfo sci = SettingsCategoryInfoProvider.GetRootSettingsCategoryInfo();
            TreeViewCategories.RootCategory = sci;

            if (sci != null)
            {
                TreeViewCategories.RootIsClickable = false;
                DataSet ds = SettingsCategoryInfoProvider.GetSettingsCategories(string.Format("CategoryParentID = {0}", sci.CategoryID), "CategoryOrder", 1, "CategoryIDPath, CategoryName, CategoryID, CategoryParentID");
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    DataRow firstRow = ds.Tables[0].Rows[0];
                    script += SelectAtferLoad(SqlHelperClass.GetString(firstRow["CategoryIDPath"], "/"), SqlHelperClass.GetString(firstRow["CategoryName"], "CMS.Settings"), SqlHelperClass.GetInteger(firstRow["CategoryID"], 0), SqlHelperClass.GetInteger(firstRow["CategoryParentID"], 0));
                }

            }
        }

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "SelectCat", ScriptHelper.GetScript(script));

        // Style site selector 
        siteSelector.DropDownSingleSelect.CssClass = "";
        siteSelector.DropDownSingleSelect.Attributes.Add("style", "width: 100%");
        lblSite.Text = GetString("general.site") + ResHelper.Colon;

        // Set site selector
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.AllowAll = false;
        siteSelector.UniSelector.SpecialFields = new string[1, 2] { { GetString("general.global"), "0" } };
        siteSelector.OnlyRunningSites = false;
    }


    /// <summary>
    /// Reloads tree content.
    /// </summary>
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);

        // Reload tree after selected site has changed.
        if (RequestHelper.IsPostBack())
        {
            TreeViewCategories.ReloadData();
        }
    }


    /// <summary>
    /// Expands tree at specified path and selects tree item by javascript.
    /// </summary>
    /// <param name="path">Selected path</param>
    /// <param name="itemName">Codename of selected tree item</param>
    /// <param name="itemId">Item identifier</param>
    /// <param name="parentId">ID of parent</param>    
    private string SelectAtferLoad(string path, string itemName, int itemId, int parentId)
    {
        TreeViewCategories.SelectPath = path;
        TreeViewCategories.SelectedItem = itemName;
        string script = String.Format("SelectNode('{0}'); NodeSelected('', {2}, {3}, {1});", itemName, parentId, itemId, mSiteId);
        return script;
    }
}
