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

public partial class CMSModules_Groups_Controls_Members_MemberEdit : CMSAdminEditControl
{
    #region "Variables"

    private GroupMemberInfo gmi = null;
    bool newItem = false;
    bool currentRolesLoaded = false;

    protected string currentValues = string.Empty;

    #endregion


    #region "Events"

    public event EventHandler OnApprove;
    public event EventHandler OnReject;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Current group member ID.
    /// </summary>
    public int MemberID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["MemberID"], 0);
        }
        set
        {
            ViewState["MemberID"] = value;
        }
    }


    /// <summary>
    /// Current group ID.
    /// </summary>
    public int GroupID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["GroupID"], 0);
        }
        set
        {
            ViewState["GroupID"] = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        if (this.StopProcessing)
        {
            return;
        }

        // Is live site
        userSelector.IsLiveSite = this.IsLiveSite;
        userSelector.ShowSiteFilter = false;
        userSelector.HideHiddenUsers = true;
        userSelector.HideDisabledUsers = true;
        usRoles.IsLiveSite = this.IsLiveSite;

        // In case of uniselector's callback calling must be where condition set here
        string where = CreateWhereCondition();
        usRoles.WhereCondition = where;

        if (!RequestHelper.IsPostBack() && !IsLiveSite)
        {
            // Reload data
            ReloadData();
        }

        // Initialize user selector
        if (CMSContext.CurrentSite != null)
        {
            this.userSelector.SiteID = CMSContext.CurrentSite.SiteID;
        }

        // Add onclick handlers
        this.btnSave.Click += new EventHandler(btnSave_Click);
        this.btnApprove.Click += new EventHandler(btnApprove_Click);
        this.btnReject.Click += new EventHandler(btnReject_Click);
        usRoles.OnSelectionChanged += new EventHandler(usRoles_OnSelectionChanged);
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        // Get member object info
        if ((this.MemberID > 0))
        {
            this.gmi = GroupMemberInfoProvider.GetGroupMemberInfo(this.MemberID);
        }

        // Get roles for current user 
        LoadCurrentRoles();

        string where = CreateWhereCondition();
        usRoles.WhereCondition = where;

        // Show message or uniselector?
        if (DataHelper.DataSourceIsEmpty(RoleInfoProvider.GetRoles("RoleID", where, null, 1)))
        {
            usRoles.Visible = false;
            lblRole.Visible = true;
        }
        else
        {
            usRoles.Visible = true;
            lblRole.Visible = false;
        }

        // Enable or disable buttons according to state of user's approval process
        if (gmi != null)
        {
            // Current user cannot approve/reject him self
            if (IsLiveSite && (gmi.MemberUserID == CMSContext.CurrentUser.UserID))
            {
                // Member can nothing
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
            else if (gmi.MemberStatus == GroupMemberStatus.Approved)
            {
                // Member can be rejected
                btnApprove.Enabled = false;
                btnReject.Enabled = true;
            }
            else if (gmi.MemberStatus == GroupMemberStatus.Rejected)
            {
                // Member can be approved
                btnApprove.Enabled = true;
                btnReject.Enabled = false;
            }
            else if (gmi.MemberStatus == GroupMemberStatus.WaitingForApproval)
            {
                // Member can be rejected and approved
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
        }

        InitializeForm();
        usRoles.Value = currentValues;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            // Save data
            this.lblInfo.Text = GetString("general.changessaved");
            this.lblInfo.Visible = true;

            this.RaiseOnSaved();
        }
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (ApproveMember())
        {
            // Approve member
            this.lblInfo.Text = GetString("group.member.userhasbeenapproved");
            this.lblInfo.Visible = true;
            ReloadData();

            if (OnApprove != null)
            {
                OnApprove(this, null);
            }
        }
    }


    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (RejectMember())
        {
            // Reject member
            this.lblInfo.Text = GetString("group.member.userhasbeenrejected");
            this.lblInfo.Visible = true;
            ReloadData();

            if (OnReject != null)
            {
                OnReject(this, null);
            }
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Resets the form to default values.
    /// </summary>
    public override void ClearForm()
    {
        // Clear form
        this.txtComment.Text = "";
        this.chkApprove.Checked = true;
    }


    /// <summary>
    /// Approves member.
    /// </summary>
    public bool ApproveMember()
    {
        // Check MANAGE permission for groups module
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
        {
            return false;
        }

        EnsureMember();
        if ((this.gmi != null) && (CMSContext.CurrentUser != null))
        {
            // Set properties
            this.gmi.MemberApprovedByUserID = CMSContext.CurrentUser.UserID;
            this.gmi.MemberStatus = GroupMemberStatus.Approved;
            this.gmi.MemberApprovedWhen = DateTime.Now;
            this.gmi.MemberRejectedWhen = DataHelper.DATETIME_NOT_SELECTED;
            GroupMemberInfoProvider.SetGroupMemberInfo(this.gmi);
            GroupInfo group = GroupInfoProvider.GetGroupInfo(this.GroupID);
            if ((group != null) && (group.GroupSendWaitingForApprovalNotification))
            {
                // Send notification email
                GroupMemberInfoProvider.SendNotificationMail("Groups.MemberApproved", CMSContext.CurrentSiteName, this.gmi, false);
            }

            this.lblMemberApproved.Text = GetApprovalInfoText(gmi.MemberApprovedWhen, gmi.MemberApprovedByUserID);
            this.lblMemberRejected.Text = GetApprovalInfoText(gmi.MemberRejectedWhen, gmi.MemberApprovedByUserID);
            return true;
        }
        return false;
    }


    /// <summary>
    /// Approves member.
    /// </summary>
    public bool RejectMember()
    {
        // Check MANAGE permission for groups module
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
        {
            return false;
        }

        EnsureMember();
        if ((this.gmi != null) && (CMSContext.CurrentUser != null))
        {
            // Set properties
            this.gmi.MemberApprovedByUserID = CMSContext.CurrentUser.UserID;
            this.gmi.MemberStatus = GroupMemberStatus.Rejected;
            this.gmi.MemberApprovedWhen = DataHelper.DATETIME_NOT_SELECTED;
            this.gmi.MemberRejectedWhen = DateTime.Now;
            
            // Save to database
            GroupMemberInfoProvider.SetGroupMemberInfo(this.gmi);
            
            GroupInfo group = GroupInfoProvider.GetGroupInfo(this.GroupID);
            if ((group != null) && (group.GroupSendWaitingForApprovalNotification))
            {
                // Send notification email
                GroupMemberInfoProvider.SendNotificationMail("Groups.MemberRejected", CMSContext.CurrentSiteName, this.gmi, false);
            }

            this.lblMemberApproved.Text = GetApprovalInfoText(gmi.MemberApprovedWhen, gmi.MemberApprovedByUserID);
            this.lblMemberRejected.Text = GetApprovalInfoText(gmi.MemberRejectedWhen, gmi.MemberApprovedByUserID);
            return true;
        }
        return false;
    }


    /// <summary>
    /// Updates the current Group or creates new if no MemberID is present.
    /// </summary>
    public bool SaveData()
    {
        // Check MANAGE permission for groups module
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
        {
            return false;
        }

        EnsureMember();

        newItem = (this.MemberID == 0);

        if (gmi != null)
        {
            // Get user info
            UserInfo ui = UserInfoProvider.GetUserInfo(gmi.MemberUserID);
            if (ui != null)
            {
                // Save user roles
                SaveRoles(ui.UserID);

                gmi.MemberComment = this.txtComment.Text;
                GroupMemberInfoProvider.SetGroupMemberInfo(gmi);

                return true;
            }
        }
        else
        {
            // New member
            if (newItem)
            {
                int userId = ValidationHelper.GetInteger(userSelector.Value, 0);

                // Check if some user was selected
                if (userId == 0)
                {
                    lblError.Visible = true;
                    lblError.ResourceString = "group.member.selectuser";
                    return false;
                }

                // Check if user is not already group member
                gmi = GroupMemberInfoProvider.GetGroupMemberInfo(userId, this.GroupID);
                if (gmi != null)
                {
                    lblError.Visible = true;
                    lblError.ResourceString = "group.member.userexists";
                    return false;
                }

                // New member object
                gmi = new GroupMemberInfo();
                gmi.MemberGroupID = this.GroupID;
                gmi.MemberJoined = DateTime.Now;
                gmi.MemberUserID = userId;
                gmi.MemberComment = this.txtComment.Text;
                
                if (this.chkApprove.Checked)
                {
                    // Approve member
                    gmi.MemberStatus = GroupMemberStatus.Approved;
                    gmi.MemberApprovedWhen = DateTime.Now;
                    gmi.MemberApprovedByUserID = CMSContext.CurrentUser.UserID;
                }
                else
                {
                    gmi.MemberStatus = GroupMemberStatus.WaitingForApproval;
                    gmi.MemberApprovedByUserID = CMSContext.CurrentUser.UserID;
                }

                // Save member to database
                GroupMemberInfoProvider.SetGroupMemberInfo(gmi);
                GroupInfo group = GroupInfoProvider.GetGroupInfo(this.GroupID);
                if (group != null)
                {
                    // Send notification email
                    if ((this.chkApprove.Checked) && (group.GroupSendWaitingForApprovalNotification))
                    {
                        GroupMemberInfoProvider.SendNotificationMail("Groups.MemberJoinedConfirmation", CMSContext.CurrentSiteName, gmi, false);
                    }
                    else
                    {
                        if (group.GroupSendWaitingForApprovalNotification)
                        {
                            GroupMemberInfoProvider.SendNotificationMail("Groups.MemberJoinedWaitingForApproval ", CMSContext.CurrentSiteName, gmi, false);
                        }
                    }
                }

                // Save user roles
                SaveRoles(userId);

                this.MemberID = gmi.MemberID;
                return true;
            }
        }

        return false;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Ensures the memberinfo object is initialized.
    /// </summary>
    private void EnsureMember()
    {
        if (this.MemberID > 0)
        {
            if (this.gmi == null)
            {
                // Get group member
                this.gmi = GroupMemberInfoProvider.GetGroupMemberInfo(this.MemberID);
            }
        }
    }


    /// <summary>
    /// Gets roles for current user.
    /// </summary>
    private void LoadCurrentRoles()
    {
        if (this.gmi != null)
        {
            // Get user roles
            DataSet ds = UserRoleInfoProvider.GetUserRoles("UserID = " + gmi.MemberUserID + "AND RoleID IN (SELECT RoleID FROM CMS_Role WHERE RoleGroupID = " + gmi.MemberGroupID + ")", null, 0, "RoleID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "RoleID"));
            }

            currentRolesLoaded = true;
        }
    }


    /// <summary>
    /// Creates where condition for unigrid with roles.
    /// </summary>
    private string CreateWhereCondition()
    {
        string where = null;

        if (this.gmi != null)
        {
            // Member
            where = "(RoleGroupID = " + this.gmi.MemberGroupID + ")";
        }
        else
        {
            // Group
            where = "(RoleGroupID = " + this.GroupID + ")";
        }

        return where;
    }


    /// <summary>
    /// Initializes the contols in the form.
    /// </summary>
    private void InitializeForm()
    {
        newItem = (this.MemberID == 0);

        // Intialize UI
        this.plcEdit.Visible = !newItem;
        this.plcNew.Visible = newItem;
        this.userSelector.Visible = newItem;
        this.btnApprove.Visible = !newItem;
        this.btnReject.Visible = !newItem;
        this.pnlRoles.GroupingText = GetString("group.member.memberinroles");

        if (newItem)
        {
            this.pnlRoles.GroupingText = GetString("group.member.addmemberinroles");
        }

        // Get strings
        this.lblFullNameLabel.Text = GetString("general.user") + ResHelper.Colon;
        this.lblComment.Text = GetString("group.member.comment") + ResHelper.Colon;
        this.lblMemberApprovedLabel.Text = GetString("group.member.approved") + ResHelper.Colon;
        this.lblMemberApprove.Text = GetString("general.approve") + ResHelper.Colon;
        this.lblMemberRejectedLabel.Text = GetString("group.member.rejected") + ResHelper.Colon;
        this.lblMemberJoinedLabel.Text = GetString("group.member.joined") + ResHelper.Colon;

        // Initialize buttons
        this.btnSave.Text = GetString("general.ok");
        this.btnApprove.Text = GetString("general.approve");
        this.btnReject.Text = GetString("general.reject");

        ClearForm();

        // Handle existing Group editing - prepare the data
        if (this.MemberID > 0)
        {
            HandleExistingMember(gmi);
        }
    }


    /// <summary>
    /// Fills the data into form for specified Group member.
    /// </summary>
    private void HandleExistingMember(GroupMemberInfo gmi)
    {
        if (gmi != null)
        {
            // Fill controls with data from existing user
            int userId = ValidationHelper.GetInteger(gmi.MemberUserID, 0);
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            if (ui != null)
            {
                this.lblFullName.Text = HTMLHelper.HTMLEncode(ui.FullName);
            }

            this.txtComment.Text = gmi.MemberComment;
            this.lblMemberApproved.Text = GetApprovalInfoText(gmi.MemberApprovedWhen, gmi.MemberApprovedByUserID);
            this.lblMemberRejected.Text = GetApprovalInfoText(gmi.MemberRejectedWhen, gmi.MemberApprovedByUserID);
            this.lblMemberJoined.Text = CMSContext.ConvertDateTime(gmi.MemberJoined, this).ToString();
        }
    }


    /// <summary>
    /// Returns the approval text in format "date (approved by user full name)".
    /// </summary>
    /// <param name="date">Date time</param>
    /// <param name="userId">User id</param>
    private string GetApprovalInfoText(DateTime date, int userId)
    {
        string retval = "";

        if (date != DataHelper.DATETIME_NOT_SELECTED)
        {
            // Get current time
            retval = CMSContext.ConvertDateTime(date, this).ToString();

            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            if (ui != null)
            {
                // Add user's full name
                retval += " (" + HTMLHelper.HTMLEncode(ui.FullName) + ")";
            }
        }
        return retval;
    }


    /// <summary>
    /// Saves roles of specified user.
    /// </summary>    
    private void SaveRoles(int userID)
    {
        // Load user's roles
        if (!currentRolesLoaded)
        {
            LoadCurrentRoles();
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usRoles.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Removes relationship between user and role
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);
                    UserRoleInfoProvider.RemoveUserFromRole(userID, roleID);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add relationship between user and role
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);
                    UserRoleInfoProvider.AddUserToRole(userID, roleID);
                }
            }
        }
    }


    /// <summary>
    /// Handles the OnSelectionChanged event of the usRoles control.
    /// Saves the user roles when they are changed.
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data</param>
    private void usRoles_OnSelectionChanged(object sender, EventArgs e)
    {
        // Check MANAGE permission for groups module
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
        {
            return;
        }

        EnsureMember();

        if (gmi != null)
        {
            UserInfo ui = UserInfoProvider.GetUserInfo(gmi.MemberUserID);
            if (ui != null)
            {
                // Save user roles
                SaveRoles(ui.UserID);
            }
        }
    }

    #endregion
}
