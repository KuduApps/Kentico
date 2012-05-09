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

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Menu : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitlePageTemplate.TitleText = GetString("Administration-PageTemplates_Header.Title");
        PageTitlePageTemplate.TitleImage = GetImageUrl("Objects/CMS_PageTemplate/object.png");
    }
}
