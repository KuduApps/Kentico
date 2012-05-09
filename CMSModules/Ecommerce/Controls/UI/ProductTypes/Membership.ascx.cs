using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Controls_UI_ProductTypes_Membership : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// Membership ID.
    /// </summary>
    public int MembershipID
    {
        get
        {
            return ValidationHelper.GetInteger(this.selectMembershipElem.MembershipID, 0);
        }
        set
        {
            this.selectMembershipElem.MembershipID = value;
        }
    }


    /// <summary>
    /// Membership GUID.
    /// </summary>
    public Guid MembershipGUID
    {
        get
        {
            return ValidationHelper.GetGuid(this.selectMembershipElem.MembershipGUID, Guid.Empty);
        }
        set
        {
            this.selectMembershipElem.MembershipGUID = value;
        }
    }


    /// <summary>
    /// Membership validity type.
    /// </summary>
    public ValidityEnum MembershipValidity
    {
        get
        {
            return this.selectValidityElem.Validity;
        }
        set
        {
            this.selectValidityElem.Validity = value;
        }
    }


    /// <summary>
    /// Membership valid for multiplier.
    /// </summary>
    public int MembershipValidFor
    {
        get
        {
            return this.selectValidityElem.ValidFor;
        }
        set
        {
            this.selectValidityElem.ValidFor = value;
        }
    }


    /// <summary>
    /// Membership valid until date and time.
    /// </summary>
    public DateTime MembershipValidUntil
    {
        get
        {
            // If valid until date not set
            if (this.selectValidityElem.ValidUntil.Equals(DataHelper.DATETIME_NOT_SELECTED))
            {
                // Return default value
                return DataHelper.DATETIME_NOT_SELECTED;
            }
            else
            {
                // Return with midnight time
                return new DateTime(this.selectValidityElem.ValidUntil.Year, this.selectValidityElem.ValidUntil.Month, this.selectValidityElem.ValidUntil.Day, 23, 59, 59);
            }
        }
        set
        {
            this.selectValidityElem.ValidUntil = value;
        }
    }


    /// <summary>
    /// Error message.
    /// </summary>
    public string ErrorMessage
    {
        get;
        set;
    }


    /// <summary>
    /// Site ID.
    /// </summary>
    public int SiteID
    {
        get;
        set;
    }

    #endregion


    #region "Events"

    public event EventHandler OnValidityChanged;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Pass product site ID to selector
        this.selectMembershipElem.SiteID = this.SiteID;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Validates form and returns string with error messages.
    /// </summary>
    public string Validate()
    {
        this.ErrorMessage = String.Empty;

        // Validate membership group
        if (String.IsNullOrEmpty(this.ErrorMessage) && (this.selectMembershipElem.MembershipID == 0))
        {
            this.ErrorMessage = this.GetString("com.membership.nomembershipselectederror");
        }

        // Validate membership validity
        if (String.IsNullOrEmpty(this.ErrorMessage))
        {
            this.ErrorMessage = this.selectValidityElem.Validate();
        }

        return this.ErrorMessage;
    }


    protected void selectValidityElem_OnValidityChanged(object sender, EventArgs e)
    {
        if (this.OnValidityChanged != null)
        {
            this.OnValidityChanged(this, null);
        }
    }

    #endregion
}
