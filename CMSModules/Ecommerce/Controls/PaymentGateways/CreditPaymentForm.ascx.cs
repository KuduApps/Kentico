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

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.CMSHelper;


public partial class CMSModules_Ecommerce_Controls_PaymentGateways_CreditPaymentForm : CreditPaymentForm
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize controls
        lblTitle.Text = ResHelper.GetString("CreditPayment.lblTitle");

        if ((this.ShoppingCartControl != null) && 
            (this.ShoppingCartControl.PaymentGatewayProvider != null) &&
            (this.ShoppingCartInfoObj != null))             
        {
            CMSCreditPaymentProvider provider = (CMSCreditPaymentProvider)this.ShoppingCartControl.PaymentGatewayProvider;   

            // If user is not authorized to finish payment
            if (!provider.IsUserAuthorizedToFinishPayment())
            {
                // Display error message
                lblError.Visible = true;
                lblError.Text = provider.ErrorMessage;

                // Disable payment processing
                this.ShoppingCartControl.ButtonNext.Enabled = false;
            }
            else
            {
                // Reloads available credit
                provider.ReloadPaymentData();

                // Displays available credit
                DisplayAvailableCredit();
            }
        }
    }


    /// <summary>
    /// Displays available credit.
    /// </summary>
    public override void DisplayAvailableCredit()
    {
        CMSCreditPaymentProvider cmsCreditProvider = (CMSCreditPaymentProvider)this.ShoppingCartControl.PaymentGatewayProvider;

        if (cmsCreditProvider != null)
        {
            lblCredit.Text = ResHelper.GetString("CreditPayment.lblCredit");

            if ((cmsCreditProvider.MainCurrencyObj != null) && (cmsCreditProvider.OrderCurrencyObj != null))
            {
                // Order currency is different from main currency
                if (cmsCreditProvider.MainCurrencyObj.CurrencyID != cmsCreditProvider.OrderCurrencyObj.CurrencyID)
                {
                    // Set available credit string
                    lblCreditValue.Text = CurrencyInfoProvider.GetFormattedPrice(cmsCreditProvider.AvailableCreditInOrderCurrency, cmsCreditProvider.OrderCurrencyObj);
                    lblCreditValue.Text += " (" + CurrencyInfoProvider.GetFormattedPrice(cmsCreditProvider.AvailableCreditInMainCurrency, cmsCreditProvider.MainCurrencyObj) + ")";
                }
                // Order currency is equal to main currency
                else
                {
                    lblCreditValue.Text = CurrencyInfoProvider.GetFormattedPrice(cmsCreditProvider.AvailableCreditInMainCurrency, cmsCreditProvider.MainCurrencyObj);
                }
            }
        }
    }
}
