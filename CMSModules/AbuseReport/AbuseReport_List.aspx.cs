using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSImportExport;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_AbuseReport_AbuseReport_List : CMSAbuseReportPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set the page title
        CurrentMaster.Title.TitleText = GetString("abuse.reportabuse");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_AbuseReport/object.png");
        CurrentMaster.Title.HelpTopicName = "abusereport_list";

        siteSelector.DropDownSingleSelect.AutoPostBack = false;

        // Load dropdown list
        if (!RequestHelper.IsPostBack())
        {
            InitializeComponents();
            siteSelector.Value = CMSContext.CurrentSiteID;
        }

        // Create WHERE condition with ReportStatus
        string completeWhere = "";
        if (!String.IsNullOrEmpty(drpStatus.SelectedValue) && (drpStatus.SelectedValue != "-1"))
        {
            completeWhere = SqlHelperClass.AddWhereCondition(completeWhere, "(ReportStatus = " + ValidationHelper.GetInteger(drpStatus.SelectedValue, 0) + ")");
        }

        // Create WHERE condition with ReportTitle
        string txt = txtTitle.Text.Trim().Replace("'", "''");
        if (!string.IsNullOrEmpty(txt))
        {
            completeWhere = SqlHelperClass.AddWhereCondition(completeWhere, "(ReportTitle LIKE '%" + txt + "%')");
        }

        // Create WHERE condition with ReportObjectType
        int siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        if (siteId == 0)
        {
            siteId = CMSContext.CurrentSiteID;
        }
        if (siteId > 0)
        {
            completeWhere = SqlHelperClass.AddWhereCondition(completeWhere, "(ReportSiteID = " + siteId + ")");
        }

        ucAbuseReportList.WhereCondition = completeWhere;
        ucAbuseReportList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(ucAbuseReportList_OnCheckPermissions);
    }


    /// <summary>
    /// Check permission.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="sender">Sender</param>
    void ucAbuseReportList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.AbuseReport", permissionType))
        {
            sender.StopProcessing = true;
            RedirectToAccessDenied("CMS.AbuseReport", permissionType);
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Loads status from enumeration to dropdown list.
    /// </summary>
    private void InitializeComponents()
    {
        drpStatus.Items.Add(new ListItem(GetString("general.selectall"), "-1"));
        drpStatus.Items.Add(new ListItem(GetString("general.new"), "0"));
        drpStatus.Items.Add(new ListItem(GetString("general.solved"), "1"));
        drpStatus.Items.Add(new ListItem(GetString("general.rejected"), "2"));

        // Status preselection by URL
        string preselectedStatus = QueryHelper.GetString("status", null);
        if (!String.IsNullOrEmpty(preselectedStatus))
        {
            foreach (ListItem item in this.drpStatus.Items)
            {
                if (item.Value == preselectedStatus)
                {
                    this.drpStatus.SelectedIndex = this.drpStatus.Items.IndexOf(item);
                }
            }
        }

        // Show site selector only for global admin
        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            // Set site selector
            siteSelector.AllowAll = true;
        }
        else
        {
            plcSites.Visible = false;
        }
    }

    #endregion
}
