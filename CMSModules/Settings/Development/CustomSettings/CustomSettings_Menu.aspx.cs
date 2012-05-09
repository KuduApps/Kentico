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
using System.Text;
using System.Text.RegularExpressions;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Settings_Development_CustomSettings_CustomSettings_Menu : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(this.Page);

        // Root of tree flag
        string treeRoot = QueryHelper.GetString("treeroot", "customsettings");

        // Decide which category to use as root: custom settings or settings
        switch (treeRoot)
        {
            case ("settings"):
                treeSettings.CategoryName = "CMS.Settings";
                treeSettings.RootIsClickable = true;
                this.menuElem.WholeSettings = true;
                break;
            case ("customsettings"):
            default:
                treeSettings.CategoryName = "CMS.CustomSettings";
                this.menuElem.WholeSettings = false;
                break;
        }

        this.menuElem.AfterAction += new OnActionEventHandler(menuElem_AfterAction);

        // Prepare root info
        SettingsCategoryInfo rootCat = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(treeSettings.CategoryName);

        StringBuilder sb = new StringBuilder("");
        StringBuilder sbAfter = new StringBuilder("");
        // Create links for JS
        sb.Append("var frameURL = '").Append(ResolveUrl("~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Default.aspx?treeroot=" + ScriptHelper.GetString(treeRoot, false))).Append("';");
        sb.Append("var newURL = '").Append(ResolveUrl("~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Edit.aspx?treeroot=" + ScriptHelper.GetString(treeRoot, false))).Append("';");
        sb.Append("var rootParentId = ").Append(rootCat != null ? rootCat.CategoryParentID : 0).Append(";");

        // Disable delete on custom settings category
        if (treeRoot == "settings")
        {
            DataSet ds = SettingsCategoryInfoProvider.GetSettingsCategories("CategoryName = 'CMS.CustomSettings'", null, 1, "CategoryID");
            int customSettingsId = 0;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                customSettingsId = SqlHelperClass.GetInteger(ds.Tables[0].Rows[0]["CategoryID"], 0);
            }
            sb.Append("var customSettingsId = ").Append(customSettingsId).Append(";");
        }
        else
        {
            sb.Append("var customSettingsId = 0;");
        }


        if (!RequestHelper.IsPostBack())
        {
            int categoryId = QueryHelper.GetInteger("categoryid", -1);
            treeSettings.SelectPath = "/";

            SettingsCategoryInfo category = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(categoryId);
            // Select requested category
            if (category != null)
            {
                treeSettings.SelectPath = category.CategoryIDPath;
                this.menuElem.ElementID = category.CategoryID;
                this.menuElem.ParentID = category.CategoryParentID;
                this.menuElem.Value = category.CategoryID + "|" + category.CategoryParentID;

                // Select category after page load
                sbAfter.Append("SelectNode(" + ScriptHelper.GetString(category.CategoryName) + ");");
                sbAfter.Append("enableMenu(").Append(category.CategoryID).Append(",").Append(category.CategoryParentID).Append(");");
            }
            //  Select root
            else
            {
                if (rootCat != null)
                {
                    this.menuElem.ElementID = rootCat.CategoryID;
                    this.menuElem.ParentID = rootCat.CategoryParentID;
                    this.menuElem.Value = rootCat.CategoryID + "|" + rootCat.CategoryParentID;
                }

                // Select category after page load
                sbAfter.Append("SelectNode(").Append(ScriptHelper.GetString(treeSettings.CategoryName)).Append(");");
                sbAfter.Append("enableMenu(").Append(rootCat.CategoryID).Append(",").Append(rootCat.CategoryParentID).Append(");");
            }
        }
        sb.Append("var postParentId = ").Append(rootCat.CategoryParentID).Append(";");

        this.ltlScript.Text = ScriptHelper.GetScript(sb.ToString());
        this.ltlAfterScript.Text = ScriptHelper.GetScript(sbAfter.ToString());
    }


    /// <summary>
    /// Raised after menu action (new, delete, up or down) has been taken.
    /// </summary>
    protected void menuElem_AfterAction(string actionName, object actionArgument)
    {
        string[] split = actionArgument.ToString().Split('|');
        int categoryId = ValidationHelper.GetInteger(split[0], 0);

        SettingsCategoryInfo category = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(categoryId);
        if (category != null)
        {
            treeSettings.SelectPath = category.CategoryIDPath;
            treeSettings.SelectedItem = category.CategoryName;
            switch (actionName.ToLower())
            {
                case "delete":
                    treeSettings.ExpandPath = category.CategoryIDPath + "/";
                    // Reload header and content after save
                    StringBuilder sb = new StringBuilder();

                    sb.Append("if (window.parent != null) {");
                    sb.Append("if (window.parent.frames['customsettingsmain'] != null) {");
                    sb.Append("window.parent.frames['customsettingsmain'].location = '" + ResolveUrl("CustomSettingsCategory_Default.aspx") + "?categoryid=" + categoryId + "';");
                    sb.Append("}");
                    sb.Append("}");

                    this.ltlScript.Text += ScriptHelper.GetScript(sb.ToString());
                    treeSettings.ExpandPath = category.CategoryIDPath + "/";

                    // Update menu actions parameters
                    this.menuElem.Value = category.CategoryID + "|" + category.CategoryParentID;
                    break;

                case "moveup":
                    if (split.Length == 2)
                    {
                        this.ltlScript.Text += ScriptHelper.GetScript("window.tabIndex = " + split[1] + ";");
                    }
                    break;

                case "movedown":
                    if (split.Length == 2)
                    {
                        this.ltlScript.Text += ScriptHelper.GetScript("window.tabIndex = " + split[1] + ";");
                    }
                    break;
            }
            this.ltlScript.Text += ScriptHelper.GetScript("SelectNode(" + ScriptHelper.GetString(category.CategoryName) + ");");
            this.ltlScript.Text += ScriptHelper.GetScript("var postParentId = " + category.CategoryParentID + ";");

            // Load data
            treeSettings.ReloadData();
        }
    }
}

