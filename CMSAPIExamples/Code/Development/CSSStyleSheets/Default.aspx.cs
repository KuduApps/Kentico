using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

[Title(Text = "CSS sylesheets", ImageUrl = "Objects/CMS_CSSStylesheet/object.png")]
public partial class CMSAPIExamples_Code_Development_CSSStyleSheets_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Css stylesheet
        this.apiCreateCssStylesheet.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateCssStylesheet);
        this.apiGetAndUpdateCssStylesheet.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateCssStylesheet);
        this.apiGetAndBulkUpdateCssStylesheets.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateCssStylesheets);
        this.apiDeleteCssStylesheet.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCssStylesheet);

        // Css stylesheet on site
        this.apiAddCssStylesheetToSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddCssStylesheetToSite);
        this.apiRemoveCssStylesheetFromSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveCssStylesheetFromSite);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Css stylesheet
        this.apiCreateCssStylesheet.Run();
        this.apiGetAndUpdateCssStylesheet.Run();
        this.apiGetAndBulkUpdateCssStylesheets.Run();

        // Css stylesheet on site
        this.apiAddCssStylesheetToSite.Run();
        
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Css stylesheet on site
        this.apiRemoveCssStylesheetFromSite.Run();

        // Css stylesheet
        this.apiDeleteCssStylesheet.Run();
    }

    #endregion


    #region "API examples - Css stylesheet"

    /// <summary>
    /// Creates css stylesheet. Called when the "Create stylesheet" button is pressed.
    /// </summary>
    private bool CreateCssStylesheet()
    {
        // Create new css stylesheet object
        CssStylesheetInfo newStylesheet = new CssStylesheetInfo();

        // Set the properties
        newStylesheet.StylesheetDisplayName = "My new stylesheet";
        newStylesheet.StylesheetName = "MyNewStylesheet";
        newStylesheet.StylesheetText = "Some CSS code";


        // Save the css stylesheet
        CssStylesheetInfoProvider.SetCssStylesheetInfo(newStylesheet);

        return true;
    }


    /// <summary>
    /// Gets and updates css stylesheet. Called when the "Get and update stylesheet" button is pressed.
    /// Expects the CreateCssStylesheet method to be run first.
    /// </summary>
    private bool GetAndUpdateCssStylesheet()
    {
        // Get the css stylesheet
        CssStylesheetInfo updateStylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewStylesheet");
        if (updateStylesheet != null)
        {
            // Update the properties
            updateStylesheet.StylesheetDisplayName = updateStylesheet.StylesheetDisplayName.ToLower();

            // Save the changes
            CssStylesheetInfoProvider.SetCssStylesheetInfo(updateStylesheet);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates css stylesheets. Called when the "Get and bulk update stylesheets" button is pressed.
    /// Expects the CreateCssStylesheet method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateCssStylesheets()
    {
        // Prepare the parameters
        string where = "StylesheetName LIKE N'MyNewStylesheet%'";

        // Get the data
        DataSet stylesheets = CssStylesheetInfoProvider.GetCssStylesheets(where, null);
        if (!DataHelper.DataSourceIsEmpty(stylesheets))
        {
            // Loop through the individual items
            foreach (DataRow stylesheetDr in stylesheets.Tables[0].Rows)
            {
                // Create object from DataRow
                CssStylesheetInfo modifyStylesheet = new CssStylesheetInfo(stylesheetDr);

                // Update the properties
                modifyStylesheet.StylesheetDisplayName = modifyStylesheet.StylesheetDisplayName.ToUpper();

                // Save the changes
                CssStylesheetInfoProvider.SetCssStylesheetInfo(modifyStylesheet);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes css stylesheet. Called when the "Delete stylesheet" button is pressed.
    /// Expects the CreateCssStylesheet method to be run first.
    /// </summary>
    private bool DeleteCssStylesheet()
    {
        // Get the css stylesheet
        CssStylesheetInfo deleteStylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewStylesheet");

        // Delete the css stylesheet
        CssStylesheetInfoProvider.DeleteCssStylesheetInfo(deleteStylesheet);

        return (deleteStylesheet != null);
    }

    #endregion


    #region "API examples - Css stylesheet on site"

    /// <summary>
    /// Adds css stylesheet to site. Called when the "Add stylesheet to site" button is pressed.
    /// Expects the CreateCssStylesheet method to be run first.
    /// </summary>
    private bool AddCssStylesheetToSite()
    {
        // Get the css stylesheet
        CssStylesheetInfo stylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewStylesheet");
        if (stylesheet != null)
        {
            int stylesheetId = stylesheet.StylesheetID;
            int siteId = CMSContext.CurrentSiteID;

            // Save the binding
            CssStylesheetSiteInfoProvider.AddCssStylesheetToSite(stylesheetId, siteId);

            return true;
        }

        return false;
    }


  
    /// <summary>
    /// Removes css stylesheet from site. Called when the "Remove stylesheet from site" button is pressed.
    /// Expects the AddCssStylesheetToSite method to be run first.
    /// </summary>
    private bool RemoveCssStylesheetFromSite()
    {
        // Get the css stylesheet
        CssStylesheetInfo removeStylesheet = CssStylesheetInfoProvider.GetCssStylesheetInfo("MyNewStylesheet");
        if (removeStylesheet != null)
        {
            int siteId = CMSContext.CurrentSiteID;

            // Get the binding
            CssStylesheetSiteInfo stylesheetSite = CssStylesheetSiteInfoProvider.GetCssStylesheetSiteInfo(removeStylesheet.StylesheetID, siteId);

            // Delete the binding
            CssStylesheetSiteInfoProvider.DeleteCssStylesheetSiteInfo(stylesheetSite);

            return true;
        }

        return false;
    }

    #endregion
}
