using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;

// Title
[Title("Objects/OM_Account/object.png", "om.account.list", "onlinemarketing_account_list")]

public partial class CMSModules_ContactManagement_Pages_Tools_Account_List : CMSContactManagementAccountsPage
{
    #region "Variables"

    private int siteId = CMSContext.CurrentSiteID;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // In cmsdesk "siteID is available in control
        if (!this.IsSiteManager)
        {
            // Init site filter when user is authorised for global and site accounts
            if (AuthorizedForGlobalAccounts && AuthorizedForSiteAccounts)
            {
                CurrentMaster.DisplaySiteSelectorPanel = true;
                if (!RequestHelper.IsPostBack())
                {
                    SiteOrGlobalSelector.SiteID = QueryHelper.GetInteger("siteId", siteId);
                }
                siteId = SiteOrGlobalSelector.SiteID;
                if (siteId == UniSelector.US_GLOBAL_OR_SITE_RECORD)
                {
                    listElem.WhereCondition = SiteOrGlobalSelector.GetSiteWhereCondition("AccountSiteID");
                }
            }
            // User is authorised for site accounts
            else if (AuthorizedForSiteAccounts)
            {
                // Use default value = current site ID
            }
            // User is authorised only for global accounts
            else if (AuthorizedForGlobalAccounts)
            {
                siteId = UniSelector.US_GLOBAL_RECORD;
            }
            // User is not authorized
            else
            {
                RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadAccounts");
            }
        }
        // In sitemanager "siteID" is in querystring
        else
        {
            siteId = this.SiteID;

            // Hide title
            CurrentMaster.Title.TitleText = CurrentMaster.Title.TitleImage = string.Empty;
        }

        // Set header actions (add button)
        listElem.SiteID = siteId;
        string url = this.ResolveUrl("New.aspx?siteId=" + siteId);
        if (this.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "isSiteManager", "1");
        }
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("om.account.new");
        actions[0, 3] = url;
        actions[0, 5] = GetImageUrl("Objects/OM_Account/add.png");
        hdrActions.Actions = actions;

        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "Accounts", null, "menu");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Disable actions for unauthorized users
        if (!AccountHelper.AuthorizedModifyAccount(listElem.SiteID, false))
        {
            hdrActions.Enabled = false;
        }
        // Allow new button only for particular sites or (global) site
        else if ((listElem.SiteID < 0) && (listElem.SiteID != UniSelector.US_GLOBAL_RECORD))
        {
            hdrActions.Enabled = false;
            lblWarnNew.Visible = true;
        }
    }

    #endregion
}
