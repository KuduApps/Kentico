using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.SiteProvider;
using CMS.EventLog;

/// <summary>
/// Sample custom user info provider, does log an event upon the user update.
/// </summary>
public class CustomSiteInfoProvider : SiteInfoProvider
{
    /// <summary>
    /// Sets the specified site data.
    /// </summary>
    /// <param name="siteInfoObj">New site info data</param>
    protected override void SetSiteInfoInternal(SiteInfo siteInfoObj)
    {
        base.SetSiteInfoInternal(siteInfoObj);

        // Log the event that the site was updated
        EventLogProvider ev = new EventLogProvider();
        ev.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "MyCustomSiteInfoProvider", "SetSiteInfo", null, "The site was updated");
    }
}