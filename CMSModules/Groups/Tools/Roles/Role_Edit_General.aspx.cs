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

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.Community;
using CMS.UIControls;

public partial class CMSModules_Groups_Tools_Roles_Role_Edit_General : CMSGroupRolesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize the editing control
        this.roleEditElem.ItemID = QueryHelper.GetInteger("roleid", 0);

        // Edit/Create only roles from current site
        if (CMSContext.CurrentSite != null)
        {
            this.roleEditElem.SiteID = CMSContext.CurrentSite.SiteID;
        }

        this.roleEditElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(roleEditElem_OnCheckPermissions);

        roleEditElem.GroupID = QueryHelper.GetInteger("groupid", 0);
    }


    void roleEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        CheckPermissions(roleEditElem.GroupID, CMSAdminControl.PERMISSION_MANAGE);
    }
}
