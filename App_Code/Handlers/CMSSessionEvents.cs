using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.SettingsProvider;

/// <summary>
/// Global CMS session events
/// </summary>
public class CMSSessionEvents
{
    /// <summary>
    /// Fires upon the application start
    /// </summary>
    public static CMSHandler Start = new CMSHandler();

    /// <summary>
    /// Fires upon the application end
    /// </summary>
    public static CMSHandler End = new CMSHandler();
}
