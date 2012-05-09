using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSAPIExamples_Default : CMSAPIExamplePage
{
    protected string headerpage;
    protected string menupage;
    protected string blankpage;

    protected void Page_Load(object sender, EventArgs e)
    {
        headerpage = ResolveUrl("~/CMSAPIExamples/Pages/Header.aspx");
        frmMenu.Attributes["src"] = "Pages/Menu.aspx" + URLHelper.Url.Query;
        menupage = ResolveUrl("~/CMSAPIExamples/Pages/Menu.aspx");
        blankpage = ResolveUrl("~/CMSPages/blank.htm");

        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }
    }
}
