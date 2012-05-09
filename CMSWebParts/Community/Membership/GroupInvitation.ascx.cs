using System;
using System.Web;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.Community;
using CMS.EmailEngine;
using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.WebAnalytics;
using CMS.PortalEngine;

public partial class CMSWebParts_Community_Membership_GroupInvitation : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether to send default group e-mails.
    /// </summary>
    public bool SendDefaultGroupEmails
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SendDefaultGroupEmails"), false);
        }
        set
        {
            SetValue("SendDefaultGroupEmails", value);
        }
    }


    ///// <summary>
    ///// Gets or sets the value that indicates whether to send e-mail to inviter.
    ///// </summary>
    public bool SendEmailToInviter
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SendEmailToInviter"), false);
        }
        set
        {
            SetValue("SendEmailToInviter", value);
        }
    }


    /// <summary>
    /// Gets or sets logon page URL.
    /// </summary>
    public string LoginURL
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LoginURL"), string.Empty);
        }
        set
        {
            SetValue("LoginURL", value);
        }
    }

    #region "Caption properties"

    public string InvitationNoLongerExists
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("InvitationNoLongerExists"), GetString("group.invitationnolongerexists"));
        }
    }


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


    #endregion

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                // If user is public
                if (CMSContext.CurrentUser.IsPublic())
                {
                    // Get logon url
                    string logonUrl = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSecuredAreasLogonPage");
                    logonUrl = DataHelper.GetNotEmpty(LoginURL, logonUrl);

                    // Create redirect url
                    logonUrl = URLHelper.ResolveUrl(logonUrl) + "?ReturnURL=" + HttpUtility.UrlEncode(URLHelper.CurrentURL);
                    URLHelper.Redirect(logonUrl);
                }
                else
                {
                    // Get invitation by GUID
                    Guid invitationGuid = QueryHelper.GetGuid("invitationguid", Guid.Empty);
                    if (invitationGuid != Guid.Empty)
                    {
                        InvitationInfo invitation = InvitationInfoProvider.GetInvitationInfo(invitationGuid);
                        if (invitation != null)
                        {
                            // Check if invitation is valid
                            if ((invitation.InvitationValidTo == DateTimeHelper.ZERO_TIME) ||
                                (invitation.InvitationValidTo >= DateTime.Now))
                            {
                                GroupInfo group = GroupInfoProvider.GetGroupInfo(invitation.InvitationGroupID);

                                if (group != null)
                                {
                                    // Check whether current user is the user who should be invited
                                    if ((invitation.InvitedUserID > 0) && (invitation.InvitedUserID != CMSContext.CurrentUser.UserID))
                                    {
                                        lblInfo.CssClass = "InvitationErrorLabel";
                                        lblInfo.Text = InvitationIsNotValid;
                                        lblInfo.Visible = true;
                                        return;
                                    }

                                    // If user was invited by e-mail
                                    if (invitation.InvitedUserID == 0)
                                    {
                                        invitation.InvitedUserID = CMSContext.CurrentUser.UserID;
                                    }

                                    if (!GroupMemberInfoProvider.IsMemberOfGroup(invitation.InvitedUserID, invitation.InvitationGroupID))
                                    {
                                        // Create group member info object
                                        GroupMemberInfo groupMember = new GroupMemberInfo();
                                        groupMember.MemberInvitedByUserID = invitation.InvitedByUserID;
                                        groupMember.MemberUserID = CMSContext.CurrentUser.UserID;
                                        groupMember.MemberGroupID = invitation.InvitationGroupID;
                                        groupMember.MemberJoined = DateTime.Now;

                                        // Set proper status depending on grouo settings
                                        switch (group.GroupApproveMembers)
                                        {
                                            // Only approved members can join
                                            case GroupApproveMembersEnum.ApprovedCanJoin:
                                                groupMember.MemberStatus = GroupMemberStatus.WaitingForApproval;
                                                lblInfo.Text = MemberWaiting.Replace("##GROUPNAME##", HTMLHelper.HTMLEncode(group.GroupDisplayName));
                                                break;
                                            // Only invited members
                                            case GroupApproveMembersEnum.InvitedWithoutApproval:
                                            // Any site members can join
                                            case GroupApproveMembersEnum.AnyoneCanJoin:
                                                groupMember.MemberApprovedWhen = DateTime.Now;
                                                groupMember.MemberStatus = GroupMemberStatus.Approved;
                                                lblInfo.Text = MemberJoined.Replace("##GROUPNAME##", HTMLHelper.HTMLEncode(group.GroupDisplayName));
                                                break;
                                        }
                                        // Store info object to database
                                        GroupMemberInfoProvider.SetGroupMemberInfo(groupMember);

                                        // Handle sending e-mails
                                        if (SendEmailToInviter || SendDefaultGroupEmails)
                                        {
                                            UserInfo sender = UserInfoProvider.GetFullUserInfo(groupMember.MemberUserID);
                                            UserInfo recipient = UserInfoProvider.GetFullUserInfo(groupMember.MemberInvitedByUserID);

                                            if (SendEmailToInviter)
                                            {
                                                EmailTemplateInfo template = EmailTemplateProvider.GetEmailTemplate("Groups.MemberAcceptedInvitation", CMSContext.CurrentSiteName);

                                                // Resolve macros
                                                MacroResolver resolver = CMSContext.CurrentResolver;
                                                resolver.SourceData = new object[] { sender, recipient, group, groupMember };
                                                resolver.SetNamedSourceData("Sender", sender);
                                                resolver.SetNamedSourceData("Recipient", recipient);
                                                resolver.SetNamedSourceData("Group", group);
                                                resolver.SetNamedSourceData("GroupMember", groupMember);

                                                if (!String.IsNullOrEmpty(recipient.Email) && !String.IsNullOrEmpty(sender.Email))
                                                {
                                                    // Send e-mail
                                                    EmailMessage message = new EmailMessage();
                                                    message.Recipients = recipient.Email;
                                                    message.From = EmailHelper.GetSender(template, SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress"));
                                                    message.Subject = resolver.ResolveMacros(template.TemplateSubject);
                                                    message.PlainTextBody = resolver.ResolveMacros(template.TemplatePlainText);

                                                    // Enable macro encoding for body
                                                    resolver.EncodeResolvedValues = true;
                                                    message.Body = resolver.ResolveMacros(template.TemplateText);
                                                    message.CcRecipients = template.TemplateCc;
                                                    message.BccRecipients = template.TemplateBcc;
                                                    message.EmailFormat = EmailFormatEnum.Default;

                                                    MetaFileInfoProvider.ResolveMetaFileImages(message, template.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                                                    EmailSender.SendEmail(CMSContext.CurrentSiteName, message);
                                                }
                                            }

                                            if (SendDefaultGroupEmails)
                                            {
                                                if (group.GroupSendJoinLeaveNotification &&
                                                    (groupMember.MemberStatus == GroupMemberStatus.Approved))
                                                {
                                                    GroupMemberInfoProvider.SendNotificationMail("Groups.MemberJoin", CMSContext.CurrentSiteName, groupMember, true);
                                                    GroupMemberInfoProvider.SendNotificationMail("Groups.MemberJoinedConfirmation", CMSContext.CurrentSiteName, groupMember, false);
                                                }
                                                if (group.GroupSendWaitingForApprovalNotification && (groupMember.MemberStatus == GroupMemberStatus.WaitingForApproval))
                                                {
                                                    GroupMemberInfoProvider.SendNotificationMail("Groups.MemberWaitingForApproval", CMSContext.CurrentSiteName, groupMember, true);
                                                    GroupMemberInfoProvider.SendNotificationMail("Groups.MemberJoinedWaitingForApproval", CMSContext.CurrentSiteName, groupMember, false);
                                                }
                                            }
                                        }

                                        // Delete all invitations to specified group for specified user (based on e-mail or userId)
                                        string whereCondition = "InvitationGroupID = " + invitation.InvitationGroupID + " AND (InvitedUserID=" + CMSContext.CurrentUser.UserID + " OR InvitationUserEmail = N'" + SqlHelperClass.GetSafeQueryString(CMSContext.CurrentUser.Email, false) + "')";
                                        InvitationInfoProvider.DeleteInvitations(whereCondition);

                                        // Log activity
                                        LogJoinActivity(groupMember, group.GroupLogActivity, group.GroupDisplayName);
                                    }
                                    else
                                    {
                                        lblInfo.Text = UserIsAlreadyMember.Replace("##GROUPNAME##", HTMLHelper.HTMLEncode(group.GroupDisplayName));
                                        lblInfo.CssClass = "InvitationErrorLabel";

                                        // Delete this invitation
                                        InvitationInfoProvider.DeleteInvitationInfo(invitation);
                                    }
                                }
                                else
                                {
                                    lblInfo.Text = GroupNoLongerExists;
                                    lblInfo.CssClass = "InvitationErrorLabel";
                                    // Delete this invitation
                                    InvitationInfoProvider.DeleteInvitationInfo(invitation);
                                }
                            }
                            else
                            {
                                lblInfo.Text = InvitationIsNotValid;
                                lblInfo.CssClass = "InvitationErrorLabel";
                                // Delete this invitation
                                InvitationInfoProvider.DeleteInvitationInfo(invitation);
                            }
                        }
                        else
                        {
                            lblInfo.Text = InvitationNoLongerExists;
                            lblInfo.CssClass = "InvitationErrorLabel";
                        }
                        lblInfo.Visible = true;
                    }
                    else
                    {
                        Visible = false;
                    }
                }
            }
        }
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
}
