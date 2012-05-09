using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.WorkflowEngine;

public partial class CMSModules_Workflows_Workflow_Step_General : SiteManagerPage
{
    #region "Private variables"

    private WorkflowStepInfo mCurrentStepInfo = null;

    #endregion


    #region "Private properties"

    private static int WorkflowStepId
    {
        get
        {
            return QueryHelper.GetInteger("workflowStepId", 0);
        }
    }


    private WorkflowStepInfo CurrentStepInfo
    {
        get
        {
            if (mCurrentStepInfo == null)
            {
                mCurrentStepInfo = WorkflowStepInfoProvider.GetWorkflowStepInfo(WorkflowStepId);
                // Set edited object
                EditedObject = mCurrentStepInfo;
            }
            return mCurrentStepInfo;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        RequiredFieldValidatorCodeName.ErrorMessage = GetString("Development-Workflow_New.RequiresCodeName");
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("Development-Workflow_New.RequiresDisplayName");

        if ((WorkflowStepId != 0) && !RequestHelper.IsPostBack())
        {
            if (CurrentStepInfo != null)
            {
                LoadData(CurrentStepInfo);
            }
        }
    }


    /// <summary>
    /// Loads data of edited workflow from DB into TextBoxes.
    /// </summary>
    protected void LoadData(WorkflowStepInfo wsi)
    {
        txtWorkflowStepCodeName.Text = wsi.StepName;
        switch (wsi.StepName.ToLower())
        {
            case "edit":
            case "published":
            case "archived":
                txtWorkflowStepCodeName.Enabled = false;
                break;
        }
        txtWorkflowStepDisplayName.Text = wsi.StepDisplayName;
    }


    /// <summary>
    /// Saves data of edited workflow from TextBoxes into DB.
    /// </summary>
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        // Finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtWorkflowStepDisplayName.Text, GetString("Development-Workflow_New.RequiresDisplayName")).NotEmpty(txtWorkflowStepCodeName.Text, GetString("Development-Workflow_New.RequiresCodeName"))
            .IsCodeName(txtWorkflowStepCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (result == "")
        {
            if (WorkflowStepId > 0)
            {
                if (CurrentStepInfo != null)
                {
                    if (CurrentStepInfo.StepDisplayName != txtWorkflowStepDisplayName.Text)
                    {
                        // Refresh header
                        ScriptHelper.RefreshTabHeader(Page, null);
                    }

                    CurrentStepInfo.StepDisplayName = txtWorkflowStepDisplayName.Text;
                    CurrentStepInfo.StepName = txtWorkflowStepCodeName.Text;
                    WorkflowStepInfoProvider.SetWorkflowStepInfo(CurrentStepInfo);

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                    
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Development-Workflow_Step_New.WorkflowExists");
                }
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
