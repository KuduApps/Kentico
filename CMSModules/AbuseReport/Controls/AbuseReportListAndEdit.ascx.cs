using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSModules_AbuseReport_Controls_AbuseReportListAndEdit : CMSAdminControl
{
    #region "Properties"

    /// <summary>
    /// Where condition for grid.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ucAbuseReportList.WhereCondition;
        }
        set
        {
            ucAbuseReportList.WhereCondition = value;
        }
    }


    /// <summary>
    /// Items per page for grid.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return ucAbuseReportList.ItemsPerPage;
        }
        set
        {
            ucAbuseReportList.ItemsPerPage = value;
        }
    }


    /// <summary>
    /// Site name filter.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ucAbuseReportList.SiteName;
        }
        set
        {
            ucAbuseReportList.SiteName = value;
        }
    }


    /// <summary>
    /// Status of abuse report.
    /// </summary>
    public int Status
    {
        get
        {
            return ucAbuseReportList.Status;
        }
        set
        {
            ucAbuseReportList.Status = value;
        }
    }


    /// <summary>
    /// Order by for uni grid.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ucAbuseReportList.OrderBy;
        }
        set
        {
            ucAbuseReportList.OrderBy = value;
        }
    }


    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            ucAbuseEdit.StopProcessing = value;
            ucAbuseReportList.StopProcessing = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        ucAbuseReportList.UseEditOnPostback = true;
       
        if (!String.IsNullOrEmpty(hfEditReport.Value))
        {
            ucAbuseEdit.ReportID = ValidationHelper.GetInteger(hfEditReport.Value, 0);            
        }

        // Edit control must have stop processing true if list (first edit will be executed in prerender) to handle ItemEdit properly
        if (ucAbuseEdit.ReportID == 0)
        {
            ucAbuseEdit.StopProcessing = true;
        }

        lnkEditBack.Click += new EventHandler(lnkEditBack_Click);
        ucAbuseReportList.OnCheckPermissions += new CheckPermissionsEventHandler(ucAbuseReportList_OnCheckPermissions);
        ucAbuseEdit.OnCheckPermissions += new CheckPermissionsEventHandler(ucAbuseReportList_OnCheckPermissions);
    }


    /// <summary>
    /// Check permissions.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="sender">Sender</param>
    void ucAbuseReportList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }


    protected override void OnPreRender(EventArgs e)
    {
        if ((ucAbuseReportList.EditReportID != 0) || (!String.IsNullOrEmpty(hfEditReport.Value)))
        {
            ucAbuseEdit.Visible = true;
            ucAbuseReportList.Visible = false;
            ucAbuseReportList.StopProcessing = true;            
            ucAbuseEdit.StopProcessing = false;

            int reportID = 0;

            ucAbuseEdit.ReportID = ucAbuseReportList.EditReportID;

            bool editForceLoad = false;
            if (ucAbuseEdit.ReportID != 0)
            {
                hfEditReport.Value = ucAbuseEdit.ReportID.ToString();
                reportID = ucAbuseEdit.ReportID;
                editForceLoad = true;
            }

            ucAbuseEdit.ReloadData(editForceLoad);

            //Breadcrumbs
            lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";
            lnkEditBack.Text = GetString("abuse.reports");            
            
            if (!String.IsNullOrEmpty(hfEditReport.Value))
            {
                reportID = ValidationHelper.GetInteger(hfEditReport.Value,0);
            }

            AbuseReportInfo ari = AbuseReportInfoProvider.GetAbuseReportInfo(reportID);
            if (ari != null)
            {
                lblEditNew.Text = HTMLHelper.HTMLEncode(ari.ReportTitle);
            }
            pnlHeader.Visible = true;

        }
        base.OnPreRender(e);
    }


    void lnkEditBack_Click(object sender, EventArgs e)
    {
        hfEditReport.Value = String.Empty;
        ucAbuseEdit.Visible = false;
        ucAbuseReportList.Visible = true;
        pnlHeader.Visible = false;
    }
}
