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

public partial class CMSModules_Polls_Tools_Polls_Answer_Edit : CMSPollsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get AnswerID and PollID from querystring
        int pollId = QueryHelper.GetInteger("pollid", 0);

        string currentPollAnswer = GetString("Polls_Answer_Edit.NewItemCaption");
        
        int answerId = QueryHelper.GetInteger("answerId", 0);
        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            AnswerEdit.Saved = true;
        }
        AnswerEdit.ItemID = answerId;
        AnswerEdit.PollId = pollId;

        if (answerId > 0)
        {
            // Modifying existing answer
            this.CurrentMaster.Title.HelpTopicName = "answer_edit";
            PollAnswerInfo pollAnswerObj = PollAnswerInfoProvider.GetPollAnswerInfo(answerId);
            EditedObject = pollAnswerObj;
            if (pollAnswerObj != null)
            {
                currentPollAnswer = GetString("Polls_Answer_Edit.AnswerLabel") + " " + pollAnswerObj.AnswerOrder.ToString();
                pollId = pollAnswerObj.AnswerPollID;
            }
        }
        else
        {
            // Creating new answer - check if parent object exists
            EditedObject = PollInfoProvider.GetPollInfo(pollId);
            this.CurrentMaster.Title.HelpTopicName = "new_answer";
        }

        // Initializes page title control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Polls_Answer_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Polls/Tools/Polls_Answer_List.aspx?pollId=" + pollId;
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
        actions[0, 3] = ResolveUrl("Polls_Answer_Edit.aspx?pollId=" + pollId.ToString());
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Polls/addanswer.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        AnswerEdit.OnSaved += new EventHandler(AnswerEdit_OnSaved);
        AnswerEdit.IsLiveSite = false;
    }


    /// <summary>
    /// AnswerEdit event handler.
    /// </summary>
    protected void AnswerEdit_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Polls_Answer_Edit.aspx?answerId=" + AnswerEdit.ItemID.ToString() + "&saved=1");
    }
}
