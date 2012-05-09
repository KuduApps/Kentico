using System;
using System.Text;

using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Workflows_Workflow_General : SiteManagerPage
{
    #region "Private variables"

    protected int workflowId = 0;
    protected WorkflowInfo currentWorkflow = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize form and controls
        Title = "Workflow Edit - General";

        rfvCodeName.ErrorMessage = GetString("Development-Workflow_New.RequiresCodeName");
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("Development-Workflow_New.RequiresDisplayName");

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }

        // Get ID of workflow from querystring
        workflowId = QueryHelper.GetInteger("workflowid", 0);

        if (workflowId > 0)
        {
            currentWorkflow = WorkflowInfoProvider.GetWorkflowInfo(workflowId);
            // Set edited object
            EditedObject = currentWorkflow;
        }

        if (!RequestHelper.IsPostBack() && (currentWorkflow != null))
        {
            txtCodeName.Text = currentWorkflow.WorkflowName;
            TextBoxWorkflowDisplayName.Text = currentWorkflow.WorkflowDisplayName;
            chkAutoPublish.Checked = currentWorkflow.WorkflowAutoPublishChanges;

            bool? useCheckInCheckOut = currentWorkflow.WorkflowUseCheckinCheckout;

            // Is enabled or disabled check-in/check-out
            if (useCheckInCheckOut.HasValue)
            {
                radYes.Checked = useCheckInCheckOut.Value;
                radNo.Checked = !useCheckInCheckOut.Value;
            }
            // Inherit from global settings
            else
            {
                radSiteSettings.Checked = true;
            }
        }
    }


    /// <summary>
    /// Saves data of edited workflow from TextBoxes into DB.
    /// </summary>
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        // finds whether required fields are not empty
        string result = new Validator().NotEmpty(TextBoxWorkflowDisplayName.Text, GetString("Development-Workflow_New.RequiresDisplayName"))
            .NotEmpty(txtCodeName.Text, GetString("Development-Workflow_New.RequiresCodeName"))
            .IsCodeName(txtCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (result == "")
        {
            if (currentWorkflow != null)
            {
                // Codename must be unique
                WorkflowInfo wiCodename = WorkflowInfoProvider.GetWorkflowInfo(txtCodeName.Text);
                if ((wiCodename == null) || (wiCodename.WorkflowID == currentWorkflow.WorkflowID))
                {
                    if (currentWorkflow.WorkflowDisplayName != TextBoxWorkflowDisplayName.Text)
                    {
                        // Refresh header
                        ScriptHelper.RefreshTabHeader(Page, null);
                    }

                    currentWorkflow.WorkflowDisplayName = TextBoxWorkflowDisplayName.Text;
                    currentWorkflow.WorkflowName = txtCodeName.Text;
                    currentWorkflow.WorkflowAutoPublishChanges = chkAutoPublish.Checked;

                    // Inherited from global settings
                    if (radSiteSettings.Checked)
                    {
                        currentWorkflow.WorkflowUseCheckinCheckout = null;
                    }
                    else
                    {
                        currentWorkflow.WorkflowUseCheckinCheckout = radYes.Checked;
                    }

                    // Save workflow info
                    WorkflowInfoProvider.SetWorkflowInfo(currentWorkflow);

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
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
                lblError.Text = GetString("Development-Workflow_General.WorkflowDoesNotExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion
}
