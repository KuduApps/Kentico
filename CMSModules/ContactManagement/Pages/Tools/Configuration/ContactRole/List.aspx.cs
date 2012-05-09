using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.OnlineMarketing;

// Title
[Title("Objects/OM_ContactRole/object.png", "om.ContactRole.list", "onlinemarketing_ContactRole_list")]

public partial class CMSModules_ContactManagement_Pages_Tools_Configuration_ContactRole_List : CMSContactManagementContactRolePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // In cmsdesk "siteID is available in control
        if (!this.IsSiteManager)
        {
            // Init site filter when user is authorised for global and site account statuses
            if (AuthorizedForGlobalConfiguration && AuthorizedForSiteConfiguration)
            {
                CurrentMaster.DisplaySiteSelectorPanel = true;
                if (!RequestHelper.IsPostBack())
                {
                    SiteOrGlobalSelector.SiteID = QueryHelper.GetInteger("siteId", CMSContext.CurrentSiteID);
                }
                SiteID = SiteOrGlobalSelector.SiteID;
            }
            // User is authorised for site account statuses
            else if (AuthorizedForSiteConfiguration)
            {
                SiteID = CMSContext.CurrentSiteID;
            }
            // User is authorised only for global account statuses
            else if (AuthorizedForGlobalConfiguration)
            {
                SiteID = UniSelector.US_GLOBAL_RECORD;
            }
            // User is not authorized
            else
            {
                RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadConfiguration");
            }
        }

        // Set header actions (add button)
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("om.ContactRole.new");
        actions[0, 3] = AddSiteQuery("Edit.aspx", null);
        actions[0, 5] = GetImageUrl("Objects/OM_ContactRole/add.png");
        hdrActions.Actions = actions;

        // Filter site data
        Grid.WhereCondition = SqlHelperClass.AddWhereCondition(Grid.WhereCondition, GetSiteFilter("ContactRoleSiteID"));
        Grid.OnBeforeDataReload += () => { Grid.NamedColumns["sitename"].Visible = ((SiteID < 0) && (SiteID != UniSelector.US_GLOBAL_RECORD)); };
        Grid.EditActionUrl = AddSiteQuery(Grid.EditActionUrl, null);
        Grid.ZeroRowsText = GetString("om.contactrole.notfound");
        Grid.OnAction += new OnActionEventHandler(Grid_OnAction);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Check permissions to create new record
        if ((SiteID > 0) && !ModifySiteConfiguration)
        {
            hdrActions.Enabled = false;
        }
        else if ((SiteID <= 0) && !ModifyGlobalConfiguration)
        {
            hdrActions.Enabled = false;
        }
        // Allow new button only for particular sites or (global) site
        else if ((SiteID < 0) && (SiteID != UniSelector.US_GLOBAL_RECORD))
        {
            hdrActions.Enabled = false;
            lblWarnNew.Visible = true;
        }
    }


    /// <summary>
    /// UniGrid action handler.
    /// </summary>
    void Grid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            ContactRoleInfo cri = ContactRoleInfoProvider.GetContactRoleInfo(ValidationHelper.GetInteger(actionArgument, 0));
            if (cri != null)
            {
                if (ConfigurationHelper.AuthorizedModifyConfiguration(cri.ContactRoleSiteID, true))
                {
                    ContactRoleInfoProvider.DeleteContactRoleInfo(cri);
                }
            }
        }
    }
}