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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Reporting;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Reporting_Tools_ReportCategory_New : CMSReportingPage
{
    protected int categoryID = 0;
    protected int parentCategoryID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvCodeName.ErrorMessage = GetString("ReportCategory_Edit.ErrorCodeName");
        rfvDisplayName.ErrorMessage = GetString("ReportCategory_Edit.ErrorDisplayName");

        // control initializations				
        lblCategoryDisplayName.Text = GetString("ReportCategory_Edit.CategoryDisplayNameLabel");
        lblCategoryCodeName.Text = GetString("ReportCategory_Edit.CategoryCodeNameLabel");

        btnOk.Text = GetString("General.OK");

        string currentReportCategory = GetString("ReportCategory_Edit.NewItemCaption");

        // get reportCategory id from querystring		
        categoryID = ValidationHelper.GetInteger(Request.QueryString["CategoryID"], 0);
        parentCategoryID = ValidationHelper.GetInteger(Request.QueryString["parentCategoryID"], 0);

        if (categoryID > 0)
        {
            ReportCategoryInfo reportCategoryObj = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryID);
            if (reportCategoryObj != null)
            {
                currentReportCategory = reportCategoryObj.CategoryDisplayName;

                // fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(reportCategoryObj);

                    // show that the reportCategory was created or updated successfully
                    if (ValidationHelper.GetString(Request.QueryString["saved"], "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }

        this.InitializeMasterPage(currentReportCategory, parentCategoryID);
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage(string currentReportCategory, int parentCategoryID)
    {
        // Initialize help and title
        this.Title = "Report category edit";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "category_edit";

        //Load category name
        ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(parentCategoryID);
        string categoryName = GetString("reporting.reports");
        if (rci != null)
        {
            categoryName = rci.CategoryDisplayName;
        }

        // initializes breadcrumbs		
        string[,] tabs = new string[3, 4];

        tabs[0, 0] = GetString("tools.ui.reporting");
        tabs[0, 1] = ResolveUrl("~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx");
        tabs[0, 2] = "";
        tabs[0, 3] = "if (parent.frames['reportcategorytree']) { parent.frames['reportcategorytree'].location.href = 'reportcategory_tree.aspx'}";

        tabs[1, 0] = categoryName;
        tabs[1, 1] = "~/CMSModules/Reporting/Tools/ReportCategory_Edit_Frameset.aspx?categoryID=" + parentCategoryID;
        tabs[1, 2] = "";

        tabs[2, 0] = currentReportCategory;
        tabs[2, 1] = "";
        tabs[2, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = tabs;

        this.CurrentMaster.Title.TitleText = GetString("ReportCategory_Edit.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportCategory/new.png");
    }


    /// <summary>
    /// Load data of editing reportCategory.
    /// </summary>
    /// <param name="reportCategoryObj">ReportCategory object</param>
    protected void LoadData(ReportCategoryInfo reportCategoryObj)
    {
        txtCategoryDisplayName.Text = reportCategoryObj.CategoryDisplayName;
        txtCategoryCodeName.Text = reportCategoryObj.CategoryCodeName;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }

        string errorMessage = new Validator().NotEmpty(txtCategoryCodeName.Text, GetString("ReportCategory_Edit.ErrorCodeName")).NotEmpty(txtCategoryDisplayName.Text, GetString("ReportCategory_Edit.ErrorDisplayName")).Result;

        if ((errorMessage == "") && (!ValidationHelper.IsCodeName(txtCategoryCodeName.Text.Trim())))
        {
            errorMessage = GetString("ReportCategory_Edit.InvalidCodeName");
        }

        if (errorMessage == "")
        {
            ReportCategoryInfo rcCodeNameCheck = ReportCategoryInfoProvider.GetReportCategoryInfo(txtCategoryCodeName.Text.Trim());
            //check reportCategory codename
            if ((rcCodeNameCheck == null) || (rcCodeNameCheck.CategoryID == categoryID))
            {
                ReportCategoryInfo reportCategoryObj = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryID);
                // if reportCategory doesnt already exist, create new one
                if (reportCategoryObj == null)
                {
                    reportCategoryObj = new ReportCategoryInfo();
                }

                reportCategoryObj.CategoryDisplayName = txtCategoryDisplayName.Text.Trim();
                reportCategoryObj.CategoryCodeName = txtCategoryCodeName.Text.Trim();
                reportCategoryObj.CategoryParentID = parentCategoryID;

                ReportCategoryInfoProvider.SetReportCategoryInfo(reportCategoryObj);

                ltlScript.Text += "<script type=\"text/javascript\">";
                ltlScript.Text += @"if (parent.frames['reportcategorytree']) {
                parent.frames['reportcategorytree'].location.href = 'ReportCategory_tree.aspx?categoryid=" + reportCategoryObj.CategoryID + @"';
                    }
                 this.location.href = 'ReportCategory_edit_Frameset.aspx?CategoryID=" + Convert.ToString(reportCategoryObj.CategoryID) + @"&saved=1';
                </script>";

            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("ReportCategory_Edit.ReportCategoryAlreadyExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
