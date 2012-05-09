using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.PortalEngine;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSWebParts_Text_editableimage : CMSAbstractEditableWebPart, IDialogControl
{
    #region "Variables"

    ViewModeEnum viewMode = ViewModeEnum.Unknown;

    protected const int NOT_KOWN = -1;
    protected XmlData mImageAutoResize = null;

    protected int mResizeToWidth = 0;
    protected int mResizeToHeight = 0;
    protected int mResizeToMaxSideSize = 0;
    protected bool mDimensionsLoaded = false;

    #endregion


    #region "Controls"

    /// <summary>
    /// Image.
    /// </summary>
    protected Image imgImage = null;

    /// <summary>
    /// Region title.
    /// </summary>
    protected Label lblTitle = null;

    /// <summary>
    /// Error label.
    /// </summary>
    protected Label lblError = null;

    /// <summary>
    /// Region panel.
    /// </summary>
    protected Panel pnlEditor = null;

    /// <summary>
    /// Image selector.
    /// </summary>
    protected ImageSelector selPath = null;

    /// <summary>
    /// Image title.
    /// </summary>
    protected string mImageTitle = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Configuration of the dialog for inserting Images.
    /// </summary>
    public DialogConfiguration DialogConfig
    {
        get
        {
            return selPath.DialogConfig;
        }
        set
        {
            selPath.DialogConfig = value;
        }
    }


    /// <summary>
    /// Gets or sets the title of the image region which is displayed in the EDIT mode.
    /// </summary>
    public string ImageTitle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ImageTitle"), this.ID);
        }
        set
        {
            this.SetValue("ImageTitle", value);
        }
    }


    /// <summary>
    /// Gets or sets the image width.
    /// </summary>
    public int ImageWidth
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("ImageWidth"), 0);
        }
        set
        {
            this.SetValue("ImageWidth", value);
        }
    }


    /// <summary>
    /// Gets or sets the image height.
    /// </summary>
    public int ImageHeight
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("ImageHeight"), 0);
        }
        set
        {
            this.SetValue("ImageHeight", value);
        }
    }


    /// <summary>
    /// Gets or sets the alternative image text (ALT tag of the image).
    /// </summary>
    public string AlternateText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AlternateText"), "");
        }
        set
        {
            this.SetValue("AlternateText", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the path to file is displayed in EDIT mode.
    /// </summary>
    public bool DisplaySelectorTextBox
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplaySelectorTextBox"), this.selPath.ImagePathTextBox.Visible);
        }
        set
        {
            this.SetValue("DisplaySelectorTextBox", value);
        }
    }


    /// <summary>
    /// Gets or sets the name of the css class applied to the image.
    /// </summary>
    public string ImageCssClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ImageCssClass"), "");
        }
        set
        {
            this.SetValue("ImageCssClass", value);
        }
    }


    /// <summary>
    /// Gets or sets the style tag of the image.
    /// </summary>
    public string ImageStyle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ImageStyle"), "");
        }
        set
        {
            this.SetValue("ImageStyle", value);
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
    /// Default image dispaled when no image is selected.
    /// </summary>
    public string DefaultImage
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DefaultImage"), "");
        }
        set
        {
            SetValue("DefaultImage", value);
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Autoresize configuration for the image.
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
        }
    }


    protected override void OnInit(EventArgs e)
    {
        // Initialize viewmode
        viewMode = this.PageManager.ViewMode;

        base.OnInit(e);
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    public CMSWebParts_Text_editableimage()
    {
        this.PreRender += new EventHandler(Page_PreRender);
        this.Load += new EventHandler(Page_Load);
    }


    /// <summary>
    /// Overriden CreateChildControls method.
    /// </summary>
    protected override void CreateChildControls()
    {
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
                    this.pnlEditor.CssClass = "EditableImageEdit EditableImage_" + this.ID;
                    if (this.ImageWidth > 0)
                    {
                        this.pnlEditor.Style.Add(HtmlTextWriterStyle.Width, this.ImageWidth.ToString() + "px;");
                        //this.pnlEditor.Width = new Unit(this.DialogWidth); // Causes Invalid cast on Render
                    }
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

                    // Add image selector
                    this.selPath = new ImageSelector(null, true);
                    this.selPath.Culture = CMSContext.CurrentUser.PreferredUICultureCode;
                    this.selPath.EnableOpenInFull = false;
                    this.selPath.ID = "selPath";
                    this.selPath.UseImagePath = true;
                    this.selPath.ImageCssClass = this.ImageCssClass;
                    this.selPath.ImageStyle = this.ImageStyle;
                    this.selPath.ShowTextBox = this.DisplaySelectorTextBox;
                    this.selPath.DefaultValue = this.DefaultImage;

                    // Dialog configuration
                    this.selPath.DialogConfig.ResizeToHeight = this.ResizeToHeight;
                    this.selPath.DialogConfig.ResizeToWidth = this.ResizeToWidth;
                    this.selPath.DialogConfig.ResizeToMaxSideSize = this.ResizeToMaxSideSize;

                    this.pnlEditor.Controls.Add(this.selPath);

                    this.selPath.Enabled = (viewMode == ViewModeEnum.Edit);
                    this.selPath.IsLiveSite = (viewMode == ViewModeEnum.LiveSite);
                    break;

                default:
                    // Display content in non editing modes
                    this.imgImage = new Image();
                    this.imgImage.ID = "imgImage";
                    this.imgImage.GenerateEmptyAlternateText = true;
                    if (this.ImageCssClass != "")
                    {
                        this.imgImage.CssClass = this.ImageCssClass;
                    }
                    if (this.ImageStyle != "")
                    {
                        this.imgImage.Attributes.Add("style", this.ImageStyle);
                    }

                    this.imgImage.AlternateText = this.AlternateText;
                    this.imgImage.ToolTip = this.AlternateText;
                    this.imgImage.EnableViewState = false;
                    this.Controls.Add(this.imgImage);
                    break;
            }
        }
    }


    /// <summary>
    /// Load the content to the region, applies the InheritContent.
    /// </summary>
    /// <param name="pageInfo">PageInfo with the content data</param>
    /// <param name="forceReload">If true, the content is forced to reload</param>
    public override void LoadContent(PageInfo pageInfo, bool forceReload)
    {
        if (pageInfo == null)
        {
            return;
        }

        // Get the content
        PageInfo sourceInfo = pageInfo;

        // Prepare the ID
        string id = this.PartInstance.ControlID.ToLower();
        if (this.InstanceGUID != Guid.Empty)
        {
            id += ";" + this.InstanceGUID.ToString().ToLower();
            if (this.PartInstance.CurrentVariantInstance != null)
            {
                id += "(" + this.PartInstance.CurrentVariantInstance.ControlID + ")";
            }
            else if (this.IsWidget && this.IsVariant)
            {
                id += "(" + this.PartInstance.ControlID + ")";
            }
        }

        string content = ValidationHelper.GetString(pageInfo.EditableWebParts[id], "");

        // Load the content if published
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || !this.SelectOnlyPublished || ((sourceInfo != null) && sourceInfo.IsPublished))
        {
            LoadContent(content, forceReload | PortalContext.MVTCombinationPanelChanged);
        }
    }


    /// <summary>
    /// Loads the control content.
    /// </summary>
    /// <param name="content">Content to load</param>
    /// <param name="forceReload">If true, the content is forced to reload</param>
    public override void LoadContent(string content, bool forceReload)
    {
        if (!this.StopProcessing)
        {
            // Load the properties
            this.EnsureChildControls();

            string path = null;
            // Load the image data
            if (!string.IsNullOrEmpty(content))
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(content);

                XmlNodeList properties = xml.SelectNodes("image/property");
                if (properties != null)
                {
                    foreach (XmlNode node in properties)
                    {
                        if (node.Attributes["name"] != null)
                        {
                            switch (node.Attributes["name"].Value.ToLower())
                            {
                                case "imagepath":
                                    path = ResolveUrl(node.InnerText.Trim());
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                // Ensure correct url from media selector
                path = Server.HtmlDecode(DefaultImage);
            }

            switch (viewMode)
            {
                case ViewModeEnum.Edit:
                case ViewModeEnum.EditDisabled:
                    // Force image width    
                    if (this.ImageWidth > 0)
                    {
                        selPath.ImageWidth = this.ImageWidth;
                        selPath.ImagePreviewControl.Width = this.ImageWidth;
                    }

                    // Force image height
                    if (this.ImageHeight > 0)
                    {
                        selPath.ImageHeight = this.ImageHeight;
                        selPath.ImagePreviewControl.Height = this.ImageHeight;
                    }

                    // Initialize selected value
                    if (forceReload || !RequestHelper.IsPostBack())
                    {
                        this.selPath.Value = path;
                    }

                    // Set image title
                    if (this.ImageTitle != "")
                    {
                        this.lblTitle.Text = this.ImageTitle;
                    }
                    else
                    {
                        this.lblTitle.Visible = false;
                    }

                    break;

                default:
                    if (string.IsNullOrEmpty(path))
                    {
                        this.Visible = false;
                        return;
                    }

                    // Force image width
                    if (this.ImageWidth > 0)
                    {
                        imgImage.Width = ImageWidth;
                        path = URLHelper.AddParameterToUrl(path, "width", this.ImageWidth.ToString());
                    }

                    // Force image height
                    if (this.ImageHeight > 0)
                    {
                        imgImage.Height = ImageHeight;
                        path = URLHelper.AddParameterToUrl(path, "height", this.ImageHeight.ToString());
                    }
                    imgImage.AlternateText = this.AlternateText;

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
                            imgImage.ImageUrl = path;
                        }
                        else
                        {
                            imgImage.Visible = false;
                        }
                    }
                    else
                    {
                        imgImage.Visible = false;
                    }

                    break;
            }
        }
    }


    /// <summary>
    /// Gets the current control content.
    /// </summary>
    public override string GetContent()
    {
        if (!this.StopProcessing)
        {
            this.EnsureChildControls();

            switch (viewMode)
            {
                case ViewModeEnum.Edit:
                case ViewModeEnum.EditDisabled:
                    string path = URLHelper.UnResolveUrl(this.selPath.Value.Trim(), URLHelper.ApplicationPath);
                    if (!string.IsNullOrEmpty(path))
                    {
                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml("<image></image>");

                        // Add path
                        XmlNode newNode = xml.CreateElement("property");

                        XmlAttribute attr = xml.CreateAttribute("name");
                        attr.Value = "imagepath";
                        newNode.Attributes.Append(attr);


                        newNode.InnerText = path;
                        if (xml.DocumentElement != null)
                        {
                            xml.DocumentElement.AppendChild(newNode);
                        }
                        return xml.OuterXml;
                    }
                    else
                    {
                        return string.Empty;
                    }
            }
        }

        return null;
    }


    /// <summary>
    /// Load event handler.
    /// </summary>
    void Page_Load(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
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
                    if (this.selPath != null)
                    {
                        this.selPath.Enabled = (viewMode == ViewModeEnum.Edit);
                    }

                    if (this.lblError != null)
                    {
                        this.lblError.Visible = (this.lblError.Text != "");
                    }
                    this.lblTitle.Text = this.ImageTitle;
                    break;
            }
        }
    }
}
