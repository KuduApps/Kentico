using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Workflows_Workflow_Steps : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        stepsGrid.OnAction += stepsGrid_OnAction;
        stepsGrid.DataBinding += stepsGrid_DataBinding;

        int workflowId = QueryHelper.GetInteger("workflowid", 0);

        // Control initialization
        InitializeMasterPage(workflowId);

        // Prepare the steps query parameters
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@StepWorkflowID", workflowId);
        stepsGrid.WhereCondition = "StepWorkflowID = @StepWorkflowID";
        stepsGrid.QueryParameters = parameters;

        stepsGrid.OnExternalDataBound += stepsGrid_OnExternalDataBound;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage(int workflowId)
    {
        // Get workflow
        WorkflowInfo wi = WorkflowInfoProvider.GetWorkflowInfo(workflowId);
        // Set edited object
        EditedObject = wi;

        if (wi != null)
        {
            // Check if 'automatically publish changes' is allowed
            if (wi.WorkflowAutoPublishChanges)
            {
                lblInfo.Text = GetString("Development-Workflow_Steps.CustomStepsCanNotBeCreated");
                lblInfo.Visible = true;
            }
            else
            {
                // Set actions
                string[,] actions = new string[1, 8];
                actions[0, 0] = "HyperLink";
                actions[0, 1] = GetString("Development-Workflow_Steps.NewStep");
                actions[0, 3] = "~/CMSModules/Workflows/Workflow_Step_New.aspx?workflowid=" + workflowId;
                actions[0, 5] = GetImageUrl("Objects/CMS_WorkflowStep/add.png");

                CurrentMaster.HeaderActions.Actions = actions;
            }
        }
    }

    #endregion


    #region "Grid events"

    protected static object stepsGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "allowaction":
                GridViewRow container = (GridViewRow)parameter;
                switch (((DataRowView)container.DataItem)["StepName"].ToString().ToLower())
                {
                    case "edit":
                    case "published":
                    case "archived":
                        ((Control)sender).Visible = false;
                        break;
                }
                break;
        }
        return parameter;
    }


    protected void stepsGrid_DataBinding(object sender, EventArgs e)
    {
        stepsGrid.GridView.Sort("StepOrder", SortDirection.Ascending);
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void stepsGrid_OnAction(string actionName, object actionArgument)
    {
        int workflowstepid = Convert.ToInt32(actionArgument);
        if (actionName == "edit")
        {
            URLHelper.Redirect("Workflow_Step_Edit.aspx?workflowstepid=" + workflowstepid);
        }

        else if (actionName == "delete")
        {
            // Check if documents use the workflow
            WorkflowStepInfo si = WorkflowStepInfoProvider.GetWorkflowStepInfo(workflowstepid);
            if (si != null)
            {
                switch (si.StepName.ToLower())
                {
                    case "edit":
                    case "published":
                        // Delete the workflow step
                        WorkflowStepInfoProvider.DeleteWorkflowStepInfo(workflowstepid);
                        break;

                    default:
                        string where = "DocumentWorkflowStepID = " + workflowstepid;

                        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                        DataSet ds = tree.SelectNodes(TreeProvider.ALL_SITES, "/%", TreeProvider.ALL_CULTURES, false, null, where, "SiteName, NodeAliasPath", -1, false);
                        if (!DataHelper.DataSourceIsEmpty(ds))
                        {
                            lblError.Text = GetString("Workflow.CannotDeleteStepUsed") + "<br />";
                            lblError.Visible = true;

                            int index = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (index > 10)
                                {
                                    lblError.Text += "<br />&nbsp;...";
                                    break;
                                }
                                lblError.Text += "<br />&nbsp;" + dr["SiteName"] + " - " + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["DocumentNamePath"], ""));
                                index++;
                            }
                        }
                        else
                        {
                            // Delete the workflow step
                            WorkflowStepInfoProvider.DeleteWorkflowStepInfo(workflowstepid);
                        }
                        break;
                }
            }
        }

        else if (actionName == "moveup")
        {
            WorkflowStepInfoProvider.MoveStepUp(WorkflowStepInfoProvider.GetWorkflowStepInfo(workflowstepid));
        }

        else if (actionName == "movedown")
        {
            WorkflowStepInfoProvider.MoveStepDown(WorkflowStepInfoProvider.GetWorkflowStepInfo(workflowstepid));
        }
    }

    #endregion
}
