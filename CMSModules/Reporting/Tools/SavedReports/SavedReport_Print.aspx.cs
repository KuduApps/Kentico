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

using CMS.Reporting;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Reporting_Tools_SavedReports_SavedReport_Print : CMSReportingModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo user = CMSContext.CurrentUser;

        int reportId = ValidationHelper.GetInteger(Request.QueryString["reportid"], 0);
        SavedReportInfo sri = SavedReportInfoProvider.GetSavedReportInfo(reportId);
        if (sri != null)
        {
            ltlHtml.Text = HTMLHelper.ResolveUrls(sri.SavedReportHTML, ResolveUrl("~/"));
        }
    }
}
