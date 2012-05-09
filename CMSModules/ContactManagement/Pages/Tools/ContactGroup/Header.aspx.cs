using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACTGROUP, "groupid")]

// Title panel
[Title("Objects/OM_ContactGroup/object.png", "om.contactgroup.edit", "onlinemarketing_contactgroup_general")]

// Tabs
[Tabs(3, "content")]
[Tab(0, "general.general", "Tab_General.aspx", "SetHelpTopic('helpTopic', 'onlinemarketing_contactgroup_general');")]
[Tab(1, "om.contact.list", "Tab_Contacts.aspx", "SetHelpTopic('helpTopic', 'onlinemarketing_contactgroup_contacts');")]
[Tab(2, "om.account.list", "Tab_Accounts.aspx", "SetHelpTopic('helpTopic', 'onlinemarketing_contactgroup_accounts');")]

public partial class CMSModules_ContactManagement_Pages_Tools_ContactGroup_Header : CMSContactManagementContactGroupsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = ResolveUrl("~/CMSModules/ContactManagement/Pages/Tools/ContactGroup/List.aspx");
        url = URLHelper.AddParameterToUrl(url, "siteid", this.SiteID.ToString());

        // Check if running under site manager (and distribute "site manager" flag to other tabs)
        if (this.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");

            // Hide title
            CurrentMaster.Title.TitleText = CurrentMaster.Title.TitleImage = string.Empty;
        }

        CurrentPage.InitBreadcrumbs(2);
        CurrentPage.SetBreadcrumb(0, GetString("om.contactgroup.list"), url, "_parent", null);
        CurrentPage.SetBreadcrumb(1, CMSContext.ResolveMacros("{%EditedObject.DisplayName%}"), null, null, null);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        UITabs tabs = ((CMSMasterPage)CurrentMaster).Tabs;

        // Remove 'saved' query parameter
        string query = URLHelper.RemoveUrlParameter(URLHelper.Url.Query, "saved");
        for (int i = 0; i < tabs.Tabs.GetLength(0); i++)
        {
            tabs.Tabs[i, 2] += query;
        }
    }
}
