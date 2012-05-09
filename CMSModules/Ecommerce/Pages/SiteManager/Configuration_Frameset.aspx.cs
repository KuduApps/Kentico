using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_SiteManager_Configuration_Frameset : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        configEdit.Attributes["src"] = "../Tools/Configuration/Configuration_Frameset.aspx?siteId=0";
    }
}
