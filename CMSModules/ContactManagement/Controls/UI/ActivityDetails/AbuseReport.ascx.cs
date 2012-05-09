using System;

using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.WebAnalytics;
using CMS.SiteProvider;

public partial class CMSModules_ContactManagement_Controls_UI_ActivityDetails_AbuseReport : ActivityDetail
{

    #region "Methods"

    public override bool LoadData(ActivityInfo ai)
    {
        if ((ai == null) || ai.ActivityType != PredefinedActivityType.ABUSE_REPORT)
        {
            return false;
        }

        int nodeId = ai.ActivityNodeID;
        lblDocIDVal.Text = GetLinkForDocument(nodeId, ai.ActivityCulture);

        AbuseReportInfo ari = AbuseReportInfoProvider.GetAbuseReportInfo(ai.ActivityItemID);
        if (ari != null)
        {
            txtComment.Text = ari.ReportComment;
        }

        return true;
    }

    #endregion
}

