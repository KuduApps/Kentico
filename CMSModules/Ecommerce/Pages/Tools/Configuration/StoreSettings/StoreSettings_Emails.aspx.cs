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

using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_Emails : CMSEcommerceStoreSettingsPage, IPostBackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.Settings.Emails"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.Settings.Emails");
        }

        // Register scripts for saving document by shortcut
        ScriptHelper.RegisterSaveShortcut(this, "save", false);        

        // Set up header
        CurrentMaster.HeaderActions.Actions = GetHeaderActions();
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;

        // Assign category id, site id
        this.SettingsGroupViewer.CategoryName = "CMS.ECommerce";
        this.SettingsGroupViewer.Where = "CategoryName = N'CMS.Ecommerce.Emails'";
        this.SettingsGroupViewer.SiteID = this.SiteID;
        this.SettingsGroupViewer.ShowExportLink = false;
        this.SettingsGroupViewer.AllowGlobalInfoMessage = false;
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

    #endregion
}
