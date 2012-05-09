using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_Country : CMSTaxClassesPage
{
    #region "Variables"

    protected string mSave = null;
    protected int mTaxClassId = 0;
    protected TaxClassInfo mTaxClassObj = null;
    protected string currencyCode = "";
    private Hashtable mChangedTextBoxes = new Hashtable();
    private Hashtable mChangedCheckBoxes = new Hashtable();

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(this);

        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.TaxClasses.Countries"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.TaxClasses.Countries");
        }

        mTaxClassId = QueryHelper.GetInteger("taxclassid", 0);

        mTaxClassObj = TaxClassInfoProvider.GetTaxClassInfo(mTaxClassId);
        EditedObject = mTaxClassObj;
        // Check if configured tax class belongs to configured site
        if (mTaxClassObj != null)
        {
            CheckEditedObjectSiteID(mTaxClassObj.TaxClassSiteID);

            currencyCode = CurrencyInfoProvider.GetMainCurrencyCode(mTaxClassObj.TaxClassSiteID);

            // Check presence of main currency
            string currencyErr = CheckMainCurrency(mTaxClassObj.TaxClassSiteID);
            if (!string.IsNullOrEmpty(currencyErr))
            {
                // Show message
                lblError.Text = currencyErr;
                lblError.Visible = true;
            }
        }

        GridViewCountries.Columns[0].HeaderText = GetString("TaxClass_Country.Country");
        GridViewCountries.Columns[1].HeaderText = GetString("TaxClass_Country.Value");
        GridViewCountries.Columns[2].HeaderText = GetString("TaxClass_Country.IsFlat");
        DataSet ds = TaxClassCountryInfoProvider.GetCountriesAndTaxValues(mTaxClassId);
        GridViewCountries.Columns[3].Visible = true;
        GridViewCountries.DataSource = ds.Tables[0];
        GridViewCountries.DataBind();
        // After id is copied, the 4th column it's not needed anymore
        GridViewCountries.Columns[3].Visible = false;
        GridViewCountries.GridLines = GridLines.Horizontal;

        // Init scripts
        string currencySwitchScript = string.Format(@"
function switchCurrency(isFlatValue, labelId){{
  if(isFlatValue)
  {{
    jQuery('#'+labelId).html('({0})');
  }}else{{
    jQuery('#'+labelId).html('(%)');
  }}
}}", ScriptHelper.GetString(currencyCode, false));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "CurrencySwitch", currencySwitchScript, true);
        ScriptHelper.RegisterStartupScript(this, typeof(string), "InitializeGrid", "jQuery('input[id*=\"chkIsFlatValue\"]').change();", true);

        mSave = GetString("general.save");

        // Set header actions
        string[,] actions = new string[1, 10];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = this.GetString("Header.Settings.SaveChanged");
        actions[0, 2] = String.Empty;
        actions[0, 3] = String.Empty;
        actions[0, 4] = String.Empty;
        actions[0, 5] = this.GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = true.ToString();

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += this.HeaderActions_ActionPerformed;
    }


    protected void GridViewCountries_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridViewCountries.Rows.Count; i++)
        {
            // Copy id from 5th column to invisible label in last column
            Label id = new Label();
            id.Visible = false;
            id.Text = GridViewCountries.Rows[i].Cells[3].Text;
            GridViewCountries.Rows[i].Cells[3].Controls.Add(id);

            GridViewRow row = GridViewCountries.Rows[i];

            TextBox txtValue = ControlsHelper.GetChildControl(row, typeof(TextBox), "txtTaxValue") as TextBox;
            txtValue.ID = "txtTaxValue" + id.Text;

            CheckBox chkIsFlat = ControlsHelper.GetChildControl(row, typeof(CheckBox), "chkIsFlatValue") as CheckBox;
            chkIsFlat.ID = "chkIsFlatValue" + id.Text;

            Label lblCurrency = ControlsHelper.GetChildControl(row, typeof(Label), "lblCurrency") as Label;
            if (lblCurrency != null)
            {
                chkIsFlat.InputAttributes["onclick"] = "switchCurrency(this.checked, '" + lblCurrency.ClientID + "')";
                chkIsFlat.InputAttributes["onchange"] = "switchCurrency(this.checked, '" + lblCurrency.ClientID + "')";
            }
        }
    }


    protected void txtTaxValue_Changed(object sender, EventArgs e)
    {
        mChangedTextBoxes[((TextBox)sender).ID] = sender;
    }


    protected void chkIsFlatValue_Changed(object sender, EventArgs e)
    {
        mChangedCheckBoxes[((CheckBox)sender).ID] = sender;
    }


    protected bool ConvertToBoolean(object value)
    {
        return ValidationHelper.GetBoolean(value, false);
    }


    private void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "save":
                Save();
                break;
        }
    }


    // Saves
    protected void Save()
    {
        // Check permissions
        CheckConfigurationModification();

        string errorMessage = String.Empty;
        bool saved = false;

        // Loop through countries
        for (int i = 0; i < this.GridViewCountries.Rows.Count; i++)
        {
            Label lblCountryId = (Label)this.GridViewCountries.Rows[i].Cells[3].Controls[0];
            int countryId = ValidationHelper.GetInteger(lblCountryId.Text, 0);

            TaxClassCountryInfo countryInfo;
            string countryName = null;
            TextBox txtValue = null;
            CheckBox chkIsFlat = null;

            if (countryId > 0)
            {
                countryName = ((Label)GridViewCountries.Rows[i].Cells[0].Controls[1]).Text;
                txtValue = (TextBox)GridViewCountries.Rows[i].Cells[1].Controls[1];
                chkIsFlat = (CheckBox)GridViewCountries.Rows[i].Cells[2].Controls[1];

                if ((this.mChangedTextBoxes[txtValue.ID] != null) || (this.mChangedCheckBoxes[chkIsFlat.ID]) != null)
                {
                    // Remove country tax information if tax value is empty
                    if (String.IsNullOrEmpty(txtValue.Text))
                    {
                        TaxClassCountryInfoProvider.RemoveCountryTaxValue(countryId, this.mTaxClassId);
                        chkIsFlat.Checked = false;
                        saved = true;
                    }
                    // Update country tax information if tax value is not empty
                    else
                    {
                        // Valid percentage or flat tax value
                        if ((!chkIsFlat.Checked && ValidationHelper.IsInRange(0, 100, ValidationHelper.GetDouble(txtValue.Text, -1))) || (chkIsFlat.Checked && ValidationHelper.IsPositiveNumber(txtValue.Text)))
                        {
                            countryInfo = TaxClassCountryInfoProvider.GetTaxClassCountryInfo(countryId, this.mTaxClassId);
                            countryInfo = countryInfo ?? new TaxClassCountryInfo();

                            countryInfo.CountryID = countryId;
                            countryInfo.TaxClassID = mTaxClassId;
                            countryInfo.TaxValue = ValidationHelper.GetDouble(txtValue.Text, 0);
                            countryInfo.IsFlatValue = chkIsFlat.Checked;

                            TaxClassCountryInfoProvider.SetTaxClassCountryInfo(countryInfo);
                            saved = true;
                        }
                        // Invalid tax value
                        else
                        {
                            errorMessage += countryName + ", ";
                        }
                    }
                }
            }
        }

        // Error message
        if (!String.IsNullOrEmpty(errorMessage))
        {
            // Remove last comma
            if (errorMessage.EndsWith(", "))
            {
                errorMessage = errorMessage.Remove(errorMessage.Length - 2, 2);
            }

            this.lblError.Visible = true;
            this.lblError.Text = String.Format("{0} - {1}", errorMessage, GetString("Com.Error.TaxValue"));
        }
        
        // Display info message if some records were saved
        if (String.IsNullOrEmpty(errorMessage) || saved)
        {
            // Info message
            this.lblInfo.Visible = true;
            this.lblInfo.Text = GetString("General.ChangesSaved");
        }
    }

    #endregion
}
