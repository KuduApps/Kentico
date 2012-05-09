using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_State : CMSTaxClassesPage
{
    #region "Variables"

    protected string mSave = "";
    protected int mTaxClassId = 0;
    protected int mCountryId = 0;
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
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.TaxClasses.States"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.TaxClasses.States");
        }

        // Get tax class Id from URL
        mTaxClassId = QueryHelper.GetInteger("taxclassid", 0);

        mTaxClassObj = TaxClassInfoProvider.GetTaxClassInfo(mTaxClassId);
        EditedObject = mTaxClassObj;
        // Check if configured tax class belongs to configured site
        if (mTaxClassObj != null)
        {
            // Check site id of tax class
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

        if (!RequestHelper.IsPostBack())
        {
            // Fill the drpCountry with countries which have some states or colonies
            drpCountry.DataSource = CountryInfoProvider.GetCountriesWithStates();
            drpCountry.DataValueField = "CountryID";
            drpCountry.DataTextField = "CountryDisplayName";
            drpCountry.DataBind();
        }
        // Set grid view properties
        gvStates.Columns[0].HeaderText = GetString("taxclass_state.gvstates.state");
        gvStates.Columns[1].HeaderText = GetString("Code");
        gvStates.Columns[2].HeaderText = GetString("taxclass_state.gvstates.value");
        gvStates.Columns[3].HeaderText = GetString("taxclass_state.gvstates.isflat");
        gvStates.Columns[4].HeaderText = GetString("StateId");

        gvStates.GridLines = GridLines.Horizontal;

        LoadGridViewData();

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
        this.CurrentMaster.DisplaySiteSelectorPanel = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        gvStates.Columns[1].Visible = false;
    }


    protected void LoadGridViewData()
    {
        gvStates.Columns[4].Visible = true;
        mCountryId = ValidationHelper.GetInteger(drpCountry.SelectedValue, 0);
        DataSet ds = TaxClassStateInfoProvider.GetStatesAndTaxValues(mTaxClassId, mCountryId);
        gvStates.DataSource = ds.Tables[0];
        gvStates.DataBind();
        gvStates.Columns[4].Visible = false;
    }


    protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGridViewData();
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


    /// <summary>
    /// Saves values.
    /// </summary>
    protected void Save()
    {
        // Check permissions
        CheckConfigurationModification();

        string errorMessage = String.Empty;
        bool saved = false;

        // Loop through states
        for (int i = 0; i < gvStates.Rows.Count; i++)
        {
            Label lblStateId = (Label)gvStates.Rows[i].Cells[4].Controls[0];
            int stateId = ValidationHelper.GetInteger(lblStateId.Text, 0);

            TaxClassStateInfo stateInfo;
            string stateName = null;
            TextBox txtValue = null;
            CheckBox chkIsFlat = null;

            if (stateId > 0)
            {
                stateName = ((Label)gvStates.Rows[i].Cells[0].Controls[1]).Text;
                txtValue = (TextBox)gvStates.Rows[i].Cells[2].Controls[1];
                chkIsFlat = (CheckBox)gvStates.Rows[i].Cells[3].Controls[1];

                if ((this.mChangedTextBoxes[txtValue.ID] != null) || (this.mChangedCheckBoxes[chkIsFlat.ID] != null))
                {
                    // Remove state tax information if tax value is empty
                    if (String.IsNullOrEmpty(txtValue.Text))
                    {
                        TaxClassStateInfoProvider.RemoveStateTaxValue(this.mTaxClassId, stateId);
                        chkIsFlat.Checked = false;
                        saved = true;
                    }
                    // Update state tax information if tax value is not empty
                    else
                    {
                        // Valid percentage or flat tax value
                        if ((!chkIsFlat.Checked && ValidationHelper.IsInRange(0, 100, ValidationHelper.GetDouble(txtValue.Text, -1))) || (chkIsFlat.Checked && ValidationHelper.IsPositiveNumber(txtValue.Text)))
                        {
                            stateInfo = TaxClassStateInfoProvider.GetTaxClassStateInfo(this.mTaxClassId, stateId);
                            stateInfo = stateInfo ?? new TaxClassStateInfo();

                            stateInfo.StateID = stateId;
                            stateInfo.TaxClassID = mTaxClassId;
                            stateInfo.TaxValue = ValidationHelper.GetDouble(txtValue.Text, 0);
                            stateInfo.IsFlatValue = chkIsFlat.Checked;

                            TaxClassStateInfoProvider.SetTaxClassStateInfo(stateInfo);
                            saved = true;
                        }
                        // Invalid tax value
                        else
                        {
                            errorMessage += stateName + ", ";
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


    protected void gvStates_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gvStates.Rows.Count; i++)
        {
            // Copy id from 5th column to invisible label in last column
            Label id = new Label();
            id.Visible = false;
            id.Text = gvStates.Rows[i].Cells[4].Text;
            gvStates.Rows[i].Cells[4].Controls.Add(id);

            GridViewRow row = gvStates.Rows[i];

            // Set unique text box ID
            TextBox txtValue = ControlsHelper.GetChildControl(row, typeof(TextBox), "txtTaxValue") as TextBox;
            txtValue.ID = "txtTaxValue" + id.Text;

            // Set unique check box ID
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

    #endregion
}