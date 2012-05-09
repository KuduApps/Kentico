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

public partial class CMSModules_Ecommerce_Pages_Tools_Manufacturers_Manufacturer_Edit : CMSManufacturersPage
{
    #region "Variables"

    protected int manufacturerId = 0;
    protected int editedSiteId = -1;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Field validator error messages initialization
        rfvDisplayName.ErrorMessage = GetString("manufacturer_Edit.errorEmptyDisplayName");

        // Control initializations				
        lblManufacturerDisplayName.Text = GetString("Manufacturer_Edit.ManufacturerDisplayNameLabel");
        lblManufactureHomepage.Text = GetString("Manufacturer_Edit.ManufactureHomepageLabel");
        btnOk.Text = GetString("General.OK");

        string currentManufacturer = GetString("Manufacturer_Edit.NewItemCaption");

        // Get manufacturer id from querystring		
        manufacturerId = QueryHelper.GetInteger("manufacturerId", 0);
        editedSiteId = ConfiguredSiteID;
        if (manufacturerId > 0)
        {
            ManufacturerInfo manufacturerObj = ManufacturerInfoProvider.GetManufacturerInfo(manufacturerId);
            EditedObject = manufacturerObj;

            if (manufacturerObj != null)
            {
                currentManufacturer = manufacturerObj.ManufacturerDisplayName;

                editedSiteId = manufacturerObj.ManufacturerSiteID;

                // Check edited object site ID
                CheckEditedObjectSiteID(editedSiteId);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(manufacturerObj);

                    // Show that the manufacturer was created or updated successfully
                    if (ValidationHelper.GetString(Request.QueryString["saved"], "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }

        this.CurrentMaster.Title.HelpTopicName = "newedit_manufacturer";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Manufacturer_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Manufacturers/Manufacturer_List.aspx?siteId=" + this.SiteID;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(currentManufacturer, editedSiteId);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Set master title
        if (manufacturerId > 0)
        {
            this.CurrentMaster.Title.TitleText = GetString("com.manufacturer.edit");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Manufacturer/object.png");
        }
        else
        {
            this.CurrentMaster.Title.TitleText = GetString("Manufacturer_List.NewItemCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Manufacturer/new.png");
        }

        AddMenuButtonSelectScript("Manufacturers", "");
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Load data of editing manufacturer.
    /// </summary>
    /// <param name="manufacturerObj">Manufacturer object</param>
    protected void LoadData(ManufacturerInfo manufacturerObj)
    {
        txtManufacturerDisplayName.Text = manufacturerObj.ManufacturerDisplayName;
        txtManufactureHomepage.Text = manufacturerObj.ManufactureHomepage;
        chkManufacturerEnabled.Checked = manufacturerObj.ManufacturerEnabled;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (editedSiteId <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifyManufacturer(global))
        {
            if (global)
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyManufacturers");
            }
        }

        // Check input values from textboxes and other controls - new Validator()
        string errorMessage = new Validator().NotEmpty(txtManufacturerDisplayName.Text.Trim(), GetString("manufacturer_Edit.errorEmptyDisplayName")).Result;

        if (errorMessage == "")
        {
            ManufacturerInfo manufacturerObj = ManufacturerInfoProvider.GetManufacturerInfo(manufacturerId);

            if (manufacturerObj == null)
            {
                manufacturerObj = new ManufacturerInfo();
                // Assign site ID
                manufacturerObj.ManufacturerSiteID = ConfiguredSiteID;
            }

            manufacturerObj.ManufacturerID = manufacturerId;
            manufacturerObj.ManufacturerDisplayName = txtManufacturerDisplayName.Text.Trim();
            manufacturerObj.ManufactureHomepage = txtManufactureHomepage.Text.Trim();
            manufacturerObj.ManufacturerEnabled = chkManufacturerEnabled.Checked;

            // Save changes
            ManufacturerInfoProvider.SetManufacturerInfo(manufacturerObj);

            URLHelper.Redirect("Manufacturer_Edit.aspx?manufacturerId=" + Convert.ToString(manufacturerObj.ManufacturerID) + "&saved=1&siteId=" + SiteID.ToString());
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion
}
