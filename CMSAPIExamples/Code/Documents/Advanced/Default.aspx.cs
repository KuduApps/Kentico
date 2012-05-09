using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

[Title(Text = "Content management - Advanced", ImageUrl = "Objects/CMS_Document/object.png")]
public partial class CMSAPIExamples_Code_Documents_Advanced_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Preparatioin
        this.apiCreateDocumentStructure.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDocumentStructure);
        
        // Organizing documents
        this.apiMoveDocumentUp.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(MoveDocumentUp);
        this.apiMoveDocumentDown.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(MoveDocumentDown);
        this.apiSortDocumentsAlphabetically.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SortDocumentsAlphabetically);
        this.apiSortDocumentsByDate.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SortDocumentsByDate);

        // Recycle bin
        this.apiMoveToRecycleBin.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(MoveToRecycleBin);
        this.apiRestoreFromRecycleBin.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RestoreFromRecycleBin);

        // Cleanup
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

        // Preparation
        this.apiCreateDocumentStructure.Run();

        // Organizing documents
        this.apiMoveDocumentUp.Run();
        this.apiMoveDocumentDown.Run();
        this.apiSortDocumentsAlphabetically.Run();
        this.apiSortDocumentsByDate.Run();

        // Recycle bin
        this.apiMoveToRecycleBin.Run();
        this.apiRestoreFromRecycleBin.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Delete document structure
        this.apiDeleteDocumentStructure.Run();

    }

    #endregion


    #region "API Example - Preparation"

    /// <summary>
    /// Prepares the example document structure. Called when the "Prepare documents" button is pressed.
    /// </summary>
    private bool CreateDocumentStructure()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // First get the root node
        TreeNode parentNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", "en-us");

        if (parentNode != null)
        {
            // First create a folder
            TreeNode node = TreeNode.New("CMS.Folder", tree);

            node.DocumentName = "API Example";
            node.DocumentCulture = "en-us";

            node.Insert(parentNode);

            parentNode = node;

            // Create a few documents
            for (int i = 1; i <= 3; i++)
            {
                node = TreeNode.New("CMS.MenuItem", tree);

                node.DocumentName = "Document " + i;
                node.DocumentCulture = "en-us";

                node.Insert(parentNode);
            }

            return true;
        }

        return false;
    }

    #endregion


    #region "API Example - Organizing documents"

    /// <summary>
    /// Moves a document up in the content tree. Called when the "Move document up" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool MoveDocumentUp()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Select a node
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/Document-2", "en-us");

        if (node != null)
        {
            // Move the node up
            tree.MoveNodeUp(node.NodeID);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Moves a document down in the content tree. Called when the "Move document down" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    /// <returns></returns>
    private bool MoveDocumentDown()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Select a node
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/Document-1", "en-us");

        if (node != null)
        {
            // Move the node up
            tree.MoveNodeDown(node.NodeID);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Sorts a content tree subsection by document name from A to Z. Called when the "Sort documents alphabetically" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool SortDocumentsAlphabetically()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Select a node
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", "en-us");

        if (node != null)
        {
            // Sort its child nodes alphabetically - ascending
            tree.OrderNodesAlphabetically(node.NodeID, true);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Sorts a content tree subsection by date from oldest to newest. Called when the "Sort documents by date" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool SortDocumentsByDate()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Select a node
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", "en-us");

        if (node != null)
        {
            // Sort its child nodes by date - descending
            tree.OrderNodesByDate(node.NodeID, false);

            return true;
        }

        return false;
    }

    #endregion


    #region "API Example - Recycle bin"

    /// <summary>
    /// Deletes a document to the recycle bin. Called when the "Move document to recycle bin" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool MoveToRecycleBin()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/Document-1", "en-us");

        if (node != null)
        {
            // Delete the document without destroying its history
            DocumentHelper.DeleteDocument(node, tree, true, false, true);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Restores the document from the recycle bin. Called when the "Restore document" button is pressed.
    /// Expects the "CreateDocumentStructure" and "MoveDocumentToRecycleBin" methods to be run first.
    /// </summary>
    private bool RestoreFromRecycleBin()
    {
        // Prepare the where condition
        string where = "VersionNodeAliasPath LIKE N'/API-Example/Document-1'";

        // Get the recycled document
        DataSet recycleBin = VersionHistoryInfoProvider.GetRecycleBin(CMSContext.CurrentSiteID, where, null, 0, "VersionHistoryID");

        if (!DataHelper.DataSourceIsEmpty(recycleBin))
        {
            // Create a new version history info object from the data row
            VersionHistoryInfo version = new VersionHistoryInfo(recycleBin.Tables[0].Rows[0]);

            // Create a new version manager instance and restore the document
            VersionManager manager = new VersionManager(new TreeProvider(CMSContext.CurrentUser));
            manager.RestoreDocument(version.VersionHistoryID);

            return true;
        }

        return false;
    }

    #endregion


    #region "API Example - Cleanup"

    /// <summary>
    /// Deletes the example document structure. Called when the "Delete document structure" button is pressed.
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>    
    private bool DeleteDocumentStructure()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the API Example folder
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", "en-us");

        // Delete the folder including all dependencies and child documents
        node.Delete();

        return true;
    }

    #endregion
}