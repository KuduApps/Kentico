using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

[Title(Text = "Workflow advanced", ImageUrl = "Objects/CMS_Workflow/object.png")]
public partial class CMSAPIExamples_Code_Documents_Workflow_Advanced_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Creating documents
        this.apiCreateExampleObjects.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateExampleObjects);

        // Editing documents
        this.apiCheckOut.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CheckOut);
        this.apiEditDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(EditDocument);
        this.apiCheckIn.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CheckIn);
        this.apiUndoCheckout.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(UndoCheckout);

        // Workflow process
        this.apiMoveToNextStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(MoveToNextStep);
        this.apiMoveToPreviousStep.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(MoveToPreviousStep);
        this.apiArchiveDocument.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(ArchiveDocument);

        // Versioning
        this.apiRollbackVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RollbackVersion);
        this.apiDeleteVersion.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteVersion);
        this.apiDestroyHistory.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DestroyHistory);

        // Cleanup
        this.apiDeleteExampleObjects.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteExampleObjects);
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
        this.apiCreateExampleObjects.Run();

        // Editing documents
        this.apiCheckOut.Run();
        this.apiEditDocument.Run();
        this.apiCheckIn.Run();

        // Workflow process
        this.apiMoveToNextStep.Run();
        this.apiMoveToPreviousStep.Run();
        this.apiDestroyHistory.Run();

        // Versioning
        this.apiRollbackVersion.Run();
        this.apiDeleteVersion.Run();
        this.apiDestroyHistory.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        this.apiDeleteExampleObjects.Run();
    }

    #endregion


    #region "API examples - Preparation"

    /// <summary>
    ///Creates the document, workflow scope and step needed for this example. Called when the "Create example objects" button is pressed.
    /// </summary>
    private bool CreateExampleObjects()
    {
        // Create a new tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the root node
        TreeNode parent = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", "en-us");

        if (parent != null)
        {
            // Create the API document
            TreeNode node = TreeNode.New("CMS.MenuItem", tree);

            node.DocumentName = "API Example";
            node.DocumentCulture = "en-us";

            // Insert it to database
            DocumentHelper.InsertDocument(node, parent, tree);

            // Get the default workflow
            WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("default");

            if (workflow != null)
            {
                // Get the document data
                node = DocumentHelper.GetDocument(node, tree);

                // Create new workflow scope
                WorkflowScopeInfo scope = new WorkflowScopeInfo();

                // Assign to the default workflow and current site and set starting alias path to the example document
                scope.ScopeWorkflowID = workflow.WorkflowID;
                scope.ScopeStartingPath = node.NodeAliasPath;
                scope.ScopeSiteID = CMSContext.CurrentSiteID;

                // Save the scope into the database
                WorkflowScopeInfoProvider.SetWorkflowScopeInfo(scope);

                // Create a new workflow step
                WorkflowStepInfo step = new WorkflowStepInfo();

                // Set its properties
                step.StepWorkflowID = workflow.WorkflowID;
                step.StepName = "MyNewWorkflowStep";
                step.StepDisplayName = "My new workflow step";
                step.StepOrder = 1;

                // Save the workflow step
                WorkflowStepInfoProvider.SetWorkflowStepInfo(step);

                // Ensure correct step order
                WorkflowStepInfoProvider.InitStepOrders(step.StepWorkflowID);

                return true;
            }
            else
            {
                apiCreateExampleObjects.ErrorMessage = "The default workflow was not found.";
            }
        }

        return false;
    }


    #endregion


    #region "API examples - Managing documents"

    /// <summary>
    /// Checks out the document. If check-in/check-out is disabled for the default workflow, the example can't be used. Called when the "Check out document" button is pressed.
    /// Expects the "CreateExampleObjects" method to be run first.
    /// </summary>
    private bool CheckOut()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            // Create a new Workflow manager instance
            WorkflowManager workflowmanager = new WorkflowManager(tree);

            // Make sure the document uses workflow
            WorkflowInfo workflow = workflowmanager.GetNodeWorkflow(node);

            if (workflow != null)
            {
                // Check if the workflow uses check-in/check-out functionality
                if (workflow.UseCheckInCheckOut(CMSContext.CurrentSiteName))
                {
                    // The document has to be checked in
                    if (!node.IsCheckedOut)
                    {
                        // Create a new version manager instance
                        VersionManager versionmanager = new VersionManager(tree);

                        // Check out the document to create a new document version
                        versionmanager.CheckOut(node);

                        return true;
                    }
                    else
                    {
                        apiCheckOut.ErrorMessage = "The document has already been checked out.";
                    }
                }
                else
                {
                    apiCheckOut.ErrorMessage = "The workflow does not use check-in/check-out. See the \"Edit document\" example, which checks the document out and in automatically.";
                }
            }
            else
            {
                apiCheckOut.ErrorMessage = "The document doesn't use workflow.";
            }
        }

        return false;
    }

    /// <summary>
    /// If the document hasn't been checked out, creates a new modified version of the document. If it has, the example modifies the checked out version. Called when the "Edit document" button is pressed.
    /// Expects the "CreateExampleObjects" method and optionally the "CheckOut" method to be run first.
    /// </summary>
    private bool EditDocument()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            WorkflowManager workflowmanager = new WorkflowManager(tree);

            // Make sure the document uses workflow
            WorkflowInfo workflow = workflowmanager.GetNodeWorkflow(node);

            if (workflow != null)
            {
                // Check if the workflow uses check-in/check-out
                bool autoCheck = !workflow.UseCheckInCheckOut(CMSContext.CurrentSiteName);

                // Create a new version manager instance
                VersionManager versionmanager = new VersionManager(tree);

                // If it does not use check-in/check-out, check out the document automatically
                if (autoCheck)
                {
                    versionmanager.CheckOut(node);
                }

                if (node.IsCheckedOut)
                {
                    // Edit the last version of the document
                    string newName = node.DocumentName.ToLower();

                    node.DocumentName = newName;
                    node.SetValue("MenuItemName", newName);

                    // Save the document version
                    DocumentHelper.UpdateDocument(node, tree);

                    // Automatically check in
                    if (autoCheck)
                    {
                        versionmanager.CheckIn(node, null, null);
                    }

                    return true;
                }
                else
                {
                    apiEditDocument.ErrorMessage = "The document hasn't been checked out.";
                }
            }
            else
            {
                apiEditDocument.ErrorMessage = "The document doesn't use workflow.";
            }
        }

        return false;
    }

    /// <summary>
    /// Checks in the document. If check-in/check-out is disabled for the default workflow, the example can't be used. Called when the "Check in document" button is pressed.
    /// Expects the "CreateExampleObjects" and "CheckOut" methods to be run first.
    /// </summary>
    private bool CheckIn()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            WorkflowManager workflowmanager = new WorkflowManager(tree);

            // Make sure the document uses workflow
            WorkflowInfo workflow = workflowmanager.GetNodeWorkflow(node);

            if (workflow != null)
            {
                if (node.IsCheckedOut)
                {
                    VersionManager versionmanager = new VersionManager(tree);

                    // Check in the document
                    versionmanager.CheckIn(node, null, null);

                    return true;
                }
                else
                {
                    apiCheckIn.ErrorMessage = "The document hasn't been checked out.";
                }
            }
            else
            {
                apiCheckIn.ErrorMessage = "The document doesn't use workflow.";
            }
        }

        return false;
    }

    /// <summary>
    /// Reverts back the changes made in the latest version if check-in/check-out is used. Called when the "Undo check-out" button is pressed.
    /// Expects the "CreateExampleObjects" and "CheckOut" methods to be run first.
    /// </summary>
    private bool UndoCheckout()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            WorkflowManager workflowmanager = new WorkflowManager(tree);

            // Make sure the document uses workflow
            WorkflowInfo workflow = workflowmanager.GetNodeWorkflow(node);

            if (workflow != null)
            {
                if (node.IsCheckedOut)
                {
                    VersionManager versionmanager = new VersionManager(tree);

                    // Undo the checkout
                    versionmanager.UndoCheckOut(node);

                    return true;
                }
                else
                {
                    apiUndoCheckout.ErrorMessage = "The document hasn't been checked out.";
                }
            }
            else
            {
                apiUndoCheckout.ErrorMessage = "The document doesn't use workflow.";
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Workflow process"

    /// <summary>
    /// Moves the document to the next step in the workflow process. Called when the "Move to next step" button is pressed.
    /// Expects the "CreateExampleObjects" and at least the "EditDocument" method to be run first.
    /// </summary>
    private bool MoveToNextStep()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            WorkflowManager workflowManager = new WorkflowManager(tree);

            WorkflowInfo workflow = workflowManager.GetNodeWorkflow(node);

            // Check if the document uses workflow
            if (workflow != null)
            {
                // Check if the workflow doesn't use automatic publishing, otherwise, documents can't change workflow steps.
                if (!workflow.WorkflowAutoPublishChanges)
                {
                    // Check if the current user can move the document to the next step
                    if (workflowManager.CanUserApprove(node))
                    {
                        // Move the document to the next step
                        workflowManager.MoveToNextStep(node, null);

                        return true;
                    }
                    else
                    {
                        apiMoveToNextStep.ErrorMessage = "You are not authorized to approve the document.";
                    }
                }
                else
                {
                    apiMoveToNextStep.ErrorMessage = "The document uses versioning without workflow, changes are published automatically.";
                }
            }
            else
            {
                apiMoveToNextStep.ErrorMessage = "The document doesn't use workflow.";
            }
        }

        return false;
    }

    /// <summary>
    /// Moves the document to the previous step in the workflow process. Called when the "Move to previous step" button is pressed.
    /// Expects the "CreateExampleObjects" and "MoveToNextStep" method to be run first.
    /// </summary>
    private bool MoveToPreviousStep()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            WorkflowManager workflowManager = new WorkflowManager(tree);

            WorkflowInfo workflow = workflowManager.GetNodeWorkflow(node);

            // Check if the document uses workflow
            if (workflow != null)
            {
                // Check if the workflow doesn't use automatic publishing, otherwise, documents can't change workflow steps.
                if (!workflow.WorkflowAutoPublishChanges)
                {
                    // Check if the current user can move the document to the next step
                    if (workflowManager.CanUserApprove(node))
                    {
                        // Move the document to the previous step
                        workflowManager.MoveToPreviousStep(node, null);

                        return true;
                    }
                    else
                    {
                        apiMoveToPreviousStep.ErrorMessage = "You are not authorized to reject the document.";
                    }
                }
                else
                {
                    apiMoveToPreviousStep.ErrorMessage = "The document uses versioning without workflow, changes are published automatically.";
                }
            }
            else
            {
                apiMoveToPreviousStep.ErrorMessage = "The document doesn't use workflow.";
            }
        }

        return false;
    }

    /// <summary>
    /// Moves the document to the Archived step in the workflow process. Called when the "Archive document" button is pressed.
    /// Expects the "CreateExampleObjects" method to be run first.
    /// </summary>
    private bool ArchiveDocument()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            WorkflowManager workflowManager = new WorkflowManager(tree);

            WorkflowInfo workflow = workflowManager.GetNodeWorkflow(node);

            // Check if the document uses workflow
            if (workflow != null)
            {
                // Archive the document
                workflowManager.ArchiveDocument(node, null);

                return true;
            }
            else
            {
                apiArchiveDocument.ErrorMessage = "The document doesn't use workflow.";
            }
        }

        return false;
    }

    #endregion


    #region "API examples - Versioning"

    /// <summary>
    /// Rolls the document back to a specified document version. Called when the "Rollback version" button is pressed.
    /// Expects the "CreateExampleObjects" and at least the "EditDocument" method to be run first.
    /// </summary>
    private bool RollbackVersion()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            // Prepare the WHERE condition for the oldest document version
            where = "DocumentID = " + node.DocumentID;
            orderBy = "ModifiedWhen ASC";
            int topN = 1;

            // Get the version ID
            DataSet versionHistory = VersionHistoryInfoProvider.GetVersionHistories(where, orderBy, topN, columns);

            if (!DataHelper.DataSourceIsEmpty(versionHistory))
            {
                // Create the Version history info object
                VersionHistoryInfo version = new VersionHistoryInfo(versionHistory.Tables[0].Rows[0]);

                VersionManager versionManager = new VersionManager(tree);

                // Roll back version
                versionManager.RollbackVersion(version.VersionHistoryID);

                return true;
            }
            else
            {
                apiRollbackVersion.ErrorMessage = "The document's version history is empty.";
            }
        }

        return false;
    }

    /// <summary>
    /// Deletes a specified document version. Called when the "Delete version" button is pressed.
    /// Expects the "CreateExampleObjects" and at least the "EditDocument" method to be run first.
    /// </summary>
    private bool DeleteVersion()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            // Prepare the WHERE condition for the latest document version
            where = "DocumentID = " + node.DocumentID;
            orderBy = "ModifiedWhen DESC";
            int topN = 1;

            // Get the version ID
            DataSet versionHistory = VersionHistoryInfoProvider.GetVersionHistories(where, orderBy, topN, columns);

            if (!DataHelper.DataSourceIsEmpty(versionHistory))
            {
                // Create the Version history info object
                VersionHistoryInfo version = new VersionHistoryInfo(versionHistory.Tables[0].Rows[0]);

                VersionManager versionManager = new VersionManager(tree);

                // Delete the version
                versionManager.DestroyDocumentVersion(version.VersionHistoryID);

                return true;
            }
            else
            {
                apiDeleteVersion.ErrorMessage = "The document's version history is empty.";
            }
        }

        return false;
    }

    /// <summary>
    /// Destroys the entire document's version history. Called when the "Destroy version history" button is pressed.
    /// Expects the "CreateExampleObjects" method to be run first.
    /// </summary>
    private bool DestroyHistory()
    {
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

        // Get the document
        TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, culture, combineWithDefaultCulture, classNames, where, orderBy, maxRelativeLevel, selectOnlyPublished, columns, tree);

        if (node != null)
        {
            VersionManager versionManager = new VersionManager(tree);

            // Destroy the version history
            versionManager.DestroyDocumentHistory(node.DocumentID);

            return true;
        }

        return false;
    }

    #endregion


    #region "API examples - Cleanup"

    /// <summary>
    /// Deletes the workflow scope, workflow step and the document used for this example. Called when the "Delete example objects" button is pressed.
    /// Expects the "CreateExampleObjects" method to be run first.
    /// </summary>
    private bool DeleteExampleObjects()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get the example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", "en-us");

        if (node != null)
        {
            // Delete the document
            DocumentHelper.DeleteDocument(node, tree, true, true, true);
        }

        string where = "ScopeStartingPath LIKE '/API-Example%'";

        // Get example workflow scopes 
        DataSet scopes = WorkflowScopeInfoProvider.GetWorkflowScopes(where, null, 0, null);

        if (!DataHelper.DataSourceIsEmpty(scopes))
        {
            // Loop through all the scopes in case more identical scopes were accidentally created
            foreach (DataRow scopeRow in scopes.Tables[0].Rows)
            {
                // Create scope info object
                WorkflowScopeInfo scope = new WorkflowScopeInfo(scopeRow);

                // Delete the scope
                WorkflowScopeInfoProvider.DeleteWorkflowScopeInfo(scope);
            }
        }

        // Get the default workflow
        WorkflowInfo workflow = WorkflowInfoProvider.GetWorkflowInfo("default");

        if (workflow != null)
        {
            // Get the example step
            WorkflowStepInfo step = WorkflowStepInfoProvider.GetWorkflowStepInfo("MyNewWorkflowStep", workflow.WorkflowID);

            if (step != null)
            {
                // Delete the step
                WorkflowStepInfoProvider.DeleteWorkflowStepInfo(step);
            }
        }

        return true;
    }

    #endregion
}