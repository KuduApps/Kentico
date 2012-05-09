using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.EventLog;

using TreeNode = CMS.TreeEngine.TreeNode;

/// <summary>
/// Control for handling global content actions.
/// </summary>
public abstract class ContentActionsControl : CMSUserControl
{
    #region "Variables"

    private CurrentUserInfo mCurrentUser = null;
    private TreeProvider mTreeProvider = null;
    protected SiteInfo currentSite = null;
    private bool mIsMultipleAction = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Tree provider.
    /// </summary>
    public TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider(CurrentUser));
        }
    }


    /// <summary>
    /// Current user.
    /// </summary>
    public CurrentUserInfo CurrentUser
    {
        get
        {
            return mCurrentUser ?? (mCurrentUser = CMSContext.CurrentUser);
        }
        set
        {
            mCurrentUser = value;
        }
    }


    /// <summary>
    /// Determines whether action is a dialog action.
    /// </summary>
    public bool IsDialogAction
    {
        get
        {
            return mIsMultipleAction;
        }
        set
        {
            mIsMultipleAction = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnLoad(EventArgs e)
    {
        // Initialize current user and site
        CurrentUser = CMSContext.CurrentUser;
        currentSite = CMSContext.CurrentSite;
        base.OnLoad(e);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Processes the given action.
    /// </summary>
    /// <param name="node">Node to process</param>
    /// <param name="targetNode">Target node</param>
    /// <param name="action">Action to process</param>
    /// <param name="childNodes">Process also child nodes</param>
    /// <param name="throwExceptions">If true, the exceptions are thrown</param>
    /// <param name="copyPermissions">Indicates if permissions should be copied</param>
    protected TreeNode ProcessAction(TreeNode node, TreeNode targetNode, string action, bool childNodes, bool throwExceptions, bool copyPermissions)
    {
        int nodeId = node.NodeID;
        int targetId = targetNode.NodeID;

        action = action.ToLower();
        bool first = action.EndsWith("first");

        // Perform the action
        switch (action)
        {
            case "linknodefirst":
            case "linknodeposition":
                {
                    try
                    {
                        // Set the parent ID
                        int newPosition = 1;

                        if ((targetNode.NodeClassName.ToLower() != "cms.root") && !first)
                        {
                            // Init the node orders in parent
                            TreeProvider.InitNodeOrders(targetNode.NodeParentID);
                            targetNode = TreeProvider.SelectSingleNode(targetId);

                            // Get the target order and real parent ID
                            int newTargetId = targetNode.NodeParentID;
                            newPosition = targetNode.NodeOrder + 1;

                            targetNode = TreeProvider.SelectSingleNode(newTargetId);
                        }

                        // Link the node
                        TreeNode newNode = LinkNode(node, targetNode, TreeProvider, copyPermissions, childNodes);
                        if (newNode != null)
                        {
                            nodeId = newNode.NodeID;

                            // Reposition the node
                            if (newPosition == 1)
                            {
                                // First position
                                int newOrder = TreeProvider.SetNodeOrder(nodeId, DocumentOrderEnum.First);
                                newNode.SetValue("NodeOrder", newOrder);
                            }
                            else
                            {
                                targetNode = TreeProvider.SelectSingleNode(targetId);
                                newPosition = targetNode.NodeOrder + 1;

                                // After the target
                                TreeProvider.SetNodeOrder(nodeId, newPosition, false);
                                newNode.SetValue("NodeOrder", newPosition);
                            }
                        }

                        return newNode;
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider.LogException("Content", "LINKDOC", ex);

                        AddError(GetString("ContentRequest.LinkFailed"));
                        if (throwExceptions)
                        {
                            throw;
                        }
                        return null;
                    }
                }

            case "linknode":
                {
                    try
                    {
                        // Link the node
                        TreeNode newNode = LinkNode(node, targetNode, TreeProvider, copyPermissions, childNodes);
                        if (newNode != null)
                        {
                            // Set default position
                            int newOrder = TreeProvider.SetNodeOrder(newNode.NodeID, DocumentOrderEnum.First);
                            newNode.SetValue("NodeOrder", newOrder);

                        }
                        return newNode;
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider.LogException("Content", "LINKDOC", ex);

                        AddError(GetString("ContentRequest.LinkFailed"));
                        if (throwExceptions)
                        {
                            throw;
                        }
                        return null;
                    }
                }

            case "copynodefirst":
            case "copynodeposition":
                {
                    try
                    {
                        // Set the parent ID
                        int newPosition = 1;

                        if ((targetNode.NodeClassName.ToLower() != "cms.root") && !first)
                        {
                            // Init the node orders in parent
                            TreeProvider.InitNodeOrders(targetNode.NodeParentID);
                            targetNode = TreeProvider.SelectSingleNode(targetId);

                            // Get the target order and real parent ID
                            int newTargetId = targetNode.NodeParentID;
                            newPosition = targetNode.NodeOrder + 1;

                            targetNode = TreeProvider.SelectSingleNode(newTargetId);
                        }

                        // Copy the node
                        TreeNode newNode = CopyNode(node, targetNode, childNodes, TreeProvider, copyPermissions);
                        if (newNode != null)
                        {
                            nodeId = newNode.NodeID;

                            // Reposition the node
                            if (newPosition == 1)
                            {
                                // First position
                                int newOrder = TreeProvider.SetNodeOrder(nodeId, DocumentOrderEnum.First);
                                newNode.SetValue("NodeOrder", newOrder);
                            }
                            else
                            {
                                targetNode = TreeProvider.SelectSingleNode(targetId);
                                newPosition = targetNode.NodeOrder + 1;

                                // After the target
                                TreeProvider.SetNodeOrder(nodeId, newPosition, false);
                                newNode.SetValue("NodeOrder", newPosition);
                            }

                            return newNode;
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider.LogException("Content", "COPYDOC", ex);

                        AddError(GetString("ContentRequest.CopyFailed"));
                        if (throwExceptions)
                        {
                            throw;
                        }
                        return null;
                    }
                }
                break;

            case "copynode":
                {
                    try
                    {
                        // Copy the node
                        TreeNode newNode = CopyNode(node, targetNode, childNodes, TreeProvider, copyPermissions);
                        if (newNode != null)
                        {
                            // Set default position
                            int newOrder = TreeProvider.SetNodeOrder(newNode.NodeID, DocumentOrderEnum.First);
                            newNode.SetValue("NodeOrder", newOrder);

                        }
                        return newNode;
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider.LogException("Content", "COPYDOC", ex);

                        AddError(GetString("ContentRequest.CopyFailed"));
                        if (throwExceptions)
                        {
                            throw;
                        }
                        return null;
                    }
                }

            case "movenodefirst":
            case "movenodeposition":
                {
                    try
                    {
                        // Check the permissions for document
                        if (CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)
                        {
                            // Set the parent ID
                            int newPosition = 1;

                            if ((targetNode.NodeClassName.ToLower() != "cms.root") && !first)
                            {
                                // Init the node orders in parent
                                TreeProvider.InitNodeOrders(targetNode.NodeParentID);
                                targetNode = TreeProvider.SelectSingleNode(targetId);

                                // Get the target order and real parent ID
                                int newTargetId = targetNode.NodeParentID;
                                newPosition = targetNode.NodeOrder + 1;

                                targetNode = TreeProvider.SelectSingleNode(newTargetId);
                            }

                            // Move the node under the correct parent
                            if (((node.NodeOrder != newPosition) || (node.NodeParentID != targetNode.NodeID)) && MoveNode(node, targetNode, TreeProvider, copyPermissions))
                            {
                                if (targetNode.NodeID == nodeId)
                                {
                                    return null;
                                }

                                // Reposition the node
                                if (newPosition == 1)
                                {
                                    // First position
                                    int newOrder = TreeProvider.SetNodeOrder(nodeId, DocumentOrderEnum.First);
                                    node.SetValue("NodeOrder", newOrder);
                                }
                                else
                                {
                                    // After the target
                                    TreeProvider.SetNodeOrder(nodeId, newPosition, false);
                                    node.SetValue("NodeOrder", newPosition);
                                }

                                return node;
                            }
                        }
                        else
                        {
                            string encodedAliasPath = " (" + HTMLHelper.HTMLEncode(node.NodeAliasPath) + ")";
                            AddError(GetString("ContentRequest.NotAllowedToMove") + encodedAliasPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider.LogException("Content", "MOVEDOC", ex);

                        AddError(GetString("ContentRequest.MoveFailed"));
                        if (throwExceptions)
                        {
                            throw;
                        }
                        return null;
                    }
                }
                break;

            case "movenode":
                {
                    try
                    {
                        // Move the node
                        if (MoveNode(node, targetNode, TreeProvider, copyPermissions))
                        {
                            // Set default position
                            int newOrder = TreeProvider.SetNodeOrder(nodeId, DocumentOrderEnum.First);
                            node.SetValue("NodeOrder", newOrder);
                            return node;
                        }
                    }
                    catch (Exception ex)
                    {
                        EventLogProvider.LogException("Content", "MOVEDOC", ex);

                        AddError(GetString("ContentRequest.MoveFailed"));
                        if (throwExceptions)
                        {
                            throw;
                        }
                        return null;
                    }
                }
                break;
        }

        return null;
    }


    /// <summary>
    /// Links the node to the specified target.
    /// </summary>
    /// <param name="node">Node to move</param>
    /// <param name="targetNode">Target node</param>
    /// <param name="tree">Tree provider</param>
    /// <param name="copyPermissions">Indicates if node permissions should be copied</param>
    /// <param name="childNodes">Indicates whether to link also child nodes</param>
    protected TreeNode LinkNode(TreeNode node, TreeNode targetNode, TreeProvider tree, bool copyPermissions, bool childNodes)
    {
        string encodedAliasPath = " (" + HTMLHelper.HTMLEncode(node.NodeAliasPath) + ")";
        int targetId = targetNode.NodeID;

        // Check create permission
        if (!IsUserAuthorizedToCopyOrLink(node, targetId, node.NodeClassName))
        {
            AddError(GetString("ContentRequest.NotAllowedToLink") + encodedAliasPath);
            return null;
        }

        // Check allowed child class
        int targetClassId = ValidationHelper.GetInteger(targetNode.GetValue("NodeClassID"), 0);
        int nodeClassId = ValidationHelper.GetInteger(node.GetValue("NodeClassID"), 0);
        if (!DataClassInfoProvider.IsChildClassAllowed(targetClassId, nodeClassId) || (ClassSiteInfoProvider.GetClassSiteInfo(nodeClassId, targetNode.NodeSiteID) == null))
        {
            AddError(String.Format(GetString("ContentRequest.ErrorDocumentTypeNotAllowed"), node.NodeAliasPath, node.NodeClassName));
            return null;
        }

        // Determine whether any child nodes are present
        bool includeChildNodes = (node.NodeChildNodesCount > 0) && childNodes;

        // Document can't be copied under itself if child nodes are present
        if ((node.NodeID == targetNode.NodeID) && includeChildNodes)
        {
            AddError(GetString("ContentRequest.CannotLinkToItself") + encodedAliasPath);
            return null;
        }

        string domainToCheck = null;
        if (targetNode.NodeSiteID == node.NodeSiteID)
        {
            domainToCheck = URLHelper.GetCurrentDomain();
        }
        else
        {
            SiteInfo targetSite = SiteInfoProvider.GetSiteInfo(targetNode.NodeSiteID);
            domainToCheck = targetSite.DomainName;
        }

        // Check the licence limitations
        if ((node.NodeClassName.ToLower() == "cms.blog") && !LicenseHelper.LicenseVersionCheck(domainToCheck, FeatureEnum.Blogs, VersionActionEnum.Insert))
        {
            AddError(GetString("cmsdesk.bloglicenselimits"));
            return null;
        }

        // Check cyclic linking (linking of the node to some of its child nodes)
        if ((targetNode.NodeSiteID == node.NodeSiteID) && (targetNode.NodeAliasPath.TrimEnd('/') + "/").StartsWith(node.NodeAliasPath + "/", StringComparison.CurrentCultureIgnoreCase) && includeChildNodes)
        {
            AddError(GetString("ContentRequest.CannotLinkToChild"));
            return null;
        }

        // Copy the document
        AddLog(HTMLHelper.HTMLEncode(node.NodeAliasPath));

        DocumentHelper.InsertDocumentAsLink(node, targetId, tree, includeChildNodes, copyPermissions);
        SetExpandedNode(node.NodeParentID);
        return node;
    }


    /// <summary>
    /// Copies the node to the specified target.
    /// </summary>
    /// <param name="node">Node to copy</param>
    /// <param name="targetNode">Target node</param>
    /// <param name="tree">Tree provider</param>
    /// <param name="childNodes">Copy also child nodes</param>
    /// <param name="copyPermissions">Indicates if node permissions should be copied</param>
    protected TreeNode CopyNode(TreeNode node, TreeNode targetNode, bool childNodes, TreeProvider tree, bool copyPermissions)
    {
        return CopyNode(node, targetNode, childNodes, tree, copyPermissions, null);
    }


    /// <summary>
    /// Copies the node to the specified target.
    /// </summary>
    /// <param name="node">Node to copy</param>
    /// <param name="targetNode">Target node</param>
    /// <param name="tree">Tree provider</param>
    /// <param name="childNodes">Copy also child nodes</param>
    /// <param name="copyPermissions">Indicates if node permissions should be copied</param>
    /// <param name="newDocumentName">New document name</param>
    protected TreeNode CopyNode(TreeNode node, TreeNode targetNode, bool childNodes, TreeProvider tree, bool copyPermissions, string newDocumentName)
    {
        string encodedAliasPath = " (" + HTMLHelper.HTMLEncode(node.NodeAliasPath) + ")";
        int targetId = targetNode.NodeID;

        // Do not copy child nodes in case of no child nodes
        childNodes = childNodes && (node.NodeChildNodesCount > 0);

        // Get the document to copy
        int nodeId = node.NodeID;
        if ((nodeId == targetId) && childNodes)
        {
            AddError(GetString("ContentRequest.CannotCopyToItself") + encodedAliasPath);
            return null;
        }

        // Check move permission
        if (!IsUserAuthorizedToCopyOrLink(node, targetId, node.NodeClassName))
        {
            AddError(GetString("ContentRequest.NotAllowedToCopy") + encodedAliasPath);
            return null;
        }

        // Check cyclic copying (copying of the node to some of its child nodes)
        if (childNodes && (targetNode.NodeSiteID == node.NodeSiteID) && targetNode.NodeAliasPath.StartsWith(node.NodeAliasPath + "/", StringComparison.CurrentCultureIgnoreCase))
        {
            AddError(GetString("ContentRequest.CannotCopyToChild"));
            return null;
        }

        string domainToCheck = null;
        if (targetNode.NodeSiteID == node.NodeSiteID)
        {
            domainToCheck = URLHelper.GetCurrentDomain();
        }
        else
        {
            SiteInfo targetSite = SiteInfoProvider.GetSiteInfo(targetNode.NodeSiteID);
            domainToCheck = targetSite.DomainName;
        }

        // Check the licence limitations
        if ((node.NodeClassName.ToLower() == "cms.blog") && !LicenseHelper.LicenseVersionCheck(domainToCheck, FeatureEnum.Blogs, VersionActionEnum.Insert))
        {
            AddError(GetString("cmsdesk.bloglicenselimits"));
            return null;
        }

        // Check allowed child class
        int targetClassId = ValidationHelper.GetInteger(targetNode.GetValue("NodeClassID"), 0);
        int nodeClassId = ValidationHelper.GetInteger(node.GetValue("NodeClassID"), 0);
        if (!DataClassInfoProvider.IsChildClassAllowed(targetClassId, nodeClassId) || (ClassSiteInfoProvider.GetClassSiteInfo(nodeClassId, targetNode.NodeSiteID) == null))
        {
            AddError(String.Format(GetString("ContentRequest.ErrorDocumentTypeNotAllowed"), node.NodeAliasPath, node.NodeClassName));
            return null;
        }

        // Copy the document
        AddLog(HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.DocumentCulture + ")"));

        node = DocumentHelper.CopyDocument(node, targetId, childNodes, tree, 0, 0, copyPermissions, newDocumentName);
        SetExpandedNode(node.NodeParentID);

        return node;
    }


    /// <summary>
    /// Moves the node to the specified target.
    /// </summary>
    /// <param name="node">Node to move</param>
    /// <param name="targetNode">Target node</param>
    /// <param name="tree">Tree provider</param>
    /// <param name="preservePermissions">Indicates if node permissions should be preserved</param>
    /// <returns>Whether to set new order</returns>
    protected bool MoveNode(TreeNode node, TreeNode targetNode, TreeProvider tree, bool preservePermissions)
    {
        int targetId = targetNode.NodeID;
        string encodedAliasPath = " (" + HTMLHelper.HTMLEncode(node.NodeAliasPath) + ")";

        // If node parent ID is already the target ID, do not move it
        if (targetId == node.NodeParentID)
        {
            if (IsDialogAction)
            {
                // Impossible to move document to same location
                AddError(GetString("contentrequest.cannotmovetosameloc") + encodedAliasPath);
            }
            return true;
        }
        
        // Check move permission
        if (!IsUserAuthorizedToMove(node, targetId, node.NodeClassName))
        {
            AddError(GetString("ContentRequest.NotAllowedToMove") + encodedAliasPath);
            return false;
        }       

        // Get the document to copy
        int nodeId = node.NodeID;
        if (nodeId == targetId)
        {
            AddError(GetString("ContentRequest.CannotMoveToItself") + encodedAliasPath);
            return false;
        }

        // Check cyclic movement (movement of the node to some of its child nodes)
        if ((targetNode.NodeSiteID == node.NodeSiteID) && targetNode.NodeAliasPath.StartsWith(node.NodeAliasPath + "/", StringComparison.CurrentCultureIgnoreCase))
        {
            AddError(GetString("ContentRequest.CannotMoveToChild"));
            return false;
        }

        if (targetNode.NodeSiteID != node.NodeSiteID)
        {
            SiteInfo targetSite = SiteInfoProvider.GetSiteInfo(targetNode.NodeSiteID);
            string domainToCheck = targetSite.DomainName;

            // Check the licence limitations
            if ((node.NodeClassName.ToLower() == "cms.blog") && !LicenseHelper.LicenseVersionCheck(domainToCheck, FeatureEnum.Blogs, VersionActionEnum.Insert))
            {
                AddError(GetString("cmsdesk.bloglicenselimits"));
                return false;
            }
        }

        // Check allowed child classes
        int targetClassId = ValidationHelper.GetInteger(targetNode.GetValue("NodeClassID"), 0);
        int nodeClassId = ValidationHelper.GetInteger(node.GetValue("NodeClassID"), 0);
        if (!DataClassInfoProvider.IsChildClassAllowed(targetClassId, nodeClassId) || (ClassSiteInfoProvider.GetClassSiteInfo(nodeClassId, targetNode.NodeSiteID) == null))
        {
            AddError(String.Format(GetString("ContentRequest.ErrorDocumentTypeNotAllowed"), node.NodeAliasPath, node.NodeClassName));
            return false;
        }

        // Get child documents
        if ((node.NodeChildNodesCount) > 0 && IsDialogAction)
        {
            string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeAliasPath");
            DataSet childNodes = TreeProvider.SelectNodes(currentSite.SiteName, node.NodeAliasPath.TrimEnd('/') + "/%", TreeProvider.ALL_CULTURES, true, null, null, null, TreeProvider.ALL_LEVELS, false, 0, columns);

            if (!DataHelper.DataSourceIsEmpty(childNodes))
            {
                foreach (DataRow childNode in childNodes.Tables[0].Rows)
                {
                    AddLog(HTMLHelper.HTMLEncode(childNode["NodeAliasPath"] + " (" + childNode["DocumentCulture"] + ")"));
                }
            }
        }

        // Move the document
        AddLog(HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.DocumentCulture + ")"));

        DocumentHelper.MoveDocument(node, targetId, tree, preservePermissions);
        SetExpandedNode(node.NodeParentID);

        return true;
    }

    #endregion


    #region "Virtual members"

    /// <summary>
    /// Handles error message.
    /// </summary>
    /// <param name="errorMessage">Message</param>
    protected virtual void AddError(string errorMessage)
    {
        AddError(errorMessage);
    }


    /// <summary>
    /// Adds the log information.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected virtual void AddLog(string newLog)
    {

    }


    /// <summary>
    /// Sets the expanded node ID.
    /// </summary>
    /// <param name="nodeId">Node ID to set</param>
    protected virtual void SetExpandedNode(int nodeId)
    {
    }

    #endregion


    #region "Permission checking"

    /// <summary>
    /// Determines whether current user is authorized to move document.
    /// </summary>
    /// <param name="sourceNode">Source node</param>
    /// <param name="targetNodeId">Target node class name</param>
    /// <param name="sourceNodeClassName">Source node class name</param>
    /// <returns>True if authorized</returns>
    protected bool IsUserAuthorizedToMove(TreeNode sourceNode, int targetNodeId, string sourceNodeClassName)
    {
        bool isAuthorized = false;

        // Check 'modify permission' to source document
        if (CurrentUser.IsAuthorizedPerDocument(sourceNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)
        {
            // Check 'create permission'
            if (CurrentUser.IsAuthorizedToCreateNewDocument(targetNodeId, sourceNodeClassName))
            {
                isAuthorized = true;
            }
        }

        return isAuthorized;
    }


    /// <summary>
    /// Determines whether current user is authorized to copy document.
    /// </summary>
    /// <param name="sourceNode">Source node</param>
    /// <param name="targetNodeId">Target node class name</param>
    /// <param name="sourceNodeClassName">Source node class name</param>
    /// <returns>True if authorized</returns>
    protected bool IsUserAuthorizedToCopyOrLink(TreeNode sourceNode, int targetNodeId, string sourceNodeClassName)
    {
        bool isAuthorized = false;

        // Check 'read permission' to source document
        if (CurrentUser.IsAuthorizedPerDocument(sourceNode, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed)
        {
            // Check 'create permission'
            if (CurrentUser.IsAuthorizedToCreateNewDocument(targetNodeId, sourceNodeClassName))
            {
                isAuthorized = true;
            }
        }

        return isAuthorized;
    }

    #endregion
}
