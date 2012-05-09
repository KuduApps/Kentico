using System;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSInlineControls_YouTubeControl : InlineUserControl
{
    #region "Properties"

    /// <summary>
    /// Url of youtube media.
    /// </summary>
    public string Url
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Url"), "");
        }
        set
        {
            SetValue("Url", value);
        }
    }


    /// <summary>
    /// Enable full screen for youtube player.
    /// </summary>
    public bool Fs
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("Fs"), false);
        }
        set
        {
            SetValue("Fs", value);
        }
    }


    /// <summary>
    /// Gets or sets the value which indicates whether the video should be played in HD by default.
    /// </summary>
    public bool PlayInHD
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("hd"), false);
        }
        set
        {
            SetValue("hd", value);
        }
    }


    /// <summary>
    /// Enable auto play for youtube player.
    /// </summary>
    public bool AutoPlay
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AutoPlay"), false);
        }
        set
        {
            SetValue("AutoPlay", value);
        }
    }


    /// <summary>
    /// Enable loop for youtube player.
    /// </summary>
    public bool Loop
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("Loop"), false);
        }
        set
        {
            SetValue("Loop", value);
        }
    }


    /// <summary>
    /// Enable relative videos in youtube player.
    /// </summary>
    public bool Rel
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("Rel"), false);
        }
        set
        {
            SetValue("Rel", value);
        }
    }


    /// <summary>
    /// Enable delayed cookies for youtube player.
    /// </summary>
    public bool Cookies
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("Cookies"), false);
        }
        set
        {
            SetValue("Cookies", value);
        }
    }


    /// <summary>
    /// Show border around youtube player.
    /// </summary>
    public bool Border
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("Border"), false);
        }
        set
        {
            SetValue("Border", value);
        }
    }


    /// <summary>
    /// Color 1 for youtube player.
    /// </summary>
    public string Color1
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Color1"), "#666666");
        }
        set
        {
            SetValue("Color1", value);
        }
    }


    /// <summary>
    /// Color 2 for youtube player.
    /// </summary>
    public string Color2
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Color2"), "#efefef");
        }
        set
        {
            SetValue("Color2", value);
        }
    }


    /// <summary>
    /// Width of youtube player.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Width"), 0);
        }
        set
        {
            SetValue("Width", value);
        }
    }


    /// <summary>
    /// Height of youtube player.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("Height"), 0);
        }
        set
        {
            SetValue("Height", value);
        }
    }


    /// <summary>
    /// Control parameter.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return Url;
        }
        set
        {
            Url = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        YouTubeVideoParameters ytParams = new YouTubeVideoParameters();
        ytParams.Url = ResolveUrl(Url).Replace("\"", "\\\"");
        ytParams.FullScreen = Fs;
        ytParams.PlayInHD = PlayInHD;
        ytParams.AutoPlay = AutoPlay;
        ytParams.Loop = Loop;
        ytParams.RelatedVideos = Rel;
        ytParams.Delayed = Cookies;
        ytParams.Border = Border;
        ytParams.Color1 = Color1;
        ytParams.Color2 = Color2;
        ytParams.Width = Width;
        ytParams.Height = Height;

        ltlYouTube.Text = MediaHelper.GetYouTubeVideo(ytParams);

        string script = ScriptHelper.GetScript("window.onbeforeunload = function(){\n var ytEmbeds = document.getElementsByTagName('embed'); if (ytEmbeds.length > 0) {\n for (var i = 0; i < ytEmbeds.length; i++){\n ytEmbeds[i].parentNode.removeChild(ytEmbeds[i]);}}}");
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(Page), "YouTubeUnload", script);
    }

    #endregion
}
