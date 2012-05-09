using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.CMSHelper;

public partial class CMSModules_Integration_Pages_Administration_Log : CMSIntegrationPage
{
    #region "Variables"

    private IntegrationSynchronizationInfo mSynchronizationInfo = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the synchronization identifier.
    /// </summary>
    private int SynchronizationID
    {
        get
        {
            return QueryHelper.GetInteger("synchronizationid", 0);
        }
    }


    /// <summary>
    /// Gets the synchronization info object.
    /// </summary>
    private IntegrationSynchronizationInfo SynchronizationInfo
    {
        get
        {
            return mSynchronizationInfo ?? (mSynchronizationInfo = IntegrationSynchronizationInfoProvider.GetIntegrationSynchronizationInfo(SynchronizationID));
        }
    }

    #endregion"


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register modal dialog scripts
        RegisterModalPageScripts();

        gridLog.OnAction += gridLog_OnAction;
        gridLog.ZeroRowsText = GetString("Task.LogNoEvents");
        gridLog.WhereCondition = "SyncLogSynchronizationID = " + SynchronizationID;

        CurrentMaster.Title.TitleText = GetString("Task.LogHeader");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Integration_Task/object.png");
        CurrentMaster.DisplayControlsPanel = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (SynchronizationInfo != null)
        {
            IntegrationTaskInfo ti = IntegrationTaskInfoProvider.GetIntegrationTaskInfo(SynchronizationInfo.SynchronizationTaskID);
            IntegrationConnectorInfo si = IntegrationConnectorInfoProvider.GetIntegrationConnectorInfo(SynchronizationInfo.SynchronizationConnectorID);
            // Prepare task description
            StringBuilder sbTaskInfo = new StringBuilder();
            sbTaskInfo.Append("<table>");
            if ((ti != null) || (si != null))
            {
                if (ti != null)
                {
                    sbTaskInfo.Append("<tr><td class=\"Title Grid\">" + GetString("integration.tasktitle") + ":</td><td>" + HTMLHelper.HTMLEncode(ti.TaskTitle) + "</td></tr>");
                }
                if (si != null)
                {
                    sbTaskInfo.Append("<tr><td class=\"Title Grid\">" + GetString("integration.connectorname") + ":</td><td>" + HTMLHelper.HTMLEncode(si.ConnectorDisplayName) + "</td></tr>");
                }
            }
            sbTaskInfo.Append("</table>");
            lblInfo.Text = sbTaskInfo.ToString();
        }
        lblInfo.Visible = (lblInfo.Text != "");
        base.OnPreRender(e);
    }

    #endregion


    #region "Control events"

    /// <summary>
    /// UniGrid action event handler.
    /// </summary>
    protected void gridLog_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                int logid = ValidationHelper.GetInteger(actionArgument, 0);
                if (logid > 0)
                {
                    IntegrationSyncLogInfoProvider.DeleteIntegrationSyncLogInfo(logid);
                }
                break;
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        IntegrationSyncLogInfoProvider.DeleteIntegrationSyncLogs(SynchronizationID);
        gridLog.ReloadData();
    }

    #endregion
}