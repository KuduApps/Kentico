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

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_Tax : CMSProductsPage
{
    #region "Variables"

    private SKUInfo sku = null;

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        if (QueryHelper.GetInteger("categoryid", 0) > 0)
        {
            this.IsProductOption = true;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.GetInteger("categoryid", 0) > 0)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "ProductOptions.Options.TaxClasses"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "ProductOptions.Options.TaxClasses");
            }
        }
        else
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Products.TaxClasses"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Products.TaxClasses");
            }
        }

        int productId = QueryHelper.GetInteger("productId", 0);
        if (productId > 0)
        {
            sku = SKUInfoProvider.GetSKUInfo(productId);
            EditedObject = sku;

            if (sku != null)
            {
                // Check products site id
                CheckEditedObjectSiteID(sku.SKUSiteID);

                this.taxForm.ProductID = productId;
                this.taxForm.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
            }
        }
    }

    #endregion


    #region "Methods"

    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        if (CheckProductPermissions(sku))
        {
            // Save chages
            this.taxForm.SaveItems();
        }
    }


    /// <summary>
    /// Disables form editing.
    /// </summary>
    private void DisableFormEditing()
    {
        this.taxForm.Enabled = false;
    }

    #endregion
}
