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

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Modules_Pages_Development_Module_Edit_Frameset : SiteManagerPage
{
    protected string contentUrl = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        int tabIndex = QueryHelper.GetInteger("tabIndex", 0);
        switch (tabIndex)
        {
            case 2:
                contentUrl = "Module_UI_Frameset.aspx";
                break;

            default:
                contentUrl = "Module_Edit_General.aspx";
                break;
        }

        contentUrl += "?moduleId=" + Request.QueryString["moduleId"] + "&saved=" + Request.QueryString["saved"];
    }
}
