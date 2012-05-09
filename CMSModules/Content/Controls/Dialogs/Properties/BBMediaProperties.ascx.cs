using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Dialogs_Properties_BBMediaProperties : ItemProperties
{
    #region "Private properties"

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

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether the URL text box should be hidden.
    /// </summary>
    public bool HideUrlBox
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["HideUrlBox"], false);
        }
        set
        {
            ViewState["HideUrlBox"] = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            if (!URLHelper.IsPostback() && this.IsWeb)
            {
                this.pnlEmpty.Visible = false;
                this.pnlTabs.CssClass = "Dialog_Tabs";
            }

            // Refresh button
            this.imgRefresh.Click += new ImageClickEventHandler(imgRefresh_Click);
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
            this.txtUrl.Attributes["onkeydown"] = postBackKeyDownTxtRef;

            this.btnHidden.Click += new EventHandler(btnHidden_Click);
            this.btnTxtHidden.Click += new EventHandler(btnTxtHidden_Click);
            this.btnHiddenSize.Click += new EventHandler(btnHiddenSize_Click);

            this.widthHeightElem.CustomRefreshCode = ControlsHelper.GetPostBackEventReference(this.btnHiddenSize, "") + ";return false;";
            this.widthHeightElem.ShowActions = true;

            if (!URLHelper.IsPostback())
            {
                this.EditorClientID = QueryHelper.GetString("editor_clientid", "");
                this.widthHeightElem.Locked = true;
                LoadPreview();
            }

            this.plcUrlBox.Visible = !this.HideUrlBox;

            if (!string.IsNullOrEmpty(this.NoSelectionText))
            {
                this.lblEmpty.Text = this.NoSelectionText;
            }
            else
            {
                this.pnlEmpty.Visible = false;
            }

            LoadPreview();
        }
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
        this.txtUrl.Text = url;

        LoadPreview();
    }


    protected void btnHidden_Click(object sender, EventArgs e)
    {
        // Update item URL
        bool getPermanent = ((this.widthHeightElem.Width <= this.DefaultWidth) ||
                            (this.widthHeightElem.Height <= this.DefaultHeight)) &&
                            (this.SourceType == MediaSourceEnum.MediaLibraries);

        this.txtUrl.Text = UpdateUrl(this.widthHeightElem.Width, this.widthHeightElem.Height, (getPermanent ? this.PermanentUrl : this.OriginalUrl));
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
        return CMSDialogHelper.UpdateUrl(width, height, this.DefaultWidth, this.DefaultHeight, url, this.SourceType);
    }


    /// <summary>
    /// Configures the preview control.
    /// </summary>
    private void LoadPreview()
    {
        string url = this.txtUrl.Text;
        if (!string.IsNullOrEmpty(url))
        {
            int dotIndex = url.LastIndexOf('.');
            string ext = null;
            if (dotIndex > 0)
            {
                ext = url.Substring(dotIndex);
            }
            // Only for non-image extensions add chset to url
            if (!ImageHelper.IsImage(ext))
            {
                url = URLHelper.UpdateParameterInUrl(url, "chset", Guid.NewGuid().ToString());

                // Add latest version requirement for live site
                if (IsLiveSite)
                {
                    // Add requirement for latest version of files for current document
                    string newparams = "latestforhistoryid=" + this.HistoryID;
                    newparams += "&hash=" + ValidationHelper.GetHashString("h" + this.HistoryID);

                    url += "&" + newparams;
                }
            }

            this.imagePreview.Visible = true;
            this.imagePreview.URL = url;
            this.imagePreview.SizeToURL = ValidationHelper.GetBoolean(ViewState[DialogParameters.IMG_SIZETOURL], false);
            this.imagePreview.Width = this.widthHeightElem.Width;
            this.imagePreview.Height = this.widthHeightElem.Height;

            SaveSession();
        }
        else
        {
            this.imagePreview.Visible = false;
        }
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

    #endregion


    #region "Overriden methods"

    public override void LoadSelectedItems(MediaItem item, Hashtable properties)
    {
        if (!string.IsNullOrEmpty(item.Url))
        {
            // Display the properties
            this.pnlEmpty.Visible = false;
            this.pnlTabs.CssClass = "Dialog_Tabs";

            this.widthHeightElem.Width = item.Width;
            this.widthHeightElem.Height = item.Height;

            this.DefaultWidth = item.Width;
            this.DefaultHeight = item.Height;
            this.HistoryID = item.HistoryID;

            if (properties == null)
            {
                properties = new Hashtable();
            }
            properties[DialogParameters.IMG_WIDTH] = item.Width;
            properties[DialogParameters.IMG_HEIGHT] = item.Height;
            properties[DialogParameters.IMG_ORIGINALWIDTH] = item.Width;
            properties[DialogParameters.IMG_ORIGINALHEIGHT] = item.Height;

            properties[DialogParameters.IMG_URL] = item.Url;
            this.txtUrl.Text = item.Url;
            this.OriginalUrl = item.Url;
            this.PermanentUrl = item.PermanentUrl;

            properties[DialogParameters.EDITOR_CLIENTID] = this.EditorClientID;
        }
        LoadProperties(properties);
        LoadPreview();
    }

    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties">Collection with properties</param>
    public override void LoadItemProperties(Hashtable properties)
    {
        LoadProperties(properties);
        LoadPreview();
    }

    public override void LoadProperties(Hashtable properties)
    {
        if (properties != null)
        {
            // Display the properties
            this.pnlEmpty.Visible = false;
            this.pnlTabs.CssClass = "Dialog_Tabs";

            #region "Image general tab"

            if (tabImageGeneral.Visible)
            {
                int width = ValidationHelper.GetInteger(properties[DialogParameters.IMG_WIDTH], 0);
                int height = ValidationHelper.GetInteger(properties[DialogParameters.IMG_HEIGHT], 0);

                this.DefaultWidth = ValidationHelper.GetInteger(properties[DialogParameters.IMG_ORIGINALWIDTH], 0);
                this.DefaultHeight = ValidationHelper.GetInteger(properties[DialogParameters.IMG_ORIGINALHEIGHT], 0);

                this.widthHeightElem.Width = width;
                this.widthHeightElem.Height = height;

                this.OriginalUrl = ValidationHelper.GetString(properties[DialogParameters.IMG_URL], "");
                this.txtUrl.Text = this.OriginalUrl;
                ViewState[DialogParameters.IMG_SIZETOURL] = ValidationHelper.GetBoolean(properties[DialogParameters.IMG_SIZETOURL], false);
            }

            #endregion

            #region "General items"

            this.EditorClientID = ValidationHelper.GetString(properties[DialogParameters.EDITOR_CLIENTID], "");

            #endregion

        }
    }

    /// <summary>
    /// Returns all parameters of the selected item as name – value collection.
    /// </summary>
    public override Hashtable GetItemProperties()
    {
        Hashtable retval = new Hashtable();

        #region "Image general tab"

        if (tabImageGeneral.Visible)
        {

            string url = this.txtUrl.Text.Trim();
            bool sizeToUrl = ValidationHelper.GetBoolean(ViewState[DialogParameters.IMG_SIZETOURL], false);
            if (this.widthHeightElem.Width != this.DefaultWidth)
            {
                retval[DialogParameters.IMG_WIDTH] = this.widthHeightElem.Width;
                if (sizeToUrl)
                {
                    url = URLHelper.AddParameterToUrl(url, "width", this.widthHeightElem.Width.ToString());
                }
            }
            if (this.widthHeightElem.Height != this.DefaultHeight)
            {
                retval[DialogParameters.IMG_HEIGHT] = this.widthHeightElem.Height;
                if (sizeToUrl)
                {
                    url = URLHelper.AddParameterToUrl(url, "height", this.widthHeightElem.Height.ToString());
                }
            }
            retval[DialogParameters.IMG_URL] = URLHelper.ResolveUrl(url);
            retval[DialogParameters.IMG_SIZETOURL] = sizeToUrl;

        }

        #endregion

        #region "General items"

        retval[DialogParameters.EDITOR_CLIENTID] = this.EditorClientID;

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
            LoadPreview();
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

        this.widthHeightElem.Height = 0;
        this.widthHeightElem.Width = 0;
        this.imagePreview.URL = "";
        this.txtUrl.Text = "";
    }

    #endregion
}
