using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Ecommerce_Pages_Content_Product_Product_Selection : CMSContentProductPage, IPostBackEventHandler
{
    #region "Variables"

    bool? mAllowGlobalProducts = null;

    #endregion


    #region "Properties"

    private bool AllowGlobalProducts
    {
        get
        {
            if (!mAllowGlobalProducts.HasValue)
            {
                mAllowGlobalProducts = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSStoreAllowGlobalProducts");
            }

            return mAllowGlobalProducts ?? false;
        }

    }

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        this.NodeID = QueryHelper.GetInteger("nodeid", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize header actions
        string[,] headerActions = new string[1, 10];

        headerActions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        headerActions[0, 1] = this.GetString("Header.Settings.SaveChanged");
        headerActions[0, 2] = String.Empty;
        headerActions[0, 3] = String.Empty;
        headerActions[0, 4] = String.Empty;
        headerActions[0, 5] = this.chkMarkDocAsProd.Checked ? this.GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png") : this.GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
        headerActions[0, 6] = "save"; // command name
        headerActions[0, 7] = String.Empty; // command argument
        headerActions[0, 8] = this.chkMarkDocAsProd.Checked.ToString(); // register shortcut action
        headerActions[0, 9] = this.chkMarkDocAsProd.Checked.ToString(); // enabled

        // Hide 'Create global product' option when global products are not allowed.
        radCreateGlobal.Visible = AllowGlobalProducts;

        if (!IsAuthorizedToModifyDocument())
        {
            // Disable form editing                                                            
            DisableFormEditing();

            // Disable save action
            headerActions[0, 5] = this.GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
            headerActions[0, 8] = false.ToString();
            headerActions[0, 9] = false.ToString();

            // Show access denied message
            lblGlobalError.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), this.Node.NodeAliasPath);
        }

        CMSContext.ViewMode = ViewModeEnum.Product;

        ctrlProduct.NodeID = this.NodeID;
        skuElem.SiteID = CMSContext.CurrentSiteID;
        skuElem.UniSelector.AddGlobalObjectSuffix = true;

        // Create product according setting ang checkbox state
        ctrlProduct.ProductSiteID = (AllowGlobalProducts && radCreateGlobal.Checked) ? 0 : CMSContext.CurrentSiteID;

        skuElem.UserID = 0;
        // Offer only products from users departments
        CurrentUserInfo user = CMSContext.CurrentUser;
        skuElem.UserID = user.IsAuthorizedPerResource("CMS.Ecommerce", "AccessAllDepartments") ? 0 : user.UserID;

        if (this.Node.NodeSKUID > 0)
        {
            // Get the product
            SKUInfo si = SKUInfoProvider.GetSKUInfo(this.Node.NodeSKUID);
            if (si != null)
            {
                URLHelper.Redirect("Product_Edit_Frameset.aspx?productid=" + this.Node.NodeSKUID + "&nodeid=" + this.NodeID);
            }
        }

        chkMarkDocAsProd.Text = GetString("Product_Selection.MarkDocAsProd");
        radSelect.Text = GetString("Product_Selection.SelectProduct");
        radCreate.Text = GetString("Product_Selection.CreateProduct");
        radCreateGlobal.Text = GetString("Product_Selection.CreateGlobalProduct");

        if (ctrlProduct.ProductID > 0)
        {
            pnlNew.Visible = true;
            pnlSelect.Visible = false;
            ctrlProduct.Visible = true;
            radCreate.Visible = false;
            radCreateGlobal.Visible = false;
            radSelect.Visible = false;
            chkMarkDocAsProd.Text = GetString("com_SKU_edit_general.DocumentIsProduct");
            return;
        }

        if (!RequestHelper.IsPostBack())
        {
            chkMarkDocAsProd.Checked = false;
            skuElem.Enabled = chkMarkDocAsProd.Checked;
            radSelect.Checked = true;
            radCreate.Checked = false;
            radCreateGlobal.Checked = false;
            pnlSelect.Enabled = true;
            pnlProduct.Enabled = false;
        }

        // Set header actions
        this.CurrentMaster.HeaderActions.Actions = headerActions;
        this.CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblGlobalError.Visible = !String.IsNullOrEmpty(lblGlobalError.Text);
    }

    #endregion


    #region "Methods"

    protected void chkMarkDocAsProd_CheckedChanged(object sender, EventArgs e)
    {
        if (ctrlProduct.ProductID > 0)
        {
            pnlNew.Enabled = chkMarkDocAsProd.Checked;
        }
        else
        {
            pnlProduct.Enabled = chkMarkDocAsProd.Checked;
            skuElem.Enabled = chkMarkDocAsProd.Checked;
        }
    }

    protected void rad_CheckedChanged(object sender, EventArgs e)
    {
        CheckedChanged();
    }


    private void CheckedChanged()
    {
        if (radSelect.Checked)
        {
            pnlSelect.Enabled = true;
            pnlSelect.Visible = true;
            ctrlProduct.Visible = false;
            pnlNew.Visible = false;
        }
        else
        {
            pnlSelect.Enabled = false;
            pnlSelect.Visible = false;
            ctrlProduct.Visible = true;
            pnlNew.Visible = true;

            // Set mappings
            TreeNode node = this.Node;
            if (node != null)
            {
                DataClassInfo dci = DataClassInfoProvider.GetDataClass(this.Node.NodeClassName);
                if (dci != null)
                {
                    // Get the mapped values
                    ctrlProduct.ProductName = ValidationHelper.GetString(node.GetValue(Convert.ToString(dci.SKUMappings["SKUName"])), "");
                    ctrlProduct.ProductPrice = ValidationHelper.GetDouble(node.GetValue(Convert.ToString(dci.SKUMappings["SKUPrice"])), 0);
                    ctrlProduct.ProductWeight = ValidationHelper.GetDouble(node.GetValue(Convert.ToString(dci.SKUMappings["SKUWeight"])), 0);
                    ctrlProduct.ProductHeight = ValidationHelper.GetDouble(node.GetValue(Convert.ToString(dci.SKUMappings["SKUHeight"])), 0);
                    ctrlProduct.ProductWidth = ValidationHelper.GetDouble(node.GetValue(Convert.ToString(dci.SKUMappings["SKUWidth"])), 0);
                    ctrlProduct.ProductDepth = ValidationHelper.GetDouble(node.GetValue(Convert.ToString(dci.SKUMappings["SKUDepth"])), 0);
                    ctrlProduct.ProductDescription = ValidationHelper.GetString(node.GetValue(Convert.ToString(dci.SKUMappings["SKUDescription"])), "");

                    Guid guid = ValidationHelper.GetGuid(node.GetValue(Convert.ToString(dci.SKUMappings["SKUImagePath"])), Guid.Empty);
                    ctrlProduct.ProductImagePath = ((guid != Guid.Empty) ? TreePathUtils.GetAttachmentUrl(guid, this.Node.NodeAlias) : "");
                }
            }

            // prefill form with values from the document
            ctrlProduct.SetValues();
        }
    }


    private void SubmitForm()
    {
        if (ctrlProduct.ProductID > 0)
        {
            // Check permissions
            if (ctrlProduct.ProductSiteID > 0)
            {
                if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyProducts"))
                {
                    RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyProducts");
                }
            }
            else
            {
                if (!ECommerceContext.IsUserAuthorizedForPermission("EcommerceGlobalModify"))
                {
                    RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
                }
            }

            // Update
            if (chkMarkDocAsProd.Checked)
            {
                ctrlProduct.Save();
            }
            // Delete
            else
            {
                this.Node.NodeSKUID = 0;
                this.Node.Update();

                // Update search index for node
                if ((this.Node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
                {
                    SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, this.Node.GetSearchID());
                }

                // Log synchronization
                DocumentSynchronizationHelper.LogDocumentChange(this.Node, TaskTypeEnum.UpdateDocument, this.Node.TreeProvider);

                URLHelper.Redirect("Product_Selection.aspx?nodeid=" + this.NodeID);
            }
        }
        else
        {
            if (!IsAuthorizedToModifyDocument())
            {
                RedirectToAccessDenied("CMS.Content", "Modify");
            }

            // Use existing product
            if (radSelect.Checked)
            {
                if (skuElem.SKUID > 0)
                {
                    this.Node.NodeSKUID = this.skuElem.SKUID;
                    this.Node.Update();

                    // Update search index for node
                    if ((this.Node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
                    {
                        SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, this.Node.GetSearchID());
                    }

                    // Log synchronization
                    DocumentSynchronizationHelper.LogDocumentChange(this.Node, TaskTypeEnum.UpdateDocument, this.Node.TreeProvider);

                    URLHelper.Redirect(String.Format("Product_Edit_Frameset.aspx?productid={0}&nodeid={1}&saved=1", this.Node.NodeSKUID, this.NodeID));
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Products.EmptyList");
                }
            }
            // Create new product
            else
            {
                ctrlProduct.Save();
            }
        }
    }


    protected void ctrlProduct_ProductSaved(object sender, EventArgs e)
    {
        // Set SKUID to node
        this.Node.NodeSKUID = this.ctrlProduct.ProductID;
        this.Node.Update();

        // Redirect to product edit
        URLHelper.Redirect(String.Format("Product_Edit_Frameset.aspx?productid={0}&nodeid={1}&saved=1", this.Node.NodeSKUID, this.NodeID));
    }


    /// <summary>
    /// Disables form editing.
    /// </summary>
    protected void DisableFormEditing()
    {
        chkMarkDocAsProd.Enabled = false;
        radCreate.Enabled = false;
        radCreate.Enabled = false;
        radSelect.Enabled = false;
        skuElem.Enabled = false;
        ctrlProduct.FormEnabled = false;
    }


    void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                this.SubmitForm();
                break;
        }
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
                this.SubmitForm();
                break;
        }
    }

    #endregion
}
