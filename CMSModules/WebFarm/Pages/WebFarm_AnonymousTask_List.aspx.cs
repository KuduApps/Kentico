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

public partial class CMSModules_WebFarm_Pages_WebFarm_AnonymousTask_List : SiteManagerPage
{
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

        btnEmptyTasks.Text = GetString("WebFarmTasks_List.EmptyButton");
        btnRunTasks.Text = GetString("WebFarmTasks_List.RunButton");
    }


    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string code = ValidationHelper.GetString(((DataRowView)(e.Row.DataItem)).Row["TaskErrorMessage"], string.Empty);
            if (code != String.Empty)
            {
                string color = ((e.Row.RowIndex & 1) == 1) ? "#EEC9C9" : "#FFDADA";
                e.Row.Style.Add("background-color", color);
            }
        }
    }



    protected void GridView_DataBound(object sender, EventArgs e)
    {
        pnlSites.Visible = !DataHelper.DataSourceIsEmpty(UniGrid.GridView.DataSource);
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
            // Delete task object
            WebFarmTaskInfoProvider.DeleteWebFarmTaskInfo(Convert.ToInt32(actionArgument));

            UniGrid.ReloadData();
        }
    }


    /// <summary>
    /// Clear task list.
    /// </summary>
    protected void btnEmptyTasks_Click(object sender, EventArgs e)
    {
        // Delete anonymous tasks
        WebFarmTaskInfoProvider.DeleteAllTaskInfo(null);

        UniGrid.ReloadData();
    }


    /// <summary>
    /// Run task list.
    /// </summary>
    protected void btnRunTasks_Click(object sender, EventArgs e)
    {
        // Call synchronization method
        WebSyncHelper.ProcessMyTasks();

        UniGrid.ReloadData();

        // Show info label
        lblInfo.Text = GetString("webfarmtasks.taskexecuted");
        lblInfo.Visible = true;
    }
}
