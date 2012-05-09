using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.ResourceManager;

[Title(Text = "UI cultures", ImageUrl = "CMSModules/CMS_UICultures/module.png")]
public partial class CMSAPIExamples_Code_Development_UICultures_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Uiculture
        this.apiCreateUICulture.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateUICulture);
        this.apiGetAndUpdateUICulture.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateUICulture);
        this.apiGetAndBulkUpdateUICultures.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateUICultures);
        this.apiDeleteUICulture.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteUICulture);

        // Resourcestring
        this.apiCreateResourceString.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateResourceString);
        this.apiGetAndUpdateResourceString.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateResourceString);
        this.apiDeleteResourceString.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteResourceString);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Uiculture
        this.apiCreateUICulture.Run();
        this.apiGetAndUpdateUICulture.Run();
        this.apiGetAndBulkUpdateUICultures.Run();

        // Resourcestring
        this.apiCreateResourceString.Run();
        this.apiGetAndUpdateResourceString.Run();

    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Resourcestring
        this.apiDeleteResourceString.Run();

        // Uiculture
        this.apiDeleteUICulture.Run();
    }

    #endregion


    #region "API examples - UI culture"

    /// <summary>
    /// Creates UI culture. Called when the "Create culture" button is pressed.
    /// </summary>
    private bool CreateUICulture()
    {
        // Create new UI culture object
        UICultureInfo newCulture = new UICultureInfo();

        // Set the properties
        newCulture.UICultureName = "My new culture";
        newCulture.UICultureCode = "my-cu";

        // Save the UI culture
        UICultureInfoProvider.SetUICultureInfo(newCulture);

        return true;
    }


    /// <summary>
    /// Gets and updates UI culture. Called when the "Get and update culture" button is pressed.
    /// Expects the CreateUICulture method to be run first.
    /// </summary>
    private bool GetAndUpdateUICulture()
    {
        // Get the UI culture
        UICultureInfo updateCulture = UICultureInfoProvider.GetUICultureInfo("my-cu");
        if (updateCulture != null)
        {
            // Update the properties
            updateCulture.UICultureName = updateCulture.UICultureName.ToLower();

            // Save the changes
            UICultureInfoProvider.SetUICultureInfo(updateCulture);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates UI cultures. Called when the "Get and bulk update cultures" button is pressed.
    /// Expects the CreateUICulture method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateUICultures()
    {
        // Prepare the parameters
        string where = "UICultureCode LIKE N'my-cu%'";

        // Get the data
        DataSet cultures = UICultureInfoProvider.GetUICultures(where, null);
        if (!DataHelper.DataSourceIsEmpty(cultures))
        {
            // Loop through the individual items
            foreach (DataRow cultureDr in cultures.Tables[0].Rows)
            {
                // Create object from DataRow
                UICultureInfo modifyCulture = new UICultureInfo(cultureDr);

                // Update the properties
                modifyCulture.UICultureName = modifyCulture.UICultureName.ToUpper();

                // Save the changes
                UICultureInfoProvider.SetUICultureInfo(modifyCulture);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes UI culture. Called when the "Delete culture" button is pressed.
    /// Expects the CreateUICulture method to be run first.
    /// </summary>
    private bool DeleteUICulture()
    {
        // Get the UI culture
        UICultureInfo deleteCulture = UICultureInfoProvider.GetUICultureInfo("my-cu");

        // Delete the UI culture
        UICultureInfoProvider.DeleteUICultureInfo(deleteCulture);

        return (deleteCulture != null);
    }

    #endregion


    #region "API examples - Resource string"

    /// <summary>
    /// Creates resource string in default culture. Called when the "Create resourcestring" button is pressed.
    /// Expects the CreateUICulture method to be run first.
    /// </summary>
    private bool CreateResourceString()
    {
        // Create new resource string object
        ResourceStringInfo newString = new ResourceStringInfo();

        // Set the properties
        newString.StringKey = "Test.MyNewResourceString";
        newString.UICultureCode = SqlResourceManager.DefaultUICulture;
        newString.TranslationText = "My new resource string translation.";
        newString.StringIsCustom = true;

        // Save the resource string
        SqlResourceManager.SetResourceStringInfo(newString);

        return true;
    }


    /// <summary>
    /// Gets and updates resource string. Called when the "Get and update resource string" button is pressed.
    /// Expects the CreateResourceString method to be run first.
    /// </summary>
    private bool GetAndUpdateResourceString()
    {
        // Get the resource string
        ResourceStringInfo updateString = SqlResourceManager.GetResourceStringInfo("Test.MyNewResourceString");
        if (updateString != null)
        {
            // Update the properties
            updateString.StringKey = updateString.StringKey.ToLower();

            // Save the changes
            SqlResourceManager.SetResourceStringInfo(updateString);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes resource string. Called when the "Delete resource string" button is pressed.
    /// Expects the CreateResourceString method to be run first.
    /// </summary>
    private bool DeleteResourceString()
    {
        // Get the resource string
        ResourceStringInfo deleteString = SqlResourceManager.GetResourceStringInfo("Test.MyNewResourceString");

        // Delete the resource string
        SqlResourceManager.DeleteResourceStringInfo(deleteString);

        return (deleteString != null);
    }

    #endregion
}