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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.MessageBoard;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.SiteProvider;
using CMS.Community;
using CMS.SettingsProvider;

public partial class CMSWebParts_Community_MessageBoards_GroupMessageBoard : CMSAbstractWebPart
{
    #region "Private fields"

    private string mWebPartName = "";
    private BoardInfo mBoardObj = null;
    private GroupInfo mCurrentGroup = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Current web part name.
    /// </summary>
    private string BoardDisplayName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BoardDisplayName"), "");
        }
        set
        {
            this.SetValue("BoardDisplayName", value);
        }
    }


    /// <summary>
    /// Current web part name.
    /// </summary>
    private string WebPartName
    {
        get
        {
            if (this.mWebPartName == "")
            {
                this.mWebPartName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), "");
            }

            return this.mWebPartName;
        }
    }


    /// <summary>
    /// Current group from community context.
    /// </summary>
    private GroupInfo CurrentGroup
    {
        get
        {
            if (this.mCurrentGroup == null)
            {
                this.mCurrentGroup = CommunityContext.CurrentGroup;
            }

            return this.mCurrentGroup;
        }
    }


    /// <summary>
    /// Current message board.
    /// </summary>
    private BoardInfo BoardObj
    {
        get
        {
            if (mBoardObj == null)
            {
                // Get the current group info and obtain the related board info
                if (CurrentGroup != null)
                {
                    mBoardObj = BoardInfoProvider.GetBoardInfoForGroup(CurrentGroup.GroupID, GetBoardName(this.WebPartName));
                }
            }
            return mBoardObj;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.msgBoard.StopProcessing = value;
        }
    }


    /// <summary>
    /// No messages text.
    /// </summary>
    public string NoMessagesText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NoMessagesText"), "");
        }
        set
        {
            this.SetValue("NoMessagesText", value);
        }
    }


    /// <summary>
    /// Default board moderators.
    /// </summary>
    public string BoardModerators
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BoardModerators"), "");
        }
        set
        {
            this.SetValue("BoardModerators", value);
        }
    }


    /// <summary>
    /// Transformation used to display board message text.
    /// </summary>
    public string MessageTransformation
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MessageTransformation"), "");
        }
        set
        {
            this.SetValue("MessageTransformation", value);
        }
    }


    /// <summary>
    /// Indicates whether the EDIT button should be displayed.
    /// </summary>
    public bool ShowEdit
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowEdit"), false);
        }
        set
        {
            this.SetValue("ShowEdit", value);
        }
    }


    /// <summary>
    /// Indicates whether the DELETE button should be displayed.
    /// </summary>
    public bool ShowDelete
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowDelete"), false);
        }
        set
        {
            this.SetValue("ShowDelete", value);
        }
    }


    /// <summary>
    /// Indicates whether the APPROVE button should be displayed.
    /// </summary>
    public bool ShowApprove
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowApprove"), false);
        }
        set
        {
            this.SetValue("ShowApprove", value);
        }
    }


    /// <summary>
    /// Indicates whether the REJECT button should be displayed.
    /// </summary>
    public bool ShowReject
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowReject"), false);
        }
        set
        {
            this.SetValue("ShowReject", value);
        }
    }


    /// <summary>
    /// Indicates whether the subscriptions should be enabled.
    /// </summary>
    public bool BoardEnableSubscriptions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BoardEnableSubscriptions"), false);
        }
        set
        {
            this.SetValue("BoardEnableSubscriptions", value);
        }
    }


    /// <summary>
    /// Indicates whether the permissions should be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), false);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            msgBoard.BoardProperties.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Indicates whether the existing messages should be displayed to anonymous user.
    /// </summary>
    public bool EnableAnonymousRead
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableAnonymousRead"), false);
        }
        set
        {
            this.SetValue("EnableAnonymousRead", value);
        }
    }


    /// <summary>
    /// Board unsubscription URL.
    /// </summary>
    public string BoardUnsubscriptionUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BoardUnsubscriptionUrl"), "");
        }
        set
        {
            this.SetValue("BoardUnsubscriptionUrl", value);
        }
    }


    /// <summary>
    /// Board base URL.
    /// </summary>
    public string BoardBaseUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BoardBaseUrl"), "");
        }
        set
        {
            this.SetValue("BoardBaseUrl", value);
        }
    }


    /// <summary>
    /// Indicates whether board is opened.
    /// </summary>
    public bool BoardOpened
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BoardOpened"), false);
        }
        set
        {
            this.SetValue("BoardOpened", value);
        }
    }


    /// <summary>
    /// Indicates type of board access.
    /// </summary>
    public SecurityAccessEnum BoardAccess
    {
        get
        {
            return (SecurityAccessEnum)ValidationHelper.GetInteger(this.GetValue("BoardAccess"), 0);
        }
        set
        {
            this.SetValue("BoardAccess", value);
        }
    }


    /// <summary>
    /// Indicates the board message post requires e-mail.
    /// </summary>
    public bool BoardRequireEmails
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BoardRequireEmails"), false);
        }
        set
        {
            this.SetValue("BoardRequireEmails", value);
        }
    }


    /// <summary>
    /// Indicates whether the board is moderated.
    /// </summary>
    public bool BoardModerated
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BoardModerated"), false);
        }
        set
        {
            this.SetValue("BoardModerated", value);
        }
    }


    /// <summary>
    /// Indicates whether the CAPTCHA should be used.
    /// </summary>
    public bool BoardUseCaptcha
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BoardUseCaptcha"), false);
        }
        set
        {
            this.SetValue("BoardUseCaptcha", value);
        }
    }


    /// <summary>
    /// Board opened from.
    /// </summary>
    public DateTime BoardOpenedFrom
    {
        get
        {
            return ValidationHelper.GetDateTime(this.GetValue("BoardOpenedFrom"), DateTimeHelper.ZERO_TIME);
        }
        set
        {
            this.SetValue("BoardOpenedFrom", value);
        }
    }


    /// <summary>
    /// Board opened to.
    /// </summary>
    public DateTime BoardOpenedTo
    {
        get
        {
            return ValidationHelper.GetDateTime(this.GetValue("BoardOpenedTo"), DateTimeHelper.ZERO_TIME);
        }
        set
        {
            this.SetValue("BoardOpenedTo", value);
        }
    }


    /// <summary>
    /// Indicates whether logging activity is performed.
    /// </summary>
    public bool BoardLogActivity
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("BoardLogActivity"), false);
        }
        set
        {
            this.SetValue("BoardLogActivity", value);
        }
    }


    /// <summary>
    /// Enables/disables content rating
    /// </summary>
    public bool EnableContentRating
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableContentRating"), false);
        }
        set
        {
            this.SetValue("EnableContentRating", value);
        }
    }


    /// <summary>
    /// Gets or sets type of content rating scale.
    /// </summary>
    public string RatingType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RatingType"), "StarsAJAX");
        }
        set
        {
            this.SetValue("RatingType", value);
        }
    }


    /// <summary>
    /// Gets or sets max value/length of content rating scale
    /// </summary>
    public int MaxRatingValue
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRatingValue"), 10);
        }
        set
        {
            this.SetValue("MaxRatingValue", value);
        }
    }


    /// <summary>
    /// Gest or sest the cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return base.CacheItemName;
        }
        set
        {
            base.CacheItemName = value;
            this.msgBoard.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, msgBoard.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            msgBoard.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            msgBoard.CacheMinutes = value;
        }
    }

    #endregion


    protected void Page_Init(object sender, EventArgs e)
    {
        // Initialize the controls
        SetupControls();
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControls();        
    }


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControls()
    {
        // If the web part shouldn't proceed further
        if (this.StopProcessing)
        {
            this.msgBoard.BoardProperties.StopProcessing = true;
            this.Visible = false;
        }
        else
        {
            // Set the message board transformation
            this.msgBoard.MessageTransformation = this.MessageTransformation;

            // Set buttons
            this.msgBoard.BoardProperties.ShowApproveButton = this.ShowApprove;
            this.msgBoard.BoardProperties.ShowDeleteButton = this.ShowDelete;
            this.msgBoard.BoardProperties.ShowEditButton = this.ShowEdit;
            this.msgBoard.BoardProperties.ShowRejectButton = this.ShowReject;

            // Set caching
            this.msgBoard.CacheItemName = this.CacheItemName;
            this.msgBoard.CacheMinutes = this.CacheMinutes;
            this.msgBoard.CacheDependencies = this.CacheDependencies;

            // Use board properties
            if (this.BoardObj != null)
            {
                this.msgBoard.BoardProperties.BoardAccess = this.BoardObj.BoardAccess;
                this.msgBoard.BoardProperties.BoardOwner = "group";
                this.msgBoard.BoardProperties.BoardName = this.BoardObj.BoardName;
                this.msgBoard.BoardProperties.BoardDisplayName = this.BoardObj.BoardDisplayName;

                this.msgBoard.BoardProperties.BoardUnsubscriptionUrl = BoardInfoProvider.GetUnsubscriptionUrl(this.BoardObj.BoardUnsubscriptionURL, CMSContext.CurrentSiteName);
                this.msgBoard.BoardProperties.BoardBaseUrl = (string.IsNullOrEmpty(this.BoardObj.BoardBaseURL)) ? ValidationHelper.GetString(SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSBoardBaseURL"), "") : this.BoardObj.BoardBaseURL;
                this.msgBoard.BoardProperties.BoardEnableSubscriptions = this.BoardObj.BoardEnableSubscriptions;
                this.msgBoard.BoardProperties.BoardOpened = this.BoardObj.BoardOpened;
                this.msgBoard.BoardProperties.BoardRequireEmails = this.BoardObj.BoardRequireEmails;
                this.msgBoard.BoardProperties.BoardModerated = this.BoardObj.BoardModerated;
                this.msgBoard.BoardProperties.BoardUseCaptcha = this.BoardObj.BoardUseCaptcha;
                this.msgBoard.BoardProperties.BoardOpenedFrom = this.BoardObj.BoardOpenedFrom;
                this.msgBoard.BoardProperties.BoardOpenedTo = this.BoardObj.BoardOpenedTo;
                this.msgBoard.BoardProperties.BoardEnableAnonymousRead = this.EnableAnonymousRead;
                this.msgBoard.BoardProperties.EnableContentRating = this.EnableContentRating;
                this.msgBoard.BoardProperties.CheckPermissions = this.CheckPermissions;
                this.msgBoard.BoardProperties.RatingType = this.RatingType;
                this.msgBoard.BoardProperties.MaxRatingValue = this.MaxRatingValue;
                this.msgBoard.MessageBoardID = this.BoardObj.BoardID;
                this.msgBoard.NoMessagesText = this.NoMessagesText;
            }
            // Use default properties
            else
            {
                // If the board is group and information on current group wasn't supplied hide the web part
                if (this.CurrentGroup == null)
                {
                    this.Visible = false;
                }
                else
                {
                    // Default board- document related continue
                    this.msgBoard.BoardProperties.BoardAccess = this.BoardAccess;
                    this.msgBoard.BoardProperties.BoardOwner = "group";
                    this.msgBoard.BoardProperties.BoardName = GetBoardName(this.WebPartName);

                    string boardDisplayName = null;
                    if (!String.IsNullOrEmpty(this.BoardDisplayName))
                    {
                        boardDisplayName = this.BoardDisplayName;
                    }
                    // Use predefined display name format
                    else
                    {
                        boardDisplayName = CMSContext.CurrentPageInfo.DocumentName + " (" + CMSContext.CurrentPageInfo.DocumentNamePath + ")";
                    }
                    // Limit display name length
                    this.msgBoard.BoardProperties.BoardDisplayName = TextHelper.LimitLength(boardDisplayName, 250, "");
                    
                    this.msgBoard.BoardProperties.BoardUnsubscriptionUrl = BoardInfoProvider.GetUnsubscriptionUrl(this.BoardUnsubscriptionUrl, CMSContext.CurrentSiteName);
                    this.msgBoard.BoardProperties.BoardBaseUrl = (string.IsNullOrEmpty(this.BoardBaseUrl)) ? ValidationHelper.GetString(SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSBoardBaseURL"), "") : this.BoardBaseUrl;
                    this.msgBoard.BoardProperties.BoardEnableSubscriptions = this.BoardEnableSubscriptions;
                    this.msgBoard.BoardProperties.BoardOpened = this.BoardOpened;
                    this.msgBoard.BoardProperties.BoardRequireEmails = this.BoardRequireEmails;
                    this.msgBoard.BoardProperties.BoardModerated = this.BoardModerated;
                    this.msgBoard.BoardProperties.BoardModerators = this.BoardModerators;
                    this.msgBoard.BoardProperties.BoardUseCaptcha = this.BoardUseCaptcha;
                    this.msgBoard.BoardProperties.BoardOpenedFrom = this.BoardOpenedFrom;
                    this.msgBoard.BoardProperties.BoardOpenedTo = this.BoardOpenedTo;
                    this.msgBoard.BoardProperties.BoardEnableAnonymousRead = this.EnableAnonymousRead;
                    this.msgBoard.BoardProperties.BoardLogActivity = this.BoardLogActivity;
                    this.msgBoard.BoardProperties.EnableContentRating = this.EnableContentRating;
                    this.msgBoard.BoardProperties.CheckPermissions = this.CheckPermissions;
                    this.msgBoard.BoardProperties.RatingType = this.RatingType;
                    this.msgBoard.BoardProperties.MaxRatingValue = this.MaxRatingValue;
                    this.msgBoard.MessageBoardID = 0;
                    this.msgBoard.NoMessagesText = this.NoMessagesText;
                }
            }
        }
    }


    /// <summary>
    /// Returns board name according board type.
    /// </summary>
    /// <param name="webPartName">Name of the web part</param>
    private string GetBoardName(string webPartName)
    {
        return (webPartName + "_group_" + CurrentGroup.GroupGUID);
    }

    #endregion
}
