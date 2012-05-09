using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.Caching;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;

public partial class CMSWebParts_Membership_Users_OnlineUsers : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Select only top n users.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("SelectTopN"), 0);
        }
        set
        {
            SetValue("SelectTopN", value);
        }
    }


    /// <summary>
    /// Select only users localized in specified path.  
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Path"), "");
        }
        set
        {
            SetValue("Path", value);
        }
    }


    /// <summary>
    /// Gets or sets the transformation name.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), "");
        }
        set
        {
            SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets the additional info text.
    /// </summary>
    public string AdditionalInfoText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("AdditionalInfoText"), "");
        }
        set
        {
            SetValue("AdditionalInfoText", value);
        }
    }


    /// <summary>
    /// Gets or sets the no users on-line text.
    /// </summary>
    public string NoUsersOnlineText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NoUsersOnlineText"), "");
        }
        set
        {
            SetValue("NoUsersOnlineText", value);
        }
    }


    /// <summary>
    /// Gets or sets columns to be retrieved from database.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Columns"), "");
        }
        set
        {
            SetValue("Columns", value);
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// OnPreRender override method.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        SetupControl();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Setups control properties.
    /// </summary>
    protected void SetupControl()
    {
        // Check StopProcessing property
        if (StopProcessing)
        {
            Visible = false;
        }
        else
        {
            SetContext();

            DataSet users = null;
            bool transLoaded = false;

            // Load transformation
            if (!string.IsNullOrEmpty(TransformationName))
            {
                repUsers.ItemTemplate = CMSDataProperties.LoadTransformation(this, TransformationName, false);
                transLoaded = true;
            }

            if ((transLoaded) || (!String.IsNullOrEmpty(Path)))
            {
                // Try to get data from cache
                using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref users, this.CacheMinutes, true, this.CacheItemName, "onlineusers", CMSContext.CurrentSiteName, SelectTopN, Columns, Path))
                {
                    if (cs.LoadData)
                    {
                        // Get the data
                        users = SessionManager.GetOnlineUsers(null, null, SelectTopN, Columns, CMSContext.ResolveCurrentPath(Path), CMSContext.CurrentSiteName, false, false);

                        // Prepare the cache dependency
                        if (cs.Cached)
                        {
                            cs.CacheDependency = GetCacheDependency();
                            cs.Data = users;
                        }
                    }
                }

                // Data bind
                if (!DataHelper.DataSourceIsEmpty(users))
                {
                    // Set to repeater
                    repUsers.DataSource = users;
                    repUsers.DataBind();
                }
            }

            int authenticated = 0;
            int publicUsers = 0;

            string numbers = string.Empty;

            // Get or generate cache item name
            string cacheItemNameNumbers = this.CacheItemName;
            if (!string.IsNullOrEmpty(cacheItemNameNumbers))
            {
                cacheItemNameNumbers += "Number";
            }

            // Try to get data from cache
            using (CachedSection<string> cs = new CachedSection<string>(ref numbers, this.CacheMinutes, true, cacheItemNameNumbers, "onlineusersnumber", CMSContext.CurrentSiteName, Path))
            {
                if (cs.LoadData)
                {
                    // Get the data
                    SessionManager.GetUsersNumber(CurrentSiteName, CMSContext.ResolveCurrentPath(Path), false, false, out publicUsers, out authenticated);

                    // Save to the cache
                    if (cs.Cached)
                    {
                        cs.CacheDependency = GetCacheDependency();
                        cs.Data = publicUsers.ToString() + ";" + authenticated.ToString();
                    }
                }
                else
                {
                    // Retrieved from cache
                    string[] nums = numbers.Split(';');

                    publicUsers = ValidationHelper.GetInteger(nums[0], 0);
                    authenticated = ValidationHelper.GetInteger(nums[1], 0);
                }
            }
                        
            // Check if at least one user is online
            if ((publicUsers + authenticated) == 0)
            {
                ltrAdditionaInfos.Text = NoUsersOnlineText;
            }
            else
            {
                ltrAdditionaInfos.Text = string.Format(AdditionalInfoText, publicUsers + authenticated, publicUsers, authenticated);
            }
        }

        ReleaseContext();
    }


    /// <summary>
    /// Clears the cached items.
    /// </summary>
    public override void ClearCache()
    {
        string useCacheItemName = DataHelper.GetNotEmpty(CacheItemName, CacheHelper.GetCacheItemName("onlineusers", CMSContext.CurrentSiteName, SelectTopN, Columns, Path));

        CacheHelper.ClearCache(useCacheItemName);

        // Get or generate cache item name for number
        string cacheItemNameNumbers = CacheItemName;
        if (!string.IsNullOrEmpty(cacheItemNameNumbers))
        {
            cacheItemNameNumbers += "Number";
        }

        string useCacheItemNameNumber = DataHelper.GetNotEmpty(cacheItemNameNumbers, CacheHelper.GetCacheItemName("onlineusersnumber", CMSContext.CurrentSiteName, Path));
        CacheHelper.ClearCache(useCacheItemNameNumber);
    }

    #endregion
}
