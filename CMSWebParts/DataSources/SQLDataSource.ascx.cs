using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSWebParts_DataSources_SQLDataSource : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gest or sest the cache item name.
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
            this.srcSQL.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, this.srcSQL.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            this.srcSQL.CacheDependencies = value;
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
            this.srcSQL.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the server authentication mode.
    /// </summary>
    public int AuthenticationMode
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("AuthenticationMode"), 0);
        }
        set
        {
            this.SetValue("AuthenticationMode", value);
            SQLServerAuthenticationModeEnum mode = (value == 0) ? SQLServerAuthenticationModeEnum.SQLServerAuthentication : SQLServerAuthenticationModeEnum.WindowsAuthentication;
            this.srcSQL.AuthenticationMode = mode;
        }
    }


    /// <summary>
    /// Gets or sets query text.
    /// </summary>
    public string QueryText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("QueryText"), null);
        }
        set
        {
            this.SetValue("QueryText", value);
            this.srcSQL.QueryText = value;
        }
    }


    /// <summary>
    /// Gets or sets query type. (Standard query or stored procedure.).
    /// </summary>
    public int QueryType
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("QueryType"), 0);
        }
        set
        {
            this.SetValue("QueryType", value);
            QueryTypeEnum type = (value == 0) ? QueryTypeEnum.SQLQuery : QueryTypeEnum.StoredProcedure;
            this.srcSQL.QueryType = type;
        }
    }

    
    /// <summary>
    /// Gets or sets complete connection string.
    /// </summary>
    public string ConnectionString
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ConnectionString"), null);
        }
        set
        {
            this.SetValue("ConnectionString", value);
            this.srcSQL.ConnectionString = value;
        }
    }


    /// <summary>
    /// Gets or sets database name.
    /// </summary>
    public string DatabaseName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DatabaseName"), null);
        }
        set
        {
            this.SetValue("DatabaseName", value);
            this.srcSQL.DatabaseName = value;
        }
    }


    /// <summary>
    /// Gets or sets database server name.
    /// </summary>
    public string ServerName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ServerName"), null);
        }
        set
        {
            this.SetValue("ServerName", value);
            this.srcSQL.ServerName = value;
        }
    }


    /// <summary>
    /// Gets or sets user name.
    /// </summary>
    public string UserName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("UserName"), null);
        }
        set
        {
            this.SetValue("UserName", value);
            this.srcSQL.UserName = value;
        }
    }


    /// <summary>
    /// Gets or sets password.
    /// </summary>
    public string Password
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Password"), null);
        }
        set
        {
            this.SetValue("Password", value);
            this.srcSQL.Password = value;
        }
    }


    /// <summary>
    /// Gets or sets connection language.
    /// </summary>
    public string Language
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Language"), null), "English");
        }
        set
        {
            this.SetValue("Language", value);
            this.srcSQL.Language = value;
        }
    }


    /// <summary>
    /// Gets or sets connection timeout (240 seconds by default).
    /// </summary>
    public int Timeout
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Timeout"), this.srcSQL.Timeout);
        }
        set
        {
            this.SetValue("Timeout", value);
            this.srcSQL.Timeout = value;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        this.SetupControl();
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
            // Document properties
            this.srcSQL.FilterName = ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID);
            this.srcSQL.CacheItemName = this.CacheItemName;
            this.srcSQL.CacheDependencies = this.CacheDependencies;
            this.srcSQL.CacheMinutes = this.CacheMinutes;
            
            this.srcSQL.AuthenticationMode = (this.AuthenticationMode == 0) ? SQLServerAuthenticationModeEnum.SQLServerAuthentication : SQLServerAuthenticationModeEnum.WindowsAuthentication;
            this.srcSQL.ServerName = this.ServerName;
            this.srcSQL.Password = this.Password;
            this.srcSQL.UserName = this.UserName;
            this.srcSQL.DatabaseName = this.DatabaseName;
            this.srcSQL.QueryType = (this.QueryType == 0) ? QueryTypeEnum.SQLQuery : QueryTypeEnum.StoredProcedure;
            this.srcSQL.QueryText = this.QueryText;
            this.srcSQL.ConnectionString = this.ConnectionString;
            this.srcSQL.Language = this.Language;
            this.srcSQL.Timeout = this.Timeout;
        }
    }


    /// <summary>
    /// Clears cache.
    /// </summary>
    public override void ClearCache()
    {
        this.srcSQL.ClearCache();
    }
}
