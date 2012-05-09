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
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_DiscountCoupons_DiscountCoupon_Edit_General : CMSDiscountCouponsPage
{
    protected int mDiscountId = 0;
    protected string currencyCode = "";
    protected int editedSiteId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "DiscountCoupons.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "DiscountCoupons.General");
        }

        rfvCouponCode.ErrorMessage = GetString("DiscounCoupon_Edit.errorCode");
        rfvDisplayName.ErrorMessage = GetString("DiscounCoupon_Edit.errorDisplay");

        // Control initializations				
        lblDiscountCouponValue.Text = GetString("DiscounCoupon_Edit.DiscountCouponAbsoluteValueLabel");
        lblDiscountCouponValidTo.Text = GetString("DiscounCoupon_Edit.DiscountCouponValidToLabel");
        lblDiscountCouponCode.Text = GetString("DiscounCoupon_Edit.DiscountCouponCodeLabel");

        lblDiscountCouponDisplayName.Text = GetString("DiscounCoupon_Edit.DiscountCouponDisplayNameLabel");
        lblDiscountCouponValidFrom.Text = GetString("DiscounCoupon_Edit.DiscountCouponValidFromLabel");

        radFixed.Text = GetString("DiscounCoupon_Edit.radFixed");
        radPercentage.Text = GetString("DiscounCoupon_Edit.radPercentage");

        btnOk.Text = GetString("General.OK");
        dtPickerDiscountCouponValidTo.SupportFolder = "~/CMSAdminControls/Calendar";
        dtPickerDiscountCouponValidFrom.SupportFolder = "~/CMSAdminControls/Calendar";

        // Get discountCoupon id from querystring		
        mDiscountId = QueryHelper.GetInteger("discountid", 0);
        editedSiteId = ConfiguredSiteID;
        if (mDiscountId > 0)
        {
            DiscountCouponInfo discountCouponObj = DiscountCouponInfoProvider.GetDiscountCouponInfo(mDiscountId);
            EditedObject = discountCouponObj;

            if (discountCouponObj != null)
            {
                editedSiteId = discountCouponObj.DiscountCouponSiteID;

                // Check if edited object belongs to configured site
                CheckEditedObjectSiteID(editedSiteId);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(discountCouponObj);

                    // Show that the discountCoupon was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }

        currencyCode = HTMLHelper.HTMLEncode(CurrencyInfoProvider.GetMainCurrencyCode(editedSiteId));

        // Check presence of main currency
        string currencyErr = CheckMainCurrency(ConfiguredSiteID);
        if (!string.IsNullOrEmpty(currencyErr))
        {
            // Show message
            lblError.Text = currencyErr;
            lblError.Visible = true;
        }

        radFixed.Attributes["onclick"] = "jQuery('span[id*=\"lblCurrency\"]').html(" + ScriptHelper.GetString("(" + currencyCode + ")") + ")";
        radPercentage.Attributes["onclick"] = "jQuery('span[id*=\"lblCurrency\"]').html('(%)')";
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblCurrency.Text = string.Format("({0})", radFixed.Checked ? currencyCode : "%");
    }


    /// <summary>
    /// Load data of editing discountCoupon.
    /// </summary>
    /// <param name="discountCouponObj">DiscountCoupon object</param>
    protected void LoadData(DiscountCouponInfo discountCouponObj)
    {
        txtDiscountCouponValue.Text = Convert.ToString(discountCouponObj.DiscountCouponValue);
        dtPickerDiscountCouponValidTo.SelectedDateTime = discountCouponObj.DiscountCouponValidTo;
        txtDiscountCouponCode.Text = discountCouponObj.DiscountCouponCode;
        txtDiscountCouponDisplayName.Text = discountCouponObj.DiscountCouponDisplayName;
        dtPickerDiscountCouponValidFrom.SelectedDateTime = discountCouponObj.DiscountCouponValidFrom;

        radPercentage.Checked = !discountCouponObj.DiscountCouponIsFlatValue;
        radFixed.Checked = discountCouponObj.DiscountCouponIsFlatValue;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (editedSiteId <= 0);
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
            string siteWhere = (editedSiteId > 0) ? " AND (DiscountCouponSiteID = " + editedSiteId + " OR DiscountCouponSiteID IS NULL)" : "";
            DataSet ds = DiscountCouponInfoProvider.GetDiscountCoupons("DiscountCouponCode = '" + txtDiscountCouponCode.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                discountCouponObj = new DiscountCouponInfo(ds.Tables[0].Rows[0]);
            }

            // If discountCouponCode value is unique														
            if ((discountCouponObj == null) || (discountCouponObj.DiscountCouponID == mDiscountId))
            {
                // If discountCouponCode value is unique -> determine whether it is update or insert 
                if ((discountCouponObj == null))
                {
                    // Get DiscountCouponInfo object by primary key
                    discountCouponObj = DiscountCouponInfoProvider.GetDiscountCouponInfo(mDiscountId);
                    if (discountCouponObj == null)
                    {
                        // Create new item -> insert
                        discountCouponObj = new DiscountCouponInfo();
                        discountCouponObj.DiscountCouponSiteID = editedSiteId;
                    }
                }

                discountCouponObj.DiscountCouponValue = ValidationHelper.GetDouble(txtDiscountCouponValue.Text.Trim(), 0.0);
                discountCouponObj.DiscountCouponCode = txtDiscountCouponCode.Text.Trim();
                discountCouponObj.DiscountCouponIsFlatValue = true;

                if (radPercentage.Checked)
                {
                    discountCouponObj.DiscountCouponIsFlatValue = false;
                }

                discountCouponObj.DiscountCouponDisplayName = txtDiscountCouponDisplayName.Text.Trim();
                discountCouponObj.DiscountCouponValidFrom = dtPickerDiscountCouponValidFrom.SelectedDateTime;
                discountCouponObj.DiscountCouponValidTo = dtPickerDiscountCouponValidTo.SelectedDateTime;

                DiscountCouponInfoProvider.SetDiscountCouponInfo(discountCouponObj);

                URLHelper.Redirect("DiscountCoupon_Edit_General.aspx?discountid=" + Convert.ToString(discountCouponObj.DiscountCouponID) + "&saved=1&siteId=" + SiteID);
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
