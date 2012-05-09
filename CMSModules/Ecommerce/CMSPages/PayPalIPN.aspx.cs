using System;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.EcommerceProvider;
using CMS.EventLog;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_CMSPages_PayPalIPN : CMSPage
{
    private int orderId = 0;
    private string transactionId = "";
    private string paymentStatus = "";
    private string orderCulture = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get order id
        orderId = ValidationHelper.GetInteger(Request.Form["invoice"], 0);

        // Get transaction id
        transactionId = ValidationHelper.GetString(Request.Form["txn_id"], "");

        // Get payment status
        paymentStatus = ValidationHelper.GetString(Request.Form["payment_status"], "");

        // Get notification culture stored in custom field
        orderCulture = ValidationHelper.GetString(Request.Form["custom"], "");

        CMSPayPalProvider payPalProvider = null;
        string errorMessage = "";

        // Get paypal provider
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);
        if (oi != null)
        {
            try
            {
                payPalProvider = (CMSPayPalProvider)CMSPaymentGatewayProvider.GetPaymentGatewayProvider(oi.OrderPaymentOptionID);
                payPalProvider.OrderId = orderId;
            }
            catch (Exception ex)
            {
                // Log exception
                errorMessage = EventLogProvider.GetExceptionLogMessage(ex);
                LogEvent(errorMessage, "Payment_Provider_Not_Found");
                return;
            }
        }
        else
        {
            // Order not found
            errorMessage = string.Format(GetString("PaymentGatewayProvider.ordernotfound"), orderId);
            LogEvent(errorMessage, "Order_Not_Found");
            return;
        }

        PayPalPaymentResultInfo paypalResult = (PayPalPaymentResultInfo)(payPalProvider.PaymentResult);

        // Confirm payment and get PayPal payment gateway response
        string response = payPalProvider.ConfirmPayment(Request.Form);

        // Set payment as verified / not verified
        paypalResult.PayPalPaymentVerified = (response.ToLower() == "verified");

        // Set transaction ID
        paypalResult.PaymentTransactionID = transactionId;

        // Set payment status (take received status seriously only if payment was verified)
        paypalResult.PayPalPaymentStatus = PayPalPaymentResultInfo.GetPayPalPaymentStatus(paypalResult.PayPalPaymentVerified ? paymentStatus : "failed");

        // Set payment as completed / not completed
        switch (paypalResult.PayPalPaymentStatus)
        {
            case PayPalPaymentStatusEnum.CanceledReversal:
            case PayPalPaymentStatusEnum.Completed:
            case PayPalPaymentStatusEnum.Processed:
                paypalResult.PaymentIsCompleted = true;
                break;

            default:
                paypalResult.PaymentIsCompleted = false;
                break;
        }

        // Compare payment data to order data
        string error = payPalProvider.ComparePaymentDataToOrderData(Request.Form);
        if (error != "")
        {
            paypalResult.PaymentIsCompleted = false;
            paypalResult.PayPalPaymentVerified = false;
            paypalResult.PaymentDescription = error;
        }

        // Order culture
        payPalProvider.ShoppingCartInfoObj.ShoppingCartCulture = orderCulture;

        // Update order payment result in database
        errorMessage = payPalProvider.UpdateOrderPaymentResult();
        if (errorMessage != "")
        {
            // Log the event
            errorMessage += error;
            LogEvent(errorMessage, "Order_Payment_Result_Update");
        }
    }


    /// <summary>
    /// Logs error.
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="eventCode">Event code</param>
    protected void LogEvent(string message, string eventCode)
    {
        try
        {
            // Add some additional information to the error message            
            string info = "ORDER ID: " + orderId + "\r\n";
            info += "TRANSACTION ID: " + transactionId + "\r\n";
            info += "PAYMENT STATUS: " + paymentStatus + "\r\n";

            message = info + message;

            EventLogProvider evenLog = new EventLogProvider();
            evenLog.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "PayPal IPN", eventCode, 0, "", 0, "", "", message, 0, "");
        }
        catch
        {
            // Unable to log the event
        }
    }
}

