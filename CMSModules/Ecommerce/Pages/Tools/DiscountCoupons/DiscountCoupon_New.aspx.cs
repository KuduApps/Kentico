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
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_DiscountCoupons_DiscountCoupon_New : CMSDiscountCouponsPage
{
    protected string currencyCode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        currencyCode = HTMLHelper.HTMLEncode(CurrencyInfoProvider.GetMainCurrencyCode(ConfiguredSiteID));

        rfvCouponCode.ErrorMessage = GetString("DiscounCoupon_Edit.errorCode");
        rfvDisplayName.ErrorMessage = GetString("DiscounCoupon_Edit.errorDisplay");

        // Control initializations				
        lblDiscountCouponCode.Text = GetString("DiscounCoupon_Edit.DiscountCouponCodeLabel");
        lblDiscountCouponDisplayName.Text = GetString("DiscounCoupon_Edit.DiscountCouponDisplayNameLabel");
        btnOk.Text = GetString("General.OK");

        lblDiscountCouponValidTo.Text = GetString("DiscounCoupon_Edit.DiscountCouponValidToLabel");
        lblDiscountCouponValidFrom.Text = GetString("DiscounCoupon_Edit.DiscountCouponValidFromLabel");
        lblDiscountCouponCode.Text = GetString("DiscounCoupon_Edit.DiscountCouponCodeLabel");
        lblDiscountCouponValue.Text = GetString("DiscounCoupon_Edit.DiscountCouponAbsoluteValueLabel");

        radFixed.Text = string.Format(GetString("DiscounCoupon_Edit.radFixed"), currencyCode);
        radFixed.Attributes["onclick"] = "jQuery('span[id*=\"lblCurrency\"]').html(" + ScriptHelper.GetString("(" + currencyCode + ")") + ")";
        radPercentage.Text = GetString("DiscounCoupon_Edit.radPercentage");
        radPercentage.Attributes["onclick"] = "jQuery('span[id*=\"lblCurrency\"]').html('(%)')";

        if (!URLHelper.IsPostback())
        {
            radFixed.Checked = true;
            radPercentage.Checked = false;
        }

        rfvCouponCode.ErrorMessage = GetString("DiscounCoupon_Edit.errorCode");

        dtPickerDiscountCouponValidTo.SupportFolder = "~/CMSAdminControls/Calendar";
        dtPickerDiscountCouponValidFrom.SupportFolder = "~/CMSAdminControls/Calendar";

        string currentDiscountCoupon = GetString("DiscounCoupon_Edit.NewItemCaption");

        // Page title
        this.CurrentMaster.Title.HelpTopicName = "CMS_Ecommerce_DiscountCoupons_General";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title breadcrumbs control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("DiscounCoupon_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/DiscountCoupons/DiscountCoupon_List.aspx?siteId=" + SiteID;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(currentDiscountCoupon, ConfiguredSiteID);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("DiscounCoupon_List.NewItemCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_DiscountCoupon/new.png");

        // Check presence of main currency
        string currencyErr = CheckMainCurrency(ConfiguredSiteID);
        if (!string.IsNullOrEmpty(currencyErr))
        {
            // Show message
            lblError.Text = currencyErr;
            lblError.Visible = true;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblCurrency.Text = string.Format("({0})", radFixed.Checked ? currencyCode : "%");

    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (ConfiguredSiteID <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifyDiscountCoupon(global))
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

        string errorMessage = new Validator().NotEmpty(txtDiscountCouponDisplayName.Text.Trim(), GetString("DiscounCoupon_Edit.errorDisplay"))
                                             .NotEmpty(txtDiscountCouponCode.Text.Trim(), GetString("DiscounCoupon_Edit.errorCode"))
                                             .IsCodeName(txtDiscountCouponCode.Text.Trim(), GetString("DiscounCoupon_Edit.errorCodeFormat")).Result;

        // Discount value validation
        if (errorMessage == "")
        {
            // Relative
            if (this.radPercentage.Checked && !ValidationHelper.IsInRange(0, 100, ValidationHelper.GetDouble(this.txtDiscountCouponValue.Text.Trim(), -1)))
            {
                errorMessage = GetString("Com.Error.RelativeDiscountValue");
            }
            // Absolute
            else if (this.radFixed.Checked && !ValidationHelper.IsPositiveNumber(ValidationHelper.GetDouble(this.txtDiscountCouponValue.Text.Trim(), -1)))
            {
                errorMessage = GetString("Com.Error.AbsoluteDiscountValue");
            }
        }

        // From/to date validation
        if (errorMessage == "")
        {
            if ((!dtPickerDiscountCouponValidFrom.IsValidRange()) || (!dtPickerDiscountCouponValidTo.IsValidRange()))
            {
                errorMessage = GetString("general.errorinvaliddatetimerange");
            }

            if ((dtPickerDiscountCouponValidFrom.SelectedDateTime != DateTime.MinValue) &&
            (dtPickerDiscountCouponValidTo.SelectedDateTime != DateTime.MinValue) &&
            (dtPickerDiscountCouponValidFrom.SelectedDateTime >= dtPickerDiscountCouponValidTo.SelectedDateTime))
            {
                errorMessage = GetString("General.DateOverlaps");
            }
        }

        if (errorMessage == "")
        {
            // DiscountCoupon code name must to be unique
            DiscountCouponInfo discountCouponObj = null;
            string siteWhere = (ConfiguredSiteID > 0) ? " AND (DiscountCouponSiteID = " + ConfiguredSiteID + " OR DiscountCouponSiteID IS NULL)" : "";
            DataSet ds = DiscountCouponInfoProvider.GetDiscountCoupons("DiscountCouponCode = '" + txtDiscountCouponCode.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                discountCouponObj = new DiscountCouponInfo(ds.Tables[0].Rows[0]);
            }

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                discountCouponObj = new DiscountCouponInfo(ds.Tables[0].Rows[0]);
            }

            // If discountCouponCode value is unique														
            if ((discountCouponObj == null))
            {
                // Create new item -> insert
                discountCouponObj = new DiscountCouponInfo();

                discountCouponObj.DiscountCouponCode = txtDiscountCouponCode.Text.Trim();
                discountCouponObj.DiscountCouponDisplayName = txtDiscountCouponDisplayName.Text.Trim();
                discountCouponObj.DiscountCouponIsExcluded = false;
                discountCouponObj.DiscountCouponSiteID = ConfiguredSiteID;

                discountCouponObj.DiscountCouponIsFlatValue = true;

                if (radPercentage.Checked)
                {
                    discountCouponObj.DiscountCouponIsFlatValue = false;
                }

                discountCouponObj.DiscountCouponValue = ValidationHelper.GetDouble(txtDiscountCouponValue.Text.Trim(), 0.0);
                discountCouponObj.DiscountCouponCode = txtDiscountCouponCode.Text.Trim();
                discountCouponObj.DiscountCouponValidFrom = dtPickerDiscountCouponValidFrom.SelectedDateTime;
                discountCouponObj.DiscountCouponValidTo = dtPickerDiscountCouponValidTo.SelectedDateTime;

                DiscountCouponInfoProvider.SetDiscountCouponInfo(discountCouponObj);

                URLHelper.Redirect("DiscountCoupon_Edit_Frameset.aspx?discountid=" + Convert.ToString(discountCouponObj.DiscountCouponID) + "&saved=1&siteId=" + SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("DiscounCoupon_Edit.DiscountCouponCodeExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
