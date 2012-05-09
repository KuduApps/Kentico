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
using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_StoreSettings_StoreSettings_Frameset : CMSEcommerceStoreSettingsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        storesettingsHeader.Attributes["src"] = "StoreSettings_Header.aspx?siteId=" + this.SiteID.ToString();
        rowsFrameset.Attributes["rows"] = TabsFrameHeight.ToString() + ", *";
    }
}
