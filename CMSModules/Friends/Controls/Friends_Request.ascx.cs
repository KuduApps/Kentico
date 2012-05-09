using System;
using System.Collections;

using CMS.CMSHelper;
using CMS.Community;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Friends_Controls_Friends_Request : FriendsActionControl
{
    #region "Public properties"

    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            selectUser.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set user id
        UserID = QueryHelper.GetInteger("userid", 0);
        RequestedUserID = QueryHelper.GetInteger("requestid", 0);
        int siteId = QueryHelper.GetInteger("siteid", -1);


        if (RequestedUserID != 0)
        {
            plcUserSelect.Visible = false;
        }

        bool isGlobalAdmin = CMSContext.CurrentUser.IsGlobalAdministrator;

        // Show site filter for global admin only in sitemanager
        selectUser.ShowSiteFilter = ((siteId == 0) && isGlobalAdmin);

        selectUser.WhereCondition = "UserName NOT LIKE N'public'";

        if (IsLiveSite && !isGlobalAdmin)
        {
            selectUser.HideDisabledUsers = true;
            selectUser.HideHiddenUsers = true;
            selectUser.HideNonApprovedUsers = true;
        }

        // Enable automatic approval
        plcAdministrator.Visible = CanApprove();

        if (!ModuleEntry.IsModuleLoaded(ModuleEntry.MESSAGING))
        {
            chkSendMessage.Visible = false;
            chkSendMessage.Checked = false;
        }
    }


    /// <summary>
    /// Formats username of sender and recipients.
    /// </summary>
    public override string GetFormattedUserName(string userName, string fullName, string nickName)
    {
        return Functions.GetFormattedUserName(userName, fullName, nickName, this.IsLiveSite);
    }

    #endregion


    #region "Button handling"

    protected void btnRequest_Click(object senderObject, EventArgs e)
    {
        RaiseOnCheckPermissions(PERMISSION_MANAGE, this);

        string message = string.Empty;

        // Requested user id not set explicitly
        if (RequestedUserID == 0)
        {
            RequestedUserID = ValidationHelper.GetInteger(selectUser.Value, 0);
        }

        // Both users have to be specified
        if ((RequestedUserID == 0) || (UserID == 0))
        {
            message = GetString("friends.friendrequired");
        }
        else
        {
            bool friendshipExists = FriendInfoProvider.FriendshipExists(UserID, RequestedUserID);

            if (!friendshipExists)
            {
                // Set up control
                Comment = txtComment.Text;
                SendMail = chkSendEmail.Checked;
                SendMessage = chkSendMessage.Checked;
                SelectedFriends = new ArrayList();
                SelectedFriends.Add(RequestedUserID);
                AutomaticApprovment = chkAutomaticApprove.Checked;

                message = PerformAction(FriendsActionEnum.Request);
            }
            else
            {
                message = GetString("friends.friendshipexists");
            }
        }

        bool error = (message != string.Empty);

        lblError.Visible = error;
        lblInfo.Visible = !error;

        if (error)
        {
            lblError.Text = message;
        }
        else
        {
            // Register wopener script
            ScriptHelper.RegisterWOpenerScript(Page);

            btnRequest.Enabled = false;
            selectUser.Enabled = false;
            txtComment.Enabled = false;
            chkAutomaticApprove.Enabled = false;
            chkSendEmail.Enabled = false;
            chkSendMessage.Enabled = false;
            btnCancel.ResourceString = "general.close";
            lblInfo.ResourceString = "friends.friendshiprequested";
            btnCancel.OnClientClick = "if((wopener != null) && (wopener.refreshList != null)){wopener.refreshList();}window.close();return false;";
        }
    }

    #endregion
}
