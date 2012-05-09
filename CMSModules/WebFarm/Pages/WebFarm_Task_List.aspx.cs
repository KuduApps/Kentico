using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Security.Principal;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.WebFarmSync;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.WebFarmSyncHelper;
using CMS.EventLog;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.DataEngine;

public partial class CMSModules_WebFarm_Pages_WebFarm_Task_List : SiteManagerPage
{
    private const string allServers = "##ALL##";
    private string selectedServer = allServers;


    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)this.CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.ZeroRowsText = GetString("WebFarmTasks_List.ZeroRows");
        UniGrid.GridView.DataBound += new EventHandler(GridView_DataBound);
        UniGrid.GridView.RowDataBound += GridView_RowDataBound;

        lblServer.Text = GetString("WebFarmTasks_List.ServerLabel");
        btnEmptyTasks.Text = GetString("WebFarmTasks_List.EmptyButton");
        btnRunTasks.Text = GetString("WebFarmTasks_List.RunButton");

        uniSelector.SpecialFields = new string[1, 2] { { GetString("WebFarmList.All"), allServers } };
        uniSelector.DropDownSingleSelect.AutoPostBack = true;

        if (RequestHelper.IsPostBack())
        {
            selectedServer = ValidationHelper.GetString(uniSelector.Value, allServers);
        }

        if (selectedServer != allServers)
        {
            UniGrid.WhereCondition = "ServerName = '" + SqlHelperClass.GetSafeQueryString(selectedServer, false) + "'";
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        UniGrid.GridView.Columns[1].Visible = (selectedServer == allServers);
    }


    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string code = ValidationHelper.GetString(((DataRowView)(e.Row.DataItem)).Row["ErrorMessage"], string.Empty);
            if (code != String.Empty)
            {
                string color = ((e.Row.RowIndex & 1) == 1) ? "#EEC9C9" : "#FFDADA";
                e.Row.Style.Add("background-color", color);
            }
        }
    }



    protected void GridView_DataBound(object sender, EventArgs e)
    {
        btnEmptyTasks.Visible = !DataHelper.DataSourceIsEmpty(UniGrid.GridView.DataSource);
        btnRunTasks.Visible = !DataHelper.DataSourceIsEmpty(UniGrid.GridView.DataSource);
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            // Delete object from database
            if (selectedServer == allServers)
            {
                // Delete task object
                WebFarmTaskInfoProvider.DeleteWebFarmTaskInfo(Convert.ToInt32(actionArgument));
            }
            else
            {
                // Get infos for task and server
                WebFarmTaskInfo wfti = WebFarmTaskInfoProvider.GetWebFarmTaskInfo(Convert.ToInt32(actionArgument));
                WebFarmServerInfo wfsi = WebFarmServerInfoProvider.GetWebFarmServerInfo(selectedServer);
                // Delete task binding to server
                WebFarmTaskInfoProvider.DeleteServerTask(wfsi.ServerID, wfti.TaskID);
            }

            UniGrid.ReloadData();
        }
    }


    /// <summary>
    /// Clear task list.
    /// </summary>
    protected void btnEmptyTasks_Click(object sender, EventArgs e)
    {
        // Delete all task for specified server (or all servers)
        switch (selectedServer)
        {
            case allServers:
                // delete all task objects
                WebFarmTaskInfoProvider.DeleteAllTaskInfo();
                break;
            default:
                // delete bindings to specified server
                WebFarmTaskInfoProvider.DeleteAllTaskInfo(SqlHelperClass.GetSafeQueryString(selectedServer, false));
                break;
        }

        UniGrid.ReloadData();
    }


    /// <summary>
    /// Run task list.
    /// </summary>
    protected void btnRunTasks_Click(object sender, EventArgs e)
    {

        switch (selectedServer)
        {
            case allServers:
                WebSyncHelper.SynchronizeWebFarm(true);
                // Call synchronization method
                WebSyncHelper.ProcessMyTasks();
                break;

            default:
                // Get the server info object
                WebFarmServerInfo wfsi = WebFarmServerInfoProvider.GetWebFarmServerInfo(SqlHelperClass.GetSafeQueryString(selectedServer, false));
                // If server is enabled
                if (wfsi.ServerEnabled)
                {
                    if (wfsi.ServerName.ToLower() == WebSyncHelperClass.ServerName.ToLower())
                    {
                        // Call synchronization method
                        WebSyncHelper.ProcessMyTasks();
                    }
                    else
                    {
                        if (WebSyncHelperClass.Servers.Contains(wfsi.ServerID))
                        {
                            WebFarmUpdaterAsync wfu = new WebFarmUpdaterAsync();
                            // Add server for sync
                            wfu.Urls.Add(wfsi.ServerURL.TrimEnd('/') + WebSyncHelperClass.WebFarmUpdaterPage);
                        }
                    }
                }
                break;
        }

        UniGrid.ReloadData();

        // Show info label
        lblInfo.Text = GetString("webfarmtasks.taskexecuted");
        lblInfo.Visible = true;
    }
}
