using System;
using System.Text;
using System.Web.UI.WebControls;

using CMS.DataEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_AdminControls_Controls_Class_QueryEdit : CMSUserControl
{
    #region "Variables"

    private int mQueryID;


    private int mClassID;


    private bool mShowHelp = true;


    private string mClassName;


    private string mQueryName;


    private Query mQuery;


    private string mRefreshPageURL;


    private CMSMasterPage mCurrentMaster;

    #endregion


    #region "Properties"

    /// <summary>
    /// ID of the current query.
    /// </summary>
    public int QueryID
    {
        get
        {
            return mQueryID;
        }
        set
        {
            mQueryID = value;
        }
    }


    /// <summary>
    /// ID of the class current query belongs to.
    /// </summary>
    public int ClassID
    {
        get
        {
            return mClassID;
        }
        set
        {
            mClassID = value;
        }
    }


    /// <summary>
    /// Indicates whether the help should be displayed in the bottom of the page.
    /// </summary>
    public bool ShowHelp
    {
        get
        {
            return mShowHelp;
        }
        set
        {
            mShowHelp = value;
        }
    }


    /// <summary>
    /// Page that should be refreshed after button click.
    /// </summary>
    public string RefreshPageURL
    {
        get
        {
            return mRefreshPageURL;
        }
        set
        {
            mRefreshPageURL = value;
        }
    }


    /// <summary>
    /// If true, control is used in site manager.
    /// </summary>
    public bool IsSiteManager
    {
        get
        {
            return filter.IsSiteManager;
        }
        set
        {
            filter.IsSiteManager = value;
        }
    }


    /// <summary>
    /// Gets or sets whether the control is in dialog mode.
    /// </summary>
    public bool DialogMode
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets whether the contorl is in edit mode.
    /// </summary>
    public bool EditMode
    {
        get;
        set;
    }


    /// <summary>
    /// Query to edit.
    /// </summary>
    private Query Query
    {
        get
        {
            return mQuery;
        }
        set
        {
            mQuery = value;
        }
    }


    /// <summary>
    /// Name of the class query belongs to.
    /// </summary>
    private string ClassName
    {
        get
        {
            return mClassName;
        }
        set
        {
            mClassName = value;
        }
    }


    /// <summary>
    /// Name of the actual query.
    /// </summary>
    private string QueryName
    {
        get
        {
            return mQueryName;
        }
        set
        {
            mQueryName = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Init(object sender, EventArgs e)
    {
        mCurrentMaster = Page.Master as CMSMasterPage;

        if (mCurrentMaster == null)
        {
            throw new Exception("Page using this control must have CMSMasterPage master page.");
        }

        // Create and add help control to dialog's header if not in tab mode
        if (!mCurrentMaster.TabMode)
        {
            mCurrentMaster.Title.HelpTopicName = "newedit_query";
            mCurrentMaster.Title.HelpName = "helpTopic";
        }

        // Header actions must be initilized here, otherwise it might be too late in the page lifecycle
        InitHeaderActions();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize the control
        SetupControl();

        if (!RequestHelper.IsPostBack())
        {
            // Shows that the query was created or updated successfully
            if (QueryHelper.GetInteger("saved", 0) == 1)
            {
                ShowInfo(GetString("General.ChangesSaved"));
            }
        }

        // Set correct help title for custom table
        if (!String.IsNullOrEmpty(ClassName))
        {
            DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(ClassName);
            if (classInfo.ClassIsCustomTable)
            {
                mCurrentMaster.Title.HelpTopicName = "customtable_edit_newedit_query";
            }
        }
    }


    /// <summary>
    /// Generate default query.
    /// </summary>
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        rblQueryType.SelectedIndex = 0;
        try
        {
            txtQueryText.Text = SqlGenerator.GetSqlQuery(ClassName, SqlGenerator.GetQueryType(QueryName), null);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }


    public bool Save(bool closeOnSave)
    {
        // Validate query name for emptiness and code name format
        string result = new Validator()
            .NotEmpty(txtQueryName.Text.Trim(), GetString("queryedit.erroremptyqueryname"))
            .IsIdentificator(txtQueryName.Text.Trim(), GetString("queryedit.querynameformat"))
            .Result;

        // If the entries were succesfully validated
        if (result != string.Empty)
        {
            ShowError(result);
            return false;
        }

        bool isSaved = false;

        if (filter.Visible)
        {
            ClassID = filter.ClassId;
            ClassName = DataClassInfoProvider.GetClassName(filter.ClassId);
        }

        // Gets full query name in the form - "ClassName.QueryName"
        string fullQueryName = !string.IsNullOrEmpty(ClassName) ?
            string.Concat(ClassName, ".", txtQueryName.Text) : txtQueryName.Text;

        // Finds whether new or edited query is unique in the document type
        Query newQuery = QueryProvider.GetQueryFromDB(fullQueryName);

        // If query with specified name already exist
        if (newQuery != null)
        {
            // Check if it is the same query as one being edited
            if (newQuery.QueryId != QueryID)
            {
                // The new query tries to use the same code name
                ShowError(GetString("queryedit.errorexistingqueryid"));
                return false;
            }

            Query = newQuery;
            isSaved = SaveQuery();
        }
        else
        {
            // Query with specified name doesn't exist yet            
            isSaved = SaveQuery();
        }

        // If the query was successfully saved
        RedirectOnSave(isSaved, closeOnSave);

        if (isSaved)
        {
            ShowInfo(GetString("General.ChangesSaved"));
        }

        return true;
    }


    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        mQueryName = GetString("queryedit.newquery");

        // If the existing query is being edited
        if (QueryID > 0)
        {
            // Get information on existing query
            Query = QueryProvider.GetQuery(QueryID);
            if (Query != null)
            {
                ClassName = DataClassInfoProvider.GetClassName(Query.QueryClassId);
                QueryName = Query.QueryName;

                if (!RequestHelper.IsPostBack())
                {
                    // Fills form with the existing query information
                    LoadValues();
                    txtQueryText.Focus();
                }
            }
        }
        // New query is being created
        else if (ClassID > 0)
        {
            ClassName = DataClassInfoProvider.GetClassName(ClassID);
            txtQueryName.Focus();
        }

        plcLoadGeneration.Visible = SettingsKeyProvider.DevelopmentMode;

        // Ensure generate button and custom checkbox for default queries
        if (SqlGenerator.IsSystemQuery(QueryName))
        {
            btnGenerate.Visible = true;
            plcIsCustom.Visible = true;
        }

        // Initialize the validator
        RequiredFieldValidatorQueryName.ErrorMessage = GetString("queryedit.erroremptyqueryname");

        if (ShowHelp)
        {
            DisplayHelperTable();
        }

        // Filter available only when creating new query in dialog mode
        plcDocTypeFilter.Visible = filter.Visible = DialogMode && !EditMode;

        // Set filter preselection 
        if (plcDocTypeFilter.Visible)
        {
            filter.SelectedValue = QueryHelper.GetString("selectedvalue", null);
            filter.FilterMode = SettingsObjectType.QUERY;
        }

        // Hide header actions when creating new query in dialog mode                       
        mCurrentMaster.HeaderActions.Parent.Visible = (!DialogMode || EditMode) && Visible;

        txtQueryName.ReadOnly = DialogMode && EditMode;

        // Set dialog's content panel css class
        if (DialogMode)
        {
            if (EditMode)
            {
                mCurrentMaster.PanelContent.CssClass = "PageContent";
            }
            else
            {
                pnlContainer.CssClass = "PageContent";
            }
        }
    }


    /// <summary>
    /// Displays helper table with transformation examples.
    /// </summary>
    private void DisplayHelperTable()
    {
        // Add header row
        TableRow headerRow = new TableRow() { TableSection = TableRowSection.TableHeader, CssClass = "UniGridHead" };
        headerRow.Cells.Add(new TableCell() { Text = GetString("queryedit.helpcaption"), Wrap = false });
        headerRow.Cells.Add(new TableCell() { Text = GetString("queryedit.helpdescription"), Width = Unit.Percentage(100) });
        tblHelp.Rows.Add(headerRow);

        // Add macro rows
        string[,] rows = 
        {
            {"##WHERE##",  GetString("queryedit.help_where")},
            {"##TOPN##",   GetString("queryedit.help_topn")},
            {"##ORDERBY##", GetString("queryedit.help_orderby")},
            {"##COLUMNS##", GetString("queryedit.help_columns")}
        };

        TableRow macroRow;
        for (int i = 0; i <= rows.GetUpperBound(0); i++)
        {
            macroRow = new TableRow() { CssClass = ((i % 2) == 0) ? "EvenRow" : "OddRow" };
            macroRow.Cells.Add(new TableCell() { Text = rows[i, 0], Wrap = false });
            macroRow.Cells.Add(new TableCell() { Text = rows[i, 1] });
            tblHelp.Rows.Add(macroRow);
        }

        // Make help table visible on click only
        tblHelp.Attributes["style"] = "display: none;";
    }


    /// <summary>
    /// Loads values from query.
    /// </summary>
    private void LoadValues()
    {
        txtQueryName.Text = QueryName;
        rblQueryType.SelectedValue = Query.QueryType.ToString();
        chbTransaction.Checked = Query.RequiresTransaction;
        txtQueryText.Text = Query.QueryText;
        drpGeneration.Value = Query.QueryLoadGeneration;
        chckIsCustom.Checked = Query.QueryIsCustom || Query.QueryIsLocked;
    }


    /// <summary>
    /// Saves new or edited query of the given name and returns to the query list.
    /// </summary>
    /// <returns>True if query was succesfully saved</returns>
    private bool SaveQuery()
    {
        // The query ID was specified, edit existing
        bool existing = (QueryID > 0);
        Query newQuery = existing ? QueryProvider.GetQuery(QueryID) : new Query();

        // Sets query object's properties
        newQuery.QueryName = txtQueryName.Text;
        DataClassInfo dci = DataClassInfoProvider.GetDataClass(ClassName);
        if (dci != null)
        {
            newQuery.QueryClassId = dci.ClassID;
        }
        else
        {
            ShowError(GetString("editedobject.notexists"));
            return false;
        }

        // Check the query type
        newQuery.QueryType = rblQueryType.SelectedValue == "SQLQuery" ? QueryTypeEnum.SQLQuery : QueryTypeEnum.StoredProcedure;

        newQuery.RequiresTransaction = chbTransaction.Checked;
        // Non-system queries for classes other than document types, custom tables are always set to NOT custom
        if (!SqlGenerator.IsSystemQuery(QueryName) && !dci.ClassIsDocumentType && !dci.ClassIsCustomTable && !dci.ClassShowAsSystemTable)
        {
            newQuery.QueryIsCustom = false;
            newQuery.QueryIsLocked = false;
        }
        else
        {
            newQuery.QueryIsCustom = chckIsCustom.Checked;
            newQuery.QueryIsLocked = chckIsCustom.Checked;
        }
        newQuery.QueryText = txtQueryText.Text.TrimEnd('\r', '\n');
        newQuery.QueryLoadGeneration = drpGeneration.Value;

        // Insert new / update existing query
        QueryProvider.SetQuery(newQuery);

        Query = newQuery;

        return true;
    }


    private void RedirectOnSave(bool isSaved, bool closeOnSave)
    {
        if (RefreshPageURL == null || !isSaved)
        {
            return;
        }

        if (DialogMode)
        {
            // Check for selector ID
            string selector = QueryHelper.GetString("selectorid", string.Empty);
            if (!string.IsNullOrEmpty(selector))
            {
                // Add selector refresh
                StringBuilder script = new StringBuilder();
                if (!EditMode)
                {
                    script.AppendFormat(@"
                        var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments;
                        if (wopener) {{                        
                            wopener.US_SelectNewValue_{0}('{1}');                        
		                }}",
                        selector, Query.QueryFullName);
                }

                script.AppendFormat(@"
                        window.name = '{2}';
                        window.open('{0}?name={1}&saved=1&editonlycode=true&selectorid={2}&tabmode={3}', window.name);
                        ",
                        URLHelper.ResolveUrl(RefreshPageURL), Query.QueryFullName, selector, QueryHelper.GetInteger("tabmode", 0));

                if (closeOnSave)
                {
                    script.AppendLine("window.top.close();");
                }

                ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script.ToString(), true);
            }
        }
        else
        {
            string redirectUrl = URLHelper.AddParameterToUrl(RefreshPageURL, "saved", "1");
            redirectUrl = URLHelper.AddParameterToUrl(redirectUrl, "queryid", Query.QueryId.ToString());
            redirectUrl = URLHelper.AddParameterToUrl(redirectUrl, "tabmode", QueryHelper.GetInteger("tabmode", 0).ToString());
            URLHelper.Redirect(redirectUrl);
        }
    }


    private void InitHeaderActions()
    {
        string[,] actions = new string[1, 12];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        mCurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        mCurrentMaster.HeaderActions.ActionPerformed += (sender, e) =>
        {
            if (e.CommandName == "save")
            {
                Save(false);
            }
        };
        mCurrentMaster.HeaderActions.Actions = actions;
    }


    private void ShowError(string message)
    {
        lblError.Visible = true;
        lblError.Text = message;
    }


    private void ShowInfo(string message)
    {
        lblInfo.Visible = true;
        lblInfo.Text = message;
    }

    #endregion
}