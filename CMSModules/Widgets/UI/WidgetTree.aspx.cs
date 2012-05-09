using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.FormEngine;


public partial class CMSModules_Widgets_UI_WidgetTree : SiteManagerPage, IPostBackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register scripts
        this.RegisterExportScript();
        ScriptHelper.RegisterJQuery(this.Page);

        // Use only global settings for max tree nodes
        widgetTree.UseGlobalSettings = true;

        // Setup menu action images
        imgNewCategory.ImageUrl = GetImageUrl("Objects/CMS_WidgetCategory/add.png");
        imgNewWidget.ImageUrl = GetImageUrl("Objects/CMS_Widget/add.png");
        imgDeleteItem.ImageUrl = GetImageUrl("Objects/CMS_Widget/delete.png");
        imgExportObject.ImageUrl = GetImageUrl("Objects/CMS_Widget/export.png");
        imgCloneWidget.ImageUrl = GetImageUrl("Objects/CMS_Widget/clone.png");

        // Setup menu action labels
        lnkNewWidget.Text = GetString("widgets.NewWidget");
        lnkDeleteItem.Text = GetString("widgets.DeleteItem");
        lnkNewCategory.Text = GetString("widgets.NewCategory");
        lnkExportObject.Text = GetString("widgets.ExportWidget");
        lnkCloneWidget.Text = GetString("widgets.CloneWidget");

        // Setup menu action scripts
        lnkNewWidget.Attributes.Add("onclick", "NewItem('widget');");
        lnkNewCategory.Attributes.Add("onclick", "NewItem('widgetcategory');");
        lnkDeleteItem.Attributes.Add("onclick", "DeleteItem();");
        lnkExportObject.Attributes.Add("onclick", "ExportObject();");
        lnkCloneWidget.Attributes.Add("onclick", "CloneWidget();");

        // Tooltips
        lnkNewWidget.ToolTip = GetString("widgets.NewWidget");
        lnkDeleteItem.ToolTip = GetString("widgets.DeleteItem");
        lnkNewCategory.ToolTip = GetString("widgets.NewCategory");
        lnkExportObject.ToolTip = GetString("widgets.ExportWidget");
        lnkCloneWidget.ToolTip = GetString("widgets.CloneWidget");

        string script = "var doNotReloadContent = false;\n";

        // URLs for menu actions
        script += "var categoryURL = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Category_Frameset.aspx") + "';\n";
        script += "var categoryNewURL = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetCategory_Edit.aspx") + "';\n";
        script += "var widgetURL = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_Frameset.aspx") + "';\n";
        script += "var newWidgetURL = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_New.aspx") + "';\n";
        script += "var cloneURL = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/Dialogs/Widget_Clone.aspx") + "';\n";
        script += "var webpartSelectorURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPartSelector.aspx") + "';\n";

        // Script for deleting widget or category
        string postbackRef = ControlsHelper.GetPostBackEventReference(this.Page, "##");
        string deleteScript = "function DeleteItem() { \n" +
                                " if ((selectedItemId > 0) && (selectedItemParent > 0)) { " +
                                "   var message = (selectedItemType == 'widgetcategory') ? '" + GetString("widgets.deletecategoryconfirm") + "' : '" + GetString("widgets.deleteconfirm") + "';" +
                                "   if (confirm(message)) {\n " +
                                      postbackRef.Replace("'##'", "'delete;'+selectedItemType+';'+selectedItemId+';'+selectedItemParent") + ";\n" +
                                "   }\n" +
                                " }\n" +
                              "}\n";
        script += deleteScript;

        // Script for new widget
        string newWidgetScript = "function OnSelectWebPart(webpartId) { \n" +
            " if ((webpartId > 0) && (selectedItemId > 0) && (selectedItemType == 'widgetcategory')) { \n" +
            postbackRef.Replace("'##'", "'newwidget;' + webpartId + ';' + selectedItemId") +
            "\n }  }\n";

        script += newWidgetScript;


        // Preselect tree item
        if (!RequestHelper.IsPostBack())
        {
            int categoryId = QueryHelper.GetInteger("categoryid", 0);
            int widgetId = QueryHelper.GetInteger("widgetid", 0);
            bool reload = QueryHelper.GetBoolean("reload", false);

            // Select category
            if (categoryId > 0)
            {
                WidgetCategoryInfo wci = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(categoryId);
                if (wci != null)
                {
                    // If not set explicitly stop reloading of right frame
                    if (!reload)
                    {
                        script += "doNotReloadContent = true;";
                    }
                    script += SelectAtferLoad(wci.WidgetCategoryPath + "/", categoryId, "widgetcategory", wci.WidgetCategoryParentID);
                }
            }
            // Select widget
            else if (widgetId > 0)
            {
                WidgetInfo wi = WidgetInfoProvider.GetWidgetInfo(widgetId);
                if (wi != null)
                {
                    WidgetCategoryInfo wci = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(wi.WidgetCategoryID);
                    if (wci != null)
                    {
                        // If not set explicitly stop reloading of right frame
                        if (!reload)
                        {
                            script += "doNotReloadContent = true;";
                        }
                        string path = wci.WidgetCategoryPath + "/" + wi.WidgetName;
                        script += SelectAtferLoad(path, widgetId, "widget", wi.WidgetCategoryID);
                    }
                }
            }
            // Select root by default
            else
            {
                WidgetCategoryInfo wci = WidgetCategoryInfoProvider.GetWidgetCategoryInfo("/");
                if (wci != null)
                {
                    script += SelectAtferLoad("/", wci.WidgetCategoryID, "widgetcategory", 0);
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
        if ((values != null) && (values.Length > 2))
        {
            string action = values[0];
            int id = 0;
            int parentId = 0;
            string script = String.Empty;
            WidgetCategoryInfo wci = null;

            switch (action)
            {
                case "newwidget":

                    id = ValidationHelper.GetInteger(values[1], 0);
                    parentId = ValidationHelper.GetInteger(values[2], 0);

                    // Create new widget of selected type
                    WidgetInfo wi = NewWidget(id, parentId);
                    if (wi != null)
                    {
                        // Select parent node after delete
                        wci = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(parentId);
                        if (wci != null)
                        {
                            script = SelectAtferLoad(wci.WidgetCategoryPath + "/" + wi.WidgetName, wi.WidgetID, "widget", wi.WidgetCategoryID);
                        }
                    }

                    break;

                // Delete widget or widget category
                case "delete":

                    id = ValidationHelper.GetInteger(values[2], 0);
                    parentId = ValidationHelper.GetInteger(values[3], 0);

                    switch (values[1])
                    {
                        case "widget":
                            WidgetInfoProvider.DeleteWidgetInfo(id);
                            break;
                        case "widgetcategory":
                            try
                            {
                                WidgetCategoryInfoProvider.DeleteWidgetCategoryInfo(id);
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
                    wci = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(parentId);
                    if (wci != null)
                    {
                        script = SelectAtferLoad(wci.WidgetCategoryPath + "/", parentId, "widgetcategory", wci.WidgetCategoryParentID) + script;
                    }
                    break;
            }

            widgetTree.ReloadData();

            ltlScript.Text += ScriptHelper.GetScript(script);
        }
    }


    /// <summary>
    /// Finds unique widget name. Uses base prefix string with incrementing counter.
    /// </summary>
    /// <param name="basePrefix">New code name will start with this string</param>
    /// <param name="maxLength">Maximum length of unique name</param>
    /// <returns>Unique widget code name.</returns>
    private string FindUniqueWidgetName(string basePrefix, int maxLength)
    {
        int i = 0;
        string newName = null;

        // Loop to get unique widget name
        do
        {
            i++;
            string postfix = "_" + i;
            newName = TextHelper.LimitLength(basePrefix, maxLength - postfix.Length, "") + postfix;
        } while (WidgetInfoProvider.GetWidgetInfo(newName) != null);

        return newName;
    }


    /// <summary>
    /// Creates new widget with setting from parent webpart.
    /// </summary>
    /// <param name="parentWebpartId">ID of parent webpart</param>
    /// <param name="categoryId">ID of parent widget category</param>
    /// <returns>Created widget info</returns>
    private WidgetInfo NewWidget(int parentWebpartId, int categoryId)
    {
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(parentWebpartId);

        // Widget cannot be created from inherited webpart
        if ((wpi != null) && (wpi.WebPartParentID == 0))
        {
            // Set widget according to parent webpart
            WidgetInfo wi = new WidgetInfo();
            wi.WidgetName = FindUniqueWidgetName(wpi.WebPartName, 100);
            wi.WidgetDisplayName = wpi.WebPartDisplayName;
            wi.WidgetDescription = wpi.WebPartDescription;
            wi.WidgetDocumentation = wpi.WebPartDocumentation;

            wi.WidgetProperties = FormHelper.GetFormFieldsWithDefaultValue(wpi.WebPartProperties, "visible", "false");

            wi.WidgetWebPartID = parentWebpartId;
            wi.WidgetCategoryID = categoryId;

            // Save new widget to DB
            WidgetInfoProvider.SetWidgetInfo(wi);

            // Get thumbnail image from webpart
            DataSet ds = MetaFileInfoProvider.GetMetaFiles(wpi.WebPartID, PortalObjectType.WEBPART, null, null, null, null, 1);

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                MetaFileInfo mfi = new MetaFileInfo(ds.Tables[0].Rows[0]);
                mfi.Generalized.EnsureBinaryData();
                mfi.MetaFileID = 0;
                mfi.MetaFileGUID = Guid.NewGuid();
                mfi.MetaFileObjectID = wi.WidgetID;
                mfi.MetaFileObjectType = PortalObjectType.WIDGET;

                MetaFileInfoProvider.SetMetaFileInfo(mfi);
            }

            // Return ID of newly created widget
            return wi;
        }

        return null;
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
        widgetTree.SelectPath = path;
        string script = String.Format("SelectNode({0},'{1}',{2});", itemId, type, parentId);
        return script;
    }
}
