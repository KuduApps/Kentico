using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ExchangeRates_ExchangeTable_Edit : CMSExchangeRatesPage
{
    #region "Variables"

    private int mExchangeTableId = 0;
    private ExchangeTableInfo exchangeTableObj = null;
    private Hashtable mTextBoxes = new Hashtable();
    private Hashtable mData = new Hashtable();
    private Dictionary<int, DataRow> mExchangeRates = new Dictionary<int, DataRow>();
    private CurrencyInfo mMainCurrency = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        editGrid.RowDataBound += new GridViewRowEventHandler(editGrid_RowDataBound);

        // Get main currency
        mMainCurrency = CurrencyInfoProvider.GetMainCurrency(this.ConfiguredSiteID);

        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        // Control initializations				
        lblExchangeTableValidFrom.Text = GetString("ExchangeTable_Edit.ExchangeTableValidFromLabel");
        lblExchangeTableDisplayName.Text = GetString("ExchangeTable_Edit.ExchangeTableDisplayNameLabel");
        lblExchangeTableValidTo.Text = GetString("ExchangeTable_Edit.ExchangeTableValidToLabel");

        // Help image
        this.imgHelp.ImageUrl = GetImageUrl("General/HelpSmall.png");
        this.imgHelp.ToolTip = GetString("ExchangeTable_Edit.ExchangeRateHelp");
        this.imgHelpFromGlobal.ImageUrl = GetImageUrl("General/HelpSmall.png");
        this.imgHelpFromGlobal.ToolTip = GetString("ExchangeTable_Edit.ExchangeRateHelp");

        lblRates.Text = GetString("ExchangeTable_Edit.ExchangeRates");

        btnOk.Text = GetString("General.OK");
        dtPickerExchangeTableValidFrom.SupportFolder = "~/CMSAdminControls/Calendar";
        dtPickerExchangeTableValidTo.SupportFolder = "~/CMSAdminControls/Calendar";

        string currentTableTitle = GetString("ExchangeTable_Edit.NewItemCaption");

        // Get exchangeTable id from querystring		
        mExchangeTableId = QueryHelper.GetInteger("exchangeid", 0);
        if (mExchangeTableId > 0)
        {
            exchangeTableObj = ExchangeTableInfoProvider.GetExchangeTableInfo(mExchangeTableId);
            EditedObject = exchangeTableObj;

            if (exchangeTableObj != null)
            {
                // Check tables site id
                CheckEditedObjectSiteID(exchangeTableObj.ExchangeTableSiteID);
                // Set title
                currentTableTitle = exchangeTableObj.ExchangeTableDisplayName;

                LoadData(exchangeTableObj);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    // Show that the exchangeTable was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }

            this.CurrentMaster.Title.TitleText = GetString("ExchangeTable_Edit.HeaderCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_ExchangeTable/object.png");

            // Init Copy from global link
            InitCopyFromGlobalLink();
        }
        // Creating a new exchange table
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                // Preset valid from date
                ExchangeTableInfo tableInfo = ExchangeTableInfoProvider.GetLastExchangeTableInfo(this.ConfiguredSiteID);
                if (tableInfo != null)
                {
                    dtPickerExchangeTableValidFrom.SelectedDateTime = tableInfo.ExchangeTableValidTo;
                }
            }
            // Grids are visible only in edit mode
            plcGrid.Visible = false;

            this.CurrentMaster.Title.TitleText = GetString("ExchangeTable_New.HeaderCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_ExchangeTable/new.png");
        }

        // Initializes page title breadcrumbs control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("ExchangeTable_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/ExchangeRates/ExchangeTable_List.aspx?siteId=" + SiteID;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = currentTableTitle;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        this.CurrentMaster.Title.HelpTopicName = "new_rateexchange_rate_edit";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        plcRateFromGlobal.Visible = IsFromGlobalRateNeeded();

        // Check presence of main currency
        string currencyErr = CheckMainCurrency(ConfiguredSiteID);
        if (!string.IsNullOrEmpty(currencyErr))
        {
            // Show message
            lblNoMainCurrency.Text = currencyErr;
            plcNoMainCurrency.Visible = true;
            plcRates.Visible = false;
        }
    }

    #endregion


    #region "Event handlers"

    void editGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItemIndex >= 0)
        {
            TextBox txt = new TextBox();
            txt.CssClass = "ShortTextBox";
            txt.TextChanged += new EventHandler(txt_TextChanged);
            txt.MaxLength = 10;

            // Id of the currency displayed in this row
            int curId = ValidationHelper.GetInteger(((DataRowView)e.Row.DataItem)["CurrencyID"], 0);

            // Find exchange rate for this row currency
            string rateValue = "";
            if (mExchangeRates.ContainsKey(curId))
            {
                DataRow row = mExchangeRates[curId];
                rateValue = ValidationHelper.GetDouble(row["ExchangeRateValue"], -1).ToString();
            }

            // Fill and add text box to the "Rate value" column of the grid
            txt.Text = rateValue;
            e.Row.Cells[1].Controls.Add(txt);
            mData[txt.ClientID] = e.Row.DataItem;
        }
    }


    void txt_TextChanged(object sender, EventArgs e)
    {
        mTextBoxes[((TextBox)sender).ClientID] = sender;
    }


    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "copyfromglobal":
                CopyFromGlobal();
                URLHelper.Redirect("ExchangeTable_Edit.aspx?exchangeid=" + exchangeTableObj.ExchangeTableID + "&saved=1&siteId=" + SiteID);
                break;
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check permissions
        CheckConfigurationModification();

        string errorMessage = new Validator().NotEmpty(txtExchangeTableDisplayName.Text.Trim(), GetString("general.requiresdisplayname")).Result;

        if ((errorMessage == "") && (plcRateFromGlobal.Visible))
        {
            errorMessage = new Validator().NotEmpty(txtGlobalExchangeRate.Text.Trim(), GetString("ExchangeTable_Edit.DoubleFormatRequired")).Result;
        }

        if ((errorMessage == "") && (plcRateFromGlobal.Visible))
        {
            if (!ValidationHelper.IsPositiveNumber(txtGlobalExchangeRate.Text.Trim()) || (ValidationHelper.GetDouble(txtGlobalExchangeRate.Text.Trim(), 0) == 0))
            {
                errorMessage = GetString("ExchangeTable_Edit.errorRate");
            }
        }

        // From/to date validation
        if (errorMessage == "")
        {
            if ((!dtPickerExchangeTableValidFrom.IsValidRange()) || (!dtPickerExchangeTableValidTo.IsValidRange()))
            {
                errorMessage = GetString("general.errorinvaliddatetimerange");
            }

            if ((dtPickerExchangeTableValidFrom.SelectedDateTime != DateTime.MinValue) &&
            (dtPickerExchangeTableValidTo.SelectedDateTime != DateTime.MinValue) &&
            (dtPickerExchangeTableValidFrom.SelectedDateTime >= dtPickerExchangeTableValidTo.SelectedDateTime))
            {
                errorMessage = GetString("General.DateOverlaps");
            }
        }

        // Exchange rates validation
        if (errorMessage == String.Empty)
        {
            foreach (TextBox txt in mTextBoxes.Values)
            {
                string tmp = txt.Text.Trim();
                if (tmp != String.Empty)
                {
                    // Exchange rate mus be double
                    if (!ValidationHelper.IsDouble(tmp))
                    {
                        errorMessage = GetString("ExchangeTable_Edit.DoubleFormatRequired");
                        break;
                    }
                    // Exchange rate must be positive
                    else if (!ValidationHelper.IsPositiveNumber(tmp))
                    {
                        errorMessage = GetString("ExchangeTable_Edit.errorRate");
                    }
                }
            }
        }

        // Save changes if no validation error
        if (errorMessage == "")
        {
            ExchangeTableInfo exchangeTableObj = ExchangeTableInfoProvider.GetExchangeTableInfo(txtExchangeTableDisplayName.Text.Trim(), SiteInfoProvider.GetSiteName(this.ConfiguredSiteID));

            // If exchangeTableName value is unique														
            if ((exchangeTableObj == null) || (exchangeTableObj.ExchangeTableID == mExchangeTableId))
            {

                // Get ExchangeTableInfo object by primary key
                exchangeTableObj = ExchangeTableInfoProvider.GetExchangeTableInfo(mExchangeTableId);
                if (exchangeTableObj == null)
                {
                    // Create new item -> insert
                    exchangeTableObj = new ExchangeTableInfo();
                    exchangeTableObj.ExchangeTableSiteID = ConfiguredSiteID;
                }

                exchangeTableObj.ExchangeTableValidFrom = dtPickerExchangeTableValidFrom.SelectedDateTime;
                exchangeTableObj.ExchangeTableDisplayName = txtExchangeTableDisplayName.Text.Trim();
                exchangeTableObj.ExchangeTableValidTo = dtPickerExchangeTableValidTo.SelectedDateTime;
                exchangeTableObj.ExchangeTableRateFromGlobalCurrency = 0;
                if (plcRateFromGlobal.Visible)
                {
                    exchangeTableObj.ExchangeTableRateFromGlobalCurrency = ValidationHelper.GetDouble(txtGlobalExchangeRate.Text.Trim(), 0);
                }

                // Save general exchange table information
                ExchangeTableInfoProvider.SetExchangeTableInfo(exchangeTableObj);

                // Save rates on edit
                if (mExchangeTableId > 0)
                {
                    foreach (TextBox txt in mTextBoxes.Values)
                    {
                        if (mData[txt.ClientID] != null)
                        {
                            int rateCurrencyId = ValidationHelper.GetInteger(((DataRowView)mData[txt.ClientID])["CurrencyID"], 0);
                            bool rateExists = mExchangeRates.ContainsKey(rateCurrencyId);

                            if (rateExists)
                            {
                                ExchangeRateInfo rate = new ExchangeRateInfo(mExchangeRates[rateCurrencyId]);

                                if (txt.Text.Trim() == String.Empty)
                                {
                                    // Remove exchange rate
                                    ExchangeRateInfoProvider.DeleteExchangeRateInfo(rate);
                                }
                                else
                                {
                                    rate.ExchangeRateValue = ValidationHelper.GetDouble(txt.Text.Trim(), 0);
                                    // Update rate
                                    ExchangeRateInfoProvider.SetExchangeRateInfo(rate);
                                }
                            }
                            else
                            {
                                if (txt.Text.Trim() != String.Empty)
                                {
                                    // Insert exchange rate                      
                                    ExchangeRateInfo rate = new ExchangeRateInfo();
                                    rate.ExchangeRateToCurrencyID = rateCurrencyId;
                                    rate.ExchangeRateValue = ValidationHelper.GetDouble(txt.Text.Trim(), 0);
                                    rate.ExchangeTableID = mExchangeTableId;

                                    ExchangeRateInfoProvider.SetExchangeRateInfo(rate);
                                }
                            }
                        }
                    }
                }

                URLHelper.Redirect("ExchangeTable_Edit.aspx?exchangeid=" + exchangeTableObj.ExchangeTableID + "&saved=1&siteId=" + SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("ExchangeTable_Edit.CurrencyNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Load data of editing exchangeTable.
    /// </summary>
    /// <param name="exchangeTableObj">ExchangeTable object</param>
    protected void LoadData(ExchangeTableInfo exchangeTableObj)
    {
        editGrid.Columns[0].HeaderText = GetString("ExchangeTable_Edit.ToCurrency");
        editGrid.Columns[1].HeaderText = GetString("ExchangeTable_Edit.RateValue");
        lblToCurrency.Text = GetString("ExchangeTable_Edit.ToCurrency");
        lblRateValue.Text = GetString("ExchangeTable_Edit.RateValue");

        // Get exchange rates and fill the dictionary
        DataSet dsExRates = ExchangeRateInfoProvider.GetExchangeRates(mExchangeTableId);
        if (!DataHelper.DataSourceIsEmpty(dsExRates))
        {
            foreach (DataRow dr in dsExRates.Tables[0].Rows)
            {
                int toCurrencyId = ValidationHelper.GetInteger(dr["ExchangeRateToCurrencyID"], -1);
                if (!mExchangeRates.ContainsKey(toCurrencyId))
                {
                    mExchangeRates.Add(toCurrencyId, dr);
                }
            }
        }

        DataSet dsAllCurrencies = CurrencyInfoProvider.GetCurrencies(this.ConfiguredSiteID, false);
        // Row index of main currency
        int mainCurrIndex = -1;
        int i = 0;

        if (!DataHelper.DataSourceIsEmpty(dsAllCurrencies))
        {
            // Find main currency in all currencies dataset
            if (mMainCurrency != null)
            {
                // Prepare site main currency unit label
                string siteCode = mMainCurrency.CurrencyCode;
                lblSiteMainCurrency.Text = siteCode;
                lblMainToSite.Text = string.Format(GetString("ExchangeTable_Edit.FromMainToSite"), HTMLHelper.HTMLEncode(siteCode));

                // Prepare global main currency unit label
                string globalCode = CurrencyInfoProvider.GetMainCurrencyCode(0);
                lblFromGlobalToMain.Text = string.Format(GetString("ExchangeTable_Edit.FromGlobalToMain"), HTMLHelper.HTMLEncode(globalCode));

                foreach (DataRow dr in dsAllCurrencies.Tables[0].Rows)
                {
                    if (ValidationHelper.GetInteger(dr["CurrencyID"], -1) == mMainCurrency.CurrencyID)
                    {
                        mainCurrIndex = i;
                    }
                    i++;
                }
            }

            // Remove found main currency
            if (mainCurrIndex != -1)
            {
                dsAllCurrencies.Tables[0].Rows[mainCurrIndex].Delete();
                dsAllCurrencies.AcceptChanges();
            }
        }

        if (DataHelper.DataSourceIsEmpty(dsAllCurrencies))
        {
            // Site exchange rates section is visible only when more currencies exist
            plcSiteRates.Visible = false;
            lblMainToSite.Visible = false;
            plcNoCurrency.Visible = true;
        }

        // Hide rates part when no grid visible
        plcGrid.Visible = plcSiteRates.Visible || plcRateFromGlobal.Visible;

        // Use currencies in grid
        editGrid.DataSource = dsAllCurrencies;
        editGrid.DataBind();

        // Fill editing form
        if (!RequestHelper.IsPostBack())
        {
            dtPickerExchangeTableValidFrom.SelectedDateTime = exchangeTableObj.ExchangeTableValidFrom;
            txtExchangeTableDisplayName.Text = exchangeTableObj.ExchangeTableDisplayName;
            dtPickerExchangeTableValidTo.SelectedDateTime = exchangeTableObj.ExchangeTableValidTo;
            txtGlobalExchangeRate.Text = Convert.ToString(exchangeTableObj.ExchangeTableRateFromGlobalCurrency);
        }
    }


    /// <summary>
    /// Initializes copy from global link. 
    /// </summary>
    protected void InitCopyFromGlobalLink()
    {
        // Nothing to be done
        if ((mMainCurrency == null) || string.IsNullOrEmpty(mMainCurrency.CurrencyCode))
        {
            return;
        }

        // Show copy from global link when not configuring global currencies.
        if (ConfiguredSiteID != 0)
        {
            // Allow copying only if global main currency and site main currency are the same
            if (mMainCurrency.CurrencyCode == CurrencyInfoProvider.GetMainCurrencyCode(0))
            {
                string[,] actions = new string[2, 9];

                // Show "Copy from global" link only if there is at least one global exchange rate table
                DataSet ds = ExchangeTableInfoProvider.GetExchangeTables("ExchangeTableSiteID IS NULL", null, 1, "ExchangeTableID");
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
                    actions[1, 1] = GetString("general.copyfromglobal");
                    actions[1, 2] = "return ConfirmCopyFromGlobal();";
                    actions[1, 3] = null;
                    actions[1, 4] = null;
                    actions[1, 5] = GetImageUrl("Objects/Ecommerce_ExchangeTable/fromglobal.png");
                    actions[1, 6] = "copyFromGlobal";
                    actions[1, 7] = String.Empty;
                    actions[1, 8] = true.ToString();

                    // Register javascript to confirm generate 
                    string script = "function ConfirmCopyFromGlobal() {return confirm(" + ScriptHelper.GetString(GetString("com.ConfirmExchangeTableFromGlobal")) + ");}";
                    ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ConfirmCopyFromGlobal", ScriptHelper.GetScript(script));

                    this.CurrentMaster.HeaderActions.Actions = actions;
                    this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);
                }
            }
        }
    }


    /// <summary>
    ///  Copies site-specific exchange rates from last valid global exchange table.
    /// </summary>
    protected void CopyFromGlobal()
    {
        CheckConfigurationModification();

        ExchangeTableInfo globalTable = ExchangeTableInfoProvider.GetLastExchangeTableInfo(0);
        
        if (globalTable != null)
        {
            ExchangeRateInfoProvider.CopyExchangeRates(globalTable.ExchangeTableID, mExchangeTableId);
        }
    }


    /// <summary>
    /// Indicates if exchange rate from global main currency is needed.
    /// </summary>
    protected bool IsFromGlobalRateNeeded()
    {
        string siteName = SiteInfoProvider.GetSiteName(this.ConfiguredSiteID);

        if((this.ConfiguredSiteID == 0) || (ECommerceSettings.UseGlobalCurrencies(siteName)))
        {
            return false;
        }

        string globalMainCode = CurrencyInfoProvider.GetMainCurrencyCode(0);
        string siteMainCode = CurrencyInfoProvider.GetMainCurrencyCode(this.ConfiguredSiteID);

        // Check whether main currencies are defined
        if (string.IsNullOrEmpty(siteMainCode) || string.IsNullOrEmpty(globalMainCode))
        {
            return false;
        }

        // Check whether global and site main currency are the same
        if(globalMainCode.ToLower() == siteMainCode.ToLower())
        {
            return false;
        }

        return ECommerceSettings.AllowGlobalDiscountCoupons(siteName) || 
            ECommerceSettings.AllowGlobalProductOptions(siteName) || 
            ECommerceSettings.AllowGlobalProducts(siteName) ||
            ECommerceSettings.AllowGlobalShippingOptions(siteName) || 
            ECommerceSettings.UseGlobalCredit(siteName) || 
            ECommerceSettings.UseGlobalTaxClasses(siteName);
    }

    #endregion
}
