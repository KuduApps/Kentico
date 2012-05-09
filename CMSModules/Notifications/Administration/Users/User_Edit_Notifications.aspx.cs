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
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Notifications_Administration_Users_User_Edit_Notifications : CMSUsersPage
{
    protected int userId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the license
        string domain = URLHelper.GetCurrentDomain();
        if (domain != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(domain, FeatureEnum.Notifications);
        }

        // Check "read" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Read"))
        {
            RedirectToAccessDenied("CMS.Users", "Read");
        }

        userId = QueryHelper.GetInteger("userid", 0);
        
        // Check that only global administrator can edit global administrator's accouns
        if (userId > 0)
        {
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            EditedObject = ui;

            if (!CheckGlobalAdminEdit(ui))
            {
                userNotificationsElem.Visible = false;
                lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                lblError.Visible = true;
                return;
            }
        }

        this.userNotificationsElem.UserId = userId;
        this.userNotificationsElem.SiteID = SiteID;
        this.userNotificationsElem.ZeroRowsText = GetString("notifications.administration.userhasnonotifications");
        this.userNotificationsElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(userNotificationsElem_OnCheckPermissions);
    }


    protected void userNotificationsElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        CurrentUserInfo cui = CMSContext.CurrentUser;
        if ((cui == null) || ((userId != cui.UserID) && !cui.IsAuthorizedPerResource("CMS.Users", permissionType)))
        {
            RedirectToCMSDeskAccessDenied("CMS.Users", permissionType);
        }
    }
}
