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

public partial class CMSMessages_PageNotAvailable : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string reason = QueryHelper.GetString("reason", "");
        bool showLink = QueryHelper.GetBoolean("showlink", true);

        switch (reason.ToLower())
        {
            case "missingculture":
                this.titleElem.TitleText = GetString("MissingCulture.Header");
                this.lblInfo.Text = GetString("MissingCulture.Info");
                break;

            case "splitviewmissingculture":
                this.titleElem.TitleText = GetString("MissingCulture.Header");
                this.lblInfo.Text = GetString("SplitviewMissingCulture.Info");
                break;

            case "notpublished":
                this.titleElem.TitleText = GetString("NotPublished.Header");
                this.lblInfo.Text = GetString("NotPublished.Info");
                break;

            default:
                this.titleElem.TitleText = GetString("NotAvailable.Header");
                this.lblInfo.Text = GetString("NotAvailable.Info");
                break;
        }

        this.titleElem.TitleImage = GetImageUrl("Others/Messages/info.png");

        if (showLink)
        {
            this.lnkBack.Text = GetString("404.Back");
            this.lnkBack.NavigateUrl = "~/";
        }
    }
}
