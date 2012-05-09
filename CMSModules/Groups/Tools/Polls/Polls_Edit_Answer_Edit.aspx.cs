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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Polls;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Polls_Polls_Edit_Answer_Edit : CMSGroupPollsPage
{
    protected int pollId = 0;
    protected int answerId = 0;
    protected int groupId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get AnswerID and PollID from querystring
        pollId = QueryHelper.GetInteger("pollId", 0);
        answerId = QueryHelper.GetInteger("answerId", 0);
        groupId = QueryHelper.GetInteger("groupId", 0);

        string currentPollAnswer = GetString("Polls_Answer_Edit.NewItemCaption");

        // Initialize AnswerEdit control
        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            AnswerEdit.Saved = true;
        }
        AnswerEdit.ItemID = answerId;
        AnswerEdit.PollId = pollId;
        AnswerEdit.OnSaved += new EventHandler(AnswerEdit_OnSaved);
        AnswerEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(AnswerEdit_OnCheckPermissions);

        if (answerId > 0)
        {
            this.CurrentMaster.Title.HelpTopicName = "answer_edit";
            PollAnswerInfo pollAnswerObj = PollAnswerInfoProvider.GetPollAnswerInfo(answerId);
            EditedObject = pollAnswerObj;
            if (pollAnswerObj != null)
            {
                // Check that poll belongs to the specified group
                if ((pollAnswerObj.AnswerPollID > 0) && (groupId > 0))
                {
                    PollInfo poll = PollInfoProvider.GetPollInfo(pollAnswerObj.AnswerPollID);

                     // Answer not found or doesn't belong to specified group
                     if ((poll == null) || (poll.PollGroupID != groupId))
                     {
                         RedirectToAccessDenied(GetString("community.group.pollnotassigned"));
                     }
                }

                // Set control
                currentPollAnswer = GetString("Polls_Answer_Edit.AnswerLabel") + " " + pollAnswerObj.AnswerOrder.ToString();
                pollId = pollAnswerObj.AnswerPollID;
            }
        }
        else
        {
            this.CurrentMaster.Title.HelpTopicName = "new_answer";
        }

        // Validate
        EditedObject = PollInfoProvider.GetPollInfo(pollId);

        if (!RequestHelper.IsPostBack())
        {
            AnswerEdit.ReloadData();
        }

        // Initializes page title control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Polls_Answer_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Groups/Tools/Polls/Polls_Edit_Answer_List.aspx?pollId=" + pollId + "&groupId=" + groupId;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = currentPollAnswer;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Polls_Answer_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Polls_Edit_Answer_Edit.aspx?pollId=" + pollId.ToString() + "&groupId=" + groupId);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Polls_PollAnswer/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    void AnswerEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check 'Manage' permission
        PollInfo pi = PollInfoProvider.GetPollInfo(AnswerEdit.PollId);
        int groupId = 0;

        if (pi != null)
        {
            groupId = pi.PollGroupID;
        }

        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    /// <summary>
    /// AnswerEdit event handler.
    /// </summary>
    void AnswerEdit_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Polls_Edit_Answer_Edit.aspx?answerId=" + AnswerEdit.ItemID.ToString() + "&groupId=" + groupId + "&saved=1");
    }
}
