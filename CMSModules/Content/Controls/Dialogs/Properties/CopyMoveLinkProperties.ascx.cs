using System;
using System.Collections;
using System.Data;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Text;

using CMS.CMSHelper;
using CMS.EventLog;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.DataEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_Dialogs_Properties_CopyMoveLinkProperties : ContentActionsControl
{
    #region "Private variables, constants & enums"

    private const string underlying = "Underlying";

    private string parentAlias = string.Empty;
    private string whereCondition = string.Empty;
    private string canceledString = null;

    private int targetId = 0;

    private bool multiple = false;
    private bool sameSite = true;

    private static readonly Hashtable mErrors = new Hashtable();
    private readonly ArrayList nodeIds = new ArrayList();
    private DataSet documentsToProcess = null;
    private Hashtable mParameters = null;

    private SiteInfo targetSite = null;

    /// <summary>
    /// Possible actions
    /// </summary>
    protected enum Action
    {
        Move = 0,
        Copy = 1,
        Link = 2,
        LinkDoc = 3
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns current dialog action.
    /// </summary>
    private Action CurrentAction
    {
        get
        {
            try
            {
                return (Action)Enum.Parse(typeof(Action), ValidationHelper.GetString(Parameters["output"], "move"), true);
            }
            catch
            {
                return Action.Move;
            }
        }
    }


    /// <summary>
    /// Perform action.
    /// </summary>
    private bool DoAction
    {
        get
        {
            string action = ValidationHelper.GetString(Parameters["action"], null);
            int targetId = ValidationHelper.GetInteger(Parameters["targetid"], 0);

            return (!String.IsNullOrEmpty(action) && (targetId > 0));
        }
    }


    /// <summary>
    /// Current log context.
    /// </summary>
    public LogContext CurrentLog
    {
        get
        {
            return EnsureLog();
        }
    }


    /// <summary>
    /// Ensures the logging context.
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext currentLog = LogContext.EnsureLog(ctlAsync.ProcessGUID);

        currentLog.Reversed = true;
        currentLog.LineSeparator = "<br />";

        return currentLog;
    }


    /// <summary>
    /// Current Error.
    /// </summary>
    private string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mErrors["ProcessingError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mErrors["ProcessingError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Indicates if permissions should be copied or preserved.
    /// </summary>
    private static bool CopyPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(SessionHelper.GetValue("CopyMoveDocCopyPermissions"), false);
        }
        set
        {
            SessionHelper.SetValue("CopyMoveDocCopyPermissions", value);
        }
    }


    /// <summary>
    /// Hashtable containing dialog parameters.
    /// </summary>
    private Hashtable Parameters
    {
        get
        {
            if (mParameters == null)
            {
                string identificator = QueryHelper.GetString("params", null);
                mParameters = (Hashtable)WindowHelper.GetItem(identificator);
            }
            return mParameters;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            return;
        }

        // Check if hashtable containing dialog parameters is not empty
        if ((Parameters == null) || (Parameters.Count == 0))
        {
            return;
        }

        // Register CopyMove.js script file
        ScriptHelper.RegisterScriptFile(this.Page, "~/CMSModules/Content/Controls/Dialogs/Properties/CopyMove.js");

        IsDialogAction = true;
        // Setup tree provider
        TreeProvider.AllowAsyncActions = false;

        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;

        if (!RequestHelper.IsCallback())
        {
            // Get whether action is multiple
            multiple = ValidationHelper.GetBoolean(Parameters["multiple"], false);

            // Get the sorce node
            string nodeIdsString = ValidationHelper.GetString(Parameters["sourcenodeids"], string.Empty);
            string[] nodeIdsArr = nodeIdsString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string nodeId in nodeIdsArr)
            {
                int id = ValidationHelper.GetInteger(nodeId, 0);
                if (id != 0)
                {
                    nodeIds.Add(id);
                }
            }
            // Get target node id
            targetId = ValidationHelper.GetInteger(Parameters["targetid"], 0);

            using (TreeNode tn = TreeProvider.SelectSingleNode(targetId))
            {
                if ((tn != null) && (tn.NodeSiteID != currentSite.SiteID))
                {
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(tn.NodeSiteID);
                    if (si != null)
                    {
                        targetSite = si;
                    }
                }
                else
                {
                    targetSite = currentSite;
                }
            }

            // Set if operation take place on same site
            sameSite = (currentSite == targetSite);

            btnCancel.Text = GetString("General.Cancel");
            btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

            // Set introducing text
            if (targetId == 0)
            {
                switch (CurrentAction)
                {
                    case Action.Move:
                    case Action.Copy:
                    case Action.Link:
                        lblEmpty.Text = GetString("dialogs.copymove.select");
                        break;

                    case Action.LinkDoc:
                        lblEmpty.Text = GetString("dialogs.linkdoc.select");
                        break;
                }
            }

            // Check if target of action is another site
            if (!sameSite)
            {
                plcCopyPermissions.Visible = false;
                plcPreservePermissions.Visible = false;
            }

            if (!RequestHelper.IsPostBack())
            {
                object check = null;

                // Preset checkbox value
                switch (CurrentAction)
                {
                    case Action.Copy:
                        // Ensure underlying items checkbox
                        check = WindowHelper.GetItem(Action.Copy + underlying);
                        if (check == null)
                        {
                            WindowHelper.Add(Action.Copy + underlying, true);
                        }
                        chkUnderlying.Checked = ValidationHelper.GetBoolean(check, true);
                        if (sameSite)
                        {
                            chkCopyPermissions.Checked = CopyPermissions;
                        }
                        break;

                    case Action.Link:
                    case Action.LinkDoc:
                        // Ensure underlying items checkbox    
                        check = WindowHelper.GetItem(Action.Link + underlying);
                        if (check == null)
                        {
                            WindowHelper.Add(Action.Link + underlying, false);
                        }
                        chkUnderlying.Checked = ValidationHelper.GetBoolean(check, false);
                        if (sameSite)
                        {
                            chkCopyPermissions.Checked = CopyPermissions;
                        }
                        break;

                    case Action.Move:
                        if (sameSite)
                        {
                            chkPreservePermissions.Checked = CopyPermissions;
                        }
                        break;
                }
            }

            string listInfoString = string.Empty;

            // Set up layout and strings depending on selected action
            switch (CurrentAction)
            {
                case Action.Move:
                    listInfoString = "dialogs.move.listinfo";
                    canceledString = "ContentRequest.MoveCanceled";
                    plcUnderlying.Visible = false;
                    plcCopyPermissions.Visible = false;
                    chkPreservePermissions.Text = GetString("contentrequest.preservepermissions");
                    break;

                case Action.Copy:
                    listInfoString = "dialogs.copy.listinfo";
                    canceledString = "ContentRequest.CopyingCanceled";
                    chkUnderlying.ResourceString = "contentrequest.copyunderlying";
                    plcUnderlying.Visible = true;
                    plcPreservePermissions.Visible = false;
                    chkCopyPermissions.Text = GetString("contentrequest.copypermissions");
                    break;

                case Action.Link:
                    listInfoString = "dialogs.link.listinfo";
                    canceledString = "ContentRequest.LinkCanceled";
                    chkUnderlying.ResourceString = "contentrequest.linkunderlying";
                    plcUnderlying.Visible = true;
                    plcPreservePermissions.Visible = false;
                    chkCopyPermissions.Text = GetString("contentrequest.copypermissions");
                    break;

                case Action.LinkDoc:
                    listInfoString = "dialogs.link.listinfo";
                    canceledString = "ContentRequest.LinkCanceled";
                    chkUnderlying.ResourceString = "contentrequest.linkunderlying";
                    plcUnderlying.Visible = true;
                    plcPreservePermissions.Visible = false;
                    chkCopyPermissions.Text = GetString("contentrequest.copypermissions");
                    break;
            }

            // Localize string
            canceledString = GetString(canceledString);

            // Get alias path of document selected in tree
            string selectedAliasPath = TreePathUtils.GetAliasPathByNodeId(targetId);

            // Set target alias path
            if ((CurrentAction == Action.Copy) || (CurrentAction == Action.Move) || (CurrentAction == Action.Link))
            {
                lblAliasPath.Text = selectedAliasPath;
            }

            if (nodeIds.Count == 1)
            {
                TreeNode sourceNode = null;
                string sourceAliasPath = string.Empty;

                // Get source node
                if ((CurrentAction == Action.Copy) || (CurrentAction == Action.Move) || (CurrentAction == Action.Link))
                {
                    int nodeId = ValidationHelper.GetInteger(nodeIds[0], 0);
                    sourceNode = TreeProvider.SelectSingleNode(nodeId);
                }
                else if (CurrentAction == Action.LinkDoc)
                {
                    sourceNode = TreeProvider.SelectSingleNode(targetId);
                    if (sourceNode != null)
                    {
                        sourceAliasPath = sourceNode.NodeAliasPath;
                    }
                    // Show document to be linked
                    lblDocToCopyList.Text = sourceAliasPath;
                }

                // Hide checkbox if document has no childs
                if (sourceNode != null)
                {
                    if (sourceNode.NodeChildNodesCount == 0)
                    {
                        plcUnderlying.Visible = false;
                    }
                }
            }

            // Set visibility of panels
            pnlGeneralTab.Visible = true;
            pnlLog.Visible = false;

            // Get where condition for multiple operation
            whereCondition = ValidationHelper.GetString(Parameters["where"], string.Empty);

            // Get the aliaspaths of the documents to copy/move/link
            parentAlias = ValidationHelper.GetString(Parameters["parentalias"], string.Empty);

            if (!String.IsNullOrEmpty(parentAlias))
            {
                lblDocToCopy.Text = GetString(listInfoString + "all") + ResHelper.Colon;
                lblDocToCopyList.Text = HTMLHelper.HTMLEncode(parentAlias);
            }
            else
            {
                lblDocToCopy.Text = GetString(listInfoString) + ResHelper.Colon;

                // Get the list of alias paths
                if (!String.IsNullOrEmpty(nodeIdsString))
                {
                    // Get alias paths from session
                    string aliasPaths = SessionHelper.GetValue("CopyMoveDocAliasPaths").ToString();

                    // Set alias paths
                    if ((CurrentAction == Action.Copy) || (CurrentAction == Action.Move) || (CurrentAction == Action.Link))
                    {
                        // As source paths
                        lblDocToCopyList.Text = aliasPaths;
                    }
                    else
                    {
                        // As target path
                        lblAliasPath.Text = aliasPaths;
                    }
                }
            }

            if (!RequestHelper.IsPostBack() && DoAction)
            {
                // Perform Move / Copy / Link action
                PerformAction();
            }

            pnlEmpty.Visible = (targetId <= 0);
            pnlGeneralTab.Visible = (targetId > 0);
        }
    }

    #endregion


    #region "Control events"

    protected void chkUnderlying_OnCheckedChanged(object sender, EventArgs e)
    {
        // Store whether to copy/link also underlying documents
        switch (CurrentAction)
        {
            case Action.Copy:
                WindowHelper.Add(Action.Copy + underlying, chkUnderlying.Checked);
                break;
            case Action.Link:
            case Action.LinkDoc:
                WindowHelper.Add(Action.Link + underlying, chkUnderlying.Checked);
                break;
        }
    }


    protected void chkPreservePermissions_OnCheckedChanged(object sender, EventArgs e)
    {
        // Set whether to copy permissions
        CopyPermissions = chkPreservePermissions.Checked;
    }


    protected void chkCopyPermissions_OnCheckedChanged(object sender, EventArgs e)
    {
        // Set whether to copy permissions
        CopyPermissions = chkCopyPermissions.Checked;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Moves document(s).
    /// </summary>
    /// <param name="parameter">Indicates if document permissions should be preserved</param>
    private void Move(object parameter)
    {
        int oldSiteId = 0;
        int newSiteId = 0;
        int parentId = 0;
        int nodeId = 0;
        TreeNode node = null;
        bool preservePermissions = ValidationHelper.GetBoolean(parameter, false);
        ctlAsync.Parameter = null;

        try
        {
            AddLog(GetString("ContentRequest.StartMove"));

            if (targetId == 0)
            {
                AddError(GetString("ContentRequest.ErrorMissingTarget"));
                return;
            }

            // Check if allow child type
            TreeNode targetNode = TreeProvider.SelectSingleNode(targetId, TreeProvider.ALL_CULTURES);
            if (targetNode == null)
            {
                AddError(GetString("ContentRequest.ErrorMissingTarget"));
                return;
            }

            PrepareNodeIdsForAllDocuments(currentSite.SiteName);
            if (DataHelper.DataSourceIsEmpty(documentsToProcess))
            {
                // Create where condition
                string where = SqlHelperClass.GetWhereCondition("NodeID", (int[])nodeIds.ToArray(typeof(int)));
                string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeParentID, DocumentName, NodeAliasPath, NodeLinkedNodeID");
                documentsToProcess = TreeProvider.SelectNodes(currentSite.SiteName, "/%", TreeProvider.ALL_CULTURES, true, null, where, null, TreeProvider.ALL_LEVELS, false, 0, columns);
            }

            if (!DataHelper.DataSourceIsEmpty(documentsToProcess))
            {
                foreach (DataRow nodeRow in documentsToProcess.Tables[0].Rows)
                {
                    nodeId = ValidationHelper.GetInteger(nodeRow["NodeID"], 0);
                    string className = nodeRow["ClassName"].ToString();
                    string aliasPath = nodeRow["NodeAliasPath"].ToString();
                    string docCulture = nodeRow["DocumentCulture"].ToString();

                    // Get document to move
                    node = DocumentHelper.GetDocument(currentSite.SiteName, aliasPath, docCulture, false, className, null, null, TreeProvider.ALL_LEVELS, false, null, TreeProvider);

                    if (node == null)
                    {
                        AddLog(string.Format(GetString("ContentRequest.DocumentNoLongerExists"), HTMLHelper.HTMLEncode(aliasPath)));
                        continue;
                    }

                    oldSiteId = node.NodeSiteID;
                    parentId = node.NodeParentID;

                    // Move the document
                    MoveNode(node, targetNode, TreeProvider, preservePermissions);
                    newSiteId = node.NodeSiteID;
                }
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state != CMSThread.ABORT_REASON_STOP)
            {
                // Try to get ID of site
                int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
                // Log event to event log
                LogExceptionToEventLog("MOVEDOC", "ContentRequest.MoveFailed", nodeId, ex, siteId);
            }
        }
        catch (Exception ex)
        {
            // Try to get ID of site
            int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
            // Log event to event log
            LogExceptionToEventLog("MOVEDOC", "ContentRequest.MoveFailed", nodeId, ex, siteId);
            HandlePossibleErrors();
        }
        finally
        {
            if (multiple)
            {
                ctlAsync.Parameter = "RefreshListing();";
            }
            else
            {
                // Set moved document in current site or parent node if copy to other site
                if (oldSiteId == newSiteId)
                {
                    // Process result
                    ctlAsync.Parameter = "TreeSelectNode(" + nodeId + ");TreeRefreshNode(" + targetId + ", " + nodeId + ");";
                }
                else
                {
                    ctlAsync.Parameter = "TreeRefreshNode(" + parentId + ", " + parentId + ");TreeSelectNode(" + parentId + ");";
                }
            }
        }
    }


    /// <summary>
    /// Copies document(s).
    /// </summary>
    private void Copy(object parameter)
    {
        int nodeId = 0;
        int oldSiteId = 0;
        int newSiteId = 0;
        TreeNode node = null;

        // Process Action parameters
        string[] parameters = ValidationHelper.GetString(parameter, "False;False").Split(';');
        if (parameters.Length != 2)
        {
            parameters = "False;False".Split(';');
        }
        bool includeChildNodes = ValidationHelper.GetBoolean(parameters[0], false);
        bool copyPermissions = ValidationHelper.GetBoolean(parameters[1], false);
        ctlAsync.Parameter = null;
        try
        {
            AddLog(GetString("ContentRequest.StartCopy"));

            if (targetId == 0)
            {
                AddError(GetString("ContentRequest.ErrorMissingTarget"));
                return;
            }
            // Get target document
            TreeNode targetNode = TreeProvider.SelectSingleNode(targetId, TreeProvider.ALL_CULTURES);
            if (targetNode == null)
            {
                AddError(GetString("ContentRequest.ErrorMissingTarget"));
                return;
            }

            PrepareNodeIdsForAllDocuments(currentSite.SiteName);
            if (DataHelper.DataSourceIsEmpty(documentsToProcess))
            {
                // Create where condition
                string where = SqlHelperClass.GetWhereCondition("NodeID", (int[])nodeIds.ToArray(typeof(int)));
                string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeAliasPath, ClassName, DocumentCulture");

                documentsToProcess = TreeProvider.SelectNodes(currentSite.SiteName, "/%", TreeProvider.ALL_CULTURES, true, null, where, null, TreeProvider.ALL_LEVELS, false, 0, columns);
            }

            if (!DataHelper.DataSourceIsEmpty(documentsToProcess))
            {
                foreach (DataRow nodeRow in documentsToProcess.Tables[0].Rows)
                {
                    // Get the current document
                    nodeId = ValidationHelper.GetInteger(nodeRow["NodeID"], 0);
                    string className = nodeRow["ClassName"].ToString();
                    string aliasPath = nodeRow["NodeAliasPath"].ToString();
                    string docCulture = nodeRow["DocumentCulture"].ToString();
                    node = DocumentHelper.GetDocument(currentSite.SiteName, aliasPath, docCulture, false, className, null, null, TreeProvider.ALL_LEVELS, false, null, TreeProvider);

                    if (node == null)
                    {
                        AddLog(string.Format(GetString("ContentRequest.DocumentNoLongerExists"), HTMLHelper.HTMLEncode(aliasPath)));
                        continue;
                    }

                    oldSiteId = node.NodeSiteID;

                    // Copy the document
                    TreeNode copiedNode = CopyNode(node, targetNode, includeChildNodes, TreeProvider, copyPermissions);
                    if (copiedNode != null)
                    {
                        node = copiedNode;
                    }
                    newSiteId = node.NodeSiteID;
                }
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state != CMSThread.ABORT_REASON_STOP)
            {
                // Try to get ID of site
                int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
                // Log event to event log
                LogExceptionToEventLog("COPYDOC", "ContentRequest.CopyFailed", nodeId, ex, siteId);
            }
        }
        catch (Exception ex)
        {
            // Try to get ID of site
            int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
            // Log event to event log
            LogExceptionToEventLog("COPYDOC", "ContentRequest.CopyFailed", nodeId, ex, siteId);
            HandlePossibleErrors();
        }
        finally
        {
            if (multiple)
            {
                AddLog(GetString("ContentRequest.CopyOK"));
                ctlAsync.Parameter = "RefreshListing();";
            }
            else
            {
                // Set moved document in current site or parent node if copy to other site
                if (oldSiteId == newSiteId)
                {
                    // Process result
                    if (node != null)
                    {
                        nodeId = (multiple ? nodeId : node.NodeID);

                        ctlAsync.Parameter = "TreeSelectNode(" + nodeId + ");TreeRefreshNode(" + targetId + ", " + nodeId + ");";
                    }
                    else
                    {
                        AddError(GetString("ContentRequest.CopyFailed"));
                    }
                }
                else
                {
                    AddLog(GetString("ContentRequest.CopyOK"));
                    ctlAsync.Parameter = string.Empty;
                }
            }
        }
    }


    private void Link(object parameter)
    {
        // Process Action parameters
        string[] parameters = ValidationHelper.GetString(parameter, "False;False").Split(';');
        if (parameters.Length != 2)
        {
            parameters = "False;False".Split(';');
        }
        bool includeChildNodes = ValidationHelper.GetBoolean(parameters[0], false);
        bool copyPermissions = ValidationHelper.GetBoolean(parameters[1], false);
        ctlAsync.Parameter = null;
        Link(includeChildNodes, targetId, nodeIds, Action.Link, copyPermissions);
    }


    private void LinkDoc(object parameter)
    {
        if ((nodeIds.Count > 0) && (targetId != 0))
        {
            // Switch parameters
            int currentTargetId = ValidationHelper.GetInteger(nodeIds[0], 0);
            ArrayList currentNodeIds = new ArrayList();
            currentNodeIds.Add(targetId);

            // Process Action parameters
            string[] parameters = ValidationHelper.GetString(parameter, "False;False").Split(';');
            if (parameters.Length != 2)
            {
                parameters = "False;False".Split(';');
            }
            bool includeChildNodes = ValidationHelper.GetBoolean(parameters[0], false);
            bool copyPermissions = ValidationHelper.GetBoolean(parameters[1], false);
            ctlAsync.Parameter = null;

            Link(includeChildNodes, currentTargetId, currentNodeIds, Action.LinkDoc, copyPermissions);
        }
    }


    /// <summary>
    /// Links selected document(s).
    /// </summary>
    /// <param name="includeChildNodes">Determines whether include child nodes</param>
    /// <param name="targetNodeId">Target node ID</param>
    /// <param name="sourceNodes">Nodes</param>
    /// <param name="performedAction">Action to be performed</param>
    /// <param name="copyPermissions">Indicates if the document permissions should be copied</param>
    private void Link(bool includeChildNodes, int targetNodeId, ArrayList sourceNodes, Action performedAction, bool copyPermissions)
    {
        int nodeId = 0;
        int oldSiteId = 0;
        int newSiteId = 0;
        TreeNode node = null;

        try
        {
            AddLog(GetString("ContentRequest.StartLink"));

            if (targetNodeId == 0)
            {
                AddError(GetString("ContentRequest.ErrorMissingTarget"));
                return;
            }

            // Check if allow child type
            TreeNode targetNode = TreeProvider.SelectSingleNode(targetNodeId, TreeProvider.ALL_CULTURES);
            if (targetNode == null)
            {
                AddError(GetString("ContentRequest.ErrorMissingTarget"));
                return;
            }

            // Prepare NodeIDs to process
            if (performedAction == Action.LinkDoc)
            {
                PrepareNodeIdsForAllDocuments(targetSite.SiteName);
            }
            else if (performedAction == Action.Link)
            {
                PrepareNodeIdsForAllDocuments(currentSite.SiteName);
            }

            string siteName = (performedAction == Action.LinkDoc) ? targetSite.SiteName : currentSite.SiteName;

            if (DataHelper.DataSourceIsEmpty(documentsToProcess))
            {
                // Create where condition
                string where = SqlHelperClass.GetWhereCondition("NodeID", (int[])sourceNodes.ToArray(typeof(int)));
                string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeParentID, DocumentName, NodeAliasPath, NodeLinkedNodeID");

                documentsToProcess = TreeProvider.SelectNodes(siteName, "/%", TreeProvider.ALL_CULTURES, true, null, where, null, TreeProvider.ALL_LEVELS, false, 0, columns);
            }

            if (!DataHelper.DataSourceIsEmpty(documentsToProcess))
            {
                foreach (DataRow nodeRow in documentsToProcess.Tables[0].Rows)
                {
                    nodeId = ValidationHelper.GetInteger(nodeRow["NodeID"], 0);
                    string className = nodeRow["ClassName"].ToString();
                    string aliasPath = nodeRow["NodeAliasPath"].ToString();
                    string docCulture = nodeRow["DocumentCulture"].ToString();

                    // Get document to link
                    node = DocumentHelper.GetDocument(siteName, aliasPath, docCulture, false, className, null, null, TreeProvider.ALL_LEVELS, false, null, TreeProvider);

                    if (node == null)
                    {
                        AddLog(string.Format(GetString("ContentRequest.DocumentNoLongerExists"), HTMLHelper.HTMLEncode(aliasPath)));
                        continue;
                    }

                    oldSiteId = node.NodeSiteID;

                    // Link the document
                    TreeNode linkedNode = LinkNode(node, targetNode, TreeProvider, copyPermissions, includeChildNodes);
                    if (linkedNode != null)
                    {
                        node = linkedNode;
                    }
                    newSiteId = node.NodeSiteID;
                }
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state != CMSThread.ABORT_REASON_STOP)
            {
                // Try to get ID of site
                int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
                // Log event to event log
                LogExceptionToEventLog("LINKDOC", "ContentRequest.LinkFailed", nodeId, ex, siteId);
            }
        }
        catch (Exception ex)
        {
            // Try to get ID of site
            int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
            // Log event to event log
            LogExceptionToEventLog("LINKDOC", "ContentRequest.LinkFailed", nodeId, ex, siteId);
            HandlePossibleErrors();
        }
        finally
        {
            if (multiple)
            {
                AddLog(GetString("ContentRequest.LinkOK"));
                ctlAsync.Parameter = "RefreshListing();";
            }
            else
            {
                // Set linked document in current site or parent node if linked to other site
                if (oldSiteId == newSiteId)
                {
                    if (node == null)
                    {
                        AddError(GetString("ContentRequest.LinkFailed"));
                    }
                }
                else
                {
                    AddLog(GetString("ContentRequest.LinkOK"));
                }

                // Process result
                if (node != null)
                {
                    ctlAsync.Parameter = "TreeSelectNode(" + node.NodeID + ");TreeRefreshNode(" + node.NodeID + ", " + node.NodeID + ");";
                }
            }
        }
    }


    /// <summary>
    /// Performes the Move / Copy / Link action.
    /// </summary>
    public void PerformAction()
    {
        switch (CurrentAction)
        {
            case Action.Move:
                titleElemAsync.TitleText = GetString("contentrequest.startmove");
                titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemove.png");
                // Process move
                RunAsync(Move);
                break;

            case Action.Copy:
                titleElemAsync.TitleText = GetString("contentrequest.startcopy");
                titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlecopy.png");
                // Process copy
                RunAsync(Copy);
                break;

            case Action.Link:
                titleElemAsync.TitleText = GetString("contentrequest.StartLink");
                titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
                // Process link
                RunAsync(Link);
                break;

            case Action.LinkDoc:
                titleElemAsync.TitleText = GetString("contentrequest.StartLink");
                titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");
                // Process link
                RunAsync(LinkDoc);
                break;
        }
    }

    #endregion


    #region "Help methods"

    /// <summary>
    /// When exception occures, log it to event log and add message to asnyc log.
    /// </summary>
    /// <param name="errorTitle">Error to add to async log</param>
    /// <param name="nodeId">ID of node which caused operation to fail</param>
    /// <param name="ex">Exception to log</param>
    /// <param name="siteId">Site identifier</param>
    /// <param name="eventCode">Code of event</param>
    private void LogExceptionToEventLog(string eventCode, string errorTitle, int nodeId, Exception ex, int siteId)
    {
        LogContext.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", eventCode, CurrentUser.UserID, CurrentUser.UserName, nodeId,
                          null, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), siteId,
                          HTTPHelper.GetAbsoluteUri(), HTTPHelper.MachineName, HTTPHelper
                          .GetUrlReferrer(), HTTPHelper.GetUserAgent());

        AddError(GetString(errorTitle) + " : " + EventLogProvider.GetExceptionLogMessage(ex));
    }


    /// <summary>
    /// Prepares IDs of nodes when action is performed for all documents under specified parent.
    /// </summary>
    private void PrepareNodeIdsForAllDocuments(string siteName)
    {
        if (!string.IsNullOrEmpty(parentAlias))
        {
            string where = "ClassName <> 'CMS.Root'";
            if (!string.IsNullOrEmpty(whereCondition))
            {
                where = SqlHelperClass.AddWhereCondition(where, whereCondition);
            }
            string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeParentID, DocumentName, NodeAliasPath, NodeLinkedNodeID");
            documentsToProcess = TreeProvider.SelectNodes(siteName, parentAlias.TrimEnd('/') + "/%", TreeProvider.ALL_CULTURES, true, null, where, null, 1, false, 0, columns);
            nodeIds.Clear();
            if (!DataHelper.DataSourceIsEmpty(documentsToProcess))
            {
                foreach (DataRow row in documentsToProcess.Tables[0].Rows)
                {
                    nodeIds.Add(ValidationHelper.GetInteger(row["NodeID"], 0));
                }
            }
        }
    }

    #endregion


    #region "Handling async thread"

    /// <summary>
    /// Runs async thread.
    /// </summary>
    /// <param name="action">Method to run</param>
    protected void RunAsync(AsyncAction action)
    {
        pnlLog.Visible = true;
        pnlGeneralTab.Visible = false;

        CurrentLog.Close();
        CurrentError = string.Empty;

        bool copyPerm = CopyPermissions && sameSite;

        AddScript("InitializeLog();");
        switch (CurrentAction)
        {
            case Action.Copy:
                ctlAsync.Parameter = ValidationHelper.GetBoolean(WindowHelper.GetItem(Action.Copy + underlying), false) + ";" + copyPerm;
                break;
            case Action.Link:
            case Action.LinkDoc:
                ctlAsync.Parameter = ValidationHelper.GetBoolean(WindowHelper.GetItem(Action.Link + underlying), false) + ";" + copyPerm;
                break;
            case Action.Move:
                ctlAsync.Parameter = copyPerm;
                break;
        }
        ctlAsync.RunAsync(action, WindowsIdentity.GetCurrent());
    }


    /// <summary>
    /// Adds the log information.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected override void AddLog(string newLog)
    {
        EnsureLog();
        LogContext.AppendLine(newLog);
    }


    /// <summary>
    /// Adds the error to collection of errors.
    /// </summary>
    /// <param name="error">Error message</param>
    protected override void AddError(string error)
    {
        AddLog(error);
        string separator = multiple ? "<br />" : "\n";
        CurrentError = (error + separator + CurrentError);
    }


    /// <summary>
    /// Ensures any error or info is displayed to user.
    /// </summary>
    /// <returns>True if error occurred.</returns>
    protected bool HandlePossibleErrors()
    {
        if (!string.IsNullOrEmpty(CurrentError))
        {
            if (multiple)
            {
                lblError.Text = CurrentError;
            }
            else
            {
                AddAlert(CurrentError);
            }
            ctlAsync.Log = CurrentLog.Log;
            return true;
        }
        return false;
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        // Perform actions necessary to cancel running action
        pnlLog.Visible = false;
        pnlGeneralTab.Visible = true;

        AddScript("var __pendingCallbacks = new Array();");
        DestroyLog();
        RefreshDialogTree();
        string refreshScript = ValidationHelper.GetString(ctlAsync.Parameter, string.Empty);
        if (!string.IsNullOrEmpty(refreshScript))
        {
            AddScript(refreshScript);
        }
        AddError(canceledString);
        HandlePossibleErrors();
        CurrentLog.Close();
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        // Handle error
        pnlLog.Visible = false;
        pnlGeneralTab.Visible = true;
        DestroyLog();
        RefreshContentTree();
        HandlePossibleErrors();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        DestroyLog();
        ClearSelection();
        if (HandlePossibleErrors())
        {
            // If error occurred
            RefreshContentTree();
            RefreshDialogTree();
        }
        else
        {
            string refreshScript = ValidationHelper.GetString(ctlAsync.Parameter, string.Empty);
            if (!string.IsNullOrEmpty(refreshScript))
            {
                AddScript(refreshScript);
            }
            CloseDialog();
        }
        // Finalize log context
        CurrentLog.Close();
    }

    #endregion


    #region "JavaScripts"

    /// <summary>
    /// Adds the alert message to the output request window.
    /// </summary>
    /// <param name="message">Message to display</param>
    private void AddAlert(string message)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), message.GetHashCode().ToString(), ScriptHelper.GetAlertScript(message));
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), ScriptHelper.GetScript(script));
    }


    /// <summary>
    /// Adds script to refresh dialog tree.
    /// </summary>
    private void RefreshDialogTree()
    {
        StringBuilder refTree = new StringBuilder();
        refTree.Append(@"
if(parent != null)
{
    if(parent.SetAction != null)
    {
        parent.SetAction('refreshtree', '", targetId, @"');
    }
    if(parent.RaiseHiddenPostBack != null)
    {
        parent.RaiseHiddenPostBack();
    }
}
        ");
        AddScript(refTree.ToString());
    }


    /// <summary>
    /// Adds script to destroy log.
    /// </summary>
    private void DestroyLog()
    {
        AddScript("DestroyLog();");
    }


    /// <summary>
    /// Adds script to refresh content tree.
    /// </summary>
    private void RefreshContentTree()
    {
        AddScript("TreeRefresh();");
    }


    /// <summary>
    /// Adds script to clear selection of listing.
    /// </summary>
    private void ClearSelection()
    {
        AddScript("ClearSelection();");
    }


    /// <summary>
    /// Adds script to close dialog.
    /// </summary>
    private void CloseDialog()
    {
        AddScript("window.top.close();");
    }

    #endregion
}