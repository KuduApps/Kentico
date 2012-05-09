using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Ecommerce_FormControls_CurrencySelector : FormEngineUserControl
{
    #region "Variables"

    private bool mShowAllItems = false;
    private bool mAddNoneRecord = false;
    private bool mAddSelectRecord = false;
    private bool mDisplayOnlyWithExchangeRate = false;
    private bool mRenderInline = false;
    private bool mAddSiteDefaultCurrency = false;
    private bool mExcludeSiteDefaultCurrency = false;
    private bool mDisplayOnlyEnabled = true;
    private bool? mUsingGlobalObjects = null;
    private int mSiteId = -1;
    private string mAdditionalItems = "";
    private bool mIncludeSelected = true;
    private bool mDoFullPostback = false;

    #endregion


    #region "Public properties"

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
            if (this.uniSelector != null)
            {
                this.uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Indicates whether to show all items ("more items" is not displayed).
    /// </summary>
    public bool ShowAllItems
    {
        get
        {
            return this.mShowAllItems;
        }
        set
        {
            this.mShowAllItems = value;
        }
    }


    /// <summary>
    /// Indicates whether to render update panel in inline mode.
    /// </summary>
    public bool RenderInline
    {
        get
        {
            return this.mRenderInline;
        }
        set
        {
            this.mRenderInline = value;
        }
    }


    /// <summary>
    /// Indicates if only currencies with exchange rate will be displayed. Main currency will be included. Default value is false. 
    /// </summary>
    public bool DisplayOnlyWithExchangeRate
    {
        get
        {
            return mDisplayOnlyWithExchangeRate;
        }
        set
        {
            mDisplayOnlyWithExchangeRate = value;
        }
    }


    /// <summary>
    /// Add none record to the dropdownlist.
    /// </summary>
    public bool AddNoneRecord
    {
        get
        {
            return mAddNoneRecord;
        }
        set
        {
            mAddNoneRecord = value;

            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            this.uniSelector.AllowEmpty = value;
        }
    }


    /// <summary>
    /// Add (select) record to the dropdownlist.
    /// </summary>
    public bool AddSelectRecord
    {
        get
        {
            return mAddSelectRecord;
        }
        set
        {
            mAddSelectRecord = value;
        }
    }


    /// <summary>
    /// Indicates whether to add current site default currency.
    /// </summary>
    public bool AddSiteDefaultCurrency
    {
        get
        {
            return mAddSiteDefaultCurrency;
        }
        set
        {
            mAddSiteDefaultCurrency = value;
        }
    }


    /// <summary>
    /// Indicates whether to exclude current site default currency.
    /// </summary>
    public bool ExcludeSiteDefaultCurrency
    {
        get
        {
            return mExcludeSiteDefaultCurrency;
        }
        set
        {
            mExcludeSiteDefaultCurrency = value;
        }
    }


    /// <summary>
    /// Indicates if selected value will be included in the list despite of other conditions.
    /// </summary>
    public bool IncludeSelected
    {
        get
        {
            return mIncludeSelected;
        }
        set
        {
            mIncludeSelected = value;
        }
    }


    /// <summary>
    /// Currency ID.
    /// </summary>
    public int CurrencyID
    {
        get
        {
            return ValidationHelper.GetInteger(uniSelector.Value, 0);
        }
        set
        {
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }
            uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.CurrencyID;
        }
        set
        {
            this.CurrencyID = ValidationHelper.GetInteger(value, 0);
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
    /// Returns inner DropDownList control.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Returns inner UniSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return this.uniSelector;
        }
    }

    /// <summary>
    /// Allows to display currencies only for specified site id. Use 0 for global currencies. Default value is current site id.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteId == -1)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;
            mUsingGlobalObjects = null;
        }
    }


    /// <summary>
    /// Allows to display only enabled items. Default value is true.
    /// </summary>
    public bool DisplayOnlyEnabled
    {
        get
        {
            return mDisplayOnlyEnabled;
        }
        set
        {
            mDisplayOnlyEnabled = value;
        }
    }


    /// <summary>
    /// Id of items which have to be displayed. Use ',' or ';' as separator if more ids required.
    /// </summary>
    public string AdditionalItems
    {
        get
        {
            return mAdditionalItems;
        }
        set
        {
            // Prevent from setting null value
            if (value != null)
            {
                mAdditionalItems = value.Replace(';', ',');
            }
            else
            {
                mAdditionalItems = "";
            }
        }
    }


    /// <summary>
    /// Indicates whether update panel is enabled.
    /// </summary>
    public bool DoFullPostback
    {
        get
        {
            return mDoFullPostback;
        }
        set
        {
            mDoFullPostback = value;
        }
    }

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Returns true if site given by SiteID uses global currencies.
    /// </summary>
    protected bool UsingGlobalObjects
    {
        get
        {
            // Unknown yet
            if (!mUsingGlobalObjects.HasValue)
            {
                mUsingGlobalObjects = false;
                // Try to figure out from settings
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    mUsingGlobalObjects = ECommerceSettings.UseGlobalCurrencies(si.SiteName);
                }
            }

            return mUsingGlobalObjects.Value;
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
            InitSelector();
        }
    }


    /// <summary>
    /// Inits the selector.
    /// </summary>
    protected void InitSelector()
    {
        if (this.RenderInline)
        {
            this.pnlUpdate.RenderMode = UpdatePanelRenderMode.Inline;
        }
        if (this.ShowAllItems)
        {
            this.uniSelector.MaxDisplayedItems = 1000;
        }

        this.uniSelector.EnabledColumnName = "CurrencyEnabled";
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;

        if (DoFullPostback)
        {
            ControlsHelper.RegisterPostbackControl(this.uniSelector.DropDownSingleSelect);
        }

        if (AddSelectRecord)
        {
            string[,] fields = new string[1, 2];
            fields[0, 0] = GetString("currencyselector.select");
            fields[0, 1] = "-1";
            this.uniSelector.SpecialFields = fields;
        }

        CurrencyInfo main = CurrencyInfoProvider.GetMainCurrency(this.SiteID);
        int mainCurrencyId = (main != null) ? main.CurrencyID : 0;

        string where = "";
        if (DisplayOnlyWithExchangeRate)
        {
            ExchangeTableInfo tableInfo = ExchangeTableInfoProvider.GetLastExchangeTableInfo(this.SiteID);
            if (tableInfo != null)
            {
                where = "(CurrencyID = " + mainCurrencyId + " OR CurrencyID IN (SELECT ExchangeRateToCurrencyID FROM COM_CurrencyExchangeRate WHERE COM_CurrencyExchangeRate.ExchangeTableID = " + tableInfo.ExchangeTableID + ") AND CurrencyEnabled = 1)";
            }
            else
            {
                where = "(0=1)";
            }
        }

        if (this.AddSiteDefaultCurrency && (main != null))
        {
            where = SqlHelperClass.AddWhereCondition(where, "CurrencyID = " + mainCurrencyId, "OR");
        }

        if (this.ExcludeSiteDefaultCurrency && (main != null))
        {
            where = SqlHelperClass.AddWhereCondition(where, "(NOT CurrencyID = " + mainCurrencyId + ")");
        }

        // Select only records by speciffied site
        if (SiteID >= 0)
        {
            // Show global records by default
            int filteredSiteId = 0;

            // Check configuration when site specified
            if (SiteID > 0)
            {
                // Show site specific records when not using global statuses
                filteredSiteId = UsingGlobalObjects ? 0 : SiteID;
            }

            where = SqlHelperClass.AddWhereCondition(where, "(ISNULL(CurrencySiteID, 0) = " + filteredSiteId + ")");
        }

        // Filter out only enabled items
        if (this.DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "CurrencyEnabled = 1");
        }

        // Add items which have to be on the list (if any)
        string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
        if ((!string.IsNullOrEmpty(where)) && (!string.IsNullOrEmpty(additionalList)))
        {
            where = SqlHelperClass.AddWhereCondition(where, "(CurrencyID IN (" + additionalList + "))", "OR");
        }

        // Selected value (if any) must be on the list
        if ((!string.IsNullOrEmpty(where)) && (CurrencyID > 0) && (IncludeSelected))
        {
            where = SqlHelperClass.AddWhereCondition(where, "(CurrencyID = " + CurrencyID + ")", "OR");
        }

        // Set where condition
        this.uniSelector.WhereCondition = where;
    }
}
