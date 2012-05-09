using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Modules_Pages_Development_Module_UI_EditFrameset : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int moduleid = QueryHelper.GetInteger("moduleid", 0);
        int elementid = QueryHelper.GetInteger("elementid", 0);
        int parentid = QueryHelper.GetInteger("parentid", 0);
        int saved = QueryHelper.GetInteger("saved", 0);
        int tabindex = QueryHelper.GetInteger("tabindex", 0);

        if (tabindex == 0)
        {
            this.editContent.Attributes["src"] = ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_General.aspx") + "?moduleID=" + moduleid + "&elementid=" + elementid + "&parentId=" + parentid + "&saved=" + saved + "&tabIndex=" + tabindex;
        }
        else
        {
            this.editContent.Attributes["src"] = ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_Roles.aspx") + "?moduleID=" + moduleid + "&elementid=" + elementid + "&parentId=" + parentid + "&saved=" + saved + "&tabIndex=" + tabindex;
        }
    }
}
