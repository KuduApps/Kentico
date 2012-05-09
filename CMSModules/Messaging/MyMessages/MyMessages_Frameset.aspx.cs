using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.Messaging;

public partial class CMSModules_Messaging_MyMessages_MyMessages_Frameset : CMSMyMessagesPage
{
    #region "Public variables"

    public int userId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        userId = CMSContext.CurrentUser.UserID;
    }

    #endregion
}
