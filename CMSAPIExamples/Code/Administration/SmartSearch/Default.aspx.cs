using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using System.Configuration;

[Title(Text = "Smart search", ImageUrl = "Objects/CMS_SearchIndex/object.png")]
public partial class CMSAPIExamples_Code_Administration_SmartSearch_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Search index
        this.apiCreateSearchIndex.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateSearchIndex);
        this.apiGetAndUpdateSearchIndex.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateSearchIndex);
        this.apiGetAndBulkUpdateSearchIndexes.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateSearchIndexes);
        this.apiDeleteSearchIndex.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteSearchIndex);
        this.apiCreateIndexSettings.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateIndexSettings);

        // Search index on site
        this.apiAddSearchIndexToSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddSearchIndexToSite);
        this.apiRemoveSearchIndexFromSite.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveSearchIndexFromSite);
        
        // Culture on search index
        this.apiAddCultureToSearchIndex.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(AddCultureToSearchIndex);
        this.apiRemoveCultureFromSearchIndex.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RemoveCultureFromSearchIndex);

        // Search actions
        this.apiRebuildIndex.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RebuildIndex);
        this.apiSearchText.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SearchText);
        this.apiUpdateIndex.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(UpdateIndex);
    }

    
    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Search index
        this.apiCreateSearchIndex.Run();
        this.apiGetAndUpdateSearchIndex.Run();
        this.apiGetAndBulkUpdateSearchIndexes.Run();
        this.apiCreateIndexSettings.Run();

        // Search index on site
        this.apiAddSearchIndexToSite.Run();

        // Culture on search index
        this.apiAddCultureToSearchIndex.Run();

        // Search actions
        this.apiRebuildIndex.Run();
        this.apiSearchText.Run();
        this.apiUpdateIndex.Run();


    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Culture on search index\
        this.apiRemoveCultureFromSearchIndex.Run();

        // Search index on site
        this.apiRemoveSearchIndexFromSite.Run();

        // Search index
        this.apiDeleteSearchIndex.Run();
    }

    #endregion


    #region "API examples - Search index"

    /// <summary>
    /// Creates search index. Called when the "Create index" button is pressed.
    /// </summary>
    private bool CreateSearchIndex()
    {
        // Create new search index object
        SearchIndexInfo newIndex = new SearchIndexInfo();

        // Set the properties
        newIndex.IndexDisplayName = "My new index";
        newIndex.IndexName = "MyNewIndex";
        newIndex.IndexIsCommunityGroup = false;
        newIndex.IndexType = PredefinedObjectType.DOCUMENT;
        newIndex.IndexAnalyzerType = AnalyzerTypeEnum.StandardAnalyzer;
        newIndex.StopWordsFile = "";
        
        // Save the search index
        SearchIndexInfoProvider.SetSearchIndexInfo(newIndex);

        return true;
    }


    /// <summary>
    /// Gets and updates search index. Called when the "Get and update index" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool GetAndUpdateSearchIndex()
    {
        // Get the search index
        SearchIndexInfo updateIndex = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");
        if (updateIndex != null)
        {
            // Update the properties
            updateIndex.IndexDisplayName = updateIndex.IndexDisplayName.ToLower();

            // Save the changes
            SearchIndexInfoProvider.SetSearchIndexInfo(updateIndex);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates search indexes. Called when the "Get and bulk update indexes" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateSearchIndexes()
    {
        // Prepare the parameters
        string where = "IndexName LIKE N'MyNewIndex%'";

        // Get the data
        DataSet indexes = SearchIndexInfoProvider.GetSearchIndexes(where, null);
        if (!DataHelper.DataSourceIsEmpty(indexes))
        {
            // Loop through the individual items
            foreach (DataRow indexDr in indexes.Tables[0].Rows)
            {
                // Create object from DataRow
                SearchIndexInfo modifyIndex = new SearchIndexInfo(indexDr);

                // Update the properties
                modifyIndex.IndexDisplayName = modifyIndex.IndexDisplayName.ToUpper();

                // Save the changes
                SearchIndexInfoProvider.SetSearchIndexInfo(modifyIndex);
            }

            return true;
        }

        return false;
    }



    /// <summary>
    /// Creates index settings. Called when the "Create index settings" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool CreateIndexSettings()
    {
        // Get the search index
        SearchIndexInfo index = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");
        if (index != null)
        {
            // Create new index settings
            SearchIndexSettingsInfo indexSettings = new SearchIndexSettingsInfo();
            // Set setting properties
            indexSettings.IncludeBlogs = true;
            indexSettings.IncludeForums = true;
            indexSettings.IncludeMessageCommunication = true;
            indexSettings.ClassNames = ""; // for all document types
            indexSettings.Path = "/%";
            indexSettings.Type = SearchIndexSettingsInfo.TYPE_ALLOWED;
            indexSettings.ID = Guid.NewGuid();

            // Save index settings                     
            SearchIndexSettings settings = new SearchIndexSettings();
            settings.SetSearchIndexSettingsInfo(indexSettings);
            index.IndexSettings = settings;

            // Save to database
            SearchIndexInfoProvider.SetSearchIndexInfo(index);

            return true;
        }
        return false;
    }


    /// <summary>
    /// Deletes search index. Called when the "Delete index" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool DeleteSearchIndex()
    {
        // Get the search index
        SearchIndexInfo deleteIndex = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");

        // Delete the search index
        SearchIndexInfoProvider.DeleteSearchIndexInfo(deleteIndex);

        return (deleteIndex != null);
    }

    #endregion


    #region "API examples - Search index on site"

    /// <summary>
    /// Adds search index to site. Called when the "Add index to site" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool AddSearchIndexToSite()
    {
        // Get the search index
        SearchIndexInfo index = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");
        if (index != null)
        {
            int indexId = index.IndexID;
            int siteId = CMSContext.CurrentSiteID;

            // Save the binding
            SearchIndexSiteInfoProvider.AddSearchIndexToSite(indexId, siteId);

            return true;
        }

        return false;
    }
    
    
    /// <summary>
    /// Removes search index from site. Called when the "Remove index from site" button is pressed.
    /// Expects the AddSearchIndexToSite method to be run first.
    /// </summary>
    private bool RemoveSearchIndexFromSite()
    {
        // Get the search index
        SearchIndexInfo removeIndex = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");
        if (removeIndex != null)
        {
            int siteId = CMSContext.CurrentSiteID;

            // Get the binding
            SearchIndexSiteInfo indexSite = SearchIndexSiteInfoProvider.GetSearchIndexSiteInfo(removeIndex.IndexID, siteId);

            // Delete the binding
            SearchIndexSiteInfoProvider.DeleteSearchIndexSiteInfo(indexSite);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Search index on culture"

    /// <summary>
    /// Adds culture to search index. Called when the "Add culture to index" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool AddCultureToSearchIndex()
    {
        // Get the search index and culture
        SearchIndexInfo index = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");
        CultureInfo culture = CultureInfoProvider.GetCultureInfo("en-us");

        if ((index != null) && (culture != null))
        {
            // Save the binding
            SearchIndexCultureInfoProvider.AddSearchIndexCulture(index.IndexID, culture.CultureID);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Removes culture from search index. Called when the "Remove culture from index" button is pressed.
    /// Expects the AddCultureToSearchIndex method to be run first.
    /// </summary>
    private bool RemoveCultureFromSearchIndex()
    {
        // Get the search index
        SearchIndexInfo removeIndex = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");
        CultureInfo culture = CultureInfoProvider.GetCultureInfo("en-us");

        if ((removeIndex != null) && (culture != null))
        {
            // Get the binding
            SearchIndexCultureInfo indexCulture = SearchIndexCultureInfoProvider.GetSearchIndexCultureInfo(removeIndex.IndexID, culture.CultureID);

            // Delete the binding
            SearchIndexCultureInfoProvider.DeleteSearchIndexCultureInfo(indexCulture);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Search actions"

    /// <summary>
    /// Rebuilds the search index. Called when the "Rebuild index" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool RebuildIndex()
    {
        // Get the search index
        SearchIndexInfo index = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");

        if (index != null)
        {
            // Create rebuild task 
            SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Rebuild, index.IndexType, null, index.IndexName);

            return true;
        }
        return false;
    }


    /// <summary>
    /// Searchs text. Called when the "Search text" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool SearchText()
    {
        // Get the search index
        SearchIndexInfo index = SearchIndexInfoProvider.GetSearchIndexInfo("MyNewIndex");

        int numberOfResults = 0;

        if (index != null)
        {
            // Set the properties
            string searchText = "home";
            string path = "/%";
            string classNames = "";
            string cultureCode = "EN-US";
            string defaultCulture = CultureHelper.DefaultCulture.IetfLanguageTag;
            Lucene.Net.Search.Sort sort = SearchHelper.GetSort("##SCORE##");
            bool combineWithDefaultCulture = false;
            bool checkPermissions = false;
            bool searchInAttachments = false;
            string searchIndexes = index.IndexName;
            int displayResults = 100;
            int startingPosition = 0;
            int numberOfProcessedResults = 100;
            UserInfo userInfo = CMSContext.CurrentUser;
            string attachmentWhere = "";
            string attachmentOrderBy = "";

            // Get search results
            DataSet ds = SearchHelper.Search(searchText, sort, path, classNames, cultureCode, defaultCulture, combineWithDefaultCulture, checkPermissions, searchInAttachments, searchIndexes, displayResults, startingPosition, numberOfProcessedResults, userInfo, out numberOfResults, attachmentWhere, attachmentOrderBy);

            // If found at least one item
            if (numberOfResults > 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Adds search index to site. Called when the "Add index to site" button is pressed.
    /// Expects the CreateSearchIndex method to be run first.
    /// </summary>
    private bool UpdateIndex()
    {
        // Tree provider
        TreeProvider provider = new CMS.TreeEngine.TreeProvider(CMS.CMSHelper.CMSContext.CurrentUser);
        // Get document of specified site, aliaspath and culture
        TreeNode node = provider.SelectSingleNode(CMS.CMSHelper.CMSContext.CurrentSiteName, "/", "en-us");

        // If node exists
        if ((node != null) && (node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
        {
            // Edit and save document node
            node.NodeDocType += " changed";
            node.Update();

            // Create update task
            SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());

            return true;
        }
        return false;
    }

    
    #endregion
}
