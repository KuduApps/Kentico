using System;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_MyDesk_MyProfile_MyProfile_ChangePassword : CMSMyProfilePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyProfile.ChangePassword")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyProfile.ChangePassword");
        }
    }
}
