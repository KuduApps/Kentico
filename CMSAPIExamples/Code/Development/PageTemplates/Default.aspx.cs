using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;

[Title(Text = "Page templates", ImageUrl = "Objects/CMS_PageTemplate/object.png")]
public partial class CMSAPIExamples_Code_Development_PageTemplates_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Page template
        this.apiCreatePageTemplate.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreatePageTemplate);
        this.apiGetAndUpdatePageTemplate.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdatePageTemplate);
        this.apiGetAndBulkUpdatePageTemplates.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdatePageTemplates);
        this.apiDeletePageTemplate.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeletePageTemplate);

        // Page template on site
        this.apiAddPageTemplateToSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddPageTemplateToSite);
        this.apiRemovePageTemplateFromSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemovePageTemplateFromSite);

        // Page template category
        this.apiCreatePageTemplateCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreatePageTemplateCategory);
        this.apiGetAndUpdatePageTemplateCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdatePageTemplateCategory);
        this.apiGetAndBulkUpdatePageTemplateCategories.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdatePageTemplateCategories);
        this.apiDeletePageTemplateCategory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeletePageTemplateCategory);

        // Page template scope
        this.apiCreatePageTemplateScope.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreatePageTemplateScope);
        this.apiDeletePageTemplateScope.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeletePageTemplateScope);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Page template category
        this.apiCreatePageTemplateCategory.Run();
        this.apiGetAndUpdatePageTemplateCategory.Run();
        this.apiGetAndBulkUpdatePageTemplateCategories.Run();

        // Page template
        this.apiCreatePageTemplate.Run();
        this.apiGetAndUpdatePageTemplate.Run();
        this.apiGetAndBulkUpdatePageTemplates.Run();

        // Page template on site
        this.apiAddPageTemplateToSite.Run();

        // Page template scope
        this.apiCreatePageTemplateScope.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Page template scope
        this.apiDeletePageTemplateScope.Run();

        // Page template on site
        this.apiRemovePageTemplateFromSite.Run();

        // Page template
        this.apiDeletePageTemplate.Run();

        // Page template category
        this.apiDeletePageTemplateCategory.Run();

    }

    #endregion


    #region "API examples - Page template category"

    /// <summary>
    /// Creates page template category. Called when the "Create category" button is pressed.
    /// </summary>
    private bool CreatePageTemplateCategory()
    {
        // Create new page template category object
        PageTemplateCategoryInfo newCategory = new PageTemplateCategoryInfo();

        // Set the properties
        newCategory.DisplayName = "My new category";
        newCategory.CategoryName = "MyNewCategory";

        // Save the page template category
        PageTemplateCategoryInfoProvider.SetPageTemplateCategoryInfo(newCategory);

        return true;
    }


    /// <summary>
    /// Gets and updates page template category. Called when the "Get and update category" button is pressed.
    /// Expects the CreatePageTemplateCategory method to be run first.
    /// </summary>
    private bool GetAndUpdatePageTemplateCategory()
    {
        // Get the page template category
        PageTemplateCategoryInfo updateCategory = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo("MyNewCategory");
        if (updateCategory != null)
        {
            // Update the properties
            updateCategory.DisplayName = updateCategory.DisplayName.ToLower();

            // Save the changes
            PageTemplateCategoryInfoProvider.SetPageTemplateCategoryInfo(updateCategory);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates page template categories. Called when the "Get and bulk update categories" button is pressed.
    /// Expects the CreatePageTemplateCategory method to be run first.
    /// </summary>
    private bool GetAndBulkUpdatePageTemplateCategories()
    {
        // Prepare the parameters
        string where = "CategoryName LIKE N'MyNewCategory%'";

        // Get the data
        DataSet categories = PageTemplateCategoryInfoProvider.GetCategoriesList(where, null);
        if (!DataHelper.DataSourceIsEmpty(categories))
        {
            // Loop through the individual items
            foreach (DataRow categoryDr in categories.Tables[0].Rows)
            {
                // Create object from DataRow
                PageTemplateCategoryInfo modifyCategory = new PageTemplateCategoryInfo(categoryDr);

                // Update the properties
                modifyCategory.DisplayName = modifyCategory.DisplayName.ToUpper();

                // Save the changes
                PageTemplateCategoryInfoProvider.SetPageTemplateCategoryInfo(modifyCategory);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes page template category. Called when the "Delete category" button is pressed.
    /// Expects the CreatePageTemplateCategory method to be run first.
    /// </summary>
    private bool DeletePageTemplateCategory()
    {
        // Get the page template category
        PageTemplateCategoryInfo deleteCategory = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo("MyNewCategory");

        // Delete the page template category
        PageTemplateCategoryInfoProvider.DeletePageTemplateCategory(deleteCategory);

        return (deleteCategory != null);
    }

    #endregion


    #region "API examples - Page template"

    /// <summary>
    /// Creates page template. Called when the "Create template" button is pressed.
    /// </summary>
    private bool CreatePageTemplate()
    {
        // Get the page template category
        PageTemplateCategoryInfo category = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo("MyNewCategory");
        if (category != null)
        {
            // Create new page template object
            PageTemplateInfo newTemplate = new PageTemplateInfo();

            // Set the properties
            newTemplate.DisplayName = "My new template";
            newTemplate.CodeName = "MyNewTemplate";
            newTemplate.Description = "This is page template created by API Example";
            newTemplate.PageTemplateSiteID = CMSContext.CurrentSiteID;
            newTemplate.FileName = " ";
            newTemplate.ShowAsMasterTemplate = false;
            newTemplate.IsPortal = true;
            newTemplate.InheritPageLevels = ""; // inherits all
            newTemplate.IsReusable = true;
            newTemplate.CategoryID = category.CategoryId;


            // Save the page template
            PageTemplateInfoProvider.SetPageTemplateInfo(newTemplate);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and updates page template. Called when the "Get and update template" button is pressed.
    /// Expects the CreatePageTemplate method to be run first.
    /// </summary>
    private bool GetAndUpdatePageTemplate()
    {
        // Get the page template
        PageTemplateInfo updateTemplate = PageTemplateInfoProvider.GetPageTemplateInfo("MyNewTemplate");
        if (updateTemplate != null)
        {
            // Update the properties
            updateTemplate.DisplayName = updateTemplate.DisplayName.ToLower();

            // Save the changes
            PageTemplateInfoProvider.SetPageTemplateInfo(updateTemplate);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates page templates. Called when the "Get and bulk update templates" button is pressed.
    /// Expects the CreatePageTemplate method to be run first.
    /// </summary>
    private bool GetAndBulkUpdatePageTemplates()
    {
        // Prepare the parameters
        string where = "PageTemplateCodeName LIKE N'MyNewTemplate%'";

        // Get the data
        DataSet templates = PageTemplateInfoProvider.GetTemplates(where, null);
        if (!DataHelper.DataSourceIsEmpty(templates))
        {
            // Loop through the individual items
            foreach (DataRow templateDr in templates.Tables[0].Rows)
            {
                // Create object from DataRow
                PageTemplateInfo modifyTemplate = new PageTemplateInfo(templateDr);

                // Update the properties
                modifyTemplate.DisplayName = modifyTemplate.DisplayName.ToUpper();

                // Save the changes
                PageTemplateInfoProvider.SetPageTemplateInfo(modifyTemplate);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes page template. Called when the "Delete template" button is pressed.
    /// Expects the CreatePageTemplate method to be run first.
    /// </summary>
    private bool DeletePageTemplate()
    {
        // Get the page template
        PageTemplateInfo deleteTemplate = PageTemplateInfoProvider.GetPageTemplateInfo("MyNewTemplate");

        // Delete the page template
        PageTemplateInfoProvider.DeletePageTemplate(deleteTemplate);

        return (deleteTemplate != null);
    }

    #endregion


    #region "API examples - Page template on site"

    /// <summary>
    /// Adds page template to site. Called when the "Add template to site" button is pressed.
    /// Expects the CreatePageTemplate method to be run first.
    /// </summary>
    private bool AddPageTemplateToSite()
    {
        // Get the page template
        PageTemplateInfo template = PageTemplateInfoProvider.GetPageTemplateInfo("MyNewTemplate");
        if (template != null)
        {
            int templateId = template.PageTemplateId;
            int siteId = CMSContext.CurrentSiteID;

            // Save the binding
            PageTemplateSiteInfoProvider.AddPageTemplateToSite(templateId, siteId);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes page template from site. Called when the "Remove template from site" button is pressed.
    /// Expects the AddPageTemplateToSite method to be run first.
    /// </summary>
    private bool RemovePageTemplateFromSite()
    {
        // Get the page template
        PageTemplateInfo removeTemplate = PageTemplateInfoProvider.GetPageTemplateInfo("MyNewTemplate");
        if (removeTemplate != null)
        {
            int siteId = CMSContext.CurrentSiteID;

            // Get the binding
            PageTemplateSiteInfo templateSite = PageTemplateSiteInfoProvider.GetPageTemplateSiteInfo(removeTemplate.PageTemplateId, siteId);

            // Delete the binding
            PageTemplateSiteInfoProvider.DeletePageTemplateSiteInfo(templateSite);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Page template scope"

    /// <summary>
    /// Creates page template scope. Called when the "Create scope" button is pressed.
    /// </summary>
    private bool CreatePageTemplateScope()
    {
        // Get template object
        PageTemplateInfo template = PageTemplateInfoProvider.GetPageTemplateInfo("MyNewTemplate");

        // If template exists
        if (template != null)
        {
            // Page template isn't from all pages
            template.PageTemplateForAllPages = false;

            // Create new template scope
            PageTemplateScopeInfo newScope = new PageTemplateScopeInfo();

            // Set some properties
            newScope.PageTemplateScopeTemplateID = template.PageTemplateId;
            newScope.PageTemplateScopeSiteID = CMSContext.CurrentSiteID;
            newScope.PageTemplateScopePath = "/";
            newScope.PageTemplateScopeLevels = "/{0}/{1}";

            // Save scope to database
            PageTemplateScopeInfoProvider.SetPageTemplateScopeInfo(newScope);

            // Update page template
            PageTemplateInfoProvider.SetPageTemplateInfo(template);

            return true;
        }

        return false;
    }

      
    /// <summary>
    /// Deletes page template scope. Called when the "Delete scope" button is pressed.
    /// Expects the CreatePageTemplateScope method to be run first.
    /// </summary>
    private bool DeletePageTemplateScope()
    {
        string columns = "";
        string where = "";
        
        // Get template object
        PageTemplateInfo template = PageTemplateInfoProvider.GetPageTemplateInfo("MyNewTemplate");

        // If template exists
        if (template != null)
        {
            where = "PageTemplateScopeTemplateID = " + template.PageTemplateId;
        }
        
        
        DataSet scopes = PageTemplateScopeInfoProvider.GetTemplateScopes(columns, where, null, 0);

        if (!DataHelper.DataSourceIsEmpty(scopes))
        {
            // Get the page template scope
            PageTemplateScopeInfo deleteScope = new PageTemplateScopeInfo(scopes.Tables[0].Rows[0]);

            // Delete the page template scope
            PageTemplateScopeInfoProvider.DeletePageTemplateScopeInfo(deleteScope);

            return true;
        }
        return false;
    }

    #endregion
}
