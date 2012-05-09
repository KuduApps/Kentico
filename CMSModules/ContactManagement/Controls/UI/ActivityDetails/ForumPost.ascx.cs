using System;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_ContactManagement_Controls_UI_ActivityDetails_ForumPost : ActivityDetail
{
    #region "Methods"

    public override bool LoadData(ActivityInfo ai)
    {
        if ((ai == null) || !ModuleEntry.IsModuleLoaded(ModuleEntry.FORUMS))
        {
            return false;
        }

        switch (ai.ActivityType)
        {
            case PredefinedActivityType.FORUM_POST:
            case PredefinedActivityType.SUBSCRIPTION_FORUM_POST:
                break;
            default:
                return false;

        }

        int nodeId = ai.ActivityNodeID;
        lblDocIDVal.Text = GetLinkForDocument(nodeId, ai.ActivityCulture);

        if (ai.ActivityType == PredefinedActivityType.FORUM_POST)
        {
            GeneralizedInfo iinfo = ModuleCommands.ForumsGetForumPostInfo(ai.ActivityItemDetailID);
            if (iinfo != null)
            {
                plcComment.Visible = true;
                lblPostSubjectVal.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(iinfo.GetValue("PostSubject"), null));
                txtPost.Text = ValidationHelper.GetString(iinfo.GetValue("PostText"), null);
            }
        }

        return true;
    }


    #endregion
}

