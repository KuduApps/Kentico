using System;
using System.Text;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Roles_Role_Edit_General : CMSRolesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize the editing control
        roleEditElem.ItemID = QueryHelper.GetInteger("roleid", 0);
        int siteId = SiteID;
        if (siteId == 0)
        {
            siteId = SelectedSiteID;
        }
       
        if ((siteId == 0) && CMSContext.CurrentUser.UserSiteManagerAdmin)
        {        
            roleEditElem.GlobalRole = true;
        }
        roleEditElem.SiteID = siteId;
        roleEditElem.OnSaved += roleEditElem_OnSaved;
        roleEditElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(roleEditElem_OnCheckPermissions);
    }

    
    protected void roleEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Roles", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.Roles", permissionType);
        }
    }


    /// <summary>
    /// OnOK click event only if new role was created.
    /// </summary>
    protected void roleEditElem_OnSaved(object sender, EventArgs e)
    {
        StringBuilder parameters = new StringBuilder();
        // CMS desk site context
        if (SiteID != 0)
        {
            parameters.Append("&siteid=", SiteID);
        }

        //CMS site manager site context        
        if (SelectedSiteID != 0)
        {
            parameters.Append("&selectedsiteid=",SelectedSiteID);
        }

        URLHelper.Redirect("Role_Edit_Frameset.aspx?roleId=" + roleEditElem.ItemID + parameters.ToString());
    }
}
