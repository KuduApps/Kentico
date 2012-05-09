using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;
using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

[RegisterTitle("content.ui.propertiesworkflow")]
public partial class CMSModules_Content_CMSDesk_Properties_Workflow : CMSPropertiesPage
{
    #region "Private variables"

    // Current step ID
    private int currentStepId = 0;

    // Current Node
    private TreeNode mNode = null;
    private TreeProvider mTree = null;
    private WorkflowManager mWorkflowManager = null;

    private UserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;
    private WorkflowInfo mWorkflowInfo = null;
    private bool wfInfoBinded = false;
    private bool? mCanUserApprove = null;
    private readonly bool displaySplitMode = CMSContext.DisplaySplitMode;

    #endregion


    #region "Properties"

    /// <summary>
    /// Tree provider
    /// </summary>
    private TreeProvider Tree
    {
        get
        {
            return mTree ?? (mTree = new TreeProvider(CMSContext.CurrentUser));
        }
    }


    /// <summary>
    /// Workflow manager
    /// </summary>
    private WorkflowManager WorkflowManager
    {
        get
        {
            return mWorkflowManager ?? (mWorkflowManager = new WorkflowManager(Tree));
        }
    }


    /// <summary>
    /// Tree node
    /// </summary>
    private TreeNode Node
    {
        get
        {
            return mNode ?? (mNode = DocumentHelper.GetDocument(NodeID, CMSContext.PreferredCultureCode, Tree));
        }
    }


    /// <summary>
    /// Determines whether user can approve the document
    /// </summary>
    private bool CanUserApprove
    {
        get
        {
            if (mCanUserApprove == null)
            {
                mCanUserApprove = WorkflowManager.CanUserApprove(Node, CMSContext.CurrentUser);
            }
            return mCanUserApprove.Value;
        }
    }


    /// <summary>
    /// Determines whether document is checked out
    /// </summary>
    private bool DocumentIsCheckedOut
    {
        get
        {
            return (Node != null) && Node.IsCheckedOut;
        }
    }


    /// <summary>
    /// Identifier of current node
    /// </summary>
    private static int NodeID
    {
        get
        {
            // Current Node ID        
            return QueryHelper.GetInteger("nodeid", 0);
        }
    }


    /// <summary>
    /// Workflow info object
    /// </summary>
    private WorkflowInfo WorkflowInfo
    {
        get
        {
            if ((mWorkflowInfo == null) && !wfInfoBinded)
            {
                mWorkflowInfo = WorkflowManager.GetNodeWorkflow(Node);
                wfInfoBinded = true;
            }
            return mWorkflowInfo;
        }
    }


    /// <summary>
    /// Determines whether to send workflow emails.
    /// </summary>
    private static bool SendWorkflowEmails
    {
        get
        {
            return SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSSendWorkflowEmails");
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Workflow"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Workflow");
        }

        // Redirect to page 'New culture version' in split mode. It must be before setting EditedDocument.
        if ((Node == null) && displaySplitMode)
        {
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
        }
        // Set edited document
        EditedDocument = Node;

        gridSteps.WhereCondition = "StepWorkflowID = @StepWorkflowID";

        // Hide custom steps if license doesn't allow them or check automatically publish changes
        if (!WorkflowInfoProvider.IsCustomStepAllowed())
        {
            gridSteps.WhereCondition = SqlHelperClass.AddWhereCondition(gridSteps.WhereCondition, "((StepName = 'edit') OR (StepName = 'published') OR (StepName = 'archived'))");
        }
        // Hide custom steps (without actual step) if functionality 'Automatically publish changes' is allowed
        else if ((WorkflowInfo != null) && WorkflowInfo.WorkflowAutoPublishChanges)
        {
            gridSteps.WhereCondition = SqlHelperClass.AddWhereCondition(gridSteps.WhereCondition, "(StepName = 'edit') OR (StepName = 'published') OR (StepName = 'archived')");
            // Get current step info
            WorkflowStepInfo currentStep = WorkflowManager.GetStepInfo(Node);

            if (currentStep != null)
            {
                string currentStepName = currentStep.StepName.ToLower();

                if ((currentStepName != "edit") && (currentStepName != "published") && (currentStepName != "archived"))
                {
                    gridSteps.WhereCondition = SqlHelperClass.AddWhereCondition(gridSteps.WhereCondition, "(StepName = '" + SqlHelperClass.GetSafeQueryString(currentStep.StepName, false) + "')", "OR");
                }
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the scripts
        ScriptHelper.RegisterProgress(Page);

        UIContext.PropertyTab = PropertyTabEnum.Workflow;
        // Turn sorting off
        gridSteps.GridView.AllowSorting = false;

        // Prepare the query parameters
        QueryDataParameters parameters = new QueryDataParameters();
        parameters.Add("@DocumentID", 0);

        // Prepare the steps query parameters
        QueryDataParameters stepsParameters = new QueryDataParameters();
        stepsParameters.Add("@StepWorkflowID", 0);

        if (Node != null)
        {
            // Check read permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), Node.NodeAliasPath));
            }

            // Prepare parameters
            parameters[0].Value = Node.DocumentID;
            currentStepId = ValidationHelper.GetInteger(Node.GetValue("DocumentWorkflowStepID"), 0);

            if (WorkflowInfo != null)
            {
                stepsParameters[0].Value = WorkflowInfo.WorkflowID;
                lblWorkflowName.Text = string.Format(GetString("WorfklowProperties.lblWorkflowName"), HTMLHelper.HTMLEncode(ResHelper.LocalizeString(WorkflowInfo.WorkflowDisplayName)));

                // Check if 'automatically publish changes' is allowed
                if (WorkflowInfo.WorkflowAutoPublishChanges)
                {
                    lblWorkflowName.Text += " " + GetString("WorfklowProperties.AutoPublishChanges");
                }

                // Initialize unigrids
                gridHistory.OnExternalDataBound += gridHistory_OnExternalDataBound;
                gridSteps.OnExternalDataBound += gridSteps_OnExternalDataBound;
                gridHistory.ZeroRowsText = GetString("workflowproperties.nohistoryyet");
            }

            // Register js synchronization script for split mode
            if (displaySplitMode)
            {
                RegisterSplitModeSync(true, false);
            }
        }
        else
        {
            pnlContent.Visible = false;
        }

        // Initialize query parameters of grids
        gridSteps.QueryParameters = stepsParameters;
        gridHistory.QueryParameters = parameters;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        ReloadData();
    }

    #endregion


    #region "Grid events"

    /// <summary>
    /// External step binding.
    /// </summary>
    protected object gridSteps_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "currentstepinfo":
                DataRowView data = (DataRowView)parameter;
                if (currentStepId <= 0)
                {
                    string stepName = ValidationHelper.GetString(data["StepName"], string.Empty);
                    if (stepName.ToLower() == "edit")
                    {
                        return "<img src=\"" + GetImageUrl("CMSModules/CMS_Content/Properties/currentstep.png") + "\" alt=\"\" />";
                    }
                }
                else
                {
                    // Check if version history exists and node is published
                    if (Node.IsPublished && (Node.DocumentCheckedOutVersionHistoryID <= 0))
                    {
                        string stepName = ValidationHelper.GetString(data["StepName"], string.Empty);
                        if (stepName.ToLower() == "published")
                        {
                            return "<img src=\"" + GetImageUrl("CMSModules/CMS_Content/Properties/currentstep.png") + "\" alt=\"\" />";
                        }
                    }
                    else
                    {
                        int stepId = ValidationHelper.GetInteger(data["StepID"], 0);
                        if (stepId == currentStepId)
                        {
                            return "<img src=\"" + GetImageUrl("CMSModules/CMS_Content/Properties/currentstep.png") + "\" alt=\"\" />";
                        }
                    }
                }
                return string.Empty;

            case "steporder":
                if (sender != null)
                {
                    // Get grid row
                    GridViewRow row = (GridViewRow)((DataControlFieldCell)sender).Parent;
                    int pageOffset = (gridSteps.Pager.CurrentPage - 1) * gridSteps.Pager.CurrentPageSize;
                    // Return row index
                    return (pageOffset + row.RowIndex + 1).ToString();
                }
                return string.Empty;

        }
        return parameter;
    }


    /// <summary>
    /// External history binding.
    /// </summary>
    protected object gridHistory_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView drv = null;
        switch (sourceName.ToLower())
        {
            case "action":
                drv = (DataRowView)parameter;
                bool wasrejected = ValidationHelper.GetBoolean(drv["WasRejected"], false);
                // Get name of step
                string stepName = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "StepName"), String.Empty);
                if (!wasrejected)
                {
                    // Return correct step title
                    switch (stepName.ToLower())
                    {
                        case "archived":
                            return GetString("WorfklowProperties.Archived");

                        case "published":
                            return GetString("WorfklowProperties.Published");

                        case "edit":
                            return GetString("WorfklowProperties.NewVersion");

                        default:
                            return GetString("WorfklowProperties.Approved");
                    }
                }
                else
                {
                    return GetString("WorfklowProperties.Rejected");
                }

            // Get approved time
            case "approvedwhen":
            case "approvedwhentooltip":
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    return string.Empty;
                }
                else
                {
                    if (currentUserInfo == null)
                    {
                        currentUserInfo = CMSContext.CurrentUser;
                    }

                    if (currentSiteInfo == null)
                    {
                        currentSiteInfo = CMSContext.CurrentSite;
                    }

                    // Apply time zone information
                    bool displayGMT = (sourceName == "approvedwhentooltip");
                    DateTime time = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                    return TimeZoneHelper.ConvertToUserTimeZone(time, displayGMT, currentUserInfo, currentSiteInfo);
                }

            case "formattedusername":
                drv = (DataRowView)parameter;
                string userName = ValidationHelper.GetString(drv["UserName"], String.Empty);
                string fullName = ValidationHelper.GetString(drv["FullName"], String.Empty);

                return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(userName, fullName));

            case "stepname":
                return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(parameter.ToString()));

        }
        return parameter;
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Reloads the page data.
    /// </summary>
    protected void ReloadData()
    {
        SetupForm();
        gridHistory.ReloadData();
        gridSteps.ReloadData();
    }


    private void ClearComment()
    {
        txtComment.Text = string.Empty;
    }


    /// <summary>
    /// Reloads the form status.
    /// </summary>
    protected void SetupForm()
    {
        // Check modify permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
        {
            pnlForm.Enabled = false;
            btnArchive.Visible = false;
            btnApprove.Enabled = false;
            btnReject.Enabled = false;
            lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), Node.NodeAliasPath);
        }
        else
        {
            bool autoPublishChanges = false;
            // Check if auto publish changes is allowed
            if (WorkflowInfo != null)
            {
                autoPublishChanges = WorkflowInfo.WorkflowAutoPublishChanges;
            }

            if ((WorkflowInfo != null) || (currentStepId > 0))
            {
                // Setup the form
                WorkflowStepInfo stepInfo = null;
                if (Node.IsPublished && (Node.DocumentCheckedOutVersionHistoryID <= 0))
                {
                    stepInfo = WorkflowStepInfoProvider.GetWorkflowStepInfo("published", WorkflowInfo.WorkflowID);
                }
                else
                {
                    stepInfo = WorkflowStepInfoProvider.GetWorkflowStepInfo(currentStepId) ??
                               WorkflowManager.GetFirstWorkflowStep(Node, WorkflowInfo);
                }

                if (stepInfo != null)
                {
                    currentStepId = stepInfo.StepID;
                    bool buttonEnabled = CanUserApprove && !DocumentIsCheckedOut;
                    bool archiveEnabled = CMSContext.CurrentUser.IsAuthorizedPerResource("cms.content", "manageworkflow", Node.NodeSiteName) && !DocumentIsCheckedOut;

                    switch (stepInfo.StepName.ToLower())
                    {
                        // When document is published
                        case "published":
                            btnReject.Enabled = (Node.DocumentCheckedOutVersionHistoryID > 0) && buttonEnabled;
                            btnReject.Visible = !autoPublishChanges;
                            btnApprove.Enabled = false;
                            btnApprove.Visible = false;
                            btnArchive.Enabled = archiveEnabled;
                            btnArchive.Visible = true;
                            break;

                        // When document is archived
                        case "archived":
                            btnApprove.Enabled = false;
                            btnApprove.Visible = false;
                            btnReject.Enabled = false;
                            btnReject.Visible = false;
                            btnArchive.Enabled = false;
                            btnArchive.Visible = false;
                            pnlForm.Enabled = false;
                            break;

                        // When document is in edit step
                        case "edit":
                            btnApprove.Enabled = !DocumentIsCheckedOut;
                            btnApprove.Visible = true;
                            btnReject.Enabled = false;
                            btnReject.Visible = false;
                            btnArchive.Enabled = archiveEnabled;
                            btnArchive.Visible = true;
                            break;

                        // When document is in custom step
                        default:
                            btnApprove.Enabled = buttonEnabled;
                            btnApprove.Visible = true;
                            btnReject.Enabled = buttonEnabled;
                            btnReject.Visible = !autoPublishChanges;
                            btnArchive.Enabled = archiveEnabled;
                            btnArchive.Visible = true;
                            break;
                    }

                    bool actionIsAllowed = btnApprove.Enabled || btnReject.Enabled || btnArchive.Enabled;

                    pnlForm.Enabled = actionIsAllowed && !DocumentIsCheckedOut;
                    lblSendMail.Enabled = actionIsAllowed;
                    chkSendMail.Enabled = actionIsAllowed;
                    plcSendMail.Visible = SendWorkflowEmails;
                }
            }
            else
            {
                // No workflow operation has been done yet
                btnApprove.Enabled = !DocumentIsCheckedOut;
                btnApprove.Visible = !autoPublishChanges;
                btnReject.Enabled = false;
                btnReject.Visible = false;
                btnArchive.Enabled = false;
                btnArchive.Visible = false;
            }
        }

        // Check if document isn't checked out
        if (DocumentIsCheckedOut)
        {
            lblInfo.Text = GetString("WorfklowProperties.DocumentIsCheckedOut");
        }

        // If no workflow scope set for node, hide the data  
        if (WorkflowInfo == null)
        {
            lblInfo.Text = GetString("properties.scopenotset");
            pnlWorkflow.Visible = false;
        }
    }

    #endregion


    #region "Button handling"

    /// <summary>
    /// Approve event handler.
    /// </summary>
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        // Check modify permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
        {
            return;
        }

        // Get original step
        WorkflowStepInfo originalStep = WorkflowManager.GetStepInfo(Node);

        // Approve document
        WorkflowStepInfo nextStep = WorkflowManager.MoveToNextStep(Node, txtComment.Text);

        // Send workflow e-mails
        if (chkSendMail.Checked && SendWorkflowEmails)
        {
            if ((nextStep == null) || (nextStep.StepName.ToLower() == "published"))
            {
                // Publish e-mails
                WorkflowManager.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, nextStep, WorkflowActionEnum.Published, txtComment.Text);
            }
            else
            {
                // Approve e-mails
                WorkflowManager.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, nextStep, WorkflowActionEnum.Approved, txtComment.Text);
            }
        }

        ClearComment();

        if (nextStep != null)
        {
            currentStepId = nextStep.StepID;
        }
        else
        {
            // If no next step (workflow cancelled), hide the form
            lblInfo.Text = GetString("WorfklowProperties.WorkflowFinished");
            pnlWorkflow.Visible = false;
        }

        string siteName = CMSContext.CurrentSiteName;

        // Refresh tree if document is published and icon published or not published or version not published should be displayed
        if (DocumentIsPublished(Node) && (UIHelper.DisplayPublishedIcon(siteName) || UIHelper.DisplayNotPublishedIcon(siteName) || UIHelper.DisplayVersionNotPublishedIcon(siteName)))
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "refreshTree", ScriptHelper.GetScript("RefreshTree(" + Node.NodeParentID + ", " + Node.NodeID + ");"));
        }
    }


    /// <summary>
    /// Reject event handler.
    /// </summary>
    protected void btnReject_Click(object sender, EventArgs e)
    {
        // Check modify permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
        {
            return;
        }

        // Get original step
        WorkflowStepInfo originalStep = WorkflowManager.GetStepInfo(Node);

        // Reject document
        WorkflowStepInfo previousStep = WorkflowManager.MoveToPreviousStep(Node, txtComment.Text);
        currentStepId = previousStep.StepID;

        // Send workflow e-mails
        if (chkSendMail.Checked && SendWorkflowEmails)
        {
            WorkflowManager.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, previousStep, WorkflowActionEnum.Rejected, txtComment.Text);
        }

        ClearComment();

        string siteName = CMSContext.CurrentSiteName;

        // Refresh tree when original step name was 'published' and icon published or icon not published should be displayed 
        if ((originalStep.StepName.ToLower() == "published") && (UIHelper.DisplayPublishedIcon(siteName) || UIHelper.DisplayNotPublishedIcon(siteName)))
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "refreshTree", ScriptHelper.GetScript("RefreshTree(" + Node.NodeParentID + ", " + Node.NodeID + ");"));
        }
    }


    /// <summary>
    /// Archive event handler.
    /// </summary>
    protected void btnArchive_Click(object sender, EventArgs e)
    {
        // Check modify permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
        {
            return;
        }

        // Get original step
        WorkflowStepInfo originalStep = WorkflowManager.GetStepInfo(Node);

        // Archive document
        WorkflowStepInfo nextStep = WorkflowManager.ArchiveDocument(Node, txtComment.Text);
        currentStepId = nextStep.StepID;

        // Send workflow e-mails
        if (chkSendMail.Checked && SendWorkflowEmails)
        {
            WorkflowManager.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, nextStep, WorkflowActionEnum.Archived, txtComment.Text);
        }

        ClearComment();

        string siteName = CMSContext.CurrentSiteName;

        // Refresh tree
        if (UIHelper.DisplayArchivedIcon(siteName) || UIHelper.DisplayPublishedIcon(siteName) || UIHelper.DisplayNotPublishedIcon(siteName))
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "refreshTree", ScriptHelper.GetScript("RefreshTree(" + Node.NodeParentID + ", " + Node.NodeID + ");"));
        }
    }

    #endregion
}
