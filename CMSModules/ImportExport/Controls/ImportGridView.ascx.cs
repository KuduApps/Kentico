using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.Controls;

public partial class CMSModules_ImportExport_Controls_ImportGridView : CMSUserControl, IUniPageable
{
    #region "Variables"

    // Indicates if object selection is enabled
    protected bool selectionEnabled = true;

    protected string codeNameColumnName = "";
    protected string displayNameColumnName = "";

    protected string preposition = "chck_";

    private SiteImportSettings mSettings = null;

    protected ArrayList mExistingObjects = null;

    private StringBuilder sbAvailable = new StringBuilder();

    #endregion


    #region "Public properties"

    /// <summary>
    /// Import settings.
    /// </summary>
    public SiteImportSettings Settings
    {
        get
        {
            return mSettings;
        }
        set
        {
            mSettings = value;
        }
    }


    /// <summary>
    /// Existing objects in the database.
    /// </summary>
    public ArrayList ExistingObjects
    {
        get
        {
            if (mExistingObjects == null)
            {
                mExistingObjects = new ArrayList();

                // Get the existing objects from database
                DataSet ds = ImportProvider.GetExistingObjects(Settings, ObjectType, SiteObject, true);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    // Get info object
                    GeneralizedInfo infoObj = CMSObjectHelper.GetReadOnlyObject(ObjectType);
                    if (infoObj == null)
                    {
                        throw new Exception("[ImportGridView]: Object type '" + ObjectType + "' not found.");
                    }

                    int colIndex = ds.Tables[0].Columns.IndexOf(infoObj.CodeNameColumn);
                    if (colIndex >= 0)
                    {
                        // For each object get codename
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string codeName = ValidationHelper.GetString(dr[colIndex], null);

                            if (codeName != null)
                            {
                                mExistingObjects.Add(codeName.ToLower());
                            }
                        }
                    }
                }
            }

            return mExistingObjects;
        }
    }


    /// <summary>
    /// Current object type.
    /// </summary>
    public string ObjectType
    {
        get
        {
            return ValidationHelper.GetString(ViewState["ObjectType"], null);
        }
        set
        {
            ViewState["ObjectType"] = value;
        }
    }


    /// <summary>
    /// True if site object.
    /// </summary>
    public bool SiteObject
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["SiteObject"], false);
        }
        set
        {
            ViewState["SiteObject"] = value;
        }
    }


    /// <summary>
    /// Data source.
    /// </summary>
    public DataSet DataSource
    {
        get;
        set;
    }


    /// <summary>
    /// Pager control.
    /// </summary>
    public UniGridPager PagerControl
    {
        get
        {
            return pagerElem;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        pagerElem.PagedControl = this;

        if (RequestHelper.IsPostBack())
        {
            if (this.Settings != null)
            {
                // Process the results of the available tasks
                string[] available = this.hdnAvailableItems.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (available != null)
                {
                    foreach (string codeName in available)
                    {
                        string name = GetCheckBoxName(codeName);

                        if (Request.Form[name] == null)
                        {
                            // Unchecked
                            this.Settings.Deselect(ObjectType, codeName, SiteObject);
                        }
                        else
                        {
                            // Checked
                            this.Settings.Select(ObjectType, codeName, SiteObject);
                        }
                    }
                }
            }
        }
    }
    

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Render the available task IDs
        if (sbAvailable != null)
        {
            this.hdnAvailableItems.Value = sbAvailable.ToString();
        }
        else
        {
            this.hdnAvailableItems.Value = "";
        }
    }


    /// <summary>
    /// Returns the ID for the checkbox.
    /// </summary>
    /// <param name="codeName">Object code name</param>
    protected string GetCheckBoxId(object codeName)
    {
        if (sbAvailable == null)
        {
            sbAvailable = new StringBuilder();
        }
        else
        {
            sbAvailable.Append(";");
        }
        sbAvailable.Append(codeName);

        return preposition + ValidationHelper.GetIdentifier(codeName).ToLower().Replace("'", "\'"); ;
    }


    /// <summary>
    /// Returns the name for the checkbox.
    /// </summary>
    /// <param name="codeName">Object code name</param>
    protected string GetCheckBoxName(object codeName)
    {
        return preposition + ValidationHelper.GetIdentifier(codeName).ToLower().Replace("'", "\'");
    }
    

    /// <summary>
    /// Bind the data.
    /// </summary>
    public void Bind()
    {
        if (!string.IsNullOrEmpty(ObjectType))
        {
            pnlGrid.Visible = true;
            selectionEnabled = ((ObjectType != LicenseObjectType.LICENSEKEY) || !Settings.IsOlderVersion);

            if (selectionEnabled)
            {
                // Initilaize strings
                btnAll.Text = GetString("ImportExport.All");
                btnNone.Text = GetString("export.none");
                btnDefault.Text = GetString("General.Default");
            }

            pnlLinks.Visible = selectionEnabled;

            // Get object info
            GeneralizedInfo info = CMSObjectHelper.GetReadOnlyObject(ObjectType);
            if (info != null)
            {
                plcGrid.Visible = true;
                codeNameColumnName = info.CodeNameColumn;
                displayNameColumnName = info.DisplayNameColumn;

                // Get data source
                DataSet ds = DataSource;

                DataTable table = null;
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    // Get the table
                    string tableName = CMSObjectHelper.GetTableName(info);
                    table = ds.Tables[tableName];

                    // Set correct ID for direct page contol
                    pagerElem.DirectPageControlID = ((float)table.Rows.Count / pagerElem.CurrentPageSize > 20.0f) ? "txtPage" : "drpPage";

                    // Sort data
                    if (!DataHelper.DataSourceIsEmpty(table))
                    {
                        string orderBy = GetOrderByExpression(info);
                        table.DefaultView.Sort = orderBy;
                    }
                }

                // Prepare checkbox column
                TemplateField checkBoxField = (TemplateField)gvObjects.Columns[0];
                checkBoxField.HeaderText = GetString("General.Import");

                // Prepare name field
                TemplateField nameField = (TemplateField)gvObjects.Columns[1];
                nameField.HeaderText = GetString("general.displayname");

                if (!DataHelper.DataSourceIsEmpty(table))
                {
                    plcObjects.Visible = true;
                    lblNoData.Visible = false;
                    gvObjects.DataSource = table;

                    // Call page binding event
                    if (OnPageBinding != null)
                    {
                        OnPageBinding(this, null);
                    }

                    PagedDataSource pagedDS = gvObjects.DataSource as PagedDataSource;
                    if (pagedDS != null)
                    {
                        if (pagedDS.PageSize <= 0)
                        {
                            gvObjects.DataSource = table;
                        }
                    }

                    gvObjects.DataBind();
                }
                else
                {
                    plcObjects.Visible = false;
                    lblNoData.Visible = true;
                    lblNoData.Text = String.Format(GetString("ImportGridView.NoData"), GetString("objecttype." + ObjectType.Replace(".", "_").Replace("#", "_")));
                }
            }
            else
            {
                plcGrid.Visible = false;
            }

            // Disable license selection
            bool enable = !((ObjectType == LicenseObjectType.LICENSEKEY) && Settings.IsOlderVersion);
            gvObjects.Enabled = enable;
            pnlLinks.Enabled = enable;
            lblInfo.Text = enable ? GetString("ImportGridView.Info") : GetString("ImportGridView.Disabled");
        }
        else
        {
            pnlGrid.Visible = false;
            gvObjects.DataSource = null;
            gvObjects.DataBind();
        }
    }


    // Get orderby expression
    private static string GetOrderByExpression(GeneralizedInfo info)
    {
        switch (info.ObjectType)
        {
            case PortalObjectType.PAGETEMPLATE:
                return "PageTemplateIsReusable DESC," + info.DisplayNameColumn;

            default:
                {
                    if (info.DisplayNameColumn != TypeInfo.COLUMN_NAME_UNKNOWN)
                    {
                        return info.DisplayNameColumn;
                    }
                    else
                    {
                        return info.CodeNameColumn;
                    }
                }
        }
    }


    /// <summary>
    /// Ensure objects preselection and genearate appropriate code snippet.
    /// </summary>
    /// <param name="codeName">Code name</param>
    protected string IsChecked(object codeName)
    {
        string value = "";
        string name = ValidationHelper.GetString(codeName, "");
        if (Settings.IsSelected(ObjectType, name, SiteObject))
        {
            value += " checked=\"checked\" ";
        }

        if (!selectionEnabled)
        {
            value += " disabled=\"disabled\" ";
        }

        return value;
    }


    /// <summary>
    /// Check if object is in conflict in database and genearate appropriate code snippet.
    /// </summary>
    /// <param name="codeName">Code name</param>
    protected string IsInConflict(object codeName)
    {
        string name = ValidationHelper.GetString(codeName, "").ToLower();
        if ((ExistingObjects != null) && (ExistingObjects.Contains(name)))
        {
            return "*";
        }
        return "";
    }


    protected string GetName(object codeNameObj, object displayNameObj)
    {
        string codeName = ValidationHelper.GetString(codeNameObj, "");
        string displayName = ValidationHelper.GetString(displayNameObj, "");

        if (string.IsNullOrEmpty(displayName))
        {
            return codeName;
        }
        return ResHelper.LocalizeString(displayName);
    }


    protected void btnAll_Click(object sender, EventArgs e)
    {
        // Load all selection
        this.Settings.LoadDefaultSelection(ObjectType, SiteObject, ImportTypeEnum.All, true, false);
    }


    protected void btnNone_Click(object sender, EventArgs e)
    {
        // Load none selection
        this.Settings.LoadDefaultSelection(ObjectType, SiteObject, ImportTypeEnum.None, true, false);
    }


    protected void btnDefault_Click(object sender, EventArgs e)
    {
        // Load default selection
        ImportTypeEnum importType = ImportTypeEnum.Default;
        if (this.Settings.IsWebTemplate)
        {
            if (SiteInfoProvider.GetSitesCount() == 0)
            {
                // No site exists, overwrite all
                importType = ImportTypeEnum.All;
            }
            else
            {
                // Some site exists, only new objects
                importType = ImportTypeEnum.New;
            }
        }

        this.Settings.LoadDefaultSelection(ObjectType, SiteObject, importType, true, false);
    }


    #region "IUniPageable Members"

    /// <summary>
    /// Pager data item.
    /// </summary>
    public object PagerDataItem
    {
        get
        {
            return this.gvObjects.DataSource;
        }
        set
        {
            this.gvObjects.DataSource = value;
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


    public int PagerForceNumberOfResults
    {
        get
        {
            return -1;
        }
        set
        {
        }
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
    }

    #endregion
}
