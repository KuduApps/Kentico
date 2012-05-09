using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.EventLog;
using CMS.ExtendedControls;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Objects_Controls_ViewObjectVersion : CMSUserControl, IPostBackEventHandler
{
    #region "Variables"

    ObjectVersionHistoryInfo mVersion = null;
    ObjectVersionHistoryInfo mVersionCompare = null;
    TimeZoneInfo mServerTimeZone = null;
    TimeZoneInfo mUsedTimeZone = null;
    bool mObjectDataOnly = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if comparison is available
    /// </summary>
    public bool NoComparison
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets object version ID to display
    /// </summary>
    public int VersionID
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets object version ID to compare
    /// </summary>
    public int VersionCompareID
    {
        get;
        set;
    }
    
    
    /// <summary>
    /// List of excluded table names separated by semicolon (;)
    /// </summary>
    public string ExcludedTableNames
    {
        get
        {
            return viewDataSet.ExcludedTableNames;
        }
        set
        {
            viewDataSet.ExcludedTableNames = value;
        }
    }


    /// <summary>
    /// Object version history to display
    /// </summary>
    public ObjectVersionHistoryInfo Version
    {
        get
        {
            if (mVersion == null)
            {
                if (VersionID > 0)
                {
                    mVersion = ObjectVersionHistoryInfoProvider.GetVersionHistoryInfo(VersionID);
                }
            }
            return mVersion;
        }
        set
        {
            mVersion = value;
        }
    }


    /// <summary>
    /// Object version history to dcompare
    /// </summary>
    public ObjectVersionHistoryInfo VersionCompare
    {
        get
        {
            if (mVersionCompare == null)
            {
                if (VersionCompareID > 0)
                {
                    mVersionCompare = ObjectVersionHistoryInfoProvider.GetVersionHistoryInfo(VersionCompareID);
                }
            }
            return mVersionCompare;
        }
        set
        {
            mVersionCompare = value;
        }
    }


    /// <summary>
    /// Indicates if only object data or also additional child objects data should be proccessed
    /// </summary>
    public bool ObjectDataOnly
    {
        get
        {
            return mObjectDataOnly;
        }
        set
        {
            mObjectDataOnly = value;
        }
    }


    /// <summary>
    /// User time zone
    /// </summary>
    private TimeZoneInfo UsedTimeZone
    {
        get
        {
            if (mUsedTimeZone == null)
            {
                TimeZoneHelper.GetCurrentTimeZoneDateTimeString(DateTime.Now, CMSContext.CurrentUser, CMSContext.CurrentSite, out mUsedTimeZone);
            }
            return mUsedTimeZone;
        }
    }


    /// <summary>
    /// Server time zone
    /// </summary>
    private TimeZoneInfo ServerTimeZone
    {
        get
        {
            if (mServerTimeZone == null)
            {
                mServerTimeZone = TimeZoneHelper.ServerTimeZone;
            }
            return mServerTimeZone;
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Page load
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register WOpener script
        ScriptHelper.RegisterWOpenerScript(Page);

        if (QueryHelper.GetString("rollbackok", string.Empty) == "1")
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("objectversioning.rollbackOK");
        }

        // No comparing available in Recycle bin
        pnlControl.Visible = !NoComparison;

        if (Version != null)
        {
            SetupControls();
        }
        else
        {
            lblInfo.Text = GetString("objectversion.notexists");
            lblInfo.Visible = true;
            pnlAdditionalControls.Visible = false;
            viewDataSet.Visible = false;
        }
    }


    /// <summary>
    /// Page pre-render
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if ((Version != null) && (VersionCompare != null))
        {
            // Prepare header with rollback controls
            TableRow tr = new TableRow();
            tr.CssClass = "UniGridHead";

            TableHeaderCell th = new TableHeaderCell();
            th.Text = GetString("lock.versionnumber");
            tr.Cells.Add(th);

            // Switch header sides if necessary
            if (VersionCompare.VersionID < Version.VersionID)
            {
                tr.Cells.Add(GetRollbackTableHeaderCell("compare", VersionCompare));
                tr.Cells.Add(GetRollbackTableHeaderCell("source", Version));
            }
            else
            {
                tr.Cells.Add(GetRollbackTableHeaderCell("source", Version));
                tr.Cells.Add(GetRollbackTableHeaderCell("compare", VersionCompare));
            }

            if ((viewDataSet.DataSet.Tables.Count <= 1) && (viewDataSet.CompareDataSet.Tables.Count <= 1))
            {
                viewDataSet.Table.Rows.RemoveAt(0);
            }
            viewDataSet.Table.Rows.AddAt(0, tr);
        }
    }


    /// <summary>
    /// Setup controls
    /// </summary>
    private void SetupControls()
    {
        if (!RequestHelper.IsPostBack())
        {
            LoadDropDown();
            chkDisplayAllData.Checked = !ObjectDataOnly;
        }

        drpCompareTo.SelectedIndexChanged += drpCompareTo_SelectedIndexChanged;

        if (!NoComparison)
        {
            ObjectDataOnly = !chkDisplayAllData.Checked;
        }

        SyncHelper sh = SyncHelper.GetInstance();
        sh.OperationType = OperationTypeEnum.Versioning;

        // Get object version DataSet
        DataSet dsObject = sh.GetDataSet(Version.VersionXML, TaskTypeEnum.UpdateObject, Version.VersionObjectType);
        DataSet dsCompare = null;

        // Get object compare version DataSet
        if (VersionCompare != null)
        {
            dsCompare = sh.GetDataSet(VersionCompare.VersionXML, TaskTypeEnum.UpdateObject, VersionCompare.VersionObjectType);
        }

        // Filter out data if necessary
        if (ObjectDataOnly)
        {
            // Get object data table name
            GeneralizedInfo obj = CMSObjectHelper.GetReadOnlyObject(Version.VersionObjectType);
            string objectTable = CMSObjectHelper.GetTableName(obj);

            dsObject = CreateTableDataSet(dsObject, objectTable, obj);
            dsCompare = CreateTableDataSet(dsCompare, objectTable, obj);
        }

        // Switch version data to ensure lower version is on the left side
        if ((Version != null) && (VersionCompare != null) && (VersionCompare.VersionID < Version.VersionID))
        {
            viewDataSet.DataSet = dsCompare;
            viewDataSet.CompareDataSet = dsObject;
        }
        else
        {
            viewDataSet.DataSet = dsObject;
            viewDataSet.CompareDataSet = dsCompare;
        }
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Load comparsion drop-down list with data
    /// </summary>
    private void LoadDropDown()
    {
        drpCompareTo.Items.Clear();

        DataSet dsVersions = ObjectVersionManager.GetObjectHistory(Version.VersionObjectType, Version.VersionObjectID, "VersionID != " + Version.VersionID, "VersionModifiedWhen DESC, VersionNumber DESC", -1, "VersionID, VersionModifiedWhen, VersionNumber");

        // Converting modified time to corect time zone
        if (!DataHelper.DataSourceIsEmpty(dsVersions))
        {
            foreach (DataRow dr in dsVersions.Tables[0].Rows)
            {
                string verId = ValidationHelper.GetString(dr["VersionID"], String.Empty);
                string verNumber = ValidationHelper.GetString(dr["VersionNumber"], String.Empty);
                DateTime verModified = ValidationHelper.GetDateTime(dr["VersionModifiedWhen"], DataHelper.DATETIME_NOT_SELECTED);
                drpCompareTo.Items.Add(new ListItem(GetVersionNumber(verNumber, verModified), verId));
            }
        }

        // If history to compare is available
        if (drpCompareTo.Items.Count > 0)
        {
            drpCompareTo.Items.Insert(0, "(select version)");
        }
        // Otherwise hide dropdown list
        else
        {
            pnlControl.Visible = false;
        }

        // Pre-select dropdown list
        if (VersionCompare != null)
        {
            drpCompareTo.SelectedValue = VersionCompare.VersionID.ToString();
        }

    }


    /// <summary>
    /// Get version date string in required format 
    /// </summary>
    /// <param name="dateModified">DateTime when was version modified</param>
    private string GetVersionDateString(DateTime dateModified)
    {
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(CMSContext.CurrentUser.PreferredUICultureCode);
        if (UsedTimeZone != null)
        {
            return TimeZoneHelper.ConvertTimeZoneDateTime(dateModified, ServerTimeZone, UsedTimeZone).ToString(cultureInfo) + TimeZoneHelper.GetGMTStringOffset(" GMT{0: + 00.00; - 00.00}", UsedTimeZone);
        }
        else
        {
            return dateModified.ToString(cultureInfo);
        }
    }


    /// <summary>
    /// Get version number together with version date string
    /// </summary>
    /// <param name="versionNumber">Version number</param>
    /// <param name="versionModified">Version modified DateTime</param>
    /// <returns></returns>
    private string GetVersionNumber(string versionNumber, DateTime versionModified)
    {
        versionNumber += " (" + GetVersionDateString(versionModified) + ")";
        return versionNumber;
    }


    /// <summary>
    /// Gets new table header cell which contains label and rollback image.
    /// </summary>
    /// <param name="suffixID">ID suffix</param>
    /// <param name="documentID">Document ID</param>
    /// <param name="versionID">Version history ID</param>
    /// <param name="action">Action</param>
    private TableHeaderCell GetRollbackTableHeaderCell(string suffixID, ObjectVersionHistoryInfo objectVersion)
    {
        TableHeaderCell tblHeaderCell = new TableHeaderCell();

        string tooltip = null;

        // Label
        Label lblValue = new Label();
        lblValue.ID = "lbl" + suffixID;
        lblValue.Text = HTMLHelper.HTMLEncode(GetVersionNumber(objectVersion.VersionNumber, objectVersion.VersionModifiedWhen));

        // Panel
        Panel pnlLabel = new Panel();
        pnlLabel.ID = "pnlLabel" + suffixID;
        pnlLabel.CssClass = "LeftAlign";
        pnlLabel.Controls.Add(lblValue);

        tblHeaderCell.Controls.Add(pnlLabel);

        // Add rollback controls if user authorized to modify selected object
        if (UserInfoProvider.IsAuthorizedPerObject(objectVersion.VersionObjectType, PermissionsEnum.Modify, CMSContext.CurrentSiteName, CMSContext.CurrentUser))
        {
            // Rollback panel 
            Panel pnlImage = new Panel();
            pnlImage.ID = "pnlRollback" + suffixID;
            pnlImage.CssClass = "RightAlign";

            // Rollback image
            Image imgRollback = new Image();
            imgRollback.ID = "imgRollback" + suffixID;

            string confirmScript = null;
            
            // Set image action and description according to roll back type
            if (chkDisplayAllData.Checked)
            {
                imgRollback.ImageUrl = GetImageUrl("CMSModules/CMS_RecycleBin/restorechilds.png");
                confirmScript = "if (confirm(\"" + GetString("objectversioning.versionlist.confirmfullrollback") + "\")) { ";
                confirmScript += ControlsHelper.GetPostBackEventReference(this, objectVersion.VersionID.ToString() + "|" + true) + "; return false; }";
                tooltip = GetString("objectversioning.versionlist.versionfullrollbacktooltip");
            }
            else
            {
                imgRollback.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/undo.png");
                confirmScript = "if (confirm(\"" + GetString("Unigrid.ObjectVersionHistory.Actions.Rollback.Confirmation") + "\")) { ";
                confirmScript += ControlsHelper.GetPostBackEventReference(this, objectVersion.VersionID.ToString() + "|" + false) + "; return false; }";
                tooltip = GetString("history.versionrollbacktooltip");
            }

            imgRollback.Style.Add("cursor", "pointer");
            imgRollback.AlternateText = tooltip;
            imgRollback.ToolTip = tooltip;

            // Prepare onclick script
            imgRollback.Attributes.Add("onclick", confirmScript);

            pnlImage.Controls.Add(imgRollback);
            tblHeaderCell.Controls.Add(pnlImage);
        }

        return tblHeaderCell;
    }


    /// <summary>
    /// Create new DataSet from table with specified table name
    /// </summary>
    /// <param name="sourceDs">Source DataSet</param>
    /// <param name="tableName">Main table name</param>
    /// <param name="obj">Object which data contains DataSet</param>
    /// <returns>Result DataSet</returns>
    private DataSet CreateTableDataSet(DataSet sourceDs, string tableName, GeneralizedInfo obj)
    {
        if (!DataHelper.DataSourceIsEmpty(sourceDs))
        {
            DataTable dt = sourceDs.Tables[tableName];

            DataSet dsResult = new DataSet();
            if (!DataHelper.DataSourceIsEmpty(dt))
            {
                dsResult.Tables.Add(dt.Copy());
                return dsResult;
            }
        }

        return null;
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Dropdown list selection changed
    /// </summary>
    private void drpCompareTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string url = URLHelper.CurrentURL;

        url = URLHelper.RemoveParameterFromUrl(url, "rollbackok");

        if (drpCompareTo.SelectedIndex == 0)
        {
            url = URLHelper.RemoveParameterFromUrl(url, "comparehistoryid");
        }
        else
        {
            url = URLHelper.AddParameterToUrl(url, "comparehistoryid", drpCompareTo.SelectedValue);
        }
        url = URLHelper.AddParameterToUrl(url, "showall", chkDisplayAllData.Checked.ToString());

        url = URLHelper.RemoveParameterFromUrl(url, "hash");
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
        URLHelper.Redirect(url);
    }

    #endregion


    #region IPostBackEventHandler Members

    /// <summary>
    /// Raises event postback event
    /// </summary>
    /// <param name="eventArgument">Argument</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        string[] args = eventArgument.Split('|');
        if (args.Length == 2)
        {
            int rollbackVersionId = ValidationHelper.GetInteger(args[0], 0);
            bool processChilds = ValidationHelper.GetBoolean(args[1], false);
            if (rollbackVersionId > 0)
            {
                try
                {
                    // Rollback version
                    int newVersionHistoryId = ObjectVersionManager.RollbackVersion(rollbackVersionId, processChilds);

                    lblInfo.Text = GetString("objectversioning.rollbackOK");

                    string url = URLHelper.CurrentURL;

                    // Add URL parameters
                    url = URLHelper.AddParameterToUrl(url, "versionhistoryid", newVersionHistoryId.ToString());
                    url = URLHelper.AddParameterToUrl(url, "comparehistoryid", VersionCompare.VersionID.ToString());
                    url = URLHelper.AddParameterToUrl(url, "rollbackok", "1");
                    url = URLHelper.AddParameterToUrl(url, "showall", processChilds.ToString());
                    url = URLHelper.RemoveParameterFromUrl(url, "hash");
                    url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));

                    // Prepare URL
                    url = ScriptHelper.GetString(URLHelper.ResolveUrl(url), true);

                    // Prepare script for refresh parent window and this dialog
                    StringBuilder builder = new StringBuilder();
                    builder.Append("if (wopener != null) {\n");

                    string clientId = QueryHelper.GetString("clientid", "");
                    if (!String.IsNullOrEmpty(clientId))
                    {
                        builder.Append("if (wopener.RefreshVersions_", clientId, " != null) {wopener.RefreshVersions_", clientId, "();}",
                                       "if (wopener.RefreshRelatedContent_", clientId, " != null) {wopener.RefreshRelatedContent_", clientId, "();}}");
                    }

                    builder.Append("window.document.location.replace(" + url + ");");

                    string script = ScriptHelper.GetScript(builder.ToString());
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "RefreshAndReload", script);
                }
                catch (Exception ex)
                {
                    lblError.Text = GetString("objectversioning.recyclebin.restorationfailed") + " " + GetString("general.seeeventlog");

                    // Log to event log
                    EventLogProvider.LogException("View object version", "OBJECTRESTORE", ex);
                }
            }
        }
    }

    #endregion
}
