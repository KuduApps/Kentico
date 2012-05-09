using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Options : CMSContentProductPage
{
    #region "Variables"

    protected int nodeId = 0;
    protected int productId = 0;
    protected SKUInfo sku = null;

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Get the info from query string
        nodeId = QueryHelper.GetInteger("nodeid", 0);
        productId = QueryHelper.GetInteger("productID", 0);

        this.NodeID = nodeId;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "ContentProduct.Options"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "ContentProduct.Options");
        }

        if (this.Node != null)
        {
            sku = SKUInfoProvider.GetSKUInfo(productId);

            EditedObject = sku;

            if (sku != null)
            {
                // Check site ID
                CheckProductSiteID(sku.SKUSiteID);

                ucOptions.ProductID = productId;
                ucOptions.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
            }
        }
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Check permissions
        if(CheckProductPermissions(sku))
        {
            ucOptions.SaveItems();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Disables form editing.
    /// </summary>
    private void DisableFormEditing()
    {
        ucOptions.Enabled = false;
    }

    #endregion
}
