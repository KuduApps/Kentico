using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.EventLog;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.DataEngine;

public partial class CMSPages_GetTestingModeReport : CMSPage
{
    private static int id = 0;

    /// <summary>
    /// Returns information about web.config.
    /// </summary>
    private string GetAppSettingsInfo()
    {
        StringBuilder sb = new StringBuilder();

        foreach (string key in ConfigurationManager.AppSettings.AllKeys)
        {
            sb.AppendLine("<div>");
            sb.AppendLine(GetAppSetting(key));
            sb.AppendLine("</div>");
        }

        return sb.ToString();
    }


    /// <summary>
    /// Returns one web.config key info.
    /// </summary>
    /// <param name="key">Key name</param>
    private string GetAppSetting(string key)
    {
        string setting = "<add key=\"" + key + "\" value=\"" + ConfigurationManager.AppSettings[key] + "\" />";
        return HTMLHelper.HTMLEncode(setting);
    }


    /// <summary>
    /// Returns information about web.config.
    /// </summary>
    private string GetConnectionStringsInfo()
    {
        StringBuilder sb = new StringBuilder();

        foreach (ConnectionStringSettings key in ConfigurationManager.ConnectionStrings)
        {
            sb.AppendLine("<div>");
            sb.AppendLine(GetConnStr(key));
            sb.AppendLine("</div>");
        }

        return sb.ToString();
    }


    /// <summary>
    /// Returns one connection string info.
    /// </summary>
    /// <param name="connStr">Connection string name</param>
    private string GetConnStr(ConnectionStringSettings connStr)
    {
        string setting = "<add name=\"" + connStr.Name + "\" connectionString=\"" + connStr.ConnectionString + "\" />";
        return HTMLHelper.HTMLEncode(setting);
    }


    /// <summary>
    /// Returns CMS Version with build time.
    /// </summary>
    private string GetCMSVersion()
    {
        Version v = CMSContext.GetCMSVersion();
        string result = v.ToString(4);
        DateTime date = new DateTime(2000, 1, 1);
        date = date.AddDays(v.Build).AddSeconds(v.MinorRevision * 2);
        return result + " (" + date.ToString() + ")";
    }


    /// <summary>
    /// Returns basic testing information.
    /// </summary>
    private string GetBasicInfo()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table>",
            "<tr><td><strong>",
                ResHelper.GetString("cmstesting.livesiteurl"),
            "</strong></td><td>",
                URLHelper.GetApplicationUrl(),
            "</td></tr>",
            "<tr><td><strong>",
                ResHelper.GetString("cmstesting.buildnumber"),
            "</strong></td><td>",
                GetCMSVersion(),
            "</td></tr>",
            "<tr><td><strong>",
                ResHelper.GetString("cmstesting.currentuser"),
            "</strong></td><td>",
               CMSContext.CurrentUser.UserName,
            "</td></tr>",
            "<tr><td><strong>",
                ResHelper.GetString("cmstesting.useragent"),
            "</strong></td><td>",
               BrowserHelper.GetUserAgent(),
            "</td></tr>",
            "<tr><td><strong>",
                ResHelper.GetString("cmstesting.connectionstrings"),
            "</strong></td><td>",
               GetConnectionStringsInfo(),
            "</td></tr>",
            "</table>");

        return sb.ToString();
    }


    /// <summary>
    /// Returns last 20 events from EventLog.
    /// </summary>
    private string GetEventLog()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<div>");

        EventLogProvider eventProvider = new EventLogProvider();
        DataSet ds = eventProvider.GetAllEvents("EventType = 'E' OR EventType = 'W'", "EventTime DESC", 20, null);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            sb.Append("<table style=\"width: 100%;\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td><strong>" + ResHelper.GetString("cmstesting.eventlog.EventType") + "</strong></td>");
            sb.AppendLine("<td><strong>" + ResHelper.GetString("cmstesting.eventlog.EventCode") + "</strong></td>");
            sb.AppendLine("<td><strong>" + ResHelper.GetString("cmstesting.eventlog.UserName") + "</strong></td>");
            sb.AppendLine("<td><strong>" + ResHelper.GetString("cmstesting.eventlog.IPAddress") + "</strong></td>");
            sb.AppendLine("<td><strong>" + ResHelper.GetString("cmstesting.eventlog.DocumentName") + "</strong></td>");
            sb.AppendLine("<td><strong>" + ResHelper.GetString("cmstesting.eventlog.SiteName") + "</strong></td>");
            sb.AppendLine("<td><strong>" + ResHelper.GetString("cmstesting.eventlog.EventMachineName") + "</strong></td>");
            sb.AppendLine("</tr>");

            string evenStyle = " background-color: rgb(244, 244, 244);";

            int i = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string siteName = SiteInfoProvider.GetSiteName(ValidationHelper.GetInteger(dr["SiteID"], 0));

                sb.AppendLine("<tr style=\"cursor: pointer;" + (i % 2 == 0 ? evenStyle : "") + "\" onclick=\"document.getElementById('table_event_" + i + "').style.display = (this.hide ? 'none' : 'block'); this.hide = !this.hide; return false;\">");
                sb.AppendLine("<td>" + dr["EventType"] + "</td>");
                sb.AppendLine("<td>" + dr["EventCode"] + "</td>");
                sb.AppendLine("<td>" + dr["UserName"] + "</td>");
                sb.AppendLine("<td>" + dr["IPAddress"] + "</td>");
                sb.AppendLine("<td>" + dr["DocumentName"] + "</td>");
                sb.AppendLine("<td>" + siteName + "</td>");
                sb.AppendLine("<td>" + dr["EventMachineName"] + "</td>");
                sb.AppendLine("</tr>");
                sb.AppendLine("<tr><td colspan=\"7\">");
                string item = "<table style=\"display: none;\" id=\"table_event_" + i + "\">";
                foreach (DataColumn col in ds.Tables[0].Columns)
                {
                    item += GetEventLogItem(col.ColumnName, dr);
                }
                item += "</table>";
                sb.AppendLine(item);
                sb.AppendLine("</td></tr>");

                i++;
            }
            sb.Append("</table>");
        }
        sb.Append("</div>");
        return sb.ToString();
    }


    /// <summary>
    /// Returns one event log event item (one column transformed to one row in a table).
    /// </summary>
    /// <param name="columnName">Column name</param>
    /// <param name="dr">DataRow</param>
    private string GetEventLogItem(string columnName, DataRow dr)
    {
        string item = "";
        string itemValue = ValidationHelper.GetString(dr[columnName], "");
        if (!string.IsNullOrEmpty(itemValue))
        {
            item += "<tr><td><strong>" + ResHelper.GetString("cmstesting.eventlog." + columnName) + "</strong></td><td>" + itemValue + "</td></tr>";
        }
        return item;
    }


    /// <summary>
    /// Returns system information (renderes control on Site Manager / System / General tab).
    /// </summary>
    private string GetDebug()
    {
        StringBuilder sb = new StringBuilder();

        int id = 0;

        for (int i = RequestHelper.LastLogs.Count - 1; i >= 0; i--)
        {
            // Get the request log
            RequestLog log = (RequestLog)RequestHelper.LastLogs[i];
            if (log != null)
            {
                List<DataTable> logs = AllLog.GetLogs(log);
                if (logs.Count > 1)
                {
                    DataTable table = AllLog.MergeTables(logs, this.Page, false);

                    string tdStyle = "style=\"border: 1px solid rgb(204, 204, 204);\"";
                    string evenStyle = "style=\"background-color: rgb(244, 244, 244);\"";
                    string headerStyle = "style=\"border: 1px solid rgb(204, 204, 204); white-space: nowrap;\" scope=\"col\"";
                    string tableStyle = "style=\"background-color: White; border-color: rgb(204, 204, 204); border-style: solid; width: 100%; border-collapse: collapse;\"";

                    sb.Append("<div style=\"margin-top: 15px;\"><strong>" + log.RequestURL + "</strong>" + "&nbsp;(" + log.RequestTime + ")<br/>");

                    sb.Append(
                        "<table " + tableStyle + "><tbody><tr " + evenStyle + " align=\"left\"><th " + headerStyle + ">&nbsp;</th>",
                        "<th " + headerStyle + ">",
                        GetString("AllLog.DebugType"),
                        "</th><th " + headerStyle + ">",
                        GetString("AllLog.Information"),
                        "</th><th " + headerStyle + ">",
                        GetString("AllLog.Result"),
                        "</th><th " + headerStyle + ">",
                        GetString("AllLog.Context"),
                        "</th><th " + headerStyle + ">",
                        GetString("AllLog.TotalDuration"),
                        "</th><th " + headerStyle + ">",
                        GetString("AllLog.Duration"),
                        "</th></tr>");

                    bool even = false;
                    foreach (DataRow dr in table.Rows)
                    {
                        sb.Append(
                            "<tr" + (even ? " " + evenStyle : "") + "><td " + tdStyle + "><strong>",
                            dr["Index"],
                            "</strong></td><td " + tdStyle + ">",
                            dr["DebugType"],
                            "</td><td " + tdStyle + ">",
                            dr["Information"],
                            "</td><td " + tdStyle + ">",
                            dr["Result"],
                            "</td><td " + tdStyle + " align=\"right\">",
                            GetContext(dr["Context"].ToString(), id++),
                            "</td><td " + tdStyle + " width=\"70px\" align=\"right\">",
                            dr["TotalDuration"],
                            "</td><td " + tdStyle + " width=\"70px\" align=\"right\">",
                            dr["Duration"],
                            "</td></tr>");
                        even = !even;
                    }

                    sb.Append("</tbody></table></div>");
                }
            }
        }

        return sb.ToString();
    }


    /// <summary>
    /// Changes the script for hiding/showing context in the debug.
    /// </summary>
    /// <param name="original">Original context HTML</param>
    /// <param name="id">ID of the control</param>
    private string GetContext(string original, int id)
    {
        string result = original.Replace("this.nextSibling", "document.getElementById('context_" + id + "')");
        result = result.Replace("<div style=\"display: none; color: #888888;\">", "<div id=\"context_" + id + "\" style=\"display: none; color: #888888;\">");
        return result;
    }


    /// <summary>
    /// Returns info from System / General tab.
    /// </summary>
    private string GetSystemInfo()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<table>");
        sb.AppendLine("<tr><td>");

        // System information
        sb.AppendLine("<table>");
        sb.AppendLine("<tr><td colspan=\"2\"><strong>");
        sb.AppendLine(ResHelper.GetString("Administration-System.SystemInfo"));
        sb.AppendLine("</strong></td></tr>");
        sb.AppendLine(GetRow("Administration-System.MachineName", HTTPHelper.MachineName));
        sb.AppendLine(GetEmptyRow());
        sb.AppendLine(GetRow("Administration-System.Account", System.Security.Principal.WindowsIdentity.GetCurrent().Name));
        sb.AppendLine(GetRow("Administration-System.Version", System.Environment.Version.ToString()));

        string pool = "N/A";
        try
        {
            pool = SystemHelper.GetApplicationPoolName();
        }
        catch { }
        sb.AppendLine(GetRow("Administration-System.Pool", pool));
        sb.AppendLine(GetEmptyRow());
        sb.AppendLine(GetRow("Administration-System.IP", Request.UserHostAddress));
        sb.AppendLine(GetRow("Administration-System.MachineName", HTTPHelper.MachineName));
        sb.AppendLine("</table>");

        sb.AppendLine("</td><td>");

        // Database information
        sb.AppendLine("<table>");
        sb.AppendLine("<tr><td colspan=\"2\"><strong>");
        sb.AppendLine(ResHelper.GetString("Administration-System.DatabaseInfo"));
        sb.AppendLine("</strong></td></tr>");
        sb.AppendLine(GetRow("Administration-System.ServerName", TableManager.DatabaseServerName));
        sb.AppendLine(GetRow("Administration-System.ServerVersion", TableManager.DatabaseServerVersion));
        sb.AppendLine(GetEmptyRow());
        sb.AppendLine(GetRow("Administration-System.DatabaseName", TableManager.DatabaseName));
        sb.AppendLine(GetRow("Administration-System.DatabaseSize", TableManager.DatabaseSize));
        sb.AppendLine("</table>");

        sb.AppendLine("</td></tr>");
        sb.AppendLine(GetEmptyRow());
        sb.AppendLine("<tr><td>");

        // Memory statistics
        sb.AppendLine("<table>");
        sb.AppendLine("<tr><td colspan=\"2\"><strong>");
        sb.AppendLine(ResHelper.GetString("Administration-System.MemoryStatistics"));
        sb.AppendLine("</strong></td></tr>");
        sb.AppendLine(GetRow("Administration-System.Memory", DataHelper.GetSizeString(GC.GetTotalMemory(false))));
        sb.AppendLine(GetEmptyRow());

        string virtualMemory = "N/A";
        string physicalMemory = "N/A";
        string peakMemory = "N/A";
        try
        {
            virtualMemory = DataHelper.GetSizeString(SystemHelper.GetVirtualMemorySize());
            physicalMemory = DataHelper.GetSizeString(SystemHelper.GetWorkingSetSize());
            peakMemory = DataHelper.GetSizeString(SystemHelper.GetPeakWorkingSetSize());
        }
        catch { }

        sb.AppendLine(GetRow("Administration-System.PeakMemory", peakMemory));
        sb.AppendLine(GetRow("Administration-System.PhysicalMemory", physicalMemory));
        sb.AppendLine(GetRow("Administration-System.VirtualMemory", virtualMemory));
        sb.AppendLine("</table>");

        sb.AppendLine("</td><td>");

        // GC information
        sb.AppendLine("<table>");
        sb.AppendLine("<tr><td colspan=\"2\"><strong>");
        sb.AppendLine(ResHelper.GetString("Administration-System.GC"));
        sb.AppendLine("</strong></td></tr>");
        try
        {
            int generations = GC.MaxGeneration;
            for (int i = 0; i <= generations; i++)
            {
                int count = GC.CollectionCount(i);
                sb.AppendLine("<tr><td style=\"white-space: nowrap; width: 200px;\">" + GetString("GC.Generation") + " " + i.ToString() + ":</td><td>" + count.ToString() + "</td></tr>");
            }
        }
        catch { }
        sb.AppendLine("</table>");

        sb.AppendLine("</td></tr>");
        sb.AppendLine(GetEmptyRow());
        sb.AppendLine("<tr><td>");

        // Page view statistics
        long pending = RequestHelper.PendingRequests.GetValue(null);
        if (pending > 1)
        {
            // Current request does not count as pending at the time of display
            pending--;
        }
        if (pending < 0)
        {
            pending = 0;
        }

        // Remove miliseconds from the end of the time string
        string timeSpan = (DateTime.Now - CMSAppBase.ApplicationStart).ToString();
        int index = timeSpan.LastIndexOf('.');
        if (index >= 0)
        {
            timeSpan = timeSpan.Remove(index);
        }

        sb.AppendLine("<table>");
        sb.AppendLine("<tr><td><strong>");
        sb.AppendLine(ResHelper.GetString("Administration-System.PageViews"));
        sb.AppendLine("</strong></td><td><strong>");
        sb.AppendLine(ResHelper.GetString("Administration-System.PageViewsValues"));
        sb.AppendLine("</strong></td></tr>");
        sb.AppendLine(GetRow("Administration-System.Pages", RequestHelper.TotalPageRequests.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.GetFilePages", RequestHelper.TotalGetFileRequests.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.SystemPages", RequestHelper.TotalSystemPageRequests.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.NonPages", RequestHelper.TotalNonPageRequests.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.PagesNotFound", RequestHelper.TotalPageNotFoundRequests.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.Pending", pending.ToString()));
        sb.AppendLine(GetEmptyRow());
        sb.AppendLine(GetRow("Administration.System.RunTime", timeSpan));
        sb.AppendLine("</table>");

        sb.AppendLine("</td><td>");

        // Cache statistics
        sb.AppendLine("<table>");
        sb.AppendLine("<tr><td colspan=\"2\"><strong>");
        sb.AppendLine(ResHelper.GetString("Administration-System.CacheStatistics"));
        sb.AppendLine("</strong></td></tr>");
        sb.AppendLine(GetRow("Administration-System.CacheItems", Cache.Count.ToString()));
        sb.AppendLine(GetEmptyRow());
        sb.AppendLine(GetRow("Administration-System.CacheExpired", CacheHelper.Expired.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.CacheRemoved", CacheHelper.Removed.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.CacheDependency", CacheHelper.DependencyChanged.GetValue(null).ToString()));
        sb.AppendLine(GetRow("Administration-System.CacheUnderused", CacheHelper.Underused.GetValue(null).ToString()));
        sb.AppendLine("</table>");

        sb.AppendLine("</td></tr></table>");

        return sb.ToString();
    }

    /// <summary>
    /// Creates one row of a table (tr tag).
    /// </summary>
    /// <param name="title">Left part</param>
    /// <param name="content">Right part</param>
    private string GetEmptyRow()
    {
        return "<tr><td colspan=\"2\">&nbsp;</td></tr>";
    }


    /// <summary>
    /// Creates one row of a table (tr tag).
    /// </summary>
    /// <param name="title">Left part</param>
    /// <param name="content">Right part</param>
    private string GetRow(string title, string content)
    {
        return "<tr><td>" + ResHelper.GetString(title) + "</td><td>" + content + "</td></tr>";
    }


    /// <summary>
    /// Creates show hide section.
    /// </summary>
    /// <param name="title">Title of the section</param>
    /// <param name="content">Content which can be dynamically shown/hidden</param>
    /// <param name="showByDefault">If true content is shown by default</param>
    /// <param name="isHeader">If true, title is put into h2 tag</param>
    private string MakeShowHideSection(string title, string content, bool showByDefault, bool isHeader)
    {
        string titleTag = (isHeader ? "<h2>" + HTMLHelper.HTMLEncode(title) + "</h2>" : HTMLHelper.HTMLEncode(title));
        string result = "<div><a onclick=\"document.getElementById('section_" + id + "').style.display = (this.hide ? 'none' : 'block'); this.hide = !this.hide; return false;\" href=\"#\">" + titleTag + "</a>";
        result += "<div id=\"section_" + id + "\" style=\"display: " + (!showByDefault ? "none" : "block") + ";\">" + content + "</div></div>";
        id++;
        return result;
    }


    /// <summary>
    /// Generates report code.
    /// </summary>
    public string GetReportCode()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(
            "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">",
            "<html xmlns=\"http://www.w3.org/1999/xhtml\">",
            "<head><title>CMS Testing Report</title></head><body>",
            "<h1>" + ResHelper.GetString("cmstesting.basicinformation") + "</h1>",
            "<div class=\"BasicInfo\">",
            GetBasicInfo(),
            MakeShowHideSection(ResHelper.GetString("cmstesting.system"), GetSystemInfo(), false, true),
            "</div>",
            "<h1>" + ResHelper.GetString("cmstesting.advancedinformation") + "</h1>",
            "<div class=\"AdvancedInfo\">",
            MakeShowHideSection(ResHelper.GetString("cmstesting.webconfig"), GetAppSettingsInfo(), false, true),
            MakeShowHideSection(ResHelper.GetString("cmstesting.eventlog"), GetEventLog(), false, true),
            MakeShowHideSection(ResHelper.GetString("cmstesting.debug"), GetDebug(), false, true),
            "</div></body></html>");
        return HTMLHelper.ReformatHTML(sb.ToString(), "  ");
    }


    /// <summary>
    /// Creates the download report response.
    /// </summary>
    private void MakeResponse()
    {
        // Clear all generated HTML code
        Response.Clear();

        // Generate sql script
        string responseText = GetReportCode();
        if (responseText != null)
        {
            // Add header containing attachment
            Response.AddHeader("content-disposition", "attachment;filename=cmstesting_report.html");

            // Ensure UTF-8 formating
            Response.ContentEncoding = Encoding.UTF8;

            // Set content type
            Response.ContentType = "text/html";
            Response.Charset = string.Empty;

            // Disable view state
            EnableViewState = false;

            // Clear content of response
            Response.ClearContent();

            // Start writing sql script to output stream
            Response.Output.Write(responseText);
            Response.Output.Close();

            // End response
            RequestHelper.EndResponse();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (SettingsKeyProvider.TestingMode)
        {
            MakeResponse();
        }
    }

}