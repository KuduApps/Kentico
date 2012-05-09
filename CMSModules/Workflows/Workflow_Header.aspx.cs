using System;

using CMS.WorkflowEngine;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Workflows_Workflow_Header : SiteManagerPage
{
    protected int workflowId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Workflow Edit - Header";

        workflowId = QueryHelper.GetInteger("workflowid", 0);

        WorkflowInfo wi = WorkflowInfoProvider.GetWorkflowInfo(workflowId);
        // Set edited object
        EditedObject = wi;

        const string workflowListUrl = "~/CMSModules/Workflows/Workflow_List.aspx";
        string workflows = GetString("Development-Workflow_Edit.Workflows");
        string currentWorkflow = string.Empty;
        string title = GetString("Development-Workflow_Edit.Title");

        if (wi != null)
        {
            currentWorkflow = wi.WorkflowDisplayName;
        }

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();

            if (QueryHelper.GetString("showtab", string.Empty) == "steps")
            {
                CurrentMaster.Tabs.SelectedTab = 1;
            }
        }

        // Initialize master page title
        InitializeMasterPage(title, workflows, workflowListUrl, currentWorkflow);
    }


    /// <summary>
    // Initializes the master page title
    /// </summary>
    private void InitializeMasterPage(string title, string workflows, string workflowListUrl, string currentWorkflow)
    {
        // Set the master page title
        CurrentMaster.Title.TitleText = title;
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Workflow/object.png");
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "newgeneral_tab";

        // Intialize master page
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = workflows;
        pageTitleTabs[0, 1] = workflowListUrl;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentWorkflow;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Initializes workflow edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'newgeneral_tab');";
        tabs[0, 2] = "Workflow_General.aspx?workflowId=" + workflowId;
        tabs[1, 0] = GetString("Development-Workflow_Edit.Steps");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'steps_tab');";
        tabs[1, 2] = "Workflow_Steps.aspx?workflowId=" + workflowId;

        // Hide if no site or no running site
        if ((SiteInfoProvider.GetSitesCount() > 0))
        {
            tabs[2, 0] = GetString("Development-Workflow_Edit.Scopes");
            tabs[2, 1] = "SetHelpTopic('helpTopic', 'scope_tab');";
            tabs[2, 2] = "Workflow_Scopes.aspx?workflowId=" + workflowId;
        }

        tabs[3, 0] = GetString("general.documents");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'documents_tab');";
        tabs[3, 2] = "Workflow_Documents.aspx?workflowId=" + workflowId;

        CurrentMaster.Tabs.UrlTarget = "workflowsContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }
}
