using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

[Title(Text = "Custom tables", ImageUrl = "Objects/CMS_CustomTable/object.png")]
public partial class CMSAPIExamples_Code_Development_CustomTables_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // CustomTableItem
        this.apiCreateCustomTableItem.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateCustomTableItem);
        this.apiGetAndUpdateCustomTableItem.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateCustomTableItem);
        this.apiGetAndMoveCustomTableItemDown.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndMoveCustomTableItemDown);
        this.apiGetAndMoveCustomTableItemUp.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndMoveCustomTableItemUp);
        this.apiGetAndBulkUpdateCustomTableItems.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateCustomTableItems);
        this.apiDeleteCustomTableItem.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCustomTableItem);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Custom table item
        this.apiCreateCustomTableItem.Run();
        this.apiGetAndUpdateCustomTableItem.Run();
        this.apiGetAndMoveCustomTableItemDown.Run();
        this.apiGetAndMoveCustomTableItemUp.Run();
        this.apiGetAndBulkUpdateCustomTableItems.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Custom table item
        this.apiDeleteCustomTableItem.Run();
    }

    #endregion


    #region "API examples - CustomTableItem"

    /// <summary>
    /// Creates custom table item. Called when the "Create item" button is pressed.
    /// </summary>
    private bool CreateCustomTableItem()
    {
        // Create new Custom table item provider
        CustomTableItemProvider customTableProvider = new CustomTableItemProvider(CMSContext.CurrentUser);

        // Prepare the parameters
        string customTableClassName = "customtable.sampletable";

        // Check if Custom table 'Sample table' exists
        DataClassInfo customTable = DataClassInfoProvider.GetDataClass(customTableClassName);
        if (customTable != null)            
        {
            // Create new custom table item 
            CustomTableItem newCustomTableItem = new CustomTableItem(customTableClassName, customTableProvider);

            // Set the ItemText field value
            newCustomTableItem.SetValue("ItemText", "New text");

            // Insert the custom table item into database
            newCustomTableItem.Insert();

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates custom table item. Called when the "Get and update item" button is pressed.
    /// Expects the CreateCustomTableItem method to be run first.
    /// </summary>
    private bool GetAndUpdateCustomTableItem()
    {
        // Create new Custom table item provider
        CustomTableItemProvider customTableProvider = new CustomTableItemProvider(CMSContext.CurrentUser);

        string customTableClassName = "customtable.sampletable";

        // Check if Custom table 'Sample table' exists
        DataClassInfo customTable = DataClassInfoProvider.GetDataClass(customTableClassName);
        if (customTable != null)
        {
            // Prepare the parameters 
            string where = "ItemText LIKE N'New text'";
            int topN = 1;
            string columns = "ItemID";

            // Get the data set according to the parameters 
            DataSet dataSet = customTableProvider.GetItems(customTableClassName, where, null, topN, columns);

            if (!DataHelper.DataSourceIsEmpty(dataSet))
            {
                // Get the custom table item ID
                int itemID = ValidationHelper.GetInteger(dataSet.Tables[0].Rows[0][0], 0);

                // Get the custom table item
                CustomTableItem updateCustomTableItem = customTableProvider.GetItem(itemID, customTableClassName);

                if (updateCustomTableItem != null)
                {
                    string itemText = ValidationHelper.GetString(updateCustomTableItem.GetValue("ItemText"), "");

                    // Set new values
                    updateCustomTableItem.SetValue("ItemText", itemText.ToLower());

                    // Save the changes
                    updateCustomTableItem.Update();

                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Gets the custom table item and moves it down. Called when the "Get and move item down" button is pressed.
    /// Expects the CreateCustomTableItem method to be run first.
    /// </summary>
    private bool GetAndMoveCustomTableItemDown()
    {
        // Create new Custom table item provider
        CustomTableItemProvider customTableProvider = new CustomTableItemProvider(CMSContext.CurrentUser);
        
        string customTableClassName = "customtable.sampletable";
        
        // Check if Custom table 'Sample table' exists
        DataClassInfo customTable = DataClassInfoProvider.GetDataClass(customTableClassName);
        if (customTable != null)
        {
            // Prepare the parameters
            string where = "ItemText LIKE N'New text'";
            int topN = 1;
            string columns = "ItemID";

            // Get the data set according to the parameters 
            DataSet dataSet = customTableProvider.GetItems(customTableClassName, where, null, topN, columns);

            if (!DataHelper.DataSourceIsEmpty(dataSet))
            {
                // Get the custom table item ID
                int itemID = ValidationHelper.GetInteger(dataSet.Tables[0].Rows[0][0], 0);

                // Move the item down
                customTableProvider.MoveItemDown(itemID, customTableClassName);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets the custom table item and moves it up. Called when the "Get and move item up" button is pressed.
    /// Expects the CreateCustomTableItem method to be run first.
    /// </summary>
    private bool GetAndMoveCustomTableItemUp()
    {
        // Create new Custom table item provider
        CustomTableItemProvider customTableProvider = new CustomTableItemProvider(CMSContext.CurrentUser);

        string customTableClassName = "customtable.sampletable";

        // Check if Custom table 'Sample table' exists
        DataClassInfo customTable = DataClassInfoProvider.GetDataClass(customTableClassName);
        if (customTable != null)
        {
            // Prepare the parameters
            string where = "ItemText LIKE N'New text'";
            int topN = 1;
            string columns = "ItemID";

            // Get the data set according to the parameters 
            DataSet dataSet = customTableProvider.GetItems(customTableClassName, where, null, topN, columns);

            if (!DataHelper.DataSourceIsEmpty(dataSet))
            {
                // Get the custom table item ID
                int itemID = ValidationHelper.GetInteger(dataSet.Tables[0].Rows[0][0], 0);

                // Move the item up
                customTableProvider.MoveItemUp(itemID, customTableClassName);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates custom table items. Called when the "Get and bulk update items" button is pressed.
    /// Expects the CreateCustomTableItem method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateCustomTableItems()
    {
        // Create new Custom table item provider
        CustomTableItemProvider customTableProvider = new CustomTableItemProvider(CMSContext.CurrentUser);
       
        string customTableClassName = "customtable.sampletable";
        
        // Check if Custom table 'Sample table' exists
        DataClassInfo customTable = DataClassInfoProvider.GetDataClass(customTableClassName);
        if (customTable != null)
        {
            // Prepare the parameters

            string where = "ItemText LIKE N'New text%'";

            // Get the data
            DataSet customTableItems = customTableProvider.GetItems(customTableClassName, where, null);
            if (!DataHelper.DataSourceIsEmpty(customTableItems))
            {
                // Loop through the individual items
                foreach (DataRow customTableItemDr in customTableItems.Tables[0].Rows)
                {
                    // Create object from DataRow
                    CustomTableItem modifyCustomTableItem = new CustomTableItem(customTableItemDr, customTableClassName);

                    string itemText = ValidationHelper.GetString(modifyCustomTableItem.GetValue("ItemText"), "");

                    // Set new values
                    modifyCustomTableItem.SetValue("ItemText", itemText.ToUpper());

                    // Save the changes
                    modifyCustomTableItem.Update();
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Deletes customTableItem. Called when the "Delete item" button is pressed.
    /// Expects the CreateCustomTableItem method to be run first.
    /// </summary>
    private bool DeleteCustomTableItem()
    {
        // Create new Custom table item provider
        CustomTableItemProvider customTableProvider = new CustomTableItemProvider(CMSContext.CurrentUser);

        string customTableClassName = "customtable.sampletable";
        
        // Check if Custom table 'Sample table' exists
        DataClassInfo customTable = DataClassInfoProvider.GetDataClass(customTableClassName);
        if (customTable != null)
        {
            // Prepare the parameters
            string where = "ItemText LIKE N'New text%'";

            // Get the data
            DataSet customTableItems = customTableProvider.GetItems(customTableClassName, where, null);
            if (!DataHelper.DataSourceIsEmpty(customTableItems))
            {
                // Loop through the individual items
                foreach (DataRow customTableItemDr in customTableItems.Tables[0].Rows)
                {
                    // Create object from DataRow
                    CustomTableItem deleteCustomTableItem = new CustomTableItem(customTableItemDr, customTableClassName);

                    // Delete custom table item from database
                    deleteCustomTableItem.Delete();
                }

                return true;
            }
        }

        return false;
    }

    #endregion
}
