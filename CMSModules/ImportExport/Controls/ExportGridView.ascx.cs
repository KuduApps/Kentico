using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.Controls;

public partial class CMSModules_ImportExport_Controls_ExportGridView : CMSUserControl, IUniPageable
{
    #region "Variables"

    protected string codeNameColumnName = "";
    protected string displayNameColumnName = "";

    protected string preposition = "echck_";

    private SiteExportSettings mSettings = null;

    protected int pagerForceNumberOfResults = -1;

    private StringBuilder sbAvailable = new StringBuilder();

    #endregion


    #region "Public properties"

    /// <summary>
    /// Export settings.
    /// </summary>
    public SiteExportSettings Settings
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
    /// Current object type.
    /// </summary>
    public string ObjectType
    {
        get
        {
            return ValidationHelper.GetString(ViewState["ObjectType"], CMSObjectHelper.GROUP_OBJECTS);
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
    /// Gets current page size from pager.
    /// </summary>
    protected int CurrentPageSize
    {
        get
        {
            return pagerElem.CurrentPageSize;
        }
    }


    /// <summary>
    /// Gets current offset.
    /// </summary>
    protected int CurrentOffset
    {
        get
        {
            return CurrentPageSize * (pagerElem.CurrentPage - 1);
        }
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
            if (Settings != null)
            {
                // Process the results of the available tasks
                string[] available = hdnAvailableItems.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (available != null)
                {
                    foreach (string codeName in available)
                    {
                        string name = GetCheckBoxName(codeName);

                        if (Request.Form[name] == null)
                        {
                            // Unchecked
                            Settings.Deselect(ObjectType, codeName, SiteObject);
                        }
                        else
                        {
                            // Checked
                            Settings.Select(ObjectType, codeName, SiteObject);
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
        hdnAvailableItems.Value = (sbAvailable != null) ? sbAvailable.ToString() : string.Empty;
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

        return preposition + ValidationHelper.GetIdentifier(codeName).ToLower().Replace("'", "\'");
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

            // Initialize strings
            btnAll.Text = GetString("ImportExport.All");
            btnNone.Text = GetString("export.none");
            btnDefault.Text = GetString("General.Default");

            // Get object info
            GeneralizedInfo info = CMSObjectHelper.GetReadOnlyObject(ObjectType);
            if (info != null)
            {
                plcGrid.Visible = true;
                codeNameColumnName = info.CodeNameColumn;
                displayNameColumnName = info.DisplayNameColumn;

                // Get data source
                string where = GenerateWhereCondition();
                string orderBy = GetOrderByExpression(info);

                // Prepare the columns
                string columns = null;
                if (info.CodeNameColumn != TypeInfo.COLUMN_NAME_UNKNOWN)
                {
                    columns += info.CodeNameColumn;
                }
                if ((info.DisplayNameColumn.ToLower() != info.CodeNameColumn.ToLower()) && (info.DisplayNameColumn != TypeInfo.COLUMN_NAME_UNKNOWN))
                {
                    if (columns != null)
                    {
                        columns += ", ";
                    }
                    columns += info.DisplayNameColumn;
                }

                DataSet ds = info.GetData(null, where, orderBy, -1, columns, false, CurrentOffset, CurrentPageSize, ref pagerForceNumberOfResults);

                // Set correct ID for direct page contol
                pagerElem.DirectPageControlID = ((float)pagerForceNumberOfResults / pagerElem.CurrentPageSize > 20.0f) ? "txtPage" : "drpPage";

                // Call page binding event
                if (OnPageBinding != null)
                {
                    OnPageBinding(this, null);
                }

                // Prepare checkbox field
                TemplateField checkBoxField = (TemplateField)gvObjects.Columns[0];
                checkBoxField.HeaderText = GetString("General.Export");

                // Prepare display name field
                TemplateField nameField = (TemplateField)gvObjects.Columns[1];
                nameField.HeaderText = GetString("general.displayname");

                // Load data
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    plcObjects.Visible = true;
                    lblNoData.Visible = false;
                    gvObjects.DataSource = ds;
                    gvObjects.DataBind();
                }
                else
                {
                    plcObjects.Visible = false;
                    lblNoData.Visible = true;
                    lblNoData.Text = String.Format(GetString("ExportGridView.NoData"), GetString("objecttype." + ObjectType.Replace(".", "_").Replace("#", "_")));
                }
            }
            else
            {
                plcGrid.Visible = false;
            }
        }
        else
        {
            pnlGrid.Visible = false;
            gvObjects.DataSource = null;
            gvObjects.DataBind();
        }
    }


    // Genearate where condition
    private string GenerateWhereCondition()
    {
        return Settings.GetObjectWhereCondition(ObjectType, SiteObject);
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
    /// Ensure objects preselection.
    /// </summary>
    /// <param name="codeName">Code name</param>
    protected string IsChecked(object codeName)
    {
        string name = ValidationHelper.GetString(codeName, "");
        if (Settings.IsSelected(ObjectType, name, SiteObject))
        {
            return " checked=\"checked\" ";
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
        DateTime originalTS = Settings.TimeStamp;

        Settings.TimeStamp = DateTimeHelper.ZERO_TIME;
        Settings.LoadDefaultSelection(ObjectType, SiteObject, ExportTypeEnum.All, true, false);
        Settings.TimeStamp = originalTS;
    }


    protected void btnNone_Click(object sender, EventArgs e)
    {
        // Load none selection
        Settings.LoadDefaultSelection(ObjectType, SiteObject, ExportTypeEnum.None, true, false);
    }


    protected void btnDefault_Click(object sender, EventArgs e)
    {
        // Load default selection
        Settings.LoadDefaultSelection(ObjectType, SiteObject, ExportTypeEnum.Default, true, false);
    }


    #region "IUniPageable Members"

    /// <summary>
    /// Pager data item.
    /// </summary>
    public object PagerDataItem
    {
        get
        {
            return gvObjects.DataSource;
        }
        set
        {
            gvObjects.DataSource = value;
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
            return pagerForceNumberOfResults;
        }
        set
        {
            pagerForceNumberOfResults = value;
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