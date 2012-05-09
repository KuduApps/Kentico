using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Messaging;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Messaging_Controls_Outbox : CMSUserControl
{
    #region "Variables & enums"

    private string mZeroRowsText = null;
    private int mPageSize = 10;
    private bool mShowOriginalMessage = true;
    private bool mMarkReadMessage = false;
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
        Delete = 1
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Current message id.
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
            outboxGrid.ZeroRowsText = value;
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
            return outboxGrid;
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
            outboxGrid.PageSize = value.ToString();
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


    /// <summary>
    /// Mark read messages.
    /// </summary>
    public bool MarkReadMessage
    {
        get
        {
            return mMarkReadMessage;
        }
        set
        {
            mMarkReadMessage = value;
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


    #region "Page events"

    protected override void EnsureChildControls()
    {
        base.EnsureChildControls();
        if (outboxGrid == null)
        {
            pnlOutbox.LoadContainer();
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
        pnlAction.Visible = outboxGrid.Visible && (outboxGrid.GridView.Rows.Count > 0);
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
            outboxGrid.StopProcessing = true;
        }
        else
        {
            bool isPostBack = RequestHelper.IsPostBack();

            if (!isPostBack)
            {
                outboxGrid.DelayedReload = false;
            }
            else
            {
                // Find postback invoker
                if (!RequestHelper.CausedPostback(outboxGrid, btnNewMessage, ucSendMessage.SendButton, ucSendMessage.CancelButton, btnBackToList, btnForward, btnDelete, btnOk))
                {
                    outboxGrid.DelayedReload = false;
                }

                SetupLabels();
            }

            // Show content only for authenticated users
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                // Show control
                Visible = true;
                // Initialize unigrid
                outboxGrid.OnExternalDataBound += outboxGrid_OnExternalDataBound;
                outboxGrid.GridView.RowDataBound += GridView_RowDataBound;
                outboxGrid.OnAction += outboxGrid_OnAction;
                // Set where condition clause
                outboxGrid.WhereCondition = "MessageSenderUserID=" + CMSContext.CurrentUser.UserID + " AND (MessageSenderDeleted=0 OR MessageSenderDeleted IS NULL)";
                outboxGrid.GridView.DataBound += GridView_DataBound;
                outboxGrid.GridView.PageSize = PageSize;
                outboxGrid.OnBeforeDataReload += outboxGrid_OnBeforeDataReload;
                outboxGrid.OnBeforeSorting += outboxGrid_OnBeforeSorting;
                outboxGrid.OnShowButtonClick += outboxGrid_OnShowButtonClick;
                outboxGrid.OnPageSizeChanged += outboxGrid_OnPageSizeChanged;
                outboxGrid.IsLiveSite = IsLiveSite;
                // Setup inner controls
                ucSendMessage.IsLiveSite = IsLiveSite;
                ucViewMessage.IsLiveSite = IsLiveSite;
                ucViewMessage.MessageMode = MessageModeEnum.Outbox;
                ucViewMessage.Message = Message;

                // Hide helper labels
                lblInfo.Visible = false;
                lblError.Visible = false;

                // Create and assign javascripts confirmation
                btnDelete.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("Messsaging.DeletionConfirmation")) + ");"; ;

                // Set images
                imgDelete.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/delete.png", IsLiveSite);
                imgDelete.ToolTip = GetString("Messaging.Delete");
                imgDelete.AlternateText = GetString("Messaging.Delete");
                imgForward.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/forward.png", IsLiveSite);
                imgForward.ToolTip = GetString("Messaging.Forward");
                imgForward.AlternateText = GetString("Messaging.Forward");
                imgNew.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_Messaging/newmessage.png", IsLiveSite);
                imgNew.ToolTip = GetString("Messaging.NewMessage");
                imgNew.AlternateText = GetString("Messaging.NewMessage");

                // Register events
                ucSendMessage.SendButtonClick += ucSendMessage_SendButtonClick;
                btnNewMessage.Click += btnNewMessage_Click;
                btnForward.Click += btnForward_Click;
                btnDelete.Click += btnDelete_Click;
                btnBackToList.Click += btnBackToList_Click;

                StringBuilder actionScript = new StringBuilder();
                actionScript.Append(
@"function PerformAction_", ClientID, @"(selectionFunction, actionId, actionLabel, whatId) {
    var confirmation = null;
    var label = document.getElementById(actionLabel);
    var action = document.getElementById(actionId).value;
    var whatDrp = document.getElementById(whatId);
    if (action == '", (int)Action.SelectAction, @"') {
        label.innerHTML = '", GetString("MassAction.SelectSomeAction"), @"';
    }
    else if (eval(selectionFunction) && (whatDrp.value == '", (int)What.SelectedMessages, @"')) {
        label.innerHTML = '", GetString("Messaging.SelectMessages"), @"';
    }
    else if (action == '", (int)Action.Delete, @"') {
        confirmation = ", ScriptHelper.GetString(GetString("Messaging.ConfirmDelete")), @";
    }
    if (confirmation != null) {
        return confirm(confirmation);
    }
    return false;
}
");

                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "actionScript", ScriptHelper.GetScript(actionScript.ToString()));

                // Add action to button
                btnOk.OnClientClick = "return PerformAction_" + ClientID + "('" + outboxGrid.GetCheckSelectionScript() + "','" + drpAction.ClientID + "','" + lblActionInfo.ClientID + "','" + drpWhat.ClientID + "');";

                btnOk.Click += btnOk_Click;

                if (!isPostBack)
                {
                    // Initialize dropdown lists
                    drpWhat.Items.Add(new ListItem(GetString("Messaging." + What.SelectedMessages), Convert.ToInt32(What.SelectedMessages).ToString()));
                    drpWhat.Items.Add(new ListItem(GetString("Messaging." + What.AllMessages), Convert.ToInt32(What.AllMessages).ToString()));

                    drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
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
    /// Delete message.
    /// </summary>
    private void DeleteMessage()
    {
        try
        {
            MessageInfoProvider.DeleteSentMessage(CurrentMessageId);
            lblInfo.Visible = true;
            lblInfo.Text = GetString("Messsaging.MessageDeleted");
            pnlList.Visible = true;
            pnlNew.Visible = false;
            pnlView.Visible = false;
            pnlBackToList.Visible = false;
            outboxGrid.DelayedReload = false;
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }


    /// <summary>
    /// Perform selected action (Delete).
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
            where = SqlHelperClass.GetWhereCondition<int>("MessageID", (string[])outboxGrid.SelectedItems.ToArray(typeof(string)), false);
            resultMessage = GetString("Messaging." + What.SelectedMessages);
        }
        else
        {
            return;
        }

        // Action 'Delete'
        if ((action == Action.Delete))
        {
            // Delete selected messages
            MessageInfoProvider.DeleteSentMessages(CMSContext.CurrentUser.UserID, where);

            resultMessage += " " + GetString("Messaging.Action.Result.Deleted");

            lblInfo.Text = resultMessage;
            lblInfo.Visible = true;

            outboxGrid.ClearSelectedItems();
            outboxGrid.ReloadData();
        }
    }

    #endregion


    #region "Buttons actions"

    protected void outboxGrid_OnShowButtonClick(object sender, EventArgs e)
    {
        outboxGrid.DelayedReload = false;
    }


    protected void btnBackToList_Click(object sender, EventArgs e)
    {
        pnlList.Visible = true;
        pnlNew.Visible = false;
        pnlView.Visible = false;
        pnlBackToList.Visible = false;
        ucSendMessage.SendMessageMode = MessageActionEnum.None;
        outboxGrid.ReloadData();
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteMessage();
        outboxGrid.ReloadData();
    }


    protected void btnForward_Click(object sender, EventArgs e)
    {
        ForwardMesssage();
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
            // Mark read message
            if (MarkReadMessage)
            {
                DateTime messageRead = ValidationHelper.GetDateTime(DataHelper.GetDataRowValue(rowView.Row, "MessageRead"), DateTimeHelper.ZERO_TIME);
                if (messageRead == DateTimeHelper.ZERO_TIME)
                {
                    e.Row.CssClass += " Unread";
                }
            }
        }
    }


    protected void outboxGrid_OnBeforeDataReload()
    {
        // Bind footer
        BindFooter();
    }


    protected void outboxGrid_OnPageSizeChanged()
    {
        outboxGrid.ReloadData();
    }


    protected void outboxGrid_OnBeforeSorting(object sender, EventArgs e)
    {
        outboxGrid.ReloadData();
    }


    protected object outboxGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "messagerecipientnickname":
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

            case "messageread":
                // Get date read
                return GetDateRead(parameter);

        }
        return parameter;
    }


    protected void GridView_DataBound(object sender, EventArgs e)
    {
        // Setup column styles
        if (!MarkReadMessage)
        {
            outboxGrid.GridView.Columns[4].Visible = false;
        }
    }


    protected void outboxGrid_OnAction(string actionName, object actionArgument)
    {
        CurrentMessageId = ValidationHelper.GetInteger(actionArgument, 0);
        switch (actionName)
        {
            // Delete message
            case "delete":
                DeleteMessage();
                break;

            // View message
            case "view":
                ViewMessage();
                break;

            // Forward message
            case "forward":
                ForwardMesssage();
                break;
        }
    }


    #endregion


    #region "Protected methods"

    /// <summary>
    /// Bind the data.
    /// </summary>
    private void BindFooter()
    {
        lblFooter.Text = string.Format(GetString("Messaging.NumOfMessages"), MessageInfoProvider.GetSentMessagesCount(CMSContext.CurrentUser.UserID));
    }


    protected string GetDateRead(object messageReadDate)
    {
        DateTime messageRead = ValidationHelper.GetDateTime(messageReadDate, DateTimeHelper.ZERO_TIME);

        if (currentUserInfo == null)
        {
            currentUserInfo = CMSContext.CurrentUser;
        }
        if (currentSiteInfo == null)
        {
            currentSiteInfo = CMSContext.CurrentSite;
        }
        DateTime currentDateTime = ValidationHelper.GetDateTime(messageReadDate, DateTimeHelper.ZERO_TIME);

        if (messageRead != DateTimeHelper.ZERO_TIME)
        {
            if (IsLiveSite)
            {
                return CMSContext.ConvertDateTime(currentDateTime, this).ToString();
            }
            else
            {
                return TimeZoneHelper.GetCurrentTimeZoneDateTimeString(currentDateTime, currentUserInfo, currentSiteInfo, out usedTimeZone);
            }
        }
        else
        {
            return GetString("Messaging.OutboxMessageUnread");
        }
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
