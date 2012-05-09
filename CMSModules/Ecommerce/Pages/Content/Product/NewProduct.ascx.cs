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

using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.SiteProvider;
using CMS.IO;
using CMS.UIControls;
using CMS.Synchronization;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Ecommerce_Pages_Content_Product_NewProduct : CMSUserControl, INewProductControl
{

    #region "Variables"

    private int mClassID = 0;
    private DataClassInfo mClassObj = null;
    private TreeNode mNode = null;
    private int mSiteId = -1;

    #endregion


    #region "Properties"

    /// <summary>
    /// Class ID.
    /// </summary>
    public int ClassID
    {
        get
        {
            return mClassID;
        }
        set
        {
            mClassID = value;
        }
    }


    /// <summary>
    /// Product document node.
    /// </summary>
    public TreeNode Node
    {
        get
        {
            return mNode;
        }
        set
        {
            mNode = value;
        }
    }


    /// <summary>
    /// Class data.
    /// </summary>
    public DataClassInfo ClassObj
    {
        get
        {
            if (mClassObj == null)
            {
                mClassObj = DataClassInfoProvider.GetDataClass(this.ClassID);
            }
            return mClassObj;
        }
    }


    /// <summary>
    /// Site ID of the newly created.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteId < 0)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize HTML editor
        htmlSKUDescription.AutoDetectLanguage = false;
        htmlSKUDescription.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlSKUDescription.EditorAreaCSS = FormHelper.GetHtmlEditorAreaCss(CMSContext.CurrentSiteName);
        htmlSKUDescription.ToolbarSet = "Basic";

        // Initialize labels			                
        lblSKUName.Text = GetString("com_sku_edit_general.skunamelabel");
        lblSKUPrice.Text = GetString("com_sku_edit_general.skupricelabel");
        lblSKUImagePath.Text = GetString("com.newproduct.lblskuimage");
        lblSKUDepartment.Text = GetString("com_sku_edit_general.skudepartmentidlabel");
        lblSKUImagePathSelect.Text = GetString("com.newproduct.skuimagepath");

        txtSKUPrice.EmptyErrorMessage = GetString("com.newproduct.skupriceempty");
        txtSKUPrice.ValidationErrorMessage = GetString("com.newproduct.skupricenotdouble");


        if (this.ClassObj != null)
        {
            // If product should be created automatically
            if (this.ClassObj.ClassCreateSKU)
            {
                // Hide all controls
                plcSKUControls.Visible = false;
            }
            // If product should not be created automatically
            else
            {
                // Hide only some controls according to the Document-SKU bindings
                HideControls();

                // Load controls data
                LoadData();
            }
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Loads data.
    /// </summary>
    private void LoadData()
    {
        // Get current user
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (user != null)
        {
            // If global administrator -> show all departments
            if (!user.IsGlobalAdministrator)
            {
                departmentElem.UserID = user.UserID;
            }
        }
    }


    /// <summary>
    /// Hide some controls according to the Document-SKU bindings.
    /// </summary>
    private void HideControls()
    {
        if (this.ClassObj != null)
        {
            plcSKUName.Visible = !IsSKUColumnMapped("SKUName");
            plcSKUPrice.Visible = !IsSKUColumnMapped("SKUPrice");
            plcSKUDescription.Visible = !IsSKUColumnMapped("SKUDescription");
            plcMetaFile.Visible = !IsSKUColumnMapped("SKUImagePath") && ECommerceSettings.UseMetaFileForProductImage;
            plcImagePath.Visible = !IsSKUColumnMapped("SKUImagePath") && !ECommerceSettings.UseMetaFileForProductImage;
        }
    }


    /// <summary>
    /// Determines whether specified SKU column is mapped to some document field.
    /// </summary>
    /// <param name="skuColumnName">SKU column name</param>
    private bool IsSKUColumnMapped(string skuColumnName)
    {
        return (GetDocumentMappedField(skuColumnName) != "");
    }


    /// <summary>
    /// Returns column name of the document which is mapped to the specified SKU column.
    /// </summary>
    /// <param name="skuColumnName">SKU column name</param>
    private string GetDocumentMappedField(string skuColumnName)
    {
        return ValidationHelper.GetString(this.ClassObj.SKUMappings[skuColumnName], "");
    }


    /// <summary>
    /// Validates form data and returns TRUE if succeeded, otherwise returns FALSE.
    /// </summary>
    public bool ValidateData()
    {
        if (plcSKUControls.Visible)
        {
            string error = "";

            if (!SKUInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Ecommerce, VersionActionEnum.Insert))
            {
                error = GetString("ecommerceproduct.versioncheck");
            }

            // If global meta files should be stored in filesystem
            if ((error == "") && ucMetaFile.Visible && (ucMetaFile.PostedFile != null) && MetaFileInfoProvider.StoreFilesInFileSystem(null))
            {
                // Get product image path
                string path = MetaFileInfoProvider.GetFilesFolderPath(null);

                // Check permission for image folder
                if (!DirectoryHelper.CheckPermissions(path))
                {
                    error = String.Format(GetString("com.newproduct.accessdeniedtopath"), path);
                }
            }


            // Validate SKU name
            if ((error == "") && (txtSKUName.Visible) && (txtSKUName.Text.Trim() == ""))
            {
                // SKU name not entered
                error = GetString("com.newproduct.skunameempty");
            }


            if (error == "")
            {
                // Validate SKU price
                if (txtSKUPrice.Visible)
                {
                    error = txtSKUPrice.ValidatePrice(false);
                }
            }

            // Show error message
            if (error != "")
            {
                lblError.Visible = true;
                lblError.Text = error;
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// Saves SKU data and returns created SKU object.
    /// </summary>
    public SKUInfo SaveData()
    {
        // Check permissions
        if (SiteID > 0)
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

        if ((plcSKUControls.Visible) && (this.Node != null))
        {
            // Create empty SKU object
            SKUInfo skuObj = new SKUInfo();

            // Set SKU site id
            skuObj.SKUSiteID = SiteID;

            // Set SKU Name
            if (plcSKUName.Visible)
            {
                skuObj.SKUName = txtSKUName.Text.Trim();
            }
            else
            {
                string skuNameField = GetDocumentMappedField("SKUName");
                skuObj.SKUName = ValidationHelper.GetString(this.Node.GetValue(skuNameField), "");
            }

            // Set SKU price
            if (plcSKUPrice.Visible)
            {
                skuObj.SKUPrice = txtSKUPrice.Value;
            }
            else
            {
                string skuPriceField = GetDocumentMappedField("SKUPrice");
                skuObj.SKUPrice = ValidationHelper.GetDouble(this.Node.GetValue(skuPriceField), 0);
            }

            // Set SKU image path according to the document binding
            if (!plcMetaFile.Visible && !plcImagePath.Visible)
            {
                string skuImageField = GetDocumentMappedField("SKUImagePath");
                skuObj.SKUImagePath = ValidationHelper.GetString(this.Node.GetValue(skuImageField), "");
            }

            // Set SKU description
            if (plcSKUDescription.Visible)
            {
                skuObj.SKUDescription = htmlSKUDescription.Value;
            }
            else
            {
                string skuDescriptionField = GetDocumentMappedField("SKUDescription");
                skuObj.SKUDescription = ValidationHelper.GetString(this.Node.GetValue(skuDescriptionField), "");
            }

            // Set SKU department
            skuObj.SKUDepartmentID = departmentElem.DepartmentID;

            skuObj.SKUEnabled = true;

            // Create new SKU
            SKUInfoProvider.SetSKUInfo(skuObj);

            if ((plcImagePath.Visible || plcMetaFile.Visible) && (skuObj.SKUID > 0))
            {
                if (ECommerceSettings.UseMetaFileForProductImage)
                {
                    // Save meta file
                    ucMetaFile.ObjectID = skuObj.SKUID;
                    ucMetaFile.ObjectType = ECommerceObjectType.SKU;
                    ucMetaFile.Category = MetaFileInfoProvider.OBJECT_CATEGORY_IMAGE;
                    ucMetaFile.UploadFile();

                    // Update product image path according to its meta file
                    DataSet ds = MetaFileInfoProvider.GetMetaFiles(ucMetaFile.ObjectID, skuObj.TypeInfo.ObjectType);
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        // Set product image path
                        MetaFileInfo metaFile = new MetaFileInfo(ds.Tables[0].Rows[0]);
                        skuObj.SKUImagePath = MetaFileInfoProvider.GetMetaFileUrl(metaFile.MetaFileGUID, metaFile.MetaFileName);
                    }
                }
                else
                {
                    skuObj.SKUImagePath = this.imgSelect.Value;
                }

                // Update product
                SKUInfoProvider.SetSKUInfo(skuObj);
            }

            return skuObj;
        }
        return null;
    }


    /// <summary>
    /// Saves SKU data and returns created SKU object.
    /// </summary>
    GeneralizedInfo INewProductControl.SaveData()
    {
        return SaveData();
    }

    #endregion
}
