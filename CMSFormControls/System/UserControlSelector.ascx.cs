using System;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.IO;

public partial class CMSFormControls_System_UserControlSelector : FormEngineUserControl, ICallbackEventHandler
{
    #region "Variables"

    private FileSystemDialogConfiguration mDialogConfig = null;
    private bool mAllowEmptyValue = true;
    private string mSelectedPathPrefix = String.Empty;
    private bool mEnabled = true;
    /// <summary>
    /// Hidden value field.
    /// </summary>
    protected HiddenField mHiddenUrl = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Selector value: path of the file or folder.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.txtPath.Text;
        }
        set
        {
            if (value != null)
            {
                this.txtPath.Text = value.ToString();
            }
            else
            {
                this.txtPath.Text = String.Empty;
            }
        }
    }


    /// <summary>
    /// Configuration of the dialog for inserting Images.
    /// </summary>
    public FileSystemDialogConfiguration DialogConfig
    {
        get
        {
            if (mDialogConfig == null)
            {
                mDialogConfig = new FileSystemDialogConfiguration();
            }
            return mDialogConfig;
        }
        set
        {
            mDialogConfig = value;
        }
    }


    /// <summary>
    /// Gets or sets if value of form control could be empty.
    /// </summary>
    public bool AllowEmptyValue
    {
        get
        {
            return this.mAllowEmptyValue;
        }
        set
        {
            this.mAllowEmptyValue = value;
        }
    }


    /// <summary>
    /// Gets or sets prefix for paths with preselected source folder(webparts,form controls,...).
    /// </summary>
    public string SelectedPathPrefix
    {
        get
        {
            return mSelectedPathPrefix;
        }
        set
        {
            mSelectedPathPrefix = value;
        }
    }


    /// <summary>
    /// Validates the return value of form control.
    /// </summary>
    public override bool IsValid()
    {
        if (!AllowEmptyValue)
        {
            if (String.IsNullOrEmpty(txtPath.Text.Trim()))
            {
                if (!DialogConfig.ShowFolders)
                {
                    this.ValidationError = ResHelper.GetString("UserControlSelector.RequireFileName");
                }
                else
                {
                    this.ValidationError = ResHelper.GetString("UserControlSelector.RequireFolderName");
                }
                return false;
            }
        }

        if (DialogConfig.ShowFolders)
        {
            if (!IsAllowedAndNotExcludedFolder(txtPath.Text))
            {
                this.ValidationError = ResHelper.GetString("dialogs.filesystem.NotAllowedFolder");
                return false;
            }
        }
        else
        {
            string ext = (txtPath.Text.Contains(".") ? txtPath.Text.Substring(txtPath.Text.LastIndexOf(".")) : String.Empty);
            if (!IsAllowedExtension(ext) || IsExcludedExtension(ext))
            {
                if (!String.IsNullOrEmpty(DialogConfig.AllowedExtensions))
                {
                    string allowedExt = ";" + DialogConfig.AllowedExtensions + ";";

                    if (!String.IsNullOrEmpty(DialogConfig.ExcludedExtensions))
                    {
                        foreach (string excludedExt in DialogConfig.ExcludedExtensions.Split(';'))
                        {
                            allowedExt = allowedExt.Replace(";" + excludedExt + ";", ";");
                        }
                    }
                    this.ValidationError = ResHelper.GetString("dialogs.filesystem.NotAllowedExtension").Replace("%%extensions%%", FormatExtensions(allowedExt));
                }
                else
                {
                    this.ValidationError = ResHelper.GetString("dialogs.filesystem.ExcludedExtension").Replace("%%extensions%%", FormatExtensions(DialogConfig.ExcludedExtensions));
                }
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// Gets or sets if value can be changed.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return mEnabled;
        }
        set
        {
            mEnabled = value;
            this.txtPath.Enabled = value;
            this.btnSelect.Enabled = value;
            this.btnClear.Enabled = value;
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Init event.
    /// </summary>
    /// <param name="sender">Sender parameter</param>
    /// <param name="e">Arguments</param>
    protected void Page_Init(object sender, EventArgs e)
    {
        CreateChildControls();
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            SetupControls();
        }
    }


    /// <summary>
    /// Ensure creation of controls.
    /// </summary>
    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        if (mHiddenUrl == null)
        {
            mHiddenUrl = new HiddenField();
            mHiddenUrl.ID = "hidUrl";

            this.Controls.Add(mHiddenUrl);
        }

    }


    /// <summary>
    /// Setup all contained controls.
    /// </summary>
    private void SetupControls()
    {
        ScriptHelper.RegisterJQuery(this.Page);
        this.btnSelect.Text = ResHelper.GetString("General.select");
        this.btnClear.Text = ResHelper.GetString("General.clear");

        if (Enabled)
        {
            // Configure FileSystem dialog
            string width = this.DialogConfig.DialogWidth.ToString();
            string height = this.DialogConfig.DialogHeight.ToString();
            if (this.DialogConfig.UseRelativeDimensions)
            {
                width += "%";
                height += "%";
            }

            this.DialogConfig.EditorClientID = txtPath.ClientID;
            this.DialogConfig.SelectedPath = txtPath.Text;

            string url = GetDialogURL(this.DialogConfig, this.SelectedPathPrefix);

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this.Page);

            // Register the Path related javascript functions
            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "FileSystemSelector_" + this.ClientID, ScriptHelper.GetScript(
                "function UpdateModalDialogURL_" + this.ClientID + "(newValue,context){ var item = document.getElementById(context + '_hidUrl'); if(item!=null && item.value!=null){item.value = newValue;}}\n" +
                "function SetMediaValue_" + this.ClientID + "(selectorId){if(window.Changed){Changed();} var newValue = document.getElementById(selectorId + '_txtPath').value;" + this.Page.ClientScript.GetCallbackEventReference(this, "newValue", "UpdateModalDialogURL_" + this.ClientID, "selectorId") + ";} \n" +
                "function ClearSelection_" + this.ClientID + "(selectorId){ if(window.Changed){Changed();}document.getElementById(selectorId + '_txtPath').value=''; var newValue='';" + this.Page.ClientScript.GetCallbackEventReference(this, "newValue", "UpdateModalDialogURL_" + this.ClientID, "selectorId") + "; }"));

            // Setup buttons
            this.txtPath.Attributes.Add("onchange", "SetMediaValue_" + this.ClientID + "('" + this.ClientID + "');");

            ScriptHelper.RegisterStartupScript(this, typeof(string), this.DialogConfig.EditorClientID + "script", ScriptHelper.GetScript(
                "var hidField_" + this.ClientID + " = document.getElementById('" + this.ClientID + "' + '_hidUrl'); if(hidField_" + this.ClientID + ") {hidField_" + this.ClientID + ".value='" + url + "';}"));
            this.btnSelect.Attributes.Add("onclick", "var url_" + this.ClientID + " = document.getElementById('" + this.ClientID + "' + '_hidUrl').value; modalDialog(url_" + this.ClientID + ", 'SelectFile', '" + width + "', '" + height + "', null); return false;");
            this.btnClear.Attributes.Add("onclick", "ClearSelection_" + this.ClientID + "('" + this.ClientID + "'); return false;");
        }
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Returns query string which will be passed to the CMS dialogs (Insert image or media/Insert link).
    /// </summary>
    /// <param name="config">Dialog configuration</param>  
    /// <param name="selectedPathPrefix">Path prefix of selected value</param>
    public string GetDialogURL(FileSystemDialogConfiguration config, string selectedPathPrefix)
    {
        StringBuilder builder = new StringBuilder();

        // Set constraints
        // Allowed files extensions            
        if (!String.IsNullOrEmpty(config.AllowedExtensions))
        {
            builder.Append("&allowed_extensions=" + Server.UrlEncode(config.AllowedExtensions));
        }

        // Excluded extensions
        if (!String.IsNullOrEmpty(config.ExcludedExtensions))
        {
            builder.Append("&excluded_extensions=" + Server.UrlEncode(config.ExcludedExtensions));
        }

        // Allowed folders 
        if (!String.IsNullOrEmpty(config.AllowedFolders))
        {
            builder.Append("&allowed_folders=" + Server.UrlEncode(config.AllowedFolders));
        }

        // Excluded folders
        if (!String.IsNullOrEmpty(config.ExcludedFolders))
        {
            builder.Append("&excluded_folders=" + Server.UrlEncode(config.ExcludedFolders));
        }

        // Default path-preselected path in filesystem tree
        if (!String.IsNullOrEmpty(config.DefaultPath))
        {
            builder.Append("&default_path=" + Server.UrlEncode(config.DefaultPath));
        }

        // Deny non-application starting path
        if (!config.AllowNonApplicationPath)
        {
            builder.Append("&allow_nonapp_path=0");
        }

        // SelectedPath - actual value of textbox
        if (!String.IsNullOrEmpty(config.SelectedPath))
        {
            string selectedPath = config.SelectedPath;
            if (!(selectedPath.StartsWith("~") || selectedPath.StartsWith("/") || selectedPath.StartsWith(".") || selectedPath.StartsWith("\\"))
                && ((String.IsNullOrEmpty(config.StartingPath)) || (config.StartingPath.StartsWith("~"))) && (!String.IsNullOrEmpty(selectedPathPrefix)))
            {
                selectedPath = selectedPathPrefix.TrimEnd('/') + "/" + selectedPath.TrimStart('/');
            }
            builder.Append("&selected_path=" + Server.UrlEncode(selectedPath));
        }

        // Starting path in filesystem
        if (!String.IsNullOrEmpty(config.StartingPath))
        {
            builder.Append("&starting_path=" + Server.UrlEncode(config.StartingPath));
        }

        // Show only folders|files
        builder.Append("&show_folders=" + Server.UrlEncode(config.ShowFolders.ToString()));

        // Editor client id
        if (!String.IsNullOrEmpty(config.EditorClientID))
        {
            builder.Append("&editor_clientid=" + Server.UrlEncode(config.EditorClientID));
        }

        // Get hash for complete query string
        string query = HttpUtility.UrlPathEncode("?" + builder.ToString().TrimStart('&'));
        string hash = QueryHelper.GetHash(query);

        // Get complete query string with attached hash
        string queryString = HttpUtility.UrlPathEncode(URLHelper.EncodeQueryString("?" + builder.Append("&hash=" + hash).ToString().TrimStart('&')));

        string baseUrl = "~/CMSFormControls/Selectors/";

        // Get complet URL
        return ResolveUrl(baseUrl + "SelectFileOrFolder/Default.aspx" + queryString);

    }


    /// <summary>
    /// Check if manually typed extension is allowed.
    /// </summary>
    /// <param name="extension">File extension</param>
    /// <returns>True if allowed, false otherwise</returns>
    private bool IsAllowedExtension(string extension)
    {
        if (String.IsNullOrEmpty(this.DialogConfig.AllowedExtensions))
        {
            return true;
        }
        else
        {
            string extensions = ";" + this.DialogConfig.AllowedExtensions.ToLower() + ";";
            if (extensions.Contains(";" + extension.ToLower().TrimStart('.') + ";"))
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Check if manually typed extension is excluded.
    /// </summary>
    /// <param name="extension">File extension</param>
    /// <returns>True if excluded, false otherwise</returns>
    private bool IsExcludedExtension(string extension)
    {
        if (String.IsNullOrEmpty(this.DialogConfig.ExcludedExtensions))
        {
            return false;
        }
        else
        {
            string extensions = ";" + this.DialogConfig.ExcludedExtensions.ToLower() + ";";
            if (extensions.Contains(";" + extension.ToLower().TrimStart('.') + ";"))
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Check if folder is allowed and not excluded.
    /// </summary>
    /// <param name="info">DiretoryInfo to check</param>
    /// <returns>True if folder isallowed and not excluded otherwise false</returns>
    private bool IsAllowedAndNotExcludedFolder(string folder)
    {
        bool isAllowed = false;
        bool isExcluded = false;

        string startPath = this.DialogConfig.StartingPath;

        // Resolve relative URL with ~
        if (startPath.StartsWith("~"))
        {
            startPath = ResolveUrl(startPath);
        }

        // Map to server if not network path
        if (!startPath.Contains("\\\\"))
        {
            startPath = Server.MapPath(startPath);
        }

        startPath = DirectoryHelper.EnsurePathBackSlash(startPath.ToLower());
        string folderName = DirectoryHelper.EnsurePathBackSlash(folder.ToLower());
        try
        {
            folderName = Server.MapPath(folderName);
        }
        catch
        {
        }

        folderName = folderName.ToLower();

        // Check if folder is allowed
        if (String.IsNullOrEmpty(this.DialogConfig.AllowedFolders))
        {
            isAllowed = true;
        }
        else
        {
            foreach (string path in this.DialogConfig.AllowedFolders.ToLower().Split(';'))
            {
                if (folderName.StartsWith(startPath + path))
                {
                    isAllowed = true;
                }
            }
        }

        // Check if folder isn't excluded
        if (!String.IsNullOrEmpty(this.DialogConfig.ExcludedFolders))
        {
            foreach (string path in this.DialogConfig.ExcludedFolders.ToLower().Split(';'))
            {
                if (folderName.StartsWith(startPath + path))
                {
                    isExcluded = true;
                }
            }
        }
        return (isAllowed) && (!isExcluded);
    }


    /// <summary>
    /// Format extensions.
    /// </summary>
    /// <param name="extensions">Extensions string to be displayed</param>
    /// <returns></returns>
    private string FormatExtensions(string extensions)
    {
        string ext = ";" + extensions.Trim(';');
        return ext.Replace(";", ";.").TrimStart(';').Replace(";", ", ");
    }

    #endregion


    #region "Callback handling"

    /// <summary>
    /// Raises the callback event.
    /// </summary>
    public void RaiseCallbackEvent(string eventArgument)
    {
        //LoadDisplayValues(eventArgument);
        // Configure Image dialog
        string width = this.DialogConfig.DialogWidth.ToString();
        string height = this.DialogConfig.DialogHeight.ToString();
        if (this.DialogConfig.UseRelativeDimensions)
        {
            width += "%";
            height += "%";
        }
        this.DialogConfig.SelectedPath = eventArgument;
        string url = GetDialogURL(this.DialogConfig, this.SelectedPathPrefix);
        this.mHiddenUrl.Value = url;

    }


    /// <summary>
    /// Prepares the callback result.
    /// </summary>
    public string GetCallbackResult()
    {
        return this.mHiddenUrl.Value;
    }

    #endregion
}
