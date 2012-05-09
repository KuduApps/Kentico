using System;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_MyDesk_MyProfile_MyProfile_Subscriptions : CMSMyProfilePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyProfile.Subscriptions")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyProfile.Subscriptions");
        }
    }
}
