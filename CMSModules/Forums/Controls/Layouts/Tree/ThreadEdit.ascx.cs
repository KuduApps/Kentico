using System;

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Forums_Controls_Layouts_Tree_ThreadEdit : ForumViewer
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CopyValues(forumEdit);

        if (this.IsAdHocForum)
        {
            plcHeader.Visible = false;
        }

        forumEdit.OnPreview += forumEdit_OnPreview;
        forumEdit.OnModerationRequired += new EventHandler(forumEdit_OnModerationRequired);

        // Check whether subscription is for forum or post
        if (ForumContext.CurrentReplyThread == null)
        {
            ltrTitle.Text = GetString("Forums_ForumNewPost_Header.NewThread");

            if (ForumContext.CurrentPost != null && ForumContext.CurrentMode == ForumMode.Edit)
            {
                ltrTitle.Text = GetString("Forums_ForumNewPost_Header.EditPost");
            }
        }
        else
        {
            plcPreview.Visible = true;
            ltrTitle.Text = GetString("Forums_ForumNewPost_Header.Reply");
            ltrAvatar.Text = AvatarImage(ForumContext.CurrentReplyThread);
            ltrSubject.Text = HTMLHelper.HTMLEncode(ForumContext.CurrentReplyThread.PostSubject);
            ltrText.Text = ResolvePostText(ForumContext.CurrentReplyThread.PostText);
            ltrUserName.Text = HTMLHelper.HTMLEncode(ForumContext.CurrentReplyThread.PostUserName);
            ltrTime.Text = CMSContext.ConvertDateTime(ForumContext.CurrentReplyThread.PostTime, this).ToString();

            UserSettingsInfo usi = UserSettingsInfoProvider.GetUserSettingsInfoByUser(ForumContext.CurrentReplyThread.PostUserID);
            BadgeInfo bi = null;
            string badgeName = null;
            string badgeImageUrl = null;

            if (usi != null)
            {
                bi = BadgeInfoProvider.GetBadgeInfo(usi.UserBadgeID);
                if (bi != null)
                {
                    badgeName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(bi.BadgeDisplayName));
                    badgeImageUrl = HTMLHelper.HTMLEncode(bi.BadgeImageURL);
                }
            }

            ltrBadge.Text = GetNotEmpty(badgeName, "<div class=\"Badge\">" + badgeName + "</div>", "<div class=\"Badge\">" + GetString("Forums.PublicBadge") + "</div>", ForumActionType.Badge) +
                            GetNotEmpty(badgeImageUrl, "<div class=\"BadgeImage\"><img alt=\"" + badgeName + "\" src=\"" + ResolveUrl(ValidationHelper.GetString(badgeImageUrl, "")) + "\" /></div>", "", ForumActionType.Badge);
        }
    }


    /// <summary>
    /// OnModeration required.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void forumEdit_OnModerationRequired(object sender, EventArgs e)
    {
        CMSUserControl ctrl = sender as CMSUserControl;
        if (ctrl != null)
        {
            ctrl.StopProcessing = true;
        }

        plcPreview.Visible = false;
        forumEdit.Visible = false;
        plcModerationRequired.Visible = true;
        ltrTitle.Visible = false;
        lblModerationInfo.Text = GetString("forums.requiresmoderationafteraction");
        btnOk.Text = GetString("general.ok");
    }


    /// <summary>
    /// OK click hadler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Redirect back to the forum or forum thread
        URLHelper.Redirect(ClearURL());
    }


    /// <summary>
    /// OnPreview handler.
    /// </summary>
    protected void forumEdit_OnPreview(object sender, EventArgs e)
    {
        // Preview title
        ltrTitle.Text = GetString("Forums_ForumNewPost_Header.Preview");
        // Display placeholder
        plcPreview.Visible = true;

        // Post properties
        ForumPostInfo fpi = sender as ForumPostInfo;
        if (fpi != null)
        {
            ltrAvatar.Text = AvatarImage(fpi);
            ltrSubject.Text = HTMLHelper.HTMLEncode(fpi.PostSubject);
            ltrText.Text = ResolvePostText(fpi.PostText);
            ltrUserName.Text = HTMLHelper.HTMLEncode(fpi.PostUserName);
            ltrTime.Text = CMSContext.ConvertDateTime(fpi.PostTime, this).ToString();

            BadgeInfo bi = null;
            string badgeName = null;
            string badgeImageUrl = null;

            bi = BadgeInfoProvider.GetBadgeInfo(CMSContext.CurrentUser.UserSettings.UserBadgeID);
            if (bi != null)
            {
                badgeName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(bi.BadgeDisplayName));
                badgeImageUrl = HTMLHelper.HTMLEncode(bi.BadgeImageURL);
            }

            ltrBadge.Text = GetNotEmpty(badgeName, "<div class=\"Badge\">" + badgeName + "</div>", "<div class=\"Badge\">" + GetString("Forums.PublicBadge") + "</div>", ForumActionType.Badge) +
                            GetNotEmpty(badgeImageUrl, "<div class=\"BadgeImage\"><img alt=\"" + badgeName + "\" src=\"" + ResolveUrl(ValidationHelper.GetString(badgeImageUrl, "")) + "\" /></div>", "", ForumActionType.Badge);
        }
    }
}
