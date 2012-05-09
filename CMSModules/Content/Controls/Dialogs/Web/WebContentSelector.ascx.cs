using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Net;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Dialogs_Web_WebContentSelector : CMSUserControl
{
    #region "Variables"

    private SelectableContentEnum mSelectableContent = SelectableContentEnum.OnlyMedia;
    private DialogConfiguration mConfig = null;
    private int mWidth = 0;
    private int mHeight = 0;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns current properties (according to OutputFormat).
    /// </summary>
    private ItemProperties Properties
    {
        get
        {
            switch (this.Config.OutputFormat)
            {
                case OutputFormatEnum.HTMLMedia:
                    return this.propMedia;
                case OutputFormatEnum.BBMedia:
                    return this.propBBMedia;
                default:
                    return this.propURL;
            }
        }
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets current dialog configuration.
    /// </summary>
    public DialogConfiguration Config
    {
        get
        {
            if (this.mConfig == null)
            {
                this.mConfig = DialogConfiguration.GetDialogConfiguration();
            }
            return this.mConfig;
        }
    }


    /// <summary>
    /// Gets or sets the type of the content which can be selected.
    /// </summary>
    public SelectableContentEnum SelectableContent
    {
        get
        {
            return this.mSelectableContent;
        }
        set
        {
            this.mSelectableContent = value;
        }
    }


    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            this.propMedia.IsLiveSite = value;
            this.propBBMedia.IsLiveSite = value;
            base.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!this.StopProcessing)
        {
            this.drpMediaType.Items.Add(new ListItem(GetString("dialogs.web.select"), ""));
            this.drpMediaType.Items.Add(new ListItem(GetString("dialogs.web.image"), "image"));
            if (this.Config.SelectableContent != SelectableContentEnum.OnlyImages)
            {
                this.drpMediaType.Items.Add(new ListItem(GetString("dialogs.web.av"), "av"));
                this.drpMediaType.Items.Add(new ListItem(GetString("dialogs.web.flash"), "flash"));
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            if (this.Config.OutputFormat == OutputFormatEnum.URL)
            {
                this.plcMediaType.Visible = false;
                this.plcRefresh.Visible = false;
                this.pnlProperties.CssClass = "DialogWebProperties DialogWebPropertiesTiny";
            }

            this.drpMediaType.SelectedIndexChanged += new EventHandler(drpMediaType_SelectedIndexChanged);

            this.imgRefresh.ImageUrl = GetImageUrl("Design/Controls/Dialogs/refresh.png");
            this.imgRefresh.ToolTip = GetString("dialogs.web.refresh");
            this.imgRefresh.Click += new ImageClickEventHandler(imgRefresh_Click);

            // Get reffernce causing postback to hidden button
            string postBackRef = ControlsHelper.GetPostBackEventReference(this.hdnButton, "");
            this.ltlScript.Text = ScriptHelper.GetScript("function RaiseHiddenPostBack(){" + postBackRef + ";}\n");
            this.plcInfo.Visible = false;

            // OnChange and OnKeyDown event triggers
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "txtUrlChange", ScriptHelper.GetScript("$j(function(){ $j('" + txtUrl.ClientID + "').change(function (){ $j('#" + imgRefresh.ClientID + "').trigger('click');});});"));
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "txtUrlKeyDown", ScriptHelper.GetScript("$j(function(){ $j('#" + txtUrl.ClientID + "').keydown(function(event){ if (event.keyCode == 13) { $j('#" + imgRefresh.ClientID + "').trigger('click'); return false;}});});"));

            InitializeDesignScripts();

            if (!RequestHelper.IsPostBack())
            {
                InitFromQueryString();
                DisplayProperties();

                if (this.Config.OutputFormat == OutputFormatEnum.BBMedia)
                {
                    // For BB editor properties are always visible and only image is allowed.
                    this.plcBBMediaProp.Visible = true;
                    this.propBBMedia.NoSelectionText = "";
                    this.drpMediaType.Items.Remove(new ListItem(GetString("dialogs.web.select"), ""));
                }

                Hashtable selectedItem = SessionHelper.GetValue("DialogParameters") as Hashtable;
                if ((selectedItem != null) && (selectedItem.Count > 0))
                {
                    LoadSelectedItem(selectedItem);
                    SessionHelper.SetValue("DialogParameters", null);
                }
                else
                {
                    // Try get selected item from session
                    selectedItem = SessionHelper.GetValue("DialogSelectedParameters") as Hashtable;
                    if ((selectedItem != null) && (selectedItem.Count > 0))
                    {
                        LoadSelectedItem(selectedItem);
                    }
                }
            }
        }
    }


    protected void imgRefresh_Click(object sender, EventArgs e)
    {
        if (this.plcMediaType.Visible)
        {
            MediaSource source = CMSDialogHelper.GetMediaData(this.txtUrl.Text, null);
            if ((source == null) || (source.MediaType == MediaTypeEnum.Unknown))
            {
                this.Properties.ItemNotSystem = true;

                // Try get source type from URL extension
                int index = this.txtUrl.Text.LastIndexOf('.');
                if (index > 0)
                {
                    string ext = this.txtUrl.Text.Substring(index);
                    if (ext.Contains("?")) 
                    {
                        ext = URLHelper.RemoveQuery(ext);
                    }
                    if (source == null)
                    {
                        source = new MediaSource();
                    }
                    source.Extension = ext;
                    if (source.MediaType == MediaTypeEnum.Image)
                    {
                        try
                        {
                            // Get the data
                            WebClient wc = new WebClient();
                            byte[] img = wc.DownloadData(this.txtUrl.Text.Trim());
                            ImageHelper ih = new ImageHelper(img);
                            if (ih.ImageWidth > 0)
                            {
                                this.mWidth = ih.ImageWidth;
                            }
                            if (ih.ImageHeight > 0)
                            {
                                this.mHeight = ih.ImageHeight;
                            }
                            wc.Dispose();
                        }
                        catch { }
                    }
                    else
                    {
                        source.MediaWidth = 300;
                        source.MediaHeight = 200;
                    }
                }
            }
            else 
            {
                this.Properties.ItemNotSystem = false;
            }

            if (source != null)
            {
                // Set default dimensions when not specified
                if ((this.mWidth == 0) && (this.mHeight == 0))
                {
                    this.mWidth = source.MediaWidth;
                    this.mHeight = source.MediaHeight;
                }
                switch (source.MediaType)
                {
                    case MediaTypeEnum.Image:
                        this.drpMediaType.SelectedValue = "image";
                        break;

                    case MediaTypeEnum.AudioVideo:
                        this.drpMediaType.SelectedValue = "av";
                        break;

                    case MediaTypeEnum.Flash:
                        this.drpMediaType.SelectedValue = "flash";
                        break;
                    default:
                        this.drpMediaType.SelectedValue = "";
                        this.plcInfo.Visible = true;
                        this.lblInfo.ResourceString = "dialogs.web.selecttype";
                        break;
                }
            }

            if (source != null)
            {
                SetLastType(source.MediaType);
            }

            ShowProperties();
        }
    }


    protected void drpMediaType_SelectedIndexChanged(object sender, EventArgs e)
    {
        MediaTypeEnum type = MediaTypeEnum.Unknown;
        switch (drpMediaType.SelectedValue.ToLower())
        {
            case "image":
                type = MediaTypeEnum.Image;
                break;
            case "av":
                type = MediaTypeEnum.AudioVideo;
                break;
            case "flash":
                type = MediaTypeEnum.Flash;
                break;
        }

        SetLastType(type);
        ShowProperties();
    }


    protected void hdnButtonUrl_Click(object sender, EventArgs e)
    {
        Hashtable properties = new Hashtable();
        properties[DialogParameters.URL_URL] = this.txtUrl.Text.Trim();
        properties[DialogParameters.EDITOR_CLIENTID] = this.Config.EditorClientID;
        drpMediaType.SelectedValue = "";
        this.Properties.LoadProperties(properties);
    }


    protected void hdnButton_Click(object sender, EventArgs e)
    {
        Hashtable properties = GetSelectedItem();
        string script = null;
        if (this.Config.OutputFormat == OutputFormatEnum.URL)
        {
            properties[DialogParameters.URL_URL] = this.txtUrl.Text.Trim();
            properties[DialogParameters.EDITOR_CLIENTID] = this.Config.EditorClientID;
            script = CMSDialogHelper.GetUrlItem(properties);
        }
        else
        {
            switch (this.drpMediaType.SelectedValue)
            {
                case "image":
                    script = CMSDialogHelper.GetImageItem(properties);
                    break;
                case "av":
                    script = CMSDialogHelper.GetAVItem(properties);
                    break;
                case "flash":
                    script = CMSDialogHelper.GetFlashItem(properties);
                    break;
                default:
                    script = CMSDialogHelper.GetUrlItem(properties);
                    break;
            }
        }
        if (!String.IsNullOrEmpty(script))
        {
            ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "insertItemScript", ScriptHelper.GetScript(script));
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Shows correct properties according to the settings.
    /// </summary>
    private void ShowProperties()
    {
        this.Properties.Config = this.Config;

        // Save session data before shoving properties
        Hashtable dialogParameters = SessionHelper.GetValue("DialogSelectedParameters") as Hashtable;
        if (dialogParameters != null)
        {
            dialogParameters = (Hashtable)dialogParameters.Clone();
        }

        DisplayProperties();

        MediaItem mi = new MediaItem();
        mi.Url = this.txtUrl.Text;
        if (this.mWidth > 0)
        {
            mi.Width = this.mWidth;
        }
        if (this.mHeight > 0)
        {
            mi.Height = this.mHeight;
        }

        // Try get source type from URL extension
        string ext = null;
        int index = this.txtUrl.Text.LastIndexOf('.');
        if (index > 0)
        {
            ext = this.txtUrl.Text.Substring(index);
        }

        if (this.Config.OutputFormat == OutputFormatEnum.HTMLMedia)
        {

            switch (this.drpMediaType.SelectedValue)
            {
                case "image":
                    this.propMedia.ViewMode = MediaTypeEnum.Image;
                    mi.Extension = String.IsNullOrEmpty(ext) ? "jpg" : ext;
                    break;

                case "av":
                    this.propMedia.ViewMode = MediaTypeEnum.AudioVideo;
                    mi.Extension = String.IsNullOrEmpty(ext) ? "avi" : ext;
                    break;

                case "flash":
                    this.propMedia.ViewMode = MediaTypeEnum.Flash;
                    mi.Extension = String.IsNullOrEmpty(ext) ? "swf" : ext;
                    break;
                default:
                    this.plcHTMLMediaProp.Visible = false;
                    break;
            }
            if (URLHelper.IsPostback())
            {
                this.Properties.LoadSelectedItems(mi, dialogParameters);
            }
        }
        else if ((this.Config.OutputFormat == OutputFormatEnum.BBMedia) && (URLHelper.IsPostback()))
        {
            mi.Extension = String.IsNullOrEmpty(ext) ? "jpg" : ext;
            this.Properties.LoadSelectedItems(mi, dialogParameters);
        }
        else if ((this.Config.OutputFormat == OutputFormatEnum.URL) && (URLHelper.IsPostback()))
        {
            this.Properties.LoadSelectedItems(mi, dialogParameters);
        }
        // Set saved session data back into session
        if (dialogParameters != null)
        {
            SessionHelper.SetValue("DialogSelectedParameters", dialogParameters);
        }
    }


    /// <summary>
    /// Display panel of properties.
    /// </summary>
    private void DisplayProperties()
    {
        this.plcBBMediaProp.Visible = false;
        this.plcHTMLMediaProp.Visible = false;
        this.plcURLProp.Visible = false;

        switch (this.Config.OutputFormat)
        {
            case OutputFormatEnum.HTMLMedia:
                this.plcHTMLMediaProp.Visible = true;
                break;

            case OutputFormatEnum.BBMedia:
                this.plcBBMediaProp.Visible = true;
                break;

            case OutputFormatEnum.URL:
            default:
                this.plcURLProp.Visible = true;
                break;
        }
    }


    /// <summary>
    /// Update last type value in dialog selected parameters.
    /// </summary>
    /// <param name="type">Type</param>
    private void SetLastType(MediaTypeEnum type)
    {
        // Get selected prameters
        Hashtable dialogParameters = SessionHelper.GetValue("DialogSelectedParameters") as Hashtable;
        if (dialogParameters == null)
        {
            dialogParameters = new Hashtable();
        }

        // Update last type
        dialogParameters[DialogParameters.LAST_TYPE] = type;
        SessionHelper.SetValue("DialogSelectedParameters", dialogParameters);
    }


    /// <summary>
    /// Initialize design jQuery scripts.
    /// </summary>
    private void InitializeDesignScripts()
    {
        ScriptHelper.RegisterStartupScript(Page, typeof(Page), "designScript", ScriptHelper.GetScript("setTimeout('InitializeDesign();',200);$j(window).resize(function() { InitializeDesign(); });"));
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Initializes its properties according to the URL parameters.
    /// </summary>
    public void InitFromQueryString()
    {
        switch (this.Config.OutputFormat)
        {
            case OutputFormatEnum.HTMLMedia:
                this.SelectableContent = SelectableContentEnum.OnlyMedia;
                break;

            case OutputFormatEnum.BBMedia:
                this.SelectableContent = SelectableContentEnum.OnlyImages;
                break;

            default:
                string content = QueryHelper.GetString("content", "");
                if (content == "img")
                {
                    this.SelectableContent = SelectableContentEnum.OnlyImages;
                }
                else
                {
                    this.SelectableContent = SelectableContentEnum.AllContent;
                }
                break;
        }
    }


    /// <summary>
    /// Returns selected item parameters as name-value collection.
    /// </summary>
    public Hashtable GetSelectedItem()
    {
        return this.Properties.GetItemProperties();
    }


    /// <summary>
    /// Loads selected item parameters into the selector.
    /// </summary>
    /// <param name="properties">Name-value collection representing item to load</param>
    public void LoadSelectedItem(Hashtable properties)
    {
        if ((properties != null) && (properties.Count > 0))
        {
            Hashtable temp = (Hashtable)properties.Clone();

            if ((properties[DialogParameters.AV_URL] != null) && ((properties[DialogParameters.LAST_TYPE] == null) || ((MediaTypeEnum)properties[DialogParameters.LAST_TYPE] == MediaTypeEnum.AudioVideo)))
            {
                this.drpMediaType.SelectedValue = "av";
                this.txtUrl.Text = properties[DialogParameters.AV_URL].ToString();
            }
            else if ((properties[DialogParameters.FLASH_URL] != null) && ((properties[DialogParameters.LAST_TYPE] == null) || ((MediaTypeEnum)properties[DialogParameters.LAST_TYPE] == MediaTypeEnum.Flash)))
            {
                this.drpMediaType.SelectedValue = "flash";
                this.txtUrl.Text = properties[DialogParameters.FLASH_URL].ToString();
            }
            else if ((properties[DialogParameters.IMG_URL] != null) && ((properties[DialogParameters.LAST_TYPE] == null) || ((MediaTypeEnum)properties[DialogParameters.LAST_TYPE] == MediaTypeEnum.Image)))
            {
                this.drpMediaType.SelectedValue = "image";

                /*int width = ValidationHelper.GetInteger(temp[DialogParameters.IMG_WIDTH], 0);
                int height = ValidationHelper.GetInteger(temp[DialogParameters.IMG_HEIGHT], 0);

                int originalWidth = ValidationHelper.GetInteger(temp[DialogParameters.IMG_ORIGINALWIDTH], 0);
                int originalHeight = ValidationHelper.GetInteger(temp[DialogParameters.IMG_ORIGINALHEIGHT], 0);*/
                // Update URL
                string url = ValidationHelper.GetString(properties[DialogParameters.IMG_URL], "");
                this.txtUrl.Text = url;
            }
            else if ((properties[DialogParameters.URL_URL] != null) && ((properties[DialogParameters.LAST_TYPE] == null) || ((MediaTypeEnum)properties[DialogParameters.LAST_TYPE] == MediaTypeEnum.Unknown)))
            {
                this.txtUrl.Text = properties[DialogParameters.URL_URL].ToString();
            }
            if (!String.IsNullOrEmpty(txtUrl.Text))
            {
                ShowProperties();
                // Load temp properties because ShowProperties() change original properties
                this.Properties.LoadItemProperties(temp);
            }
        }
    }

    #endregion
}
