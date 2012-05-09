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
using System.Collections.Generic;

using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_General : CMSEcommerceStoreSettingsPage, IPostBackEventHandler
{
    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        GlobalObjectsKeyName = ECommerceSettings.USE_GLOBAL_CURRENCIES;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        CheckPermissions();
        InitializeControls();
        RegisterClientScripts();

        // Register custom info message handler
        this.SettingsGroupViewer.OnInfoMessageChanged += new CMSModules_Settings_Controls_SettingsGroupViewer.InfoMessageChangedHandler(SettingsGroupViewer_OnInfoMessageChanged);
        
        // Init label for accessibility
        this.lblHdnChangeCurrency.Text = GetString("general.change");
        this.lblHdnChangeCurrency.Style["display"] = "none";

        // Append tooltip
        ScriptHelper.AppendTooltip(this.lblCurrentMainCurrency, GetString("com.maincurrencytooltip"), "help");

        // Display info message when main currency saved
        if (QueryHelper.GetBoolean("currencysaved", false))
        {
            this.lblInfo.Visible = true;
            this.lblInfo.Text = GetString("com.storesettings.maincurrencychanged"); ;
        }
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Gets current main currency.
    /// </summary>
    private void GetCurrentMainCurrency()
    {
        int mainCurrencyId = -1;

        CurrencyInfo mainCurrency = CurrencyInfoProvider.GetMainCurrency(this.ConfiguredSiteID);
        if (mainCurrency != null)
        {
            mainCurrencyId = mainCurrency.CurrencyID;
            lblMainCurrency.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(mainCurrency.CurrencyDisplayName));
        }
        else
        {
            lblMainCurrency.Text = GetString("general.na");
        }

        DataSet ds = CurrencyInfoProvider.GetCurrencies("CurrencyEnabled = 1 AND ISNULL(CurrencySiteID, 0) = " + this.ConfiguredSiteID + " AND NOT CurrencyID = " + mainCurrencyId, null);
        // When there is no choice
        if (DataHelper.DataSourceIsEmpty(ds))
        {
            // Disable "change main currency" button
            this.btnChangeCurrency.Enabled = false;
        }
    }


    /// <summary>
    /// Registers client script for the page.
    /// </summary>
    private void RegisterClientScripts()
    {
        // Register scripts for saving document by shortcut
        ScriptHelper.RegisterSaveShortcut(this, "save", false);

        // Register scripts for currency change
        ScriptHelper.RegisterDialogScript(this);
    }


    /// <summary>
    /// Checks current user for permissions for CMS Desk / Ecommerce.
    /// </summary>
    private void CheckPermissions()
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.Settings.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.Settings.General");
        }
    }


    /// <summary>
    /// Initialization of form controls.
    /// </summary>
    private void InitializeControls()
    {
        CurrentMaster.HeaderActions.Actions = GetHeaderActions();
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;

        // Assign category id, site id
        this.SettingsGroupViewer.CategoryName = "CMS.ECommerce";
        this.SettingsGroupViewer.Where = "CategoryName IN (N'CMS.Ecommerce.Accounts', N'CMS.Ecommerce.Shipping', N'CMS.Ecommerce.Taxes')";
        this.SettingsGroupViewer.SiteID = this.SiteID;
        this.SettingsGroupViewer.ShowExportLink = false;
        this.SettingsGroupViewer.AllowGlobalInfoMessage = false;

        GetCurrentMainCurrency();

        btnChangeCurrency.OnClientClick = "modalDialog('StoreSettings_ChangeCurrency.aspx?siteId=" + this.ConfiguredSiteID + "', 'ChangeMainCurrency', 600, 480); return false;";
    }


    /// <summary>
    /// Gets string array representing header actions.
    /// </summary>
    /// <returns>Array of strings</returns>
    private string[,] GetHeaderActions()
    {
        string[,] actions = new string[1, 9];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("Header.Settings.SaveChanged");
        actions[0, 2] = "";
        actions[0, 3] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "lnkSaveChanges_Click";
        actions[0, 7] = String.Empty;
        actions[0, 8] = true.ToString();

        return actions;
    }


    private void SaveChanges()
    {
        // Check 'EcommerceModify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Ecommerce", "ConfigurationModify"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "ConfigurationModify");
            return;
        }

        this.SettingsGroupViewer.SaveChanges();
    }


    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Handles actions performed on the master header.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "lnksavechanges_click":
                SaveChanges();
                break;
        }
    }


    /// <summary>
    /// Handles postback events.
    /// </summary>
    /// <param name="eventArgument">Postback argument</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument.ToLower() == "save")
        {
            SaveChanges();
        }
    }


    /// <summary>
    /// Ensures displaying of info message.
    /// </summary>
    /// <param name="message">Message to show</param>
    /// <param name="visible">Visibility of message</param>
    /// <param name="isError">Error indicator</param>
    private void SettingsGroupViewer_OnInfoMessageChanged(string message, bool visible, bool isError)
    {
        this.lblInfo.Visible = visible;
        if (visible)
        {
            this.lblInfo.Text = message;
            if (isError)
            {
                this.lblInfo.CssClass = "ErrorLabel";
            }
        }
    }


    #endregion
}
