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

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSModules_Support_Pages_Default : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.frameTree.Attributes.Add("src", "leftmenu.aspx?siteid=" + Request.QueryString["siteid"]);
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.colsFrameset);
        }
    }
}
