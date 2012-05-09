using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.EventLog;
using CMS.GlobalHelper;

/// <summary>
/// Sample custom cache helper, does log an event upon cache item removal.
/// </summary>
public class CustomCacheHelper : CacheHelper
{
    /// <summary>
    /// Removes the item from the cache.
    /// </summary>
    /// <param name="key">Cache key</param>
    protected override object RemoveInternal(string key)
    {
        // Log the event that the user was updated
        EventLogProvider ev = new EventLogProvider();
        ev.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "MyCustomCacheHelper", "Remove", null, "The cache item was removed");

        return base.RemoveInternal(key);
    }
}