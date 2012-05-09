using System;
using System.Xml;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.IO;
using CMS.EventLog;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.Controls;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.URLRewritingEngine;
using CMS.UIControls.UniGridConfig;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.SiteProvider;

using Action = CMS.UIControls.UniGridConfig.Action;

public partial class CMSAdminControls_UI_UniGrid_UniGrid : UniGrid, IUniPageable
{
    #region "Constants"

    private const string SELECT_ALL_PREFIX = "UG_SelectAll_";

    private const string SELECT_PREFIX = "UG_Select_";

    private const string CMD_PREFIX = "UG_Cmd_";

    private const string DESTROY_OBJECT_PREFIX = "UG_DestroyObj_";

    private const int halfPageCountLimit = 1000;

    #endregion


    #region "Variables"

    private Button showButton = null;

    private int rowIndex = 1;

    private bool resetSelection = false;

    private bool visiblePagesSet = false;

    private static string DEFAULT_ACTIONS_MENU = "~/CMSAdminControls/UI/UniGrid/Controls/UniGridMenu.ascx";

    private bool mShowObjectMenu = true;

    private bool mCheckRelative = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets GridView control of UniGrid.
    /// </summary>
    public override GridView GridView
    {
        get
        {
            return UniGridView;
        }
    }


    /// <summary>
    /// Hidden field containing selected items.
    /// </summary>
    public override HiddenField SelectionHiddenField
    {
        get
        {
            return hidSelection;
        }
    }


    /// <summary>
    /// Gets or sets UniGrid pager control of UniGrid.
    /// </summary>
    public override UniGridPager Pager
    {
        get
        {
            return pagerElem;
        }
    }


    /// <summary>
    /// If true, relative ancestor div is checked in context menu
    /// </summary>
    public bool CheckRelative
    {
        get
        {
            return mCheckRelative;
        }
        set
        {
            mCheckRelative = value;
        }
    }


    /// <summary>
    /// Gets selected items from UniGrid.
    /// </summary>
    public override ArrayList SelectedItems
    {
        get
        {
            return GetSelectedItems();
        }
        set
        {
            SetSectedItems(value);
        }
    }


    /// <summary>
    /// Gets selected items from UniGrid.
    /// </summary>
    public override ArrayList DeselectedItems
    {
        get
        {
            return GetDeselectedItems();
        }
    }


    /// <summary>
    /// Gets selected items from UniGrid.
    /// </summary>
    public override ArrayList NewlySelectedItems
    {
        get
        {
            return GetNewlySelectedItems();
        }
    }


    /// <summary>
    /// Gets filter placeHolder from Unigrid.
    /// </summary>
    public override PlaceHolder FilterPlaceHolder
    {
        get
        {
            return filter;
        }
    }


    /// <summary>
    /// Gets page size dropdown from Unigrid Pager.
    /// </summary>
    public override DropDownList PageSizeDropdown
    {
        get
        {
            return Pager.PageSizeDropdown;
        }
    }


    /// <summary>
    /// Defines whether to show object menu (menu containing relationships, export/backup, destroy object, ... functionality) 
    /// </summary>
    public bool ShowObjectMenu
    {
        get
        {
            return mShowObjectMenu;
        }
        set
        {
            mShowObjectMenu = value;
        }
    }


    /// <summary>
    /// Gets filter form.
    /// </summary>
    public override BasicForm FilterForm
    {
        get
        {
            return filterForm;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Control's init event handler.
    /// </summary>
    protected void Page_Init(object sender, EventArgs e)
    {
        advancedExportElem.UniGrid = this;
    }


    /// <summary>
    /// Control's load event handler.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Do not load on callback
        if (RequestHelper.IsCallback())
        {
            StopProcessing = true;
            return;
        }

        if (StopProcessing)
        {
            Visible = false;
            FilterForm.StopProcessing = true;
        }
        else
        {
            SetPager();

            if (LoadGridDefinition())
            {
                ActionsHidden = hidActions;
                ActionsHashHidden = hidActionsHash;

                // Check whether current request is row action command and if so, raise action
                if (!String.IsNullOrEmpty(hidCmdName.Value) && (Request.Form["__EVENTTARGET"] == UniqueID) && ((Request.Form["__EVENTARGUMENT"] == "UniGridAction")))
                {
                    HandleAction(hidCmdName.Value, hidCmdArg.Value);
                }

                // Set order by clause
                ProcessSorting();

                // Set filter form
                if (!string.IsNullOrEmpty(FilterFormName))
                {
                    SetBasicFormFilter();
                    if (!EventRequest() && !DelayedReload)
                    {
                        ReloadData();
                    }
                }
                // Get data from database and set them to the grid view
                else if (FilterByQueryString)
                {
                    if (displayFilter)
                    {
                        SetFilter(true);
                    }
                    else
                    {
                        if (!EventRequest() && !DelayedReload)
                        {
                            ReloadData();
                        }
                    }
                }
                else
                {
                    if (!EventRequest() && !DelayedReload)
                    {
                        ReloadData();
                    }
                }
            }
        }

        // Clear hidden action on load event. If unigrid is invisible, page pre render is not fired
        ClearActions();
    }


    /// <summary>
    /// Control's PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (FilterIsSet)
        {
            // Check for FilteredZeroRowsText
            if ((GridView.Rows.Count == 0) && !String.IsNullOrEmpty(FilteredZeroRowsText))
            {
                // Display filter zero rows text
                lblInfo.Text = FilteredZeroRowsText;
                lblInfo.Visible = true;
                pagerElem.Visible = false;
            }
            else
            {
                lblInfo.Visible = false;
                pagerElem.Visible = true;
            }
        }
        else
        {
            // Check for ZeroRowsText
            if (GridView.Rows.Count == 0)
            {
                if (!HideControlForZeroRows && !String.IsNullOrEmpty(ZeroRowsText))
                {
                    // Display zero rows text
                    lblInfo.Text = ZeroRowsText;
                    lblInfo.Visible = true;
                    pagerElem.Visible = false;
                    // Check additional filter visibility
                    CheckFilterVisibility();
                }
                else
                {
                    lblInfo.Visible = false;
                    pagerElem.Visible = false;
                    filter.Visible = false;
                }
            }
            else
            {
                lblInfo.Visible = false;
                pagerElem.Visible = true;
                // Check additional filter visibility
                CheckFilterVisibility();
            }
        }

        if (Visible && !StopProcessing)
        {
            RegisterCmdScripts();
        }

        if (Pager.CurrentPage > halfPageCountLimit)
        {
            // Enlarge direct page textbox
            TextBox txtPage = ControlsHelper.GetChildControl(pagerElem, typeof(TextBox), "txtPage") as TextBox;
            if (txtPage != null)
            {
                txtPage.Style.Add(HtmlTextWriterStyle.Width, "50px");
            }
        }

        advancedExportElem.Visible = ShowActionsMenu;

        // Hide info label when error message is displayed
        lblInfo.Visible = lblInfo.Visible && !lblError.Visible;
    }

    #endregion


    #region "Public methods"


    /// <summary>
    /// Clears UniGrid's information on recently performed action. Under normal circumstances there is no need to perform this action.
    /// However sometimes forcing grid to clear the actions is required.
    /// </summary>
    public void ClearActions()
    {
        // Clear hiddden fields
        hidCmdName.Value = null;
        hidCmdArg.Value = null;
    }


    /// <summary>
    /// Clears all selected items from hidden values.
    /// </summary>
    public void ClearSelectedItems()
    {
        ClearHiddenValues(SelectionHiddenField);
    }


    /// <summary>
    /// Loads the XML configuration of the grid.
    /// </summary>
    public bool LoadXmlConfiguration()
    {
        // If no configuration is given, do not process
        if (string.IsNullOrEmpty(GridName))
        {
            return true;
        }
        string xmlFilePath = Server.MapPath(GridName);

        // Check the configuration file
        if (!File.Exists(xmlFilePath))
        {
            lblError.Text = String.Format(GetString("unigrid.noxmlfile"), xmlFilePath);
            lblError.Visible = true;
            return false;
        }

        // Load the XML configuration
        XmlDocument document = new XmlDocument();
        document.Load(xmlFilePath);
        XmlNode node = document.DocumentElement;

        if (node != null)
        {
            // Load options definition
            XmlNode optionNode = node.SelectSingleNode("options");
            if (optionNode != null)
            {
                GridOptions = new UniGridOptions(optionNode);
            }

            // Load actions definition
            XmlNode actionsNode = node.SelectSingleNode("actions");
            if (actionsNode != null)
            {
                GridActions = new UniGridActions(actionsNode);
            }

            // Load pager definition
            XmlNode pagerNode = node.SelectSingleNode("pager");
            if (pagerNode != null)
            {
                PagerConfig = new UniGridPagerConfig(pagerNode);
            }

            // Select list of "column" nodes
            XmlNode columnsNode = node.SelectSingleNode("columns");
            if (columnsNode != null)
            {
                GridColumns = new UniGridColumns(columnsNode);
            }

            // Try to get ObjectType from definition
            XmlNode objectTypeNode = node.SelectSingleNode("objecttype");
            if (objectTypeNode != null)
            {
                // Get object type information
                LoadObjectTypeDefinition(objectTypeNode);
            }
            else
            {
                // Get query information
                XmlNode queryNode = node.SelectSingleNode("query");
                LoadQueryDefinition(queryNode);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Loads the grid definition.
    /// </summary>
    public override bool LoadGridDefinition()
    {
        if (GridView.Columns.Count == 0)
        {
            using (Table filterTable = new Table())
            {
                filter.Controls.Clear();
                // Clear all columns from the grid view
                UniGridView.Columns.Clear();
                UniGridView.GridLines = GridLines.Horizontal;
                if (!LoadXmlConfiguration())
                {
                    return false;
                }
                // Load options
                if (GridOptions != null)
                {
                    LoadOptionsDefinition(GridOptions, filterTable);
                }
                if (GridActions == null && ShowActionsMenu)
                {
                    EmptyAction emptyAction = new EmptyAction();
                    GridActions = new UniGridActions();
                    GridActions.Actions.Add(emptyAction);
                }
                // Actions
                if (GridActions != null)
                {
                    LoadActionsDefinition(GridActions);
                }
                // Load pager configuration
                if (PagerConfig != null)
                {
                    LoadPagerDefinition(PagerConfig);
                }
                // Set direct page control id from viewstate
                if (ViewState["DirectPageControlID"] != null)
                {
                    Pager.DirectPageControlID = ViewState["DirectPageControlID"].ToString();
                }
                // Raise load columns event
                RaiseLoadColumns();
                // Load columns
                if (GridColumns != null)
                {
                    foreach (Column col in GridColumns.Columns)
                    {
                        // Load column definition
                        LoadColumnDefinition(col, filterTable);
                    }
                }
                if (displayFilter)
                {
                    // Finish filter form with "Show" button
                    CreateFilterButton(filterTable);
                }
            }
        }
        return true;
    }


    /// <summary>
    /// Reloads the grid data.
    /// </summary>
    public override void ReloadData()
    {
        try
        {
            // Ensure grid definition before realod data
            LoadGridDefinition();

            RaiseOnBeforeDataReload();
            rowIndex = 1;

            // Get Current TOP N
            if (CurrentPageSize > 0)
            {
                int currentPageIndex = Pager.CurrentPage;
                int pageSize = (CurrentPageSize > 0) ? CurrentPageSize : UniGridView.PageSize;

                CurrentTopN = pageSize * (currentPageIndex + Pager.CurrentPagesGroupSize);
            }

            if (CurrentTopN < TopN)
            {
                CurrentTopN = TopN;
            }

            // If first/last button and direct page contol in pager is hidden use current topN for better performance
            if (!Pager.ShowDirectPageControl && !Pager.ShowFirstLastButtons)
            {
                TopN = CurrentTopN;
            }

            // Retrieve data
            UniGridView.DataSource = RetrieveData();

            // Sort external dataset only if no query and info object is set
            if (string.IsNullOrEmpty(Query) && (InfoObject == null) && (!DataHelper.DataSourceIsEmpty(UniGridView.DataSource) && !DataSourceIsSorted))
            {
                SortUniGridDataSource();
            }

            RaiseOnAfterDataReload();

            SetUnigridControls();

            // Check if datasource is loaded
            if (DataHelper.DataSourceIsEmpty(GridView.DataSource) && (pagerElem.CurrentPage > 1))
            {
                pagerElem.UniPager.CurrentPage = 1;
                ReloadData();
            }

            // Resolve the edit action URL
            if (!String.IsNullOrEmpty(EditActionUrl))
            {
                EditActionUrl = CMSContext.ResolveMacros(EditActionUrl);
            }

            SortColumns.Clear();
            UniGridView.DataBind();

            mRowsCount = DataHelper.GetItemsCount(UniGridView.DataSource);

            CheckFilterVisibility();
        }
        catch (Exception ex)
        {
            lblError.Text = GetString("unigrid.error.reload");
            lblError.Visible = true;

            // Display tooltip only development mode is enabled
            if (SettingsKeyProvider.DevelopmentMode)
            {
                lblError.ToolTip = ex.Message;
            }

            // Log exception
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent("UniGrid", "RELOADDATA", ex.InnerException ?? ex);
        }
    }


    /// <summary>
    /// Gets a dataset with data based on UniGrid's settings.
    /// </summary>
    /// <returns>DataSet with data</returns>
    public override DataSet RetrieveData()
    {
        DataSet ds = null;

        // If datasource for unigrid is query (not dataset), then execute query
        if (!string.IsNullOrEmpty(Query))
        {
            // Reload the data with current parameters
            ds = ConnectionHelper.ExecuteQuery(Query, QueryParameters, CompleteWhereCondition, CurrentOrder, TopN, Columns, CurrentOffset, CurrentPageSize, ref pagerForceNumberOfResults);
        }
        // If UniGrid is in ObjectType mode, get the data according to given object type.
        else if (InfoObject != null)
        {
            // Get the result set
            ds = InfoObject.GetData(QueryParameters, CompleteWhereCondition, CurrentOrder, TopN, Columns, false, CurrentOffset, CurrentPageSize, ref pagerForceNumberOfResults);
        }
        // External dataset is used
        else
        {
            ds = RaiseDataReload();
        }
        // Add empty dataset
        if (ds == null)
        {
            ds = new DataSet();
            ds.Tables.Add();
        }

        // Raise event 'OnRetrieveData'
        ds = RaiseAfterRetrieveData(ds);

        return ds;
    }


    /// <summary>
    /// Returns where condition from unigrid filters.
    /// </summary>
    public string GetFilter()
    {
        return GetFilter(false);
    }


    /// <summary>
    /// Returns where condition from unigrid filters for DataTable.Select.
    /// </summary>
    public string GetDataTableFilter()
    {
        return GetFilter(true);
    }


    /// <summary>
    /// Returns where condition from unigrid filters.
    /// </summary>
    public string GetFilter(bool isDataTable)
    {
        string where = string.Empty;
        // Count of the conditions in the 'where clause'
        int whereConditionCount = 0;

        // Process all filter fields
        foreach (object[] filterField in mFilterFields)
        {
            string filterFormat = (string)filterField[1];
            // AND in 'where clause'  
            string andExpression;
            Control mainControl = (Control)filterField[2];
            Control valueControl = (Control)filterField[3];
            string filterPath = (string)filterField[4];

            if (String.IsNullOrEmpty(filterPath))
            {
                if (mainControl is DropDownList)
                {
                    // Dropdown list filter
                    DropDownList ddlistControl = (DropDownList)mainControl;
                    TextBox txtControl = (TextBox)valueControl;

                    string textboxValue = txtControl.Text;
                    string textboxID = txtControl.ID;

                    // Empty field -> no filter is set for this field
                    if (textboxValue != "")
                    {
                        string op = ddlistControl.SelectedValue;
                        string value = textboxValue.Replace("\'", "''");
                        string columnName = ddlistControl.ID.Trim().TrimStart('[').TrimEnd(']').Trim();

                        // Format {0} = column name, {1} = operator, {2} = value, {3} = default condition
                        string defaultFormat = null;

                        if (textboxID.EndsWith("TextValue"))
                        {
                            switch (op.ToLower())
                            {
                                // LIKE operators
                                case "like":
                                case "not like":
                                    defaultFormat = isDataTable ? "[{0}] {1} '%{2}%'" : "[{0}] {1} N'%{2}%'";
                                    break;

                                // Standard operators
                                default:
                                    defaultFormat = isDataTable ? "[{0}] {1} '{2}'" : "[{0}] {1} N'{2}'";
                                    break;
                            }
                        }
                        else // textboxID.EndsWith("NumberValue")
                        {
                            if (ValidationHelper.IsDouble(value) || ValidationHelper.IsInteger(value))
                            {
                                defaultFormat = "[{0}] {1} {2}";
                            }
                        }

                        if (!String.IsNullOrEmpty(defaultFormat))
                        {
                            string defaultCondition = String.Format(defaultFormat, columnName, op, value);

                            string condition = defaultCondition;
                            if (filterFormat != null)
                            {
                                condition = String.Format(filterFormat, columnName, op, value, defaultCondition);
                            }

                            andExpression = (whereConditionCount > 0 ? " AND " : string.Empty);

                            // ddlistControl.ID                 - column name
                            // ddlistControl.SelectedValue      - condition option
                            // textboxSqlValue                  - condition value                        
                            where += String.Format("{0}({1})", andExpression, condition);
                            whereConditionCount++;
                        }
                    }

                    // Prepare query string
                    if (FilterByQueryString)
                    {
                        queryStringHashTable[ddlistControl.ID] = String.Format("{0};{1}", ddlistControl.SelectedValue, textboxValue);

                    }
                }
                else if (valueControl is DropDownList)
                {
                    // Checkbox filter
                    DropDownList currentControl = (DropDownList)valueControl;
                    string value = currentControl.SelectedValue;
                    if (value != "")
                    {
                        andExpression = (whereConditionCount > 0) ? " AND" : "";
                        where += String.Format("{0} {1} = {2}", andExpression, currentControl.ID, value);
                        whereConditionCount++;
                    }

                    // Prepare query string
                    if (FilterByQueryString)
                    {
                        queryStringHashTable[currentControl.ID] = ";" + value;
                    }
                }
            }
            // If is defined filter path
            else
            {
                CMSAbstractBaseFilterControl customFilter = (CMSAbstractBaseFilterControl)filterField[3];
                string customWhere = customFilter.WhereCondition;
                if (!String.IsNullOrEmpty(customWhere))
                {
                    andExpression = (whereConditionCount > 0) ? " AND " : "";
                    where += andExpression + customWhere;
                    whereConditionCount++;
                }

                // Prepare query string
                if (FilterByQueryString && RequestHelper.IsPostBack())
                {
                    queryStringHashTable[customFilter.ID] = customFilter.Value;
                }
            }
        }

        return where;
    }


    /// <summary>
    /// Uncheck all checkboxes in selection column.
    /// </summary>
    public override void ResetSelection()
    {
        ResetSelection(true);
    }


    /// <summary>
    /// Uncheck all checkboxes in selection column.
    /// </summary>
    /// <param name="reset">Indicates if reset selection javascript should be registered</param>
    public void ResetSelection(bool reset)
    {
        SelectionHiddenField.Value = String.Empty;
        hidNewSelection.Value = String.Empty;
        hidDeSelection.Value = String.Empty;
        resetSelection = reset;
    }

    #endregion


    #region "UniGrid events"

    /// <summary>
    /// Process data from filter.
    /// </summary>
    protected void ShowButton_Click(object sender, EventArgs e)
    {
        RaiseShowButtonClick(sender, e);

        string where = GetFilter();
        where = RaiseBeforeFiltering(where);
        Pager.UniPager.CurrentPage = 1;

        SetFilter(!DelayedReload, where);
    }


    protected void pageSizeDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        RaisePageSizeChanged();
    }


    protected void UniGridView_Sorting(object sender, EventArgs e)
    {
        RaiseBeforeSorting(sender, e);
    }


    /// <summary>
    /// After data bound event.
    /// </summary>
    protected void UniGridView_DataBound(object sender, EventArgs e)
    {
        // Set actions hash into hidden field
        SetActionsHash();

        SetPager();

        // Call page binding event
        if (OnPageBinding != null)
        {
            OnPageBinding(this, null);
        }
    }


    protected void UniGridView_RowCreating(object sender, GridViewRowEventArgs e)
    {
        // If row type is header
        if (e.Row.RowType == DataControlRowType.Header)
        {
            // Add sorting definition to list of sort columns
            SortColumns.Add(SortDirect.ToLower());

            // Parse the sort expression
            string sort = SortDirect.ToLower().Replace("[", "").Replace("]", "").Trim();
            if (sort.StartsWith("cast("))
            {
                sort = sort.Substring(5);
            }

            Match sortMatch = OrderByRegex.Match(sort);
            string sortColumn = null;
            string sortDirection = null;
            if (sortMatch.Success)
            {
                // Get column name
                if (sortMatch.Groups[1].Success)
                {
                    sortColumn = sortMatch.Groups[1].Value;
                }
                // Get sort direction
                sortDirection = sortMatch.Groups[2].Success ? sortMatch.Groups[2].Value : "asc";
            }
            else
            {
                // Get column name from sort expression
                int space = sort.IndexOfAny(new char[] { ' ', ',' });
                sortColumn = space > -1 ? sort.Substring(0, space) : sort;
                sortDirection = "asc";
            }

            // Check if displaying arrow indicating sorting is requested
            if (showSortDirection)
            {
                // Prepare the columns
                foreach (TableCell Cell in e.Row.Cells)
                {
                    // If there is some sorting expression
                    DataControlFieldCell dataField = (DataControlFieldCell)Cell;
                    string fieldSortExpression = dataField.ContainingField.SortExpression;
                    if (!DataHelper.IsEmpty(fieldSortExpression))
                    {
                        SortColumns.Add(fieldSortExpression.ToLower());

                        // If actual sorting expressions is this cell
                        if (String.Equals(sortColumn, fieldSortExpression.Replace("[", "").Replace("]", "").Trim(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            // Initialize sort arrow
                            Literal sortArrow = new Literal()
                            {
                                Text = String.Format("<span class=\"UniGridSort{0}\" >&nbsp;&nbsp;&nbsp;</span>", ((sortDirection == "desc") ? "Down" : "Up"))
                            };

                            if (DataHelper.IsEmpty(Cell.Text))
                            {
                                if ((Cell.Controls.Count != 0) && (Cell.Controls[0] != null))
                                {
                                    // Add original text
                                    Cell.Controls[0].Controls.Add(new LiteralControl(String.Format("<span class=\"UniGridSortLabel\">{0}</span>", ((LinkButton)(Cell.Controls[0])).Text)));
                                    // Add one space before image
                                    Cell.Controls[0].Controls.Add(new LiteralControl("&nbsp;"));
                                    Cell.Controls[0].Controls.Add(sortArrow);
                                }
                                else
                                {
                                    // Add one space before image
                                    Cell.Controls.Add(new LiteralControl("&nbsp;"));
                                    Cell.Controls.Add(sortArrow);
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = (rowIndex % 2 == 0) ? "OddRow" : "EvenRow";
            rowIndex++;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.CssClass = "UniGridFooter";
        }
        else if (e.Row.RowType == DataControlRowType.Pager)
        {
            e.Row.CssClass = "UniGridPager";
        }
    }


    /// <summary>
    /// Handles the action event.
    /// </summary>
    /// <param name="cmdName">Command name</param>
    /// <param name="cmdValue">Command value</param>
    public void HandleAction(string cmdName, string cmdValue)
    {
        string action = cmdName.ToLower();

        // Check action security and redirect if user not authorized
        CheckActionAndRedirect(action);
        GeneralizedInfo infoObj = null;
        int objectId = 0;
        switch (action)
        {
            case "#delete":
            case "#destroyobject":
                {
                    // Delete the object
                    objectId = ValidationHelper.GetInteger(cmdValue, 0);
                    if (objectId > 0)
                    {
                        infoObj = CMSObjectHelper.GetReadOnlyObject(ObjectType);
                        infoObj = BaseAbstractInfoProvider.GetInfoById(infoObj.TypeInfo.OriginalObjectType, objectId);
                        if (infoObj != null)
                        {
                            switch (action)
                            {
                                case "#delete":
                                    // Check the dependencies
                                    AbstractProvider providerObj = infoObj.TypeInfo.ProviderObject;
                                    if (providerObj.CheckObjectDependencies(objectId))
                                    {
                                        lblError.Visible = true;
                                        lblError.Text = GetString("ecommerce.deletedisabledwithoutenable");
                                        return;
                                    }

                                    // Delete the object
                                    infoObj.DeleteObject();
                                    break;

                                case "#destroyobject":
                                    if (CMSContext.CurrentUser.IsAuthorizedPerObject(PermissionsEnum.Destroy, infoObj.ObjectType, CMSContext.CurrentSiteName))
                                    {
                                        using (CMSActionContext context = new CMSActionContext())
                                        {
                                            context.CreateVersion = false;

                                            Action ac = GridActions.GetAction("#delete");
                                            if (ac != null)
                                            {
                                                HandleAction("#delete", cmdValue);
                                            }
                                            else
                                            {
                                                ac = GridActions.GetAction("delete");
                                                if (ac != null)
                                                {
                                                    RaiseAction("delete", cmdValue);
                                                }
                                                else
                                                {
                                                    lblError.Visible = true;
                                                    lblError.Text = GetString("objectversioning.destroyobject.nodeleteaction");
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
                break;


            default:
                RaiseAction(cmdName, cmdValue);
                break;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Sets unigrid controls.
    /// </summary>
    private void SetUnigridControls()
    {
        filter.Visible = displayFilter;

        // Indicates whether unigrid datasource is empty or not
        isEmpty = DataHelper.DataSourceIsEmpty(UniGridView.DataSource);

        if (isEmpty)
        {
            // Try to reload data for previous page if action was used and no data loaded (mostly delete)
            if (onActionUsed && Pager.CurrentPage > 1)
            {
                Pager.UniPager.CurrentPage = Pager.CurrentPage - 1;
                ReloadData();
            }
            else if (HideControlForZeroRows && (WhereClause == ""))
            {
                // Hide filter
                filter.Visible = false;
            }
        }
        else
        {
            // Disable GridView paging because UniGridPager will provide paging
            UniGridView.AllowPaging = false;

            // Process paging if pager is displayed
            if (Pager.DisplayPager)
            {
                // Get items count
                int itemsCount = DataHelper.GetItemsCount(UniGridView.DataSource);
                int pageSize = Pager.CurrentPageSize;

                if ((pageSize > 0) && (itemsCount > pageSize))
                {
                    // Get data table from datasource
                    DataTable dt = DataHelper.GetDataTable(UniGridView.DataSource);
                    if (dt != null)
                    {
                        DataTable newTable = dt.Clone();
                        int lastOffset = Math.Min(CurrentOffset + CurrentPageSize, dt.DefaultView.Count);
                        // Get only rows for current page
                        for (int i = CurrentOffset; i < lastOffset; i++)
                        {
                            DataRow dataRow = newTable.NewRow();

                            dataRow.ItemArray = dt.DefaultView[i].Row.ItemArray;
                            newTable.Rows.Add(dataRow);
                        }

                        UniGridView.DataSource = newTable;
                        PagerForceNumberOfResults = itemsCount;
                    }
                }

                if (dataSourceIsUsed && (itemsCount > pageSize))
                {
                    PagerForceNumberOfResults = itemsCount;
                }
                if (CurrentPageSize > 0)
                {
                    if (!visiblePagesSet)
                    {
                        Pager.VisiblePages = (((CurrentOffset / CurrentPageSize) + 1)) > halfPageCountLimit ? 5 : 10;
                    }

                    Pager.DirectPageControlID = ((float)PagerForceNumberOfResults / pageSize > 20.0f) ? "txtPage" : "drpPage";
                    // Save direct page control id in viewstate
                    ViewState["DirectPageControlID"] = Pager.DirectPageControlID;
                }
            }
        }
    }


    /// <summary>
    /// Load options definition.
    /// </summary>
    /// <param name="options">Options configuration</param>
    /// <param name="filterTable">Table for filter</param>
    private void LoadOptionsDefinition(UniGridOptions options, Table filterTable)
    {
        // Create filter table according to the key value "DisplayFilter"            
        displayFilter = options.DisplayFilter;
        if (displayFilter)
        {
            filter.Controls.Add(filterTable);
        }

        // Filter limit
        if (options.FilterLimit > -1)
        {
            FilterLimit = options.FilterLimit;
        }

        // Display sort direction images
        showSortDirection = options.ShowSortDirection;

        // Display selection column with checkboxes
        showSelection = options.ShowSelection;
        if (showSelection)
        {
            TemplateField chkColumn = new TemplateField();

            using (CheckBox headerBox = new CheckBox { ID = "headerBox" })
            {
                using (CheckBox itemBox = new CheckBox { ID = "itemBox" })
                {
                    // Set selection argument
                    itemBox.Attributes["selectioncolumn"] = options.SelectionColumn;
                    chkColumn.HeaderTemplate = new GridViewTemplate(ListItemType.Header, this, headerBox);
                    chkColumn.ItemTemplate = new GridViewTemplate(ListItemType.Item, this, itemBox);
                }
            }
            UniGridView.Columns.Add(chkColumn);
        }

        // Get pagesize options
        if (!String.IsNullOrEmpty(options.PageSize))
        {
            Pager.PageSizeOptions = options.PageSize;
        }

        // Set pagging acording to the key value "DisplayPageSizeDropdown"                
        if (options.DisplayPageSizeDropdown != null)
        {
            Pager.ShowPageSize = options.DisplayPageSizeDropdown.Value;
        }
    }


    /// <summary>
    /// Loads actions definition.
    /// </summary>
    /// <param name="actions">Configuration of the actions</param>
    private void LoadActionsDefinition(UniGridActions actions)
    {
        // Custom template field of the grid view
        TemplateField actionsColumn = new TemplateField();

        // Ensure width of the column
        if (!String.IsNullOrEmpty(actions.Width))
        {
            actionsColumn.ItemStyle.Width = new Unit(actions.Width);
        }

        // Add object menu if possible
        if ((actions.Actions.Count > 0 && !(actions.Actions.FirstOrDefault() is EmptyAction)) && ShowObjectMenu && UniGridFunctions.ShowUniGridObjectContextMenu(CMSObjectHelper.GetReadOnlyObject(ObjectType)))
        {
            actions.Actions.RemoveAll(a => a is EmptyAction);
            // Check if object menu already contained
            var menus = from action in actions.Actions.OfType<Action>()
                        where (action.Name.ToLower() == "#objectmenu") || (!String.IsNullOrEmpty(action.ContextMenu))
                        select action;

            // Add object menu of necessary
            if ((menus.Count() == 0) && !IsLiveSite)
            {
                Action action = new Action("#objectmenu");
                action.ExternalSourceName = "#objectmenu";
                actions.Actions.Add(action);
            }
        }

        // Show header?
        if (actions.ShowHeader)
        {
            if (ShowActionsMenu && string.IsNullOrEmpty(actions.ContextMenu))
            {
                actions.ContextMenu = DEFAULT_ACTIONS_MENU;
                actions.Caption = "General.OtherActions";
            }
            // Fill in the custom template field
            GridViewTemplate headerTemplate = new GridViewTemplate(ListItemType.Header, this, actions, GetString("unigrid.actions"), ImageDirectoryPath, DefaultImageDirectoryPath, Page);
            headerTemplate.ContextMenuParent = plcContextMenu;
            headerTemplate.CheckRelative = CheckRelative;
            actionsColumn.HeaderTemplate = headerTemplate;
            if (ShowActionsMenu)
            {
                if (actions.Actions.FirstOrDefault() is EmptyAction)
                {
                    actionsColumn.HeaderStyle.CssClass = "EmptyAC";
                }
                else
                {
                    actionsColumn.HeaderStyle.CssClass = "AC";
                }
            }
        }
        GridViewTemplate actionsTemplate = new GridViewTemplate(ListItemType.Item, this, actions, null, ImageDirectoryPath, DefaultImageDirectoryPath, Page);
        actionsTemplate.OnExternalDataBound += RaiseExternalDataBound;
        actionsTemplate.ContextMenuParent = plcContextMenu;
        actionsTemplate.CheckRelative = CheckRelative;
        actionsColumn.ItemTemplate = actionsTemplate;

        if (!IsLiveSite)
        {
            actionsColumn.ItemStyle.CssClass = "NW UniGridActions";
        }
        else
        {
            actionsColumn.ItemStyle.CssClass = "UniGridActions";
            actionsColumn.ItemStyle.Wrap = false;
        }

        // CSS class name
        string cssClass = actions.CssClass;
        if (cssClass != null)
        {
            actionsColumn.HeaderStyle.CssClass += " " + cssClass;
            actionsColumn.ItemStyle.CssClass += " " + cssClass;
            actionsColumn.FooterStyle.CssClass += " " + cssClass;
        }

        // Add custom column to grid view
        UniGridView.Columns.Add(actionsColumn);
    }


    /// <summary>
    /// Load unigrid pager configuration.
    /// </summary>
    /// <param name="config">Pager configuration</param>
    private void LoadPagerDefinition(UniGridPagerConfig config)
    {
        if (config.DisplayPager != null)
        {
            Pager.DisplayPager = config.DisplayPager.Value;
        }

        // Load definition only if pager is displayed
        if (Pager.DisplayPager)
        {
            if (config.PageSizeOptions != null)
            {
                Pager.PageSizeOptions = config.PageSizeOptions;
            }
            if (config.ShowDirectPageControl != null)
            {
                Pager.ShowDirectPageControl = config.ShowDirectPageControl.Value;
            }
            if (config.ShowFirstLastButtons != null)
            {
                Pager.ShowFirstLastButtons = config.ShowFirstLastButtons.Value;
            }
            if (config.ShowPageSize != null)
            {
                Pager.ShowPageSize = config.ShowPageSize.Value;
            }
            if (config.ShowPreviousNextButtons != null)
            {
                Pager.ShowPreviousNextButtons = config.ShowPreviousNextButtons.Value;
            }
            if (config.ShowPreviousNextPageGroup != null)
            {
                Pager.ShowPreviousNextPageGroup = config.ShowPreviousNextPageGroup.Value;
            }
            if (config.VisiblePages > 0)
            {
                Pager.VisiblePages = config.VisiblePages;
                visiblePagesSet = true;
            }
            if (config.DefaultPageSize > 0)
            {
                Pager.DefaultPageSize = config.DefaultPageSize;
            }

            // Try to get page size from request
            string selectedPageSize = Request.Form[Pager.PageSizeDropdown.UniqueID];
            int pageSize = 0;

            if (selectedPageSize != null)
            {
                pageSize = ValidationHelper.GetInteger(selectedPageSize, 0);
            }
            else if (config.DefaultPageSize > 0)
            {
                pageSize = config.DefaultPageSize;
            }

            if ((pageSize > 0) || (pageSize == -1))
            {
                Pager.CurrentPageSize = pageSize;
            }
        }
        else
        {
            // Reset page size
            Pager.CurrentPageSize = -1;
        }
    }


    /// <summary>
    /// Load single column definition.
    /// </summary>
    /// <param name="column">Column to use</param>
    /// <param name="filterTable">Table for filter</param>
    private void LoadColumnDefinition(Column column, Table filterTable)
    {
        DataControlField field = null;
        string cssClass = column.CssClass;
        string columnCaption = null;

        // Process the column type Hyperlink or BoundColumn based on the parameters
        if ((column.HRef != null) ||
            (column.ExternalSourceName != null) ||
            (column.Localize) ||
            (column.Icon != null) ||
            (column.Tooltip != null) ||
            (column.Action != null) ||
            (column.Style != null) ||
            (column.MaxLength > 0))
        {
            ExtendedBoundField linkColumn = new ExtendedBoundField();
            field = linkColumn;

            // Attribute "source"
            if (column.Source != null)
            {
                linkColumn.DataField = column.Source;
                if (column.AllowSorting)
                {
                    if (!String.IsNullOrEmpty(column.Sort))
                    {
                        linkColumn.SortExpression = column.Sort;
                    }
                    else if (column.Source.ToLower() != ExtendedBoundField.ALL_DATA.ToLower())
                    {
                        linkColumn.SortExpression = column.Source;
                    }
                }
            }

            // Action parameters
            if (column.Action != null)
            {
                linkColumn.Action = column.Action;

                // Action parameters
                if (column.CommandArgument != null)
                {
                    linkColumn.CommandArgument = column.CommandArgument;
                }
            }

            // Action parameters
            if (column.Parameters != null)
            {
                linkColumn.ActionParameters = column.Parameters;
            }

            // Navigate URL
            if (column.HRef != null)
            {
                linkColumn.NavigateUrl = column.HRef;
            }

            // External source
            if (column.ExternalSourceName != null)
            {
                linkColumn.ExternalSourceName = column.ExternalSourceName;
                linkColumn.OnExternalDataBound += RaiseExternalDataBound;
            }

            // Localize strings?
            linkColumn.LocalizeStrings = column.Localize;

            // Style
            if (column.Style != null)
            {
                linkColumn.Style = column.Style;
            }

            // Class name
            if (cssClass != null)
            {
                linkColumn.HeaderStyle.CssClass += " " + cssClass;
                linkColumn.ItemStyle.CssClass += " " + cssClass;
                linkColumn.FooterStyle.CssClass += " " + cssClass;
            }

            // Icon
            if (column.Icon != null)
            {
                if (linkColumn.DataField == "")
                {
                    linkColumn.DataField = ExtendedBoundField.ALL_DATA;
                }
                linkColumn.Icon = GetActionImage(column.Icon);
            }

            // Max length
            if (column.MaxLength > 0)
            {
                linkColumn.MaxLength = column.MaxLength;
            }

            // Process "tooltip" node
            ColumnTooltip tooltip = column.Tooltip;
            if (tooltip != null)
            {
                // If there is some tooltip register TooltipScript
                if ((tooltip.Source != null) || (tooltip.ExternalSourceName != null))
                {
                    ScriptHelper.RegisterTooltip(Page);
                }

                // Tooltip source
                if (tooltip.Source != null)
                {
                    linkColumn.TooltipSourceName = tooltip.Source;
                }

                // Tooltip external source
                if (tooltip.ExternalSourceName != null)
                {
                    linkColumn.TooltipExternalSourceName = tooltip.ExternalSourceName;
                }

                // Tooltip width
                if (tooltip.Width != null)
                {
                    linkColumn.TooltipWidth = tooltip.Width;
                }

                // Encode tooltip
                linkColumn.TooltipEncode = tooltip.Encode;
            }
        }
        else
        {
            BoundField userColumn = new BoundField(); // Custom column of the grid view
            field = userColumn;

            // Attribute "source"
            if (column.Source != null)
            {
                userColumn.DataField = column.Source;

                // Allow sorting
                if (column.AllowSorting)
                {
                    if (column.Source.ToLower() != ExtendedBoundField.ALL_DATA.ToLower())
                    {
                        userColumn.SortExpression = column.Source;
                    }
                    else if (column.Sort != null)
                    {
                        userColumn.SortExpression = column.Sort;
                    }
                }
            }
        }

        if (!IsLiveSite)
        {
            field.HeaderStyle.CssClass = "NW";
        }
        else
        {
            field.HeaderStyle.Wrap = false;
        }

        // Column name
        if (column.Name != null)
        {
            NamedColumns[column.Name] = field;
        }

        // Caption
        if (column.Caption != null)
        {
            columnCaption = GetString(ResHelper.GetResourceName(column.Caption));
            field.HeaderText = columnCaption;
        }

        // Width
        if (column.Width != null)
        {
            if (GridView.ShowHeader)
            {
                field.HeaderStyle.Width = new Unit(column.Width);
            }
            else
            {
                field.ItemStyle.Width = new Unit(column.Width);
            }
        }

        // Visible
        field.Visible = column.Visible;

        // Is text?
        if (column.IsText && (column.Source != null))
        {
            TextColumns.Add(column.Source);
        }

        // Wrap?
        if (!column.Wrap)
        {
            if (!IsLiveSite)
            {
                field.ItemStyle.CssClass = "NW";
            }
            else
            {
                field.ItemStyle.Wrap = false;
            }
        }

        // Class name
        if (cssClass != null)
        {
            field.HeaderStyle.CssClass += " " + cssClass;
            field.ItemStyle.CssClass += " " + cssClass;
            field.FooterStyle.CssClass += " " + cssClass;
        }

        // Process "filter" node
        if (displayFilter)
        {
            // Filter
            ColumnFilter filter = column.Filter;
            if (filter != null)
            {
                object option = null;
                object value = null;

                // Filter via query string
                if (FilterByQueryString)
                {
                    if (String.IsNullOrEmpty(filter.Path))
                    {
                        string values = QueryHelper.GetString(column.Source, null);
                        if (!string.IsNullOrEmpty(values))
                        {
                            string[] pair = values.Split(';');
                            option = pair[0];
                            value = pair[1];
                        }
                    }
                    else
                    {
                        value = QueryHelper.GetString(column.Source, null);
                    }
                }

                string filterSource = filter.Source ?? column.Source;

                // Add the filter field
                AddFilterField(filter.Type, filter.Path, filter.Format, filterSource, columnCaption, filterTable, option, value, filter.Size);
            }
        }

        // Add custom column to gridview
        UniGridView.Columns.Add(field);
    }


    /// <summary>
    /// Load query definition from XML.
    /// </summary>
    /// <param name="objectTypeNode">XML query definition node</param>
    private void LoadObjectTypeDefinition(XmlNode objectTypeNode)
    {
        if (objectTypeNode != null)
        {
            ObjectType = objectTypeNode.Attributes["name"].Value;

            // Set the columns property if columns are defined
            LoadColumns(objectTypeNode);
        }
    }


    /// <summary>
    /// Load query definition from XML.
    /// </summary>
    /// <param name="queryNode">XML query definition node</param>
    private void LoadQueryDefinition(XmlNode queryNode)
    {
        if (queryNode != null)
        {
            Query = queryNode.Attributes["name"].Value;

            // Set the columns property if columns are defined
            LoadColumns(queryNode);
            LoadAllColumns(queryNode);

            // Load the query parameters
            XmlNodeList parameters = queryNode.SelectNodes("parameter");
            if ((parameters != null) && (parameters.Count > 0))
            {
                QueryDataParameters newParams = new QueryDataParameters();

                // Process all parameters
                foreach (XmlNode param in parameters)
                {
                    object value = null;
                    string name = param.Attributes["name"].Value;

                    switch (param.Attributes["type"].Value.ToLower())
                    {
                        case "string":
                            value = param.Attributes["value"].Value;
                            break;

                        case "int":
                            value = ValidationHelper.GetInteger(param.Attributes["value"].Value, 0);
                            break;

                        case "double":
                            value = Convert.ToDouble(param.Attributes["value"].Value);
                            break;

                        case "bool":
                            value = Convert.ToBoolean(param.Attributes["value"].Value);
                            break;
                    }

                    newParams.Add(name, value);
                }

                QueryParameters = newParams;
            }
        }
    }


    /// <summary>
    /// Sets the columns property if columns are defined.
    /// </summary>
    /// <param name="queryNode">Node from which to load columns</param>
    private void LoadAllColumns(XmlNode queryNode)
    {
        XmlAttribute allColumns = queryNode.Attributes["allcolumns"];
        if (allColumns != null)
        {
            AllColumns = DataHelper.GetNotEmpty(allColumns.Value, AllColumns);
        }
    }


    /// <summary>
    /// Sets the columns property if columns are defined.
    /// </summary>
    /// <param name="queryNode">Node from which to load columns</param>
    private void LoadColumns(XmlNode queryNode)
    {
        XmlAttribute columns = queryNode.Attributes["columns"];
        if (columns != null)
        {
            Columns = DataHelper.GetNotEmpty(columns.Value, Columns);
        }
    }


    /// <summary>
    /// Add filter field to the filter table.
    /// </summary>
    /// <param name="fieldType">Field type</param>
    /// <param name="fieldPath">Filter contol file path. If not starts with ~/ default directory '~/CMSAdminControls/UI/UniGrid/Filters/' will be inserted at begining</param>
    /// <param name="filterFormat">Filter format string</param>
    /// <param name="fieldSourceName">Source field name</param>
    /// <param name="fieldDisplayName">Field display name</param>
    /// <param name="filterTable">Filter table</param>
    /// <param name="filterOption">Filter option</param>
    /// <param name="filterValue">Filter value</param>
    /// <param name="filterSize">Filter size</param>
    private void AddFilterField(string fieldType, string fieldPath, string filterFormat, string fieldSourceName, string fieldDisplayName, Table filterTable, object filterOption, object filterValue, int filterSize)
    {
        TableRow tRow = new TableRow();

        TableCell tCellName = new TableCell();
        TableCell tCellOption = new TableCell();
        TableCell tCellValue = new TableCell();

        // Ensure fieldSourceName is Javascript valid
        fieldSourceName = fieldSourceName.Replace(ALL, "__ALL__");

        // Label
        Label textName = new Label
        {
            Text = fieldDisplayName + ":",
            ID = fieldSourceName + "Name",
            EnableViewState = false
        };

        tCellName.Controls.Add(textName);
        tRow.Cells.Add(tCellName);

        // Filter option
        string option = null;
        if (filterOption != null)
        {
            option = ValidationHelper.GetString(filterOption, null);
        }

        // Filter value
        string value = null;
        if (filterValue != null)
        {
            value = ValidationHelper.GetString(filterValue, null);
        }

        // Filter definition
        // 0 = Type
        // 1 = Format
        // 2 = Option control
        // 3 = Filter value control
        // 4 = FilterID
        object[] filterDefinition = new object[5];
        filterDefinition[0] = (fieldType != null) ? fieldType.ToLower() : null;
        filterDefinition[1] = filterFormat;

        // If no filter path us default filter
        if (String.IsNullOrEmpty(fieldPath))
        {
            switch (fieldType.ToLower())
            {
                // Text filter
                case "text":
                    {
                        DropDownList textOptionFilterField = new DropDownList();
                        textOptionFilterField.Items.Add(new ListItem("LIKE", "LIKE"));
                        textOptionFilterField.Items.Add(new ListItem("NOT LIKE", "NOT LIKE"));
                        textOptionFilterField.Items.Add(new ListItem("=", "="));
                        textOptionFilterField.Items.Add(new ListItem("<>", "<>"));
                        textOptionFilterField.CssClass = "ContentDropdown";
                        textOptionFilterField.ID = fieldSourceName;

                        // Select filter option
                        try
                        {
                            textOptionFilterField.SelectedValue = option;
                        }
                        catch { }

                        LocalizedLabel lblSelect = new LocalizedLabel
                        {
                            EnableViewState = false,
                            Display = false,
                            AssociatedControlID = textOptionFilterField.ID,
                            ResourceString = "general.select"
                        };

                        tCellOption.Controls.Add(lblSelect);
                        tCellOption.Controls.Add(textOptionFilterField);
                        tRow.Cells.Add(tCellOption);

                        // Add text field
                        TextBox textValueFilterField = new TextBox
                        {
                            ID = fieldSourceName + "TextValue",
                            Text = value
                        };
                        if (filterSize > 0)
                        {
                            textValueFilterField.MaxLength = filterSize;
                        }
                        tCellValue.Controls.Add(textValueFilterField);
                        tRow.Cells.Add(tCellValue);
                        textName.AssociatedControlID = textValueFilterField.ID;

                        filterDefinition[2] = textOptionFilterField;
                        filterDefinition[3] = textValueFilterField;
                    }
                    break;

                // Boolean filter
                case "bool":
                    {
                        DropDownList booleanOptionFilterField = new DropDownList();
                        booleanOptionFilterField.Items.Add(new ListItem(GetString("general.selectall"), ""));
                        booleanOptionFilterField.Items.Add(new ListItem(GetString("general.yes"), "1"));
                        booleanOptionFilterField.Items.Add(new ListItem(GetString("general.no"), "0"));
                        booleanOptionFilterField.CssClass = "ContentDropdown";
                        booleanOptionFilterField.ID = fieldSourceName;
                        textName.AssociatedControlID = booleanOptionFilterField.ID;

                        // Select filter option
                        try
                        {
                            booleanOptionFilterField.SelectedValue = value;
                        }
                        catch { }

                        tCellValue.Controls.Add(booleanOptionFilterField);
                        tRow.Cells.Add(tCellValue);

                        filterDefinition[3] = booleanOptionFilterField;
                    }
                    break;

                // Integer filter
                case "integer":
                case "double":
                    {
                        DropDownList numberOptionFilterField = new DropDownList();
                        numberOptionFilterField.Items.Add(new ListItem("=", "="));
                        numberOptionFilterField.Items.Add(new ListItem("<>", "<>"));
                        numberOptionFilterField.Items.Add(new ListItem("<", "<"));
                        numberOptionFilterField.Items.Add(new ListItem(">", ">"));
                        numberOptionFilterField.CssClass = "ContentDropdown";
                        numberOptionFilterField.ID = fieldSourceName;

                        // Select filter option
                        try
                        {
                            numberOptionFilterField.SelectedValue = option;
                        }
                        catch
                        {
                        }

                        LocalizedLabel lblSelect = new LocalizedLabel
                        {
                            EnableViewState = false,
                            Display = false,
                            AssociatedControlID = numberOptionFilterField.ID,
                            ResourceString = "general.select"
                        };

                        // Add filter field
                        tCellOption.Controls.Add(lblSelect);
                        tCellOption.Controls.Add(numberOptionFilterField);
                        tRow.Cells.Add(tCellOption);

                        TextBox numberValueFilterField = new TextBox
                        {
                            ID = fieldSourceName + "NumberValue",
                            Text = value
                        };

                        if (filterSize > 0)
                        {
                            numberValueFilterField.MaxLength = filterSize;
                        }
                        numberValueFilterField.EnableViewState = false;

                        tCellValue.Controls.Add(numberValueFilterField);
                        tRow.Cells.Add(tCellValue);

                        filterDefinition[2] = numberOptionFilterField;
                        filterDefinition[3] = numberValueFilterField;
                    }
                    break;
            }
        }
        // Else if filter path is defined use custom filter
        else
        {
            string path = fieldPath.StartsWith("~/") ? fieldPath : FilterDirectoryPath + fieldPath.TrimStart('/');
            CMSAbstractBaseFilterControl filterControl = LoadControl(path) as CMSAbstractBaseFilterControl;

            if (filterControl != null)
            {
                filterControl.ID = fieldSourceName;
                tCellValue.Controls.Add(filterControl);
            }
            tCellValue.Attributes["colspan"] = "2";
            tRow.Cells.Add(tCellValue);
            if (filterControl != null)
            {
                filterControl.FilteredControl = this;
                if (!RequestHelper.IsPostBack())
                {
                    filterControl.Value = value;
                }

                filterDefinition[3] = filterControl;
                filterDefinition[4] = filterControl.ID;
            }
        }

        mFilterFields.Add(filterDefinition);

        filterTable.Rows.Add(tRow);
    }


    /// <summary>
    /// Creates filter show button.
    /// </summary>
    private void CreateFilterButton(Table filterTable)
    {
        // Add button to the bottom of the filter table
        showButton = new CMSButton();
        Literal ltlBreak = new Literal();
        TableRow tRow = new TableRow();
        TableCell tCell = new TableCell();
        showButton.ID = "btnShow";
        showButton.Text = GetString("general.show");
        showButton.CssClass = "ContentButton";
        showButton.Click += ShowButton_Click;
        showButton.EnableViewState = false;
        ltlBreak.EnableViewState = false;
        ltlBreak.Text = "<br /><br />";
        tCell.Controls.Add(showButton);
        tCell.Controls.Add(ltlBreak);
        tCell.ColumnSpan = 2;
        tCell.EnableViewState = false;
        TableCell indentCell = new TableCell
        {
            EnableViewState = false,
            Text = "&nbsp;"
        };
        tRow.Cells.Add(indentCell);   // Indent 'Show' button
        tRow.Cells.Add(tCell);
        filterTable.Rows.Add(tRow);
        pnlHeader.DefaultButton = showButton.ID;
    }

    private void SetFilter(bool reloadData)
    {
        SetFilter(reloadData, null);
    }


    /// <summary>
    /// Sets filter to the grid view and save it to the view state.
    /// </summary>
    /// <param name="reloadData">Reload data</param>
    private void SetFilter(bool reloadData, string where)
    {
        // Where can be empty string - it means that filter condition was added to WhereCondition property 
        if (where == null)
        {
            where = GetFilter();
        }

        // Filter by query string
        if (FilterByQueryString && !reloadData)
        {
            string url = URLRewriter.CurrentURL;
            foreach (string name in queryStringHashTable.Keys)
            {
                if (queryStringHashTable[name] != null)
                {
                    string value = HttpContext.Current.Server.UrlEncode(queryStringHashTable[name].ToString());
                    url = URLHelper.AddParameterToUrl(url, name, value);
                }
            }
            URLHelper.Redirect(url);
        }
        else
        {
            WhereClause = where;
            FilterIsSet = true;
            if ((!DelayedReload) && (reloadData))
            {
                // Get data from database and set them to the grid view
                ReloadData();
            }
        }
    }


    /// <summary>
    /// Checks filter visibility according to filter limit and rows count.
    /// </summary>
    private void CheckFilterVisibility()
    {
        if (displayFilter)
        {
            if (FilterLimit > 0)
            {
                bool isPostBack = RequestHelper.IsPostBack();

                if (!string.IsNullOrEmpty(FilterFormName))
                {
                    filter.Visible = false;
                    if (!isPostBack || !FilterIsSet)
                    {
                        bool visibleFilter = false;

                        if (!ShowFilter)
                        {
                            // Count of items
                            int items = Pager.UniPager.DataSourceItemsCount;
                            // Current page size
                            int currentPageSize = Pager.CurrentPageSize;

                            visibleFilter = ((currentPageSize > 0) && (items > currentPageSize));
                        }

                        // If count of rows is greater than filter limit or current page size, show filter
                        plcFilterForm.Visible = ShowFilter || visibleFilter;
                    }
                }
                else if (FilterByQueryString)
                {
                    filter.Visible = (FilterIsSet && ShowFilter);
                }
                else
                {
                    if (!isPostBack || !FilterIsSet)
                    {
                        filter.Visible = ShowFilter;
                    }
                }
            }
        }
        else
        {
            // Hide filter
            filter.Visible = false;
            plcFilterForm.Visible = false;
        }
    }


    /// <summary>
    /// Sorts UniGrid data source according to sort directive saved in viewstate.
    /// </summary>
    private void SortUniGridDataSource()
    {
        if (SortDirect != "")
        {
            object ds = UniGridView.DataSource;

            // If source isn't empty
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                // Set sort directive from viewstate
                if (ds is DataTable)
                {
                    // Data table
                    try
                    {
                        ((DataTable)(ds)).DefaultView.Sort = SortDirect;
                    }
                    catch { }
                }
                else if (ds is DataSet)
                {
                    // DataSet
                    try
                    {
                        ((DataSet)(ds)).Tables[0].DefaultView.Sort = SortDirect;
                        ds = ((DataSet)(ds)).Tables[0].DefaultView;
                    }
                    catch { }
                }
                else if (ds is DataView)
                {
                    // Data view
                    try
                    {
                        ((DataView)(ds)).Sort = SortDirect;
                    }
                    catch { }
                }
            }
        }
    }


    /// <summary>
    /// Changes sorting direction by specified column.
    /// </summary>
    /// <param name="orderByColumn">Column name to order by</param>
    /// <param name="orderByString">Old order by string</param> 
    private void ChangeSortDirection(string orderByColumn, string orderByString)
    {
        orderByColumn = orderByColumn.Trim().TrimStart('[').TrimEnd(']').Trim();
        orderByString = orderByString.Trim().TrimStart('[');

        // If order by column is long text use CAST in ORDER BY part of query
        if (TextColumns.Contains(orderByColumn))
        {
            if (orderByString.EndsWith("desc"))
            {
                SortDirect = String.Format("CAST([{0}] AS nvarchar(32)) asc", orderByColumn);
            }
            else
            {
                SortDirect = String.Format("CAST([{0}] AS nvarchar(32)) desc", orderByColumn);
            }
        }
        else
        {
            string orderByDirection = "asc";
            Match orderByMatch = OrderByRegex.Match(orderByString);
            if (orderByMatch.Success)
            {
                if (orderByMatch.Groups[2].Success)
                {
                    orderByDirection = orderByMatch.Groups[2].Value;
                }
            }

            // Sort by the same column -> the other directon
            if (orderByString.StartsWith(orderByColumn))
            {
                SortDirect = (orderByDirection == "desc") ? String.Format("[{0}] asc", orderByColumn) : String.Format("[{0}] desc", orderByColumn);
            }
            // Sort by a new column -> implicitly direction is ASC
            else
            {
                SortDirect = String.Format("[{0}] asc", orderByColumn);
            }
        }
    }


    /// <summary>
    /// Returns List of selected Items.
    /// </summary>
    private ArrayList GetSelectedItems()
    {
        return GetHiddenValues(SelectionHiddenField);
    }


    /// <summary>
    /// Sets selection values for UniGrid.
    /// </summary>
    /// <param name="values">Arraylist of values to selection</param>
    private void SetSectedItems(ArrayList values)
    {
        SetHiddenValues(values, SelectionHiddenField, null);
    }


    /// <summary>
    /// Returns List of deselected Items.
    /// </summary>
    private ArrayList GetDeselectedItems()
    {
        return GetHiddenValues(hidDeSelection);
    }


    /// <summary>
    /// Returns List of newly selected Items.
    /// </summary>
    private ArrayList GetNewlySelectedItems()
    {
        return GetHiddenValues(hidNewSelection);
    }


    /// <summary>
    /// Returns array list from hidden field.
    /// </summary>
    /// <param name="field">Hidden field with values separated with |</param>
    private static ArrayList GetHiddenValues(HiddenField field)
    {
        string hiddenValue = field.Value.Trim('|');
        ArrayList list = new ArrayList();
        string[] values = hiddenValue.Split('|');
        foreach (string value in values)
        {
            if (!list.Contains(value))
            {
                list.Add(value);
            }
        }
        list.Remove("");
        return list;
    }


    /// <summary>
    /// Sets array list values into hidden field.
    /// </summary>
    /// <param name="values">Arraylist of values to selection</param>
    /// <param name="field">Hidden field</param>
    private static void SetHiddenValues(ArrayList values, HiddenField actionsField, HiddenField hashField)
    {
        if (values != null)
        {
            if (actionsField != null)
            {
                // Build the list of actions
                StringBuilder sb = new StringBuilder();
                sb.Append("|");

                foreach (object value in values)
                {
                    sb.Append(value);
                    sb.Append("|");
                }

                // Action IDs
                string actions = sb.ToString();
                actionsField.Value = actions;

                // Actions hash
                if (hashField != null)
                {
                    hashField.Value = ValidationHelper.GetHashString(actions);
                }
            }
        }
    }


    /// <summary>
    /// Clears all selected items from hidden values.
    /// </summary>
    /// <param name="field">Hidden field</param>
    private static void ClearHiddenValues(HiddenField field)
    {
        if (field != null)
        {
            field.Value = "";
        }
    }


    /// <summary>
    /// Sets hidden field with actions hashes.
    /// </summary>
    private void SetActionsHash()
    {
        if (ActionsID.Count > 0)
        {
            SetHiddenValues(ActionsID, hidActions, hidActionsHash);
        }
    }


    /// <summary>
    /// Sets pager control.
    /// </summary>
    private void SetPager()
    {
        Pager.PagedControl = this;
    }


    /// <summary>
    /// Sets the sort direction if current request is sorting.
    /// </summary>
    private void ProcessSorting()
    {
        // Get current event target
        string uniqieId = ValidationHelper.GetString(Request.Params["__EVENTTARGET"], String.Empty);
        // Get current argument
        string eventargument = ValidationHelper.GetString(Request.Params["__EVENTARGUMENT"], String.Empty);

        if ((uniqieId == GridView.UniqueID) && (eventargument.StartsWith("Sort")))
        {
            string orderByColumn = Convert.ToString(eventargument.Split('$')[1]);
            if (SortColumns.Contains(orderByColumn.ToLower()))
            {
                // If sorting is called for the first time and default sorting (OrderBy property) is set
                if ((SortDirect == "") && !string.IsNullOrEmpty(OrderBy))
                {
                    ChangeSortDirection(orderByColumn, OrderBy);
                }
                else
                {
                    ChangeSortDirection(orderByColumn, SortDirect);
                }
            }
        }
    }


    /// <summary>
    /// Returns true if current request was fired by page change or filter show button.
    /// </summary>
    private bool EventRequest()
    {
        if (URLHelper.IsPostback())
        {
            // Get current event target
            string uniqieId = ValidationHelper.GetString(Request.Params["__EVENTTARGET"], String.Empty);
            // Get current argument
            string eventargument = ValidationHelper.GetString(Request.Params["__EVENTARGUMENT"], String.Empty).ToLower();

            // Check whether current request is paging
            if (!String.IsNullOrEmpty(uniqieId) && (uniqieId == GridView.UniqueID) && eventargument.StartsWith("page"))
            {
                return true;
            }

            // Check whether show button is defined
            if (showButton != null)
            {
                // If button name is not empty => button fire postback
                if (!string.IsNullOrEmpty(Request.Params[showButton.UniqueID]))
                {
                    return true;
                }
            }

            // Check whether show button in basic form is defined
            if (FilterForm.SubmitButton != null)
            {
                // If submit button name is not empty => button fire postback
                if (!string.IsNullOrEmpty(Request.Params[FilterForm.SubmitButton.UniqueID]))
                {
                    return true;
                }
            }
        }

        // Non-paging request by default
        return false;
    }


    /// <summary>
    /// Returns icon file for current theme or from default if current doesn't exist.
    /// </summary>
    /// <param name="iconfile">Icon file name</param>
    private string GetActionImage(string iconfile)
    {
        if (File.Exists(MapPath(ImageDirectoryPath + iconfile)))
        {
            return (ImageDirectoryPath + iconfile);
        }

        // Short path to the icon
        if (ControlsExtensions.RenderShortIDs)
        {
            return UIHelper.GetShortImageUrl(UIHelper.UNIGRID_ICONS, iconfile);
        }

        return GetImageUrl("Design/Controls/UniGrid/Actions/" + iconfile);
    }


    /// <summary>
    /// Register unigrid commands scripts.
    /// </summary>
    private void RegisterCmdScripts()
    {
        StringBuilder builder = new StringBuilder();

        // Redir function
        if (EditActionUrl != null)
        {
            builder.Append("function UG_Redir(url) { document.location.replace(url); return false; }\n");
        }

        builder.Append("function UG_Reload() { ", Page.ClientScript.GetPostBackEventReference(this, "Reload"), " }\n");

        // Actions
        builder.Append(
@"
function Get(id) {
    return document.getElementById(id);
}

function ", CMD_PREFIX, ClientID, @"(name, arg) {
    var nameObj = Get('", hidCmdName.ClientID, @"');
    var argObj = Get('", hidCmdArg.ClientID, @"');
    if ((nameObj != null) && (argObj != null)) {
        nameObj.value = name;
        argObj.value = arg;
        ", Page.ClientScript.GetPostBackEventReference(this, "UniGridAction"), @"
    } 
    
    return false;
}

function ", DESTROY_OBJECT_PREFIX, ClientID, @"(arg) {"
    , CMD_PREFIX, ClientID, @"('#destroyobject',arg);
}");


        if (showSelection)
        {
            // Selection - click
            builder.Append(
"function ", SELECT_PREFIX, ClientID, @"(checkBox, arg) {
    if (checkBox == null) return;
        
    var sel = Get('", GetSelectionFieldClientID(), @"');
    var newSel = Get('", hidNewSelection.ClientID, @"');
    var deSel = Get('", hidDeSelection.ClientID, @"');

    if ((sel == null) || (newSel == null) || (deSel == null)) return;
        
    if (newSel.value == '') {
        newSel.value = '|';
    }
    if (deSel.value == '') {
        deSel.value = '|'
    }
    if (sel.value == '') {
        sel.value = '|'
    }
    if (checkBox.checked) {
        sel.value += arg + '|'
        if (deSel.value.indexOf('|' + arg + '|') >= 0) {
            deSel.value = deSel.value.replace('|' + arg + '|', '|')
        }
        else {
            newSel.value += arg + '|'
        }
    }
    else {
        sel.value = sel.value.replace('|' + arg + '|', '|')
        if (newSel.value.indexOf('|' + arg + '|') >= 0) {
            newSel.value = newSel.value.replace('|' + arg + '|', '|')
        }
        else {
            deSel.value += arg + '|'
        }
    }
}
");

            // Selection - select all
            builder.Append(
"function ", SELECT_ALL_PREFIX, ClientID, @"(chkBox) {
    var elems = document.getElementsByTagName('INPUT');
    var re = new RegExp('", ClientID, @"');
    for(i=0; i<elems.length; i++) {
        if(elems[i].type == 'checkbox') {
            if(elems[i].id.match(re)) {
                if((!elems[i].id != chkBox.id) && (chkBox.checked != elems[i].checked)) {
                    elems[i].click();
                }
            }
        }
    }
}
");

            // Selection - clear
            builder.Append(
"function ", CLEAR_SELECTION_PREFIX, ClientID, @"() {
     var inp = document.getElementsByTagName('input');
     if (inp != null) {
         for (var i = 0; i< inp.length; i++) {
             if ((inp[i].type.toLowerCase() == 'checkbox') && (inp[i].id.match(/^", ClientID, @"/i))) {
                 inp[i].checked = false;
             }
         }
     }

     var sel = Get('", GetSelectionFieldClientID(), @"');
     var newSel = Get('", hidNewSelection.ClientID, @"');
     var deSel = Get('", hidDeSelection.ClientID, @"');
     if (sel != null) {
         sel.value = '';
     }
     if (newSel != null) {
         newSel.value = '';
     }
     if (deSel != null) {
         deSel.value = '';
     }
}
");

            // Selection - IsSelectionEmpty
            builder.Append(
"function ", CHECK_SELECTION_PREFIX, ClientID, @"() {
    var sel = Get('", GetSelectionFieldClientID(), @"');
    var items = sel.value;
    return !(items != '' && items != '|');
}
");

            if (resetSelection)
            {
                builder.Append("if (", CLEAR_SELECTION_PREFIX, ClientID, ") { ", CLEAR_SELECTION_PREFIX, ClientID, "(); }");
            }
        }

        ScriptHelper.RegisterStartupScript(this, typeof(string), "UniGrid_" + ClientID, ScriptHelper.GetScript(builder.ToString()));
    }


    /// <summary>
    /// Sets basic form filter.
    /// </summary>
    private void SetBasicFormFilter()
    {
        // Get alternative form layout if defined
        AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(FilterFormName);
        if (afi != null)
        {
            // Get form info
            FormInfo fi = FormHelper.GetFormInfo(FilterFormName, true);
            if (fi != null)
            {
                FilterForm.OnAfterSave += BasicForm_OnAfterSave;
                // Set form info
                FilterForm.FormInformation = fi;

                // Set filter button
                FilterForm.SubmitButton.ID = "btnShow";
                FilterForm.SubmitButton.Text = ResHelper.GetString("general.show");
                FilterForm.SubmitButton.CssClass = "ContentButton";

                // Set layout
                if (!string.IsNullOrEmpty(afi.FormLayout))
                {
                    FilterForm.FormLayout = afi.FormLayout;
                }
                else
                {
                    // Indent filter
                    Literal ltlBreak = new Literal();
                    ltlBreak.EnableViewState = false;
                    ltlBreak.Text = "<br />";
                    plcFilterForm.Controls.Add(ltlBreak);
                }

                FilterForm.CheckFieldEmptiness = false;
                FilterForm.LoadData(FilterFormData);
            }
        }
    }


    /// <summary>
    /// Handles OnAfterSave event of basic form.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event argument</param>
    private void BasicForm_OnAfterSave(object sender, EventArgs e)
    {
        // Set where clause
        WhereClause = FilterForm.GetWhereCondition();
        FilterIsSet = !string.IsNullOrEmpty(WhereClause);
        ReloadData();
    }


    /// <summary>
    /// Checks whether user is authorized for specified action.
    /// </summary>
    /// <param name="actionName">Action name</param>    
    private void CheckActionAndRedirect(string actionName)
    {
        // Get the action
        Action action = GridActions.GetAction(actionName);

        if ((action != null) && (!string.IsNullOrEmpty(action.ModuleName)))
        {
            CurrentUserInfo user = CMSContext.CurrentUser;
            string siteName = CMSContext.CurrentSiteName;

            // Check module permissions
            if (!string.IsNullOrEmpty(action.Permissions) && !user.IsAuthorizedPerResource(action.ModuleName, action.Permissions, siteName))
            {
                RedirectToAccessDenied(action.ModuleName, action.Permissions);
            }

            // Check module UI elements
            if (!string.IsNullOrEmpty(action.UIElements) && !user.IsAuthorizedPerUIElement(action.ModuleName, action.UIElements.Split(';'), siteName))
            {
                RedirectToUIElementAccessDenied(action.ModuleName, action.UIElements);
            }
        }
    }

    #endregion


    #region "IUniPageable Members"

    /// <summary>
    /// Pager data item.
    /// </summary>
    public object PagerDataItem
    {
        get
        {
            return UniGridView.DataSource;
        }
        set
        {
            UniGridView.DataSource = value;
        }
    }


    /// <summary>
    /// Pager control.
    /// </summary>
    public UniPager UniPagerControl
    {
        get;
        set;
    }

    /// <summary>
    /// Occurs when the control bind data.
    /// </summary>
    public event EventHandler<EventArgs> OnPageBinding;


    /// <summary>
    /// Occurs when the pager change the page and current mode is postback => reload data
    /// </summary>
    public event EventHandler<EventArgs> OnPageChanged;


    /// <summary>
    /// Evokes control databind.
    /// </summary>
    public virtual void ReBind()
    {
        if (OnPageChanged != null)
        {
            OnPageChanged(this, null);
        }

        ReloadData();
    }

    #endregion
}
