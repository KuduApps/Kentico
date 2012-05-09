using System;
using System.Collections;
using System.Web.UI;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Dialogs_YouTube_YouTubeProperties : ItemProperties
{
    #region "Private properties"

    /// <summary>
    /// Returns the default width of the YouTube video.
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
    /// Returns the default height of the YouTube video.
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


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(Page);

        if (!StopProcessing)
        {
            // Refresh button
            imgRefresh.ImageUrl = GetImageUrl("Design/Controls/Dialogs/refresh.png");
            imgRefresh.ToolTip = GetString("dialogs.web.refresh");

            // Color pickers
            colorElem1.SupportFolder = "~/CMSAdminControls/ColorPicker";
            colorElem2.SupportFolder = "~/CMSAdminControls/ColorPicker";

            // YouTube default colors control
            youTubeColors.OnSelectedItemClick = ControlsHelper.GetPostBackEventReference(btnDefaultColorsHidden, "");
            youTubeColors.LoadColors(new string[]{"#666666", "#EFEFEF", "#3A3A3A", "#999999", 
                "#2B405B", "#6B84B6", "#006699", "#54ABD6", "#234900", "#4E9E00", "#E1600F", 
                "#FEBD01", "#CC2550", "#E87A9F", "#402061", "#9461CA", "#5D1719", "#CD311B"});

            // YouTube default sizes control
            youTubeSizes.OnSelectedItemClick = ControlsHelper.GetPostBackEventReference(btnDefaultSizesHidden, "");
            if (chkShowBorder.Checked)
            {
                youTubeSizes.LoadSizes(new int[] { 445, 284, 500, 315, 580, 360, 660, 405 });
            }
            else
            {
                youTubeSizes.LoadSizes(new int[] { 425, 264, 480, 295, 560, 340, 640, 385 });
            }
            chkShowBorder.CheckedChanged += chkShowBorder_CheckedChanged;
            btnDefaultColorsHidden.Click += btnDefaultColorsHidden_Click;
            btnHiddenPreview.Click += btnHiddenPreview_Click;
            btnHiddenInsert.Click += btnHiddenInsert_Click;
            btnHiddenSizeRefresh.Click += btnHiddenSizeRefresh_Click;
            btnDefaultSizesHidden.Click += btnDefaultSizesHidden_Click;
            colorElem1.ColorChanged += colorElem1_ColorChanged;
            colorElem2.ColorChanged += colorElem2_ColorChanged;

            sizeElem.CustomRefreshCode = ControlsHelper.GetPostBackEventReference(btnHiddenSizeRefresh, "") + ";return false;";

            CMSDialogHelper.RegisterDialogHelper(Page);
            ltlScript.Text = 
                ScriptHelper.GetScript(string.Format("function insertItem(){{{0}}}", 
                    Page.ClientScript.GetPostBackEventReference(btnHiddenInsert, string.Empty)));

            ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "loading", ScriptHelper.GetScript(
                string.Format("Loading('{0}','{1}');", 
                    GetString("dialogs.youtube.preview").Replace("\'", "\\\'"), 
                    GetString("dialogs.youtube.previewloading").Replace("\'", "\\\'"))));

            SetupOnChange();

            if (!RequestHelper.IsPostBack())
            {
                sizeElem.Locked = true;
                sizeElem.Width = DefaultWidth = 425;
                sizeElem.Height = DefaultHeight = 264;
                colorElem1.SelectedColor = "#666666";
                colorElem2.SelectedColor = "#EFEFEF";

                Hashtable dialogParameters = SessionHelper.GetValue("DialogParameters") as Hashtable;
                if ((dialogParameters != null) && (dialogParameters.Count > 0))
                {
                    LoadItemProperties(dialogParameters);
                    SessionHelper.SetValue("DialogParameters", null);
                }
            }
        }
    }


    protected void btnHiddenSizeRefresh_Click(object sender, EventArgs e)
    {
        sizeElem.Width = DefaultWidth;
        sizeElem.Height = DefaultHeight;

        LoadPreview();
    }


    protected void btnDefaultSizesHidden_Click(object sender, EventArgs e)
    {
        sizeElem.Width = youTubeSizes.SelectedWidth;
        sizeElem.Height = youTubeSizes.SelectedHeight;

        DefaultWidth = youTubeSizes.SelectedWidth;
        DefaultHeight = youTubeSizes.SelectedHeight;

        LoadPreview();
    }


    protected void chkShowBorder_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowBorder.Checked)
        {
            sizeElem.Width += 20;
            sizeElem.Height += 20;
        }
        else
        {
            sizeElem.Width -= 20;
            sizeElem.Height -= 20;
        }
        LoadPreview();
    }


    protected void btnHiddenPreview_Click(object sender, EventArgs e)
    {
        LoadPreview();
    }


    protected void colorElem1_ColorChanged(object sender, EventArgs e)
    {
        colorElem1.SelectedColor = colorElem1.ColorTextBox.Text.Trim();
        LoadPreview();
    }


    protected void colorElem2_ColorChanged(object sender, EventArgs e)
    {
        colorElem2.SelectedColor = colorElem2.ColorTextBox.Text.Trim();
        LoadPreview();
    }


    protected void btnHiddenInsert_Click(object sender, EventArgs e)
    {
        if (Validate())
        {
            Hashtable ytProperties = GetItemProperties();

            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "insertYouTube", ScriptHelper.GetScript(CMSDialogHelper.GetYouTubeItem(ytProperties)));
        }
    }


    protected void btnDefaultColorsHidden_Click(object sender, EventArgs e)
    {
        colorElem1.SelectedColor = youTubeColors.SelectedColor1;
        colorElem2.SelectedColor = youTubeColors.SelectedColor2;
        LoadPreview();
    }

    protected void imgRefresh_Click(object sender, ImageClickEventArgs e)
    {
        LoadPreview();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Loads the preview with proper data.
    /// </summary>
    private void LoadPreview()
    {
        if (!string.IsNullOrEmpty(this.txtLinkText.Text.Trim()))
        {
            previewElem.AutoPlay = false; // Always ignore autoplay in preview
            previewElem.PlayInHD = chkPlayInHD.Checked;
            previewElem.Border = chkShowBorder.Checked;
            previewElem.Color1 = colorElem1.SelectedColor;
            previewElem.Color2 = colorElem2.SelectedColor;
            previewElem.Cookies = chkEnableDelayed.Checked;
            previewElem.Fs = chkFullScreen.Checked;
            previewElem.Height = sizeElem.Height;
            previewElem.Width = sizeElem.Width;
            previewElem.Loop = chkLoop.Checked;
            previewElem.Rel = chkIncludeRelated.Checked;
            previewElem.Url = txtLinkText.Text.Trim();
        }
    }

    private void SetupOnChange()
    {
        string postBackRef = string.Format("setTimeout({0},100);", 
            ScriptHelper.GetString(ControlsHelper.GetPostBackEventReference(btnHiddenPreview, "")));

        txtLinkText.Attributes["onchange"] = postBackRef;
        sizeElem.HeightTextBox.Attributes["onchange"] = postBackRef;
        sizeElem.WidthTextBox.Attributes["onchange"] = postBackRef;
        chkPlayInHD.InputAttributes["onclick"] = postBackRef;
        chkAutoplay.InputAttributes["onclick"] = postBackRef;
        chkEnableDelayed.InputAttributes["onclick"] = postBackRef;
        chkFullScreen.InputAttributes["onclick"] = postBackRef;
        chkIncludeRelated.InputAttributes["onclick"] = postBackRef;
        chkLoop.InputAttributes["onclick"] = postBackRef;
    }

    #endregion


    #region "Overriden methods"

    /// <summary>
    /// Loads the properites into control.
    /// </summary>
    /// <param name="properties"></param>
    public override void LoadItemProperties(Hashtable properties)
    {
        if (properties != null)
        {

            bool playInHd = ValidationHelper.GetBoolean(properties[DialogParameters.YOUTUBE_PLAYINHD], false);
            bool autoplay = ValidationHelper.GetBoolean(properties[DialogParameters.YOUTUBE_AUTOPLAY], false);
            bool border = ValidationHelper.GetBoolean(properties[DialogParameters.YOUTUBE_BORDER], false);
            bool cookies = ValidationHelper.GetBoolean(properties[DialogParameters.YOUTUBE_COOKIES], false);
            bool fullScreen = ValidationHelper.GetBoolean(properties[DialogParameters.YOUTUBE_FS], false);
            bool loop = ValidationHelper.GetBoolean(properties[DialogParameters.YOUTUBE_LOOP], false);
            bool relatedVideos = ValidationHelper.GetBoolean(properties[DialogParameters.YOUTUBE_REL], false);
            string url = ValidationHelper.GetString(properties[DialogParameters.YOUTUBE_URL], "");
            int width = ValidationHelper.GetInteger(properties[DialogParameters.YOUTUBE_WIDTH], 425);
            int height = ValidationHelper.GetInteger(properties[DialogParameters.YOUTUBE_HEIGHT], 264);
            string color1 = ValidationHelper.GetString(properties[DialogParameters.YOUTUBE_COLOR1], "#666666");
            string color2 = ValidationHelper.GetString(properties[DialogParameters.YOUTUBE_COLOR2], "#EFEFEF");
            if (String.IsNullOrEmpty(color1))
            {
                color1 = "#666666";
            }
            if (String.IsNullOrEmpty(color2))
            {
                color2 = "#EFEFEF";
            }

            DefaultWidth = width;
            DefaultHeight = height;

            chkPlayInHD.Checked = playInHd;
            chkAutoplay.Checked = autoplay;
            chkEnableDelayed.Checked = cookies;
            chkFullScreen.Checked = fullScreen;
            chkIncludeRelated.Checked = relatedVideos;
            chkLoop.Checked = loop;
            chkShowBorder.Checked = border;
            txtLinkText.Text = url;
            colorElem1.SelectedColor = color1;
            colorElem2.SelectedColor = color2;
            sizeElem.Width = width;
            sizeElem.Height = height;

            LoadPreview();
        }
    }


    /// <summary>
    /// Returns all parameters of the selected item as name – value collection.
    /// </summary>
    public override Hashtable GetItemProperties()
    {
        Hashtable retval = new Hashtable();

        retval[DialogParameters.YOUTUBE_PLAYINHD] = chkPlayInHD.Checked;
        retval[DialogParameters.YOUTUBE_AUTOPLAY] = chkAutoplay.Checked;
        retval[DialogParameters.YOUTUBE_BORDER] = chkShowBorder.Checked;
        retval[DialogParameters.YOUTUBE_COLOR1] = colorElem1.SelectedColor;
        retval[DialogParameters.YOUTUBE_COLOR2] = colorElem2.SelectedColor;
        retval[DialogParameters.YOUTUBE_COOKIES] = chkEnableDelayed.Checked;
        retval[DialogParameters.YOUTUBE_FS] = chkFullScreen.Checked;
        retval[DialogParameters.YOUTUBE_HEIGHT] = sizeElem.Height;
        retval[DialogParameters.YOUTUBE_LOOP] = chkLoop.Checked;
        retval[DialogParameters.YOUTUBE_REL] = chkIncludeRelated.Checked;
        retval[DialogParameters.YOUTUBE_URL] = txtLinkText.Text.Trim();
        retval[DialogParameters.YOUTUBE_WIDTH] = sizeElem.Width;
        retval[DialogParameters.OBJECT_TYPE] = "youtubevideo";

        return retval;
    }


    /// <summary>
    /// Clears the properties form.
    /// </summary>
    public override void ClearProperties(bool hideProperties)
    {
        sizeElem.Height = 425;
        sizeElem.Width = 264;

        chkPlayInHD.Checked = false;
        chkAutoplay.Checked = false;
        chkShowBorder.Checked = false;
        chkLoop.Checked = false;
        chkIncludeRelated.Checked = true;
        chkEnableDelayed.Checked = false;
        chkFullScreen.Checked = true;

        colorElem1.SelectedColor = "#666666";
        colorElem2.SelectedColor = "#EFEFEF";

        previewElem.Url = "";
    }


    /// <summary>
    /// Validates all the user input.
    /// </summary>
    public override bool Validate()
    {
        string errorMessage = "";

        if (!sizeElem.Validate())
        {
            errorMessage += " " + GetString("dialogs.youtube.invalidsize");
        }
        if ((colorElem1.ColorTextBox.Text.Trim() != "") && !ValidationHelper.IsColor(colorElem1.ColorTextBox.Text.Trim()))
        {
            errorMessage += " " + GetString("dialogs.youtube.invalidcolor1");
        }
        if ((colorElem2.ColorTextBox.Text.Trim() != "") && !ValidationHelper.IsColor(colorElem2.ColorTextBox.Text.Trim()))
        {
            errorMessage += " " + GetString("dialogs.youtube.invalidcolor2");
        }

        errorMessage = errorMessage.Trim();
        if (errorMessage != "")
        {
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "YouTubePropertiesError", ScriptHelper.GetAlertScript(errorMessage));
            return false;
        }
        return true;
    }

    #endregion
}