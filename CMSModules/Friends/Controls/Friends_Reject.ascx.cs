using System;

using CMS.Community;
using CMS.GlobalHelper;

public partial class CMSModules_Friends_Controls_Friends_Reject : FriendsActionControl
{
    #region "Button handling"

    protected void btnReject_Click(object senderObject, EventArgs e)
    {
        RaiseOnCheckPermissions(PERMISSION_MANAGE, this);

        // Set up control
        Comment = txtComment.Text;
        SendMail = chkSendEmail.Checked;
        SendMessage = chkSendMessage.Checked;

        lblError.Text = PerformAction(FriendsActionEnum.Reject);

        bool error = (lblError.Text != string.Empty);
        lblError.Visible = error;
        lblInfo.Visible = !error;

        if (!error)
        {
            // Register wopener script
            ScriptHelper.RegisterWOpenerScript(Page);

            btnReject.Enabled = false;
            txtComment.Enabled = false;
            chkSendEmail.Enabled = false;
            chkSendMessage.Enabled = false;
            btnCancel.ResourceString = "general.close";
            lblInfo.ResourceString = "friends.friendshiprejected";
            btnCancel.OnClientClick = "if((wopener != null) && (wopener.refreshList != null)){wopener.refreshList();}window.close();return false;";
        }
    }

    #endregion


    /// <summary>
    /// Formats username of sender and recipients.
    /// </summary>
    public override string GetFormattedUserName(string userName, string fullName, string nickName)
    {
        return Functions.GetFormattedUserName(userName, fullName, nickName, this.IsLiveSite);
    }
}
