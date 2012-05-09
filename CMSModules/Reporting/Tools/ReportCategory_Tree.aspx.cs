using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.ExtendedControls;
using CMS.Reporting;
using CMS.CMSHelper;


public partial class CMSModules_Reporting_Tools_ReportCategory_Tree : CMSReportingPage, IPostBackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.RegisterExportScript();

        //// Images
        imgNewCategory.ImageUrl = GetImageUrl("Objects/CMS_WebPartCategory/add.png");
        imgNewReport.ImageUrl = GetImageUrl("Objects/Reporting_report/add.png");
        imgDeleteItem.ImageUrl = GetImageUrl("Objects/CMS_WebPart/delete.png");
        imgExportObject.ImageUrl = GetImageUrl("Objects/CMS_WebPart/export.png");
        imgCloneReport.ImageUrl = GetImageUrl("CMSModules/CMS_WebParts/clone.png");

        // Resource strings
        lnkDeleteItem.Text = GetString("Development-Report_Tree.DeleteSelected");
        lnkNewCategory.Text = GetString("Development-Report_Tree.NewCategory");
        lnkNewReport.Text = GetString("Development-Report_Tree.NewReport");
        lnkExportObject.Text = GetString("Development-Report_Tree.ExportObject");
        lnkCloneReport.Text = GetString("Development-Report_Tree.CloneReport");

        // Setup menu action scripts
        lnkNewReport.Attributes.Add("onclick", "NewItem('report');");
        lnkNewCategory.Attributes.Add("onclick", "NewItem('reportcategory');");
        lnkDeleteItem.Attributes.Add("onclick", "DeleteItem();");
        lnkExportObject.Attributes.Add("onclick", "ExportObject();");
        lnkCloneReport.Attributes.Add("onclick", "CloneReport();");

        // Widgets
        lnkDeleteItem.ToolTip = GetString("Development-Report_Tree.DeleteSelected");
        lnkNewCategory.ToolTip = GetString("Development-Report_Tree.NewCategory");
        lnkNewReport.ToolTip = GetString("Development-Report_Tree.NewReport");
        lnkExportObject.ToolTip = GetString("Development-Report_Tree.ExportObject");
        lnkCloneReport.ToolTip = GetString("Development-Report_Tree.CloneReport");

        pnlSubBox.CssClass = BrowserHelper.GetBrowserClass();

        //// URLs for menu actions
        string script = "var categoryURL = '" + ResolveUrl("ReportCategory_Edit_Frameset.aspx") + "';\n";
        script += "var reportURL = '" + ResolveUrl("Report_Edit.aspx") + "';\n";
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
            int reportID = QueryHelper.GetInteger("reportid", 0);
            bool reload = QueryHelper.GetBoolean("reload", false);

            // Select category
            if (categoryId > 0)
            {
                ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryId);
                if (rci != null)
                {
                    // If not set explicitly stop reloading of right frame
                    if (!reload)
                    {
                        script += "doNotReloadContent = true;";
                    }
                    script += SelectAtferLoad(rci.CategoryPath, categoryId, "reportcategory", rci.CategoryParentID, true);
                }
            }
            // Select report
            else if (reportID > 0)
            {
                ReportInfo ri = ReportInfoProvider.GetReportInfo(reportID);
                if (ri != null)
                {
                    ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(ri.ReportCategoryID);
                    if (rci != null)
                    {
                        // If not set explicitly stop reloading of right frame
                        if (!reload)
                        {
                            script += "doNotReloadContent = true;";
                        }
                        string path = rci.CategoryPath + "/" + ri.ReportName;
                        script += SelectAtferLoad(path, reportID, "report", ri.ReportCategoryID, true);
                    }
                }
            }
            // Select root by default
            else
            {
                ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo("/");
                if (rci != null)
                {
                    script += SelectAtferLoad("/", rci.CategoryID, "reportcategory", 0, true);
                }

                // Directly dispatch an action, if set by URL
                if (QueryHelper.GetString("action", null) == "newcategory")
                {
                    script += "NewItem('reportcategory');";
                }
            }
        }

        ltlScript.Text += ScriptHelper.GetScript(script);
    }


    /// <summary>
    /// Handles delete action.
    /// </summary>
    /// <param name="eventArgument">Objecttype(report or reportcategory);objectid</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        string[] values = eventArgument.Split(';');
        if ((values != null) && (values.Length == 3))
        {
            int id = ValidationHelper.GetInteger(values[1], 0);
            int parentId = ValidationHelper.GetInteger(values[2], 0);
            ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(parentId);

            //Test permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
            {
                String denyScript = @"var redirected = false;if (!redirected) {if ((window.parent != null) && (window.parent.frames['reportcategorytree'] != null)) {
                parent.frames['reportedit'].location = '" + ResolveUrl(AccessDeniedPage) + @"?resource=cms.reporting&permission=Modify';redirected = true;         
                }}";
                if (rci != null)
                {
                    denyScript = SelectAtferLoad(rci.CategoryPath + "/", id, values[0], parentId, false) + denyScript;
                }
                ltlScript.Text += ScriptHelper.GetScript(denyScript);
                return;
            }

            string script = String.Empty;

            switch (values[0])
            {
                case "report":
                    ReportInfoProvider.DeleteReportInfo(id);
                    break;
                case "reportcategory":
                    try
                    {
                        ReportCategoryInfoProvider.DeleteReportCategoryInfo(id);
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
            if (rci != null)
            {
                script = SelectAtferLoad(rci.CategoryPath + "/", parentId, "reportcategory", rci.CategoryParentID, true) + script;
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
    /// <param name="type">Type of tree item</param>
    /// <param name="updateRightPanel">Indicates whether update right panel</param>    
    private string SelectAtferLoad(string path, int itemId, string type, int parentId, bool updateRightPanel)
    {
        treeElem.SelectPath = path;
        string script = String.Format("SelectReportNode({0},'{1}',{2},'" + updateRightPanel.ToString() + "');", itemId, type, parentId);
        return script;
    }
}

