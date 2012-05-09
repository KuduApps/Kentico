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

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_DiscountLevelSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mAddNoneRecord = false;
    private string[,] mSpecialFields = null;
    private bool mDisplayOnlyEnabled = true;
    private bool? mAllowGlobalObjects = null;
    private bool? mDisplaySiteItems = null;
    private bool? mDisplayGlobalItems = null;
    private int mSiteId = -1;
    private string mAdditionalItems = "";

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
    /// DiscountLevel.
    /// </summary>
    public int DiscountLevel
    {
        get
        {
            return ValidationHelper.GetInteger(uniSelector.Value, 0);
        }
        set
        {
            uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Add none record (option) to dropdownlist.
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
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.DiscountLevel;
        }
        set
        {
            this.DiscountLevel = ValidationHelper.GetInteger(value, 0);
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
    /// Indicates if site items will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplaySiteItems
    {
        get
        {
            // Unknown yet
            if (!mDisplaySiteItems.HasValue)
            {
                // Display site item when working with site records
                mDisplaySiteItems = (SiteID != 0);
            }

            return mDisplaySiteItems.Value;
        }
        set
        {
            mDisplaySiteItems = value;
        }
    }


    /// <summary>
    /// Indicates if global items will be displayed. By default, value is based on SiteID property and global objects setting.
    /// </summary>
    public bool DisplayGlobalItems
    {
        get
        {
            // Unknown yet
            if (!mDisplayGlobalItems.HasValue)
            {
                mDisplayGlobalItems = false;
                if ((SiteID == 0) || AllowGlobalObjects)
                {
                    mDisplayGlobalItems = true;
                }
            }

            return mDisplayGlobalItems.Value;
        }
        set
        {
            mDisplayGlobalItems = value;
        }
    }


    /// <summary>
    /// Allows to display discount levels only for specified site id. Use 0 for global discount levels. Default value is current site id.
    /// </summary>
    public int SiteID
    {
        get
        {
            // No site id given
            if (mSiteId == -1)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;

            mDisplayGlobalItems = null;
            mDisplaySiteItems = null;
            mAllowGlobalObjects = null;
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
    /// ID of items which have to be displayed. Use ',' or ';' as separator if more IDs required.
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
    /// Array with the special field values that should be added to the DropDownList selection, 
    /// between the (none) item and the other items, e.g.: 
    /// string[1,2]: { { "(current group)", "{%CommunityContext.CurrentGroup.GroupID%}" } }.
    /// </summary>
    public string[,] SpecialFields
    {
        get
        {
            return mSpecialFields;
        }
        set
        {
            mSpecialFields = value;
        }
    }

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Returns true if site given by SiteID uses global suppliers beside site-specific ones.
    /// </summary>
    protected bool AllowGlobalObjects
    {
        get
        {
            // Unknown yet
            if (!mAllowGlobalObjects.HasValue)
            {
                mAllowGlobalObjects = false;
                // Try to figure out from settings
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    mAllowGlobalObjects = ECommerceSettings.AllowGlobalDiscountLevels(si.SiteName);
                }
            }

            return mAllowGlobalObjects.Value;
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
        this.uniSelector.EnabledColumnName = "DiscountLevelEnabled";
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;
        this.uniSelector.SpecialFields = this.SpecialFields;

        string where = "";
        // Add global items
        if (DisplayGlobalItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "DiscountLevelSiteID IS NULL", "OR");
        }
        // Add site specific items
        if (DisplaySiteItems)
        {
            where = SqlHelperClass.AddWhereCondition(where, "DiscountLevelSiteID = " + SiteID, "OR");
        }

        // Filter out only enabled items
        if (this.DisplayOnlyEnabled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "DiscountLevelEnabled = 1");
        }

        // Add items which have to be on the list
        string additionalList = SqlHelperClass.GetSafeQueryString(this.AdditionalItems, false);
        if (!string.IsNullOrEmpty(additionalList))
        {
            where = SqlHelperClass.AddWhereCondition(where, "DiscountLevelID IN (" + additionalList + ")", "OR");
        }

        // Selected value must be on the list
        if (DiscountLevel > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "DiscountLevelID = " + DiscountLevel, "OR");
        }

        this.uniSelector.WhereCondition = where;
    }
}
