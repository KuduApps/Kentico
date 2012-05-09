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
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSPages_Dialogs_UserRegistration : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string defaultAliasPath = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultAliasPath");
        string defaultUrl = CMSContext.GetUrl(defaultAliasPath);

        if (!String.IsNullOrEmpty(defaultUrl))
        {
            defaultUrl = ResolveUrl(defaultUrl);
        }

        RegistrationApproval.SuccessfulApprovalText = GetString("membership.userconfirmed") + " " + "<a href=\"" + defaultUrl + "\" title=\"" + ResHelper.GetString("General.ClickHereToContinue") + "\" >" + ResHelper.GetString("General.ClickHereToContinue") + "</a>";
        RegistrationApproval.WaitingForApprovalText = GetString("mem.reg.SuccessfulApprovalWaitingForAdministratorApproval");

        // Set administrator e-mail
        RegistrationApproval.AdministratorEmail = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSAdminEmailAddress");
        RegistrationApproval.FromAddress = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress");
    }
}
