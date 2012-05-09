using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;

public partial class CMSSiteManager_Development_Cultures_Culture_Edit_Sites : SiteManagerPage
{
    #region "Variables"

    private CultureInfo culture;


    protected string currentValues = string.Empty;    

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int cultureId = QueryHelper.GetInteger("cultureid", 0);
        EditedObject = culture = GetSafeCulture(cultureId);        

        // Get the active sites
        currentValues = GetCultureSites();

        if (!RequestHelper.IsPostBack())
        {
            usSites.Value = currentValues;
        }        

        usSites.OnSelectionChanged += usSites_OnSelectionChanged;
    }


    /// <summary>
    /// Returns string with culture sites.
    /// </summary>    
    private string GetCultureSites()
    {
        DataSet cultures = CultureSiteInfoProvider.GetCultureSites("SiteID", "CultureID = " + culture.CultureID, null, 0);
        if (!DataHelper.DataSourceIsEmpty(cultures))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(cultures.Tables[0], "SiteID"));
        }

        return string.Empty;
    }


    protected void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveSites();
    }


    protected void SaveSites()
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(usSites.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        bool somethingChanged = false;
        bool hasErrors = false;

        if (!string.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                    CultureInfo ci = CultureInfoProvider.GetCultureInfo(culture.CultureID);                    
                    if ((si != null) && (ci != null))
                    {
                        // Check if site does not contain document from this culture
                        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                        DataSet nodes = tree.SelectNodes(si.SiteName, "/%", ci.CultureCode, false, null, null, null, -1, false, 1, "NodeID");
                        if (DataHelper.DataSourceIsEmpty(nodes))
                        {
                            CultureInfoProvider.RemoveCultureFromSite(culture.CultureID, siteId);
                            somethingChanged = true;
                        }
                        else
                        {
                            hasErrors = true;
                            ShowError(string.Format(GetString("Culture_Edit_Sites.ErrorRemoveSiteFromCulture"), si.DisplayName));
                            continue;
                        }
                    }
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!string.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    // Add cullture to site
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                    if (si != null)
                    {
                        if (CultureInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Multilingual, VersionActionEnum.Insert))
                        {
                            CultureInfoProvider.AddCultureToSite(culture.CultureID, siteId);
                            somethingChanged = true;
                        }
                        else
                        {
                            hasErrors = true;
                            ShowError(GetString("licenselimitation.siteculturesexceeded"));
                            break;
                        }
                    }
                }
            }
        }

        // If there were some errors, reload uniselector
        if (hasErrors)
        {
            usSites.Value = GetCultureSites();
            usSites.Reload(true);
        }

        if (somethingChanged)
        {
            ShowInformation(GetString("General.ChangesSaved"));
        }
    }


    private static CultureInfo GetSafeCulture(int cultureId)
    {
        if (cultureId <= 0)
        {
            return null;
        }

        try
        {
            return CultureInfoProvider.GetCultureInfo(cultureId);
        }
        catch (Exception)
        {
            return null;
        }
    }

    #endregion
}