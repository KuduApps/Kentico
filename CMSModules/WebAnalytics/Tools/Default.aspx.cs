using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.LicenseProvider;
using CMS.ExtendedControls;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_WebAnalytics_Tools_Default : CMSWebAnalyticsPage
{
    protected override void OnPreRender(EventArgs e)
    {
        analyticsTree.Attributes["src"] = "Analytics_Statistics.aspx" + URLHelper.Url.Query;

        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.colsFramesetAnalytics);
        }

        ScriptHelper.RegisterTitleScript(this, GetString("tools.ui.webanalytics"));

        base.OnPreRender(e);
    }
}
