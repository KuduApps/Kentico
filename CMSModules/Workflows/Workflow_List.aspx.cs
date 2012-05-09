using System;
using System.Web.UI.WebControls;

using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Workflows_Workflow_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Workflows - Workflow List";

        // Set master page elements
        InitializeMasterPage();

        // Control initialization
        UniGridWorkflows.OnAction += UniGridRoles_OnAction;
        UniGridWorkflows.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    ///  Initializes master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Set the master page title element
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Workflow/object.png");
        CurrentMaster.Title.TitleText = GetString("Development-Workflow_List.Title");
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "workflow_list";

        // Set the action element
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Development-Workflow_List.NewWorkflow");
        actions[0, 5] = GetImageUrl("Objects/CMS_Workflow/add.png");
        actions[0, 3] = "~/CMSModules/Workflows/Workflow_New.aspx";

        CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridRoles_OnAction(string actionName, object actionArgument)
    {
        int workflowid = Convert.ToInt32(actionArgument);

        if (actionName == "edit")
        {
            URLHelper.Redirect("Workflow_Edit.aspx?workflowid=" + workflowid);
        }
        else if (actionName == "delete")
        {
            // Check if documents use the workflow
            if (WorkflowInfoProvider.CheckDependencies(workflowid))
            {
                lblError.Text = GetString("Workflow.CannotDeleteUsed");
                lblError.Visible = true;
                return;
            }
            else
            {
                // Delete the workflow
                WorkflowInfoProvider.DeleteWorkflowInfo(workflowid);
            }
        }
    }
}
