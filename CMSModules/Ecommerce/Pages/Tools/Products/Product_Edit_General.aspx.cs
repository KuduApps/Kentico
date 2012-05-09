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

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_Edit_General : CMSProductsPage, IPostBackEventHandler
{
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
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "ProductOptions.Options.General"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "ProductOptions.Options.General");
            }
        }
        else
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Products.General"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Products.General");
            }
        }

        this.productEditElem.ProductSiteID = ConfiguredSiteID;
        this.productEditElem.ProductSaved += new EventHandler(productEditElem_ProductSaved);

        int productId = QueryHelper.GetInteger("productId", 0);
        if (productId > 0)
        {
            SKUInfo sku = SKUInfoProvider.GetSKUInfo(productId);
            EditedObject = sku;

            if (sku != null)
            {
                // Check product site id
                CheckEditedObjectSiteID(sku.SKUSiteID);

                string errorMessage = CMSContentProductPage.CheckProductDepartmentPermissions(sku);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    // Disable form editing                                                            
                    DisableFormEditing();

                    // Show access denied message
                    lblError.Text = errorMessage;
                }

                productEditElem.ProductID = productId;

                productEditElem.ProductSiteID = sku.SKUSiteID;
            }
        }

        // Set header actions
        string[,] actions = new string[1, 10];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = this.GetString("Header.Settings.SaveChanged");
        actions[0, 2] = String.Empty;
        actions[0, 3] = String.Empty;
        actions[0, 4] = String.Empty;
        actions[0, 5] = this.productEditElem.FormEnabled ? this.GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png") : this.GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
        actions[0, 6] = "save"; // command name
        actions[0, 7] = String.Empty; // command argument
        actions[0, 8] = this.productEditElem.FormEnabled.ToString(); // register shortcut action
        actions[0, 9] = this.productEditElem.FormEnabled.ToString(); // enabled

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += this.HeaderActions_ActionPerformed;
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
        productEditElem.FormEnabled = false;
    }


    private void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                this.productEditElem.Save();
                break;
        }
    }


    private void productEditElem_ProductSaved(object sender, EventArgs e)
    {
        // Refresh header
        ScriptHelper.RefreshTabHeader(this, "general");
    }
    
    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Handles postback events.
    /// </summary>
    /// <param name="eventArgument">Postback argument</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        switch (eventArgument.ToLower())
        {
            case "save":
                this.productEditElem.Save();
                break;
        }
    }

    #endregion
}
