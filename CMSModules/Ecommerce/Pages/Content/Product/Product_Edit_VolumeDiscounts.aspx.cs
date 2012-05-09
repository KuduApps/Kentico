using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_VolumeDiscounts : CMSContentProductPage
{
    #region "Variables"

    protected int nodeId = 0;
    protected int productId = 0;
    protected SKUInfo sku = null;
    protected CurrencyInfo productCurrency = null;
    protected bool disableEditing = false;

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
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "ContentProduct.VolumeDiscounts"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "ContentProduct.VolumeDiscounts");
        }

        if (this.Node != null)
        {
            if (!IsAuthorizedToModifyDocument())
            {
                // Disable form editing                                                            
                disableEditing = true;

                // Show access denied message
                lblError.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), this.Node.NodeAliasPath);
            }

            // Get currency for discount value formatting
            sku = SKUInfoProvider.GetSKUInfo(productId);

            EditedObject = sku;

            if (sku != null)
            {
                // Check site ID
                CheckProductSiteID(sku.SKUSiteID);
            }

            productCurrency = CurrencyInfoProvider.GetMainCurrency(sku != null ? sku.SKUSiteID : Node.NodeSiteID);

            // Set action handler
            gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
            gridElem.WhereCondition = "VolumeDiscountSKUID = " + productId;

            // Set the master page actions element        
            string[,] actions = new string[1, 10];
            actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[0, 1] = GetString("Product_Edit_VolumeDiscount_List.NewItemCaption");
            actions[0, 2] = (disableEditing ? "return false;" : "modalDialog('" + ResolveUrl("~/CMSModules/Ecommerce/Pages/Content/Product/Product_Edit_VolumeDiscount_Edit.aspx") + "?ProductID=" + productId + "&dialog=1', 'VolumeDiscounts', 500, 350);");
            actions[0, 3] = null;
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("Objects/Ecommerce_VolumeDiscount/add.png");
            actions[0, 9] = (!disableEditing).ToString();

            this.CurrentMaster.HeaderActions.Actions = actions;

            // Register modal dialog script
            ScriptHelper.RegisterDialogScript(this);
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName.ToLower() == "delete")
        {
            if ((sku != null) && CheckProductPermissions(sku))
            {
                // Delete VolumeDiscountInfo object from database
                VolumeDiscountInfoProvider.DeleteVolumeDiscountInfo(Convert.ToInt32(actionArgument));
            }
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnExternalDataBound event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Source name</param>
    /// <param name="parameter">Parameter</param>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "discountvalue":
                DataRowView row = (DataRowView)parameter;
                double value = ValidationHelper.GetDouble(row["VolumeDiscountValue"], 0);
                bool isFlat = ValidationHelper.GetBoolean(row["VolumeDiscountIsFlatValue"], false);

                // If value is relative, add "%" next to the value.
                if (isFlat)
                {
                    return CurrencyInfoProvider.GetFormattedPrice(value, productCurrency);
                }
                else
                {
                    return value.ToString() + "%";
                }

            case "edit":
                if (disableEditing)
                {
                    // Disable editing
                    ImageButton editButton = ((ImageButton)sender);
                    editButton.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Editdisabled.png");
                    editButton.OnClientClick = "return false;";
                }
                else
                {
                    // Open modal dialog with volume discount edit form
                    int volumeDiscountId = ValidationHelper.GetInteger(((DataRowView)((GridViewRow)parameter).DataItem)[0], 0);
                    ((WebControl)sender).Attributes["onclick"] = "modalDialog('" + ResolveUrl("~/CMSModules/Ecommerce/Pages/Content/Product/Product_Edit_VolumeDiscount_Edit.aspx") + "?ProductID=" + productId + "&VolumeDiscountID=" + volumeDiscountId + "&dialog=1', 'VolumeDiscounts', 500, 350); return false;";
                }
                break;

            case "delete":
                if (disableEditing)
                {
                    ImageButton deleteButton = ((ImageButton)sender);
                    deleteButton.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Deletedisabled.png");
                    deleteButton.OnClientClick = "return false;";
                }
                break;
        }

        return parameter;
    }

    #endregion
}

