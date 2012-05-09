using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.MessageBoard;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.WebAnalytics;
using CMS.TreeEngine;

public partial class CMSModules_MessageBoards_Controls_Messages_MessageEdit : CMSAdminEditControl
{
    #region "Events"

    public event OnAfterMessageSavedEventHandler OnAfterMessageSaved;
    public event OnBeforeMessageSavedEventHandler OnBeforeMessageSaved;

    #endregion


    #region "Variables"

    private bool mAdvancedMode = false;
    private bool mCheckFloodProtection = false;
    private int mMessageID = 0;
    private int mMessageBoardID = 0;
    private bool mModalMode = false;

    private AbstractRatingControl ratingControl = null;
    private BoardProperties mBoardProperties = new BoardProperties();
    private BoardMessageInfo messageInfo = null;
    private BoardInfo mBoard = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Advance mode.
    /// </summary>
    public bool AdvancedMode
    {
        get
        {
            return this.mAdvancedMode;
        }
        set
        {
            this.mAdvancedMode = ValidationHelper.GetBoolean(value, false);
        }
    }


    /// <summary>
    /// Advance mode.
    /// </summary>
    public bool CheckFloodProtection
    {
        get
        {
            return this.mCheckFloodProtection;
        }
        set
        {
            this.mCheckFloodProtection = value;
        }
    }


    /// <summary>
    /// Message Id.
    /// </summary>
    public int MessageID
    {
        get
        {
            return this.mMessageID;
        }
        set
        {
            this.mMessageID = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Message board Id.
    /// </summary>
    public int MessageBoardID
    {
        get
        {
            if (this.mMessageBoardID == 0)
            {
                this.mMessageBoardID = (messageInfo != null) ? messageInfo.MessageBoardID : 0;
            }
            return this.mMessageBoardID;
        }
        set
        {
            this.mMessageBoardID = ValidationHelper.GetInteger(value, 0);

            mBoard = null;
        }
    }


    /// <summary>
    /// Message board object.
    /// </summary>
    public BoardInfo Board
    {
        get
        {
            return mBoard ?? (mBoard = BoardInfoProvider.GetBoardInfo(MessageBoardID));
        }
    }


    /// <summary>
    /// Message board properties.
    /// </summary>
    public BoardProperties BoardProperties
    {
        get
        {
            return this.mBoardProperties;
        }
        set
        {
            this.mBoardProperties = value;
        }
    }


    /// <summary>
    /// Indicates if message edit is in modal dialog.
    /// </summary>
    public bool ModalMode
    {
        get
        {
            return this.mModalMode;
        }
        set
        {
            this.mModalMode = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Let parent check 'Modify' permission if required
        if (!RaiseOnCheckPermissions(PERMISSION_MODIFY, this))
        {
            // Parent page doesn't check permissions
        }

        this.SetContext();

        // Initialize the controls
        SetupControls();

        // Reload data if necessary
        if (!URLHelper.IsPostback())
        {
            ReloadData();
        }

        this.ReleaseContext();
    }


    #region "Events handling"

    protected void ratingControl_RatingEvent(AbstractRatingControl sender)
    {
        ViewState["ratingvalue"] = sender.CurrentRating;
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Let the parent control now new message is being saved
        if (OnBeforeMessageSaved != null)
        {
            OnBeforeMessageSaved();
        }

        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        // Validate form
        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            // Check flooding when message being inserted through the LiveSite
            if (this.CheckFloodProtection && this.IsLiveSite && FloodProtectionHelper.CheckFlooding(CMSContext.CurrentSiteName, CMSContext.CurrentUser))
            {
                lblError.Visible = true;
                lblError.Text = GetString("General.FloodProtection");
                return;
            }

            CurrentUserInfo currentUser = CMSContext.CurrentUser;

            BoardMessageInfo messageInfo = null;

            if (MessageID > 0)
            {
                // Get message info
                messageInfo = BoardMessageInfoProvider.GetBoardMessageInfo(MessageID);
                MessageBoardID = messageInfo.MessageBoardID;
            }
            else
            {
                // Create new info
                messageInfo = new BoardMessageInfo();

                // User IP adress
                messageInfo.MessageUserInfo.IPAddress = Request.UserHostAddress;
                // User agent
                messageInfo.MessageUserInfo.Agent = Request.UserAgent;
            }

            // Setup message info
            messageInfo.MessageEmail = txtEmail.Text.Trim();
            messageInfo.MessageText = txtMessage.Text.Trim();

            // Handle message URL
            string url = txtURL.Text.Trim();
            if ((url != "http://") && (url != "https://") && (url != ""))
            {
                if ((!url.ToLower().StartsWith("http://")) && (!url.ToLower().StartsWith("https://")))
                {
                    url = "http://" + url;
                }
            }
            else
            {
                url = "";
            }
            messageInfo.MessageURL = url;
            messageInfo.MessageURL = messageInfo.MessageURL.ToLower().Replace("javascript", "_javascript");

            messageInfo.MessageUserName = this.txtUserName.Text.Trim();
            if (!currentUser.IsPublic())
            {
                messageInfo.MessageUserID = currentUser.UserID;
            }

            messageInfo.MessageIsSpam = ValidationHelper.GetBoolean(this.chkSpam.Checked, false);

            if (this.BoardProperties.EnableContentRating && (ratingControl != null) &&
                (ratingControl.GetCurrentRating() > 0))
            {
                messageInfo.MessageRatingValue = ratingControl.CurrentRating;
            }

            BoardInfo boardInfo = null;

            // If there is message board
            if (MessageBoardID > 0)
            {
                // Load message board
                boardInfo = Board;
            }
            else
            {
                // Create new message board according to webpart properties
                boardInfo = new BoardInfo(this.BoardProperties);
                BoardInfoProvider.SetBoardInfo(boardInfo);

                // Update information on current message board
                this.MessageBoardID = boardInfo.BoardID;

                // Set board-role relationship                
                BoardRoleInfoProvider.SetBoardRoles(this.MessageBoardID, this.BoardProperties.BoardRoles);

                // Set moderators
                BoardModeratorInfoProvider.SetBoardModerators(this.MessageBoardID, this.BoardProperties.BoardModerators);
            }

            if (boardInfo != null)
            {
                // If the very new message is inserted
                if (this.MessageID == 0)
                {
                    // If creating message set inserted to now and assign to board
                    messageInfo.MessageInserted = currentUser.DateTimeNow;
                    messageInfo.MessageBoardID = MessageBoardID;

                    // Handle auto approve action
                    bool isAuthorized = BoardInfoProvider.IsUserAuthorizedToManageMessages(boardInfo);
                    if (isAuthorized)
                    {
                        messageInfo.MessageApprovedByUserID = currentUser.UserID;
                        messageInfo.MessageApproved = true;
                    }
                    else
                    {
                        // Is board moderated ?
                        messageInfo.MessageApprovedByUserID = 0;
                        messageInfo.MessageApproved = !boardInfo.BoardModerated;
                    }
                }
                else
                {
                    if (this.chkApproved.Checked)
                    {
                        // Set current user as approver
                        messageInfo.MessageApproved = true;
                        messageInfo.MessageApprovedByUserID = currentUser.UserID;
                    }
                    else
                    {
                        messageInfo.MessageApproved = false;
                        messageInfo.MessageApprovedByUserID = 0;
                    }
                }

                if (!AdvancedMode)
                {
                    if (!BadWordInfoProvider.CanUseBadWords(CMSContext.CurrentUser, CMSContext.CurrentSiteName))
                    {
                        // Columns to check
                        Dictionary<string, int> collumns = new Dictionary<string, int>();
                        collumns.Add("MessageText", 0);
                        collumns.Add("MessageUserName", 250);

                        // Perform bad words check 
                        errorMessage = BadWordsHelper.CheckBadWords(messageInfo, collumns, "MessageApproved", "MessageApprovedByUserID",
                                                                        messageInfo.MessageText, currentUser.UserID);

                        // Additionaly check empty fields
                        if (errorMessage == string.Empty)
                        {
                            if (!ValidateMessage(messageInfo))
                            {
                                errorMessage = GetString("board.messageedit.emptybadword");
                            }
                        }
                    }
                }

                // Subscribe this user to message board
                if (chkSubscribe.Checked)
                {
                    string email = messageInfo.MessageEmail;

                    // Check for duplicit e-mails
                    DataSet ds = BoardSubscriptionInfoProvider.GetSubscriptions("SubscriptionBoardID=" + this.MessageBoardID +
                        " AND SubscriptionEmail='" + SqlHelperClass.GetSafeQueryString(email, false) + "'", null);
                    if (DataHelper.DataSourceIsEmpty(ds))
                    {
                        BoardSubscriptionInfo bsi = new BoardSubscriptionInfo();
                        bsi.SubscriptionBoardID = this.MessageBoardID;
                        bsi.SubscriptionEmail = email;
                        if (!currentUser.IsPublic())
                        {
                            bsi.SubscriptionUserID = currentUser.UserID;
                        }
                        BoardSubscriptionInfoProvider.SetBoardSubscriptionInfo(bsi);
                        ClearForm();
                        LogSubscribingActivity(bsi, boardInfo);
                    }
                    else
                    {
                        errorMessage = GetString("board.subscription.emailexists");
                    }
                }

                if (errorMessage == "")
                {
                    try
                    {
                        // Save message info
                        BoardMessageInfoProvider.SetBoardMessageInfo(messageInfo);

                        LogCommentActivity(messageInfo, boardInfo);

                        // If the board is moderated let the user know message is waiting for approval
                        if (boardInfo.BoardModerated && (messageInfo.MessageApproved == false))
                        {
                            this.lblInfo.Text = GetString("board.messageedit.waitingapproval");
                            this.lblInfo.Visible = true;
                        }

                        // Rise after message saved event
                        if (OnAfterMessageSaved != null)
                        {
                            OnAfterMessageSaved(messageInfo);
                        }

                        // Clear form content
                        ClearForm();
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                    }
                }
            }
        }


        if (errorMessage != "")
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
        }
        else
        {
            // Regenerate new captcha
            captchaElem.GenerateNew();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControls()
    {
        lblRating.Text = GetString("board.messageedit.rating");
        lblEmail.Text = GetString("board.messageedit.email");
        lblMessage.Text = GetString("board.messageedit.message");
        lblURL.Text = GetString("board.messageedit.url");
        lblUserName.Text = GetString("board.messageedit.username");

        chkSubscribe.Text = GetString("board.messageedit.subscribe");

        rfvMessage.ErrorMessage = GetString("board.messageedit.rfvmessage");
        rfvUserName.ErrorMessage = GetString("board.messageedit.rfvusername");
        revEmailValid.ErrorMessage = GetString("board.messageedit.revemail");
        rfvEmail.ErrorMessage = GetString("board.messageedit.rfvemail");

        // Ensure unique validation group name in case of multiple controls in one page
        string valGroup = UniqueID;

        txtUserName.ValidationGroup = valGroup;
        rfvUserName.ValidationGroup = valGroup;

        txtEmail.ValidationGroup = valGroup;
        rfvEmail.ValidationGroup = valGroup;
        revEmailValid.ValidationGroup = valGroup;
        revEmailValid.ValidationExpression = @"^([\w0-9_\-\+]+(\.[\w0-9_\-\+]+)*@[\w0-9_-]+(\.[\w0-9_-]+)+)*$";

        txtMessage.ValidationGroup = valGroup;
        rfvMessage.ValidationGroup = valGroup;

        btnOk.ValidationGroup = valGroup;
        btnOkFooter.ValidationGroup = valGroup;

        if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
        {
            lblMessage.CssClass = "";
        }

        // Load message board
        if (BoardProperties != null)
        {
            if (!BoardProperties.BoardRequireEmails)
            {
                rfvEmail.Enabled = false;
            }

            if ((BoardProperties.BoardUseCaptcha) && (!AdvancedMode))
            {
                // Show captcha text and control
                pnlCaptcha.Visible = true;
            }
        }

        plcRating.Visible = false;
        if (!AdvancedMode && this.BoardProperties.EnableContentRating)
        {
            if (CMSContext.CurrentDocument != null)
            {
                plcRating.Visible = true;
                try
                {
                    // Insert rating control to page
                    ratingControl = (AbstractRatingControl)(Page.LoadControl(AbstractRatingControl.GetRatingControlUrl(BoardProperties.RatingType + ".ascx")));
                }
                catch (Exception ex)
                {
                    Controls.Add(new LiteralControl(ex.Message));
                    return;
                }

                // Init values
                ratingControl.ID = this.ID + "_RatingControl";
                ratingControl.MaxRating = BoardProperties.MaxRatingValue;
                ratingControl.Visible = true;
                ratingControl.Enabled = true;
                ratingControl.RatingEvent += ratingControl_RatingEvent;
                ratingControl.CurrentRating = ValidationHelper.GetDouble(ViewState["ratingvalue"], 0);
                ratingControl.ExternalManagement = true;
                pnlRating.Controls.Clear();
                pnlRating.Controls.Add(ratingControl);
            }
        }

        if (AdvancedMode)
        {
            // Initialize advanced controls
            plcAdvanced.Visible = true;
            lblApproved.Text = GetString("board.messageedit.approved");
            lblSpam.Text = GetString("board.messageedit.spam");
            lblInsertedCaption.Text = GetString("board.messageedit.inserted");
            btnOk.ResourceString = "general.ok";
            btnOkFooter.ResourceString = "general.ok";

            // Show or hide "Inserted" label
            bool showInserted = (MessageID > 0);
            lblInsertedCaption.Visible = showInserted;
            lblInserted.Visible = showInserted;
            chkSubscribe.Visible = false;
        }
        else
        {
            // If is not moderated then autocheck approve
            if (!this.BoardProperties.BoardModerated)
            {
                chkApproved.Checked = true;
            }
        }

        if (ModalMode)
        {
            plcFooter.Visible = true;
            pnlOkButton.Visible = false;
        }
        else
        {
            plcFooter.Visible = false;
            pnlOkButton.Visible = true;
        }

        // Show/hide subscription option
        plcChkSubscribe.Visible = this.BoardProperties.BoardEnableSubscriptions;

        // For new message hide Is approved chkbox (auto approve)
        if (this.MessageID <= 0)
        {
            this.plcApproved.Visible = false;
        }
    }


    private static bool ValidateMessage(BoardMessageInfo messageInfo)
    {
        if ((messageInfo.MessageText == null) || (messageInfo.MessageUserName == null))
        {
            return false;
        }

        return ((messageInfo.MessageText.Trim() != "") && (messageInfo.MessageUserName.Trim() != ""));
    }


    /// <summary>
    /// Validate message form and return error message if is some.
    /// </summary>
    private string ValidateForm()
    {
        txtUserName.Text = txtUserName.Text.Trim();
        txtEmail.Text = txtEmail.Text.Trim();
        txtMessage.Text = txtMessage.Text.Trim();

        // Check for required fields
        string errorMessage = new Validator()
            .NotEmpty(txtUserName.Text, rfvUserName.ErrorMessage)
            .NotEmpty(txtMessage.Text, rfvMessage.ErrorMessage).Result;

        if (errorMessage == "")
        {
            if (BoardProperties.BoardRequireEmails)
            {
                // Check e-mail address if board require
                errorMessage = new Validator()
                .NotEmpty(txtEmail.Text, rfvEmail.ErrorMessage)
                .IsEmail(txtEmail.Text, revEmailValid.ErrorMessage).Result;
            }
            else
            {
                if (txtEmail.Text != "")
                {
                    // Check e-mail address if is some
                    errorMessage = new Validator()
                    .IsEmail(txtEmail.Text, revEmailValid.ErrorMessage).Result;
                }
            }
        }

        if ((chkSubscribe.Checked) && (errorMessage == String.Empty))
        {
            errorMessage = new Validator()
            .NotEmpty(txtEmail.Text, GetString("board.messageedit.rfvemail"))
            .IsEmail(txtEmail.Text, GetString("board.messageedit.revemail")).Result;
        }

        if ((BoardProperties.BoardUseCaptcha) && (errorMessage == ""))
        {
            // Check whether security code is correct
            if (!captchaElem.IsValid())
            {
                errorMessage = captchaElem.ValidationError;
            }
        }

        return errorMessage;
    }


    /// <summary>
    /// Rloads the form data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        if (this.mMessageID > 0)
        {
            messageInfo = BoardMessageInfoProvider.GetBoardMessageInfo(this.mMessageID);
            if (messageInfo != null)
            {
                // Check whether edited message belongs to a board from current site
                if ((Board != null) && (Board.BoardSiteID != CMSContext.CurrentSiteID))
                {
                    EditedObject = null;
                }

                // Set textfields and checkboxes
                txtEmail.Text = messageInfo.MessageEmail;
                txtMessage.Text = messageInfo.MessageText;
                txtURL.Text = messageInfo.MessageURL;
                txtUserName.Text = messageInfo.MessageUserName;
                chkApproved.Checked = messageInfo.MessageApproved;
                chkSpam.Checked = messageInfo.MessageIsSpam;
                lblInserted.Text = CMSContext.ConvertDateTime(messageInfo.MessageInserted, this).ToString();
            }
        }
        else
        {
            ClearForm();
        }
    }


    /// <summary>
    /// Clears all input boxes.
    /// </summary>
    public override void ClearForm()
    {
        txtUserName.Text = String.Empty;
        txtEmail.Text = String.Empty;
        txtMessage.Text = String.Empty;
        txtURL.Text = "http://";

        if (!CMSContext.CurrentUser.IsPublic())
        {
            txtUserName.Text = !DataHelper.IsEmpty(CMSContext.CurrentUser.UserNickName) ? CMSContext.CurrentUser.UserNickName : CMSContext.CurrentUser.FullName;
            txtEmail.Text = CMSContext.CurrentUser.Email;
        }
    }


    /// <summary>
    /// Log activity (subscribing).
    /// </summary>
    /// <param name="bsi">Board subscription info object</param>
    /// <param name="bi">Message board info</param>
    private void LogSubscribingActivity(BoardSubscriptionInfo bsi, BoardInfo bi)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || (bsi == null) || (bi == null) || !bi.BoardLogActivity || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser)
            || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.MessageBoardSubscriptionEnabled(siteName))
        {
            return;
        }

        TreeNode currentDoc = CMSContext.CurrentDocument;
        
        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.SUBSCRIPTION_MESSAGE_BOARD,
            TitleData = bi.BoardDisplayName,
            URL = URLHelper.CurrentRelativePath,
            NodeID = (currentDoc != null ? currentDoc.NodeID : 0),
            Culture = (currentDoc != null ? currentDoc.DocumentCulture : null),
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }


    /// <summary>
    /// Log activity posting
    /// </summary>
    /// <param name="bsi">Board subscription info object</param>
    /// <param name="bi">Message board info</param>
    private void LogCommentActivity(BoardMessageInfo bmi, BoardInfo bi)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || (bmi == null) || (bi == null) || !bi.BoardLogActivity || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser)
            || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.MessageBoardPostsEnabled(siteName))
        {
            return;
        }

        int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
        Dictionary<string, object> contactData = new Dictionary<string, object>();
        contactData.Add("ContactEmail", bmi.MessageEmail);
        contactData.Add("ContactWebSite", bmi.MessageURL);
        contactData.Add("ContactLastName", bmi.MessageUserName);
        ModuleCommands.OnlineMarketingUpdateContactFromExternalSource(contactData, false, contactId);

        TreeNode currentDoc = CMSContext.CurrentDocument;
        var data = new ActivityData()
        {
            ContactID = contactId,
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.MESSAGE_BOARD_COMMENT,
            TitleData = bi.BoardDisplayName,
            ItemID = bmi.MessageBoardID,
            URL = URLHelper.CurrentRelativePath,
            ItemDetailID = bmi.MessageID,
            NodeID = (currentDoc != null ? currentDoc.NodeID : 0),
            Culture = (currentDoc != null ? currentDoc.DocumentCulture : null),
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }

    #endregion
}
