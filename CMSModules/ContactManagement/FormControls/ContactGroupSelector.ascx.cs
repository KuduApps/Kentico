using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_FormControls_ContactGroupSelector : FormEngineUserControl
{
    #region "Variables"

    private int mSiteId = -1;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Allows to display accounts only for specified site id. Use 0 for global objects. Default value is current site id.
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
        }
    }


    /// <summary>
    /// Gets selected contact group ID.
    /// </summary>
    public int ContactGroupID
    {
        get
        {
            return ValidationHelper.GetInteger(this.Value, 0);
        }
    }


    /// <summary>
    /// Returns Uniselector.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            EnsureChildControls();
            return uniSelector;
        }
    }


    /// <summary>
    /// Gets image dialog.
    /// </summary>
    public Image ImageDialog
    {
        get
        {
            EnsureChildControls();
            return uniSelector.ImageDialog;
        }
    }


    /// <summary>
    /// Gets hyperlink dialog.
    /// </summary>
    public HyperLink LinkDialog
    {
        get
        {
            EnsureChildControls();
            return uniSelector.LinkDialog;
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
            return uniSelector.Value;
        }
        set
        {
            EnsureChildControls();
            uniSelector.Value = ValidationHelper.GetString(value, "");
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

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        bool authorizedSite = false;
        bool authorizedGlobal = ContactGroupHelper.AuthorizedReadContactGroup(UniSelector.US_GLOBAL_RECORD, false);

        if (this.SiteID > 0)
        {
            authorizedSite = ContactGroupHelper.AuthorizedReadContactGroup(this.SiteID, false);
        }
        else
        {
            authorizedSite = ContactGroupHelper.AuthorizedReadContactGroup(CMSContext.CurrentSiteID, false);
        }

        // Site objects
        if (this.SiteID > 0)
        {
            if (authorizedSite)
            {
                uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(this.WhereCondition, "(ContactGroupSiteID = " + this.SiteID + ")");
            }
            else
            {
                uniSelector.WhereCondition = "(1=0)";
            }
        }
        // Global objects
        else if (this.SiteID == UniSelector.US_GLOBAL_RECORD)
        {
            if (authorizedGlobal)
            {
                uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(this.WhereCondition, "(ContactGroupSiteID IS NULL)");
            }
            else
            {
                uniSelector.WhereCondition = "(1=0)";
            }
        }
        // Global or site objects
        else if (this.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD)
        {
            if (authorizedSite && authorizedGlobal)
            {
                uniSelector.WhereCondition = "(ContactGroupSiteID IS NULL OR ContactGroupSiteID = " + CMSContext.CurrentSiteID + ")";
                uniSelector.AddGlobalObjectSuffix = true;
            }
            else if (authorizedGlobal)
            {
                uniSelector.WhereCondition = "ContactGroupSiteID IS NULL";
            }
            else if (authorizedSite)
            {
                uniSelector.WhereCondition = "ContactGroupSiteID = " + CMSContext.CurrentSiteID;
            }
            else
            {
                uniSelector.WhereCondition = "(1=0)";
            }
        }
        // Display all objects
        else if ((this.SiteID == UniSelector.US_ALL_RECORDS) && CMSContext.CurrentUser.UserSiteManagerAdmin)
        {
            uniSelector.WhereCondition = "(ContactGroupSiteID IS NULL OR ContactGroupSiteID > 0)";
            uniSelector.AddGlobalObjectSuffix = true;
        }
        // Not enough permissions
        else
        {
            uniSelector.WhereCondition = "(1=0)";
        }

        // Initialize selector
        ImageDialog.CssClass = "NewItemImage";
        LinkDialog.CssClass = "NewItemLink";
        uniSelector.IsLiveSite = false;
        uniSelector.ButtonImage = GetImageUrl("/Objects/OM_ContactGroup/add.png");
        uniSelector.Reload(true);
    }


    /// <summary>
    /// Overrides base GetValue method and enables to return UniSelector control with 'uniselector' property name.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    public override object GetValue(string propertyName)
    {
        if (propertyName.Equals("uniselector", StringComparison.InvariantCultureIgnoreCase))
        {
            // Return uniselector control
            return UniSelector;
        }

        // Return other values
        return base.GetValue(propertyName);
    }

    #endregion
}