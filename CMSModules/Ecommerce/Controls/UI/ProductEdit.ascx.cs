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
using CMS.TreeEngine;
using CMS.FormEngine;
using CMS.LicenseProvider;
using CMS.IO;
using CMS.FormControls;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Ecommerce_Controls_UI_ProductEdit : FormEngineUserControl
{
    #region "Variables"

    private string mProductName = "";
    private double mProductWeight = 0;
    private double mProductHeight = 0;
    private double mProductWidth = 0;
    private double mProductDepth = 0;
    private string mProductImagePath = "";
    private string mProductDescription = "";
    private double mProductPrice = 0;
    private int mProductSiteId = 0;

    private int mOptionCategoryId = 0;
    private int mProductId = 0;
    private int mNodeId = 0;

    private bool hasAttachmentImagePath = true;
    private int editedSiteId = 0;

    private bool uploadToNonexistentSku = false;

    private bool? mProductOrdered = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Product name.
    /// </summary>
    public string ProductName
    {
        get
        {
            return mProductName;
        }
        set
        {
            mProductName = value;
        }
    }


    /// <summary>
    /// Product price.
    /// </summary>
    public double ProductPrice
    {
        get
        {
            return mProductPrice;
        }
        set
        {
            mProductPrice = value;
        }
    }


    /// <summary>
    /// Product weight.
    /// </summary>
    public double ProductWeight
    {
        get
        {
            return mProductWeight;
        }
        set
        {
            mProductWeight = value;
        }
    }


    /// <summary>
    /// Product height.
    /// </summary>
    public double ProductHeight
    {
        get
        {
            return mProductHeight;
        }
        set
        {
            mProductHeight = value;
        }
    }


    /// <summary>
    /// Product width.
    /// </summary>
    public double ProductWidth
    {
        get
        {
            return mProductWidth;
        }
        set
        {
            mProductWidth = value;
        }
    }


    /// <summary>
    /// Product depth.
    /// </summary>
    public double ProductDepth
    {
        get
        {
            return mProductDepth;
        }
        set
        {
            mProductDepth = value;
        }
    }


    /// <summary>
    /// Product image path.
    /// </summary>
    public string ProductImagePath
    {
        get
        {
            return mProductImagePath;
        }
        set
        {
            mProductImagePath = value;
        }
    }


    /// <summary>
    /// Product description.
    /// </summary>
    public string ProductDescription
    {
        get
        {
            return mProductDescription;
        }
        set
        {
            mProductDescription = value;
        }
    }


    /// <summary>
    /// Product ID.
    /// </summary>
    public int ProductID
    {
        get
        {
            return mProductId;
        }
        set
        {
            mProductId = value;
        }
    }


    /// <summary>
    /// NodeID.
    /// </summary>
    public int NodeID
    {
        get
        {
            return mNodeId;
        }
        set
        {
            mNodeId = value;
        }
    }


    /// <summary>
    /// Option category ID.
    /// </summary>
    public int OptionCategoryID
    {
        get
        {
            return mOptionCategoryId;
        }
        set
        {
            mOptionCategoryId = value;
        }
    }


    /// <summary>
    /// Id of the site.
    /// </summary>
    public int ProductSiteID
    {
        get
        {
            return mProductSiteId;
        }
        set
        {
            mProductSiteId = value;
        }
    }


    /// <summary>
    /// Indicates whether form is enabled or not.
    /// </summary>
    public bool FormEnabled
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["FormEnabled"], true);
        }
        set
        {
            this.txtSKUAvailableInDays.Enabled = value;
            this.htmlTemplateBody.Enabled = value;
            this.txtSKUName.Enabled = value;
            this.txtSKUNumber.Enabled = value;
            this.txtSKUPrice.Enabled = value;
            this.chkSKUEnabled.Enabled = value;
            this.departmentElem.Enabled = value;
            this.internalStatusElem.Enabled = value;
            this.manufacturerElem.Enabled = value;
            this.publicStatusElem.Enabled = value;
            this.supplierElem.Enabled = value;
            this.txtSKUWeight.Enabled = value;
            this.txtSKUHeight.Enabled = value;
            this.txtSKUWidth.Enabled = value;
            this.txtSKUDepth.Enabled = value;
            this.txtSKUAvailableItems.Enabled = value;
            this.chkSKUSellOnlyAvailable.Enabled = value;
            this.selectProductTypeElem.Enabled = value;
            this.chkNeedsShipping.Enabled = value;
            this.txtMaxOrderItems.Enabled = value;

            // Image
            this.imgSelect.Enabled = value;
            this.ucMetaFile.Enabled = value;

            // Conversion
            this.pnlConversion.Enabled = value;
            this.ucConversion.Enabled = value;

            ViewState["FormEnabled"] = value;
        }
    }


    /// <summary>
    /// Error message describing error while input data.
    /// </summary>
    public override string ErrorMessage
    {
        get
        {
            return this.lblError.Text.Trim();
        }
        set
        {
            this.lblError.Text = value;
            this.lblError.Visible = !String.IsNullOrEmpty(lblError.Text.Trim());
        }
    }


    /// <summary>
    /// Indicates if product was already ordered.
    /// </summary>
    public bool ProductOrdered
    {
        get
        {
            // Just created product is not used in order
            if (ProductID <= 0)
            {
                return false;
            }

            if (!mProductOrdered.HasValue)
            {
                // Check if any OrderItem bound to edited product exists
                DataSet ds = OrderItemInfoProvider.GetOrderItems("OrderItemSKUID = " + ProductID, null, 1, "OrderItemID");
                mProductOrdered = !DataHelper.DataSourceIsEmpty(ds);
            }

            return mProductOrdered ?? true;
        }
    }

    #endregion


    #region "Events"

    public event EventHandler ProductSaved;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        editedSiteId = ProductSiteID;

        manufacturerElem.SiteID = editedSiteId;
        departmentElem.SiteID = editedSiteId;
        supplierElem.SiteID = editedSiteId;
        publicStatusElem.SiteID = editedSiteId;
        internalStatusElem.SiteID = editedSiteId;
        txtSKUPrice.CurrencySiteID = editedSiteId;

        htmlTemplateBody.AutoDetectLanguage = false;
        htmlTemplateBody.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlTemplateBody.EditorAreaCSS = FormHelper.GetHtmlEditorAreaCss(CMSContext.CurrentSiteName);
        htmlTemplateBody.ToolbarSet = "Basic";

        // Set form groups texts
        this.pnlGeneral.GroupingText = this.GetString("com.productedit.general");
        this.pnlMembership.GroupingText = this.GetString("com.producttype.membership");
        this.pnlEProduct.GroupingText = this.GetString("com.producttype.eproduct");
        this.pnlDonation.GroupingText = this.GetString("com.producttype.donation");
        this.pnlBundle.GroupingText = this.GetString("com.producttype.bundle");
        this.pnlStatus.GroupingText = this.GetString("com.productedit.status");
        this.pnlShipping.GroupingText = this.GetString("com.productedit.shipping");
        this.pnlInventory.GroupingText = this.GetString("com.productedit.inventory");

        // Set validation messages
        this.txtSKUPrice.ValidationErrorMessage = this.GetString("com.productedit.priceinvalid");
        this.txtSKUPrice.EmptyErrorMessage = this.GetString("com.productedit.priceinvalid");
        this.txtSKUPrice.ValidatorOnNewLine = true;
        this.rfvSKUName.ErrorMessage = this.GetString("com.productedit.nameinvalid");
        this.rvSKUDepth.ErrorMessage = this.GetString("com.productedit.packagedepthinvalid");
        this.rvSKUHeight.ErrorMessage = this.GetString("com.productedit.packageheightinvalid");
        this.rvSKUWeight.ErrorMessage = this.GetString("com.productedit.packageweightinvalid");
        this.rvSKUWidth.ErrorMessage = this.GetString("com.productedit.packagewidthinvalid");
        this.rvSKUAvailableItems.ErrorMessage = this.GetString("com.productedit.availableitemsinvalid");
        this.rvSKUAvailableInDays.ErrorMessage = this.GetString("com.productedit.availabilityinvalid");
        this.rvMaxOrderItems.ErrorMessage = this.GetString("com.productedit.maxorderitemsinvalid");
        this.pnlConversion.GroupingText = this.GetString("conversion.conversion.list");

        // Conversion's logging avaible only for site objects
        if (ProductSiteID == 0)
        {
            this.pnlConversion.Visible = false;
        }

        // Get current product info
        SKUInfo skuObj = SKUInfoProvider.GetSKUInfo(mProductId);

        // If product exists
        if (skuObj != null)
        {
            this.editedSiteId = skuObj.SKUSiteID;
            string imagePath = skuObj.SKUImagePath;

            if (String.IsNullOrEmpty(imagePath) || imagePath.ToLower().StartsWith("~/getmetafile/"))
            {
                this.hasAttachmentImagePath = false;
            }

            if (this.OptionCategoryID == 0)
            {
                this.OptionCategoryID = skuObj.SKUOptionCategoryID;
            }

            if (this.OptionCategoryID > 0)
            {
                OptionCategoryInfo optionCat = OptionCategoryInfoProvider.GetOptionCategoryInfo(this.OptionCategoryID);
                if ((optionCat != null) &&
                    ((optionCat.CategorySelectionType == OptionCategorySelectionTypeEnum.TextBox) ||
                    (optionCat.CategorySelectionType == OptionCategorySelectionTypeEnum.TextArea)))
                {
                    this.selectProductTypeElem.AllowBundle = false;
                    this.selectProductTypeElem.AllowDonation = false;
                    this.selectProductTypeElem.AllowEproduct = false;
                    this.selectProductTypeElem.AllowMembership = false;
                    this.selectProductTypeElem.AllowStandardProduct = false;

                    this.selectProductTypeElem.AllowText = true;
                }

            }

            // Set site IDs
            this.membershipElem.SiteID = skuObj.SKUSiteID;
            this.eProductElem.SiteID = skuObj.SKUSiteID;
            this.eProductElem.SKUID = skuObj.SKUID;
            this.donationElem.SiteID = skuObj.SKUSiteID;
            this.bundleElem.SiteID = skuObj.SKUSiteID;
            this.bundleElem.BundleID = skuObj.SKUID;

            if (!RequestHelper.IsPostBack())
            {
                this.LoadData(skuObj);
            }
        }
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                // If creating a product option
                if (this.OptionCategoryID > 0)
                {
                    // Disable specific product type options
                    this.selectProductTypeElem.AllowBundle = false;
                    this.selectProductTypeElem.AllowDonation = false;
                    this.selectProductTypeElem.Initialize();
                }
            }

            this.hasAttachmentImagePath = false;

            this.membershipElem.SiteID = this.ProductSiteID;
            this.eProductElem.SiteID = this.ProductSiteID;
            this.eProductElem.SKUID = this.ProductID;
            this.donationElem.SiteID = this.ProductSiteID;
            this.bundleElem.SiteID = this.ProductSiteID;
            this.bundleElem.BundleID = this.ProductID;
        }

        // Get currently selected product type
        SKUProductTypeEnum selectedProductType = SKUInfoProvider.GetSKUProductTypeEnum((string)this.selectProductTypeElem.Value);

        // Stop processing of e-product properties element if selected product type is not e-product
        this.eProductElem.StopProcessing = (selectedProductType != SKUProductTypeEnum.EProduct);

        // Stop processing of bundle properties element if selected product type is not bundle
        this.bundleElem.StopProcessing = (selectedProductType != SKUProductTypeEnum.Bundle);

        // Enable uploaders if sufficient SKU modify permissions
        if (ECommerceContext.IsUserAuthorizedToModifySKU(editedSiteId == 0))
        {
            this.imgSelect.Enabled = this.FormEnabled;
            this.ucMetaFile.Enabled = this.FormEnabled;
        }

        CurrentUserInfo cui = CMSContext.CurrentUser;
        if ((cui != null) && (!cui.IsGlobalAdministrator))
        {
            departmentElem.UserID = cui.UserID;
        }

        if (ECommerceSettings.UseMetaFileForProductImage && !hasAttachmentImagePath)
        {
            // Display image uploader for existing product
            this.plcExistingProductImage.Visible = true;
            this.ucMetaFile.ObjectID = mProductId;
            this.ucMetaFile.ObjectType = ECommerceObjectType.SKU;
            this.ucMetaFile.Category = MetaFileInfoProvider.OBJECT_CATEGORY_IMAGE;
            this.ucMetaFile.SiteID = editedSiteId;
            this.ucMetaFile.OnAfterDelete += new EventHandler(ucMetaFile_OnAfterDelete);
        }
        else
        {
            // Display image uploader for new product
            this.plcNewProductImage.Visible = true;
        }

        // Check presence of main currency
        if (CurrencyInfoProvider.GetMainCurrency(editedSiteId) == null)
        {
            bool usingGlobal = ECommerceSettings.UseGlobalCurrencies(SiteInfoProvider.GetSiteName(editedSiteId));

            if (usingGlobal)
            {
                lblError.Text = GetString("com.noglobalmaincurrency");
            }
            else
            {
                lblError.Text = GetString("com.nomaincurrency");
            }

            lblError.Visible = true;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Chcek if product type can be changed
        if (ProductOrdered)
        {
            // Disable product type selector
            selectProductTypeElem.Enabled = false;

            // Uncoment this section to display warning informing, that product has already been placed into an order.
            // Show info message
            // lblUsedProductWarning.Visible = true;
            // lblUsedProductWarning.Style["cursor"] = "help";
        }

        // Display 'Changes were saved' message
        if (QueryHelper.GetString("saved", "") == "1")
        {
            this.lblInfo.Visible = true;
            this.lblInfo.Text = this.GetString("general.changessaved");
        }

        // Display new product error if required
        string newProductError = ValidationHelper.GetString(SessionHelper.GetValue("NewProductError"), null);

        if (!String.IsNullOrEmpty(newProductError))
        {
            this.lblNewProductError.Text = newProductError.Trim(new char[] { ';' }).Replace(";", "<br />");
            this.lblNewProductError.Visible = true;

            SessionHelper.Remove("NewProductError");
        }

        this.lblInfo.Visible = !String.IsNullOrEmpty(this.lblInfo.Text);

        base.OnPreRender(e);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Load data of editing ecommerceEcommerce.
    /// </summary>
    /// <param name="skuiObj">EcommerceEcommerce object</param>
    protected void LoadData(SKUInfo skuiObj)
    {
        // Set data reloaded flag
        this.ViewState["DataReloaded"] = true;

        // General
        txtSKUName.Text = skuiObj.SKUName;
        txtSKUNumber.Text = skuiObj.SKUNumber;
        htmlTemplateBody.ResolvedValue = skuiObj.SKUDescription;
        txtSKUPrice.Value = skuiObj.SKUPrice;
        departmentElem.DepartmentID = skuiObj.SKUDepartmentID;
        manufacturerElem.ManufacturerID = skuiObj.SKUManufacturerID;
        supplierElem.SupplierID = skuiObj.SKUSupplierID;
        imgSelect.Value = skuiObj.SKUImagePath;

        // Status
        chkSKUEnabled.Checked = skuiObj.SKUEnabled;
        publicStatusElem.PublicStatusID = skuiObj.SKUPublicStatusID;
        internalStatusElem.InternalStatusID = skuiObj.SKUInternalStatusID;

        // Shipping
        chkNeedsShipping.Checked = skuiObj.SKUNeedsShipping;
        txtSKUWeight.Text = (skuiObj.SKUWeight > 0) ? skuiObj.SKUWeight.ToString() : String.Empty;
        txtSKUWidth.Text = (skuiObj.SKUWidth > 0) ? skuiObj.SKUWidth.ToString() : String.Empty;
        txtSKUHeight.Text = (skuiObj.SKUHeight > 0) ? skuiObj.SKUHeight.ToString() : String.Empty;
        txtSKUDepth.Text = (skuiObj.SKUDepth > 0) ? skuiObj.SKUDepth.ToString() : String.Empty;

        // Inventory
        this.chkSKUSellOnlyAvailable.Checked = skuiObj.SKUSellOnlyAvailable;
        this.txtSKUAvailableItems.Text = (skuiObj.GetIntegerValue("SKUAvailableItems", -1) == -1) ? String.Empty : skuiObj.SKUAvailableItems.ToString();
        this.txtSKUAvailableInDays.Text = (skuiObj.GetIntegerValue("SKUAvailableInDays", -1) == -1) ? String.Empty : skuiObj.SKUAvailableInDays.ToString();
        this.txtMaxOrderItems.Text = (skuiObj.GetIntegerValue("SKUMaxItemsInOrder", -1) == -1) ? String.Empty : skuiObj.SKUMaxItemsInOrder.ToString();

        // Conversions
        ucConversion.Value = skuiObj.SKUConversionName;
        txtConversionValue.Text = skuiObj.SKUConversionValue.ToString();

        // If editing a product option
        if (skuiObj.SKUOptionCategoryID > 0)
        {
            // Disable specific product type options
            this.selectProductTypeElem.AllowBundle = false;
            this.selectProductTypeElem.AllowDonation = false;
            this.selectProductTypeElem.Initialize();
        }

        // Load product type specific properties
        switch (skuiObj.SKUProductType)
        {
            // Initialize membership specific properties
            case SKUProductTypeEnum.Membership:
                this.membershipElem.MembershipGUID = skuiObj.SKUMembershipGUID;
                this.membershipElem.MembershipValidity = skuiObj.SKUValidity;

                if (skuiObj.SKUValidity == ValidityEnum.Until)
                {
                    this.membershipElem.MembershipValidUntil = skuiObj.SKUValidUntil;
                }
                else
                {
                    this.membershipElem.MembershipValidFor = skuiObj.SKUValidFor;
                }
                break;

            // Initialize e-product specific properties
            case SKUProductTypeEnum.EProduct:
                this.eProductElem.EProductValidity = skuiObj.SKUValidity;

                if (skuiObj.SKUValidity == ValidityEnum.Until)
                {
                    this.eProductElem.EProductValidUntil = skuiObj.SKUValidUntil;
                }
                else
                {
                    this.eProductElem.EProductValidFor = skuiObj.SKUValidFor;
                }
                break;

            // Initialize donation specific properties
            case SKUProductTypeEnum.Donation:
                this.donationElem.DonationIsPrivate = skuiObj.SKUPrivateDonation;

                // If minimum price is set
                if (skuiObj.GetDoubleValue("SKUMinPrice", -1) != -1)
                {
                    this.donationElem.MinimumDonationAmount = skuiObj.SKUMinPrice;
                }

                // If maximum price is set
                if (skuiObj.GetDoubleValue("SKUMaxPrice", -1) != -1)
                {
                    this.donationElem.MaximumDonationAmount = skuiObj.SKUMaxPrice;
                }
                break;

            // Initialize bundle specific properties
            case SKUProductTypeEnum.Bundle:
                this.bundleElem.RemoveFromInventory = skuiObj.SKUBundleInventoryType;
                break;
        }

        // Select product type
        this.selectProductTypeElem.Value = SKUInfoProvider.GetSKUProductTypeString(skuiObj.SKUProductType);
    }


    /// <summary>
    /// Saves edited SKU and returns it's ID. In case of error 0 is returned.
    /// </summary>
    public int Save()
    {
        int skuId = this.SaveInternal();

        // If SKU was saved succesfully, fire the product saved event
        if ((skuId != 0) && (this.ProductSaved != null))
        {
            this.ProductSaved(this, EventArgs.Empty);
        }

        // Return SKU ID
        return skuId;
    }


    /// <summary>
    /// Saves edited SKU and returns it's ID. In case of error 0 is returned. Does not fire product saved event.
    /// </summary>
    private int SaveInternal()
    {
        // Check permissions
        this.CheckModifyPermission();

        // If form is valid and enabled
        if (this.Validate() && this.FormEnabled)
        {
            bool newItem = false;

            // Get SKUInfo
            SKUInfo skuiObj = SKUInfoProvider.GetSKUInfo(mProductId);

            if (skuiObj == null)
            {
                newItem = true;

                // Create new item -> insert
                skuiObj = new SKUInfo();
                skuiObj.SKUSiteID = editedSiteId;
            }
            else
            {
                SKUProductTypeEnum oldProductType = skuiObj.SKUProductType;
                SKUProductTypeEnum newProductType = SKUInfoProvider.GetSKUProductTypeEnum((string)this.selectProductTypeElem.Value);

                // Remove e-product dependencies if required
                if ((oldProductType == SKUProductTypeEnum.EProduct) && (newProductType != SKUProductTypeEnum.EProduct))
                {
                    // Delete meta files
                    MetaFileInfoProvider.DeleteFiles(skuiObj.SKUID, ECommerceObjectType.SKU, MetaFileInfoProvider.OBJECT_CATEGORY_EPRODUCT);

                    // Delete SKU files
                    DataSet skuFiles = SKUFileInfoProvider.GetSKUFiles("FileSKUID = " + skuiObj.SKUID, null);

                    foreach (DataRow skuFile in skuFiles.Tables[0].Rows)
                    {
                        SKUFileInfo skufi = new SKUFileInfo(skuFile);
                        SKUFileInfoProvider.DeleteSKUFileInfo(skufi);
                    }
                }

                // Remove bundle dependencies if required
                if ((oldProductType == SKUProductTypeEnum.Bundle) && (newProductType != SKUProductTypeEnum.Bundle))
                {
                    // Delete SKU to bundle mappings
                    DataSet bundles = BundleInfoProvider.GetBundles("BundleID = " + skuiObj.SKUID, null);

                    foreach (DataRow bundle in bundles.Tables[0].Rows)
                    {
                        BundleInfo bi = new BundleInfo(bundle);
                        BundleInfoProvider.DeleteBundleInfo(bi);
                    }
                }
            }

            skuiObj.SKUName = this.txtSKUName.Text.Trim();
            skuiObj.SKUNumber = this.txtSKUNumber.Text.Trim();
            skuiObj.SKUDescription = this.htmlTemplateBody.ResolvedValue;
            skuiObj.SKUPrice = this.txtSKUPrice.Value;
            skuiObj.SKUEnabled = this.chkSKUEnabled.Checked;
            skuiObj.SKUInternalStatusID = this.internalStatusElem.InternalStatusID;
            skuiObj.SKUDepartmentID = this.departmentElem.DepartmentID;
            skuiObj.SKUManufacturerID = this.manufacturerElem.ManufacturerID;
            skuiObj.SKUPublicStatusID = this.publicStatusElem.PublicStatusID;
            skuiObj.SKUSupplierID = this.supplierElem.SupplierID;
            skuiObj.SKUSellOnlyAvailable = this.chkSKUSellOnlyAvailable.Checked;
            skuiObj.SKUNeedsShipping = this.chkNeedsShipping.Checked;
            skuiObj.SKUWeight = ValidationHelper.GetDouble(this.txtSKUWeight.Text.Trim(), 0);
            skuiObj.SKUHeight = ValidationHelper.GetDouble(this.txtSKUHeight.Text.Trim(), 0);
            skuiObj.SKUWidth = ValidationHelper.GetDouble(this.txtSKUWidth.Text.Trim(), 0);
            skuiObj.SKUDepth = ValidationHelper.GetDouble(this.txtSKUDepth.Text.Trim(), 0);
            skuiObj.SKUConversionName = ValidationHelper.GetString(this.ucConversion.Value, String.Empty);
            skuiObj.SKUConversionValue = this.txtConversionValue.Text.Trim();

            if (String.IsNullOrEmpty(this.txtSKUAvailableItems.Text.Trim()))
            {
                skuiObj.SetValue("SKUAvailableItems", null);
            }
            else
            {
                skuiObj.SKUAvailableItems = ValidationHelper.GetInteger(this.txtSKUAvailableItems.Text.Trim(), 0);
            }

            if (String.IsNullOrEmpty(this.txtSKUAvailableInDays.Text.Trim()))
            {
                skuiObj.SetValue("SKUAvailableInDays", null);
            }
            else
            {
                skuiObj.SKUAvailableInDays = ValidationHelper.GetInteger(this.txtSKUAvailableInDays.Text.Trim(), 0);
            }

            if (String.IsNullOrEmpty(this.txtMaxOrderItems.Text.Trim()))
            {
                skuiObj.SetValue("SKUMaxItemsInOrder", null);
            }
            else
            {
                skuiObj.SKUMaxItemsInOrder = ValidationHelper.GetInteger(this.txtMaxOrderItems.Text.Trim(), 0);
            }

            if (!ProductOrdered)
            {
                // Set product type
                skuiObj.SKUProductType = SKUInfoProvider.GetSKUProductTypeEnum((string)this.selectProductTypeElem.Value);
            }

            // Clear product type specific properties
            skuiObj.SetValue("SKUMembershipGUID", null);
            skuiObj.SetValue("SKUValidity", null);
            skuiObj.SetValue("SKUValidFor", null);
            skuiObj.SetValue("SKUValidUntil", null);
            skuiObj.SetValue("SKUMaxDownloads", null);
            skuiObj.SetValue("SKUBundleInventoryType", null);
            skuiObj.SetValue("SKUPrivateDonation", null);
            skuiObj.SetValue("SKUMinPrice", null);
            skuiObj.SetValue("SKUMaxPrice", null);

            // Set product type specific properties
            switch (skuiObj.SKUProductType)
            {
                // Set membership specific properties
                case SKUProductTypeEnum.Membership:
                    skuiObj.SKUMembershipGUID = this.membershipElem.MembershipGUID;
                    skuiObj.SKUValidity = this.membershipElem.MembershipValidity;

                    if (skuiObj.SKUValidity == ValidityEnum.Until)
                    {
                        skuiObj.SKUValidUntil = this.membershipElem.MembershipValidUntil;
                    }
                    else
                    {
                        skuiObj.SKUValidFor = this.membershipElem.MembershipValidFor;
                    }
                    break;

                // Set e-product specific properties
                case SKUProductTypeEnum.EProduct:
                    skuiObj.SKUValidity = this.eProductElem.EProductValidity;

                    if (skuiObj.SKUValidity == ValidityEnum.Until)
                    {
                        skuiObj.SKUValidUntil = this.eProductElem.EProductValidUntil;
                    }
                    else
                    {
                        skuiObj.SKUValidFor = this.eProductElem.EProductValidFor;
                    }
                    break;

                // Set donation specific properties
                case SKUProductTypeEnum.Donation:
                    skuiObj.SKUPrivateDonation = this.donationElem.DonationIsPrivate;

                    if (this.donationElem.MinimumDonationAmount == 0.0)
                    {
                        skuiObj.SetValue("SKUMinPrice", null);
                    }
                    else
                    {
                        skuiObj.SKUMinPrice = this.donationElem.MinimumDonationAmount;
                    }

                    if (this.donationElem.MaximumDonationAmount == 0.0)
                    {
                        skuiObj.SetValue("SKUMaxPrice", null);
                    }
                    else
                    {
                        skuiObj.SKUMaxPrice = this.donationElem.MaximumDonationAmount;
                    }
                    break;

                // Set bundle specific properties
                case SKUProductTypeEnum.Bundle:
                    skuiObj.SKUBundleInventoryType = this.bundleElem.RemoveFromInventory;
                    break;
            }

            // When creating new product option
            if ((this.ProductID == 0) && (this.OptionCategoryID > 0))
            {
                skuiObj.SKUOptionCategoryID = this.OptionCategoryID;
            }

            if ((newItem) && (!SKUInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Ecommerce, VersionActionEnum.Insert)))
            {
                lblError.Visible = true;
                lblError.Text = GetString("ecommerceproduct.versioncheck");

                return 0;
            }

            SKUInfoProvider.SetSKUInfo(skuiObj);

            if (newItem)
            {
                if (ECommerceSettings.UseMetaFileForProductImage)
                {
                    // Get allowed extensions
                    string settingKey = (skuiObj.IsGlobal) ? "CMSUploadExtensions" : (CMSContext.CurrentSiteName + ".CMSUploadExtensions");
                    string allowedExtensions = SettingsKeyProvider.GetStringValue(settingKey);

                    // Get posted file
                    HttpPostedFile file = this.ucMetaFile.PostedFile;

                    if ((file != null) && (file.ContentLength > 0))
                    {
                        // Get file extension
                        string extension = Path.GetExtension(file.FileName);

                        // Check if file is an image and its extension is allowed
                        if (ImageHelper.IsImage(extension) && (String.IsNullOrEmpty(allowedExtensions) || FileHelper.CheckExtension(extension, allowedExtensions)))
                        {
                            // Upload SKU image meta file
                            this.ucMetaFile.ObjectID = skuiObj.SKUID;
                            this.ucMetaFile.UploadFile();

                            // Update SKU image path
                            this.UpdateSKUImagePath(skuiObj);
                        }
                        else
                        {
                            // Set error message
                            string error = ValidationHelper.GetString(SessionHelper.GetValue("NewProductError"), null);
                            error += ";" + String.Format(this.GetString("com.productedit.invalidproductimage"), extension);
                            SessionHelper.SetValue("NewProductError", error);
                        }
                    }
                }
                else
                {
                    skuiObj.SKUImagePath = this.imgSelect.Value;
                }

                // Upload initial e-product file
                if (skuiObj.SKUProductType == SKUProductTypeEnum.EProduct)
                {
                    this.eProductElem.SKUID = skuiObj.SKUID;
                    this.eProductElem.UploadNewProductFile();
                }
            }
            else
            {
                // Update SKU image path
                UpdateSKUImagePath(skuiObj);
            }

            SKUInfoProvider.SetSKUInfo(skuiObj);

            if ((mNodeId > 0) && (mProductId == 0))
            {
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                TreeNode node = tree.SelectSingleNode(mNodeId, TreeProvider.ALL_CULTURES);
                node.NodeSKUID = skuiObj.SKUID;
                node.Update();

                // Update search index for node
                if ((node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
                {
                    SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
                }

                // Log synchronization
                DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);

                // Ensure new SKU values
                SKUInfoProvider.SetSKUInfo(skuiObj);
            }

            // If SKU is of bundle product type and bundle does not exist yet
            if ((skuiObj.SKUProductType == SKUProductTypeEnum.Bundle) && (this.bundleElem.BundleID == 0))
            {
                // Set bundle ID
                this.bundleElem.BundleID = skuiObj.SKUID;

                // Save selected products
                this.bundleElem.SaveProductsSelectionChanges();
            }

            this.ProductID = skuiObj.SKUID;

            // Reload form
            this.LoadData(skuiObj);

            // Set changes saved message
            this.lblInfo.Text = this.GetString("general.changessaved");

            return ValidationHelper.GetInteger(skuiObj.SKUID, 0);
        }
        else
        {
            return 0;
        }
    }


    private bool Validate()
    {
        string errorMessage = String.Empty;

        // Validate name
        errorMessage = new Validator().NotEmpty(this.txtSKUName.Text.Trim(), this.rfvSKUName.ErrorMessage).Result;

        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.ErrorMessage = errorMessage;
            return false;
        }

        // Validate price
        errorMessage = this.txtSKUPrice.ValidatePrice(this.OptionCategoryID > 0);

        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.ErrorMessage = errorMessage;
            return false;
        }

        // If global meta files should be stored in filesystem
        if (this.ucMetaFile.Visible && (this.ucMetaFile.PostedFile != null) && MetaFileInfoProvider.StoreFilesInFileSystem(null))
        {
            // Get product image path
            string path = MetaFileInfoProvider.GetFilesFolderPath(null);

            // Check permission for image folder
            if (!DirectoryHelper.CheckPermissions(path))
            {
                this.ErrorMessage = String.Format(this.GetString("com.newproduct.accessdeniedtopath"), path);
                return false;
            }
        }

        // Validate product type specific properties
        switch (SKUInfoProvider.GetSKUProductTypeEnum((string)this.selectProductTypeElem.Value))
        {
            case SKUProductTypeEnum.Membership:
                errorMessage = this.membershipElem.Validate();
                break;

            case SKUProductTypeEnum.EProduct:
                errorMessage = this.eProductElem.Validate();
                break;

            case SKUProductTypeEnum.Donation:
                errorMessage = this.donationElem.Validate();

                // If price does not fall into the range specified by minimum and maximum donation amount
                if (((this.donationElem.MinimumDonationAmount > 0.0) && (this.txtSKUPrice.Value < this.donationElem.MinimumDonationAmount))
                    || ((this.donationElem.MaximumDonationAmount > 0.0) && (this.txtSKUPrice.Value > this.donationElem.MaximumDonationAmount)))
                {
                    this.ErrorMessage = this.GetString("com.productedit.donationpriceinvalid");
                    return false;
                }
                break;

            case SKUProductTypeEnum.Bundle:
                errorMessage = this.bundleElem.Validate();
                break;
        }

        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.ErrorMessage = errorMessage;
            return false;
        }

        string temp = String.Empty;

        // Validate weight
        temp = this.txtSKUWeight.Text.Trim();
        if (!String.IsNullOrEmpty(temp))
        {
            double skuWeight = ValidationHelper.GetDouble(temp, 0);

            if ((skuWeight == 0) || (!ValidationHelper.IsPositiveNumber(temp)))
            {
                this.ErrorMessage = this.GetString("com.productedit.packageweightinvalid");
                return false;
            }
        }

        // Validate height
        temp = this.txtSKUHeight.Text.Trim();
        if (!String.IsNullOrEmpty(temp))
        {
            double skuHeight = ValidationHelper.GetDouble(temp, 0);

            if ((skuHeight == 0) || (!ValidationHelper.IsPositiveNumber(temp)))
            {
                this.ErrorMessage = this.GetString("com.productedit.packageheightinvalid");
                return false;
            }
        }

        // Validate width
        temp = this.txtSKUWidth.Text.Trim();
        if (!String.IsNullOrEmpty(temp))
        {
            double skuWidth = ValidationHelper.GetDouble(temp, 0);

            if ((skuWidth == 0) || (!ValidationHelper.IsPositiveNumber(temp)))
            {
                this.ErrorMessage = this.GetString("com.productedit.packagewidthinvalid");
                return false;
            }
        }

        // Validate depth
        temp = this.txtSKUDepth.Text.Trim();
        if (!String.IsNullOrEmpty(temp))
        {
            double skuDepth = ValidationHelper.GetDouble(temp, 0);

            if ((skuDepth == 0) || (!ValidationHelper.IsPositiveNumber(temp)))
            {
                this.ErrorMessage = this.GetString("com.productedit.packagedepthinvalid");
                return false;
            }
        }

        // Validate available items
        temp = this.txtSKUAvailableItems.Text.Trim();
        if (!String.IsNullOrEmpty(temp) && !ValidationHelper.IsInteger(temp))
        {
            this.ErrorMessage = this.GetString("com.productedit.availableitemsinvalid");
            return false;
        }

        // Validate availability
        temp = this.txtSKUAvailableInDays.Text.Trim();
        if (!String.IsNullOrEmpty(temp) && !ValidationHelper.IsInteger(temp))
        {
            this.ErrorMessage = this.GetString("com.productedit.availabilityinvalid");
            return false;
        }

        // Validate max items in one order
        temp = this.txtMaxOrderItems.Text.Trim();
        if (!String.IsNullOrEmpty(temp) && (!ValidationHelper.IsInteger(temp) || (ValidationHelper.GetInteger(temp, 0) <= 0)))
        {
            this.ErrorMessage = this.GetString("com.productedit.maxorderitemsinvalid");
            return false;
        }

        // Validate conversion name 
        if (!ucConversion.IsValid())
        {
            this.ErrorMessage = GetString("conversion.validcodename");
            return false;
        }

        // Form is valid
        return true;
    }


    private void UpdateSKUImagePath(SKUInfo skuObj)
    {
        if (ECommerceSettings.UseMetaFileForProductImage && !hasAttachmentImagePath)
        {
            // Update product image path according to its meta file
            DataSet ds = MetaFileInfoProvider.GetMetaFiles(ucMetaFile.ObjectID, skuObj.TypeInfo.ObjectType, MetaFileInfoProvider.OBJECT_CATEGORY_IMAGE, null, null);

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                MetaFileInfo metaFile = new MetaFileInfo(ds.Tables[0].Rows[0]);
                skuObj.SKUImagePath = MetaFileInfoProvider.GetMetaFileUrl(metaFile.MetaFileGUID, metaFile.MetaFileName);
            }
            else
            {
                skuObj.SKUImagePath = "";
            }
        }
        else
        {
            // Update product image path from the image selector
            skuObj.SKUImagePath = imgSelect.Value;
        }
    }


    void ucMetaFile_OnAfterDelete(object sender, EventArgs e)
    {
        SKUInfo skui = SKUInfoProvider.GetSKUInfo(this.ProductID);

        if (skui != null)
        {
            // Update SKU image path
            this.UpdateSKUImagePath(skui);
            SKUInfoProvider.SetSKUInfo(skui);
        }
    }


    /// <summary>
    /// Checks whether SKU belonging to site editedSiteId can be modified by current user. 
    /// </summary>
    private void CheckModifyPermission()
    {
        bool global = (editedSiteId <= 0);
        bool authorized = (OptionCategoryID > 0) ? ECommerceContext.IsUserAuthorizedToModifyOptionCategory(global) : ECommerceContext.IsUserAuthorizedToModifySKU(global);

        if (!authorized)
        {
            // Check module permissions
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


    /// <summary>
    /// Sets values to the form fields.
    /// </summary>
    public void SetValues()
    {
        txtSKUName.Text = mProductName;
        txtSKUHeight.Text = (mProductHeight > 0) ? mProductHeight.ToString() : "";
        txtSKUWeight.Text = (mProductWeight > 0) ? mProductWeight.ToString() : "";
        txtSKUWidth.Text = (mProductWidth > 0) ? mProductWidth.ToString() : "";
        txtSKUDepth.Text = (mProductDepth > 0) ? mProductDepth.ToString() : "";
        htmlTemplateBody.ResolvedValue = mProductDescription;
        imgSelect.Value = mProductImagePath;
    }


    /// <summary>
    /// Loads defaults if product data were not reloaded recently.
    /// </summary>
    private void LoadDefaults()
    {
        this.chkNeedsShipping.Checked = false;

        // Load product type specific defaults
        switch (SKUInfoProvider.GetSKUProductTypeEnum((string)this.selectProductTypeElem.Value))
        {
            case SKUProductTypeEnum.Product:
                this.chkNeedsShipping.Checked = true;
                break;

            case SKUProductTypeEnum.Membership:
                break;

            case SKUProductTypeEnum.EProduct:
                break;

            case SKUProductTypeEnum.Donation:
                break;

            case SKUProductTypeEnum.Bundle:
                this.chkNeedsShipping.Checked = true;
                break;
        }
    }


    protected void selectProductTypeElem_OnSelectionChanged(object sender, EventArgs e)
    {
        // Show product type specific properties
        switch (SKUInfoProvider.GetSKUProductTypeEnum((string)this.selectProductTypeElem.Value))
        {
            case SKUProductTypeEnum.Product:
                this.typeSpecificOptionsElem.ActiveViewIndex = -1;
                break;

            case SKUProductTypeEnum.Membership:
                this.typeSpecificOptionsElem.SetActiveView(this.membershipViewElem);
                break;

            case SKUProductTypeEnum.EProduct:
                this.typeSpecificOptionsElem.SetActiveView(this.eProductViewElem);
                break;

            case SKUProductTypeEnum.Donation:
                this.typeSpecificOptionsElem.SetActiveView(this.donationViewElem);
                break;

            case SKUProductTypeEnum.Bundle:
                this.typeSpecificOptionsElem.SetActiveView(this.bundleViewElem);
                this.bundleElem.ReloadRequired = true;
                break;
        }

        // If data was not reloaded recently
        if (!ValidationHelper.GetBoolean(this.ViewState["DataReloaded"], false))
        {
            this.LoadDefaults();
        }

        // Clear data reloaded flag
        this.ViewState["DataReloaded"] = false;
    }


    protected void membershipElem_OnValidityChanged(object sender, EventArgs e)
    {
        this.LoadDefaults();
    }


    protected void eProductElem_OnValidityChanged(object sender, EventArgs e)
    {
        this.LoadDefaults();
    }


    protected void eProductElem_OnBeforeUpload(object sender, EventArgs e)
    {
        // If uploading to nonexistent SKU
        if (this.eProductElem.SKUID == 0)
        {
            // Set the flag for later use
            this.uploadToNonexistentSku = true;

            // Save the product and set the SKU ID
            this.eProductElem.SKUID = this.SaveInternal();
        }
    }


    protected void eProductElem_OnAfterUpload(object sender, EventArgs e)
    {
        // If upload was performed for nonexistent SKU and SKU was saved succesfully
        if (this.uploadToNonexistentSku && (this.eProductElem.SKUID != 0))
        {
            // Fire the product saved event
            if (this.ProductSaved != null)
            {
                this.ProductSaved(this, EventArgs.Empty);
            }
        }
    }


    protected void bundleElem_OnProductsSelectionChangesSaved(object sender, EventArgs e)
    {
        // Set changes saved message
        this.lblInfo.Text = this.GetString("general.changessaved");
    }

    #endregion
}
