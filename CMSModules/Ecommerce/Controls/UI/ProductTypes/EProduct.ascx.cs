using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.IO;

public partial class CMSModules_Ecommerce_Controls_UI_ProductTypes_EProduct : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// SKU ID.
    /// </summary>
    public int SKUID
    {
        get;
        set;
    }


    /// <summary>
    /// E-product validity type.
    /// </summary>
    public ValidityEnum EProductValidity
    {
        get
        {
            return this.selectValidityElem.Validity;
        }
        set
        {
            this.selectValidityElem.Validity = value;
        }
    }


    /// <summary>
    /// E-product valid for multiplier.
    /// </summary>
    public int EProductValidFor
    {
        get
        {
            return this.selectValidityElem.ValidFor;
        }
        set
        {
            this.selectValidityElem.ValidFor = value;
        }
    }


    /// <summary>
    /// E-product valid until date and time.
    /// </summary>
    public DateTime EProductValidUntil
    {
        get
        {
            if (this.selectValidityElem.ValidUntil.Equals(DataHelper.DATETIME_NOT_SELECTED))
            {
                // Return default value
                return DataHelper.DATETIME_NOT_SELECTED;
            }
            else
            {
                // Return date with midnight time
                return new DateTime(this.selectValidityElem.ValidUntil.Year, this.selectValidityElem.ValidUntil.Month, this.selectValidityElem.ValidUntil.Day, 23, 59, 59);
            }
        }
        set
        {
            this.selectValidityElem.ValidUntil = value;
        }
    }


    /// <summary>
    /// Site ID.
    /// </summary>
    public int SiteID
    {
        get;
        set;
    }


    /// <summary>
    /// Error message.
    /// </summary>
    public string ErrorMessage
    {
        get;
        set;
    }

    #endregion


    #region "Events"

    public event EventHandler OnValidityChanged;

    public event EventHandler OnBeforeUpload;

    public event EventHandler OnAfterUpload;

    #endregion


    #region "Page methods"

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (this.StopProcessing)
        {
            this.fileListElem.StopProcessing = true;
            return;
        }

        // If product does not exist yet
        if (this.SKUID == 0)
        {
            // Initialize new product file uploader
            this.plcNewProductUploader.Visible = true;

            this.newProductFileUploader.ObjectType = ECommerceObjectType.SKU;
            this.newProductFileUploader.Category = MetaFileInfoProvider.OBJECT_CATEGORY_EPRODUCT;
            this.newProductFileUploader.SiteID = this.SiteID;

            // Do not use file list control
            this.plcFileList.Visible = false;
            this.fileListElem.StopProcessing = true;
        }
        else
        {
            // Initialize file list control
            this.fileListElem.ObjectID = this.SKUID;
            this.fileListElem.ObjectType = ECommerceObjectType.SKU;
            this.fileListElem.Category = MetaFileInfoProvider.OBJECT_CATEGORY_EPRODUCT;
            this.fileListElem.SiteID = this.SiteID;
            this.fileListElem.OnBeforeUpload += new CancelEventHandler(fileListElem_OnBeforeUpload);
            this.fileListElem.OnAfterUpload += new EventHandler(fileListElem_OnAfterUpload);
            this.fileListElem.OnBeforeDelete += new CancelEventHandler(fileListElem_OnBeforeDelete);
        }

        // Enable uploaders if sufficient SKU modify permissions
        if (ECommerceContext.IsUserAuthorizedToModifySKU(this.SiteID == 0))
        {
            this.fileListElem.AllowEdit = true;
            this.newProductFileUploader.Enabled = true;
        }

    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show attachment error message if set
        this.lblAttachmentError.Visible = !String.IsNullOrEmpty(this.lblAttachmentError.Text) || !String.IsNullOrEmpty(this.lblAttachmentError.ResourceString);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Uploads file with new product uploader.
    /// </summary>
    public void UploadNewProductFile()
    {
        // Get SKU
        SKUInfo skui = SKUInfoProvider.GetSKUInfo(this.SKUID);

        // Get allowed extensions
        string allowedExtensions = null;

        if (skui != null)
        {
            string settingKey = (skui.IsGlobal) ? "CMSUploadExtensions" : (CMSContext.CurrentSiteName + ".CMSUploadExtensions");
            allowedExtensions = SettingsKeyProvider.GetStringValue(settingKey);
        }

        // Get posted file
        HttpPostedFile file = this.newProductFileUploader.PostedFile;

        if ((file != null) && (file.ContentLength > 0) && !String.IsNullOrEmpty(allowedExtensions))
        {
            // Get file extension
            string extension = Path.GetExtension(file.FileName); ;

            // If file extension is not allowed
            if (!FileHelper.CheckExtension(extension, allowedExtensions))
            {
                // Set error message and don't upload
                string error = ValidationHelper.GetString(SessionHelper.GetValue("NewProductError"), null);
                error += ";" + String.Format(this.GetString("com.eproduct.attachmentinvalid"), allowedExtensions.Replace(";", ", "));
                SessionHelper.SetValue("NewProductError", error);
                return;
            }
        }

        // Upload attachment
        this.newProductFileUploader.ObjectID = this.SKUID;
        this.newProductFileUploader.UploadFile();

        // Get uploaded meta file
        MetaFileInfo mfi = this.newProductFileUploader.CurrentlyHandledMetaFile;

        if (mfi != null)
        {
            // Create new SKU file
            SKUFileInfo skufi = new SKUFileInfo();

            skufi.FileSKUID = this.SKUID;
            skufi.FileMetaFileGUID = mfi.MetaFileGUID;
            skufi.FileName = mfi.MetaFileName;
            skufi.FilePath = MetaFileInfoProvider.GetMetaFileUrl(mfi.MetaFileGUID, mfi.MetaFileName);
            skufi.FileType = MediaSourceEnum.MetaFile.ToString();

            // Save SKU file
            SKUFileInfoProvider.SetSKUFileInfo(skufi);
        }
    }


    /// <summary>
    /// Validates form and returns string with error messages.
    /// </summary>
    public string Validate()
    {
        this.ErrorMessage = String.Empty;

        // Validate e-product validity
        if (String.IsNullOrEmpty(this.ErrorMessage))
        {
            this.ErrorMessage = this.selectValidityElem.Validate();
        }

        return this.ErrorMessage;
    }


    protected void selectValidityElem_OnValidityChanged(object sender, EventArgs e)
    {
        // Bubble up the validity changed event
        if (this.OnValidityChanged != null)
        {
            this.OnValidityChanged(this, EventArgs.Empty);
        }
    }


    void fileListElem_OnBeforeUpload(object sender, CancelEventArgs e)
    {
        // Bubble up the before upload event
        if (this.OnBeforeUpload != null)
        {
            this.OnBeforeUpload(this, EventArgs.Empty);
        }

        // If SKU ID not set
        if (this.SKUID == 0)
        {
            // Cancel upload
            e.Cancel = true;
        }
        else
        {
            this.fileListElem.ObjectID = this.SKUID;
        }
    }


    void fileListElem_OnAfterUpload(object sender, EventArgs e)
    {
        // Get uploaded meta file
        MetaFileInfo mfi = this.fileListElem.CurrentlyHandledMetaFile;

        SKUFileInfo skufi = null;

        // Get SKU file for this meta file
        DataSet skuFiles = SKUFileInfoProvider.GetSKUFiles(String.Format("FileMetaFileGUID = '{0}'", mfi.MetaFileGUID), null);

        // If SKU file does not exist
        if (DataHelper.DataSourceIsEmpty(skuFiles))
        {
            // Create new SKU file
            skufi = new SKUFileInfo();
            skufi.FileSKUID = this.SKUID;
            skufi.FileMetaFileGUID = mfi.MetaFileGUID;
        }
        // If SKU file exists
        else
        {
            // Get existing SKU file
            skufi = new SKUFileInfo(skuFiles.Tables[0].Rows[0]);
        }

        skufi.FileName = mfi.MetaFileName;
        skufi.FilePath = MetaFileInfoProvider.GetMetaFileUrl(mfi.MetaFileGUID, mfi.MetaFileName);
        skufi.FileType = MediaSourceEnum.MetaFile.ToString();

        // Save SKU file
        SKUFileInfoProvider.SetSKUFileInfo(skufi);

        // Bubble up the after upload event
        if (this.OnAfterUpload != null)
        {
            this.OnAfterUpload(this, EventArgs.Empty);
        }
    }


    void fileListElem_OnBeforeDelete(object sender, CancelEventArgs e)
    {
        // Get meta file that is being deleted
        MetaFileInfo mfi = this.fileListElem.CurrentlyHandledMetaFile;

        // Get SKU files with meta file's GUID
        DataSet skuFiles = SKUFileInfoProvider.GetSKUFiles(String.Format("FileMetaFileGUID = '{0}'", mfi.MetaFileGUID), null);

        if (!DataHelper.DataSourceIsEmpty(skuFiles))
        {
            // Get SKU file
            SKUFileInfo skufi = new SKUFileInfo(skuFiles.Tables[0].Rows[0]);

            if (skufi != null)
            {
                // If SKU file has no dependencies
                if (!SKUFileInfoProvider.CheckDependencies(skufi))
                {
                    // Delete SKU file information
                    SKUFileInfoProvider.DeleteSKUFileInfo(skufi);
                }
                else
                {
                    // Set error message about existing dependency
                    this.ErrorMessage = this.GetString("ecommerce.deletedisabled");
                    this.lblAttachmentError.Text = this.ErrorMessage;

                    // Cancel delete
                    e.Cancel = true;
                }
            }
        }
    }

    #endregion
}
