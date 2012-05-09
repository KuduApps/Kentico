using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_FormControls_SelectProductType : FormEngineUserControl
{
    #region "Variables"

    private bool mAllowAll = true;
    private bool mAllowStandardProduct = true;
    private bool mAllowMembership = true;
    private bool mAllowEproduct = true;
    private bool mAllowDonation = true;
    private bool mAllowBundle = true;
    private bool mAllowText = false;
    
    #endregion


    #region "Properties"

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
            base.Enabled = value;
            this.drpProductType.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating whether a postback to the server automatically occurs when the selection is changed by the user.
    /// </summary>
    public bool AutoPostBack
    {
        get
        {
            return this.drpProductType.AutoPostBack;
        }
        set
        {
            this.drpProductType.AutoPostBack = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.drpProductType.SelectedItem.Value;
        }
        set
        {
            ListItem item = this.drpProductType.Items.FindByValue((string)value);

            if (item != null)
            {
                item.Selected = true;

                // Fire selection changed event
                if (this.OnSelectionChanged != null)
                {
                    this.OnSelectionChanged(this, null);
                }
            }
        }
    }


    /// <summary>
    /// Indicates if all option is available for selection.
    /// </summary>
    public bool AllowAll
    {
        get
        {
            return this.mAllowAll;
        }
        set
        {
            this.mAllowAll = value;
        }
    }


    /// <summary>
    /// Indicates if standard product type option is available for selection.
    /// </summary>
    public bool AllowStandardProduct
    {
        get
        {
            return this.mAllowStandardProduct;
        }
        set
        {
            this.mAllowStandardProduct = value;
        }
    }


    /// <summary>
    /// Indicates if membership product type option is available for selection.
    /// </summary>
    public bool AllowMembership
    {
        get
        {
            return this.mAllowMembership;
        }
        set
        {
            this.mAllowMembership = value;
        }
    }


    /// <summary>
    /// Indicates if e-product product type option is available for selection.
    /// </summary>
    public bool AllowEproduct
    {
        get
        {
            return this.mAllowEproduct;
        }
        set
        {
            this.mAllowEproduct = value;
        }
    }


    /// <summary>
    /// Indicates if donation product type option is available for selection.
    /// </summary>
    public bool AllowDonation
    {
        get
        {
            return this.mAllowDonation;
        }
        set
        {
            this.mAllowDonation = value;
        }
    }


    /// <summary>
    /// Indicates if bundle product type option is available for selection.
    /// </summary>
    public bool AllowBundle
    {
        get
        {
            return this.mAllowBundle;
        }
        set
        {
            this.mAllowBundle = value;
        }
    }


    /// <summary>
    /// Indicates if text product type option is available for selection.
    /// </summary>
    public bool AllowText
    {
        get
        {
            return mAllowText;
        }
        set
        {
            mAllowText = value;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Selection changed event.
    /// </summary>
    public event EventHandler OnSelectionChanged;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!RequestHelper.IsPostBack())
        {
            this.Initialize();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Initializes control.
    /// </summary>
    public void Initialize()
    {
        // Clear current options
        this.drpProductType.Items.Clear();

        // Add standar product option when allowed
        if (this.AllowStandardProduct)
        {
            this.AddProductType("com.producttype.product", SKUProductTypeEnum.Product);
        }

        // Add membership option when membership feature is available and membership option is allowed
        if (LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.Membership) && this.AllowMembership)
        {
            this.AddProductType("com.producttype.membership", SKUProductTypeEnum.Membership);
        }

        // Add e-product option when allowed
        if (this.AllowEproduct)
        {
            this.AddProductType("com.producttype.eproduct", SKUProductTypeEnum.EProduct);
        }

        // Add donation option when allowed
        if (this.AllowDonation)
        {
            this.AddProductType("com.producttype.donation", SKUProductTypeEnum.Donation);
        }

        // Add bundle option when allowed
        if (this.AllowBundle)
        {
            this.AddProductType("com.producttype.bundle", SKUProductTypeEnum.Bundle);
        }

        // Add text option when allowed
        if (this.AllowText)
        {
            this.AddProductType("com.producttype.text", SKUProductTypeEnum.Text);
        }

        // Add all option if required
        if (this.AllowAll)
        {
            this.drpProductType.Items.Insert(0, new ListItem(this.GetString("general.selectall"), "ALL"));
        }
    }


    protected void drpProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Fire selection changed event when product type dropdown selection occurs
        if (this.OnSelectionChanged != null)
        {
            this.OnSelectionChanged(this, null);
        }
    }


    /// <summary>
    /// Adds item to product type dropdown list.
    /// </summary>
    /// <param name="productTypeName">Product type name resource string</param>
    /// <param name="productType">Product type</param>
    private void AddProductType(string productTypeName, SKUProductTypeEnum productType)
    {
        this.drpProductType.Items.Add(new ListItem(this.GetString(productTypeName), SKUInfoProvider.GetSKUProductTypeString(productType)));
    }

    #endregion
}
