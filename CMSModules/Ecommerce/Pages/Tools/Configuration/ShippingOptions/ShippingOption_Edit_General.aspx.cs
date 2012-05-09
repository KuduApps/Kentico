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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_General : CMSShippingOptionsPage
{
    protected int mShippingOptionID = 0;
    protected int mEditedSiteId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.ShippingOptions.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.ShippingOptions.General");
        }

        // Required field validator error messages initialization
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvName.ErrorMessage = GetString("COM_ShippingOption_Edit.NameError");
        txtShippingOptionCharge.EmptyErrorMessage = GetString("COM_ShippingOption_Edit.ChargeError");
        txtShippingOptionCharge.ValidationErrorMessage = GetString("COM_ShippingOption_Edit.ChargePositive");

        // Control initializations				
        lblShippingOptionDisplayName.Text = GetString("COM_ShippingOption_Edit.ShippingOptionDisplayNameLabel");
        lblShippingOptionCharge.Text = GetString("COM_ShippingOption_Edit.ShippingOptionChargeLabel");
        lblShippingOptionName.Text = GetString("COM_ShippingOption_Edit.ShippingOptionNameLabel");

        btnOk.Text = GetString("General.OK");

        // Get shippingOption id from querystring		
        mShippingOptionID = QueryHelper.GetInteger("shippingOptionID", 0);
        mEditedSiteId = ConfiguredSiteID;
        // Edit shiping option
        if (mShippingOptionID > 0)
        {
            ShippingOptionInfo shippingOptionObj = ShippingOptionInfoProvider.GetShippingOptionInfo(mShippingOptionID);
            EditedObject = shippingOptionObj;

            if (shippingOptionObj != null)
            {
                mEditedSiteId = shippingOptionObj.ShippingOptionSiteID;
                CheckEditedObjectSiteID(mEditedSiteId);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(shippingOptionObj);
                    // Show that the shippingOption was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }

        // Ensure currency code after price value
        txtShippingOptionCharge.CurrencySiteID = mEditedSiteId;

        // Check presence of main currency
        string currencyErr = CheckMainCurrency(mEditedSiteId);
        if (!string.IsNullOrEmpty(currencyErr))
        {
            // Show message
            lblError.Text = currencyErr;
            lblError.Visible = true;
        }
    }


    /// <summary>
    /// Load data of editing shippingOption.
    /// </summary>
    /// <param name="shippingOptionObj">ShippingOption object</param>
    protected void LoadData(ShippingOptionInfo shippingOptionObj)
    {
        txtShippingOptionDisplayName.Text = shippingOptionObj.ShippingOptionDisplayName;
        txtShippingOptionCharge.Value = shippingOptionObj.ShippingOptionCharge;
        txtShippingOptionName.Text = shippingOptionObj.ShippingOptionName;
        chkShippingOptionEnabled.Checked = shippingOptionObj.ShippingOptionEnabled;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check permissions
        CheckConfigurationModification(mEditedSiteId);

        string errorMessage = new Validator()
            .NotEmpty(txtShippingOptionDisplayName.Text.Trim(), rfvDisplayName.ErrorMessage)
            .NotEmpty(txtShippingOptionName.Text.Trim(), rfvName.ErrorMessage).Result;

        if (!ValidationHelper.IsCodeName(txtShippingOptionName.Text.Trim()))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }

        if (errorMessage == "")
        {
            errorMessage = txtShippingOptionCharge.ValidatePrice(false);
        }

        if (errorMessage == "")
        {
            // ShippingOptionName must be unique
            ShippingOptionInfo shippingOptionObj = null;
            string siteWhere = (mEditedSiteId > 0) ? " AND (ShippingOptionSiteID = " + mEditedSiteId + " OR ShippingOptionSiteID IS NULL)" : "";
            DataSet ds = ShippingOptionInfoProvider.GetShippingOptions("ShippingOptionName = '" + txtShippingOptionName.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                shippingOptionObj = new ShippingOptionInfo(ds.Tables[0].Rows[0]);
            }

            if ((shippingOptionObj == null) || (shippingOptionObj.ShippingOptionID == mShippingOptionID))
            {
                // Get object
                if ((shippingOptionObj == null))
                {
                    shippingOptionObj = ShippingOptionInfoProvider.GetShippingOptionInfo(mShippingOptionID);
                    if (shippingOptionObj == null)
                    {
                        shippingOptionObj = new ShippingOptionInfo();
                        shippingOptionObj.ShippingOptionSiteID = mEditedSiteId;
                    }
                }

                shippingOptionObj.ShippingOptionDisplayName = txtShippingOptionDisplayName.Text.Trim();
                shippingOptionObj.ShippingOptionCharge = txtShippingOptionCharge.Value;
                shippingOptionObj.ShippingOptionName = txtShippingOptionName.Text.Trim();
                shippingOptionObj.ShippingOptionEnabled = chkShippingOptionEnabled.Checked;

                // Save changes
                ShippingOptionInfoProvider.SetShippingOptionInfo(shippingOptionObj);

                URLHelper.Redirect("ShippingOption_Edit_General.aspx?shippingOptionID=" + shippingOptionObj.ShippingOptionID + "&saved=1&siteId=" + SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("ShippingOption_Edit.ShippingOptionNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
