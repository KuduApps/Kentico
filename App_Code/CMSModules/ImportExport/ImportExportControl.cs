using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

using CMS.CMSImportExport;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.FormEngine;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.WebFarmSync;
using CMS.UIControls;
using CMS.SettingsProvider;

/// <summary>
/// Base import / export control
/// </summary>
public abstract class ImportExportControl : CMSUserControl
{
    /// <summary>
    /// Import / export settings
    /// </summary>
    AbstractImportExportSettings mSettings = null;


    #region "Public properties"

    /// <summary>
    /// Additional settings.
    /// </summary>
    public AbstractImportExportSettings Settings
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
    /// Version check.
    /// </summary>
    public int VersionCheck
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["VersionCheck"], -1);
        }
        set
        {
            ViewState["VersionCheck"] = value;
        }
    }


    /// <summary>
    /// Hotfix version check.
    /// </summary>
    public int HotfixVersionCheck
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["HotfixVersionCheck"], -1);
        }
        set
        {
            ViewState["HotfixVersionCheck"] = value;
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Gets current settings.
    /// </summary>
    public virtual void SaveSettings()
    {
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public virtual void ReloadData()
    {
    }


    /// <summary>
    /// Checks the license for selected objects.
    /// </summary>
    public static string CheckLicenses(SiteImportSettings settings)
    {
        string result = null;

        object[,] checkFeatures = new object[,] {
            { FeatureEnum.Unknown, "", false },
            { FeatureEnum.BizForms, FormObjectType.BIZFORM, true },
            { FeatureEnum.Forums, PredefinedObjectType.FORUM, true },
            { FeatureEnum.Newsletters, PredefinedObjectType.NEWSLETTER, true },
            { FeatureEnum.Subscribers, PredefinedObjectType.NEWSLETTERUSERSUBSCRIBER + ";" + PredefinedObjectType.NEWSLETTERROLESUBSCRIBER + ";" + PredefinedObjectType.NEWSLETTERSUBSCRIBER, true }, 
            { FeatureEnum.Staging, SynchronizationObjectType.STAGINGSERVER, true },
            { FeatureEnum.Ecommerce, PredefinedObjectType.SKU, false }, 
            { FeatureEnum.Polls, PredefinedObjectType.POLL, false },
            { FeatureEnum.Webfarm, WebFarmObjectType.WEBFARMSERVER, false },
            { FeatureEnum.SiteMembers, SiteObjectType.USER, false },
            { FeatureEnum.WorkflowVersioning, PredefinedObjectType.WORKFLOW, false }
            };

        // Get imported licenses
        DataSet ds = ImportProvider.LoadObjects(settings, LicenseObjectType.LICENSEKEY, false);

        string domain = string.IsNullOrEmpty(settings.SiteDomain) ? URLHelper.GetCurrentDomain().ToLower() : URLHelper.RemovePort(settings.SiteDomain).ToLower();
        
        // Remove application path
        int slashIndex = domain.IndexOf("/");
        if (slashIndex > -1)
        {
            domain = domain.Substring(0, slashIndex);
        }

        bool anyDomain = ((domain == "localhost") || (domain == "127.0.0.1"));

        // Check all features
        for (int i = 0; i <= checkFeatures.GetUpperBound(0); i++)
        {
            string[] objectTypes = ((string)checkFeatures[i, 1]).Split(';');
            bool siteObject = (bool)checkFeatures[i, 2];
            string objectType = objectTypes[0];

            // Check objects
            int count = (objectType != "") ? 0 : 1;
            for (int j = 0; j < objectTypes.Length; ++j)
            {
                objectType = objectTypes[j];
                if (objectType != "")
                {
                    ArrayList codenames = settings.GetSelectedObjects(objectType, siteObject);
                    count += (codenames == null) ? 0 : codenames.Count;
                }
            }

            // Check a special case for Workflows
            if (objectType.Equals(PredefinedObjectType.WORKFLOW))
            {
                // Check workflows
                bool includeWorkflowScopes = ValidationHelper.GetBoolean(settings.GetSettings(ImportExportHelper.SETTINGS_WORKFLOW_SCOPES), false);
                if (includeWorkflowScopes)
                {
                    // Check if there are any workflow scopes in the imported package and if there are, increase the count.
                    DataSet dsScopes = ImportProvider.LoadObjects(settings, PredefinedObjectType.WORKFLOWSCOPE, true);
                    if (!DataHelper.DataSourceIsEmpty(dsScopes))
                    {
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                FeatureEnum feature = (FeatureEnum)checkFeatures[i, 0];

                // Get best available license from DB
                LicenseKeyInfo bestLicense = LicenseKeyInfoProvider.GetLicenseKeyInfo(domain, feature);
                if ((bestLicense != null) && (bestLicense.ValidationResult != LicenseValidationEnum.Valid))
                {
                    bestLicense = null;
                }

                // Check new licenses
                LicenseKeyInfo bestSelected = null;
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        LicenseKeyInfo lki = new LicenseKeyInfo(dr);
                        // Use license only if selected
                        if ((settings.IsSelected(LicenseObjectType.LICENSEKEY, lki.Domain, false)) && (anyDomain || (lki.Domain.ToLower() == domain)) && LicenseKeyInfoProvider.IsBetterLicense(lki, bestSelected, feature))
                        {
                            bestSelected = lki;
                            if (bestSelected.Domain.ToLower() == domain)
                            {
                                break;
                            }
                        }
                    }
                }

                // Check the license
                if (feature == FeatureEnum.Unknown)
                {
                    if ((bestLicense == null) && (bestSelected == null))
                    {
                        return ResHelper.GetString("Import.NoLicense");
                    }
                }
                else
                {
                    // Check the limit
                    int limit = GetLimit(bestLicense, feature);

                    // Use a database license if it is better than the one which is being imported
                    if (LicenseKeyInfoProvider.IsBetterLicense(bestLicense, bestSelected, feature))
                    {
                        bestSelected = bestLicense;
                    }

                    int selectedLimit = GetLimit(bestSelected, feature);
                    if (bestSelected != null)
                    {
                        if (bestSelected.ValidationResult == LicenseValidationEnum.Valid)
                        {
                            if (!anyDomain || (bestSelected.Domain.ToLower() == domain))
                            {
                                limit = selectedLimit;
                            }
                            else
                            {
                                // If selected better, take the selected
                                if (selectedLimit > limit)
                                {
                                    limit = selectedLimit;
                                }
                            }
                        }
                        else
                        {
                            if (!anyDomain || (bestSelected.Domain.ToLower() == domain))
                            {
                                limit = 0;
                            }
                        }
                    }
                    if (limit < count)
                    {
                        if (limit <= 0)
                        {
                            result += String.Format(ResHelper.GetString("Import.LimitZero"), ResHelper.GetString("ObjectTasks." + objectType.Replace(".", "_")));
                        }
                        else
                        {
                            result += String.Format(ResHelper.GetString("Import.LimitExceeded"), ResHelper.GetString("ObjectTasks." + objectType.Replace(".", "_")), limit);
                        }

                        // If better license
                        if ((bestLicense != null) && (bestSelected != null) && LicenseKeyInfoProvider.IsBetterLicense(bestLicense, bestSelected, feature))
                        {
                            result += " " + ResHelper.GetString("Import.BetterLicenseExists");
                        }

                        result += "<br />";
                    }
                }
            }
        }

        return result;
    }


    /// <summary>
    /// Gets the limit of objects for given license and feature.
    /// </summary>
    /// <param name="license">License</param>
    /// <param name="feature">Feature</param>
    public static int GetLimit(LicenseKeyInfo license, FeatureEnum feature)
    {
        if (license == null)
        {
            return 0;
        }

        // If feature not available, no objects allowed
        if (!LicenseKeyInfoProvider.IsFeatureAvailable(license, feature))
        {
            return 0;
        }

        // Get version limit
        int limit = LicenseKeyInfoProvider.VersionLimitations(license, feature);
        if (limit == 0)
        {
            return int.MaxValue;
        }

        return limit;
    }


    /// <summary>
    /// Checks the version of the controls (without hotfix version).
    /// </summary>
    public bool CheckVersion()
    {
        if (VersionCheck == -1)
        {
            SiteImportSettings importSettings = (SiteImportSettings)this.Settings;
            if ((importSettings != null) && importSettings.TemporaryFilesCreated)
            {
                VersionCheck = ImportExportHelper.IsLowerVersion(importSettings.Version, null, CMSContext.SYSTEM_VERSION, null) ? 1 : 0;
            }
            else
            {
                return false;
            }
        }
        return (VersionCheck > 0);
    }
    

    /// <summary>
    /// Checks the hotfix version of the controls.
    /// </summary>
    public bool CheckHotfixVersion()
    {
        if (HotfixVersionCheck == -1)
        {
            SiteImportSettings importSettings = (SiteImportSettings)Settings;
            if ((importSettings != null) && importSettings.TemporaryFilesCreated)
            {
                HotfixVersionCheck = (ValidationHelper.GetInteger(importSettings.HotfixVersion, 0) < ValidationHelper.GetInteger(CMSContext.HotfixVersion, 0)) ? 1 : 0;
            }
            else
            {
                return false;
            }
        }
        return (HotfixVersionCheck > 0);
    }

    #endregion
}
