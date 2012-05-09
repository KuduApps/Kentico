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
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Tree : SiteManagerPage, IPostBackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register scripts
        ScriptHelper.RegisterJQuery(this.Page);
        this.RegisterExportScript();

        treeElem.UseGlobalSettings = true;

        // Images
        imgNewCategory.ImageUrl = GetImageUrl("Objects/CMS_WebPartCategory/add.png");
        imgNewWebPart.ImageUrl = GetImageUrl("Objects/CMS_WebPart/add.png");
        imgDeleteItem.ImageUrl = GetImageUrl("Objects/CMS_WebPart/delete.png");
        imgExportObject.ImageUrl = GetImageUrl("Objects/CMS_WebPart/export.png");
        imgCloneWebpart.ImageUrl = GetImageUrl("CMSModules/CMS_WebParts/clone.png");

        // Resource strings
        lnkDeleteItem.Text = GetString("Development-WebPart_Tree.DeleteItem");
        lnkNewCategory.Text = GetString("Development-WebPart_Tree.NewCategory");
        lnkNewWebPart.Text = GetString("Development-WebPart_Tree.NewWebPart");
        lnkExportObject.Text = GetString("Development-WebPart_Tree.ExportObject");
        lnkCloneWebPart.Text = GetString("Development-WebPart_Tree.CloneWebpart");

        // Setup menu action scripts
        lnkNewWebPart.Attributes.Add("onclick", "NewItem('webpart');");
        lnkNewCategory.Attributes.Add("onclick", "NewItem('webpartcategory');");
        lnkDeleteItem.Attributes.Add("onclick", "DeleteItem();");
        lnkExportObject.Attributes.Add("onclick", "ExportObject();");
        lnkCloneWebPart.Attributes.Add("onclick", "CloneWebPart();");

        // Tooltips
        lnkDeleteItem.ToolTip = GetString("Development-WebPart_Tree.DeleteItem");
        lnkNewCategory.ToolTip = GetString("Development-WebPart_Tree.NewCategory");
        lnkNewWebPart.ToolTip = GetString("Development-WebPart_Tree.NewWebPart");
        lnkExportObject.ToolTip = GetString("Development-WebPart_Tree.ExportObject");
        lnkCloneWebPart.ToolTip = GetString("Development-WebPart_Tree.CloneWebpart");

        string script = "var doNotReloadContent = false;\n";

        // URLs for menu actions
        script += "var categoryURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Frameset.aspx") + "';\n";
        script += "var categoryNewURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Category.aspx") + "';\n";
        script += "var webpartURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_Frameset.aspx") + "';\n";
        script += "var newWebpartURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_New.aspx") + "';\n";
        script += "var cloneURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Clone.aspx") + "';\n";
        script += "var doNotReloadContent = false;\n";

        // Script for deleting widget or category
        string delPostback = ControlsHelper.GetPostBackEventReference(this.Page, "##");
        string deleteScript = "function DeleteItem() { \n" +
                                " if ((selectedItemId > 0) && (selectedItemParent > 0) && " +
                                " confirm('" + GetString("general.deleteconfirmation") + "')) {\n " +
                                    delPostback.Replace("'##'", "selectedItemType+';'+selectedItemId+';'+selectedItemParent") + ";\n" +
                                "}\n" +
                              "}\n";
        script += deleteScript;


        // Preselect tree item
        if (!RequestHelper.IsPostBack())
        {
            int categoryId = QueryHelper.GetInteger("categoryid", 0);
            int webpartId = QueryHelper.GetInteger("webpartid", 0);
            bool reload = QueryHelper.GetBoolean("reload", false);

            // Select category
            if (categoryId > 0)
            {
                WebPartCategoryInfo wci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(categoryId);
                if (wci != null)
                {
                    // If not set explicitly stop reloading of right frame
                    if (!reload)
                    {
                        script += "doNotReloadContent = true;";
                    }
                    script += SelectAtferLoad(wci.CategoryPath, categoryId, "webpartcategory", wci.CategoryParentID);
                }
            }
            // Select webpart
            else if (webpartId > 0)
            {
                WebPartInfo wi = WebPartInfoProvider.GetWebPartInfo(webpartId);
                if (wi != null)
                {
                    WebPartCategoryInfo wci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(wi.WebPartCategoryID);
                    if (wci != null)
                    {
                        // If not set explicitly stop reloading of right frame
                        if (!reload)
                        {
                            script += "doNotReloadContent = true;";
                        }
                        string path = wci.CategoryPath + "/" + wi.WebPartName;
                        script += SelectAtferLoad(path, webpartId, "webpart", wi.WebPartCategoryID);
                    }
                }
            }
            // Select root by default
            else
            {
                WebPartCategoryInfo wci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName("/");
                if (wci != null)
                {
                    script += SelectAtferLoad("/", wci.CategoryID, "webpartcategory", 0);
                }
            }
        }

        ltlScript.Text += ScriptHelper.GetScript(script);

        // Special browser class for RTL scrollbars correction
        pnlSubBox.CssClass = BrowserHelper.GetBrowserClass();
    }


    /// <summary>
    /// Handles delete action.
    /// </summary>
    /// <param name="eventArgument">Objecttype(widget or widgetcategory);objectid</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        string[] values = eventArgument.Split(';');
        if ((values != null) && (values.Length == 3))
        {
            int id = ValidationHelper.GetInteger(values[1], 0);
            int parentId = ValidationHelper.GetInteger(values[2], 0);
            string script = String.Empty;

            switch (values[0])
            {
                case "webpart":
                    WebPartInfoProvider.DeleteWebPartInfo(id);
                    break;
                case "webpartcategory":
                    try
                    {
                        WebPartCategoryInfoProvider.DeleteCategoryInfo(id);
                    }
                    catch (Exception ex)
                    {
                        // Make alert with exception message, most probable cause is deleting category with subcategories
                        script = String.Format("alert('{0}');\n", ex.Message);

                        // Current node stays selected
                        parentId = id;
                    }
                    break;
            }

            // Select parent node after delete
            WebPartCategoryInfo wci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(parentId);
            if (wci != null)
            {
                script = SelectAtferLoad(wci.CategoryPath + "/", parentId, "webpartcategory", wci.CategoryParentID) + script;
                ltlScript.Text += ScriptHelper.GetScript(script);
            }

            treeElem.ReloadData();
        }
    }


    /// <summary>
    /// Expands tree at specified path and selects tree item by javascript.
    /// </summary>
    /// <param name="path">Selected path</param>
    /// <param name="itemId">ID of selected tree item</param>
    /// <param name="type">Type of tree item</param>
    /// <param name="parentId">ID of parent</param>    
    private string SelectAtferLoad(string path, int itemId, string type, int parentId)
    {
        treeElem.SelectPath = path;
        string script = String.Format("SelectNode({0},'{1}',{2});", itemId, type, parentId);
        return script;
    }
}
