using System;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.Messaging;

public partial class CMSModules_Messaging_MyMessages_MyMessages_ContactList : CMSMyMessagesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void CheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Do not check permissions since user can always manage her messages
    }
}
