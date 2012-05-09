using System;

using CMS.GlobalHelper;
using CMS.PortalControls;

public partial class CMSWebParts_Media_Flash : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the URL of the flash to be displayed.
    /// </summary>
    public string FlashURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FlashURL"), "");
        }
        set
        {
            this.SetValue("FlashURL", value);
        }
    }


    /// <summary>
    /// Gets or sets additional parameters for player.
    /// </summary>
    public string AdditionalParameters
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AdditionalParameters"), "");
        }
        set
        {
            this.SetValue("AdditionalParameters", value);
        }
    }


    /// <summary>
    /// Gets or sets the width of the flash.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), 200);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Gets or sets the height of the flash.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Height"), 150);
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Gets or sets the quality of the flash.
    /// </summary>
    public string Quality
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Quality"), "best");
        }
        set
        {
            this.SetValue("Quality", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether flash is started automatically.
    /// </summary>
    public bool AutoPlay
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AutoPlay"), true);
        }
        set
        {
            this.SetValue("AutoPlay", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether flash after the end is automatically started again.
    /// </summary>
    public bool Loop
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("Loop"), true);
        }
        set
        {
            this.SetValue("Loop", value);
        }
    }


    /// <summary>
    /// Gets or sets the scale of the flash.
    /// </summary>
    public string Scale
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Scale"), "default");
        }
        set
        {
            this.SetValue("Scale", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether flash is automatically activated.
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
            string additionalParams = string.Empty;
            if (!String.IsNullOrEmpty(this.AdditionalParameters))
            {
                additionalParams = this.AdditionalParameters.Trim() + "\n";
            }

            if (this.AutoActivation)
            {
                ltlPlaceholder.Text = "<div class=\"VideoLikeContent\" id=\"FlashPlaceholder_" + ltlScript.ClientID + "\" ></div>";

                // Register external script
                ScriptHelper.RegisterScriptFile(this.Page, "~/CMSWebParts/Media/Flash_files/flash.js");

                // Call function for flash object insertion               
                ltlScript.Text = BuildScriptBlock(additionalParams);
            }
            else
            {
                // Create flash
                this.ltlPlaceholder.Text = "<div class=\"VideoLikeContent\" ><object type=\"application/x-shockwave-flash\" width=\"" + this.Width + "\" height=\"" + this.Height + "\" data=\"" + HTMLHelper.HTMLEncode(ResolveUrl(this.FlashURL)) + "\">\n" +
                    "<param name=\"classid\" value=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" />\n" +
                    "<param name=\"codebase\" value=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0\" />\n" +
                    "<param name=\"movie\" value=\"" + HTMLHelper.HTMLEncode(ResolveUrl(this.FlashURL)) + "\" />\n" +
                    "<param name=\"quality\" value=\"" + HTMLHelper.HTMLEncode(this.Quality) + "\" />\n" +
                    "<param name=\"scale\" value=\"" + HTMLHelper.HTMLEncode(this.Scale) + "\" />\n" +
                    "<param name=\"play\" value=\"" + this.AutoPlay + "\" />\n" +
                    "<param name=\"loop\" value=\"" + this.Loop + "\" />\n" +
                    "<param name=\"pluginurl\" value=\"http://www.adobe.com/go/getflashplayer\" />\n" +
                    "<param name=\"wmode\" value=\"transparent\" />\n" +
                    additionalParams +
                    GetString("Flash.NotSupported") + "\n" +
                    "</object></div>";
            }
        }
    }


    /// <summary>
    /// Creates a script block which loads a Flash object at runtime.
    /// </summary>
    /// <param name="additionalParams">Additional parameters for the script</param>
    /// <returns>Script block that will load a Flash object</returns>
    private string BuildScriptBlock(string additionalParams)
    {
        string scriptBlock = string.Format(@"LoadFlash('FlashPlaceholder_{0}', '{1}', {2}, {3}, '{4}', '{5}', {6}, {7}, {8}, {9})",
            ltlScript.ClientID,
            HTMLHelper.HTMLEncode(ResolveUrl(this.FlashURL)),
            this.Width,
            this.Height,
            HTMLHelper.HTMLEncode(this.Quality),
            HTMLHelper.HTMLEncode(this.Scale),
            this.AutoPlay.ToString().ToLower(),
            this.Loop.ToString().ToLower(),
            ScriptHelper.GetString(GetString("Flash.NotSupported")),
            string.IsNullOrEmpty(additionalParams) ? "''" : additionalParams);

        return ScriptHelper.GetScript(scriptBlock);
    }

    #endregion
}