using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Dialogs_Properties_URLProperties : ItemProperties
{
    #region "Private properties"

    /// <summary>
    /// Indicates whether the properties are used for path selector form control.
    /// </summary>
    private bool IsSelectPath
    {
        get
        {
            return (this.Config.CustomFormatCode == "selectpath");
        }
    }


    /// <summary>
    /// Indicates whether the properties are used for single path selector form control.
    /// </summary>
    private bool IsSelectSinglePath
    {
        get
        {
            return (QueryHelper.GetString("selectionmode", "") == "single") && this.IsSelectPath;
        }
    }


    /// <summary>
    /// Indicates whether the properties are used for select relationship path.
    /// </summary>
    private bool IsRelationship
    {
        get
        {
            return (this.Config.CustomFormatCode == "relationship");
        }
    }


    /// <summary>
    /// Gets or sets the original URL (the one which come when properties are loaded).
    /// </summary>
    private string OriginalUrl
    {
        get
        {
            return ValidationHelper.GetString(ViewState["OriginalUrl"], "");
        }
        set
        {
            ViewState["OriginalUrl"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the permanent URL.
    /// </summary>
    private string PermanentUrl
    {
        get
        {
            return ValidationHelper.GetString(ViewState["PermanentUrl"], "");
        }
        set
        {
            ViewState["PermanentUrl"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show or hide the controls in WidthHeightSelector.
    /// </summary>
    private bool ShowSizeControls
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["ShowSizeControls"], false);
        }
        set
        {
            ViewState["ShowSizeControls"] = value;
        }
    }


    /// <summary>
    /// Indicates whether the properties were not loaded yet.
    /// </summary>
    private bool NoSelectedYet
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["NoSelectedYet"], true);
        }
        set
        {
            ViewState["NoSelectedYet"] = value;
        }
    }


    /// <summary>
    /// Returns the default width of the width height selector.
    /// </summary>
    private int DefaultWidth
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["DefaultHeight"], 0);
        }
        set
        {
            ViewState["DefaultHeight"] = value;
        }
    }


    /// <summary>
    /// Returns the default height of the width height selector.
    /// </summary>
    private int DefaultHeight
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["DefaultWidth"], 0);
        }
        set
        {
            ViewState["DefaultWidth"] = value;
        }
    }


    /// <summary>
    /// Indicates whether the current item is image.
    /// </summary>
    private bool CurrentIsImage
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["CurrentIsImage"], false);
        }
        set
        {
            ViewState["CurrentIsImage"] = value;
        }
    }


    /// <summary>
    /// Indicates whether the current item is audio/video/flash.
    /// </summary>
    private bool CurrentIsMedia
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["CurrentIsMedia"], false);
        }
        set
        {
            ViewState["CurrentIsMedia"] = value;
        }
    }


    /// <summary>
    /// Indicates whether the properties are displayed for the media selector.
    /// </summary>
    private bool IsMediaSelector
    {
        get
        {
            return ((this.Config.OutputFormat == OutputFormatEnum.URL) && (this.Config.SelectableContent == SelectableContentEnum.AllFiles));
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value which determines whether the control is displayed on the Web tab.
    /// </summary>
    public bool IsWeb
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsWeb"], false);
        }
        set
        {
            ViewState["IsWeb"] = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            if (this.Config != null)
            {
                if ((this.Config.OutputFormat == OutputFormatEnum.URL) && this.IsWeb)
                {
                    this.plcPropContent.Visible = false;
                }

                if ((this.Config.OutputFormat == OutputFormatEnum.URL) && (this.Config.SelectableContent == SelectableContentEnum.AllContent))
                {
                    DisplaySizeSelector(false);
                    this.pnlImagePreview.CssClass = "DialogPropertiesPreview DialogPropertiesPreviewFull";
                    this.pnlMediaPreview.CssClass = "DialogPropertiesPreview DialogPropertiesPreviewFull";
                }
            }

            // Refresh button
            this.imgRefresh.ImageUrl = GetImageUrl("Design/Controls/Dialogs/refresh.png");
            this.imgRefresh.ToolTip = GetString("dialogs.web.refresh");

            this.tabImageGeneral.HeaderText = GetString("general.general");

            string postBackRef = ControlsHelper.GetPostBackEventReference(this.btnHidden, "");
            string postBackKeyDownRef = "var keynum;if(window.event){keynum = event.keyCode;}else if(event.which){keynum = event.which;}if(keynum == 13){" + postBackRef + "; return false;}";
            string postBackRefTxt = ControlsHelper.GetPostBackEventReference(this.btnTxtHidden, "");
            string postBackKeyDownTxtRef = "var keynum;if(window.event){keynum = event.keyCode;}else if(event.which){keynum = event.which;}if(keynum == 13){" + postBackRefTxt + "; return false;}";

            // Assign javascript change event to all fields (to refresh the preview)
            this.widthHeightElem.TextBoxesClass = "ShortTextBox";
            this.widthHeightElem.HeightTextBox.Attributes["onchange"] = postBackRef;
            this.widthHeightElem.HeightTextBox.Attributes["onkeydown"] = postBackKeyDownRef;
            this.widthHeightElem.WidthTextBox.Attributes["onchange"] = postBackRef;
            this.widthHeightElem.WidthTextBox.Attributes["onkeydown"] = postBackKeyDownRef;
            this.txtUrl.Attributes["onchange"] = postBackRefTxt;
            this.txtUrl.Attributes["onkeydown"] = postBackKeyDownRef;

            this.btnHidden.Click += new EventHandler(btnHidden_Click);
            this.btnTxtHidden.Click += new EventHandler(btnTxtHidden_Click);
            this.btnHiddenSize.Click += new EventHandler(btnHiddenSize_Click);

            if (this.IsSelectPath || this.IsRelationship)
            {
                this.plcUrl.Visible = false;
                this.plcSizeSelectorHeight.Visible = false;
                this.plcPreviewArea.Visible = false;

                this.plcSelectPath.Visible = true;

                string resString = (this.IsSelectPath ? "dialogs.web.selectedpath" : "dialogs.web.selecteddoc");
                this.lblSelectPah.ResourceString = resString;
                this.lblSelectPah.DisplayColon = true;
            }

            if (this.IsSelectPath && !this.IsSelectSinglePath)
            {
                this.plcIncludeSubitems.Visible = true;
                this.chkItems.Attributes.Add("onclick", "chkItemsChecked_Changed(this.checked);");
            }

            LoadPreview();
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!URLHelper.IsPostback())
        {
            this.widthHeightElem.Locked = true;
        }

        this.widthHeightElem.CustomRefreshCode = ControlsHelper.GetPostBackEventReference(this.btnHiddenSize, "") + "; return false;";
        this.widthHeightElem.ShowActions = this.ShowSizeControls;

        bool isLink = ((this.Config.OutputFormat == OutputFormatEnum.BBLink) || (this.Config.OutputFormat == OutputFormatEnum.HTMLLink) ||
            ((this.Config.OutputFormat == OutputFormatEnum.URL) && ((this.Config.SelectableContent == SelectableContentEnum.AllContent) || (this.Config.SelectableContent == SelectableContentEnum.OnlyFlash))));

        DisplaySizeSelector(this.ShowSizeControls && !isLink);

        this.lblEmpty.Text = this.NoSelectionText;
    }


    /// <summary>
    /// Update parameters and preview from URL textbox.
    /// </summary>
    private void UpdateFromUrl()
    {
        int width = ValidationHelper.GetInteger(URLHelper.GetUrlParameter(this.txtUrl.Text, "width"), 0);
        int height = ValidationHelper.GetInteger(URLHelper.GetUrlParameter(this.txtUrl.Text, "height"), 0);

        // Update WIDTH and HEIGHT information according URL
        if (width > 0) this.widthHeightElem.Width = width;
        if (height > 0) this.widthHeightElem.Height = height;

        LoadPreview();
    }


    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        UpdateFromUrl();
    }


    protected void btnTxtHidden_Click(object sender, EventArgs e)
    {
        UpdateFromUrl();
    }


    protected void btnHiddenSize_Click(object sender, EventArgs e)
    {
        this.widthHeightElem.Width = this.DefaultWidth;
        this.widthHeightElem.Height = this.DefaultHeight;

        // Remove width & height parameters from url
        string url = URLHelper.RemoveParameterFromUrl(URLHelper.RemoveParameterFromUrl(this.OriginalUrl, "width"), "height");

        // If media selector - insert dimensions to the URL
        if (this.IsMediaSelector)
        {
            url = EnsureMediaSelector(url);
        }

        this.txtUrl.Text = url;

        LoadPreview();
    }


    protected void btnHidden_Click(object sender, EventArgs e)
    {
        // Update item URL
        bool getPermanent = ((this.widthHeightElem.Width < this.DefaultWidth) ||
                            (this.widthHeightElem.Height < this.DefaultHeight)) &&
                            (this.SourceType == MediaSourceEnum.MediaLibraries);

        string url = UpdateUrl(this.widthHeightElem.Width, this.widthHeightElem.Height, (getPermanent ? this.PermanentUrl : this.OriginalUrl));

        // If media selector - insert dimensions to the URL
        if (this.IsMediaSelector)
        {
            url = EnsureMediaSelector(url);
        }

        this.txtUrl.Text = url;

        LoadPreview();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns URL updated according specified properties.
    /// </summary>
    /// <param name="height">Height of the item</param>
    /// <param name="width">Width of the item</param>
    /// <param name="url">URL to update</param>
    private string UpdateUrl(int width, int height, string url)
    {
        // WIDTH & HEIGHT isn't required in URL
        if (((this.Config.OutputFormat == OutputFormatEnum.URL) && !this.CurrentIsImage) && !(this.IsMediaSelector && this.ShowSizeControls))
        {
            return url;
        }

        // Media selector always put dimensions in the URL when size selector is visible
        bool forceSizeToUrl = (this.IsMediaSelector && this.ShowSizeControls);

        return CMSDialogHelper.UpdateUrl(width, height, this.DefaultWidth, this.DefaultHeight, url, this.SourceType, forceSizeToUrl);
    }


    /// <summary>
    /// Performs additional actions for the media selector.
    /// </summary>
    /// <param name="url">Base item URL</param>
    private string EnsureMediaSelector(string url)
    {
        if (this.ShowSizeControls)
        {
            url = CMSDialogHelper.UpdateUrl(this.widthHeightElem.Width, this.widthHeightElem.Height, 0, 0, url, this.SourceType, true);
        }

        return EnsureExtension(this.txtUrl.Text, url);
    }


    /// <summary>
    /// Ensures that URL contains extension as required.
    /// </summary>
    /// <param name="refUrl">Original URL possibly containing extension</param>
    /// <param name="updateUrl">URL to ensure extension for</param>
    private string EnsureExtension(string refUrl, string updateUrl)
    {
        string ext = URLHelper.GetUrlParameter(refUrl, "ext");
        if (!string.IsNullOrEmpty(ext))
        {
            updateUrl = URLHelper.UpdateParameterInUrl(updateUrl, "ext", ext);
        }

        return updateUrl;
    }


    /// <summary>
    /// Configures the preview control.
    /// </summary>
    private void LoadPreview()
    {
        this.pnlImagePreview.Visible = this.CurrentIsImage;
        this.pnlMediaPreview.Visible = (!this.CurrentIsImage && this.CurrentIsMedia);
        if (this.CurrentIsImage)
        {
            string url = this.txtUrl.Text.Trim();
            if (!string.IsNullOrEmpty(url))
            {
                url = URLHelper.UpdateParameterInUrl(url, "chset", Guid.NewGuid().ToString());
            }

            // Add latest version requirement for live site
            if (IsLiveSite)
            {
                // Add requirement for latest version of files for current document
                string newparams = "latestforhistoryid=" + this.HistoryID;
                newparams += "&hash=" + ValidationHelper.GetHashString("h" + this.HistoryID);

                url += "&" + newparams;
            }

            this.imagePreview.URL = url;
            this.imagePreview.SizeToURL = ValidationHelper.GetBoolean(ViewState[DialogParameters.IMG_SIZETOURL], false);
            this.imagePreview.Width = this.widthHeightElem.Width;
            this.imagePreview.Height = this.widthHeightElem.Height;
        }
        else
        {
            string url = this.txtUrl.Text.Trim();
            string ext = ValidationHelper.GetString(ViewState[DialogParameters.URL_EXT], "");
            if (!String.IsNullOrEmpty(ext))
            {
                url = URLHelper.UpdateParameterInUrl(url, "ext", "." + ext.TrimStart('.'));
            }
            this.mediaPreview.Url = url;

            if (this.CurrentIsMedia && ((this.widthHeightElem.Width == 0) || this.widthHeightElem.Height == 0))
            {
                this.widthHeightElem.Width = 300;
                this.widthHeightElem.Height = GetDefaultAVHeight(ext);
            }

            if (MediaHelper.IsFlash(ext))
            {
                this.mediaPreview.AutoPlay = true;
            }
            else if (MediaHelper.IsAudioVideo(ext))
            {
                this.mediaPreview.AVControls = true;
            }

            this.mediaPreview.Width = this.widthHeightElem.Width;
            this.mediaPreview.Height = this.widthHeightElem.Height;
        }

        // Ensure extension is at the end of URL
        string urlExt = URLHelper.GetUrlParameter(this.txtUrl.Text, "ext");
        if (!string.IsNullOrEmpty(urlExt))
        {
            this.txtUrl.Text = URLHelper.UpdateParameterInUrl(this.txtUrl.Text, "ext", urlExt);
        }

        SaveSession();
    }


    /// <summary>
    /// Save current properties into session.
    /// </summary>
    private void SaveSession()
    {
        Hashtable savedProperties = SessionHelper.GetValue("DialogSelectedParameters") as Hashtable;
        if (savedProperties == null)
        {
            savedProperties = new Hashtable();
        }
        Hashtable properties = GetItemProperties();
        foreach (DictionaryEntry entry in properties)
        {
            savedProperties[entry.Key] = entry.Value;
        }
        SessionHelper.SetValue("DialogSelectedParameters", savedProperties);
    }


    /// <summary>
    /// Show or hide size selector.
    /// </summary>
    /// <param name="display">Indicates if size selector should be displayed</param>
    private void DisplaySizeSelector(bool display)
    {
        if (display)
        {
            this.plcSizeSelectorWidth.Visible = true;
            this.plcSizeSelectorHeight.Visible = true;
            this.previewTd.Attributes["rowspan"] = "2";
            this.previewTd.Attributes["colspan"] = "1";
        }
        else
        {
            this.plcSizeSelectorWidth.Visible = false;
            this.plcSizeSelectorHeight.Visible = false;
            this.previewTd.Attributes["rowspan"] = "1";
            this.previewTd.Attributes["colspan"] = "3";
        }
    }

    #endregion


    #region "Overriden methods"

    public override void LoadSelectedItems(MediaItem item, Hashtable properties)
    {
        if (properties == null)
        {
            properties = new Hashtable();
        }

        string url = item.Url;
        this.OriginalUrl = item.Url;
        this.PermanentUrl = item.PermanentUrl;
        this.HistoryID = item.HistoryID;

        switch (item.MediaType)
        {
            case MediaTypeEnum.Image:
                properties[DialogParameters.IMG_WIDTH] = item.Width;
                properties[DialogParameters.IMG_HEIGHT] = item.Height;
                properties[DialogParameters.IMG_ORIGINALWIDTH] = item.Width;
                properties[DialogParameters.IMG_ORIGINALHEIGHT] = item.Height;
                break;
            case MediaTypeEnum.AudioVideo:
                properties[DialogParameters.AV_WIDTH] = 400;
                properties[DialogParameters.AV_HEIGHT] = 300;
                properties[DialogParameters.AV_CONTROLS] = true;
                break;
            case MediaTypeEnum.Flash:
                properties[DialogParameters.FLASH_WIDTH] = item.Width;
                properties[DialogParameters.FLASH_HEIGHT] = item.Height;
                properties[DialogParameters.FLASH_AUTOPLAY] = true;
                url = URLHelper.UpdateParameterInUrl(url, "ext", "." + item.Extension.TrimStart('.'));
                break;
            case MediaTypeEnum.Unknown:
            default:
                break;
        }

        ViewState[DialogParameters.URL_EXT] = item.Extension;
        properties[DialogParameters.URL_EXT] = item.Extension;
        properties[DialogParameters.URL_URL] = url;

        ViewState[DialogParameters.DOC_TARGETNODEID] = item.NodeID;
        properties[DialogParameters.DOC_TARGETNODEID] = item.NodeID;
        properties[DialogParameters.DOC_NODEALIASPATH] = item.AliasPath;

        LoadProperties(properties);

        if (tabImageGeneral.Visible)
        {
            LoadPreview();
        }
    }


    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection with properties</param>
    public override void LoadItemProperties(Hashtable properties)
    {
        LoadProperties(properties);
        if (tabImageGeneral.Visible)
        {
            LoadPreview();
        }
    }


    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection with properties</param>
    public override void LoadProperties(Hashtable properties)
    {
        if (properties != null)
        {
            // Display the properties
            this.pnlEmpty.Visible = false;
            this.pnlTabs.CssClass = "Dialog_Tabs";

            #region "Image general tab"

            // Display size selector only if required or image
            string ext = ValidationHelper.GetString(properties[DialogParameters.URL_EXT], "");
            this.CurrentIsImage = ImageHelper.IsImage(ext);
            this.CurrentIsMedia = !this.CurrentIsImage ? (MediaHelper.IsAudioVideo(ext) || MediaHelper.IsFlash(ext)) : false;
            this.ShowSizeControls = DisplaySizeSelector();

            if (tabImageGeneral.Visible)
            {
                string url = HttpUtility.HtmlDecode(ValidationHelper.GetString(properties[DialogParameters.URL_URL], ""));
                if (url != "")
                {
                    this.OriginalUrl = url;
                }

                if ((this.Config.SelectableContent == SelectableContentEnum.OnlyMedia) || this.IsMediaSelector)
                {
                    url = URLHelper.UpdateParameterInUrl(url, "ext", "." + ext.TrimStart('.'));
                }

                if (this.CurrentIsImage)
                {
                    int width = ValidationHelper.GetInteger(properties[DialogParameters.IMG_WIDTH], 0);
                    int height = ValidationHelper.GetInteger(properties[DialogParameters.IMG_HEIGHT], 0);

                    this.DefaultWidth = ValidationHelper.GetInteger(properties[DialogParameters.IMG_ORIGINALWIDTH], 0);
                    this.DefaultHeight = ValidationHelper.GetInteger(properties[DialogParameters.IMG_ORIGINALHEIGHT], 0);

                    // Ensure WIDTH & HEIGHT dimensions
                    if (width == 0)
                    {
                        width = ValidationHelper.GetInteger(URLHelper.GetUrlParameter(url, "width"), 0);
                        if (width == 0)
                        {
                            width = this.DefaultWidth;
                        }
                    }
                    if (height == 0)
                    {
                        height = ValidationHelper.GetInteger(URLHelper.GetUrlParameter(url, "height"), 0);
                        if (height == 0)
                        {
                            height = this.DefaultHeight;
                        }
                    }

                    this.widthHeightElem.Width = width;
                    this.widthHeightElem.Height = height;

                    ViewState[DialogParameters.IMG_SIZETOURL] = ValidationHelper.GetBoolean(properties[DialogParameters.IMG_SIZETOURL], false);
                }
                else
                {
                    this.DefaultWidth = 300;
                    this.DefaultHeight = GetDefaultAVHeight(ext);

                    this.widthHeightElem.Width = this.DefaultWidth;
                    this.widthHeightElem.Height = this.DefaultHeight;
                }

                // If media selector - insert dimensions to the URL
                if (this.IsMediaSelector && this.ShowSizeControls)
                {
                    url = CMSDialogHelper.UpdateUrl(this.widthHeightElem.Width, this.widthHeightElem.Height, 0, 0, url, this.SourceType, true);
                }
                this.txtUrl.Text = url;

                // Initialize media file URLs
                if (this.SourceType == MediaSourceEnum.MediaLibraries)
                {
                    this.OriginalUrl = (string.IsNullOrEmpty(this.OriginalUrl) ? ValidationHelper.GetString(properties[DialogParameters.URL_DIRECT], "") : this.OriginalUrl);
                    this.PermanentUrl = (string.IsNullOrEmpty(this.PermanentUrl) ? ValidationHelper.GetString(properties[DialogParameters.URL_PERMANENT], "") : this.PermanentUrl);
                }
                else
                {
                    this.OriginalUrl = url;
                }
            }

            #endregion

            #region "General items"

            ViewState[DialogParameters.URL_EXT] = (properties[DialogParameters.URL_EXT] != null ? ValidationHelper.GetString(properties[DialogParameters.URL_EXT], "") : ValidationHelper.GetString(properties[DialogParameters.IMG_EXT], ""));
            ViewState[DialogParameters.URL_URL] = ValidationHelper.GetString(properties[DialogParameters.URL_URL], "");
            this.EditorClientID = ValidationHelper.GetString(properties[DialogParameters.EDITOR_CLIENTID], "");

            #endregion

            #region "Select path & Relationship items"

            if (this.IsRelationship || this.IsSelectPath)
            {
                this.txtSelectPath.Text = ValidationHelper.GetString(properties[DialogParameters.DOC_NODEALIASPATH], "");

                if (this.chkItems.Visible)
                {
                    if (this.NoSelectedYet)
                    {
                        this.chkItems.Checked = true;
                        this.NoSelectedYet = false;
                    }

                    if (this.chkItems.Checked) 
                    {
                        this.txtSelectPath.Text = this.txtSelectPath.Text.TrimEnd('/') + "/%";
                    }
                }                
            }

            #endregion

        }
    }


    /// <summary>
    /// Decides whether the size selector should be displayed.
    /// </summary>
    /// <param name="isImage">Is current file image</param>
    /// <param name="isMedia">Is current file media</param>
    private bool DisplaySizeSelector()
    {
        bool result = false;

        // Start with media selector
        result = result || (this.IsMediaSelector && (this.CurrentIsImage || this.CurrentIsMedia));

        // Is image selector ?
        result = result || ((this.Config.OutputFormat == OutputFormatEnum.URL) && (this.Config.SelectableContent == SelectableContentEnum.OnlyImages));

        return result;
    }


    /// <summary>
    /// Returns default height for the A/V items.
    /// </summary>
    /// <param name="ext">Extension of the file</param>
    private int GetDefaultAVHeight(string ext)
    {
        // Audio default height = 45, video = 200
        return (MediaHelper.IsAudio(ext) ? 45 : 200);
    }


    /// <summary>
    /// Returns all parameters of the selected item as name – value collection.
    /// </summary>
    public override Hashtable GetItemProperties()
    {
        Hashtable retval = new Hashtable();

        #region "Image general tab"

        string ext = ValidationHelper.GetString(ViewState[DialogParameters.URL_EXT], "");
        string url = ValidationHelper.GetString(ViewState[DialogParameters.URL_URL], "");

        if (!(this.IsRelationship || this.IsSelectPath))
        {

            bool resolveUrl = !((this.Config.OutputFormat == OutputFormatEnum.URL) && (this.Config.SelectableContent == SelectableContentEnum.OnlyMedia));

            // Exception for MediaSelector control (it can't be resolved)
            url = (resolveUrl ? URLHelper.ResolveUrl(url) : url);

            if (MediaHelper.IsFlash(ext))
            {
                retval[DialogParameters.FLASH_URL] = this.txtUrl.Text.Trim();
            }
            else if (MediaHelper.IsAudioVideo(ext))
            {
                retval[DialogParameters.AV_URL] = this.txtUrl.Text.Trim();
            }
            else if (tabImageGeneral.Visible)
            {
                string imgUrl = this.txtUrl.Text.Trim();
                bool sizeToUrl = ValidationHelper.GetBoolean(ViewState[DialogParameters.IMG_SIZETOURL], false);
                if ((this.widthHeightElem.Width != this.DefaultWidth) && sizeToUrl)
                {
                    imgUrl = URLHelper.UpdateParameterInUrl(imgUrl, "width", this.widthHeightElem.Width.ToString());
                }
                if ((this.widthHeightElem.Height != this.DefaultHeight) && sizeToUrl)
                {
                    imgUrl = URLHelper.UpdateParameterInUrl(imgUrl, "height", this.widthHeightElem.Height.ToString());
                }
                retval[DialogParameters.IMG_HEIGHT] = this.widthHeightElem.Height;
                retval[DialogParameters.IMG_WIDTH] = this.widthHeightElem.Width;
                retval[DialogParameters.IMG_URL] = (resolveUrl ? URLHelper.ResolveUrl(imgUrl) : imgUrl);
                retval[DialogParameters.IMG_EXT] = ValidationHelper.GetString(ViewState[DialogParameters.URL_EXT], "");
                retval[DialogParameters.IMG_SIZETOURL] = sizeToUrl;

            }
        }

        #endregion

        #region "General items"

        retval[DialogParameters.URL_EXT] = ext;
        retval[DialogParameters.URL_URL] = url;
        retval[DialogParameters.EDITOR_CLIENTID] = this.EditorClientID;

        #endregion

        #region "Select path & Relationship items"

        if (this.IsRelationship || this.IsSelectPath)
        {
            string path = this.txtSelectPath.Text;
            if (this.chkItems.Checked)
            {
                if (!path.EndsWith("/%"))
                {
                    path = path.TrimEnd('/') + "/%";
                }
            }
            else if (path.EndsWith("/%"))
            {
                path = path.Substring(0, path.Length - 2);
            }
            retval[DialogParameters.DOC_NODEALIASPATH] = path;

            // Fill target node id only if single path selection is enabled or in relationship dialog
            if (IsSelectSinglePath || IsRelationship)
            {
                retval[DialogParameters.DOC_TARGETNODEID] = ViewState[DialogParameters.DOC_TARGETNODEID];
            }
        }

        #endregion

        return retval;
    }


    /// <summary>
    /// Validates all the user input.
    /// </summary>
    public override bool Validate()
    {
        string errorMessage = "";

        if (!this.widthHeightElem.Validate())
        {
            errorMessage += " " + GetString("dialogs.image.invalidsize");
        }

        errorMessage = errorMessage.Trim();
        if (errorMessage != "")
        {
            ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "ImagePropertiesError", ScriptHelper.GetAlertScript(errorMessage));
            return false;
        }

        return true;
    }


    /// <summary>
    /// Clears the properties form.
    /// </summary>
    public override void ClearProperties(bool hideProperties)
    {
        // Hide the properties
        this.pnlEmpty.Visible = hideProperties;
        this.pnlTabs.CssClass = (hideProperties ? "DialogElementHidden" : "Dialog_Tabs");
        this.chkItems.Checked = false;

        this.widthHeightElem.Height = 0;
        this.widthHeightElem.Width = 0;
        this.imagePreview.URL = "";
        this.txtUrl.Text = "";
        this.txtSelectPath.Text = "";
    }

    #endregion
}
