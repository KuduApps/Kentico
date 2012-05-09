using System;

using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Content_CMSDesk_Properties_Advanced_EditableContent_Default : CMSModalPage
{
    protected override void OnPreRender(EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        tree.Attributes["src"] = "tree.aspx" + URLHelper.Url.Query;
        main.Attributes["src"] = "main.aspx" + URLHelper.Url.Query;
        base.OnPreRender(e);
    }


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
}
