using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_ImportExport_Controls_ImportSiteDetails : CMSUserControl
{
    private SiteImportSettings mSettings = null;


    #region "Properties"

    /// <summary>
    /// Import settings.
    /// </summary>
    public SiteImportSettings Settings
    {
        get
        {
            return mSettings;
        }
        set
        {
            mSettings = value;
        }
    }


    /// <summary>
    /// Default site display name.
    /// </summary>
    public string SiteDisplayName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SiteDisplayName"], null);
        }
        set
        {
            ViewState["SiteDisplayName"] = value;
        }
    }


    /// <summary>
    /// Default site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SiteName"], null);
        }
        set
        {
            ViewState["SiteName"] = value;
        }
    }


    /// <summary>
    /// Default domain name.
    /// </summary>
    public string DomainName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["DomainName"], null);
        }
        set
        {
            ViewState["DomainName"] = value;
        }
    }


    /// <summary>
    /// Site default culture.
    /// </summary>
    public string Culture
    {
        get
        {
            return DataHelper.GetNotEmpty(cultureElem.Value, SettingsKeyProvider.GetStringValue("CMSDefaultCultureCode"));
        }
        set
        {
            cultureElem.Value = value;
        }
    }


    /// <summary>
    /// Display culture selector.
    /// </summary>
    public bool DisplayCulture
    {
        get
        {
            return plcCulture.Visible;
        }
        set
        {
            plcCulture.Visible = value;
        }
    }


    /// <summary>
    /// If true, existing site selestion is allowed.
    /// </summary>
    public bool AllowExisting
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["AllowExisting"], true);
        }
        set
        {
            ViewState["AllowExisting"] = value;
        }
    }


    /// <summary>
    /// If true, single object is imported.
    /// </summary>
    public bool SingleObject
    {
        get
        {
            if (Settings == null)
            {
                return false;
            }
            return ValidationHelper.GetBoolean(Settings.GetInfo(ImportExportHelper.INFO_SINGLE_OBJECT), false);
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            if (!Page.IsCallback)
            {
                radNewSite.CheckedChanged += rad_CheckedChanged;
                radExisting.CheckedChanged += rad_CheckedChanged;

                rfvSiteDisplayName.ErrorMessage = GetString("ImportSite.StepSiteDetails.SiteDisplayNameError");
                rfvSiteName.ErrorMessage = GetString("ImportSite.StepSiteDetails.SiteNameError");
                rfvDomain.ErrorMessage = GetString("ImportSite.StepSiteDetails.ErrorDomain");

                lblSiteDisplayName.Text = GetString("ImportSite.StepSiteDetails.SiteDisplayName");
                lblSiteName.Text = GetString("ImportSite.StepSiteDetails.SiteName");
                lblDomain.Text = GetString("ImportSite.StepSiteDetails.DomainName");
                lblCulture.Text = GetString("ImportSite.StepSiteDetails.CultureName");
                lblSite.Text = GetString("ImportSite.StepSiteDetails.Site");

                radExisting.Text = GetString("ImportSite.StepSiteDetails.ExistingSite");
                radNewSite.Text = GetString("ImportSite.StepSiteDetails.NewSite");

                if (AllowExisting)
                {
                    siteSelector.AllowAll = false;
                    siteSelector.AllowEmpty = false;
                    if (siteSelector.DropDownSingleSelect.Items.Count == 0)
                    {
                        if (SiteInfoProvider.GetSitesCount() > 0)
                        {
                            plcNewSelection.Visible = true;
                            plcExisting.Visible = true;

                            siteSelector.Value = CMSContext.CurrentSiteID;

                            // Force to load now
                            siteSelector.Reload(true);
                        }
                        else
                        {
                            plcNewSelection.Visible = false;
                            plcExisting.Visible = false;
                        }
                    }


                    // Single site object is imported
                    if (!SingleObject || !Settings.SiteIsIncluded)
                    {
                        ltlScript.Text += ScriptHelper.GetScript(
                            "function SelectionChanged() { \n" +
                            "   var newSite = document.getElementById('" + radNewSite.ClientID + "').checked; \n" +
                            "   document.getElementById('" + txtDomain.ClientID + "').disabled = !newSite; \n" +
                            "   document.getElementById('" + txtSiteDisplayName.TextBox.ClientID + "').disabled = !newSite; \n" +
                            "   document.getElementById('" + txtSiteName.ClientID + "').disabled = !newSite; \n" +
                            "   document.getElementById('" + siteSelector.DropDownSingleSelect.ClientID + "').disabled = newSite; \n" +
                            "} \n"
                        );

                        radNewSite.Attributes.Add("onclick", "SelectionChanged()");
                        radExisting.Attributes.Add("onclick", "SelectionChanged()");
                    }
                }
                else
                {
                    plcNewSelection.Visible = false;
                    plcExisting.Visible = false;
                }
            }
        }
    }


    void rad_CheckedChanged(object sender, EventArgs e)
    {
        rfvDomain.Enabled = radNewSite.Checked;
        rfvSiteDisplayName.Enabled = radNewSite.Checked;
        rfvSiteName.Enabled = radNewSite.Checked;

        ReloadData(false);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public void ReloadData()
    {
        ReloadData(true);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    /// <param name="refreshSelection">Refresh selection</param>
    public void ReloadData(bool refreshSelection)
    {
        bool singleSiteObject = SingleObject && Settings.SiteIsIncluded;

        // Refresh selection
        if (refreshSelection)
        {
            radExisting.Checked = singleSiteObject;
            radNewSite.Checked = !singleSiteObject;
            plcExistingSelection.Visible = !singleSiteObject;
            plcNewSelection.Visible = !singleSiteObject && AllowExisting && (siteSelector.DropDownSingleSelect.Items.Count != 0);
            plcNewSite.Visible = !singleSiteObject;
            siteSelector.Enabled = singleSiteObject;
        }

        txtDomain.Enabled = radNewSite.Checked;
        txtSiteDisplayName.Enabled = radNewSite.Checked;
        txtSiteName.Enabled = radNewSite.Checked;
        cultureElem.Enabled = radNewSite.Checked;
        siteSelector.Enabled = !radNewSite.Checked;

        DataTable table = ImportProvider.GetObjectsData(Settings, SiteObjectType.SITE, true);
        if (!DataHelper.DataSourceIsEmpty(table))
        {
            // Get datarow
            DataRow dr = table.Rows[0];

            txtSiteDisplayName.Text = SiteDisplayName ?? dr["SiteDisplayName"].ToString();

            txtDomain.Text = DomainName ?? dr["SiteDomainName"].ToString();

            txtSiteName.Text = SiteName ?? dr["SiteName"].ToString();
        }
    }


    /// <summary>
    /// Apply control settings.
    /// </summary>
    public bool ApplySettings()
    {
        // Set additional settings
        Settings.ExistingSite = !radNewSite.Checked;
        Settings.SetSettings(ImportExportHelper.SETTINGS_BIZFORM_DATA, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_FORUM_POSTS, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_BOARD_MESSAGES, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_USER_DASHBOARDS, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_USER_SITE_DASHBOARDS, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_SCOPES, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_PAGETEMPLATE_VARIANTS, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_CUSTOMTABLE_DATA, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_WORKFLOW_SCOPES, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_MEDIA_FILES, radNewSite.Checked);
        Settings.SetSettings(ImportExportHelper.SETTINGS_MEDIA_FILES_PHYSICAL, radNewSite.Checked);

        if (radNewSite.Checked)
        {
            Settings.SiteId = 0;

            // Get site name
            string siteName = txtSiteName.Text.Trim();

            // Get validation result
            string result = new Validator().NotEmpty(txtSiteDisplayName.Text, GetString("ImportSite.StepSiteDetails.SiteDisplayNameError")).NotEmpty(siteName, GetString("ImportSite.StepSiteDetails.SiteNameError")).IsCodeName(txtSiteName.Text, GetString("ImportSite.StepSiteDetails.SiteNameNotValid")).Result;

            // Check if there is site with specified code name
            if (SiteInfoProvider.GetSiteInfo(siteName) != null)
            {
                result = GetString("NewSite_SiteDetails.ErrorSiteExists");
            }

            if (string.IsNullOrEmpty(result))
            {
                txtDomain.Text = URLHelper.RemoveProtocol(txtDomain.Text);

                // Set site details
                Settings.SiteDisplayName = txtSiteDisplayName.Text;
                Settings.SiteDomain = txtDomain.Text;
                Settings.SiteName = siteName;
            }
            else
            {
                lblError.Text = result;
                return false;
            }
        }
        else
        {
            // Existing site            
            Settings.SiteId = ValidationHelper.GetInteger(siteSelector.Value, 0);

            // Get site info
            SiteInfo si = SiteInfoProvider.GetSiteInfo(Settings.SiteId);
            if (si != null)
            {
                Settings.SiteDisplayName = si.DisplayName;
                Settings.SiteDomain = si.DomainName;
                Settings.SiteName = si.SiteName;
            }
        }
        return true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblError.Visible = (lblError.Text != "");
    }
}
