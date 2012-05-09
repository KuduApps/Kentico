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

public partial class CMSModules_ProjectManagement_Controls_UI_Projecttaskpriority_Edit : CMSAdminEditControl
{
    #region "Variables"

    private ProjectTaskPriorityInfo mProjecttaskpriorityObj = null;
    private int mProjecttaskpriorityId = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Projecttaskpriority data.
    /// </summary>
    public ProjectTaskPriorityInfo ProjecttaskpriorityObj
    {
        get
        {
            if (mProjecttaskpriorityObj == null)
            {
                mProjecttaskpriorityObj = ProjectTaskPriorityInfoProvider.GetProjectTaskPriorityInfo(this.TaskPriorityID);
            }

            return mProjecttaskpriorityObj;
        }
        set
        {
            mProjecttaskpriorityObj = value;
            if (value != null)
            {
                mProjecttaskpriorityId = value.TaskPriorityID;
            }
            else
            {
                mProjecttaskpriorityId = 0;
            }
        }
    }


    /// <summary>
    /// Projecttaskpriority ID.
    /// </summary>
    public int TaskPriorityID
    {
        get
        {
            return mProjecttaskpriorityId;
        }
        set
        {
            mProjecttaskpriorityId = value;
            mProjecttaskpriorityObj = null;
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
        if (mProjecttaskpriorityId > 0)
        {
            EditedObject = ProjecttaskpriorityObj;
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
        this.rfvTaskPriorityName.ErrorMessage = GetString("general.requirescodename");
        this.rfvTaskPriorityDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        // Display 'Changes were saved' message if required
        if (QueryHelper.GetBoolean("saved", false))
        {
            this.lblInfo.Text = GetString("general.changessaved");            
        }
    }


    /// <summary>
    /// Loads the data into the form.
    /// </summary>
    private void LoadData()
    {
        // Load the form from the info object
        if (this.ProjecttaskpriorityObj != null)
        {
            this.txtTaskPriorityName.Text = this.ProjecttaskpriorityObj.TaskPriorityName;
            this.txtTaskPriorityDisplayName.Text = this.ProjecttaskpriorityObj.TaskPriorityDisplayName;
            this.chkTaskPriorityEnabled.Checked = this.ProjecttaskpriorityObj.TaskPriorityEnabled;
            this.chkTaskPriorityDefault.Checked = this.ProjecttaskpriorityObj.TaskPriorityDefault;
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
            if (this.ProjecttaskpriorityObj == null)
            {
                this.ProjecttaskpriorityObj = new ProjectTaskPriorityInfo();
                this.ProjecttaskpriorityObj.TaskPriorityOrder = ProjectTaskPriorityInfoProvider.GetPriorityCount(false) + 1;
            }

            // Initialize object
            this.ProjecttaskpriorityObj.TaskPriorityName = this.txtTaskPriorityName.Text.Trim();
            this.ProjecttaskpriorityObj.TaskPriorityDisplayName = this.txtTaskPriorityDisplayName.Text.Trim();
            this.ProjecttaskpriorityObj.TaskPriorityEnabled = this.chkTaskPriorityEnabled.Checked;
            this.ProjecttaskpriorityObj.TaskPriorityDefault = this.chkTaskPriorityDefault.Checked;

            // Save object data to database
            ProjectTaskPriorityInfoProvider.SetProjectTaskPriorityInfo(this.ProjecttaskpriorityObj);

            this.ItemID = this.ProjecttaskpriorityObj.TaskPriorityID;
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
        string codename = this.txtTaskPriorityName.Text.Trim();

        // Validate required fields
        string errorMessage = new Validator()
            .NotEmpty(this.txtTaskPriorityDisplayName.Text.Trim(), this.rfvTaskPriorityDisplayName.ErrorMessage)
            .NotEmpty(codename, this.rfvTaskPriorityName.ErrorMessage)
            .IsCodeName(codename, GetString("general.invalidcodename")).Result;

        // Check the uniqueness of the codename
        ProjectTaskPriorityInfo ptpi = ProjectTaskPriorityInfoProvider.GetProjectTaskPriorityInfo(codename);
        if ((ptpi != null) && (ptpi.TaskPriorityID != this.TaskPriorityID))
        {
            errorMessage = GetString("general.codenameexists");
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