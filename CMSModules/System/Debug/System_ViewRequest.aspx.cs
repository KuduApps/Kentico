using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.SiteProvider;
using CMS.OutputFilter;
using CMS.IO;

public partial class CMSModules_System_Debug_System_ViewRequest : CMSDebugPage
{
    Guid guid = Guid.Empty;
    

    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup title
        CurrentMaster.Title.TitleText = GetString("ViewRequest.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/__GLOBAL__/Object.png");

        guid = QueryHelper.GetGuid("guid", Guid.Empty);
        if (guid != Guid.Empty)
        {
            // Find the root log
            RequestLog log = Find(new ArrayList[] { 
                SqlHelperClass.LastLogs, 
                CacheHelper.LastLogs, 
                File.LastLogs,
                CMSControlsHelper.LastLogs, 
                SecurityHelper.LastLogs, 
                OutputHelper.LastLogs,
                RequestHelper.LastLogs
            } );

            if (log != null)
            {
                // Setup the logs
                RequestLogs logs = log.ParentLogs;

                this.plcLogs.Controls.Add(new LiteralControl("<div><strong>&nbsp;" + logs.RequestURL + "</strong> (" + logs.RequestTime.ToString("hh:MM:ss") + ")</div><br />"));

                this.logFiles.Log = logs.FilesLog;
                this.logCache.Log = logs.CacheLog;
                this.logOutput.Log = logs.OutputLog;
                this.logSec.Log = logs.SecurityLog;
                this.logMac.Log = logs.MacrosLog;
                this.logSQL.Log = logs.QueryLog;
                this.logState.Log = logs.ViewStateLog;
                this.logReq.Log = logs.RequestLog;
                this.logFarm.Log = logs.WebFarmLog;
            }
        }
    }


    /// <summary>
    /// Finds any log in the given lists.
    /// </summary>
    /// <param name="lists">Array of the available lists</param>
    protected RequestLog Find(ArrayList[] lists)
    {
        foreach (ArrayList list in lists)
        {
            RequestLog log = RequestLog.FindByGUID(list, guid);
            if (log != null)
            {
                return log;
            }
        }

        return null;
    }

    #endregion
}
