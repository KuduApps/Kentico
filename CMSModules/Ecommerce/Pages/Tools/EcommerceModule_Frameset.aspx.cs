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

using CMS.LicenseProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_EcommerceModule_Frameset : CMSEcommercePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        frameMenu.Attributes.Add("src", "Header.aspx" + URLHelper.Url.Query);
    }
}
