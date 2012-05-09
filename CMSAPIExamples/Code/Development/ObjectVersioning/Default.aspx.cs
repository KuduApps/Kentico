using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.SettingsProvider;

[Title(Text = "Object versioning", ImageUrl = "CMSModules/CMS_Settings/Categories/CMS_Synchronization_Versioning/module.png")]
public partial class CMSAPIExamples_Code_Development_ObjectVersioning_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Versioning
        this.apiCreateVersionedObject.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateVersionedObject);
        this.apiCreateVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateVersion);
        this.apiRollbackVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RollbackVersion);
        this.apiDestroyVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DestroyVersion);
        this.apiDestroyHistory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DestroyHistory);
        this.apiEnsureVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(EnsureVersion);

        // Recycle bin
        this.apiDeleteObject.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteObject);
        this.apiRestoreObject.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RestoreObject);

        // Deleting
        this.apiDestroyObject.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DestroyObject);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Versioning
        this.apiCreateVersionedObject.Run();
        this.apiCreateVersion.Run();
        this.apiRollbackVersion.Run();
        this.apiDestroyVersion.Run();
        this.apiDestroyHistory.Run();
        this.apiEnsureVersion.Run();

        // Recycle bin
        this.apiDeleteObject.Run();
        this.apiRestoreObject.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Versioning
        this.apiDestroyObject.Run();
    }

    #endregion


    #region "API examples - Object versioning"

    /// <summary>
    /// Creates versioned css stylesheet. Called when the "Create versioned object" button is pressed.
    /// </summary>
    private bool CreateVersionedObject()
    {
        // Create new css stylesheet object
        CssStylesheetInfo newStylesheet = new CssStylesheetInfo();

        // Check if object versioning of stylesheet objects is allowed on current site
        if (ObjectVersionManager.AllowObjectVersioning(newStylesheet, CMSContext.CurrentSiteName))
        {
            // Set the properties
            newStylesheet.StylesheetDisplayName = "My new versioned stylesheet";
            newStylesheet.StylesheetName = "MyNewVersionedStylesheet";
            newStylesheet.StylesheetText = "Some versioned CSS code";

            // Save the css stylesheet
            CssStylesheetInfoProvider.SetCssStylesheetInfo(newStylesheet);

            // Add css stylesheet to site
            int stylesheetId = newStylesheet.StylesheetID;
            int siteId = CMSContext.CurrentSiteID;

            CssStylesheetSiteInfoProvider.AddCssStylesheetToSite(stylesheetId, siteId);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Creates new version of the object. Called when the "Create version" button is pressed.
    /// Expects the CreateVersionedObject method to be run first.
    /// </summary>
    private bool CreateVersion()
    {
        // Get the css stylesheet
        CssStylesheetInfo newStylesheetVersion = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewVersionedStylesheet");
        if (newStylesheetVersion != null)
        {
            // Check if object versioning of stylesheet objects is allowed on current site
            if (ObjectVersionManager.AllowObjectVersioning(newStylesheetVersion, CMSContext.CurrentSiteName))
            {
                // Update the properties
                newStylesheetVersion.StylesheetDisplayName = newStylesheetVersion.StylesheetDisplayName.ToLower();

                // Create new version
                ObjectVersionManager.CreateVersion(newStylesheetVersion, true);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Provides version rollback. Called when the "Rollback version" button is pressed.
    /// Expects the CreateVersionedObject method to be run first.
    /// </summary>
    private bool RollbackVersion()
    {
        // Get the css stylesheet
        CssStylesheetInfo stylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewVersionedStylesheet");
        if (stylesheet != null)
        {
            // Prepare query parameters
            string where = "VersionObjectID =" + stylesheet.StylesheetID + " AND VersionObjectType = '" + stylesheet.ObjectType + "'";
            string orderBy = "VersionModifiedWhen ASC";
            int topN = 1;

            // Get dataset with versions according to the parameters
            DataSet versionDS = ObjectVersionHistoryInfoProvider.GetVersionHistories(where, orderBy, topN, null);

            if (!DataHelper.DataSourceIsEmpty(versionDS))
            {
                // Get version
                ObjectVersionHistoryInfo version = new ObjectVersionHistoryInfo(versionDS.Tables[0].Rows[0]);

                // Roll back
                ObjectVersionManager.RollbackVersion(version.VersionID);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Destroys latest version from history. Called when the "Destroy version" button is pressed.
    /// Expects the CreateVersionedObject method to be run first.
    /// </summary>
    private bool DestroyVersion()
    {
        // Get the css stylesheet
        CssStylesheetInfo stylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewVersionedStylesheet");
        if (stylesheet != null)
        {
            // Get the latest version
            ObjectVersionHistoryInfo version = ObjectVersionManager.GetLatestVersion(stylesheet.ObjectType, stylesheet.StylesheetID);

            if (version != null)
            {
                // Destroy the latest version
                ObjectVersionManager.DestroyObjectVersion(version.VersionID);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Destroys version history. Called when the "Destroy history" button is pressed.
    /// Expects the CreateVersionedObject method to be run first.
    /// </summary>
    private bool DestroyHistory()
    {
        // Get the css stylesheet
        CssStylesheetInfo stylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewVersionedStylesheet");
        if (stylesheet != null)
        {
            // Destroy version history
            ObjectVersionManager.DestroyObjectHistory(stylesheet.ObjectType, stylesheet.StylesheetID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Destroys object (without creating new version for recycle bin). Called when the "DestroyObject" button is pressed.
    /// Expects the CreateVersionedObject method to be run first.
    /// </summary>
    private bool DestroyObject()
    {
        // Get the css stylesheet
        CssStylesheetInfo destroyStylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewVersionedStylesheet");

        if (destroyStylesheet != null)
        {
            // Destroy the object (in action context with disabled creating of new versions for recycle bin)
            using (CMSActionContext context = new CMSActionContext())
            {
                // Disable creating of new versions
                context.CreateVersion = false;

                // Destroy the css stylesheet
                CssStylesheetInfoProvider.DeleteCssStylesheetInfo(destroyStylesheet);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Creates new version of the object. Called when the "Ensure version" button is pressed.
    /// Expects the CreateVersionedObject method to be run first.
    /// </summary>
    private bool EnsureVersion()
    {
        // Get the css stylesheet
        CssStylesheetInfo stylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewVersionedStylesheet");
        if (stylesheet != null)
        {
            // Check if object versioning of stylesheet objects is allowed on current site
            if (ObjectVersionManager.AllowObjectVersioning(stylesheet, CMSContext.CurrentSiteName))
            {
                // Ensure version
                ObjectVersionManager.EnsureVersion(stylesheet, false);

                return true;
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Object recycle bin"

    /// <summary>
    /// Deletes object. Called when the "Delete object" button is pressed.
    /// Expects the CreateVersionedObject method to be run first.
    /// </summary>
    private bool DeleteObject()
    {
        // Get the css stylesheet
        CssStylesheetInfo deleteStylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewVersionedStylesheet");

        if (deleteStylesheet != null)
        {
            // Check if restoring from recycle bin is allowed on current site
            if (ObjectVersionManager.AllowObjectRestore(deleteStylesheet, CMSContext.CurrentSiteName))
            {
                // Delete the css stylesheet
                CssStylesheetInfoProvider.DeleteCssStylesheetInfo(deleteStylesheet);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Restores object from recycle bin. Called when the "Restore object" button is pressed.
    /// Expects the DeleteObject method to be run first.
    /// </summary>
    private bool RestoreObject()
    {
        // Prepare query parameters
        string where = "VersionObjectType = '" + SiteObjectType.CSSSTYLESHEET + "' AND VersionDeletedByUserID = " + CMSContext.CurrentUser.UserID;
        string orderBy = "VersionDeletedWhen DESC";
        int topN = 1;

        // Get dataset with versions according to the parameters
        DataSet versionDS = ObjectVersionHistoryInfoProvider.GetVersionHistories(where, orderBy, topN, null);

        if (!DataHelper.DataSourceIsEmpty(versionDS))
        {
            // Get version
            ObjectVersionHistoryInfo version = new ObjectVersionHistoryInfo(versionDS.Tables[0].Rows[0]);

            // Restore the object
            ObjectVersionManager.RestoreObject(version.VersionID, true);

            return true;
        }

        return false;
    }

    #endregion
}
