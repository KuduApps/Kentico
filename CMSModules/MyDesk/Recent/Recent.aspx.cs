using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_MyDesk_Recent_Recent : CMSDocumentsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "RecentDocs")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "RecentDocs");
        }

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("MyDesk.RecentTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_MyDesk/RecentDocuments/module.png");

        CurrentMaster.Title.HelpTopicName = "recent_documents";
        CurrentMaster.Title.HelpName = "helpTopic";

        ucRecent.SiteName = CMSContext.CurrentSite.SiteName;
    }
}
