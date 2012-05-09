using System;
using System.Text;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

public partial class CMSModules_ContactManagement_Controls_UI_ActivityDetails_PollVoting : ActivityDetail
{

    #region "Methods"

    public override bool LoadData(ActivityInfo ai)
    {
        if ((ai == null) || !ModuleEntry.IsModuleLoaded(ModuleEntry.POLLS) || (ai.ActivityType != PredefinedActivityType.POLL_VOTING))
        {
            return false;
        }

        GeneralizedInfo ipollinfo = ModuleCommands.PollsGetPollInfo(ai.ActivityItemID);
        if (ipollinfo != null)
        {
            string pollQuestion = ValidationHelper.GetString(ipollinfo.GetValue("PollQuestion"), null);
            ucDetails.AddRow("om.activitydetails.pollquestion", pollQuestion);
        }

        if (ai.ActivityValue != null)
        {
            string[] answerIDs = ai.ActivityValue.Split(new char[] {ActivityLogProvider.POLL_ANSWER_SEPARATOR}, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder answers = new StringBuilder();
            foreach (string id in answerIDs)
            {
                GeneralizedInfo iansinfo = ModuleCommands.PollsGetPollAnswerInfo(ValidationHelper.GetInteger(id, 0));
                if (iansinfo != null)
                {
                    answers.Append(HTMLHelper.HTMLEncode(ValidationHelper.GetString(iansinfo.GetValue("AnswerText"), null)));
                    answers.Append("<br />");
                }
            }
        ucDetails.AddRow("om.activitydetails.pollanswer", answers.ToString());
        }

        return ucDetails.IsDataLoaded;
    }


    #endregion
}

