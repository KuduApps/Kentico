using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.SettingsProvider;

/// <summary>
/// Global CMS request events
/// </summary>
public class CMSRequestEvents
{
    /// <summary>
    /// Fires upon the begin request
    /// </summary>
    public static CMSHandler Begin = new CMSHandler();

    /// <summary>
    /// Fires upon the end request
    /// </summary>
    public static CMSHandler End = new CMSHandler();

    /// <summary>
    /// Fires upon the request authentication
    /// </summary>
    public static CMSHandler Authenticate  = new CMSHandler();

    /// <summary>
    /// Fires upon the request authorization
    /// </summary>
    public static CMSHandler Authorize = new CMSHandler();

    /// <summary>
    /// Fires upon the request handler mapping
    /// </summary>
    public static CMSHandler MapRequestHandler = new CMSHandler();

    /// <summary>
    /// Fires upon the acquiring request state
    /// </summary>
    public static CMSHandler AcquireRequestState = new CMSHandler();
}
