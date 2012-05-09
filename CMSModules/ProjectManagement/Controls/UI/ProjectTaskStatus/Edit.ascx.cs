using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Controls_UI_Projecttaskstatus_Edit : CMSAdminEditControl
{
    #region "Variables"

    private ProjectTaskStatusInfo mProjecttaskstatusObj = null;
    private int mProjecttaskstatusId = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Projecttaskstatus data.
    /// </summary>
    public ProjectTaskStatusInfo ProjecttaskstatusObj
    {
        get
        {
            if (mProjecttaskstatusObj == null)
            {
                mProjecttaskstatusObj = ProjectTaskStatusInfoProvider.GetProjectTaskStatusInfo(this.TaskStatusID);
            }

            return mProjecttaskstatusObj;
        }
        set
        {
            mProjecttaskstatusObj = value;
            if (value != null)
            {
                mProjecttaskstatusId = value.TaskStatusID;
            }
            else
            {
                mProjecttaskstatusId = 0;
            }
        }
    }


    /// <summary>
    /// Projecttaskstatus ID.
    /// </summary>
    public int TaskStatusID
    {
        get
        {
            return mProjecttaskstatusId;
        }
        set
        {
            mProjecttaskstatusId = value;
            mProjecttaskstatusObj = null;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPermissions("CMS.ProjectManagement", ProjectManagementPermissionType.MANAGE_CONFIGURATION);

        if (this.StopProcessing)
        {
            return;
        }

        SetupControls();

        // Set edited object
        if (mProjecttaskstatusId > 0)
        {
            EditedObject = ProjecttaskstatusObj;
        }

        // Load the form data
        if (!URLHelper.IsPostback())
        {
            LoadData();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show or hide messages
        this.lblError.Visible = !string.IsNullOrEmpty(this.lblError.Text);
        this.lblInfo.Visible = !string.IsNullOrEmpty(this.lblInfo.Text);
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Validate and save the data
        Process();
    }

    #endregion


    #region "Private methods"
        
    /// <summary>
    /// Initializes form controls.
    /// </summary>
    private void SetupControls()
    {
        // Button
        btnOk.Text = GetString("general.ok");

        // Validators
        this.rfvTaskStatusDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvTaskStatusName.ErrorMessage = GetString("general.requirescodename");

        // Display 'Changes were saved' message if required
        if (QueryHelper.GetBoolean("saved", false))
        {
            this.lblInfo.Text = GetString("general.changessaved");            
        }

        colorPicker.SupportFolder = URLHelper.ResolveUrl("~/CMSAdminControls/ColorPicker");
    }


    /// <summary>
    /// Loads the data into the form.
    /// </summary>
    private void LoadData()
    {
        // Load the form from the info object
        if (this.ProjecttaskstatusObj != null)
        {
            this.txtTaskStatusName.Text = this.ProjecttaskstatusObj.TaskStatusName;
            this.txtTaskStatusDisplayName.Text = this.ProjecttaskstatusObj.TaskStatusDisplayName;
            this.colorPicker.SelectedColor = this.ProjecttaskstatusObj.TaskStatusColor;
            this.txtTaskStatusIcon.Text = this.ProjecttaskstatusObj.TaskStatusIcon;
            this.chkTaskStatusIsFinished.Checked = this.ProjecttaskstatusObj.TaskStatusIsFinished;
            this.chkTaskStatusIsNotStarted.Checked = this.ProjecttaskstatusObj.TaskStatusIsNotStarted;
            this.chkTaskStatusEnabled.Checked = this.ProjecttaskstatusObj.TaskStatusEnabled;
        }
    }


    /// <summary>
    // Processes the form - saves the data.
    /// </summary>
    private void Process()
    {
        // Validate the form
        if (Validate())
        {
            // Ensure the info object
            if (this.ProjecttaskstatusObj == null)
            {
                this.ProjecttaskstatusObj = new ProjectTaskStatusInfo();
                this.ProjecttaskstatusObj.TaskStatusOrder = ProjectTaskStatusInfoProvider.GetStatusCount(false) + 1;
            }

            // Initialize object
            this.ProjecttaskstatusObj.TaskStatusName = this.txtTaskStatusName.Text.Trim();
            this.ProjecttaskstatusObj.TaskStatusDisplayName = this.txtTaskStatusDisplayName.Text.Trim();
            this.ProjecttaskstatusObj.TaskStatusColor = this.colorPicker.SelectedColor;
            this.ProjecttaskstatusObj.TaskStatusIcon = this.txtTaskStatusIcon.Text.Trim();
            this.ProjecttaskstatusObj.TaskStatusIsFinished = this.chkTaskStatusIsFinished.Checked;
            this.ProjecttaskstatusObj.TaskStatusIsNotStarted = this.chkTaskStatusIsNotStarted.Checked;
            this.ProjecttaskstatusObj.TaskStatusEnabled = this.chkTaskStatusEnabled.Checked;

            // Save object data to database
            ProjectTaskStatusInfoProvider.SetProjectTaskStatusInfo(this.ProjecttaskstatusObj);

            this.ItemID = this.ProjecttaskstatusObj.TaskStatusID;
            this.RaiseOnSaved();

            // Set the info message
            this.lblInfo.Text = GetString("general.changessaved");            
        }
    }


    /// <summary>
    /// Validates the form. If validation succeeds returns true, otherwise returns false.
    /// </summary>
    private bool Validate()
    {
        string codename = this.txtTaskStatusName.Text.Trim();

        // Validate required fields
        string errorMessage = new Validator()
            .NotEmpty(this.txtTaskStatusDisplayName.Text.Trim(), this.rfvTaskStatusDisplayName.ErrorMessage)
            .NotEmpty(codename, this.rfvTaskStatusName.ErrorMessage)
            .IsCodeName(codename, GetString("general.invalidcodename")).Result;

        // Check the uniqueness of the codename
        ProjectTaskStatusInfo ptsi = ProjectTaskStatusInfoProvider.GetProjectTaskStatusInfo(codename);
        if ((ptsi != null) && (ptsi.TaskStatusID != this.TaskStatusID))
        {
            errorMessage = GetString("general.codenameexists");
        }

        // Give error if status is both: started and finished
        if (this.chkTaskStatusIsFinished.Checked && this.chkTaskStatusIsNotStarted.Checked)
        {
            errorMessage = GetString("pm.projectstatus.startandfinish");
        }

        // Set the error message
        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.lblError.Text = errorMessage;         
            return false;
        }

        return true;
    }

    #endregion
}