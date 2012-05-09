using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Forums;

public partial class CMSModules_Forums_Controls_Layouts_Flat_SubscriptionEdit : ForumViewer
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsAdHocForum)
        {
            plcHeader.Visible = false;
        }

        // Check whether subscription is for forum or post
        if (ForumContext.CurrentSubscribeThread == null)
        {
            ltrTitle.Text = GetString("ForumSubscription.SubscribeForum");
        }
        else
        {
            plcPreview.Visible = true;

            ltrTitle.Text = GetString("ForumSubscription.SubscribePost");

            ltrAvatar.Text = AvatarImage(ForumContext.CurrentSubscribeThread);
            ltrSubject.Text = HTMLHelper.HTMLEncode(ForumContext.CurrentSubscribeThread.PostSubject);
            ltrText.Text = ResolvePostText(ForumContext.CurrentSubscribeThread.PostText);
            ltrUserName.Text = HTMLHelper.HTMLEncode(ForumContext.CurrentSubscribeThread.PostUserName);
            ltrTime.Text = CMSContext.ConvertDateTime(ForumContext.CurrentSubscribeThread.PostTime, this).ToString();
        }
    }
}

