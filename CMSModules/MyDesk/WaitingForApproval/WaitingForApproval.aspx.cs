using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_MyDesk_WaitingForApproval_WaitingForApproval : CMSDocumentsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "WaitingDocs")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "WaitingDocs");
        }

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("MyDesk.WaitingForApproval");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_MyDesk/WaitingForApproval/module.png");

        CurrentMaster.Title.HelpTopicName = "waiting_for_my_approval";
        CurrentMaster.Title.HelpName = "helpTopic";

        ucWaitingForApproval.SiteName = CMSContext.CurrentSite.SiteName;
    }
}
