using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_EmailTemplates_Pages_Frameset : CMSAdministrationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "Modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EmailTemplates", "Modify"))
        {
            RedirectToAccessDenied("CMS.EmailTemplates", "Modify");
        }
    }
}
