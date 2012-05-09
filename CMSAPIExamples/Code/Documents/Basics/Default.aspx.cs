using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;

[Title(Text = "Content management - Basics", ImageUrl = "Objects/CMS_Document/object.png")]
public partial class CMSAPIExamples_Code_Documents_Basics_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Creating documents
        this.apiCreateDocumentStructure.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDocumentStructure);
        this.apiCreateDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDocument);
        this.apiCreateNewCultureVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateNewCultureVersion);
        this.apiCreateLinkedDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateLinkedDocument);

        // Managing documents
        this.apiGetAndUpdateDocuments.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateDocuments);
        this.apiCopyDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CopyDocument);
        this.apiMoveDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(MoveDocument);
        
        // Deleting documents
        this.apiDeleteLinkedDocuments.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteLinkedDocuments);
        this.apiDeleteCultureVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteCultureVersion);
        this.apiDeleteDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteDocument);
        this.apiDeleteDocumentStructure.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteDocumentStructure);
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
        this.apiCreateDocumentStructure.Run();
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

        // Deleting documents
        this.apiDeleteLinkedDocuments.Run();
        this.apiDeleteCultureVersion.Run();
        this.apiDeleteDocument.Run();
        this.apiDeleteDocumentStructure.Run();

    }

    #endregion


    #region "API examples - Creating documents"

    /// <summary>
    /// Creates the initial document strucutre used for the example. Called when the "Create document structure" button is pressed.
    /// </summary>
    private bool CreateDocumentStructure()
    {
        // Add a new culture to the current site
        CultureInfo culture = CultureInfoProvider.GetCultureInfo("de-de");
        CultureSiteInfoProvider.AddCultureToSite(culture.CultureID, CMSContext.CurrentSiteID);
        
        // Create new instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get parent node
        TreeNode parentNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", "en-us");

        if (parentNode != null)
        {
            // Create the API Example folder
            TreeNode newNode = TreeNode.New("CMS.Folder", tree);

            newNode.DocumentName = "API Example";
            newNode.DocumentCulture = "en-us";

            newNode.Insert(parentNode);
            
            parentNode = newNode;

            // Create the Source folder - the document to be moved will be stored here
            newNode = TreeNode.New("CMS.Folder", tree);

            newNode.DocumentName = "Source";
            newNode.DocumentCulture = "en-us";

            newNode.Insert(parentNode);

            // Create the Target folder - a document will be moved here
            newNode = TreeNode.New("CMS.Folder", tree);

            newNode.DocumentName = "Target";
            newNode.DocumentCulture = "en-us";

            newNode.Insert(parentNode);

            return true;
        }
        
        return false;
    }

    /// <summary>
    /// Creates a document under the API Example folder. Called when the "Create document" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool CreateDocument()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the parent node - the API Example folder
        TreeNode parentNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", "en-us");

        if (parentNode != null)
        {
            // Create a new instance of the Tree node
            TreeNode newNode = TreeNode.New("CMS.MenuItem", tree);

            // Set the document's properties
            newNode.DocumentName = "My new document";
            newNode.DocumentCulture = "en-us";

            // Insert the document into the content tree
            newNode.Insert(parentNode);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Creates a new culture version of a document. Called when the "Create new culture version" button is pressed.
    /// Expects the "CreateDocumentStructure" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool CreateNewCultureVersion()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the document in the english culture
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/My-new-document", "en-us");

        if (node != null)
        {
            // Translate its name
            node.DocumentName = "My new translation";
            node.SetValue("MenuItemName", "My new translation");

            node.DocumentCulture = "de-de";

            // Insert into database
            node.InsertAsNewCultureVersion();

            return true;
        }

        return false;
    }

    /// <summary>
    /// Creates a linked document. Called when the "Create linked document" button is pressed.
    /// Expects the "CreateDocumentStructure" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool CreateLinkedDocument()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the parent document
        TreeNode parentNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", "en-us");

        if (parentNode != null)
        {
            // Get the original document
            TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/My-new-document", "en-us");

            if (node != null )
            {
                // Insert the link
                node.InsertAsLink(parentNode);

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
    /// Gets menu items under the API Example folder and updates them. Called when the "Get and update documents" button is pressed.
    /// Expects the "CreateDocumentStructure" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool GetAndUpdateDocuments()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Fill dataset with documents
        DataSet documents = tree.SelectNodes(CMSContext.CurrentSiteName, "/API-Example/%", "en-us", false, "CMS.MenuItem");

        if (!DataHelper.DataSourceIsEmpty(documents))
        {
            // Loop through all documents
            foreach (DataRow documentRow in documents.Tables[0].Rows)
            {
                // Create a new Tree node from the data row
                TreeNode editDocument = TreeNode.New(documentRow, "CMS.MenuItem", tree);

                string newName = editDocument.DocumentName.ToLower();

                // Change coupled data
                editDocument.SetValue("MenuItemName", newName);
                // Change document data
                editDocument.DocumentName = newName;

                // Save to database
                editDocument.Update();               
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Copies a document to the prepared Source folder. Called when the "Copy document" button is pressed.
    /// Expects the "CreateDocumentStructure" and "CreateDocument" method to be run first.
    /// </summary>
    private bool CopyDocument()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/My-new-document", "en-us");

        // Get the new parent document
        TreeNode parentNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/Source", "en-us");

        if ((node != null) && (parentNode != null))
        {
            // Copy the document
            tree.CopyNode(node, parentNode.NodeID);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Moves a document to the prepared Target folder. Called when the "Move document" button is pressed.
    /// Expects the "CreateDocumentStructure", "CreateDocument" and "CopyDocument" methods to be run first.
    /// </summary>
    private bool MoveDocument()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/Source/My-new-document", "en-us");

        // Get the new parent document
        TreeNode parentNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/Target", "en-us");

        if ((node != null) && (parentNode != null))
        {
            // Move the document
            tree.MoveNode(node, parentNode.NodeID);

            return true;
        }

        return false;
    }

    #endregion


    #region "API Example - Deleting documents"

    /// <summary>
    /// Deletes the linked document. Called when the "Delete linked document" button is pressed.
    /// Expects the "CreateDocumentStructure", "CreateDocument" and "CreateLinkedDocument" methods to be run first.
    /// </summary>
    private bool DeleteLinkedDocuments()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        string siteName = CMSContext.CurrentSiteName;

        // Get the document
        TreeNode node = tree.SelectSingleNode(siteName, "/API-Example/My-new-document", "en-us");

        if (node != null)
        {
            // Prepare the where condition
            string where = "NodeLinkedNodeID = " + node.NodeID;

            // Get linked documents' IDs
            DataSet documents = tree.SelectNodes(siteName, "/API-Example/%", "en-us", false, null, where, null, -1, false, -1, "NodeID");

            if (!DataHelper.DataSourceIsEmpty(documents))
            {
                // Loop through the documents and delete them. Alternatively, the DeleteLinks method from the TreeProvider can be used to delete all document's links.
                foreach (DataRow documentRow in documents.Tables[0].Rows)
                {
                    // Get additional document data
                    TreeNode document = tree.SelectSingleNode(ValidationHelper.GetInteger(documentRow["NodeID"], 0));

                    // Delete the document
                    document.Delete();
                }

                return true;
            }
            else
            {
                apiDeleteLinkedDocuments.ErrorMessage = "No linked documents were found.";
            }
        }

        return false;
    }

    /// <summary>
    /// Deletes the culture version of the document. Called when the "Delete culture version" button is pressed.
    /// Expects the "CreateDocumentStructure", "CreateDocument" and "CreateNewCultureVersion" methods to be run first.
    /// </summary>
    private bool DeleteCultureVersion()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the german culture version of the document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/My-new-document", "de-de");

        if (node != null)
        {
            // Delete the document
            node.Delete();

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes all remaining culture versions of the document. Called when the "Delete document" button is pressed.
    /// Expects the "CreateDocumentStructure" and "CreateDocument" methods to be run first.
    /// </summary>
    private bool DeleteDocument()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/My-new-document", "en-us");

        if (node != null)
        {
            // Delete the document and all its culture versions
            node.DeleteAllCultures();

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes the example document structure. Called when the "Delete document structure" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool DeleteDocumentStructure()
    {
        // Create an instance of the Tree provider first
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the API Example folder
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", "en-us");

        if (node != null)
        {
            // Delete the folder and all child documents
            node.DeleteAllCultures();
        }

        CultureInfo culture = CultureInfoProvider.GetCultureInfo("de-de");

        // Remove the example culture from the site
        CultureSiteInfoProvider.RemoveCultureFromSite(culture.CultureID, CMSContext.CurrentSiteID);

        return true;
    }

    #endregion
}