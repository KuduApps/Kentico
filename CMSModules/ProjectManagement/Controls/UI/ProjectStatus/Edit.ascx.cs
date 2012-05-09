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

public partial class CMSModules_ProjectManagement_Controls_UI_Projectstatus_Edit : CMSAdminEditControl
{
    #region "Variables"

    private ProjectStatusInfo mProjectstatusObj = null;
    private int mProjectstatusId = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Projectstatus data.
    /// </summary>
    public ProjectStatusInfo ProjectstatusObj
    {
        get
        {
            if (mProjectstatusObj == null)
            {
                mProjectstatusObj = ProjectStatusInfoProvider.GetProjectStatusInfo(this.StatusID);
            }

            return mProjectstatusObj;
        }
        set
        {
            mProjectstatusObj = value;
            if (value != null)
            {
                mProjectstatusId = value.StatusID;
            }
            else
            {
                mProjectstatusId = 0;
            }
        }
    }


    /// <summary>
    /// Projectstatus ID.
    /// </summary>
    public int StatusID
    {
        get
        {
            return mProjectstatusId;
        }
        set
        {
            mProjectstatusId = value;
            mProjectstatusObj = null;
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
        if (this.StatusID > 0)
        {
            EditedObject = ProjectstatusObj;
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
        this.rfvStatusDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvStatusName.ErrorMessage = GetString("general.requirescodename");

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
        if (this.ProjectstatusObj != null)
        {
            this.txtStatusName.Text = this.ProjectstatusObj.StatusName;
            this.txtStatusDisplayName.Text = this.ProjectstatusObj.StatusDisplayName;
            this.colorPicker.SelectedColor = this.ProjectstatusObj.StatusColor;
            this.txtStatusIcon.Text = this.ProjectstatusObj.StatusIcon;
            this.chkStatusIsFinished.Checked = this.ProjectstatusObj.StatusIsFinished;
            this.chkStatusIsNotStarted.Checked = this.ProjectstatusObj.StatusIsNotStarted;
            this.chkStatusEnabled.Checked = this.ProjectstatusObj.StatusEnabled;
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
            if (this.ProjectstatusObj == null)
            {
                this.ProjectstatusObj = new ProjectStatusInfo();
                this.ProjectstatusObj.StatusOrder = ProjectStatusInfoProvider.GetStatusCount(false) + 1;
            }

            // Initialize object
            this.ProjectstatusObj.StatusName = this.txtStatusName.Text.Trim();
            this.ProjectstatusObj.StatusDisplayName = this.txtStatusDisplayName.Text.Trim();
            this.ProjectstatusObj.StatusColor = this.colorPicker.SelectedColor;
            this.ProjectstatusObj.StatusIcon = this.txtStatusIcon.Text.Trim();
            this.ProjectstatusObj.StatusIsFinished = this.chkStatusIsFinished.Checked;
            this.ProjectstatusObj.StatusIsNotStarted = this.chkStatusIsNotStarted.Checked;
            this.ProjectstatusObj.StatusEnabled = this.chkStatusEnabled.Checked;

            // Save object data to database
            ProjectStatusInfoProvider.SetProjectStatusInfo(this.ProjectstatusObj);

            this.ItemID = this.ProjectstatusObj.StatusID;
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
        string codename = this.txtStatusName.Text.Trim();

        // Validate required fields
        string errorMessage = new Validator()
            .NotEmpty(this.txtStatusDisplayName.Text.Trim(), this.rfvStatusDisplayName.ErrorMessage)
            .NotEmpty(codename, this.rfvStatusName.ErrorMessage)
            .IsCodeName(codename, GetString("general.invalidcodename")).Result;

        // Check the uniqueness of the codename
        ProjectStatusInfo psi = ProjectStatusInfoProvider.GetProjectStatusInfo(codename);
        if ((psi != null) && (psi.StatusID != this.StatusID))
        {
            errorMessage = GetString("general.codenameexists");
        }

        // Give error if status is both: started and finished
        if (this.chkStatusIsFinished.Checked && this.chkStatusIsNotStarted.Checked)
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