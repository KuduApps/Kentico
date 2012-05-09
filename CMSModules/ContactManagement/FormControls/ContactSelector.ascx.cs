using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_FormControls_ContactSelector : FormEngineUserControl
{
    #region "Variables"

    private int mSiteId = -1;
    private CurrentUserInfo currentUser;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Allows to display contacts only for specified site id. Use 0 for global objects. Default value is current site id.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
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
    /// Gets selected ContactID.
    /// </summary>
    public int ContactID
    {
        get
        {
            EnsureChildControls();
            return ValidationHelper.GetInteger(this.Value, 0);
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
            this.EnsureChildControls();
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


    /// <summary>
    /// Gets or sets if control is placed in sitemanager.
    /// </summary>
    public bool IsSiteManager
    {
        get
        {
            return uniSelector.IsSiteManager;
        }
        set
        {
            uniSelector.IsSiteManager = value;
        }
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
        string where = null;
        currentUser = CMSContext.CurrentUser;
        uniSelector.FilterControl = "~/CMSModules/ContactManagement/FormControls/SearchContactFullName.ascx";
        uniSelector.UseDefaultNameFilter = false;

        bool authorizedSiteContacts = false;
        bool authorizedGlobalContacts = ContactHelper.AuthorizedReadContact(UniSelector.US_GLOBAL_RECORD, false);

        if (this.SiteID > 0)
        {
            authorizedSiteContacts = ContactHelper.AuthorizedReadContact(this.SiteID, false);
        }
        else
        {
            authorizedSiteContacts = ContactHelper.AuthorizedReadContact(CMSContext.CurrentSiteID, false);
        }

        // Filter site objects
        if (this.SiteID > 0)
        {
            if (authorizedSiteContacts)
            {
                where = "(ContactSiteID = " + this.SiteID + " AND ContactMergedWithContactID IS NULL)";
            }
            else
            {
                where = "(1=0)";
            }
        }
        // Filter only global objects
        else if ((this.SiteID == UniSelector.US_GLOBAL_RECORD) || (this.SiteID == 0))
        {
            if (authorizedGlobalContacts)
            {
                where = "(ContactSiteID IS NULL AND ContactGlobalContactID IS NULL)";
            }
            else
            {
                where = "(1=0)";
            }
        }
        // Display current site and global contacts
        else if (this.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD)
        {
            if (authorizedSiteContacts && authorizedGlobalContacts)
            {
                where = "(ContactSiteID IS NULL AND ContactGlobalContactID IS NULL) OR (ContactSiteID = " + CMSContext.CurrentSiteID + " AND ContactMergedWithContactID IS NULL)";
                uniSelector.AddGlobalObjectSuffix = true;
            }
            else if (authorizedGlobalContacts)
            {
                where = "(ContactSiteID IS NULL AND ContactMergedWithContactID IS NULL)";
            }
            else if (authorizedSiteContacts)
            {
                where = "(ContactSiteID = " + CMSContext.CurrentSiteID + " AND ContactMergedWithContactID IS NULL)";
            }
            else
            {
                where = "(1=0)";
            }
        }
        // Display all objects
        else if ((this.SiteID == UniSelector.US_ALL_RECORDS) && currentUser.UserSiteManagerAdmin)
        {
            where = "((ContactSiteID IS NULL AND ContactGlobalContactID IS NULL) OR (ContactSiteID > 0 AND ContactMergedWithContactID IS NULL))";
            uniSelector.AddGlobalObjectSuffix = true;
        }
        // Not enough permissions
        else
        {
            where = "(1=0)";
        }

        uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(this.WhereCondition, where);
        uniSelector.Reload(true);
    }


    /// <summary>
    /// Gets where condition.
    /// </summary>
    public override string GetWhereCondition()
    {
        if (ValidationHelper.GetInteger(uniSelector.Value, UniSelector.US_NONE_RECORD) == UniSelector.US_NONE_RECORD)
        {
            return FieldInfo.Name + " IS NULL";
        }
        else
        {
            return FieldInfo.Name + " = " + uniSelector.Value;
        }
    }

    #endregion
}