using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.DataEngine;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;
using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Content_CMSDesk_View_Listing : CMSContentPage, ICallbackEventHandler
{
    #region "Private and protected variables & enums"

    // Private fields
    private CurrentUserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;
    private TreeProvider tree = null;
    private TreeNode node = null;
    private ArrayList mFlagsControls = null;
    private DataSet mSiteCultures = null;
    private DialogConfiguration mConfig = null;

    private string mDefaultSiteCulture = null;
    private string currentSiteName = null;
    private int nodeId = 0;
    private bool dataLoaded = false;
    private bool allSelected = false;
    private bool checkPermissions = false;
    private string aliasPath = null;
    private string urlParameter = string.Empty;

    private Action callbackAction = Action.Move;

    // Possible actions
    private enum Action
    {
        SelectAction = 0,
        Move = 1,
        Copy = 2,
        Link = 3,
        Delete = 4,
        Publish = 5,
        Archive = 6
    }

    // Action scope
    private enum What
    {
        SelectedDocuments = 0,
        AllDocuments = 1
    }

    // Action scope for callback handling
    private enum Argument
    {
        Action = 0,
        AllSelected = 1,
        Items = 2
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns selected culture.
    /// </summary>
    private string SelectedCulture
    {
        get
        {
            Control postbackControl = ControlsHelper.GetPostBackControl(Page);
            return ValidationHelper.GetString((postbackControl == btnShow) ? cultureElem.Value : ViewState["SelectedCulture"], string.Empty);
        }
        set
        {
            cultureElem.Value = value;
            ViewState["SelectedCulture"] = value;
        }
    }


    /// <summary>
    /// Default culture of the site.
    /// </summary>
    private string DefaultSiteCulture
    {
        get
        {
            return mDefaultSiteCulture ?? (mDefaultSiteCulture = CultureHelper.GetDefaultCulture(currentSiteName));
        }
    }


    /// <summary>
    /// Hashtable with document flags controls.
    /// </summary>
    private ArrayList FlagsControls
    {
        get
        {
            return mFlagsControls ?? (mFlagsControls = new ArrayList());
        }
    }


    /// <summary>
    /// Site cultures.
    /// </summary>
    private DataSet SiteCultures
    {
        get
        {
            if (mSiteCultures == null)
            {
                mSiteCultures = CultureInfoProvider.GetSiteCultures(currentSiteName).Copy();
                if (!DataHelper.DataSourceIsEmpty(mSiteCultures))
                {
                    DataTable cultureTable = mSiteCultures.Tables[0];
                    DataRow[] defaultCultureRow = cultureTable.Select("CultureCode='" + DefaultSiteCulture + "'");

                    // Ensure default culture to be first
                    DataRow dr = cultureTable.NewRow();
                    if (defaultCultureRow.Length > 0)
                    {
                        dr.ItemArray = defaultCultureRow[0].ItemArray;
                        cultureTable.Rows.InsertAt(dr, 0);
                        cultureTable.Rows.Remove(defaultCultureRow[0]);
                    }
                }
            }
            return mSiteCultures;
        }
    }


    /// <summary>
    /// Gets the configuration for Copy and Move dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (mConfig == null)
            {
                mConfig = new DialogConfiguration();
                mConfig.ContentSelectedSite = CMSContext.CurrentSiteName;
                mConfig.OutputFormat = OutputFormatEnum.Custom;
                mConfig.SelectableContent = SelectableContentEnum.AllContent;
                mConfig.HideAttachments = false;
            }
            return mConfig;
        }
    }


    /// <summary>
    /// Holds current where condition of filter.
    /// </summary>
    private string CurrentWhereCondition
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CurrentWhereCondition"], string.Empty);
        }
        set
        {
            ViewState["CurrentWhereCondition"] = value;
        }
    }


    /// <summary>
    /// Dialog control identificator.
    /// </summary>
    private string Identificator
    {
        get
        {
            string identificator = hdnIdentificator.Value;
            if (string.IsNullOrEmpty(identificator))
            {
                identificator = Guid.NewGuid().ToString();
                hdnIdentificator.Value = identificator;
            }

            return identificator;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterScriptFile(this, @"~/CMSModules/Content/CMSDesk/View/Listing.js");

        currentSiteName = CMSContext.CurrentSiteName;
        currentUserInfo = CMSContext.CurrentUser;

        // Current Node ID
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("Content.ListingTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Menu/Listing.png");

        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "list_tab";

        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Listing.ParentDirectory");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/Listing/parent.png");
        CurrentMaster.HeaderActions.Actions = actions;

        if (nodeId > 0)
        {
            tree = new TreeProvider(currentUserInfo);
            checkPermissions = tree.CheckDocumentUIPermissions(currentSiteName);
            node = tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
            // Set edited document
            EditedDocument = node;

            if (node != null)
            {
                if (currentUserInfo.IsAuthorizedPerDocument(node, NodePermissionsEnum.ExploreTree) != AuthorizationResultEnum.Allowed)
                {
                    RedirectToCMSDeskAccessDenied("CMS.Content", "exploretree");
                }

                aliasPath = node.NodeAliasPath;

                // Setup the link to the parent document
                if ((node.NodeClassName.ToLower() != "cms.root") && (currentUserInfo.UserStartingAliasPath.ToLower() != node.NodeAliasPath.ToLower()))
                {
                    CurrentMaster.HeaderActions.Actions[0, 3] = "javascript:SelectItem(" + node.NodeParentID + ");";
                }
                else
                {
                    CurrentMaster.HeaderActions.Visible = false;
                    CurrentMaster.PanelBody.FindControl("pnlActions").Visible = false;
                }
            }

            ScriptHelper.RegisterProgress(this);
            ScriptHelper.RegisterDialogScript(this);
            ScriptHelper.RegisterJQuery(this);

            InitDropdowLists();

            cultureElem.DropDownCultures.Width = 222;

            // Prepare JavaScript for actions
            StringBuilder actionScript = new StringBuilder();
            actionScript.Append(
@"function PerformAction(selectionFunction, selectionField, dropId){
    var label = document.getElementById('" + lblInfo.ClientID + @"');
    var whatDrp = document.getElementById('" + drpWhat.ClientID + @"');
    var action = document.getElementById(dropId).value;
    var selectionFieldElem = document.getElementById(selectionField);
    var allSelected = " + (int)What.SelectedDocuments + @";
    if (action == '" + (int)Action.SelectAction + @"'){
        label.innerHTML = '" + GetString("massaction.selectsomeaction") + @"';
        return false;
    }
    if(whatDrp.value == '" + (int)What.AllDocuments + @"'){
        allSelected = " + (int)What.AllDocuments + @";
    }
    var items = selectionFieldElem.value;
    if(!eval(selectionFunction) || whatDrp.value == '" + (int)What.AllDocuments + @"'){
        var argument = '|' + allSelected + '|' + items;
        switch(action){
            case '" + (int)Action.Move + @"':
                argument = '" + Action.Move + "' + argument;" + ClientScript.GetCallbackEventReference(this, "argument", "OpenModal", string.Empty) + @";
                break;

            case '" + (int)Action.Copy + @"':
                argument = '" + Action.Copy + "' + argument;" + ClientScript.GetCallbackEventReference(this, "argument", "OpenModal", string.Empty) + @";
                break;

            case '" + (int)Action.Link + @"':
                argument = '" + Action.Link + "' + argument;" + ClientScript.GetCallbackEventReference(this, "argument", "OpenModal", string.Empty) + @";
                break;

            case '" + (int)Action.Delete + @"':
                argument = '" + Action.Delete + "' + argument;" + ClientScript.GetCallbackEventReference(this, "argument", "Redirect", string.Empty) + @";
                break;

            case '" + (int)Action.Publish + @"':
                argument = '" + Action.Publish + "' + argument;" + ClientScript.GetCallbackEventReference(this, "argument", "Redirect", string.Empty) + @";
                break;

            case '" + (int)Action.Archive + @"':
                argument = '" + Action.Archive + "' + argument;" + ClientScript.GetCallbackEventReference(this, "argument", "Redirect", string.Empty) + @";
                break;

            default:
                return false;
        }
    }
    else{
        label.innerHTML = '" + GetString("documents.selectdocuments") + @"';
    }
    return false;
}

function OpenModal(arg, context){
    modalDialog(arg,'actionDialog','90%', '85%');
}

function Redirect(arg, context){
    document.location.replace(arg);
}");

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "actionScript", ScriptHelper.GetScript(actionScript.ToString()));

            // Add action to button
            btnOk.OnClientClick = "return PerformAction('" + gridDocuments.GetCheckSelectionScript() + "','" + gridDocuments.GetSelectionFieldClientID() + "','" + drpAction.ClientID + "');";

            // Initialize dropdown lists
            if (!RequestHelper.IsPostBack())
            {
                drpAction.Items.Add(new ListItem(GetString("general." + Action.SelectAction), Convert.ToInt32(Action.SelectAction).ToString()));
                drpAction.Items.Add(new ListItem(GetString("general." + Action.Move), Convert.ToInt32(Action.Move).ToString()));
                drpAction.Items.Add(new ListItem(GetString("general." + Action.Copy), Convert.ToInt32(Action.Copy).ToString()));
                drpAction.Items.Add(new ListItem(GetString("general." + Action.Link), Convert.ToInt32(Action.Link).ToString()));
                drpAction.Items.Add(new ListItem(GetString("general." + Action.Delete), Convert.ToInt32(Action.Delete).ToString()));
                if (currentUserInfo.IsGlobalAdministrator || currentUserInfo.IsAuthorizedPerResource("CMS.Content", "ManageWorkflow"))
                {
                    drpAction.Items.Add(new ListItem(GetString("general." + Action.Publish), Convert.ToInt32(Action.Publish).ToString()));
                    drpAction.Items.Add(new ListItem(GetString("general." + Action.Archive), Convert.ToInt32(Action.Archive).ToString()));
                }

                drpWhat.Items.Add(new ListItem(GetString("contentlisting." + What.SelectedDocuments), Convert.ToInt32(What.SelectedDocuments).ToString()));
                drpWhat.Items.Add(new ListItem(GetString("contentlisting." + What.AllDocuments), Convert.ToInt32(What.AllDocuments).ToString()));
            }

            // Setup the grid
            gridDocuments.OnExternalDataBound += gridDocuments_OnExternalDataBound;
            gridDocuments.OnBeforeDataReload += gridDocuments_OnBeforeDataReload;
            gridDocuments.OnDataReload += gridDocuments_OnDataReload;
            gridDocuments.ZeroRowsText = GetString("content.nochilddocumentsfound");
            gridDocuments.ShowActionsMenu = true;
            if (node != null)
            {
                gridDocuments.WhereCondition = "NodeParentID = " + node.NodeID + " AND NodeLevel = " + (node.NodeLevel + 1);
            }

            // Initialize columns
            string columns = @"DocumentLastVersionName, DocumentName, NodeParentID,
                    ClassDisplayName, DocumentModifiedWhen, Published, DocumentLastVersionNumber, DocumentMenuRedirectURL, DocumentLastVersionMenuRedirectUrl, DocumentIsArchived, DocumentCheckedOutByUserID,
                    DocumentPublishedVersionHistoryID, DocumentWorkflowStepID, DocumentCheckedOutVersionHistoryID, DocumentNamePath, DocumentPublishFrom, DocumentType, DocumentLastVersionType, NodeAliasPath";

            if (checkPermissions)
            {
                columns = SqlHelperClass.MergeColumns(columns, TreeProvider.SECURITYCHECK_REQUIRED_COLUMNS);
            }
            gridDocuments.Columns = columns;

            StringBuilder refreshScripts = new StringBuilder();
            refreshScripts.Append(
@"function RefreshTree()
{
    if((parent != null) && (parent.parent != null) && (parent.parent.frames['contenttree'] != null) && (parent.parent.frames['contenttree'].RefreshNode != null))
    {
        parent.parent.frames['contenttree'].RefreshNode(", nodeId, @",", nodeId, @");
    }
}
function ClearSelection()
{ 
", gridDocuments.GetClearSelectionScript(), @"
}
function RefreshGrid()
{
    ClearSelection();
    RefreshTree();",
    ClientScript.GetPostBackEventReference(btnShow, "null"), @"
}");
            // Register refresh scripts
            string refreshScript = ScriptHelper.GetScript(refreshScripts.ToString());
            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "refreshListing", refreshScript);

            // Get all possible columns to retrieve
            IDataClass nodeClass = DataClassFactory.NewDataClass("CMS.Tree");
            DocumentInfo di = new DocumentInfo();
            gridDocuments.AllColumns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(di.ColumnNames.ToArray()), SqlHelperClass.MergeColumns(nodeClass.ColumnNames.ToArray()));

        }
    }


    /// <summary>
    /// OnPreRender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if (!dataLoaded)
        {
            gridDocuments.ReloadData();
        }

        // Hide column with languages if only one culture is assigned to the site
        if (DataHelper.DataSourceIsEmpty(SiteCultures) || (SiteCultures.Tables[0].Rows.Count <= 1))
        {
            DataControlField languagesColumn = gridDocuments.GridView.Columns[7];
            languagesColumn.Visible = false;
            plcLang.Visible = false;
        }
        else
        {
            if (FlagsControls.Count != 0)
            {
                // Get all document node IDs
                string nodeIds = null;
                foreach (DocumentFlagsControl ucDocFlags in FlagsControls)
                {
                    nodeIds += ucDocFlags.NodeID + ",";
                }

                if (nodeIds != null)
                {
                    nodeIds = nodeIds.TrimEnd(',');
                }

                // Get all documents
                tree.SelectQueryName = "SelectVersions";

                string columns = "NodeID, VersionNumber, DocumentCulture, DocumentModifiedWhen, DocumentLastPublished";
                if (checkPermissions)
                {
                    columns = SqlHelperClass.MergeColumns(columns, TreeProvider.SECURITYCHECK_REQUIRED_COLUMNS);
                }

                DataSet dsDocs = null;
                if (!checkPermissions || (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed))
                {
                    dsDocs = tree.SelectNodes(currentSiteName, "/%", TreeProvider.ALL_CULTURES, false, null, "NodeID IN (" + nodeIds + ")", null, -1, false, 0, columns);
                }

                // Check permissions
                if (checkPermissions)
                {
                    dsDocs = TreeSecurityProvider.FilterDataSetByPermissions(dsDocs, NodePermissionsEnum.Read, currentUserInfo);
                }

                if (!DataHelper.DataSourceIsEmpty(dsDocs))
                {
                    GroupedDataSource gDSDocs = new GroupedDataSource(dsDocs, "NodeID");

                    // Initialize the document flags controls
                    foreach (DocumentFlagsControl ucDocFlags in FlagsControls)
                    {
                        ucDocFlags.DataSource = gDSDocs;
                        ucDocFlags.ReloadData();
                    }
                }
            }
        }

        base.OnPreRender(e);
    }

    #endregion


    #region "Control events"

    protected void btnShow_OnClick(object sender, EventArgs e)
    {
        pnlGrid.Update();
    }

    #endregion


    #region "Grid events"

    protected void gridDocuments_OnBeforeDataReload()
    {
        ApplyFilterWhereCondition();
    }


    protected DataSet gridDocuments_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        dataLoaded = true;

        if (node != null)
        {
            // Get the site
            SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
            if (si != null)
            {
                DataSet ds = null;
                if (!checkPermissions || (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed))
                {
                    // Get documents
                    columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, columns);
                    ds = DocumentHelper.GetDocuments(currentSiteName, TreeProvider.ALL_DOCUMENTS, TreeProvider.ALL_CULTURES, true, null, completeWhere, "NodeOrder ASC, NodeName ASC, NodeAlias ASC", -1, false, gridDocuments.TopN, columns, tree);
                }
                else
                {
                    gridDocuments.ZeroRowsText = GetString("ContentTree.ReadDocumentDenied");
                }

                // Check permissions
                if (checkPermissions)
                {
                    ds = TreeSecurityProvider.FilterDataSetByPermissions(ds, NodePermissionsEnum.Read, currentUserInfo);
                }

                if (DataHelper.DataSourceIsEmpty(ds))
                {
                    pnlFooter.Visible = false;

                    string cultureFilterValue = SelectedCulture;
                    string cultureOperatorValue = ValidationHelper.GetString(drpLanguage.SelectedValue, null);
                    bool cultureFilterOff = (cultureFilterValue == "##ANY##") || (cultureFilterValue == string.Empty) && (cultureOperatorValue == "=");
                    bool typeFilterOff = string.IsNullOrEmpty(txtType.Text);
                    bool nameFilterOff = string.IsNullOrEmpty(txtName.Text);

                    // If filter is not set
                    if (typeFilterOff && nameFilterOff && cultureFilterOff && !RequestHelper.IsPostBack())
                    {
                        pnlFilter.Visible = false;
                    }
                }
                else
                {
                    pnlFooter.Visible = true;
                }

                // Set the data source
                return ds;
            }
        }
        return null;
    }


    /// <summary>
    /// External data binding handler.
    /// </summary>
    protected object gridDocuments_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        int currentNodeId = 0;

        sourceName = sourceName.ToLower();
        switch (sourceName)
        {
            case "published":
                {
                    // Published state
                    bool published = ValidationHelper.GetBoolean(parameter, true);
                    if (published)
                    {
                        return "<span class=\"DocumentPublishedYes\">" + GetString("General.Yes") + "</span>";
                    }
                    else
                    {
                        return "<span class=\"DocumentPublishedNo\">" + GetString("General.No") + "</span>";
                    }
                }

            case "versionnumber":
                {
                    // Version number
                    if (parameter == DBNull.Value)
                    {
                        parameter = "-";
                    }
                    parameter = HTMLHelper.HTMLEncode(parameter.ToString());

                    return parameter;
                }

            case "documentname":
                {
                    // Document name
                    DataRowView data = (DataRowView)parameter;
                    string className = ValidationHelper.GetString(data["ClassName"], string.Empty);
                    string name = ValidationHelper.GetString(data["DocumentName"], string.Empty);
                    string culture = ValidationHelper.GetString(data["DocumentCulture"], string.Empty);
                    string cultureString = null;

                    currentNodeId = ValidationHelper.GetInteger(data["NodeID"], 0);

                    int nodeParentId = ValidationHelper.GetInteger(data["NodeParentID"], 0);
                    string preferredCulture = CMSContext.PreferredCultureCode;

                    // Default culture
                    if (culture.ToLower() != preferredCulture.ToLower())
                    {
                        cultureString = " (" + culture + ")";
                    }

                    string tooltip = UniGridFunctions.DocumentNameTooltip(data);

                    string imageUrl = null;
                    if (className.Equals("cms.file", StringComparison.InvariantCultureIgnoreCase))
                    {
                        string extension = ValidationHelper.GetString(data["DocumentType"], "");
                        imageUrl = GetFileIconUrl(extension, "List");
                    }
                    // Use class icons
                    else
                    {
                        imageUrl = ResolveUrl(GetDocumentTypeIconUrl(className));
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append(
                        "<img src=\"", imageUrl, "\" class=\"UnigridActionButton\" /> ",
                        "<a href=\"javascript: SelectItem(", currentNodeId, ", ", nodeParentId, ");\" ",
                        "onmouseout=\"UnTip()\" onmouseover=\"Tip('", tooltip, "')\">",
                        HTMLHelper.HTMLEncode(TextHelper.LimitLength(name, 50)), cultureString,
                        "</a>"
                    );

                    // Prepare parameters
                    int workflowStepId = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(data, "DocumentWorkflowStepID"), 0);
                    string stepName = null;

                    if (workflowStepId > 0)
                    {
                        WorkflowStepInfo stepInfo = WorkflowStepInfoProvider.GetWorkflowStepInfo(workflowStepId);
                        if (stepInfo != null)
                        {
                            stepName = stepInfo.StepName;
                        }
                    }

                    // Create data container
                    IDataContainer container = new DataRowContainer(data);

                    // Add icons
                    sb.Append(" ", UIHelper.GetDocumentMarks(Page, currentSiteName, preferredCulture, stepName, container));

                    return sb.ToString();
                }

            case "documentculture":
                {
                    // Dynamically load document flags control
                    DocumentFlagsControl ucDocFlags = Page.LoadControl("~/CMSAdminControls/UI/DocumentFlags.ascx") as DocumentFlagsControl;

                    // Set document flags properties
                    if (ucDocFlags != null)
                    {
                        // Get node ID
                        currentNodeId = ValidationHelper.GetInteger(parameter, 0);

                        ucDocFlags.ID = "docFlags" + currentNodeId;
                        ucDocFlags.SiteCultures = SiteCultures;
                        ucDocFlags.NodeID = currentNodeId;
                        ucDocFlags.StopProcessing = true;

                        // Keep the control for later usage
                        FlagsControls.Add(ucDocFlags);
                        return ucDocFlags;
                    }
                }
                break;

            case "modifiedwhen":
            case "modifiedwhentooltip":
                // Modified when
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    return "";
                }
                else
                {
                    DateTime modifiedWhen = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                    if (currentUserInfo == null)
                    {
                        currentUserInfo = CMSContext.CurrentUser;
                    }
                    if (currentSiteInfo == null)
                    {
                        currentSiteInfo = CMSContext.CurrentSite;
                    }

                    bool displayGMT = (sourceName == "modifiedwhentooltip");
                    return TimeZoneHelper.ConvertToUserTimeZone(modifiedWhen, displayGMT, currentUserInfo, currentSiteInfo);
                }

            case "classdisplayname":
            case "classdisplaynametooltip":
                // Localize class display name
                if (!string.IsNullOrEmpty(parameter.ToString()))
                {
                    return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(parameter.ToString()));
                }

                return "";
        }

        return parameter;
    }

    #endregion


    #region "Methods"

    private void ApplyFilterWhereCondition()
    {
        string whereCondition = null;
        string documentName = txtName.Text.Trim().Replace("'", "''");
        documentName = TextHelper.LimitLength(documentName, 100);
        // Name
        if (documentName != string.Empty)
        {
            switch (drpOperator.SelectedValue)
            {
                case "NOT LIKE":
                case "LIKE":
                    whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "DocumentName " + drpOperator.SelectedValue + " N'%" + documentName + "%'");
                    break;

                case "=":
                case "<>":
                    whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "DocumentName " + drpOperator.SelectedValue + " N'" + documentName + "'");
                    break;
            }
        }

        string documentType = txtType.Text.Trim().Replace("'", "''");
        documentType = TextHelper.LimitLength(documentType, 100);
        // Type
        if (documentType != string.Empty)
        {
            switch (drpOperator2.SelectedValue)
            {
                case "NOT LIKE":
                case "LIKE":
                    whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "ClassDisplayName " + drpOperator2.SelectedValue + " N'%" + documentType + "%'");
                    break;

                case "=":
                case "<>":
                    whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "ClassDisplayName " + drpOperator2.SelectedValue + " N'" + documentType + "'");
                    break;
            }
        }

        // Translated
        string val = SelectedCulture;
        if (val == string.Empty)
        {
            val = "##ANY##";
        }

        if (val != "##ANY##")
        {
            switch (val)
            {
                case "##ALL##":
                    whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "((SELECT COUNT(*) FROM View_CMS_Tree_Joined AS TreeView WHERE TreeView.NodeID = View_CMS_Tree_Joined_Versions.NodeID) " + SqlHelperClass.GetSafeQueryString(drpLanguage.SelectedValue, false) + " " + SiteCultures.Tables[0].Rows.Count + ")");
                    break;

                default:
                    string oper = (drpLanguage.SelectedValue == "<>") ? "NOT" : "";
                    whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "NodeID " + oper + " IN (SELECT NodeID FROM View_CMS_Tree_Joined AS TreeView WHERE TreeView.NodeID = NodeID AND DocumentCulture = '" + SqlHelperClass.GetSafeQueryString(val, false) + "')");
                    break;
            }
        }
        else if (drpLanguage.SelectedValue == "<>")
        {
            whereCondition = SqlHelperClass.NO_DATA_WHERE;
        }

        CurrentWhereCondition = SqlHelperClass.AddWhereCondition(CurrentWhereCondition, whereCondition);
        gridDocuments.WhereClause = whereCondition;
    }


    private void InitDropdowLists()
    {
        // Init cultures
        cultureElem.DropDownCultures.CssClass = "SelectorDropDown";
        cultureElem.AddDefaultRecord = false;
        cultureElem.UpdatePanel.RenderMode = UpdatePanelRenderMode.Inline;
        cultureElem.SpecialFields = new string[,] { 
            { GetString("transman.anyculture"), "##ANY##" } ,
            { GetString("transman.allcultures"), "##ALL##" } 
        };

        // Init operands
        if (drpLanguage.Items.Count == 0)
        {
            drpLanguage.Items.Add(new ListItem(GetString("transman.translatedto"), "="));
            drpLanguage.Items.Add(new ListItem(GetString("transman.nottranslatedto"), "<>"));
        }

        // Init operands
        if (drpOperator.Items.Count == 0)
        {
            drpOperator.Items.Add(new ListItem("LIKE", "LIKE"));
            drpOperator.Items.Add(new ListItem("NOT LIKE", "NOT LIKE"));
            drpOperator.Items.Add(new ListItem("=", "="));
            drpOperator.Items.Add(new ListItem("<>", "<>"));
        }

        // Init operands
        if (drpOperator2.Items.Count == 0)
        {
            drpOperator2.Items.Add(new ListItem("LIKE", "LIKE"));
            drpOperator2.Items.Add(new ListItem("NOT LIKE", "NOT LIKE"));
            drpOperator2.Items.Add(new ListItem("=", "="));
            drpOperator2.Items.Add(new ListItem("<>", "<>"));
        }
    }

    #endregion


    #region "ICallbackEventHandler Members"

    public string GetCallbackResult()
    {
        string returnUrl = string.Empty;
        Hashtable parameters = new Hashtable();

        switch (callbackAction)
        {
            case Action.Copy:
            case Action.Move:
            case Action.Link:

                // Get default dialog URL
                Config.CustomFormatCode = callbackAction.ToString().ToLower();
                returnUrl = CMSDialogHelper.GetDialogUrl(Config, false, false, null, false);

                // Adjust URL to our needs
                returnUrl = URLHelper.RemoveParameterFromUrl(returnUrl, "hash");
                returnUrl = URLHelper.AddParameterToUrl(returnUrl, "multiple", "true");

                // Process parameters
                if (!string.IsNullOrEmpty(urlParameter))
                {
                    returnUrl = URLHelper.AddParameterToUrl(returnUrl, "sourcenodeids", urlParameter);
                }
                else if (!string.IsNullOrEmpty(aliasPath))
                {
                    parameters["parentalias"] = aliasPath;
                }

                if (!string.IsNullOrEmpty(CurrentWhereCondition))
                {
                    parameters["where"] = CurrentWhereCondition;
                }

                break;

            case Action.Delete:
                returnUrl = "../Delete.aspx?multiple=true";

                // Process parameters
                if (allSelected)
                {
                    if (!string.IsNullOrEmpty(aliasPath))
                    {
                        parameters["parentaliaspath"] = aliasPath;
                    }
                    if (!string.IsNullOrEmpty(CurrentWhereCondition))
                    {
                        parameters["where"] = CurrentWhereCondition;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(urlParameter))
                    {
                        returnUrl = URLHelper.AddParameterToUrl(returnUrl, "nodeId", urlParameter);
                    }
                }
                break;

            case Action.Archive:
            case Action.Publish:
                returnUrl = "../PublishArchive.aspx?multiple=true";
                returnUrl = URLHelper.AddParameterToUrl(returnUrl, "action", callbackAction.ToString());

                // Process parameters
                if (allSelected)
                {
                    if (!string.IsNullOrEmpty(aliasPath))
                    {
                        parameters["parentaliaspath"] = aliasPath;
                    }
                    if (!string.IsNullOrEmpty(CurrentWhereCondition))
                    {
                        parameters["where"] = CurrentWhereCondition;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(urlParameter))
                    {
                        parameters["nodeids"] = urlParameter;
                    }
                }
                break;
        }

        // Store parameters to window helper
        WindowHelper.Add(Identificator, parameters);

        // Add parameters identifier and hash, encode query string
        returnUrl = URLHelper.AddParameterToUrl(returnUrl, "params", Identificator);
        returnUrl = ResolveUrl(returnUrl);
        returnUrl = URLHelper.AddParameterToUrl(returnUrl, "hash", QueryHelper.GetHash(URLHelper.GetQuery(returnUrl)));

        return returnUrl;
    }


    public void RaiseCallbackEvent(string eventArgument)
    {
        string[] arguments = eventArgument.Trim('|').Split('|');
        if (arguments.Length > 1)
        {
            // Parse callback arguments
            callbackAction = (Action)Enum.Parse(typeof(Action), arguments[(int)Argument.Action]);
            allSelected = ValidationHelper.GetBoolean(arguments[(int)Argument.AllSelected], false);
            if (!allSelected)
            {
                // Get selected node identifiers
                for (int i = (int)Argument.Items; i < arguments.Length; i++)
                {
                    urlParameter += arguments[i] + "|";
                }
            }
        }
    }

    #endregion
}