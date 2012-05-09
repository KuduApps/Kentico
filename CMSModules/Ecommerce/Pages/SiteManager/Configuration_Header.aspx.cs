using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_SiteManager_Configuration_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Administration-LeftMenu.Ecommerce");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Ecommerce/module.png");
    }
}
