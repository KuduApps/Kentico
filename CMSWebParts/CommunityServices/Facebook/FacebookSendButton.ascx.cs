using System;
using System.Web.UI;
using System.Text;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;

public partial class CMSWebParts_CommunityServices_Facebook_FacebookSendButton : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Url to send.
    /// </summary>
    public string Url
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("URL"), string.Empty);
        }
        set
        {
            this.SetValue("URL", value);
        }
    }


    /// <summary>
    /// Font of the text in iframe.
    /// </summary>
    public string Font
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Font"), "arial");
        }
        set
        {
            SetValue("Font", value);
        }
    }


    /// <summary>
    /// Color scheme of the button and text.
    /// </summary>
    public string ColorScheme
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ColorScheme"), "light");
        }
        set
        {
            SetValue("ColorScheme", value);
        }
    }


    /// <summary>
    /// Indicates whether to generate meta data tags or not.
    /// </summary>
    public bool GenerateMetaData
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("GenerateMetaData"), false);
        }
        set
        {
            SetValue("GenerateMetaData", value);
        }
    }


    /// <summary>
    /// Meta data title.
    /// </summary>
    public string MetaTitle
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MetaTitle"), "");
        }
        set
        {
            SetValue("MetaTitle", value);
        }
    }


    /// <summary>
    /// Meta data type.
    /// </summary>
    public string MetaType
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MetaType"), "");
        }
        set
        {
            SetValue("MetaType", value);
        }
    }


    /// <summary>
    /// Meta data URL.
    /// </summary>
    public string MetaUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MetaUrl"), "");
        }
        set
        {
            SetValue("MetaUrl", value);
        }
    }

    
    /// <summary>
    /// Meta data site name.
    /// </summary>
    public string MetaSiteName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MetaSiteName"), "");
        }
        set
        {
            SetValue("MetaSiteName", value);
        }
    }


    /// <summary>
    /// Meta data image URL.
    /// </summary>
    public string MetaImage
    {
        get
        {
            return ValidationHelper.GetString(GetValue("MetaImage"), "");
        }
        set
        {
            SetValue("MetaImage", value);
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Loads the web part content.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Sets up the control.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // If paramater URL is empty, set URL of current document
            string url = Url;
            if (string.IsNullOrEmpty(url) && (CMSContext.CurrentDocument != null))
            {
                TreeNode node = CMSContext.CurrentDocument;
                url = CMSContext.GetUrl(node.NodeAliasPath, node.DocumentUrlPath, CMSContext.CurrentSiteName);
            }
            else
            {
                url = ResolveUrl(url);
            }

            // Get culture code
            string culture = CultureHelper.GetFacebookCulture(CMSContext.PreferredCultureCode);

            // Get FB code
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"fb-root\"></div><script src=\"http://connect.facebook.net/", culture, "/all.js#xfbml=1\"></script><fb:send");
            sb.Append(" href=\"", URLHelper.GetAbsoluteUrl(url), "\"");
            sb.Append(" font=\"", Font, "\"");
            sb.Append(" colorscheme=\"", ColorScheme, "\"");
            sb.Append("></fb:send>");
            ltlSendButtonCode.Text = sb.ToString();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Generate meta tags
            if (GenerateMetaData)
            {
                StringBuilder sb = new StringBuilder();

                if (MetaTitle != "")
                {
                    sb.AppendLine("<meta property=\"og:title\" content=\"" + MetaTitle + "\" />");
                }
                if (MetaType != "")
                {
                    sb.AppendLine("<meta property=\"og:type\" content=\"" + MetaType + "\" />");
                }
                if (MetaSiteName != "")
                {
                    sb.AppendLine("<meta property=\"og:site_name\" content=\"" + MetaSiteName + "\" />");
                }
                if (MetaImage != "")
                {
                    sb.AppendLine("<meta property=\"og:image\" content=\"" + MetaImage + "\" />");
                }
                if (MetaUrl != "")
                {
                    sb.AppendLine("<meta property=\"og:url\" content=\"" + MetaUrl + "\" />");
                }

                Page.Header.Controls.Add(new LiteralControl(sb.ToString()));
            }
        }
    }

    #endregion
}
