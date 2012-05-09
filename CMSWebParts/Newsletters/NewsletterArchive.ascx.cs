using System;
using System.Web.Caching;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Newsletter;

public partial class CMSWebParts_Newsletters_NewsletterArchive : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the newsletter code name.
    /// </summary>
    public string NewsletterName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NewsletterName"), "");
        }
        set
        {
            SetValue("NewsletterName", value);
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), "cms.root.newsletter_archive");
        }
        set
        {
            SetValue("TransformationName", value);
            repNewsArchive.TransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether only issues where mailout time is bigger than current time can be selected.
    /// </summary>
    public bool SelectOnlySendedIssues
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SelectOnlySendedIssues"), false);
        }
        set
        {
            SetValue("SelectOnlySendedIssues", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether value 'IgnoreShowInNewsletterArchive' for select issues will be ignored.
    /// </summary>
    public bool IgnoreShowInNewsletterArchive
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("IgnoreShowInNewsletterArchive"), false);
        }
        set
        {
            SetValue("IgnoreShowInNewsletterArchive", value);
        }
    }

    #endregion


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
        if (StopProcessing)
        {
            repNewsArchive.StopProcessing = true;
        }
        else
        {
            SetContext();

            Newsletter news = null;
            
            // Try to get data from cache
            using (CachedSection<Newsletter> cs = new CachedSection<Newsletter>(ref news, this.CacheMinutes, true, this.CacheItemName, "newsletterarchive", CMSContext.CurrentSiteName, NewsletterName))
            {
                if (cs.LoadData)
                {
                    // Get the data
                    news = NewsletterProvider.GetNewsletter(NewsletterName, CMSContext.CurrentSite.SiteID);

                    // Add data to the cache
                    if (CacheMinutes > 0)
                    {
                        cs.CacheDependency = GetCacheDependency();
                        cs.Data = news;
                    }
                }
            }

            ReleaseContext();

            if (news != null)
            {
                repNewsArchive.ControlContext = ControlContext;

                repNewsArchive.QueryName = "newsletter.issue.selectall";
                repNewsArchive.OrderBy = "IssueMailoutTime";

                string where = "(IssueNewsletterID = " + news.NewsletterID + ")";

                if (!IgnoreShowInNewsletterArchive)
                {
                    where += " AND (IssueShowInNewsletterArchive = 1)";
                }

                if (SelectOnlySendedIssues)
                {
                    where += " AND (IssueMailoutTime IS NOT NULL) AND (IssueMailoutTime < getDate())";
                }

                repNewsArchive.WhereCondition = where;
                repNewsArchive.TransformationName = TransformationName;
            }
        }
    }


    /// <summary>
    /// Reloads the data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        repNewsArchive.ReloadData(true);
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        repNewsArchive.ClearCache();
        string useCacheItemName = DataHelper.GetNotEmpty(CacheItemName, CacheHelper.BaseCacheKey + "|" + URLHelper.Url + "|" + ClientID + "|" + NewsletterName + "|" + CMSContext.CurrentSite.SiteID);
        CacheHelper.ClearCache(useCacheItemName);
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        Visible = !StopProcessing;

        if (!repNewsArchive.HasData())
        {
            Visible = false;
        }
        base.OnPreRender(e);
    }
}
