using System;

using CMS.WorkflowEngine;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Workflows_Workflow_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Workflows - New Workflow";

        // initializes labels
        ButtonOK.Text = GetString("General.OK");

        string workflowList = GetString("Development-Workflow_Edit.Workflows");
        string currentWorkflow = GetString("Development-Workflow_List.NewWorkflow");
        string title = GetString("Development-Workflow_List.NewWorkflow");
        // initializes validators
        RequiredFieldValidatorCodeName.ErrorMessage = GetString("Development-Workflow_New.RequiresCodeName");
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("Development-Workflow_New.RequiresDisplayName");

        const string workflowListUrl = "~/CMSModules/Workflows/Workflow_List.aspx";

        // Initialize master page elements
        InitializeMasterPage(title, workflowList, workflowListUrl, currentWorkflow);
    }


    /// <summary>
    /// Initializes master page elements.
    /// </summary>
    private void InitializeMasterPage(string title, string workflowList, string workflowListUrl, string currentWorkflow)
    {
        // Set the master page title
        CurrentMaster.Title.TitleText = title;
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Workflow/new.png");
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "newgeneral_tab";

        // Initializes page title
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = workflowList;
        pageTitleTabs[0, 1] = workflowListUrl;
        pageTitleTabs[1, 0] = currentWorkflow;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Saves new workflow's data and redirects to Workflow_Edit.aspx.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        // finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtWorkflowDisplayName.Text, GetString("Development-Workflow_New.RequiresDisplayName")).NotEmpty(txtWorkflowCodeName.Text, GetString("Development-Workflow_New.RequiresCodeName"))
            .IsCodeName(txtWorkflowCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (result == "")
        {
            WorkflowInfo wi = WorkflowInfoProvider.GetWorkflowInfo(txtWorkflowCodeName.Text);
            if (wi == null)
            {
                int workflowId = SaveNewWorkflow();

                if (workflowId > 0)
                {
                    WorkflowStepInfoProvider.CreateDefaultWorkflowSteps(workflowId);
                    URLHelper.Redirect("Workflow_Edit.aspx?workflowid=" + workflowId + "&saved=1");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Development-Workflow_New.WorkflowExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Saves new wokflow's data into DB.
    /// </summary>
    /// <returns>Returns ID of created wokflow</returns>
    protected int SaveNewWorkflow()
    {
        WorkflowInfo wi = new WorkflowInfo();
        wi.WorkflowDisplayName = txtWorkflowDisplayName.Text;
        wi.WorkflowName = txtWorkflowCodeName.Text;
        WorkflowInfoProvider.SetWorkflowInfo(wi);
        return wi.WorkflowID;
    }
}
