using System.Web;

using CMS.GlobalHelper;
using CMS.PortalControls;

public partial class CMSWebParts_CommunityServices_YouTubeVideo : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether the video is automatically activated.
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
    ///  Gets or sets the URL of YouTube video to be displayed.
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
    /// Gets or sets the video width.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), 425);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Gets or sets the video height.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Height"), 355);
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether video start immediately after webpart load.
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
    /// Reloads data for partial caching.
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
            string videoUrl = HTMLHelper.HTMLEncode(this.VideoURL);

            // Index of query string parameter value 'v' 
            int pos = videoUrl.LastIndexOf("?v=");
            
            // video url
            string url = "";
            
            
            // Sets real video url in accordance to VideoUrl property value
            if (pos != -1)
            {
                url = "http://www.youtube.com/v/" + videoUrl.Substring(pos + 3) + "&amp;rel=1";
            }
            else
            {
                url = videoUrl;
            }

            // Ensure autoplay
            if (this.AutoPlay)
            {
                url += "&amp;autoplay=1";
            }

            if (this.AutoActivation)
            {
                ltlPlaceholder.Text = "<div class=\"VideoLikeContent\" id=\"YTPlaceholder_" + ltlScript.ClientID + "\" ></div>";

                // Register external script
                ScriptHelper.RegisterScriptFile(this.Page, "~/CMSWebParts/CommunityServices/YouTubeVideo_files/video.js");

                // Call function for video object insertion
                ltlScript.Text = BuildScriptBlock(url);
            }
            else
            {
                this.ltlPlaceholder.Text = "<div class=\"VideoLikeContent\" ><object type=\"application/x-shockwave-flash\" data=\"" + url + "\" width=\"" + this.Width + "\" height=\"" + this.Height + "\" id=\"VideoPlayback\">" +
                      "<param name=\"classid\" value=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" />\n" +
                      "<param name=\"codebase\" value=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" />\n" +
                      "<param name=\"movie\" value=\"" + url + "\" />" +
                      "<param name=\"allowScriptAcess\" value=\"sameDomain\" />" +
                      "<param name=\"quality\" value=\"best\" />" +
                      "<param name=\"scale\" value=\"noScale\" />" +
                      "<param name=\"pluginurl\" value=\"http://www.adobe.com/go/getflashplayer\" />\n" +
                      "<param name=\"salign\" value=\"TL\" />" +
                      "<param name=\"FlashVars\" value=\"playerMode=embedded\" />" +
                      "<param name=\"wmode\" value=\"transparent\" />\n" +
                      GetString("Flash.NotSupported") + "\n" +
                      "</object></div>";
            }
        }
    }

    
    /// <summary>
    /// Creates a script block which loads a YouTube video at runtime.
    /// </summary>
    /// <param name="additionalParams">URL to the video</param>
    /// <returns>Script block that will load a YouTube video</returns>
    private string BuildScriptBlock(string url)
    {
        string scriptBlock = string.Format("LoadYTVideo('YTPlaceholder_{0}', '{1}', {2}, {3});",
            ltlScript.ClientID,
            url,
            this.Width,
            this.Height);

        return ScriptHelper.GetScript(scriptBlock);
    }

    #endregion
}