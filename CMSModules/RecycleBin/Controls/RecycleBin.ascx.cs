using System;
using System.Collections;
using System.Data;
using System.Security.Principal;
using System.Threading;
using System.Web.UI.WebControls;
using System.Text;

using CMS.CMSHelper;
using CMS.EventLog;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.DataEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_RecycleBin_Controls_RecycleBin : CMSUserControl
{
    #region "Private variables"

    private bool mIsSingleSite = true;
    private string mSiteName = String.Empty;
    private string currentCulture = CultureHelper.DefaultUICulture;
    private CurrentUserInfo currentUser = null;
    private SiteInfo currentSite = null;
    private string mDocumentType = String.Empty;
    private bool mRestrictUsers = true;
    private SiteInfo mSelectedSite = null;

    private static readonly Hashtable mInfos = new Hashtable();
    private string mOrderBy = "VersionDeletedWhen DESC";
    private string mDocumentAge = String.Empty;
    private string mDocumentName = String.Empty;
    private string mItemsPerPage = String.Empty;
    private What currentWhat = default(What);

    #endregion


    #region "Structures"

    /// <summary>
    /// Structure that holds settings for async operations.
    /// </summary>
    struct BinSettingsContainer
    {
        public CurrentUserInfo User { get; private set; }

        public What CurrentWhat { get; private set; }

        public BinSettingsContainer(CurrentUserInfo user, What what)
            : this()
        {
            User = user;
            CurrentWhat = what;
        }
    }

    #endregion


    #region "Enumerations"

    protected enum Action
    {
        SelectAction = 0,
        Restore = 1,
        Delete = 2
    }

    protected enum What
    {
        SelectedDocuments = 0,
        AllDocuments = 1
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Depends on site name set.
    /// </summary>
    public bool IsSingleSite
    {
        get
        {
            return mIsSingleSite;
        }
        set
        {
            mIsSingleSite = value;
        }
    }


    /// <summary>
    /// Gets number of document age conditions.
    /// </summary>
    protected int AgeModifiersCount
    {
        get
        {
            int count = 0;
            if (!String.IsNullOrEmpty(DocumentAge))
            {
                string[] ages = DocumentAge.Split(';');
                if (ages.Length == 2)
                {
                    if (ValidationHelper.GetInteger(ages[1], 0) > 0)
                    {
                        count++;
                    }

                    if (ValidationHelper.GetInteger(ages[0], 0) > 0)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }


    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return mSiteName;
        }
        set
        {
            mSiteName = value;
            SiteInfo siteInfo = SiteInfoProvider.GetSiteInfo(mSiteName);
            if (siteInfo != null)
            {
                mSelectedSite = siteInfo;
            }
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
    /// Current Error.
    /// </summary>
    public string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mInfos["RestoreError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["RestoreError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Current Info.
    /// </summary>
    public string CurrentInfo
    {
        get
        {
            return ValidationHelper.GetString(mInfos["RestoreInfo_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["RestoreInfo_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Order by for grid.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return mOrderBy;
        }

        set
        {
            mOrderBy = value;
        }
    }


    /// <summary>
    /// Filter by document type.
    /// </summary>
    public string DocumentType
    {
        get
        {
            return mDocumentType;
        }

        set
        {
            mDocumentType = value;
        }
    }


    /// <summary>
    /// Items per page.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return mItemsPerPage;
        }
        set
        {
            mItemsPerPage = value;
        }
    }


    /// <summary>
    /// Age of documents in days.
    /// </summary>
    public string DocumentAge
    {
        get
        {
            return mDocumentAge;
        }
        set
        {
            mDocumentAge = value;
        }
    }


    /// <summary>
    /// Document name for grid filetr.
    /// </summary>
    public string DocumentName
    {
        get
        {
            return mDocumentName;
        }
        set
        {
            mDocumentName = value;
        }
    }


    /// <summary>
    /// Indicates if restrictions should be applied on users displayed in filter.
    /// </summary>
    public bool RestrictUsers
    {
        get
        {
            return mRestrictUsers;
        }
        set
        {
            mRestrictUsers = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the main CMS script
        ScriptHelper.RegisterCMS(Page);

        if (StopProcessing)
        {
            ugRecycleBin.StopProcessing = true;
            return;
        }

        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(Page);

        // Set current UI culture
        currentCulture = CultureHelper.PreferredUICulture;

        // Get current user info
        currentUser = CMSContext.CurrentUser;

        if (!RequestHelper.IsCallback())
        {
            ControlsHelper.RegisterPostbackControl(btnOk);
            // Create action script
            StringBuilder actionScript = new StringBuilder();
            actionScript.AppendLine("function PerformAction(selectionFunction, selectionField, dropId, validationLabel, whatId)");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   var selectionFieldElem = document.getElementById(selectionField);");
            actionScript.AppendLine("   var label = document.getElementById(validationLabel);");
            actionScript.AppendLine("   var items = selectionFieldElem.value;");
            actionScript.AppendLine("   var whatDrp = document.getElementById(whatId);");
            actionScript.AppendLine("   var allDocs = whatDrp.value == '" + (int)What.AllDocuments + "';");
            actionScript.AppendLine("   var action = document.getElementById(dropId).value;");
            actionScript.AppendLine("   if (action == '" + (int)Action.SelectAction + "')");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       label.innerHTML = '" + GetString("massaction.selectsomeaction") + "';");
            actionScript.AppendLine("       label.style.display = 'block';");
            actionScript.AppendLine("       return false;");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("   if(!eval(selectionFunction) || allDocs)");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       var confirmed = false;");
            actionScript.AppendLine("       var confMessage = '';");
            actionScript.AppendLine("       switch(action)");
            actionScript.AppendLine("       {");
            actionScript.AppendLine("           case '" + (int)Action.Restore + "':");
            actionScript.AppendLine("               confMessage = '" + GetString("recyclebin.confirmrestores") + "';");
            actionScript.AppendLine("               break;");
            actionScript.AppendLine("           case '" + (int)Action.Delete + "':");
            actionScript.AppendLine("               confMessage = allDocs ?  '" + GetString("recyclebin.confirmemptyrecbin") + "' : '" + GetString("recyclebin.confirmdeleteselected") + "';");
            actionScript.AppendLine("               break;");
            actionScript.AppendLine("       }");
            actionScript.AppendLine("       return confirm(confMessage);");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("   else");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       label.innerHTML = '" + GetString("documents.selectdocuments") + "';");
            actionScript.AppendLine("       label.style.display = 'block';");
            actionScript.AppendLine("       return false;");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("}");
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "recycleBinScript", ScriptHelper.GetScript(actionScript.ToString()));

            // Set page size
            int itemsPerPage = ValidationHelper.GetInteger(ItemsPerPage, 0);
            if ((itemsPerPage > 0) && !RequestHelper.IsPostBack())
            {
                ugRecycleBin.Pager.DefaultPageSize = itemsPerPage;
            }

            // Add action to button
            btnOk.OnClientClick = "return PerformAction('" + ugRecycleBin.GetCheckSelectionScript() + "','" + ugRecycleBin.GetSelectionFieldClientID() + "','" + drpAction.ClientID + "','" + lblValidation.ClientID + "', '" + drpWhat.ClientID + "');";

            // Initialize dropdown lists
            if (!RequestHelper.IsPostBack())
            {
                drpAction.Items.Add(new ListItem(GetString("general." + Action.Restore), Convert.ToInt32(Action.Restore).ToString()));
                drpAction.Items.Add(new ListItem(GetString("general." + Action.Delete), Convert.ToInt32(Action.Delete).ToString()));

                drpWhat.Items.Add(new ListItem(GetString("contentlisting." + What.SelectedDocuments), Convert.ToInt32(What.SelectedDocuments).ToString()));
                drpWhat.Items.Add(new ListItem(GetString("contentlisting." + What.AllDocuments), Convert.ToInt32(What.AllDocuments).ToString()));

                ugRecycleBin.OrderBy = OrderBy;
            }

            PrepareGrid();

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(Page);

            // Register script for viewing versions
            string viewVersionScript = "function ViewVersion(versionHistoryId) {modalDialog('" + ResolveUrl("~/CMSModules/RecycleBin/Pages/ViewVersion.aspx") + "?noCompare=1&versionHistoryId=' + versionHistoryId, 'contentversion', 900, 600);}";
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "viewVersionScript", ScriptHelper.GetScript(viewVersionScript));

            // Initialize buttons
            btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

            string error = QueryHelper.GetString("displayerror", String.Empty);
            if (error != String.Empty)
            {
                lblError.Text = GetString("recyclebin.errorsomenotdestroyed");
            }

            // Set visibility of panels
            pnlLog.Visible = false;
        }
        else
        {
            ugRecycleBin.StopProcessing = true;
        }

        // Set filter data
        filterBin.SiteID = (mSelectedSite != null) ? mSelectedSite.SiteID : UniSelector.US_ALL_RECORDS;
        filterBin.ReloadData();

        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;
    }


    private void PrepareGrid()
    {
        string where = null;
        int siteId = (mSelectedSite != null) ? mSelectedSite.SiteID : 0;
        DateTime modifiedFrom = DateTimeHelper.ZERO_TIME;
        DateTime modifiedTo = DateTimeHelper.ZERO_TIME;
        SetDocumentAge(ref modifiedFrom, ref modifiedTo);
        bool modifiedFromSet = (modifiedFrom != DateTimeHelper.ZERO_TIME);
        bool modifiedToSet = (modifiedTo != DateTimeHelper.ZERO_TIME);

        // Prepare the parameters
        QueryDataParameters parameters = new QueryDataParameters();

        parameters.Add("@SiteID", siteId);

        if (modifiedFromSet)
        {
            parameters.Add("@FROM", modifiedFrom);
        }
        if (modifiedToSet)
        {
            parameters.Add("@TO", modifiedTo);
        }

        if (modifiedFromSet)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ModifiedWhen >= @FROM ");
        }
        if (modifiedToSet)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ModifiedWhen <= @TO ");
        }

        ugRecycleBin.QueryParameters = parameters;
        ugRecycleBin.WhereCondition = GetWhereCondition(where);
        ugRecycleBin.HideControlForZeroRows = false;
        ugRecycleBin.OnExternalDataBound += ugRecycleBin_OnExternalDataBound;
        ugRecycleBin.OnAction += ugRecycleBin_OnAction;

        // If filter is set
        if (filterBin.FilterIsSet)
        {
            ugRecycleBin.ZeroRowsText = GetString("unigrid.filteredzerorowstext");
        }
        else
        {
            ugRecycleBin.ZeroRowsText = IsSingleSite ? GetString("RecycleBin.NoDocuments") : GetString("RecycleBin.Empty");
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Show labels
        lblError.Visible = (lblError.Text != string.Empty);
        lblInfo.Visible = (lblInfo.Text != string.Empty);

        // Hide multiple actions if grid is empty
        pnlFooter.Visible = ugRecycleBin.GridView.Rows.Count > 0;

        // Hide site name column
        ugRecycleBin.GridView.Columns[5].Visible = (mSelectedSite == null) && !IsSingleSite;

        base.OnPreRender(e);
    }

    #endregion


    #region "Restoring & destroying methods"

    /// <summary>
    /// Restores documents selected in UniGrid.
    /// </summary>
    private void Restore(object parameter)
    {
        try
        {
            // Begin log
            AddLog(ResHelper.GetString("Recyclebin.RestoringDocuments", currentCulture));
            BinSettingsContainer settings = (BinSettingsContainer)parameter;
            DataSet recycleBin = null;
            switch (settings.CurrentWhat)
            {
                case What.AllDocuments:
                    DateTime modifiedFrom = DateTimeHelper.ZERO_TIME;
                    DateTime modifiedTo = DateTimeHelper.ZERO_TIME;
                    SetDocumentAge(ref modifiedFrom, ref modifiedTo);
                    recycleBin = VersionHistoryInfoProvider.GetRecycleBin((mSelectedSite != null) ? mSelectedSite.SiteID : 0, 0, GetWhereCondition(string.Empty), "CMS_VersionHistory.DocumentNamePath ASC", -1, "VersionHistoryID, CMS_VersionHistory.DocumentNamePath, CMS_VersionHistory.VersionDocumentName", modifiedFrom, modifiedTo);
                    break;

                case What.SelectedDocuments:
                    ArrayList toRestore = ugRecycleBin.SelectedItems;
                    // Restore selected documents
                    if (toRestore.Count > 0)
                    {
                        string where = SqlHelperClass.GetWhereCondition("VersionHistoryID", (string[])toRestore.ToArray(typeof(string)));
                        recycleBin = VersionHistoryInfoProvider.GetRecycleBin(0, where, "CMS_VersionHistory.DocumentNamePath ASC", -1, "VersionHistoryID, CMS_VersionHistory.DocumentNamePath, CMS_VersionHistory.VersionDocumentName");
                    }
                    break;
            }

            if (!DataHelper.DataSourceIsEmpty(recycleBin))
            {
                RestoreDataSet(settings.User, recycleBin);
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state == CMSThread.ABORT_REASON_STOP)
            {
                // When canceled
                CurrentInfo = ResHelper.GetString("Recyclebin.RestorationCanceled", currentCulture);
                AddLog(CurrentInfo);
            }
            else
            {
                // Log error
                CurrentError = ResHelper.GetString("Recyclebin.RestorationFailed", currentCulture) + ": " + ex.Message;
                AddLog(CurrentError);
            }
        }
        catch (Exception ex)
        {
            // Log error
            CurrentError = ResHelper.GetString("Recyclebin.RestorationFailed", currentCulture) + ": " + ex.Message;
            AddLog(CurrentError);
        }
    }


    /// <summary>
    /// Restores set of given version histories.
    /// </summary>
    /// <param name="currentUserInfo">Current user info</param>
    /// <param name="recycleBin">DataSet with nodes to restore</param>
    private void RestoreDataSet(CurrentUserInfo currentUserInfo, DataSet recycleBin)
    {
        // Result flags
        bool resultOK = true;
        bool permissionsOK = true;

        if (!DataHelper.DataSourceIsEmpty(recycleBin))
        {
            TreeProvider tree = new TreeProvider(currentUserInfo);
            tree.AllowAsyncActions = false;
            VersionManager verMan = new VersionManager(tree);
            // Restore all documents
            foreach (DataRow dataRow in recycleBin.Tables[0].Rows)
            {
                int versionId = ValidationHelper.GetInteger(dataRow["VersionHistoryID"], 0);

                // Log actual event
                string taskTitle = HTMLHelper.HTMLEncode(ValidationHelper.GetString(dataRow["DocumentNamePath"], string.Empty));

                // Restore document
                if (versionId > 0)
                {
                    // Check permissions
                    TreeNode tn = null;
                    if (!IsAuthorizedPerDocument(versionId, "Create", currentUser, out tn, verMan))
                    {
                        CurrentError = String.Format(ResHelper.GetString("Recyclebin.RestorationFailedPermissions", currentCulture), taskTitle);
                        AddLog(CurrentError);
                        permissionsOK = false;
                    }
                    else
                    {
                        tn = verMan.RestoreDocument(versionId, tn);
                        if (tn != null)
                        {
                            AddLog(ResHelper.GetString("general.document", currentCulture) + "'" + taskTitle + "'");
                        }
                        else
                        {
                            // Set result flag
                            if (resultOK)
                            {
                                resultOK = false;
                            }
                        }
                    }
                }
            }
        }

        if (resultOK && permissionsOK)
        {
            CurrentInfo = ResHelper.GetString("Recyclebin.RestorationOK", currentCulture);
            AddLog(CurrentInfo);
        }
        else
        {
            CurrentError = ResHelper.GetString("Recyclebin.RestorationFailed", currentCulture);
            if (!permissionsOK)
            {
                CurrentError += "<br />" + ResHelper.GetString("recyclebin.errorsomenotrestored", currentCulture);
            }
            AddLog(CurrentError);
        }
    }


    /// <summary>
    /// Empties recycle bin.
    /// </summary>
    private void EmptyBin(object parameter)
    {
        // Begin log
        AddLog(ResHelper.GetString("Recyclebin.EmptyingBin", currentCulture));
        BinSettingsContainer settings = (BinSettingsContainer)parameter;
        CurrentUserInfo currentUserInfo = settings.User;

        DataSet recycleBin = null;
        string where = null;
        DateTime modifiedFrom = DateTimeHelper.ZERO_TIME;
        DateTime modifiedTo = DateTimeHelper.ZERO_TIME;
        switch (settings.CurrentWhat)
        {
            case What.AllDocuments:
                SetDocumentAge(ref modifiedFrom, ref modifiedTo);
                where = GetWhereCondition(string.Empty);
                break;

            case What.SelectedDocuments:
                ArrayList toRestore = ugRecycleBin.SelectedItems;
                // Restore selected documents
                if (toRestore.Count > 0)
                {
                    where = SqlHelperClass.GetWhereCondition("VersionHistoryID", (string[])toRestore.ToArray(typeof(string)));
                }
                break;
        }
        recycleBin = VersionHistoryInfoProvider.GetRecycleBin((mSelectedSite != null) ? mSelectedSite.SiteID : 0, 0, where, "DocumentNamePath ASC", -1, null, modifiedFrom, modifiedTo);

        try
        {
            if (!DataHelper.DataSourceIsEmpty(recycleBin))
            {
                TreeProvider tree = new TreeProvider(currentUserInfo);
                tree.AllowAsyncActions = false;
                VersionManager verMan = new VersionManager(tree);

                foreach (DataRow dr in recycleBin.Tables[0].Rows)
                {
                    int versionHistoryId = Convert.ToInt32(dr["VersionHistoryID"]);
                    string documentNamePath = ValidationHelper.GetString(dr["DocumentNamePath"], string.Empty);
                    // Check permissions
                    TreeNode tn = null;
                    if (!IsAuthorizedPerDocument(versionHistoryId, "Destroy", currentUser, out tn, verMan))
                    {
                        CurrentError = String.Format(ResHelper.GetString("Recyclebin.DestructionFailedPermissions", currentCulture), documentNamePath);
                        AddLog(CurrentError);
                    }
                    else
                    {
                        AddLog(ResHelper.GetString("general.document", currentCulture) + "'" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["DocumentNamePath"], string.Empty)) + "'");
                        // Destroy the version
                        verMan.DestroyDocumentHistory(ValidationHelper.GetInteger(dr["DocumentID"], 0));
                        LogContext.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "Content", "DESTROYDOC", currentUser.UserID, currentUser.UserName, 0, null, HTTPHelper.UserHostAddress, string.Format(ResHelper.GetString("Recyclebin.documentdestroyed"), documentNamePath), currentSite.SiteID, HTTPHelper.GetAbsoluteUri(), HTTPHelper.MachineName, HTTPHelper.GetUrlReferrer(), HTTPHelper.GetUserAgent());
                    }
                }
                if (!String.IsNullOrEmpty(CurrentError))
                {
                    CurrentError = ResHelper.GetString("recyclebin.errorsomenotdestroyed", currentCulture);
                    AddLog(CurrentError);
                }
                else
                {
                    CurrentInfo = ResHelper.GetString("recyclebin.destroyok", currentCulture);
                    AddLog(CurrentInfo);
                }
            }
        }
        catch (ThreadAbortException ex)
        {
            string state = ValidationHelper.GetString(ex.ExceptionState, string.Empty);
            if (state != CMSThread.ABORT_REASON_STOP)
            {
                // Log error
                CurrentError = "Error occurred: " + ex.Message;
                AddLog(CurrentError);
            }
        }
        catch (Exception ex)
        {
            // Log error
            CurrentError = "Error occurred: " + ex.Message;
            AddLog(CurrentError);
        }
    }

    #endregion


    #region "Handling async thread"

    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        HandlePossibleErrors();
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        HandlePossibleErrors();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        HandlePossibleErrors();
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


    private void HandlePossibleErrors()
    {
        CurrentLog.Close();
        TerminateCallbacks();
        lblError.Text = CurrentError;
        lblInfo.Text = CurrentInfo;
        ugRecycleBin.ResetSelection();
    }


    private void TerminateCallbacks()
    {
        string terminatingScript = ScriptHelper.GetScript("var __pendingCallbacks = new Array();");
        ScriptHelper.RegisterStartupScript(this, typeof(string), "terminatePendingCallbacks", terminatingScript);
    }


    /// <summary>
    /// Runs async thread.
    /// </summary>
    /// <param name="action">Method to run</param>
    protected void RunAsync(AsyncAction action)
    {
        pnlLog.Visible = true;
        //pnlContent.Visible = false;

        CurrentError = string.Empty;
        CurrentInfo = string.Empty;
        CurrentLog.Close();
        EnsureLog();

        ctlAsync.RunAsync(action, WindowsIdentity.GetCurrent());
    }

    #endregion


    #region "Button handling"

    protected void btnOk_OnClick(object sender, EventArgs e)
    {
        pnlLog.Visible = true;
        //pnlContent.Visible = false;

        CurrentError = string.Empty;
        CurrentLog.Close();
        EnsureLog();

        int actionValue = ValidationHelper.GetInteger(drpAction.SelectedValue, 0);
        Action action = (Action)actionValue;

        int whatValue = ValidationHelper.GetInteger(drpWhat.SelectedValue, 0);
        currentWhat = (What)whatValue;

        ctlAsync.Parameter = new BinSettingsContainer(currentUser, currentWhat);
        switch (action)
        {
            case Action.Restore:
                switch (currentWhat)
                {
                    case What.AllDocuments:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/restoreall.png");
                        break;

                    case What.SelectedDocuments:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/restoreselected.png");
                        if (ugRecycleBin.SelectedItems.Count <= 0)
                        {
                            return;
                        }
                        break;
                }
                titleElemAsync.TitleText = GetString("Recyclebin.RestoringDocuments");
                RunAsync(Restore);
                break;

            case Action.Delete:
                switch (currentWhat)
                {
                    case What.AllDocuments:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/emptybin.png");
                        break;

                    case What.SelectedDocuments:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/emptyselected.png");
                        break;
                }
                titleElemAsync.TitleText = GetString("recyclebin.emptyingbin");
                RunAsync(EmptyBin);
                break;
        }
    }

    #endregion


    #region "Grid events"

    protected object ugRecycleBin_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        sourceName = sourceName.ToLower();
        switch (sourceName)
        {
            case "view":
                ImageButton btnView = sender as ImageButton;
                if (btnView != null)
                {
                    GridViewRow row = (GridViewRow)parameter;
                    object siteId = DataHelper.GetDataRowViewValue((DataRowView)row.DataItem, "NodeSiteID");
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(ValidationHelper.GetInteger(siteId, 0));
                    if (si != null)
                    {
                        if (si.Status == SiteStatusEnum.Stopped)
                        {
                            btnView.Enabled = false;
                            btnView.Style.Add("cursor", "default");
                            btnView.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Viewdisabled.png");
                        }
                    }
                }
                break;

            case "nodesiteid":
                {
                    int siteId = ValidationHelper.GetInteger(parameter, 0);
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                    return (si != null) ? HTMLHelper.HTMLEncode(si.DisplayName) : string.Empty;
                }

            case "versionclassid":
                int classId = ValidationHelper.GetInteger(parameter, 0);
                if (classId > 0)
                {
                    DataClassInfo dci = DataClassInfoProvider.GetDataClass(classId);
                    return (dci != null) ? HTMLHelper.HTMLEncode(dci.ClassDisplayName) : GetString("general.na");
                }
                return GetString("general.na");

            case "documentname":
                string documentName = ValidationHelper.GetString(parameter, null);
                return string.IsNullOrEmpty(documentName) ? GetString("general.na") : HTMLHelper.HTMLEncode(documentName);

            case "deletedwhen":
            case "deletedwhentooltip":
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    return string.Empty;
                }
                else
                {
                    DateTime deletedWhen = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                    if (currentUser == null)
                    {
                        currentUser = CMSContext.CurrentUser;
                    }
                    if (currentSite == null)
                    {
                        currentSite = CMSContext.CurrentSite;
                    }

                    bool displayGMT = (sourceName == "deletedwhentooltip");
                    return TimeZoneHelper.ConvertToUserTimeZone(deletedWhen, displayGMT, currentUser, currentSite);
                }
        }

        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void ugRecycleBin_OnAction(string actionName, object actionArgument)
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        VersionManager verMan = new VersionManager(tree);
        int versionHistoryId = ValidationHelper.GetInteger(actionArgument, 0);
        TreeNode doc = null;
        if (actionName == "restore")
        {
            try
            {
                if (IsAuthorizedPerDocument(versionHistoryId, "Create", currentUser, out doc, verMan))
                {
                    verMan.RestoreDocument(versionHistoryId, doc);
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("Recyclebin.RestorationOK");

                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = String.Format(ResHelper.GetString("Recyclebin.RestorationFailedPermissions", currentCulture), doc.DocumentNamePath);
                }
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = GetString("recyclebin.errorrestoringdocument") + " " + ex.Message;
            }
        }
        else if (actionName == "destroy")
        {
            if (IsAuthorizedPerDocument(versionHistoryId, "Destroy", currentUser, out doc, verMan))
            {
                verMan.DestroyDocumentHistory(doc.DocumentID);
                lblInfo.Visible = true;
                lblInfo.Text = GetString("recyclebin.destroyok");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = String.Format(ResHelper.GetString("recyclebin.destructionfailedpermissions", currentCulture), doc.DocumentNamePath);
            }

        }
        ugRecycleBin.ResetSelection();
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Merges given where condition with additional settings.
    /// </summary>
    /// <param name="where">Original where condition</param>
    /// <returns>New where condition</returns>
    private string GetWhereCondition(string where)
    {
        // Apply filter settings
        if (!string.IsNullOrEmpty(filterBin.WhereCondition))
        {
            where = SqlHelperClass.AddWhereCondition(where, filterBin.WhereCondition);
        }

        // Filter by document name
        if (!string.IsNullOrEmpty(DocumentName))
        {
            where = SqlHelperClass.AddWhereCondition(where, "CMS_VersionHistory.VersionDocumentName LIKE '%" + SqlHelperClass.GetSafeQueryString(DocumentName, false) + "%'");
        }

        // Filter by document type
        if (!String.IsNullOrEmpty(DocumentType))
        {
            string[] types = DocumentType.Split(';');
            where = SqlHelperClass.AddWhereCondition(where, SqlHelperClass.GetWhereCondition<string>("ClassName", types, true));
        }

        return where;
    }


    /// <summary>
    /// Sets age of documents in order to obtain correct data set.
    /// </summary>
    /// <param name="modifiedFrom">Document modified from</param>
    /// <param name="modifiedTo">Document modified to</param>
    private void SetDocumentAge(ref DateTime modifiedFrom, ref DateTime modifiedTo)
    {
        // Set age of documents
        if (!string.IsNullOrEmpty(DocumentAge))
        {
            string[] ages = DocumentAge.Split(';');
            if (ages.Length == 2)
            {
                // Compute 'from' and 'to' values
                int from = ValidationHelper.GetInteger(ages[1], 0);
                int to = ValidationHelper.GetInteger(ages[0], 0);

                if (from > 0)
                {
                    modifiedFrom = DateTime.Now.AddDays((-1) * from);
                }

                if (to > 0)
                {
                    modifiedTo = DateTime.Now.AddDays((-1) * to);
                }
            }
        }
    }


    /// <summary>
    /// Check user permissions for document version.
    /// </summary>
    /// <param name="versionId">Document version</param>
    /// <param name="permission">Permission</param>
    /// <param name="user">User</param>
    /// <param name="checkedNode">Checked node</param>
    /// <param name="versionManager">Version manager</param>
    /// <returns>True if authorized, false otherwise</returns>
    public bool IsAuthorizedPerDocument(int versionId, string permission, CurrentUserInfo user, out TreeNode checkedNode, VersionManager versionManager)
    {
        if (versionManager == null)
        {
            TreeProvider tree = new TreeProvider(user);
            tree.AllowAsyncActions = false;
            versionManager = new VersionManager(tree);
        }

        // Get the values form deleted node
        checkedNode = versionManager.GetVersion(versionId);
        return IsAuthorizedPerDocument(checkedNode, permission, user);
    }


    /// <summary>
    /// Check user permissions for document.
    /// </summary>
    /// <param name="document">Document</param>
    /// <param name="permission">Permissions</param>
    /// <param name="user">User</param>
    /// <returns>TreeNode if authorized, null otherwise</returns>
    public bool IsAuthorizedPerDocument(TreeNode document, string permission, CurrentUserInfo user)
    {
        // Initialize variables
        string className = null;

        // Check global permission
        bool userHasGlobalPerm = user.IsAuthorizedPerResource("CMS.Content", permission);

        // Get node properties
        try
        {
            // Get the values form deleted node
            className = document.NodeClassName;
        }
        catch (ThreadAbortException)
        {
            throw;
        }
        catch (Exception ex)
        {
            CurrentError = "Error occurred: " + ex.Message;
            AddLog(CurrentError);
        }

        bool additionalPermission = false;

        if (permission.ToLower() == "create")
        {
            additionalPermission = user.IsAuthorizedPerClassName(className, "CreateSpecific");
        }

        // Check permissions
        if (userHasGlobalPerm || user.IsAuthorizedPerClassName(className, permission) || additionalPermission)
        {
            return true;
        }

        return false;
    }

    #endregion
}
