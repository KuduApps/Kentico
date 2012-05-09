using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

// Set page title
[Title("CMSModules/CMS_MyDesk/RecycleBin/module.png", "Administration-RecycleBin.Header", "recycle_bin")]

public partial class CMSModules_RecycleBin_Pages_RecycleBin_Header : SiteManagerPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        // Set page tabs
        InitTabs(2, "recbin_content");
        SetTab(0, GetString("general.documents"), "RecycleBin_Documents.aspx", "SetHelpTopic('helpTopic', 'recycle_bin');");
        if (LicenseKeyInfoProvider.IsFeatureAvailable(FeatureEnum.ObjectVersioning))
        {
            SetTab(1, GetString("staging.objects"), "RecycleBin_Objects.aspx", "SetHelpTopic('helpTopic', 'objectsrecycle_bin');");
        }
    }
}

