using System;
using System.Collections;
using System.Web.UI;
using System.Security.Principal;
using System.Text;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSAdminControls_AsyncControl : CMSUserControl, ICallbackEventHandler
{
    #region "Private variables & constants"

    /// <summary>
    /// Table of the worker processes.
    /// </summary>
    private static readonly Hashtable mWorkers = new Hashtable();

    private AsyncWorker mWorker = null;
    private string mCallbackResult = null;
    private bool mPostbackOnError = true;
    private string mLog = null;

    // Constants
    private const string RESULT_FINISHED = "finished";
    private const string RESULT_RUNNING = "running";
    private const string RESULT_ERROR = "error";
    private const string RESULT_STOPPED = "stopped";
    private const string RESULT_THREADLOST = "threadlost";

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the current log.
    /// </summary>
    public string Log
    {
        get
        {
            if ((mLog == null) && (OnRequestLog != null))
            {
                OnRequestLog(this, new EventArgs());
            }

            return mLog;
        }
        set
        {
            mLog = value;
        }
    }


    /// <summary>
    /// Process GUID.
    /// </summary>
    public Guid ProcessGUID
    {
        get
        {
            if (ViewState["ProcessGUID"] == null)
            {
                ViewState["ProcessGUID"] = Guid.NewGuid();
            }
            return ValidationHelper.GetGuid(ViewState["ProcessGUID"], Guid.NewGuid());
        }
        set
        {
            ViewState["ProcessGUID"] = value;
        }
    }


    /// <summary>
    /// Asynchronous worker.
    /// </summary>
    public AsyncWorker Worker
    {
        get
        {
            if (mWorker == null)
            {
                string key = "AsyncWorker_" + ProcessGUID;
                mWorker = (AsyncWorker)mWorkers[key];
                if (mWorker == null)
                {
                    mWorker = new AsyncWorker
                                  {
                                      // Ensure process guid
                                      ProcessGUID = ProcessGUID
                                  };
                    mWorkers[key] = mWorker;
                }
            }

            return mWorker;
        }
    }


    /// <summary>
    /// True if the postback should occure after error.
    /// </summary>
    public bool PostbackOnError
    {
        get
        {
            return mPostbackOnError;
        }
        set
        {
            mPostbackOnError = value;
        }
    }


    /// <summary>
    /// Process parameter.
    /// </summary>
    public new object Parameter
    {
        get
        {
            return Worker.Parameter;
        }
        set
        {
            Worker.Parameter = value;
        }
    }


    /// <summary>
    /// Indicates if the logging is ascendant.
    /// </summary>
    public bool AscendantLog
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if the control should use the string from resource file.
    /// </summary>
    public bool UseFileStrings
    {
        get;
        set;
    }


    /// <summary>
    /// Gets the worker status.
    /// </summary>
    public AsyncWorkerStatusEnum Status
    {
        get
        {
            return Worker.Status;
        }
    }


    /// <summary>
    /// Maximum log length. (0 = unlimited)
    /// </summary>
    public int MaxLogLines
    {
        get;
        set;
    }

    #endregion


    #region "Page events"

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Hide buttons
        btnFinished.Attributes.Add("style", "display: none;");
        btnError.Attributes.Add("style", "display: none;");
        btnCancel.Attributes.Add("style", "display: none;");

        // Register full postbacks
        ControlsHelper.RegisterPostbackControl(btnFinished);
        ControlsHelper.RegisterPostbackControl(btnError);
        ControlsHelper.RegisterPostbackControl(btnCancel);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        RenderScripts(false);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns localized string.
    /// </summary>
    /// <param name="stringName">String to localize</param>
    public override string GetString(string stringName)
    {
        if (UseFileStrings)
        {
            return ResHelper.GetFileString(stringName);
        }
        else
        {
            return base.GetString(stringName);
        }
    }


    /// <summary>
    /// Returns true if the worker for current control exists.
    /// </summary>
    protected bool WorkerExists()
    {
        if (ProcessGUID != Guid.Empty)
        {
            string key = "AsyncWorker_" + ProcessGUID;
            return (mWorkers[key] != null);
        }
        return false;
    }


    /// <summary>
    /// Runs the asynchronous process without any thread.
    /// </summary>
    public void RunAsync()
    {
        RenderScripts(false);
    }


    /// <summary>
    /// Runs the asynchronous action.
    /// </summary>
    /// <param name="action">Action to run</param>
    /// <param name="wi">Windows identity (windows user)</param>
    public void RunAsync(AsyncAction action, WindowsIdentity wi)
    {
        RenderScripts(true);

        Worker.Stop();
        Worker.Reset();

        Worker.RunAsync(action, wi);
    }


    /// <summary>
    /// Stops the worker.
    /// </summary>
    public void Stop()
    {
        Worker.Stop();
    }


    /// <summary>
    /// Registers scripts necessary.
    /// </summary>
    /// <param name="force">Determines whether to register scrips in any case</param>
    protected void RenderScripts(bool force)
    {
        if (!RequestHelper.IsCallback() || force)
        {
            if (((Worker.Status == AsyncWorkerStatusEnum.Running) || (Worker.Status == AsyncWorkerStatusEnum.WaitForFinish)) || force)
            {
                string machineName = SqlHelperClass.MachineName;
                const int TIMEOUT = 200;

                // Prepare the scripts
                StringBuilder script = new StringBuilder();

                // Initialize variables
                script.Append(
@"
var logText_", ClientID, @" = '';
var callBackParam_", ClientID, @" = '';
var machineName_", ClientID, " = '", machineName.ToLower(), @"';
var asyncProcessFinished_", ClientID, @" = false;
var timeout_", ClientID, @" = null;
var asyncBusy = false;");
                // Register function that repeatedly gets content
                script.Append(@"
function AC_GetAsyncStatus_", ClientID, @"() {
    if (!asyncBusy) {
        asyncBusy = true; 
        setTimeout('asyncBusy = false;', 2000);
        callBackParam_", ClientID, " = logText_", ClientID, ".length + '|' + machineName_", ClientID, @";
        ", Page.ClientScript.GetCallbackEventReference(this, "callBackParam_" + ClientID, "AC_ReceiveAsyncStatus_" + ClientID, "logText_" + ClientID + ".length", false), @";
    }
    if (asyncProcessFinished_", ClientID, @") {
        CancelAction_", ClientID, @"(false);
        return;
    }
    else
    {
        timeout = setTimeout(function() { AC_GetAsyncStatus_", ClientID, "(); }, ", TIMEOUT, @");
    }
}");
                // Register closing script
                script.Append(@"
function AC_SetClose_", ClientID, @"() {
    var cancelElem = document.getElementById('", btnCancel.ClientID, @"');
    if (cancelElem != null) {
        cancelElem.value = '", GetString("General.Close"), @"';
    }
}");
                // Register function that parses callback
                script.Append(@"        
function AC_ReceiveAsyncStatus_", ClientID, @"(rvalue, context) {
    asyncBusy = false;
    if (asyncProcessFinished_", ClientID, @") {
        return;
    }
    values = rvalue.split('|');
    code = values[0];
    var i = 1;
    var resultValue = '';
    for (i = 1; i<values.length; i++) {
        resultValue += values[i];
    }
    if (resultValue != '')
    {
        AC_SetLog_", ClientID, @"(resultValue, context);
    }
    if ( code == 'running') { 
    }
    else if (code == '", RESULT_FINISHED, @"') {
        asyncProcessFinished_", ClientID, @" = true;
        ", Page.ClientScript.GetPostBackEventReference(btnFinished, null), @";
    }
    else if ((code == '", RESULT_THREADLOST, @"') || (code == '", RESULT_STOPPED, @"')) {
        asyncProcessFinished_", ClientID, @" = true;
        AC_SetClose_", ClientID, @"();
    }
    else if (code == '", RESULT_ERROR, @"') {
        asyncProcessFinished_", ClientID, @" = true;
        ", (PostbackOnError ? (Page.ClientScript.GetPostBackEventReference(btnError, null) + ";") : "AC_SetClose_" + ClientID + "();"), @"
    }
}");
                // Register function that displays content
                script.Append(@"
function AC_SetLog_", ClientID, @"(text, length) {
    var elem = document.getElementById('", pnlAsync.ClientID, @"');
    var messageText = logText_", ClientID, @";");

                if (MaxLogLines == 0)
                {
                    script.Append(@"
    messageText = ", (AscendantLog ? "messageText.substring(0, length) + text;" : "text + messageText.substring(messageText.length - length);"), @"
    if (messageText.length > logText_", ClientID, @".length) {
        logText_", ClientID, @" = elem.innerHTML = messageText;
    }
}");
                }
                else
                {
                    script.Append(@"  
    elem.innerHTML = text;
}");
                }
                // Set timeout for getting content
                script.Append(@"            
timeout_", ClientID, " = setTimeout(function() {AC_GetAsyncStatus_", ClientID, "();}, ", TIMEOUT, ");");

                ScriptHelper.RegisterStartupScript(this, typeof(string), "asyncScript" + ClientID, ScriptHelper.GetScript(script.ToString()));
            }

            // Register cancel script
            StringBuilder cancelScript = new StringBuilder();
            cancelScript.Append(
@"function CancelAction_", ClientID, @"(withPostback) {
    asyncProcessFinished_", ClientID, @" = true;
    if (withPostback) {
        ", Page.ClientScript.GetPostBackEventReference(btnCancel, null), @";
    }
    else
    {
        var t = timeout_", ClientID, @";
        if((t != 'undefined') && (t != null)) {
            clearTimeout(timeout_", ClientID, @");
        }
    }
}");
            ScriptHelper.RegisterStartupScript(this, typeof(string), "cancelScript" + ClientID, ScriptHelper.GetScript(cancelScript.ToString()));
        }
    }


    public string GetCancelScript(bool withPostback)
    {
        return "CancelAction_" + ClientID + "(" + withPostback.ToString().ToLower() + ");";
    }

    #endregion


    #region "Callback handling"

    /// <summary>
    /// Raises the callback event.
    /// </summary>
    /// <param name="eventArgument">Event argument</param>
    public void RaiseCallbackEvent(string eventArgument)
    {
        string[] args = eventArgument.Split('|');
        int requestedLength = ValidationHelper.GetInteger(args[0], 0);
        mCallbackResult = string.Empty;

        if (SqlHelperClass.MachineName.ToLower() == args[1])
        {
            if (WorkerExists())
            {
                switch (Worker.Status)
                {
                    case AsyncWorkerStatusEnum.Finished:
                    case AsyncWorkerStatusEnum.WaitForFinish:
                        // Allow worker to finish
                        mCallbackResult = RESULT_FINISHED;
                        break;

                    case AsyncWorkerStatusEnum.Running:
                        mCallbackResult = RESULT_RUNNING;
                        break;

                    case AsyncWorkerStatusEnum.Error:
                        // Allow worker to finish
                        mCallbackResult = RESULT_ERROR;
                        break;

                    case AsyncWorkerStatusEnum.Stopped:
                        mCallbackResult = RESULT_STOPPED;
                        break;
                }

                string log = Log;
                if (!string.IsNullOrEmpty(log))
                {
                    int logLength = log.Length;
                    int trimStart = 0;
                    int trimLength = 0;

                    if (MaxLogLines > 0)
                    {
                        // Get position of the specified occurrence of new line tag
                        int index = log.NthIndexOf("<br />", MaxLogLines, !AscendantLog);
                        bool indexExists = (index > -1);
                        if (AscendantLog)
                        {
                            // Select max. number of lines from the end of the log
                            trimStart = indexExists ? index : 0;
                            trimLength = indexExists ? (logLength - index) : logLength;
                        }
                        else
                        {
                            // Select max. number of lines from the begining of the log
                            trimStart = 0;
                            trimLength = indexExists ? index : logLength;
                        }
                    }
                    else
                    {
                        // Correct the length in case of invalid value
                        if (requestedLength > logLength)
                        {
                            requestedLength = logLength;
                        }
                        // Send only the part that is not present on the client machine
                        trimStart = AscendantLog ? requestedLength : 0;
                        trimLength = (logLength - requestedLength);
                    }

                    // Get the message within the specified bounds
                    log = log.Substring(trimStart, trimLength);
                }
                // Send the message to client
                mCallbackResult += "|" + log;
            }
            else
            {
                mCallbackResult = RESULT_THREADLOST + "|" + GetString("AsyncControl.ThreadLost");
            }
        }
        else
        {
            mCallbackResult = RESULT_RUNNING + "|";
        }
    }


    /// <summary>
    /// Returns the result of a callback.
    /// </summary>
    public string GetCallbackResult()
    {
        return mCallbackResult;
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Finished event handler.
    /// </summary>
    public event EventHandler OnFinished;


    /// <summary>
    /// Error event handler.
    /// </summary>
    public new event EventHandler OnError;


    /// <summary>
    /// Cancel event handler.
    /// </summary>
    public event EventHandler OnCancel;


    /// <summary>
    /// Error event handler.
    /// </summary>
    public event EventHandler OnRequestLog;


    /// <summary>
    /// Raises the finished event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    public void RaiseFinished(object sender, EventArgs e)
    {
        if (OnFinished != null)
        {
            OnFinished(this, e);
        }
    }


    /// <summary>
    /// Raises the Error event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    public void RaiseError(object sender, EventArgs e)
    {
        if (OnError != null)
        {
            OnError(this, e);
        }
    }


    /// <summary>
    /// Raises the Cancel event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event args</param>
    public void RaiseCancel(object sender, EventArgs e)
    {
        if (OnCancel != null)
        {
            OnCancel(this, e);
        }
    }


    protected void btnFinished_Click(object sender, EventArgs e)
    {
        RaiseFinished(this, e);
    }


    protected void btnError_Click(object sender, EventArgs e)
    {
        RaiseError(this, e);
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (WorkerExists())
        {
            if (Worker.Status == AsyncWorkerStatusEnum.Running)
            {
                // Stop worker
                Worker.Stop();
            }
        }

        RaiseCancel(this, e);
    }

    #endregion
}
