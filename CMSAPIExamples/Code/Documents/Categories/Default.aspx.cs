using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;

[Title(Text = "Categories", ImageUrl = "CMSModules/CMS_Categories/module.png")]
public partial class CMSAPIExamples_Code_Documents_Categories_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Category
        this.apiCreateCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateCategory);
        this.apiGetAndUpdateCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateCategory);
        this.apiGetAndBulkUpdateCategories.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateCategories);
        this.apiDeleteCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCategory);

        // Document in category
        this.apiAddDocumentToCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddDocumentToCategory);
        this.apiRemoveDocumentFromCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveDocumentFromCategory);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Category
        this.apiCreateCategory.Run();
        this.apiGetAndUpdateCategory.Run();
        this.apiGetAndBulkUpdateCategories.Run();

        // Document in category
        this.apiAddDocumentToCategory.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Document in category
        this.apiRemoveDocumentFromCategory.Run();

        // Category
        this.apiDeleteCategory.Run();
    }

    #endregion


    #region "API examples - Category"

    /// <summary>
    /// Creates category. Called when the "Create category" button is pressed.
    /// </summary>
    private bool CreateCategory()
    {
        // Create new category object
        CategoryInfo newCategory = new CategoryInfo();

        // Set the properties
        newCategory.CategoryDisplayName = "My new category";
        newCategory.CategoryName = "MyNewCategory";
        newCategory.CategoryDescription = "My new category description";
        newCategory.CategorySiteID = CMSContext.CurrentSiteID;
        newCategory.CategoryCount = 0;
        newCategory.CategoryEnabled = true;

        // Save the category
        CategoryInfoProvider.SetCategoryInfo(newCategory);

        return true;
    }


    /// <summary>
    /// Gets and updates category. Called when the "Get and update category" button is pressed.
    /// Expects the CreateCategory method to be run first.
    /// </summary>
    private bool GetAndUpdateCategory()
    {
        // Get the category
        CategoryInfo updateCategory = CategoryInfoProvider.GetCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);
        if (updateCategory != null)
        {
            // Update the properties
            updateCategory.CategoryDisplayName = updateCategory.CategoryDisplayName.ToLower();

            // Save the changes
            CategoryInfoProvider.SetCategoryInfo(updateCategory);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates categories. Called when the "Get and bulk update categories" button is pressed.
    /// Expects the CreateCategory method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateCategories()
    {
        // Prepare the parameters
        string where = "CategoryName LIKE N'MyNewCategory%'";

        // Get the data
        DataSet categories = CategoryInfoProvider.GetCategories(where, null);
        if (!DataHelper.DataSourceIsEmpty(categories))
        {
            // Loop through the individual items
            foreach (DataRow categoryDr in categories.Tables[0].Rows)
            {
                // Create object from DataRow
                CategoryInfo modifyCategory = new CategoryInfo(categoryDr);

                // Update the properties
                modifyCategory.CategoryDisplayName = modifyCategory.CategoryDisplayName.ToUpper();

                // Save the changes
                CategoryInfoProvider.SetCategoryInfo(modifyCategory);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes category. Called when the "Delete category" button is pressed.
    /// Expects the CreateCategory method to be run first.
    /// </summary>
    private bool DeleteCategory()
    {
        // Get the category
        CategoryInfo deleteCategory = CategoryInfoProvider.GetCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);

        // Delete the category
        CategoryInfoProvider.DeleteCategoryInfo(deleteCategory);

        return (deleteCategory != null);
    }

    #endregion


    #region "API examples - Document in category"

    /// <summary>
    /// Adds document to category. Called when the button "Add document from category" is pressed.
    /// </summary>
    private bool AddDocumentToCategory()
    {
        // Get the category
        CategoryInfo category = CategoryInfoProvider.GetCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);
        if (category != null)
        {
            // Get the tree structure
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            // Get the root document
            TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);

            // Add document to category
            DocumentCategoryInfoProvider.AddDocumentToCategory(root.DocumentID, category.CategoryID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes document from category. Called when the button "Remove document from category" is pressed.
    /// Expects the method AddDocumentToCategory to be run first.
    /// </summary>
    private bool RemoveDocumentFromCategory()
    {
        // Get the category
        CategoryInfo category = CategoryInfoProvider.GetCategoryInfo("MyNewCategory", CMSContext.CurrentSiteName);
        if (category != null)
        {
            // Get the tree structure
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            // Get the root document
            TreeNode root = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", null, true);

            // Get the document category relationship
            DocumentCategoryInfo documentCategory = DocumentCategoryInfoProvider.GetDocumentCategoryInfo(root.DocumentID, category.CategoryID);

            if (documentCategory != null)
            {
                // Remove document from category
                DocumentCategoryInfoProvider.DeleteDocumentCategoryInfo(documentCategory);

                return true;
            }
        }

        return false;
    }

    #endregion
}
