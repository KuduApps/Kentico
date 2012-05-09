using System;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Friends_CMSPages_MessageUserSelector_FriendsList : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Add styles
        RegisterDialogCSSLink();

        // Check license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty) != string.Empty)
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Friends);
        }
    }
}
