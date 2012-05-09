using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.Messaging;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.PortalControls;
using CMS.ExtendedControls;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Messaging_Controls_Inbox : CMSUserControl
{
    #region "Variables & enums"

    private string mZeroRowsText = null;
    private int mPageSize = 10;
    private bool mPasteOriginalMessage = true;
    private bool mShowOriginalMessage = true;
    protected string currentViewButtonClientId = null;
    private TimeZoneInfo usedTimeZone = null;
    private UserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;
    private MessageInfo mMessage = null;

    protected enum What
    {
        SelectedMessages = 0,
        AllMessages = 1
    }

    protected enum Action
    {
        SelectAction = 0,
        MarkAsRead = 1,
        MarkAsUnread = 2,
        Delete = 3
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Current message ID.
    /// </summary>
    protected int CurrentMessageId
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["CurrentMessageId"], 0);
        }
        set
        {
            ViewState["CurrentMessageId"] = value;
            if (value <= 0)
            {
                mMessage = null;
            }
        }
    }


    /// <summary>
    /// Current message.
    /// </summary>
    protected MessageInfo Message
    {
        get
        {
            if ((mMessage == null) && (CurrentMessageId > 0))
            {
                mMessage = MessageInfoProvider.GetMessageInfo(CurrentMessageId);
            }
            return mMessage;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return mZeroRowsText;
        }
        set
        {
            mZeroRowsText = value;
            EnsureChildControls();
            inboxGrid.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Size of the page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return mPageSize;
        }
        set
        {
            mPageSize = value;
            EnsureChildControls();
            inboxGrid.PageSize = value.ToString();
        }
    }


    /// <summary>
    /// Inner grid control.
    /// </summary>
    public UniGrid Grid
    {
        get
        {
            EnsureChildControls();
            return inboxGrid;
        }
    }


    /// <summary>
    /// True if original message should be pasted to the current.
    /// </summary>
    public bool PasteOriginalMessage
    {
        get
        {
            return mPasteOriginalMessage;
        }
        set
        {
            mPasteOriginalMessage = value;
            EnsureChildControls();
            ucSendMessage.PasteOriginalMessage = value;
        }
    }


    /// <summary>
    /// True if original message should be shown.
    /// </summary>
    public bool ShowOriginalMessage
    {
        get
        {
            return mShowOriginalMessage;
        }
        set
        {
            mShowOriginalMessage = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void EnsureChildControls()
    {
        base.EnsureChildControls();

        if (inboxGrid == null)
        {
            pnlInbox.LoadContainer();
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        SetupControls();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Set info message
        if (ucViewMessage.InformationText != string.Empty)
        {
            lblInfo.Visible = true;
            lblInfo.Text = ucViewMessage.InformationText;
        }

        // Set error message
        if (ucViewMessage.ErrorText != string.Empty)
        {
            lblError.Visible = true;
            lblError.Text = ucViewMessage.ErrorText;
        }

        // Hide footer action panel
        pnlAction.Visible = inboxGrid.Visible && (inboxGrid.GridView.Rows.Count > 0);
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Sets up controls.
    /// </summary>
    public void SetupControls()
    {
        if (StopProcessing)
        {
            // Do nothing
            ucSendMessage.StopProcessing = true;
            ucViewMessage.StopProcessing = true;
            inboxGrid.StopProcessing = true;
        }
        else
        {
            bool isPostBack = RequestHelper.IsPostBack();

            if (!isPostBack)
            {
                inboxGrid.DelayedReload = false;
            }
            else
            {
                // Find postback invoker
                if (!RequestHelper.CausedPostback(inboxGrid, btnNewMessage, ucSendMessage.SendButton, ucSendMessage.CancelButton, btnBackToList, btnReply, btnHidden, btnForward, btnDelete, btnOk))
                {
                    inboxGrid.DelayedReload = false;
                }

                SetupLabels();
            }

            // Show content only for authenticated users
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                // Show control
                Visible = true;
                // Initialize unigrid
                inboxGrid.OnExternalDataBound += inboxGrid_OnExternalDataBound;
                inboxGrid.GridView.RowDataBound += GridView_RowDataBound;
                inboxGrid.OnAction += inboxGrid_OnAction;
                // Set where condition clause
                inboxGrid.WhereCondition = "MessageRecipientUserID=" + CMSContext.CurrentUser.UserID + " AND (MessageRecipientDeleted=0 OR MessageRecipientDeleted IS NULL)";
                inboxGrid.GridView.PageSize = PageSize;
                inboxGrid.OnBeforeDataReload += inboxGrid_OnBeforeDataReload;
                inboxGrid.OnBeforeSorting += inboxGrid_OnBeforeSorting;
                inboxGrid.OnShowButtonClick += inboxGrid_OnShowButtonClick;
                inboxGrid.OnPageSizeChanged += inboxGrid_OnPageSizeChanged;
                inboxGrid.IsLiveSite = IsLiveSite;

                // Setup inner controls
                ucSendMessage.IsLiveSite = IsLiveSite;
                ucViewMessage.IsLiveSite = IsLiveSite;
                ucViewMessage.MessageMode = MessageModeEnum.Inbox;
                ucViewMessage.Message = Message;

                // Hide helper labels
                lblInfo.Visible = false;
                lblError.Visible = false;

                // Create and assign javascript confirmation
                btnDelete.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("Messsaging.DeletionConfirmation")) + ");"; ;

                // Set images
                imgDelete.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/delete.png", IsLiveSite);
                imgDelete.ToolTip = GetString("Messaging.Delete");
                imgDelete.AlternateText = GetString("Messaging.Delete"); ;
                imgForward.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/forward.png", IsLiveSite);
                imgForward.ToolTip = GetString("Messaging.Forward");
                imgForward.AlternateText = GetString("Messaging.Forward");
                imgReply.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/reply.png", IsLiveSite);
                imgReply.ToolTip = GetString("Messaging.Reply");
                imgReply.AlternateText = GetString("Messaging.Reply");
                imgNew.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_Messaging/newmessage.png", IsLiveSite);
                imgNew.ToolTip = GetString("Messaging.NewMessage");
                imgNew.AlternateText = GetString("Messaging.NewMessage");

                // Register events
                ucSendMessage.SendButtonClick += ucSendMessage_SendButtonClick;
                btnNewMessage.Click += btnNewMessage_Click;
                btnReply.Click += btnReply_Click;
                btnForward.Click += btnForward_Click;
                btnDelete.Click += btnDelete_Click;
                btnBackToList.Click += btnBackToList_Click;
                btnHidden.Click += new EventHandler(btnHidden_Click);

                StringBuilder actionScript = new StringBuilder();
                actionScript.Append(
@"
function PerformAction_", ClientID, @"(selectionFunction, actionId, actionLabel, whatId) {
    var confirmation = null;
    var label = document.getElementById(actionLabel);
    var action = document.getElementById(actionId).value;
    var whatDrp = document.getElementById(whatId);
    if (action == '", (int)Action.SelectAction, @"') {
        label.innerHTML = '", GetString("MassAction.SelectSomeAction"), @"'
    }
    else if (eval(selectionFunction) && (whatDrp.value == '", (int)What.SelectedMessages, @"')) {
        label.innerHTML = '", GetString("Messaging.SelectMessages"), @"';
    }
    else {
        switch(action) {
            case '", (int)Action.MarkAsRead, @"':
                confirmation = ", ScriptHelper.GetString(GetString("Messaging.ConfirmMarkAsRead")), @";
                break;
            case '", (int)Action.MarkAsUnread, @"':
                confirmation = ", ScriptHelper.GetString(GetString("Messaging.ConfirmMarkAsUnread")), @";
                break;
            case '", (int)Action.Delete, @"':
                confirmation = ", ScriptHelper.GetString(GetString("Messaging.ConfirmDelete")), @";
                break;
            default:
                confirmation = null;
                break;
        }
        if (confirmation != null) {
            return confirm(confirmation)
        }
    }
    return false;
}
");

                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "actionScript", ScriptHelper.GetScript(actionScript.ToString()));

                string messageActionScript =
@"
function ContextMessageAction_" + inboxGrid.ClientID + @"(action, messageId) {
    document.getElementById('" + hdnValue.ClientID + @"').value = action + ';' + messageId;" +
    ControlsHelper.GetPostBackEventReference(btnHidden, null) + @";
}";
                ScriptHelper.RegisterStartupScript(this, typeof(string), "messageAction_" + ClientID + new Random(DateTime.Now.Millisecond).Next(), ScriptHelper.GetScript(messageActionScript));

                // Add action to button
                btnOk.OnClientClick = "return PerformAction_" + ClientID + "('" + inboxGrid.GetCheckSelectionScript() + "','" + drpAction.ClientID + "','" + lblActionInfo.ClientID + "','" + drpWhat.ClientID + "');";
                btnOk.Click += btnOk_Click;

                if (!isPostBack)
                {
                    // Initialize dropdown lists
                    drpWhat.Items.Add(new ListItem(GetString("Messaging." + What.SelectedMessages), Convert.ToInt32(What.SelectedMessages).ToString()));
                    drpWhat.Items.Add(new ListItem(GetString("Messaging." + What.AllMessages), Convert.ToInt32(What.AllMessages).ToString()));

                    drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
                    drpAction.Items.Add(new ListItem(GetString("Messaging.Action." + Action.MarkAsRead), Convert.ToInt32(Action.MarkAsRead).ToString()));
                    drpAction.Items.Add(new ListItem(GetString("Messaging.Action." + Action.MarkAsUnread), Convert.ToInt32(Action.MarkAsUnread).ToString()));
                    drpAction.Items.Add(new ListItem(GetString("Messaging.Action." + Action.Delete), Convert.ToInt32(Action.Delete).ToString()));
                }
            }
            else
            {
                Visible = false;
            }
        }
    }

    #endregion


    #region "Message actions"

    /// <summary>
    /// New message.
    /// </summary>
    private void NewMessage()
    {
        // Initialize new message control
        pnlBackToList.Visible = true;
        lblBackToList.ResourceString = "messaging.newmessage";
        pnlList.Visible = false;
        pnlNew.Visible = true;
        pnlActions.Visible = false;

        // Initilaize new and view message
        ucSendMessage.StopProcessing = false;
        ucSendMessage.SendMessageMode = MessageActionEnum.New;
        ucSendMessage.MessageId = 0;
        ucSendMessage.Message = null;
        ucSendMessage.ReloadData();
    }


    /// <summary>
    /// Reply message.
    /// </summary>
    private void ReplyMessage()
    {
        pnlBackToList.Visible = true;
        lblBackToList.ResourceString = "messaging.replytomessage";
        pnlList.Visible = false;
        pnlNew.Visible = true;
        pnlView.Visible = ShowOriginalMessage;
        pnlActions.Visible = false;

        // Initilaize new and view message
        ucSendMessage.StopProcessing = false;
        ucSendMessage.Message = Message;
        ucSendMessage.MessageSubject = GetString("Messaging.ReSign");
        ucSendMessage.SendMessageMode = MessageActionEnum.Reply;
        ucSendMessage.ReloadData();
        ucViewMessage.StopProcessing = false;
        ucViewMessage.Message = Message;
        ucViewMessage.ReloadData();
        lblNewMessageHeader.ResourceString = "Messaging.Reply";
        lblViewMessageHeader.ResourceString = "Messaging.OriginalMessage";
    }


    /// <summary>
    /// Forward message.
    /// </summary>
    private void ForwardMesssage()
    {
        pnlBackToList.Visible = true;
        lblBackToList.ResourceString = "messaging.forwardmessage";
        pnlList.Visible = false;
        pnlNew.Visible = true;
        pnlView.Visible = ShowOriginalMessage;
        pnlActions.Visible = false;

        // Initilaize new and view message
        ucSendMessage.StopProcessing = false;
        ucSendMessage.Message = Message;
        ucSendMessage.MessageSubject = GetString("Messaging.ForwardSign");
        ucSendMessage.SendMessageMode = MessageActionEnum.Forward;
        ucSendMessage.ReloadData();
        ucViewMessage.StopProcessing = false;
        ucViewMessage.Message = Message;
        ucViewMessage.ReloadData();
        lblNewMessageHeader.ResourceString = "Messaging.Forward";
        lblViewMessageHeader.ResourceString = "Messaging.OriginalMessage";
    }


    /// <summary>
    /// View message.
    /// </summary>
    private void ViewMessage()
    {
        pnlActions.Visible = true;
        pnlList.Visible = false;
        pnlNew.Visible = false;
        pnlView.Visible = true;
        pnlBackToList.Visible = true;
        lblBackToList.ResourceString = "messaging.viewmessage";

        // Initialize view message control
        ucViewMessage.StopProcessing = false;
        ucViewMessage.Message = Message;
        ucViewMessage.ReloadData();
    }


    /// <summary>
    /// Sets message to read message.
    /// </summary>
    private void ReadMessage()
    {
        if (Message != null)
        {
            bool updateMessage = false;

            // Check message read date
            if (Message.MessageRead == DateTimeHelper.ZERO_TIME)
            {
                Message.MessageRead = DateTime.Now;
                updateMessage = true;
            }

            // Check message read flag
            if (!Message.MessageIsRead)
            {
                Message.MessageIsRead = true;
                updateMessage = true;
            }

            // Update message
            if (updateMessage)
            {
                MessageInfoProvider.SetMessageInfo(Message);
            }
        }
    }


    /// <summary>
    /// Delete message.
    /// </summary>
    private void DeleteMessage()
    {
        try
        {
            inboxGrid.DelayedReload = false;
            MessageInfoProvider.DeleteReceivedMessage(CurrentMessageId);
            lblInfo.Visible = true;
            lblInfo.Text = GetString("Messsaging.MessageDeleted");
            pnlList.Visible = true;
            pnlNew.Visible = false;
            pnlView.Visible = false;
            pnlBackToList.Visible = false;
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }


    /// <summary>
    /// Mark message as read.
    /// </summary>
    private void MarkAsRead()
    {
        if (Message != null)
        {
            // Mark as read
            if ((Message.MessageRead == DateTimeHelper.ZERO_TIME) || !Message.MessageIsRead)
            {
                Message.MessageIsRead = true;
                if (Message.MessageRead == DateTimeHelper.ZERO_TIME)
                {
                    Message.MessageRead = DateTime.Now;
                }
                MessageInfoProvider.SetMessageInfo(Message);
            }
        }

        inboxGrid.DelayedReload = false;
    }


    /// <summary>
    /// Mark message as unread.
    /// </summary>
    private void MarkAsUnread()
    {
        if (Message != null)
        {
            // Mark as unread
            if (Message.MessageIsRead)
            {
                Message.MessageIsRead = false;
                MessageInfoProvider.SetMessageInfo(Message);
            }
        }

        inboxGrid.DelayedReload = false;
    }


    /// <summary>
    /// Perform selected action (Mark as read, Mark as unread, Delete).
    /// </summary>
    private void PerformAction()
    {
        string resultMessage = string.Empty;

        Action action = (Action)ValidationHelper.GetInteger(drpAction.SelectedItem.Value, 0);
        What what = (What)ValidationHelper.GetInteger(drpWhat.SelectedItem.Value, 0);

        string where = null;

        // All messages
        if (what == What.AllMessages)
        {
            resultMessage = GetString("Messaging." + What.AllMessages);
        }
        // Selected messages
        else if (what == What.SelectedMessages)
        {
            where = SqlHelperClass.GetWhereCondition<int>("MessageID", (string[])inboxGrid.SelectedItems.ToArray(typeof(string)), false);
            resultMessage = GetString("Messaging." + What.SelectedMessages);
        }
        else
        {
            return;
        }

        // Action 'Delete'
        if ((action == Action.Delete))
        {
            MessageInfoProvider.DeleteReceivedMessages(CMSContext.CurrentUser.UserID, where);
            resultMessage += " " + GetString("Messaging.Action.Result.Deleted");
        }
        // Action 'Mark as read'
        else if ((action == Action.MarkAsRead))
        {
            MessageInfoProvider.MarkReadReceivedMessages(CMSContext.CurrentUser.UserID, where, DateTime.Now);
            resultMessage += " " + GetString("Messaging.Action.Result.MarkAsRead");
        }
        // Action 'Mark as unread'
        else if (action == Action.MarkAsUnread)
        {
            MessageInfoProvider.MarkUnreadReceivedMessages(CMSContext.CurrentUser.UserID, where);
            resultMessage += " " + GetString("Messaging.Action.Result.MarkAsUnread");
        }
        else
        {
            return;
        }

        lblInfo.Text = resultMessage;
        lblInfo.Visible = true;

        if (!string.IsNullOrEmpty(resultMessage))
        {
            lblInfo.Text = resultMessage;
            lblInfo.Visible = true;
        }

        inboxGrid.ClearSelectedItems();
        inboxGrid.ReloadData();
    }

    #endregion


    #region "Buttons actions"

    void btnHidden_Click(object sender, EventArgs e)
    {
        // Process message action
        string[] args = hdnValue.Value.Split(';');
        if (args.Length == 2)
        {
            inboxGrid_OnAction(args[0], args[1]);
            inboxGrid.ReloadData();
        }
    }


    protected void inboxGrid_OnShowButtonClick(object sender, EventArgs e)
    {
        inboxGrid.DelayedReload = false;
    }


    protected void btnBackToList_Click(object sender, EventArgs e)
    {
        pnlList.Visible = true;
        pnlNew.Visible = false;
        pnlView.Visible = false;
        pnlBackToList.Visible = false;
        ucSendMessage.SendMessageMode = MessageActionEnum.None;
        inboxGrid.ReloadData();
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteMessage();
        inboxGrid.ReloadData();
    }


    protected void btnForward_Click(object sender, EventArgs e)
    {
        ForwardMesssage();
    }


    protected void btnReply_Click(object sender, EventArgs e)
    {
        ReplyMessage();
    }


    protected void btnNewMessage_Click(object sender, EventArgs e)
    {
        NewMessage();
    }


    protected void ucSendMessage_SendButtonClick(object sender, EventArgs e)
    {
        // If no error, inform user
        if (ucSendMessage.ErrorText == string.Empty)
        {
            lblInfo.Visible = true;
            lblInfo.Text = ucSendMessage.InformationText;
            btnBackToList_Click(sender, e);
        }
        else
        {
            ucViewMessage.StopProcessing = false;
            SetupLabels();
            ucViewMessage.ReloadData();
            pnlActions.Visible = false;
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        PerformAction();
    }

    #endregion


    #region "Grid methods"

    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView rowView = ((DataRowView)e.Row.DataItem);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Mark unread message
            DateTime messageRead = ValidationHelper.GetDateTime(DataHelper.GetDataRowViewValue(rowView, "MessageRead"), DateTimeHelper.ZERO_TIME);
            bool messageIsRead = ValidationHelper.GetBoolean(DataHelper.GetDataRowViewValue(rowView, "MessageIsRead"), true);
            if ((messageRead == DateTimeHelper.ZERO_TIME) || !messageIsRead)
            {
                e.Row.CssClass += " Unread";
            }
        }
    }


    protected void inboxGrid_OnBeforeSorting(object sender, EventArgs e)
    {
        inboxGrid.ReloadData();
    }

    protected void inboxGrid_OnPageSizeChanged()
    {
        inboxGrid.ReloadData();
    }


    protected void inboxGrid_OnBeforeDataReload()
    {
        // Bind grid footer
        BindFooter();
    }


    protected object inboxGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "messagesendernickname":
            case "messagesubject":
                // Avoid XSS
                return HTMLHelper.HTMLEncode(Convert.ToString(parameter));

            case "messagesent":
                if (currentUserInfo == null)
                {
                    currentUserInfo = CMSContext.CurrentUser;
                }
                if (currentSiteInfo == null)
                {
                    currentSiteInfo = CMSContext.CurrentSite;
                }
                DateTime currentDateTime = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                if (IsLiveSite)
                {
                    return CMSContext.ConvertDateTime(currentDateTime, this);
                }
                else
                {
                    return TimeZoneHelper.GetCurrentTimeZoneDateTimeString(currentDateTime, currentUserInfo, currentSiteInfo, out usedTimeZone);
                }
        }
        return parameter;
    }


    protected void inboxGrid_OnAction(string actionName, object actionArgument)
    {
        CurrentMessageId = ValidationHelper.GetInteger(actionArgument, 0);
        
        // Force reinitialization of message object if differs from currently processed message
        if ((Message != null) && (Message.MessageID != CurrentMessageId))
        {
            mMessage = null;
        }

        switch (actionName)
        {
            // Delete message
            case "delete":
                DeleteMessage();
                break;

            // Reply message
            case "reply":
                ReadMessage();
                ReplyMessage();
                break;

            // View message
            case "view":
                ReadMessage();
                ViewMessage();
                break;

            // Forward message
            case "forward":
                ReadMessage();
                ForwardMesssage();
                break;

            // Mark message as read
            case "markread":
                MarkAsRead();
                break;

            // Mark message as unread
            case "markunread":
                MarkAsUnread();
                break;
        }
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Bind the data.
    /// </summary>
    protected void BindFooter()
    {
        int userId = CMSContext.CurrentUser.UserID;
        lblFooter.Text = string.Format(GetString("Messaging.UnreadOfAll"), MessageInfoProvider.GetUnreadMessagesCount(userId), MessageInfoProvider.GetMessagesCount(userId));
    }

    /// <summary>
    /// Sel label values.
    /// </summary>
    protected void SetupLabels()
    {
        if (pnlNew.Visible)
        {
            switch (ucSendMessage.SendMessageMode)
            {
                case MessageActionEnum.New:
                    lblBackToList.ResourceString = "messaging.newmessage";
                    break;

                case MessageActionEnum.Forward:
                    lblBackToList.ResourceString = "messaging.forwardmessage";
                    lblNewMessageHeader.ResourceString = "Messaging.Forward";
                    lblViewMessageHeader.ResourceString = "Messaging.OriginalMessage";
                    break;

                case MessageActionEnum.Reply:
                    lblBackToList.ResourceString = "messaging.replytomessage";
                    lblNewMessageHeader.ResourceString = "Messaging.Reply";
                    lblViewMessageHeader.ResourceString = "Messaging.OriginalMessage";
                    break;

            }

            pnlActions.Visible = false;
        }
        else
        {
            if (pnlView.Visible)
            {
                lblBackToList.ResourceString = "messaging.viewmessage";
            }
        }

        ucViewMessage.StopProcessing = !pnlView.Visible;
    }

    #endregion
}
