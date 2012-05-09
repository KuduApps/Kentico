using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Polls;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Polls_Polls_Edit_Answer_List : CMSGroupPollsPage
{
    protected int pollId = 0;
    protected int groupId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get poll id from querystring		
        pollId = QueryHelper.GetInteger("pollId", 0);
        groupId = QueryHelper.GetInteger("groupId", 0);

        string[,] actions = new string[2, 7];

        // New item link
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Polls_Answer_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Polls_Edit_Answer_Edit.aspx?pollId=" + pollId.ToString() + "&groupId=" + groupId);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Polls_PollAnswer/add.png");

        // Reset answer button
        actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[1, 1] = GetString("Polls_Answer_List.ResetButton");
        actions[1, 2] = "return confirm(" + ScriptHelper.GetString(GetString("Polls_Answer_List.ResetConfirmation")) + ");";
        actions[1, 3] = null;
        actions[1, 4] = null;
        actions[1, 5] = GetImageUrl("CMSModules/CMS_Polls/resetanswers.png");
        actions[1, 6] = "btnReset_Click";

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);

        AnswerList.OnEdit += new EventHandler(AnswerList_OnEdit);
        AnswerList.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(AnswerList_OnCheckPermissions);
        AnswerList.PollId = pollId;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!RequestHelper.IsPostBack())
        {
            AnswerList.ReloadData();
        }
    }


    void AnswerList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check 'Manage' permission
        int groupId = 0;

        PollInfo pi = PollInfoProvider.GetPollInfo(AnswerList.PollId);
        if (pi != null)
        {
            groupId = pi.PollGroupID;
        }

        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    /// <summary>
    /// AnswerList edit action handler.
    /// </summary>
    void AnswerList_OnEdit(object sender, EventArgs e)
    {
        URLHelper.Redirect("Polls_Edit_Answer_Edit.aspx?answerId=" + AnswerList.SelectedItemID.ToString() + "&groupId=" + groupId);
    }


    /// <summary>
    /// Header action handler.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "btnreset_click":   // Reset all answer counts
                // Check 'Manage' permission
                PollInfo pi = PollInfoProvider.GetPollInfo(AnswerList.PollId);
                int groupId = 0;

                if (pi != null)
                {
                    groupId = pi.PollGroupID;
                }

                // Check permissions
                CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);

                if (pollId > 0)
                {
                    PollAnswerInfoProvider.ResetAnswers(pollId);
                    AnswerList.ReloadData();
                }
                break;
        }
    }
}
