using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Messaging;
using CMS.UIControls;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Messaging_Controls_ViewMessage : CMSUserControl
{
    #region "Variables"

    private MessageModeEnum mMessageMode = MessageModeEnum.Inbox;
    protected MessageInfo mMessage = null;
    protected UserInfo currentUserInfo = null;
    protected UserInfo messageUserInfo = null;
    private TimeZoneInfo usedTimeZone = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Message ID.
    /// </summary>
    public int MessageId
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["MessageId"], 0);
        }
        set
        {
            ViewState["MessageId"] = value;
            if (value <= 0)
            {
                mMessage = null;
            }
        }
    }


    /// <summary>
    /// Message.
    /// </summary>
    public MessageInfo Message
    {
        get
        {
            if ((mMessage == null) && (MessageId > 0))
            {
                mMessage = MessageInfoProvider.GetMessageInfo(MessageId);
            }
            return mMessage;
        }
        set
        {
            mMessage = value;
            if (mMessage != null)
            {
                ViewState["MessageId"] = mMessage.MessageID;
            }
        }
    }


    /// <summary>
    /// Message mode.
    /// </summary>
    public MessageModeEnum MessageMode
    {
        get
        {
            return mMessageMode;
        }
        set
        {
            mMessageMode = value;
        }
    }


    /// <summary>
    /// Information text.
    /// </summary>
    public string InformationText
    {
        get
        {
            return ucMessageUserButtons.InformationText;
        }
    }


    /// <summary>
    /// Error text.
    /// </summary>
    public string ErrorText
    {
        get
        {
            return ucMessageUserButtons.ErrorText;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Message id is set, display message details
        if (Message != null)
        {
            // Find postback invoker
            string invokerName = Page.Request.Params.Get("__EVENTTARGET");
            // If postback was caused by user buttons
            if (invokerName.Contains(ucMessageUserButtons.UniqueID))
            {
                StopProcessing = false;
            }
            ReloadData();
        }
    }

    #endregion


    #region "Other methods"

    public void ReloadData()
    {
        if (StopProcessing)
        {
            // Do nothing
            ucMessageUserButtons.StopProcessing = true;
            ucUserPicture.StopProcessing = true;
        }
        else
        {
            ucMessageUserButtons.StopProcessing = false;
            ucUserPicture.StopProcessing = false;

            if (Message != null)
            {
                // Get current user info
                currentUserInfo = CMSContext.CurrentUser;
                // Get message user info
                if (MessageMode == MessageModeEnum.Inbox)
                {
                    messageUserInfo = UserInfoProvider.GetUserInfo(Message.MessageSenderUserID);
                }
                else
                {
                    messageUserInfo = UserInfoProvider.GetUserInfo(Message.MessageRecipientUserID);
                }

                // Display only to authorized user
                if ((currentUserInfo.UserID == Message.MessageRecipientUserID) || (currentUserInfo.UserID == Message.MessageSenderUserID) || currentUserInfo.IsGlobalAdministrator)
                {
                    pnlViewMessage.Visible = true;
                    lblDateCaption.Text = GetString("Messaging.Date");
                    lblSubjectCaption.Text = GetString("general.subject");
                    lblFromCaption.Text = (MessageMode == MessageModeEnum.Inbox) ? GetString("Messaging.From") : GetString("Messaging.To");

                    // Sender exists
                    if (messageUserInfo != null)
                    {
                        ucUserPicture.Visible = true;
                        ucUserPicture.UserID = messageUserInfo.UserID;

                        // Disable message user buttons on live site for hidden or disabled users
                        if (IsLiveSite && !currentUserInfo.IsGlobalAdministrator && (!messageUserInfo.Enabled || messageUserInfo.UserIsHidden))
                        {
                            ucMessageUserButtons.RelatedUserId = 0;
                        }
                        else
                        {
                            ucMessageUserButtons.RelatedUserId = messageUserInfo.UserID;
                        }
                        lblFrom.Text = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(messageUserInfo.UserName, messageUserInfo.FullName, messageUserInfo.UserNickName, IsLiveSite));
                    }
                    else
                    {
                        ucMessageUserButtons.RelatedUserId = 0;
                        lblFrom.Text = HTMLHelper.HTMLEncode(Message.MessageSenderNickName);
                    }
                    string body = Message.MessageBody;
                    // Resolve macros
                    DiscussionMacroHelper dmh = new DiscussionMacroHelper();
                    body = dmh.ResolveMacros(body);

                    lblSubject.Text = HTMLHelper.HTMLEncodeLineBreaks(Message.MessageSubject);
                    if (IsLiveSite)
                    {
                        lblDate.Text = CMSContext.ConvertDateTime(Message.MessageSent, this).ToString();
                    }
                    else
                    {
                        lblDate.Text = TimeZoneHelper.GetCurrentTimeZoneDateTimeString(Message.MessageSent, currentUserInfo, CMSContext.CurrentSite, out usedTimeZone);
                    }
                    lblBody.Text = body;
                }
            }
            else
            {
                lblError.Text = GetString("Messaging.MessageDoesntExist");
                lblError.Visible = true;
            }
        }
    }

    #endregion
}