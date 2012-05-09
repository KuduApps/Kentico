using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Threading;
using System.Security.Principal;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.EventLog;

public partial class CMSModules_Staging_Tools_AllTasks_Tasks : CMSStagingAllTasksPage
{
    #region "Protected variables"

    /// <summary>
    /// Message storage for async control
    /// </summary>
    protected static Hashtable mInfos = new Hashtable();

    private int serverId = 0;
    private string eventCode = null;
    private string eventType = null;

    protected string viewImage = string.Empty;
    protected string deleteImage = string.Empty;
    protected string syncImage = string.Empty;

    protected string viewTooltip = string.Empty;
    protected string deleteTooltip = string.Empty;
    protected string syncTooltip = string.Empty;

    protected string syncCurrent = null;
    protected CurrentUserInfo currentUser = null;
    protected GeneralConnection mConnection = null;

    protected string objectType = string.Empty;
    protected int siteId = 0;

    protected int currentSiteId = 0;
    protected string currentSiteName = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Connection.
    /// </summary>
    public GeneralConnection Connection
    {
        get
        {
            return mConnection ?? (mConnection = ConnectionHelper.GetConnection());
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
        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(Page);

        // Initialize current user for the async actions
        currentUser = CMSContext.CurrentUser;

        if (!RequestHelper.IsCallback())
        {
            // Check 'Manage object tasks' permission
            if (!currentUser.IsAuthorizedPerResource("cms.staging", "ManageAllTasks"))
            {
                RedirectToAccessDenied("cms.staging", "ManageAllTasks");
            }

            currentSiteId = CMSContext.CurrentSiteID;
            currentSiteName = CMSContext.CurrentSiteName;

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this);
            serverId = QueryHelper.GetInteger("serverid", 0);

            // Get the selected types
            ObjectTypeTreeNode selectedNode = TaskInfoProvider.ObjectTree.FindNode(objectType, (siteId > 0));
            objectType = (selectedNode != null) ? selectedNode.GetObjectTypes(true) : string.Empty;

            // Setup title
            titleElem.TitleImage = GetImageUrl("/CMSModules/CMS_Staging/synchronization.png");

            if (!RequestHelper.CausedPostback(btnSyncSelected, btnSyncAll))
            {
                // Initialize images
                viewImage = GetImageUrl("Design/Controls/UniGrid/Actions/View.png");
                deleteImage = GetImageUrl("Design/Controls/UniGrid/Actions/Delete.png");
                syncImage = GetImageUrl("Design/Controls/UniGrid/Actions/Synchronize.png");

                // Initialize tooltips
                syncTooltip = GetString("general.synchronize");
                deleteTooltip = GetString("general.delete");
                viewTooltip = GetString("general.view");
                syncCurrent = GetString("ObjectTasks.SyncCurrent");

                plcContent.Visible = true;

                // Initialize buttons
                btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");
                btnCancel.Text = GetString("General.Cancel");
                btnDeleteAll.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("Tasks.ConfirmDeleteAll")) + ");";
                btnDeleteSelected.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("general.confirmdelete")) + ");";
                btnSyncSelected.OnClientClick = "return !" + gridTasks.GetCheckSelectionScript();

                // Initialize grid
                gridTasks.ZeroRowsText = GetString("Tasks.NoTasks");
                gridTasks.OnAction += gridTasks_OnAction;
                gridTasks.OnDataReload += gridTasks_OnDataReload;
                gridTasks.OnExternalDataBound += gridTasks_OnExternalDataBound;
                gridTasks.ShowActionsMenu = true;
                gridTasks.Columns = "TaskID, TaskSiteID, TaskDocumentID, TaskNodeAliasPath, TaskTitle, TaskTime, TaskType, TaskObjectType, TaskObjectID, TaskRunning, (SELECT COUNT(*) FROM Staging_Synchronization WHERE SynchronizationTaskID = TaskID AND SynchronizationErrorMessage IS NOT NULL AND (SynchronizationServerID = @ServerID OR (@ServerID = 0 AND (@TaskSiteID = 0 OR SynchronizationServerID IN (SELECT ServerID FROM Staging_Server WHERE ServerSiteID = @TaskSiteID AND ServerEnabled=1))))) AS FailedCount";
                TaskInfo ti = new TaskInfo();
                gridTasks.AllColumns = SqlHelperClass.MergeColumns(ti.ColumnNames.ToArray());

                pnlLog.Visible = false;
            }
        }

        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Hide labels if empty
        lblError.Visible = (lblError.Text != string.Empty);
        lblInfo.Visible = (lblInfo.Text != string.Empty);
        base.OnPreRender(e);
    }

    #endregion


    #region "Grid events & methods"

    protected void gridTasks_OnAction(string actionName, object actionArgument)
    {
        // Parse action argument
        int taskId = ValidationHelper.GetInteger(actionArgument, 0);
        eventType = EventLogProvider.EVENT_TYPE_INFORMATION;

        if (taskId > 0)
        {
            TaskInfo task = TaskInfoProvider.GetTaskInfo(taskId);

            if (task != null)
            {
                switch (actionName.ToLower())
                {
                    case "delete":
                        // Delete task
                        eventCode = "DELETESELECTEDTASK";
                        AddEventLog(string.Format(ResHelper.GetAPIString("deletion.running", "Deleting '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                        SynchronizationInfoProvider.DeleteSynchronizationInfo(taskId, serverId, currentSiteId);
                        break;

                    case "synchronize":
                        string result = null;
                        try
                        {
                            // Run task synchronization
                            eventCode = "SYNCSELECTEDTASK";
                            result = StagingHelper.RunSynchronization(taskId, serverId, true, currentSiteId);

                            if (string.IsNullOrEmpty(result))
                            {
                                lblInfo.Text = GetString("Tasks.SynchronizationOK");
                            }
                            else
                            {
                                lblError.Text = GetString("Tasks.SynchronizationFailed");
                                eventType = EventLogProvider.EVENT_TYPE_ERROR;
                            }
                        }
                        catch (Exception ex)
                        {
                            result = ex.Message;
                            lblError.Text = GetString("Tasks.SynchronizationFailed");
                            eventType = EventLogProvider.EVENT_TYPE_ERROR;
                        }
                        // Log message
                        AddEventLog(result + string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                        break;
                }
            }
        }
    }


    protected object gridTasks_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView drv = null;
        int taskId = 0;
        switch (sourceName.ToLower())
        {
            case "taskresult":
                drv = parameter as DataRowView;
                int failedCount = ValidationHelper.GetInteger(drv["FailedCount"], 0);
                taskId = ValidationHelper.GetInteger(drv["TaskID"], 0);
                return GetResultLink(failedCount, taskId);

            case "view":
                if (sender is ImageButton)
                {
                    // Add view JavaScript
                    ImageButton btnView = (ImageButton)sender;
                    drv = UniGridFunctions.GetDataRowView((DataControlFieldCell)btnView.Parent);
                    taskId = ValidationHelper.GetInteger(drv["TaskID"], 0);
                    string url = ResolveUrl(String.Format("~/CMSModules/Staging/Tools/View.aspx?taskid={0}&tasktype=All&hash={1}", taskId, QueryHelper.GetHash("?taskid=" + taskId + "&tasktype=All"))); ;
                    btnView.OnClientClick = "window.open('" + url + "');return false;";
                    return btnView;
                }
                else
                {
                    return string.Empty;
                }

            case "tasktime":
                return DateTime.Parse(parameter.ToString()).ToString();
        }
        return parameter;
    }


    protected DataSet gridTasks_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        // Get the tasks
        DataSet ds = TaskInfoProvider.SelectTaskList(currentSiteId, serverId, completeWhere, currentOrder, 0, columns, currentOffset, currentPageSize, ref totalRecords);
        pnlFooter.Visible = (totalRecords > 0);
        return ds;
    }


    /// <summary>
    /// Returns the result link for the synchronization log.
    /// </summary>
    /// <param name="failedCount">Failed items count</param>
    /// <param name="taskId">Task ID</param>
    protected string GetResultLink(object failedCount, object taskId)
    {
        int count = ValidationHelper.GetInteger(failedCount, 0);
        if (count > 0)
        {
            string logUrl = ResolveUrl(String.Format("~/CMSModules/Staging/Tools/log.aspx?taskid={0}&serverId={1}&tasktype=All", taskId, serverId));
            logUrl = URLHelper.AddParameterToUrl(logUrl, "hash", QueryHelper.GetHash(logUrl));
            return "<a target=\"_blank\" href=\"" + logUrl + "\" onclick=\"modalDialog('" + logUrl + "', 'tasklog', 700, 500); return false;\">" + GetString("Tasks.ResultFailed") + "</a>";
        }
        else
        {
            return string.Empty;
        }
    }

    #endregion


    #region "Async methods"

    /// <summary>
    /// All items synchronization.
    /// </summary>
    protected void SynchronizeAll(object parameter)
    {
        string result = string.Empty;
        eventCode = "SYNCALLTASKS";
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        try
        {
            AddLog(GetString("Synchronization.RunningTasks"));

            // Get the tasks
            DataSet ds = TaskInfoProvider.SelectTaskList(currentSiteId, serverId, null, "TaskID", -1, "TaskID, TaskTitle");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string taskTitle = ValidationHelper.GetString(row["TaskTitle"], string.Empty);
                    AddLog(string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(taskTitle)));

                    // Get ID
                    int taskId = ValidationHelper.GetInteger(row["TaskID"], 0);
                    if (taskId > 0)
                    {
                        result += StagingHelper.RunSynchronization(taskId, serverId, true, currentSiteId);
                    }
                }
            }

            // Log possible errors
            if (result != string.Empty)
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
                AddErrorLog(CurrentError, result);
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
        if (parameter == null)
        {
            return;
        }

        string result = string.Empty;
        eventCode = "SYNCSELECTEDTASKS";
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        ArrayList list = (ArrayList)parameter;
        try
        {
            AddLog(GetString("Synchronization.RunningTasks"));

            foreach (string taskIdString in list)
            {
                int taskId = ValidationHelper.GetInteger(taskIdString, 0);
                if (taskId > 0)
                {
                    // Synchronize the task
                    TaskInfo task = TaskInfoProvider.GetTaskInfo(taskId);
                    if (task != null)
                    {
                        AddLog(string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                        result += StagingHelper.RunSynchronization(taskId, serverId, true, currentSiteId);
                    }
                }
            }

            // Log possible error
            if (result != string.Empty)
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
                AddErrorLog(CurrentError, result);
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
        if (parameter == null)
        {
            return;
        }

        eventCode = "DELETESELECTEDTASKS";
        CanceledString = GetString("Tasks.DeletionCanceled");
        ArrayList list = (ArrayList)parameter;
        try
        {
            AddLog(GetString("Synchronization.DeletingTasks"));

            foreach (string taskIdString in list)
            {
                int taskId = ValidationHelper.GetInteger(taskIdString, 0);
                if (taskId > 0)
                {
                    TaskInfo task = TaskInfoProvider.GetTaskInfo(taskId);

                    if (task != null)
                    {
                        AddLog(string.Format(ResHelper.GetAPIString("deletion.running", "Deleting '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                        // Delete synchronization
                        SynchronizationInfoProvider.DeleteSynchronizationInfo(task, serverId, currentSiteId);
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
            // Get the tasks
            DataSet ds = TaskInfoProvider.SelectTaskList(currentSiteId, serverId, null, "TaskID", -1, "TaskID, TaskTitle");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int taskId = ValidationHelper.GetInteger(row["TaskID"], 0);
                    if (taskId > 0)
                    {
                        string taskTitle = ValidationHelper.GetString(row["TaskTitle"], null);
                        AddLog(string.Format(ResHelper.GetAPIString("deletion.running", "Deleting '{0}' task"), HTMLHelper.HTMLEncode(taskTitle)));
                        // Delete synchronization
                        SynchronizationInfoProvider.DeleteSynchronizationInfo(taskId, serverId, currentSiteId);
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

    protected void btnSyncSelected_Click(object sender, EventArgs e)
    {
        if (gridTasks.SelectedItems.Count > 0)
        {
            titleElem.TitleText = GetString("Synchronization.Title");
            ctlAsync.Parameter = gridTasks.SelectedItems;
            // Run asynchronous action
            RunAsync(SynchronizeSelected);
        }
    }

    protected void btnSyncAll_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.Title");
        CurrentInfo = GetString("Tasks.SynchronizationCanceled");
        // Run asynchronous action
        RunAsync(SynchronizeAll);
    }


    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.DeletingTasksTitle");
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Staging/deletion.png");
        // Run asynchronous action
        RunAsync(DeleteAll);
    }


    protected void btnDeleteSelected_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.DeletingTasksTitle");
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Staging/deletion.png");
        if (gridTasks.SelectedItems.Count > 0)
        {
            ctlAsync.Parameter = gridTasks.SelectedItems;
            // Run asynchronous action
            RunAsync(DeleteSelected);
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
        gridTasks.ResetSelection();

        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
        FinalizeContext();
    }


    protected void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        gridTasks.ResetSelection();

        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
        FinalizeContext();
    }


    protected void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        CurrentInfo = CanceledString;
        gridTasks.ResetSelection();

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
        plcContent.Visible = false;

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
        LogContext.LogEvent(eventType, DateTime.Now, "Staging", eventCode, currentUser.UserID, currentUser.UserName, 0,
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
}