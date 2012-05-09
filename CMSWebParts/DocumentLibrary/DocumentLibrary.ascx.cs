using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;

public partial class CMSWebParts_DocumentLibrary_DocumentLibrary : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Alias path determining parent document of library.
    /// </summary>
    public string LibraryPath
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LibraryPath"), libraryElem.LibraryPath);
        }
        set
        {
            SetValue("LibraryPath", value);
            libraryElem.LibraryPath = CMSContext.ResolveCurrentPath(value);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether documents are combined with default culture version.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CombineWithDefaultCulture"), libraryElem.CombineWithDefaultCulture);
        }
        set
        {
            SetValue("CombineWithDefaultCulture", value);
            libraryElem.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Number of displayed documents.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("PageSize"), libraryElem.PageSize);
        }
        set
        {
            SetValue("PageSize", value);
            libraryElem.PageSize = value;
        }
    }


    /// <summary>
    /// Specifies the form used for editing document properties.
    /// </summary>
    public string DocumentForm
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DocumentForm"), libraryElem.DocumentForm);
        }
        set
        {
            SetValue("DocumentForm", value);
            libraryElem.DocumentForm = value;
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
            libraryElem.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return ValidationHelper.GetString(base.CacheDependencies, libraryElem.CacheDependencies);
        }
        set
        {
            base.CacheDependencies = value;
            libraryElem.CacheDependencies = value;
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
            libraryElem.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows result.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ZeroRowsText"), libraryElem.ZeroRowsText);
        }
        set
        {
            SetValue("ZeroRowsText", value);
            libraryElem.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions should be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CheckPermissions"), libraryElem.CheckPermissions);
        }
        set
        {
            SetValue("CheckPermissions", value);
        }
    }


    /// <summary>
    /// Permissions icon image URL.
    /// </summary>
    public string PermissionsIconImageUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("PermissionsIconImageUrl"), libraryElem.PermissionsIconImageUrl);
        }
        set
        {
            SetValue("PermissionsIconImageUrl", value);
            libraryElem.PermissionsIconImageUrl = value;
        }
    }


    /// <summary>
    /// New file icon image URL.
    /// </summary>
    public string NewFileIconImageUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(GetValue("NewFileIconImageUrl"), libraryElem.NewFileIconImageUrl);
        }
        set
        {
            SetValue("NewFileIconImageUrl", value);
            libraryElem.NewFileIconImageUrl = value;
        }
    }

    #endregion


    #region "Page events"

    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
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
            libraryElem.StopProcessing = value;
        }
    }

    #endregion


    #region "Methods"

    private void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
            libraryElem.StopProcessing = true;
        }
        else
        {
            // Initialize document library control
            libraryElem.LibraryPath = CMSContext.ResolveCurrentPath(LibraryPath);
            libraryElem.CombineWithDefaultCulture = CombineWithDefaultCulture;
            libraryElem.PageSize = PageSize;
            libraryElem.DocumentForm = DocumentForm;
            libraryElem.CacheDependencies = CacheDependencies;
            libraryElem.CacheItemName = CacheItemName;
            libraryElem.CacheMinutes = CacheMinutes;
            libraryElem.ZeroRowsText = ZeroRowsText;
            libraryElem.CheckPermissions = CheckPermissions;
            libraryElem.PermissionsIconImageUrl = PermissionsIconImageUrl;
            libraryElem.NewFileIconImageUrl = NewFileIconImageUrl;
        }
    }

    #endregion
}
