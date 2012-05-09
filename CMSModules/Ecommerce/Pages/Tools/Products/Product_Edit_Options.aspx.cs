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
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_Options : CMSProductsPage
{
    #region "Variables"

    private SKUInfo sku = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Products.Options"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Products.Options");
        }

        int productId = QueryHelper.GetInteger("productId", 0);
        if (productId > 0)
        {
            sku = SKUInfoProvider.GetSKUInfo(productId);

            EditedObject = sku;

            if (sku != null)
            {
                // Check site ID
                CheckEditedObjectSiteID(sku.SKUSiteID);
                
                ucOptions.ProductID = productId;
                ucOptions.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Disables form editing.
    /// </summary>
    private void DisableFormEditing()
    {
        ucOptions.Enabled = false;
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Check permissions
        if (CheckProductPermissions(sku))
        {
            ucOptions.SaveItems();
        }
    }

    #endregion
}
