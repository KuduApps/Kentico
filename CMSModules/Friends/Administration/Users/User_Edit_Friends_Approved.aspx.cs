using System;
using System.Collections;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;


public partial class CMSModules_Friends_Administration_Users_User_Edit_Friends_Approved : CMSUsersPage
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

        // Check that only global administrator can edit global administrator's accounts
        if (userId > 0)
        {
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
                string imagePath = GetImageUrl("Objects/CMS_Friend/");

                ScriptHelper.RegisterDialogScript(this);
                FriendsList.UserID = userId;
                FriendsList.OnCheckPermissions += CheckPermissions;
                FriendsList.ZeroRowsText = GetString("friends.nouserfriends");

                // Request friend link
                string script =
                    "function displayRequest(){ \n" +
                        "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Friends/Dialogs/Friends_Request.aspx") + "?userid=" + userId + "&siteid=" + SiteID + "', 'rejectDialog', 480, 350);}";

                ScriptHelper.RegisterStartupScript(this, GetType(), "displayModalRequest", ScriptHelper.GetScript(script));
                string[,] actions = new string[1, 6];
                actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
                actions[0, 1] = GetString("Friends_List.NewItemCaption");
                actions[0, 2] = null;
                actions[0, 3] = "javascript:displayRequest();";
                actions[0, 4] = null;
                actions[0, 5] = imagePath + "add.png";
                CurrentMaster.HeaderActions.Actions = actions;
            }
        }
    }


    protected void CheckPermissions(string permissionType, CMSAdminControl sender)
    {
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
