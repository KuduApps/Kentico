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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Roles_Role_Edit : CMSGroupRolesPage
{
    protected string parameters = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        int roleId = QueryHelper.GetInteger("roleid", 0);
        int groupId = QueryHelper.GetInteger("groupid", 0);

        parameters = "?roleid=" + roleId + "&groupid=" + groupId;
    }
}
