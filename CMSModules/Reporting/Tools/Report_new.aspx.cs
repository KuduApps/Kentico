using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Reporting;
using CMS.UIControls;

public partial class CMSModules_Reporting_Tools_Report_new : CMSReportingPage
{
    int categoryId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }

        // control initializations				
        rfvReportDisplayName.ErrorMessage = GetString("Report_New.EmptyDisplayName");
        rfvReportName.ErrorMessage = GetString("Report_New.EmptyCodeName");

        lblReportDisplayName.Text = GetString("Report_New.DisplayNameLabel");
        lblReportName.Text = GetString("Report_New.NameLabel");
        lblReportAccess.Text = GetString("Report_New.ReportAccessLabel");

        btnOk.Text = GetString("General.OK");

        categoryId = ValidationHelper.GetInteger(Request.QueryString["categoryId"], 0);

        InitializeMasterPage();
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        //Load category name
        ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryId);
        string categoryName = GetString("Report_New.ReportList");
        if (rci != null)
        {
            categoryName = rci.CategoryDisplayName;
        }

        // Initializes page title label
        string[,] tabs = new string[3, 4];
        tabs[0, 0] = GetString("tools.ui.reporting");
        tabs[0, 1] = ResolveUrl("~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx");
        tabs[0, 2] = "";
        tabs[0, 3] = "if (parent.frames['reportcategorytree']) { parent.frames['reportcategorytree'].location.href = 'reportcategory_tree.aspx'}";

        tabs[1, 0] = categoryName;
        tabs[1, 1] = "~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx?categoryid=" + categoryId;
        tabs[1, 2] = "";

        tabs[2, 0] = GetString("Report_New.NewReport");
        tabs[2, 1] = "";
        tabs[2, 2] = "";

        CurrentMaster.Title.Breadcrumbs = tabs;

        // Initialize title and help
        Title = "Report new";
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "new_report";
        CurrentMaster.Title.TitleText = GetString("report_new.newreport");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_report/new.png");
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }


        string errorMessage = new Validator().NotEmpty(txtReportDisplayName.Text.Trim(), rfvReportDisplayName.ErrorMessage).NotEmpty(txtReportName.Text.Trim(), rfvReportName.ErrorMessage).Result;

        if ((errorMessage == "") && (!ValidationHelper.IsCodeName(txtReportName.Text.Trim())))
        {
            errorMessage = GetString("general.invalidcodename");
        }

        if (ReportCategoryInfoProvider.GetReportCategoryInfo(categoryId) == null)
        {
            errorMessage = GetString("Report_General.InvalidReportCategory");
        }

        ReportAccessEnum reportAccess = ReportAccessEnum.All;
        if (!chkReportAccess.Checked)
        {
            reportAccess = ReportAccessEnum.Authenticated;
        }

        if (errorMessage == "")
        {
            //if report with given name already exists show error message
            if (ReportInfoProvider.GetReportInfo(txtReportName.Text.Trim()) != null)
            {
                lblError.Visible = true;
                lblError.Text = GetString("Report_New.ReportAlreadyExists");
                return;
            }

            ReportInfo ri = new ReportInfo();

            ri.ReportDisplayName = txtReportDisplayName.Text.Trim();
            ri.ReportName = txtReportName.Text.Trim();
            ri.ReportCategoryID = categoryId;
            ri.ReportLayout = "";
            ri.ReportParameters = "";
            ri.ReportAccess = reportAccess;

            ReportInfoProvider.SetReportInfo(ri);

            ltlScript.Text += "<script type=\"text/javascript\">";
            ltlScript.Text += @"if (parent.frames['reportcategorytree'])
                                {
                                    parent.frames['reportcategorytree'].location.href = 'ReportCategory_tree.aspx?reportid=" + ri.ReportID + @"';
                                }
                                if (parent.parent.frames['reportcategorytree'])
                                {
                                    parent.parent.frames['reportcategorytree'].location.href = 'ReportCategory_tree.aspx?reportid=" + ri.ReportID + @"';
                                }
                 this.location.href = 'Report_Edit.aspx?reportId=" + Convert.ToString(ri.ReportID) + @"&saved=1&categoryID=" + categoryId + @"'
                </script>";

        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
