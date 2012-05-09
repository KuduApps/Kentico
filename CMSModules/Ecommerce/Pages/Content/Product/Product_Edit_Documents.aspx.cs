using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Documents : CMSContentProductPage
{
    #region "Variables"

    protected int nodeId = 0;
    protected int productId = 0;

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
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "ContentProduct.Documents"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "ContentProduct.Documents");
        }

        if (this.Node != null)
        {
            SKUInfo sku = SKUInfoProvider.GetSKUInfo(productId);
            EditedObject = sku;

            if (sku != null)
            {
                // Check site ID
                CheckProductSiteID(sku.SKUSiteID);

                if (!IsAuthorizedToModifyDocument())
                {
                    // Disable form editing                                                            
                    DisableFormEditing();

                    // Show access denied message
                    lblError.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), this.Node.NodeAliasPath);
                }

                string errorMessage = CheckProductDepartmentPermissions(sku);
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

    #endregion


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
