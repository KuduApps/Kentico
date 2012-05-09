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

public partial class CMSModules_Groups_Tools_Forums_Forums_Forum_General : CMSGroupForumPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.forumEdit.ForumID = QueryHelper.GetInteger("forumid", 0);
        this.forumEdit.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(forumEdit_OnCheckPermissions);
        this.forumEdit.IsLiveSite = false;
    }


    void forumEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;
        ForumInfo fi = ForumInfoProvider.GetForumInfo(this.forumEdit.ForumID);
        if (fi != null)
        {
            ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(fi.ForumGroupID);
            if (fgi != null)
            {
                groupId = fgi.GroupGroupID;
            }
        }
        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }
}
