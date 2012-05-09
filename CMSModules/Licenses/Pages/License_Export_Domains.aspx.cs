using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSImportExport;
using CMS.IO;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSModules_Licenses_Pages_License_Export_Domains : SiteManagerPage
{
    #region "Page events"

    /// <summary>
    /// Page load event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("license.export");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_LicenseKey/export24.png");

        this.CurrentMaster.Title.HelpTopicName = "license_export";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Setup breadcrums
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Licenses_License_New.Licenses");
        pageTitleTabs[0, 1] = "~/CMSModules/Licenses/Pages/License_List.aspx";
        pageTitleTabs[1, 0] = GetString("license.export");
        pageTitleTabs[1, 1] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        // Resource strings
        rfvFileName.ErrorMessage = GetString("license.export.filenameempty");

        // Default file name
        if (!RequestHelper.IsPostBack())
        {
            lblInfo.ResourceString = "license.export.info";
            lblInfo.Visible = true;
            txtFileName.Text = "list_of_domains_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmm") + ".txt";
        }
    }


    /// <summary>
    /// Button Ok click.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Validate file name
        string fileName = txtFileName.Text.Trim();
        string errorMessage = new Validator().NotEmpty(fileName, GetString("lincense.export.filenameempty")).IsFileName(fileName, GetString("license.export.notvalidfilename")).Result;
        if (!string.IsNullOrEmpty(errorMessage))
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
        else
        {
            try
            {
                // Create writers
                string path = ImportExportHelper.GetSiteUtilsFolder() + "Export\\" + fileName;
                DirectoryHelper.EnsureDiskPath(path, SettingsKeyProvider.WebApplicationPhysicalPath);

                using (FileStream file = FileStream.New(path, FileMode.Create))
                {
                    using (StreamWriter sw = StreamWriter.New(file))
                    {
                        // Array list for duplicity checking
                        ArrayList allSites = new ArrayList();

                        // Get all sites
                        DataSet sites = SiteInfoProvider.GetSites(null, null, "SiteID,SiteDomainName");
                        if (!DataHelper.DataSourceIsEmpty(sites))
                        {
                            foreach (DataRow dr in sites.Tables[0].Rows)
                            {
                                // Get domain
                                string domain = ValidationHelper.GetString(dr["SiteDomainName"], "");
                                if (!string.IsNullOrEmpty(domain))
                                {
                                    domain = GetDomain(domain);
                                    // Add to file
                                    if (!allSites.Contains(domain))
                                    {
                                        sw.WriteLine(domain);
                                        allSites.Add(domain);
                                    }

                                    // Add all domain aliases
                                    DataSet aliases = SiteDomainAliasInfoProvider.GetDomainAliases("SiteID=" + ValidationHelper.GetString(dr["SiteID"], ""), null, "SiteDomainAliasName");
                                    if (!DataHelper.DataSourceIsEmpty(aliases))
                                    {
                                        foreach (DataRow drAlias in aliases.Tables[0].Rows)
                                        {
                                            // Get domain
                                            domain = ValidationHelper.GetString(drAlias["SiteDomainAliasName"], "");
                                            if (!string.IsNullOrEmpty(domain))
                                            {
                                                domain = GetDomain(domain);
                                                // Add to file
                                                if (!allSites.Contains(domain))
                                                {
                                                    sw.WriteLine(domain);
                                                    allSites.Add(domain);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Output
                string downloadPath = ImportExportHelper.GetSiteUtilsFolderRelativePath();
                string location = null;
                string link = null;

                // Path is relative path
                if (downloadPath != null)
                {
                    string externalUrl = null;
                    if (StorageHelper.IsExternalStorage)
                    {
                        externalUrl = File.GetFileUrl(downloadPath + "Export/" + fileName, CMSContext.CurrentSiteName);
                    }

                    if (string.IsNullOrEmpty(externalUrl))
                    {
                        location = URLHelper.ResolveUrl(downloadPath + "Export/" + fileName);
                    }
                    else
                    {
                        location = externalUrl;
                    }

                    link = "<a href=\"" + location + "\" target=\"_blank\">" + GetString("license.export.download") + "</a>";
                }
                else
                {
                    location = path;
                }

                lblInfo.Text = string.Format(GetString("license.export.exported"), location) + "<br /><br />" + link;
                lblInfo.Visible = true;

                plcTextBox.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }

    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns domain without www, protocol, port and appliaction path.
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    private string GetDomain(string domain)
    {
        // Trim to domain
        domain = URLHelper.RemovePortFromURL(domain);
        domain = URLHelper.RemoveProtocol(domain);
        domain = URLHelper.RemoveWWW(domain);

        // Virtual directory
        int slash = domain.IndexOf('/');
        if (slash > -1)
        {
            domain = domain.Substring(0, slash);
        }

        return domain;
    }

    #endregion
}
