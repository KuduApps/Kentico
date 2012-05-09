using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Friends_Administration_Users_User_Edit_Friends_Rejected : CMSUsersPage
{
    #region "Variables"

    protected int userId = 0;
    protected CurrentUserInfo currentUser = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check hash
        if (!QueryHelper.ValidateHash("hash"))
        {
            RedirectToAccessDenied(ResHelper.GetString("dialogs.badhashtitle"));
        }
        
        userId = QueryHelper.GetInteger("userId", 0);
        currentUser = CMSContext.CurrentUser;

        // Check 'read' permissions
        if (!currentUser.IsAuthorizedPerResource("CMS.Friends", "Read") && (currentUser.UserID != userId))
        {
            RedirectToAccessDenied("CMS.Friends", "Read");
        }
        
        // Check license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty) != string.Empty)
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Friends);
        }

        if (userId > 0)
        {
            // Check that only global administrator can edit global administrator's accounts
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            EditedObject = ui;

            if (!CheckGlobalAdminEdit(ui))
            {
                plcTable.Visible = false;
                lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                lblError.Visible = true;
            }
            else
            {
                ScriptHelper.RegisterDialogScript(this);
                FriendsListRejected.UserID = userId;
                FriendsListRejected.UseEncapsulation = false;
                FriendsListRejected.OnCheckPermissions += CheckPermissions;
                FriendsListRejected.ZeroRowsText = GetString("friends.nouserrejectedfriends");
            }
        }
    }


    void CheckPermissions(string permissionType, CMSAdminControl sender)
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if ((!currentUser.IsAuthorizedPerResource("CMS.Friends", permissionType)) && (currentUser.UserID != userId))
        {
            RedirectToAccessDenied("CMS.Friends", permissionType);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }
}
