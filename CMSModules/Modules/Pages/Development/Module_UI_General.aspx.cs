using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Modules_Pages_Development_Module_UI_General : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int elementId = QueryHelper.GetInteger("elementid", 0);
        int parentId = QueryHelper.GetInteger("parentId", 0);
        int moduleId = QueryHelper.GetInteger("moduleid", 0);

        if (parentId > 0)
        {
            this.editElem.ElementID = elementId;
            this.editElem.ParentID = parentId;
            this.editElem.ResourceID = moduleId;
        }
    }
}
