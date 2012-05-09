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

using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_MediaLibrary_FormControls_MediaLibrarySelector : FormEngineUserControl
{
    #region "Delegates & Events"

    public delegate void OnSelectedLibraryChanged();
    public event OnSelectedLibraryChanged SelectedLibraryChanged;

    #endregion


    #region "Variables"

    private bool mUseLibraryNameForSelection = true;
    private bool mAddNoneItemsRecord = false;
    private bool mAddCurrentLibraryRecord = true;
    private bool mUseAutoPostBack = false;
    private bool mNoneWhenEmpty = false;
    private string mWhere = "";
    private string mSiteName = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// ID of the site libraries should belongs to.
    /// </summary>
    public int SiteID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["SiteID"], 0);
        }
        set
        {
            ViewState["SiteID"] = value;
        }
    }


    /// <summary>
    /// Gets the site name from the site ID.
    /// </summary>
    private string SiteName
    {
        get
        {
            if (this.mSiteName == null)
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(this.SiteID);
                if (si != null)
                {
                    this.mSiteName = si.SiteName;
                }
            }
            return mSiteName;
        }
    }


    /// <summary>
    /// ID of the group libraries should belongs to.
    /// </summary>
    public int GroupID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["GroupID"], 0);
        }
        set
        {
            ViewState["GroupID"] = value;
        }
    }


    /// <summary>
    /// Gets or sets WHERE condition used to filter libraries.
    /// </summary>
    public string Where
    {
        get
        {
            return this.mWhere;
        }
        set
        {
            this.mWhere = value;

            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            this.uniSelector.WhereCondition = GetWhereConditionInternal();
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (this.UseLibraryNameForSelection)
            {
                return this.MediaLibraryName;
            }
            else
            {
                return this.MediaLibraryID;
            }
        }
        set
        {
            if (this.UseLibraryNameForSelection)
            {
                this.MediaLibraryName = ValidationHelper.GetString(value, "");
            }
            else
            {
                this.MediaLibraryID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the MediaLibrary ID.
    /// </summary>
    public int MediaLibraryID
    {
        get
        {
            if (this.UseLibraryNameForSelection)
            {
                string name = ValidationHelper.GetString(uniSelector.Value, "");
                MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(name, this.SiteName);
                if (mli != null)
                {
                    return mli.LibraryID;
                }
                return 0;
            }
            else
            {
                return ValidationHelper.GetInteger(uniSelector.Value, 0);
            }
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            if (this.UseLibraryNameForSelection)
            {
                MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(value);
                if (mli != null)
                {
                    this.uniSelector.Value = mli.LibraryID;
                }
            }
            else
            {
                this.uniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the MediaLibrary code name.
    /// </summary>
    public string MediaLibraryName
    {
        get
        {
            if (this.UseLibraryNameForSelection)
            {
                return ValidationHelper.GetString(this.uniSelector.Value, "");
            }
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelector.Value, 0);
                MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(id);
                if (mli != null)
                {
                    return mli.LibraryName;
                }
                return "";
            }
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            if (this.UseLibraryNameForSelection)
            {
                this.uniSelector.Value = value;
            }
            else
            {
                MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(value, this.SiteName);
                if (mli != null)
                {
                    this.uniSelector.Value = mli.LibraryName;
                }
            }
        }
    }


    /// <summary>
    /// If true, selected value is LibraryName, if false, selected value is LibraryID.
    /// </summary>
    public bool UseLibraryNameForSelection
    {
        get
        {
            return this.mUseLibraryNameForSelection;
        }
        set
        {
            this.mUseLibraryNameForSelection = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add none item record to the dropdownlist.
    /// </summary>
    public bool AddNoneItemsRecord
    {
        get
        {
            return this.mAddNoneItemsRecord;
        }
        set
        {
            this.mAddNoneItemsRecord = value;
        }
    }


    /// <summary>
    /// Indicates whether the '(none)' record should be added automatically when no library loaded.
    /// </summary>
    public bool NoneWhenEmpty
    {
        get
        {
            return this.mNoneWhenEmpty;
        }
        set
        {
            this.mNoneWhenEmpty = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add current media library record to the dropdownlist.
    /// </summary>
    public bool AddCurrentLibraryRecord
    {
        get
        {
            return this.mAddCurrentLibraryRecord;
        }
        set
        {
            this.mAddCurrentLibraryRecord = value;
        }
    }


    /// <summary>
    /// Indicates if autopostback should be used.
    /// </summary>
    public bool UseAutoPostBack
    {
        get
        {
            return this.mUseAutoPostBack;
        }
        set
        {
            this.mUseAutoPostBack = value;

            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            this.uniSelector.DropDownSingleSelect.AutoPostBack = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;

            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            this.uniSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Returns ClientID of the dropdownlist.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect.ClientID;
        }
    }


    /// <summary>
    /// Inner control.
    /// </summary>
    public DropDownList InnerControl
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData(false);
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        ReloadData(true);
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData(bool forceReload)
    {
        string where = GetWhereConditionInternal();

        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowEmpty = false;
        this.uniSelector.AllowAll = false;
        this.uniSelector.ReturnColumnName = (this.UseLibraryNameForSelection ? "LibraryName" : "LibraryID");
        this.uniSelector.WhereCondition = where;
        this.uniSelector.OnSelectionChanged += new EventHandler(uniSelector_OnSelectionChanged);
        this.uniSelector.DropDownSingleSelect.AutoPostBack = this.UseAutoPostBack;

        bool noLibrary = DataHelper.DataSourceIsEmpty(MediaLibraryInfoProvider.GetMediaLibraries(where, null, 1, "LibraryID"));

        // Insert '(none)' record if no library exists - only if '(none)' isn't inserted by default
        if (((noLibrary && this.NoneWhenEmpty) || this.AddNoneItemsRecord) && this.AddCurrentLibraryRecord)
        {
            this.uniSelector.SpecialFields = new string[2, 2] {{ GetString("general.selectnone"), "" }, 
                                                               { GetString("media.current"), MediaLibraryInfoProvider.CURRENT_LIBRARY } };
        }
        else
        {
            if ((noLibrary && this.NoneWhenEmpty) || this.AddNoneItemsRecord)
            {
                this.uniSelector.SpecialFields = new string[1, 2] { { GetString("general.selectnone"), "" } };
            }
            else if (this.AddCurrentLibraryRecord)
            {
                this.uniSelector.SpecialFields = new string[1, 2] { { GetString("media.current"), MediaLibraryInfoProvider.CURRENT_LIBRARY } };
            }
            else
            {
                this.uniSelector.SpecialFields = null;
            }
        }

        if (forceReload)
        {
            this.uniSelector.Reload(true);
        }
    }


    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Let registered controls to know library selection changed
        if (this.SelectedLibraryChanged != null)
        {
            SelectedLibraryChanged();
        }
    }


    private string GetWhereConditionInternal()
    {
        string where = "LibraryGroupID " +((GroupID > 0) ? "=" + GroupID : "IS NULL");

        if (SiteID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "LibrarySiteID = " + SiteID);
        }
        else if (SiteID == 0)
        {            
            where = SqlHelperClass.AddWhereCondition(where, "LibrarySiteID = " + CMSContext.CurrentSiteID);
        }
        else
        {
            where = SqlHelperClass.AddWhereCondition(where, SqlHelperClass.NO_DATA_WHERE);
        }

        if (Where != "")
        {
            where = SqlHelperClass.AddWhereCondition(where, Where);
        }

        return where;
    }
}
