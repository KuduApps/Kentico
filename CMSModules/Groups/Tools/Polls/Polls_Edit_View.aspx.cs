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
using CMS.Polls;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Polls_Polls_Edit_View : CMSGroupPollsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get PollID and GroupID from querystring
        int pollId = QueryHelper.GetInteger("pollid", 0);
        int groupId = QueryHelper.GetInteger("groupid", 0);

        PollInfo pi = PollInfoProvider.GetPollInfo(pollId);
        EditedObject = pi;

        if ((pi != null) && (pi.PollGroupID == groupId))
        {
            PollView.PollCodeName = pi.PollCodeName;
            PollView.PollSiteID = pi.PollSiteID;
            PollView.PollGroupID = pi.PollGroupID;
            PollView.CountType = CountTypeEnum.Percentage;
            PollView.ShowGraph = true;
            PollView.ShowResultsAfterVote = true;
            // Check permissions during voting if user hasn't got 'Manage' permission
            PollView.CheckPermissions = (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", CMSAdminControl.PERMISSION_MANAGE));
            PollView.CheckVoted = false;
            PollView.HideWhenNotAuthorized = false;
            PollView.CheckOpen = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        int textLength = PollView.ButtonText.Length;
        if (textLength > 15)
        {
            PollView.VoteButton.CssClass = "XLongSubmitButton";
        }
        else if (textLength > 8)
        {
            PollView.VoteButton.CssClass = "LongSubmitButton";
        }
        else
        {
            PollView.VoteButton.CssClass = "SubmitButton";
        }
    }
}
