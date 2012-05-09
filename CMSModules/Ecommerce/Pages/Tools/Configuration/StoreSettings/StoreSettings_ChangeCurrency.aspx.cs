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

using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.LicenseProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_ChangeCurrency : CMSEcommerceModalPage
{
    private CurrencyInfo mainCurrency = null;
    private int editedSiteId = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register JQuery
        ScriptHelper.RegisterJQuery(this);

        // Check permissions (only global admin can see this dialog)
        CurrentUserInfo ui = CMSContext.CurrentUser;

        if ((ui == null) || !ui.IsGlobalAdministrator)
        {
            // Redirect to access denied
            RedirectToAccessDenied(GetString("StoreSettings_ChangeCurrency.AccessDenied"));
        }

        int siteId = QueryHelper.GetInteger("siteId", CMSContext.CurrentSiteID);

        if (ui.IsGlobalAdministrator)
        {
            editedSiteId = (siteId <= 0) ? 0 : siteId;
        }
        else
        {
            editedSiteId = CMSContext.CurrentSiteID;
        }

        mainCurrency = CurrencyInfoProvider.GetMainCurrency(editedSiteId);

        // Load the UI
        this.CurrentMaster.Page.Title = "Ecommerce - Change main currency";
        this.CurrentMaster.Title.TitleText = GetString("StoreSettings_ChangeCurrency.ChangeCurrencyTitle");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Currency/object.png");
        this.chkRecalculateFromGlobal.Text = GetString("StoreSettings_ChangeCurrency.ChangeSiteFromGlobal");
        this.chkRecalculateFromGlobal.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkCredit.Text = GetString("StoreSettings_ChangeCurrency.ChangeCredit");
        this.chkCredit.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkDocuments.Text = GetString("StoreSettings_ChangeCurrency.ChangeDocuments");
        this.chkDocuments.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkExchangeRates.Text = GetString("StoreSettings_ChangeCurrency.ChangeExchangeRates");
        this.chkExchangeRates.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkFlatDiscountsCoupons.Text = GetString("StoreSettings_ChangeCurrency.ChangeDiscountCoupons");
        this.chkFlatDiscountsCoupons.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkFlatTaxes.Text = GetString("StoreSettings_ChangeCurrency.ChangeTaxes");
        this.chkFlatTaxes.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkFlatVolumeDiscounts.Text = GetString("StoreSettings_ChangeCurrency.ChangeVolumeDiscounts");
        this.chkFlatVolumeDiscounts.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkProductPrices.Text = GetString("StoreSettings_ChangeCurrency.ChangeProductPrices");
        this.chkProductPrices.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkShipping.Text = GetString("StoreSettings_ChangeCurrency.ChangeShipping");
        this.chkShipping.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkFreeShipping.Text = GetString("StoreSettings_ChangeCurrency.ChangeFreeShipping");
        this.chkFreeShipping.InputAttributes["onclick"] = "checkRecalculation()";
        this.chkOrders.Text = GetString("StoreSettings_ChangeCurrency.ChangeOrder");
        this.chkOrders.InputAttributes["onclick"] = "checkRecalculation()";
        this.btnCancel.Text = GetString("General.Cancel");
        this.lblExchangeRate.Text = GetString("StoreSettings_ChangeCurrency.ExchangeRate");
        this.lblRound.Text = GetString("StoreSettings_ChangeCurrency.Round");
        this.btnOk.Text = GetString("General.OK");
        this.imgHelp.ImageUrl = GetImageUrl("General/HelpSmall.png");
        this.imgHelp.ToolTip = GetString("StoreSettings_ChangeCurrency.ExchangeRateHelp");
        this.imgRoundHelp.ImageUrl = GetImageUrl("General/HelpSmall.png");
        this.imgRoundHelp.ToolTip = GetString("StoreSettings_ChangeCurrency.ExchangeRateRoundHelp");
        this.lblOldMainLabel.Text = GetString("StoreSettings_ChangeCurrency.OldMainCurrency");
        this.lblNewMainCurrency.Text = GetString("StoreSettings_ChangeCurrency.NewMainCurrency");

        if (mainCurrency != null)
        {
            // Set confirmation message for OK button
            this.btnOk.Attributes["onclick"] = "return confirm(" + ScriptHelper.GetString(GetString("StoreSettings_ChangeCurrency.Confirmation")) + ")";

            this.lblOldMainCurrency.Text = HTMLHelper.HTMLEncode(mainCurrency.CurrencyDisplayName);
        }
        else
        {
            this.plcObjectsSelection.Visible = false;
            this.plcRecalculationDetails.Visible = false;
            this.plcOldCurrency.Visible = false;
        }

        this.currencyElem.AddSelectRecord = true;
        this.currencyElem.SiteID = editedSiteId;

        this.plcRecountDocuments.Visible = (editedSiteId != 0);
        this.plcRecalculateFromGlobal.Visible = (editedSiteId == 0);

        if (!URLHelper.IsPostback())
        {
            if (QueryHelper.GetBoolean("saved", false))
            {
                // Refresh the page
                ltlScript.Text = ScriptHelper.GetScript(@"var loc = wopener.location;
                if(!(/currencysaved=1/.test(loc)))
                {
                    loc += '&currencysaved=1';
                }
                wopener.location.replace(loc); window.close();");

                lblInfo.Text = GetString("general.changessaved");
                lblInfo.Visible = true;
            }
        }

        // Prepare checkRecalculation script parts for checkboxes which can be hidden
        string conditionalCheckboxesScript = "";

        if(this.plcRecountDocuments.Visible)
        {
            conditionalCheckboxesScript = string.Format(@"
  tmp = jQuery('#{0}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}", chkDocuments.ClientID);
        }

        if (this.plcRecalculateFromGlobal.Visible)
        {
            conditionalCheckboxesScript += string.Format(@"
  tmp = jQuery('#{0}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}", chkRecalculateFromGlobal.ClientID);
        }

        // Init scripts
        string checkRecalculationScript = string.Format(@"
function checkRecalculation(){{
  var recalcNeeded = false;   
  var tmp = jQuery('#{0}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{1}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{2}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{3}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{4}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{5}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{6}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{7}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
  tmp = jQuery('#{8}');
  if(tmp.length == 1 && tmp[0].checked) {{recalcNeeded = true;}}
{11}

  if(recalcNeeded)
  {{
    jQuery('#{9}').parents('tr').show();
    jQuery('#{10}').parents('tr').show();
  }}else
  {{
    jQuery('#{9}').parents('tr').hide();
    jQuery('#{10}').parents('tr').hide();
  }}
}}", chkExchangeRates.ClientID, chkProductPrices.ClientID, chkFlatTaxes.ClientID, chkFlatDiscountsCoupons.ClientID, chkFlatVolumeDiscounts.ClientID, chkCredit.ClientID, chkShipping.ClientID, chkFreeShipping.ClientID, chkOrders.ClientID, txtEchangeRate.ClientID, txtRound.ClientID, conditionalCheckboxesScript);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "CheckRecalculationScript", checkRecalculationScript, true);
        ScriptHelper.RegisterStartupScript(this, typeof(string), "CheckRecalculationStartup", "checkRecalculation();", true);
    }


    /// <summary>
    /// Changes the selected prices and other object fields.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string err = "";

        if ((mainCurrency != null) && RecalculationRequested())
        {
            err = new Validator().NotEmpty(txtEchangeRate.Text.Trim(), GetString("StoreSettings_ChangeCurrency.EmptyExchangeRate"))
                                         .NotEmpty(txtRound.Text.Trim(), GetString("StoreSettings_ChangeCurrency.InvalidRound"))
                                         .IsInteger(txtRound.Text.Trim(), GetString("StoreSettings_ChangeCurrency.InvalidRound"))
                                         .IsDouble(txtEchangeRate.Text.Trim(), GetString("StoreSettings_ChangeCurrency.InvalidExchangeRate"))
                                         .IsPositiveNumber(txtEchangeRate.Text.Trim(), GetString("StoreSettings_ChangeCurrency.NegativeExchangeRate")).Result;
        }

        // Check new currency ID
        int newCurrencyId = ValidationHelper.GetInteger(currencyElem.Value, 0);
        if (string.IsNullOrEmpty(err) && (newCurrencyId <= 0))
        {
            err = GetString("StoreSettings_ChangeCurrency.NoNewMainCurrency");
        }

        // Show error message if any
        if (err != "")
        {
            lblError.Text = err;
            lblError.Visible = true;
            return;
        }

        // Get the new main currency
        CurrencyInfo newCurrency = CurrencyInfoProvider.GetCurrencyInfo(newCurrencyId);
        if (newCurrency != null)
        {
            // Only select new main currency when no old main currency specified
            if (mainCurrency == null)
            {
                newCurrency.CurrencyIsMain = true;
                CurrencyInfoProvider.SetCurrencyInfo(newCurrency);

                // Refresh the page
                URLHelper.Redirect(URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "saved", "1"));

                return;
            }

            // Set new main currency and recalculate requested objects
            double rate = 1 / ValidationHelper.GetDouble(txtEchangeRate.Text.Trim(), 1);
            int round = ValidationHelper.GetInteger(txtRound.Text.Trim(), 2);

            try
            {
                RecalculationSettings settings = new RecalculationSettings();
                settings.ExchangeRates = chkExchangeRates.Checked;
                settings.FromGlobalCurrencyRates = (this.plcRecalculateFromGlobal.Visible && chkRecalculateFromGlobal.Checked);
                settings.Products = chkProductPrices.Checked;
                settings.Taxes = chkFlatTaxes.Checked;
                settings.DiscountCoupons = chkFlatDiscountsCoupons.Checked;
                settings.VolumeDiscounts = chkFlatVolumeDiscounts.Checked;
                settings.CreditEvents = chkCredit.Checked;
                settings.ShippingOptions = chkShipping.Checked;
                settings.Documents = (this.plcRecountDocuments.Visible && chkDocuments.Checked);
                settings.ShippingFreeLimit = chkFreeShipping.Checked;
                settings.Orders = chkOrders.Checked;

                // Recalculates the values
                CurrencyInfoProvider.ChangeMainCurrency(editedSiteId, newCurrencyId, rate, round, settings);

                // Refresh the page
                URLHelper.Redirect(URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "saved", "1"));
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                return;
            }
        }
        else
        {
            // No currency selected
            lblError.Text = GetString("StoreSettings_ChangeCurrency.NoNewMainCurrency");
            lblError.Visible = true;
            return;
        }
    }


    /// <summary>
    /// Returns true when at least one checkox is checked.
    /// </summary>
    /// <returns></returns>
    private bool RecalculationRequested()
    {
        return chkExchangeRates.Checked || (this.plcRecalculateFromGlobal.Visible && chkRecalculateFromGlobal.Checked) || chkProductPrices.Checked ||
        chkFlatTaxes.Checked || chkFlatDiscountsCoupons.Checked || chkFlatVolumeDiscounts.Checked || chkCredit.Checked || chkShipping.Checked ||
        (this.plcRecountDocuments.Visible && chkDocuments.Checked) || chkFreeShipping.Checked || chkOrders.Checked;
    }
}
