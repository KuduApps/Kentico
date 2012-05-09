using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using System.Text;

public partial class CMSAdminControls_UI_UniSelector_UniSelector : UniSelector, IPostBackEventHandler, ICallbackEventHandler
{
    #region "Private variables"

    private bool registerScripts = false;
    private string checkBoxClass = null;

    private TextHelper th = new TextHelper();
    private Hashtable parameters = new Hashtable();
    private GeneralizedInfo objectType = null;
    private GeneralizedInfo listingObject = null;
    private string mListingObjectType = string.Empty;
    private string mListingWhereCondition = String.Empty;
    private DataSet result = null;

    private bool enabled = true;
    private bool mLoaded = false;
    private bool mPageChanged = false;
    private int mNewCurrentPage = 0;

    private string mEditWindowName = "EditWindow";
    private int mEditDialogWindowWidth = 1000;
    private int mEditDialogWindowHeight = 700;
    private bool mDynamicFirstColumnName = true;
    private bool mIsSiteManager = false;

    private StringBuilder javaScript = new StringBuilder();

    #endregion


    #region "Delegates and events"

    /// <summary>
    /// Delegate for additional data bound event.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="sourceName">Source name for grid column</param>
    /// <param name="parameter">Column (row) grid values</param>
    /// <param name="value">Value given from classic grid databound event</param>
    public delegate object AdditionalDataBoundEventHandler(object sender, string sourceName, object parameter, string value);


    /// <summary>
    /// Event for additional manipulation with uni selector grid.
    /// </summary>
    public event AdditionalDataBoundEventHandler OnAdditionalDataBound;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Object of the specific given type.
    /// </summary>
    protected GeneralizedInfo Object
    {
        get
        {
            // Make sure that objects specified by given object type exist
            if ((objectType == null) && !String.IsNullOrEmpty(ObjectType))
            {
                objectType = CMSObjectHelper.GetObject(ObjectType);
            }

            return objectType;
        }
    }


    /// <summary>
    /// Type of alternative info object used in multiple picker grid view.
    /// </summary>
    public GeneralizedInfo ListingObject
    {
        get
        {
            // Make sure that objects specified by given object type exist
            if ((listingObject == null) && !String.IsNullOrEmpty(ListingObjectType))
            {
                listingObject = CMSObjectHelper.GetObject(ListingObjectType);
            }

            return listingObject;
        }
    }


    /// <summary>
    /// If true first column is dynamically named by object type info.
    /// </summary>
    public bool DynamicColumnName
    {
        get
        {
            return mDynamicFirstColumnName;
        }
        set
        {
            mDynamicFirstColumnName = value;
        }
    }


    /// <summary>
    /// If true, uniselector is used in site manager.
    /// </summary>
    public bool IsSiteManager
    {
        get
        {
            return mIsSiteManager;
        }
        set
        {
            mIsSiteManager = value;
        }
    }


    /// <summary>
    /// Name of alternative grid view listing object type.
    /// </summary>
    public string ListingObjectType
    {
        get
        {
            return mListingObjectType;
        }
        set
        {
            mListingObjectType = value;
        }
    }


    /// <summary>
    /// Name of alternative grid view listing object type.
    /// </summary>
    public string ListingWhereCondition
    {
        get
        {
            return mListingWhereCondition;
        }
        set
        {
            mListingWhereCondition = value;
        }
    }


    /// <summary>
    /// Dialog control identificator.
    /// </summary>
    protected string Identificator
    {
        get
        {
            string identificator = hdnIdentificator.Value;
            if (string.IsNullOrEmpty(identificator))
            {
                identificator = Request.Form[hdnIdentificator.UniqueID];
                if (string.IsNullOrEmpty(identificator))
                {
                    identificator = Guid.NewGuid().ToString();
                }
                hdnIdentificator.Value = identificator;
            }

            return identificator;
        }
    }


    /// <summary>
    /// Gets the value that indicates whether current selector in multiple mode displays some data or whether the dropdown contains some data.
    /// </summary>
    public override bool HasData
    {
        get
        {
            // Ensure the data
            if (!StopProcessing)
            {
                Reload(false);
            }

            return ValidationHelper.GetBoolean(ViewState["HasData"], false);
        }
        protected set
        {
            ViewState["HasData"] = value;
        }
    }


    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(ViewState["ZeroRowsText"], null);
        }
        set
        {
            ViewState["ZeroRowsText"] = value;
        }
    }


    /// <summary>
    /// Name of the edit window.
    /// </summary>
    public string EditWindowName
    {
        get
        {
            return mEditWindowName;
        }
        set
        {
            mEditWindowName = value;
        }
    }


    /// <summary>
    /// Gets or sets the width of modal dialog window used for editing.
    /// </summary>
    public int EditDialogWindowWidth
    {
        get
        {
            return this.mEditDialogWindowWidth;
        }
        set
        {
            this.mEditDialogWindowWidth = value;
        }
    }


    /// <summary>
    /// Gets or sets the height of modal dialog window used for editing.
    /// </summary>
    public int EditDialogWindowHeight
    {
        get
        {
            return this.mEditDialogWindowHeight;
        }
        set
        {
            this.mEditDialogWindowHeight = value;
        }
    }


    /// <summary>
    /// Value of the "(all)" DDL record, -1 by default.
    /// </summary>
    public string AllRecordValue
    {
        get
        {
            return ValidationHelper.GetString(ViewState["AllRecordValue"], US_ALL_RECORDS.ToString());
        }
        set
        {
            ViewState["AllRecordValue"] = value;
        }
    }


    /// <summary>
    /// Value of the "(none)" DDL record, 0 by default.
    /// </summary>
    public string NoneRecordValue
    {
        get
        {
            return ValidationHelper.GetString(ViewState["NoneRecordValue"], US_NONE_RECORD.ToString());
        }
        set
        {
            ViewState["NoneRecordValue"] = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            switch (SelectionMode)
            {
                // Dropdown mode
                case SelectionModeEnum.SingleDropDownList:
                    if (!String.IsNullOrEmpty(hdnDialogSelect.Value))
                    {
                        return hdnDialogSelect.Value;
                    }
                    else
                    {
                        return drpSingleSelect.SelectedValue;
                    }

                // Textbox mode
                case SelectionModeEnum.SingleTextBox:
                case SelectionModeEnum.MultipleTextBox:
                    if (AllowEditTextBox)
                    {
                        return txtSingleSelect.Text.Trim(ValuesSeparator.ToCharArray());
                    }
                    else
                    {
                        return hiddenField.Value.Trim(ValuesSeparator.ToCharArray());
                    }

                // Other modes
                default:
                    return hiddenField.Value.Trim(ValuesSeparator.ToCharArray());
            }
        }
        set
        {
            switch (SelectionMode)
            {
                // Dropdown mode
                case SelectionModeEnum.SingleDropDownList:
                    hdnDialogSelect.Value = ValidationHelper.GetString(value, "");
                    break;

                // Textbox mode
                case SelectionModeEnum.SingleTextBox:
                case SelectionModeEnum.MultipleTextBox:
                    if (AllowEditTextBox)
                    {
                        txtSingleSelect.Text = ValidationHelper.GetString(value, "").Trim(ValuesSeparator.ToCharArray());
                    }
                    else
                    {
                        hiddenField.Value = ";" + ValidationHelper.GetString(value, "").Trim(ValuesSeparator.ToCharArray()) + ";";
                    }
                    break;

                // Other modes
                default:
                    hiddenField.Value = ";" + ValidationHelper.GetString(value, "").Trim(ValuesSeparator.ToCharArray()) + ";";
                    break;
            }
            ViewState["HasValue"] = true;
        }
    }


    /// <summary>
    /// Gets or sets if control is enabled.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return enabled;
        }
        set
        {
            enabled = value;

            btnClear.Enabled = enabled;
            btnSelect.Enabled = enabled;
            btnDialog.Enabled = enabled;
            lnkDialog.Enabled = enabled;
            drpSingleSelect.Enabled = enabled;
            txtSingleSelect.Enabled = enabled;
            btnRemoveSelected.Enabled = enabled;
            btnAddItems.Enabled = enabled;
            btnMenu.Enabled = enabled;
        }
    }


    /// <summary>
    /// Indicates if UniSelector value was set.
    /// </summary>
    public bool HasValue
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["HasValue"], false);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the UniSelector should apply WhereCondition for the selected value (default: false). This does not affect the modal dialog.
    /// </summary>
    public bool ApplyValueRestrictions
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["ApplyValueRestrictions"], true);
        }
        set
        {
            ViewState["ApplyValueRestrictions"] = value;
        }
    }

    #endregion


    #region "Controls properties"

    /// <summary>
    /// Gets the single select drop down field.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            return drpSingleSelect;
        }
    }


    /// <summary>
    /// Drop down list selection edit button.
    /// </summary>
    public Button ButtonDropDownEdit
    {
        get
        {
            return btnDropEdit;
        }
    }


    /// <summary>
    /// Gets the Select button control.
    /// </summary>
    public Button ButtonSelect
    {
        get
        {
            return btnSelect;
        }
    }


    /// <summary>
    /// Gets the Clear button control.
    /// </summary>
    public Button ButtonClear
    {
        get
        {
            return btnClear;
        }
    }


    /// <summary>
    /// Gets the Remove selected items button.
    /// </summary>
    public Button ButtonRemoveSelected
    {
        get
        {
            return btnRemoveSelected;
        }
    }


    /// <summary>
    /// Gets the Add items button control.
    /// </summary>
    public Button ButtonAddItems
    {
        get
        {
            return btnAddItems;
        }
    }


    /// <summary>
    /// Gets the text box selection control.
    /// </summary>
    public TextBox TextBoxSelect
    {
        get
        {
            return txtSingleSelect;
        }
    }


    /// <summary>
    /// Textbox selection edit button.
    /// </summary>
    public Button ButtonEdit
    {
        get
        {
            return btnEdit;
        }
    }


    /// <summary>
    /// Multiple selection grid.
    /// </summary>
    public UniGrid UniGrid
    {
        get
        {
            return uniGrid;
        }
    }


    /// <summary>
    /// Button selection control.
    /// </summary>
    public Button ButtonDialog
    {
        get
        {
            return btnDialog;
        }
    }


    /// <summary>
    /// Link selection control.
    /// </summary>
    public HyperLink LinkDialog
    {
        get
        {
            return lnkDialog;
        }
    }


    /// <summary>
    /// Link selection image control.
    /// </summary>
    public Image ImageDialog
    {
        get
        {
            return imgDialog;
        }
    }


    /// <summary>
    /// New item button.
    /// </summary>
    public Button ButtonDropDownNew
    {
        get
        {
            return btnDropNew;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        return true;
    }


    protected override void LoadViewState(object savedState)
    {
        base.LoadViewState(savedState);

        // Get values from form if the control is loaded dynamicly
        hdnIdentificator.Value = Request.Form[hdnIdentificator.UniqueID];
        hiddenField.Value = Request.Form[hiddenField.UniqueID];
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (RequestHelper.IsCallback())
        {
            StopProcessing = true;
            return;
        }

        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(this.Page);
        ScriptHelper.RegisterWOpenerScript(this.Page);

        checkBoxClass = Guid.NewGuid().ToString().Replace("-", "");

        // Bound events
        drpSingleSelect.SelectedIndexChanged += drpSingleSelect_SelectedIndexChanged;

        uniGrid.OnExternalDataBound += uniGrid_OnExternalDataBound;
        uniGrid.OnPageChanged += new EventHandler<EventArgs>(uniGrid_OnPageChanged);
        uniGrid.IsLiveSite = IsLiveSite;

        uniGrid.Pager.DefaultPageSize = 10;

        btnClear.Click += btnClear_Click;

        // If control is enabled, then display content
        if (!StopProcessing && (SelectionMode == SelectionModeEnum.Multiple))
        {
            if (!RequestHelper.CausedPostback(btnRemoveSelected, this, uniGrid.Pager.UniPager))
            {
                Reload(false);
            }
        }
    }


    /// <summary>
    /// Change header title for multiple selection.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if (StopProcessing)
        {
            return;
        }

        // If control is enabled, then display content
        if ((SelectionMode != SelectionModeEnum.Multiple) || !mLoaded)
        {
            Reload(false);
        }

        if (registerScripts)
        {
            RegisterScripts();
        }

        // Display two columns when in multiple selection
        if ((SelectionMode == SelectionModeEnum.Multiple) && (uniGrid.GridView.HeaderRow != null))
        {
            CheckBox chkAll = new CheckBox();
            chkAll.ID = "chkAll";
            chkAll.ToolTip = GetString("General.CheckAll");
            chkAll.InputAttributes.Add("onclick", "US_SelectAllItems('" + ClientID + "', '" + ValuesSeparator + "', this, 'chk" + checkBoxClass + "')");
            chkAll.Enabled = Enabled;

            uniGrid.GridView.HeaderRow.Cells[0].Controls.Clear();
            uniGrid.GridView.HeaderRow.Cells[0].Controls.Add(chkAll);
            uniGrid.GridView.Columns[0].ItemStyle.CssClass = "UnigridSelection";

            if (DynamicColumnName)
            {
                uniGrid.GridView.HeaderRow.Cells[1].Text = GetString(ResourcePrefix + ".itemname|general.itemname");
            }
        }

        lblStatus.Visible = !String.IsNullOrEmpty(lblStatus.Text);

        // If the page was not changed, deselect all
        if (!mPageChanged)
        {
            hiddenSelected.Value = "";
        }

        // Always reset the new value from dialog
        hdnDialogSelect.Value = "";

        base.OnPreRender(e);
    }


    protected override void Render(HtmlTextWriter writer)
    {
        if (StopProcessing)
        {
            return;
        }
        // Render starting tag of wrapping element
        switch (SelectionMode)
        {
            case SelectionModeEnum.SingleButton:
            case SelectionModeEnum.SingleDropDownList:
            case SelectionModeEnum.SingleTextBox:
                writer.Write("<span id=\"" + ClientID + "\">");
                break;

            case SelectionModeEnum.Multiple:
            case SelectionModeEnum.MultipleButton:
            case SelectionModeEnum.MultipleTextBox:
                writer.Write("<div id=\"" + ClientID + "\">");
                break;
        }

        // Render child controls
        RenderChildren(writer);

        // Render ending tag of wrapping element
        switch (SelectionMode)
        {
            case SelectionModeEnum.SingleButton:
            case SelectionModeEnum.SingleDropDownList:
            case SelectionModeEnum.SingleTextBox:
                writer.Write("</span>");
                break;

            case SelectionModeEnum.Multiple:
            case SelectionModeEnum.MultipleButton:
            case SelectionModeEnum.MultipleTextBox:
                writer.Write("</div>");
                break;
        }
    }


    /// <summary>
    /// Reloads all controls.
    /// </summary>
    /// <param name="forceReload">Indicates if data should be loaded from DB</param>
    public void Reload(bool forceReload)
    {
        if (!mLoaded || forceReload)
        {
            LoadObjects(forceReload);

            if (!StopProcessing)
            {
                SetupControls();
                
                registerScripts = true;

                ReloadData(forceReload);
            }

            mLoaded = true;
        }
    }


    /// <summary>
    /// Displays data according to selection mode.
    /// </summary>
    private void ReloadData(bool forceReload)
    {
        // Check that object type is not empty
        if (Object != null)
        {
            // Display form control content according to selection mode
            switch (SelectionMode)
            {
                case SelectionModeEnum.SingleTextBox:
                    DisplayTextBox();
                    break;

                case SelectionModeEnum.SingleDropDownList:
                    DisplayDropDownList(forceReload);
                    break;

                case SelectionModeEnum.Multiple:
                    DisplayMultiple(forceReload);
                    break;

                case SelectionModeEnum.MultipleTextBox:
                    DisplayMultipleTextBox();
                    break;

                case SelectionModeEnum.SingleButton:
                case SelectionModeEnum.MultipleButton:
                    DisplayButton();
                    break;
            }
        }
    }


    /// <summary>
    /// Setups controls.
    /// </summary>
    private void SetupControls()
    {
        // Add resource strings
        btnClear.ResourceString = ResourcePrefix + ".clear|general.clear";
        lblClear.ResourceString = ResourcePrefix + ".clear|general.clear";
        btnSelect.ResourceString = ResourcePrefix + ".select|general.select";
        lblSelect.ResourceString = ResourcePrefix + ".select|general.select";
        btnDialog.ResourceString = ResourcePrefix + ".select|general.select";
        lblDialog.ResourceString = ResourcePrefix + ".select|general.select";
        lnkDialog.ResourceString = ResourcePrefix + ".select|general.select";
        lblSingleSelectTxt.ResourceString = ResourcePrefix + ".selectitem|general.selectitem";
        lblSingleSelectDrp.ResourceString = ResourcePrefix + ".selectitem|general.selectitem";

        // Add event handlers
        string selScript = "US_SelectionDialog_" + ClientID + "(); return false;";

        switch (SelectionMode)
        {
            // Dropdownlist mode
            case SelectionModeEnum.SingleDropDownList:
                {
                    if (!String.IsNullOrEmpty(EditItemPageUrl))
                    {
                        btnDropEdit.Visible = true;
                        btnDropEdit.ResourceString = ResourcePrefix + ".edit|general.edit";
                        lblDropEdit.Visible = true;
                        lblDropEdit.ResourceString = ResourcePrefix + ".edit|general.edit";
                        btnDropEdit.OnClientClick = "US_EditItem_" + ClientID + "(document.getElementById('" + drpSingleSelect.ClientID + "').value); return false;";
                    }

                    // New button
                    if (!String.IsNullOrEmpty(NewItemPageUrl))
                    {
                        btnDropNew.Visible = true;
                        btnDropNew.ResourceString = ResourcePrefix + ".new|general.new";
                        lblDropNew.Visible = true;
                        lblDropNew.ResourceString = ResourcePrefix + ".new|general.new";
                        btnDropNew.OnClientClick = "US_NewItem_" + ClientID + "(document.getElementById('" + drpSingleSelect.ClientID + "').value); return false;";
                    }
                }
                break;

            // Multiple selection mode
            case SelectionModeEnum.Multiple:
                {
                    btnRemoveSelected.ResourceString = ResourcePrefix + ".removeselected|general.removeselected";
                    lblRemoveSelected.ResourceString = ResourcePrefix + ".removeselected|general.removeselected";
                    btnAddItems.ResourceString = ResourcePrefix + ".additems|general.additems";
                    lblAddItems.ResourceString = ResourcePrefix + ".additems|general.additems";

                    string confirmation = RemoveConfirmation;
                    if (confirmation == null)
                    {
                        confirmation = GetString("general.confirmremove");
                    }
                    if (!String.IsNullOrEmpty(confirmation))
                    {
                        btnRemoveSelected.OnClientClick = "return confirm(" + ScriptHelper.GetString(confirmation) + ")";
                    }

                    btnAddItems.OnClientClick = selScript;

                    this.btnMenu.Parameter = ScriptHelper.GetString(this.ClientID);
                    this.btnMenu.MenuControlPath = "~/CMSAdminControls/UI/UniSelector/Controls/SelectorMenu.ascx";
                    this.btnMenu.IsLiveSite = IsLiveSite;
                }
                break;

            // Button mode
            case SelectionModeEnum.SingleButton:
            case SelectionModeEnum.MultipleButton:
                // Setup button
                if (ButtonImage == null)
                {
                    btnDialog.OnClientClick = selScript;
                }
                else
                {
                    if (Enabled)
                    { 
                        lnkDialog.OnClientClick = selScript;
                    }
                    lnkDialog.NavigateUrl = "#";
                    if (ButtonImage != "")
                    {
                        imgDialog.ImageUrl = ButtonImage;
                        imgDialog.AlternateText = GetString(ResourcePrefix + ".select|general.select");
                        imgDialog.ToolTip = GetString(ResourcePrefix + ".select|general.select");
                    }
                    else
                    {
                        imgDialog.Visible = false;
                    }
                }
                break;

            // Single textbox mode
            case SelectionModeEnum.SingleTextBox:
                {
                    // Select button
                    if (AllowEditTextBox)
                    {
                        btnSelect.OnClientClick = "US_SelectionDialog_" + ClientID + "('$|' + document.getElementById('" + txtSingleSelect.ClientID + "').value); return false;";
                    }
                    else
                    {
                        btnSelect.OnClientClick = selScript;
                    }

                    // Edit button
                    if (!String.IsNullOrEmpty(EditItemPageUrl))
                    {
                        btnEdit.Visible = true;
                        btnEdit.ResourceString = ResourcePrefix + ".edit|general.edit";
                        lblEdit.Visible = true;
                        lblEdit.ResourceString = ResourcePrefix + ".edit|general.edit";
                        if (AllowEditTextBox)
                        {
                            btnEdit.OnClientClick = "US_EditItem_" + ClientID + "(document.getElementById('" + txtSingleSelect.ClientID + "').value); return false;";
                        }
                        else
                        {
                            btnEdit.OnClientClick = "US_EditItem_" + ClientID + "(document.getElementById('" + hiddenField.ClientID + "').value); return false;";
                        }
                    }

                    // New button
                    if (!String.IsNullOrEmpty(NewItemPageUrl))
                    {
                        btnNew.Visible = true;
                        btnNew.ResourceString = ResourcePrefix + ".new|general.new";
                        lblNew.Visible = true;
                        lblNew.ResourceString = ResourcePrefix + ".new|general.new";
                        if (AllowEditTextBox)
                        {
                            btnNew.OnClientClick = "US_NewItem_" + ClientID + "(document.getElementById('" + txtSingleSelect.ClientID + "').value); return false;";
                        }
                        else
                        {
                            btnNew.OnClientClick = "US_NewItem_" + ClientID + "(document.getElementById('" + hiddenField.ClientID + "').value); return false;";
                        }
                    }
                }
                break;

            // Multiple textbox
            case SelectionModeEnum.MultipleTextBox:
                // Select button
                if (AllowEditTextBox)
                {
                    btnSelect.OnClientClick = "US_SelectionDialog_" + ClientID + "('$|' + document.getElementById('" + txtSingleSelect.ClientID + "').value); return false;";
                }
                else
                {
                    btnSelect.OnClientClick = selScript;
                }
                break;

            default:
                btnSelect.OnClientClick = selScript;
                break;
        }
    }


    /// <summary>
    /// Loads objects from DB and stores it to variables.
    /// </summary>
    private void LoadObjects(bool forceReload)
    {
        if (Object != null)
        {
            // Set return column name
            if (String.IsNullOrEmpty(ReturnColumnName))
            {
                ReturnColumnName = Object.IDColumn;
            }

            // Open selection dialog depending if UniSelector is on live site
            string url = null;
            if (IsLiveSite)
            {
                url = "~/CMSAdminControls/UI/UniSelector/LiveSelectionDialog.aspx";
            }
            else
            {
                url = "~/CMSAdminControls/UI/UniSelector/SelectionDialog.aspx";
            }

            if (!String.IsNullOrEmpty(SelectItemPageUrl))
            {
                url = SelectItemPageUrl;
            }

            url += "?SelectionMode=" + SelectionMode + "&hidElem=" + hiddenField.ClientID + "&params=" + ScriptHelper.GetString(Identificator, false) + "&clientId=" + ClientID + "&localize=" + (LocalizeItems ? 1 : 0) + AdditionalUrlParameters;

            // Create modal dialogs and datasets according to selection mode
            switch (SelectionMode)
            {
                // Single text box seleciton mode
                case SelectionModeEnum.SingleTextBox:
                    url += "&txtElem=" + txtSingleSelect.ClientID;
                    break;

                // Single drop down list selection mode
                case SelectionModeEnum.SingleDropDownList:
                    if (drpSingleSelect.Items.Count == 0)
                    {
                        forceReload = true;
                    }
                    result = GetResultSet(null, MaxDisplayedTotalItems + 1, 0, forceReload);

                    url += "&selectElem=" + hdnDialogSelect.ClientID;
                    break;

                // Multiple selection mode
                case SelectionModeEnum.Multiple:
                    {
                        uniGrid.GridName = GridName;
                        uniGrid.LoadGridDefinition();

                        // Set custom page according to settings to restrict size of data
                        if (ItemsPerPage > 0)
                        {
                            uniGrid.Pager.DefaultPageSize = ItemsPerPage;
                        }

                        // Ensure new current page number
                        if (mNewCurrentPage > 0)
                        {
                            uniGrid.Pager.UniPager.CurrentPage = mNewCurrentPage;
                        }

                        int currentOffset = (uniGrid.Pager.CurrentPage - 1) * uniGrid.Pager.CurrentPageSize;
                        int totalRecords = 0;
                        result = GetResultSet(null, 0, 0, forceReload, currentOffset, uniGrid.Pager.CurrentPageSize, ref totalRecords);

                        // If not first page and no data loaded load first page
                        if (DataHelper.DataSourceIsEmpty(result) && (currentOffset > 0))
                        {
                            // Set unigird page to 1 and reload data
                            uniGrid.Pager.UniPager.CurrentPage = 1;
                            result = GetResultSet(null, 0, 0, forceReload, 0, uniGrid.Pager.CurrentPageSize, ref totalRecords);
                        }

                        uniGrid.PagerForceNumberOfResults = totalRecords;

                        javaScript.Append("function US_RemoveAll_", ClientID, "(){ if (confirm(", ScriptHelper.GetString(GetString("general.confirmremoveall")), ")) {", Page.ClientScript.GetPostBackEventReference(this, "removeall"), "; return false; }} \n");
                    }
                    break;

                // Multiple text box seleciton mode
                case SelectionModeEnum.MultipleTextBox:
                    url += "&txtElem=" + txtSingleSelect.ClientID;
                    break;

                // Button selection
                case SelectionModeEnum.SingleButton:
                case SelectionModeEnum.MultipleButton:
                    break;

                default:
                    url = null;
                    result = null;
                    break;
            }

            // Selection dialog window
            if (url != null)
            {
                // Add IsSiteManager parameter to handle edit and new window                
                url += IsSiteManager ? "&siteManager=true" : String.Empty;
                // Add hash
                string hash = ValidationHelper.GetHashString(url.Substring(url.IndexOf('?')));
                url += "&hash=" + hash;

                javaScript.Append("function US_SelectionDialog_", ClientID, "(values) { ", Page.ClientScript.GetCallbackEventReference(this, "values", "US_SelectionDialogReady_" + ClientID, "'" + ResolveUrl(url) + "'"), "; } \n");
            }

            // Create selection changed function
            if (OnSelectionChangedAvailable())
            {
                javaScript.Append("function US_SelectionChanged_", ClientID, "() { ", Page.ClientScript.GetPostBackEventReference(this, "selectionchanged"), "; } \n");
            }

            // New item window
            if (!String.IsNullOrEmpty(NewItemPageUrl))
            {
                string newUrl = URLHelper.AddParameterToUrl(NewItemPageUrl, "selectorId", ClientID) + AdditionalUrlParameters;

                javaScript.Append("function US_NewItem_", ClientID, "(selectedItem) {{ var url = '", ResolveUrl(newUrl), "';modalDialog(url.replace(/##ITEMID##/i, selectedItem),'NewItem', ", EditDialogWindowWidth, ", ", EditDialogWindowHeight, "); }} \n");
            }

            // Edit item window
            if (!String.IsNullOrEmpty(EditItemPageUrl))
            {
                string newUrl = URLHelper.AddParameterToUrl(EditItemPageUrl, "selectorId", ClientID) + AdditionalUrlParameters;

                javaScript.Append(
@"
function US_EditItem_", ClientID, @"(selectedItem) {
    if (selectedItem == '') { 
        alert('", GetString(ResourcePrefix + ".pleaseselectitem|general.pleaseselectitem"), @"'); 
        return false; 
    }
    else if (selectedItem.indexOf('{%') >= 0) { 
        alert('", ResHelper.GetAPIString(ResourcePrefix + ".cannoteditmacro|general.cannoteditmacro", "The selected value contains a macro expression, the item cannot be edited from this context."), @"'); 
        return false; 
    }
    var url = '", ResolveUrl(newUrl), @"'; 
    modalDialog(url.replace(/##ITEMID##/i, selectedItem),'", EditWindowName, "', ", EditDialogWindowWidth, ", ", EditDialogWindowHeight, @"); 
}
"
                );
            }

            javaScript.Append(
                "function US_ReloadPage_", ClientID, "(){ ", Page.ClientScript.GetPostBackEventReference(this, "reload"), "; return false; } \n",
                "function US_SelectItems_", ClientID, "(items){ document.getElementById('", hiddenField.ClientID, "').value = items; ", Page.ClientScript.GetPostBackEventReference(this, "selectitems"), "; return false; } \n",
                "function US_SelectNewValue_", ClientID, "(selValue){ document.getElementById('", hiddenSelected.ClientID, "').value = selValue; ", Page.ClientScript.GetPostBackEventReference(this, "selectnewvalue"), "; return false; }\n");
        }
        else
        {
            lblStatus.Text = "[UniSelector]: Object type '" + ObjectType + "' not found.";
            StopProcessing = true;
        }
    }


    /// <summary>
    /// Sets the dialog parameters to the context.
    /// </summary>
    protected void SetDialogParameters(string values)
    {
        parameters["SelectionMode"] = SelectionMode;
        parameters["ResourcePrefix"] = ResourcePrefix;
        parameters["ObjectType"] = ObjectType;
        parameters["ReturnColumnName"] = ReturnColumnName;
        parameters["IconPath"] = IconPath;
        parameters["AllowEmpty"] = AllowEmpty;
        parameters["AllowAll"] = AllowAll;
        parameters["NoneRecordValue"] = NoneRecordValue;
        parameters["AllRecordValue"] = AllRecordValue;
        parameters["FilterControl"] = FilterControl;
        parameters["UseDefaultNameFilter"] = UseDefaultNameFilter;
        parameters["WhereCondition"] = WhereCondition;
        parameters["OrderBy"] = OrderBy;
        parameters["ItemsPerPage"] = ItemsPerPage;
        parameters["EmptyReplacement"] = EmptyReplacement;
        parameters["Values"] = ";" + values + ";";
        parameters["DisplayNameFormat"] = DisplayNameFormat;
        parameters["DialogGridName"] = DialogGridName;
        parameters["AdditionalColumns"] = AdditionalColumns;
        parameters["CallbackMethod"] = CallbackMethod;
        parameters["AllowEditTextBox"] = AllowEditTextBox;
        parameters["FireOnChanged"] = OnSelectionChangedAvailable();
        parameters["DisabledItems"] = DisabledItems;
        parameters["AddGlobalObjectSuffix"] = AddGlobalObjectSuffix;
        parameters["AddGlobalObjectNamePrefix"] = AddGlobalObjectNamePrefix;
        parameters["GlobalObjectSuffix"] = GlobalObjectSuffix;
        parameters["RemoveMultipleCommas"] = RemoveMultipleCommas;
        parameters["IsSiteManager"] = IsSiteManager;
        parameters["FilterMode"] = this.GetValue("FilterMode");

        WindowHelper.Add(Identificator, parameters);
    }


    /// <summary>
    /// Displays single selection textbox.
    /// </summary>
    private void DisplayTextBox()
    {
        plcTextBoxSelect.Visible = true;

        if (!AllowEmpty)
        {
            btnClear.Visible = false;
            lblClear.Visible = false;
        }

        if (AllowEditTextBox)
        {
            // Load the selected value
            txtSingleSelect.ReadOnly = false;
        }
        else
        {
            // Get the item
            string id = ValidationHelper.GetString(Value, null);
            if (!String.IsNullOrEmpty(id))
            {
                // Load textbox with data
                DataSet item = GetResultSet(id, 1, 0, true);
                if (!DataHelper.DataSourceIsEmpty(item))
                {
                    txtSingleSelect.Text = GetItemName(item.Tables[0].Rows[0]);
                }
            }
            else
            {
                txtSingleSelect.Text = "";
            }
        }
    }


    /// <summary>
    /// Displays single selection drop down list.
    /// </summary>
    private void DisplayDropDownList(bool forceReload)
    {
        hiddenField.Visible = false;

        plcDropdownSelect.Visible = true;

        object selectedValue = this.Value;

        if (!RequestHelper.IsPostBack() || forceReload || (drpSingleSelect.Items.Count == 0) || !String.IsNullOrEmpty(this.EnabledColumnName))
        {
            // Prepare controls and variables
            drpSingleSelect.Items.Clear();

            bool hasData = !DataHelper.DataSourceIsEmpty(result);

            // Load data to drop-down list
            if (hasData && (Object != null))
            {
                drpSingleSelect.Items.Clear();

                bool maxExceeded = (result.Tables[0].Rows.Count > MaxDisplayedTotalItems);

                // Populate the dropdownlist
                int index = 0;
                foreach (DataRow dr in result.Tables[0].Rows)
                {
                    drpSingleSelect.Items.Add(NewListItem(dr));

                    if (maxExceeded && (++index >= MaxDisplayedItems))
                    {
                        break;
                    }
                }

                // Check if all items were displayed or if '(more items)' item should be added
                if (maxExceeded)
                {
                    drpSingleSelect.Items.Add(new ListItem(GetString(ResourcePrefix + ".moreitems|general.moreitems"), US_MORE_RECORDS.ToString()));
                }
            }

            // Display special items
            if ((SpecialFields != null) && (SpecialFields.Length > 0))
            {
                for (int i = 0; i < SpecialFields.Length / 2; i++)
                {
                    string text = SpecialFields[i, 0];
                    string value = SpecialFields[i, 1];

                    // Insert only items that are not empty
                    if (!String.IsNullOrEmpty(text))
                    {
                        drpSingleSelect.Items.Insert(i, new ListItem(text, value));
                    }
                }
            }

            // Display '(none)' item
            if (AllowEmpty)
            {
                // Get item name
                string name = GetString(ResourcePrefix + ".empty|general.empty");

                drpSingleSelect.Items.Insert(0, new ListItem(name, this.NoneRecordValue));
            }

            // Display '(all)' item
            if (AllowAll)
            {
                // Get item name
                string name = GetString(ResourcePrefix + ".selectall|general.selectall");

                drpSingleSelect.Items.Insert(0, new ListItem(name, this.AllRecordValue));
            }

            // Load selected value to drop-down list
            if (!String.IsNullOrEmpty((string)selectedValue))
            {
                // Pre-select item from Value field
                string id = ValidationHelper.GetString(selectedValue, null);
                ListItem selectedItem = ControlsHelper.FindItemByValue(drpSingleSelect, id, false);

                // Select item which is already loaded in drop-down list
                if (selectedItem != null)
                {
                    drpSingleSelect.SelectedValue = selectedItem.Value;
                }
                // Select item which is not in drop-down list
                else
                {
                    // Find item by ID
                    DataSet item = GetResultSet(id, 1, 0, true);

                    if (!DataHelper.DataSourceIsEmpty(item))
                    {
                        ListItem newItem = NewListItem(item.Tables[0].Rows[0]);

                        // Add selected item to drop down list
                        if (!drpSingleSelect.Items.Contains(newItem))
                        {
                            drpSingleSelect.Items.Add(newItem);
                        }
                        drpSingleSelect.SelectedValue = newItem.Value;
                    }
                    else
                    {
                        try
                        {
                            drpSingleSelect.SelectedValue = id;
                        }
                        catch { }
                    }
                }
            }

            // New item link
            if (!String.IsNullOrEmpty(NewItemPageUrl))
            {
                drpSingleSelect.Items.Add(new ListItem(GetString(ResourcePrefix + ".newitem|general.newitem"), US_NEW_RECORD.ToString()));
            }

            // If no data in drop-down list, show none and disable
            if (drpSingleSelect.Items.Count == 0)
            {
                // Get item name
                string name = GetString(ResourcePrefix + ".empty|general.empty");

                drpSingleSelect.Items.Insert(0, new ListItem(name, this.NoneRecordValue));
                drpSingleSelect.Enabled = false;
            }
            else
            {
                drpSingleSelect.Enabled = Enabled;
            }

            HasData = hasData;
        }

        // Build onchange script
        string onChangeScript = "if (!US_ItemChanged(this, '" + ClientID + "')) return false;";
        if (!string.IsNullOrEmpty(OnBeforeClientChanged))
        {
            onChangeScript = OnBeforeClientChanged + onChangeScript;
        }
        if (!string.IsNullOrEmpty(OnAfterClientChanged))
        {
            onChangeScript += OnAfterClientChanged;
        }
        // Add open modal window JavaScript event
        drpSingleSelect.Attributes.Add("onchange", onChangeScript);

        // Enable / disable the edit button
        switch (drpSingleSelect.SelectedValue)
        {
            case "":
            case "0":
            case "-1":
            case "-2":
            case "-3":
                btnDropEdit.Enabled = false;
                break;

            default:
                btnDropEdit.Enabled = true;
                break;
        }
    }


    /// <summary>
    /// Creates a new list item based on the given DataRow.
    /// </summary>
    private ListItem NewListItem(DataRow dr)
    {
        string itemname = GetItemName(dr);
        if (this.LocalizeItems)
        {
            itemname = ResHelper.LocalizeString(itemname);
        }

        // Create new item
        ListItem newItem = new ListItem(itemname, dr[ReturnColumnName].ToString());

        RaiseOnListItemCreated(newItem);

        // Set disabled if disabled
        if (this.EnabledColumnName != null)
        {
            bool isEnabled = ValidationHelper.GetBoolean(dr[this.EnabledColumnName], false);
            if (isEnabled)
            {
                newItem.Attributes.Add("class", "DropDownItemEnabled");
            }
            else
            {
                newItem.Attributes.Add("class", "DropDownItemDisabled");
            }
        }

        return newItem;
    }


    /// <summary>
    /// Displays mulitple selection grid.
    /// </summary>
    private void DisplayMultiple(bool forceReload)
    {
        btnMenu.ContextMenuParent = plcContextMenu;
        pnlGrid.Visible = true;

        uniGrid.GridName = GridName;
        uniGrid.LoadGridDefinition();

        bool hasData = !DataHelper.DataSourceIsEmpty(result);

        // Load data to unigrid
        if (hasData || forceReload)
        {
            uniGrid.DataSource = result;
            if (!RequestHelper.IsPostBack())
            {
                uniGrid.Pager.DefaultPageSize = ItemsPerPage;
            }
            if (ItemsPerPage > 0)
            {
                uniGrid.Pager.PageSizeOptions = ItemsPerPage.ToString();
            }
            uniGrid.ReloadData();
        }

        // Display "No data" message
        if (!hasData)
        {
            lblStatus.Text = ZeroRowsText ?? GetString(ResourcePrefix + ".nodata|general.nodata");
        }
        else
        {
            lblStatus.Text = "";
        }

        btnRemoveSelected.Visible = hasData;
        lblRemoveSelected.Visible = btnRemoveSelected.Visible;

        btnMenu.Visible = hasData && !IsLiveSite;

        HasData = hasData;
    }


    /// <summary>
    /// Displays multiple selection textbox.
    /// </summary>
    private void DisplayMultipleTextBox()
    {
        plcTextBoxSelect.Visible = true;

        if (!AllowEmpty)
        {
            btnClear.Visible = false;
            lblClear.Visible = false;
        }

        // Setup the textbox
        if (AllowEditTextBox)
        {
            txtSingleSelect.ReadOnly = false;
        }
        else
        {
            txtSingleSelect.Text = ValidationHelper.GetString(Value, "").Trim(new char[] { ';', ' ' });
        }
    }


    /// <summary>
    /// Displays selection button.
    /// </summary>
    private void DisplayButton()
    {
        if (ButtonImage == null)
        {
            // Standard button
            plcButtonSelect.Visible = true;
        }
        else
        {
            // Link button
            plcImageSelect.Visible = true;
        }
    }


    /// <summary>
    /// Returns data set depending on specified properties.
    /// </summary>
    private DataSet GetResultSet(string id, int topN, int pageIndex, bool forceReload)
    {
        int totalRecords = 0;
        return GetResultSet(id, topN, pageIndex, forceReload, 0, 0, ref totalRecords);
    }


    /// <summary>
    /// Returns data set depending on specified properties.
    /// </summary>
    private DataSet GetResultSet(string id, int topN, int pageIndex, bool forceReload, int offset, int maxRecords, ref int totalRecords)
    {
        DataSet ds = null;

        // Init columns
        string columns = null;
        if (DisplayNameFormat == USER_DISPLAY_FORMAT)
        {
            // Ensure columns which are needed for USER_DISPLAY_FORMAT
            columns = "UserName;FullName;";
        }
        else if (DisplayNameFormat != null)
        {
            columns = DataHelper.GetNotEmpty(TextHelper.GetMacros(DisplayNameFormat), Object.DisplayNameColumn).Replace(";", ", ");
        }
        else
        {
            columns = Object.DisplayNameColumn;
        }

        // Add the default format name column to the query
        if (DefaultDisplayNameFormat != null)
        {
            string defaultColumn = DataHelper.GetNotEmpty(TextHelper.GetMacros(DefaultDisplayNameFormat), Object.DisplayNameColumn).Replace(";", ", ");
            columns = SqlHelperClass.MergeColumns(columns, defaultColumn);
        }

        // Add return column name
        columns = SqlHelperClass.MergeColumns(columns, ReturnColumnName);

        // Add additional columns
        columns = SqlHelperClass.MergeColumns(columns, AdditionalColumns);

        // Add enabled column name
        columns = SqlHelperClass.MergeColumns(columns, EnabledColumnName);

        // Ensure SiteID column (for global object prefixes/suffixes)
        if (this.AddGlobalObjectNamePrefix || this.AddGlobalObjectSuffix)
        {
            if ((objectType != null) && (objectType.SiteIDColumn != TypeInfo.COLUMN_NAME_UNKNOWN))
            {
                columns = SqlHelperClass.MergeColumns(columns, objectType.SiteIDColumn);
            }
        }

        // Return result set for single selectors
        string itemsWhere = null;


        // Prepare the parameters
        QueryDataParameters parameters = new QueryDataParameters();

        using (var condition = new SelectCondition(parameters))
        {
            if (SelectionMode != SelectionModeEnum.Multiple)
            {
                // Build where condition
                if (!String.IsNullOrEmpty(id))
                {
                    itemsWhere = ReturnColumnName + " = '" + SqlHelperClass.GetSafeQueryString(id, false) + "'";
                }
            }
            // Return result set for multiple selection
            else
            {
                // Get where condition for selected items
                string[] items = hiddenField.Value.Split(ValuesSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                bool isString = !Object.IDColumn.Equals(ReturnColumnName, StringComparison.InvariantCultureIgnoreCase);
                if (isString)
                {
                    // Names
                    condition.Prepare(ReturnColumnName, items, false);
                }
                else
                {
                    // IDs
                    condition.PrepareIDs(ReturnColumnName, items);
                }

                // Do not return anything if the selection is empty
                if (condition.IsEmpty)
                {
                    return null;
                }

                itemsWhere = condition.WhereCondition;
            }

            // Modify WHERE condition
            string where = itemsWhere;

            // Apply value restrictions
            if (ApplyValueRestrictions)
            {
                where = SqlHelperClass.AddWhereCondition(where, (ListingWhereCondition != String.Empty) ? ListingWhereCondition : WhereCondition);
            }

            // Order by
            string orderBy = OrderBy;
            if (String.IsNullOrEmpty(orderBy))
            {
                orderBy = Object.DisplayNameColumn;
            }

            GeneralizedInfo obj = (ListingObject == null) ? Object : ListingObject;

            // Get the result set
            ds = obj.GetData(parameters, where, orderBy, topN, columns, false, offset, maxRecords, ref totalRecords);
        }

        return ds;
    }


    /// <summary>
    /// Registers scripts.
    /// </summary>
    private void RegisterScripts()
    {
        // Register JavaScript
        ScriptHelper.RegisterDialogScript(Page);

        javaScript.Append(
@"
function US_SelectionDialogReady_", ClientID, @"(rvalue, context) 
{ 
    modalDialog(context + ((rvalue != '') ? '&selectedvalue=' + rvalue : ''), '", DialogWindowName, "', ", DialogWindowWidth.ToString(), ", ", DialogWindowHeight.ToString(), @", null, null, true); 
    return false;
}");

        // Open dialog script
        ScriptHelper.RegisterScriptFile(this.Page, "uniselector.js");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UniSelectorReady" + ClientID, ScriptHelper.GetScript(javaScript.ToString()));

        if (this.SelectionMode == SelectionModeEnum.SingleDropDownList)
        {
            // DDL initialization
            ScriptHelper.RegisterStartupScript(this, typeof(string), "UniSelector_" + ClientID, ScriptHelper.GetScript("US_InitDropDown(document.getElementById('" + drpSingleSelect.ClientID + "'))"));
        }
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Drop-down list event handler.
    /// </summary>
    void drpSingleSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Only raise selected index changed when other than (more items...) is selected
        if (drpSingleSelect.SelectedValue != US_MORE_RECORDS.ToString())
        {
            RaiseSelectionChanged();
        }
    }


    /// <summary>
    /// Unigrid external databound handler.
    /// </summary>
    object uniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string val = String.Empty;

        switch (sourceName.ToLowerInvariant())
        {
            case "yesno":
                val = UniGridFunctions.ColoredSpanYesNo(parameter);
                break;

            case "select":
                {
                    // Get item ID
                    DataRowView drv = (parameter as DataRowView);
                    string itemID = drv[ReturnColumnName].ToString();

                    // Keep the check status if checked
                    bool isChecked = mPageChanged && (hiddenSelected.Value.IndexOf(ValuesSeparator + itemID + ValuesSeparator, StringComparison.InvariantCultureIgnoreCase) >= 0);

                    val = "<input id=\"chk" + checkBoxClass + "_" + itemID + "\" type=\"checkbox\" onclick=\"US_ProcessItem('" + ClientID + "', '" + ValuesSeparator + "', this);\" class=\"chk" + checkBoxClass + "\"" + (isChecked ? "checked=\"checked\" " : "") + (Enabled ? "" : "disabled=\"disabled\"") + " />";
                    break;
                }

            case "itemname":
                {
                    DataRowView drv = (parameter as DataRowView);

                    // Get item ID
                    string itemID = drv[ReturnColumnName].ToString();

                    // Get item name
                    string itemName = GetItemName(drv.Row);

                    if (Enabled)
                    {
                        val = "<div class=\"SelectableItem\" onclick=\"US_ProcessItem('" + ClientID + "', '" + ValuesSeparator + "', document.getElementById('chk" + checkBoxClass + "_" + ScriptHelper.GetString(itemID).Trim('\'') + "'), true); return false;\">" + HTMLHelper.HTMLEncode(TextHelper.LimitLength(itemName, 100)) + "</div>";
                    }
                    else
                    {
                        val = "<div>" + HTMLHelper.HTMLEncode(TextHelper.LimitLength(itemName, 100)) + "</div>";
                    }

                    break;
                }
        }

        if (OnAdditionalDataBound != null)
        {
            val = ValidationHelper.GetString(OnAdditionalDataBound(this, sourceName, parameter, val), String.Empty);
        }

        return val;
    }


    /// <summary>
    /// Returns item display name based on DisplayNameFormat.
    /// </summary>
    /// <param name="dr">Source data row</param>    
    private string GetItemName(DataRow dr)
    {
        string itemName = null;

        // Special formatted user name
        if (DisplayNameFormat == USER_DISPLAY_FORMAT)
        {
            string userName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "UserName"), String.Empty);
            string fullName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "FullName"), String.Empty);

            itemName = Functions.GetFormattedUserName(userName, fullName, this.IsLiveSite);
        }
        else if (DisplayNameFormat == null)
        {
            itemName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, Object.DisplayNameColumn), String.Empty);
        }
        else
        {
            itemName = th.MergeText(DisplayNameFormat, dr);
        }

        // Use the DefaultDisplayNameFormat if the resolved DisplayNameFormat is empty
        if (String.IsNullOrEmpty(itemName) && (!String.IsNullOrEmpty(DefaultDisplayNameFormat)))
        {
            itemName = th.MergeText(DefaultDisplayNameFormat, dr);
        }

        // Add items to unigrid
        if (String.IsNullOrEmpty(itemName))
        {
            itemName = EmptyReplacement;
        }

        if (this.RemoveMultipleCommas)
        {
            itemName = TextHelper.RemoveMultipleCommas(itemName);
        }

        if (this.AddGlobalObjectSuffix)
        {
            if ((objectType != null) && (objectType.SiteIDColumn != TypeInfo.COLUMN_NAME_UNKNOWN))
            {
                itemName += (ValidationHelper.GetInteger(DataHelper.GetDataRowValue(dr, objectType.SiteIDColumn), 0) > 0 ? "" : " " + this.GlobalObjectSuffix); ;
            }
        }

        return itemName;
    }


    /// <summary>
    /// Unigrid page index changed handler.
    /// </summary>
    protected void uniGrid_OnPageChanged(object sender, EventArgs e)
    {
        mNewCurrentPage = uniGrid.Pager.CurrentPage;
    }


    /// <summary>
    /// Button clear click.
    /// </summary>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Value = null;
        Reload(true);
    }


    /// <summary>
    /// Button "Remove selected items" click handler.
    /// </summary>
    protected void btnRemoveSelected_Click(object sender, EventArgs e)
    {
        // Unselect selected items
        if (!String.IsNullOrEmpty(hiddenSelected.Value))
        {
            hiddenField.Value = DataHelper.GetNewItemsInList(hiddenSelected.Value, hiddenField.Value, ValuesSeparator[0]);

            Reload(true);

            RaiseSelectionChanged();
        }
    }


    /// <summary>
    /// Removes all selected items.
    /// </summary>
    protected void RemoveAll()
    {
        // Unselect selected items
        if (!String.IsNullOrEmpty(hiddenField.Value))
        {
            hiddenField.Value = "";

            Reload(true);

            RaiseSelectionChanged();
        }
    }


    /// <summary>
    /// Overriden set value to collect parameters.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    /// <param name="value">Value</param>
    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        // Set parameters for dialog
        parameters[propertyName] = value;
    }


    /// <summary>
    /// Overriden to get the parameters.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    public override object GetValue(string propertyName)
    {
        if (parameters.Contains(propertyName))
        {
            return parameters[propertyName];
        }

        return base.GetValue(propertyName);
    }

    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Hanling of the postback event.
    /// </summary>
    /// <param name="eventArgument">Event argument (selected value)</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        switch (eventArgument)
        {
            case "removeall":
                // Remove all items
                RemoveAll();
                break;

            case "selectitems":
                // Raise items selected event
                RaiseOnItemsSelected();
                break;

            case "reload":
                // Reload the data
                Reload(true);

                RaiseSelectionChanged();
                break;

            case "selectionchanged":
                // Raise selection changed event
                RaiseSelectionChanged();
                break;

            case "selectnewvalue":
                // Select new item
                switch (SelectionMode)
                {
                    // Single textbox mode
                    case SelectionModeEnum.SingleTextBox:
                        txtSingleSelect.Text = hiddenSelected.Value;
                        break;

                    // Single dropdown list
                    case SelectionModeEnum.SingleDropDownList:

                        // Reload data and select new value
                        Reload(true);
                        ListItem selectedItem = ControlsHelper.FindItemByValue(drpSingleSelect, hiddenSelected.Value, false);

                        if (selectedItem != null)
                        {
                            drpSingleSelect.SelectedValue = selectedItem.Value;
                            btnDropEdit.Enabled = true;
                            drpSingleSelect.Enabled = true;

                        }
                        break;
                }

                hiddenSelected.Value = "";
                break;
        }
    }

    #endregion


    #region "ICallbackEventHandler Members"

    string ICallbackEventHandler.GetCallbackResult()
    {
        // Prepare the parameters for dialog
        SetDialogParameters((string)Value);

        return "";
    }


    void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
    {
        // Adopt new value from callback
        if ((eventArgument != null) && eventArgument.StartsWith("$|"))
        {
            Value = eventArgument.Substring(2);
        }
    }

    #endregion
}
