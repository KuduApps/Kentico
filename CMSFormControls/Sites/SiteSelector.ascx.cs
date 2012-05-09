using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.PortalControls;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.CMSHelper;

public partial class CMSFormControls_Sites_SiteSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mAllowEmpty = false;
    private bool mAllowAll = true;
    private bool mAllowSetSpecialFields = false;
    private bool mOnlyRunning = false;
    private bool mUseCodeNameForSelection = false;
    private bool mIsLiveSite = true;
    private int mUserId = 0;
    private string mDisplayNameFormat = "{%SiteDisplayName%}";
    private bool mAllowGlobal = false;
    private string mGlobalRecordValue = UniSelector.US_GLOBAL_RECORD.ToString();
    private bool mPostbackOnDropDownChange = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets selected items.
    /// </summary>
    public override object Value
    {
        get
        {
            if (UseCodeNameForSelection)
            {
                return this.SiteName;
            }
            else
            {
                return this.SiteID;
            }
        }
        set
        {
            if (uniSelector == null)
            {
                pnlUpdate.LoadContainer();
            }

            if (uniSelector != null)
            {
                if (UseCodeNameForSelection)
                {
                    this.SiteName = ValidationHelper.GetString(value, String.Empty);
                }
                else
                {
                    int siteid = ValidationHelper.GetInteger(value, 0);

                    // Site id 0 can't exists, do not set it if not explicitly enabled
                    if ((siteid > 0) || this.AllowSetSpecialFields)
                    {
                        this.SiteID = siteid;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets the value which indicates whether to use SiteID or SiteName for value.
    /// </summary>
    public bool UseCodeNameForSelection
    {
        get
        {
            return mUseCodeNameForSelection;
        }
        set
        {
            mUseCodeNameForSelection = value;
        }
    }


    /// <summary>
    /// Enables or disables (all) item in selector.
    /// </summary>
    public bool AllowAll
    {
        get
        {
            return mAllowAll;
        }
        set
        {
            mAllowAll = value;
            if (uniSelector != null)
            {
                uniSelector.AllowAll = value;
            }
        }
    }


    /// <summary>
    /// Enables or disables (empty) item in selector.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return mAllowEmpty;
        }
        set
        {
            mAllowEmpty = value;
            if (uniSelector != null)
            {
                uniSelector.AllowEmpty = value;
            }
        }
    }


    /// <summary>
    /// Enables or disables (global) item in selector. Uses uniSelector's SpecialFields property.
    /// </summary>
    public bool AllowGlobal
    {
        get
        {
            return mAllowGlobal;
        }
        set
        {
            mAllowGlobal = value;
            if (uniSelector != null)
            {
                if (mAllowGlobal)
                {
                    uniSelector.SpecialFields = new string[1, 2] { { GetString("general.global"), this.GlobalRecordValue } };
                    AllowSetSpecialFields = true;
                }
                else
                {
                    uniSelector.SpecialFields = null;
                }
            }
        }
    }


    /// <summary>
    /// Value for (global) item, by default set to -4.
    /// </summary>
    public string GlobalRecordValue
    {
        get
        {
            return mGlobalRecordValue;
        }
        set
        {
            mGlobalRecordValue = value;
        }
    }


    /// <summary>
    /// Value for (all) item
    /// </summary>
    public string AllRecordValue
    {
        get
        {
            return uniSelector.AllRecordValue;
        }
        set
        {
            uniSelector.AllRecordValue = value;
        }
    }


    /// <summary>
    /// If enabled Value property allows to set special fields values (0 or lower).
    /// </summary>
    public bool AllowSetSpecialFields
    {
        get
        {
            return mAllowSetSpecialFields;
        }
        set
        {
            mAllowSetSpecialFields = value;
        }
    }


    /// <summary>
    /// Gets the inner UniSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return uniSelector;
        }
    }


    /// <summary>
    /// Gets the single select drop down field.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            return uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets UpdatePanel of selector.
    /// </summary>
    public CMSUpdatePanel UpdatePanel
    {
        get
        {
            return pnlUpdate;
        }
    }


    /// <summary>
    /// Enables or disables selector filter to RUNNING site status.
    /// </summary>
    public bool OnlyRunningSites
    {
        get
        {
            return mOnlyRunning;
        }
        set
        {
            mOnlyRunning = value;
        }
    }


    /// <summary>
    /// Gets or sets user id. If set site selector shows only sites assigned to user.
    /// </summary>
    public int UserId
    {
        get
        {
            return mUserId;
        }
        set
        {
            mUserId = value;
        }
    }


    /// <summary>
    /// Enables or disables site selector dropdown.
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
            uniSelector.Enabled = value;
            this.DropDownSingleSelect.Enabled = value;
        }
    }


    /// <summary>
    /// Enables or disables live site mode of selection dialog.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return mIsLiveSite;
        }
        set
        {
            mIsLiveSite = value;
            if (uniSelector != null)
            {
                uniSelector.IsLiveSite = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets DisplayNameFormat property of uni selector.
    /// </summary>
    public string DisplayNameFormat
    {
        get
        {
            return mDisplayNameFormat;
        }
        set
        {
            mDisplayNameFormat = value;
            if (uniSelector != null)
            {
                uniSelector.DisplayNameFormat = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets SiteID of current selected site.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (UseCodeNameForSelection)
            {
                string siteName = ValidationHelper.GetString(uniSelector.Value, null);
                SiteInfo si = SiteInfoProvider.GetSiteInfo(siteName);
                if (si != null)
                {
                    return si.SiteID;
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
            if (UseCodeNameForSelection)
            {
                string siteName = ValidationHelper.GetString(uniSelector.Value, null);
                SiteInfo si = SiteInfoProvider.GetSiteInfo(siteName);
                if (si != null)
                {
                    uniSelector.Value = si.SiteName;
                }
            }
            else
            {
                uniSelector.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets Site name of current selected site.
    /// </summary>
    public string SiteName
    {
        get
        {
            if (UseCodeNameForSelection)
            {
                return ValidationHelper.GetString(uniSelector.Value, null);
            }
            else
            {
                int siteId = ValidationHelper.GetInteger(uniSelector.Value, 0);
                SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                if (si != null)
                {
                    return si.SiteName;
                }
                return String.Empty;
            }
        }
        set
        {
            if (UseCodeNameForSelection)
            {
                uniSelector.Value = value;
            }
            else
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(value.ToString());
                if (si != null)
                {
                    uniSelector.Value = si.SiteID;
                }
            }
        }
    }


    /// <summary>
    /// If true, full postback is called when site changed
    /// </summary>
    public bool PostbackOnDropDownChange
    {
        get
        {
            return mPostbackOnDropDownChange;
        }
        set
        {
            mPostbackOnDropDownChange = value;
        }
    }


    /// <summary>
    /// Sets or gets stop processing state.
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
            if (uniSelector != null)
            {
                uniSelector.StopProcessing = value;
            }
        }
    }


    /// <summary>
    /// CSS class of current control.
    /// </summary>
    public string DropDownCssClass
    {
        get
        {
            return uniSelector.DropDownSingleSelect.CssClass;
        }
        set
        {
            uniSelector.DropDownSingleSelect.CssClass = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (PostbackOnDropDownChange)
        {
            uniSelector.DropDownSingleSelect.AutoPostBack = true;
            ScriptManager scr = ScriptManager.GetCurrent(Page);
            scr.RegisterPostBackControl(uniSelector);
        }

        uniSelector.DisplayNameFormat = this.DisplayNameFormat;
        uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;

        // Generate where condition
        UpdateWhereCondition();

        uniSelector.ReturnColumnName = (UseCodeNameForSelection ? "SiteName" : "SiteID");
        uniSelector.AllowEmpty = this.AllowEmpty;
        uniSelector.AllowAll = this.AllowAll;
        uniSelector.IsLiveSite = this.IsLiveSite;
        uniSelector.OrderBy = "SiteDisplayName";

        if ((AllowAll) && (UseCodeNameForSelection))
        {
            uniSelector.AllRecordValue = TreeProvider.ALL_SITES;
        }

        // If user is not admin and first time control loaded - set current site
        if ((ValidationHelper.GetString(uniSelector.Value, String.Empty) == String.Empty) && (!CMSContext.CurrentUser.IsGlobalAdministrator))
        {
            uniSelector.Value = CMSContext.CurrentSiteName;
        }
    }

    #endregion


    /// <summary>
    /// Updates uni selector where condition based on current properties values.
    /// </summary>
    public void UpdateWhereCondition()
    {
        // Running sites where condition
        if (OnlyRunningSites)
        {
            // Running status where condition
            string where = "SiteStatus = '" + SiteInfoProvider.SiteStatusToString(SiteStatusEnum.Running) + "'";

            // Combine where conditions
            uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(uniSelector.WhereCondition, where);
        }

        // Only sites assigned to user
        if (this.UserId != 0)
        {
            // User's site where condition
            string where = "SiteID IN (SELECT SiteID FROM CMS_UserSite WHERE UserID = " + this.UserId + ")";

            // Combine where conditions
            uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(uniSelector.WhereCondition, where);
        }
    }


    /// <summary>
    /// Reloads all controls.
    /// </summary>
    /// <param name="forceReload">Indicates if data should be loaded from DB</param>
    public void Reload(bool forceReload)
    {
        uniSelector.Reload(forceReload);
    }
}
