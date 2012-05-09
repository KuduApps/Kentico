using System;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Membership_Controls_Subscriptions : CMSAdminControl
{
    #region "Private variables"

    private bool mShowBlogs = true;
    private bool mShowNewsletters = true;
    private bool mShowMessageBoards = true;
    private bool mSendConfirmationMail = true;
    private int mUserId = 0;
    private int mSiteId = 0;

    private CMSAdminControl ucBlogs = null;
    private CMSAdminControl ucNewsletters = null;
    private CMSAdminControl ucBoards = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether blog post subscriptions is shown.
    /// </summary>
    public bool ShowBlogs
    {
        get
        {            
            return mShowBlogs;
        }
        set
        {
            mShowBlogs = value;
        }
    }


    /// <summary>
    /// Indicates whether messageboards subscriptions is shown.
    /// </summary>
    public bool ShowMessageBoards
    {
        get
        {
            return mShowMessageBoards;
        }
        set
        {
            mShowMessageBoards = value;
        }
    }


    /// <summary>
    /// Indicates whether newsletters subscriptions is shown.
    /// </summary>
    public bool ShowNewsletters
    {
        get
        {
            return mShowNewsletters;
        }
        set
        {
            mShowNewsletters = value;
        }
    }


    /// <summary>
    /// Indicates whether send email when (un)subscribed.
    /// </summary>
    public bool SendConfirmationMail
    {
        get
        {
            return mSendConfirmationMail;
        }
        set
        {
            mSendConfirmationMail = value;
        }
    }


    /// <summary>
    /// Gets or sets user ID.
    /// </summary>
    public int UserID
    {
        get
        {
            return mUserId;
        }
        set
        {
            mUserId = value;
        }
    }


    /// <summary>
    /// Gets or sets site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
            if (ucNewsletters != null)
            {
                ucNewsletters.SetValue("siteid", value);
            }
            if (ucBlogs != null)
            {
                ucBlogs.SetValue("siteid", value);
            }
            if (ucBoards != null)
            {
                ucBoards.SetValue("siteid", value);
            }
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!StopProcessing)
        {
            SetupControl();
        }
    }


    protected void SetupControl()
    {
        // Get current user if UserID is not defined
        int userId = UserID;
        if ((userId <= 0) && (CMSContext.CurrentUser != null))
        {
            userId = CMSContext.CurrentUser.UserID;
        }

        // Use current site if SiteID is not defined
        int siteId = SiteID;
        if (siteId <= 0)
        {
            siteId = CMSContext.CurrentSiteID;
        }
        string siteName = SiteInfoProvider.GetSiteName(siteId);

        bool firstInserted = false;

        // Try to init newsletters subscriptions
        if (ShowNewsletters && ModuleEntry.IsModuleLoaded(ModuleEntry.NEWSLETTER))        
        {
            ucNewsletters = Page.LoadControl("~/CMSModules/Newsletters/Controls/MySubscriptions.ascx") as CMSAdminControl;
            if (ucNewsletters != null)
            {
                pnlNewsletters.Visible = true;
                pnlNewsletters.GroupingText = "&nbsp;" + GetString("Subscriptions.Newsletters") + "&nbsp;";
                ucNewsletters.ID = "ucNewsletters";
                ucNewsletters.SetValue("externaluse", true);
                ucNewsletters.SetValue("forcedvisible", true);
                ucNewsletters.SetValue("userid", userId);
                ucNewsletters.SetValue("siteid", siteId);
                ucNewsletters.SetValue("sendconfirmationemail", SendConfirmationMail);
                ucNewsletters.StopProcessing = StopProcessing;
                ucNewsletters.SetValue("islivesite", this.IsLiveSite);

                pnlNewsletters.Controls.Clear();
                pnlNewsletters.Controls.Add(new LiteralControl("<div class=\"SubscriptionsGroup\">"));
                pnlNewsletters.Controls.Add(ucNewsletters);
                pnlNewsletters.Controls.Add(new LiteralControl("</div>"));

                firstInserted = true;

                ucNewsletters.OnCheckPermissions += ucNewsletters_OnCheckPermissions;
            }
        }

        // Try to init blog post subscriptions
        if (ShowBlogs && ModuleEntry.IsModuleLoaded(ModuleEntry.BLOGS) && ResourceSiteInfoProvider.IsResourceOnSite(ModuleEntry.BLOGS, siteName))
        {
            ucBlogs = Page.LoadControl("~/CMSModules/Blogs/Controls/BlogPostSubscriptions.ascx") as CMSAdminControl;
            if (ucBlogs != null)
            {
                pnlBlogs.Visible = true;
                pnlBlogs.GroupingText = "&nbsp;" + GetString("Subscriptions.BlogPosts") + "&nbsp;";
                ucBlogs.ID = "ucBlogs";
                ucBlogs.SetValue("userid", userId);
                ucBlogs.SetValue("siteid", siteId);
                ucBlogs.StopProcessing = StopProcessing;
                ucBlogs.OnCheckPermissions += ucBlogs_OnCheckPermissions;
                ucBlogs.SetValue("islivesite", this.IsLiveSite);

                if (firstInserted)
                {
                    pnlBlogs.Attributes.Add("class", "SubscriptionsPanel");
                }

                pnlBlogs.Controls.Clear();
                pnlBlogs.Controls.Add(new LiteralControl("<div class=\"SubscriptionsGroup\">"));
                pnlBlogs.Controls.Add(ucBlogs);
                pnlBlogs.Controls.Add(new LiteralControl("</div>"));

                firstInserted = true;
            }
        }

        // Try to init message board subscriptions
        if (ShowMessageBoards && ModuleEntry.IsModuleLoaded(ModuleEntry.MESSAGEBOARD) && ResourceSiteInfoProvider.IsResourceOnSite(ModuleEntry.MESSAGEBOARD, siteName))
        {
            ucBoards = Page.LoadControl("~/CMSModules/MessageBoards/Controls/Boards/BoardUserSubscriptions.ascx") as CMSAdminControl;
            if (ucBoards != null)
            {
                pnlBoards.Visible = true;
                pnlBoards.GroupingText = "&nbsp;" + GetString("Subscriptions.MessageBoards") + "&nbsp;";
                ucBoards.ID = "ucBoards";
                ucBoards.SetValue("userid", userId);
                ucBoards.SetValue("siteid", siteId);
                ucBoards.StopProcessing = StopProcessing;
                ucBoards.OnCheckPermissions += ucBoards_OnCheckPermissions;
                ucBoards.SetValue("islivesite", this.IsLiveSite);

                if (firstInserted)
                {
                    pnlBoards.Attributes.Add("class", "SubscriptionsPanel");
                }
                pnlBoards.Controls.Clear();
                pnlBoards.Controls.Add(new LiteralControl("<div class=\"SubscriptionsGroup\">"));
                pnlBoards.Controls.Add(ucBoards);
                pnlBoards.Controls.Add(new LiteralControl("</div>"));
            }
        }
    }



    #region "Security"

    protected void ucNewsletters_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }


    protected void ucBlogs_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }


    protected void ucBoards_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);
        switch (propertyName.ToLower())
        {
            case "userid":
                UserID = ValidationHelper.GetInteger(value, 0);
                break;
            case "siteid":
                SiteID = ValidationHelper.GetInteger(value, 0);
                break;
            case "showblogs":
                ShowBlogs = ValidationHelper.GetBoolean(value, true);
                break;
            case "showmessageboards":
                ShowMessageBoards = ValidationHelper.GetBoolean(value, true);
                break;
            case "shownewsletters":
                ShowNewsletters = ValidationHelper.GetBoolean(value, true);
                break;
            case "sendconfirmationemail":
                SendConfirmationMail = ValidationHelper.GetBoolean(value, true);
                break;

        }
    }


    /// <summary>
    /// Reloads the data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }
}
