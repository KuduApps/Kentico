using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_FormControls_ContactStatusSelector : FormEngineUserControl
{
    #region "Variables"

    private int mSiteID = CMSContext.CurrentSiteID;
    private bool? mIsSiteManager;

    #endregion


    #region "Properties"

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
            this.EnsureChildControls();
            base.Enabled = value;
            this.uniselector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            if (uniselector.Value.ToString() == UniSelector.US_NONE_RECORD.ToString())
            {
                return null;
            }
            else
            {
                return uniselector.Value;
            }
        }
        set
        {
            this.EnsureChildControls();
            uniselector.Value = ValidationHelper.GetString(value, "");
        }
    }


    /// <summary>
    /// Gets or sets SiteID of contact statuses.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;
        }
    }


    /// <summary>
    /// Returns Uniselector.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            if (uniselector == null)
            {
                pnlUpdate.LoadContainer();
            }
            return uniselector;
        }
    }


    /// <summary>
    /// Dropdownlist used in Uniselector.
    /// </summary>
    public DropDownList DropDownList
    {
        get
        {
            if (uniselector == null)
            {
                pnlUpdate.LoadContainer();
            }
            return uniselector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Specifies, whether the selector allows selection of all items. If the dialog allows selection of all items, 
    /// it displays the (all) field in the DDL variant and All button in the Textbox variant (default false). 
    /// When property is selected then Uniselector doesn't load any data from DB, it just returns -1 value 
    /// and external code must handle data loading.
    /// </summary>
    public bool AllowAllItem
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("AllowAllItem"), true);
        }
        set
        {
            SetValue("AllowAllItem", value);
        }
    }


    /// <summary>
    /// Gets or sets if all contact statuses regardless of site should be displayed.
    /// </summary>
    public bool DisplayAll
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets if only global and site statuses should be displayed.
    /// </summary>
    public bool DisplaySiteOrGlobal
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplaySiteOrGlobal"), false);
        }
        set
        {
            SetValue("DisplaySiteOrGlobal", value);
        }
    }


    /// <summary>
    /// Gets selected ContactStatusID.
    /// </summary>
    public int ContactStatusID
    {
        get
        {
            return ValidationHelper.GetInteger(this.Value, 0);
        }
    }


    /// <summary>
    /// SQL WHERE condition of uniselector.
    /// </summary>
    public string WhereCondition
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets if current context is in sitemanager.
    /// </summary>
    public bool? IsSiteManager
    {
        get
        {
            return mIsSiteManager ?? QueryHelper.GetBoolean("issitemanager", false);
        }
        set
        {
            mIsSiteManager = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            uniselector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    public void ReloadData()
    {
        string where = WhereCondition;

        bool globalInCMSDesk = (SiteID == UniSelector.US_GLOBAL_RECORD) && SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".cmscmglobalconfiguration");
        bool settingEnabledForSite = SettingsKeyProvider.GetBoolValue(SiteInfoProvider.GetSiteName(SiteID) + ".cmscmglobalconfiguration");
        bool userInSiteManager = (IsSiteManager == true);
        bool userAuthorized = ConfigurationHelper.AuthorizedReadConfiguration(UniSelector.US_GLOBAL_RECORD, false);

        bool allowGlobal = userAuthorized && (userInSiteManager || globalInCMSDesk || settingEnabledForSite);
        bool allowSite = ConfigurationHelper.AuthorizedReadConfiguration(SiteID > 0 ? SiteID : CMSContext.CurrentSiteID, false);

        uniselector.AllowAll = AllowAllItem;

        if (DisplayAll || DisplaySiteOrGlobal)
        {
            // Display all site and global statuses
            if (DisplayAll && allowSite && allowGlobal)
            {
                // No WHERE condition required
            }
            // Display current site and global statuses
            else if (DisplaySiteOrGlobal && allowSite && allowGlobal && (SiteID > 0))
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactStatusSiteID IS NULL OR ContactStatusSiteID = " + (SiteID == 0 ? CMSContext.CurrentSiteID : SiteID));
            }
            // Current site
            else if (allowSite && (SiteID > 0))
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactStatusSiteID = " + SiteID);
            }
            // Display global statuses
            else if (allowGlobal)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactStatusSiteID IS NULL ");
            }
            // Don't display anything
            if (String.IsNullOrEmpty(where) && !DisplayAll)
            {
                where = "(1=0)";
            }
        }
        // Display either global or current site statuses
        else
        {
            // Current site
            if ((SiteID > 0) && allowSite)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactStatusSiteID = " + SiteID);
            }
            // Display global statuses
            else if (((SiteID == UniSelector.US_GLOBAL_RECORD) || (SiteID == UniSelector.US_NONE_RECORD)) && allowGlobal)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactStatusSiteID IS NULL ");
            }
            // Don't display anything
            if (String.IsNullOrEmpty(where))
            {
                where = "(1=0)";
            }
        }
        uniselector.WhereCondition = where;
        uniselector.Reload(true);
    }


    /// <summary>
    /// Gets where condition.
    /// </summary>
    public override string GetWhereCondition()
    {
        if (ValidationHelper.GetInteger(uniselector.Value, UniSelector.US_NONE_RECORD) == UniSelector.US_NONE_RECORD)
        {
            return FieldInfo.Name + " IS NULL";
        }
        else
        {
            return FieldInfo.Name + " = " + uniselector.Value;
        }
    }

    #endregion
}