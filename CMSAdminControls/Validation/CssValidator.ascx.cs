using System;
using System.Net;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Security.Principal;
using System.Threading;
using System.Web;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.IO;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.ExtendedControls;

public partial class CMSAdminControls_Validation_CssValidator : DocumentValidator
{
    #region "Constants"

    private const string DEFAULT_VALIDATOR_URL = "http://jigsaw.w3.org/css-validator/validator";
    private const string VALIDATOR_PROFILE = "css21";
    private const int VALIDATION_DELAY = 300;

    #endregion


    #region "Variables"

    private string mValidatorURL = null;
    private string mValidatorProfile = null;
    private int mValidationDelay = 0;
    private bool mUseServerRequest = false;

    private Regex mInlineStylesRegex = null;
    private Regex mLinkedStylesRegex = null;
    private CurrentUserInfo currentUser = null;
    private SiteInfo currentSite = null;
    private string currentCulture = CultureHelper.DefaultUICulture;
    private static DataSet mDataSource = null;
    private static readonly Hashtable mPostProcessingRequired = new Hashtable();
    private static readonly Hashtable mDataSources = new Hashtable();
    private static readonly Hashtable mErrors = new Hashtable();
    private string mExcludedCSS = ";designmode.css;skin.css;";

    #endregion


    #region "Properties"

    /// <summary>
    /// URL to which validator requests will be sent
    /// </summary>
    public string ValidatorURL
    {
        get
        {
            if (mValidatorURL == null)
            {
                mValidatorURL = DataHelper.GetNotEmpty(SettingsKeyProvider.GetStringValue("CMSValidationCSSValidatorURL"), DEFAULT_VALIDATOR_URL);
            }
            return mValidatorURL;
        }
        set
        {
            mValidatorURL = value;
        }
    }


    /// <summary>
    /// Current log context
    /// </summary>
    public LogContext CurrentLog
    {
        get
        {
            return EnsureLog();
        }
    }


    /// <summary>
    /// Indicates if control is used on live site
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
            gridValidationResult.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates if server request  will be used rather than javascript request to obtain HTML
    /// </summary>
    public bool UseServerRequestType
    {
        get
        {
            return mUseServerRequest;
        }
        set
        {
            mUseServerRequest = value;
        }
    }


    /// <summary>
    /// Gets or sets source of the data for unigrid control
    /// </summary>
    public override DataSet DataSource
    {
        get
        {
            if (mDataSource == null)
            {
                mDataSource = base.DataSource;
                if (mDataSource == null)
                {
                    mDataSource = mDataSources[ctlAsync.ProcessGUID] as DataSet;

                }
            }
            base.DataSource = mDataSource;

            return mDataSource;
        }
        set
        {
            mDataSource = value;
            mDataSources[ctlAsync.ProcessGUID] = mDataSource;
            base.DataSource = mDataSource;

        }
    }


    /// <summary>
    /// Current Error
    /// </summary>
    private string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mErrors["LinkChecker_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mErrors["LinkChecker_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Regular expression to get inline css styles
    /// </summary>
    private Regex InlineStylesRegex
    {
        get
        {
            if (mInlineStylesRegex == null)
            {
                mInlineStylesRegex = RegexHelper.GetRegex("<style[^>]*>(?<comment><!--)?(?<css>[^<]*)(?(comment)-->)</style>", RegexOptions.Singleline);
            }
            return mInlineStylesRegex;
        }
    }


    /// <summary>
    /// Regular expression to get linked css styles
    /// </summary>
    private Regex LinkedStylesRegex
    {
        get
        {
            if (mLinkedStylesRegex == null)
            {
                mLinkedStylesRegex = RegexHelper.GetRegex("(?<link><link)?(?(link)[^>]*(?<type>type\\s*=\\s*(?<qc1>[\"']?)text/css(?(qc1)\\k<qc1>))?[^>]*href\\s*=\\s*(?<qc2>[\"'])|@import\\s*url\\s*(?<bracket>\\()?(?<qc3>[\"'])?(?=([^<])*</style))(?<url>(?(link)[^\"'>\\s]*|[^\"']*))(?(link)(?(qc2)\\k<qc2>)[^>]*(?(type)|(\\s*type\\s*=\\s*(?<qc1>[\"']?)text/css(?(qc1)\\k<qc1>))))", RegexOptions.Singleline);
            }
            return mLinkedStylesRegex;
        }
    }


    /// <summary>
    /// Key to store validation result
    /// </summary>
    protected override string ResultKey
    {
        get
        {
            return "validation|css|" + CMSContext.PreferredCultureCode + "|" + Url;
        }
    }


    /// <summary>
    /// Indicates which CSS profile should be used for validation
    /// </summary>
    private string ValidatorProfile
    {
        get
        {
            return mValidatorProfile;
        }
    }


    /// <summary>
    /// Delay between validation requests to server
    /// </summary>
    private int ValidationDelay
    {
        get
        {
            if (mValidationDelay == 0)
            {
                mValidationDelay = ValidationHelper.GetInteger(SettingsKeyProvider.GetIntValue("CMSValidationCSSValidatorDelay"), VALIDATION_DELAY);
            }
            return mValidationDelay;
        }
    }


    /// <summary>
    /// Indicates if data post processing required
    /// </summary>
    private bool DataPostProcessing
    {
        get
        {
            return ValidationHelper.GetBoolean(mPostProcessingRequired[ctlAsync.ProcessGUID], false);
        }
        set
        {
            mPostProcessingRequired[ctlAsync.ProcessGUID] = value;
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Page load 
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            DataSource = null;
        }


        // Configure controls
        SetupControls();

        if (RequestHelper.IsPostBack())
        {
            // Fill the grid data source
            if (!DataHelper.DataSourceIsEmpty(DataSource))
            {
                gridValidationResult.DataSource = DataSource;
            }

            ProcessResult(DataSource);
        }
    }


    /// <summary>
    /// Page load 
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!RequestHelper.IsCallback() && RequestHelper.IsPostBack() && DataPostProcessing)
        {
            DocumentValidationHelper.PostProcessValidationData(DataSource, DocumentValidationEnum.CSS, null);
            DataPostProcessing = false;

            // Fill the grid data source
            if (!DataHelper.DataSourceIsEmpty(DataSource))
            {
                gridValidationResult.DataSource = DataSource;
                gridValidationResult.ReloadData();
            }

            ProcessResult(DataSource);
        }

        // Additional settings
        if (DataHelper.DataSourceIsEmpty(DataSource))
        {
            lnkExportToExcel.Enabled = lnkNewWindow.Enabled = false;
            lnkExportToExcel.CssClass = lnkNewWindow.CssClass = "MenuItemEditDisabled";
            lnkNewWindow.OnClientClick = lnkExportToExcel.OnClientClick = null;
        }
        else
        {
            lnkNewWindow.OnClientClick = String.Format("modalDialog('" + ResolveUrl("~/CMSModules/Content/CMSDesk/Validation/ValidationResults.aspx") + "?datakey={0}&docid={1}&hash={2}', 'ViewValidationResult', 800, 600);return false;", ResultKey, Node.DocumentID, QueryHelper.GetHash(String.Format("?datakey={0}&docid={1}", ResultKey, Node.DocumentID)));
        }
    }

    /// <summary>
    /// Initializes all nested controls.
    /// </summary>
    private void SetupControls()
    {
        InitializeScripts();

        // Set current UI culture
        currentCulture = CultureHelper.PreferredUICulture;

        // Initialize current user
        currentUser = CMSContext.CurrentUser;

        // Initialize current site
        currentSite = CMSContext.CurrentSite;

        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;
        ctlAsync.PostbackOnError = true;

        // Initialize cancel button
        btnCancel.Text = ResHelper.GetString("general.cancel");
        btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

        titleElemAsync.TitleText = GetString("validation.css.checkingcss");
        titleElemAsync.TitleImage = GetImageUrl("Design/Controls/Validation/check.png");

        // Add validate item link to header actions
        lnkValidate.OnClientClick = "LoadHTMLToElement('" + hdnHTML.ClientID + "','" + Url + "');";
        lblValidate.ResourceString = "general.validate";
        lnkValidate.ToolTip = GetString("general.validate");
        imgValidate.AlternateText = GetString("general.validate");
        imgValidate.ImageUrl = GetImageUrl("Design/Controls/Validation/check.png");

        // Add view code link to actions
        lnkViewCode.OnClientClick = String.Format("modalDialog('" + ResolveUrl("~/CMSModules/Content/CMSDesk/Validation/ViewCode.aspx") + "?url={0}&hash={1}', 'ViewHTMLCode', 800, 600);return false;", Url, QueryHelper.GetHash("?url=" + Url));
        lnkViewCode.ToolTip = GetString("validation.viewcodetooltip");
        lblViewCode.ResourceString = "validation.viewcode";
        imgViewCode.ImageUrl = GetImageUrl("Design/Controls/Validation/codeview.png");
        imgViewCode.AlternateText = GetString("validation.viewcode");

        lnkNewWindow.ToolTip = GetString("validation.showresultsnewwindow");
        lblNewWindow.ResourceString = "validation.showresultsnewwindow";
        imgNewWindow.ImageUrl = GetImageUrl("Design/Controls/Validation/windownew.png");
        imgNewWindow.DisabledImageUrl = GetImageUrl("Design/Controls/Validation/windownewdisabled.png");

        lnkExportToExcel.ToolTip = GetString("export.exporttoexcel");
        lblExportToExcel.ResourceString = "export.exporttoexcel";
        imgExportToExcel.ImageUrl = GetImageUrl("Design/Controls/Validation/exportexcel.png");
        imgExportToExcel.DisabledImageUrl = GetImageUrl("Design/Controls/Validation/exportexceldisabled.png");
        imgExportToExcel.AlternateText = GetString("export.exporttoexcel");

        // Set sorting and add events
        gridValidationResult.OrderBy = "line";
        gridValidationResult.IsLiveSite = IsLiveSite;
        gridValidationResult.ZeroRowsText = GetString("validation.css.notvalidated");
        gridValidationResult.OnExternalDataBound += gridValidationResult_OnExternalDataBound;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void lnkValidate_Click(object sender, EventArgs e)
    {
        pnlLog.Visible = true;
        DataSource = null;
        pnlGrid.Visible = false;

        CurrentLog.Close();
        CurrentError = string.Empty;
        EnsureLog();

        // Get the full domain
        ctlAsync.Parameter = URLHelper.GetFullDomain() + ";" + URLHelper.GetFullApplicationUrl() + ";" + URLHelper.RemoveProtocolAndDomain(Url);
        ctlAsync.RunAsync(CheckCss, WindowsIdentity.GetCurrent());
    }


    /// <summary>
    /// Export handler.
    /// </summary>
    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        UniGridExportHelper export = new UniGridExportHelper(gridValidationResult);
        export.ExportRawData = false;
        export.GenerateHeader = true;
        export.FileName = ValidationHelper.GetSafeFileName("CSSValidation_" + ((Node != null) ? Node.DocumentNamePath : Url) + "_" + DateTime.Now.ToString()).Replace(" ", "_");
        export.ExportData(DataExportFormatEnum.XLSX, Page.Response);
    }


    /// <summary>
    /// On external databound event
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Action what is called</param>
    /// <param name="parameter">Parameter</param>
    /// <returns>Result object</returns>
    protected object gridValidationResult_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        return parameter;
    }

    #endregion


    #region "Validation methods"

    /// <summary>
    /// Prepare Dictionary with requests for CSS validation
    /// </summary>
    /// <param name="parameter">Asynchronous parameter containing current url data to resolve absolute URL </param>
    private Dictionary<string, string> GetValidationRequests(string parameter)
    {
        string html = GetHtml(Url);
        Dictionary<string, string> cssRequests = null;
        string[] urlParams = parameter.Split(';');

        if (!String.IsNullOrEmpty(html))
        {
            cssRequests = new Dictionary<string, string>();

            // Get inline CSS
            AddLog(GetString("validation.css.preparinginline"));
            StringBuilder sbInline = new StringBuilder();
            foreach (Match m in InlineStylesRegex.Matches(html))
            {
                string captured = m.Groups["css"].Value;
                sbInline.Append(captured);
                sbInline.Append("\n");
            }

            cssRequests.Add(DocumentValidationHelper.InlineCSSSource, sbInline.ToString());

            // Get linked styles URLs
            WebClient client = new WebClient();
            foreach (Match m in LinkedStylesRegex.Matches(html))
            {
                string url = m.Groups["url"].Value;
                url = Server.HtmlDecode(url);

                string css = null;
                if (!String.IsNullOrEmpty(url))
                {
                    bool processCss = true;
                    string[] excludedCsss = mExcludedCSS.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    // Check if CSS is not excluded (CMS stylesheets)
                    foreach (string excludedCss in excludedCsss)
                    {
                        if (url.EndsWith(excludedCss, StringComparison.InvariantCultureIgnoreCase))
                        {
                            processCss = false;
                            break;
                        }
                    }

                    if (processCss && !cssRequests.ContainsKey(url))
                    {
                        AddLog(String.Format(GetString("validation.css.preparinglinkedstyles"), url));

                        try
                        {
                            // Get CSS data from URL
                            StreamReader reader = StreamReader.New(client.OpenRead(DocumentValidationHelper.DisableMinificationOnUrl(URLHelper.GetAbsoluteUrl(url, urlParams[0], urlParams[1], urlParams[2]))));
                            css = reader.ReadToEnd();
                            if (!String.IsNullOrEmpty(css))
                            {
                                cssRequests.Add(url, css.Trim(new char[] { '\r', '\n' }));
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        return cssRequests;
    }


    /// <summary>
    /// Get HTML code using server or client method
    /// </summary>
    /// <param name="url">URL to obtain HTML from</param>
    private string GetHtml(string url)
    {
        if (UseServerRequestType)
        {
            // Create web client and try to obatin HTML using it
            WebClient client = new WebClient();
            try
            {
                StreamReader reader = StreamReader.New(client.OpenRead(url));
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                lblError.Text = String.Format(ResHelper.GetString("validation.exception"), e.Message);
                lblError.Visible = true;
                return null;
            }
        }
        else
        {
            // Get HTML stored using javascript
            return ValidationHelper.Base64Decode(hdnHTML.Value);
        }
    }


    /// <summary>
    /// Send validation request to validator and obtain result 
    /// </summary>
    /// <param name="validatorParameters">Validator parameters</param>
    /// <returns>DataSet containing validator response</returns>
    private DataSet GetValidationResults(Dictionary<string, string> validationData, string parameter)
    {
        DataSet dsResponse = null;
        DataSet dsResult = new DataSet();
        DataTable dtResponse = null;

        List<string> validatedUrls = validationData.Keys.ToList<string>();
        Random randGen = new Random();
        DataSource = dsResult;

        string source = null;
        int counter = 0;
        while (validatedUrls.Count > 0)
        {
            // Check if source is processed repeatedly
            if (source == validatedUrls[0])
            {
                counter++;
            }
            else
            {
                counter = 0;
            }

            // Set current source to validate
            source = validatedUrls[0];
            string cssData = validationData[source];
            validatedUrls.RemoveAt(0);

            if (!String.IsNullOrEmpty(cssData))
            {
                // Create web request
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ValidatorURL);
                req.Method = "POST";
                string boundary = "---------------------------" + randGen.Next(1000000, 9999999) + randGen.Next(1000000, 9999999);
                req.ContentType = "multipart/form-data; boundary=" + boundary;

                // Set data to web request for validation           
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(GetRequestData(GetRequestDictionary(cssData), boundary));
                req.ContentLength = data.Length;
                StreamWrapper writer = StreamWrapper.New(req.GetRequestStream());
                writer.Write(data, 0, data.Length);
                writer.Close();

                try
                {
                    // Process server answer
                    AddLog(String.Format(GetString("validation.css.validatingcss"), source));
                    StreamWrapper response = StreamWrapper.New(req.GetResponse().GetResponseStream());
                    if (response != null)
                    {
                        dsResponse = new DataSet();
                        dsResponse.ReadXml(response.SystemStream);
                    }

                    string[] currentUrlValues = parameter.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters["sitename"] = CMSContext.CurrentSiteName;
                    parameters["user"] = currentUser;
                    parameters["source"] = source;
                    parameters["domainurl"] = currentUrlValues[0];
                    parameters["applicationurl"] = currentUrlValues[1];

                    dtResponse = DocumentValidationHelper.ProcessValidationResult(dsResponse, DocumentValidationEnum.CSS, parameters);

                    // Check if response contain any relevant data
                    if (!DataHelper.DataSourceIsEmpty(dtResponse))
                    {
                        // Add response data to validation DataSet
                        if (DataHelper.DataSourceIsEmpty(dsResult))
                        {
                            dsResult.Tables.Add(dtResponse);
                        }
                        else
                        {
                            dsResult.Tables[0].Merge(dtResponse);
                        }
                    }
                }
                catch
                {
                    if (counter < 5)
                    {
                        validatedUrls.Insert(0, source);
                    }
                }
                finally
                {
                    Thread.Sleep(ValidationDelay);
                }
            }
        }

        return dsResult;
    }


    /// <summary>
    /// Get dictionary with request parameters
    /// </summary>
    /// <param name="data">CSS data to be checked</param>
    private Dictionary<string, string> GetRequestDictionary(string data)
    {
        Dictionary<string, string> reqData = new Dictionary<string, string>();
        reqData.Add("text", data);
        reqData.Add("profile", ValidatorProfile);
        reqData.Add("usermedium", "all");
        reqData.Add("type", "none");
        reqData.Add("warning", "1");
        reqData.Add("output", "soap12");
        return reqData;
    }


    /// <summary>
    /// Get request data which will be sent using HTTP request to validator
    /// </summary>
    /// <param name="data">Data to create </param>
    /// <param name="boundary">HTTP boundary string</param>
    private string GetRequestData(Dictionary<string, string> data, string boundary)
    {
        string separator = "\r\n";
        boundary = "--" + boundary;

        // Prepare begining of the request data
        StringBuilder sbRequest = new StringBuilder();
        sbRequest.Append(boundary);
        sbRequest.Append(separator);

        // Proces request form data
        foreach (string key in data.Keys)
        {
            sbRequest.Append(String.Format("Content-Disposition: form-data; name=\"{0}\"", key));
            sbRequest.Append(separator);
            sbRequest.Append(separator);
            sbRequest.Append(data[key]);
            sbRequest.Append(separator);
            sbRequest.Append(boundary);
            sbRequest.Append(separator);

        }
        string request = sbRequest.ToString();

        // Add final boundary dashes
        request = request.Insert(request.Length - 2, "--");
        return request;
    }


    /// <summary>
    /// Process validation results
    /// </summary>
    /// <param name="result">DataSet with result of validation</param>
    public void ProcessResult(DataSet validationResult)
    {
        if (validationResult != null)
        {
            pnlStatus.Visible = true;
            lblError.Visible = !String.IsNullOrEmpty(lblError.Text);

            // Check if result is not empty
            if (!DataHelper.DataSourceIsEmpty(validationResult))
            {
                // Show validation errors
                lblStatus.Text = GetString("validation.css.resultinvalid");
                imgStatus.ImageUrl = GetImageUrl("Design/Controls/Validation/warning.png");
                lblResults.Visible = true;
                lblResults.Text = ResHelper.GetString("validation.validationresults");
                gridValidationResult.Visible = true;
            }
            else
            {
                // Show validation is valid
                lblStatus.Text = GetString("validation.css.resultvalid");
                lblResults.Visible = false;
                gridValidationResult.Visible = false;
                imgStatus.ImageUrl = GetImageUrl("Design/Controls/Validation/check.png");
            }
        }
        else
        {
            // No results obtained during validation, show error
            pnlStatus.Visible = false;
            lblResults.Visible = false;
            gridValidationResult.Visible = false;
            if (string.IsNullOrEmpty(lblError.Text))
            {
                lblError.Text = GetString("validation.errorinitialization");
            }
            lblError.Visible = true;
        }
    }


    /// <summary>
    /// Check document CSS
    /// </summary>
    /// <param name="parameter">Parameter containing data to resolve relative links to absolute</param>
    private void CheckCss(object parameter)
    {
        try
        {
            AddLog(ResHelper.GetString("validation.css.checkingcss", currentCulture));
            Dictionary<string, string> requests = GetValidationRequests(ValidationHelper.GetString(parameter, null));

            // Ensure thread doesn't finish to early in special situations 
            if ((requests == null) || (requests.Count <= 1))
            {
                Thread.Sleep(200);
            }

            if (requests != null)
            {
                GetValidationResults(requests, ValidationHelper.GetString(parameter, null));
                DataPostProcessing = true;
            }
            else
            {
                CurrentError = GetString("validation.diffdomainorprotocol");
            }
            pnlLog.Visible = false;
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // When canceled
                AddLog(ResHelper.GetString("validation.css.abort", currentCulture));
                ctlAsync.RaiseError(null, null);
            }
            else
            {
                lblError.Text = ex.Message;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    #endregion


    #region "Handling async thread"

    /// <summary>
    /// On cancel event
    /// </summary>
    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        ctlAsync.Parameter = null;
        AddError(ResHelper.GetString("validation.validationcanceled"));
        ScriptHelper.RegisterStartupScript(this, typeof(string), "CancelLog", ScriptHelper.GetScript("var __pendingCallbacks = new Array();"));
        lblError.Text = CurrentError;
        lblError.CssClass = "InfoLabel";
        pnlLog.Visible = false;
        pnlGrid.Visible = true;
        CurrentLog.Close();
        DataPostProcessing = true;

    }


    /// <summary>
    /// On request log event
    /// </summary>
    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    /// <summary>
    /// On error event
    /// </summary>
    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        if (ctlAsync.Status == AsyncWorkerStatusEnum.Running)
        {
            ctlAsync.Stop();
        }
        ctlAsync.Parameter = null;
        lblError.Text = CurrentError;
        CurrentLog.Close();
    }


    /// <summary>
    /// On finished event
    /// </summary>
    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        lblError.Text = CurrentError;
        CurrentLog.Close();
        pnlLog.Visible = false;
        pnlGrid.Visible = true;
    }


    /// <summary>
    /// Ensures the logging context
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext log = LogContext.EnsureLog(ctlAsync.ProcessGUID);
        log.Reversed = true;
        log.LineSeparator = "<br />";
        return log;
    }


    /// <summary>
    /// Adds the log information
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected void AddLog(string newLog)
    {
        EnsureLog();
        LogContext.AppendLine(newLog);
    }


    /// <summary>
    /// Adds the error to collection of errors
    /// </summary>
    /// <param name="error">Error message</param>
    protected void AddError(string error)
    {
        AddLog(error);
        CurrentError = (error + "<br />" + CurrentError);
    }

    #endregion
}