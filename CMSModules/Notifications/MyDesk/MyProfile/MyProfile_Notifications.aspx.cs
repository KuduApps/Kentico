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

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Notifications_MyDesk_MyProfile_MyProfile_Notifications : CMSMyProfilePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Notifications", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Notifications");
        }

        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyProfile.Notifications")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyProfile.Notifications");
        }

        this.userNotificationsElem.UserId = CMSContext.CurrentUser.UserID;
        this.userNotificationsElem.SiteID = CMSContext.CurrentSiteID;
    }
}
