using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Community;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.SiteProvider;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.PortalEngine;

public partial class CMSWebParts_Community_Membership_MyInvitations : CMSAbstractWebPart
{
    #region "Private variables"

    private int mUserId = 0;
    private string mUserName = String.Empty;
    protected string mDeleteImageUrl = string.Empty;
    protected string mAcceptImageUrl = string.Empty;
    protected string mDeleteToolTip = string.Empty;
    protected string mAcceptToolTip = string.Empty;
    private UserInfo userInfo = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets URL of the delete button image.
    /// </summary>
    public string DeleteImageUrl
    {
        get
        {
            return URLHelper.ResolveUrl(DataHelper.GetNotEmpty(GetValue("DeleteImageUrl"), GetImageUrl("Design/Controls/UniGrid/Actions/delete.png")));
        }
        set
        {
            mDeleteImageUrl = value;
            SetValue("DeleteImageUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets URL of the accept button image.
    /// </summary>
    public string AcceptImageUrl
    {
        get
        {
            return URLHelper.ResolveUrl(DataHelper.GetNotEmpty(GetValue("AcceptImageUrl"), GetImageUrl("Design/Controls/UniGrid/Actions/Approve.png")));
        }
        set
        {
            mDeleteImageUrl = value;
            SetValue("AcceptImageUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideControlForZeroRows"), rptMyInvitations.HideControlForZeroRows);
        }
        set
        {
            SetValue("HideControlForZeroRows", value);
            rptMyInvitations.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed when there is no data.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ZeroRowsText"), rptMyInvitations.ZeroRowsText);
        }
        set
        {
            SetValue("ZeroRowsText", value);
            rptMyInvitations.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Id of uesr.
    /// </summary>
    [Obsolete ("Use UserName instead")]
    public int UserID
    {
        get
        {
            mUserId = ValidationHelper.GetInteger(GetValue("UserID"), 0);
            return (mUserId == 0) ? CMSContext.CurrentUser.UserID : mUserId;
        }
        set
        {
            SetValue("UserID", value);
            mUserId = value;
        }
    }


    /// <summary>
    /// Name of user.
    /// </summary>
    public string UserName
    {
        get
        {
            mUserName = ValidationHelper.GetString(GetValue("UserName"), String.Empty);
            if (mUserName != String.Empty)
            {
                return mUserName;
            }

            // Back compatibility
            int userID = ValidationHelper.GetInteger(GetValue("UserID"), 0);
            if (userID != 0)
            {
                if (userID == CMSContext.CurrentUser.UserID)
                {
                    return CMSContext.CurrentUser.UserName;
                }

                if (userInfo == null)
                {
                    userInfo = UserInfoProvider.GetUserInfo(userID);
                }
                
                if (userInfo != null)
                {
                    return userInfo.UserName;
                }
            }
            return CMSContext.CurrentUser.UserName;
        }            
        set
        {
            SetValue("UserName", value);
            mUserName = value;
        }
    }

    #endregion


    #region "Caption properties"


    public string InvitationIsNotValid
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("InvitationIsNotValid"), GetString("group.invitationisnotvalid"));
        }
    }


    public string GroupNoLongerExists
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("GroupNoLongerExists"), GetString("group.nolongerexists"));
        }
    }


    public string MemberJoined
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("MemberJoined"), GetString("groups.memberjoined"));
        }
    }


    public string MemberWaiting
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("MemberWaiting"), GetString("groups.memberjoinedwaiting"));
        }
    }


    public string UserIsAlreadyMember
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("UserIsAlreadyMember"), GetString("groups.userisalreadymember"));
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }    


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();        
        SetupControl();
    }


    protected void SetupControl()
    {
        if (StopProcessing)
        {
            Visible = false;
            // Do not load data
        }
        else
        {
            ltlMessage.Text = ScriptHelper.GetScript("var deleteMessage ='" + GetString("general.confirmdelete") + "';");
            ltlMessage.Text += ScriptHelper.GetScript("var acceptMessage ='" + GetString("groupinvitation.confirmaccept") + "';");
            rptMyInvitations.ZeroRowsText = ZeroRowsText;
            rptMyInvitations.HideControlForZeroRows = HideControlForZeroRows;
            mDeleteImageUrl = DeleteImageUrl;
            mAcceptImageUrl = AcceptImageUrl;
            mAcceptToolTip = GetString("general.accept");
            mDeleteToolTip = GetString("general.delete");
            BindData();
        }
    }


    /// <summary>
    /// Deletes invitation.
    /// </summary>
    protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            int invitationId = ValidationHelper.GetInteger(e.CommandArgument, 0);
            InvitationInfoProvider.DeleteInvitationInfo(invitationId);
            BindData();
        }
    }


    /// <summary>
    /// Accepts invitation.
    /// </summary>
    protected void btnAccept_OnCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "accept")
        {
            int invitationId = ValidationHelper.GetInteger(e.CommandArgument, 0);
            InvitationInfo invitation = InvitationInfoProvider.GetInvitationInfo(invitationId);

            if ((invitation.InvitationValidTo == DateTimeHelper.ZERO_TIME) ||
                                (invitation.InvitationValidTo >= DateTime.Now))
            {
                GroupInfo group = GroupInfoProvider.GetGroupInfo(invitation.InvitationGroupID);
                if (group != null)
                {
                    if (!GroupMemberInfoProvider.IsMemberOfGroup(invitation.InvitedUserID, invitation.InvitationGroupID))
                    {
                        // Transfer user name to user ID
                        if (userInfo == null)
                        {
                            userInfo = UserInfoProvider.GetUserInfo(UserName);
                        }
                        if (userInfo != null)
                        {
                            // Create group member info object
                            GroupMemberInfo groupMember = new GroupMemberInfo();
                            groupMember.MemberInvitedByUserID = invitation.InvitedByUserID;
                            groupMember.MemberUserID = userInfo.UserID;
                            groupMember.MemberGroupID = invitation.InvitationGroupID;
                            groupMember.MemberJoined = DateTime.Now;

                            // Set proper status depending on grouo settings
                            switch (group.GroupApproveMembers)
                            {
                                case GroupApproveMembersEnum.ApprovedCanJoin:
                                    groupMember.MemberStatus = GroupMemberStatus.WaitingForApproval;
                                    lblInfo.Text = MemberWaiting.Replace("##GROUPNAME##", HTMLHelper.HTMLEncode(group.GroupDisplayName)) + "<br /><br />";
                                    break;
                                default:
                                    groupMember.MemberApprovedByUserID = invitation.InvitedByUserID;
                                    groupMember.MemberApprovedWhen = DateTime.Now;
                                    groupMember.MemberStatus = GroupMemberStatus.Approved;
                                    lblInfo.Text = MemberJoined.Replace("##GROUPNAME##", HTMLHelper.HTMLEncode(group.GroupDisplayName)) + "<br /><br />";
                                    break;
                            }
                            // Store info object to database
                            GroupMemberInfoProvider.SetGroupMemberInfo(groupMember);

                            // Delete all invitations to specified group for specified user
                            string whereCondition = "InvitationGroupID = " + invitation.InvitationGroupID + " AND (InvitedUserID=" + userInfo.UserID + ")";
                            InvitationInfoProvider.DeleteInvitations(whereCondition);

                            // Log activity
                            LogJoinActivity(groupMember, group.GroupLogActivity, group.GroupDisplayName);
                        }
                    }
                    else
                    {
                        lblInfo.Text = UserIsAlreadyMember.Replace("##GROUPNAME##", HTMLHelper.HTMLEncode(group.GroupDisplayName)) + "<br /><br />";
                        lblInfo.CssClass = "InvitationErrorLabel";
                        InvitationInfoProvider.DeleteInvitationInfo(invitation);
                    }
                }
                else
                {
                    lblInfo.Text = GroupNoLongerExists + "<br /><br />"; ;
                    lblInfo.CssClass = "InvitationErrorLabel";
                    InvitationInfoProvider.DeleteInvitationInfo(invitation);
                }
            }
            else
            {
                lblInfo.Text = InvitationIsNotValid + "<br /><br />"; ;
                lblInfo.CssClass = "InvitationErrorLabel";
                InvitationInfoProvider.DeleteInvitationInfo(invitation);
            }

            lblInfo.Visible = true;
            BindData();
        }
    }


    /// <summary>
    /// Binds data to repeater.
    /// </summary>
    private void BindData()
    {
        if (UserName != String.Empty)
        {
            DataSet invitations = InvitationInfoProvider.GetMyInvitations("InvitedUserID IN (SELECT UserID FROM CMS_User WHERE UserName='"+UserName.Replace("'","''") + "')" , "InvitationCreated");
            rptMyInvitations.DataSource = invitations;
            rptMyInvitations.DataBind();
            // Hide control if no data
            if (DataHelper.DataSourceIsEmpty(invitations) && (HideControlForZeroRows))
            {
                Visible = false;
                rptMyInvitations.Visible = false;
            }
        }
    }


    /// <summary>
    /// Resolve text.
    /// </summary>
    /// <param name="value">Input value</param>
    public string ResolveText(object value)
    {
        return HTMLHelper.HTMLEncode(ValidationHelper.GetString(value, ""));
    }


    /// <summary>
    /// Log activity
    /// </summary>
    /// <param name="gmi">Member info</param>
    /// <param name="logActivity">Determines whether activity logging is enabled for current group</param>
    /// <param name="groupDisplayName">Display name of the group</param>
    private void LogJoinActivity(GroupMemberInfo gmi, bool logActivity, string groupDisplayName)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || (gmi == null) || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
            || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) || !ActivitySettingsHelper.JoiningAGroupEnabled(siteName))
        {
            return;
        }

        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.JOIN_GROUP,
            TitleData = groupDisplayName,
            ItemID = gmi.MemberGroupID,
            URL = URLHelper.CurrentRelativePath,
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }

    #endregion
}
