using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Text.RegularExpressions;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.FormEngine;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSWebParts_Text_editabletext : CMSAbstractEditableWebPart
{
    #region "Variables"

    protected string mHtmlAreaToolbar = "";
    protected string mHtmlAreaToolbarLocation = "";
    protected bool mShowToolbar = false;
    protected string mHTMLEditorCssStylesheet = "";

    protected CMSEditableRegionTypeEnum mRegionType = CMSEditableRegionTypeEnum.TextBox;
    protected string mRegionTitle = "";

    protected int mMaxLength = 0;
    protected int mMinLength = 0;
    protected int mDialogHeight = 0;
    protected int mDialogWidth = 0;

    protected bool mWordWrap = true;

    protected Label lblTitle = null;
    protected Panel pnlEditor = null;
    protected Label lblError = null;
    protected IHtmlEditor htmlValue = null;
    protected TextBox txtValue = null;
    //protected Label lblTitle = null;

    protected Literal ltlContent = null;
    ViewModeEnum viewMode = ViewModeEnum.Unknown;

    protected XmlData mImageAutoResize = null;
    protected int mResizeToWidth = 0;
    protected int mResizeToHeight = 0;
    protected int mResizeToMaxSideSize = 0;
    protected bool mDimensionsLoaded = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the type of server control which is displayed in the editable region.
    /// </summary>
    [Category("Appearence"), Description("Gets or sets the type of server control which is displayed in the editable region.")]
    public virtual CMSEditableRegionTypeEnum RegionType
    {
        get
        {
            return mRegionType;
        }
        set
        {
            mRegionType = value;
        }
    }


    /// <summary>
    /// Gets or sets the control title which is displayed in the editable mode.
    /// </summary>
    [Category("Appearence"), Description("Gets or sets the control title which is displayed in the editable mode.")]
    public string RegionTitle
    {
        get
        {
            if (mRegionTitle == null)
            {
                mRegionTitle = this.ID;
            }

            return mRegionTitle;
        }
        set
        {
            mRegionTitle = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximum length of the content.
    /// </summary>
    [Category("Behavior"), Description("Gets or sets the maximum length of the content.")]
    public int MaxLength
    {
        get
        {
            return mMaxLength;
        }
        set
        {
            mMaxLength = value;
        }
    }


    /// <summary>
    /// Gets or sets the minimum length of the content.
    /// </summary>
    [Category("Behavior"), Description("Gets or sets the minimum length of the content.")]
    public int MinLength
    {
        get
        {
            return mMinLength;
        }
        set
        {
            mMinLength = value;
        }
    }


    /// <summary>
    /// Gets or sets the height of the control.
    /// </summary>
    [Category("Appearance"), Description("Gets or sets the height of the control.")]
    public int DialogHeight
    {
        get
        {
            return mDialogHeight;
        }
        set
        {
            mDialogHeight = value;
        }
    }


    /// <summary>
    /// Gets or sets the width of the control.
    /// </summary>
    [Category("Appearance"), Description("Gets or sets the width of the control.")]
    public int DialogWidth
    {
        get
        {
            return mDialogWidth;
        }
        set
        {
            mDialogWidth = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the CSS stylesheet used by the control (for HTML area RegionType).
    /// </summary>
    [Category("Appearance"), Description("Gets or sets the name of the CSS stylesheet used by the control (for HTML area RegionType).")]
    public string HTMLEditorCssStylesheet
    {
        get
        {
            return mHTMLEditorCssStylesheet;
        }
        set
        {
            mHTMLEditorCssStylesheet = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether to wrap the text if using text area field.
    /// </summary>
    [Category("Appearance"), Description("Gets or sets the value that indicates whether to wrap the text if using text area field.")]
    public bool WordWrap
    {
        get
        {
            return mWordWrap;
        }
        set
        {
            mWordWrap = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the HTML editor toolbar.
    /// </summary>
    public string HtmlAreaToolbar
    {
        get
        {
            return this.mHtmlAreaToolbar;
        }
        set
        {
            if (value == null)
            {
                this.mHtmlAreaToolbar = "";
            }
            else
            {
                this.mHtmlAreaToolbar = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the location of the HTML editor toolbar.
    /// </summary>
    public string HtmlAreaToolbarLocation
    {
        get
        {
            return this.mHtmlAreaToolbarLocation;
        }
        set
        {
            if (value == null)
            {
                this.mHtmlAreaToolbarLocation = "";
            }
            else
            {
                this.mHtmlAreaToolbarLocation = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), false);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
        }
    }


    /// <summary>
    /// Width the image should be automatically resized to after it is uploaded.
    /// </summary>
    public int ResizeToWidth
    {
        get
        {
            if (!mDimensionsLoaded)
            {
                // Use image auto resize settings
                Hashtable settings = this.ImageAutoResize.ConvertToHashtable();
                ImageHelper.GetAutoResizeDimensions(settings, CMSContext.CurrentSiteName, out mResizeToWidth, out mResizeToHeight, out mResizeToMaxSideSize);
                mDimensionsLoaded = true;
            }
            return mResizeToWidth;
        }
        set
        {
            mResizeToWidth = value;
            mDimensionsLoaded = true;
        }
    }


    /// <summary>
    /// Height the image should be automatically resized to after it is uploaded.
    /// </summary>
    public int ResizeToHeight
    {
        get
        {
            if (!mDimensionsLoaded)
            {
                // Use image auto resize settings
                Hashtable settings = this.ImageAutoResize.ConvertToHashtable();
                ImageHelper.GetAutoResizeDimensions(settings, CMSContext.CurrentSiteName, out mResizeToWidth, out mResizeToHeight, out mResizeToMaxSideSize);
                mDimensionsLoaded = true;
            }
            return mResizeToHeight;
        }
        set
        {
            mResizeToHeight = value;
            mDimensionsLoaded = true;
        }
    }


    /// <summary>
    /// Max side size the image should be automatically resized to after it is uploaded.
    /// </summary>
    public int ResizeToMaxSideSize
    {
        get
        {
            if (!mDimensionsLoaded)
            {
                // Use image auto resize settings
                Hashtable settings = this.ImageAutoResize.ConvertToHashtable();
                ImageHelper.GetAutoResizeDimensions(settings, CMSContext.CurrentSiteName, out mResizeToWidth, out mResizeToHeight, out mResizeToMaxSideSize);
                mDimensionsLoaded = true;
            }
            return mResizeToMaxSideSize;
        }
        set
        {
            mResizeToMaxSideSize = value;
            mDimensionsLoaded = true;
        }
    }


    /// <summary>
    /// Enables or disables resolving of inline controls.
    /// </summary>
    public bool ResolveDynamicControls
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ResolveDynamicControls"), true);
        }
        set
        {
            this.SetValue("ResolveDynamicControls", value);
        }
    }


    /// <summary>
    /// Default text displayed if no content filled.
    /// </summary>
    public string DefaultText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DefaultText"), "");
        }
        set
        {
            SetValue("DefaultText", value);
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Auto resize configuration.
    /// </summary>
    private XmlData ImageAutoResize
    {
        get
        {
            if (mImageAutoResize == null)
            {
                mImageAutoResize = new XmlData("AutoResize");
                mImageAutoResize.LoadData(ValidationHelper.GetString(this.GetValue("ImageAutoResize"), ""));
            }
            return mImageAutoResize;
        }
    }

    #endregion


    /// <summary>
    /// Constructor.
    /// </summary>
    public CMSWebParts_Text_editabletext()
    {
        this.PreRender += new EventHandler(Page_PreRender);
        this.Load += new EventHandler(Page_Load);
    }


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        // Do not hide for roles in edit or preview mode
        switch (this.ViewMode)
        {
            case ViewModeEnum.Edit:
            case ViewModeEnum.EditDisabled:
            case ViewModeEnum.Design:
            case ViewModeEnum.DesignDisabled:
            case ViewModeEnum.EditNotCurrent:
            case ViewModeEnum.Preview:
                this.DisplayToRoles = "";
                break;
        }

        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Load the properties
            this.RegionTitle = DataHelper.GetNotEmpty(this.GetValue("RegionTitle"), this.RegionTitle);
            this.RegionType = CMSEditableRegion.GetRegionType(DataHelper.GetNotEmpty(this.GetValue("RegionType"), this.RegionType.ToString()));
            this.DialogWidth = DataHelper.GetNotZero(this.GetValue("DialogWidth"), this.DialogWidth);
            this.DialogHeight = DataHelper.GetNotZero(this.GetValue("DialogHeight"), this.DialogHeight);
            this.HTMLEditorCssStylesheet = ValidationHelper.GetString(this.GetValue("HtmlEditorCssStylesheet"), this.HTMLEditorCssStylesheet);
            this.WordWrap = ValidationHelper.GetBoolean(this.GetValue("WordWrap"), this.WordWrap);
            this.MinLength = DataHelper.GetNotZero(this.GetValue("MinLength"), this.MinLength);
            this.MaxLength = DataHelper.GetNotZero(this.GetValue("MaxLength"), this.MaxLength);
            this.HtmlAreaToolbar = DataHelper.GetNotEmpty(this.GetValue("HtmlAreaToolbar"), this.HtmlAreaToolbar);
            this.HtmlAreaToolbarLocation = DataHelper.GetNotEmpty(this.GetValue("HtmlAreaToolbarLocation"), this.HtmlAreaToolbarLocation);
        }
    }


    protected void ApplySettings()
    {
        this.EnsureChildControls();

        if (!this.StopProcessing)
        {
            // Create controls by actual page mode
            switch (viewMode)
            {
                case ViewModeEnum.Edit:
                case ViewModeEnum.EditDisabled:
                    // Edit mode
                    if (this.DialogWidth > 0)
                    {
                        this.pnlEditor.Style.Add(HtmlTextWriterStyle.Width, this.DialogWidth.ToString() + "px;");
                    }

                    // Display the region control based on the region type
                    switch (this.RegionType)
                    {
                        case CMSEditableRegionTypeEnum.HtmlEditor:
                            // HTML Editor
                            if (this.DialogWidth > 0)
                            {
                                this.htmlValue.Width = new Unit(this.DialogWidth);
                            }
                            if (this.DialogHeight > 0)
                            {
                                this.htmlValue.Height = new Unit(this.DialogHeight);
                            }

                            // Set toolbar location
                            if (this.HtmlAreaToolbarLocation != "")
                            {
                                // Show the toolbar
                                if (this.HtmlAreaToolbarLocation.ToLower() == "out:cktoolbar")
                                {
                                    mShowToolbar = true;
                                }

                                this.htmlValue.ToolbarLocation = this.HtmlAreaToolbarLocation;
                            }

                            // Set the visual appearrance
                            if (this.HtmlAreaToolbar != "")
                            {
                                this.htmlValue.ToolbarSet = this.HtmlAreaToolbar;
                            }

                            // Get editor area css file
                            if (this.HTMLEditorCssStylesheet != "")
                            {
                                htmlValue.EditorAreaCSS = CSSHelper.GetStylesheetUrl(this.HTMLEditorCssStylesheet);
                            }
                            else if (CMSContext.CurrentSite != null)
                            {
                                htmlValue.EditorAreaCSS = FormHelper.GetHtmlEditorAreaCss(CMSContext.CurrentSiteName);
                            }

                            // Set "Insert image or media" dialog configuration                            
                            htmlValue.MediaDialogConfig.ResizeToHeight = this.ResizeToHeight;
                            htmlValue.MediaDialogConfig.ResizeToWidth = this.ResizeToWidth;
                            htmlValue.MediaDialogConfig.ResizeToMaxSideSize = this.ResizeToMaxSideSize;

                            // Set "Insert link" dialog configuration  
                            htmlValue.LinkDialogConfig.ResizeToHeight = this.ResizeToHeight;
                            htmlValue.LinkDialogConfig.ResizeToWidth = this.ResizeToWidth;
                            htmlValue.LinkDialogConfig.ResizeToMaxSideSize = this.ResizeToMaxSideSize;

                            // Set "Quickly insert image" configuration
                            htmlValue.QuickInsertConfig.ResizeToHeight = this.ResizeToHeight;
                            htmlValue.QuickInsertConfig.ResizeToWidth = this.ResizeToWidth;
                            htmlValue.QuickInsertConfig.ResizeToMaxSideSize = this.ResizeToMaxSideSize;

                            break;

                        case CMSEditableRegionTypeEnum.TextArea:
                        case CMSEditableRegionTypeEnum.TextBox:
                            // TextBox
                            if (this.RegionType == CMSEditableRegionTypeEnum.TextArea)
                            {
                                this.txtValue.TextMode = TextBoxMode.MultiLine;
                            }
                            else
                            {
                                this.txtValue.TextMode = TextBoxMode.SingleLine;
                            }

                            if (this.DialogWidth > 0)
                            {
                                this.txtValue.Width = new Unit(this.DialogWidth - 8);
                            }
                            else
                            {
                                // Default width is 100%
                                this.txtValue.Width = new Unit(100, UnitType.Percentage);
                            }

                            if (this.DialogHeight > 0)
                            {
                                this.txtValue.Height = new Unit(this.DialogHeight);
                            }

                            this.txtValue.Wrap = this.WordWrap;

                            break;
                    }
                    break;
            }
        }
    }


    /// <summary>
    /// Overriden CreateChildControls method.
    /// </summary>
    protected override void CreateChildControls()
    {
        SetupControl();

        this.Controls.Clear();
        base.CreateChildControls();

        if (!this.StopProcessing)
        {
            // Initialize viewmode
            viewMode = this.PageManager.ViewMode;

            // Create controls by actual page mode
            switch (viewMode)
            {
                case ViewModeEnum.Edit:
                case ViewModeEnum.EditDisabled:

                    // Main editor panel
                    this.pnlEditor = new Panel();
                    this.pnlEditor.ID = "pnlEditor";
                    this.pnlEditor.CssClass = "EditableTextEdit EditableText_" + this.ID;
                    this.Controls.Add(pnlEditor);

                    // Title label
                    this.lblTitle = new Label();
                    this.lblTitle.EnableViewState = false;
                    this.lblTitle.CssClass = "EditableTextTitle";
                    this.pnlEditor.Controls.Add(this.lblTitle);

                    // Error label
                    this.lblError = new Label();
                    this.lblError.EnableViewState = false;
                    this.lblError.CssClass = "EditableTextError";
                    this.pnlEditor.Controls.Add(this.lblError);

                    // Display the region control based on the region type
                    switch (this.RegionType)
                    {
                        case CMSEditableRegionTypeEnum.HtmlEditor:
                            // HTML Editor
                            this.htmlValue = new CMSHtmlEditor();
                            this.htmlValue.IsLiveSite = false;
                            this.htmlValue.ID = "htmlValue";
                            this.htmlValue.AutoDetectLanguage = false;
                            this.htmlValue.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

                            // Set direction
                            this.htmlValue.Config["ContentsLangDirection"] = "ltr";

                            if (CultureHelper.IsPreferredCultureRTL())
                            {
                                this.htmlValue.Config["ContentsLangDirection"] = "rtl";
                            }

                            // Set the language
                            try
                            {
                                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo(DataHelper.GetNotEmpty(CMSContext.CurrentUser.PreferredUICultureCode, CMSContext.PreferredCultureCode));
                                this.htmlValue.DefaultLanguage = ci.TwoLetterISOLanguageName;
                            }
                            catch
                            {
                            }

                            this.htmlValue.AutoDetectLanguage = false;
                            this.htmlValue.Enabled = (viewMode == ViewModeEnum.Edit);

                            if (viewMode == ViewModeEnum.EditDisabled)
                            {
                                this.pnlEditor.Controls.Add(new LiteralControl("<div style=\"width: 98%\">"));
                                this.pnlEditor.Controls.Add((Control)this.htmlValue);
                                this.pnlEditor.Controls.Add(new LiteralControl("</div>"));
                            }
                            else
                            {
                                this.pnlEditor.Controls.Add((Control)this.htmlValue);
                            }
                            break;

                        case CMSEditableRegionTypeEnum.TextArea:
                        case CMSEditableRegionTypeEnum.TextBox:
                            // TextBox
                            this.txtValue = new TextBox();
                            this.txtValue.ID = "txtValue";
                            this.txtValue.CssClass = "EditableTextTextBox";

                            this.txtValue.Enabled = (viewMode == ViewModeEnum.Edit);
                            this.pnlEditor.Controls.Add(this.txtValue);
                            break;
                    }
                    break;

                default:
                    // Display content in non editing modes
                    this.ltlContent = new Literal();
                    this.ltlContent.ID = "ltlContent";
                    this.ltlContent.EnableViewState = false;
                    this.Controls.Add(this.ltlContent);
                    break;
            }
        }
    }


    /// <summary>
    /// Loads the control content.
    /// </summary>
    /// <param name="content">Content to load</param>
    /// <param name="forceReload">If true, the content is forced to reload</param>
    public override void LoadContent(string content, bool forceReload)
    {
        if (this.StopProcessing)
        {
        }
        else
        {
            ApplySettings();

            content = ValidationHelper.GetString(content, "");

            // If content empty set default text
            if (String.IsNullOrEmpty(content) && ((viewMode != ViewModeEnum.Edit) && (viewMode != ViewModeEnum.EditDisabled)))
            {
                content = DefaultText;
            }

            // Resolve URLs
            content = HTMLHelper.ResolveUrls(content, null);

            switch (viewMode)
            {
                case ViewModeEnum.Edit:
                case ViewModeEnum.EditDisabled:
                    switch (this.RegionType)
                    {
                        case CMSEditableRegionTypeEnum.HtmlEditor:
                            // HTML editor
                            if ((forceReload || (!RequestHelper.IsPostBack()) || (viewMode != ViewModeEnum.Edit)) && (this.htmlValue != null))
                            {
                                this.htmlValue.ResolvedValue = content;
                            }
                            break;

                        case CMSEditableRegionTypeEnum.TextArea:
                        case CMSEditableRegionTypeEnum.TextBox:
                            // TextBox
                            if ((forceReload || !RequestHelper.IsPostBack()) && (this.txtValue != null))
                            {
                                this.txtValue.Text = content;
                            }
                            break;
                    }
                    break;


                default:
                    // Check authorization
                    bool isAuthorized = true;
                    if (this.CheckPermissions)
                    {
                        isAuthorized = this.PageManager.IsAuthorized;
                    }

                    // Only published
                    if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || !this.SelectOnlyPublished || (this.PagePlaceholder.PageInfo.IsPublished))
                    {
                        if (isAuthorized)
                        {
                            if (ltlContent == null)
                            {
                                ltlContent = (Literal)this.FindControl("ltlContent");
                            }
                            if (ltlContent != null)
                            {
                                this.ltlContent.Text = CMSContext.CurrentResolver.ResolveMacros(content);

                                // Resolve inline controls
                                if (this.ResolveDynamicControls)
                                {
                                    ControlsHelper.ResolveDynamicControls(this);
                                }
                            }
                        }
                    }

                    break;
            }
        }
    }


    /// <summary>
    /// Returns true if the control uses HTML editor.
    /// </summary>
    /// <param name="toolbarLocation">Toolbar location to check</param>
    public override bool UsesHtmlEditor(string toolbarLocation)
    {
        if ((this.RegionType == CMSEditableRegionTypeEnum.HtmlEditor) && (viewMode == ViewModeEnum.Edit))
        {
            return (toolbarLocation == null) || (toolbarLocation == this.HtmlAreaToolbarLocation);
        }
        return false;
    }


    /// <summary>
    /// Returns true if entered data is valid. If data is invalid, it returns false and displays an error message.
    /// </summary>
    public override bool IsValid()
    {
        string textWithOut = "";
        bool mIsValid = true;
        string mError = "";

        switch (viewMode)
        {
            case ViewModeEnum.Edit:
            case ViewModeEnum.EditDisabled:
                switch (this.RegionType)
                {
                    case CMSEditableRegionTypeEnum.HtmlEditor:
                        // HTML editor
                        if (htmlValue != null)
                        {
                            textWithOut = HTMLHelper.StripTags(htmlValue.ResolvedValue);
                            if ((textWithOut.Length > this.MaxLength) && (this.MaxLength > 0))
                            {
                                mError = String.Format(GetString("EditableText.ErrorMax"), textWithOut.Length, this.MaxLength);
                                mIsValid = false;
                            }
                            if ((textWithOut.Length < this.MinLength) && (this.MinLength > 0))
                            {
                                mError = String.Format(GetString("EditableText.ErrorMin"), textWithOut.Length, this.MinLength);
                                mIsValid = false;
                            }
                        }
                        break;

                    case CMSEditableRegionTypeEnum.TextArea:
                    case CMSEditableRegionTypeEnum.TextBox:
                        // TextBox
                        if (this.txtValue != null)
                        {
                            textWithOut = HTMLHelper.StripTags(this.txtValue.Text);
                            if ((textWithOut.Length > this.MaxLength) && (this.MaxLength > 0))
                            {
                                mError = String.Format(GetString("EditableText.ErrorMax"), textWithOut.Length, this.MaxLength);
                                mIsValid = false;
                            }
                            if ((textWithOut.Length < this.MinLength) && (this.MinLength > 0))
                            {
                                mError = String.Format(GetString("EditableText.ErrorMin"), textWithOut.Length, this.MinLength);
                                mIsValid = false;
                            }
                        }
                        break;
                }
                break;
        }

        if (!mIsValid)
        {
            lblError.Text = mError;
        }

        return mIsValid;
    }


    /// <summary>
    /// Gets the current control content.
    /// </summary>
    public override string GetContent()
    {
        if (this.StopProcessing)
        {
        }
        else
        {
            this.EnsureChildControls();

            switch (viewMode)
            {
                case ViewModeEnum.Edit:
                case ViewModeEnum.EditDisabled:
                    switch (this.RegionType)
                    {
                        case CMSEditableRegionTypeEnum.HtmlEditor:
                            // HTML editor
                            if (htmlValue != null)
                            {
                                return htmlValue.ResolvedValue;
                            }
                            break;

                        case CMSEditableRegionTypeEnum.TextArea:
                        case CMSEditableRegionTypeEnum.TextBox:
                            // TextBox
                            if (txtValue != null)
                            {
                                return this.txtValue.Text;
                            }
                            break;
                    }
                    break;
            }
        }

        return null;
    }


    /// <summary>
    /// Returns the arraylist of the field IDs (Client IDs of the inner controls) that should be spell checked.
    /// </summary>
    public override ArrayList GetSpellCheckFields()
    {
        switch (viewMode)
        {
            case ViewModeEnum.Edit:
                ArrayList result = new ArrayList();
                switch (this.RegionType)
                {
                    case CMSEditableRegionTypeEnum.HtmlEditor:
                        // HTML editor
                        if (htmlValue != null)
                        {
                            result.Add(htmlValue.ClientID);
                        }
                        break;

                    case CMSEditableRegionTypeEnum.TextArea:
                    case CMSEditableRegionTypeEnum.TextBox:
                        // TextBox
                        if (txtValue != null)
                        {
                            result.Add(txtValue.ClientID);
                        }
                        break;
                }
                return result;
        }
        return null;
    }


    protected override void OnInit(EventArgs e)
    {
        // Initialize viewmode
        viewMode = this.PageManager.ViewMode;

        base.OnInit(e);
    }


    /// <summary>
    /// Load event handler.
    /// </summary>
    void Page_Load(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    void Page_PreRender(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            // Initialize viewmode
            viewMode = (CMSContext.ViewMode == ViewModeEnum.Preview) ? ViewModeEnum.Preview : this.PageManager.ViewMode;               

            switch (viewMode)
            {
                case ViewModeEnum.Edit:
                case ViewModeEnum.EditDisabled:
                    // Set enabled
                    if (this.htmlValue != null)
                    {
                        this.htmlValue.Enabled = (viewMode == ViewModeEnum.Edit);
                    }
                    if (this.txtValue != null)
                    {
                        this.txtValue.Enabled = (viewMode == ViewModeEnum.Edit);
                    }

                    if (mShowToolbar && (viewMode == ViewModeEnum.Edit))
                    {
                        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), ScriptHelper.TOOLBAR_SCRIPT_KEY, ScriptHelper.ToolbarScript);
                    }

                    if (this.lblError != null)
                    {
                        this.lblError.Visible = (this.lblError.Text != "");
                    }

                    this.lblTitle.Text = this.RegionTitle;
                    this.lblTitle.Visible = (this.lblTitle.Text != "");

                    // Allow to select text in the source editor area
                    if (pnlDesign != null)
                    {
                        ScriptHelper.RegisterStartupScript(this, typeof(string), "onselectstart", "document.getElementById('" + pnlDesign.ClientID + "').parentNode.onselectstart = function() { return true; };", true);
                    }

                    break;
            }
        }
    }
}
