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

public partial class CMSMessages_PageNotFound : MessagePage
{
    private bool useRedirect = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.StatusCode = 404;

        // Use javascript redirect if custom 404 page is used at pagenotfoundpath         
        if (useRedirect)
        {
            string notFoundUrl = ValidationHelper.GetString(SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSPageNotFoundUrl"), "");

            notFoundUrl = URLHelper.ResolveUrl(notFoundUrl);

            string paramValue = QueryHelper.GetString("aspxerrorpath", String.Empty);

            // Escape special characters
            paramValue = ScriptHelper.GetString(paramValue, false);

            // Add parameter about what page was originaly not found
            notFoundUrl = URLHelper.AddParameterToUrl(notFoundUrl, "aspxerrorpath", paramValue);

            this.Header.Controls.Add(new LiteralControl(ScriptHelper.GetScript("document.location.replace('" + notFoundUrl + "');")));            
        }

        // Set preferred content culture
        SetLiveCulture();

        this.titleElem.TitleText = GetString("404.Header");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/error.png");

        this.lblInfo.Text = String.Format(GetString("404.Info"), QueryHelper.GetText("aspxerrorpath", String.Empty));

        this.lnkBack.Text = GetString("404.Back");
        this.lnkBack.NavigateUrl = "~/";

    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        useRedirect = QueryHelper.GetBoolean("pagenotfoundpath", false);
        if (useRedirect)
        {
            // Clear theme to get rid of CSS 
            this.Theme = null;
        }
    }
}
