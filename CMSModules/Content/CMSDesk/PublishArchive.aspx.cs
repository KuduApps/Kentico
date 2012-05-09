using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

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

public partial class CMSModules_Content_CMSDesk_PublishArchive : CMSContentPage
{
    #region "Private variables & enums"

    private readonly List<int> nodeIds = new List<int>();
    private static readonly Hashtable mErrors = new Hashtable();
    private readonly Dictionary<int, string> list = new Dictionary<int, string>();
    private Hashtable mParameters = null;

    private int cancelNodeId = 0;
    private CurrentUserInfo currentUser = null;
    private string currentSiteName = null;
    private int currentSiteId = 0;
    private string currentCulture = CultureHelper.DefaultUICulture;
    private string canceledString = null;

    private Action mCurrentAction = Action.None;

    protected enum Action
    {
        None = 0,
        Publish = 1,
        Archive = 2
    }

    #endregion


    #region "Properties"

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
    /// Current Error.
    /// </summary>
    public string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mErrors["PublishArchiveError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mErrors["PublishArchiveError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Determines current action.
    /// </summary>
    private Action CurrentAction
    {
        get
        {
            if (mCurrentAction == Action.None)
            {
                string actionString = QueryHelper.GetString("action", Action.None.ToString());
                mCurrentAction = (Action)Enum.Parse(typeof(Action), actionString);
            }

            return mCurrentAction;
        }
    }


    /// <summary>
    /// Where condition used for multiple actions.
    /// </summary>
    private string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(Parameters["where"], string.Empty);
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
        // Register main CMS script file
        ScriptHelper.RegisterCMS(this);

        if (QueryHelper.ValidateHash("hash") && (Parameters != null))
        {
            // Initialize current user
            currentUser = CMSContext.CurrentUser;
            // Check permissions
            if (!currentUser.IsGlobalAdministrator &&
                !currentUser.IsAuthorizedPerResource("CMS.Content", "manageworkflow"))
            {
                RedirectToAccessDenied("CMS.Content", "manageworkflow");
            }
            // Set current UI culture
            currentCulture = CultureHelper.PreferredUICulture;
            // Initialize current site
            currentSiteName = CMSContext.CurrentSiteName;
            currentSiteId = CMSContext.CurrentSiteID;

            // Initialize events
            ctlAsync.OnFinished += ctlAsync_OnFinished;
            ctlAsync.OnError += ctlAsync_OnError;
            ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
            ctlAsync.OnCancel += ctlAsync_OnCancel;

            if (!IsCallback)
            {
                DataSet allDocs = null;
                TreeProvider tree = new TreeProvider(currentUser);

                // Current Node ID to delete
                string parentAliasPath = ValidationHelper.GetString(Parameters["parentaliaspath"], string.Empty);
                if (string.IsNullOrEmpty(parentAliasPath))
                {
                    // Get IDs of nodes
                    string nodeIdsString = ValidationHelper.GetString(Parameters["nodeids"], string.Empty);
                    string[] nodeIdsArr = nodeIdsString.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string nodeId in nodeIdsArr)
                    {
                        int id = ValidationHelper.GetInteger(nodeId, 0);
                        if (id != 0)
                        {
                            nodeIds.Add(id);
                        }
                    }
                }
                else
                {
                    string where = "ClassName <> 'CMS.Root'";
                    if (!string.IsNullOrEmpty(WhereCondition))
                    {
                        where = SqlHelperClass.AddWhereCondition(where, WhereCondition);
                    }
                    string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS,
                                                                 "NodeParentID, DocumentName,DocumentCheckedOutByUserID");
                    allDocs = tree.SelectNodes(currentSiteName, parentAliasPath.TrimEnd('/') + "/%",
                                               TreeProvider.ALL_CULTURES, true, null, where, "DocumentName", 1, false, 0,
                                               columns);

                    if (!DataHelper.DataSourceIsEmpty(allDocs))
                    {
                        foreach (DataRow row in allDocs.Tables[0].Rows)
                        {
                            nodeIds.Add(ValidationHelper.GetInteger(row["NodeID"], 0));
                        }
                    }
                }

                // Initialize strings based on current action
                switch (CurrentAction)
                {
                    case Action.Archive:
                        lblQuestion.ResourceString = "content.archivequestion";
                        chkAllCultures.ResourceString = "content.archiveallcultures";
                        chkUnderlying.ResourceString = "content.archiveunderlying";
                        canceledString = GetString("content.archivecanceled");

                        // Setup title of log
                        titleElemAsync.TitleText = GetString("content.archivingdocuments");
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/archive.png");

                        // Setup page title text and image
                        CurrentMaster.Title.TitleText = GetString("Content.ArchiveTitle");
                        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/archive.png");
                        break;

                    case Action.Publish:
                        lblQuestion.ResourceString = "content.publishquestion";
                        chkAllCultures.ResourceString = "content.publishallcultures";
                        chkUnderlying.ResourceString = "content.publishunderlying";
                        canceledString = GetString("content.publishcanceled");

                        // Setup title of log
                        titleElemAsync.TitleText = GetString("content.publishingdocuments");
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/publish.png");

                        // Setup page title text and image
                        CurrentMaster.Title.TitleText = GetString("Content.PublishTitle");
                        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/publish.png");
                        break;
                }
                if (nodeIds.Count == 0)
                {
                    // Hide if no node was specified
                    pnlContent.Visible = false;
                    return;
                }

                btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

                // Register the dialog script
                ScriptHelper.RegisterDialogScript(this);

                // Set visibility of panels
                pnlContent.Visible = true;
                pnlLog.Visible = false;

                // Set all cultures checkbox
                DataSet culturesDS = CultureInfoProvider.GetSiteCultures(currentSiteName);
                if ((DataHelper.DataSourceIsEmpty(culturesDS)) || (culturesDS.Tables[0].Rows.Count <= 1))
                {
                    chkAllCultures.Checked = true;
                    plcAllCultures.Visible = false;
                }

                if (nodeIds.Count > 0)
                {
                    pnlDocList.Visible = true;

                    // Create where condition
                    string where = SqlHelperClass.GetWhereCondition("NodeID", nodeIds.ToArray());
                    string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS,
                                                                 "NodeParentID, DocumentName,DocumentCheckedOutByUserID");

                    // Select nodes
                    DataSet ds = allDocs ??
                                 tree.SelectNodes(currentSiteName, "/%", TreeProvider.ALL_CULTURES, true, null, where,
                                                  "DocumentName", TreeProvider.ALL_LEVELS, false, 0, columns);

                    // Enumerate selected documents
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        cancelNodeId =
                            ValidationHelper.GetInteger(
                                DataHelper.GetDataRowValue(ds.Tables[0].Rows[0], "NodeParentID"), 0);

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            AddToList(dr);
                        }

                        // Display enumeration of documents
                        foreach (KeyValuePair<int, string> line in list)
                        {
                            lblDocuments.Text += line.Value;
                        }
                    }
                }
            }
        }
        else
        {
            pnlPublish.Visible = false;
            lblError.Text = GetString("dialogs.badhashtext");
        }
    }


    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = string.Empty;
        base.OnPreInit(e);
    }


    protected override void OnPreRender(EventArgs e)
    {
        lblError.Visible = (lblError.Text != string.Empty);
        base.OnPreRender(e);
    }

    #endregion


    #region "Button actions"

    protected void btnNo_Click(object sender, EventArgs e)
    {
        // Go back to listing
        AddScript("DisplayDocument(" + cancelNodeId + ");");
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        pnlLog.Visible = true;
        pnlContent.Visible = false;

        EnsureLog();
        CurrentError = string.Empty;

        ctlAsync.Parameter = currentUser.PreferredCultureCode + ";" + CMSContext.CurrentSiteName;
        switch (CurrentAction)
        {
            case Action.Publish:
                ctlAsync.RunAsync(PublishAll, WindowsIdentity.GetCurrent());
                break;

            case Action.Archive:
                ctlAsync.RunAsync(ArchiveAll, WindowsIdentity.GetCurrent());
                break;
        }
    }

    #endregion


    #region "Async methods"

    /// <summary>
    /// Archives document(s).
    /// </summary>
    private void ArchiveAll(object parameter)
    {
        if (parameter == null)
        {
            return;
        }

        TreeProvider tree = new TreeProvider(currentUser);
        tree.AllowAsyncActions = false;

        try
        {
            // Begin log
            AddLog(ResHelper.GetString("content.archivingdocuments", currentCulture));

            string[] parameters = ((string)parameter).Split(';');

            string siteName = parameters[1];

            // Get identifiers
            int[] workNodes = nodeIds.ToArray();

            // Prepare the where condition
            string where = SqlHelperClass.GetWhereCondition("NodeID", workNodes);
            string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeAliasPath, ClassName, DocumentCulture");

            // Get cultures
            string cultureCode = chkAllCultures.Checked ? TreeProvider.ALL_CULTURES : parameters[0];

            // Get the documents
            DataSet documents = tree.SelectNodes(siteName, "/%", cultureCode, false, null, where, "NodeAliasPath DESC", TreeProvider.ALL_LEVELS, false, 0, columns);

            // Create instance of workflow manager class
            WorkflowManager wm = new WorkflowManager(tree);

            if (!DataHelper.DataSourceIsEmpty(documents))
            {
                foreach (DataRow nodeRow in documents.Tables[0].Rows)
                {
                    // Get the current document

                    string aliasPath = ValidationHelper.GetString(nodeRow["NodeAliasPath"], string.Empty);
                    TreeNode node = GetDocument(tree, siteName, nodeRow);

                    if (PerformArchive(wm, tree, node, aliasPath))
                    {
                        return;
                    }

                    // Process underlying documents
                    if (chkUnderlying.Checked && (node.NodeChildNodesCount > 0))
                    {
                        if (ArchiveSubDocuments(node, tree, wm, cultureCode, siteName))
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                AddError(ResHelper.GetString("content.nothingtoarchive", currentCulture));
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state != CMSThread.ABORT_REASON_STOP)
            {
                // Log error
                LogExceptionToEventLog(ResHelper.GetString("content.archivefailed", currentCulture), ex);
            }
        }
        catch (Exception ex)
        {
            // Log error
            LogExceptionToEventLog(ResHelper.GetString("content.archivefailed", currentCulture), ex);
        }
    }


    /// <summary>
    /// Archives sub documents of given node.
    /// </summary>
    /// <param name="parentNode">Parent node</param>
    /// <param name="tree">Tree provider</param>
    /// <param name="wm">Workflow manager</param>
    /// <param name="cultureCode">Culture code</param>
    /// <param name="siteName">Site name</param>
    /// <returns>TRUE if operation fails and whole process should be canceled.</returns>
    private bool ArchiveSubDocuments(TreeNode parentNode, TreeProvider tree, WorkflowManager wm, string cultureCode, string siteName)
    {
        DataSet subDocuments = GetSubDocuments(parentNode, tree, cultureCode, siteName);

        if (!DataHelper.DataSourceIsEmpty(subDocuments))
        {
            foreach (DataRow nodeRow in subDocuments.Tables[0].Rows)
            {
                // Get the current document

                string aliasPath = ValidationHelper.GetString(nodeRow["NodeAliasPath"], string.Empty);
                if (PerformArchive(wm, tree, GetDocument(tree, siteName, nodeRow), aliasPath))
                {
                    return true;
                }
            }
        }
        return false;
    }


    /// <summary>
    /// Publishes document(s).
    /// </summary>
    private void PublishAll(object parameter)
    {
        if (parameter == null)
        {
            return;
        }

        TreeProvider tree = new TreeProvider(currentUser);
        tree.AllowAsyncActions = false;

        try
        {
            // Begin log
            AddLog(ResHelper.GetString("content.publishingdocuments", currentCulture));

            string[] parameters = ((string)parameter).Split(';');

            string siteName = parameters[1];

            // Get identifiers
            int[] workNodes = nodeIds.ToArray();

            // Prepare the where condition
            string where = SqlHelperClass.GetWhereCondition("NodeID", workNodes);
            string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeAliasPath, ClassName, DocumentCulture");

            // Get cultures
            string cultureCode = chkAllCultures.Checked ? TreeProvider.ALL_CULTURES : parameters[0];

            // Get the documents
            DataSet documents = tree.SelectNodes(siteName, "/%", cultureCode, false, null, where, "NodeAliasPath DESC", TreeProvider.ALL_LEVELS, false, 0, columns);

            // Create instance of workflow manager class
            WorkflowManager wm = new WorkflowManager(tree);

            if (!DataHelper.DataSourceIsEmpty(documents))
            {
                foreach (DataRow nodeRow in documents.Tables[0].Rows)
                {
                    // Get the current document
                    TreeNode node = GetDocument(tree, siteName, nodeRow);

                    // Publish document
                    if (PerformPublish(wm, tree, siteName, nodeRow))
                    {
                        return;
                    }

                    // Process underlying documents
                    if (chkUnderlying.Checked && (node.NodeChildNodesCount > 0))
                    {
                        if (PublishSubDocuments(node, tree, wm, cultureCode, siteName))
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                AddError(ResHelper.GetString("content.nothingtopublish", currentCulture));
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state != CMSThread.ABORT_REASON_STOP)
            {
                // Log error
                LogExceptionToEventLog(ResHelper.GetString("content.publishfailed", currentCulture), ex);
            }
        }
        catch (Exception ex)
        {
            // Log error
            LogExceptionToEventLog(ResHelper.GetString("content.publishfailed", currentCulture), ex);
        }
    }


    /// <summary>
    /// Publishes sub documents of given node.
    /// </summary>
    /// <param name="parentNode">Parent node</param>
    /// <param name="tree">Tree provider</param>
    /// <param name="wm">Workflow manager</param>
    /// <param name="cultureCode">Culture code</param>
    /// <param name="siteName">Site name</param>
    /// <returns>TRUE if operation fails and whole process should be canceled.</returns>
    private bool PublishSubDocuments(TreeNode parentNode, TreeProvider tree, WorkflowManager wm, string cultureCode, string siteName)
    {
        // Get sub documents
        DataSet subDocuments = GetSubDocuments(parentNode, tree, cultureCode, siteName);
        if (!DataHelper.DataSourceIsEmpty(subDocuments))
        {
            foreach (DataRow nodeRow in subDocuments.Tables[0].Rows)
            {
                if (PerformPublish(wm, tree, siteName, nodeRow))
                {
                    return true;
                }
            }
        }
        return false;
    }


    /// <summary>
    /// Performs necessary checks and publishes document.
    /// </summary>
    /// <returns>TRUE if operation fails and whole process should be canceled.</returns>
    private bool PerformPublish(WorkflowManager wm, TreeProvider tree, string siteName, DataRow nodeRow)
    {
        string aliasPath = ValidationHelper.GetString(nodeRow["NodeAliasPath"], string.Empty);
        return PerformPublish(wm, tree, GetDocument(tree, siteName, nodeRow), aliasPath);
    }


    /// <summary>
    /// Performs necessary checks and publishes document.
    /// </summary>
    /// <returns>TRUE if operation fails and whole process should be canceled.</returns>
    private bool PerformPublish(WorkflowManager wm, TreeProvider tree, TreeNode node, string aliasPath)
    {
        if (node != null)
        {
            if (currentUser.UserHasAllowedCultures && !currentUser.IsCultureAllowed(node.DocumentCulture, node.NodeSiteName))
            {
                AddLog(string.Format(GetString("content.notallowedtomodifycultureversion"), node.DocumentCulture, node.NodeAliasPath));
            }
            else
            {
                if (!UndoPossibleCheckOut(tree, node))
                {
                    return true;
                }

                WorkflowStepInfo currentStep = null;
                try
                {
                    // Try to get workflow scope
                    currentStep = wm.GetStepInfo(node);
                }
                catch
                {
                    AddLog(string.Format(ResHelper.GetString("content.publishnowf"), HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.DocumentCulture + ")")));
                    return false;
                }

                if (currentStep != null)
                {
                    string pathCulture = HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.DocumentCulture + ")");

                    // Publish document
                    if (Publish(node, wm, currentStep))
                    {
                        pathCulture = string.Format(ResHelper.GetString("content.publishedalready"), pathCulture);
                    }

                    // Add log record
                    AddLog(pathCulture);
                }
            }
            return false;
        }
        else
        {
            AddLog(string.Format(ResHelper.GetString("ContentRequest.DocumentNoLongerExists", currentCulture), HTMLHelper.HTMLEncode(aliasPath)));
            return false;
        }
    }


    /// <summary>
    /// Performs necessary checks and archives document.
    /// </summary>
    /// <returns>TRUE if operation fails and whole process should be canceled.</returns>
    private bool PerformArchive(WorkflowManager wm, TreeProvider tree, TreeNode node, string aliasPath)
    {
        if (node != null)
        {
            if (!UndoPossibleCheckOut(tree, node))
            {
                return true;
            }
            try
            {
                if (currentUser.UserHasAllowedCultures && !currentUser.IsCultureAllowed(node.DocumentCulture, node.NodeSiteName))
                {
                    AddLog(string.Format(GetString("content.notallowedtomodifycultureversion"), node.DocumentCulture, node.NodeAliasPath));
                }
                else
                {
                    // Add log record
                    AddLog(HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.DocumentCulture + ")"));

                    // Archive document
                    wm.ArchiveDocument(node, string.Empty);
                }
            }
            catch
            {
                AddLog(string.Format(ResHelper.GetString("content.archivenowf"), HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.DocumentCulture + ")")));
            }
            return false;
        }
        else
        {
            AddLog(string.Format(ResHelper.GetString("ContentRequest.DocumentNoLongerExists", currentCulture), HTMLHelper.HTMLEncode(aliasPath)));
            return false;
        }
    }


    /// <summary>
    /// Publishes document.
    /// </summary>
    /// <param name="node">Node to publish</param>
    /// <param name="wm">Workflow manager</param>
    /// <param name="currentStep">Current workflow step</param>
    /// <returns>Whether node is already published</returns>
    private static bool Publish(TreeNode node, WorkflowManager wm, WorkflowStepInfo currentStep)
    {
        bool toReturn = true;
        if (currentStep != null)
        {
            // For archive step start new version
            if (currentStep.StepName.ToLower() == "archived")
            {
                VersionManager vm = new VersionManager(node.TreeProvider);
                currentStep = vm.CheckOut(node, node.IsPublished, true);
                vm.CheckIn(node, null, null);
            }

            // Approve until the step is publish
            while ((currentStep != null) && (currentStep.StepName.ToLower() != "published"))
            {
                currentStep = wm.MoveToNextStep(node, string.Empty);
                toReturn = false;
            }

            // Document is already published, check if still under workflow
            if (toReturn && (currentStep.StepName.ToLower() == "published"))
            {
                WorkflowScopeInfo wsi = wm.GetNodeWorkflowScope(node);
                if (wsi == null)
                {
                    DocumentHelper.ClearWorkflowInformation(node);
                    VersionManager vm = new VersionManager(node.TreeProvider);
                    vm.RemoveWorkflow(node);
                }
            }
        }

        return toReturn;
    }


    /// <summary>
    /// Undoes checkout for given node.
    /// </summary>
    /// <param name="tree">Tree provider</param>
    /// <param name="node">Node to undo checkout</param>
    /// <returns>FALSE when document is checked out and checkbox for undoing checkout is not checked</returns>
    private bool UndoPossibleCheckOut(TreeProvider tree, TreeNode node)
    {
        if (node.IsCheckedOut)
        {
            if (chkUndoCheckOut.Checked)
            {
                node.SetValue("DocumentCheckedOutByUserID", null);
                node.SetValue("DocumentCheckedOutAutomatically", null);
                node.SetValue("DocumentCheckedOutWhen", null);
                DocumentHelper.UpdateDocument(node, tree);
            }
            else
            {
                AddError(string.Format(ResHelper.GetString("content.checkedoutdocument"), HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.GetValue("DocumentCulture") + ")")));
                return false;
            }
        }
        return true;
    }

    #endregion


    #region "Help methods"

    private static TreeNode GetDocument(TreeProvider tree, string siteName, DataRow nodeRow)
    {
        // Get the current document
        string className = ValidationHelper.GetString(nodeRow["ClassName"], string.Empty);
        string aliasPath = ValidationHelper.GetString(nodeRow["NodeAliasPath"], string.Empty);
        string docCulture = ValidationHelper.GetString(nodeRow["DocumentCulture"], string.Empty);
        return DocumentHelper.GetDocument(siteName, aliasPath, docCulture, false, className, null, null, TreeProvider.ALL_LEVELS, false, null, tree);
    }


    private static DataSet GetSubDocuments(TreeNode parentNode, TreeProvider tree, string cultureCode, string siteName)
    {
        string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeAliasPath, ClassName, DocumentCulture");
        // Get subdocuments
        return tree.SelectNodes(siteName, parentNode.NodeAliasPath + "/%", cultureCode, false, null, null, null, TreeProvider.ALL_LEVELS, false, 0, columns);
    }

    /// <summary>
    /// When exception occures, log it to event log.
    /// </summary>
    /// <param name="messageTitle">Title message</param>
    /// <param name="ex">Exception to log</param>
    private void LogExceptionToEventLog(string messageTitle, Exception ex)
    {
        AddError(messageTitle + ": " + ex.Message);
        LogContext.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "PUBLISHDOC", currentUser.UserID,
                     currentUser.UserName, 0, null,
                     HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex),
                     currentSiteId, HTTPHelper.GetAbsoluteUri(), HTTPHelper.MachineName, HTTPHelper.GetUrlReferrer(), HTTPHelper.GetUserAgent());
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ltlScript.Text += ScriptHelper.GetScript(script);
    }


    /// <summary>
    /// Adds document to list.
    /// </summary>
    /// <param name="dr">Data row with document</param>
    private void AddToList(DataRow dr)
    {
        int nodeId = ValidationHelper.GetInteger(dr["NodeID"], 0);
        int linkedNodeId = ValidationHelper.GetInteger(dr["NodeLinkedNodeID"], 0);
        int documentCheckedOutByUserID = ValidationHelper.GetInteger(dr["DocumentCheckedOutByUserID"], 0);
        string name = HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["DocumentName"], string.Empty));
        if (documentCheckedOutByUserID != 0)
        {
            name += " " + UIHelper.GetDocumentMarkImage(Page, DocumentMarkEnum.CheckedOut);
        }
        if (linkedNodeId != 0)
        {
            name += " " + UIHelper.GetDocumentMarkImage(Page, DocumentMarkEnum.Link);
        }
        name += "<br />";
        list[nodeId] = name;
    }


    private void HandlePossibleError()
    {
        if (!string.IsNullOrEmpty(CurrentError))
        {
            lblError.Text = CurrentError;
            lblError.Visible = true;
        }
        // Clear selection
        CurrentLog.Close();
    }

    #endregion


    #region "Handling async thread"

    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        AddScript("var __pendingCallbacks = new Array();");
        RefreshTree();
        AddError(canceledString);
        HandlePossibleError();
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        RefreshTree();
        HandlePossibleError();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(CurrentError))
        {
            AddScript("DisplayDocument(" + cancelNodeId + ");");
        }
        else
        {
            RefreshTree();
        }
        HandlePossibleError();
    }


    /// <summary>
    /// Refreshes content tree.
    /// </summary>
    private void RefreshTree()
    {
        if (cancelNodeId != 0)
        {
            AddScript("RefreshTree(" + cancelNodeId + ");");
        }
    }


    /// <summary>
    /// Ensures the logging context.
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext log = LogContext.EnsureLog(ctlAsync.ProcessGUID);
        log.Reversed = true;
        log.LineSeparator = "<br />";
        return log;
    }


    /// <summary>
    /// Adds the log information.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected void AddLog(string newLog)
    {
        EnsureLog();
        LogContext.AppendLine(newLog);
    }


    /// <summary>
    /// Adds the error to collection of errors.
    /// </summary>
    /// <param name="error">Error message</param>
    protected void AddError(string error)
    {
        AddLog(error);
        CurrentError = (error + "<br />" + CurrentError);
    }

    #endregion
}
