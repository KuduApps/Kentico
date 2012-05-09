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

using CMS.MessageBoard;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.WebAnalytics;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;
using System.Collections.Generic;

public partial class CMSModules_MessageBoards_Controls_NewSubscription : CMSUserControl
{
    #region "Private variables"

    private BoardProperties mBoardProperties = null;
    int mBoardID = 0;

    #endregion


    #region "Public properties"
    
    /// <summary>
    /// ForumId.
    /// </summary>
    public int BoardID
    {
        get 
        { 
            return mBoardID; 
        }
        set 
        { 
            mBoardID = value; 
        }
    }


    /// <summary>
    /// Properties passed from the upper control.
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

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string valGroup = this.UniqueID;

        lblEmail.ResourceString = "board.subscription.email";
        btnOk.ResourceString = "board.subscription.subscribe";
        btnOk.ValidationGroup = valGroup;

        rfvEmailRequired.ErrorMessage = GetString("board.subscription.noemail");
        rfvEmailRequired.ValidationGroup = valGroup;
                
        this.revEmailValid.ValidationGroup = valGroup;
        this.revEmailValid.ErrorMessage = GetString("board.messageedit.revemail");
        this.revEmailValid.ValidationExpression = @"^([\w0-9_\-\+]+(\.[\w0-9_\-\+]+)*@[\w0-9_-]+(\.[\w0-9_-]+)+)*$";
    }


    /// <summary>
    /// Pre-fill user e-mail.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!RequestHelper.IsPostBack())
        {
            if (txtEmail.Text.Trim() == "" && (CMSContext.CurrentUser.Email != null) && (CMSContext.CurrentUser.Email != ""))
            {
                txtEmail.Text = CMSContext.CurrentUser.Email;
            }
        }
    }


    /// <summary>
    /// OK click handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        // Check input fields
        string email = txtEmail.Text.Trim();
        string result = new Validator().NotEmpty(email, rfvEmailRequired.ErrorMessage)
            .IsEmail(email, GetString("general.correctemailformat")).Result;

        // Try to subscribe new subscriber
        if (result == "")
        {
            // Try to create a new board
            BoardInfo boardInfo = null;
            if (this.BoardID == 0) 
            {
                // Create new message board according to webpart properties
                boardInfo = new BoardInfo(this.BoardProperties);
                BoardInfoProvider.SetBoardInfo(boardInfo);

                // Update information on current message board
                this.BoardID = boardInfo.BoardID;

                // Set board-role relationship                
                BoardRoleInfoProvider.SetBoardRoles(this.BoardID, this.BoardProperties.BoardRoles);

                // Set moderators
                BoardModeratorInfoProvider.SetBoardModerators(this.BoardID, this.BoardProperties.BoardModerators);
            }
            
            if (this.BoardID > 0)
            {
                // Check for duplicit e-mails
                DataSet ds = BoardSubscriptionInfoProvider.GetSubscriptions("SubscriptionBoardID=" + this.BoardID +
                    " AND SubscriptionEmail='" + SqlHelperClass.GetSafeQueryString(email, false) + "'", null);
                if (DataHelper.DataSourceIsEmpty(ds))
                {
                    BoardSubscriptionInfo bsi = new BoardSubscriptionInfo();
                    bsi.SubscriptionBoardID = this.BoardID;
                    bsi.SubscriptionEmail = email;
                    if ((CMSContext.CurrentUser != null) && !CMSContext.CurrentUser.IsPublic())
                    {
                        bsi.SubscriptionUserID = CMSContext.CurrentUser.UserID;
                    }
                    BoardSubscriptionInfoProvider.SetBoardSubscriptionInfo(bsi);
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("board.subscription.beensubscribed");

                    // Clear form
                    txtEmail.Text = "";
                    if (boardInfo == null)
                    {
                        boardInfo = BoardInfoProvider.GetBoardInfo(this.BoardID);
                    }
                    LogActivity(bsi, boardInfo);
                }
                else
                {
                    result = GetString("board.subscription.emailexists");
                }
            }
        }

        if (result != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Log activity (subscribing)
    /// </summary>
    /// <param name="bsi"></param>
    private void LogActivity(BoardSubscriptionInfo bsi, BoardInfo bi)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || (bsi == null) || (bi == null) || !bi.BoardLogActivity || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
            || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) || !ActivitySettingsHelper.MessageBoardSubscriptionEnabled(siteName) )
        {
            return;
        }

        TreeNode currentDoc = CMSContext.CurrentDocument;
        int contactId = ModuleCommands.OnlineMarketingGetCurrentContactID();
        Dictionary<string, object> contactData = new Dictionary<string, object>();
        contactData.Add("ContactEmail", bsi.SubscriptionEmail);
        ModuleCommands.OnlineMarketingUpdateContactFromExternalSource(contactData, false, contactId);
        var data = new ActivityData()
        {
            ContactID = contactId,
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.SUBSCRIPTION_MESSAGE_BOARD,
            TitleData = bi.BoardName,
            URL = URLHelper.CurrentRelativePath,
            NodeID = (currentDoc != null ? currentDoc.NodeID : 0),
            Culture = (currentDoc != null ? currentDoc.DocumentCulture : null),
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }

    #endregion
}
