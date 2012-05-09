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
[Title("Objects/OM_Contact/object.png", "om.contact.list", "onlinemarketing_contact_list")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_List : CMSContactManagementContactsPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // In cmsdesk siteID is in control
        if (!this.IsSiteManager)
        {
            // Init site filter when user is authorised for global and site contacts
            if (AuthorizedForGlobalContacts && AuthorizedForSiteContacts)
            {
                CurrentMaster.DisplaySiteSelectorPanel = true;
                if (!RequestHelper.IsPostBack())
                {
                    SiteOrGlobalSelector.SiteID = QueryHelper.GetInteger("siteId", CMSContext.CurrentSiteID);
                }
                listElem.SiteID = SiteOrGlobalSelector.SiteID;
                if (listElem.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD)
                {
                    listElem.WhereCondition = SiteOrGlobalSelector.GetSiteWhereCondition("ContactSiteID");
                }
            }
            // Authorised for site contacts only
            else if (AuthorizedForSiteContacts)
            {
                listElem.SiteID = CMSContext.CurrentSiteID;
            }
            // Authorised for global contacts only
            else if (AuthorizedForGlobalContacts)
            {
                listElem.SiteID = UniSelector.US_GLOBAL_RECORD;
            }
            // User is not authorized
            else
            {
                RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadContacts");
            }
        }
        // In sitemanager "siteID" is in querystring
        else
        {
            listElem.SiteID = this.SiteID;
            // Hide title
            CurrentMaster.Title.TitleText = CurrentMaster.Title.TitleImage = string.Empty;
        }

        // Set header actions (add button)
        string url = this.ResolveUrl("New.aspx?siteId=" + listElem.SiteID);
        if (this.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "isSiteManager", "1");
        }     
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("om.contact.new");
        actions[0, 3] = url;
        actions[0, 5] = GetImageUrl("Objects/OM_Contact/add.png");
        hdrActions.Actions = actions;

        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "Contacts", null, "menu");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Disable actions for unauthorized users
        if (!ContactHelper.AuthorizedModifyContact(listElem.SiteID, false))
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
