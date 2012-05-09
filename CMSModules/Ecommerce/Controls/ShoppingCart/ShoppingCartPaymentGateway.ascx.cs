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
using CMS.EcommerceProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartPaymentGateway : ShoppingCartStep
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // No payment provider loaded -> skip payment
        if (this.ShoppingCartControl.PaymentGatewayProvider == null)
        {
            // Clean current order payment result when editing existing order and payment was skipped
            if ((this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems) &&
                !this.ShoppingCartControl.IsCurrentStepPostBack)
            {
                CleanUpOrderPaymentResult();
            }

            // Raise payment skipped
            this.ShoppingCartControl.RaisePaymentSkippedEvent();

            // When on the live site
            if (!this.ShoppingCartControl.IsInternalOrder)
            {
                // Get Url the user should be redirected to
                string url = this.ShoppingCartControl.GetRedirectAfterPurchaseUrl();
                
                // Remove shopping cart data from database and from session
                this.ShoppingCartControl.CleanUpShoppingCart();

                if (!string.IsNullOrEmpty(url))
                {
                    URLHelper.Redirect(url);
                }
                else
                {
                    URLHelper.Redirect(this.ShoppingCartControl.PreviousPageUrl);
                }
            }
            return;
        }
        else if (this.ShoppingCartInfoObj != null)
        {
            LoadData();
        }

        lblTitle.Text = GetString("PaymentSummary.Title");
        lblTotalPrice.Text = GetString("PaymentSummary.TotalPrice");
        lblOrderId.Text = GetString("PaymentSummary.OrderId");
        lblPayment.Text = GetString("PaymentSummary.Payment");
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {

        // Set buttons properties
        if (!(this.ShoppingCartControl.PaymentGatewayProvider.IsPaymentCompleted) ||
            (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems))
        {
            // Show 'Skip payment' button
            this.ShoppingCartControl.ButtonBack.CssClass = "LongButton";
            this.ShoppingCartControl.ButtonBack.Text = GetString("ShoppingCart.PaymentGateway.SkipPayment");
            // Show 'Finish payment' button
            this.ShoppingCartControl.ButtonNext.CssClass = "LongButton";
            this.ShoppingCartControl.ButtonNext.Text = GetString("ShoppingCart.PaymentGateway.FinishPayment");
        }
    }


    public override void ButtonNextClickAction()
    {
        // Standart action - Process payment
        base.ButtonNextClickAction();

        if (this.ShoppingCartControl.PaymentGatewayProvider.IsPaymentCompleted)
        {
            // Remove current shopping cart data from session and from database
            this.ShoppingCartControl.CleanUpShoppingCart();

            // Live site
            if (!this.ShoppingCartControl.IsInternalOrder)
            {
                string url = "";
                if (this.ShoppingCartControl.RedirectAfterPurchase != "")
                {
                    url = CMSContext.GetUrl(this.ShoppingCartControl.RedirectAfterPurchase);
                }
                else
                {
                    url = CMSContext.GetUrl("/");
                }

                URLHelper.Redirect(url);
            }
        }
    }


    public override void ButtonBackClickAction()
    {
        // Clean current order payment result when editing existing order and payment was skipped
        //if (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems)
        //{
        //    CleanUpOrderPaymentResult();
        //}

        // Payment was skipped
        this.ShoppingCartControl.RaisePaymentSkippedEvent();

        // Remove current shopping cart data from session and from database
        this.ShoppingCartControl.CleanUpShoppingCart();

        // Live site - skip payment
        if (!this.ShoppingCartControl.IsInternalOrder)
        {
            string url = "";
            if (this.ShoppingCartControl.RedirectAfterPurchase != "")
            {
                url = CMSContext.GetUrl(this.ShoppingCartControl.RedirectAfterPurchase);
            }
            else
            {
                url = CMSContext.GetUrl("/");
            }

            URLHelper.Redirect(url);
        }
    }


    public override bool IsValid()
    {
        return ((this.ShoppingCartControl.PaymentGatewayProvider != null) && (this.ShoppingCartControl.PaymentGatewayProvider.ValidateCustomData() == ""));
    }


    public override bool ProcessStep()
    {
        if (this.ShoppingCartControl.PaymentGatewayProvider != null)
        {
            // Proces current step payment gateway custom data
            this.ShoppingCartControl.PaymentGatewayProvider.ProcessCustomData();

            // Skip payment when already payed except when editing existing order
            if ((!this.ShoppingCartControl.PaymentGatewayProvider.IsPaymentCompleted) ||
                (this.ShoppingCartControl.CheckoutProcessType == CheckoutProcessEnum.CMSDeskOrderItems))
            {
                // Process payment 
                this.ShoppingCartControl.PaymentGatewayProvider.ProcessPayment();
            }

            // Show info message
            if (this.ShoppingCartControl.PaymentGatewayProvider.InfoMessage != "")
            {
                lblInfo.Visible = true;
                lblInfo.Text = this.ShoppingCartControl.PaymentGatewayProvider.InfoMessage;
            }

            // Show error message
            if (this.ShoppingCartControl.PaymentGatewayProvider.ErrorMessage != "")
            {
                lblError.Visible = true;
                lblError.Text = this.ShoppingCartControl.PaymentGatewayProvider.ErrorMessage;
                return false;
            }

            if (this.ShoppingCartControl.PaymentGatewayProvider.IsPaymentCompleted)
            {
                // Raise payment completed event
                this.ShoppingCartControl.RaisePaymentCompletedEvent();
                return true;
            }
        }
        return false;
    }


    private void LoadData()
    {
        // Payment summary
        lblTotalPriceValue.Text = CurrencyInfoProvider.GetFormattedPrice(this.ShoppingCartInfoObj.RoundedTotalPrice, this.ShoppingCartInfoObj.CurrencyInfoObj);
        lblOrderIdValue.Text = Convert.ToString(this.ShoppingCartInfoObj.OrderId);
        if (this.ShoppingCartInfoObj.PaymentOptionInfoObj != null)
        {
            lblPaymentValue.Text = ResHelper.LocalizeString(this.ShoppingCartInfoObj.PaymentOptionInfoObj.PaymentOptionDisplayName);
        }

        // Add payment gateway custom data
        this.ShoppingCartControl.PaymentGatewayProvider.AddCustomData();

        // Show "Order saved" info message
        if (!this.ShoppingCartControl.IsCurrentStepPostBack)
        {
            //if (this.ShoppingCartControl.IsInternalOrder)
            //{
            //    lblInfo.Text = GetString("General.ChangesSaved");
            //}
            //else
            //{
            //    lblInfo.Text = GetString("ShoppingCart.PaymentGateway.OrderSaved");
            //}
            lblInfo.Text = GetString("ShoppingCart.PaymentGateway.OrderSaved");
            lblInfo.Visible = true;
        }
        else
        {
            lblInfo.Text = "";
        }
    }


    /// <summary>
    /// Clean up current order payment result.
    /// </summary>
    private void CleanUpOrderPaymentResult()
    {
        if (this.ShoppingCartInfoObj != null)
        {
            OrderInfo oi = OrderInfoProvider.GetOrderInfo(this.ShoppingCartInfoObj.OrderId);
            if (oi != null)
            {
                oi.OrderPaymentResult = null;
                OrderInfoProvider.SetOrderInfo(oi);
            }
        }
    }
}
