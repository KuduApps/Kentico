using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSModules_MyDesk_MyProfile_MyProfile_Categories : CMSMyProfilePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check site availability
        if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.Categories", CMSContext.CurrentSiteName))
        {
            RedirectToResourceNotAvailableOnSite("CMS.Categories");
        }

        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyProfile.Categories")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyProfile.Categories");
        }
    }
}
