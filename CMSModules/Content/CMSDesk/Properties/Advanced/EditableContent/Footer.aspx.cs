using System;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Content_CMSDesk_Properties_Advanced_EditableContent_Footer : CMSContentPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check 'read' permissions
        if (!user.IsAuthorizedPerResource("CMS.Content", "Read"))
        {
            RedirectToAccessDenied("CMS.Content", "Read");
        }

        // Check UIProfile
        if (!user.IsAuthorizedPerUIElement("CMS.Content", "Properties.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.General");
        }

        if (!user.IsAuthorizedPerUIElement("CMS.Content", "General.Advanced"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "General.Advanced");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
