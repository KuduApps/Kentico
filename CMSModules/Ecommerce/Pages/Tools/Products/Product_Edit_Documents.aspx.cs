using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_Documents : CMSProductsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Products.Documents"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Products.Documents");
        }

        int productId = QueryHelper.GetInteger("productId", 0);
        if (productId > 0)
        {
            SKUInfo sku = SKUInfoProvider.GetSKUInfo(productId);
            EditedObject = sku;
            
            if (sku != null)
            {
                string errorMessage = CMSContentProductPage.CheckProductDepartmentPermissions(sku);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    // Disable form editing                                                            
                    DisableFormEditing();

                    // Show access denied message
                    lblError.Text = errorMessage;
                }

                productDocuments.ProductID = productId;
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
    }


    #region "Private methods"

    /// <summary>
    /// Disables form editing.
    /// </summary>
    private void DisableFormEditing()
    {
        productDocuments.Enabled = false;
    }

    #endregion
}
