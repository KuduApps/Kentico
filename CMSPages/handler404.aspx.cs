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

public partial class CMSPages_handler404 : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set preferred content culture
        SetLiveCulture();
        this.titleElem.TitleText = GetString("404.Header");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/error.png");

        this.lblInfo.Text = String.Format(GetString("404.Info"), QueryHelper.GetText("aspxerrorpath", String.Empty));

        this.lnkBack.Text = GetString("404.Back");
        this.lnkBack.NavigateUrl = "~/";
    }


    /// <summary>
    /// Disable handler base tag.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        UseBaseTagForHandlerPage = false;
        base.OnInit(e);
    }
}
