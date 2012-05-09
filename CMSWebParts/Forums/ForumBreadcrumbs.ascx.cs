using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;

public partial class CMSWebParts_Forums_ForumBreadcrumbs : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the breadcrumbs separator.
    /// </summary>
    public string BreadcrumbsSeparator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbsSeparator"), ctrlForumBreadcrumbs.BreadcrumbSeparator);
        }
        set
        {
            this.SetValue("BreadcrumbsSeparator", value);
            ctrlForumBreadcrumbs.BreadcrumbSeparator = value;
        }
    }


    /// <summary>
    /// Gets or sets the breadcrumbs prefix.
    /// </summary>
    public string BreadcrumbPrefix
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbPrefix"), ctrlForumBreadcrumbs.BreadcrumbPrefix);
        }
        set
        {
            this.SetValue("BreadcrumbPrefix", value);
            ctrlForumBreadcrumbs.BreadcrumbPrefix = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether current item should be rendered as link.
    /// </summary>
    public bool UseLinkForCurrentItem
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseLinkForCurrentItem"), ctrlForumBreadcrumbs.UseLinkForCurrentItem);
        }
        set
        {
            SetValue("UseLinkForCurrentItem", value);
            ctrlForumBreadcrumbs.UseLinkForCurrentItem = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether forum group should be displayed in breadcrumbs.
    /// </summary>
    public bool DisplayGroup
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayGroup"), ctrlForumBreadcrumbs.DisplayGroup);
        }
        set
        {
            this.SetValue("DisplayGroup", value);
            ctrlForumBreadcrumbs.DisplayGroup = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether thread name should be displayed in breadcrumbs.
    /// </summary>
    public bool DisplayThread
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayThread"), ctrlForumBreadcrumbs.DisplayThread);
        }
        set
        {
            this.SetValue("DisplayThread", value);
            ctrlForumBreadcrumbs.DisplayThread = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether threads names should be displayed in breadcrumbs.
    /// </summary>
    public bool DisplayThreads
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayThreads"), ctrlForumBreadcrumbs.DisplayThreads);
        }
        set
        {
            this.SetValue("DisplayThreads", value);
            ctrlForumBreadcrumbs.DisplayThreads = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether breadcrumbs should be hidden on forum group page
    /// This option hides only forum breadcrumbs, breadcrumbs prefix is allways visible if is defined
    /// </summary>
    public bool HideBreadcrumbsOnForumGroupPage
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideBreadcrumbsOnForumGroupPage"), ctrlForumBreadcrumbs.HideBreadcrumbsOnForumGroupPage);
        }
        set
        {
            this.SetValue("HideBreadcrumbsOnForumGroupPage", value);
            ctrlForumBreadcrumbs.HideBreadcrumbsOnForumGroupPage = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether friendly URL should be used.
    /// </summary>
    public bool UseFriendlyURL
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseFriendlyURL"), ctrlForumBreadcrumbs.UseFriendlyURL);
        }
        set
        {
            this.SetValue("UseFriendlyURL", value);
            ctrlForumBreadcrumbs.UseFriendlyURL = value;
        }
    }


    /// <summary>
    /// Gets or sets the forum base URL without extension.
    /// </summary>
    public string FriendlyBaseURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FriendlyBaseURLs"), ctrlForumBreadcrumbs.FriendlyBaseURL);
        }
        set
        {
            this.SetValue("FriendlyBaseURLs", value);
            this.ctrlForumBreadcrumbs.FriendlyBaseURL = value;
        }
    }


    /// <summary>
    /// Gets or sets the friendly URL extension. For extension less URLs sets it to empty string.
    /// </summary>
    public string FriendlyURLExtension
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FriendlyURLExtension"), ctrlForumBreadcrumbs.FriendlyURLExtension);
        }
        set
        {
            this.SetValue("FriendlyURLExtension", value);
            this.ctrlForumBreadcrumbs.FriendlyURLExtension = value;
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
            // Do nothing
        }
        else
        {
            ctrlForumBreadcrumbs.BreadcrumbSeparator = this.BreadcrumbsSeparator;
            ctrlForumBreadcrumbs.BreadcrumbPrefix = this.BreadcrumbPrefix;
            ctrlForumBreadcrumbs.UseLinkForCurrentItem = this.UseLinkForCurrentItem;
            ctrlForumBreadcrumbs.HideBreadcrumbsOnForumGroupPage = this.HideBreadcrumbsOnForumGroupPage;

            ctrlForumBreadcrumbs.DisplayGroup = this.DisplayGroup;
            ctrlForumBreadcrumbs.DisplayThread = this.DisplayThread;
            ctrlForumBreadcrumbs.DisplayThreads = this.DisplayThreads;
            
            ctrlForumBreadcrumbs.UseFriendlyURL = this.UseFriendlyURL;
            ctrlForumBreadcrumbs.FriendlyBaseURL = this.FriendlyBaseURL;
            ctrlForumBreadcrumbs.FriendlyURLExtension = this.FriendlyURLExtension;
        }
    }

    #endregion
}
