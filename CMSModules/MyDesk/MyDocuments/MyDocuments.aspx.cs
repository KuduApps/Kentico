using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_MyDesk_MyDocuments_MyDocuments : CMSDocumentsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyDocuments")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyDocuments");
        }

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("MyDesk.MyDocumentsTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_MyDesk/MyDocuments/module.png");

        CurrentMaster.Title.HelpTopicName = "my_documents";
        CurrentMaster.Title.HelpName = "helpTopic";

        ucMyDocuments.SiteName = CMSContext.CurrentSiteName;
    }
}
