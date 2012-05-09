using System;
using System.Web;
using System.Data;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Web.UI;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.IO;
using CMS.CMSHelper;

public partial class CMSAdminControls_Validation_HTMLValidator : DocumentValidator
{
    #region "Constants"

    private const string DEFAULT_VALIDATOR_URL = "http://validator.w3.org/check";

    #endregion


    #region "Variables"

    private string mValidatorURL = null;
    private DataSet mDataSource = null;
    private bool mUseServerRequest = false;
    private string mAppValidatorPath = null;

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
                mValidatorURL = DataHelper.GetNotEmpty(SettingsKeyProvider.GetStringValue("CMSValidationHTMLValidatorURL"), DEFAULT_VALIDATOR_URL);
            }
            return mValidatorURL;
        }
        set
        {
            mValidatorURL = value;
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
    /// Validator application path use to resolve links referecing on validator pages
    /// </summary>
    private string AppValidatorPath
    {
        get
        {
            if (mAppValidatorPath == null)
            {
                mAppValidatorPath = URLHelper.RemoveProtocolAndDomain(ValidatorURL);
                int lastId = mAppValidatorPath.LastIndexOf('/');
                if (lastId >= 0)
                {
                    mAppValidatorPath = mAppValidatorPath.Substring(0, lastId).TrimEnd('/');
                }
                else
                {
                    mAppValidatorPath = "";
                }
            }
            return mAppValidatorPath;
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
    /// Key to store validation result
    /// </summary>
    protected override string ResultKey
    {
        get
        {
            return "validation|html|" + CMSContext.PreferredCultureCode + "|" + Url;
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
            }
            base.DataSource = mDataSource;

            return mDataSource;
        }
        set
        {
            base.DataSource = value;
            mDataSource = value;
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
            ReloadData();
        }
    }


    /// <summary>
    /// Initializes all nested controls.
    /// </summary>
    private void SetupControls()
    {
        InitializeScripts();

        // Add validate item link to actions
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

        if (DataHelper.DataSourceIsEmpty(DataSource))
        {
            lnkExportToExcel.Enabled = lnkNewWindow.Enabled = false;
            lnkExportToExcel.CssClass = lnkNewWindow.CssClass = "MenuItemEditDisabled";
            lnkNewWindow.OnClientClick = lnkExportToExcel.OnClientClick = null;
        }
        else
        {
            lnkExportToExcel.Enabled = lnkNewWindow.Enabled = true;
            lnkExportToExcel.CssClass = lnkNewWindow.CssClass = "MenuItemEdit";
            lnkNewWindow.OnClientClick = String.Format("modalDialog('" + ResolveUrl("~/CMSModules/Content/CMSDesk/Validation/ValidationResults.aspx") + "?datakey={0}&docid={1}&hash={2}', 'ViewValidationResult', 800, 600);return false;", ResultKey, Node.DocumentID, QueryHelper.GetHash(String.Format("?datakey={0}&docid={1}", ResultKey, Node.DocumentID)));
            lnkExportToExcel.OnClientClick = "$j(\"#" + up.ClientID + "\").remove()";
        }

        // Set sorting and add events       
        gridValidationResult.OrderBy = "line ASC";
        gridValidationResult.IsLiveSite = IsLiveSite;
        gridValidationResult.OnExternalDataBound += gridValidationResult_OnExternalDataBound;
        gridValidationResult.ZeroRowsText = GetString("validation.html.notvalidated");

        // Set custom validating text
        up.ProgressHTML = String.Concat("<div style=\"display: none;\" id=\"", up.ClientID, "\" class=\"UP\"><div>", String.Concat("<img src=\"", UIHelper.GetImageUrl(Page, "Design/Preloaders/preload16.gif"), "\" alt=\"", GetString("validation.validating"), "\" /><span>", GetString("validation.validating"), "</span>"), "</div></div>");
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void lnkValidate_Click(object sender, EventArgs e)
    {
        DataSource = null;
        DataSource = ValidateHtml();
        ReloadData();
    }


    /// <summary>
    /// Loads data from the data source property.
    /// </summary>
    public void ReloadData()
    {
        // Fill the grid data source
        if (!DataHelper.DataSourceIsEmpty(DataSource))
        {
            gridValidationResult.DataSource = DataSource;
            gridValidationResult.ReloadData();
        }

        ProcessResult(DataSource);

        SetupControls();
    }


    /// <summary>
    /// Export handler.
    /// </summary>
    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        UniGridExportHelper export = new UniGridExportHelper(gridValidationResult);
        export.ExportRawData = false;
        export.GenerateHeader = true;
        export.FileName = ValidationHelper.GetSafeFileName("HTMLValidation_" + ((Node != null) ? Node.DocumentNamePath : Url) + "_" + DateTime.Now.ToString()).Replace(" ", "_");
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


    #region "Validation request methods"

    /// <summary>
    /// Get validation request parameters
    /// </summary>
    /// <param name="htmlDocument">Content of HTML document</param>
    /// <returns>Validator request parameters</returns>
    private string GetRequestParameters(string htmlDocument)
    {
        string requestData = "fragment=" + HttpUtility.UrlEncode(htmlDocument);
        requestData += "&output=soap12";

        return requestData;
    }


    /// <summary>
    /// Send validation request to validator and obtain result 
    /// </summary>
    /// <param name="validatorParameters">Validator parameters</param>
    /// <returns>DataSet containing validator response</returns>
    private DataSet GetValidationResult(string validatorParameters)
    {
        try
        {
            DataSet dsValResult = null;

            // Create web request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ValidatorURL);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(validatorParameters);
            req.ContentLength = data.Length;
            StreamWrapper writer = StreamWrapper.New(req.GetRequestStream());
            writer.Write(data, 0, data.Length);
            writer.Close();

            // Process server answer
            StreamWrapper answer = StreamWrapper.New(req.GetResponse().GetResponseStream());
            if (answer != null)
            {
                dsValResult = new DataSet();
                dsValResult.ReadXml(answer.SystemStream);
            }

            return dsValResult;
        }
        catch
        {
            lblError.Text = GetString("validation.servererror");
            return null;
        }
    }


    /// <summary>
    /// General method to process validation and return validation results
    /// </summary>
    private DataSet ValidateHtml()
    {
        if (!String.IsNullOrEmpty(Url))
        {
            string docHtml = GetHtml(Url);
            if (!String.IsNullOrEmpty(docHtml))
            {
                DataSet dsValidationResult = GetValidationResult(GetRequestParameters(docHtml));

                if (!DataHelper.DataSourceIsEmpty(dsValidationResult))
                {
                    // Check if result contains error table
                    if (!DataHelper.DataSourceIsEmpty(dsValidationResult.Tables["error"]))
                    {
                        Dictionary<string, object> parameters = new Dictionary<string, object>();
                        parameters["validatorurl"] = ValidatorURL;
                        parameters["validatorapppath"] = AppValidatorPath;

                        DataTable tbError = DocumentValidationHelper.ProcessValidationResult(dsValidationResult, DocumentValidationEnum.HTML, parameters);
                        DataSet result = new DataSet();
                        result.Tables.Add(tbError);
                        return result;
                    }
                    else
                    {
                        return new DataSet();
                    }
                }
            }
            else
            {
                lblError.Text = GetString("validation.diffdomainorprotocol");
            }
        }

        return null;
    }


    /// <summary>
    /// Process validation results
    /// </summary>
    /// <param name="result"></param>
    public void ProcessResult(DataSet validationResult)
    {
        if (validationResult != null)
        {
            pnlStatus.Visible = true;
            lblError.Visible = false;

            // Check if result is not empty
            if (!DataHelper.DataSourceIsEmpty(validationResult) && !DataHelper.DataSourceIsEmpty(validationResult.Tables["error"]))
            {
                // Show validation errors
                lblResults.Text = GetString("validation.validationresults");
                lblResults.Visible = true;
                gridValidationResult.Visible = true;
                lblStatus.Text = GetString("validation.html.resultinvalid");
                imgStatus.ImageUrl = GetImageUrl("Design/Controls/Validation/warning.png");
            }
            else
            {
                // Show validation is valid
                lblStatus.Text = GetString("validation.html.resultvalid");
                lblResults.Visible = false;
                gridValidationResult.Visible = false;
                imgStatus.ImageUrl = GetImageUrl("Design/Controls/Validation/check.png");
            }
        }
        else
        {
            // No results obtained from validator, show error
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
    /// Fires specific action and returns result provided by the parent control.
    /// </summary>
    /// <param name="dr">Data related to the action.</param>
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
            catch
            {
                return null;
            }
        }
        else
        {
            // Get HTML stored using javascript
            return ValidationHelper.Base64Decode(hdnHTML.Value);
        }
    }

    #endregion
}