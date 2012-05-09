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

public partial class CMSModules_Ecommerce_Pages_Tools_DiscountLevels_DiscountLevel_New : CMSDiscountLevelsPage
{
    protected int mDiscountLevelId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Control initializations
        this.lblDiscountLevelValidFrom.Text = GetString("DiscountLevel_Edit.DiscountLevelValidFromLabel");
        this.lblDiscountLevelValue.Text = GetString("DiscountLevel_Edit.DiscountLevelValueLabel");
        this.lblDiscountLevelValidTo.Text = GetString("DiscountLevel_Edit.DiscountLevelValidToLabel");
        this.btnOk.Text = GetString("General.OK");
        this.dtPickerDiscountLevelValidFrom.SupportFolder = "~/CMSAdminControls/Calendar";
        this.dtPickerDiscountLevelValidTo.SupportFolder = "~/CMSAdminControls/Calendar";

        // Init Validators
        this.rfvDiscountLevelDisplayName.ErrorMessage = GetString("DiscountLevel_Edit.rfvDiscountLevelDisplayName.ErrorMessage");
        this.rfvDiscountLevelName.ErrorMessage = GetString("DiscountLevel_Edit.rfvDiscountLevelName.ErrorMessage");
        this.rfvDiscountLevelValue.ErrorMessage = GetString("DiscountLevel_Edit.rfvDiscountLevelValue.ErrorMessage");

        // Page title
        this.CurrentMaster.Title.HelpTopicName = "new_levelgeneral_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title breadcrumbs control
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("DiscountLevel_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/DiscountLevels/DiscountLevel_List.aspx?siteId=" + SiteID;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(GetString("DiscountLevel_Edit.NewItemCaption"), ConfiguredSiteID);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("DiscountLevel_List.NewItemCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_DiscountLevel/new.png");
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (ConfiguredSiteID <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifyDiscountLevel(global))
        {
            if (global)
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyDiscounts");
            }
        }

        string errorMessage = new Validator().NotEmpty(txtDiscountLevelDisplayName.Text.Trim(), GetString("DiscountLevel_Edit.rfvDiscountLevelDisplayName.ErrorMessage"))
                                             .NotEmpty(txtDiscountLevelName.Text.Trim(), GetString("DiscountLevel_Edit.rfvDiscountLevelName.ErrorMessage"))
                                             .IsCodeName(txtDiscountLevelName.Text.Trim(), GetString("General.ErrorCodeNameInIdentificatorFormat"))
                                             .NotEmpty(txtDiscountLevelValue.Text.Trim(), GetString("DiscountLevel_Edit.rfvDiscountLevelValue.ErrorMessage")).Result;

        // Discount value validation
        if (errorMessage == "")
        {
            if (!ValidationHelper.IsInRange(0, 100, ValidationHelper.GetDouble(this.txtDiscountLevelValue.Text.Trim(), -1)))
            {
                errorMessage = GetString("Com.Error.RelativeDiscountValue");
            }
        }

        // From/to date validation
        if (errorMessage == "")
        {
            if ((!dtPickerDiscountLevelValidFrom.IsValidRange()) || (!dtPickerDiscountLevelValidTo.IsValidRange()))
            {
                errorMessage = GetString("general.errorinvaliddatetimerange");
            }

            if ((dtPickerDiscountLevelValidFrom.SelectedDateTime != DateTime.MinValue) &&
            (dtPickerDiscountLevelValidTo.SelectedDateTime != DateTime.MinValue) &&
            (dtPickerDiscountLevelValidFrom.SelectedDateTime >= dtPickerDiscountLevelValidTo.SelectedDateTime))
            {
                errorMessage = GetString("General.DateOverlaps");
            }
        }

        if (errorMessage == "")
        {
            // Discount level code name must be unique
            DiscountLevelInfo discountLevelObj = null;
            string siteWhere = (ConfiguredSiteID > 0) ? " AND (DiscountLevelSiteID = " + ConfiguredSiteID + " OR DiscountLevelSiteID IS NULL)" : "";
            DataSet ds = DiscountLevelInfoProvider.GetDiscountLevels("DiscountLevelName = '" + txtDiscountLevelName.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                discountLevelObj = new DiscountLevelInfo(ds.Tables[0].Rows[0]);
            }

            // If name is unique OR ids are same
            if ((discountLevelObj == null) || (discountLevelObj.DiscountLevelID == mDiscountLevelId))
            {
                // If name is unique
                if ((discountLevelObj == null))
                {
                    // And id does not exist -> insert new
                    discountLevelObj = DiscountLevelInfoProvider.GetDiscountLevelInfo(mDiscountLevelId);
                    if (discountLevelObj == null)
                    {
                        // Create new DiscountLevelInfo
                        discountLevelObj = new DiscountLevelInfo();
                        discountLevelObj.DiscountLevelSiteID = ConfiguredSiteID;
                    }
                }

                // Set values
                discountLevelObj.DiscountLevelValidFrom = dtPickerDiscountLevelValidFrom.SelectedDateTime;
                discountLevelObj.DiscountLevelEnabled = chkDiscountLevelEnabled.Checked;
                discountLevelObj.DiscountLevelName = txtDiscountLevelName.Text.Trim();
                discountLevelObj.DiscountLevelValue = Convert.ToDouble(txtDiscountLevelValue.Text.Trim());
                discountLevelObj.DiscountLevelValidTo = dtPickerDiscountLevelValidTo.SelectedDateTime;
                discountLevelObj.DiscountLevelDisplayName = txtDiscountLevelDisplayName.Text.Trim();

                DiscountLevelInfoProvider.SetDiscountLevelInfo(discountLevelObj);

                URLHelper.Redirect("DiscountLevel_Edit_Frameset.aspx?discountLevelId=" + Convert.ToString(discountLevelObj.DiscountLevelID) + "&saved=1&siteId=" + SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("DiscountLevel_Edit.DiscountLevelNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
