using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSModules_Modules_Pages_Development_Module_UI_Frameset : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.uiFrameset);
        }

        int moduleId = QueryHelper.GetInteger("moduleid", 0);

        treeFrame.Attributes["src"] = "Module_UI_Tree.aspx?moduleId=" + moduleId;
        contentFrame.Attributes["src"] = "Module_UI_New.aspx?moduleId=" + moduleId + "&saved=" + QueryHelper.GetInteger("saved", 0);
    }
}
