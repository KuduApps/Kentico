using System;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSSiteManager_Administration_default : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        ScriptHelper.RegisterTitleScript(this, GetString("Header.Administration"));
    }
}
