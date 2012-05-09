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
using CMS.EcommerceProvider;

public partial class CMSModules_Ecommerce_Controls_PaymentGateways_PayPalForm : CMSPaymentGatewayForm
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTitle.Text = ResHelper.GetString("PayPalForm.Title");
        lblInfo.Text = ResHelper.GetString("PayPalForm.InfoMessage");
    }
}
