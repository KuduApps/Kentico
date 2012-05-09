using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.LicenseProvider;

public partial class CMSModules_Membership_FormControls_Membership_SelectMembership : FormEngineUserControl
{
    #region "Variables"

    private int mSiteId = -1;
    private bool mUseCodeNameForSelection = true;
    private bool mUseGUIDForSelection = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets membership ID.
    /// </summary>
    public int MembershipID
    {
        get
        {
            this.EnsureChildControls();

            // Use GUID
            if (this.UseGUIDForSelection)
            {
                Guid guid = ValidationHelper.GetGuid(this.uniSelectorElem.Value, Guid.Empty);

                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(guid);

                return (mi != null) ? mi.MembershipID : 0;
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                string name = ValidationHelper.GetString(this.uniSelectorElem.Value, String.Empty);

                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(name, SiteInfoProvider.GetSiteName(this.SiteID));

                return (mi != null) ? mi.MembershipID : 0;
            }
            // Use ID
            else
            {
                return ValidationHelper.GetInteger(this.uniSelectorElem.Value, 0);
            }
        }
        set
        {
            this.EnsureChildControls();

            // Use GUID
            if (this.UseGUIDForSelection)
            {
                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(value);

                if (mi != null)
                {
                    this.uniSelectorElem.Value = mi.MembershipGUID;
                }
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(value);

                if (mi != null)
                {
                    this.uniSelectorElem.Value = mi.MembershipName;
                }
            }
            // Use ID
            else
            {
                this.uniSelectorElem.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets membership GUID.
    /// </summary>
    public Guid MembershipGUID
    {
        get
        {
            this.EnsureChildControls();

            // Use GUID
            if (this.UseGUIDForSelection)
            {
                return ValidationHelper.GetGuid(this.uniSelectorElem.Value, Guid.Empty);
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                string name = ValidationHelper.GetString(this.uniSelectorElem.Value, String.Empty);

                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(name, SiteInfoProvider.GetSiteName(this.SiteID));

                return (mi != null) ? mi.MembershipGUID : Guid.Empty;
            }
            // Use ID
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelectorElem.Value, 0);

                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(id);

                return (mi != null) ? mi.MembershipGUID : Guid.Empty;
            }
        }
        set
        {
            this.EnsureChildControls();

            // Use GUID
            if (this.UseGUIDForSelection)
            {
                this.uniSelectorElem.Value = value;
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(value);

                if (mi != null)
                {
                    this.uniSelectorElem.Value = mi.MembershipName;
                }
            }
            // Use ID
            else
            {
                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(value);

                if (mi != null)
                {
                    this.uniSelectorElem.Value = mi.MembershipID;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets membership code name.
    /// </summary>
    public string MembershipName
    {
        get
        {
            this.EnsureChildControls();

            // Use GUID
            if (this.UseGUIDForSelection)
            {
                Guid guid = ValidationHelper.GetGuid(this.uniSelectorElem.Value, Guid.Empty);

                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(guid);

                return (mi != null) ? mi.MembershipName : String.Empty;
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                return ValidationHelper.GetString(this.uniSelectorElem.Value, String.Empty);
            }
            // Use ID
            else
            {
                int id = ValidationHelper.GetInteger(this.uniSelectorElem.Value, 0);

                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(id);

                return (mi != null) ? mi.MembershipName : String.Empty;
            }
        }
        set
        {
            this.EnsureChildControls();

            // Use GUID
            if (this.UseGUIDForSelection)
            {
                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(value, SiteInfoProvider.GetSiteName(this.SiteID));

                if (mi != null)
                {
                    this.uniSelectorElem.Value = mi.MembershipGUID;
                }
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                this.uniSelectorElem.Value = value;
            }
            // Use ID
            else
            {
                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(value, SiteInfoProvider.GetSiteName(this.SiteID));

                if (mi != null)
                {
                    this.uniSelectorElem.Value = mi.MembershipID;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Use GUID
            if (this.UseGUIDForSelection)
            {
                return this.MembershipGUID;
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                return this.MembershipName;
            }
            // Use ID
            else
            {
                return this.MembershipID;
            }
        }
        set
        {
            // Use GUID
            if (this.UseGUIDForSelection)
            {
                this.MembershipGUID = ValidationHelper.GetGuid(value, Guid.Empty);
            }
            // Use code name
            else if (this.UseCodeNameForSelection)
            {
                this.MembershipName = ValidationHelper.GetString(value, String.Empty);
            }
            // Use ID
            else
            {
                this.MembershipID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets client ID of the dropdown list.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            this.EnsureChildControls();

            return this.uniSelectorElem.DropDownSingleSelect.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add "none" option to dropdown list.
    /// </summary>
    public bool AddNoneRecord
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets enabled state of the control.
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

            if (this.uniSelectorElem != null)
            {
                this.uniSelectorElem.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Indicates if membership GUID is used for selection.
    /// </summary>
    public bool UseGUIDForSelection
    {
        get
        {
            return this.mUseGUIDForSelection;
        }
        set
        {
            this.mUseGUIDForSelection = value;

            // If setting to true
            if (value)
            {
                // Set other options to false
                this.mUseCodeNameForSelection = false;
            }
        }
    }


    /// <summary>
    /// Indicates if membership code name is used for selection.
    /// </summary>
    public bool UseCodeNameForSelection
    {
        get
        {
            return this.mUseCodeNameForSelection;
        }
        set
        {
            this.mUseCodeNameForSelection = value;

            // If setting to true
            if (value)
            {
                // Set other options to false
                this.mUseGUIDForSelection = !value;
            }
        }
    }


    /// <summary>
    /// Allows to display memberships only for specified site ID. Use 0 for global memberships. Default value is current site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (this.mSiteId == -1)
            {
                this.mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.Membership))
        {
            this.StopProcessing = true;
            return;
        }

        this.Initialize();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Initliazes the control.
    /// </summary>
    protected void Initialize()
    {
        this.uniSelectorElem.IsLiveSite = this.IsLiveSite;
        this.uniSelectorElem.AllowEmpty = this.AddNoneRecord;

        // Use GUID
        if (this.UseGUIDForSelection)
        {
            this.uniSelectorElem.ReturnColumnName = "MembershipGUID";
        }
        // Use code name
        else if (this.UseCodeNameForSelection)
        {
            this.uniSelectorElem.ReturnColumnName = "MembershipName";
        }
        // Use ID
        else
        {
            this.uniSelectorElem.ReturnColumnName = "MembershipID";
        }

        // Set up where condition
        string where = "";

        if (this.SiteID > 0)
        {
            // Add site items
            where = SqlHelperClass.AddWhereCondition(where, String.Format("(MembershipSiteID = {0})", this.SiteID));
        }
        else
        {
            // Add global items
            where = SqlHelperClass.AddWhereCondition(where, "(MembershipSiteID IS NULL)");
        }

        // Ensure selected item
        if (this.MembershipID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("(MembershipID = {0})", this.MembershipID), "OR");
        }

        // Set where condition
        this.uniSelectorElem.WhereCondition = where;
    }

    #endregion
}
