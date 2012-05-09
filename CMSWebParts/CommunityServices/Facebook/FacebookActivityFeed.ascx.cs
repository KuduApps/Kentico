using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSWebParts_CommunityServices_Facebook_FacebookActivityFeed : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// The domain to show activity for.
    /// </summary>
    public string Domain
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Domain"), "");
        }
        set
        {
            this.SetValue("Domain", value);
        }
    }


    /// <summary>
    /// Reference parameter.
    /// </summary>
    public string RefParameter
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RefParameter"), "");
        }
        set
        {
            this.SetValue("RefParameter", value);
        }
    }


    /// <summary>
    /// Width of the web part in pixels.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), 300);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Height of the web part in pixels.
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
    /// Indicates whether to show facebook header or not.
    /// </summary>
    public bool ShowHeader
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowHeader"), true);
        }
        set
        {
            this.SetValue("ShowHeader", value);
        }
    }


    /// <summary>
    /// Color scheme of the web part.
    /// </summary>
    public string ColorScheme
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ColorScheme"), "");
        }
        set
        {
            this.SetValue("ColorScheme", value);
        }
    }


    /// <summary>
    /// Font of the web part.
    /// </summary>
    public string Font
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Font"), "");
        }
        set
        {
            this.SetValue("Font", value);
        }
    }


    /// <summary>
    /// Border color of the web part.
    /// </summary>
    public string BorderColor
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BorderColor"), "");
        }
        set
        {
            this.SetValue("BorderColor", value);
        }
    }


    /// <summary>
    /// Indicates whether to always show recommendations in the web part.
    /// </summary>
    public bool ShowRecommendations
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowRecommendations"), false);
        }
        set
        {
            this.SetValue("ShowRecommendations", value);
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
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            // Iframe code
            string query = null;
            string src = "http://www.facebook.com/plugins/activity.php";

            if (string.IsNullOrEmpty(Domain))
            {
                Domain = CMSContext.CurrentSite.DomainName;
            }

            if (!string.IsNullOrEmpty(BorderColor))
            {
                string borderColor = BorderColor;

                // Replace # if it is present
                borderColor = borderColor.Replace("#", "%23");

                query = URLHelper.AddUrlParameter(query, "border_color", borderColor);
            }

            if (!string.IsNullOrEmpty(Font))
            {
                query = URLHelper.AddUrlParameter(query, "font", Font);
            }

            if (!string.IsNullOrEmpty(RefParameter))
            {
                query = URLHelper.AddUrlParameter(query, "ref", RefParameter);
            }

            query = URLHelper.AddUrlParameter(query, "site", Domain);
            query = URLHelper.AddUrlParameter(query, "header", ShowHeader.ToString());
            query = URLHelper.AddUrlParameter(query, "width", Width.ToString());
            query = URLHelper.AddUrlParameter(query, "recommendations", ShowRecommendations.ToString());
            query = URLHelper.AddUrlParameter(query, "colorscheme", ColorScheme);
            query = URLHelper.AddUrlParameter(query, "height", Height.ToString());

            src = URLHelper.EncodeQueryString(URLHelper.AppendQuery(src, query));

            ltlActivityFeed.Text = "<iframe src=\"" + src + "\"";
            ltlActivityFeed.Text += " scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:" + Width + "px; height:" + Height + "px;\"></iframe>";

        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}