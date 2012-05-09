using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_MyDesk_RecycleBin_RecycleBin_Documents : CMSMyDeskPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UIProfile
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", new string[] { "MyDeskMyData", "MyRecycleBin", "MyRecycleBin.Documents" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyDeskMyData;MyRecycleBin;MyRecycleBin.Documents");
        }

        recycleBin.SiteName = CMSContext.CurrentSiteName;
    }

    #endregion
}

