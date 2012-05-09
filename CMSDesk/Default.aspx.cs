using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSDesk_Default : CMSDeskPage
{
    protected string trialHeight;
    protected string trialExpires;
    protected string trialPageURL;
    protected string headerPageURL;
    protected string techPreviewHeight = "0";
    protected string techPreviewPageURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (LicenseHelper.ApplicationExpires != DateTime.MinValue)
        {
            trialHeight = "17";
            trialExpires = "?appexpires=" + LicenseHelper.ApplicationExpires.Subtract(DateTime.Now).Days;
        }
        // Check the license key for trial version
        else if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), string.Empty) != string.Empty)
        {
            LicenseKeyInfo lki = LicenseKeyInfoProvider.GetLicenseKeyInfo(URLHelper.GetCurrentDomain());
            if ((lki != null) && (lki.Key.Length == LicenseKeyInfo.TRIAL_KEY_LENGTH) && (lki.ExpirationDateReal != LicenseKeyInfo.TIME_UNLIMITED_LICENSE))
            {
                trialHeight = "17";
                trialExpires = "?expirationdate=" + lki.ExpirationDateReal.Subtract(DateTime.Now).Days;
            }
            else
            {
                trialHeight = "0";
                trialExpires = "";
            }

            if (lki != null)
            {
                // Check the number of users for free edition
                if (lki.Edition == ProductEditionEnum.Free)
                {
                    UserInfoProvider.LicenseController();
                }
            }
        }

        // Display the techPreview frame if there is a key in the web.config
        if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSUseTechnicalPreview"], false))
        {
            techPreviewHeight = "17";
            techPreviewPageURL = ResolveUrl("~/CMSSiteManager/techpreview.aspx");
        }

        trialPageURL = ResolveUrl("~/CMSSiteManager/trialversion.aspx");
        headerPageURL = ResolveUrl("~/CMSDesk/Header.aspx");
    }
}
