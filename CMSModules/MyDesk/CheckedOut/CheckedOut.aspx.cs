using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_MyDesk_CheckedOut_CheckedOut : CMSDocumentsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.WorkflowVersioning);
        }

        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "CheckedOutDocs")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "CheckedOutDocs");
        }

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("MyDesk.CheckedOutTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_MyDesk/CheckedOut/module.png");

        CurrentMaster.Title.HelpTopicName = "checked_out_by_me";
        CurrentMaster.Title.HelpName = "helpTopic";        

        ucCheckedOut.SiteName = CMSContext.CurrentSite.SiteName;
    }
}
