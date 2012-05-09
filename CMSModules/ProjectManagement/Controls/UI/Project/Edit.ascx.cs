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
using CMS.MembershipProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_ProjectManagement_Controls_UI_Project_Edit : CMSAdminEditControl
{
    #region "Variables"

    private ProjectInfo mProjectObj = null;
    private int mProjectId = 0;
    private bool mDelayedReload = false;
    private int mProjectNodeID = 0;
    private Guid mCodenameGuid = Guid.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the value that indicates whether validators should be disabled
    /// If true only server side validation will be working
    /// </summary>
    public bool DisableOnSiteValidators
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisableOnSiteValidators"), false);
        }
        set
        {
            this.SetValue("DisableOnSiteValidators", value);
        }
    }


    /// <summary>
    /// Gets the project info object.
    /// </summary>
    public ProjectInfo ProjectObj
    {
        get
        {
            if (mProjectObj == null)
            {
                mProjectObj = ProjectInfoProvider.GetProjectInfo(this.ProjectID);
            }

            return mProjectObj;
        }
    }


    /// <summary>
    /// Gets the guid which should be used for codename in simple mode.
    /// </summary>
    protected Guid CodenameGUID
    {
        get
        {
            if (mCodenameGuid == Guid.Empty)
            {
                mCodenameGuid = Guid.NewGuid();
            }
            return mCodenameGuid;
        }
    }


    /// <summary>
    /// If false dont display ok button.
    /// </summary>
    public bool ShowOKButton
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowOKButton"), true);
        }
        set
        {
            this.SetValue("ShowOKButton", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether page selector should be displayed.
    /// </summary>
    public bool ShowPageSelector
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowPageSelector"), true);
        }
        set
        {
            this.SetValue("ShowPageSelector", value);
        }
    }


    /// <summary>
    /// ID of groud where project belongs to.
    /// </summary>
    public int CommunityGroupID
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("CommunityGroupID"), 0);
        }
        set
        {
            this.SetValue("CommunityGroupID", value);
            userSelector.GroupID = this.CommunityGroupID;
        }
    }


    /// <summary>
    /// ID of document where project belongs to.
    /// </summary>
    public int ProjectNodeID
    {
        get
        {
            return mProjectNodeID;
        }
        set
        {
            mProjectNodeID = value;
        }
    }


    /// <summary>
    /// Project ID.
    /// </summary>
    public int ProjectID
    {
        get
        {
            return mProjectId;
        }
        set
        {
            mProjectId = value;
            mProjectObj = null;
        }
    }


    /// <summary>
    /// Indicates delayed reload not from page_load.
    /// </summary>
    public bool DelayedReload
    {
        get
        {
            return mDelayedReload;
        }
        set
        {
            mDelayedReload = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        userSelector.UniSelector.Value = "-1";
        pageSelector.Value = "-1";
        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        SetupControls();

        // Set edited object
        if (this.ProjectID > 0)
        {
            EditedObject = ProjectObj;
        }

        // Load the form data
        if ((!URLHelper.IsPostback()) && (!DelayedReload))
        {
            LoadData();
        }

        btnOk.Visible = ShowOKButton;

        // Set associated controls for form controls due to validity
        lblProjectOwner.AssociatedControlClientID = userSelector.ValueElementID;
        lblProjectPage.AssociatedControlClientID = pageSelector.ValueElementID;
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
        Save();
    }

    #endregion


    #region "Public Methods"

    /// <summary>
    /// Saves control with actual data.
    /// </summary>
    public bool Save()
    {
        if (!CheckPermissions("CMS.ProjectManagement", CMSAdminControl.PERMISSION_MANAGE))
        {
            return false;
        }

        // Validate the form
        if (Validate())
        {
            // Indicates whether project is new
            bool isNew = false;

            int progress = 0;

            // Ensure the info object
            if (this.ProjectObj == null)
            {
                // New project

                ProjectInfo pi = new ProjectInfo();
                // First initialization of the Access propery - allow authenticated users
                pi.ProjectAccess = 1222;
                pi.ProjectCreatedByID = CMSContext.CurrentUser.UserID;

                pi.ProjectOwner = 0;

                if (CommunityGroupID != 0)
                {
                    pi.ProjectGroupID = CommunityGroupID;
                    // Set default access to the group
                    pi.ProjectAccess = 3333;
                }

                mProjectObj = pi;
                isNew = true;

            }
            else
            {
                // Existing project
                
                // Reset ProjectOrder if checkbox was unchecked
                if ((this.ProjectObj.ProjectAllowOrdering)
                    && (!this.chkProjectAllowOrdering.Checked))
                {
                    ProjectInfoProvider.ResetProjectOrder(this.ProjectObj.ProjectID);
                }

                // Clear the hashtables if the codename has been changed
                if ((this.ProjectObj.ProjectGroupID > 0)
                    && this.ProjectObj.ProjectName != this.txtProjectName.Text)
                {
                    ProjectInfoProvider.Clear(true);
                }

                progress = ProjectInfoProvider.GetProjectProgress(this.ProjectObj.ProjectID);
            }

            this.ltrProjectProgress.Text = ProjectTaskInfoProvider.GenerateProgressHtml(progress, true);

            // Initialize object
            this.ProjectObj.ProjectSiteID = CMSContext.CurrentSiteID;

            if (DisplayMode == ControlDisplayModeEnum.Simple)
            {
                if (isNew)
                {
                    this.ProjectObj.ProjectName = ValidationHelper.GetCodeName(txtProjectDisplayName.Text, 50) + ((CommunityGroupID > 0) ? "_group_" : "_general_") + this.CodenameGUID;
                }
            }
            else
            {
                this.ProjectObj.ProjectName = this.txtProjectName.Text.Trim();
            }
            this.ProjectObj.ProjectDisplayName = this.txtProjectDisplayName.Text.Trim();
            this.ProjectObj.ProjectDescription = this.txtProjectDescription.Text.Trim();
            this.ProjectObj.ProjectStartDate = this.dtpProjectStartDate.SelectedDateTime;
            this.ProjectObj.ProjectDeadline = this.dtpProjectDeadline.SelectedDateTime;
            this.ProjectObj.ProjectOwner = ValidationHelper.GetInteger(userSelector.UniSelector.Value, 0);
            this.ProjectObj.ProjectStatusID = ValidationHelper.GetInteger(drpProjectStatus.SelectedValue, 0);
            this.ProjectObj.ProjectAllowOrdering = this.chkProjectAllowOrdering.Checked;

            // Set ProjectNodeID
            if (!this.ShowPageSelector)
            {
                // Set current node id for new project
                if (isNew && (CMSContext.CurrentDocument != null))
                {
                    this.ProjectObj.ProjectNodeID = CMSContext.CurrentDocument.NodeID;
                }
            }
            else
            {
                TreeProvider treeProvider = new TreeProvider();
                TreeNode node = treeProvider.SelectSingleNode(ValidationHelper.GetGuid(this.pageSelector.Value, Guid.Empty), TreeProvider.ALL_CULTURES, CMSContext.CurrentSiteName);
                if (node != null)
                {
                    this.ProjectObj.ProjectNodeID = node.NodeID;
                }
                else
                {
                    this.ProjectObj.ProjectNodeID = 0;
                }
            }

            // Use try/catch due to license check
            try
            {
                // Save object data to database
                ProjectInfoProvider.SetProjectInfo(this.ProjectObj);
                this.ProjectID = this.ProjectObj.ProjectID;

                this.ItemID = this.ProjectObj.ProjectID;
                this.RaiseOnSaved();

                // Set the info message
                this.lblInfo.Text = GetString("general.changessaved");
                return true;
            }
            catch(Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
        return false;
    }


    /// <summary>
    /// Sets the error text.
    /// </summary>
    /// <param name="errorText">Error message</param>
    public void SetError(string errorText)
    {
        // Check whether error message is defined
        if (!String.IsNullOrEmpty(errorText))
        {
            lblError.Visible = true;
            lblError.Text = errorText;
        }
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

        // Set tooltips
        lblProjectDisplayName.ToolTip = GetString("pm.project.tooltip.displayname");
        lblProjectName.ToolTip = GetString("pm.project.tooltip.codename");
        lblProjectDescription.ToolTip = GetString("pm.project.tooltip.description");
        lblProjectStartDate.ToolTip = GetString("pm.project.tooltip.startdate");
        lblProjectDeadline.ToolTip = GetString("pm.project.tooltip.deadline");
        lblProjectProgress.ToolTip = GetString("pm.project.tooltip.progress");
        lblProjectOwner.ToolTip = GetString("pm.project.tooltip.owner");
        lblProjectStatusID.ToolTip = GetString("pm.project.tooltip.status");
        lblProjectPage.ToolTip = GetString("pm.project.tooltip.page");
        lblProjectAllowOrdering.ToolTip = GetString("pm.project.tooltip.allowOrdering");

        // Disable validators if it is required
        if (DisableOnSiteValidators)
        {
            this.rfvProjectName.Enabled = false;
            this.rfvProjectDisplayName.Enabled = false;
        }

        // Validator texts
        this.rfvProjectName.ErrorMessage = GetString("general.requirescodename");
        this.rfvProjectDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        this.pageSelector.IsLiveSite = this.IsLiveSite;
        this.pageSelector.EnableSiteSelection = false;

        // Page selector - show only documents of the current group
        if (CommunityGroupID > 0)
        {
            GeneralizedInfo infoObj = ModuleCommands.CommunityGetGroupInfo(CommunityGroupID);
            if (infoObj != null)
            {
                Guid groupNodeGUID = ValidationHelper.GetGuid(infoObj.GetValue("GroupNodeGUID"), Guid.Empty);

                if (groupNodeGUID != Guid.Empty)
                {
                    TreeProvider treeProvider = new TreeProvider();
                    TreeNode node = treeProvider.SelectSingleNode(groupNodeGUID, TreeProvider.ALL_CULTURES, CMSContext.CurrentSiteName);
                    if (node != null)
                    {
                        this.pageSelector.ContentStartingPath = node.NodeAliasPath;
                    }
                }
                else
                {
                    this.pageSelector.Enabled = false;
                }
            }
        }

        this.userSelector.IsLiveSite = this.IsLiveSite;
        this.userSelector.SiteID = CMSContext.CurrentSiteID;
        this.userSelector.ShowSiteFilter = false;
        this.userSelector.GroupID = CommunityGroupID;
        this.userSelector.ApplyValueRestrictions = false;

        // Hide hidden & disabled user on live site
        if (this.IsLiveSite)
        {
            this.userSelector.HideHiddenUsers = true;
            this.userSelector.HideDisabledUsers = true;
            this.userSelector.HideNonApprovedUsers = true;
        }

        // Hide page selector on live site
        if (!this.ShowPageSelector)
        {
            this.plcProjectPage.Visible = false;
        }

        // Hide codename textbox for simple display mode
        if (DisplayMode == ControlDisplayModeEnum.Simple)
        {
            plcCodeName.Visible = false;
        }

        // Display 'Changes were saved' message if required
        if (QueryHelper.GetBoolean("saved", false))
        {
            this.lblInfo.Text = GetString("general.changessaved");
        }
    }


    public override void ReloadData()
    {
        LoadData();
        base.ReloadData();
    }


    /// <summary>
    /// Loads the data into the form.
    /// </summary>
    public void LoadData()
    {
        // Check if the projects belongs to the current site
        if ((this.ProjectObj != null) && (this.ProjectObj.ProjectSiteID != CMSContext.CurrentSiteID))
        {
            return;
        }

        // If dealayed reload or not postback with not delayed reload
        if (((!URLHelper.IsPostback()) && (!DelayedReload)) || (DelayedReload))
        {
            LoadDropDown();
        }

        // Load the form from the info object
        if (this.ProjectObj != null)
        {
            this.txtProjectName.Text = this.ProjectObj.ProjectName;
            this.txtProjectDisplayName.Text = this.ProjectObj.ProjectDisplayName;
            this.txtProjectDescription.Text = this.ProjectObj.ProjectDescription;
            this.dtpProjectStartDate.SelectedDateTime = this.ProjectObj.ProjectStartDate;
            this.dtpProjectDeadline.SelectedDateTime = this.ProjectObj.ProjectDeadline;

            int progress = ProjectInfoProvider.GetProjectProgress(this.ProjectObj.ProjectID);
            this.ltrProjectProgress.Text = ProjectTaskInfoProvider.GenerateProgressHtml(progress, true);

            if (this.ProjectObj.ProjectOwner != 0)
            {
                userSelector.UniSelector.Value = this.ProjectObj.ProjectOwner;
            }
            else
            {
                userSelector.UniSelector.Value = String.Empty;
            }

            this.chkProjectAllowOrdering.Checked = this.ProjectObj.ProjectAllowOrdering;

            SetStatusDrp(this.ProjectObj.ProjectStatusID);

            if (this.ProjectObj.ProjectNodeID != 0)
            {
                SetProjectPage(this.ProjectObj.ProjectNodeID);
            }
        }
        else
        {
            userSelector.UniSelector.Value = "";
            CurrentUserInfo cui = CMSContext.CurrentUser;
            if (!this.IsLiveSite || cui.UserEnabled)
            {
                // Load default data
                userSelector.UniSelector.Value = CMSContext.CurrentUser.UserID;
            }

            this.pageSelector.Value = String.Empty;
            if (ProjectNodeID != 0)
            {
                SetProjectPage(ProjectNodeID);
            }

            this.ltrProjectProgress.Text = ProjectTaskInfoProvider.GenerateProgressHtml(0, true);

            // Hide progress bar for a new project
            this.plcProgress.Visible = false;
        }
    }


    /// <summary>
    /// Loads the data to the status dropdown field.
    /// </summary>
    private void LoadDropDown()
    {
        drpProjectStatus.DataSource = ProjectStatusInfoProvider.GetProjectStatuses(true);
        drpProjectStatus.DataValueField = "StatusID";
        drpProjectStatus.DataTextField = "StatusDisplayName";
        drpProjectStatus.DataBind();
    }



    /// <summary>
    /// Validates the form. If validation succeeds returns true, otherwise returns false.
    /// </summary>
    private bool Validate()
    {
        string codename = this.txtProjectName.Text.Trim();
        if (this.DisplayMode == ControlDisplayModeEnum.Simple)
        {
            codename = ValidationHelper.GetCodeName(txtProjectDisplayName.Text, 50) + ((CommunityGroupID > 0) ? "_group_" : "_general_") + this.CodenameGUID;
        }

        // Validate required fields
        string errorMessage = new Validator()
            .NotEmpty(this.txtProjectDisplayName.Text.Trim(), this.rfvProjectDisplayName.ErrorMessage)
            .NotEmpty(codename, this.rfvProjectName.ErrorMessage)
            .IsCodeName(codename, GetString("general.invalidcodename")).Result;


        if (!dtpProjectDeadline.IsValidRange() || !dtpProjectStartDate.IsValidRange())
        {
            errorMessage = GetString("general.errorinvaliddatetimerange");
        }

        // Check the uniqueness of the codename
        ProjectInfo pi = ProjectInfoProvider.GetProjectInfo(codename, CMSContext.CurrentSiteID, CommunityGroupID);
        if ((pi != null) && (pi.ProjectID != this.ProjectID))
        {
            errorMessage = GetString("general.codenameexists");
        }

        // Check if there is at least one status defined
        if (ValidationHelper.GetInteger(drpProjectStatus.SelectedValue, 0) == 0)
        {
            errorMessage = GetString("pm.projectstatus.warningnorecord");
        }

        // Set the error message
        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.lblError.Text = errorMessage;
            return false;
        }

        return true;
    }


    /// <summary>
    /// Selects status the in drop down list.
    /// </summary>
    /// <param name="value">The selected value</param>
    private void SetStatusDrp(int value)
    {
        if (drpProjectStatus.Items.FindByValue(value.ToString()) == null)
        {
            // Status not found (is disabled) - add manually
            ProjectStatusInfo status = ProjectStatusInfoProvider.GetProjectStatusInfo(value);
            if (status != null)
            {
                drpProjectStatus.Items.Add(new ListItem(status.StatusDisplayName, status.StatusID.ToString()));
            }
        }

        drpProjectStatus.SelectedValue = value.ToString();
    }


    /// <summary>
    /// Sets the project page.
    /// </summary>
    /// <param name="nodeID">The node ID</param>
    private void SetProjectPage(int nodeID)
    {
        TreeProvider treeProvider = new TreeProvider();
        TreeNode node = treeProvider.SelectSingleNode(nodeID);
        if (node != null)
        {
            this.pageSelector.Value = node.NodeGUID.ToString();
        }
    }


    /// <summary>
    /// Clears form.
    /// </summary>
    public override void ClearForm()
    {
        this.txtProjectDescription.Text = String.Empty;
        this.txtProjectDisplayName.Text = String.Empty;
        this.txtProjectName.Text = String.Empty;
        this.chkProjectAllowOrdering.Checked = true;
        this.dtpProjectStartDate.SelectedDateTime = DateTimeHelper.ZERO_TIME;
        this.dtpProjectDeadline.SelectedDateTime = DateTimeHelper.ZERO_TIME;
        this.userSelector.UniSelector.Value = "";
        this.drpProjectStatus.SelectedIndex = 0;
        this.pageSelector.Clear();
        base.ClearForm();
    }

    #endregion
}