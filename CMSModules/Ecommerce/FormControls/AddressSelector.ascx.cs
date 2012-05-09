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
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_AddressSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Variables"

    private bool mAddNewRecord = false;
    private bool mAddNoneRecord = true;
    private bool mShowBilling = true;
    private bool mShowShipping = true;
    private bool mShowCompany = true;
    private bool mShowOnlyEnabled = true;
    private bool mShowAll = false;
    private bool mRenderInline = false;
    private int mCustomerId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the customer the addresses of which should be displayed.
    /// </summary>
    public int CustomerID
    {
        get
        {
            return this.mCustomerId;
        }
        set
        {
            this.mCustomerId = value;
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
    /// Indicates whether to display all addresses - if true all other settings (ShowOnlyEnabled, ShowBilling, ShowShipping) are ignored.
    /// </summary>
    public bool ShowAll
    {
        get
        {
            return this.mShowAll;
        }
        set
        {
            this.mShowAll = value;
        }
    }


    /// <summary>
    /// Indicates whether to display only enabled addresses.
    /// </summary>
    public bool ShowOnlyEnabled
    {
        get
        {
            return this.mShowOnlyEnabled;
        }
        set
        {
            this.mShowOnlyEnabled = value;
        }
    }


    /// <summary>
    /// Indicates whether to display billing addresses.
    /// </summary>
    public bool ShowBilling
    {
        get
        {
            return this.mShowBilling;
        }
        set
        {
            this.mShowBilling = value;
        }
    }

    /// <summary>
    /// Indicates whether to shipping billing addresses.
    /// </summary>
    public bool ShowShipping
    {
        get
        {
            return this.mShowShipping;
        }
        set
        {
            this.mShowShipping = value;
        }
    }


    /// <summary>
    /// Indicates whether to display company addresses.
    /// </summary>
    public bool ShowCompany
    {
        get
        {
            return this.mShowCompany;
        }
        set
        {
            this.mShowCompany = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.AddressID;
        }
        set
        {
            this.AddressID = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Gets or sets the Address ID.
    /// </summary>
    public int AddressID
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
            this.uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add new item record to the dropdownlist.
    /// </summary>
    public bool AddNewRecord
    {
        get
        {
            return mAddNewRecord;
        }
        set
        {
            mAddNewRecord = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add none item record to the dropdownlist.
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
    /// Returns inner DropDown control.
    /// </summary>
    public DropDownList DropDownSingleSelect
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
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        if (this.RenderInline)
        {
            this.pnlUpdate.RenderMode = UpdatePanelRenderMode.Inline;
        }

        this.uniSelector.EnabledColumnName = "AddressEnabled";
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.AllowEmpty = this.AddNoneRecord;

        string where = "AddressCustomerID = " + this.CustomerID;

        if (!this.ShowAll)
        {
            if (this.ShowOnlyEnabled)
            {
                where += " AND AddressEnabled = 1";
            }
            if (!this.ShowBilling || !this.ShowShipping)
            {
                if (this.ShowBilling)
                {
                    where += " AND AddressIsBilling = 1";
                }
                if (this.ShowShipping)
                {
                    where += " AND AddressIsShipping = 1";
                }
                if (this.ShowCompany)
                {
                    where += " AND AddressIsCompany = 1";
                }
            }
        }

        // Include selected value
        if (AddressID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "AddressID = " + AddressID, "OR");
        }

        this.uniSelector.WhereCondition = where;

        if (this.AddNewRecord)
        {
            this.uniSelector.SpecialFields = new string[,] { { GetString("shoppingcartorderaddresses.newaddress"), "0" } };
        }
    }
}
