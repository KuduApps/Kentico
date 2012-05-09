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
using System.Threading;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Controls_ShoppingCart_ShoppingCartSKUPriceDetail : CMSEcommerceModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize product price detail
        InitializeSKUPriceDetailControl();

        // Set the title
        this.CurrentMaster.Title.TitleText = GetString("ProductPriceDetail.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Ecommerce/pricedetail.png");

        btnClose.Text = GetString("General.Close"); 
        btnClose.OnClientClick = "Close(); return false;";
    }


    /// <summary>
    /// Initializes properties of the control which display current sku price detail.
    /// </summary>
    private void InitializeSKUPriceDetailControl()
    {
        // Set current SKU ID
        this.ucSKUPriceDetail.CartItemGuid = QueryHelper.GetGuid("itemguid", Guid.Empty);

        // Get local shopping cart session name
        string sessionName = QueryHelper.GetString("cart", String.Empty);
        if (sessionName != String.Empty)
        {
            // Get local shopping cart when in CMSDesk
            object obj = SessionHelper.GetValue(sessionName);
            if (obj != null)
            {
                this.ucSKUPriceDetail.ShoppingCart = (ShoppingCartInfo)obj;
            }
        }
    }
}
