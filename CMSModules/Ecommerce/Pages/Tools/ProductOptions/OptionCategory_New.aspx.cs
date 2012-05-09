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

public partial class CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_New : CMSProductOptionCategoriesPage
{
    protected int editedSiteId = -1;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Field validator error messages initialization
        rfvCategoryName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        // Control initializations				
        btnOk.Text = GetString("General.OK");
        radText.Text = GetString("com.categorytype.text");
        radSelection.Text = GetString("com.categorytype.selection");

        editedSiteId = ConfiguredSiteID;

        // Initializes page title control
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("optioncategory_edit.itemlistlink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/ProductOptions/OptionCategory_List.aspx?siteId=" + SiteID;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(GetString("optioncategory_list.newitemcaption"), editedSiteId);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Set help topic
        this.CurrentMaster.Title.HelpTopicName = "new_option_category";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("OptionCategory_List.NewItemCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_OptionCategory/new.png");
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (editedSiteId <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifyOptionCategory(global))
        {
            // Check module permissions
            if (global)
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyProducts");
            }
        }

        // Check input value from textboxs
        string errorMessage = new Validator().NotEmpty(txtDisplayName.Text, GetString("general.requiresdisplayname"))
            .NotEmpty(txtCategoryName.Text, GetString("general.requirescodename"))
            .IsIdentificator(txtCategoryName.Text, GetString("optioncategory_new.errorNotIdentificator")).Result;

        if (errorMessage == "")
        {
            // Category code name must be unique
            OptionCategoryInfo optionCategoryObj = null;
            string siteWhere = (ConfiguredSiteID > 0) ? " AND (CategorySiteID = " + ConfiguredSiteID + " OR CategorySiteID IS NULL)" : "";
            DataSet ds = OptionCategoryInfoProvider.GetOptionCategories("CategoryName = '" + txtCategoryName.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                optionCategoryObj = new OptionCategoryInfo(ds.Tables[0].Rows[0]);
            }

            // If category code name value is unique														
            if (optionCategoryObj == null)
            {
                // Create, fill and set OptionCategoryInfo object
                optionCategoryObj = new OptionCategoryInfo();
                optionCategoryObj.CategoryDisplayName = txtDisplayName.Text.Trim();
                optionCategoryObj.CategoryName = txtCategoryName.Text.Trim();
                optionCategoryObj.CategorySelectionType = radSelection.Checked ? OptionCategorySelectionTypeEnum.Dropdownlist : OptionCategorySelectionTypeEnum.TextBox;
                optionCategoryObj.CategoryEnabled = true;
                optionCategoryObj.CategoryDefaultRecord = "";
                optionCategoryObj.CategoryDefaultOptions = "";
                optionCategoryObj.CategorySiteID = ConfiguredSiteID;

                // Create category and option under transaction
                using (CMSTransactionScope tr = new CMSTransactionScope())
                {
                    OptionCategoryInfoProvider.SetOptionCategoryInfo(optionCategoryObj);

                    if (radText.Checked)
                    {
                        // Create text product option
                        SKUInfo option = new SKUInfo()
                        {
                            SKUOptionCategoryID = optionCategoryObj.CategoryID,
                            SKUProductType = SKUProductTypeEnum.Text,
                            SKUSiteID = ConfiguredSiteID,
                            SKUName = optionCategoryObj.CategoryDisplayName,
                            SKUDepartmentID = 0,
                            SKUPrice = 0,
                            SKUNeedsShipping = false,
                            SKUWeight = 0,
                            SKUEnabled = true
                        };

                        SKUInfoProvider.SetSKUInfo(option);
                    }

                    // Commit a transaction
                    tr.Commit();
                }

                URLHelper.Redirect("OptionCategory_Edit.aspx?categoryId=" + Convert.ToString(optionCategoryObj.CategoryID) + "&saved=1&siteId=" + SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("optioncategory_new.errorExistingCodeName");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
