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

public partial class CMSWebParts_Navigation_cmssitemap : CMSAbstractWebPart
{
    #region "Document properties"

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
            this.smElem.CacheMinutes = value;
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
            smElem.CacheDependencies = value;
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
            smElem.CacheItemName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.smElem.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            smElem.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Classnames"), this.smElem.ClassNames), this.smElem.ClassNames);
        }
        set
        {
            this.SetValue("ClassNames", value);
            this.smElem.ClassNames = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents are combined with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), this.smElem.CombineWithDefaultCulture);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            this.smElem.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets the culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CultureCode"), this.smElem.CultureCode), this.smElem.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            this.smElem.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximal relative level.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRelativeLevel"), this.smElem.MaxRelativeLevel);
        }
        set
        {
            this.SetValue("MaxRelativeLevel", value);
            this.smElem.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by clause.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OrderBy"), this.smElem.OrderBy), this.smElem.OrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.smElem.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the nodes path.
    /// </summary>
    public string Path
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Path"), this.smElem.Path), this.smElem.Path);
        }
        set
        {
            this.SetValue("Path", value);
            this.smElem.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents must be published.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), this.smElem.SelectOnlyPublished);
        }
        set
        {
            this.SetValue("SelctOnlyPublished", value);
            this.smElem.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SiteName"), this.smElem.SiteName), this.smElem.SiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.smElem.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("WhereCondition"), this.smElem.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.smElem.WhereCondition = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether item ID will be rendered.
    /// </summary>
    public bool RenderLinkTitle
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderLinkTitle"), this.smElem.RenderLinkTitle);
        }
        set
        {
            this.SetValue("RenderLinkTitle", value);
            this.smElem.RenderLinkTitle = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether text can be wrapped or space is replaced with non breakable space.
    /// </summary>
    public bool WordWrap
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("WordWrap"), this.smElem.WordWrap);
        }
        set
        {
            this.SetValue("WordWrap", value);
            this.smElem.WordWrap = value;
        }
    }


    /// <summary>
    /// Gets or sets the link URL target.
    /// </summary>
    public string UrlTarget
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("UrlTarget"), this.smElem.UrlTarget), this.smElem.UrlTarget);
        }
        set
        {
            this.SetValue("UrlTarget", value);
            this.smElem.UrlTarget = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.smElem.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            smElem.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows results.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.smElem.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.smElem.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets or sets the filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), this.smElem.FilterName);
        }
        set
        {
            this.SetValue("FilterName", value);
            this.smElem.FilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the sitemap should apply menu inactivation flag.
    /// </summary>
    public bool ApplyMenuInactivation
    {
        get
        {
            return  ValidationHelper.GetBoolean( this.GetValue("ApplyMenuInactivation"), true);
        }
        set
        {
            this.SetValue("ApplyMenuInactivation", value);
        }
    }


    /// <summary>
    /// Gets or sets property which indicates if menu caption should be HTML encoded.
    /// </summary>
    public bool EncodeMenuCaption
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EncodeMenuCaption"), this.smElem.EncodeMenuCaption);
        }
        set
        {
            this.SetValue("EncodeMenuCaption", value);
            this.smElem.EncodeMenuCaption = value;
        }
    }


    /// <summary>
    /// Gets or sets the columns to be retrieved from database.
    /// </summary>  
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), this.smElem.Columns);
        }
        set
        {
            this.SetValue("Columns", value);
            this.smElem.Columns = value;
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
            this.smElem.StopProcessing = true;
        }
        else
        {
            this.smElem.ControlContext = this.ControlContext;
            
            // Set properties from Webpart form        
            this.smElem.CacheItemName = this.CacheItemName;
            this.smElem.CacheDependencies = this.CacheDependencies;
            this.smElem.CacheMinutes = this.CacheMinutes;
            this.smElem.CheckPermissions = this.CheckPermissions;
            this.smElem.ClassNames = this.ClassNames;
            this.smElem.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.smElem.CultureCode = this.CultureCode;
            this.smElem.MaxRelativeLevel = this.MaxRelativeLevel;
            this.smElem.OrderBy = this.OrderBy;
            this.smElem.Path = this.Path;
            this.smElem.SelectOnlyPublished = this.SelectOnlyPublished;
            this.smElem.SiteName = this.SiteName;
            this.smElem.UrlTarget = this.UrlTarget;
            this.smElem.WhereCondition = this.WhereCondition;
            this.smElem.RenderLinkTitle = this.RenderLinkTitle;
            this.smElem.WordWrap = this.WordWrap;
            this.smElem.ApplyMenuInactivation = this.ApplyMenuInactivation;
            this.smElem.HideControlForZeroRows = this.HideControlForZeroRows;
            this.smElem.ZeroRowsText = this.ZeroRowsText;
            this.smElem.FilterName = this.FilterName;
            this.smElem.EncodeMenuCaption = this.EncodeMenuCaption;
            this.smElem.Columns = this.Columns;
        }
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = this.smElem.Visible;

        if (DataHelper.DataSourceIsEmpty(this.smElem.DataSource) && (this.smElem.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }
}
