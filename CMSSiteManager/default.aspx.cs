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
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSSiteManager_default : SiteManagerPage
{
    protected string trialHeight = "0";
    protected string trialExpires;
    protected string trialPageURL;
    protected string techPreviewHeight = "0";
    protected string techPreviewPageURL;
    protected string headerPageURL;
    protected string desktoppage;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the expiration
        if (LicenseHelper.ApplicationExpires != DateTime.MinValue)
        {
            trialHeight = "17";
            trialExpires = "?appexpires=" + LicenseHelper.ApplicationExpires.Subtract(DateTime.Now).Days;
        }

        // Display the techPreview frame if there is a key in the web.config
        if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSUseTechnicalPreview"], false))
        {
            techPreviewHeight = "17";
            techPreviewPageURL = ResolveUrl("~/CMSSiteManager/techpreview.aspx");
        }

        trialPageURL = ResolveUrl("~/CMSSiteManager/trialversion.aspx");
        
        // Load particular section
        string section = ValidationHelper.GetString(Request.QueryString["section"], "sites").ToLower();
        string action = ValidationHelper.GetString(Request.QueryString["action"], "").ToLower();
        switch (section)
        {
            case "sites":
                switch (action)
                {
                    case "new":
                        desktoppage = ResolveUrl("~/CMSModules/ImportExport/SiteManager/Site_New.aspx");
                        break;

                    case "import":
                        desktoppage = ResolveUrl("~/CMSModules/ImportExport/SiteManager/ImportSite.aspx");
                        break;

                    case "export":
                        desktoppage = ResolveUrl("~/CMSModules/ImportExport/SiteManager/ExportSite.aspx");
                        break;
                    default:
                        desktoppage = ResolveUrl("~/CMSSiteManager/Sites/site_list.aspx");
                        break;
                }
                break;

            case "administration":
                desktoppage = ResolveUrl("~/CMSSiteManager/Administration/default.aspx");
                break;

            case "settings":
                desktoppage = ResolveUrl("~/CMSModules/Settings/SiteManager/Default.aspx");
                break;

            case "development":
                desktoppage = ResolveUrl("~/CMSSiteManager/Development/default.aspx");
                break;

            case "licenses":
                desktoppage = ResolveUrl("~/CMSModules/Licenses/Pages/License_List.aspx");
                break;
        }

        headerPageURL = ResolveUrl("~/CMSSiteManager/Header.aspx");
        desktoppage += URLHelper.Url.Query;
    }
}
