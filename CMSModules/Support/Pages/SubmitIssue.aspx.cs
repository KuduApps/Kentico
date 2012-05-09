using System;
using System.Data;
using System.Net.Mime;
using System.Text;
using System.Threading;

using CMS.CMSHelper;
using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Support_Pages_SubmitIssue : SiteManagerPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("SubmitIssue.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Support/SubmitIssueLarge.png");

        CurrentMaster.Title.HelpTopicName = "submit_issue";
        CurrentMaster.Title.HelpName = "helpTopic";

        rfvEmail.Text = GetString("Support.SubmiIssue.EmailEmpty");
        rfvSubject.Text = GetString("Support.SubmiIssue.SubjectEmpty");

        htmlTemplateBody.DefaultLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlTemplateBody.EditorAreaCSS = string.Empty;

        if (!RequestHelper.IsPostBack())
        {
            // Initialize system info
            txtSysInfo.Text = GetSystemInformation();

            ShowInformation(GetString("support.checksmtp"));
        }
    }


    /// <summary>
    /// Send e-mail to support.
    /// </summary>
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string result = new Validator()
            .NotEmpty(txtSubject.Text.Trim(), GetString("Support.SubmiIssue.SubjectEmpty"))
            .NotEmpty(txtEmail.Text.Trim(), GetString("Support.SubmiIssue.EmailEmpty"))
            .IsEmail(txtEmail.Text.Trim(), GetString("Support.SubmiIssue.EmailFormat"))
            .Result;

        if (!string.IsNullOrEmpty(result))
        {
            ShowError(result);
            return;
        }

        EmailMessage message = new EmailMessage
        {
            EmailFormat = EmailFormatEnum.Html,
            From = txtEmail.Text.Trim(),
            Subject = txtSubject.Text.Trim(),
            Recipients = "support@kentico.com"
        };

        StringBuilder sb = new StringBuilder();

        sb.Append("<html><head></head><body>");
        sb.Append(htmlTemplateBody.ResolvedValue);
        sb.Append("<br /><div>=============== System information ==================<br />");
        sb.Append(txtSysInfo.Text.Replace("\r", string.Empty).Replace("\n", "<br />"));
        sb.Append("<br />==============================================</div><br />");
        sb.Append("<div>================ Template type ==================<br />");
        sb.Append(GetEngineInfo());
        sb.Append("<br />");
        sb.Append("=============================================</div>");
        sb.Append("</body></html>");

        message.Body = sb.ToString();

        // Add settings attachment
        if (chkSettings.Checked)
        {
            // Get settings data
            string settings = GetSettingsInfo();
            if (!string.IsNullOrEmpty(settings))
            {
                Stream stream = MemoryStream.New();

                // Put the file content in the stream
                StreamWriter writer = StreamWriter.New(stream, System.Text.UnicodeEncoding.UTF8);
                writer.Write(settings);
                writer.Flush();
                stream.Seek(0, SeekOrigin.Begin);
                message.Attachments.Add(new System.Net.Mail.Attachment(stream.SystemStream, "settings.txt", MediaTypeNames.Application.Octet));
            }
        }
        // Add uploaded attachment
        if (fileUpload.HasFile)
        {
            System.Net.Mail.Attachment at = new System.Net.Mail.Attachment(fileUpload.PostedFile.InputStream, fileUpload.FileName, MediaTypeNames.Application.Octet);
            if (at != null)
            {
                message.Attachments.Add(at);
            }
        }

        EmailSender.SendEmail(null, message, true);
        ShowInformation(GetString("Support.Success"));

        ClearForm();
    }


    /// <summary>
    /// Clears the form.
    /// </summary>
    protected void ClearForm()
    {
        txtEmail.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtSysInfo.Text = GetSystemInformation();

        htmlTemplateBody.ResolvedValue = string.Empty;
        chkSettings.Checked = true;
        radDontKnow.Checked = true;
        radAspx.Checked = radMix.Checked = radPortal.Checked = false;
    }


    /// <summary>
    /// Gets data from the global and current site Settings.
    /// </summary>
    private static string GetSettingsInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("----------Settings---------");
        sb.AppendLine();
        sb.AppendLine();

        // Get global settings data
        sb.Append(GetSettingsString(0, null));
        
        int siteId = CMSContext.CurrentSiteID;
        if (siteId > 0)
        {
            // Get current site settings data
            sb.Append(GetSettingsString(siteId, CMSContext.CurrentSiteName));
        }

        return sb.ToString();
    }


    /// <summary>
    /// Returns string with Settings information for specified site.
    /// </summary>
    /// <param name="siteId">Site ID or 0 for global Settings</param>
    /// <param name="siteName">Site name or null for global Settings</param>
    private static string GetSettingsString(int siteId, string siteName)
    {
        DataSet setsDs;
        // Get global setting categories
        if (siteId == 0)
        {
            setsDs = SettingsCategoryInfoProvider.GetSettingsCategories(null, "CategoryName,CategoryOrder");
        }
        // Get site setting categories
        else
        {
            setsDs = SettingsCategoryInfoProvider.GetSettingsCategories("CategoryID IN (SELECT KeyCategoryID FROM CMS_SettingsKey WHERE SiteID = " + siteId + ")", "CategoryName,CategoryOrder");
        }

        // Check if any result exists
        if (DataHelper.DataSourceIsEmpty(setsDs))
        {
            return string.Empty;            
        }

        StringBuilder sb = new StringBuilder();
        DataSet keyDs = null;        

        string site = siteId > 0 ? siteName : "GLOBAL SETTINGS";
        sb.Append(" - ");
        sb.Append(site);
        sb.AppendLine();

        // Loop through all setting categories
        string prefix;
        foreach (DataRow setsDr in setsDs.Tables[0].Rows)
        {           
            // Get settings keys for specific category
            keyDs = SettingsKeyProvider.GetSettingsKeys(siteId, ValidationHelper.GetInteger(setsDr["CategoryId"], 0));
            if (!DataHelper.DataSourceIsEmpty(keyDs))
            {
                // Display only not empty categories
                sb.Append("\r\n\t - ");
                sb.Append(ResHelper.LocalizeString(ValidationHelper.GetString(setsDr["CategoryName"], string.Empty)));
                sb.AppendLine();

                prefix = "\t\t - ";

                // Display keys for category
                foreach (DataRow keyDr in keyDs.Tables[0].Rows)
                {
                    if (keyDr["KeyValue"] != DBNull.Value)
                    {
                        sb.AppendFormat("{0}{1} '{2}' ({3})",
                                        prefix,
                                        ValidationHelper.GetString(keyDr["KeyName"], string.Empty),
                                        ValidationHelper.GetString(keyDr["KeyValue"], string.Empty),
                                        ValidationHelper.GetString(keyDr["KeyType"], string.Empty));
                        sb.AppendLine();
                    }
                }
            }
        }

        sb.AppendLine();

        return sb.ToString();
    }


    /// <summary>
    /// Returns system information.
    /// </summary>
    private static string GetSystemInformation()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("CMS version: {0} Build: {1}", 
                        CMSContext.SYSTEM_VERSION,
                        typeof(TreeProvider).Assembly.GetName().Version.ToString(3));
        sb.AppendLine();

        sb.AppendFormat("OS version: {0}", Environment.OSVersion);
        sb.AppendLine();

        LicenseKeyInfo licenseKey = null;
        if (CMSContext.CurrentSite != null)
        {
            licenseKey = LicenseKeyInfoProvider.GetLicenseKeyInfo(CMSContext.CurrentSite.DomainName);
        }

        if (licenseKey != null)
        {
            sb.AppendFormat("License info: {0}, {1}, {2}, {3}", 
                            licenseKey.Domain, 
                            licenseKey.Edition, 
                            licenseKey.ExpirationDateReal.ToString(DateTimeHelper.DefaultIFormatProvider), 
                            licenseKey.Version);

            string packages = ValidationHelper.GetString(licenseKey.GetValue("LicensePackages"), string.Empty);
            if (!string.IsNullOrEmpty(packages))
            {
                sb.AppendFormat(", {0}", packages);
            }
        }

        return sb.ToString();
    }


    private string GetEngineInfo()
    {
        if (radAspx.Checked)
        {
            return "ASPX Templates";
        }

        if (radPortal.Checked)
        {
            return "Portal engine";
        }

        if (radMix.Checked)
        {
            return "Both";
        }

        if (radDontKnow.Checked)
        {
            return "I don't know";
        }

        return string.Empty;
    }

    #endregion
}