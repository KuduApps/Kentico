using System;

using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_CustomFields : CMSContentProductPage
{
    #region "Variables"

    protected int nodeId = 0;
    protected int productId = 0;
    protected SKUInfo skuObj = null;
    protected int editedSiteId = 0;

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


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Initialize DataForm
        if (productId > 0)
        {
            skuObj = SKUInfoProvider.GetSKUInfo(productId);
            EditedObject = skuObj;

            formProductCustomFields.Info = skuObj;
            formProductCustomFields.OnBeforeSave += formProductCustomFields_OnBeforeSave;
            formProductCustomFields.OnAfterSave += formProductCustomFields_OnAfterSave;
        }
        else
        {
            formProductCustomFields.Enabled = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        formProductCustomFields.BasicForm.SubmitButton.CssClass = "ContentButton";

        lblInfo.Visible = !String.IsNullOrEmpty(lblInfo.Text);
        lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "ContentProduct.CustomFields"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "ContentProduct.CustomFields");
        }

        if (this.Node != null)
        {
            if (skuObj != null)
            {
                editedSiteId = skuObj.SKUSiteID;

                // Check product site id
                CheckProductSiteID(editedSiteId);

                if (!IsAuthorizedToModifyDocument())
                {
                    // Disable form editing                                                            
                    DisableFormEditing();

                    // Show access denied message
                    lblError.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), this.Node.NodeAliasPath);
                }

                string errorMessage = CheckProductDepartmentPermissions(skuObj);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    // Disable form editing                                                            
                    DisableFormEditing();

                    // Show access denied message
                    lblError.Text = errorMessage;
                }
            }
        }
    }

    #endregion


    #region "Events"

    protected void formProductCustomFields_OnBeforeSave(object sender, EventArgs e)
    {
        // Check permissions
        bool global = (editedSiteId <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifySKU(global))
        {
            if (global)
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                CMSPage.RedirectToCMSDeskAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyProducts");
            }
        }
    }


    protected void formProductCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        this.lblInfo.Text = GetString("General.ChangesSaved");
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Disables form editing.
    /// </summary>
    private void DisableFormEditing()
    {
        formProductCustomFields.Enabled = false;
    }

    #endregion
}
