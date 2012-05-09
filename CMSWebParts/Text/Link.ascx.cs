using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSWebParts_Text_Link : CMSAbstractWebPart
{
    #region "Public properties

    /// <summary>
    /// Gets or sets link URL.
    /// </summary>
    public string LinkUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkUrl"), string.Empty);
        }
        set
        {
            SetValue("LinkUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets link text.
    /// </summary>
    public string LinkText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkText"), string.Empty);
        }
        set
        {
            SetValue("LinkText", value);
        }
    }


    /// <summary>
    /// Gets or sets link CSS class.
    /// </summary>
    public string LinkCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkCssClass"), string.Empty);
        }
        set
        {
            SetValue("LinkCssClass", value);
        }
    }


    /// <summary>
    /// Gets or sets link target.
    /// </summary>
    public string LinkTarget
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkTarget"), string.Empty);
        }
        set
        {
            SetValue("LinkTarget", value);
        }
    }


    /// <summary>
    /// Gets or sets image URL.
    /// </summary>
    public string ImageUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ImageUrl"), string.Empty);
        }
        set
        {
            SetValue("ImageUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets image alternate text.
    /// </summary>
    public string ImageAltText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ImageAltText"), string.Empty);
        }
        set
        {
            SetValue("ImageAltText", value);
        }
    }


    /// <summary>
    /// Gets or sets image CSS class.
    /// </summary>
    public string ImageCssClass
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ImageCssClass"), string.Empty);
        }
        set
        {
            SetValue("ImageCssClass", value);
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
    public void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // URL
            string resolvedUrl = URLHelper.ResolveUrl(LinkUrl);
            hyperLink.NavigateUrl = resolvedUrl;
            hyperLink.CssClass = LinkCssClass;
            hyperLink.Target = LinkTarget;

            bool isImageUrl = !string.IsNullOrEmpty(ImageUrl);

            // Link text
            lblText.Text = (!isImageUrl && string.IsNullOrEmpty(LinkText)) ? resolvedUrl : LinkText;

            // Image
            if (isImageUrl)
            {
                image.ImageUrl = URLHelper.ResolveUrl(ImageUrl);
                image.AlternateText = ImageAltText;
                image.CssClass = ImageCssClass;
            }
            else
            {
                image.Visible = false;
            }
        }
    }

    #endregion
}