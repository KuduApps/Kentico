using System;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;

[Title(Text = "Content management - Security", ImageUrl = "Objects/CMS_Permission/object.png")]
public partial class CMSAPIExamples_Code_Documents_Security_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Creating documents
        this.apiCreateDocumentStructure.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateDocumentStructure);

        // Deleting documents
        this.apiDeleteDocumentStructure.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteDocumentStructure);

        // Setting permissions
        this.apiSetUserPermissions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SetUserPermissions);
        this.apiSetRolePermissions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(SetRolePermissions);

        // Deleting document level permissions
        this.apiDeletePermissions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeletePermissions);

        // Permission inheritance
        this.apiBreakPermissionInheritance.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(BreakPermissionInheritance);
        this.apiRestorePermissionInheritance.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RestorePermissionInheritance);

        // Checking permissions
        this.apiCheckContentModulePermissions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CheckContentModulePermissions);
        this.apiCheckDocTypePermissions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CheckDocTypePermissions);
        this.apiCheckDocumentPermissions.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CheckDocumentPermissions);
        this.apiFilterDataSet.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(FilterDataSet);
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

        // Setting permissions
        this.apiSetUserPermissions.Run();
        this.apiSetRolePermissions.Run();

        // Permission inheritance
        this.apiBreakPermissionInheritance.Run();
        this.apiRestorePermissionInheritance.Run();

        // Checking permissions
        this.apiCheckContentModulePermissions.Run();
        this.apiCheckDocTypePermissions.Run();
        this.apiCheckDocumentPermissions.Run();
        this.apiFilterDataSet.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Deleting permissions
        this.apiDeletePermissions.Run();

        // Deleting documents
        this.apiDeleteDocumentStructure.Run();

    }

    #endregion


    #region "API examples - Documents"

    /// <summary>
    /// Creates the initial document strucutre used for the example. Called when the "Create document structure" button is pressed.
    /// </summary>
    private bool CreateDocumentStructure()
    {
        // Create new instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");

        // Get parent node
        TreeNode parentNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/", culture);

        if (parentNode != null)
        {
            // Create the API Example document
            TreeNode newNode = TreeNode.New("CMS.MenuItem", tree);

            newNode.DocumentName = "API Example";
            newNode.DocumentCulture = culture;

            newNode.Insert(parentNode);

            parentNode = newNode;

            // Create the API Example subpage
            newNode = TreeNode.New("CMS.MenuItem", tree);

            newNode.DocumentName = "API Example subpage";
            newNode.DocumentCulture = culture;

            newNode.Insert(parentNode);

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
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");


        // Get the API Example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", culture);

        if (node != null)
        {
            // Delete the document and all child documents
            node.DeleteAllCultures();
        }

        return true;
    }


    #endregion


    #region "API examples - Setting document permissions"

    /// <summary>
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool SetUserPermissions()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");

        // Get the API Example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", culture);

        if (node != null)
        {
            // Get the user
            UserInfo user = UserInfoProvider.GetUserInfo("Andy");

            if (user != null)
            {
                // Prepare allowed / denied permissions
                int allowed = 0;
                int denied = 0;
                allowed += Convert.ToInt32(Math.Pow(2, Convert.ToInt32(NodePermissionsEnum.ModifyPermissions)));

                // Create an instance of ACL provider
                AclProvider acl = new AclProvider(tree);

                // Set user permissions
                acl.SetUserPermissions(node, allowed, denied, user);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool SetRolePermissions()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");

        // Get the API Example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", culture);

        if (node != null)
        {
            // Get the role ID
            RoleInfo role = RoleInfoProvider.GetRoleInfo("CMSEditor", CMSContext.CurrentSiteName);

            if (role != null)
            {
                // Prepare allowed / denied permissions
                int allowed = 0;
                int denied = 0;
                allowed += Convert.ToInt32(Math.Pow(2, Convert.ToInt32(NodePermissionsEnum.Modify)));

                // Create an instance of ACL provider
                AclProvider acl = new AclProvider(tree);

                // Set role permissions
                acl.SetRolePermissions(node, allowed, denied, role.RoleID);

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool DeletePermissions()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");

        // Get the API Example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", culture);

        if (node != null)
        {
            // Create an instance of ACL provider
            AclProvider acl = new AclProvider(tree);

            // Get ID of ACL used on API Example document
            int nodeACLID = ValidationHelper.GetInteger(node.GetValue("NodeACLID"), 0);

            // Delete all ACL items 
            acl.ClearACLItems(nodeACLID);

            return true;
        }

        return false;
    }


    #endregion


    #region "API examples - Permission inheritance"

    /// <summary>
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool BreakPermissionInheritance()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");

        // Get the API Example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/API-Example-subpage", culture);

        if (node != null)
        {
            // Create an instance of ACL provider
            AclProvider acl = new AclProvider(tree);

            // Break permission inheritance (without copying parent permissions)
            bool copyParentPermissions = false;
            acl.BreakInherintance(node, copyParentPermissions);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool RestorePermissionInheritance()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");

        // Get the API Example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example/API-Example-subpage", culture);

        if (node != null)
        {
            // Create an instance of ACL provider
            AclProvider acl = new AclProvider(tree);

            // Restore permission inheritance
            acl.RestoreInheritance(node);

            return true;
        }

        return false;
    }


    #endregion


    #region "API examples - Checking permissions"

    /// <summary>
    /// Makes permission check for the specified module
    /// </summary>
    private bool CheckContentModulePermissions()
    {
        // Get the user
        UserInfo user = UserInfoProvider.GetUserInfo("CMSEditor");

        if (user != null)
        {
            // Check permissions and perform an action according to the result
            if (UserInfoProvider.IsAuthorizedPerResource("CMS.Content", "Read", CMSContext.CurrentSiteName, user))
            {

                apiCheckContentModulePermissions.InfoMessage = "User 'CMSEditor' is allowed to read module 'Content'.";
            }
            else
            {
                apiCheckContentModulePermissions.InfoMessage = "User 'CMSEditor' is not allowed to read module 'Content'.";
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Makes permission check for the specified document type
    /// </summary>
    private bool CheckDocTypePermissions()
    {
        // Get the user
        UserInfo user = UserInfoProvider.GetUserInfo("CMSEditor");

        if (user != null)
        {
            // Check permissions and perform an action according to the result
            if (UserInfoProvider.IsAuthorizedPerClass("CMS.MenuItem", "Read", CMSContext.CurrentSiteName, user))
            {

                apiCheckDocTypePermissions.InfoMessage = "User 'CMSEditor' is allowed to read document type 'MenuItem'.";
            }
            else
            {
                apiCheckDocTypePermissions.InfoMessage = "User 'CMSEditor' is not allowed to read document type 'MenuItem'.";
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Makes permission check for specified node - provides check in its ACLs, document type and Content module
    /// Expects the "CreateDocumentStructure" method to be run first.
    /// </summary>
    private bool CheckDocumentPermissions()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Get default culture code
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");

        // Get the API Example document
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, "/API-Example", culture);

        if (node != null)
        {
            // Get the user
            UserInfo user = UserInfoProvider.GetUserInfo("CMSEditor");

            if (user != null)
            {
                // Check permissions and perform an action according to the result
                if (TreeSecurityProvider.IsAuthorizedPerNode(node, NodePermissionsEnum.ModifyPermissions, user) == AuthorizationResultEnum.Allowed)
                {
                    apiCheckDocumentPermissions.InfoMessage = "User 'CMSEditor' is allowed to modify permissions for document 'API Example'.";
                }
                else
                {
                    apiCheckDocumentPermissions.InfoMessage = "User 'CMSEditor' is not allowed to modify permissions for document 'API Example'.";
                }

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Changes permission inheritance for documents filtered by permission 'Modify permissions' 
    /// </summary>
    private bool FilterDataSet()
    {
        // Create an instance of the Tree provider
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        // Set the parameters for getting documents
        string siteName = CMSContext.CurrentSiteName;
        string aliasPath = "/%";
        string culture = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDefaultCultureCode");
        bool combineWithDefaultCulture = true;

        // Get data set with documents
        DataSet documents = tree.SelectNodes(siteName, aliasPath, culture, combineWithDefaultCulture);

        // Get the user
        UserInfo user = UserInfoProvider.GetUserInfo("CMSEditor");

        if (user != null)
        {
            // Filter the data set by the user permissions
            TreeSecurityProvider.FilterDataSetByPermissions(documents, NodePermissionsEnum.ModifyPermissions, user);

            if (!DataHelper.DataSourceIsEmpty(documents))
            {
                // Create an instance of ACL provider
                AclProvider acl = new AclProvider(tree);

                // Loop through filtered documents
                foreach (DataRow documentRow in documents.Tables[0].Rows)
                {
                    // Create a new Tree node from the data row
                    TreeNode node = TreeNode.New(documentRow, "CMS.MenuItem", tree);

                    // Break permission inheritance (with copying parent permissions)
                    acl.BreakInherintance(node, true);
                }

                // Data set filtered successfully - permission inheritance broken for filtered items
                apiFilterDataSet.InfoMessage = "Data set with all documents filtered successfully by permission 'Modify permissions' for user 'CMSEditor'. Permission inheritance broken for filtered items.";
            }
            else
            {
                // Data set filtered successfully - no items left in data set
                apiFilterDataSet.InfoMessage = "Data set with all documents filtered successfully by permission 'Modify permissions' for user 'CMSEditor'. No items left in data set.";
            }

            return true;
        }

        return false;
    }

    #endregion
}