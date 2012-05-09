using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.skmMenuControl;

public partial class CMSWebParts_Navigation_cmsmenu : CMSAbstractWebPart
{
    #region "Document properties"

    /// <summary>
    /// Gets or sets the value that indicates whether menu sub items in the RTL culture are opened to the other side.
    /// </summary>
    public bool EnableRTLBehaviour
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableRTLBehaviour"), this.menuElem.EnableRTLBehaviour);
        }
        set
        {
            this.SetValue("EnableRTLBehaviour", value);
            this.menuElem.EnableRTLBehaviour = ValidationHelper.GetBoolean(value, false);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether up and down mouse css classes are applied.
    /// </summary>
    public bool EnableMouseUpDownClass
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EnableMouseUpDownClass"), this.menuElem.EnableMouseUpDownClass);
        }
        set
        {
            this.SetValue("EnableMouseUpDownClass", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether document menu item properties are applied.
    /// </summary>
    public bool ApplyMenuDesign
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ApplyMenuDesign"), this.menuElem.ApplyMenuDesign);
        }
        set
        {
            this.SetValue("ApplyMenuDesign", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether item image is displayed for highlighted item when highlighted image is not specified.
    /// </summary>
    public bool UseItemImagesForHiglightedItem
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseItemImagesForHiglightedItem"), this.menuElem.UseItemImagesForHiglightedItem);
        }
        set
        {
            this.SetValue("UseItemImagesForHiglightedItem", value);
        }
    }


    /// <summary>
    /// Gets or sets the cache minutes.
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
            this.menuElem.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache item dependencies.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return base.CacheDependencies;
        }
        set
        {
            base.CacheDependencies = value;
            menuElem.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the cache item. If not explicitly specified, the name is automatically 
    /// created based on the control unique ID
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
            menuElem.CacheItemName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.menuElem.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            menuElem.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Classnames"), this.menuElem.ClassNames), this.menuElem.ClassNames);
        }
        set
        {
            this.SetValue("ClassNames", value);
            this.menuElem.ClassNames = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents are combined with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), this.menuElem.CombineWithDefaultCulture);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            this.menuElem.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets the culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CultureCode"), this.menuElem.CultureCode), this.menuElem.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            this.menuElem.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximal relative level.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRelativeLevel"), this.menuElem.MaxRelativeLevel);
        }
        set
        {
            this.SetValue("MaxRelativeLevel", value);
            this.menuElem.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by clause.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OrderBy"), this.menuElem.OrderBy), this.menuElem.OrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.menuElem.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the nodes path.
    /// </summary>
    public string Path
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Path"), this.menuElem.Path), this.menuElem.Path);
        }
        set
        {
            this.SetValue("Path", value);
            this.menuElem.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents must be published.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), this.menuElem.SelectOnlyPublished);
        }
        set
        {
            this.SetValue("SelectOnlyPublished", value);
            this.menuElem.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SiteName"), this.menuElem.SiteName), this.menuElem.SiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.menuElem.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("WhereCondition"), this.menuElem.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.menuElem.WhereCondition = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether text can be wrapped or space is replaced with non breakable space.
    /// </summary>
    public bool WordWrap
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("WordWrap"), this.menuElem.WordWrap);
        }
        set
        {
            this.SetValue("WordWrap", value);
            this.menuElem.WordWrap = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether alternate text for image will be rendered.
    /// </summary>
    public bool RenderImageAlt
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderImageAlt"), this.menuElem.RenderImageAlt);
        }
        set
        {
            this.SetValue("RenderImageAlt", value);
            this.menuElem.RenderImageAlt = value;
        }
    }


    /// <summary>
    /// Gets or sets the mouse cursor (pointer, hand etc.).
    /// </summary>
    public MouseCursor Cursor
    {
        get
        {
            return (MouseCursor)ValidationHelper.GetInteger(this.GetValue("Cursor"), (int)this.menuElem.Cursor);
        }
        set
        {
            this.SetValue("Cursor", value.ToString());
            this.menuElem.Cursor = value;
        }
    }


    /// <summary>
    /// Gets or sets the css prefix. For particular levels can be used several values separated with semicolon (;).
    /// </summary>
    public string CSSPrefix
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CSSPrefix"), this.menuElem.CSSPrefix), this.menuElem.CSSPrefix);
        }
        set
        {
            this.SetValue("CSSPrefix", value);
            this.menuElem.CSSPrefix = value;
        }
    }


    /// <summary>
    /// Gets or sets the external script path.
    /// </summary>
    public string ExternalScriptPath
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("ExternalScriptPath"), this.menuElem.ExternalScriptPath);
        }
        set
        {
            this.SetValue("ExternalScriptPath", value);
            this.menuElem.ExternalScriptPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicate whether all items in path will be highlighted.
    /// </summary>
    public bool HighlightAllItemsInPath
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HighlightAllItemsInPath"), this.menuElem.HighlightAllItemsInPath);
        }
        set
        {
            this.SetValue("HighlightAllItemsInPath", value);
            this.menuElem.HighlightAllItemsInPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the nodes path which indicates path, where items in this path are highligted (at default current alias path).
    /// </summary>
    public string HighlightedNodePath
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("HighlightedNodePath"), this.menuElem.HighlightedNodePath), this.menuElem.HighlightedNodePath);
        }
        set
        {
            this.SetValue("HighlightedNodePath", value);
            this.menuElem.HighlightedNodePath = value;
        }
    }


    /// <summary>
    /// Gets or sets the menu layout (horizontal, vertical).
    /// </summary>
    public MenuLayout Layout
    {
        get
        {
            return CMS.skmMenuControl.Menu.GetLayout(DataHelper.GetNotEmpty(this.GetValue("Layout"), this.menuElem.Layout.ToString()));
        }
        set
        {
            this.SetValue("Layout", value);
            this.menuElem.Layout = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether itemname atribute is added to the item.
    /// <remarks>If you switch this property to true, the resulting HTML code will not be valid.</remarks>
    /// </summary>
    public bool RenderItemName
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderItemName"), this.menuElem.RenderItemName);
        }
        set
        {
            this.SetValue("RenderItemName", value);
            this.menuElem.RenderItemName = value;
        }
    }


    /// <summary>
    /// Gets or sets the url to the image which is assigned as sub menu indicator.
    /// </summary>
    public string SubMenuIndicator
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SubmenuIndicator"), this.menuElem.SubmenuIndicator), this.menuElem.SubmenuIndicator);
        }
        set
        {
            this.SetValue("SubmenuIndicator", value);
            this.menuElem.SubmenuIndicator = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether alternating styles are used.
    /// </summary>
    public bool UseAlternatingStyles
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseAlternatingStyles"), this.menuElem.UseAlternatingStyles);
        }
        set
        {
            this.SetValue("UseAlternatingStyles", value);
            this.menuElem.UseAlternatingStyles = value;
        }
    }


    /// <summary>
    /// Gets or sets the menu table padding.
    /// </summary>
    public int Padding
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Padding"), this.menuElem.Padding);
        }
        set
        {
            this.SetValue("Padding", value);
            this.menuElem.Padding = value;
        }
    }


    /// <summary>
    /// Gets or sets the menu table spacing.
    /// </summary>
    public int Spacing
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Spacing"), this.menuElem.Spacing);
        }
        set
        {
            this.SetValue("Spacing", value);
            this.menuElem.Spacing = value;
        }
    }


    /// <summary>
    /// Gets or sets the class that is applied to the separator.
    /// </summary>
    public string SeparatorCSS
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SeparatorCSS"), this.menuElem.SeparatorCssClass);
        }
        set
        {
            this.SetValue("", value);
            this.menuElem.SeparatorCssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the separator height.
    /// </summary>
    public int SeparatorHeight
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SeparatorHeight"), this.menuElem.SeparatorHeight);
        }
        set
        {
            this.SetValue("SeparatorHeight", value);
            this.menuElem.SeparatorHeight = value;
        }
    }


    /// <summary>
    /// Gets or sets the separator text.
    /// </summary>
    public string SeparatorText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SeparatorText"), this.menuElem.SeparatorText);
        }
        set
        {
            this.SetValue("SeparatorText", value);
            this.menuElem.SeparatorText = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.menuElem.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            menuElem.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows results.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.menuElem.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.menuElem.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), this.menuElem.FilterName);
        }
        set
        {
            this.SetValue("FilterName", value);
            this.menuElem.FilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets property which indicates if menu caption should be HTML encoded.
    /// </summary>
    public bool EncodeMenuCaption
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EncodeMenuCaption"), this.menuElem.EncodeMenuCaption);
        }
        set
        {
            this.SetValue("EncodeMenuCaption", value);
            this.menuElem.EncodeMenuCaption = value;
        }
    }


    /// <summary>
    /// Gets or sets the columns to be retrieved from database.
    /// </summary>  
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), this.menuElem.Columns);
        }
        set
        {
            this.SetValue("Columns", value);
            this.menuElem.Columns = value;
        }
    }

    #endregion


    #region "Stop processing"

    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.menuElem.StopProcessing = value;
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
        if (this.StopProcessing)
        {
            this.menuElem.StopProcessing = true;
        }
        else
        {
            this.menuElem.ControlContext = this.ControlContext;

            // Set properties from Webpart form        
            this.menuElem.ApplyMenuDesign = this.ApplyMenuDesign;
            this.menuElem.UseItemImagesForHiglightedItem = this.UseItemImagesForHiglightedItem;
            this.menuElem.CacheItemName = this.CacheItemName;
            this.menuElem.CacheDependencies = this.CacheDependencies;
            this.menuElem.CacheMinutes = this.CacheMinutes;
            this.menuElem.CheckPermissions = this.CheckPermissions;
            this.menuElem.ClassNames = this.ClassNames;
            this.menuElem.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.menuElem.CSSPrefix = this.CSSPrefix;
            this.menuElem.CultureCode = this.CultureCode;
            // Cursor is integer to MouseCursorEnum
            this.menuElem.Cursor = this.Cursor;
            this.menuElem.ExternalScriptPath = this.ExternalScriptPath;
            this.menuElem.HighlightAllItemsInPath = this.HighlightAllItemsInPath;
            this.menuElem.HighlightedNodePath = this.HighlightedNodePath;
            // Layout is integer to MenuLayoutEnum
            this.menuElem.Layout = this.Layout;
            this.menuElem.MaxRelativeLevel = this.MaxRelativeLevel;
            this.menuElem.OrderBy = this.OrderBy;
            this.menuElem.RenderItemName = this.RenderItemName;
            this.menuElem.Path = this.Path;
            this.menuElem.SelectOnlyPublished = this.SelectOnlyPublished;
            this.menuElem.SiteName = this.SiteName;
            this.menuElem.SubmenuIndicator = this.SubMenuIndicator;
            this.menuElem.UseAlternatingStyles = this.UseAlternatingStyles;
            this.menuElem.WhereCondition = this.WhereCondition;
            this.menuElem.Padding = this.Padding;
            this.menuElem.Spacing = this.Spacing;
            this.menuElem.SeparatorCssClass = this.SeparatorCSS;
            this.menuElem.SeparatorHeight = this.SeparatorHeight;
            this.menuElem.SeparatorText = this.SeparatorText;
            this.menuElem.EnableRTLBehaviour = this.EnableRTLBehaviour;
            this.menuElem.RenderImageAlt = this.RenderImageAlt;
            this.menuElem.EnableMouseUpDownClass = this.EnableMouseUpDownClass;
            this.menuElem.WordWrap = this.WordWrap;

            this.menuElem.HideControlForZeroRows = this.HideControlForZeroRows;
            this.menuElem.ZeroRowsText = this.ZeroRowsText;
            this.menuElem.EncodeMenuCaption = this.EncodeMenuCaption;

            this.menuElem.FilterName = this.FilterName;

            this.menuElem.Columns = this.Columns;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        this.menuElem.ReloadData(true);
        base.ReloadData();
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = this.menuElem.Visible;

        if (DataHelper.DataSourceIsEmpty(this.menuElem.DataSource) && (this.menuElem.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }
}
