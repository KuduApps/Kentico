using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Linq;
using System.Collections;
using System.Threading;
using System.Security.Principal;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.EventLog;
using CMS.SettingsProvider;
using System.Collections.Generic;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Staging_Tools_Tasks_Tasks : CMSStagingTasksPage
{
    #region "Variables"

    /// <summary>
    /// Message storage for async control
    /// </summary>
    protected static Hashtable mInfos = new Hashtable();

    protected bool allowView = true;

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
    protected string syncSubtree = null;

    string aliasPath = "/";

    int currentSiteId = 0;
    string currentSiteName = null;

    protected CurrentUserInfo currentUser = null;
    protected GeneralConnection mConnection = null;

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

        // Get site info
        currentSiteId = CMSContext.CurrentSiteID;
        currentSiteName = CMSContext.CurrentSiteName;

        // Initialize current user for the async actions
        currentUser = CMSContext.CurrentUser;
        serverId = QueryHelper.GetInteger("serverid", 0);

        if (RequestHelper.CausedPostback(btnSyncComplete))
        {
            SyncComplete();
        }
        else
        {
            if (!RequestHelper.IsCallback())
            {
                int nodeId = QueryHelper.GetInteger("nodeid", 0);

                aliasPath = "/";

                // Get the document node
                if (nodeId > 0)
                {
                    TreeProvider tree = new TreeProvider(currentUser);
                    TreeNode node = tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
                    if (node != null)
                    {
                        aliasPath = node.NodeAliasPath;
                    }
                }

                // Setup title
                titleElem.TitleText = GetString("Synchronization.Title");
                titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Staging/synchronization.png");

                if (!RequestHelper.CausedPostback(btnCurrent, btnSubtree, btnSyncSelected, btnSyncAll))
                {
                    // Check 'Manage servers' permission
                    if (!currentUser.IsAuthorizedPerResource("cms.staging", "ManageDocumentsTasks"))
                    {
                        RedirectToAccessDenied("cms.staging", "ManageDocumentsTasks");
                    }

                    // Register the dialog script
                    ScriptHelper.RegisterDialogScript(this);

                    ltlScript.Text +=
                        ScriptHelper.GetScript("function ConfirmDeleteTask(taskId) { return confirm(" +
                                               ScriptHelper.GetString(GetString("Tasks.ConfirmDelete")) + "); }");
                    ltlScript.Text +=
                        ScriptHelper.GetScript("function CompleteSync(){" +
                                               Page.ClientScript.GetPostBackEventReference(btnSyncComplete, null) + "}");

                    // Initialize grid
                    tasksUniGrid.OnExternalDataBound += tasksUniGrid_OnExternalDataBound;
                    tasksUniGrid.OnAction += tasksUniGrid_OnAction;
                    tasksUniGrid.OnDataReload += tasksUniGrid_OnDataReload;
                    tasksUniGrid.ShowActionsMenu = true;
                    tasksUniGrid.Columns = "TaskID, TaskSiteID, TaskDocumentID, TaskNodeAliasPath, TaskTitle, TaskTime, TaskType, TaskObjectType, TaskObjectID, TaskRunning, (SELECT COUNT(*) FROM Staging_Synchronization WHERE SynchronizationTaskID = TaskID AND SynchronizationErrorMessage IS NOT NULL AND (SynchronizationServerID = @ServerID OR (@ServerID = 0 AND (@TaskSiteID = 0 OR SynchronizationServerID IN (SELECT ServerID FROM Staging_Server WHERE ServerSiteID = @TaskSiteID AND ServerEnabled=1))))) AS FailedCount";
                    TaskInfo ti = new TaskInfo();
                    tasksUniGrid.AllColumns = SqlHelperClass.MergeColumns(ti.ColumnNames.ToArray());

                    // Initialize images
                    viewImage = GetImageUrl("Design/Controls/UniGrid/Actions/View.png");
                    deleteImage = GetImageUrl("Design/Controls/UniGrid/Actions/Delete.png");
                    syncImage = GetImageUrl("Design/Controls/UniGrid/Actions/Synchronize.png");
                    imgCurrent.ImageUrl = GetImageUrl("CMSModules/CMS_Staging/synccurrent.png");
                    imgSubtree.ImageUrl = GetImageUrl("CMSModules/CMS_Staging/syncsubtree.png");

                    // Initialize tooltips
                    syncTooltip = GetString("general.synchronize");
                    deleteTooltip = GetString("general.delete");
                    viewTooltip = GetString("general.view");
                    syncCurrent = GetString("Tasks.SyncCurrent");
                    syncSubtree = GetString("Tasks.SyncSubtree");

                    plcContent.Visible = true;

                    // Initialize buttons
                    btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");
                    btnCancel.Text = GetString("General.Cancel");
                    btnDeleteAll.Text = GetString("Tasks.DeleteAll");
                    btnDeleteSelected.Text = GetString("Tasks.DeleteSelected");
                    btnSyncAll.Text = GetString("Tasks.SyncAll");
                    btnSyncSelected.Text = GetString("Tasks.SyncSelected");
                    btnSyncSelected.OnClientClick = "return !" + tasksUniGrid.GetCheckSelectionScript();
                    btnDeleteAll.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("Tasks.ConfirmDeleteAll")) + ");";
                    btnDeleteSelected.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("general.confirmdelete")) + ");";

                    pnlLog.Visible = false;
                }
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


    #region "Grid events"

    protected void tasksUniGrid_OnAction(string actionName, object actionArgument)
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
                        eventCode = "DELETESELECTEDDOC";
                        AddEventLog(string.Format(ResHelper.GetAPIString("deletion.running", "Deleting '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                        SynchronizationInfoProvider.DeleteSynchronizationInfo(taskId, serverId, currentSiteId);
                        break;

                    case "synchronize":
                        string result = null;
                        try
                        {
                            // Run task synchronization
                            eventCode = "SYNCSELECTEDDOC";
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


    protected object tasksUniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView dr = null;
        switch (sourceName.ToLower())
        {
            case "tasktitle":
                dr = (DataRowView)parameter;
                return GetDocumentLink(dr["TaskDocumentID"], HTMLHelper.HTMLEncode(TextHelper.LimitLength(dr["TaskTitle"].ToString(), 100)), dr["TaskType"]);

            case "tasktime":
                return DateTime.Parse(parameter.ToString()).ToString();

            case "taskresult":
                dr = (DataRowView)parameter;
                return GetResultLink(dr["FailedCount"], dr["TaskID"]);

            case "taskview":
                if (sender is ImageButton)
                {
                    // Add view JavaScript
                    ImageButton viewBtn = (ImageButton)sender;
                    if (allowView)
                    {
                        string taskId = ((DataRowView)((GridViewRow)parameter).DataItem).Row["TaskID"].ToString();
                        string url = ResolveUrl(String.Format("~/CMSModules/Staging/Tools/View.aspx?taskid={0}&tasktype=Documents&hash={1}", taskId, QueryHelper.GetHash("?taskid=" + taskId + "&tasktype=Documents")));
                        viewBtn.OnClientClick = "window.open('" + url + "'); return false;";
                        return viewBtn;
                    }
                    viewBtn.Visible = false;
                    return viewBtn;
                }
                else
                {
                    return string.Empty;
                }
        }
        return parameter.ToString();
    }


    protected DataSet tasksUniGrid_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        // Get the tasks
        DataSet ds = TaskInfoProvider.SelectDocumentTaskList(currentSiteId, serverId, aliasPath, null, currentOrder, 0, columns, currentOffset, currentPageSize, ref totalRecords);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            pnlTasksGrid.Visible = true;
            pnlFooter.Visible = true;
        }
        else
        {
            if (lblInfo.Text == string.Empty)
            {
                lblInfo.Text = GetString("Tasks.NoTasks");
            }
            pnlFooter.Visible = false;
            pnlTasksGrid.Visible = false;
        }
        return ds;
    }

    #endregion


    #region "Grid helper methods"

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
            string logUrl = ResolveUrl(String.Format("~/CMSModules/Staging/Tools/log.aspx?taskid={0}&serverId={1}&tasktype=Documents", taskId, serverId));
            logUrl = URLHelper.AddParameterToUrl(logUrl, "hash", QueryHelper.GetHash(logUrl));
            return "<a target=\"_blank\" href=\"" + logUrl + "\" onclick=\"modalDialog('" + logUrl + "', 'tasklog', 700, 500); return false;\">" + GetString("Tasks.ResultFailed") + "</a>";
        }
        else
        {
            return string.Empty;
        }
    }


    /// <summary>
    /// Returns link for document view.
    /// </summary>
    /// <param name="documentId">Document ID</param>
    /// <param name="taskTitle">Task title</param>
    /// <param name="taskType">Type of the task</param>
    protected string GetDocumentLink(object documentId, object taskTitle, object taskType)
    {
        string title = ValidationHelper.GetString(taskTitle, string.Empty);
        string type = ValidationHelper.GetString(taskType, string.Empty).ToLower();
        int docId = ValidationHelper.GetInteger(documentId, 0);

        if ((type != "deletedoc") && (type != "deleteallculutres"))
        {
            string viewMode = ViewModeCode.LiveSite.ToString();

            // For publish tasks display document in preview mode
            if ((type == "publishdoc") || (type == "archivedoc"))
            {
                viewMode = ViewModeCode.Preview.ToString();
            }

            // Get document url
            string docUrl = ResolveUrl(CMSContext.GetDocumentUrl(docId)) + "?viewmode=" + viewMode;
            return "<a target=\"_blank\" href=\"" + docUrl + "\">" + HTMLHelper.HTMLEncode(title) + "</a>";
        }
        return title;
    }

    #endregion


    #region "Button handling"

    protected void btnSyncAll_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.Title");
        RunAsync(SynchronizeAll);
    }


    private void SyncComplete()
    {
        titleElem.TitleText = GetString("Synchronization.Title");
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Staging/synccurrent.png");
        RunAsync(SynchronizeComplete);
    }


    protected void btnSyncSelected_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.Title");
        ArrayList list = tasksUniGrid.SelectedItems;
        if (list.Count > 0)
        {
            ctlAsync.Parameter = list;
            RunAsync(SynchronizeSelected);
        }
    }


    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.DeletingTasksTitle");
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Staging/deletion.png");
        RunAsync(DeleteAll);
    }


    protected void btnDeleteSelected_Click(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("Synchronization.DeletingTasksTitle");
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Staging/deletion.png");
        if (tasksUniGrid.SelectedItems.Count > 0)
        {
            ctlAsync.Parameter = tasksUniGrid.SelectedItems;
            RunAsync(DeleteSelected);
        }
    }


    protected void btnCurrent_Click(object sender, EventArgs e)
    {
        RunAsync(SynchronizeCurrent);
    }


    protected void btnSubtree_Click(object sender, EventArgs e)
    {
        RunAsync(SynchronizeSubtree);
    }

    #endregion


    #region "Async methods"

    public void SynchronizeComplete(object parameter)
    {
        string result = null;
        eventCode = "SYNCCOMPLETE";
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        try
        {

            int sid = serverId;
            if (sid <= 0)
            {
                sid = SynchronizationInfoProvider.ENABLED_SERVERS;
            }

            AddLog(GetString("Synchronization.LoggingTasks"));

            // Synchronize root node
            IEnumerable<ISynchronizationTask> tasks = DocumentSynchronizationHelper.LogDocumentChange(CMSContext.CurrentSiteName, "/", TaskTypeEnum.UpdateDocument, null, sid, false, false);

            AddLog(GetString("Synchronization.RunningTasks"));

            // Run the synchronization
            foreach (TaskInfo task in tasks.OfType<TaskInfo>())
            {
                AddLog(string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                result += StagingHelper.RunSynchronization(task.TaskID, serverId, true, currentSiteId);
            }

            AddLog(GetString("Synchronization.LoggingTasks"));

            // Synchronize subnodes
            tasks = DocumentSynchronizationHelper.LogDocumentChange(CMSContext.CurrentSiteName, "/%", TaskTypeEnum.UpdateDocument, null, sid, false, false);

            AddLog(GetString("Synchronization.RunningTasks"));

            // Run the synchronization
            foreach (TaskInfo task in tasks.OfType<TaskInfo>())
            {
                AddLog(string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                result += StagingHelper.RunSynchronization(task.TaskID, serverId, true, currentSiteId);
            }

            // Log possible errors
            if (!string.IsNullOrEmpty(result))
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError, null);
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
    /// All items synchronization.
    /// </summary>
    public void SynchronizeAll(object parameter)
    {
        string result = string.Empty;
        eventCode = "SYNCALLDOCS";
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        try
        {
            AddLog(GetString("Synchronization.RunningTasks"));

            // Process all records
            DataSet ds = TaskInfoProvider.SelectDocumentTaskList(currentSiteId, serverId, aliasPath, null, "TaskID", -1, "TaskID, TaskTitle");
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

            // Log possible error
            if (result != string.Empty)
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError, null);
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
    /// Synchronizes document subtree.
    /// </summary>
    /// <param name="parameter"></param>
    protected void SynchronizeSubtree(object parameter)
    {
        string result = string.Empty;
        eventCode = "SYNCSUBTREE";
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        try
        {
            // Synchronize current node first
            result = SynchronizeCurrentInternal(false, "SYNCSUBTREE", false);

            if (result == string.Empty)
            {
                // Synchronize subnodes
                int sid = serverId;
                if (sid <= 0)
                {
                    sid = SynchronizationInfoProvider.ENABLED_SERVERS;
                }

                AddLog(GetString("Synchronization.LoggingTasks"));

                // Get the tasks
                IEnumerable<ISynchronizationTask> tasks = DocumentSynchronizationHelper.LogDocumentChange(currentSiteName, aliasPath.TrimEnd('/') + "/%", TaskTypeEnum.UpdateDocument, null, sid, false, false);

                AddLog(GetString("Synchronization.RunningTasks"));

                // Run the synchronization
                foreach (TaskInfo task in tasks.OfType<TaskInfo>())
                {
                    AddLog(string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                    result += StagingHelper.RunSynchronization(task.TaskID, serverId, true, currentSiteId);
                }
            }

            // Log possible error
            if (result != string.Empty)
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError, null);
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
    /// Synchronizes selected documents.
    /// </summary>
    /// <param name="parameter">ArrayList of document identifiers.</param>
    public void SynchronizeSelected(object parameter)
    {
        if (parameter == null)
        {
            return;
        }

        string result = string.Empty;
        eventCode = "SYNCSELECTEDDOCS";
        ArrayList list = (ArrayList)parameter;
        CanceledString = GetString("Tasks.SynchronizationCanceled");
        try
        {
            AddLog(GetString("Synchronization.RunningTasks"));

            foreach (object taskIdObj in list)
            {
                int taskId = ValidationHelper.GetInteger(taskIdObj, 0);
                if (taskId > 0)
                {
                    TaskInfo task = TaskInfoProvider.GetTaskInfo(taskId);
                    if (task != null)
                    {
                        AddLog(string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                        // Synchronize
                        result += StagingHelper.RunSynchronization(taskId, serverId, true, currentSiteId);
                    }
                }
            }

            // Log possible error
            if (result != string.Empty)
            {
                CurrentError = GetString("Tasks.SynchronizationFailed");
                AddErrorLog(CurrentError, null);
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
    /// Synchronizes the current document.
    /// </summary>
    private void SynchronizeCurrent(object parameter)
    {
        SynchronizeCurrentInternal(parameter, "SYNCCURRENTDOC", true);
    }


    /// <summary>
    /// Internal method for synchronizing current document.
    /// </summary>
    /// <param name="parameter">Parameter</param>
    /// <param name="eventCodeForLog">Event code to set</param>
    /// <param name="finalizeEventLog">Indicates whether to finalize eventlog</param>
    /// <returns>Result of synchronization</returns>
    private string SynchronizeCurrentInternal(object parameter, string eventCodeForLog, bool finalizeEventLog)
    {
        string result = string.Empty;
        eventCode = eventCodeForLog;
        bool finish = ValidationHelper.GetBoolean(parameter, true);
        CanceledString = GetString("Tasks.SynchronizationCanceled");

        int sid = serverId;
        if (sid <= 0)
        {
            sid = SynchronizationInfoProvider.ENABLED_SERVERS;
        }

        AddLog(GetString("Synchronization.LoggingTasks"));

        try
        {
            // Get the tasks
            IEnumerable<ISynchronizationTask> tasks = DocumentSynchronizationHelper.LogDocumentChange(currentSiteName, aliasPath, TaskTypeEnum.UpdateDocument, null, sid, false, false);

            AddLog(GetString("Synchronization.RunningTasks"));

            // Run the synchronization
            foreach (TaskInfo task in tasks.OfType<TaskInfo>())
            {
                AddLog(string.Format(ResHelper.GetAPIString("synchronization.running", "Processing '{0}' task"), HTMLHelper.HTMLEncode(task.TaskTitle)));
                result += StagingHelper.RunSynchronization(task.TaskID, serverId, true, currentSiteId);
            }

            if (finish)
            {
                // Log possible error
                if (result != string.Empty)
                {
                    CurrentError = GetString("Tasks.SynchronizationFailed");
                    AddErrorLog(CurrentError, null);
                }
                else
                {
                    CurrentInfo = GetString("Tasks.SynchronizationOK");
                    AddLog(CurrentInfo);
                }
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
            if (finalizeEventLog)
            {
                // Finalize log context
                FinalizeContext();
            }
        }
        return result;
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

        eventCode = "DELETESELECTEDDOCS";
        ArrayList list = (ArrayList)parameter;
        CanceledString = GetString("Tasks.DeletionCanceled");
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
        eventCode = "DELETEALLDOCS";
        CanceledString = GetString("Tasks.DeletionCanceled");
        try
        {
            AddLog(GetString("Synchronization.DeletingTasks"));
            // Get the tasks
            DataSet ds = TaskInfoProvider.SelectDocumentTaskList(currentSiteId, serverId, aliasPath, null, null, -1, "TaskID, TaskTitle");
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


    #region "Async processing"

    protected void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        // Set current log
        ctlAsync.Log = CurrentLog.Log;
    }


    protected void ctlAsync_OnError(object sender, EventArgs e)
    {
        // Handle error
        tasksUniGrid.ResetSelection();

        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
        FinalizeContext();
    }


    protected void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        tasksUniGrid.ResetSelection();
        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
        FinalizeContext();
    }


    protected void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        CurrentInfo = CanceledString;
        tasksUniGrid.ResetSelection();

        ltlScript.Text += ScriptHelper.GetScript("var __pendingCallbacks = new Array();");

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