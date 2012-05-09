using System;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.Messaging;

public partial class CMSModules_Messaging_MyMessages_MyMessages_Inbox : CMSMyMessagesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Ensure custom page size
        if (!RequestHelper.IsPostBack())
        {
            inboxElem.Grid.PageSize = "15,25,50,100,##ALL##";
            inboxElem.Grid.Pager.DefaultPageSize = 15;
        }
    }


    protected void CheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Do not check permissions since user can always manage her messages
    }
}
