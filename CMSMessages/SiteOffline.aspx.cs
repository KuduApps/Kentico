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
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSMessages_SiteOffline : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set title
        this.titleElem.TitleText = GetString("Error.SiteOffline");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/warning.png");

        SiteInfo currentSite = CMSContext.CurrentSite;
        if (currentSite != null)
        {
            if (currentSite.SiteIsOffline)
            {
                // Site is offline
                if (!String.IsNullOrEmpty(currentSite.SiteOfflineMessage))
                {
                    lblInfo.Text = CMSContext.ResolveMacros(currentSite.SiteOfflineMessage);
                }
                else
                {
                    lblInfo.Text = ResHelper.GetString("error.siteisoffline");
                }
            }
            else
            {
                // Redirect to the root
                Response.Redirect("~/");
            }
        }
    }
}
