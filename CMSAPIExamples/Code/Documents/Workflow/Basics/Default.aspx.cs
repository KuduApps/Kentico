using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

[Title(Text = "Workflow Basics", ImageUrl = "Objects/CMS_Workflow/object.png")]
public partial class CMSAPIExamples_Code_Documents_Workflow_Basics_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Creating documents
        this.apiCreateExampleObjects.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateExampleObjects);
        this.apiCreateDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDocument);
        this.apiCreateNewCultureVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateNewCultureVersion);
        this.apiCreateLinkedDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateLinkedDocument);

        // Managing documents
        this.apiGetAndUpdateDocuments.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateDocuments);
        this.apiCopyDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CopyDocument);
        this.apiMoveDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(MoveDocument);

        // Cleanup
        this.apiDeleteDocuments.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteDocuments);
        this.apiDeleteObjects.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteObjects);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Creating documents
        this.apiCreateExampleObjects.Run();
        this.apiCreateDocument.Run();
        this.apiCreateNewCultureVersion.Run();
        this.apiCreateLinkedDocument.Run();

        // Managing documents
        this.apiGetAndUpdateDocuments.Run();
        this.apiCopyDocument.Run();
        this.apiMoveDocument.Run();

    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        this.apiDeleteDocuments.Run();
        this.apiDeleteObjects.Run();
    }

    #endregion


    #region "API examples - Creating documents"

    /// <summary>
    /// Assigns another culture to the current site, then creates the document structure and workflow scope needed for this example. Called when the "Create example objects" button is pressed.
    /// </summary>
    private bool CreateExampleObjects()
    {
        // Add a new culture to the current site
        CultureInfo culture = CultureInfoProvider.GetCultureInfo("de-de");
        CultureSiteInfoProvider.AddCultureToSite(culture.CultureID, CMSContext.CurrentSiteID);

        // Create a new tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root node
        TreeNode parent = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", "en-us");

        if (parent != null)
        {
            // Create the API example folder
            TreeNode node = TreeNode.New("CMS.Folder", tree);

            node.DocumentName = "API Example";
            node.DocumentCulture = "en-us";

            // Insert it to database
            DocumentHelper.InsertDocument(node, parent, tree);

            parent = node;

            // Create the Source folder for moving
            node = TreeNode.New("CMS.Folder", tree);

            node.DocumentName = "Source";
            node.DocumentCulture = "en-us";

            DocumentHelper.InsertDocument(node, parent, tree);

            // Create the Target folder for moving
            node = TreeNode.New("CMS.Folder", tree);

            node.DocumentName = "Target";
            node.DocumentCulture = "en-us";

            DocumentHelper.InsertDocument(node, parent, tree);

            // Get the default workflow
            WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("default");

            if (workflow != null)
            {
                // Get the example folder data
                node = DocumentHelper.GetDocument(parent, tree);

                // Create new workflow scope
                WorkflowScopeInfo scope = new WorkflowScopeInfo();

                // Assign to the default workflow and current site and set starting alias path to the example document
                scope.ScopeWorkflowID = workflow.WorkflowID;
                scope.ScopeStartingPath = node.NodeAliasPath;
                scope.ScopeSiteID = CMSContext.CurrentSiteID;

                // Save the scope into the database
                WorkflowScopeInfoProvider.SetWorkflowScopeInfo(scope);

                return true;
            }
            else
            {
                apiCreateExampleObjects.ErrorMessage = "The default workflow was not found.";
            }
        }

        return false;
    }

    /// <summary>
    /// Creates a document under workflow. Called when the "Create document" button is pressed.
    /// Expects the "CreateExampleObjects" method to be run first.
    /// </summary>
    private bool CreateDocument()
    {
        // Create new tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Prepare parameters
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/API-Example";
        string culture = "en-us";
        bool combineWithDefaultCulture = false;
        string classNames = TreeProvider.ALL_CLASSNAMES;
        string where = null;
        string orderBy = null;
        int maxRelativeLevel = -1;
        bool selectOnlyPublished = false;
        string columns = null;

        // Get the example folder
        TreeNode parentNode = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (parentNode != null)
        {
            // Create a new node
            TreeNode node = TreeNode.New("CMS.MenuItem", tree);

            // Set the required document properties
            node.DocumentName = "My new document";
            node.DocumentCulture = "en-us";

            // Insert the document
            DocumentHelper.InsertDocument(node, parentNode, tree);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Creates a new culture version of the document under workflow. Called when the "Create new culture version" button is pressed.
    /// Expects the "CreateExampleObjects" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool CreateNewCultureVersion()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Prepare parameters
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/API-Example/My-new-document";
        string culture = "en-us";
        bool combineWithDefaultCulture = false;
        string classNames = TreeProvider.ALL_CLASSNAMES;
        string where = null;
        string orderBy = null;
        int maxRelativeLevel = -1;
        bool selectOnlyPublished = false;
        string columns = null;

        // Get the document in the English culture
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            // Translate its name
            node.DocumentName = "My new translation";
            node.SetValue("MenuItemName", "My new translation");

            node.DocumentCulture = "de-de";

            // Insert into database
            DocumentHelper.InsertNewCultureVersion(node, tree);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Creates a link to the document under workflow. Called when the "Create linked document" button is pressed.
    /// Expects the "CreateExampleObjects" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool CreateLinkedDocument()
    {
        // Create new tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Prepare parameters
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/API-Example";
        string culture = "en-us";
        bool combineWithDefaultCulture = false;
        string classNames = TreeProvider.ALL_CLASSNAMES;
        string where = null;
        string orderBy = null;
        int maxRelativeLevel = -1;
        bool selectOnlyPublished = false;
        string columns = null;

        // Get the example folder
        TreeNode parentNode = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (parentNode != null)
        {
            // Change the alias path
            aliasPath += "/My-new-document";

            // Get the original document
            TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

            if (node != null)
            {
                // Insert the link
                DocumentHelper.InsertDocumentAsLink(node, parentNode.NodeID, tree);

                return true;
            }
            else
            {
                apiCreateLinkedDocument.ErrorMessage = "Document 'My new document' was not found.";
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Managing documents"

    /// <summary>
    /// Gets a dataset of documents in the example section and updates them. Called when the "Get and update documents" button is pressed.
    /// Expects the "CreateExampleObjects" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool GetAndUpdateDocuments()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Prepare parameters
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/API-Example/%";
        string culture = "en-us";
        bool combineWithDefaultCulture = false;
        string classNames = "CMS.MenuItem";
        string where = null;
        string orderBy = null;
        int maxRelativeLevel = -1;
        bool selectOnlyPublished = false;

        // Fill dataset with documents
        DataSet documents = DocumentHelper.GetDocuments(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, tree);

        if (!DataHelper.DataSourceIsEmpty(documents))
        {
            // Create a new Version manager instance
            VersionManager manager = new VersionManager(tree);

            // Loop through all documents
            foreach (DataRow documentRow in documents.Tables[0].Rows)
            {
                // Create a new Tree node from the data row
                TreeNode editDocument = TreeNode.New(documentRow, "CMS.MenuItem", tree);

                // Check out the document
                manager.CheckOut(editDocument);

                string newName = editDocument.DocumentName.ToLower();

                // Change document data
                editDocument.DocumentName = newName;

                // Change coupled data
                editDocument.SetValue("MenuItemName", newName);

                // Save the changes
                DocumentHelper.UpdateDocument(editDocument, tree);

                // Check in the document
                manager.CheckIn(editDocument, null, null);
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Copies the document under workflow to a different section. Called when the "Copy document" button is pressd.
    /// Expects the "CreateExampleObjects" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool CopyDocument()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Prepare parameters
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/API-Example/My-new-document";
        string culture = "en-us";
        bool combineWithDefaultCulture = false;
        string classNames = TreeProvider.ALL_CLASSNAMES;
        string where = null;
        string orderBy = null;
        int maxRelativeLevel = -1;
        bool selectOnlyPublished = false;
        string columns = null;

        // Get the example folder
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        aliasPath = "/API-Example/Source";

        // Get the new parent document
        TreeNode parentNode = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if ((node != null) && (parentNode != null))
        {
            // Copy the document
            DocumentHelper.CopyDocument(node, parentNode.NodeID, false, tree);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Moves the document under workflow to a different section. Called when the "Move document" button is pressed.
    /// Expects the "CreateExampleObjects", "CreateDocument" and "CopyDocument" methods to be run first.
    /// </summary>
    private bool MoveDocument()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Prepare parameters
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/API-Example/My-new-document";
        string culture = "en-us";
        bool combineWithDefaultCulture = false;
        string classNames = TreeProvider.ALL_CLASSNAMES;
        string where = null;
        string orderBy = null;
        int maxRelativeLevel = -1;
        bool selectOnlyPublished = false;
        string columns = null;

        // Get the example folder
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        aliasPath = "/API-Example/Target";

        // Get the new parent document
        TreeNode parentNode = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if ((node != null) && (parentNode != null))
        {
            // Move the document
            DocumentHelper.MoveDocument(node, parentNode.NodeID, tree);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Cleanup"

    /// <summary>
    /// Deletes the document structure used for this example. Called when the "Delete documents" button is pressed.
    /// Expects the "CreateExampleObjects" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool DeleteDocuments()
    {
        // Create new tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Prepare parameters
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/API-Example";
        string culture = "en-us";
        bool combineWithDefaultCulture = false;
        string classNames = TreeProvider.ALL_CLASSNAMES;
        string where = null;
        string orderBy = null;
        int maxRelativeLevel = -1;
        bool selectOnlyPublished = false;
        string columns = null;

        // Get the example folder
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            // Prepare delete parameters
            bool deleteAllCultures = true;
            bool destroyHistory = true;
            bool deleteProduct = false;

            // Delete all culture versions of the document and destroy its version history. This method also destroys all child documents.
            DocumentHelper.DeleteDocument(node, tree, deleteAllCultures, destroyHistory, deleteProduct);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Deletes the workflow scope(s) and culture assignments used for this example. Called when the "Delete objects" button is pressed.
    /// Expects the "CreateExampleObjects" method to be run first.
    /// </summary>
    private bool DeleteObjects()
    {

        CultureInfo culture = CultureInfoProvider.GetCultureInfo("de-de");

        // Remove the example culture from the site
        CultureSiteInfoProvider.RemoveCultureFromSite(culture.CultureID, CMSContext.CurrentSiteID);

        // Prepare parameters
        string where = "ScopeStartingPath LIKE '/API-Example%'";
        string orderBy = null;
        int topN = 0;
        string columns = null;

        DataSet scopes = WorkflowScopeInfoProvider.GetWorkflowScopes(where, orderBy, topN, columns);

        if (!DataHelper.DataSourceIsEmpty(scopes))
        {
            // Loop through all the scopes in case more identical scopes were accidentally created
            foreach (DataRow scopeRow in scopes.Tables[0].Rows)
            {
                // Create scope info object
                WorkflowScopeInfo scope = new WorkflowScopeInfo(scopeRow);

                // Delete the scope
                scope.Delete();

            }

            return true;
        }

        return false;
    }

    #endregion
}