using System;
using System.Data;
using System.Collections;
using System.Security.Principal;
using System.Text;
using System.Threading;
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

public partial class CMSModules_Workflows_Workflow_Documents : SiteManagerPage
{
    #region "Private variables & enums"

    private int workflowId = 0;
    private CurrentUserInfo currentUser = null;
    private string currentCulture = CultureHelper.DefaultUICulture;

    private TreeProvider mTree = null;
    private static readonly Hashtable mInfos = new Hashtable();

    private const string GET_WORKFLOW_DOCS_WHERE = "DocumentWorkflowStepID IN (SELECT StepID FROM CMS_WorkflowStep WHERE StepWorkflowID = {0})";

    private enum Action
    {
        SelectAction = 0,
        PublishAndFinish = 1,
        RemoveWorkflow = 2
    }

    protected enum What
    {
        SelectedDocuments = 0,
        AllDocuments = 1
    }

    private What currentWhat = default(What);

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
    /// Tree provider.
    /// </summary>
    private TreeProvider Tree
    {
        get
        {
            return mTree ?? (mTree = new TreeProvider(currentUser));
        }
    }


    /// <summary>
    /// Current Error.
    /// </summary>
    private string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mInfos["WorkflowError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["WorkflowError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Current Info.
    /// </summary>
    private string CurrentInfo
    {
        get
        {
            return ValidationHelper.GetString(mInfos["WorkflowInfo_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["WorkflowInfo_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Gets or sets the cancel string.
    /// </summary>
    public string CanceledString
    {
        get
        {
            return ValidationHelper.GetString(mInfos["CanceledString_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["CanceledString_" + ctlAsync.ProcessGUID] = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;
        currentCulture = CultureHelper.PreferredUICulture;

        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;

        if (!RequestHelper.IsCallback())
        {
            // Set visibility of panels
            pnlContent.Visible = true;
            pnlLog.Visible = false;

            // Get current page template ID
            workflowId = QueryHelper.GetInteger("workflowid", 0);

            // Initialize unigrid
            docElem.ZeroRowsText = GetString(filterDocuments.FilterIsSet ? "unigrid.filteredzerorowstext" : "workflowdocuments.nodata");
            docElem.UniGrid.OnAfterDataReload += UniGrid_OnAfterDataReload;
            docElem.UniGrid.OnBeforeDataReload += UniGrid_OnBeforeDataReload;
            docElem.Tree = Tree;

            // Create action script
            StringBuilder actionScript = new StringBuilder();
            actionScript.AppendLine("function PerformAction(selectionFunction, selectionField, dropId)");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   var selectionFieldElem = document.getElementById(selectionField);");
            actionScript.AppendLine("   var label = document.getElementById('" + lblValidation.ClientID + "');");
            actionScript.AppendLine("   var items = selectionFieldElem.value;");
            actionScript.AppendLine("   var whatDrp = document.getElementById('" + drpWhat.ClientID + "');");
            actionScript.AppendLine("   var action = document.getElementById(dropId).value;");
            actionScript.AppendLine("   if (action == '" + (int)Action.SelectAction + "')");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       label.innerHTML = '" + GetString("massaction.selectsomeaction") + "';");
            actionScript.AppendLine("       return false;");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("   if(!eval(selectionFunction) || whatDrp.value == '" + (int)What.AllDocuments + "')");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       var confirmed = false;");
            actionScript.AppendLine("       var confMessage = '';");
            actionScript.AppendLine("       switch(action)");
            actionScript.AppendLine("       {");
            actionScript.AppendLine("           case '" + (int)Action.PublishAndFinish + "':");
            actionScript.AppendLine("               confMessage = '" + GetString("workflowdocuments.confrimpublish") + "';");
            actionScript.AppendLine("               break;");
            actionScript.AppendLine("           case '" + (int)Action.RemoveWorkflow + "':");
            actionScript.AppendLine("               confMessage = '" + GetString("workflowdocuments.confirmremove") + "';");
            actionScript.AppendLine("               break;");
            actionScript.AppendLine("       }");
            actionScript.AppendLine("       return confirm(confMessage);");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("   else");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       label.innerHTML = '" + GetString("documents.selectdocuments") + "';");
            actionScript.AppendLine("       return false;");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("}");
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "actionScript", ScriptHelper.GetScript(actionScript.ToString()));

            // Add action to button
            btnOk.OnClientClick = "return PerformAction('" + docElem.UniGrid.GetCheckSelectionScript() + "','" + docElem.UniGrid.GetSelectionFieldClientID() + "','" + drpAction.ClientID + "');";
            btnCancel.Text = GetString("general.cancel");
            btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

            // Initialize dropdown list with actions
            if (!RequestHelper.IsPostBack())
            {
                if (currentUser.IsGlobalAdministrator || currentUser.IsAuthorizedPerResource("CMS.Content", "manageworkflow"))
                {
                    drpAction.Items.Add(new ListItem(ResHelper.GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
                    drpAction.Items.Add(new ListItem(ResHelper.GetString("workflowdocuments." + Action.PublishAndFinish), Convert.ToInt32(Action.PublishAndFinish).ToString()));
                    drpAction.Items.Add(new ListItem(ResHelper.GetString("workflowdocuments." + Action.RemoveWorkflow), Convert.ToInt32(Action.RemoveWorkflow).ToString()));

                }

                drpWhat.Items.Add(new ListItem(ResHelper.GetString("contentlisting." + What.SelectedDocuments), Convert.ToInt32(What.SelectedDocuments).ToString()));
                drpWhat.Items.Add(new ListItem(ResHelper.GetString("contentlisting." + What.AllDocuments), Convert.ToInt32(What.AllDocuments).ToString()));
            }
        }
        docElem.SiteName = filterDocuments.SelectedSite;
    }

    #endregion


    #region "Other events"

    protected void UniGrid_OnBeforeDataReload()
    {
        string where = string.Format(GET_WORKFLOW_DOCS_WHERE, workflowId);
        where = SqlHelperClass.AddWhereCondition(where, filterDocuments.WhereCondition);
        docElem.UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(docElem.UniGrid.WhereCondition, where);
    }


    protected void UniGrid_OnAfterDataReload()
    {
        plcFilter.Visible = docElem.UniGrid.DisplayExternalFilter(filterDocuments.FilterIsSet);
        pnlFooter.Visible = !DataHelper.DataSourceIsEmpty(docElem.UniGrid.GridView.DataSource);
    }

    #endregion


    #region "Button handling"

    protected void btnOk_OnClick(object sender, EventArgs e)
    {
        pnlLog.Visible = true;
        pnlContent.Visible = false;

        CurrentError = string.Empty;
        CurrentLog.Close();
        EnsureLog();

        int actionValue = ValidationHelper.GetInteger(drpAction.SelectedValue, 0);
        Action action = (Action)actionValue;

        int whatValue = ValidationHelper.GetInteger(drpWhat.SelectedValue, 0);
        currentWhat = (What)whatValue;

        ctlAsync.Parameter = currentUser;
        switch (action)
        {
            case Action.PublishAndFinish:
                // Publish and finish workflow
                titleElemAsync.TitleText = GetString("content.publishingdocuments");
                titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Workflows/publish.png");
                ctlAsync.RunAsync(PublishAndFinish, WindowsIdentity.GetCurrent());
                break;

            case Action.RemoveWorkflow:
                // Remove workflow
                titleElemAsync.TitleText = GetString("workflowdocuments.removingwf");
                titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Workflows/removeworkflow.png");
                ctlAsync.RunAsync(RemoveWorkflow, WindowsIdentity.GetCurrent());
                break;
        }
    }

    #endregion


    #region "Thread methods"

    private void PublishAndFinish(object parameter)
    {
        TreeNode node = null;
        Tree.AllowAsyncActions = false;
        CanceledString = ResHelper.GetString("content.publishcanceled", currentCulture);
        try
        {
            // Begin log
            AddLog(ResHelper.GetString("content.preparingdocuments", currentCulture));

            // Get the documents
            DataSet documents = GetDocumentsToProcess();

            if (!DataHelper.DataSourceIsEmpty(documents))
            {
                // Create instance of workflow manager class
                WorkflowManager wm = new WorkflowManager(Tree);

                // Begin publishing
                AddLog(ResHelper.GetString("content.publishingdocuments", currentCulture));
                foreach (DataTable classTable in documents.Tables)
                {
                    foreach (DataRow nodeRow in classTable.Rows)
                    {
                        // Get the current document
                        string className = ValidationHelper.GetString(nodeRow["ClassName"], string.Empty);
                        string aliasPath = ValidationHelper.GetString(nodeRow["NodeAliasPath"], string.Empty);
                        string docCulture = ValidationHelper.GetString(nodeRow["DocumentCulture"], string.Empty);
                        string siteName = ValidationHelper.GetString(nodeRow["SiteName"], string.Empty);

                        node = DocumentHelper.GetDocument(siteName, aliasPath, docCulture, false, className, null, null, -1, false, null, Tree);

                        // Publish document
                        if (!Publish(node, wm))
                        {
                            // Add log record
                            AddLog(HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.GetValue("DocumentCulture") + ")"));
                        }
                        else
                        {
                            AddLog(string.Format(ResHelper.GetString("content.publishedalready"), HTMLHelper.HTMLEncode(node.NodeAliasPath + " (" + node.GetValue("DocumentCulture") + ")")));
                        }
                    }
                }
                CurrentInfo = GetString("workflowdocuments.publishcomplete");
            }
            else
            {
                AddError(ResHelper.GetString("content.nothingtopublish", currentCulture));
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // When canceled
                CurrentInfo = CanceledString;
            }
            else
            {
                int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
                // Log error
                LogExceptionToEventLog("PUBLISHDOC", "content.publishfailed", ex, siteId);
            }
        }
        catch (Exception ex)
        {
            int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
            // Log error
            LogExceptionToEventLog("PUBLISHDOC", "content.publishfailed", ex, siteId);
        }
    }


    private void RemoveWorkflow(object parameter)
    {
        VersionManager verMan = new VersionManager(Tree);
        TreeNode node = null;
        // Custom logging
        Tree.LogEvents = false;
        Tree.AllowAsyncActions = false;
        CanceledString = ResHelper.GetString("workflowdocuments.removingcanceled", currentCulture);
        try
        {
            // Begin log
            AddLog(ResHelper.GetString("content.preparingdocuments", currentCulture));

            // Get the documents
            DataSet documents = GetDocumentsToProcess();

            if (!DataHelper.DataSourceIsEmpty(documents))
            {
                // Begin log
                AddLog(ResHelper.GetString("workflowdocuments.removingwf", currentCulture));

                foreach (DataTable classTable in documents.Tables)
                {
                    foreach (DataRow nodeRow in classTable.Rows)
                    {
                        // Get the current document
                        string className = ValidationHelper.GetString(nodeRow["ClassName"], string.Empty);
                        string aliasPath = ValidationHelper.GetString(nodeRow["NodeAliasPath"], string.Empty);
                        string docCulture = ValidationHelper.GetString(nodeRow["DocumentCulture"], string.Empty);
                        string siteName = ValidationHelper.GetString(nodeRow["SiteName"], string.Empty);

                        // Get published version
                        node = Tree.SelectSingleNode(siteName, aliasPath, docCulture, false, className, false);
                        string encodedAliasPath = HTMLHelper.HTMLEncode(ValidationHelper.GetString(aliasPath, string.Empty) + " (" + node.GetValue("DocumentCulture") + ")");

                        // Destroy document history
                        verMan.DestroyDocumentHistory(node.DocumentID);

                        // Clear workflow
                        DocumentHelper.ClearWorkflowInformation(node);
                        node.Update();

                        // Add log record
                        AddLog(encodedAliasPath);

                        // Add record to eventlog
                        LogContext.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "Content", "REMOVEDOCWORKFLOW", currentUser.UserID,
                             currentUser.UserName, node.NodeID, node.DocumentName,
                             HTTPHelper.UserHostAddress, string.Format(GetString("workflowdocuments.removeworkflowsuccess"), encodedAliasPath),
                             node.NodeSiteID, HTTPHelper.GetAbsoluteUri(), HTTPHelper.MachineName, HTTPHelper.GetUrlReferrer(), HTTPHelper.GetUserAgent());
                    }
                }
                CurrentInfo = GetString("workflowdocuments.removecomplete");
            }
            else
            {
                AddError(ResHelper.GetString("workflowdocuments.nodocumentstoclear", currentCulture));
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // When canceled
                CurrentInfo = CanceledString;
            }
            else
            {
                int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
                // Log error
                LogExceptionToEventLog("REMOVEDOCWORKFLOW", "workflowdocuments.removefailed", ex, siteId);
            }
        }
        catch (Exception ex)
        {
            int siteId = (node != null) ? node.NodeSiteID : CMSContext.CurrentSiteID;
            // Log error
            LogExceptionToEventLog("REMOVEDOCWORKFLOW", "workflowdocuments.removefailed", ex, siteId);
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Gets a set of documents to be processed.
    /// </summary>
    /// <returns>Set of documents based on where condition and other settings</returns>
    private DataSet GetDocumentsToProcess()
    {
        string columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, "NodeAliasPath, ClassName, DocumentCulture, SiteName");
        return DocumentHelper.GetDocuments(TreeProvider.ALL_SITES, "/%", TreeProvider.ALL_CULTURES, false, TreeProvider.ALL_CLASSNAMES, GetCurrentWhere(), "NodeAliasPath DESC", -1, false, -1, columns, Tree);
    }


    /// <summary>
    /// Gets current where condition.
    /// </summary>
    /// <returns></returns>
    private string GetCurrentWhere()
    {
        ArrayList documentIds = docElem.UniGrid.SelectedItems;

        // Prepare the where condition
        string where = string.Empty;
        if (currentWhat == What.SelectedDocuments)
        {
            // Get where for selected documents
            where = "DocumentID IN (";
            foreach (object id in documentIds)
            {
                string documentId = ValidationHelper.GetString(id, string.Empty);
                where += SqlHelperClass.GetSafeQueryString(documentId, false) + ",";
            }
            where = where.TrimEnd(',') + ")";
        }
        else
        {
            if (!string.IsNullOrEmpty(filterDocuments.WhereCondition))
            {
                // Add filter condition
                where = SqlHelperClass.AddWhereCondition(where, filterDocuments.WhereCondition);
            }
            // Select documents only under current workflow
            where = SqlHelperClass.AddWhereCondition(where, string.Format(GET_WORKFLOW_DOCS_WHERE, workflowId));
        }
        return where;
    }


    /// <summary>
    /// When exception occures, log it to event log.
    /// </summary>
    /// <param name="eventCode">Code of event</param>
    /// <param name="errorTitle">Message to log to async log</param>
    /// <param name="ex">Exception to log</param>
    /// <param name="siteId">ID of site</param>
    private void LogExceptionToEventLog(string eventCode, string errorTitle, Exception ex, int siteId)
    {
        AddError(ResHelper.GetString(errorTitle, currentCulture) + ": " + ex.Message);
        LogContext.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", eventCode, currentUser.UserID,
                          currentUser.UserName, 0, null,
                          HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex),
                          siteId, HTTPHelper.GetAbsoluteUri(), HTTPHelper.MachineName, HTTPHelper.GetUrlReferrer(), HTTPHelper.GetUserAgent());
    }


    /// <summary>
    /// Publishes document.
    /// </summary>
    /// <param name="node">Node to publish</param>
    /// <param name="wm">Workflow manager</param>
    /// <returns>Whether node is already published</returns>
    private static bool Publish(TreeNode node, WorkflowManager wm)
    {
        WorkflowStepInfo currentStep = wm.GetStepInfo(node);
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

            // Remove possible checkout
            if (node.GetIntegerValue("DocumentCheckedOutByUserID") > 0)
            {
                TreeProvider.ClearCheckoutInformation(node);
                node.Update();
            }

            // Approve until the document is published
            while ((currentStep != null) && (currentStep.StepName.ToLower() != "published"))
            {
                currentStep = wm.MoveToNextStep(node, string.Empty);
                toReturn = false;
            }
        }
        return toReturn;
    }


    private void HandlePossibleError()
    {
        if (!string.IsNullOrEmpty(CurrentError))
        {
            lblError.Text = CurrentError;
            lblError.Visible = true;
        }
        if (!string.IsNullOrEmpty(CurrentInfo))
        {
            lblInfo.Text = CurrentInfo;
            lblInfo.Visible = true;
        }
        // Clear selection
        docElem.UniGrid.ResetSelection();
        CurrentLog.Close();
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


    #region "Handling async thread"

    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        CurrentInfo = CanceledString;
        ltlScript.Text += ScriptHelper.GetScript("var __pendingCallbacks = new Array();");
        HandlePossibleError();
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        HandlePossibleError();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        HandlePossibleError();
    }

    #endregion
}
