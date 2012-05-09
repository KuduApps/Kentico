using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;

public partial class CMSWebParts_CommunityServices_Facebook_FacebookComments : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Url to comment on.
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
    /// Number of posts.
    /// </summary>
    public int Posts
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Posts"), 0);
        }
        set
        {
            this.SetValue("Posts", value);
        }
    }


    /// <summary>
    /// Width of the web part in pixels.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), 500);
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
            return ValidationHelper.GetString(this.GetValue("ColorScheme"), string.Empty);
        }
        set
        {
            this.SetValue("ColorScheme", value);
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

            ltlComments.Text = "<div id=\"fb-root\"></div><script src=\"http://connect.facebook.net/" + culture + "/all.js#xfbml=1\"></script><fb:comments href=" +  URLHelper.GetAbsoluteUrl(url) + " num_posts=" + Posts.ToString() + " width=" + Width.ToString() + "></fb:comments>";
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