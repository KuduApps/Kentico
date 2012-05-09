using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Forums;
using CMS.SiteProvider;
using CMS.ExtendedControls;

public partial class CMSWebParts_Forums_ForumFavorites : CMSAbstractWebPart
{
    #region "Private variables"

    protected string mDeleteImageUrl;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether editing actions are allowed.
    /// </summary>
    public bool AllowEditing
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowEditing"), true);
        }
        set
        {
            this.SetValue("AllowEditing", value);
        }
    }


    /// <summary>
    /// Gets or sets URL of the delete button image.
    /// </summary>
    public string DeleteImageUrl
    {
        get
        {
            return URLHelper.ResolveUrl(DataHelper.GetNotEmpty(this.GetValue("DeleteImageUrl"), GetImageUrl("CMSModules/CMS_Forums/delete.png")));
        }
        set
        {
            mDeleteImageUrl = value;
            this.SetValue("DeleteImageUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets the forums URL.
    /// </summary>
    public string ForumUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ForumUrl"), "");
        }
        set
        {
            this.SetValue("ForumUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets the site name of favorites.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), "");
        }
        set
        {
            this.SetValue("SiteName", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.rptFavorites.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.rptFavorites.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed when there is no data.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.rptFavorites.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.rptFavorites.ZeroRowsText = value;
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
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
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
            string jsScript = "var favDelMessage ='" + GetString("general.confirmdelete") + "';";
            jsScript += "function ForumFavoritesSetValue(id){ document.getElementById('"+hdnValue.ClientID+"').value = id;  }";

            ltlMessage.Text = ScriptHelper.GetScript(jsScript);

            mDeleteImageUrl = this.DeleteImageUrl;

            rptFavorites.ZeroRowsText = this.ZeroRowsText;
            rptFavorites.HideControlForZeroRows = this.HideControlForZeroRows;
            BindData();
        }
    }


    /// <summary>
    /// OnPreRender override - Bind data.
    /// </summary>
    /// <param name="e">Event args</param>
    protected override void OnPreRender(EventArgs e)
    {
        bool dataBinded = false;

        if (this.UseUpdatePanel)
        {
            int favoriteId = ValidationHelper.GetInteger(hdnValue.Value, 0);
            if (favoriteId > 0)
            {
                ForumUserFavoritesInfoProvider.DeleteForumUserFavoritesInfo(favoriteId);
                ClearCache();
                BindData();
                dataBinded = true;

                if (this.UpdatePanel != null)
                {
                    this.UpdatePanel.Update();
                }
            }
        }
        
        if (!dataBinded)
        {
            BindData();
        }

        hdnValue.Value = null;
        base.OnPreRender(e);
    }



    /// <summary>
    /// Bind data to repeater.
    /// </summary>
    private void BindData()
    {
        if (CMSContext.CurrentUser != null)
        {
            int userId = CMSContext.CurrentUser.UserID;
            int siteId = CMSContext.CurrentSiteID;

            // If sitename was specified
            if (this.SiteName != String.Empty)
            {
                // Get site ID
                SiteInfo si = SiteInfoProvider.GetSiteInfo(this.SiteName);
                if (si != null)
                {
                    siteId = si.SiteID;
                }
            }

            // Get user favorites
            DataSet ds = null;
            
            // Try to get data from cache
            using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref ds, this.CacheMinutes, true, this.CacheItemName, "forumfavorites", userId, siteId))
            {
                if (cs.LoadData)
                {
                    // Get the data
                    ds = ForumUserFavoritesInfoProvider.GetFavorites(userId, siteId);

                    // Save to the cache
                    if (cs.Cached)
                    {
                        // Save to the cache
                        cs.CacheDependency = GetCacheDependency();
                        cs.Data = ds;
                    }
                }
            }

            // Bind data, even empty dataset - delete of last item                     
            rptFavorites.DataSource = ds;
            rptFavorites.DataBind();

            // Hide control if no data
            if (DataHelper.DataSourceIsEmpty(ds))
            {
                if (this.HideControlForZeroRows)
                {
                    this.Visible = false;
                }
                else
                {
                    plcEmptyDSTagEnd.Visible = true;
                    plcEmptyDSTagBegin.Visible = true;
                }
            }
            else
            {
                plcEmptyDSTagEnd.Visible = false;
                plcEmptyDSTagBegin.Visible = false;
            }
        }
    }


    /// <summary>
    /// Returns link the the forum.
    /// </summary>
    /// <param name="favoritePostId">Pots id</param>
    /// <param name="favoriteForumId">Forum id</param>    
    protected string GetFavoriteLink(object favoritePostId, object favoriteForumId, object postForumID, object postIDPath)
    {
        int postId = ValidationHelper.GetInteger(favoritePostId, 0);
        int forumId = ValidationHelper.GetInteger(favoriteForumId, 0);
        int postForumId = ValidationHelper.GetInteger(postForumID, forumId);
        string postIdPath = ValidationHelper.GetString(postIDPath, "");

        string link = "#";

        // Post favorite
        if (postId > 0)
        {
            ForumInfo forumInfo = ForumInfoProvider.GetForumInfo(postForumId);
            if (forumInfo != null)
            {

                // If forum URL is not set, try to use base forum url
                string forumUrl = (this.ForumUrl == String.Empty) ? forumInfo.ForumBaseUrl : this.ForumUrl;
                int threadId = ForumPostInfoProvider.GetPostRootFromIDPath(postIdPath);

                if (String.IsNullOrEmpty(forumUrl))
                {
                    forumUrl = URLHelper.CurrentURL;
                }

                link = URLHelper.UpdateParameterInUrl(ResolveUrl(forumUrl), "forumid", postForumId.ToString());
                link = URLHelper.UpdateParameterInUrl(link, "threadid", threadId.ToString());
                link = URLHelper.RemoveParameterFromUrl(link, "thread");
                link = URLHelper.RemoveParameterFromUrl(link, "mode");
                link = URLHelper.RemoveParameterFromUrl(link, "postid");
                link = URLHelper.RemoveParameterFromUrl(link, "replyto");
                link = URLHelper.RemoveParameterFromUrl(link, "subscribeto");
            }
        }

        // Forum favorite
        else if (forumId > 0)
        {
            ForumInfo forumInfo = ForumInfoProvider.GetForumInfo(forumId);
            if (forumInfo != null)
            {
                // If forum URL is not set, try to use base forum url
                string forumUrl = (this.ForumUrl == String.Empty) ? forumInfo.ForumBaseUrl : this.ForumUrl;

                if (String.IsNullOrEmpty(forumUrl))
                {
                    forumUrl = URLHelper.CurrentURL;
                }

                link = URLHelper.UpdateParameterInUrl(ResolveUrl(forumUrl), "forumid", forumId.ToString());
                link = URLHelper.RemoveParameterFromUrl(link, "threadid");
                link = URLHelper.RemoveParameterFromUrl(link, "thread");
                link = URLHelper.RemoveParameterFromUrl(link, "mode");
                link = URLHelper.RemoveParameterFromUrl(link, "postid");
                link = URLHelper.RemoveParameterFromUrl(link, "replyto");
                link = URLHelper.RemoveParameterFromUrl(link, "subscribeto");
            }
        }

        return HTMLHelper.HTMLEncode(link);
    }


    /// <summary>
    /// Handles delete button action - deletes user favorite.
    /// </summary>
    protected void btnDelete_OnCommand(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            int favoriteId = ValidationHelper.GetInteger(e.CommandArgument, 0);
            ForumUserFavoritesInfoProvider.DeleteForumUserFavoritesInfo(favoriteId);
            ClearCache();
            BindData();
        }
    }

    

    /// <summary>
    /// Clear cache.
    /// </summary>
    public override void ClearCache()
    {
        int userId = CMSContext.CurrentUser.UserID;
        int siteId = CMSContext.CurrentSiteID;

        // If sitename was specified
        if (this.SiteName != String.Empty)
        {
            // Get site ID
            SiteInfo si = SiteInfoProvider.GetSiteInfo(this.SiteName);
            if (si != null)
            {
                siteId = si.SiteID;
            }
        }

        string useCacheItemName = DataHelper.GetNotEmpty(this.CacheItemName, CacheHelper.BaseCacheKey + "|" + this.ClientID + "|" + userId + "|" + siteId) + "|forumfavorites";
        CacheHelper.Remove(useCacheItemName);

        base.ClearCache();
    }


    /// <summary>
    /// Gets the default cache dependencies for the data source.
    /// </summary>
    public override string GetDefaultCacheDependendencies()
    {
        // Get default dependencies
        string result = base.GetDefaultCacheDependendencies();

        if (result != null)
        {
            result += "\n";
        }

        result += "forums.forumuserfavorites|byuserid|" + CMSContext.CurrentUser.UserID;
        return result;
    }

    #endregion
}
