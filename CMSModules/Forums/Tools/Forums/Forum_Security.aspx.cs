using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.Forums;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Tools_Forums_Forum_Security : CMSForumsPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        int forumID = QueryHelper.GetInteger("forumid", 0);
        ForumContext.CheckSite(0, forumID, 0);

        this.forumSecurity.ForumID = forumID;
        this.forumSecurity.IsGroupForum = false;
        forumSecurity.IsLiveSite = false;

        this.forumSecurity.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(forumSecurity_OnCheckPermissions);

        this.InitializeMasterPage();
    }


    /// <summary>
    /// Initializes master page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        this.Title = "Forums - Forum security";        
    }


    /// <summary>
    /// OnCheckPermissions handler.
    /// </summary>
    /// <param name="permissionType">Type of the permission</param>
    /// <param name="sender">The sender</param>
    private void forumSecurity_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.forums", permissionType))
        {
            this.forumSecurity.StopProcessing = true;

            // Redirect only if permission READ is check
            if (permissionType == CMSAdminControl.PERMISSION_READ)
            {
                RedirectToCMSDeskAccessDenied("CMS.Forums", "Read");
            }
        }
    }
}
