using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Reporting;

public partial class CMSModules_Reporting_Tools_ReportCategoryEdit_Header : CMSReportingPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "category_edit_list";
        int categoryID = QueryHelper.GetInteger("CategoryID", 0);

        //// initializes page title control		
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("reporting.reports");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'category_edit_list');";
        tabs[0, 2] = ResolveUrl("Report_List.aspx?CategoryID=" + categoryID);
        tabs[1, 0] = GetString("general.general");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'category_edit');";
        tabs[1, 2] = ResolveUrl("ReportCategory_general.aspx?CategoryID=" + categoryID);
        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "categoryContent";

        //Load categoryID
        ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryID);
        string categoryName = String.Empty;
        if (rci != null)
        {
            categoryName = rci.CategoryDisplayName;
        }

        string[,] bread = new string[2, 4];

        bread[0, 0] = GetString("tools.ui.reporting");
        bread[0, 1] = ResolveUrl("~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx");
        bread[0, 2] = "_parent";
        bread[0, 3] = "if (parent.parent.frames['reportcategorytree']) { parent.parent.frames['reportcategorytree'].location.href = 'reportcategory_tree.aspx'}";

        bread[1, 0] = categoryName;
        bread[1, 1] = "";
        bread[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = bread;
    }
}

