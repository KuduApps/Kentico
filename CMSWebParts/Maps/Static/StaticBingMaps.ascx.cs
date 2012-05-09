using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.Controls;
using CMS.SettingsProvider;

public partial class CMSWebParts_Maps_Static_StaticBingMaps : CMSAbstractWebPart
{
    #region "Map properties"

    /// <summary>
    /// Gets or sets the value that indicates whether the keyboard shortcuts are enabled.
    /// </summary>
    public bool EnableKeyboardShortcuts
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableKeyboardShortcuts"), true);
        }
        set
        {
            this.SetValue("EnableKeyboardShortcuts", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the user can drag the map with the mouse. 
    /// </summary>
    public bool EnableMapDragging
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableMapDragging"), true);
        }
        set
        {
            this.SetValue("EnableMapDragging", value);
        }
    }


    /// <summary>
    /// Gets or sets the height of the map.
    /// </summary>
    public string Height
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Height"), "400");
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Gets or sets the width of the map.
    /// </summary>
    public string Width
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Width"), "400");
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Gets or sets the initial map type. 
    /// Road - The road map style. 
    /// Shaded - The shaded map style, which is a road map with shaded contours. 
    /// Aerial - The aerial map style. 
    /// Hybrid - The hybrid map style, which is an aerial map with a label overlay. 
    /// Birdseye - The bird's eye (oblique-angle) imagery map style. 
    /// BirdseyeHybrid - The bird's eye hybrid map style, which is a bird's eye map with a label overlay. 
    /// </summary>
    public string MapType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MapType"), "road");
        }
        set
        {
            this.SetValue("MapType", value);
        }
    }


    /// <summary>
    /// Gets or sets the scale of the map.
    /// </summary>
    public int Scale
    {
        get
        {
            int value = ValidationHelper.GetInteger(this.GetValue("Scale"), 3);
            if (value < 0)
            {
                value = 7;
            }
            return value;
        }
        set
        {
            this.SetValue("Scale", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether NavigationControl is displayed.
    /// </summary>
    public bool ShowNavigationControl
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowNavigationControl"), true);
        }
        set
        {
            this.SetValue("ShowNavigationControl", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether ScaleControl is displayed.
    /// </summary>
    public bool ShowScaleControl
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowScaleControl"), true);
        }
        set
        {
            this.SetValue("ShowScaleControl", value);
        }
    }


    /// <summary>
    /// Gets or sets the tool tip text for single marker in detail mode.
    /// </summary>
    public string ToolTip
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ToolTip"), "");
        }
        set
        {
            this.SetValue("ToolTip", value);
        }
    }


    /// <summary>
    /// Gets or sets the content text for single marker in detail mode.
    /// </summary>
    public string Content
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Content"), "");
        }
        set
        {
            this.SetValue("Content", value);
        }
    }


    /// <summary>
    /// Gets or sets the scale of the map when zoomed (after marker click event).
    /// </summary>
    public int ZoomScale
    {
        get
        {
            int value = ValidationHelper.GetInteger(this.GetValue("ZoomScale"), 10);
            if (value < 0)
            {
                value = 10;
            }
            return value;
        }
        set
        {
            this.SetValue("ZoomScale", value);
        }
    }


    /// <summary>
    /// Gets or sets the Bing map key.
    /// </summary>
    public string MapKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MapKey"), "");
        }
        set
        {
            this.SetValue("MapKey", value);
        }
    }

    #endregion


    #region "Location properties"

    /// <summary>
    /// Gets or sets the default location of the center of the map and/or location of single marker in detail mode.
    /// </summary>
    public string Location
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Location"), "");
        }
        set
        {
            this.SetValue("Location", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether server processing is enabled.
    /// </summary>
    public bool EnableServerProcessing
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableServerProcessing"), false);
        }
        set
        {
            this.SetValue("EnableServerProcessing", value);
        }
    }


    /// <summary>
    /// Gets or sets the latitude of the center of the map and/or latitude of single marker in detail mode.
    /// </summary>
    public double? Latitude
    {
        get
        {
            string lat = DataHelper.GetNotEmpty(this.GetValue("Latitude"), "");
            if (string.IsNullOrEmpty(lat))
            {
                return null;
            }
            else
            {
                return ValidationHelper.GetDouble(lat, 0, CultureHelper.EnglishCulture.Name);
            }
        }
        set
        {
            this.SetValue("Latitude", value);
        }
    }


    /// <summary>
    /// Gets or sets the longitude of the center of the map and/or longitude of single marker in detail mode.
    /// </summary>
    public double? Longitude
    {
        get
        {
            string lng = DataHelper.GetNotEmpty(this.GetValue("Longitude"), "");
            if (string.IsNullOrEmpty(lng))
            {
                return null;
            }
            else
            {
                return ValidationHelper.GetDouble(lng, 0, CultureHelper.EnglishCulture.Name);
            }
        }
        set
        {
            this.SetValue("Longitude", value);
        }
    }


    /// <summary>
    /// Gets or sets the URL for icon.
    /// </summary>
    public string IconURL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("IconURL"), "");
        }
        set
        {
            this.SetValue("IconURL", value);
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            int result = ValidationHelper.GetInteger(GetValue("CacheMinutes"), -1);
            if (result < 0)
            {
                // If not set, get from the site settings
                result = SettingsKeyProvider.GetIntValue(CurrentSiteName + ".CMSCacheMinutes");
            }
            return result;
        }
        set
        {
            this.SetValue("CacheMinutes", value);
        }
    }

    /// <summary>
    /// Cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CacheItemName"), "");
        }
        set
        {
            this.SetValue("CacheItemName", value);
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// On init event handler.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Due to new design mode (with preview) we need to move map down for the user to be able to drag and drop the control
        if (CMSContext.ViewMode == ViewModeEnum.Design)
        {
            ltlDesign.Text = "<div class=\"WebpartDesignPadding\"></div>";
        }
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!this.StopProcessing)
        {
            // Load map
            LoadMap();

            // Register Bing javascript files
            BasicBingMaps.RegisterMapScripts(this.Page, this.ClientID, "~/CMSWebParts/Maps/Static/StaticBingMaps_files/BingMaps.js");
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        // Re-load map
        LoadMap();
    }


    /// <summary>
    /// Generates map code and registers Bing javascript files.
    /// </summary>
    public void LoadMap()
    {

        #region "Map properties"

        // Set map properties
        CMSMapProperties mp = new CMSMapProperties();
        mp.EnableMapDragging = this.EnableMapDragging;
        mp.ShowScaleControl = this.ShowScaleControl;
        mp.EnableServerProcessing = this.EnableServerProcessing;
        mp.EnableKeyboardShortcuts = this.EnableKeyboardShortcuts;
        mp.ShowNavigationControl = this.ShowNavigationControl;
        mp.IconURL = this.IconURL;
        mp.MapKey = this.MapKey;
        mp.Latitude = this.Latitude;
        mp.Longitude = this.Longitude;
        mp.ZoomScale = this.ZoomScale;
        mp.Location = this.Location;
        mp.ToolTip = this.ToolTip;
        mp.MapType = this.MapType;
        mp.Content = this.Content;
        mp.Height = this.Height;
        mp.Scale = this.Scale;
        mp.Width = this.Width;
        mp.MapId = this.ClientID;
        mp.Page = this.Page;

        #endregion

        // Load map
        this.ltlBingMap.Text = BasicBingMaps.GenerateMap(mp, this.CacheMinutes, this.CacheItemName, this.InstanceGUID);
    }

    #endregion
}