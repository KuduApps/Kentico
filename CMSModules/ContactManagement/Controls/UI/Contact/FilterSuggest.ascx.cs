using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_ContactManagement_Controls_UI_Contact_FilterSuggest : CMSUserControl
{
    #region "Variables"

    private ContactInfo ci;
    private int mSelectedSiteID;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets current contact.
    /// </summary>
    private ContactInfo CurrentContact
    {
        get
        {
            if ((ci == null) && (CMSContext.EditedObject != null))
            {
                ci = (ContactInfo)CMSContext.EditedObject;
            }
            return ci;
        }
    }


    /// <summary>
    /// Gets value indicating if IP address checkbox is selected.
    /// </summary>
    public bool IPAddressChecked
    {
        get
        {
            return chkIPaddress.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if e-mail address checkbox is selected.
    /// </summary>
    public bool EmailChecked
    {
        get
        {
            return chkEmail.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if phone checkbox is selected.
    /// </summary>
    public bool PhoneChecked
    {
        get
        {
            return chkPhone.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if birthday checkbox is selected.
    /// </summary>
    public bool BirthdayChecked
    {
        get
        {
            return chkBirthDay.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if membership checkbox is selected.
    /// </summary>
    public bool MembershipChecked
    {
        get
        {
            return chkMembership.Checked;
        }
    }


    /// <summary>
    /// Gets value indicating if post address checkbox is selected.
    /// </summary>
    public bool PostAddressChecked
    {
        get
        {
            return chkAddress.Checked;
        }
    }


    /// <summary>
    /// Gets selected site ID.
    /// </summary>
    public int SelectedSiteID
    {
        get
        {
            return mSelectedSiteID;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        chkEmail.Enabled = !String.IsNullOrEmpty(ContactHelper.GetEmailDomain(this.CurrentContact.ContactEmail));
        chkAddress.Enabled = !String.IsNullOrEmpty(this.CurrentContact.ContactAddress1) || !String.IsNullOrEmpty(this.CurrentContact.ContactAddress2) || !String.IsNullOrEmpty(this.CurrentContact.ContactCity) || !String.IsNullOrEmpty(this.CurrentContact.ContactZIP);
        chkBirthDay.Enabled = (this.CurrentContact.ContactBirthday != DateTimeHelper.ZERO_TIME);
        chkPhone.Enabled = !String.IsNullOrEmpty(this.CurrentContact.ContactBusinessPhone) || !String.IsNullOrEmpty(this.CurrentContact.ContactHomePhone) || !String.IsNullOrEmpty(this.CurrentContact.ContactMobilePhone);
        chkMembership.Visible = chkIPaddress.Visible = ci.ContactSiteID != 0;

        // Current contact is global object
        if (ci.ContactSiteID == 0)
        {
            plcSite.Visible = true;
            // Display site selector in site manager
            if (ContactHelper.IsSiteManager)
            {
                siteOrGlobalSelector.Visible = false;
            }
            // Display 'site or global' selector in CMS desk for global objects
            else if (ContactHelper.AuthorizedReadContact(CMSContext.CurrentSiteID, false) && ContactHelper.AuthorizedModifyContact(CMSContext.CurrentSiteID, false))
            {
                siteSelector.Visible = false;
            }
            else
            {
                plcSite.Visible = false;
            }
        }
    }


    /// <summary>
    /// Returns SQL WHERE condition depending on selected checkboxes.
    /// </summary>
    /// <returns>Returns SQL WHERE condition</returns>
    public string GetWhereCondition()
    {
        string where = null;

        // IP address checked
        if (chkIPaddress.Checked)
        {
            where = "ContactID IN (SELECT IP2.IPActiveContactID FROM OM_IP AS IP1 LEFT JOIN OM_IP AS IP2 ON IP1.IPAddress LIKE IP2.IPAddress WHERE IP1.IPID <> IP2.IPID AND IP1.IPActiveContactID = " + this.CurrentContact.ContactID + ")";
        }

        // Email address checked
        if (chkEmail.Checked && !String.IsNullOrEmpty(ContactHelper.GetEmailDomain(this.CurrentContact.ContactEmail)))
        {
            string emailWhere = "(ContactEmail LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactEmail, false) + "%' OR ContactEmail LIKE '%" + SqlHelperClass.GetSafeQueryString(ContactHelper.GetEmailDomain(this.CurrentContact.ContactEmail)) + "%')";
            where = SqlHelperClass.AddWhereCondition(where, emailWhere);
        }

        // Address checked
        if (chkAddress.Checked)
        {
            string addressWhere = null;
            if (!String.IsNullOrEmpty(this.CurrentContact.ContactAddress1))
            {
                addressWhere = SqlHelperClass.AddWhereCondition(addressWhere, "ContactAddress1 LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactAddress1, false) + "%'", "OR");
            }
            if (!String.IsNullOrEmpty(this.CurrentContact.ContactAddress2))
            {
                addressWhere = SqlHelperClass.AddWhereCondition(addressWhere, "ContactAddress2 LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactAddress2, false) + "%'", "OR");
            }
            if (!String.IsNullOrEmpty(this.CurrentContact.ContactCity))
            {
                addressWhere = SqlHelperClass.AddWhereCondition(addressWhere, "ContactCity LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactCity, false) + "%'", "OR");
            }
            if (!String.IsNullOrEmpty(this.CurrentContact.ContactZIP))
            {
                addressWhere = SqlHelperClass.AddWhereCondition(addressWhere, "ContactZIP LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactZIP, false) + "%'", "OR");
            }

            if (!String.IsNullOrEmpty(addressWhere))
            {
                where = SqlHelperClass.AddWhereCondition(where, "(" + addressWhere + ")");
            }
        }

        // Birthday checked
        if (chkBirthDay.Checked && (this.CurrentContact.ContactBirthday != DateTimeHelper.ZERO_TIME))
        {
            where = SqlHelperClass.AddWhereCondition(where, "ContactBirthDay = '" + FormHelper.GetDateTimeValueInDBCulture(this.CurrentContact.ContactBirthday.ToString()) + "'");
        }

        // Phone checked
        if (chkPhone.Checked)
        {
            string phoneWhere = null;
            if (!String.IsNullOrEmpty(this.CurrentContact.ContactBusinessPhone))
            {
                phoneWhere = SqlHelperClass.AddWhereCondition(phoneWhere, "ContactBusinessPhone LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactBusinessPhone, false) + "%'");
            }
            if (!String.IsNullOrEmpty(this.CurrentContact.ContactHomePhone))
            {
                phoneWhere = SqlHelperClass.AddWhereCondition(phoneWhere, "ContactHomePhone LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactHomePhone, false) + "%'", "OR");
            }
            if (!String.IsNullOrEmpty(this.CurrentContact.ContactMobilePhone))
            {
                phoneWhere = SqlHelperClass.AddWhereCondition(phoneWhere, "ContactMobilePhone LIKE '%" + SqlHelperClass.GetSafeQueryString(this.CurrentContact.ContactMobilePhone, false) + "%'", "OR");
            }

            if (!String.IsNullOrEmpty(phoneWhere))
            {
                where = SqlHelperClass.AddWhereCondition(where, "(" + phoneWhere + ")");
            }
        }

        // Membership checked
        if (chkMembership.Checked)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ContactID IN (SELECT Member2.ActiveContactID FROM OM_Membership AS Member1 LEFT JOIN OM_Membership AS Member2 ON Member1.RelatedID = Member2.RelatedID AND Member1.MemberType LIKE Member2.MemberType WHERE Member1.MembershipID <> Member2.MembershipID AND Member1.ActiveContactID = " + this.CurrentContact.ContactID + ")");
        }

        if ((!chkAddress.Checked && !chkBirthDay.Checked && !chkEmail.Checked && !chkIPaddress.Checked && !chkPhone.Checked && !chkMembership.Checked) || (String.IsNullOrEmpty(where)))
        {
            return "(1 = 0)";
        }

        // Filter out records related to current contact
        where = SqlHelperClass.AddWhereCondition(where, "ContactID <> " + this.CurrentContact.ContactID);
        // Filter out merged records - merging into global contact
        if (CurrentContact.ContactSiteID == 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "(ContactMergedWithContactID IS NULL AND ContactGlobalContactID IS NULL AND ContactSiteID > 0) OR (ContactGlobalContactID IS NULL AND ContactSiteID IS NULL)");
        }
        // Merging into site contact
        else
        {
            where = SqlHelperClass.AddWhereCondition(where, "(ContactMergedWithContactID IS NULL AND ContactSiteID > 0)");
        }

        // For global object use siteselector's value
        if (plcSite.Visible)
        {
            mSelectedSiteID = UniSelector.US_ALL_RECORDS;
            if (siteSelector.Visible)
            {
                mSelectedSiteID = siteSelector.SiteID;
            }
            else if (siteOrGlobalSelector.Visible)
            {
                mSelectedSiteID = siteOrGlobalSelector.SiteID;
            }

            // Only global objects
            if (mSelectedSiteID == UniSelector.US_GLOBAL_RECORD)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID IS NULL");
            }
            // Global and site objects
            else if (mSelectedSiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID IS NULL OR ContactSiteID = " + CMSContext.CurrentSiteID);
            }
            // Site objects
            else if (mSelectedSiteID != UniSelector.US_ALL_RECORDS)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID = " + mSelectedSiteID);
            }
        }
        // Filter out contacts from different sites
        else
        {
            // Site contacts only
            if (this.CurrentContact.ContactSiteID > 0)
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID = " + this.CurrentContact.ContactSiteID);
            }
            // Global contacts only
            else
            {
                where = SqlHelperClass.AddWhereCondition(where, "ContactSiteID IS NULL");
            }
        }

        return where;
    }

    #endregion
}