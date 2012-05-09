using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_GlobalObjects : CMSEcommerceStoreSettingsPage, IPostBackEventHandler
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            this.RedirectToAccessDenied(GetString("security.accesspage.onlyglobaladmin"));
            return;
        }

        // Register scripts for saving document by shortcut
        ScriptHelper.RegisterSaveShortcut(this, "save", false);        

        // Set up header
        CurrentMaster.HeaderActions.Actions = GetHeaderActions();
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;

        // Assign category id, site id
        this.SettingsGroupViewer.CategoryName = "CMS.Ecommerce.GlobalObjects";
        this.SettingsGroupViewer.SiteID = this.SiteID;
        this.SettingsGroupViewer.ShowExportLink = false;
        this.SettingsGroupViewer.AllowGlobalInfoMessage = false;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        string siteName = SiteInfoProvider.GetSiteName(this.SiteID);
        
        // Show warning when using global exchange rates and site specific currencies
        if (ECommerceSettings.UseGlobalExchangeRates(siteName) && !ECommerceSettings.UseGlobalCurrencies(siteName))
        {
            lblError.Text = GetString("com.WrongCurrencyRateCombination");
            lblError.Visible = true;
        }
    }

    #endregion


    #region "Private methods"


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
                this.SettingsGroupViewer.SaveChanges();
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
            this.SettingsGroupViewer.SaveChanges();
        }
    }

    #endregion
}

