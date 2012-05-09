using System;

using CMS.WorkflowEngine;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Workflows_Workflow_Step_Header : SiteManagerPage
{
    #region "Variables"

    protected int workflowStepId = 0;
    protected WorkflowStepInfo wsi = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        workflowStepId = QueryHelper.GetInteger("workflowstepid", 0);

        string currentWorkflowStep = null;

        int workflowId = 0;
        if (workflowStepId > 0)
        {
            wsi = WorkflowStepInfoProvider.GetWorkflowStepInfo(workflowStepId);
            // Set edited object
            EditedObject = wsi;

            if (wsi != null)
            {
                workflowId = wsi.StepWorkflowID;
                currentWorkflowStep = WorkflowStepInfoProvider.GetWorkflowStepInfo(workflowStepId).StepDisplayName;
            }
        }

        string workflowStepsUrl = "~/CMSModules/Workflows/Workflow_Steps.aspx?workflowid=" + workflowId + "&showtab=steps";

        string workflowSteps = GetString("Publish.WorkflowSteps");

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        // Initialize PageTitle
        InitializeMasterPage(workflowSteps, workflowStepsUrl, currentWorkflowStep);
    }


    /// <summary>
    /// Initialize page title.
    /// </summary>
    private void InitializeMasterPage(string workflowSteps, string workflowStepsUrl, string currentWorkflowStep)
    {
        // Set master page title
        CurrentMaster.Title.HelpTopicName = "new_step";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Set master page bradcumb element
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = workflowSteps;
        pageTitleTabs[0, 1] = workflowStepsUrl;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentWorkflowStep;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Initializes workflow step edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        if (wsi != null)
        {
            string generalString = GetString("general.general");
            string rolesString = GetString("general.roles");

            string[,] tabs = new string[2, 4];
            tabs[0, 0] = generalString;
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'new_step');";
            tabs[0, 2] = "Workflow_Step_General.aspx?workflowStepId=" + workflowStepId;

            string stepName = wsi.StepName.ToLower();
            if ((stepName != "edit") && (stepName != "published") && (stepName != "archived") && (SiteInfoProvider.GetSitesCount() > 0))
            {
                tabs[1, 0] = rolesString;
                tabs[1, 1] = "SetHelpTopic('helpTopic', 'roles_tab2');";
                tabs[1, 2] = "Workflow_Step_Roles.aspx?workflowStepId=" + workflowStepId;
            }

            CurrentMaster.Tabs.UrlTarget = "wfStepContent";
            CurrentMaster.Tabs.Tabs = tabs;
        }
    }
}
