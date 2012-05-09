using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.EventLog;
using CMS.SiteProvider;

namespace Custom
{
    /// <summary>
    /// Sample search index class.
    /// </summary>
    public class MyIndex : ICustomSearchIndex
    {
        /// <summary>
        /// Implementation of rebuild method.
        /// </summary>
        /// <param name="srchInfo">Search index info</param>
        public void Rebuild(SearchIndexInfo srchInfo)
        {
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "MyCustomIndex", "Rebuild", null, "The index is building");
        }
    }
}