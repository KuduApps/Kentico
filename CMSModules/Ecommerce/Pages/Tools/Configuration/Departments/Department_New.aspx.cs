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
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_Departments_Department_New : CMSDepartmentsPage
{
    protected int mDepartmentId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        // field validator error messages initialization
        rfvDisplayName.ErrorMessage = GetString("Department_Edit.errorEmptyDisplayName");
        rfvCodeName.ErrorMessage = GetString("Department_Edit.errorEmptyCodeName");

        // control initializations				
        lblDepartmentName.Text = GetString("Department_Edit.DepartmentNameLabel");
        lblDepartmentDisplayName.Text = GetString("Department_Edit.DepartmentDisplayNameLabel");

        btnOk.Text = GetString("General.OK");

        // initializes page title control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Department_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/Departments/Department_List.aspx?siteId=" + SiteID;
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = FormatBreadcrumbObjectName(GetString("Department_Edit.NewItemCaption"), ConfiguredSiteID);
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.TitleText = GetString("Department_Edit.NewItemCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Department/new.png");

        this.CurrentMaster.Title.HelpTopicName = "departments_new";
        this.CurrentMaster.Title.HelpName = "helpTopic";

    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check permissions
        CheckConfigurationModification(ConfiguredSiteID);

        // Check input values from textboxes and other controls
        string errorMessage = new Validator()
            .NotEmpty(txtDepartmentDisplayName.Text.Trim(), GetString("Department_Edit.errorEmptyDisplayName"))
            .NotEmpty(txtDepartmentName.Text.Trim(), GetString("Department_Edit.errorEmptyCodeName")).Result;

        if (!ValidationHelper.IsCodeName(txtDepartmentName.Text.Trim()))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }

        if (errorMessage == "")
        {
            // DepartmentName must be unique
            DepartmentInfo departmentObj = null;
            string siteWhere = (ConfiguredSiteID > 0) ? " AND (DepartmentSiteID = " + ConfiguredSiteID + " OR DepartmentSiteID IS NULL)" : "";
            DataSet ds = DepartmentInfoProvider.GetDepartments("DepartmentName = '" + txtDepartmentName.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                departmentObj = new DepartmentInfo(ds.Tables[0].Rows[0]);
            }

            // If departmentName value is unique														
            if ((departmentObj == null) || (departmentObj.DepartmentID == mDepartmentId))
            {
                departmentObj = new DepartmentInfo();

                departmentObj.DepartmentID = mDepartmentId;
                departmentObj.DepartmentName = txtDepartmentName.Text.Trim();
                departmentObj.DepartmentDisplayName = txtDepartmentDisplayName.Text.Trim();
                departmentObj.DepartmentSiteID = ConfiguredSiteID;

                DepartmentInfoProvider.SetDepartmentInfo(departmentObj);

                URLHelper.Redirect("Department_Frameset.aspx?departmentId=" + Convert.ToString(departmentObj.DepartmentID) + "&saved=1&siteId="+SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Department_Edit.DepartmentNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
