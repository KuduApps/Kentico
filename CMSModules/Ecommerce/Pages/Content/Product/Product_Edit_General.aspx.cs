using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_General : CMSContentProductPage, IPostBackEventHandler
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
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "ContentProduct.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "ContentProduct.General");
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

    #endregion


    #region "Methods"

    /// <summary>
    /// Disables form editing.
    /// </summary>
    private void DisableFormEditing()
    {
        productEditElem.FormEnabled = false;
    }


    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                this.productEditElem.Save();
                break;
        }
    }

    #endregion


    #region IPostBackEventHandler Members

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
