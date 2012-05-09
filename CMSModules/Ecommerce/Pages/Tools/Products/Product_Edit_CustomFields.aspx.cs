using System;

using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_CustomFields : CMSProductsPage
{
    #region "Variables"

    protected int productId;
    protected int editedSiteId = 0;

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        if (QueryHelper.GetInteger("categoryid", 0) > 0)
        {
            IsProductOption = true;
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (QueryHelper.GetInteger("categoryid", 0) > 0)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "ProductOptions.Options.CustomFields"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "ProductOptions.Options.CustomFields");
            }
        }
        else
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Products.CustomFields"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Products.CustomFields");
            }
        }

        // Set edit mode
        productId = QueryHelper.GetInteger("productId", 0);
        if (productId > 0)
        {
            SKUInfo sku = SKUInfoProvider.GetSKUInfo(productId);
            EditedObject = sku;

            if (sku != null)
            {
                editedSiteId = sku.SKUSiteID;

                // Check product site id
                CheckEditedObjectSiteID(editedSiteId);

                string errorMessage = CMSContentProductPage.CheckProductDepartmentPermissions(sku);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    // Disable form editing                                                            
                    DisableFormEditing();

                    // Show access denied message
                    lblError.Text = errorMessage;
                }

                formProductCustomFields.Info = SKUInfoProvider.GetSKUInfo(productId);
                EditedObject = formProductCustomFields.Info;
                formProductCustomFields.OnBeforeSave += formProductCustomFields_OnBeforeSave;
                formProductCustomFields.OnAfterSave += formProductCustomFields_OnAfterSave;
            }
        }
        else
        {
            DisableFormEditing();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (formProductCustomFields.BasicForm != null)
        {
            if (formProductCustomFields.BasicForm.FieldControls.Count <= 0)
            {
                formProductCustomFields.BasicForm.SubmitButton.Visible = false;
            }
            else
            {
                formProductCustomFields.BasicForm.SubmitButton.CssClass = "ContentButton";
            }
        }

        lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
    }

    #endregion


    #region "Events"

    void formProductCustomFields_OnBeforeSave(object sender, EventArgs e)
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


    void formProductCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        // Display 'changes saved' information
        lblInfo.Visible = true;
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
