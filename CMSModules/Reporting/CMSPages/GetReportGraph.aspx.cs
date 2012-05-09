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
using System.Drawing;
using System.Collections.Generic;
using System.Threading;

using CMS.CMSHelper;
using CMS.Reporting;
using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.EventLog;
using CMS.UIControls;


using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Reporting_CMSPages_GetReportGraph : CMSPage
{
    protected Guid sGraphGuid;  

    protected void Page_Load(object sender, EventArgs e)
    {      
        //check if it is request for saved graph - by graph guid
        sGraphGuid = QueryHelper.GetGuid("graphguid", Guid.Empty);
        if (sGraphGuid != Guid.Empty)
        {
            SavedGraphInfo sGraphInfo = SavedGraphInfoProvider.GetSavedGraphInfo(sGraphGuid);
            if (sGraphInfo != null)
            {
                SavedReportInfo savedReport = SavedReportInfoProvider.GetSavedReportInfo(sGraphInfo.SavedGraphSavedReportID);
                ReportInfo report = ReportInfoProvider.GetReportInfo(savedReport.SavedReportReportID);

                //check graph security settings
                if (report.ReportAccess != ReportAccessEnum.All)
                {
                    if (!CMSContext.CurrentUser.IsAuthenticated())
                    {
                        Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                        return;
                    }
                    else
                    {
                        // Check 'Read' permission
                        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Read"))
                        {
                            URLHelper.Redirect("~/CMSSiteManager/accessdenied.aspx?resource=cms.reporting&permission=Read");
                        }
                    }
                }

                //send response with image data
                SendGraph(sGraphInfo);
                return;
            }
        }        
        // Bad parameters, guid ... -> not found
        RequestHelper.Respond404();
    }


    /// <summary>
    /// Sends the graph.
    /// </summary>
    /// <param name="graphObj">Graph obj containing graph</param>
    protected void SendGraph(SavedGraphInfo graphObj)
    {
        if (graphObj != null)
        {
            SendGraph(graphObj.SavedGraphMimeType, graphObj.SavedGraphBinary);
        }
    }


    /// <summary>
    /// Sends the graph.
    /// </summary>
    /// <param name="mimeType">Response mime type</param>
    /// <param name="graph">Raw data to be sent</param>
    protected void SendGraph(string mimeType, byte[] graph)
    {
        // Clear response.
        CookieHelper.ClearResponseCookies();
        Response.Clear();

        this.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // Prepare response
        Response.ContentType = mimeType;
        Response.OutputStream.Write(graph, 0, graph.Length);

        //RequestHelper.CompleteRequest();
        RequestHelper.EndResponse();
    }
}
