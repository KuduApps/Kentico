using System;
using System.Text;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.WorkflowEngine;
using CMS.UIControls.UniGridConfig;
using CMS.TreeEngine;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;
using Action = CMS.UIControls.UniGridConfig.Action;

public partial class CMSModules_AdminControls_Controls_Documents_Documents : CMSAdminEditControl
{
    #region "Constants"

    // Source fields
    private const string SOURCE_SITENAME = "SiteName";

    private const string SOURCE_MODIFIEDWHEN = "DocumentModifiedWhen";

    private const string SOURCE_CLASSDISPLAYNAME = "ClassDisplayName";

    private const string SOURCE_WORKFLOWSTEPID = "DocumentWorkflowStepID";

    private const string SOURCE_WORKFLOWSTEPDISPLAYNAME = "StepDisplayName";

    private const string SOURCE_VERSION = "DocumentLastVersionNumber";

    private const string SOURCE_DOCUMENTNAME = "DocumentName";

    private const string SOURCE_NODEID = "NodeID";

    private const string SOURCE_CLASSNAME = "ClassName";

    private const string SOURCE_NODESITEID = "NodeSiteID";

    private const string SOURCE_NODELINKEDNODEID = "NodeLinkedNodeID";

    private const string SOURCE_DOCUMENTCULTURE = "DocumentCulture";

    private const string SOURCE_CULTURENAME = "CultureName";

    private const string SOURCE_DOCUMENTNAMEPATH = "DocumentNamePath";

    private const string SOURCE_NODEALIASPATH = "NodeAliasPath";

    private const string SOURCE_TYPE = "Type";

    // External source fields
    private const string EXTERNALSOURCE_SITENAME = "sitename";

    private const string EXTERNALSOURCE_SITEID = "siteid";

    private const string EXTERNALSOURCE_STEPDISPLAYNAME = "stepdisplayname";

    private const string EXTERNALSOURCE_MODIFIEDWHEN = "modifiedwhen";

    private const string EXTERNALSOURCE_MODIFIEDWHENTOOLTIP = "modifiedwhentooltip";

    private const string EXTERNALSOURCE_VERSION = "versionnumber";

    private const string EXTERNALSOURCE_CULTURE = "culture";

    private const string EXTERNALSOURCE_DOCUMENTNAME = "documentname";

    private const string EXTERNALSOURCE_DOCUMENTNAMETOOLTIP = "documentnametooltip";

    private const string EXTERNALSOURCE_CLASSDISPLAYNAME = "classdisplayname";

    private const string EXTERNALSOURCE_CLASSDISPLAYNAMETOOLTIP = "classdisplaynametooltip";

    private const string EXTERNALSOURCE_STEPNAME = "stepname";

    private const string EXTERNALSOURCE_PREVIEW = "preview";

    private const string EXTERNALSOURCE_EDIT = "edit";

    private const string SELECTION_COLUMN = "DocumentID";

    // Listing type
    private const string LISTINGTYPE_RECYCLEBIN = "Recycle bin";

    #endregion


    #region "Private variables"

    private CurrentUserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;
    private SiteInfo siteInfo = null;
    private TreeProvider mTree = null;

    // Property variables
    private string mOrderBy = "DocumentModifiedWhen";
    private string mPath = "/%";
    private string mSiteName = String.Empty;
    private string mDocumentType = String.Empty;
    private string mItemsPerPage = String.Empty;
    private string mDocumentAge = String.Empty;
    private string mDocumentName = String.Empty;
    private bool mDisplayOnlyRunningSites = false;

    private ListingTypeEnum mListingType = ListingTypeEnum.MyDocuments;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Site name for filtr.
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
            UniGrid.OrderBy = value;
            mOrderBy = value;
        }
    }


    /// <summary>
    /// If true, only documents from running sites are displayed
    /// </summary>
    public bool DisplayOnlyRunningSites
    {
        get
        {
            return mDisplayOnlyRunningSites;
        }
        set
        {
            mDisplayOnlyRunningSites = value;
        }
    }


    /// <summary>
    /// Gets the number of document age conditions.
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
    /// Path filter for grid.
    /// </summary>
    public string Path
    {
        get
        {
            if (String.IsNullOrEmpty(mPath))
            {
                return "/%";
            }
            return mPath;
        }
        set
        {
            mPath = value;
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
    /// Document type for filter.
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
    /// Type of items to show.
    /// </summary>
    public ListingTypeEnum ListingType
    {
        get
        {
            return mListingType;
        }

        set
        {
            mListingType = value;
        }
    }


    /// <summary>
    /// Is one of 'my desk' document listings.
    /// </summary>
    public bool MyDeskDocuments
    {
        get
        {
            return (ListingType == ListingTypeEnum.CheckedOut) || (ListingType == ListingTypeEnum.RecentDocuments) || (ListingType == ListingTypeEnum.MyDocuments) || (ListingType == ListingTypeEnum.PendingDocuments) || (ListingType == ListingTypeEnum.OutdatedDocuments) || (ListingType == ListingTypeEnum.All);
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            gridElem.IsLiveSite = value;
            base.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Text displayed when datasource is empty.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return UniGrid.ZeroRowsText;
        }
        set
        {
            UniGrid.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Inner grid control.
    /// </summary>
    public UniGrid UniGrid
    {
        get
        {
            return gridElem;
        }
    }


    /// <summary>
    /// Tree provider.
    /// </summary>
    public TreeProvider Tree
    {
        get
        {
            return mTree ?? (mTree = new TreeProvider(CMSContext.CurrentUser));
        }
        set
        {
            mTree = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Init(object sender, EventArgs e)
    {
        // Initialize UniGrid collections
        gridElem.GridColumns = new UniGridColumns();
        gridElem.GridOptions = new UniGridOptions();
        gridElem.GridActions = new UniGridActions();

        // Add edit action
        Action editAction = new Action();
        editAction.Name = "Edit";
        editAction.ExternalSourceName = EXTERNALSOURCE_EDIT;
        editAction.Icon = "edit.png";
        editAction.Caption = "$contentmenu.edit$";
        gridElem.GridActions.Actions.Add(editAction);

        // Add view action
        Action viewAction = new Action();
        viewAction.Name = "Preview";
        viewAction.ExternalSourceName = EXTERNALSOURCE_PREVIEW;
        viewAction.Icon = "view.png";
        viewAction.Caption = "$documents.navigatetodocument$";
        gridElem.GridActions.Actions.Add(viewAction);

        // Add document name column
        Column documentName = new Column();
        documentName.Source = UniGrid.ALL;
        documentName.Sort = SOURCE_DOCUMENTNAME;
        documentName.ExternalSourceName = EXTERNALSOURCE_DOCUMENTNAME;
        documentName.Caption = "$general.documentname$";
        documentName.Wrap = false;
        documentName.Tooltip = new ColumnTooltip();
        documentName.Tooltip.Source = UniGrid.ALL;
        documentName.Tooltip.ExternalSourceName = EXTERNALSOURCE_DOCUMENTNAMETOOLTIP;
        documentName.Tooltip.Width = "0";
        documentName.Tooltip.Encode = false;

        switch (ListingType)
        {
            case ListingTypeEnum.OutdatedDocuments:
                break;

            default:
                documentName.Filter = new ColumnFilter();
                documentName.Filter.Type = "text";
                documentName.Filter.Source = SOURCE_DOCUMENTNAME;
                break;
        }
        switch (ListingType)
        {
            case ListingTypeEnum.All:
                documentName.Width = "80%";
                break;

            default:
                documentName.Width = "100%";
                break;
        }

        // Add filter for specific listing types
        if ((ListingType == ListingTypeEnum.MyDocuments) || (ListingType == ListingTypeEnum.CheckedOut) || (ListingType == ListingTypeEnum.RecentDocuments) ||
            (ListingType == ListingTypeEnum.PendingDocuments))
        {
            gridElem.GridOptions.DisplayFilter = true;
        }

        gridElem.GridColumns.Columns.Add(documentName);

        // Add listing type (only if combined view)
        Column listingType = new Column();
        listingType.Source = "type";
        listingType.ExternalSourceName = "type";
        listingType.Sort = "type";
        listingType.Wrap = false;
        listingType.Caption = "$general.listingtype$";
        if (ListingType == ListingTypeEnum.All)
        {
            gridElem.GridColumns.Columns.Add(listingType);
        }

        switch (ListingType)
        {
            case ListingTypeEnum.DocTypeDocuments:
                break;

            default:
                // Initialize document class name column
                Column classDisplayName = new Column();
                classDisplayName.Source = SOURCE_CLASSDISPLAYNAME;
                classDisplayName.ExternalSourceName = EXTERNALSOURCE_CLASSDISPLAYNAME;
                classDisplayName.MaxLength = 50;
                classDisplayName.Caption = "$general.documenttype$";
                classDisplayName.Wrap = false;
                classDisplayName.Tooltip = new ColumnTooltip();
                classDisplayName.Tooltip.Source = SOURCE_CLASSDISPLAYNAME;
                classDisplayName.Tooltip.ExternalSourceName = EXTERNALSOURCE_CLASSDISPLAYNAMETOOLTIP;
                classDisplayName.Tooltip.Width = "0";
                switch (ListingType)
                {
                    case ListingTypeEnum.OutdatedDocuments:
                        break;

                    default:
                        classDisplayName.Filter = new ColumnFilter();
                        classDisplayName.Filter.Type = "text";
                        classDisplayName.Filter.Source = SOURCE_CLASSDISPLAYNAME;
                        break;
                }
                gridElem.GridColumns.Columns.Add(classDisplayName);
                break;
        }

        // Add timestamp column
        Column modifiedWhen = new Column();
        modifiedWhen.Source = SOURCE_MODIFIEDWHEN;
        modifiedWhen.ExternalSourceName = EXTERNALSOURCE_MODIFIEDWHEN;
        modifiedWhen.Wrap = false;
        modifiedWhen.Tooltip = new ColumnTooltip();
        modifiedWhen.Tooltip.ExternalSourceName = EXTERNALSOURCE_MODIFIEDWHENTOOLTIP;
        switch (ListingType)
        {
            case ListingTypeEnum.CheckedOut:
                modifiedWhen.Caption = "$general.checkouttime$";
                break;

            default:
                modifiedWhen.Caption = "$general.modified$";
                break;
        }
        gridElem.GridColumns.Columns.Add(modifiedWhen);

        // Add column with workflow information
        Column workflowStep = new Column();
        workflowStep.Caption = "$general.workflowstep$";
        workflowStep.Wrap = false;
        switch (ListingType)
        {
            case ListingTypeEnum.All:
                workflowStep.Source = SOURCE_WORKFLOWSTEPID;
                workflowStep.ExternalSourceName = EXTERNALSOURCE_STEPNAME;
                break;

            default:
                workflowStep.Source = SOURCE_WORKFLOWSTEPDISPLAYNAME;
                workflowStep.ExternalSourceName = EXTERNALSOURCE_STEPDISPLAYNAME;
                break;
        }
        gridElem.GridColumns.Columns.Add(workflowStep);

        // Add version information
        switch (ListingType)
        {
            case ListingTypeEnum.WorkflowDocuments:
                Column versionNumber = new Column();
                versionNumber.Source = SOURCE_VERSION;
                versionNumber.ExternalSourceName = EXTERNALSOURCE_VERSION;
                versionNumber.Caption = "$general.version$";
                versionNumber.Wrap = false;
                gridElem.GridColumns.Columns.Add(versionNumber);
                break;
        }

        // Add culture column
        Column culture = new Column();
        culture.Source = UniGrid.ALL;
        culture.ExternalSourceName = EXTERNALSOURCE_CULTURE;
        culture.Caption = "$general.language$";
        culture.Sort = SOURCE_CULTURENAME;
        culture.Wrap = false;
        gridElem.GridColumns.Columns.Add(culture);

        // Add site name column
        Column siteName = null;
        switch (ListingType)
        {
            default:
                siteName = new Column();
                siteName.Source = SOURCE_SITENAME;
                siteName.ExternalSourceName = EXTERNALSOURCE_SITENAME;
                siteName.Caption = "$general.site$";
                siteName.Wrap = false;
                gridElem.GridColumns.Columns.Add(siteName);
                break;

            case ListingTypeEnum.All:
                siteName = new Column();
                siteName.Source = SOURCE_NODESITEID;
                siteName.ExternalSourceName = EXTERNALSOURCE_SITEID;
                siteName.Caption = "$general.site$";
                siteName.Wrap = false;
                gridElem.GridColumns.Columns.Add(siteName);
                break;
        }
        // Prepare columns to select
        string baseColumns = SqlHelperClass.MergeColumns(new string[] { SOURCE_CLASSNAME, SOURCE_SITENAME, SOURCE_CLASSDISPLAYNAME, SOURCE_MODIFIEDWHEN, SOURCE_WORKFLOWSTEPDISPLAYNAME, SOURCE_NODEID, SOURCE_DOCUMENTCULTURE, SOURCE_DOCUMENTNAME, SOURCE_DOCUMENTNAMEPATH, SOURCE_NODEALIASPATH, SOURCE_NODELINKEDNODEID, SOURCE_CULTURENAME });
        // Set UniGrid options
        switch (ListingType)
        {
            case ListingTypeEnum.PageTemplateDocuments:
                gridElem.Columns = SqlHelperClass.MergeColumns(new string[] { baseColumns, SOURCE_NODESITEID, "NodeACLID", "NodeOwner" });
                gridElem.ObjectType = "cms.documentlist";
                break;

            case ListingTypeEnum.WorkflowDocuments:
                gridElem.Columns = SqlHelperClass.MergeColumns(new string[] { baseColumns, SELECTION_COLUMN, SOURCE_VERSION });
                gridElem.ObjectType = "cms.documentlist";
                break;

            case ListingTypeEnum.All:
                gridElem.ObjectType = "cms.userdocumentslist";
                break;

            default:
                gridElem.Columns = baseColumns;
                gridElem.ObjectType = "cms.documentlist";
                break;
        }
        switch (ListingType)
        {
            case ListingTypeEnum.OutdatedDocuments:
            case ListingTypeEnum.WorkflowDocuments:
                break;

            case ListingTypeEnum.All:
                gridElem.GridOptions.DisplayFilter = true;
                break;
        }
        switch (ListingType)
        {
            case ListingTypeEnum.WorkflowDocuments:
                gridElem.GridOptions.ShowSelection = true;
                gridElem.GridOptions.SelectionColumn = SELECTION_COLUMN;
                break;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do nothing
            return;
        }
        gridElem.IsLiveSite = IsLiveSite;
        gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;
        gridElem.HideControlForZeroRows = false;
        currentUserInfo = CMSContext.CurrentUser;

        // Initialize strings
        string strDays = GetString("MyDesk.OutdatedDocuments.Days");
        string strWeeks = GetString("MyDesk.OutdatedDocuments.Weeks");
        string strMonths = GetString("MyDesk.OutdatedDocuments.Months");
        string strYears = GetString("MyDesk.OutdatedDocuments.Years");

        // Set proper XML for control type
        switch (ListingType)
        {
            case ListingTypeEnum.CheckedOut:
                gridElem.ZeroRowsText = GetString("mydesk.ui.nochecked");
                gridElem.WhereCondition = "View_CMS_Tree_Joined.DocumentCheckedOutByUserID = @UserID";
                break;

            case ListingTypeEnum.MyDocuments:
                gridElem.ZeroRowsText = GetString("general.nodatafound");
                gridElem.WhereCondition = "View_CMS_Tree_Joined.NodeOwner = @UserID";
                break;

            case ListingTypeEnum.RecentDocuments:
                gridElem.ZeroRowsText = GetString("general.nodatafound");
                gridElem.WhereCondition = "((View_CMS_Tree_Joined.DocumentCreatedByUserID = @UserID OR View_CMS_Tree_Joined.DocumentModifiedByUserID = @UserID OR View_CMS_Tree_Joined.DocumentCheckedOutByUserID = @UserID))";
                break;

            case ListingTypeEnum.PendingDocuments:
                gridElem.ZeroRowsText = GetString("mydesk.ui.nowaitingdocs");
                gridElem.WhereCondition = "CMS_WorkflowStep.StepName <> 'edit' AND CMS_WorkflowStep.StepName <> 'published' AND CMS_WorkflowStep.StepName <> 'archived' AND (View_CMS_Tree_Joined.DocumentWorkflowStepID IN ( SELECT StepID FROM CMS_Workflowsteproles LEFT JOIN View_CMS_UserRole_MembershipRole_ValidOnly_Joined ON View_CMS_UserRole_MembershipRole_ValidOnly_Joined.RoleID = CMS_WorkflowStepRoles.RoleID WHERE View_CMS_UserRole_MembershipRole_ValidOnly_Joined.UserID = @UserID ) OR @UserID = -1)";
                break;

            case ListingTypeEnum.OutdatedDocuments:
                // Initialize controls
                if (!RequestHelper.IsPostBack())
                {
                    // Fill the dropdown list
                    drpFilter.Items.Add(strDays);
                    drpFilter.Items.Add(strWeeks);
                    drpFilter.Items.Add(strMonths);
                    drpFilter.Items.Add(strYears);

                    // Load default value
                    txtFilter.Text = "1";
                    drpFilter.SelectedValue = strYears;

                    // Bind dropdown lists
                    BindDropDowns();
                }

                gridElem.WhereCondition = "((DocumentCreatedByUserID = @UserID OR DocumentModifiedByUserID = @UserID OR DocumentCheckedOutByUserID = @UserID) AND " + SOURCE_MODIFIEDWHEN + "<=@OlderThan AND " + SOURCE_NODESITEID + "=@SiteID)";
                // Add where condition
                if (!string.IsNullOrEmpty(txtDocumentName.Text))
                {
                    gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, GetOutdatedWhereCondition(SOURCE_DOCUMENTNAME, drpDocumentName, txtDocumentName));
                }
                if (!string.IsNullOrEmpty(txtDocumentType.Text))
                {
                    gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, GetOutdatedWhereCondition(SOURCE_CLASSDISPLAYNAME, drpDocumentType, txtDocumentType));
                }

                gridElem.ZeroRowsText = GetString("mydesk.ui.nooutdated");
                // Show custom filter
                plcOutdatedFilter.Visible = true;
                break;

            case ListingTypeEnum.WorkflowDocuments:
                break;

            case ListingTypeEnum.PageTemplateDocuments:
                gridElem.ZeroRowsText = GetString("Administration-PageTemplate_Header.Documents.nodata");
                break;

            case ListingTypeEnum.CategoryDocuments:
                gridElem.ZeroRowsText = GetString("Category_Edit.Documents.nodata");
                break;

            case ListingTypeEnum.ProductDocuments:
                break;

            case ListingTypeEnum.TagDocuments:
                gridElem.ZeroRowsText = GetString("taggroup_edit.documents.nodata");
                break;

            case ListingTypeEnum.DocTypeDocuments:
                gridElem.ZeroRowsText = GetString("DocumentType_Edit_General.Documents.nodata");
                break;

            case ListingTypeEnum.All:
                gridElem.ZeroRowsText = GetString("mydesk.ui.nodata");
                break;
        }

        // Page Size
        if (!RequestHelper.IsPostBack() && !String.IsNullOrEmpty(ItemsPerPage))
        {
            gridElem.Pager.DefaultPageSize = ValidationHelper.GetInteger(ItemsPerPage, -1);
        }

        // Order
        switch (ListingType)
        {
            case ListingTypeEnum.WorkflowDocuments:
            case ListingTypeEnum.OutdatedDocuments:
            case ListingTypeEnum.PageTemplateDocuments:
            case ListingTypeEnum.CategoryDocuments:
            case ListingTypeEnum.TagDocuments:
            case ListingTypeEnum.ProductDocuments:
            case ListingTypeEnum.DocTypeDocuments:
                gridElem.OrderBy = SOURCE_DOCUMENTNAME;
                break;

            default:
                gridElem.OrderBy = OrderBy;
                break;
        }

        if (ListingType == ListingTypeEnum.All)
        {
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, String.Format("(UserID1 = {0} OR  UserID2 = {0} OR UserID3 = {0})", currentUserInfo.UserID));
        }

        // Create query parameters
        QueryDataParameters parameters = new QueryDataParameters();

        if (ListingType == ListingTypeEnum.OutdatedDocuments)
        {
            parameters.Add("@SiteID", CMSContext.CurrentSite.SiteID);

            DateTime olderThan = DateTime.Now;
            int dateTimeValue = ValidationHelper.GetInteger(txtFilter.Text, 0);
            if (drpFilter.SelectedValue == strDays)
            {
                olderThan = olderThan.AddDays(-dateTimeValue);
            }
            else if (drpFilter.SelectedValue == strWeeks)
            {
                olderThan = olderThan.AddDays(-dateTimeValue * 7);
            }
            else if (drpFilter.SelectedValue == strMonths)
            {
                olderThan = olderThan.AddMonths(-dateTimeValue);
            }
            else if (drpFilter.SelectedValue == strYears)
            {
                olderThan = olderThan.AddYears(-dateTimeValue);
            }

            parameters.Add("@OlderThan", olderThan);
        }
        // Initialize UserID query parameter
        int userID = currentUserInfo.UserID;
        if (ListingType == ListingTypeEnum.PendingDocuments)
        {
            if ((currentUserInfo.IsGlobalAdministrator) || (currentUserInfo.IsAuthorizedPerResource("CMS.Content", "manageworkflow")))
            {
                userID = -1;
            }
        }

        parameters.Add("@UserID", userID);

        // Document Age
        if (DocumentAge != String.Empty)
        {
            string[] ages = DocumentAge.Split(';');
            if (ages.Length == 2)
            {
                // Add from a to values to temp parameters
                int from = ValidationHelper.GetInteger(ages[1], 0);
                int to = ValidationHelper.GetInteger(ages[0], 0);

                if (from > 0)
                {
                    gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, SOURCE_MODIFIEDWHEN + " >= @FROM");
                    parameters.Add("@FROM", DateTime.Now.AddDays((-1) * from));
                }

                if (to > 0)
                {
                    gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, SOURCE_MODIFIEDWHEN + " <= @TO");
                    parameters.Add("@TO", DateTime.Now.AddDays((-1) * to));
                }
            }
        }

        // Site name
        if (!String.IsNullOrEmpty(SiteName) && (SiteName != UniGrid.ALL))
        {
            SiteInfo site = SiteInfoProvider.GetSiteInfo(SiteName);
            if (site != null)
            {
                gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, SOURCE_NODESITEID + " = " + site.SiteID);
                UniGrid.GridColumns.Columns.RemoveAll(c => (c.Source == SOURCE_SITENAME || c.Source == SOURCE_NODESITEID));
            }
        }

        // Path filter
        if (Path != String.Empty)
        {
            if (ListingType == ListingTypeEnum.All)
            {
                gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, SOURCE_DOCUMENTNAMEPATH + " LIKE N'" + CMSContext.ResolveCurrentPath(Path).Replace("'", "''") + "'");
            }
            else
            {
                gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, SOURCE_NODEALIASPATH + " LIKE N'" + CMSContext.ResolveCurrentPath(Path).Replace("'", "''") + "'");
            }
        }

        // Document type filer
        if (!String.IsNullOrEmpty(DocumentType))
        {
            string[] types = DocumentType.Split(';');
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, SqlHelperClass.GetWhereCondition<string>(SOURCE_CLASSNAME, types, true));
        }

        // Document name filter
        if (DocumentName != String.Empty)
        {
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, SOURCE_DOCUMENTNAME + " LIKE N'%" + SqlHelperClass.GetSafeQueryString(DocumentName, false) + "%'");
        }

        // Site running filter
        if ((SiteName == UniGrid.ALL) && DisplayOnlyRunningSites)
        {
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "SiteName IN (SELECT SiteName FROM CMS_Site WHERE SiteStatus = 'RUNNING')");
        }

        // Set parameters
        gridElem.QueryParameters = parameters;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        string action = "window.top.location.href = completeUrl;";
        switch (ListingType)
        {
            case ListingTypeEnum.PageTemplateDocuments:
                if (IsCMSDesk)
                {
                    action = "window.open(completeUrl, 'PageTemplateWindow');";
                }
                break;
        }

        // Initialize JavaScripts
        StringBuilder redirectScript = new StringBuilder();
        redirectScript.Append(
@"function SelectItem(nodeId, culture, url) {
    if (nodeId > 0) {
        var completeUrl = url + 'default.aspx?section=content&action=edit&nodeid=' + nodeId + '&culture=' + culture;
        ", action, @"
    }
    return false;
}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "docRedir", ScriptHelper.GetScript(redirectScript.ToString()));
    }

    #endregion


    #region "Grid events"

    /// <summary>
    /// External data binding handler.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        // Prepare variables
        int nodeId = 0;
        string culture = string.Empty;
        DataRowView data = null;
        sourceName = sourceName.ToLower();
        SiteInfo site = null;

        switch (sourceName)
        {
            // Edit button
            case EXTERNALSOURCE_EDIT:
                if (sender is ImageButton)
                {
                    ImageButton editButton = (ImageButton)sender;
                    data = UniGridFunctions.GetDataRowView(editButton.Parent as DataControlFieldCell);
                    site = GetSiteFromRow(data);
                    nodeId = ValidationHelper.GetInteger(data[SOURCE_NODEID], 0);
                    culture = ValidationHelper.GetString(data[SOURCE_DOCUMENTCULTURE], string.Empty);
                    string type = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(data, SOURCE_TYPE), string.Empty);

                    // Check permissions                    
                    if ((site.Status != SiteStatusEnum.Running) || (!CMSMyDeskPage.IsUserAuthorizedPerContent(site.SiteName) || ((ListingType == ListingTypeEnum.All) && (type == LISTINGTYPE_RECYCLEBIN))))
                    {
                        editButton.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Editdisabled.png");
                        editButton.OnClientClick = "return false";
                        editButton.Style.Add(HtmlTextWriterStyle.Cursor, "default");
                    }
                    else
                    {
                        editButton.OnClientClick = "return SelectItem(" + nodeId + ", '" + culture + "','" + ResolveSiteUrl(site) + "');";
                    }
                    return editButton;

                }
                return sender;

            // Preview button
            case EXTERNALSOURCE_PREVIEW:
                if (sender is ImageButton)
                {
                    ImageButton previewButton = (ImageButton)sender;
                    data = UniGridFunctions.GetDataRowView(previewButton.Parent as DataControlFieldCell);
                    site = GetSiteFromRow(data);
                    string type = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(data, SOURCE_TYPE), string.Empty);
                    if ((site.Status != SiteStatusEnum.Running) || ((ListingType == ListingTypeEnum.All) && (type == LISTINGTYPE_RECYCLEBIN)))
                    {
                        previewButton.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Viewdisabled.png");
                        previewButton.OnClientClick = "return false";
                        previewButton.Style.Add(HtmlTextWriterStyle.Cursor, "default");
                    }
                    else
                    {
                        nodeId = ValidationHelper.GetInteger(data[SOURCE_NODEID], 0);
                        culture = ValidationHelper.GetString(data[SOURCE_DOCUMENTCULTURE], string.Empty);
                        string nodeAliasPath = ValidationHelper.GetString(data[SOURCE_NODEALIASPATH], string.Empty);
                        // Generate preview URL
                        string url = CMSContext.GetUrl(nodeAliasPath, null, site.SiteName);
                        url = URLHelper.AddParameterToUrl(url, "viewmode", "2");
                        url = URLHelper.AddParameterToUrl(url, URLHelper.LanguageParameterName, culture);
                        previewButton.OnClientClick = "window.open('" + URLHelper.ResolveUrl(url) + "','LiveSite');return false;";
                    }
                    return previewButton;
                }
                return sender;

            // Document name column
            case EXTERNALSOURCE_DOCUMENTNAME:
                data = (DataRowView)parameter;

                string name = ValidationHelper.GetString(data[SOURCE_DOCUMENTNAME], string.Empty);
                nodeId = ValidationHelper.GetInteger(data[SOURCE_NODEID], 0);
                culture = ValidationHelper.GetString(data[SOURCE_DOCUMENTCULTURE], string.Empty);
                string className = ValidationHelper.GetString(data[SOURCE_CLASSNAME], string.Empty);
                site = GetSiteFromRow(data);

                if (name == string.Empty)
                {
                    name = GetString("general.notspecified");
                }
                // Add document type icon
                string result = string.Empty;
                switch (ListingType)
                {
                    case ListingTypeEnum.DocTypeDocuments:
                        break;

                    default:
                        result = "<img src=\"" + UIHelper.GetDocumentTypeIconUrl(Parent.Page, className, String.Empty, true) + "\" class=\"UnigridActionButton\" />";
                        break;
                }

                result += "<span style=\"vertical-align: bottom;\">" + HTMLHelper.HTMLEncode(TextHelper.LimitLength(name, 50)) + "</span>";

                if (ListingType != ListingTypeEnum.All)
                {
                    bool isLink = (data.Row.Table.Columns.Contains(SOURCE_NODELINKEDNODEID) && (data[SOURCE_NODELINKEDNODEID] != DBNull.Value));
                    if (isLink)
                    {
                        // Add link icon
                        result += UIHelper.GetDocumentMarkImage(Parent.Page, DocumentMarkEnum.Link);
                    }
                }
                return result;

            // Class name column
            case EXTERNALSOURCE_CLASSDISPLAYNAME:
                string displayName = ValidationHelper.GetString(parameter, string.Empty);
                if (sourceName.ToLower() == EXTERNALSOURCE_CLASSDISPLAYNAMETOOLTIP)
                {
                    displayName = TextHelper.LimitLength(displayName, 50);
                }
                if (displayName == string.Empty)
                {
                    displayName = "-";
                }
                return HTMLHelper.HTMLEncode(displayName);

            case EXTERNALSOURCE_DOCUMENTNAMETOOLTIP:
                data = (DataRowView)parameter;
                return UniGridFunctions.DocumentNameTooltip(data);

            case EXTERNALSOURCE_STEPDISPLAYNAME:
                // Step display name
                string stepName = ValidationHelper.GetString(parameter, string.Empty);
                if (stepName == string.Empty)
                {
                    stepName = "-";
                }
                return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(stepName));

            case EXTERNALSOURCE_STEPNAME:
                // Step display name from ID
                int stepId = ValidationHelper.GetInteger(parameter, 0);
                if (stepId > 0)
                {
                    WorkflowStepInfo wsi = WorkflowStepInfoProvider.GetWorkflowStepInfo(stepId);
                    if (wsi != null)
                    {
                        return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(wsi.StepDisplayName));
                    }
                }
                return "-";

            case EXTERNALSOURCE_CULTURE:
                data = (DataRowView)parameter;
                culture = ValidationHelper.GetString(data[SOURCE_DOCUMENTCULTURE], string.Empty);
                // Add icon
                if (culture != String.Empty)
                {
                    return UniGridFunctions.DocumentCultureFlag(data, Page);
                }

                return "-";

            // Version column
            case EXTERNALSOURCE_VERSION:
                if (parameter == DBNull.Value)
                {
                    parameter = "-";
                }
                parameter = HTMLHelper.HTMLEncode(parameter.ToString());
                return parameter;

            // Site name column
            case EXTERNALSOURCE_SITENAME:
                string siteName = ValidationHelper.GetString(parameter, string.Empty);
                siteInfo = SiteInfoProvider.GetSiteInfo(siteName);
                return HTMLHelper.HTMLEncode(siteInfo.DisplayName);

            case EXTERNALSOURCE_SITEID:
                int siteId = ValidationHelper.GetInteger(parameter, 0);
                siteInfo = SiteInfoProvider.GetSiteInfo(siteId);
                return HTMLHelper.HTMLEncode(siteInfo.DisplayName);

            // Document timestamp column
            case EXTERNALSOURCE_MODIFIEDWHEN:
            case EXTERNALSOURCE_MODIFIEDWHENTOOLTIP:
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    return string.Empty;
                }
                else
                {
                    if (currentSiteInfo == null)
                    {
                        currentSiteInfo = CMSContext.CurrentSite;
                    }
                    bool displayGMT = (sourceName == EXTERNALSOURCE_MODIFIEDWHENTOOLTIP);
                    DateTime time = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                    return TimeZoneHelper.ConvertToUserTimeZone(time, displayGMT, currentUserInfo, currentSiteInfo);
                }
        }

        return parameter;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Gets where condition based on value of given controls.
    /// </summary>
    /// <param name="column">Column to compare</param>
    /// <param name="drpOperator">List control with operator</param>
    /// <param name="valueBox">Text control with value</param>
    /// <returns>Where condition for outdated documents</returns>
    private static string GetOutdatedWhereCondition(string column, ListControl drpOperator, ITextControl valueBox)
    {
        string condition = drpOperator.SelectedValue;
        string value = SqlHelperClass.GetSafeQueryString(valueBox.Text, false);
        value = TextHelper.LimitLength(value, 100);

        string where = column + " ";
        if (string.IsNullOrEmpty(value))
        {
            where = string.Empty;
        }
        else
        {
            // Create condition based on operator
            switch (condition)
            {
                case "LIKE":
                case "NOT LIKE":
                    where += condition + " N'%" + value + "%'";
                    break;

                case "=":
                case "<>":
                    where += condition + " N'" + value + "'";
                    break;

                default:
                    where = string.Empty;
                    break;
            }
        }
        return where;
    }


    /// <summary>
    /// Fills given site object based on data from given row.
    /// </summary>
    /// <param name="data">Row with site reference</param>
    /// <returns>Initialized site info</returns>
    private SiteInfo GetSiteFromRow(DataRowView data)
    {
        SiteInfo site = null;
        if (ListingType == ListingTypeEnum.All)
        {
            site = SiteInfoProvider.GetSiteInfo(ValidationHelper.GetInteger(data[SOURCE_NODESITEID], 0));
        }
        else
        {
            site = SiteInfoProvider.GetSiteInfo(ValidationHelper.GetString(data[SOURCE_SITENAME], String.Empty));
        }
        return site;
    }


    /// <summary>
    /// Binds filter dropdown lists with conditions.
    /// </summary>
    private void BindDropDowns()
    {
        string[] conditions = { "LIKE", "NOT LIKE", "=", "<>" };
        drpDocumentName.DataSource = conditions;
        drpDocumentType.DataSource = conditions;
        drpDocumentName.DataBind();
        drpDocumentType.DataBind();
    }


    /// <summary>
    /// Make url for site in form 'http(s)://sitedomain/application/cmsdesk'.
    /// </summary>
    /// <param name="site">Site info object</param>
    private string ResolveSiteUrl(SiteInfo site)
    {
        string sitedomain = site.DomainName.TrimEnd('/');

        string application = null;
        // Support of multiple web sites on single domain
        if (!sitedomain.Contains("/"))
        {
            application = ResolveUrl("~/.").TrimEnd('/');
        }

        // Application includes string '/cmsdesk'.
        application += "/cmsdesk/";

        return URLHelper.Url.Scheme + "://" + sitedomain + application;
    }

    #endregion
}
