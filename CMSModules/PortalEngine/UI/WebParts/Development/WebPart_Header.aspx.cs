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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Development-WebPart_Header.WebPartTitle");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebPart/object.png");
        this.CurrentMaster.Title.HelpTopicName = "web_parts_main";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }
}
