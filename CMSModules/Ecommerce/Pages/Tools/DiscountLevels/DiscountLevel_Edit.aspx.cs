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

public partial class CMSModules_Ecommerce_Pages_Tools_DiscountLevels_DiscountLevel_Edit : CMSDiscountLevelsPage
{
    protected int mDiscountLevelId = 0;
    protected int editedSiteId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "DiscountLevels.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "DiscountLevels.General");
        }

        // Control initializations				
        lblDiscountLevelValidFrom.Text = GetString("DiscountLevel_Edit.DiscountLevelValidFromLabel");
        lblDiscountLevelValue.Text = GetString("DiscountLevel_Edit.DiscountLevelValueLabel");
        lblDiscountLevelValidTo.Text = GetString("DiscountLevel_Edit.DiscountLevelValidToLabel");
        // Init Validators
        rfvDiscountLevelDisplayName.ErrorMessage = GetString("DiscountLevel_Edit.rfvDiscountLevelDisplayName.ErrorMessage");
        rfvDiscountLevelName.ErrorMessage = GetString("DiscountLevel_Edit.rfvDiscountLevelName.ErrorMessage");
        rfvDiscountLevelValue.ErrorMessage = GetString("DiscountLevel_Edit.rfvDiscountLevelValue.ErrorMessage");

        btnOk.Text = GetString("General.OK");
        dtPickerDiscountLevelValidFrom.SupportFolder = "~/CMSAdminControls/Calendar";
        dtPickerDiscountLevelValidTo.SupportFolder = "~/CMSAdminControls/Calendar";

        // Get discountlLevel id from querystring		
        mDiscountLevelId = QueryHelper.GetInteger("discountLevelId", 0);
        editedSiteId = ConfiguredSiteID;

        // Edit existing
        if (mDiscountLevelId > 0)
        {
            DiscountLevelInfo discountLevelObj = DiscountLevelInfoProvider.GetDiscountLevelInfo(mDiscountLevelId);
            EditedObject = discountLevelObj;

            if (discountLevelObj != null)
            {
                editedSiteId = discountLevelObj.DiscountLevelSiteID;
                // Check if edited object belongs to configured site
                CheckEditedObjectSiteID(editedSiteId);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(discountLevelObj);

                    // Show that the discountlLevel was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }
    }


    /// <summary>
    /// Load data of editing discountLevel.
    /// </summary>
    /// <param name="discountlLevelObj">DiscountLevel object</param>
    protected void LoadData(DiscountLevelInfo discountLevelObj)
    {
        dtPickerDiscountLevelValidFrom.SelectedDateTime = discountLevelObj.DiscountLevelValidFrom;
        chkDiscountLevelEnabled.Checked = discountLevelObj.DiscountLevelEnabled;
        txtDiscountLevelName.Text = discountLevelObj.DiscountLevelName;
        txtDiscountLevelValue.Text = Convert.ToString(discountLevelObj.DiscountLevelValue);
        dtPickerDiscountLevelValidTo.SelectedDateTime = discountLevelObj.DiscountLevelValidTo;
        txtDiscountLevelDisplayName.Text = discountLevelObj.DiscountLevelDisplayName;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (editedSiteId <= 0);
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

        // Is discount level name in code name format?
        if (errorMessage == "")
        {
            if (!ValidationHelper.IsCodeName(txtDiscountLevelName.Text.Trim()))
            {
                errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
            }
        }

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
            if ((!dtPickerDiscountLevelValidTo.IsValidRange()) || (!dtPickerDiscountLevelValidFrom.IsValidRange()))
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

            if ((discountLevelObj == null) || (discountLevelObj.DiscountLevelID == mDiscountLevelId))
            {
                // Get the object
                if (discountLevelObj == null)
                {
                    discountLevelObj = DiscountLevelInfoProvider.GetDiscountLevelInfo(mDiscountLevelId);
                    if (discountLevelObj == null)
                    {
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

                // Save to the database
                DiscountLevelInfoProvider.SetDiscountLevelInfo(discountLevelObj);

                URLHelper.Redirect("DiscountLevel_Edit.aspx?discountLevelId=" + Convert.ToString(discountLevelObj.DiscountLevelID) + "&saved=1&siteId=" + SiteID);
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
