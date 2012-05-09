using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

[Title(Text = "Workflows", ImageUrl = "Objects/CMS_Workflow/object.png")]
public partial class CMSAPIExamples_Code_Development_Workflows_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Workflow
        this.apiCreateWorkflow.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateWorkflow);
        this.apiGetAndUpdateWorkflow.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateWorkflow);
        this.apiGetAndBulkUpdateWorkflows.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateWorkflows);
        this.apiDeleteWorkflow.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteWorkflow);

        // Workflow step
        this.apiCreateWorkflowStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateWorkflowStep);
        this.apiAddRoleToStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddRoleToStep);
        this.apiRemoveRoleFromStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveRoleFromStep);
        this.apiDeleteWorkflowStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteWorkflowStep);

        // Workflow scope
        this.apiCreateWorkflowScope.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateWorkflowScope);
        this.apiGetAndUpdateWorkflowScope.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateWorkflowScope);
        this.apiDeleteWorkflowScope.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteWorkflowScope);

    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Workflow
        this.apiCreateWorkflow.Run();
        this.apiGetAndUpdateWorkflow.Run();
        this.apiGetAndBulkUpdateWorkflows.Run();

        // Workflow step
        this.apiCreateWorkflowStep.Run();
        this.apiAddRoleToStep.Run();

        // Workflow scope
        this.apiCreateWorkflowScope.Run();
        this.apiGetAndUpdateWorkflowScope.Run();

    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Workflow scope
        this.apiDeleteWorkflowScope.Run();

        // Workflow step
        this.apiRemoveRoleFromStep.Run();
        this.apiDeleteWorkflowStep.Run();

        // Workflow
        this.apiDeleteWorkflow.Run();
    }

    #endregion


    #region "API examples - Workflow"

    /// <summary>
    /// Creates workflow. Called when the "Create workflow" button is pressed.
    /// </summary>
    private bool CreateWorkflow()
    {
        // Create new workflow object
        WorkflowInfo newWorkflow = new WorkflowInfo();

        // Set the properties
        newWorkflow.WorkflowDisplayName = "My new workflow";
        newWorkflow.WorkflowName = "MyNewWorkflow";

        // Save the workflow
        WorkflowInfoProvider.SetWorkflowInfo(newWorkflow);

        // Create the three default workflow steps
        WorkflowStepInfoProvider.CreateDefaultWorkflowSteps(newWorkflow.WorkflowID);

        return true;
    }


    /// <summary>
    /// Gets and updates workflow. Called when the "Get and update workflow" button is pressed.
    /// Expects the CreateWorkflow method to be run first.
    /// </summary>
    private bool GetAndUpdateWorkflow()
    {
        // Get the workflow
        WorkflowInfo updateWorkflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");
        if (updateWorkflow != null)
        {
            // Update the properties
            updateWorkflow.WorkflowDisplayName = updateWorkflow.WorkflowDisplayName.ToLower();

            // Save the changes
            WorkflowInfoProvider.SetWorkflowInfo(updateWorkflow);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates workflows. Called when the "Get and bulk update workflows" button is pressed.
    /// Expects the CreateWorkflow method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateWorkflows()
    {
        // Prepare the parameters
        string where = "WorkflowName LIKE N'MyNewWorkflow%'";

        // Get the data
        DataSet workflows = WorkflowInfoProvider.GetWorkflows(where, null, 0, null);
        if (!DataHelper.DataSourceIsEmpty(workflows))
        {
            // Loop through the individual items
            foreach (DataRow workflowDr in workflows.Tables[0].Rows)
            {
                // Create object from DataRow
                WorkflowInfo modifyWorkflow = new WorkflowInfo(workflowDr);

                // Update the properties
                modifyWorkflow.WorkflowDisplayName = modifyWorkflow.WorkflowDisplayName.ToUpper();

                // Save the changes
                WorkflowInfoProvider.SetWorkflowInfo(modifyWorkflow);
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Deletes workflow. Called when the "Delete workflow" button is pressed.
    /// Expects the CreateWorkflow method to be run first.
    /// </summary>
    private bool DeleteWorkflow()
    {
        // Get the workflow
        WorkflowInfo deleteWorkflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");

        // Delete the workflow
        WorkflowInfoProvider.DeleteWorkflowInfo(deleteWorkflow);

        return (deleteWorkflow != null);
    }

    #endregion


    #region "API Examples - Workflow step"

    /// <summary>
    /// Creates a new workflow step. Called when the "Create workflow step" button is pressed.
    /// Expects the CreateWorkflow method to be run first.
    /// </summary>
    private bool CreateWorkflowStep()
    {
        // Get the workflow
        WorkflowInfo myWorkflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");
        if (myWorkflow != null)
        {
            // Create new workflow step object
            WorkflowStepInfo newStep = new WorkflowStepInfo();

            // Set the properties
            newStep.StepWorkflowID = myWorkflow.WorkflowID;
            newStep.StepName = "MyNewWorkflowStep";
            newStep.StepDisplayName = "My new workflow step";
            newStep.StepOrder = 1;

            // Save the step into database
            WorkflowStepInfoProvider.SetWorkflowStepInfo(newStep);

            // Ensure correct step order
            WorkflowStepInfoProvider.InitStepOrders(newStep.StepWorkflowID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Assings the CMS Editors role to a workflow step. Called when the "Add role to step" button is pressed.
    /// Expects the CreateWorkflow and CreateWorkflowStep methods to be run first.
    /// </summary>
    private bool AddRoleToStep()
    {
        // Get the workflow
        WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");

        if (workflow != null)
        {
            // Get the custom step
            WorkflowStepInfo step = WorkflowStepInfoProvider.GetWorkflowStepInfo("MyNewWorkflowStep", workflow.WorkflowID);

            if (step != null)
            {
                // Get the role to be assigned to the step
                RoleInfo role = RoleInfoProvider.GetRoleInfo("CMSEditor", CMSContext.CurrentSiteID);

                if (role != null)
                {
                    // Make the assignment
                    WorkflowStepRoleInfoProvider.AddRoleToWorkflowStep(step.StepID, role.RoleID);

                    return true;
                }
                else
                {
                    // Role was not found
                    apiAddRoleToStep.ErrorMessage = "Role 'CMS Editors' was not found.";
                }
            }
            else
            {
                // Step was not found
                apiAddRoleToStep.ErrorMessage = "Step 'My new workflow step' was not found.";
            }
        }

        return false;
    }

    /// <summary>
    /// Removes the assignment of the CMS Editors role from a workflow step. Called when the "Remove role from step" button is pressed.
    /// Expects the CreateWorkflow, CreateWorkflowStep and AddRoleToStep methods to be run first.
    /// </summary>
    private bool RemoveRoleFromStep()
    {
        // Get the workflow
        WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");

        if (workflow != null)
        {
            // Get the custom step
            WorkflowStepInfo step = WorkflowStepInfoProvider.GetWorkflowStepInfo("MyNewWorkflowStep", workflow.WorkflowID);

            if (step != null)
            {
                // Get the role to be assigned to the step
                RoleInfo role = RoleInfoProvider.GetRoleInfo("CMSEditor", CMSContext.CurrentSiteID);

                if (role != null)
                {
                    // Get the step - role relationship
                    WorkflowStepRoleInfo stepRoleInfo = WorkflowStepRoleInfoProvider.GetWorkflowStepRoleInfo(step.StepID, role.RoleID);

                    if (stepRoleInfo != null)
                    {
                        // Remove the assignment
                        WorkflowStepRoleInfoProvider.RemoveRoleFromWorkflowStep(step.StepID, role.RoleID);

                        return true;
                    }
                    else
                    {
                        // The role is not assigned to the step
                        apiRemoveRoleFromStep.ErrorMessage = "The 'CMS Editors' role is not assigned to the step.";
                    }
                }
                else
                {
                    // The role was not found
                    apiRemoveRoleFromStep.ErrorMessage = "The role 'CMS Editors' was not found.";
                }
            }
            else
            {
                // The step was not found
                apiRemoveRoleFromStep.ErrorMessage = "The step 'My new workflow step' was not found.";
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes the workflow step. Called when the "Delete workflow step" button is pressed.
    /// Expects the CreateWorkflow and CreateWorkflowStep methods to be run first.
    /// </summary>
    private bool DeleteWorkflowStep()
    {
        // Get the workflow
        WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");

        if (workflow != null)
        {
            // Get the custom step
            WorkflowStepInfo deleteStep = WorkflowStepInfoProvider.GetWorkflowStepInfo("MyNewWorkflowStep", workflow.WorkflowID);

            if (deleteStep != null)
            {
                // Remove the step
                WorkflowStepInfoProvider.DeleteWorkflowStepInfo(deleteStep);
                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API Examples - Workflow scope"

    /// <summary>
    /// Creates workflow scope. Called when the "Create scope" button is pressed.
    /// Expects the "CreateWorkflow" method to be run first.
    /// </summary>
    private bool CreateWorkflowScope()
    {
        // Get the workflow
        WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");

        if (workflow != null)
        {
            // Create new workflow scope object
            WorkflowScopeInfo newScope = new WorkflowScopeInfo();

            // Get the site default culture from settings
            string cultureCode = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");
            CultureInfo culture = CultureInfoProvider.GetCultureInfo(cultureCode);

            // Get root document type class ID
            int classID = DataClassInfoProvider.GetDataClass("CMS.Root").ClassID;

            // Set the properties
            newScope.ScopeStartingPath = "/";
            newScope.ScopeCultureID = culture.CultureID;
            newScope.ScopeClassID = classID;

            newScope.ScopeWorkflowID = workflow.WorkflowID;
            newScope.ScopeSiteID = CMSContext.CurrentSiteID;

            // Save the workflow scope
            WorkflowScopeInfoProvider.SetWorkflowScopeInfo(newScope);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates workflow scope. Called when the "Get and update scope" button is pressed.
    /// Expects the CreateWorkflowScope method to be run first.
    /// </summary>
    private bool GetAndUpdateWorkflowScope()
    {

        // Get the workflow
        WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");

        if (workflow != null)
        {
            // Get the workflow's scopes
            DataSet scopes = WorkflowScopeInfoProvider.GetWorkflowScopes(workflow.WorkflowID);

            if (!DataHelper.DataSourceIsEmpty(scopes))
            {
                // Create the scope info object
                WorkflowScopeInfo updateScope = new WorkflowScopeInfo(scopes.Tables[0].Rows[0]);

                // Update the properties - the scope will include all cultures and document types
                updateScope.ScopeCultureID = 0;
                updateScope.ScopeClassID = 0;

                // Save the changes
                WorkflowScopeInfoProvider.SetWorkflowScopeInfo(updateScope);

                return true;
            }
            else
            {
                // No scope was found
                apiGetAndUpdateWorkflowScope.ErrorMessage = "The scope was not found.";
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes workflow scope. Called when the "Delete scope" button is pressed.
    /// Expects the CreateWorkflowScope method to be run first.
    /// </summary>
    private bool DeleteWorkflowScope()
    {
        // Get the workflow
        WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("MyNewWorkflow");

        if (workflow != null)
        {

            // Get the workflow's scopes
            DataSet scopes = WorkflowScopeInfoProvider.GetWorkflowScopes(workflow.WorkflowID);

            if (!DataHelper.DataSourceIsEmpty(scopes))
            {
                // Create the scope info object
                WorkflowScopeInfo deleteScope = new WorkflowScopeInfo(scopes.Tables[0].Rows[0]);

                // Delete the workflow scope
                WorkflowScopeInfoProvider.DeleteWorkflowScopeInfo(deleteScope);

                return true;
            }
            else
            {
                // No scope was found
                apiDeleteWorkflowScope.ErrorMessage = "The scope was not found.";
            }
        }

        return false;
    }

    #endregion
}