using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSSiteManager_Sites_Site_Edit_Cultures : SiteManagerPage
{
    #region "Variables"

    protected string siteName = null;
    private SiteInfo si = null;
    protected string currentValues = "";
    protected string defaultCulture = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the site info
        si = SiteInfoProvider.GetSiteInfo(QueryHelper.GetInteger("siteId", 0));
        if (si != null)
        {
            bool multilingual = LicenseHelper.CheckFeature(URLHelper.GetDomainName(si.DomainName), FeatureEnum.Multilingual);
            bool cultureOnSite = CultureInfoProvider.IsCultureOnSite(CultureHelper.GetDefaultCulture(si.SiteName), si.SiteName);
            if (!multilingual && !cultureOnSite)
            {
                lnkAssignDefault.Text = GetString("sitecultures.assigntodefault");
                lnkAssignDefault.Visible = true;
                plcAll.Visible = false;
            }
            else
            {
                // Redirect only if cultures not exceeded => to be able to unassign
                if (!CultureInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Multilingual, VersionActionEnum.Edit))
                {
                    LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetDomainName(si.DomainName), FeatureEnum.Multilingual);
                }
            }

            this.lblAvialable.Text = GetString("site_edit_cultures.culturetitle");


            siteName = si.SiteName;

            // Store default culture (it can't be removed)
            defaultCulture = CultureHelper.GetDefaultCulture(siteName);

            // Get the active cultures from DB
            DataSet ds = CultureInfoProvider.GetCultures("CultureID IN (SELECT CultureID FROM CMS_SiteCulture WHERE SiteID = " + si.SiteID + ")", null, 0, "CultureCode");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "CultureCode"));
            }

            if (!RequestHelper.IsPostBack())
            {
                uniSelector.Value = currentValues;
            }
        }

        this.uniSelector.ReturnColumnName = "CultureCode";
        this.uniSelector.OnSelectionChanged += uniSelector_OnSelectionChanged;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Check if site hasn't assigned more cultures than license approve
        if ((si != null) && !CultureInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Multilingual, VersionActionEnum.Insert))
        {
            uniSelector.ButtonAddItems.Enabled = false;
        }
        else if ((si != null) && !CultureInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Multilingual, VersionActionEnum.Edit))
        {
            lblError.Text += GetString("licenselimitation.siteculturesexceeded");
            lblError.Visible = true;
            uniSelector.ButtonAddItems.Enabled = false;
        }
    }


    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {

        bool relaodNeeded = false;

        // Remove old items
        string newValues = ValidationHelper.GetString(this.uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Initialize tree provider
                TreeProvider tree = new TreeProvider();

                // Add all new items to site
                foreach (string item in newItems)
                {
                    string cultureCode = ValidationHelper.GetString(item, "");

                    // Get the documents assigned to the culture being removed
                    DataSet nodes = tree.SelectNodes(siteName, "/%", cultureCode, false, null, null, null, -1, false);
                    if (DataHelper.DataSourceIsEmpty(nodes))
                    {
                        CultureInfoProvider.RemoveCultureFromSite(cultureCode, siteName);
                    }
                    else
                    {
                        relaodNeeded = true;

                        this.lblError.Visible = true;
                        this.lblError.Text += String.Format(GetString("site_edit_cultures.errorremoveculturefromsite"), cultureCode) + '\n';
                    }
                }
            }
        }

        // Catch license limitations Exception
        try
        {
            // Add new items
            items = DataHelper.GetNewItemsInList(currentValues, newValues);
            if (!String.IsNullOrEmpty(items))
            {
                string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (newItems != null)
                {
                    // Add all new items to site
                    foreach (string item in newItems)
                    {
                        string cultureCode = ValidationHelper.GetString(item, "");
                        CultureInfoProvider.AddCultureToSite(cultureCode, siteName);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            relaodNeeded = true;
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }

        this.lblInfo.Visible = true;
        this.lblInfo.Text = GetString("general.changessaved");

        if (relaodNeeded)
        {
            // Get the active cultures from DB
            DataSet ds = CultureInfoProvider.GetCultures("CultureID IN (SELECT CultureID FROM CMS_SiteCulture WHERE SiteID = " + si.SiteID + ")", null, 0, "CultureCode");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "CultureCode"));
                uniSelector.Value = currentValues;
                uniSelector.Reload(true);
            }
        }
    }


    /// <summary>
    /// Removes all cultures from sites and assignes back default site culture.
    /// </summary>
    protected void AssignDefaultCulture(object sender, EventArgs e)
    {
        string culture = CultureHelper.GetDefaultCulture(si.SiteName);
        CultureInfoProvider.RemoveSiteCultures(si.SiteName);
        CultureInfoProvider.AddCultureToSite(culture, si.SiteName);
        lnkAssignDefault.Visible = false;
        lblInfo.Text = GetString("general.ok");
        lblInfo.Visible = true;
    }
}