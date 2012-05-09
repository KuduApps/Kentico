using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormEngine;
using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSModules_Ecommerce_FormControls_SelectTaxAddressType : FormEngineUserControl
{
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
            base.Enabled = value;
            this.rblAddresType.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return string.IsNullOrEmpty(this.rblAddresType.SelectedValue) ? "BillingAddress" : this.rblAddresType.SelectedValue;
        }
        set
        {
            this.EnsureChildControls();
            this.rblAddresType.SelectedValue = ValidationHelper.GetString(value, "BillingAddress");
        }
    }


    /// <summary>
    /// Returns ClientID of the RadioButtonList with tax options..
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.rblAddresType.ClientID;
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Loads the child controls at run-time.
    /// </summary>
    private void ReloadData()
    {
        if ((this.rblAddresType.Items == null) || (this.rblAddresType.Items.Count <= 0))
        {
            this.rblAddresType.Items.Add(new ListItem(GetString("com.BillingAddress"), "BillingAddress"));
            this.rblAddresType.Items.Add(new ListItem(GetString("com.ShippingAddress"), "ShippingAddress"));
        }
        this.rblAddresType.SelectedValue = this.Value.ToString();
    }

    #endregion
}
