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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Suppliers_Supplier_Edit : CMSSuppliersPage
{
    #region "Variables"

    protected int mSupplierId = 0;
    protected int editedSiteId = -1;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        rfvDisplayName.ErrorMessage = GetString("supplier_Edit.errorEmptyDisplayName");

        // control initializations
        lblSupplierFax.Text = GetString("supplier_Edit.SupplierFaxLabel");
        lblSupplierEmail.Text = GetString("supplier_Edit.SupplierEmailLabel");
        lblSupplierPhone.Text = GetString("supplier_Edit.SupplierPhoneLabel");
        lblSupplierDisplayName.Text = GetString("supplier_Edit.SupplierDisplayNameLabel");

        btnOk.Text = GetString("General.OK");

        string currentSupplier = GetString("supplier_Edit.NewItemCaption");

        // Get supplier ID from querystring
        mSupplierId = QueryHelper.GetInteger("supplierid", 0);
        editedSiteId = ConfiguredSiteID;

        if (mSupplierId > 0)
        {
            SupplierInfo supplierObj = SupplierInfoProvider.GetSupplierInfo(mSupplierId);
            EditedObject = supplierObj;

            if (supplierObj != null)
            {
                currentSupplier = supplierObj.SupplierDisplayName;
                // Store site id of edited supplier
                editedSiteId = supplierObj.SupplierSiteID;

                //Check site id of edited supplier
                CheckEditedObjectSiteID(editedSiteId);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(supplierObj);

                    // Show that the supplier was created or updated successfully
                    if (QueryHelper.GetBoolean("saved", false))
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }

        this.CurrentMaster.Title.HelpTopicName = "newedit_supplier";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title breadcrumbs control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("supplier_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Suppliers/Supplier_List.aspx?siteId=" + this.SiteID;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(currentSupplier, editedSiteId);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Set master title
        if (mSupplierId > 0)
        {
            this.CurrentMaster.Title.TitleText = GetString("com.supplier.edit");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Supplier/object.png");
        }
        else
        {
            this.CurrentMaster.Title.TitleText = GetString("supplier_List.NewItemCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Supplier/new.png");
        }

        AddMenuButtonSelectScript("Suppliers", "");
    }


    /// <summary>
    /// Load data of editing supplier.
    /// </summary>
    /// <param name="supplierObj">Supplier object</param>
    protected void LoadData(SupplierInfo supplierObj)
    {
        EditedObject = supplierObj;

        txtSupplierFax.Text = supplierObj.SupplierFax;
        txtSupplierEmail.Text = supplierObj.SupplierEmail;
        txtSupplierPhone.Text = supplierObj.SupplierPhone;
        txtSupplierDisplayName.Text = supplierObj.SupplierDisplayName;
        chkSupplierEnabled.Checked = supplierObj.SupplierEnabled;
    }


    /// <summary>
    /// Sets the data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (editedSiteId <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifySupplier(global))
        {
            if (global)
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifySuppliers");
            }
        }

        string errorMessage = new Validator().NotEmpty(txtSupplierDisplayName.Text.Trim(), GetString("supplier_Edit.errorEmptyDisplayName")).Result;

        // Validate email format if not empty
        if (errorMessage == "")
        {
            if ((txtSupplierEmail.Text.Trim() != "") && (!ValidationHelper.IsEmail(txtSupplierEmail.Text.Trim())))
            {
                errorMessage = GetString("supplier_Edit.errorEmailFormat");
            }
        }

        if (errorMessage == "")
        {
            SupplierInfo supplierObj = SupplierInfoProvider.GetSupplierInfo(mSupplierId);

            if (supplierObj == null)
            {
                supplierObj = new SupplierInfo();
                // Assign site ID
                supplierObj.SupplierSiteID = ConfiguredSiteID;
            }

            supplierObj.SupplierFax = txtSupplierFax.Text.Trim();
            supplierObj.SupplierEmail = txtSupplierEmail.Text.Trim();
            supplierObj.SupplierPhone = txtSupplierPhone.Text.Trim();
            supplierObj.SupplierDisplayName = txtSupplierDisplayName.Text.Trim();
            supplierObj.SupplierEnabled = chkSupplierEnabled.Checked;

            // Save changes
            SupplierInfoProvider.SetSupplierInfo(supplierObj);

            URLHelper.Redirect("Supplier_Edit.aspx?supplierid=" + Convert.ToString(supplierObj.SupplierID) + "&saved=1&siteId=" + SiteID);

        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
