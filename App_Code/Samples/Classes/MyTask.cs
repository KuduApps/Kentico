using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.EventLog;
using CMS.Scheduler;

namespace Custom
{
    /// <summary>
    /// Sample task class.
    /// </summary>
    public class MyTask : ITask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="ti">Task info</param>
        public string Execute(TaskInfo ti)
        {
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "MyTask", "Execute", null, "This task was executed from '~/App_Code/Global/CMS/CMSCustom.cs'.");

            return null;
        }
    }
}
