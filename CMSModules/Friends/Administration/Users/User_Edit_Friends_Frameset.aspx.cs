using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Friends_Administration_Users_User_Edit_Friends_Frameset : CMSUsersPage
{
    #region "Variables"

    protected int userId = 0;
    protected CurrentUserInfo currentUser = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
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
    }
}
