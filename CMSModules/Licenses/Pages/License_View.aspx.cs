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
using CMS.LicenseProvider;
using CMS.UIControls;

public partial class CMSModules_Licenses_Pages_License_View : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("Licenses_License_View.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_LicenseKey/object.png");

        lblLicenseKey.Text = GetString("Licenses_License_View.LicenseKey");
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Licenses_License_View.LicenseList");
        pageTitleTabs[0, 1] = "~/CMSModules/Licenses/Pages/License_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = "";
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        if (ValidationHelper.GetInteger(Request.QueryString["licenseid"], 0) != 0)
        {
            LicenseKeyInfo lki = LicenseKeyInfoProvider.GetLicenseKeyInfo(ValidationHelper.GetInteger(Request.QueryString["licenseid"], 0));
            if (lki != null)
            {
                pageTitleTabs[1, 0] = lki.Domain;
                lblLicenseKeyContent.Text = lki.Key;
            }
        }

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }
}
