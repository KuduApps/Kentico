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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.DataEngine;

public partial class CMSModules_Forums_Controls_Posts_ForumPost : ForumViewer
{
    #region "Variables"

    private int mPostID = 0;

    private ForumPostInfo mPostInfo = null;
    private IDataClass mPostSrcData = null;

    // Information text
    private string mInfoText = "";

    private bool mDisplayOnly = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Info text.
    /// </summary>
    public string InfoText
    {
        get
        {
            return mInfoText;
        }
        set
        {
            mInfoText = value;
        }
    }


    /// <summary>
    /// Current post id.
    /// </summary>
    public int PostID
    {
        get
        {
            return mPostID;
        }
        set
        {
            mPostID = value;
            mPostInfo = null;
        }
    }


    /// <summary>
    /// Gets or sets post info object. Post with id defined by PostID is retrieved at first request.
    /// </summary>
    public ForumPostInfo PostInfo
    {
        get
        {
            if ((mPostInfo == null) && (this.PostID > 0))
            {
                mPostInfo = ForumPostInfoProvider.GetForumPostInfo(this.PostID);
                if (mPostInfo != null)
                {
                    mPostID = mPostInfo.PostId;
                }
            }

            return mPostInfo;
        }
        set
        {
            mPostInfo = value;
            if (mPostInfo != null)
            {
                mPostID = mPostInfo.PostId;
            }
        }
    }


    /// <summary>
    /// Gets or sets post data object.
    /// </summary>
    public IDataClass PostData
    {
        get
        {
            return mPostSrcData;
        }
        set
        {
            mPostSrcData = value;
        }
    }


    public bool DisplayOnly
    {
        get
        {
            return mDisplayOnly;
        }
        set
        {
            mDisplayOnly = value;
        }
    }

    #endregion


    /// <summary>
    /// OnPreRender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        ReloadData();

        base.OnPreRender(e);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        if ((InfoText != null) && (InfoText.Trim() != ""))
        {
            lblInfo.Visible = true;
            lblInfo.Text = InfoText;
        }

        ForumPostInfo fpi = null;
        ForumInfo fi = null;

        #region "Load data"

        if (this.PostData != null)
        {
            fpi = new ForumPostInfo(PostData);
        }

        if (fpi == null)
        {
            fpi = this.PostInfo;
        }

        if (fpi != null)
        {
            this.PostData = fpi.Generalized.DataClass;
            fi = ForumInfoProvider.GetForumInfo(fpi.PostForumID);
        }
        else
        {
            return;
        }

        #endregion

        if (fi.ForumEnableAdvancedImage)
        {
            ltlText.AllowedControls = ControlsHelper.ALLOWED_FORUM_CONTROLS;
        }
        else
        {
            ltlText.AllowedControls = "none";
        }

        lnkUserName.Text = HTMLHelper.HTMLEncode(fpi.PostUserName);

        // Display converted datetime for live site
        lblDate.Text = " (" + CMSContext.ConvertDateTime(ValidationHelper.GetDateTime(fpi.PostTime, DateTimeHelper.ZERO_TIME), this).ToString() + ")";

        lblSubject.Text = HTMLHelper.HTMLEncode(fpi.PostSubject);
        DiscussionMacroHelper dmh = new DiscussionMacroHelper();
        dmh.EnableBold = fi.ForumEnableFontBold;
        dmh.EnableItalics = fi.ForumEnableFontItalics;
        dmh.EnableStrikeThrough = fi.ForumEnableFontStrike;
        dmh.EnableUnderline = fi.ForumEnableFontUnderline;
        dmh.EnableCode = fi.ForumEnableCodeSnippet;
        dmh.EnableColor = fi.ForumEnableFontColor;
        dmh.EnableImage = fi.ForumEnableImage || fi.ForumEnableAdvancedImage;
        dmh.EnableQuote = fi.ForumEnableQuote;
        dmh.EnableURL = fi.ForumEnableURL || fi.ForumEnableAdvancedURL;
        dmh.MaxImageSideSize = fi.ForumImageMaxSideSize;
        dmh.QuotePostText = GetString("DiscussionMacroResolver.QuotePostText");

        if (fi.ForumHTMLEditor)
        {
            dmh.EncodeText = false;
            dmh.ConvertLineBreaksToHTML = false;
        }
        else
        {
            dmh.EncodeText = true;
            dmh.ConvertLineBreaksToHTML = true;
        }

        ltlText.Text = "<div class=\"PostText\">" + dmh.ResolveMacros(fpi.PostText) + "</div>";

        userAvatar.Text = AvatarImage(fpi);

        if (this.DisplayBadgeInfo)
        {
            if (fpi.PostUserID > 0)
            {
                UserInfo ui = UserInfoProvider.GetUserInfo(fpi.PostUserID);
                if ((ui != null) && !ui.IsPublic())
                {
                    BadgeInfo bi = BadgeInfoProvider.GetBadgeInfo(ui.UserSettings.UserBadgeID);
                    if (bi != null)
                    {
                        ltlBadge.Text = "<div class=\"Badge\">" + HTMLHelper.HTMLEncode(bi.BadgeDisplayName) + "</div>";
                    }
                }
            }

            // Set public badge if no badge is set
            if (String.IsNullOrEmpty(ltlBadge.Text))
            {
                ltlBadge.Text = "<div class=\"Badge\">" + GetString("Forums.PublicBadge") + "</div>";
            }
        }

        if (this.EnableSignature)
        {
            if (fpi.PostUserSignature.Trim() != "")
            {
                plcSignature.Visible = true;
                ltrSignature.Text = HTMLHelper.HTMLEncode(fpi.PostUserSignature);
            }
        }

        if (!this.DisplayOnly)
        {
            string threadId = ForumPostInfoProvider.GetPostRootFromIDPath(ValidationHelper.GetString(GetData(this.PostData, "PostIdPath"), "")).ToString();

            // Reply
            if (IsAvailable(this.PostData, ForumActionType.Reply))
            {
                lnkReply.Visible = true;
                lnkReply.Text = GetString("Forums_WebInterface_ForumPost.replyLinkText");
                lnkReply.NavigateUrl = URLHelper.UpdateParameterInUrl(GetURL(this.PostData, ForumActionType.Reply), "threadid", threadId);
            }
            else
            {
                lnkReply.Visible = false;
            }

            // Quote
            if (IsAvailable(this.PostData, ForumActionType.Quote))
            {
                lnkQuote.Visible = true;
                lnkQuote.Text = GetString("Forums_WebInterface_ForumPost.quoteLinkText");
                lnkQuote.NavigateUrl = URLHelper.UpdateParameterInUrl(GetURL(this.PostData, ForumActionType.Quote), "threadid", threadId);

            }
            else
            {
                lnkQuote.Visible = false;
            }

            // Display subscribe link
            if (IsAvailable(this.PostData, ForumActionType.SubscribeToPost))
            {
                lnkSubscribe.Visible = true;
                lnkSubscribe.Text = GetString("Forums_WebInterface_ForumPost.Subscribe");
                lnkSubscribe.NavigateUrl = URLHelper.UpdateParameterInUrl(GetURL(this.PostData, ForumActionType.SubscribeToPost), "threadid", threadId);
            }
            else
            {
                lnkSubscribe.Visible = false;
            }

            lnkUserName.CssClass = "PostUserLink";

            if (!String.IsNullOrEmpty(fpi.PostUserMail) && (fi.ForumDisplayEmails))
            {
                lnkUserName.NavigateUrl = "mailto:" + HTMLHelper.HTMLEncode(fpi.PostUserMail) + "?subject=" + HTMLHelper.HTMLEncode(fpi.PostSubject);
                lnkUserName.CssClass = "PostUser";
            }
        }

        // Display action panel only if reply to link or subscribtion link are visible
        this.plcActions.Visible = ((this.lnkReply.Visible || this.lnkSubscribe.Visible || lnkQuote.Visible) & !this.DisplayOnly);

        if ((lnkReply.Visible) && (lnkQuote.Visible || lnkSubscribe.Visible))
        {
            lblActionSeparator.Visible = true;
        }

        if ((lnkQuote.Visible) && (lnkSubscribe.Visible))
        {
            lblActionSeparator2.Visible = true;
        }
    }


    /// <summary>
    /// Render.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        //if (!isEmpty)
        //{
        //    if (!mForumProperties.EnableOnSiteManagement)
        //    {
        //        ForumPostManage1.Visible = false;
        //        ForumPostManage1.StopProcessing = false;

        //    }

        base.Render(writer);
        //}
    }
}
