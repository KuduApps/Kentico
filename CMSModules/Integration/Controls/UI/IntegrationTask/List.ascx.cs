using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Threading;
using System.Text;
using System.Security.Principal;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Synchronization;
using CMS.SettingsProvider;
using CMS.SynchronizationEngine;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.EventLog;

public partial class CMSModules_Integration_Controls_UI_IntegrationTask_List : CMSAdminListControl
{
    #region "Variables and enums"

    /// <summary>
    /// Message storage for async control
    /// </summary>
    protected static Hashtable mInfos = new Hashtable();

    private string eventCode = null;
    private string eventType = null;

    protected int currentSiteId = 0;
    protected CurrentUserInfo currentUser = null;


    // Possible actions
    private enum Action
    {
        SelectAction = 0,
        Synchronize = 1,
        Delete = 2
    }


    // Action scope
    private enum What
    {
        SelectedTasks = 0,
        AllTasks = 1
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Inner grid.
    /// </summary>
    public UniGrid Grid
    {
        get
        {
            return gridElem;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Determines whether to display inbound or outbound tasks
    /// </summary>
    public bool TasksAreInbound
    {
        get;
        set;
    }


    /// <summary>
    /// Identifier of selected connector.
    /// </summary>
    public int ConnectorID
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            gridElem.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Current Info.
    /// </summary>
    public string CurrentInfo
    {
        get
        {
            return ValidationHelper.GetString(mInfos["SyncInfo_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["SyncInfo_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Current log context.
    /// </summary>
    public LogContext CurrentLog
    {
        get
        {
            return EnsureLog();
        }
    }


    /// <summary>
    /// Current Error.
    /// </summary>
    public string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mInfos["SyncError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["SyncError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Gets or sets the cancel string.
    /// </summary>
    public string CanceledString
    {
        get
        {
            return ValidationHelper.GetString(mInfos["SyncCancel_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["SyncCancel_" + ctlAsync.ProcessGUID] = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Initialize current user for the async actions
            currentUser = CMSContext.CurrentUser;

            if (!RequestHelper.IsCallback())
            {
                currentSiteId = CMSContext.CurrentSiteID;

                gridElem.OnBeforeDataReload += gridElem_OnBeforeDataReload;
                gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;
                gridElem.OnAction += gridElem_OnAction;

                // Register client validation script
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), ClientID + "_actionScript", ScriptHelper.GetScript(PrepareScript()));

                // Initialize buttons
                btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");
                btnCancel.Text = GetString("General.Cancel");
                btnOk.OnClientClick = "return PerformAction();";
                btnOk.Click += new EventHandler(btnOk_Click);

                pnlContent.Visible = true;
                pnlLog.Visible = false;

                // Initialize dropdown list
                if (!RequestHelper.IsPostBack())
                {
                    drpWhat.Items.Add(new ListItem(GetString("integration." + What.SelectedTasks), Convert.ToInt32(What.SelectedTasks).ToString()));
                    drpWhat.Items.Add(new ListItem(GetString("integration." + What.AllTasks), Convert.ToInt32(What.AllTasks).ToString()));
                }
            }

            ctlAsync.OnFinished += ctlAsync_OnFinished;
            ctlAsync.OnError += ctlAsync_OnError;
            ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
            ctlAsync.OnCancel += ctlAsync_OnCancel;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Hide labels if empty
        lblError.Visible = (lblError.Text != string.Empty);
        lblInfo.Visible = (lblInfo.Text != string.Empty);

        // Hide multiple actions if grid is empty
        pnlFooter.Visible = !gridElem.IsEmpty;

        // Initialize actions dropdown list - they need to be refreshed on connector selection
        drpAction.Items.Clear();
        drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));

        // Add synchronize option only when task processing is enabled
        IntegrationConnectorInfo connector = IntegrationConnectorInfoProvider.GetIntegrationConnectorInfo(ConnectorID);
        if ((TasksAreInbound ? IntegrationHelper.IntegrationProcessExternal : IntegrationHelper.IntegrationProcessInternal) && ((ConnectorID <= 0) || (connector != null) && connector.ConnectorEnabled && (IntegrationHelper.GetConnector(connector.ConnectorName) != null)))
        {
            drpAction.Items.Add(new ListItem(GetString("general." + Action.Synchronize), Convert.ToInt32(Action.Synchronize).ToString()));
        }
        drpAction.Items.Add(new ListItem(GetString("general." + Action.Delete), Convert.ToInt32(Action.Delete).ToString()));

        base.OnPreRender(e);
    }

    #endregion


    #region "Grid events"

    protected void gridElem_OnBeforeDataReload()
    {
        gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "TaskIsInbound = " + Convert.ToInt32(TasksAreInbound));
        if (ConnectorID > 0)
        {
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ConnectorID = " + ConnectorID);
            gridElem.GridView.Columns[4].Visible = false;
        }
    }


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView drv = null;
        switch (sourceName.ToLower())
        {
            case "result":
                drv = parameter as DataRowView;
                string errorMsg = ValidationHelper.GetString(drv["SynchronizationErrorMessage"], string.Empty);

                bool errorOccurred = !string.IsNullOrEmpty(errorMsg);
                if (errorOccurred)
                {
                    int synchronizationId = ValidationHelper.GetInteger(drv["SynchronizationID"], 0);
                    string logUrl = ResolveUrl("~/CMSModules/Integration/Pages/Administration/Log.aspx?synchronizationid=") + synchronizationId;
                    return "<a target=\"_blank\" href=\"" + logUrl + "\" onclick=\"modalDialog('" + logUrl + "', 'tasklog', 700, 500); return false;\">" + GetString("Tasks.ResultFailed") + "</a>";
                }
                else
                {
                    return string.Empty;
                }

            case "view":
                if (sender is ImageButton)
                {
                    ImageButton viewButton = sender as ImageButton;
                    drv = UniGridFunctions.GetDataRowView(viewButton.Parent as DataControlFieldCell);
                    int taskId = ValidationHelper.GetInteger(drv["TaskID"], 0);
                    string detailUrl = ResolveUrl("~/CMSModules/Integration/Pages/Administration/View.aspx?taskid=") + taskId;
                    viewButton.OnClientClick = "modalDialog('" + detailUrl + "', 'tasklog', 700, 500); return false;";
                    return viewButton;
                }
                return parameter;

            case "run":
                if (sender is ImageButton)
                {
                    ImageButton runButton = sender as ImageButton;
                    drv = UniGridFunctions.GetDataRowView(runButton.Parent as DataControlFieldCell);
                    bool connectorEnabled = ValidationHelper.GetBoolean(drv["ConnectorEnabled"], false);
                    string connectorName = ValidationHelper.GetString(drv["ConnectorName"], String.Empty);
                    bool processingDisabled = TasksAreInbound ? !IntegrationHelper.IntegrationProcessExternal : !IntegrationHelper.IntegrationProcessInternal;
                    if (processingDisabled || (IntegrationHelper.GetConnector(connectorName) == null) || !connectorEnabled)
                    {
                        // Set appropriate tooltip
                        if (processingDisabled)
                        {
                            runButton.ToolTip = GetString("integration.processingdisabled");
                        }
                        else
                        {
                            string connectorDisplayName = ValidationHelper.GetString(drv["ConnectorDisplayName"], String.Empty);
                            if (!connectorEnabled)
                            {
                                runButton.ToolTip = String.Format(GetString("integration.connectordisabled"), HTMLHelper.HTMLEncode(connectorDisplayName));
                            }
                            else
                            {
                                runButton.ToolTip = String.Format(GetString("integration.connectorunavailable"), HTMLHelper.HTMLEncode(connectorDisplayName));
                            }
                        }
                        runButton.ImageUrl = GetImageUrl("/Design/Controls/UniGrid/Actions/SynchronizeDisabled.png");
                        runButton.OnClientClick = "return false;";
                        runButton.Style.Add(HtmlTextWriterStyle.Cursor, "default");
                        return runButton;
                    }
                }
                break;
        }
        return parameter;
    }


    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        int synchronizationId = ValidationHelper.GetInteger(actionArgument, 0);
        switch (actionName.ToLower())
        {
            case "delete":
                // Delete synchronization
                IntegrationSynchronizationInfoProvider.DeleteIntegrationSynchronizationInfo(synchronizationId);
                break;

            case "run":
                // Get synchronization
                IntegrationSynchronizationInfo synchronization = IntegrationSynchronizationInfoProvider.GetIntegrationSynchronizationInfo(synchronizationId);
                if (synchronization != null)
                {
                    // Get connector and task
                    IntegrationConnectorInfo connectorInfo = IntegrationConnectorInfoProvider.GetIntegrationConnectorInfo(synchronization.SynchronizationConnectorID);
                    IntegrationTaskInfo taskInfo = IntegrationTaskInfoProvider.GetIntegrationTaskInfo(synchronization.SynchronizationTaskID);
                    if ((connectorInfo != null) && (taskInfo != null))
                    {
                        // Get connector instance
                        BaseIntegrationConnector connector = IntegrationHelper.GetConnector(connectorInfo.ConnectorName) as BaseIntegrationConnector;
                        if (connector != null)
                        {
                            // Process the task
                            if (TasksAreInbound)
                            {
                                // Always try to process the task when requested from UI
                                taskInfo.TaskProcessType = IntegrationProcessTypeEnum.Default;
                                connector.ProcessExternalTask(taskInfo);
                            }
                            else
                            {
                                connector.ProcessInternalTask(taskInfo);
                            }
                        }
                    }
                }
                break;
        }
    }

    #endregion


    #region "Async methods"

    /// <summary>
    /// All items synchronization.
    /// </summary>
    protected void SynchronizeAll(object parameter)
    {
        StringBuilder result = new StringBuilder();
        eventCode = "SYNCALLTASKS";
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        try
        {
            AddLog(GetString("Synchronization.RunningTasks"));

            // Get the synchronizations
            DataSet ds = IntegrationTaskInfoProvider.GetIntegrationTasksView(gridElem.CompleteWhereCondition, "SynchronizationID ASC", -1, "TaskTitle, ConnectorID, SynchronizationTaskID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // Get necessary synchronization properties
                    int taskId = ValidationHelper.GetInteger(row["SynchronizationTaskID"], 0);
                    int connectorId = ValidationHelper.GetInteger(row["ConnectorID"], 0);

                    result.Append(ProcessSynchronization(connectorId, taskId));
                }
            }

            // Log possible errors
            if (!String.IsNullOrEmpty(result.ToString()))
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError);
            }
            else
            {
                CurrentInfo = GetString("Tasks.SynchronizationOK");
                AddLog(CurrentInfo);
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // Canceled by user
                CurrentInfo = CanceledString;
                AddLog(CurrentInfo);
            }
            else
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError, result.ToString());
            }
        }
        catch (Exception ex)
        {
            CurrentError = GetString("Tasks.SynchronizationFailed") + ": " + ex.Message;
            AddErrorLog(CurrentError);
        }
        finally
        {
            // Finalize log context
            FinalizeContext();
        }
    }


    /// <summary>
    /// Synchronization of selected items.
    /// </summary>
    /// <param name="parameter">ArrayList of selected items</param>
    public void SynchronizeSelected(object parameter)
    {
        ArrayList list = parameter as ArrayList;
        if (list == null)
        {
            return;
        }

        StringBuilder result = new StringBuilder();
        eventCode = "SYNCSELECTEDTASKS";
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        try
        {
            AddLog(GetString("Synchronization.RunningTasks"));

            list.Sort();

            foreach (string taskIdString in list)
            {
                // Get synchronization
                int synchronizationId = ValidationHelper.GetInteger(taskIdString, 0);
                IntegrationSynchronizationInfo synchronization = IntegrationSynchronizationInfoProvider.GetIntegrationSynchronizationInfo(synchronizationId);
                if (synchronization != null)
                {
                    result.Append(ProcessSynchronization(synchronization.SynchronizationConnectorID, synchronization.SynchronizationTaskID));
                }
            }

            // Log possible error
            if (!String.IsNullOrEmpty(result.ToString()))
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError);
            }
            else
            {
                CurrentInfo = GetString("Tasks.SynchronizationOK");
                AddLog(CurrentInfo);
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // Canceled by user
                CurrentInfo = CanceledString;
                AddLog(CurrentInfo);
            }
            else
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError, result.ToString());
            }
        }
        catch (Exception ex)
        {
            CurrentError = GetString("Tasks.SynchronizationFailed") + ": " + ex.Message;
            AddErrorLog(CurrentError);
        }
        finally
        {
            // Finalize log context
            FinalizeContext();
        }
    }


    /// <summary>
    /// Deletes selected tasks.
    /// </summary>
    protected void DeleteSelected(object parameter)
    {
        ArrayList list = parameter as ArrayList;
        if (list == null)
        {
            return;
        }

        CanceledString = GetString("Tasks.DeletionCanceled");
        eventCode = "DELETESELECTEDTASKS";
        try
        {
            AddLog(GetString("Synchronization.DeletingTasks"));

            list.Sort();

            foreach (string synchronizationIdString in list)
            {
                int synchronizationId = ValidationHelper.GetInteger(synchronizationIdString, 0);
                if (synchronizationId > 0)
                {
                    IntegrationSynchronizationInfo synchronization = IntegrationSynchronizationInfoProvider.GetIntegrationSynchronizationInfo(synchronizationId);

                    if (synchronization != null)
                    {
                        IntegrationTaskInfo task = IntegrationTaskInfoProvider.GetIntegrationTaskInfo(synchronization.SynchronizationTaskID);

                        if (task != null)
                        {
                            DeleteSynchronization(synchronizationId, task.TaskTitle);
                        }
                    }
                }
            }

            CurrentInfo = GetString("Tasks.DeleteOK");
            AddLog(CurrentInfo);
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // Canceled by user
                CurrentInfo = CanceledString;
                AddLog(CurrentInfo);
            }
            else
            {
                CurrentError = GetString("Tasks.DeletionFailed");
                AddErrorLog(CurrentError);
            }
        }
        catch (Exception ex)
        {
            CurrentError = GetString("Tasks.DeletionFailed") + ": " + ex.Message;
            AddErrorLog(CurrentError);
        }
        finally
        {
            // Finalize log context
            FinalizeContext();
        }
    }


    /// <summary>
    /// Deletes all tasks.
    /// </summary>
    protected void DeleteAll(object parameter)
    {
        eventCode = "DELETEALLTASKS";
        CanceledString = GetString("Tasks.DeletionCanceled");
        try
        {
            AddLog(GetString("Synchronization.DeletingTasks"));

            // Get synchronizations
            DataSet ds = IntegrationTaskInfoProvider.GetIntegrationTasksView(gridElem.CompleteWhereCondition, "SynchronizationID ASC", -1, "SynchronizationID, TaskTitle");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // Get synchronization id
                    int synchronizationId = ValidationHelper.GetInteger(row["SynchronizationID"], 0);
                    if (synchronizationId > 0)
                    {
                        string taskTitle = ValidationHelper.GetString(row["TaskTitle"], String.Empty);
                        DeleteSynchronization(synchronizationId, taskTitle);
                    }
                }
            }

            CurrentInfo = GetString("Tasks.DeleteOK");
            AddLog(CurrentInfo);
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // Canceled by user
                CurrentInfo = CanceledString;
                AddLog(CurrentInfo);
            }
            else
            {
                CurrentError = GetString("Tasks.DeletionFailed");
                AddErrorLog(CurrentError);
            }
        }
        catch (Exception ex)
        {
            CurrentError = GetString("Tasks.DeletionFailed") + ": " + ex.Message;
            AddErrorLog(CurrentError);
        }
        finally
        {
            // Finalize log context
            FinalizeContext();
        }
    }

    #endregion


    #region "Button handling"

    protected void btnOk_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.Title");
        titleElem.TitleImage = GetImageUrl("/CMSModules/CMS_Integration/synchronization.png");

        // Run asynchronous action
        switch ((Action)Enum.Parse(typeof(Action), drpAction.SelectedValue))
        {
            case Action.Synchronize:
                if (drpWhat.SelectedIndex == (int)What.AllTasks)
                {
                    RunAsync(SynchronizeAll);
                }
                else if (gridElem.SelectedItems.Count > 0)
                {
                    ctlAsync.Parameter = gridElem.SelectedItems;
                    RunAsync(SynchronizeSelected);
                }
                break;

            case Action.Delete:
                titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Integration/deletion.png");
                if (drpWhat.SelectedIndex == (int)What.AllTasks)
                {
                    RunAsync(DeleteAll);
                }
                else if (gridElem.SelectedItems.Count > 0)
                {
                    ctlAsync.Parameter = gridElem.SelectedItems;
                    RunAsync(DeleteSelected);
                }
                break;

            default:
                lblError.Text = ResHelper.GetString("massaction.selectsomeaction");
                break;
        }
    }

    #endregion


    #region "Async processing"

    protected void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        // Set current log
        ctlAsync.Log = CurrentLog.Log;
    }


    protected void ctlAsync_OnError(object sender, EventArgs e)
    {
        // Handle error
        gridElem.ResetSelection();

        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
        FinalizeContext();
    }


    protected void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        gridElem.ResetSelection();

        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
        FinalizeContext();
    }


    protected void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        CurrentInfo = CanceledString;
        gridElem.ResetSelection();

        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
    }


    /// <summary>
    /// Executes given action asynchronously
    /// </summary>
    /// <param name="action">Action to run</param>
    protected void RunAsync(AsyncAction action)
    {
        pnlLog.Visible = true;
        CurrentLog.Close();
        EnsureLog();
        CurrentError = string.Empty;
        CurrentInfo = string.Empty;
        eventType = EventLogProvider.EVENT_TYPE_INFORMATION;
        pnlContent.Visible = false;

        ctlAsync.RunAsync(action, WindowsIdentity.GetCurrent());
    }

    #endregion


    #region "Log handling"

    /// <summary>
    /// Adds the log information.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected void AddLog(string newLog)
    {
        EnsureLog();
        LogContext.AppendLine(newLog);
        AddEventLog(newLog);
    }


    /// <summary>
    /// Adds the log error.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected void AddErrorLog(string newLog)
    {
        AddErrorLog(newLog, null);
    }


    /// <summary>
    /// Adds the log error.
    /// </summary>
    /// <param name="newLog">New log information</param>
    /// <param name="errorMessage">Error message</param>
    protected void AddErrorLog(string newLog, string errorMessage)
    {
        LogContext.AppendLine(newLog);

        string logMessage = newLog;
        if (errorMessage != null)
        {
            logMessage = errorMessage + "<br />" + logMessage;
        }
        eventType = EventLogProvider.EVENT_TYPE_ERROR;

        AddEventLog(logMessage);
    }


    /// <summary>
    /// Adds message to event log object and updates event type.
    /// </summary>
    /// <param name="logMessage">Message to log</param>
    protected void AddEventLog(string logMessage)
    {
        // Log event to event log
        LogContext.LogEvent(eventType, DateTime.Now, "Integration bus", eventCode, currentUser.UserID, currentUser.UserName, 0,
                            null, HTTPHelper.UserHostAddress, logMessage, currentSiteId,
                            HTTPHelper.GetAbsoluteUri(), HTTPHelper.MachineName, HTTPHelper.GetUrlReferrer(),
                            HTTPHelper.GetUserAgent());
    }


    /// <summary>
    /// Closes log context and causes event log to save.
    /// </summary>
    protected void FinalizeContext()
    {
        // Close current log context
        CurrentLog.Close();
    }


    /// <summary>
    /// Ensures the logging context.
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext log = LogContext.EnsureLog(ctlAsync.ProcessGUID);
        log.LogSingleEvents = false;
        log.Reversed = true;
        log.LineSeparator = "<br />";
        return log;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns client validation script.
    /// </summary>
    private string PrepareScript()
    {
        StringBuilder actionScript = new StringBuilder();
        actionScript.Append("function PerformAction(){{");
        actionScript.Append("   var action = document.getElementById(", ScriptHelper.GetString(drpAction.ClientID), ").value;");
        actionScript.Append("   var whatDrp = document.getElementById(", ScriptHelper.GetString(drpWhat.ClientID), ").value;");
        actionScript.Append("   var label = document.getElementById(", ScriptHelper.GetString(lblInfoBottom.ClientID), ");");
        actionScript.Append("   var selection = !eval(", ScriptHelper.GetString(gridElem.GetCheckSelectionScript()), ");");
        actionScript.Append("   if(!selection && (whatDrp!=", (int)What.AllTasks, ")){{");
        actionScript.Append("       label.innerHTML = ", ScriptHelper.GetString(GetString("synchronization.selecttasks")), ";");
        actionScript.Append("   }}");
        actionScript.Append("   if(action==", (int)Action.Delete, "){{");
        actionScript.Append("       if(whatDrp==", (int)What.AllTasks, "){{");
        actionScript.Append("           return confirm(", ScriptHelper.GetString(GetString("Tasks.ConfirmDeleteAll")), ");");
        actionScript.Append("       }} else if(selection){{");
        actionScript.Append("           return confirm(", ScriptHelper.GetString(GetString("general.confirmdelete")), ");");
        actionScript.Append("       }} else {{");
        actionScript.Append("           return false;");
        actionScript.Append("       }}");
        actionScript.Append("   }} else if(action==", (int)Action.Synchronize, "){{");
        actionScript.Append("       return ((whatDrp==", (int)What.AllTasks, ") || selection)");
        actionScript.Append("   }} else {{");
        actionScript.Append("       label.innerHTML = ", ScriptHelper.GetString(GetString("massaction.selectsomeaction")), ";");
        actionScript.Append("       return false;");
        actionScript.Append("   }}");
        actionScript.Append("}}");

        return actionScript.ToString();
    }

    private string ProcessSynchronization(int connectorId, int taskId)
    {
        if ((taskId > 0) && (connectorId > 0))
        {
            // Get connector and task
            IntegrationConnectorInfo connectorInfo = IntegrationConnectorInfoProvider.GetIntegrationConnectorInfo(connectorId);
            IntegrationTaskInfo taskInfo = IntegrationTaskInfoProvider.GetIntegrationTaskInfo(taskId);
            if ((connectorInfo != null) && (taskInfo != null))
            {
                if (connectorInfo.ConnectorEnabled)
                {
                    // Get connector instance
                    BaseIntegrationConnector connector = IntegrationHelper.GetConnector(connectorInfo.ConnectorName) as BaseIntegrationConnector;
                    if (connector != null)
                    {
                        AddLog(String.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(taskInfo.TaskTitle)));

                        // Process the task
                        if (TasksAreInbound)
                        {
                            // Always try to process the task when requested from UI
                            taskInfo.TaskProcessType = IntegrationProcessTypeEnum.Default;
                            return connector.ProcessExternalTask(taskInfo);
                        }
                        else
                        {
                            return connector.ProcessInternalTask(taskInfo);
                        }
                    }
                    else
                    {
                        // Can't load connector
                        AddLog(String.Format(ResHelper.GetAPIString("synchronization.skippingunavailable", "Skipping '{0}' task - failed to load associated connector."), HTMLHelper.HTMLEncode(taskInfo.TaskTitle)));
                    }
                }
                else
                {
                    // Connector is disabled
                    AddLog(String.Format(ResHelper.GetAPIString("synchronization.skippingdisabled", "Skipping '{0}' task - associated connector is disabled."), HTMLHelper.HTMLEncode(taskInfo.TaskTitle)));
                }
            }
        }

        return null;
    }


    private void DeleteSynchronization(int synchronizationId, string taskTitle)
    {
        AddLog(String.Format(ResHelper.GetAPIString("deletion.running", "Deleting '{0}' task"), HTMLHelper.HTMLEncode(taskTitle)));
        IntegrationSynchronizationInfoProvider.DeleteIntegrationSynchronizationInfo(synchronizationId);
    }

    #endregion
}