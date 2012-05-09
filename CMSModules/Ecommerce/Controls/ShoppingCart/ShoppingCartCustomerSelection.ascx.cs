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

using CMS.LicenseProvider;
using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartCustomerSelection : ShoppingCartStep
{
    protected void Page_Load(object sender, EventArgs e)
    {

        // Check feature
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Ecommerce);
        }

        // Mark previously selected customer
        if ((!this.ShoppingCartControl.IsCurrentStepPostBack) &&
            (this.ShoppingCartInfoObj.ShoppingCartCustomerID > 0))
        {
            customerSelector.CustomerID = ShoppingCartInfoObj.ShoppingCartCustomerID;
        }

        // Control initialization
        lblTitle.Text = GetString("ShoppingCart.SelectCustomer");
        //this.TitleText = GetString("Order_New.CustomerSelection.Title");
        this.ShoppingCartControl.ButtonBack.Visible = false;
        this.customerSelector.IsLiveSite = this.IsLiveSite;

    }


    /// <summary>
    /// Validate form.
    /// </summary>
    public override bool IsValid()
    {
        if (customerSelector.CustomerID > 0)
        {
            return true;
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = GetString("Order_New.CustomerSelection.NoCustomerSelected");
            return false;
        }
    }


    /// <summary>
    /// Process form data.
    /// </summary>
    public override bool ProcessStep()
    {
        try
        {
            // Get the customer
            int customerId = customerSelector.CustomerID;
            if (customerId > 0)
            {
                CustomerInfo ci = CustomerInfoProvider.GetCustomerInfo(customerId);
                if (ci != null)
                {
                    UserInfo ui = null;
                    if (ci.CustomerUserID > 0)
                    {
                        ui = UserInfoProvider.GetUserInfo(ci.CustomerUserID);
                    }
                    if (ui == null)
                    {
                        ui = CMSContext.GlobalPublicUser;
                    }

                    this.ShoppingCartInfoObj.UserInfoObj = ui;
                    this.ShoppingCartInfoObj.ShoppingCartCustomerID = customerId;

                    return true;
                }
            }

            lblError.Visible = true;
            lblError.Text = GetString("ShoppingCartSelectCustomer.ErrorSelect");
            return false;
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
            return false;
        }
    }
}
