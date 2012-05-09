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

using CMS.LicenseProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSMessages_FeatureNotAvailable : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string domain = QueryHelper.GetText("domainname", String.Empty);

        if (domain != String.Empty)
        {
            LicenseKeyInfo lki = LicenseKeyInfoProvider.GetLicenseKeyInfo(domain);
            if (lki != null)
            {
                LabelMessage.Text = GetString("CMSSiteManager.FeatureNotAvailable").Replace("%%name%%", LicenseHelper.GetEditionName(lki.Edition));
            }
            else
            {
                LabelMessage.Text = GetString("CMSSiteManager.LicenseNotFound").Replace("%%name%%", domain);
            }
        }
        this.titleElem.TitleText = GetString("CMSSiteManager.AccesDenied");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/denied.png");
    }
}
