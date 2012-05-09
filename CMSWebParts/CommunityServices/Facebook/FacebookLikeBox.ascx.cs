using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSWebParts_CommunityServices_Facebook_FacebookLikeBox : CMSAbstractWebPart
{
    #region "Constants"

    const int heightDefault = 63;
    const int heightStream = 392;
    const int heightStreamFaces = 555;
    const int heightFaces = 255;

    #endregion


    #region "Properties"

    /// <summary>
    /// Facebook page URL.
    /// </summary>
    public string FBPageUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FBPageUrl"), "");
        }
        set
        {
            this.SetValue("FBPageUrl", value);
        }
    }


    /// <summary>
    /// Width of the web part in pixels.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), 292);
        }
        set
        {
            this.SetValue("Width", value);
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
    /// Indicates whether to show profile photos or not.
    /// </summary>
    public bool ShowFaces
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowFaces"), true);
        }
        set
        {
            this.SetValue("ShowFaces", value);
        }
    }


    /// <summary>
    /// Indicates whether to display a stream of the latest posts.
    /// </summary>
    public bool ShowStream
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowStream"), true);
        }
        set
        {
            this.SetValue("ShowStream", value);
        }
    }


    /// <summary>
    /// Indicates whether to show Facebook header or not.
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
            string src = "http://www.facebook.com/plugins/likebox.php";

            // Default height if nothing additional is shown
            int height = heightDefault;

            // If faces and stream are shown
            if (ShowFaces && ShowStream)
            {
                height = heightStreamFaces;
            }
            // If only stream is shown
            else if (ShowStream)
            {
                height = heightStream;
            }
            // If only faces are shown
            else if (ShowFaces)
            {
                height = heightFaces;
            }

            // If stream or faces are shown and header is too
            if (ShowHeader && (ShowFaces || ShowStream))
            {
                height = height + 35;
            }

            query = URLHelper.AddUrlParameter(query, "href", FBPageUrl);
            query = URLHelper.AddUrlParameter(query, "header", ShowHeader.ToString());
            query = URLHelper.AddUrlParameter(query, "width", Width.ToString());
            query = URLHelper.AddUrlParameter(query, "show_faces", ShowFaces.ToString());
            query = URLHelper.AddUrlParameter(query, "stream", ShowStream.ToString());
            query = URLHelper.AddUrlParameter(query, "colorscheme", ColorScheme);
            query = URLHelper.AddUrlParameter(query, "height", height.ToString());

            src = URLHelper.EncodeQueryString(URLHelper.AppendQuery(src, query));

            ltlLikeBox.Text = "<iframe src=\"" + src + "\"";
            ltlLikeBox.Text += " scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:" + Width + "px; height:" + height + "px;\"></iframe>";

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