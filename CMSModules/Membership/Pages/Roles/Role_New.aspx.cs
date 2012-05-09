using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Roles_Role_New : CMSRolesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "Modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Roles", "Modify"))
        {
            RedirectToAccessDenied("CMS.Roles", "Modify");
        }

        string roleListUrl = "~/CMSModules/Membership/Pages/Roles/Role_List.aspx";
        if (SiteID != 0)
        {
            this.roleEditElem.SiteID = SiteID;
            roleListUrl += "?siteid=" + SiteID;
        }
        else if (SelectedSiteID != 0)
        {
            this.roleEditElem.SiteID = SelectedSiteID;
            roleListUrl += "?selectedsiteid=" + this.roleEditElem.SiteID;
        }
        if ((SiteID == 0) && (SelectedSiteID == 0) && CMSContext.CurrentUser.UserSiteManagerAdmin)
        {
            this.roleEditElem.GlobalRole = true;
            this.roleEditElem.SiteID = 0;
        }

        roleEditElem.OnSaved += new EventHandler(roleEditElem_OnSaved);

        // Pagetitle
        this.CurrentMaster.Title.HelpTopicName = "new_role";
        this.CurrentMaster.Title.TitleText = GetString("Administration-Role_New.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Role/new.png");

        // Initializes page title breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("general.roles");
        pageTitleTabs[0, 1] = roleListUrl;
        pageTitleTabs[1, 0] = GetString("Administration-Role_New.NewRole");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// OnOK click event handler.
    /// </summary>
    void roleEditElem_OnSaved(object sender, EventArgs e)
    {
        StringBuilder parameters = new StringBuilder();
        //CMS desk site context        
        if (SiteID != 0)
        {
            parameters.Append("&siteid=", SiteID);
        }

        // CMS site manager site context        
        if (SelectedSiteID != 0)
        {
            parameters.Append("&selectedsiteid=", SelectedSiteID);
        }

        URLHelper.Redirect("Role_Edit_Frameset.aspx?roleId=" + this.roleEditElem.ItemID + parameters.ToString());
    }
}
