using System;
using System.Data;

using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Workflows_Workflow_Step_Roles : SiteManagerPage
{
    #region "Protected variables"

    protected int workflowStepId = 0;
    private int siteId = 0;
    protected string currentValues = string.Empty;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        lblSite.Text = GetString("Administration-User_Edit_Sites.SelectSite");

        workflowStepId = QueryHelper.GetInteger("workflowStepId", 0);

        // Set site selector
        siteSelector.AllowGlobal = true;
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.AllowAll = false;
        siteSelector.OnlyRunningSites = false;
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
        usRoles.OnSelectionChanged += usRoles_OnSelectionChanged;

        if (!RequestHelper.IsPostBack())
        {
            siteId = CMSContext.CurrentSiteID;
            siteSelector.Value = siteId;
        }
        else
        {
            siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        }

        // If global role selected - set siteID to 0
        if (siteSelector.GlobalRecordValue == siteId.ToString())
        {
            siteId = 0;
        }

        string siteIDWhere = (siteId == 0) ? "SiteID IS NULL" : "SiteID = " + siteId;
        usRoles.WhereCondition = siteIDWhere + " AND RoleGroupID IS NULL";

        // Get the active roles for this site
        DataSet ds = WorkflowStepInfoProvider.GetStepRoles(workflowStepId);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "RoleID"));
        }

        if (!RequestHelper.IsPostBack())
        {
            usRoles.Value = currentValues;
        }
    }

    protected void usRoles_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveData();
    }

    #endregion


    #region "Control handling"

    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        pnlUpdate.Update();
    }


    private void SaveData()
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(usRoles.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);
                    // If role is authorized, unauthorize it
                    if (IsRoleAuthorized(roleID.ToString(), workflowStepId))
                    {
                        WorkflowStepRoleInfoProvider.RemoveRoleFromWorkflowStep(workflowStepId, roleID);
                    }
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);

                    // If role is not authorized, authorize it
                    if (!IsRoleAuthorized(roleID.ToString(), workflowStepId))
                    {
                        WorkflowStepRoleInfoProvider.AddRoleToWorkflowStep(workflowStepId, roleID);
                    }
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }

    #endregion


    #region "Protected methods"

    protected bool IsRoleAuthorized(string roleId, int wfStepId)
    {
        DataSet stepsRolesSet = WorkflowStepInfoProvider.GetStepRoles(wfStepId);

        if (!DataHelper.DataSourceIsEmpty(stepsRolesSet))
        {
            foreach (DataRow stepRole in stepsRolesSet.Tables[0].Rows)
            {
                if (stepRole["RoleID"].ToString() == roleId)
                {
                    return true;
                }
            }
        }
        return false;
    }

    #endregion
}
