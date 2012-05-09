using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSAdminControls_ContextMenus_UserImpersonateMenu : CMSContextMenuControl//, IPostBackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        imgImpersonate.ImageUrl = UIHelper.GetImageUrl(Page, "Objects/CMS_User/headerImpersonate.png");
        pnlAdvancedExport.Attributes.Add("onclick", "userImpersonateShowDialog()");
    }
}
