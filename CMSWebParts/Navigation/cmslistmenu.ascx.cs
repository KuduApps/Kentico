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

public partial class CMSWebParts_Navigation_cmslistmenu : CMSAbstractWebPart
{
    #region "Document properties"

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
            this.menuElem.ApplyMenuDesign = value;
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
            this.menuElem.UseItemImagesForHiglightedItem = value;
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
            this.menuElem.CheckPermissions = value;
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
    /// Gets or sets order by clause.
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
            this.SetValue("SelctOnlyPublished", value);
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
    /// Gets or sets the value that indicates whether highlighted item is displayed as link.
    /// </summary>
    public bool DisplayHighlightedItemAsLink
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayHighlightedItemAsLink"), this.menuElem.DisplayHighlightedItemAsLink);
        }
        set
        {
            this.SetValue("DisplayHighlightedItemAsLink", value);
            this.menuElem.DisplayHighlightedItemAsLink = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether only submenu under highlighted item could be render, otherwise all submenus are rendered.
    /// </summary>
    public bool DisplayOnlySelectedPath
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayOnlySelectedPath"), this.menuElem.DisplayOnlySelectedPath);
        }
        set
        {
            this.SetValue("DisplayOnlySelectedPath", value);
            this.menuElem.DisplayOnlySelectedPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the first item CSS class.
    /// </summary>
    public string FirstItemCssClass
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("FirstItemCssClass"), this.menuElem.FirstItemCssClass), this.menuElem.FirstItemCssClass);
        }
        set
        {
            this.SetValue("FirstItemCssClass", value);
            this.menuElem.FirstItemCssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets last item CSS class.
    /// </summary>
    public string LastItemCssClass
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("LastItemCssClass"), this.menuElem.LastItemCssClass), this.menuElem.LastItemCssClass);
        }
        set
        {
            this.SetValue("LastItemCssClass", value);
            this.menuElem.LastItemCssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets onmouseout script.
    /// </summary>
    public string OnMouseOutScript
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OnMouseOutScript"), this.menuElem.OnMouseOutScript), this.menuElem.OnMouseOutScript);
        }
        set
        {
            this.SetValue("OnMouseOutScript", value);
            this.menuElem.OnMouseOutScript = value;
        }
    }


    /// <summary>
    /// Gets or sets onmouseover script.
    /// </summary>
    public string OnMouseOverScript
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OnMouseOverScript"), this.menuElem.OnMouseOverScript), this.menuElem.OnMouseOverScript);
        }
        set
        {
            this.SetValue("OnMouseOverScript", value);
            this.menuElem.OnMouseOverScript = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether CSS classes will be rendered.
    /// </summary>
    public bool RenderCssClasses
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderCssClasses"), this.menuElem.RenderCssClasses);
        }
        set
        {
            this.SetValue("RenderCssClasses", value);
            this.menuElem.RenderCssClasses = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether item ID will be rendered.
    /// </summary>
    public bool RenderItemID
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderItemID"), this.menuElem.RenderItemID);
        }
        set
        {
            this.SetValue("RenderItemID", value);
            this.menuElem.RenderItemID = value;
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
    /// Gets or sets the link url target.
    /// </summary>
    public string UrlTarget
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("UrlTarget"), this.menuElem.UrlTarget), this.menuElem.UrlTarget);
        }
        set
        {
            this.SetValue("UrlTarget", value);
            this.menuElem.UrlTarget = value;
        }
    }


    /// <summary>
    /// Gets or sets the css class name, which create envelope around menu and automatically register LIHover.htc script (IE6 required this).
    /// </summary>
    public string HoverCssClassName
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("HoverCSSClassName"), this.menuElem.HoverCSSClassName);
        }
        set
        {
            this.SetValue("HoverCSSClassName", value);
            this.menuElem.HoverCSSClassName = value;
        }
    }


    /// <summary>
    /// Gets or sets the item ID prefix.
    /// </summary>
    public string ItemIDPrefix
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemIdPrefix"), this.menuElem.ItemIdPrefix);
        }
        set
        {
            this.SetValue("ItemIdPrefix", value);
            this.menuElem.ItemIdPrefix = value;
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
    /// Gets or sets the value that indicates whether link title will be rendered.
    /// </summary>
    public bool RenderLinkTitle
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderLinkTitle"), this.menuElem.RenderLinkTitle);
        }
        set
        {
            this.SetValue("RenderLinkTitle", value);
            this.menuElem.RenderLinkTitle = value;
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


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
 	    base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        menuElem.ReloadData(true);
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
            this.menuElem.MaxRelativeLevel = this.MaxRelativeLevel;
            this.menuElem.OrderBy = this.OrderBy;
            this.menuElem.Path = this.Path;
            this.menuElem.SelectOnlyPublished = this.SelectOnlyPublished;
            this.menuElem.SiteName = this.SiteName;
            this.menuElem.WhereCondition = this.WhereCondition;
            this.menuElem.CultureCode = this.CultureCode;
            this.menuElem.CombineWithDefaultCulture = this.CombineWithDefaultCulture;

            this.menuElem.CSSPrefix = this.CSSPrefix;
            this.menuElem.HighlightAllItemsInPath = this.HighlightAllItemsInPath;
            this.menuElem.HighlightedNodePath = this.HighlightedNodePath;

            this.menuElem.DisplayHighlightedItemAsLink = this.DisplayHighlightedItemAsLink;
            this.menuElem.DisplayOnlySelectedPath = this.DisplayOnlySelectedPath;
            this.menuElem.FirstItemCssClass = this.FirstItemCssClass;
            this.menuElem.LastItemCssClass = this.LastItemCssClass;
            this.menuElem.HoverCSSClassName = this.HoverCssClassName;
            
            this.menuElem.OnMouseOutScript = this.OnMouseOutScript;
            this.menuElem.OnMouseOverScript = this.OnMouseOverScript; 
            
            this.menuElem.RenderCssClasses = this.RenderCssClasses;
            this.menuElem.RenderItemID = this.RenderItemID;
            this.menuElem.ItemIdPrefix = this.ItemIDPrefix;
            this.menuElem.SubmenuIndicator = this.SubMenuIndicator;
            this.menuElem.UrlTarget = this.UrlTarget;
            this.menuElem.UseAlternatingStyles = this.UseAlternatingStyles;
            this.menuElem.RenderLinkTitle = this.RenderLinkTitle;
            this.menuElem.RenderImageAlt = this.RenderImageAlt;

            this.menuElem.WordWrap = this.WordWrap;

            this.menuElem.HideControlForZeroRows = this.HideControlForZeroRows;
            this.menuElem.ZeroRowsText = this.ZeroRowsText;
            this.menuElem.EncodeMenuCaption = this.EncodeMenuCaption;

            this.menuElem.FilterName = this.FilterName;

            this.menuElem.Columns = this.Columns;
        }
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
