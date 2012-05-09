using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.Controls;
using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSWebParts_DataSources_WebServiceDataSource : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the web service URL.
    /// </summary>
    public string WebServiceUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WebServiceUrl"), "");
        }
        set
        {
            this.SetValue("WebServiceUrl", value);
            srcWebService.WebServiceUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the parameters of the web service.
    /// </summary>
    public string WebServiceParameters
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WebServiceParameters"), "");
        }
        set
        {
            this.SetValue("WebServiceParameters", value);
            srcWebService.WebServiceParameters = value;
        }
    }


    /// <summary>
    /// Gets or sets the source filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), "");
        }
        set
        {
            this.SetValue("FilterName", value);
            srcWebService.SourceFilterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache item name.
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
            this.srcWebService.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcWebService.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcWebService.CacheDependencies = value;
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
            this.srcWebService.CacheMinutes = value;
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
    /// Initializes control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.srcWebService.WebServiceUrl = this.WebServiceUrl;
            this.srcWebService.WebServiceParameters = this.WebServiceParameters;
            this.srcWebService.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcWebService.SourceFilterName = this.FilterName;
            this.srcWebService.CacheItemName = this.CacheItemName;
            this.srcWebService.CacheDependencies = this.CacheDependencies;
            this.srcWebService.CacheMinutes = this.CacheMinutes;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcWebService.ClearCache();
    }

    #endregion
}
