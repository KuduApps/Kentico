using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.URLRewritingEngine;
using CMS.UIControls;

public partial class CMSModules_MyDesk_MyProfile_MyProfile_MyDetails : CMSMyProfilePage
{

    protected void Page_Init(object sender, EventArgs e)
    {
        // Set non live site mode here
        ucMyDetails.IsLiveSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = GetString("MyAccount.MyDetails");

        // Check UIProfile
        if ((CMSContext.CurrentUser == null) || (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.MyDesk", "MyProfile.Details")))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.MyDesk", "MyProfile.Details");
        }
    }
}
