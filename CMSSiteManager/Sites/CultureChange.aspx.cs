using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.LicenseProvider;
using CMS.EventLog;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSSiteManager_Sites_CultureChange : CMSModalSiteManagerPage
{
    private int siteId = 0;
    private string currentCulture = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get data from parameters
        siteId = QueryHelper.GetInteger("siteid", 0);
        currentCulture = QueryHelper.GetString("culture", "");

        // Strings and images
        this.btnOk.Text = GetString("General.Ok");
        this.btnCancel.Text = GetString("General.Cancel");

        this.lblNewCulture.Text = GetString("SiteDefaultCultureChange.NewCulture");
        this.chkDocuments.Text = GetString("SiteDefaultCultureChange.Documents");

        CurrentMaster.Title.TitleText = GetString("SiteDefaultCultureChange.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Sites/changeculture.png");       

        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
        if (si != null)
        {
            // Check licensing policy
            if (LicenseHelper.CheckFeature(URLHelper.GetDomainName(si.DomainName), FeatureEnum.Multilingual))
            {
                // Get only site cultures
                this.cultureSelector.SiteID = siteId;
            }
            else
            {
                // Get all cultures for non multilingual
                cultureSelector.DisplayAllCultures = true;
                
                // Have to change culture of documents
                chkDocuments.Enabled = false;
            }

            // Check version limitations
            if (!CultureInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Multilingual, VersionActionEnum.Edit))
            {
                lblError.Text = GetString("licenselimitation.siteculturesexceeded");
                lblError.Visible = true;
                pnlForm.Enabled = false;
            }


            currentCulture = CultureHelper.GetDefaultCulture(si.SiteName);
            if (!URLHelper.IsPostback())
            {
                this.cultureSelector.Value = currentCulture;
            }
        }
    }


    /// <summary>
    /// OkClick Handler.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string culture = ValidationHelper.GetString(cultureSelector.Value, "");

        if ((culture != "") && ((currentCulture.ToLower() != culture.ToLower()) || chkDocuments.Checked))
        {
            // Set new culture
            SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
            if (si != null)
            {
                try
                {
                    // Set default culture and change current culture label
                    ObjectHelper.SetSettingsKeyValue(si.SiteName + ".CMSDefaultCultureCode", culture.Trim());

                    // Change culture of documents
                    if (chkDocuments.Checked)
                    {
                        // Change culture of the documents
                        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                        tree.ChangeCulture(si.SiteName, currentCulture, culture);
                    }

                    if (!LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.Multilingual))
                    {
                        // If not multilingual, remove all cultures from the site and assign new culture
                        CultureInfoProvider.RemoveSiteCultures(si.SiteName);
                        CultureInfoProvider.AddCultureToSite(culture, si.SiteName);
                    }

                    ltlScript.Text = ScriptHelper.GetScript("wopener.ChangeCulture('" + chkDocuments.Checked.ToString() + "'); window.close();");
                }
                catch (Exception ex)
                {
                    EventLogProvider ev = new EventLogProvider();
                    ev.LogEvent("SiteManager", "ChangeDefaultCulture", ex);
                }
            }
        }
        else
        {
            ltlScript.Text = ScriptHelper.GetScript("window.close();");
        }
    }
}
