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
using CMS.Controls;

public partial class CMSWebParts_Navigation_cmstabcontrol : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether text can be wrapped or space is replaced with non breakable space.
    /// </summary>
    public bool WordWrap
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("WordWrap"), this.tabElem.WordWrap);
        }
        set
        {
            this.SetValue("WordWrap", value);
            this.tabElem.WordWrap = value;
        }
    }


    /// <summary>
    /// Gets or sets the TabControl id prefix.
    /// </summary>
    public string TabControlIdPrefix
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TabControlIdPrefix"), this.tabElem.TabControlIdPrefix);
        }
        set 
        {
            this.SetValue("TabControlIdPrefix", value);
            this.tabElem.TabControlIdPrefix = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether image alternate text is rendered.
    /// </summary>
    public bool RenderImageAlt
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderImageAlt"), this.tabElem.RenderImageAlt);
        }
        set
        {
            this.SetValue("RenderImageAlt", value);
            this.tabElem.RenderImageAlt = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether document menu item properties are applied.
    /// </summary>
    public bool ApplyMenuDesign
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ApplyMenuDesign"), this.tabElem.ApplyMenuDesign);
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
            return ValidationHelper.GetBoolean(this.GetValue("UseItemImagesForHiglightedItem"), this.tabElem.UseItemImagesForHiglightedItem);
        }
        set
        {
            this.SetValue("UseItemImagesForHiglightedItem", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
        	 return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.tabElem.CheckPermissions); 
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            this.tabElem.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
        	 return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Classnames"), this.tabElem.ClassNames), this.tabElem.ClassNames); 
        }
        set
        {
            this.SetValue("Classnames", value);
            this.tabElem.ClassNames = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents are combined with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
        	 return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), this.tabElem.CombineWithDefaultCulture); 
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            this.tabElem.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the first item is selected by default if isn't other item selected.
    /// </summary>
    public bool SelectFirstItemByDefault
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectFirstItemByDefault"), this.tabElem.SelectFirstItemByDefault);
        }
        set
        {
            this.SetValue("SelectFirstItemByDefault", value);
            this.tabElem.SelectFirstItemByDefault = value;
        }
    }


    /// <summary>
    /// Gets or sets the culture code of the documents which should be displayed.
    /// </summary>
    public string CultureCode
    {
        get
        {
        	 return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CultureCode"), this.tabElem.CultureCode), this.tabElem.CultureCode); 
        }
        set
        {
            this.SetValue("CultureCode", value);
            this.tabElem.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximal relative level of the documents which should be displayed.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
        	 return ValidationHelper.GetInteger(this.GetValue("MaxRelativeLevel"), this.tabElem.MaxRelativeLevel); 
        }
        set
        {
            this.SetValue("MaxRelativeLevel", value);
            this.tabElem.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Gets or sets the highlighted node path.
    /// </summary>
    public string HighlightedNodePath
    {
        get
        {
        	 return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("HighlightedNodePath"), this.tabElem.HighlightedNodePath), this.tabElem.HighlightedNodePath); 
        }
        set
        {
            this.SetValue("HighlightedNodePath", value);
            this.tabElem.HighlightedNodePath = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by expression.
    /// </summary>
    public string OrderBy
    {
        get
        {
        	 return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OrderBy"), this.tabElem.OrderBy), this.tabElem.OrderBy); 
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.tabElem.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the nodes path.
    /// </summary>
    public string Path
    {
        get
        {
        	 return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Path"), this.tabElem.Path), this.tabElem.Path); 
        }
        set
        {
            this.SetValue("Path", value);
            this.tabElem.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents must be published.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
        	 return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), this.tabElem.SelectOnlyPublished); 
        }
        set
        {
            this.SetValue("SelectOnlyPublished", value);
            this.tabElem.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
        	 return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SiteName"), this.tabElem.SiteName), this.tabElem.SiteName); 
        }
        set
        {
            this.SetValue("SiteName", value);
            this.tabElem.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether alternating styles are used.
    /// </summary>
    public bool UseAlternatingStyles
    {
        get
        {
        	 return ValidationHelper.GetBoolean(this.GetValue("UseAlternatingStyles"), this.tabElem.UseAlternatingStyles); 
        }
        set
        {
            this.SetValue("UseAlternatingStyles", value);
            this.tabElem.UseAlternatingStyles = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
        	 return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("WhereCondition"), this.tabElem.WhereCondition), this.tabElem.WhereCondition); 
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.tabElem.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the client script is used after selection.
    /// </summary>
    public bool UseClientScript
    {
        get
        {
        	 return ValidationHelper.GetBoolean(this.GetValue("UseClientScript"), this.tabElem.UseClientScript); 
        }
        set
        {
            this.SetValue("UseClientScript", value);
            this.tabElem.UseClientScript = value;
        }
    }


    /// <summary>
    /// Gets or sets the layout of the tab control (horizontal or vertical).
    /// </summary>
    public TabControlLayoutEnum TabControlLayout
    {
        get
        {
        	 return this.tabElem.GetTabControlLayout(ValidationHelper.GetString(this.GetValue("TabControlLayout"), this.tabElem.TabControlLayout.ToString())); 
        }
        set
        {
            this.SetValue("TabControlLayout", value);
            this.tabElem.TabControlLayout = value;
        }
    }


    /// <summary>
    /// Gets or sets the link target url.
    /// </summary>
    public string UrlTarget
    {
        get
        {
        	 return DataHelper.GetNotEmpty(this.GetValue("UrlTarget"), this.tabElem.UrlTarget); 
        }
        set
        {
            this.SetValue("UrlTarget", value);
            this.tabElem.UrlTarget = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether link title will be rendered.
    /// </summary>
    public bool RenderLinkTitle
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderLinkTitle"), this.tabElem.RenderLinkTitle);
        }
        set
        {
            this.SetValue("RenderLinkTitle", value);
            this.tabElem.RenderLinkTitle = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.tabElem.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            tabElem.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows results.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.tabElem.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.tabElem.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), this.tabElem.FilterName);
        }
        set
        {
            this.SetValue("FilterName", value);
            this.tabElem.FilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets property which indicates if menu caption should be HTML encoded.
    /// </summary>
    public bool EncodeMenuCaption
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EncodeMenuCaption"), this.tabElem.EncodeMenuCaption);
        }
        set
        {
            this.SetValue("EncodeMenuCaption", value);
            this.tabElem.EncodeMenuCaption = value;
        }
    }


    /// <summary>
    /// Gets or sets the columns to be retrieved from database.
    /// </summary>  
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), this.tabElem.Columns);
        }
        set
        {
            this.SetValue("Columns", value);
            this.tabElem.Columns = value;
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
            this.tabElem.CacheMinutes = value;
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
            tabElem.CacheDependencies = value;
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
            tabElem.CacheItemName = value;
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
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            this.tabElem.StopProcessing = true;
        }
        else
        {
            this.tabElem.ControlContext = this.ControlContext;
            
            // Set properties from Webpart form        
            this.tabElem.SelectFirstItemByDefault = this.SelectFirstItemByDefault;
            this.tabElem.ApplyMenuDesign = this.ApplyMenuDesign;
            this.tabElem.UseItemImagesForHiglightedItem = this.UseItemImagesForHiglightedItem;
            this.tabElem.CacheItemName = this.CacheItemName;
            this.tabElem.CacheDependencies = this.CacheDependencies;
            this.tabElem.CacheMinutes = this.CacheMinutes;
            this.tabElem.CheckPermissions = this.CheckPermissions;
            this.tabElem.ClassNames = this.ClassNames;
            this.tabElem.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.tabElem.CultureCode = this.CultureCode;
            this.tabElem.MaxRelativeLevel = this.MaxRelativeLevel;
            this.tabElem.HighlightedNodePath = this.HighlightedNodePath;
            this.tabElem.OrderBy = this.OrderBy;
            this.tabElem.Path = this.Path;
            this.tabElem.SelectOnlyPublished = this.SelectOnlyPublished;
            this.tabElem.SiteName = this.SiteName;
            this.tabElem.UseAlternatingStyles = this.UseAlternatingStyles;
            this.tabElem.WhereCondition = this.WhereCondition;

            this.tabElem.UseClientScript = this.UseClientScript;
            this.tabElem.TabControlLayout = this.TabControlLayout;
            this.tabElem.UrlTarget = this.UrlTarget;
            this.tabElem.RenderImageAlt = this.RenderImageAlt;
            this.tabElem.RenderLinkTitle = this.RenderLinkTitle;
            this.tabElem.TabControlIdPrefix = this.TabControlIdPrefix;
            this.tabElem.WordWrap = this.WordWrap;

            this.tabElem.HideControlForZeroRows = this.HideControlForZeroRows;
            this.tabElem.ZeroRowsText = this.ZeroRowsText;
            this.tabElem.EncodeMenuCaption = this.EncodeMenuCaption;
            this.tabElem.Columns = this.Columns;

            this.tabElem.FilterName = this.FilterName;
            this.tabElem.ReloadData(true);
        }
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = this.tabElem.Visible;

        if (DataHelper.DataSourceIsEmpty(this.tabElem.DataSource) && (this.tabElem.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }
}
