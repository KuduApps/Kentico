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

public partial class CMSModules_Reporting_Tools_ReportCategory_General : CMSReportingPage
{
    protected int mCategoryID = 0;
    bool mIsRoot = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Report category edit";

        rfvCodeName.ErrorMessage = GetString("ReportCategory_Edit.ErrorCodeName");
        rfvDisplayName.ErrorMessage = GetString("ReportCategory_Edit.ErrorDisplayName");

        // control initializations				
        lblCategoryDisplayName.Text = GetString("ReportCategory_Edit.CategoryDisplayNameLabel");
        lblCategoryCodeName.Text = GetString("ReportCategory_Edit.CategoryCodeNameLabel");

        btnOk.Text = GetString("General.OK");

        string currentReportCategory = GetString("ReportCategory_Edit.NewItemCaption");

        // get reportCategory id from querystring		
        mCategoryID = ValidationHelper.GetInteger(Request.QueryString["CategoryID"], 0);
        if (mCategoryID > 0)
        {
            ReportCategoryInfo reportCategoryObj = ReportCategoryInfoProvider.GetReportCategoryInfo(mCategoryID);
            if (reportCategoryObj != null)
            {
                if (reportCategoryObj.CategoryCodeName == "/")
                {
                    plcCategoryName.Visible = false;
                    mIsRoot = true;
                }

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
        // check 'modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }

        string errorMessage = new Validator().NotEmpty(txtCategoryCodeName.Text, GetString("ReportCategory_Edit.ErrorCodeName")).NotEmpty(txtCategoryDisplayName.Text, GetString("ReportCategory_Edit.ErrorDisplayName")).Result;

        //Test codename if root is not edited
        if ((!mIsRoot) && (errorMessage == "") && (!ValidationHelper.IsCodeName(txtCategoryCodeName.Text.Trim())))
        {
            errorMessage = GetString("ReportCategory_Edit.InvalidCodeName");
        }

        if (errorMessage == "")
        {
            ReportCategoryInfo rcCodeNameCheck = ReportCategoryInfoProvider.GetReportCategoryInfo(txtCategoryCodeName.Text.Trim());
            //check reportCategory codename
            if ((rcCodeNameCheck == null) || (rcCodeNameCheck.CategoryID == mCategoryID))
            {
                string script = string.Empty;
                ReportCategoryInfo reportCategoryObj = ReportCategoryInfoProvider.GetReportCategoryInfo(mCategoryID);

                // if reportCategory doesnt already exist, create new one
                if (reportCategoryObj == null)
                {
                    reportCategoryObj = new ReportCategoryInfo();
                }

                if (reportCategoryObj.CategoryDisplayName != txtCategoryDisplayName.Text.Trim())
                {
                    // Refresh the breadcrumb
                    ScriptHelper.RefreshTabHeader(Page, GetString("general.general"));
                }

                reportCategoryObj.CategoryDisplayName = txtCategoryDisplayName.Text.Trim();
                reportCategoryObj.CategoryCodeName = txtCategoryCodeName.Text.Trim();

                ReportCategoryInfoProvider.SetReportCategoryInfo(reportCategoryObj);

                script += @"if (parent.parent.frames['reportcategorytree']) {
                    parent.parent.frames['reportcategorytree'].location.href = 'ReportCategory_tree.aspx?categoryid=" + reportCategoryObj.CategoryID + @"';
                }";

                ltlScript.Text = ScriptHelper.GetScript(script);
               
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
