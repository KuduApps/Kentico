using System;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;
using System.Data;
using System.Threading;
using System.Security.Principal;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.EventLog;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.Synchronization;

public partial class CMSModules_Objects_Controls_ObjectsRecycleBin : CMSUserControl
{
    #region "Private variables"

    private string mSiteName = String.Empty;
    private string currentCulture = CultureHelper.DefaultUICulture;
    private CurrentUserInfo mCurrentUser = null;
    private SiteInfo mCurrentSite = null;
    private string mObjectType = String.Empty;
    private bool mIsSingleSite = true;
    private bool mRestrictUsers = true;

    private static readonly Hashtable mInfos = new Hashtable();
    private string mOrderBy = "VersionDeletedWhen DESC";
    private string mObjectDisplayName = String.Empty;
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

        public SiteInfo Site { get; private set; }

        public BinSettingsContainer(CurrentUserInfo user, What what, SiteInfo site)
            : this()
        {
            User = user;
            CurrentWhat = what;
            Site = site;
        }
    }

    #endregion


    #region "Enumerations"

    protected enum Action
    {
        SelectAction = 0,
        Restore = 1,
        RestoreWithoutSiteBindings = 2,
        RestoreToCurrentSite = 3,
        Delete = 4
    }

    protected enum What
    {
        SelectedObjects = 0,
        AllObjects = 1
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if control is used only in one site mode(CMSDesk or Widget).
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
            if (!String.IsNullOrEmpty(mSiteName) && (mSiteName != "##global##"))
            {
                SiteInfo siteInfo = SiteInfoProvider.GetSiteInfo(mSiteName);
                if (siteInfo != null)
                {
                    mCurrentSite = siteInfo;
                }
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
            return ValidationHelper.GetString(mInfos["ObjectRestoreError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["ObjectRestoreError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Current Info.
    /// </summary>
    public string CurrentInfo
    {
        get
        {
            return ValidationHelper.GetString(mInfos["ObjectRestoreInfo_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["ObjectRestoreInfo_" + ctlAsync.ProcessGUID] = value;
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
    /// Filter by object type.
    /// </summary>
    public string ObjectType
    {
        get
        {
            return mObjectType;
        }

        set
        {
            mObjectType = value;
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
    /// Object display name for grid filter.
    /// </summary>
    public string ObjectDisplayName
    {
        get
        {
            return mObjectDisplayName;
        }
        set
        {
            mObjectDisplayName = value;
        }
    }


    /// <summary>
    /// Gets current user.
    /// </summary>
    private CurrentUserInfo CurrentUser
    {
        get
        {
            if (mCurrentUser == null)
            {
                mCurrentUser = CMSContext.CurrentUser;
            }
            return mCurrentUser;
        }

    }


    /// <summary>
    /// Gets current selected site.
    /// </summary>
    private SiteInfo CurrentSite
    {
        get
        {
            if ((mCurrentSite == null) && !String.IsNullOrEmpty(SiteName) && (SiteName != "##global##"))
            {
                SiteInfo siteInfo = SiteInfoProvider.GetSiteInfo(mSiteName);
                if (siteInfo != null)
                {
                    mCurrentSite = siteInfo;
                }
            }
            return mCurrentSite;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return ugRecycleBin.IsLiveSite;
        }
        set
        {
            ugRecycleBin.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates if the control data should perform the operations.
    /// </summary>
    public bool DelayedLoading
    {
        get
        {
            return ugRecycleBin.DelayedReload;
        }
        set
        {
            ugRecycleBin.DelayedReload = value;
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
            return;
        }

        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(Page);

        // Set current UI culture
        currentCulture = CultureHelper.PreferredUICulture;

        if (!RequestHelper.IsCallback())
        {
            ControlsHelper.RegisterPostbackControl(btnOk);

            // Create action script
            StringBuilder actionScript = new StringBuilder();
            actionScript.Append(
@"
function PerformAction(selectionFunction, selectionField, dropId, validationLabel, whatId) {
  var selectionFieldElem = document.getElementById(selectionField);
  var label = document.getElementById(validationLabel);
  var items = selectionFieldElem.value;
  var whatDrp = document.getElementById(whatId);
  var allDocs = whatDrp.value == '", (int)What.AllObjects, @"';
  var action = document.getElementById(dropId).value;
  if (action == '", (int)Action.SelectAction, @"') {
     label.innerHTML = '", GetString("massaction.selectsomeaction"), @"';
     return false;
  }
  
  if(!eval(selectionFunction) || allDocs) {
     var confirmed = false;
     var confMessage = '';
     switch(action) {
        case '", (int)Action.RestoreToCurrentSite, @"':
        case '", (int)Action.RestoreWithoutSiteBindings, @"':
        case '", (int)Action.Restore, @"':
          confMessage = '", GetString("objectversioning.recyclebin.confirmrestores"), @"';
          break;
        
        case '", (int)Action.Delete, @"':
          confMessage = allDocs ?  '", GetString("objectversioning.recyclebin.confirmemptyrecbin"), @"' : '", GetString("objectversioning.recyclebin.confirmdeleteselected") + @"';
          break;
     }
     return confirm(confMessage);
  }
  else {
    label.innerHTML = '", GetString("objectversioning.recyclebin.selectobjects"), @"';
    return false;
  }
}
function ContextBinAction_", ugRecycleBin.ClientID, @"(action, versionId) {
  document.getElementById('", hdnValue.ClientID, @"').value = action + ';' + versionId;",
  ControlsHelper.GetPostBackEventReference(btnHidden, null), @";
}");

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
                drpAction.Items.Add(new ListItem(GetString("objectversioning.recyclebin." + Action.RestoreWithoutSiteBindings), Convert.ToInt32(Action.RestoreWithoutSiteBindings).ToString()));

                // Display restore to current site only if current site available
                CurrentSiteInfo si = CMSContext.CurrentSite;
                if (si != null)
                {
                    drpAction.Items.Add(new ListItem(String.Format(GetString("objectversioning.recyclebin." + Action.RestoreToCurrentSite), si.DisplayName), Convert.ToInt32(Action.RestoreToCurrentSite).ToString()));
                }
                drpAction.Items.Add(new ListItem(GetString("general." + Action.Delete), Convert.ToInt32(Action.Delete).ToString()));

                drpWhat.Items.Add(new ListItem(GetString("contentlisting." + What.SelectedObjects), Convert.ToInt32(What.SelectedObjects).ToString()));
                drpWhat.Items.Add(new ListItem(GetString("contentlisting." + What.AllObjects), Convert.ToInt32(What.AllObjects).ToString()));

                ugRecycleBin.OrderBy = OrderBy;
            }

            string where = (IsSingleSite || (SiteName == "##global##")) ? "VersionObjectSiteID IS NULL" : null;
            if (CurrentSite != null)
            {
                where = SqlHelperClass.AddWhereCondition(where, "VersionObjectSiteID = " + CurrentSite.SiteID, "OR");
            }

            ugRecycleBin.WhereCondition = GetWhereCondition(where);
            ugRecycleBin.HideControlForZeroRows = false;
            ugRecycleBin.OnExternalDataBound += ugRecycleBin_OnExternalDataBound;
            ugRecycleBin.OnAction += ugRecycleBin_OnAction;

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(Page);

            // Initialize buttons
            btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

            string error = QueryHelper.GetString("displayerror", String.Empty);
            if (error != String.Empty)
            {
                lblError.Text = GetString("objectversioning.recyclebin.errorsomenotdestroyed");
            }

            // Set visibility of panels
            pnlLog.Visible = false;
        }
        else
        {
            ugRecycleBin.StopProcessing = true;
        }

        // Initialize filter
        ReloadFilter((CurrentSite != null) ? CurrentSite.SiteID : -1, IsSingleSite, false);

        // If filter is set
        if (filterBin.FilterIsSet)
        {
            ugRecycleBin.ZeroRowsText = GetString("unigrid.filteredzerorowstext");
        }
        else
        {
            ugRecycleBin.ZeroRowsText = IsSingleSite ? GetString("objectversioning.RecycleBin.NoObjects") : GetString("RecycleBin.Empty");
        }

        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Show labels
        lblError.Visible = (lblError.Text != string.Empty);
        lblInfo.Visible = (lblInfo.Text != string.Empty);
        pnlInfo.Visible = lblError.Visible || lblInfo.Visible;
        pnlUpdateInfo.Update();

        // Hide multiple actions if grid is empty
        pnlFooter.Visible = ugRecycleBin.GridView.Rows.Count > 0;

        // Check if postback caused by filter button
        Control ctrlPostBack = ControlsHelper.GetPostBackControl(Page);
        if ((ctrlPostBack != null) && (ctrlPostBack is Button))
        {
            pnlGrid.Update();
        }

        pnlFilter.Update();

        base.OnPreRender(e);
    }

    #endregion


    #region "Restoring & destroying methods"

    /// <summary>
    /// Restores objects selected in UniGrid with binding to current site.
    /// </summary>
    private void RestoreToCurrentSite(object parameter)
    {
        Restore(parameter, Action.RestoreToCurrentSite);
    }


    /// <summary>
    /// Restores objects selected in UniGrid without site bindings.
    /// </summary>
    private void RestoreWithoutSiteBindings(object parameter)
    {
        Restore(parameter, Action.RestoreWithoutSiteBindings);
    }


    /// <summary>
    /// Restores objects selected in UniGrid with site bindings and childs.
    /// </summary>
    private void RestoreWithChilds(object parameter)
    {
        Restore(parameter, Action.Restore);
    }


    /// <summary>
    /// Restores objects selected in UniGrid.
    /// </summary>
    private void Restore(object parameter, Action action)
    {
        try
        {
            // Begin log
            AddLog(ResHelper.GetString("objectversioning.recyclebin.restoringobjects", currentCulture));

            BinSettingsContainer settings = (BinSettingsContainer)parameter;
            DataSet recycleBin = null;
            if (settings.User.IsAuthorizedPerResource("cms.globalpermissions", "RestoreObjects"))
            {
                string where = IsSingleSite ? "VersionObjectSiteID IS NULL" : null;

                switch (settings.CurrentWhat)
                {
                    case What.AllObjects:
                        if (settings.Site != null)
                        {
                            where = SqlHelperClass.AddWhereCondition(where, "VersionObjectSiteID = " + settings.Site.SiteID, "OR");
                        }
                        recycleBin = ObjectVersionHistoryInfoProvider.GetRecycleBin(GetWhereCondition(where), null, -1, "VersionID, VersionObjectDisplayName, VersionObjectType, VersionObjectID");
                        break;

                    case What.SelectedObjects:
                        ArrayList toRestore = ugRecycleBin.SelectedItems;
                        // Restore selected objects
                        if (toRestore.Count > 0)
                        {
                            where = SqlHelperClass.GetWhereCondition("VersionID", (string[])toRestore.ToArray(typeof(string)));
                            recycleBin = ObjectVersionHistoryInfoProvider.GetRecycleBin(where, OrderBy, -1, "VersionID, VersionObjectDisplayName, VersionObjectType, VersionObjectID");
                        }
                        break;
                }

                if (!DataHelper.DataSourceIsEmpty(recycleBin))
                {
                    RestoreDataSet(settings, recycleBin, action);
                }
            }
            else
            {
                CurrentError = ResHelper.GetString("objectversioning.recyclebin.restorationfailedpermissions");
                AddLog(CurrentError);
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
                CurrentError = ResHelper.GetString("objectversioning.recyclebin.restorationfailed", currentCulture) + ": " + ResHelper.GetString("general.seeeventlog", currentCulture);
                AddLog(CurrentError);

                // Log to event log
                LogException("OBJECTRESTORE", ex);
            }
        }
        catch (Exception ex)
        {
            // Log error
            CurrentError = ResHelper.GetString("objectversioning.recyclebin.restorationfailed", currentCulture) + ": " + ResHelper.GetString("general.seeeventlog", currentCulture);
            AddLog(CurrentError);

            // Log to event log
            LogException("OBJECTRESTORE", ex);
        }
    }


    /// <summary>
    /// Restores set of given version histories.
    /// </summary>
    /// <param name="currentUserInfo">Current user info</param>
    /// <param name="recycleBin">DataSet with nodes to restore</param>
    private void RestoreDataSet(BinSettingsContainer settings, DataSet recycleBin, Action action)
    {
        // Result flags
        bool resultOK = true;

        if (!DataHelper.DataSourceIsEmpty(recycleBin))
        {
            // Restore all objects
            foreach (DataRow dataRow in recycleBin.Tables[0].Rows)
            {
                int versionId = ValidationHelper.GetInteger(dataRow["VersionID"], 0);

                // Log current event
                string taskTitle = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(ValidationHelper.GetString(dataRow["VersionObjectDisplayName"], string.Empty)));

                // Restore object
                if (versionId > 0)
                {
                    GeneralizedInfo restoredObj = null;
                    try
                    {
                        switch (action)
                        {
                            case Action.Restore:
                                restoredObj = ObjectVersionManager.RestoreObject(versionId, true);
                                break;

                            case Action.RestoreToCurrentSite:
                                restoredObj = ObjectVersionManager.RestoreObject(versionId, CMSContext.CurrentSiteID);
                                break;

                            case Action.RestoreWithoutSiteBindings:
                                restoredObj = ObjectVersionManager.RestoreObject(versionId, 0);
                                break;
                        }
                    }
                    catch (CodeNameNotUniqueException ex)
                    {
                        CurrentError = String.Format(GetString("objectversioning.restorenotuniquecodename"), (ex.Object != null) ? "('" + ex.Object.ObjectCodeName + "')" : null);
                        AddLog(CurrentError);
                    }

                    if (restoredObj != null)
                    {
                        AddLog(ResHelper.GetString("general.object", currentCulture) + " '" + taskTitle + "'");
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

        if (resultOK)
        {
            CurrentInfo = ResHelper.GetString("ObjectVersioning.Recyclebin.RestorationOK", currentCulture);
            AddLog(CurrentInfo);
        }
        else
        {
            CurrentError = ResHelper.GetString("objectversioning.recyclebin.restorationfailed", currentCulture);
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
        SiteInfo currentSite = settings.Site;

        DataSet recycleBin = null;
        string where = IsSingleSite ? "VersionObjectSiteID IS NULL" : null;

        switch (settings.CurrentWhat)
        {
            case What.AllObjects:
                if (currentSite != null)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "VersionObjectSiteID = " + currentSite.SiteID, "OR");
                }
                where = GetWhereCondition(where);
                break;

            case What.SelectedObjects:
                ArrayList toRestore = ugRecycleBin.SelectedItems;
                // Restore selected objects
                if (toRestore.Count > 0)
                {
                    where = SqlHelperClass.GetWhereCondition("VersionID", (string[])toRestore.ToArray(typeof(string)));
                }
                break;
        }
        recycleBin = ObjectVersionHistoryInfoProvider.GetRecycleBin(where, null, -1, "VersionID, VersionObjectType, VersionObjectID, VersionObjectDisplayName, VersionObjectSiteID");

        try
        {
            if (!DataHelper.DataSourceIsEmpty(recycleBin))
            {
                foreach (DataRow dr in recycleBin.Tables[0].Rows)
                {
                    int versionHistoryId = Convert.ToInt32(dr["VersionID"]);
                    string versionObjType = Convert.ToString(dr["VersionObjectType"]);
                    string objName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(ValidationHelper.GetString(dr["VersionObjectDisplayName"], string.Empty)));
                    string siteName = null;
                    if (currentSite != null)
                    {
                        siteName = currentSite.SiteName;
                    }
                    else
                    {
                        int siteId = ValidationHelper.GetInteger(dr["VersionObjectSiteID"], 0);
                        siteName = SiteInfoProvider.GetSiteName(siteId);
                    }

                    // Check permissions                    
                    if (!currentUserInfo.IsAuthorizedPerObject(PermissionsEnum.Destroy, versionObjType, siteName))
                    {
                        CurrentError = String.Format(ResHelper.GetString("objectversioning.Recyclebin.DestructionFailedPermissions", currentCulture), objName);
                        AddLog(CurrentError);
                    }
                    else
                    {
                        AddLog(ResHelper.GetString("general.object", currentCulture) + " '" + objName + "'");

                        // Destroy the version
                        int versionObjId = ValidationHelper.GetInteger(dr["VersionObjectID"], 0);
                        ObjectVersionManager.DestroyObjectHistory(versionObjType, versionObjId);
                        LogContext.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "Objects", "DESTROYOBJECT", currentUserInfo.UserID, currentUserInfo.UserName, 0, null, HTTPHelper.UserHostAddress, ResHelper.GetString("objectversioning.Recyclebin.objectdestroyed"), (currentSite != null) ? currentSite.SiteID : 0, HTTPHelper.GetAbsoluteUri(), HTTPHelper.MachineName, HTTPHelper.GetUrlReferrer(), HTTPHelper.GetUserAgent());
                    }
                }
                if (!String.IsNullOrEmpty(CurrentError))
                {
                    CurrentError = ResHelper.GetString("objectversioning.recyclebin.errorsomenotdestroyed", currentCulture);
                    AddLog(CurrentError);
                }
                else
                {
                    CurrentInfo = ResHelper.GetString("ObjectVersioning.Recyclebin.DestroyOK", currentCulture);
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
                CurrentError = "Error occurred: " + ResHelper.GetString("general.seeeventlog", currentCulture);
                AddLog(CurrentError);

                // Log to event log
                LogException("EMPTYINGBIN", ex);
            }
        }
        catch (Exception ex)
        {
            // Log error
            CurrentError = "Error occurred: " + ResHelper.GetString("general.seeeventlog", currentCulture);
            AddLog(CurrentError);

            // Log to event log
            LogException("EMPTYINGBIN", ex);
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
        ReloadFilter((CurrentSite != null) ? CurrentSite.SiteID : -1, IsSingleSite, true);
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

        CurrentError = string.Empty;
        CurrentLog.Close();
        EnsureLog();

        int actionValue = ValidationHelper.GetInteger(drpAction.SelectedValue, 0);
        Action action = (Action)actionValue;

        int whatValue = ValidationHelper.GetInteger(drpWhat.SelectedValue, 0);
        currentWhat = (What)whatValue;

        ctlAsync.Parameter = new BinSettingsContainer(CurrentUser, currentWhat, CurrentSite);
        switch (action)
        {
            case Action.Restore:
            case Action.RestoreToCurrentSite:
            case Action.RestoreWithoutSiteBindings:
                switch (currentWhat)
                {
                    case What.AllObjects:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/restoreall.png");
                        break;

                    case What.SelectedObjects:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/restoreselected.png");
                        if (ugRecycleBin.SelectedItems.Count <= 0)
                        {
                            return;
                        }
                        break;
                }

                titleElemAsync.TitleText = GetString("objectversioning.Recyclebin.Restoringobjects");

                switch (action)
                {
                    case Action.Restore:
                        RunAsync(RestoreWithChilds);
                        break;

                    case Action.RestoreToCurrentSite:
                        RunAsync(RestoreToCurrentSite);
                        break;

                    case Action.RestoreWithoutSiteBindings:
                        RunAsync(RestoreWithoutSiteBindings);
                        break;
                }

                break;

            case Action.Delete:
                switch (currentWhat)
                {
                    case What.AllObjects:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/emptybin.png");
                        break;

                    case What.SelectedObjects:
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_RecycleBin/emptyselected.png");
                        break;
                }
                titleElemAsync.TitleText = GetString("recyclebin.emptyingbin");
                RunAsync(EmptyBin);
                break;
        }
    }


    protected void btnHidden_Click(object sender, EventArgs e)
    {
        // Process recycle bin action
        string[] args = hdnValue.Value.Split(';');
        if (args.Length == 2)
        {
            ugRecycleBin_OnAction(args[0], args[1]);
            ReloadData(false);
        }
    }

    #endregion


    #region "Grid events"

    protected object ugRecycleBin_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        sourceName = sourceName.ToLower();
        DataRowView data = null;
        string objType = null;
        switch (sourceName)
        {
            case "view":
                ImageButton imgView = ((ImageButton)sender);
                if (imgView != null)
                {
                    GridViewRow gvr = (GridViewRow)parameter;
                    data = (DataRowView)gvr.DataItem;
                    string viewVersionUrl = ResolveUrl("~/CMSModules/Objects/Dialogs/ViewObjectVersion.aspx?showall=1&nocompare=1&versionhistoryid=" + ValidationHelper.GetInteger(data["VersionID"], 0));
                    viewVersionUrl = URLHelper.AddParameterToUrl(viewVersionUrl, "hash", QueryHelper.GetHash(viewVersionUrl));
                    imgView.OnClientClick = "window.open(" + ScriptHelper.GetString(viewVersionUrl) + ");return false;";
                }
                break;

            case "destroy":
                ImageButton imgDestroy = sender as ImageButton;
                if (imgDestroy != null)
                {
                    DataRowView dr = (DataRowView)(((GridViewRow)parameter).DataItem);

                    objType = ValidationHelper.GetString(dr["VersionObjectType"], null);

                    // Get object site name
                    string siteName = null;
                    if (CurrentSite != null)
                    {
                        siteName = CurrentSite.SiteName;
                    }
                    else
                    {
                        int siteId = ValidationHelper.GetInteger(dr["VersionObjectSiteID"], 0);
                        siteName = SiteInfoProvider.GetSiteName(siteId);
                    }
                }
                break;

            case "restorechilds":
                ImageButton imgRestore = sender as ImageButton;
                if (imgRestore != null)
                {
                    imgRestore.ImageUrl = GetImageUrl("CMSModules/CMS_RecycleBin/restorechilds.png");
                }
                break;

            case "deletedwhen":
            case "deletedwhentooltip":
                DateTime deletedWhen = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                bool displayGMT = (sourceName == "deletedwhentooltip");
                return TimeZoneHelper.ConvertToUserTimeZone(deletedWhen, displayGMT, CurrentUser, CurrentSite);

            case "versionobjecttype":
                objType = ValidationHelper.GetString(parameter, "");
                return HTMLHelper.HTMLEncode(GetString("ObjectType." + objType.Replace(".", "_")));
        }

        return HTMLHelper.HTMLEncode(parameter.ToString());
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void ugRecycleBin_OnAction(string actionName, object actionArgument)
    {
        int versionHistoryId = ValidationHelper.GetInteger(actionArgument, 0);
        actionName = actionName.ToLower();

        switch (actionName)
        {
            case "restorechilds":
            case "restorewithoutbindings":
            case "restorecurrentsite":
                try
                {
                    if (CMSContext.CurrentUser.IsAuthorizedPerResource("cms.globalpermissions", "RestoreObjects"))
                    {
                        switch (actionName)
                        {
                            case "restorechilds":
                                ObjectVersionManager.RestoreObject(versionHistoryId, true);
                                break;

                            case "restorewithoutbindings":
                                ObjectVersionManager.RestoreObject(versionHistoryId, 0);
                                break;

                            case "restorecurrentsite":
                                ObjectVersionManager.RestoreObject(versionHistoryId, CMSContext.CurrentSiteID);
                                break;
                        }

                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("ObjectVersioning.Recyclebin.RestorationOK");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = ResHelper.GetString("objectversioning.recyclebin.restorationfailedpermissions");
                    }
                }
                catch (CodeNameNotUniqueException ex)
                {
                    lblError.Visible = true;
                    lblError.Text = String.Format(GetString("objectversioning.restorenotuniquecodename"), (ex.Object != null) ? "('" + ex.Object.ObjectCodeName + "')" : null);
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("objectversioning.recyclebin.restorationfailed") + GetString("general.seeeventlog");

                    // Log to event log
                    LogException("OBJECTRESTORE", ex);
                }
                break;

            case "destroy":

                ObjectVersionHistoryInfo verInfo = ObjectVersionHistoryInfoProvider.GetVersionHistoryInfo(versionHistoryId);

                // Get object site name
                string siteName = null;
                if (CurrentSite != null)
                {
                    siteName = CurrentSite.SiteName;
                }
                else
                {
                    siteName = SiteInfoProvider.GetSiteName(verInfo.VersionObjectSiteID);
                }

                if ((verInfo != null) && CurrentUser.IsAuthorizedPerObject(PermissionsEnum.Destroy, verInfo.VersionObjectType, siteName))
                {
                    ObjectVersionManager.DestroyObjectHistory(verInfo.VersionObjectType, verInfo.VersionObjectID);
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("ObjectVersioning.Recyclebin.DestroyOK");
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = String.Format(ResHelper.GetString("objectversioning.recyclebin.destructionfailedpermissions"), HTMLHelper.HTMLEncode(ResHelper.LocalizeString(verInfo.VersionObjectDisplayName)));
                }
                break;
        }

        ugRecycleBin.ResetSelection();
        pnlUpdateInfo.Update();
        ReloadFilter((CurrentSite != null) ? CurrentSite.SiteID : -1, IsSingleSite, false);
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

        // Add recycle bin condition
        where = SqlHelperClass.AddWhereCondition(where, "VersionDeletedWhen IS NOT NULL");

        // Filter by object name
        if (!string.IsNullOrEmpty(ObjectDisplayName))
        {
            where = SqlHelperClass.AddWhereCondition(where, "VersionObjectDisplayName LIKE '%" + SqlHelperClass.GetSafeQueryString(ObjectDisplayName, false) + "%'");
        }

        // Filter by object type
        if (!String.IsNullOrEmpty(ObjectType))
        {
            where = SqlHelperClass.AddWhereCondition(where, "VersionObjectType LIKE '%" + SqlHelperClass.GetSafeQueryString(ObjectType, false) + "%'");
        }

        return where;
    }


    /// <summary>
    /// Reload control data.
    /// </summary>
    public void ReloadData(bool refreshInfo)
    {
        int siteId = (CurrentSite != null) ? CurrentSite.SiteID : 0;
        string where = (IsSingleSite || (SiteName == "##global##")) ? "VersionObjectSiteID IS NULL" : null;

        if (siteId > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "VersionObjectSiteID = " + siteId, "OR");
        }

        // Reload filter data
        ReloadFilter(siteId, IsSingleSite, true);

        // Reload grid data
        ugRecycleBin.WhereCondition = GetWhereCondition(where);
        ugRecycleBin.ReloadData();
        pnlGrid.Update();

        if (refreshInfo)
        {
            // Hide info and error messages
            lblError.Text = string.Empty;
            lblError.Visible = false;
            lblInfo.Text = string.Empty;
            lblInfo.Visible = false;
            pnlUpdateInfo.Update();
        }
    }


    /// <summary>
    /// Reload filter data.
    /// </summary>
    private void ReloadFilter(int siteId, bool isSingleSite, bool forceReload)
    {
        filterBin.UsersPlaceHolder.Visible = CMSContext.CurrentUser.IsGlobalAdministrator;

        // Reload filter data
        filterBin.SiteID = siteId;
        filterBin.IsSingleSite = isSingleSite;
        filterBin.DisplayUsersFromAllSites = !RestrictUsers;

        if (forceReload)
        {
            filterBin.ReloadData();
            pnlFilter.Update();
        }
    }


    /// <summary>
    /// Method to log exceptions to event log.
    /// </summary>
    /// <param name="eventCode">Code of event during which exception occured</param>
    private void LogException(string eventCode, Exception ex)
    {
        // Log exception to event log
        EventLogProvider.LogException("Object recycle bin", eventCode, ex);
    }

    #endregion
}
