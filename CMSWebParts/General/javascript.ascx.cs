using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;

public partial class CMSWebParts_General_javascript : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the inline JavaScript code.
    /// </summary>
    public string InlineScript
    {
        get
        {
            return ValidationHelper.GetString(GetValue("InlineScript"), string.Empty);
        }
        set
        {
            SetValue("InlineScript", value);
        }
    }


    /// <summary>
    /// Gets or sets the inline JavaScript code page location.
    /// </summary>
    public string InlineScriptPageLocation
    {
        get
        {
            return ValidationHelper.GetString(GetValue("InlineScriptPageLocation"), string.Empty);
        }
        set
        {
            SetValue("InlineScriptPageLocation", value);
        }
    }


    /// <summary>
    /// Indicates whether the script tags are generated or not.
    /// </summary>
    public bool GenerateScriptTags
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("GenerateScriptTags"), true);
        }
        set
        {
            SetValue("GenerateScriptTags", value);
        }
    }


    /// <summary>
    /// Gets or sets the linked file url.
    /// </summary>
    public string LinkedFile
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkedFile"), string.Empty);
        }
        set
        {
            SetValue("LinkedFile", value);
        }
    }


    /// <summary>
    /// Gets or sets the linked file url page location.
    /// </summary>
    public string LinkedFilePageLocation
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkedFilePageLocation"), string.Empty);
        }
        set
        {
            SetValue("LinkedFilePageLocation", value);
        }
    }

    #endregion


    #region "Page events"

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

    #endregion


    #region "Methods"

    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Include javascript only in live site or preview mode
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) || (CMSContext.ViewMode == ViewModeEnum.Preview))
            {
                // Render the inline script
                if (InlineScript.Trim() != string.Empty)
                {
                    string inlineScript = InlineScript;

                    // Check if script tags must be generated
                    if (GenerateScriptTags && (InlineScriptPageLocation.ToLower() != "submit"))
                    {
                        inlineScript = ScriptHelper.GetScript(InlineScript);
                    }
                    // Switch for script position on the page
                    switch (InlineScriptPageLocation.ToLower())
                    {
                        case "header":
                            Page.Header.Controls.Add(new LiteralControl(inlineScript));
                            break;

                        case "beginning":
                            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), ClientID + "inlinescript", inlineScript);
                            break;

                        case "inline":
                            ltlInlineScript.Text = inlineScript;
                            break;

                        case "startup":
                            ScriptHelper.RegisterStartupScript(Page, typeof(string), ClientID + "inlinescript", inlineScript);
                            break;

                        case "submit":
                            ScriptHelper.RegisterOnSubmitStatement(Page, typeof(string), ClientID + "inlinescript", inlineScript);
                            break;

                        default:
                            ltlInlineScript.Text = inlineScript;
                            break;
                    }
                }

                // Create linked js file
                if (LinkedFile.Trim() != string.Empty)
                {
                    string linkedFile = ScriptHelper.GetIncludeScript(ResolveUrl(LinkedFile));

                    // Switch for script position on the page
                    switch (LinkedFilePageLocation.ToLower())
                    {
                        case "header":
                            Page.Header.Controls.Add(new LiteralControl(linkedFile));
                            break;

                        case "beginning":
                            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), ClientID + "script", linkedFile);
                            break;

                        case "startup":
                            ScriptHelper.RegisterStartupScript(Page, typeof(string), ClientID + "script", linkedFile);
                            break;

                        default:
                            Page.Header.Controls.Add(new LiteralControl(linkedFile));
                            break;
                    }
                }
            }
        }
    }

    #endregion
}
