using System;

using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.GlobalHelper;

public partial class CMSModules_Messaging_Dialogs_MessageUserSelector_Search : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Messaging);
    }
}
