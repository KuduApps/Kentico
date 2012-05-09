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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.PortalControls;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartDocumentationPage : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("WebPartDocumentDialog.Documentation");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_PortalEngine/Documentation.png");
        ucWebPartDocumentation.FooterClientID = divFooter.ClientID;
    }
}
