using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.Controls;

public partial class CMSAdminControls_UI_UniSelector_Controls_SelectionDialog : CMSUserControl
{
    #region "Variables"

    private SelectionModeEnum selectionMode = SelectionModeEnum.SingleButton;
    private string resourcePrefix = "general";
    private string objectType = null;
    private string returnColumnName = null;
    private string displayNameFormat = null;
    private string valuesSeparator = ";";
    private string iconPath = null;

    private bool allowEmpty = true;
    private bool allowAll = false;

    private string filterControl = null;

    private bool useDefaultNameFilter = true;

    private string whereCondition = null;
    private string orderBy = null;

    private int itemsPerPage = 10;

    private GeneralizedInfo iObjectType = null;
    private DataSet result = null;

    private string txtClientId = null;
    private string hdnClientId = null;
    private string hdnDrpId = null;

    private CMSAbstractBaseFilterControl searchControl = null;

    private string emptyReplacement = "&nbsp;";
    private string parentClientId = null;
    private string dialogGridName = "~/CMSAdminControls/UI/UniSelector/DialogItemList.xml";
    private string additionalColumns = null;
    private string callbackMethod = null;
    private string disabledItems = null;
    private string filterMode = null;

    private bool allowEditTextBox = false;
    private bool fireOnChanged = false;
    private bool mLocalizeItems = true;

    private bool mAddGlobalObjectSuffix = false;
    private bool mAddGlobalObjectNamePrefix = false;
    private string mGlobalObjectSuffix = null;

    private bool mRemoveMultipleCommas = false;

    readonly TextHelper th = new TextHelper();
    Hashtable parameters = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates whether to remove multiple commas (can happen when DisplayNameFormat is like {%column1%}, {%column2%}, {column3} and column2 is empty.
    /// </summary>
    public virtual bool RemoveMultipleCommas
    {
        get
        {
            return mRemoveMultipleCommas;
        }
        set
        {
            mRemoveMultipleCommas = value;
        }
    }


    /// <summary>
    /// Gets or set the suffix which is added to global objects if AddGlobalObjectSuffix is true. Default is "(global)".
    /// </summary>
    public string GlobalObjectSuffix
    {
        get
        {
            if (string.IsNullOrEmpty(mGlobalObjectSuffix))
            {
                mGlobalObjectSuffix = GetString("general.global");
            }
            return mGlobalObjectSuffix;
        }
        set
        {
            mGlobalObjectSuffix = value;
        }
    }


    /// <summary>
    /// Indicates whether global objects have suffix "(global)" in the grid.
    /// </summary>
    public bool AddGlobalObjectSuffix
    {
        get
        {
            return mAddGlobalObjectSuffix;
        }
        set
        {
            mAddGlobalObjectSuffix = value;
        }
    }


    /// <summary>
    /// Indicates whether global object name should have prefix '.'
    /// </summary>
    public bool AddGlobalObjectNamePrefix
    {
        get
        {
            return mAddGlobalObjectNamePrefix;
        }
        set
        {
            mAddGlobalObjectNamePrefix = value;
        }
    }


    /// <summary>
    /// Specifies whether the selection dialog should resolve localization macros.
    /// </summary>
    public bool LocalizeItems
    {
        get
        {
            return mLocalizeItems;
        }
        set
        {
            mLocalizeItems = value;
        }
    }


    /// <summary>
    /// Current page index.
    /// </summary>
    public int PageIndex
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["PageIndex"], 0);
        }
        set
        {
            ViewState["PageIndex"] = value;
        }
    }


    /// <summary>
    /// Item prefix.
    /// </summary>
    public string ItemPrefix
    {
        get
        {
            return ValidationHelper.GetString(ViewState["ItemPrefix"], "");
        }
        set
        {
            ViewState["ItemPrefix"] = value;
        }
    }


    /// <summary>
    /// Item prefix.
    /// </summary>
    public string FilterWhere
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FilterWhere"], "");
        }
        set
        {
            ViewState["FilterWhere"] = value;
        }
    }


    /// <summary>
    /// Control's unigrid.
    /// </summary>
    public UniGrid UniGrid
    {
        get
        {
            return this.uniGrid;
        }
    }

    #endregion


    #region "Methods and events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Load parameters
        LoadParameters();

        // Load custom filter
        LoadFilter();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register event handlers
        uniGrid.OnExternalDataBound += uniGrid_OnExternalDataBound;
        uniGrid.OnPageChanged += uniGrid_OnPageChanged;
        uniGrid.IsLiveSite = IsLiveSite;
        if (!RequestHelper.IsPostBack())
        {
            uniGrid.Pager.DefaultPageSize = 10;
        }

        btnSearch.Click += btnSearch_Click;

        // Load data into the unigrid
        LoadControls();

        // Get control IDs from parent window
        txtClientId = QueryHelper.GetString("txtElem", string.Empty);
        hdnClientId = QueryHelper.GetString("hidElem", string.Empty);
        hdnDrpId = QueryHelper.GetString("selectElem", string.Empty);
        parentClientId = QueryHelper.GetString("clientId", string.Empty);

        // Buttons scripts
        string buttonsScript = "function US_Cancel(){ Cancel();  return false; }";

        switch (selectionMode)
        {
            // Button modes
            case SelectionModeEnum.SingleButton:
            case SelectionModeEnum.MultipleButton:
                buttonsScript += "function US_Submit(){ SelectItems(ItemsElem().value); return false; }";
                break;

            // Textbox modes
            case SelectionModeEnum.SingleTextBox:
            case SelectionModeEnum.MultipleTextBox:
                if (allowEditTextBox)
                {
                    buttonsScript += "function US_Submit(){ SelectItems(ItemsElem().value, ItemsElem().value.replace(/^" + valuesSeparator + "+|" + valuesSeparator + "+$/g, ''), " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + ", " + ScriptHelper.GetString(hdnDrpId) + "); return false; }";
                }
                else
                {
                    buttonsScript += "function US_Submit(){ SelectItemsReload(ItemsElem().value, nameElem.value, " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + ", " + ScriptHelper.GetString(hdnDrpId) + "); return false; }";
                }
                break;

            // Other modes
            default:
                buttonsScript += "function US_Submit(){ SelectItemsReload(ItemsElem().value, nameElem.value, " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + ", " + ScriptHelper.GetString(hdnDrpId) + "); return false; }";
                break;
        }

        string script = null;

        switch (selectionMode)
        {
            // Button modes
            case SelectionModeEnum.SingleButton:
            case SelectionModeEnum.MultipleButton:
                {
                    // Register javascript code
                    if (callbackMethod == null)
                    {
                        script = "function SelectItems(items) { wopener.US_SelectItems_" + parentClientId + "(items); window.close(); }";
                    }
                    else
                    {
                        script = "function SelectItems(items) { wopener." + callbackMethod + "(items.replace(/^;+|;+$/g, '')); window.close(); }";
                    }
                }
                break;

            // Selector modes
            default:
                {
                    // Register javascript code
                    script =
                        @"function SelectItems(items, names, hiddenFieldId, txtClientId) { 
                            if(items.length > 0) { 
                                wopener.US_SetItems(items, names, hiddenFieldId, txtClientId); 
                            } else {
                                wopener.US_SetItems('','', hiddenFieldId, txtClientId, null); 
                            }" +
                            (fireOnChanged ? "wopener.US_SelectionChanged_" + parentClientId + "();" : "")
                            + @"window.close(); 
                        }

                        function SelectItemsReload(items, names, hiddenFieldId, txtClientId, hidValue) {
                            if (items.length > 0) { 
                                wopener.US_SetItems(items, names, hiddenFieldId, txtClientId, hidValue); 
                            } else {
                                wopener.US_SetItems('','', hiddenFieldId, txtClientId, hidValue); 
                            }
                            window.close();
                            wopener.US_ReloadPage_" + parentClientId + @"(); 
                            return false; 
                        }";
                }
                break;
        }

        script += @"
            var nameElem = document.getElementById('" + hidName.ClientID + @"');
            
            function ItemsElem()
            {
                return document.getElementById('" + hidItem.ClientID + @"');
            }

            function ProcessItem(chkbox, changeChecked) {   
                if (chkbox != null) {
                    var itemsElem = ItemsElem();
                    var items = itemsElem.value; 
                    var item = chkbox.id.substr(3);
                    if (changeChecked)
                    {
                        chkbox.checked = !chkbox.checked;
                    }
                    if (chkbox.checked)
                    {
                        if (items == '')
                        {
                            itemsElem.value = '" + valuesSeparator + "' + escape(item) + '" + valuesSeparator + @"';
                        }
                        else if (items.toLowerCase().indexOf('" + valuesSeparator + "' + escape(item).toLowerCase() + '" + valuesSeparator + @"') < 0)
                        {
                            itemsElem.value += escape(item) + '" + valuesSeparator + @"';
                        }
                    }
                    else
                    {
                        var re = new RegExp('" + valuesSeparator + "' + escape(item) + '" + valuesSeparator + @"', 'i');
                        itemsElem.value = items.replace(re, '" + valuesSeparator + @"');    
                    }
                }
            }
            
            function Cancel() { window.close(); }

            function SelectAllItems(checkbox)
            {
                var checkboxes = document.getElementsByTagName('input');
                for(var i = 0; i < checkboxes.length; i++)
                {
                    var chkbox = checkboxes[i];
                    if (chkbox.className == 'chckbox')
                    {
                        if(checkbox.checked) { chkbox.checked = true; }
                        else { chkbox.checked = false; }

                        ProcessItem(chkbox);
                    }
                }
            }";

        ltlScript.Text = ScriptHelper.GetScript(script + buttonsScript);
    }


    protected void uniGrid_OnPageChanged(object sender, EventArgs e)
    {
        // Load the grid data
        ReloadGrid();
    }


    /// <summary>
    /// Change header title for multiple selection.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            ChangeSearchCondition();
        }

        // Load the grid data
        ReloadGrid();

        if (uniGrid.GridView.HeaderRow != null)
        {
            switch (selectionMode)
            {
                // Multiple selection
                case SelectionModeEnum.Multiple:
                case SelectionModeEnum.MultipleTextBox:
                case SelectionModeEnum.MultipleButton:
                    {
                        //uniGrid.GridView.HeaderRow.Cells[0].Text = GetString(this.resourcePrefix + ".select|general.select");

                        CheckBox chkAll = new CheckBox();
                        chkAll.ID = "chkAll";
                        chkAll.ToolTip = GetString("UniSelector.CheckAll");
                        chkAll.Attributes.Add("onclick", "SelectAllItems(this)");

                        uniGrid.GridView.HeaderRow.Cells[0].Controls.Clear();
                        uniGrid.GridView.HeaderRow.Cells[0].Controls.Add(chkAll);
                        uniGrid.GridView.Columns[0].ItemStyle.CssClass = "UnigridSelection";

                        uniGrid.GridView.HeaderRow.Cells[1].Text = GetString(resourcePrefix + ".itemname|general.itemname");
                    }
                    break;

                // Single selection
                default:
                    {
                        uniGrid.GridView.Columns[0].Visible = false;
                        uniGrid.GridView.HeaderRow.Cells[1].Text = GetString(resourcePrefix + ".itemname|general.itemname");
                    }
                    break;
            }
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Loads dynamically custom filter if is defined.
    /// </summary>
    private void LoadFilter()
    {
        // Use user filter
        if (!String.IsNullOrEmpty(filterControl))
        {
            pnlFilter.Controls.Clear();

            searchControl = (CMSAbstractBaseFilterControl)LoadControl(filterControl);
            searchControl.FilteredControl = this;
            searchControl.OnFilterChanged += searchControl_OnFilterChanged;
            searchControl.ID = "filterElem";
            searchControl.SelectedValue = hidItem.Value.Replace(valuesSeparator, string.Empty);
            searchControl.FilterMode = filterMode;

            if (searchControl != null)
            {
                pnlFilter.Controls.Add(searchControl);
                pnlFilter.Visible = true;

                // Get init filter where condition
                FilterWhere = SqlHelperClass.AddWhereCondition("", searchControl.WhereCondition);
            }
        }
    }


    /// <summary>
    /// Loads control parameters.
    /// </summary>
    private void LoadParameters()
    {
        string identificator = QueryHelper.GetString("params", null);
        parameters = (Hashtable)WindowHelper.GetItem(identificator);

        if (parameters != null)
        {
            // Load values from session
            selectionMode = (SelectionModeEnum)parameters["SelectionMode"];
            resourcePrefix = ValidationHelper.GetString(parameters["ResourcePrefix"], "general");
            objectType = ValidationHelper.GetString(parameters["ObjectType"], null);
            returnColumnName = ValidationHelper.GetString(parameters["ReturnColumnName"], null);
            valuesSeparator = ValidationHelper.GetString(parameters["ValuesSeparator"], ";");
            iconPath = ValidationHelper.GetString(parameters["IconPath"], null);
            allowEmpty = ValidationHelper.GetBoolean(parameters["AllowEmpty"], true);
            allowAll = ValidationHelper.GetBoolean(parameters["AllowAll"], false);
            filterControl = ValidationHelper.GetString(parameters["FilterControl"], null);
            useDefaultNameFilter = ValidationHelper.GetBoolean(parameters["UseDefaultNameFilter"], true);
            whereCondition = ValidationHelper.GetString(parameters["WhereCondition"], null);
            orderBy = ValidationHelper.GetString(parameters["OrderBy"], null);
            itemsPerPage = ValidationHelper.GetInteger(parameters["ItemsPerPage"], 10);
            emptyReplacement = ValidationHelper.GetString(parameters["EmptyReplacement"], "&nbsp;");
            dialogGridName = ValidationHelper.GetString(parameters["DialogGridName"], dialogGridName);
            additionalColumns = ValidationHelper.GetString(parameters["AdditionalColumns"], null);
            callbackMethod = ValidationHelper.GetString(parameters["CallbackMethod"], null);
            allowEditTextBox = ValidationHelper.GetBoolean(parameters["AllowEditTextBox"], false);
            fireOnChanged = ValidationHelper.GetBoolean(parameters["FireOnChanged"], false);
            disabledItems = ";" + ValidationHelper.GetString(parameters["DisabledItems"], String.Empty) + ";";
            GlobalObjectSuffix = ValidationHelper.GetString(parameters["GlobalObjectSuffix"], "");
            AddGlobalObjectSuffix = ValidationHelper.GetBoolean(parameters["AddGlobalObjectSuffix"], false);
            AddGlobalObjectNamePrefix = ValidationHelper.GetBoolean(parameters["AddGlobalObjectNamePrefix"], false);
            RemoveMultipleCommas = ValidationHelper.GetBoolean(parameters["RemoveMultipleCommas"], false);
            filterMode = ValidationHelper.GetString(parameters["FilterMode"], null);

            // Pre-select unigrid values passed from parent window
            if (!RequestHelper.IsPostBack())
            {
                string values = (string)parameters["Values"];
                if (!String.IsNullOrEmpty(values))
                {
                    hidItem.Value = values;
                    parameters["Values"] = null;
                }
            }

            displayNameFormat = ValidationHelper.GetString(parameters["DisplayNameFormat"], null);
        }
    }


    /// <summary>
    /// Loads variables and objects.
    /// </summary>
    private void LoadControls()
    {
        // Display default name filter
        if (useDefaultNameFilter)
        {
            lblSearch.ResourceString = resourcePrefix + ".entersearch|general.entersearch";
            btnSearch.ResourceString = "general.search";

            pnlSearch.Visible = true;

            if (!RequestHelper.IsPostBack())
            {
                ScriptHelper.RegisterStartupScript(this, typeof(string), "Focus", ScriptHelper.GetScript("try{document.getElementById('" + txtSearch.ClientID + "').focus();}catch(err){}"));
            }
        }

        // Load objects
        if (!String.IsNullOrEmpty(objectType))
        {
            iObjectType = CMSObjectHelper.GetReadOnlyObject(objectType);
            if (iObjectType == null)
            {
                throw new Exception("[UniSelector.SelectionDialog]: Object type '" + objectType + "' not found.");
            }

            if (returnColumnName == null)
            {
                returnColumnName = iObjectType.IDColumn;
            }
        }

        if (!RequestHelper.IsPostBack())
        {
            uniGrid.Pager.DefaultPageSize = itemsPerPage;
        }

        uniGrid.GridName = dialogGridName;
        uniGrid.GridView.EnableViewState = false;

        // Show the OK button if needed
        switch (selectionMode)
        {
            case SelectionModeEnum.Multiple:
            case SelectionModeEnum.MultipleTextBox:
            case SelectionModeEnum.MultipleButton:
                {
                    pnlAll.Visible = true;

                    lnkSelectAll.Text = GetString("UniSelector.SelectAll");
                    lnkDeselectAll.Text = GetString("UniSelector.DeselectAll");
                }
                break;
        }
    }


    /// <summary>
    /// Returns dataset for specified GeneralizedInfo.
    /// </summary>
    /// <param name="returnColumn">Return column</param>
    private DataSet GetData(string returnColumn)
    {
        int totalRecords = 0;
        return GetData(returnColumn, 0, 0, ref totalRecords, true);
    }


    /// <summary>
    /// Returns dataset for specified GeneralizedInfo.
    /// </summary>
    private DataSet GetData(string returnColumn, int offset, int maxRecords, ref int totalRecords, bool selection)
    {
        // If object type is set
        if (iObjectType != null)
        {
            // Init columns
            string columns = null;

            if (!selection)
            {
                if (displayNameFormat == UniSelector.USER_DISPLAY_FORMAT)
                {
                    // Ensure columns which are needed for USER_DISPLAY_FORMAT
                    columns = "UserName;FullName";
                }
                else if (displayNameFormat != null)
                {
                    columns = DataHelper.GetNotEmpty(TextHelper.GetMacros(displayNameFormat), iObjectType.DisplayNameColumn).Replace(";", ", ");
                }
                else
                {
                    columns = iObjectType.DisplayNameColumn;
                }
            }

            // Add return column name
            columns = SqlHelperClass.MergeColumns(columns, returnColumn);

            // Add additional columns
            columns = SqlHelperClass.MergeColumns(columns, additionalColumns);

            // Get SiteID column if needed
            if (this.AddGlobalObjectSuffix && !string.IsNullOrEmpty(iObjectType.SiteIDColumn))
            {
                columns = SqlHelperClass.MergeColumns(columns, iObjectType.SiteIDColumn);
            }

            string where = SqlHelperClass.AddWhereCondition(whereCondition, FilterWhere);
            if (!String.IsNullOrEmpty(uniGrid.WhereClause))
            {
                where = SqlHelperClass.AddWhereCondition(where, uniGrid.WhereClause);
            }

            // Order by
            if (String.IsNullOrEmpty(orderBy))
            {
                orderBy = iObjectType.DisplayNameColumn;
            }

            return iObjectType.GetData(null, where, (selection ? null : orderBy), 0, columns, false, offset, maxRecords, ref totalRecords);
        }
        else
        {
            totalRecords = 0;
            return null;
        }
    }


    /// <summary>
    /// Changes ViewState with search condition for UniGrid.
    /// </summary>
    private void ChangeSearchCondition()
    {
        if (iObjectType != null)
        {
            string where = null;

            // Get default filter where
            if ((useDefaultNameFilter) && (txtSearch.Text != String.Empty))
            {
                if (displayNameFormat == UniSelector.USER_DISPLAY_FORMAT)
                {
                    // Ensure search in columns which are needed for USER_DISPLAY_FORMAT
                    where = String.Format("UserName LIKE N'%{0}%' OR FullName LIKE N'%{0}%'", txtSearch.Text.Trim().Replace("'", "''"));
                }
                else
                {
                    where = String.Format("{0} LIKE N'%{1}%'", iObjectType.DisplayNameColumn, txtSearch.Text.Trim().Replace("'", "''"));
                }
            }

            // Get search filter where
            if (searchControl != null)
            {
                where = SqlHelperClass.AddWhereCondition(where, searchControl.WhereCondition);
            }

            // Save where condition to the view state
            FilterWhere = where;
        }
    }


    /// <summary>
    /// Reloads the grid with given page index.
    /// </summary>
    protected void ReloadGrid()
    {
        int totalRecords = 0;
        int offset = uniGrid.Pager.CurrentPageSize * (uniGrid.Pager.CurrentPage - 1);
        // Reload data set with new page index
        result = GetData(returnColumnName, offset, uniGrid.Pager.CurrentPageSize, ref totalRecords, false);

        uniGrid.PagerForceNumberOfResults = totalRecords;
        uniGrid.DataSource = result;
        uniGrid.ZeroRowsText = !string.IsNullOrEmpty(FilterWhere) ? GetString(resourcePrefix + ".noitemsfound|general.noitemsfound") : GetString(resourcePrefix + ".nodatafound|general.nodatafound");
        uniGrid.ReloadData();
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Unigrid external data bound handler.
    /// </summary>
    protected object uniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLowerInvariant())
        {
            case "yesno":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "select":
                {
                    DataRowView drv = (parameter as DataRowView);

                    // Get item ID
                    string itemID = drv[returnColumnName].ToString();

                    // Add global object name prefix if required
                    if (AddGlobalObjectNamePrefix && !String.IsNullOrEmpty(iObjectType.SiteIDColumn) && (ValidationHelper.GetInteger(DataHelper.GetDataRowValue(drv.Row, iObjectType.SiteIDColumn), 0) == 0))
                    {
                        itemID = "." + itemID;
                    }

                    // Add checkbox for multiple selection
                    switch (selectionMode)
                    {
                        case SelectionModeEnum.Multiple:
                        case SelectionModeEnum.MultipleTextBox:
                        case SelectionModeEnum.MultipleButton:
                            {
                                string checkBox = "<input id=\"chk" + itemID + "\" type=\"checkbox\" onclick=\"ProcessItem(this);\" class=\"chckbox\" ";
                                if (hidItem.Value.IndexOf(valuesSeparator + itemID + valuesSeparator, StringComparison.CurrentCultureIgnoreCase) >= 0)
                                {
                                    checkBox += "checked=\"checked\" ";
                                }
                                if (disabledItems.Contains(";" + itemID + ";"))
                                {
                                    checkBox += "disabled=\"disabled\" ";
                                }
                                checkBox += "/>";

                                return checkBox;
                            }
                    }
                }
                break;

            case "itemname":
                {
                    DataRowView drv = (parameter as DataRowView);

                    // Get item ID
                    string itemID = drv[returnColumnName].ToString();

                    // Get item name
                    string itemName = "";

                    // Special formatted user name
                    if (displayNameFormat == UniSelector.USER_DISPLAY_FORMAT)
                    {
                        string userName = ValidationHelper.GetString(DataHelper.GetDataRowValue(drv.Row, "UserName"), String.Empty);
                        string fullName = ValidationHelper.GetString(DataHelper.GetDataRowValue(drv.Row, "FullName"), String.Empty);

                        itemName = Functions.GetFormattedUserName(userName, fullName, IsLiveSite);
                    }
                    else if (displayNameFormat == null)
                    {
                        itemName = drv[iObjectType.DisplayNameColumn].ToString();
                    }
                    else
                    {
                        itemName = th.MergeText(displayNameFormat, drv.Row);
                    }

                    if (RemoveMultipleCommas)
                    {
                        itemName = TextHelper.RemoveMultipleCommas(itemName);
                    }

                    // Add the prefixes
                    itemName = ItemPrefix + itemName;
                    itemID = ItemPrefix + itemID;

                    // Add global object name prefix if required
                    if (AddGlobalObjectNamePrefix && !String.IsNullOrEmpty(iObjectType.SiteIDColumn) && (ValidationHelper.GetInteger(DataHelper.GetDataRowValue(drv.Row, iObjectType.SiteIDColumn), 0) == 0))
                    {
                        itemID = "." + itemID;
                    }

                    if (String.IsNullOrEmpty(itemName))
                    {
                        itemName = emptyReplacement;
                    }

                    if (this.AddGlobalObjectSuffix)
                    {
                        if ((iObjectType != null) && !string.IsNullOrEmpty(iObjectType.SiteIDColumn))
                        {
                            itemName += (ValidationHelper.GetInteger(DataHelper.GetDataRowValue(drv.Row, iObjectType.SiteIDColumn), 0) > 0 ? "" : " " + this.GlobalObjectSuffix); ;
                        }
                    }

                    // Link action
                    string onclick = null;
                    bool disabled = disabledItems.Contains(";" + itemID + ";");
                    if (!disabled)
                    {
                        switch (selectionMode)
                        {
                            case SelectionModeEnum.Multiple:
                            case SelectionModeEnum.MultipleTextBox:
                            case SelectionModeEnum.MultipleButton:
                                onclick = "ProcessItem(document.getElementById('chk" + ScriptHelper.GetString(itemID).Trim('\'') + "'), true); return false;";
                                break;

                            case SelectionModeEnum.SingleButton:
                                onclick = "SelectItems(" + GetSafe(itemID) + "); return false;";
                                break;

                            case SelectionModeEnum.SingleTextBox:
                                if (allowEditTextBox)
                                {
                                    onclick = "SelectItems(" + GetSafe(itemID) + ", " + GetSafe(itemID) + ", " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + "); return false;";
                                }
                                else
                                {
                                    onclick = "SelectItems(" + GetSafe(itemID) + ", " + GetSafe(itemName) + ", " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + "); return false;";
                                }
                                break;

                            default:
                                onclick = "SelectItemsReload(" + GetSafe(itemID) + ", " + GetSafe(itemName) + ", " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + ", " + ScriptHelper.GetString(hdnDrpId) + "); return false;";
                                break;
                        }

                        onclick = "onclick=\"" + onclick + "\" ";
                    }

                    if (LocalizeItems)
                    {
                        itemName = ResHelper.LocalizeString(itemName);
                    }

                    return "<div " + (!disabled ? "class=\"SelectableItem\" " : null) + onclick + ">" + HTMLHelper.HTMLEncode(TextHelper.LimitLength(itemName, 100)) + "</div>";
                }
        }

        return null;
    }


    /// <summary>
    /// Returns string safe for inserting to javascript as parameter.
    /// </summary>
    /// <param name="param">Parameter</param>    
    private string GetSafe(string param)
    {
        // Replace + char for %20 to make it compatible with client side decodeURIComponent
        return ScriptHelper.GetString(Server.UrlEncode(param).Replace("+", "%20"));
    }


    /// <summary>
    /// Button search event handler.
    /// </summary>
    void btnSearch_Click(object sender, EventArgs e)
    {
        ChangeSearchCondition();
    }


    /// <summary>
    /// On search condition changed.
    /// </summary>
    void searchControl_OnFilterChanged()
    {
        ChangeSearchCondition();
    }


    /// <summary>
    /// Overriden to get the parameters.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    public override object GetValue(string propertyName)
    {
        if ((parameters != null) && parameters.Contains(propertyName))
        {
            return parameters[propertyName];
        }

        return base.GetValue(propertyName);
    }


    /// <summary>
    /// Overriden set value to collect parameters.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    /// <param name="value">Value</param>
    public override void SetValue(string propertyName, object value)
    {
        // Handle special properties
        switch (propertyName.ToLower())
        {
            case "itemprefix":
                ItemPrefix = ValidationHelper.GetString(value, "");
                break;
        }

        base.SetValue(propertyName, value);

        // Set parameters for dialog
        parameters[propertyName] = value;
    }


    protected void lnkSelectAll_Click(object sender, EventArgs e)
    {
        if (iObjectType != null)
        {
            // Get all values
            DataSet ds = GetData(returnColumnName);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                string[] values = hidItem.Value.Split(';');

                // Build hashtable of current values
                Hashtable ht = new Hashtable(values.Length);
                foreach (string value in values)
                {
                    ht[value] = true;
                }

                DataTable dt = ds.Tables[0];
                int colIndex = dt.Columns.IndexOf(returnColumnName);

                // Process all items
                foreach (DataRow dr in dt.Rows)
                {
                    string value = dr[returnColumnName].ToString();

                    if (!ht.Contains(value))
                    {
                        ht.Add(value, true);
                    }
                }

                // Build the selected values string
                StringBuilder sb = new StringBuilder();
                sb.Append(";");

                foreach (string value in ht.Keys)
                {
                    sb.Append(value, ";");
                }

                hidItem.Value = sb.ToString();
            }

            pnlHidden.Update();
        }
    }


    protected void lnkDeselectAll_Click(object sender, EventArgs e)
    {
        DataSet ds = GetData(returnColumnName);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            string[] values = hidItem.Value.Split(';');

            // Build hashtable of current values
            Hashtable ht = new Hashtable(values.Length);
            foreach (string value in values)
            {
                ht[value] = true;
            }

            // Remove the values from hashtable
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string value = dr[returnColumnName].ToString();

                if (ht.Contains(value))
                {
                    ht.Remove(value);
                }
            }

            // Build the selected values string
            StringBuilder sb = new StringBuilder();
            sb.Append(";");

            foreach (string value in ht.Keys)
            {
                sb.Append(value, ";");
            }

            hidItem.Value = sb.ToString();
        }

        // Remove the selection from hidden fields
        pnlHidden.Update();
    }

    #endregion
}
