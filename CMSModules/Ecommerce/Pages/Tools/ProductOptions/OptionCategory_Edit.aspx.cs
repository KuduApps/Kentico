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
using CMS.UIControls;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_Edit : CMSProductOptionCategoriesPage
{
	protected void Page_Load(object sender, EventArgs e)
    {
        // When hidebreadcrumbs are hidden
        if (QueryHelper.GetBoolean("hidebreadcrumbs", false))
        {
            rowsFrameset.Attributes["rows"] = TabsFrameHeight.ToString() + ",*";
        }
        RegisterModalPageScripts();

        rowsFrameset.Attributes["rows"] = TabsBreadHeadFrameHeight.ToString() + ",*";
	}
}
