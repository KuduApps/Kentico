using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

[Title(Text = "Settings", ImageUrl = "Objects/CMS_SettingsKey/object.png")]
public partial class CMSAPIExamples_Code_Settings_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Settings category
        this.apiCreateSettingsCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateSettingsCategory);
        this.apiGetAndUpdateSettingsCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateSettingsCategory);
        this.apiGetAndBulkUpdateSettingsCategories.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateSettingsCategories);
        this.apiDeleteSettingsCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteSettingsCategory);

        // Settings group
        this.apiCreateSettingsGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateSettingsGroup);
        this.apiGetAndUpdateSettingsGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateSettingsGroup);
        this.apiGetAndBulkUpdateSettingsGroups.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateSettingsGroups);
        this.apiDeleteSettingsGroup.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteSettingsGroup);

        // Settings key
        this.apiCreateSettingsKey.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateSettingsKey);
        this.apiGetAndUpdateSettingsKey.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateSettingsKey);
        this.apiGetAndBulkUpdateSettingsKeys.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateSettingsKeys);
        this.apiDeleteSettingsKey.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteSettingsKey);

        // Web.config setting
        this.apiGetWebConfigSetting.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetWebConfigSetting);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Settings category
        this.apiCreateSettingsCategory.Run();
        this.apiGetAndUpdateSettingsCategory.Run();
        this.apiGetAndBulkUpdateSettingsCategories.Run();

        // Settings group
        this.apiCreateSettingsGroup.Run();
        this.apiGetAndUpdateSettingsGroup.Run();
        this.apiGetAndBulkUpdateSettingsGroups.Run();

        // Settings key
        this.apiCreateSettingsKey.Run();
        this.apiGetAndUpdateSettingsKey.Run();
        this.apiGetAndBulkUpdateSettingsKeys.Run();

        // Web.config setting
        this.apiGetWebConfigSetting.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Settings key
        this.apiDeleteSettingsKey.Run();

        // Settings group
        this.apiDeleteSettingsGroup.Run();

        // Settings category
        this.apiDeleteSettingsCategory.Run();
    }

    #endregion


    #region "API examples - Settings category"

    /// <summary>
    /// Creates settings category. Called when the "Create category" button is pressed.
    /// </summary>
    private bool CreateSettingsCategory()
    {
        // Get parent category ID
        SettingsCategoryInfo parentCategory = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("CMS.CustomSettings");
        if (parentCategory != null)
        {
            // Create new settings category object
            SettingsCategoryInfo newCategory = new SettingsCategoryInfo();

            // Set the properties
            newCategory.CategoryDisplayName = "My New Settings Category";
            newCategory.CategoryName = "MyNewSettingsCategory";
            newCategory.CategoryOrder = 0;
            newCategory.CategoryParentID = parentCategory.CategoryID;
            newCategory.CategoryIsGroup = false;
            newCategory.CategoryIsCustom = true;

            // Create settings category
            SettingsCategoryInfoProvider.SetSettingsCategoryInfo(newCategory);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates settings category. Called when the "Get and update category" button is pressed.
    /// Expects the CreateSettingsCategory method to be run first.
    /// </summary>
    private bool GetAndUpdateSettingsCategory()
    {
        // Get the settings category
        SettingsCategoryInfo updateCategory = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("MyNewSettingsCategory");
        if (updateCategory != null)
        {
            // Update the property
            updateCategory.CategoryDisplayName = updateCategory.CategoryDisplayName.ToLower();

            // Update settings category
            SettingsCategoryInfoProvider.SetSettingsCategoryInfo(updateCategory);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates settings categories. Called when the "Get and bulk update categories" button is pressed.
    /// Expects the CreateSettingsCategory method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateSettingsCategories()
    {
        // Prepare the parameters
        string where = "CategoryName LIKE 'MyNew%' AND CategoryIsGroup = 0";

        // Get the settings categories
        DataSet categories = SettingsCategoryInfoProvider.GetSettingsCategories(where, null);
        if (!DataHelper.DataSourceIsEmpty(categories))
        {
            // Loop through the individual items
            foreach (DataRow categoryDr in categories.Tables[0].Rows)
            {
                // Create object from DataRow
                SettingsCategoryInfo modifyCategory = new SettingsCategoryInfo(categoryDr);

                // Update the property
                modifyCategory.CategoryDisplayName = modifyCategory.CategoryDisplayName.ToUpper();

                // Update the settings category
                SettingsCategoryInfoProvider.SetSettingsCategoryInfo(modifyCategory);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes settings category. Called when the "Delete category" button is pressed.
    /// Expects the CreateSettingsCategory method to be run first.
    /// </summary>
    private bool DeleteSettingsCategory()
    {
        // Get the settings category
        SettingsCategoryInfo deleteCategory = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("MyNewSettingsCategory");

        // Delete the settings category
        SettingsCategoryInfoProvider.DeleteSettingsCategoryInfo(deleteCategory);

        return (deleteCategory != null);
    }

    #endregion


    #region "API examples - Settings group"

    /// <summary>
    /// Creates settings group. Called when the "Create group" button is pressed.
    /// Expects the CreateSettingsCategory method to be run first.
    /// </summary>
    private bool CreateSettingsGroup()
    {
        // Get the settings category
        SettingsCategoryInfo settingsCategory = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("MyNewSettingsCategory");
        if (settingsCategory != null)
        {
            // Create new settings group object
            SettingsCategoryInfo newGroup = new SettingsCategoryInfo();

            // Set the properties
            newGroup.CategoryDisplayName = "My New Settings Group";
            newGroup.CategoryName = "MyNewSettingsGroup";
            newGroup.CategoryOrder = 0;
            newGroup.CategoryParentID = settingsCategory.CategoryID;
            newGroup.CategoryIsGroup = true;
            newGroup.CategoryIsCustom = true;

            // Create the settings group
            SettingsCategoryInfoProvider.SetSettingsCategoryInfo(newGroup);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates settings group. Called when the "Get and update group" button is pressed.
    /// Expects the CreateSettingsGroup method to be run first.
    /// </summary>
    private bool GetAndUpdateSettingsGroup()
    {
        // Get the settings group
        SettingsCategoryInfo updateGroup = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("MyNewSettingsGroup");
        if (updateGroup != null)
        {
            // Update the property
            updateGroup.CategoryDisplayName = updateGroup.CategoryDisplayName.ToLower();

            // Update the settings group
            SettingsCategoryInfoProvider.SetSettingsCategoryInfo(updateGroup);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates settings groups. Called when the "Get and bulk update groups" button is pressed.
    /// Expects the CreateSettingsGroup method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateSettingsGroups()
    {
        // Prepare the parameters
        string where = "CategoryName LIKE 'MyNew%' AND CategoryIsGroup = 1";
        string orderBy = "CategoryName";

        // Get the data
        DataSet groups = SettingsCategoryInfoProvider.GetSettingsCategories(where, orderBy);
        if (!DataHelper.DataSourceIsEmpty(groups))
        {
            // Loop through the individual items
            foreach (DataRow groupDr in groups.Tables[0].Rows)
            {
                // Create object from DataRow
                SettingsCategoryInfo modifyGroup = new SettingsCategoryInfo(groupDr);

                // Update the property
                modifyGroup.CategoryDisplayName = modifyGroup.CategoryDisplayName.ToUpper();

                // Update settings group
                SettingsCategoryInfoProvider.SetSettingsCategoryInfo(modifyGroup);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes settings group. Called when the "Delete group" button is pressed.
    /// Expects the CreateSettingsGroup method to be run first.
    /// </summary>
    private bool DeleteSettingsGroup()
    {
        // Get the settings group
        SettingsCategoryInfo deleteGroup = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("MyNewSettingsGroup");

        // Delete the settings group
        SettingsCategoryInfoProvider.DeleteSettingsCategoryInfo(deleteGroup);

        return (deleteGroup != null);
    }

    #endregion


    #region "API examples - Settings key"

    /// <summary>
    /// Creates settings key. Called when the "Create key" button is pressed.
    /// </summary>
    private bool CreateSettingsKey()
    {
        // Get the settings group
        SettingsCategoryInfo settingsGroup = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName("MyNewSettingsGroup");
        if (settingsGroup != null)
        {
            // Create new settings key object
            SettingsKeyInfo newKey = new SettingsKeyInfo();

            // Set the properties
            newKey.KeyDisplayName = "My new key";
            newKey.KeyName = "MyNewKey";
            newKey.KeyDescription = "My new key description";
            newKey.KeyType = "string";
            newKey.KeyValue = "My new value";
            newKey.KeyCategoryID = settingsGroup.CategoryID;
            newKey.KeyDefaultValue = null;

            // Set Site ID for site specific settings key (for global settings key is default value 0).
            newKey.SiteID = CMSContext.CurrentSiteID;

            // Create the settings key
            SettingsKeyProvider.SetValue(newKey);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates settings key. Called when the "Get and update key" button is pressed.
    /// Expects the CreateSettingsKey method to be run first.
    /// </summary>
    private bool GetAndUpdateSettingsKey()
    {
        // Get the settings key
        SettingsKeyInfo updateKey = SettingsKeyProvider.GetSettingsKeyInfo("MyNewKey", CMSContext.CurrentSiteID);
        if (updateKey != null)
        {
            // Update the property
            updateKey.KeyDisplayName = updateKey.KeyDisplayName.ToLower();

            // Update the settings key
            SettingsKeyProvider.SetValue(updateKey);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates settings keys. Called when the "Get and bulk update keys" button is pressed.
    /// Expects the CreateSettingsKey method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateSettingsKeys()
    {
        // Prepare the parameters
        string where = "KeyName LIKE N'MyNew%'";

        // Get the data
        DataSet keys = SettingsKeyProvider.GetSettingsKeys(where, null, 0, null);
        if (!DataHelper.DataSourceIsEmpty(keys))
        {
            // Loop through the individual items
            foreach (DataRow keyDr in keys.Tables[0].Rows)
            {
                // Create object from DataRow
                SettingsKeyInfo modifyKey = new SettingsKeyInfo(keyDr);

                // Update the property
                modifyKey.KeyDisplayName = modifyKey.KeyDisplayName.ToUpper();

                // Update the settings key
                SettingsKeyProvider.SetValue(modifyKey);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes settings key. Called when the "Delete key" button is pressed.
    /// Expects the CreateSettingsKey method to be run first.
    /// </summary>
    private bool DeleteSettingsKey()
    {
        // Get the settings key
        SettingsKeyInfo deleteKey = SettingsKeyProvider.GetSettingsKeyInfo("MyNewKey", CMSContext.CurrentSiteID);

        // Delete the settings key
        SettingsKeyProvider.DeleteKey(deleteKey);

        return (deleteKey != null);
    }

    #endregion


    #region "API examples - Web.config setting"

    /// <summary>
    /// Gets web.config setting. Called when the button "Get web.config setting" is pressed.
    /// </summary>
    private bool GetWebConfigSetting()
    {
        string webConfigSetting = ValidationHelper.GetString(SettingsHelper.AppSettings["WS.webservice"], "");

        return (!String.IsNullOrEmpty(webConfigSetting));
    }

    #endregion
}
