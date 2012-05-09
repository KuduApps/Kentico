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

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.MessageBoard;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_MessageBoards_Controls_MessageBoard : CMSUserControl
{
    #region "Private fields"

    private BoardProperties mBoardProperties = new BoardProperties();

    // Currently processed board info
    private BoardInfo bi = null;

    private bool mReloadPageAfterAction = false;
    private bool userVerified = false;

    private string mAliasPath = null;
    private string mCulture = null;
    private string mSiteName = null;
    private TreeNode mBoardNode = null;

    private string mMessageTransformation = null;
    private int mMessageBoardID = 0;
    //private int mCurrentGroupID = 0;
    private string mNoMessagesText = null;

    #endregion

    #region "Public properties"

    /// <summary>
    /// Returns current board properties.
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
    /// Transformation to be used for displaying the board message text.
    /// </summary>
    public string MessageTransformation
    {
        get
        {
            return this.mMessageTransformation;
        }
        set
        {
            this.mMessageTransformation = value;
        }
    }


    /// <summary>
    /// ID of the currently processed message board.
    /// </summary>
    public int MessageBoardID
    {
        get
        {
            return this.mMessageBoardID;
        }
        set
        {
            this.mMessageBoardID = value;
        }
    }


    /// <summary>
    /// No messages text.
    /// </summary>
    public string NoMessagesText
    {
        get
        {
            return this.mNoMessagesText;
        }
        set
        {
            this.mNoMessagesText = value;
        }
    }


    /// <summary>
    /// Indicates whether the page should be reloded afetr action took place.
    /// </summary>
    public bool ReloadPageAfterAction
    {
        get
        {
            return this.mReloadPageAfterAction;
        }
        set
        {
            this.mReloadPageAfterAction = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache item name.
    /// </summary>
    public string CacheItemName
    {
        get
        {
            return this.rptBoardMessages.CacheItemName;
        }
        set
        {
            this.rptBoardMessages.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public string CacheDependencies
    {
        get
        {
            return this.rptBoardMessages.CacheDependencies;
        }
        set
        {
            this.rptBoardMessages.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public int CacheMinutes
    {
        get
        {
            return this.rptBoardMessages.CacheMinutes;
        }
        set
        {
            this.rptBoardMessages.CacheMinutes = value;
        }
    }

    #endregion

    #region "Private properties"

    /// <summary>
    /// Post alias path.
    /// </summary>
    private string AliasPath
    {
        get
        {
            if (mAliasPath == null)
            {
                mAliasPath = CMSContext.CurrentPageInfo.NodeAliasPath;
            }
            return mAliasPath;
        }
    }


    /// <summary>
    /// Post culture.
    /// </summary>
    private string Culture
    {
        get
        {
            if (mCulture == null)
            {
                mCulture = CMSContext.PreferredCultureCode;
            }
            return mCulture;
        }
    }


    /// <summary>
    /// Post SiteName.
    /// </summary>
    private string SiteName
    {
        get
        {
            if (mSiteName == null)
            {
                mSiteName = CMSContext.CurrentSiteName;
            }
            return mSiteName;
        }
    }


    /// <summary>
    /// Board document node.
    /// </summary>
    private TreeNode BoardNode
    {
        get
        {
            if (mBoardNode == null)
            {
                this.SetContext();

                // Get the document
                TreeProvider tree = new TreeProvider();
                mBoardNode = tree.SelectSingleNode(this.SiteName, this.AliasPath, this.Culture, false);
                if ((mBoardNode != null) && (CMSContext.ViewMode != ViewModeEnum.LiveSite))
                {
                    mBoardNode = DocumentHelper.GetDocument(mBoardNode, tree);
                }

                this.ReleaseContext();
            }
            return mBoardNode;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for blog
        if (this.BoardProperties.CheckPermissions)
        {
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(this.BoardNode, NodePermissionsEnum.Read) != AuthorizationResultEnum.Allowed)
            {
                this.Visible = false;
            }
        }
        // Initializes the control elements
        SetupControl();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        string postBackRefference = ControlsHelper.GetPostBackEventReference(this.btnRefresh, null);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshBoardList", ScriptHelper.GetScript("function RefreshBoardList(filterParams){" +
            postBackRefference + "}"));
    }

    #region "Event handlers"

    void rptBoardMessages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            if (e.Item.Controls.Count > 0)
            {
                // Load 'MessageActions.ascx' control
                BoardMessageActions boardMsgActions = (BoardMessageActions)e.Item.Controls[0].FindControl("messageActions");

                Control pnlRating = e.Item.Controls[0].FindControl("pnlRating");

                if ((boardMsgActions != null) || (pnlRating != null))
                {
                    DataRow drvRow = ((DataRowView)e.Item.DataItem).Row;

                    // Create new comment info object
                    BoardMessageInfo bmi = new BoardMessageInfo(drvRow);

                    if (boardMsgActions != null)
                    {
                        // Initialize control                    
                        boardMsgActions.MessageID = bmi.MessageID;
                        boardMsgActions.MessageBoardID = this.MessageBoardID;

                        // Register for OnAction event
                        boardMsgActions.OnMessageAction += new BoardMessageActions.OnBoardMessageAction(boardMsgActions_OnMessageAction);

                        // Handle buttons displaying
                        boardMsgActions.ShowApprove = ((this.BoardProperties.ShowApproveButton) && (!bmi.MessageApproved) && userVerified);
                        boardMsgActions.ShowReject = ((this.BoardProperties.ShowRejectButton) && (bmi.MessageApproved) && userVerified);
                        boardMsgActions.ShowDelete = ((this.BoardProperties.ShowDeleteButton) && userVerified);
                        boardMsgActions.ShowEdit = ((this.BoardProperties.ShowEditButton) && userVerified);
                    }

                    // Init content rating control if enabled and rating is greater than zero
                    if (pnlRating != null)
                    {
                        if ((bmi.MessageRatingValue > 0) && (this.BoardProperties.EnableContentRating))
                        {
                            pnlRating.Visible = true;
                            AbstractRatingControl usrControl = null;
                            if (CMSContext.CurrentDocument != null)
                            {
                                try
                                {
                                    // Insert rating control to page
                                    usrControl = (AbstractRatingControl)(this.Page.LoadControl(AbstractRatingControl.GetRatingControlUrl(this.BoardProperties.RatingType + ".ascx")));
                                }
                                catch (Exception ex)
                                {
                                    this.Controls.Add(new LiteralControl(ex.Message));
                                    return;
                                }

                                // Init values
                                usrControl.ID = "messageRating";
                                usrControl.MaxRating = this.BoardProperties.MaxRatingValue;
                                usrControl.CurrentRating = bmi.MessageRatingValue;
                                usrControl.Visible = true;
                                usrControl.Enabled = false;

                                pnlRating.Controls.Clear();
                                pnlRating.Controls.Add(usrControl);
                            }
                        }
                        else
                        {
                            pnlRating.Visible = false;
                        }
                    }
                }
            }
        }
    }


    void boardMsgActions_OnMessageAction(string actionName, object argument)
    {
        // Get current board message ID
        int boardMessageId = ValidationHelper.GetInteger(argument, 0);
        BoardMessageInfo message = BoardMessageInfoProvider.GetBoardMessageInfo(boardMessageId);

        // Handle not existing message
        if (message == null)
        {
            return;
        }

        if ((bi != null) && BoardInfoProvider.IsUserAuthorizedToManageMessages(bi))
        {
            switch (actionName.ToLower())
            {
                case "delete":
                    // Delete message
                    BoardMessageInfoProvider.DeleteBoardMessageInfo(message);

                    this.rptBoardMessages.ClearCache();
                    ReloadData();
                    break;

                case "approve":
                    // Approve board message
                    if (CMSContext.CurrentUser != null)
                    {
                        message.MessageApprovedByUserID = CMSContext.CurrentUser.UserID;
                        message.MessageApproved = true;
                        BoardMessageInfoProvider.SetBoardMessageInfo(message);
                    }

                    this.rptBoardMessages.ClearCache();
                    ReloadData();
                    break;

                case "reject":
                    // Reject board message
                    if (CMSContext.CurrentUser != null)
                    {
                        message.MessageApprovedByUserID = 0;
                        message.MessageApproved = false;
                        BoardMessageInfoProvider.SetBoardMessageInfo(message);
                    }

                    this.rptBoardMessages.ClearCache();
                    ReloadData();
                    break;
            }
        }
    }


    void msgEdit_OnAfterMessageSaved(BoardMessageInfo message)
    {
        if ((bi == null) && (message != null) && (message.MessageBoardID > 0))
        {
            this.MessageBoardID = message.MessageBoardID;

            // Get updated board informnation
            bi = BoardInfoProvider.GetBoardInfo(message.MessageBoardID);

            userVerified = BoardInfoProvider.IsUserAuthorizedToManageMessages(bi);
        }

        this.rptBoardMessages.ClearCache();
        
        ReloadData();
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        this.rptBoardMessages.ClearCache();
        ReloadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the control elements.
    /// </summary>
    private void SetupControl()
    {
        // If the control shouldn't proceed further
        if (this.BoardProperties.StopProcessing)
        {
            this.Visible = false;
            return;
        }
        else
        {
            btnLeaveMessage.Attributes.Add("onclick", "ShowSubscription(0, '" + hdnSelSubsTab.ClientID + "','" + pnlMsgEdit.ClientID + "','" +
                pnlMsgSubscription.ClientID + "'); return false; ");
            btnSubscribe.Attributes.Add("onclick", " ShowSubscription(1, '" + hdnSelSubsTab.ClientID + "','" + pnlMsgEdit.ClientID + "','" +
                pnlMsgSubscription.ClientID + "'); return false; ");

            // Show/hide appropriate control based on current selection form hidden field
            if (ValidationHelper.GetInteger(hdnSelSubsTab.Value, 0) == 0)
            {
                pnlMsgEdit.Style.Remove("display");
                pnlMsgEdit.Style.Add("display", "block");
                pnlMsgSubscription.Style.Remove("display");
                pnlMsgSubscription.Style.Add("display", "none");
            }
            else
            {
                pnlMsgSubscription.Style.Remove("display");
                pnlMsgSubscription.Style.Add("display", "block");
                pnlMsgEdit.Style.Remove("display");
                pnlMsgEdit.Style.Add("display", "none");
            }

            // Set the repeater
            this.rptBoardMessages.QueryName = "board.message.selectall";
            this.rptBoardMessages.ZeroRowsText = HTMLHelper.HTMLEncode(this.NoMessagesText) + "<br /><br />";
            this.rptBoardMessages.ItemDataBound += new RepeaterItemEventHandler(rptBoardMessages_ItemDataBound);
            this.rptBoardMessages.TransformationName = this.MessageTransformation;

            // Set the labels
            this.lblLeaveMessage.ResourceString = "board.messageboard.leavemessage";
            this.lblNewSubscription.ResourceString = "board.newsubscription";
            this.btnSubscribe.Text = GetString("board.messageboard.subscribe");
            this.btnLeaveMessage.Text = GetString("board.messageboard.leavemessage");

            // Pass the properties down to the message edit control
            this.msgEdit.BoardProperties = this.BoardProperties;
            this.msgEdit.OnAfterMessageSaved += new OnAfterMessageSavedEventHandler(msgEdit_OnAfterMessageSaved);
            this.msgSubscription.BoardProperties = this.BoardProperties;
            this.plcBtnSubscribe.Visible = this.BoardProperties.BoardEnableSubscriptions;

            // If the message board exist and is enabled
            bi = BoardInfoProvider.GetBoardInfo(this.MessageBoardID);
            if (bi != null)
            {
                // Get basic info on users permissions            
                userVerified = BoardInfoProvider.IsUserAuthorizedToManageMessages(bi);

                if (bi.BoardEnabled)
                {
                    // If the board is moderated remember it
                    if (bi.BoardModerated)
                    {
                        this.BoardProperties.BoardModerated = true;
                    }

                    // Reload messages
                    ReloadBoardMessages();

                    // If the message board is opened users can add the messages
                    bool displayAddMessageForm = BoardInfoProvider.IsUserAuthorizedToAddMessages(bi);

                    // Hide 'add message' form when anonymous read disabled and user is not authenticated
                    displayAddMessageForm &= (BoardProperties.BoardEnableAnonymousRead || CMSContext.CurrentUser.IsAuthenticated());

                    if (displayAddMessageForm)
                    {
                        // Display the 'add the message' control
                        DisplayAddMessageForm();
                    }
                    else
                    {
                        this.msgEdit.MessageBoardID = this.MessageBoardID;
                        this.msgEdit.Visible = false;
                        this.pnlMsgEdit.Visible = false;
                    }

                    msgSubscription.BoardID = bi.BoardID;
                }
                else
                {
                    // Hide the control
                    this.Visible = false;
                }
            }
            else
            {
                // Decide whether the 'Leave message' dialog should be displayed
                if (BoardInfoProvider.IsUserAuthorizedToAddMessages(this.BoardProperties))
                {
                    DisplayAddMessageForm();
                }
                else
                {
                    // Hide the dialog, but set message board ID in case that board closed just while entering
                    this.pnlMsgEdit.Visible = false;
                    this.msgEdit.StopProcessing = false;
                }
            }
        }
    }


    /// <summary>
    /// Displays form dialog used to leave a new message.
    /// </summary>
    private void DisplayAddMessageForm()
    {
        this.msgEdit.MessageBoardID = this.MessageBoardID;
        this.msgEdit.MessageID = 0;
    }


    /// <summary>
    /// Reloads the board messages related to the currently processed message board.
    /// </summary>
    private void ReloadBoardMessages()
    {
        this.SetContext();

        // If user isn't allowed to read comments
        if (!CMSContext.CurrentUser.IsAuthenticated() && !this.BoardProperties.BoardEnableAnonymousRead)
        {
            // Do not display existing messages to anonymous user, but inform on situation
            this.lblNoMessages.Visible = true;
            this.lblNoMessages.Text = GetString("board.messagelist.anonymousreadnotallowed");
        }
        else
        {
            // If the message board ID was specified
            if (this.MessageBoardID > 0)
            {
                string where = "(MessageBoardID = " + this.MessageBoardID.ToString() + ")";

                // If the user should be displayed with all messages not just approved ones
                if (!BoardInfoProvider.IsUserAuthorizedToManageMessages(bi))
                {
                    where += " AND (MessageApproved = 1) AND ((MessageIsSpam IS NULL) OR (MessageIsSpam = 0))";
                }

                // Get board messages
                this.rptBoardMessages.WhereCondition = where;
                this.rptBoardMessages.ReloadData(true);
            }
        }

        this.ReleaseContext();
    }


    /// <summary>
    /// Reloads the data on the page.
    /// </summary>
    protected void ReloadData()
    {
        // Reload whole page
        if (this.ReloadPageAfterAction)
        {
            URLHelper.Redirect(URLRewriter.CurrentURL);
        }
        // Reload comment list only
        else
        {
            ReloadBoardMessages();
        }
    }

    #endregion
}
