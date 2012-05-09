using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_Pages_Tools_Account_Tab_Merge : CMSContactManagementAccountsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.colsFrameset);
        }

        mergeMenu.Attributes["src"] = "Merge_Header.aspx" + URLHelper.Url.Query;
        mergeContent.Attributes["src"] = "Merge_Suggested.aspx" + URLHelper.Url.Query;
    }
}
