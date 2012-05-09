using System;
using System.Collections;
using System.Data;
using System.Security.Principal;
using System.Threading;
using System.Web;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.EventLog;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Delete : CMSContentPage
{
    #region "Private variables"

    private readonly ArrayList nodeIds = new ArrayList();
    private string[] nodeIdsArr = null;
    private int cancelNodeId = 0;
    private CurrentUserInfo currentUser = null;
    private SiteInfo currentSite = null;

    private static readonly Hashtable mErrors = new Hashtable();
    private Hashtable mParameters = null;

    private string currentCulture = CultureHelper.DefaultUICulture;

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
    private string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mErrors["DeleteError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mErrors["DeleteError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Indicates whether action is multiple.
    /// </summary>
    private static bool IsMultipleAction
    {
        get
        {
            return QueryHelper.GetBoolean("multiple", false);
        }
    }


    /// <summary>
    /// Where condition used for multiple actions.
    /// </summary>
    private string WhereCondition
    {
        get
        {
            string where = string.Empty;
            if (Parameters != null)
            {
                where = ValidationHelper.GetString(Parameters["where"], string.Empty);
            }
            return where;
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
        // Register script files
        ScriptHelper.RegisterCMS(this);
        ScriptHelper.RegisterScriptFile(this, "~/CMSModules/Content/CMSDesk/Operation.js");

        if (QueryHelper.ValidateHash("hash"))
        {
            // Set current UI culture
            currentCulture = CultureHelper.PreferredUICulture;
            // Initialize current user
            currentUser = CMSContext.CurrentUser;
            // Initialize current site
            currentSite = CMSContext.CurrentSite;

            // Initialize events
            ctlAsync.OnFinished += ctlAsync_OnFinished;
            ctlAsync.OnError += ctlAsync_OnError;
            ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
            ctlAsync.OnCancel += ctlAsync_OnCancel;

            if (!RequestHelper.IsCallback())
            {
                DataSet allDocs = null;
                TreeProvider tree = new TreeProvider(currentUser);
                btnCancel.Text = GetString("general.cancel");

                // Current Node ID to delete
                string parentAliasPath = string.Empty;
                if (Parameters != null)
                {
                    parentAliasPath = ValidationHelper.GetString(Parameters["parentaliaspath"], string.Empty);
                }
                if (string.IsNullOrEmpty(parentAliasPath))
                {
                    nodeIdsArr = QueryHelper.GetString("nodeid", string.Empty).Trim('|').Split(new char[] { '|' },
                                                                                               StringSplitOptions.
                                                                                                   RemoveEmptyEntries);
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
                    allDocs = tree.SelectNodes(currentSite.SiteName, parentAliasPath.TrimEnd(new char[] { '/' }) + "/%",
                                               TreeProvider.ALL_CULTURES, true, TreeProvider.ALL_CLASSNAMES, where,
                                               "DocumentName", TreeProvider.ALL_LEVELS, false, 0,
                                               TreeProvider.SELECTNODES_REQUIRED_COLUMNS + ",DocumentName,NodeParentID,NodeSiteID");

                    if (!DataHelper.DataSourceIsEmpty(allDocs))
                    {
                        foreach (DataTable table in allDocs.Tables)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                nodeIds.Add(ValidationHelper.GetInteger(row["NodeID"], 0));
                            }
                        }
                    }
                }

                // Setup page title text and image
                CurrentMaster.Title.TitleText = GetString("Content.DeleteTitle");
                CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/delete.png");

                btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

                // Register the dialog script
                ScriptHelper.RegisterDialogScript(this);

                titleElemAsync.TitleText = GetString("ContentDelete.DeletingDocuments");
                titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/delete.png");

                // Set visibility of panels
                pnlContent.Visible = true;
                pnlLog.Visible = false;

                // Set all cultures checkbox

                if (!CultureInfoProvider.IsSiteMultilignual(currentSite.SiteName))
                {
                    chkAllCultures.Checked = true;
                    chkAllCultures.Visible = false;
                }

                if (nodeIds.Count > 0)
                {
                    if (nodeIds.Count == 1)
                    {
                        int nodeId = ValidationHelper.GetInteger(nodeIds[0], 0);
                        TreeNode node = null;
                        if (string.IsNullOrEmpty(parentAliasPath))
                        {
                            // Get any culture if current not found
                            node = tree.SelectSingleNode(nodeId, currentUser.PreferredCultureCode) ??
                                   tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
                        }
                        else
                        {
                            if (allDocs != null)
                            {
                                DataRow dr = allDocs.Tables[0].Rows[0];
                                node = TreeNode.New(dr, ValidationHelper.GetString(dr["ClassName"], string.Empty),
                                                        tree);
                            }
                        }

                        if (node != null)
                        {
                            if (!IsUserAuthorizedToDeleteDocument(node))
                            {
                                pnlDelete.Visible = false;
                                lblError.Text = String.Format(GetString("cmsdesk.notauthorizedtodeletedocument"),
                                                              HTMLHelper.HTMLEncode(node.NodeAliasPath));
                            }

                            if (node.IsLink)
                            {
                                CurrentMaster.Title.TitleText = GetString("Content.DeleteTitleLink") + " \"" +
                                                                HTMLHelper.HTMLEncode(node.DocumentName) + "\"";
                                lblQuestion.Text = GetString("ContentDelete.QuestionLink");
                                chkAllCultures.Checked = true;
                                plcCheck.Visible = false;
                            }
                            else
                            {
                                string nodeName = HTMLHelper.HTMLEncode(node.DocumentName);
                                // Get name for root document
                                if (node.NodeClassName.ToLower() == "cms.root")
                                {
                                    nodeName = HTMLHelper.HTMLEncode(currentSite.DisplayName);
                                }
                                CurrentMaster.Title.TitleText = GetString("Content.DeleteTitle") + " \"" + nodeName +
                                                                "\"";
                                // If there is SKU
                                if (node.HasSKU)
                                {
                                    GeneralizedInfo product = ModuleCommands.ECommerceGetSKUInfo(node.NodeSKUID);
                                    if (product != null)
                                    {
                                        bool authorized = false;
                                        // Check if product is global
                                        if (product.GetValue("SKUSiteID") == null)
                                        {
                                            // Check EcommerceGlobalModify permission 
                                            authorized = currentUser.IsAuthorizedPerResource("CMS.Ecommerce", "EcommerceGlobalModify");
                                        }
                                        else
                                        {
                                            // Check ModifyProducts/EcommerceModify permission
                                            authorized = currentUser.IsAuthorizedPerResource("CMS.Ecommerce", "ModifyProducts") || currentUser.IsAuthorizedPerResource("CMS.Ecommerce", "EcommerceModify");
                                        }

                                        if (authorized)
                                        {
                                            pnlDeleteSKU.Visible = true;
                                            chkDeleteSKU.Visible = true;
                                        }
                                    }
                                }
                            }

                            // Show or hide checkbox
                            chkDestroy.Visible = CanDestroy(node);

                            cancelNodeId = IsMultipleAction ? node.NodeParentID : node.NodeID;
                        }
                        lblQuestion.Text = GetString("ContentDelete.Question");
                        chkAllCultures.Text = GetString("ContentDelete.AllCultures");
                        chkDestroy.Text = GetString("ContentDelete.Destroy");
                        chkDeleteSKU.Text = GetString("ContentDelete.SKU");
                    }
                    else if (nodeIds.Count > 1)
                    {
                        pnlDocList.Visible = true;
                        string where = "NodeID IN (";
                        foreach (int nodeID in nodeIds)
                        {
                            where += nodeID + ",";
                        }

                        where = where.TrimEnd(',') + ")";
                        DataSet ds = allDocs ??
                                     tree.SelectNodes(currentSite.SiteName, "/%", TreeProvider.ALL_CULTURES, true, null,
                                                      where, "DocumentName", -1, false);

                        if (!DataHelper.DataSourceIsEmpty(ds))
                        {
                            TreeNode node = null;
                            string docList = null;

                            if (string.IsNullOrEmpty(parentAliasPath))
                            {
                                cancelNodeId =
                                    ValidationHelper.GetInteger(
                                        DataHelper.GetDataRowValue(ds.Tables[0].Rows[0], "NodeParentID"), 0);
                            }
                            else
                            {
                                cancelNodeId = TreePathUtils.GetNodeIdByAliasPath(currentSite.SiteName, parentAliasPath);
                            }

                            bool canDestroy = true;

                            foreach (DataTable table in ds.Tables)
                            {
                                foreach (DataRow dr in table.Rows)
                                {
                                    bool isLink = (dr["NodeLinkedNodeID"] != DBNull.Value);
                                    string name = (string)dr["DocumentName"];
                                    docList += HTMLHelper.HTMLEncode(name);
                                    if (isLink)
                                    {
                                        docList += UIHelper.GetDocumentMarkImage(Page, DocumentMarkEnum.Link);
                                    }
                                    docList += "<br />";
                                    lblDocuments.Text = docList;

                                    // Set visibility of checkboxes
                                    node = TreeNode.New(dr,
                                                            ValidationHelper.GetString(dr["ClassName"], string.Empty));

                                    if (!IsUserAuthorizedToDeleteDocument(node))
                                    {
                                        pnlDelete.Visible = false;
                                        lblError.Text = String.Format(
                                            GetString("cmsdesk.notauthorizedtodeletedocument"),
                                            HTMLHelper.HTMLEncode(node.NodeAliasPath));
                                        break;
                                    }

                                    // Can destroy if "can destroy all previous AND current"
                                    canDestroy = CanDestroy(node) && canDestroy;

                                    if ((currentUser.IsAuthorizedPerResource("CMS.Ecommerce", "ModifyProducts") || currentUser.IsAuthorizedPerResource("CMS.Ecommerce", "EcommerceModify")) &&
                                        (node.HasSKU))
                                    {
                                        pnlDeleteSKU.Visible = true;
                                        chkDeleteSKU.Visible = true;
                                    }
                                }
                            }
                            chkDestroy.Visible = canDestroy;
                        }

                        lblQuestion.Text = GetString("ContentDelete.QuestionMultiple");
                        CurrentMaster.Title.TitleText = GetString("Content.DeleteTitleMultiple");
                        chkAllCultures.Text = GetString("ContentDelete.AllCulturesMultiple");
                        chkDestroy.Text = GetString("ContentDelete.DestroyMultiple");
                        chkDeleteSKU.Text = GetString("ContentDelete.SKUMultiple");
                    }
                    // If user has allowed cultures specified
                    if (currentUser.UserHasAllowedCultures)
                    {
                        // Get all site cultures
                        DataSet siteCultures = CultureInfoProvider.GetSiteCultures(currentSite.SiteName);
                        bool denyAllCulturesDeletion = false;
                        // Check that user can edit all site cultures
                        foreach (DataRow culture in siteCultures.Tables[0].Rows)
                        {
                            string cultureCode =
                                ValidationHelper.GetString(DataHelper.GetDataRowValue(culture, "CultureCode"),
                                                           string.Empty);
                            if (!currentUser.IsCultureAllowed(cultureCode, currentSite.SiteName))
                            {
                                denyAllCulturesDeletion = true;
                            }
                        }
                        // If user can't edit all site cultures
                        if (denyAllCulturesDeletion)
                        {
                            // Hide all cultures selector
                            chkAllCultures.Visible = false;
                            chkAllCultures.Checked = false;
                        }
                    }
                }
                else
                {
                    // Hide everything
                    pnlContent.Visible = false;
                }
            }
        }
        else
        {
            pnlDelete.Visible = false;
            lblError.Text = GetString("dialogs.badhashtext");

        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Set visibility of controls
        lblError.Visible = (lblError.Text != string.Empty);
        pnlDeleteSKU.Visible = chkDeleteSKU.Visible;
        plcCheck.Visible = chkAllCultures.Visible || chkDestroy.Visible;
        brSeparator.Visible = pnlDocList.Visible;

        btnNo.OnClientClick = "DisplayDocument(" + cancelNodeId + "); return false";

        string refreshCurrent = "function RefreshCurrent(){ RefreshTree(" + cancelNodeId + "," + cancelNodeId + "); }";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "refreshCurrent", ScriptHelper.GetScript(refreshCurrent));

        base.OnPreRender(e);
    }

    #endregion


    #region "Button actions"

    protected void btnOK_Click(object sender, EventArgs e)
    {
        pnlLog.Visible = true;
        pnlContent.Visible = false;

        CurrentError = string.Empty;
        CurrentLog.Close();
        EnsureLog();

        ctlAsync.Parameter = currentUser.PreferredCultureCode + ";" + currentSite.SiteName;
        ctlAsync.RunAsync(Delete, WindowsIdentity.GetCurrent());
    }

    #endregion


    #region "Async methods"

    /// <summary>
    /// Deletes document(s).
    /// </summary>
    private void Delete(object parameter)
    {
        if (parameter == null || nodeIds.Count < 1)
        {
            return;
        }

        if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Blogs, VersionActionEnum.Edit))
        {
            AddError(ResHelper.GetString("cmsdesk.blogdeletelicenselimitations", currentCulture));
            return;
        }

        if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Documents, VersionActionEnum.Edit))
        {
            AddError(ResHelper.GetString("cmsdesk.documentdeletelicenselimitations", currentCulture));
            return;
        }
        int refreshId = 0;

        TreeProvider tree = new TreeProvider(currentUser);
        tree.AllowAsyncActions = false;

        try
        {
            string[] parameters = ((string)parameter).Split(';');

            string siteName = parameters[1];

            // Prepare the where condition
            string where = SqlHelperClass.GetWhereCondition("NodeID", (int[])nodeIds.ToArray(typeof(int)));
            string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeAliasPath, ClassName, DocumentCulture, NodeParentID");

            bool combineWithDefaultCulture = chkAllCultures.Checked;
            string cultureCode = combineWithDefaultCulture ? TreeProvider.ALL_CULTURES : parameters[0];

            // Begin log
            AddLog(ResHelper.GetString("ContentDelete.DeletingDocuments", currentCulture));

            // Get the documents
            DataSet ds = tree.SelectNodes(siteName, "/%", cultureCode, combineWithDefaultCulture, null, where, "NodeAliasPath DESC", TreeProvider.ALL_LEVELS, false, 0, columns);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                // Delete the documents
                foreach (DataRow nodeRow in ds.Tables[0].Rows)
                {
                    // Get the current document
                    string className = nodeRow["ClassName"].ToString();
                    string aliasPath = nodeRow["NodeAliasPath"].ToString();
                    string docCulture = nodeRow["DocumentCulture"].ToString();
                    refreshId = ValidationHelper.GetInteger(nodeRow["NodeParentID"], 0);
                    if (refreshId == 0)
                    {
                        refreshId = ValidationHelper.GetInteger(nodeRow["NodeID"], 0);
                    }
                    TreeNode node = DocumentHelper.GetDocument(siteName, aliasPath, docCulture, false, className, null, null, TreeProvider.ALL_LEVELS, false, null, tree);

                    if (node == null)
                    {
                        AddLog(string.Format(ResHelper.GetString("ContentRequest.DocumentNoLongerExists", currentCulture), HTMLHelper.HTMLEncode(aliasPath)));
                        continue;
                    }

                    // Ensure current parent ID
                    int parentId = node.NodeParentID;

                    // Check delete permissions
                    if (IsUserAuthorizedToDeleteDocument(node) && (CanDestroy(node) || !chkDestroy.Checked))
                    {
                        // Delete the document
                        if (parentId <= 0)
                        {
                            parentId = node.NodeID;
                        }

                        // Delete document
                        refreshId = DocumentHelper.DeleteDocument(node, tree, chkAllCultures.Checked, chkDestroy.Checked, chkDeleteSKU.Checked) ? parentId : node.NodeID;
                    }
                    // Access denied - not authorized to delete the document
                    else
                    {
                        AddError(string.Format(ResHelper.GetString("cmsdesk.notauthorizedtodeletedocument", currentCulture), HTMLHelper.HTMLEncode(node.NodeAliasPath)));
                        continue;
                    }
                }
            }
            else
            {
                AddError(ResHelper.GetString("DeleteDocument.CultureNotExists", currentCulture));
                return;
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // When canceled
                AddError(ResHelper.GetString("DeleteDocument.DeletionCanceled", currentCulture));
            }
            else
            {
                // Log error
                LogExceptionToEventLog(ex);
            }
        }
        catch (Exception ex)
        {
            // Log error
            LogExceptionToEventLog(ex);
        }
        finally
        {
            if (string.IsNullOrEmpty(CurrentError))
            {
                // Refresh tree
                ctlAsync.Parameter = "RefreshTree(" + refreshId + ", " + refreshId + "); \n" + "DisplayDocument(" + refreshId + ");";
            }
            else
            {
                ctlAsync.Parameter = "RefreshTree(null, null);";
            }
        }
    }

    #endregion


    #region "Help methods"

    /// <summary>
    /// When exception occures, log it to event log.
    /// </summary>
    /// <param name="ex">Exception to log</param>
    private void LogExceptionToEventLog(Exception ex)
    {
        EventLogProvider log = new EventLogProvider();

        log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "DELETEDOC", currentUser.UserID, currentUser.UserName, 0, null, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), currentSite.SiteID, HTTPHelper.GetAbsoluteUri());

        AddError(ResHelper.GetString("ContentRequest.DeleteFailed", currentCulture) + ": " + ex.Message);
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
    /// Indicates whether specified node can be destroyed by current user.
    /// </summary>
    /// <param name="node">Tree node to check</param>
    private bool CanDestroy(TreeNode node)
    {
        return (currentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Destroy) == AuthorizationResultEnum.Allowed);
    }


    /// <summary>
    /// Checks whether the user is authorized to delete document.
    /// </summary>
    /// <param name="node">Document node</param>
    protected bool IsUserAuthorizedToDeleteDocument(TreeNode node)
    {
        // Check delete permission for document
        return (currentUser.IsAuthorizedPerDocument(node, new NodePermissionsEnum[] { NodePermissionsEnum.Delete, NodePermissionsEnum.Read }) == AuthorizationResultEnum.Allowed);
    }

    #endregion


    #region "Handling async thread"

    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        ctlAsync.Parameter = null;
        AddError(GetString("DeleteDocument.DeletionCanceled"));
        ltlScript.Text += ScriptHelper.GetScript("var __pendingCallbacks = new Array();RefreshCurrent();");
        lblError.Text = CurrentError;
        CurrentLog.Close();
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        if (ctlAsync.Status == AsyncWorkerStatusEnum.Running)
        {
            ctlAsync.Stop();
        }
        ctlAsync.Parameter = null;
        lblError.Text = CurrentError;
        CurrentLog.Close();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        lblError.Text = CurrentError;
        CurrentLog.Close();

        if (!string.IsNullOrEmpty(CurrentError))
        {
            ctlAsync.Parameter = null;
            lblError.Text = CurrentError;
        }

        if (ctlAsync.Parameter != null)
        {
            AddScript(ctlAsync.Parameter.ToString());

            // Do not set the window title anymore
            CurrentMaster.Title.SetWindowTitle = false;
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
