using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_MyDesk_RecycleBin_RecycleBin_Objects : CMSMyDeskPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        // Check the license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.ObjectVersioning);
        }
    }
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", new string[] { "MyDeskMyData", "MyRecycleBin", "MyRecycleBin.Objects" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyDeskMyData;MyRecycleBin;MyRecycleBin.Objects");
        }
        recycleBin.SiteName = CMSContext.CurrentSiteName;
    }
}

