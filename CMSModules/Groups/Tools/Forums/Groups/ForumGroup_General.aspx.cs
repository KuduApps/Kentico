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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Groups_ForumGroup_General : CMSGroupForumPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.groupEdit.GroupID = QueryHelper.GetInteger("groupid", 0);
        this.groupEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(groupEdit_OnCheckPermissions);
        this.groupEdit.IsLiveSite = false;
    }


    void groupEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;

        ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(groupEdit.GroupID);
        if (fgi != null)
        {
            groupId = fgi.GroupGroupID;
        }

        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }
}
