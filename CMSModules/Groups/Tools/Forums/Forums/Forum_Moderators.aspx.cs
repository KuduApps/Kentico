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
using CMS.Forums;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Forums_Forum_Moderators : CMSGroupForumPage
{
    protected int forumId = 0;    
    protected ForumInfo forum = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        forumId = QueryHelper.GetInteger("forumid", 0);        
        if (forumId == 0)
        {
            return;
        }

        this.forumModerators.ForumID = forumId;  
        this.forumModerators.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(forumModerators_OnCheckPermissions);
        this.forumModerators.IsLiveSite = false;
    }


    void forumModerators_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        int groupId = 0;
        ForumInfo fi = ForumInfoProvider.GetForumInfo(ValidationHelper.GetInteger(Request.QueryString["forumid"], 0));
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
