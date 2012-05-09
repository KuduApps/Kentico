using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.SettingsProvider;

/// <summary>
/// Global CMS application events
/// </summary>
public class CMSApplicationEvents
{
    /// <summary>
    /// Fires upon the application start
    /// </summary>
    public static CMSHandler Start = new CMSHandler();

    /// <summary>
    /// Fires upon the application end
    /// </summary>
    public static CMSHandler End = new CMSHandler();

    /// <summary>
    /// Fires upon the application error
    /// </summary>
    public static CMSHandler Error = new CMSHandler();
}
