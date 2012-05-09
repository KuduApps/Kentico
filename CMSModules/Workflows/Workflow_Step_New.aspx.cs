using System;

using CMS.WorkflowEngine;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Workflows_Workflow_Step_New : SiteManagerPage
{
    #region "Private variables"

    protected int workflowid = 0;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        string workflowSteps = GetString("Development-Workflow_Step_New.Steps");
        string currentWorkflow = GetString("Development-Workflow_Steps.NewStep");

        // Initializes validators
        RequiredFieldValidatorCodeName.ErrorMessage = GetString("Development-Workflow_New.RequiresCodeName");
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("Development-Workflow_New.RequiresDisplayName");

        workflowid = QueryHelper.GetInteger("workflowid", 0);
        string workflowStepsUrl = "~/CMSModules/Workflows/Workflow_Steps.aspx?workflowid=" + workflowid;

        // Initializes page title
        InitializeMasterPage(workflowSteps, workflowStepsUrl, currentWorkflow);
    }


    /// <summary>
    /// Initializes master page title element.
    /// </summary>
    private void InitializeMasterPage(string workflowSteps, string workflowStepsUrl, string currentWorkflow)
    {
        CurrentMaster.Title.HelpTopicName = "new_step";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Set the breadcrumbs element
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = workflowSteps;
        pageTitleTabs[0, 1] = workflowStepsUrl;
        pageTitleTabs[0, 2] = "workflowsContent";
        pageTitleTabs[1, 0] = currentWorkflow;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Saves new workflow step's data and redirects to Workflow_Edit.aspx.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtWorkflowDisplayName.Text, GetString("Development-Workflow_Step_New.RequiresDisplayName")).NotEmpty(txtWorkflowCodeName.Text, GetString("Development-Workflow_Step_New.RequiresCodeName"))
            .IsCodeName(txtWorkflowCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (result == "")
        {
            WorkflowStepInfo wsi = WorkflowStepInfoProvider.GetWorkflowStepInfo(txtWorkflowCodeName.Text, workflowid);
            if (wsi == null)
            {
                try
                {
                    int workflowStepId = SaveNewWorkflowStep();

                    if (workflowStepId > 0)
                    {
                        URLHelper.Redirect("Workflow_Step_Edit.aspx?workflowStepid=" + workflowStepId);
                    }
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Development-Workflow_Step_New.WorkflowStepExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Saves new wokflow step's data into DB.
    /// </summary>
    /// <returns>Returns ID of created wokflow step</returns>
    protected int SaveNewWorkflowStep()
    {
        WorkflowStepInfo wsi = new WorkflowStepInfo();
        wsi.StepDisplayName = txtWorkflowDisplayName.Text;
        wsi.StepName = txtWorkflowCodeName.Text;

        // Get published step info for the proper position
        WorkflowStepInfo psi = WorkflowStepInfoProvider.GetPublishedStep(workflowid);
        if (psi != null)
        {
            wsi.StepOrder = psi.StepOrder;
            // Move the published step down
            psi.StepOrder += 1;
            WorkflowStepInfoProvider.SetWorkflowStepInfo(psi);

            // Move the archived step down
            WorkflowStepInfo asi = WorkflowStepInfoProvider.GetArchivedStep(workflowid);
            if (asi != null)
            {
                asi.StepOrder += 1;
                WorkflowStepInfoProvider.SetWorkflowStepInfo(asi);
            }
        }

        wsi.StepWorkflowID = workflowid;
        WorkflowStepInfoProvider.SetWorkflowStepInfo(wsi);
        return wsi.StepID;
    }

    #endregion
}

