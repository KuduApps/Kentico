using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;

public partial class CMSInlineControls_MediaControl : InlineUserControl
{
    #region "Properties"

    /// <summary>
    /// Url of media file.
    /// </summary>
    public string Url
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Url"), null);
        }
        set
        {
            this.SetValue("Url", value);
        }
    }


    /// <summary>
    /// Type of media file.
    /// </summary>
    public string Type
    {
        get
        {
            string type = ValidationHelper.GetString(this.GetValue("Type"), null);
            if (type == null)
            {
                type = ValidationHelper.GetString(this.GetValue("Ext"), null);
            }
            if (type == null)
            {
                type = URLHelper.GetUrlParameter(this.Url, "ext");
            }
            return type;
        }
        set
        {
            this.SetValue("Type", value);
        }
    }


    /// <summary>
    /// Width of media or flash player.
    /// </summary>
    public int Width
    {
        get
        {
            int width = ValidationHelper.GetInteger(this.GetValue("Width"), -1);
            if (width == -1)
            {
                width = ValidationHelper.GetInteger(URLHelper.GetUrlParameter(this.Url, "width"), -1);
            }
            return width;
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Height of media or flash player.
    /// </summary>
    public int Height
    {
        get
        {
            int height = ValidationHelper.GetInteger(this.GetValue("Height"), -1);
            if (height == -1)
            {
                height = ValidationHelper.GetInteger(URLHelper.GetUrlParameter(this.Url, "height"), -1);
            }
            return height;
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Auto play media or flash.
    /// </summary>
    public bool AutoPlay
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AutoPlay"), false);
        }
        set
        {
            this.SetValue("AutoPlay", value);
        }
    }


    /// <summary>
    /// Loop media or flash.
    /// </summary>
    public bool Loop
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("Loop"), false);
        }
        set
        {
            this.SetValue("Loop", value);
        }
    }


    /// <summary>
    /// Show media player controls.
    /// </summary>
    public bool AVControls
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("Controls"), true);
        }
        set
        {
            this.SetValue("Controls", value);
        }
    }


    /// <summary>
    /// Automatically active media player.
    /// </summary>
    public bool AutoActive
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AutoActive"), false);
        }
        set
        {
            this.SetValue("AutoActive", value);
        }
    }


    /// <summary>
    /// Enable flash control context menu.
    /// </summary>
    public bool Menu
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("Menu"), false);
        }
        set
        {
            this.SetValue("Menu", value);
        }
    }


    /// <summary>
    /// Scale of flash control.
    /// </summary>
    public string Scale
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Scale"), null);
        }
        set
        {
            this.SetValue("Scale", value);
        }
    }


    /// <summary>
    /// Flash control id.
    /// </summary>
    public string Id
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Id"), null);
        }
        set
        {
            this.SetValue("Id", value);
        }
    }


    /// <summary>
    /// Title of flash player control.
    /// </summary>
    public string Title
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Title"), null);
        }
        set
        {
            this.SetValue("Title", value);
        }
    }


    /// <summary>
    /// Flash control css style class.
    /// </summary>
    public string Class
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Class"), null);
        }
        set
        {
            this.SetValue("Class", value);
        }
    }


    /// <summary>
    /// Flash control inline style.
    /// </summary>
    public string Style
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Style"), null);
        }
        set
        {
            this.SetValue("Style", value);
        }
    }


    /// <summary>
    /// Flash control variables.
    /// </summary>
    public string FlashVars
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FlashVars"), null);
        }
        set
        {
            this.SetValue("FlashVars", value);
        }
    }


    /// <summary>
    /// Control parameter.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return this.Url;
        }
        set
        {
            this.Url = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (MediaHelper.IsFlash(this.Type))
        {
            CreateFlash();
        }
        else if (ImageHelper.IsImage(this.Type))
        {
            CreateImage();
        }
        else
        {
            CreateMedia();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Creates the flash object
    /// </summary>
    private void CreateFlash()
    {
        FlashParameters flParams = new FlashParameters();
        flParams.Url = URLHelper.GetAbsoluteUrl(this.Url);
        flParams.Extension = this.Type;
        flParams.Width = this.Width;
        flParams.Height = this.Height;
        flParams.Autoplay = this.AutoPlay;
        flParams.Loop = this.Loop;
        flParams.Menu = this.Menu;
        flParams.Scale = this.Scale;
        flParams.Id = HttpUtility.UrlDecode(this.Id);
        flParams.Title = HttpUtility.UrlDecode(this.Title);
        flParams.Class = HttpUtility.UrlDecode(this.Class);
        flParams.Style = HttpUtility.UrlDecode(this.Style);
        flParams.FlashVars = HttpUtility.UrlDecode(this.FlashVars);

        this.ltlMedia.Text = MediaHelper.GetFlash(flParams);
    }



    /// <summary>
    /// Creates the media (audio / video) object
    /// </summary>
    private void CreateMedia()
    {
        AudioVideoParameters avParams = new AudioVideoParameters();
        if (this.Url != null)
        {
            avParams.SiteName = CMSContext.CurrentSiteName;
            avParams.Url = URLHelper.GetAbsoluteUrl(this.Url);
            avParams.Extension = this.Type;
            avParams.Width = this.Width;
            avParams.Height = this.Height;
            avParams.AutoPlay = this.AutoPlay;
            avParams.Loop = this.Loop;
            avParams.Controls = this.AVControls;
        }

        this.ltlMedia.Text = MediaHelper.GetAudioVideo(avParams);
    }


    /// <summary>
    /// Creates the image object
    /// </summary>
    private void CreateImage()
    {
        ImageParameters imgParams = new ImageParameters();
        if (this.Url != null)
        {
            imgParams.Url = URLHelper.GetAbsoluteUrl(this.Url);
            imgParams.Extension = this.Type;
            imgParams.Width = this.Width;
            imgParams.Height = this.Height;
            imgParams.Id = HttpUtility.UrlDecode(this.Id);
            imgParams.Tooltip = HttpUtility.UrlDecode(this.Title);
            imgParams.Class = HttpUtility.UrlDecode(this.Class);
            imgParams.Style = HttpUtility.UrlDecode(this.Style);
        }
        this.ltlMedia.Text = MediaHelper.GetImage(imgParams);
    }

    #endregion
}
