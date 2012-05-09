using System.Text;
using System.Web;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;

public partial class CMSWebParts_Media_Video : CMSAbstractWebPart
{
    #region "Video properties"

    /// <summary>
    /// Gets or sets the value that indicates whether the vide is automatically activated.
    /// </summary>
    public bool AutoActivation
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AutoActivation"), false);
        }
        set
        {
            this.SetValue("AutoActivation", value);
        }
    }


    /// <summary>
    /// Gets or sets the URL of video to be displayed.
    /// </summary>
    public string VideoURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("VideoURL"), "");
        }
        set
        {
            this.SetValue("VideoURL", value);
        }
    }


    /// <summary>
    /// Gets or sets the width of video.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), 400);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Gets or sets the height of video.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Height"), 300);
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether video is automatically started.
    /// </summary>
    public bool Autostart
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("Autostart"), false);
        }
        set
        {
            this.SetValue("Autostart", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether video controller is displayed.
    /// </summary>
    public bool ShowControls
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowControls"), true);
        }
        set
        {
            this.SetValue("ShowControls", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether video after the end is automatically started again.
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

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            //FF hack (must be 1 or 0 not true or false)
            int showControls = (this.ShowControls) ? 1 : 0;
            int autoStart = (this.Autostart) ? 1 : 0;
            int loop = (this.Loop) ? 1 : 0;

            // Auto activation hack
            if (this.AutoActivation)
            {
                ltlPlaceholder.Text = "<div class=\"VideoLikeContent\" id=\"VideoPlaceholder_" + ltlScript.ClientID + "\" ></div>";

                // Register external script
                ScriptHelper.RegisterScriptFile(this.Page, "~/CMSWebParts/Media/Video_files/video.js");

                // Call function for video object insertion
                ltlScript.Text = BuildScriptBlock(showControls, autoStart, loop);
            }
            else
            {
                StringBuilder builder = new StringBuilder(512);

                builder.Append("<div class=\"VideoLikeContent\" style=\"position:relative;\" >");
                builder.Append("<object id=\"" + this.ClientID + "\" classid=\"CLSID:22D6f312-B0F6-11D0-94AB-0080C74C7E95\" width=\"" + this.Width + "\" height=\"" + this.Height + "\" type=\"video/x-ms-wmv\" standby=\"Loading Windows Media Player components...\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=6,4,7,1112\" >");
                builder.Append("<param name=\"filename\" value=\"" + HTMLHelper.HTMLEncode(URLHelper.ResolveUrl(this.VideoURL)) + "\" />");
                builder.Append("<param name=\"src\" value=\"" + HTMLHelper.HTMLEncode(URLHelper.ResolveUrl(this.VideoURL)) + "\" />");
                builder.Append("<param name=\"animationatStart\" value=\"1\" />\n");
                builder.Append("<param name=\"windowlessvideo\" value=\"1\" />\n");
                builder.Append("<param name=\"wmode\" value=\"transparent\" />\n");
                builder.Append("<param name=\"transparentatStart\" value=\"1\" />\n");
                builder.Append("<param name=\"autostart\" value=\"" + autoStart + "\" />\n");
                builder.Append("<param name=\"showControls\" value=\"" + showControls + "\" />\n");
                builder.Append("<param name=\"loop\" value=\"" + loop + "\" />\n");
                if (!CMSContext.CurrentBodyClass.Contains("IE") && !CMSContext.CurrentBodyClass.Contains("Safari"))
                {
                    builder.Append("<object type=\"application/x-mplayer2\" src=\"" + HTMLHelper.HTMLEncode(URLHelper.ResolveUrl(this.VideoURL)) + "\" name=\"" + this.ClientID + "\" width=\"" + this.Width + "\" height=\"" + this.Height + "\" autostart=\"" + autoStart + "\"  wmode=\"transparent\">\n");
                    builder.Append(GetString("Media.NotSupported") + "\n");
                    builder.Append("</object>\n");
                }
                builder.Append("</object></div>");

                this.ltlPlaceholder.Text = builder.ToString();
            }
        }
    }


    /// <summary>
    /// Creates a script block which loads a video at runtime.
    /// </summary>
    /// <param name="showControls">1 if ShowControls is true, otherwise 0</param>
    /// <param name="autoStart">1 if AutoStart is true, otherwise 0</param>
    /// <param name="loop">1 if Loop is true, otherwise 0</param>
    /// <returns>Script block that will load a video</returns>
    private string BuildScriptBlock(int showControls, int autoStart, int loop)
    {
        string scriptBlock = string.Format("LoadVideo('VideoPlaceholder_{0}', '{1}', {2}, {3}, '{4}', '{5}', '{6}', {7});",
            ltlScript.ClientID,
            HTMLHelper.HTMLEncode(URLHelper.ResolveUrl(this.VideoURL)),
            this.Width,
            this.Height,
            showControls,
            autoStart, 
            loop,
            ScriptHelper.GetString(GetString("Media.NotSupported")));

        return ScriptHelper.GetScript(scriptBlock);
    }

    #endregion
}