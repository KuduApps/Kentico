using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Security.Principal;
using System.ComponentModel;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_UI_System_ServerChecker : CMSUserControl, ICallbackEventHandler
{
    #region "Variables"

    private string mTextBoxControlID = null;
    private string mPagePath = null;
    private AsyncWorker mWorker = null;
    private Guid mCurrentProcessGUID = Guid.Empty;
    protected static Hashtable mResults = new Hashtable();

    #endregion


    #region "Properties"

    /// <summary>
    /// Client ID of textbox from which server name will be aquired.
    /// </summary>
    [DefaultValue(""), TypeConverter(typeof(ControlIDConverter))]
    public string TextBoxControlID
    {
        get
        {
            return mTextBoxControlID;
        }
        set
        {
            mTextBoxControlID = value;
        }
    }


    /// <summary>
    /// Path to page under server which will checked.
    /// </summary>
    public string PagePath
    {
        get
        {
            return mPagePath;
        }
        set
        {
            mPagePath = value;
        }
    }


    /// <summary>
    /// Async worker.
    /// </summary>
    private AsyncWorker Worker
    {
        get
        {
            return mWorker ?? (mWorker = new AsyncWorker());
        }
    }


    /// <summary>
    /// Guid of currently processed process.
    /// </summary>
    private Guid CurrentProcessGUID
    {
        get
        {
            return mCurrentProcessGUID;
        }
        set
        {
            mCurrentProcessGUID = value;
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Load event.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register javascripts
        if (!RequestHelper.IsCallback())
        {
            ScriptHelper.RegisterTooltip(Page);

            TextBox txtServerName = (TextBox)Parent.FindControl(TextBoxControlID);
            if (txtServerName != null)
            {
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ServerChecker_" + ClientID, ScriptHelper.GetScript(
                    "var processGuid_" + ClientID + "='';\n" +
                    "function UpdateStatusLabel_" + ClientID + "(value,context){var args = value.split('|'); if(args[0] == 'true') {var item = document.getElementById('" + lblStatus.ClientID + "'); if(item != null) { item.innerHTML = args[1]; }} else { if(args[1] != '') { processGuid_" + ClientID + "= value; window.setTimeout(\"" + Page.ClientScript.GetCallbackEventReference(this, "processGuid_" + ClientID, "UpdateStatusLabel_" + ClientID, "null") + "\",1000);}}}\n" +
                    "function CheckServer_" + ClientID + "(){ var item = document.getElementById('" + lblStatus.ClientID + "');item.innerHTML = '" + GetString("serverchecker.processing") + "'; var control = document.getElementById('" + txtServerName.ClientID + "'); var value='true|' + control.value;" + Page.ClientScript.GetCallbackEventReference(this, "value", "UpdateStatusLabel_" + ClientID, "null") + ";}"));
            }
        }

        // Initialize controls
        btnCheckServer.ImageUrl = URLHelper.ImagesDirectory + "/Design/Controls/ServerChecker/check.png";
        btnCheckServer.ToolTip = GetString("serverchecker.checkserver");
        btnCheckServer.Attributes.Add("onclick", "CheckServer_" + ClientID + "();return false;");
        btnCheckServer.CssClass = "ServerCheckerIcon";
        lblStatus.CssClass = "ServerCheckerStatus";
    }


    /// <summary>
    /// Check server specified with URL availability.
    /// </summary>
    /// <param name="parameter">Async worker parameter</param>
    protected void CheckServer(object parameter)
    {
        CheckServer(parameter.ToString());
    }


    /// <summary>
    /// Check server specified with URL availability.
    /// </summary>
    /// <param name="parameters">URL to be checked and result identifier</param>
    protected void CheckServer(string parameters)
    {
        string[] param = parameters.Split('|');
        string result = null;

        try
        {
            string url = param[0];

            // Resolve relative URL to full URL
            if (!String.IsNullOrEmpty(url) && url.StartsWith("~/"))
            {
                url = URLHelper.GetAbsoluteUrl(url, URLHelper.GetFullDomain(), null, null);
            }

            // Send HTTP request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ResolveUrl(url));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Process result
            result = GetString("serverchecker.serverstatus");
            if (HttpStatusCode.OK == response.StatusCode)
            {
                // HTTP = 200 - server available
                result += " " + GetString("general.ok");
            }
            else
            {
                // Other status codes
                result += " <span class=\"ServerCheckerStatusLink\" onmouseover=\"Tip('" + ScriptHelper.FormatTooltipString("<b>Response status code: " + response.StatusCode.ToString() + "</b><br /><br />" + response.StatusDescription) + "');\" onmouseout=\"UnTip();\" >" + GetString("general.error") + "</span>";
            }

            response.Close();
        }
        catch (Exception ex)
        {
            // Handling exceptions
            result = GetString("serverchecker.serverstatus") + " <span class=\"ServerCheckerStatusLink\" onmouseover=\"Tip('<b>" + ScriptHelper.FormatTooltipString(ex.Message) + "</b><br /><br />" + ScriptHelper.FormatTooltipString(ex.StackTrace) + "');\" onmouseout=\"UnTip();\" >" + GetString("general.error") + "</span>";
        }
        finally
        {
            SetCurrentResult(param[1], result);
        }
    }


    /// <summary>
    /// Gets result of process with specified ID.
    /// </summary>
    /// <param name="id">ID of process</param>
    /// <returns>Result of process</returns>
    private string GetCurrentResult(string id)
    {
        return ValidationHelper.GetString(mResults["ServerCheckerResult_" + id], string.Empty);
    }


    /// <summary>
    /// Sets result of process with specified ID.
    /// </summary>
    /// <param name="id">ID of process</param>
    /// <param name="value">Value of result</param>
    private void SetCurrentResult(string id, string value)
    {
        mResults["ServerCheckerResult_" + id] = value;
    }

    #endregion


    #region "Callback methods"

    /// <summary>
    /// Prepares the callback result.
    /// </summary>
    public string GetCallbackResult()
    {
        string result = GetCurrentResult(CurrentProcessGUID.ToString());
        // If worker finished return result
        if (!String.IsNullOrEmpty(result))
        {
            mResults.Remove("ServerCheckerResult_" + CurrentProcessGUID.ToString());
            return "true|" + result;
        }
        return "false|" + CurrentProcessGUID.ToString();
    }


    /// <summary>
    /// Raises the callback event.
    /// </summary>
    public void RaiseCallbackEvent(string eventArgument)
    {
        string[] args = eventArgument.Split('|');

        if (args.Length == 2)
        {
            // If not checking callback, run new request
            if (args[0].ToLower() == "true")
            {
                if (Worker.Status == AsyncWorkerStatusEnum.Stopped)
                {
                    CurrentProcessGUID = Guid.NewGuid();
                    if (!String.IsNullOrEmpty(args[1]))
                    {
                        // Prepare URL
                        string parameter = args[1];
                        if (!String.IsNullOrEmpty(PagePath))
                        {
                            parameter = parameter.TrimEnd('/') + "//" + PagePath;
                        }

                        // Call async request to server
                        Worker.Parameter = parameter + "|" + CurrentProcessGUID;
                        Worker.RunAsync(CheckServer, WindowsIdentity.GetCurrent());
                    }
                    else
                    {
                        SetCurrentResult(CurrentProcessGUID.ToString(), GetString("serverchecker.urlnotavailable"));
                    }
                }
            }
            else
            {
                CurrentProcessGUID = new Guid(args[1]);
            }
        }
    }

    #endregion
}