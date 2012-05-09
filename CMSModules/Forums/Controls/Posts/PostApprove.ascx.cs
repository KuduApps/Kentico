using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.Forums;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.ExtendedControls;

public partial class CMSModules_Forums_Controls_Posts_PostApprove : CMSAdminEditControl
{
    #region "Variables"

    // Current PostID
    private int mPostID = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the post ID.
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
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Load the data
        LoadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Loads the data.
    /// </summary>
    private void LoadData()
    {
        // Get the forum post
        ForumPostInfo fpi = ForumPostInfoProvider.GetForumPostInfo(ValidationHelper.GetInteger(PostID, 0));
        ForumInfo fi = null;

        if (fpi != null)
        {
            fi = ForumInfoProvider.GetForumInfo(fpi.PostForumID);
        }
        else
        {
            return;
        }

        if (fi.ForumEnableAdvancedImage)
        {
            ltrText.AllowedControls = ControlsHelper.ALLOWED_FORUM_CONTROLS;
        }
        else
        {
            ltrText.AllowedControls = "none";
        }

        // Display converted datetime for live site
        lblDate.Text = CMSContext.ConvertDateTime(ValidationHelper.GetDateTime(fpi.PostTime, DateTimeHelper.ZERO_TIME), this).ToString();

        lblUser.Text = HTMLHelper.HTMLEncode(fpi.PostUserName);
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

        // Resolve the macros and display the post text
        ltrText.Text = dmh.ResolveMacros(fpi.PostText);
    }

    #endregion
}
