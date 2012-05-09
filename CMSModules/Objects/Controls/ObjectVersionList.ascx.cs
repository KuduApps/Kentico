using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.EventLog;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.ExtendedControls;

public partial class CMSModules_Objects_Controls_ObjectVersionList : CMSUserControl
{
    #region "Variables"

    private GeneralizedInfo mObject = null;
    private bool mCheckPermissions = true;
    private UserInfo currentUser = null;
    private SiteInfo currentSite = null;
    private bool? mAllowDestroy = null;
    private bool? mAllowRestore = null;
    private bool mRegisterReloadHeaderScript = false;
    private bool mInvalidatePersistentEditedObject = false;

    #endregion


    #region "Event"

    /// <summary>
    /// Event triggered after rollback is made
    /// </summary>
    public EventHandler OnAfterRollback;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets version history object type.
    /// </summary>
    public string ObjectType
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets version history object type.
    /// </summary>
    public int ObjectID
    {
        get;
        set;
    }


    /// <summary>
    /// IInfo object representing object which version history is displayed.
    /// </summary>
    public GeneralizedInfo Object
    {
        get
        {
            if (mObject == null)
            {
                if (!String.IsNullOrEmpty(ObjectType) && (ObjectID > 0))
                {
                    mObject = BaseAbstractInfoProvider.GetInfoById(ObjectType, ObjectID);
                    if (mObject != null)
                    {
                        CMSContext.EditedObject = mObject;
                    }
                }
            }
            return mObject;
        }
        set
        {
            mObject = value;
            if (mObject != null)
            {
                ObjectID = mObject.ObjectID;
                ObjectType = mObject.ObjectType;
                CMSContext.EditedObject = mObject;
            }
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return gridHistory.IsLiveSite;
        }
        set
        {
            gridHistory.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates if permissions should be checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return mCheckPermissions;
        }
        set
        {
            mCheckPermissions = value;
        }
    }


    /// <summary>
    /// Indicates if user can destroy object version history.
    /// </summary>
    private bool AllowDestroy
    {
        get
        {
            if ((Object != null) && (mAllowDestroy == null))
            {
                mAllowDestroy = UserInfoProvider.IsAuthorizedPerObject(Object.ObjectType, PermissionsEnum.Destroy, CMSContext.CurrentSiteName, CMSContext.CurrentUser);
            }

            return ValidationHelper.GetBoolean(mAllowDestroy, false);
        }
    }


    /// <summary>
    /// Indicates if user can rollback object version history.
    /// </summary>
    private bool AllowRestore
    {
        get
        {
            if ((Object != null) && (mAllowRestore == null))
            {
                mAllowRestore = UserInfoProvider.IsAuthorizedPerObject(Object.ObjectType, PermissionsEnum.Modify, CMSContext.CurrentSiteName, CMSContext.CurrentUser);
            }

            return ValidationHelper.GetBoolean(mAllowRestore, false);
        }
    }


    /// <summary>
    /// Indicates if javascript to reload header should be rendered
    /// </summary>
    public bool RegisterReloadHeaderScript
    {
        get
        {
            return mRegisterReloadHeaderScript;
        }
        set
        {
            mRegisterReloadHeaderScript = value;
        }
    }


    /// <summary>
    /// Indicates if control should be displayed or not
    /// </summary>
    public override bool Visible
    {
        get
        {
            return base.Visible;
        }
        set
        {
            base.Visible = value;
            gridHistory.Visible = value;
        }
    }


    /// <summary>
    /// Reference to hidden refresh button
    /// </summary>
    public Button RefreshButton
    {
        get
        {
            return btnRefresh;
        }
    }


    /// <summary>
    /// Indicates if persistent edited object should be invalidated
    /// </summary>
    public bool InvalidatePersistentEditedObject
    {
        get
        {
            return (mInvalidatePersistentEditedObject && (Page is CMSPage));
        }
        set
        {
            mInvalidatePersistentEditedObject = value;
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (InvalidatePersistentEditedObject)
        {
            Control ctrl = ControlsHelper.GetPostBackControl(Page);
            if ((ctrl != null) && (ctrl.ClientID == gridHistory.ClientID))
            {
                CMSPage page = Page as CMSPage;
                if (page != null)
                {
                    page.PersistentEditedObject = null;
                }
            }

        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            gridHistory.StopProcessing = true;
        }
        else
        {
            SetupControl();

            // Initialize javascript
            InitScript();
        }
    }


    /// <summary>
    /// Setup control.
    /// </summary>
    public void SetupControl()
    {
        gridHistory.ZeroRowsText = GetString("objectversioning.objecthasnohistory");
        if (Object != null)
        {
            // Set buttons confirmation
            btnDestroy.OnClientClick = "return confirm(" + ScriptHelper.GetString(ResHelper.GetString("VersionProperties.ConfirmDestroy")) + ");";
            btnMakeMajor.OnClientClick = "return confirm(" + ScriptHelper.GetString(ResHelper.GetString("VersionProperties.ConfirmMakeMajor")) + ");";

            gridHistory.OnExternalDataBound += gridHistory_OnExternalDataBound;
            gridHistory.OnAction += gridHistory_OnAction;
            gridHistory.WhereCondition = "VersionObjectType = '" + SqlHelperClass.GetSafeQueryString(Object.ObjectType, false) + "' AND VersionObjectID = " + Object.ObjectID;
            gridHistory.Columns = "VersionID, UserName, FullName, VersionModifiedWhen, VersionNumber";
        }
        else
        {
            gridHistory.StopProcessing = true;
            lblHistory.Visible = false;
            btnDestroy.Visible = false;

            lblError.Text = GetString("objectversioning.uknownobject");
        }
    }


    /// <summary>
    /// Pre render.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (Object != null)
        {
            btnDestroy.Visible = btnMakeMajor.Visible = !gridHistory.IsEmpty;
            btnDestroy.Enabled = AllowDestroy;
            CMSContext.EditedObject = Object;
        }
        else
        {
            btnDestroy.Visible = btnMakeMajor.Visible = false;
            CMSPage.EditedObject = null;
        }

        if (plcLabels.Visible)
        {
            lblInfo.Visible = !String.IsNullOrEmpty(lblInfo.Text);
            lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
        }
    }

    #endregion


    #region "Grid events"

    protected object gridHistory_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView data = null;
        sourceName = sourceName.ToLower();
        ImageButton imgDestroy = null;
        switch (sourceName)
        {
            case "view":
                ImageButton imgView = ((ImageButton)sender);
                if (imgView != null)
                {
                    GridViewRow gvr = (GridViewRow)parameter;
                    data = (DataRowView)gvr.DataItem;
                    string viewVersionUrl = ResolveUrl("~/CMSModules/Objects/Dialogs/ViewObjectVersion.aspx?versionhistoryid=" + ValidationHelper.GetInteger(data["VersionID"], 0) + "&clientid=" + ClientID);
                    viewVersionUrl = URLHelper.AddParameterToUrl(viewVersionUrl, "hash", QueryHelper.GetHash(viewVersionUrl));
                    imgView.OnClientClick = "window.open(" + ScriptHelper.GetString(viewVersionUrl) + ");return false;";
                }
                break;

            case "allowdestroy":
                imgDestroy = ((ImageButton)sender);
                if (imgDestroy != null)
                {
                    if (CheckPermissions && !AllowDestroy)
                    {
                        imgDestroy.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/deletedisabled.png");
                        imgDestroy.Enabled = false;
                    }
                    else
                    {
                        imgDestroy.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/delete.png");
                    }
                }
                break;

            case "rollback":
                imgDestroy = ((ImageButton)sender);
                if (imgDestroy != null)
                {
                    if (CheckPermissions && !AllowRestore)
                    {
                        imgDestroy.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/undodisabled.png");
                        imgDestroy.Enabled = false;
                    }
                    else
                    {
                        imgDestroy.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/undo.png");
                    }
                }
                break;

            case "modifiedwhen":
            case "modifiedwhentooltip":
                if (currentUser == null)
                {
                    currentUser = CMSContext.CurrentUser;
                }
                if (currentSite == null)
                {
                    currentSite = CMSContext.CurrentSite;
                }
                DateTime modifiedwhen = ValidationHelper.GetDateTime(((DataRowView)parameter)["VersionModifiedWhen"], DateTimeHelper.ZERO_TIME); ;
                bool displayGMT = (sourceName == "modifiedwhentooltip");
                return TimeZoneHelper.ConvertToUserTimeZone(modifiedwhen, displayGMT, currentUser, currentSite);

            case "modifiedby":
                data = (DataRowView)parameter;
                string userName = ValidationHelper.GetString(data["UserName"], String.Empty);
                string fullName = ValidationHelper.GetString(data["FullName"], String.Empty);

                return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(userName, fullName));
        }

        return parameter;
    }


    protected void gridHistory_OnAction(string actionName, object actionArgument)
    {
        int versionHistoryId = ValidationHelper.GetInteger(actionArgument, 0);
        actionName = actionName.ToLower();
        switch (actionName.ToLower())
        {
            case "rollback":
            case "fullrollback":
                if (versionHistoryId > 0)
                {
                    try
                    {
                        ObjectVersionManager.RollbackVersion(versionHistoryId, (actionName == "fullrollback") ? true : false);
                        lblInfo.Text = GetString("objectversioning.rollbackOK");

                        // Set object to null bacause after rollback it doesn't contain current data
                        Object = null;
                        gridHistory.ReloadData();

                        if (OnAfterRollback != null)
                        {
                            OnAfterRollback(this, null);
                        }

                        ScriptHelper.RegisterStartupScript(this, typeof(string), "RefreshContent", ScriptHelper.GetScript("RefreshRelatedContent_" + ClientID + "();"));
                    }
                    catch (CodeNameNotUniqueException ex)
                    {
                        lblError.Visible = true;
                        lblError.Text = String.Format(GetString("objectversioning.restorenotuniquecodename"), (ex.Object != null) ? "('" + ex.Object.ObjectCodeName + "')" : null);
                    }
                    catch (Exception ex)
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("objectversioning.recyclebin.restorationfailed") + " " + GetString("general.seeeventlog");

                        // Log to event log
                        EventLogProvider.LogException("Object version list", "OBJECTRESTORE", ex);
                    }
                }
                break;

            case "destroy":
                if (versionHistoryId > 0)
                {
                    // Check permissions
                    if (CheckPermissions && !AllowDestroy)
                    {
                        lblError.Text = GetString("History.ErrorNotAllowedToDestroy");
                    }
                    else
                    {
                        ObjectVersionManager.DestroyObjectVersion(versionHistoryId);
                        lblInfo.Text = GetString("objectversioning.destroyOK");
                    }
                }
                break;
        }
        plcLabels.Visible = true;
    }

    #endregion


    #region "Button handling"

    /// <summary>
    /// Button destroy history click.
    /// </summary>
    protected void btnDestroy_Click(object sender, EventArgs e)
    {
        if (Object != null)
        {
            // Check permissions
            if (CheckPermissions && !AllowDestroy)
            {
                lblError.Text = GetString("History.ErrorNotAllowedToDestroy");
                plcLabels.Visible = true;
                return;
            }
            ObjectVersionManager.DestroyObjectHistory(Object.ObjectType, Object.ObjectID);

            UserInfo currentUser = CMSContext.CurrentUser;
            string objType = GetString("Objecttype." + Object.ObjectType.Replace(".", "_"));
            string description = GetString(String.Format("objectversioning.historydestroyed", SqlHelperClass.GetSafeQueryString(Object.ObjectDisplayName, false)));

            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, objType, "DESTROYHISTORY", HTTPHelper.GetAbsoluteUri(), description);

            ReloadData();
        }
        else
        {
            CMSPage.EditedObject = null;
        }
    }


    /// <summary>
    /// Button make version major click.
    /// </summary>
    protected void btnMakeMajor_Click(object sender, EventArgs e)
    {
        if (Object != null)
        {
            ObjectVersionHistoryInfo version = ObjectVersionManager.GetLatestVersion(Object.ObjectType, Object.ObjectID);
            if (version != null)
            {
                ObjectVersionManager.MakeVersionMajor(version);
                ReloadData();

                lblInfo.Text = GetString("objectversioning.makeversionmajorinfo");
            }
            else
            {
                lblError.Text = GetString("objectversioning.makeversionmajornoversion") + " " + GetString("objectversioning.objecthasnohistory");
            }

            plcLabels.Visible = true;
        }
        else
        {
            CMSPage.EditedObject = null;
        }
    }


    /// <summary>
    /// Button refresh click.
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lblInfo.Text = "";

        if (OnAfterRollback != null)
        {
            OnAfterRollback(this, null);
        }
    }


    protected void btnHidden_Click(object sender, EventArgs e)
    {
        // Process recycle bin action
        string[] args = hdnValue.Value.Split(';');
        if (args.Length == 2)
        {
            gridHistory_OnAction(args[0], args[1]);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads data in unigrid.
    /// </summary>
    public void ReloadData()
    {
        gridHistory.ReloadData();
    }


    /// <summary>
    /// Register required javascript functions
    /// </summary>
    private void InitScript()
    {
        StringBuilder sbScript = new StringBuilder();
        sbScript.Append("function RefreshRelatedContent_", ClientID, "(){if(window.RefreshContent != null) { window.RefreshContent(); }\n");
        if (RegisterReloadHeaderScript)
        {
            sbScript.Append("if(parent.frames[0] && parent.frames[0].ReloadAndSetTab) {parent.frames[0].ReloadAndSetTab(escape('selecttab|##versioningtab##'));}\n");
        }
        sbScript.Append("}\n");
        sbScript.Append("function RefreshVersions_", ClientID, "(){var button = document.getElementById('", btnRefresh.ClientID, "'); if(button){button.click();}}");
        sbScript.Append("function ContextVersionAction_", gridHistory.ClientID, @"(action, versionId) { document.getElementById('", hdnValue.ClientID, @"').value = action + ';' + versionId;", ControlsHelper.GetPostBackEventReference(btnHidden, null), ";}");

        // Register required js to reload related content
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ReloadContent_" + ClientID, ScriptHelper.GetScript(sbScript.ToString()));

    }

    #endregion
}
