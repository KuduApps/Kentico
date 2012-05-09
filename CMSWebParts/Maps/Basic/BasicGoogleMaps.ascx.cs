using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSWebParts_Maps_Basic_BasicGoogleMaps : CMSAbstractWebPart
{
    #region "Private variables"

    // Base datasource instance
    private CMSBaseDataSource mDataSourceControl = null;

    // Indicates whether control was binded
    private bool binded = false;

    // BasicGoogleMaps instance
    private BasicGoogleMaps BasicGoogleMaps = new BasicGoogleMaps();

    // Indicates whether current control was added to the filter collection
    private bool mFilterControlAdded = false;

    #endregion


    #region "Public properties"

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


    /// <summary>
    /// Control with data source.
    /// </summary>
    public CMSBaseDataSource DataSourceControl
    {
        get
        {
            // Check if control is empty and load it with the data
            if (this.mDataSourceControl == null)
            {
                if (!String.IsNullOrEmpty(this.DataSourceName))
                {
                    this.mDataSourceControl = CMSControlsHelper.GetFilter(this.DataSourceName) as CMSBaseDataSource;

                    // If not found, try to get data source control according to ClientID or find the control on page
                    if (this.mDataSourceControl == null)
                    {
                        Control parent = this.Parent;
                        // Find control on page
                        while (parent != null && this.mDataSourceControl == null)
                        {
                            Control dataSource = parent.FindControl(this.DataSourceName);
                            if (dataSource != null)
                            {
                                try
                                {
                                    this.mDataSourceControl = CMSControlsHelper.GetFilter(dataSource.ClientID) as CMSBaseDataSource;
                                    if (mDataSourceControl == null)
                                    {
                                        this.mDataSourceControl = dataSource as CMSBaseDataSource;
                                    }
                                }
                                catch { }
                            }
                            parent = parent.Parent;
                        }
                    }
                }
            }

            return this.mDataSourceControl;
        }
        set
        {
            this.mDataSourceControl = value;
        }
    }


    /// <summary>
    /// Gets or sets ItemTemplate property.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), "");
        }
        set
        {
            this.SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HideControlForZeroRows property.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.BasicGoogleMaps.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.BasicGoogleMaps.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets ZeroRowsText property.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), "");
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.BasicGoogleMaps.ZeroRowsText = value;
        }
    }

    #endregion


    #region "Map properties"

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


    /// <summary>
    /// Gets or sets the tool tip text field (filed for markers tool tip text).
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


    #region "Advanced map properties"

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

    #endregion


    #region "Page events"

    /// <summary>
    /// On init event handler.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Due to new design mode (with preview) we need to move map down for the user to be able to drag and drop the control
        if (CMSContext.ViewMode == ViewModeEnum.Design)
        {
            Label ltlDesign = new Label();
            ltlDesign.ID = "ltlDesig";
            ltlDesign.Text = "<div class=\"WebpartDesignPadding\"></div>";
            plcBasicGoogleMaps.Controls.Add(ltlDesign);
        }
    }


    /// <summary>
    /// On content loaded override.
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
        if (!this.StopProcessing)
        {
            #region "Caching options"

            // Set caching options if server processing is enabled
            if (this.DataSourceControl != null && this.EnableServerProcessing)
            {
                this.BasicGoogleMaps.CacheItemName = this.DataSourceControl.CacheItemName;

                if (this.DataSourceControl.CacheMinutes <= 0)
                {
                    // If zero or less, get from the site settings
                    this.BasicGoogleMaps.CacheMinutes = SettingsKeyProvider.GetIntValue(CurrentSiteName + ".CMSCacheMinutes");
                }
                else
                {
                    this.BasicGoogleMaps.CacheMinutes = this.DataSourceControl.CacheMinutes;
                }

                // Cache depends only on data source and properties of data source web part
                string cacheDependencies = CacheHelper.GetCacheDependencies(this.DataSourceControl.CacheDependencies, this.DataSourceControl.GetDefaultCacheDependendencies());
                // All view modes, except LiveSite mode
                if (CMSContext.ViewMode != ViewModeEnum.LiveSite)
                {
                    // Cache depends on data source, properties of data source web part and properties of BasicGoogleMaps web part
                    cacheDependencies += "webpartinstance|" + this.InstanceGUID.ToString().ToLower();
                }
                this.BasicGoogleMaps.CacheDependencies = cacheDependencies;
            }

            #endregion

            #region "Map properties"

            CMSMapProperties mp = new CMSMapProperties();
            mp.Location = this.DefaultLocation;
            mp.EnableKeyboardShortcuts = this.EnableKeyboardShortcuts;
            mp.EnableMapDragging = this.EnableMapDragging;
            mp.Height = this.Height;
            mp.Width = this.Width;
            mp.EnableServerProcessing = this.EnableServerProcessing;
            mp.Longitude = this.Longitude;
            mp.Latitude = this.Latitude;
            mp.LatitudeField = this.LatitudeField;
            mp.LongitudeField = this.LongitudeField;
            mp.LocationField = this.LocationField;
            mp.MapType = this.MapType;
            mp.NavigationControlType = this.NavigationControlType;
            mp.Scale = this.Scale;
            mp.ShowNavigationControl = this.ShowNavigationControl;
            mp.ShowScaleControl = this.ShowScaleControl;
            mp.ShowMapTypeControl = this.ShowMapTypeControl;
            mp.ToolTipField = this.ToolTipField;
            mp.IconField = this.IconField;
            mp.ZoomScale = this.ZoomScale;
            mp.MapId = this.ClientID;

            #endregion

            this.BasicGoogleMaps.MapProperties = mp;
            this.BasicGoogleMaps.HideControlForZeroRows = this.HideControlForZeroRows;
            this.BasicGoogleMaps.DataBindByDefault = false;
            this.BasicGoogleMaps.MainScriptPath = "~/CMSWebParts/Maps/Basic/BasicGoogleMaps_files/GoogleMaps.js";

            // Add basic maps control to the filter collection
            EnsureFilterControl();

            if (!String.IsNullOrEmpty(this.ZeroRowsText))
            {
                this.BasicGoogleMaps.ZeroRowsText = this.ZeroRowsText;
            }
        }
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Datasource data
        object ds = null;

        if (!this.StopProcessing)
        {
            // Set transformations if data source is not empty
            if (this.DataSourceControl != null)
            {
                // Get data from datasource
                ds = this.DataSourceControl.DataSource;

                // Check whether data exist
                if ((!DataHelper.DataSourceIsEmpty(ds)) && (!binded))
                {
                    // Initilaize related data if provided
                    if (this.DataSourceControl.RelatedData != null)
                    {
                        this.RelatedData = this.DataSourceControl.RelatedData;
                    }
                    this.LoadTransformations();
                    this.BasicGoogleMaps.DataSource = this.DataSourceControl.DataSource;
                }
            }
        }

        base.OnPreRender(e);

        if (!this.StopProcessing)
        {
            // Hide control for zero rows
            if (((this.DataSourceControl == null) || (DataHelper.DataSourceIsEmpty(ds))) && (this.HideControlForZeroRows))
            {
                this.Visible = false;
            }
            else
            {
                this.BasicGoogleMaps.DataBind();
            }
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        EnsureFilterControl();

        base.ReloadData();
    }


    /// <summary>
    /// Loads and setups web part.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        if (!this.StopProcessing)
        {
            // Add control to the control collection
            plcBasicGoogleMaps.Controls.Add(this.BasicGoogleMaps);

            // Check whether postback was executed from current transformation item
            if (RequestHelper.IsPostBack())
            {
                // Indicates whether postback was fired from current control
                bool bindControl = false;

                // Check event target value and callback parameter value
                string eventTarget = ValidationHelper.GetString(this.Request.Form["__EVENTTARGET"], String.Empty);
                string callbackParam = ValidationHelper.GetString(this.Request.Form["__CALLBACKPARAM"], String.Empty);
                if (eventTarget.StartsWith(this.UniqueID) || callbackParam.StartsWith(this.UniqueID) || eventTarget.EndsWith("_contextMenuControl"))
                {
                    bindControl = true;
                }
                // Check whether request key contains some control assigned to current control
                else
                {
                    foreach (string key in this.Request.Form.Keys)
                    {
                        if ((key != null) && key.StartsWith(this.UniqueID))
                        {
                            bindControl = true;
                            break;
                        }
                    }
                }

                if (bindControl)
                {
                    // Reload data
                    if (this.DataSourceControl != null)
                    {
                        this.LoadTransformations();
                        this.BasicGoogleMaps.DataSource = this.DataSourceControl.DataSource;
                        this.BasicGoogleMaps.DataBind();
                        binded = true;
                    }
                }
            }

            //Handle filter change event
            if (this.DataSourceControl != null)
            {
                this.DataSourceControl.OnFilterChanged += new ActionEventHandler(DataSourceControl_OnFilterChanged);
            }
        }

        base.OnLoad(e);
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Ensures current control in the filters collection.
    /// </summary>
    protected void EnsureFilterControl()
    {
        if (!mFilterControlAdded)
        {
            // Add basic repeater to the filter collection
            CMSControlsHelper.SetFilter(ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID), this.BasicGoogleMaps);
            mFilterControlAdded = true;
        }
    }


    /// <summary>
    /// Load transformations with dependence on current datasource type and datasource type.
    /// </summary>
    protected void LoadTransformations()
    {
        CMSBaseDataSource dataSource = this.DataSourceControl as CMSBaseDataSource;

        if ((dataSource != null) && !String.IsNullOrEmpty(this.TransformationName))
        {
            this.BasicGoogleMaps.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.TransformationName, false);
        }
    }


    /// <summary>
    /// OnFilter change event handler.
    /// </summary>
    protected void DataSourceControl_OnFilterChanged()
    {
        // Set forcibly visibility
        this.Visible = true;

        // Reload data
        if (this.DataSourceControl != null)
        {
            this.LoadTransformations();
            this.BasicGoogleMaps.DataSource = this.DataSourceControl.DataSource;
            this.BasicGoogleMaps.DataBind();
            binded = true;
        }
    }

    #endregion;
}