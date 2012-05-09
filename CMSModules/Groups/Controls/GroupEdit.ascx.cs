using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.Community;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Groups_Controls_GroupEdit : CMSAdminEditControl
{
    #region "Variables"

    private int mGroupId = 0;
    private int mSiteId = 0;
    private bool mHideWhenGroupIsNotSupplied = false;
    private bool mDisplayAdvanceOptions = false;
    private GroupInfo gi = null;
    private bool mAllowChangeGroupDisplayName = false;
    private bool mAllowSelectTheme = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Determines whether to hide the content of the control when GroupID is not supplied.
    /// </summary>
    public bool HideWhenGroupIsNotSupplied
    {
        get
        {
            return this.mHideWhenGroupIsNotSupplied;
        }
        set
        {
            this.mHideWhenGroupIsNotSupplied = value;

        }
    }


    /// <summary>
    /// Current group ID.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (mGroupId <= 0)
            {
                this.mGroupId = ValidationHelper.GetInteger(this.GetValue("GroupID"), 0);
            }

            return this.mGroupId;
        }
        set
        {
            this.mGroupId = value;
        }
    }


    /// <summary>
    /// If true changing theme for group page is enabled.
    /// </summary>
    public bool AllowSelectTheme
    {
        get
        {
            return mAllowSelectTheme;
        }
        set
        {
            mAllowSelectTheme = value;
        }
    }


    /// <summary>
    /// If true group display name change allowed on live site.
    /// </summary>
    public bool AllowChangeGroupDisplayName
    {
        get
        {
            return this.mAllowChangeGroupDisplayName;
        }
        set
        {
            this.mAllowChangeGroupDisplayName = value;
        }
    }


    /// <summary>
    /// Current site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (this.mSiteId <= 0)
            {
                this.mSiteId = ValidationHelper.GetInteger(this.GetValue("SiteID"), 0);
            }

            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether should be displayed advance fields like codename display name
    /// or document selector
    /// </summary>
    public bool DisplayAdvanceOptions
    {
        get
        {
            return this.mDisplayAdvanceOptions;
        }
        set
        {
            this.mDisplayAdvanceOptions = value;
            this.plcGroupLocation.Visible = value;
            this.plcAdvanceOptions.Visible = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        if (this.StopProcessing)
        {
            // Do nothing
            this.Visible = false;
            this.groupPictureEdit.StopProcessing = true;
            this.groupPageURLElem.StopProcessing = true;
        }
        else
        {
            string currSiteName = CMSContext.CurrentSiteName;
            this.plcGroupLocation.Visible = this.DisplayAdvanceOptions;
            this.plcAdvanceOptions.Visible = this.DisplayAdvanceOptions;
            this.groupPageURLElem.EnableSiteSelection = false;
            this.plcOnline.Visible = ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(currSiteName);

            ctrlSiteSelectStyleSheet.CurrentSelector.ReturnColumnName = "StyleSheetID";
            ctrlSiteSelectStyleSheet.SiteId = CMSContext.CurrentSiteID;
            ctrlSiteSelectStyleSheet.AllowEditButtons = false;
            ctrlSiteSelectStyleSheet.IsLiveSite = IsLiveSite;
            lblStyleSheetName.AssociatedControlClientID = ctrlSiteSelectStyleSheet.CurrentSelector.ClientID;

            // Is allow edit display name is set on live site
            if ((AllowChangeGroupDisplayName) && (IsLiveSite))
            {
                if (!this.plcAdvanceOptions.Visible)
                {
                    this.plcCodeName.Visible = false;
                }
                this.plcAdvanceOptions.Visible = true;
            }

            // Web parts theme selector visibility
            if ((!AllowSelectTheme) && (IsLiveSite))
            {
                plcStyleSheetSelector.Visible = false;
            }

            RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

            if (this.StopProcessing)
            {
                return;
            }

            if ((this.GroupID == 0) && this.HideWhenGroupIsNotSupplied)
            {
                this.Visible = false;
                return;
            }

            InitializeForm();

            gi = GroupInfoProvider.GetGroupInfo(this.GroupID);
            if (gi != null)
            {
                if (!RequestHelper.IsPostBack())
                {
                    // Handle existing Group editing - prepare the data
                    if (this.GroupID > 0)
                    {
                        HandleExistingGroup();
                    }
                }

                this.groupPictureEdit.GroupInfo = gi;

                // UI Tools theme selector visibility
                if ((!IsLiveSite) && (gi.GroupNodeGUID == Guid.Empty))
                {
                    plcStyleSheetSelector.Visible = false;
                }

                // Init theme selector
                if (plcStyleSheetSelector.Visible)
                {
                    if (!URLHelper.IsPostback())
                    {
                        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                        if (gi.GroupNodeGUID != Guid.Empty)
                        {
                            TreeNode node = tree.SelectSingleNode(gi.GroupNodeGUID, TreeProvider.ALL_CULTURES, CMSContext.CurrentSiteName);
                            if (node != null)
                            {
                                ctrlSiteSelectStyleSheet.Value = node.DocumentStylesheetID;
                            }
                        }
                    }
                }
            }
            else
            {
                plcStyleSheetSelector.Visible = false;
            }

            txtDescription.IsLiveSite = this.IsLiveSite;
            groupPictureEdit.IsLiveSite = this.IsLiveSite;
            groupPageURLElem.IsLiveSite = this.IsLiveSite;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }


    #region "Public methods"


    /// <summary>
    /// Sets the property value of the control, setting the value affects only local property value.
    /// </summary>
    /// <param name="propertyName">Property name to set</param>
    /// <param name="value">New property value</param>
    public override void SetValue(string propertyName, object value)
    {
        // Allow change group display name
        if (String.Compare(propertyName, "AllowChangeGroupDisplayName", true) == 0)
        {
            AllowChangeGroupDisplayName = ValidationHelper.GetBoolean(value, false);
        }

        // Allow change theme of group page
        if (String.Compare(propertyName, "AllowSelectTheme", true) == 0)
        {
            AllowSelectTheme = ValidationHelper.GetBoolean(value, false);
        }

        // Call base method
        base.SetValue(propertyName, value);
    }

    /// <summary>
    /// Updates the current Group or creates new if no GroupID is present.
    /// </summary>
    public void SaveData()
    {
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
        {
            return;
        }

        // Trim display name and code name
        string displayName = this.txtDisplayName.Text.Trim();
        string codeName = ValidationHelper.GetCodeName(this.txtCodeName.Text.Trim(), null, 89, true, true);
        this.txtCodeName.Text = codeName;

        // Validate form entries
        string errorMessage = ValidateForm(displayName, codeName);

        if (errorMessage == "")
        {
            txtCodeName.Text = codeName;
            txtDisplayName.Text = displayName;

            GroupInfo group = null;

            try
            {
                bool newGroup = false;
                // Update existing item
                if ((this.GroupID) > 0 && (gi != null))
                {
                    group = gi;
                }
                else
                {
                    group = new GroupInfo();
                    newGroup = true;
                }

                if (group != null)
                {
                    if (displayName != group.GroupDisplayName)
                    {
                        // Refresh a breadcrumb if used in the tabs layout
                        ScriptHelper.RefreshTabHeader(Page, string.Empty);
                    }

                    if (this.DisplayAdvanceOptions)
                    {
                        // Update Group fields
                        group.GroupDisplayName = displayName;
                        group.GroupName = codeName;
                        group.GroupNodeGUID = ValidationHelper.GetGuid(this.groupPageURLElem.Value, Guid.Empty);
                    }

                    if ((AllowChangeGroupDisplayName) && (IsLiveSite))
                    {
                        group.GroupDisplayName = displayName;
                    }

                    group.GroupDescription = this.txtDescription.Text;
                    group.GroupAccess = GetGroupAccess();
                    group.GroupSiteID = this.SiteID;
                    group.GroupApproveMembers = GetGroupApproveMembers();
                    group.GroupSendJoinLeaveNotification = this.chkJoinLeave.Checked;
                    group.GroupSendWaitingForApprovalNotification = this.chkWaitingForApproval.Checked;
                    this.groupPictureEdit.UpdateGroupPicture(group);

                    // If new group was created 
                    if (newGroup)
                    {
                        // Set columns GroupCreatedByUserID and GroupApprovedByUserID to current user
                        CurrentUserInfo user = CMSContext.CurrentUser;
                        if (user != null)
                        {
                            group.GroupCreatedByUserID = user.UserID;
                            group.GroupApprovedByUserID = user.UserID;
                            group.GroupApproved = true;
                        }
                    }

                    if ((!IsLiveSite) && (group.GroupNodeGUID == Guid.Empty))
                    {
                        plcStyleSheetSelector.Visible = false;
                    }

                    // Save theme 
                    int selectedSheetID = ValidationHelper.GetInteger(ctrlSiteSelectStyleSheet.Value, 0);
                    if (plcStyleSheetSelector.Visible)
                    {
                        if (group.GroupNodeGUID != Guid.Empty)
                        {
                            // Save theme for every site culture                            
                            ArrayList cultures = CultureInfoProvider.GetSiteCultureCodes(CMSContext.CurrentSiteName);
                            if (cultures != null)
                            {
                                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                                // Return class name of selected tree node
                                TreeNode treeNode = tree.SelectSingleNode(group.GroupNodeGUID, TreeProvider.ALL_CULTURES, CMSContext.CurrentSiteName);
                                if (treeNode != null)
                                {
                                    // Return all culture version of node 
                                    DataSet ds = tree.SelectNodes(CMSContext.CurrentSiteName, null, TreeProvider.ALL_CULTURES, false, treeNode.NodeClassName, "NodeGUID ='" + group.GroupNodeGUID + "'", String.Empty, -1, false);
                                    if (!DataHelper.DataSourceIsEmpty(ds))
                                    {
                                        // Loop trhough all nodes
                                        foreach (DataRow dr in ds.Tables[0].Rows)
                                        {
                                            // Create node and set treeprovider for user validation
                                            TreeNode node = TreeNode.New(dr, ValidationHelper.GetString(dr["className"], String.Empty)); ;
                                            node.TreeProvider = tree;
                                            // Update stylesheet id if set
                                            if (selectedSheetID == 0)
                                            {
                                                node.SetValue("DocumentStylesheetID", -1);
                                            }
                                            else
                                            {
                                                node.DocumentStylesheetID = selectedSheetID;
                                            }
                                            node.Update();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if ((!IsLiveSite) && (group.GroupNodeGUID != Guid.Empty))
                    {
                        plcStyleSheetSelector.Visible = true;
                    }

                    if (plcOnline.Visible)
                    {
                        // On-line marketing setting is visible => set flag according to checkbox
                        group.GroupLogActivity = chkLogActivity.Checked;
                    }
                    else
                    {
                        // On-line marketing setting is not visible => set flag to TRUE as default value
                        group.GroupLogActivity = true;
                    }

                    // Save Group in the database
                    GroupInfoProvider.SetGroupInfo(group);
                    this.groupPictureEdit.GroupInfo = group;

                    // Flush cached information
                    CMSContext.CurrentDocument = null;
                    CMSContext.CurrentPageInfo = null;
                    CMSContext.CurrentDocumentStylesheet = null;

                    // Display information on success
                    this.lblInfo.Text = GetString("general.changessaved");
                    this.lblInfo.Visible = true;

                    // If new group was created 
                    if (newGroup)
                    {
                        this.GroupID = group.GroupID;
                        this.RaiseOnSaved();
                    }
                }
            }
            catch (Exception ex)
            {
                // Display error message
                this.lblError.Text = GetString("general.erroroccurred") + " " + ex.Message;
                this.lblError.Visible = true;
            }
        }
        else
        {
            // Display error message
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the contols in the form.
    /// </summary>
    private void InitializeForm()
    {
        // Initialize errors
        this.rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvCodeName.ErrorMessage = GetString("general.requirescodename");

        // Initialize buttons
        this.btnSave.Text = GetString("general.ok");
    }


    /// <summary>
    /// Returns correct number according to radiobutton selection.
    /// </summary>
    private SecurityAccessEnum GetGroupAccess()
    {
        if (this.radSiteMembers.Checked)
        {
            return SecurityAccessEnum.AuthenticatedUsers;
        }
        else if (this.radGroupMembers.Checked)
        {
            return SecurityAccessEnum.GroupMembers;
        }
        else
        {
            return SecurityAccessEnum.AllUsers;
        }
    }


    /// <summary>
    /// Returns correct number according to radiobutton selection.
    /// </summary>
    private GroupApproveMembersEnum GetGroupApproveMembers()
    {
        if (this.radMembersApproved.Checked)
        {
            return GroupApproveMembersEnum.ApprovedCanJoin;
        }
        else if (this.radMembersInvited.Checked)
        {
            return GroupApproveMembersEnum.InvitedWithoutApproval;
        }
        else
        {
            return GroupApproveMembersEnum.AnyoneCanJoin;
        }
    }


    /// <summary>
    /// Fills the data into form for specified Group.
    /// </summary>
    private void HandleExistingGroup()
    {
        if (gi != null)
        {
            this.txtDisplayName.Text = gi.GroupDisplayName;
            this.txtCodeName.Text = gi.GroupName;
            this.txtDescription.Text = gi.GroupDescription;
            if (this.DisplayAdvanceOptions)
            {
                this.groupPageURLElem.Value = gi.GroupNodeGUID.ToString();
            }
            this.chkJoinLeave.Checked = gi.GroupSendJoinLeaveNotification;
            this.chkWaitingForApproval.Checked = gi.GroupSendWaitingForApprovalNotification;

            UserInfo ui = null;

            // Display created by user name
            if (gi.GroupCreatedByUserID != 0)
            {
                plcCreatedBy.Visible = true;

                ui = UserInfoProvider.GetUserInfo(gi.GroupCreatedByUserID);
                if (ui != null)
                {
                    this.lblCreatedByValue.Text = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(ui.UserName, this.IsLiveSite));
                }
            }

            // Display approved by user name
            if (gi.GroupApprovedByUserID != 0)
            {
                plcApprovedBy.Visible = true;

                if (gi.GroupApprovedByUserID != gi.GroupCreatedByUserID)
                {
                    ui = UserInfoProvider.GetUserInfo(gi.GroupApprovedByUserID);
                }

                if (ui != null)
                {
                    this.lblApprovedByValue.Text = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(ui.UserName, this.IsLiveSite));
                }
            }

            switch (gi.GroupAccess)
            {
                case SecurityAccessEnum.AllUsers:
                    this.radAnybody.Checked = true;
                    break;

                case SecurityAccessEnum.AuthenticatedUsers:
                    this.radSiteMembers.Checked = true;
                    break;

                case SecurityAccessEnum.GroupMembers:
                    this.radGroupMembers.Checked = true;
                    break;
            }

            switch (gi.GroupApproveMembers)
            {
                case GroupApproveMembersEnum.AnyoneCanJoin:
                    this.radMembersAny.Checked = true;
                    break;

                case GroupApproveMembersEnum.ApprovedCanJoin:
                    this.radMembersApproved.Checked = true;
                    break;

                case GroupApproveMembersEnum.InvitedWithoutApproval:
                    this.radMembersInvited.Checked = true;
                    break;
            }

            chkLogActivity.Checked = gi.GroupLogActivity;
        }
    }


    /// <summary>
    /// Checks whether the given code name is unique.
    /// </summary>
    private bool CodeNameIsUnique(string codeName, int currentGroupId)
    {

        string where = "GroupName = N'" + SqlHelperClass.GetSafeQueryString(codeName, false) + "'";

        // Filter out current group
        if (currentGroupId > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "GroupID <> " + currentGroupId);
        }

        if (this.SiteID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "GroupSiteID = " + this.SiteID);
        }


        return DataHelper.DataSourceIsEmpty(GroupInfoProvider.GetGroups(where, null));
    }


    /// <summary>
    /// Validates the form entries.
    /// </summary>
    /// <param name="codeName">Code name to validate</param>)
    /// <param name="displayName">Display name to validate</param>
    private string ValidateForm(string displayName, string codeName)
    {
        string errorMessage = String.Empty;
        if (DisplayAdvanceOptions)
        {
            errorMessage = new Validator().NotEmpty(codeName, this.rfvCodeName.ErrorMessage)
                                                 .NotEmpty(displayName, this.rfvDisplayName.ErrorMessage)
                                                 .IsCodeName(codeName, GetString("general.errorcodenameinidentificatorformat"), true).Result;

            if (errorMessage != "")
            {
                return errorMessage;
            }

            // Validate uniqueness
            if (!CodeNameIsUnique(codeName, this.GroupID))
            {
                errorMessage = GetString("general.uniquecodenameerror");
            }
        }

        // Validate file input
        if (!this.groupPictureEdit.IsValid())
        {
            errorMessage = this.groupPictureEdit.ErrorMessage;
        }

        return errorMessage;
    }

    #endregion
}
