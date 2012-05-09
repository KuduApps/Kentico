using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSWebParts_CommunityServices_Facebook_FacebookFacepile: CMSAbstractWebPart
{
    #region "Constants"

    private const int DEFAULT_WIDTH = 200;

    #endregion


    #region "Properties"

    /// <summary>
    /// Facebook page URL
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
    /// Width of the web part in pixels
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), DEFAULT_WIDTH);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Size of the web part
    /// </summary>
    public string Size
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Size"), "");
        }
        set
        {
            this.SetValue("Size", value);
        }
    }


    /// <summary>
    /// Maximum number of rows with faces
    /// </summary>
    public int RowsNumber
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("RowsNumber"), 1);
        }
        set
        {
            this.SetValue("RowsNumber", value);
        }
    }


    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties
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
            string src = "http://www.facebook.com/plugins/facepile.php";

            query = URLHelper.AddUrlParameter(query, "href", URLHelper.EncodeQueryString(FBPageUrl));
            query = URLHelper.AddUrlParameter(query, "size", Size.ToString());
            query = URLHelper.AddUrlParameter(query, "width", Width.ToString());
            query = URLHelper.AddUrlParameter(query, "max_rows", RowsNumber.ToString());

            src = URLHelper.EncodeQueryString(URLHelper.AppendQuery(src, query));

            ltlLikeBox.Text = "<iframe src=\"" + src + "\"";
            ltlLikeBox.Text += " scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; width:" + Width + "px;\"></iframe>";
        }
    }


    /// <summary>
    /// Reloads the control data
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion
}