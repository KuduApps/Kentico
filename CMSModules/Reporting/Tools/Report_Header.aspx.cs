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
using CMS.Reporting;
using CMS.CMSHelper;
using CMS.UIControls;

// Set edited object
[EditedObject("reporting.report", "reportid")]

// Set help
[Help("view_tab2", "helpTopic")]

public partial class CMSModules_Reporting_Tools_Report_Header : CMSReportingPage
{
    protected int categoryId = 0;
    protected int saved = 0;
    protected ReportInfo ri = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        saved = ValidationHelper.GetInteger(Request.QueryString["saved"], 0);
        categoryId = ValidationHelper.GetInteger(Request.QueryString["categoryId"], 0);

        ReportInfo ri = EditedObject as ReportInfo;

        if (ri != null)
        {
            categoryId = ri.ReportCategoryID;
        }

        this.InitializeMasterPage(ri.ReportDisplayName, categoryId);
        InitalizeMenu(ri.ReportID);
    }


    /// <summary>
    /// Initilizes master page.
    /// </summary>
    protected void InitializeMasterPage(string currentReport, int categoryID)
    {
        // Load category name
        ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryID);
        string categoryName = GetString("Report_Header.ReportList");
        if (rci != null)
        {
            categoryName = rci.CategoryDisplayName;
        }

        InitBreadcrumbs(3);
        SetBreadcrumb(0, GetString("tools.ui.reporting"), ResolveUrl("~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx"), "_parent", "if (parent.parent.frames['reportcategorytree']) { parent.parent.frames['reportcategorytree'].location.href = 'reportcategory_tree.aspx'}");
        SetBreadcrumb(1, categoryName, ResolveUrl("~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx?CategoryID=" + categoryID), "_parent", "if (parent.parent.frames['reportcategorytree']) { parent.parent.frames['reportcategorytree'].location.href = 'reportcategory_tree.aspx?categoryID=" + categoryID + "'}");
        SetBreadcrumb(2, currentReport, null, null, null);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SetSavedTab",
                ScriptHelper.GetScript("function SetSavedTab() { SelTab(3,'',''); SetHelpTopic('helpTopic', 'saved_reports_tab'); } \n"
        ));
    }


    /// <summary>
    /// Initializes user edit menu.
    /// </summary>
    protected void InitalizeMenu(int reportId)
    {
        InitTabs(4, "reportContent");
        SetTab(0, GetString("general.view"), "Report_View.aspx?reportid=" + reportId, "SetHelpTopic('helpTopic', 'view_tab2');");
        SetTab(1, GetString("general.general"), "Report_General.aspx?reportid=" + reportId, "SetHelpTopic('helpTopic', 'general_tab7');");
        SetTab(2, GetString("Report_Header.Parameters"), "Report_Parameters.aspx?reportid=" + reportId, "SetHelpTopic('helpTopic', 'parameters_tab');");
        SetTab(3, GetString("Report_Header.SavedReports"), "./SavedReports/SavedReports_List.aspx?reportid=" + reportId, "SetHelpTopic('helpTopic', 'saved_reports_tab');");

        this.CurrentMaster.Tabs.SelectedTab = saved;
    }
}
