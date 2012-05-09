using System;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSDesk_Administration_Default : CMSAdministrationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterTitleScript(this, GetString("cmsdesk.ui.administration"));
    }
}
