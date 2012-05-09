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

public partial class CMSModules_Ecommerce_CMSPages_ShoppingCartSKUPriceDetail : CMSLiveModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize product price detail
        this.ucSKUPriceDetail.CartItemGuid = QueryHelper.GetGuid("itemguid", Guid.Empty);
        this.ucSKUPriceDetail.ShoppingCart = ECommerceContext.CurrentShoppingCart;

        // Set the title
        this.CurrentMaster.Title.TitleText = GetString("ProductPriceDetail.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Ecommerce/pricedetail.png");

        btnClose.Text = GetString("General.Close"); 
        btnClose.OnClientClick = "Close(); return false;";
    }
}
