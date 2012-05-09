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
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_UICultures_Pages_Development_UICultures_Default : SiteManagerPage
{
    protected string contentUrl = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        contentUrl = ResolveUrl("ResourceString/List.aspx?uicultureid=");

        // Default UI culture strings
        string defaultCulture = CultureHelper.DefaultUICulture;
        try
        {
            UICultureInfo ci = UICultureInfoProvider.GetUICultureInfo(defaultCulture);
            if (ci != null)
            {
                contentUrl += ci.UICultureID;
            }
        }
        catch { }
    }
}
