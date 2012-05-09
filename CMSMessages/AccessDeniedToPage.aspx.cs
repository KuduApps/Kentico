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

using CMS.URLRewritingEngine;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSMessages_AccessDeniedToPage : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.titleElem.TitleText = GetString("AccessDeniedToPage.Header");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/denied.png");

        string url = QueryHelper.GetText("url", String.Empty);
        if (url == String.Empty)
        {
            this.lblInfo.Text = GetString("AccessDeniedToPage.InfoNoPage");
        }
        else
        {
            this.lblInfo.Text = String.Format(GetString("AccessDeniedToPage.Info"), url);
        }

        this.lnkBack.Text = GetString("AccessDeniedToPage.Back");
        this.lnkBack.NavigateUrl = "~/";
    }
}
