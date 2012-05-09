using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.SettingsProvider;

public partial class CMSWebParts_Maps_Documents_GoogleMaps : CMSAbstractWebPart
{
    #region "Variables"

    private bool reloadData = false;
    
    #endregion


    #region "Location properties"

    /// <summary>
    /// Gets or sets the address field.
    /// </summary>
    public string LocationField
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LocationField"), string.Empty);
        }
        set
        {
            this.SetValue("LocationField", value);
        }
    }


    /// <summary>
    /// Gets or sets the default location of the center of the map.
    /// </summary>
    public string DefaultLocation
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DefaultLocation"), string.Empty);
        }
        set
        {
            this.SetValue("DefaultLocation", value);
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
    /// Gets or sets the latitude of of the center of the map.
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
    /// Gets or sets the longitude of of the center of the map.
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
    /// Gets or sets the source latitude field.
    /// </summary>
    public string LatitudeField
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LatitudeField"), "");
        }
        set
        {
            this.SetValue("LatitudeField", value);
        }
    }


    /// <summary>
    /// Gets or sets the source longitude field.
    /// </summary>
    public string LongitudeField
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LongitudeField"), "");
        }
        set
        {
            this.SetValue("LongitudeField", value);
        }
    }

    #endregion


    #region "Map properties"

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
    /// Gets or sets the tool tip text field (fieled for markers tool tip text).
    /// </summary>
    public string ToolTipField
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ToolTipField"), "");
        }
        set
        {
            this.SetValue("ToolTipField", value);
        }
    }


    /// <summary>
    /// Gets or sets the icon field (fieled for icon URL).
    /// </summary>
    public string IconField
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("IconField"), "");
        }
        set
        {
            this.SetValue("IconField", value);
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
    /// Gets or sets the value that indicates whether MapTypeControl is displayed.
    /// </summary>
    public bool ShowMapTypeControl
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowMapTypeControl"), true);
        }
        set
        {
            this.SetValue("ShowMapTypeControl", value);
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
    /// Gets or sets the initial map type.
    /// ROADMAP - This map type displays a normal street map.
    /// SATELLITE - This map type displays a transparent layer of major streets on satellite images.
    /// HYBRID - This map type displays a transparent layer of major streets on satellite images.
    /// TERRAIN - This map type displays maps with physical features such as terrain and vegetation.
    /// </summary>
    public string MapType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MapType"), "ROADMAP");
        }
        set
        {
            this.SetValue("MapType", value);
        }
    }


    /// <summary>
    /// The Navigation control may appear in one of the following style options:
    /// Default picks an appropriate navigation control based on the map's size and the device on which the map is running.
    /// Small displays a mini-zoom control, consisting of only + and - buttons. This style is appropriate for mobile devices.
    /// Zoom & Pan displays the standard zoom slider control with a panning control, as on Google Maps.
    /// Android displays the small zoom control as used on the native Maps application on Android devices.
    /// </summary>
    public int NavigationControlType
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("NavigationControlType"), 0);
        }
        set
        {
            this.SetValue("NavigationControlType", value);
        }
    }

    #endregion
    

    #region "Document properties"

    /// <summary>
    /// Cache item name.
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return base.CacheItemName;
        }
        set
        {
            base.CacheItemName = value;
            this.ucDocumentSource.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.ucDocumentSource.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.ucDocumentSource.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            this.ucDocumentSource.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Check permissions.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), false);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            ucDocumentSource.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Classnames"), this.ucDocumentSource.ClassNames), this.ucDocumentSource.ClassNames);
        }
        set
        {
            this.SetValue("ClassNames", value);
            this.ucDocumentSource.ClassNames = value;
        }
    }


    /// <summary>
    /// Combine with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), false);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            this.ucDocumentSource.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Filter out duplicate documents.
    /// </summary>
    public bool FilterOutDuplicates
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("FilterOutDuplicates"), this.ucDocumentSource.FilterOutDuplicates);
        }
        set
        {
            this.SetValue("FilterOutDuplicates", value);
            this.ucDocumentSource.FilterOutDuplicates = value;
        }
    }


    /// <summary>
    /// Culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CultureCode"), this.ucDocumentSource.CultureCode), this.ucDocumentSource.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            this.ucDocumentSource.CultureCode = value;
        }
    }


    /// <summary>
    /// Maximal relative level.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRelativeLevel"), -1);
        }
        set
        {
            this.SetValue("MaxRelativeLevel", value);
            this.ucDocumentSource.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Order by clause.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OrderBy"), this.ucDocumentSource.OrderBy), this.ucDocumentSource.OrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.ucDocumentSource.OrderBy = value;
        }
    }


    /// <summary>
    /// Nodes path.
    /// </summary>
    public string Path
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Path"), null), null);
        }
        set
        {
            this.SetValue("Path", value);
            this.ucDocumentSource.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("TransformationName"), ""), "");
        }
        set
        {
            this.SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Select only published nodes.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), true);
        }
        set
        {
            this.SetValue("SelectOnlyPublished", value);
            this.ucDocumentSource.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SiteName"), ""), CMSContext.CurrentSiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.ucDocumentSource.SiteName = value;
        }
    }


    /// <summary>
    /// Where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("WhereCondition"), this.ucDocumentSource.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.ucDocumentSource.WhereCondition = value;
        }
    }


    /// <summary>
    /// Select top N items.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectTopN"), this.ucDocumentSource.SelectTopN);
        }
        set
        {
            this.SetValue("SelectTopN", value);
            this.ucDocumentSource.SelectTopN = value;
        }
    }

    #endregion


    #region "Relationships properties"

    /// <summary>
    /// Related node is on the left side.
    /// </summary>
    public bool RelatedNodeIsOnTheLeftSide
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RelatedNodeIsOnTheLeftSide"), false);
        }
        set
        {
            this.SetValue("RelatedNodeIsOnTheLeftSide", value);
            this.ucDocumentSource.RelatedNodeIsOnTheLeftSide = value;
        }
    }


    /// <summary>
    /// Relationship name.
    /// </summary>
    public string RelationshipName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("RelationshipName"), this.ucDocumentSource.RelationshipName), this.ucDocumentSource.RelationshipName);
        }
        set
        {
            this.SetValue("RelationshipName", value);
            this.ucDocumentSource.RelationshipName = value;
        }
    }


    /// <summary>
    /// Relationship with node GUID.
    /// </summary>
    public Guid RelationshipWithNodeGUID
    {
        get
        {
            return ValidationHelper.GetGuid(this.GetValue("RelationshipWithNodeGuid"), Guid.Empty);
        }
        set
        {
            this.SetValue("RelationshipWithNodeGuid", value);
            this.ucDocumentSource.RelationshipWithNodeGuid = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Hide control for zero rows.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.ucGoogleMap.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.ucGoogleMap.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.ucGoogleMap.ZeroRowsText), this.ucGoogleMap.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.ucGoogleMap.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets whethter datasource is empty or not.
    /// </summary>
    public bool HasData
    {
        get
        {
            return !StopProcessing && !DataHelper.DataSourceIsEmpty(ucDocumentSource.DataSource);
        }
    }


    /// <summary>
    /// Gets or sets name of source.
    /// </summary>
    public string DataSourceName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DataSourceName"), "");
        }
        set
        {
            this.SetValue("DataSourceName", value);
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
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
            ucGoogleMap.Visible = false;
        }
        else
        {
            #region "Data source properties"

            // Set Documents data source control
            ucDocumentSource.Path = this.Path;
            ucDocumentSource.ClassNames = this.ClassNames;
            ucDocumentSource.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            ucDocumentSource.CultureCode = this.CultureCode;
            ucDocumentSource.MaxRelativeLevel = this.MaxRelativeLevel;
            ucDocumentSource.OrderBy = this.OrderBy;
            ucDocumentSource.SelectOnlyPublished = this.SelectOnlyPublished;
            ucDocumentSource.SelectTopN = this.SelectTopN;
            ucDocumentSource.SiteName = this.SiteName;
            ucDocumentSource.WhereCondition = this.WhereCondition;
            ucDocumentSource.FilterOutDuplicates = this.FilterOutDuplicates;
            ucDocumentSource.CheckPermissions = this.CheckPermissions;
            ucDocumentSource.RelationshipName = this.RelationshipName;
            ucDocumentSource.RelationshipWithNodeGuid = this.RelationshipWithNodeGUID;
            ucDocumentSource.RelatedNodeIsOnTheLeftSide = this.RelatedNodeIsOnTheLeftSide;
            ucDocumentSource.CacheDependencies = this.CacheDependencies;
            ucDocumentSource.CacheMinutes = this.CacheMinutes;
            ucDocumentSource.CacheItemName = this.CacheItemName;

            #endregion

            #region "Google map caching options"

            // Set caching options if server processing is enabled
            if (this.ucDocumentSource != null && this.EnableServerProcessing)
            {
                this.ucGoogleMap.CacheItemName = this.ucDocumentSource.CacheItemName;

                if (this.ucDocumentSource.CacheMinutes <= 0)
                {
                    // If zero or less, get from the site settings
                    this.ucGoogleMap.CacheMinutes = SettingsKeyProvider.GetIntValue(CurrentSiteName + ".CMSCacheMinutes");
                }
                else
                {
                    this.ucGoogleMap.CacheMinutes = this.ucDocumentSource.CacheMinutes;
                }

                // Cache depends on data source and properties of web part
                this.ucGoogleMap.CacheDependencies = CacheHelper.GetCacheDependencies(this.CacheDependencies, this.ucDocumentSource.GetDefaultCacheDependendencies());
            }

            #endregion

            #region "Map properties"

            // Set BasicGoogleMaps control
            CMSMapProperties mp = new CMSMapProperties();
            mp.EnableKeyboardShortcuts = this.EnableKeyboardShortcuts;
            mp.EnableMapDragging = this.EnableMapDragging;
            mp.EnableServerProcessing = this.EnableServerProcessing;
            mp.Height = this.Height;
            mp.Latitude = this.Latitude;
            mp.LatitudeField = this.LatitudeField;
            mp.Location = this.DefaultLocation;
            mp.LocationField = this.LocationField;
            mp.Longitude = this.Longitude;
            mp.LongitudeField = this.LongitudeField;
            mp.IconField = this.IconField;
            mp.MapId = this.ClientID;
            mp.MapType = this.MapType;
            mp.NavigationControlType = this.NavigationControlType;
            mp.Scale = this.Scale;
            mp.ShowNavigationControl = this.ShowNavigationControl;
            mp.ShowScaleControl = this.ShowScaleControl;
            mp.ShowMapTypeControl = this.ShowMapTypeControl;
            mp.ToolTipField = this.ToolTipField;
            mp.Width = this.Width;
            mp.ZoomScale = this.ZoomScale;

            ucGoogleMap.DataBindByDefault = false;
            ucGoogleMap.MapProperties = mp;
            ucGoogleMap.ItemTemplate = CMSDataProperties.LoadTransformation(this, TransformationName, false);
            ucGoogleMap.MainScriptPath = "~/CMSWebParts/Maps/Documents/GoogleMaps_files/GoogleMaps.js";
            ucGoogleMap.HideControlForZeroRows = this.HideControlForZeroRows;

            if (!String.IsNullOrEmpty(this.ZeroRowsText))
            {
                ucGoogleMap.ZeroRowsText = this.ZeroRowsText;
            }

            #endregion

            if (reloadData)
            {
                ucDocumentSource.DataSource = null;
            }

            // Connects Google map with data source
            ucGoogleMap.DataSource = ucDocumentSource.DataSource;
            ucGoogleMap.RelatedData = ucDocumentSource.RelatedData;

            if (HasData)
            {
                ucGoogleMap.DataBind();
            }
        }
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        Visible = ucGoogleMap.Visible;

        if (!HasData && HideControlForZeroRows)
        {
            Visible = false;
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
        SetupControl();
    }
    

    /// <summary>
    /// Reloads the data.
    /// </summary>
    public override void ReloadData()
    {
        ReloadData(false);
    }


    /// <summary>
    /// Reloads the data.
    /// </summary>
    /// <param name="forceReload">Indicates if the reload should be forced</param>
    public void ReloadData(bool forceReload)
    {
        reloadData = forceReload;
        SetupControl();
    }


    /// <summary>s
    /// Clears cache
    /// </summary>
    public override void ClearCache()
    {
        this.ucDocumentSource.ClearCache();
    }

    #endregion
}