using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;
using System.Text;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.Controls;

public partial class CMSModules_ImportExport_Controls_ImportGridTasks : CMSUserControl, IUniPageable
{
    #region "Variables"

    // Indicates if object selection is enabled
    protected bool selectionEnabled = true;

    protected string codeNameColumnName = "";
    protected string displayNameColumnName = "";

    protected string preposition = "chck_";
    protected string taskPreposition = "etchck_";

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
                                // Initialize array list
                                if (mExistingObjects == null)
                                {
                                    mExistingObjects = new ArrayList();
                                }

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
                string[] available = this.hdnAvailableTasks.Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (available != null)
                {
                    foreach (string item in available)
                    {
                        int taskId = ValidationHelper.GetInteger(item, 0);
                        string name = GetCheckBoxName(taskId);

                        if (Request.Form[name] == null)
                        {
                            // Unchecked
                            this.Settings.DeselectTask(ObjectType, taskId, SiteObject);
                        }
                        else
                        {
                            // Checked
                            this.Settings.SelectTask(ObjectType, taskId, SiteObject);
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
            this.hdnAvailableTasks.Value = sbAvailable.ToString();
        }
        else
        {
            this.hdnAvailableTasks.Value = "";
        }
    }


    /// <summary>
    /// Returns the ID for the checkbox.
    /// </summary>
    /// <param name="taskId">Task ID</param>
    protected string GetCheckBoxId(object taskId)
    {
        if (sbAvailable == null)
        {
            sbAvailable = new StringBuilder();
        }
        else
        {
            sbAvailable.Append(";");
        }
        sbAvailable.Append(taskId);

        return taskPreposition + taskId;
    }


    /// <summary>
    /// Returns the name for the checkbox.
    /// </summary>
    /// <param name="taskId">Task ID</param>
    protected string GetCheckBoxName(object taskId)
    {
        return taskPreposition + taskId;
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

            lblTasks.Text = GetString("Export.Tasks");

            if (selectionEnabled)
            {
                // Initilaize strings
                btnAllTasks.Text = GetString("ImportExport.All");
                btnNoneTasks.Text = GetString("export.none");
            }

            pnlTaskLinks.Visible = selectionEnabled;

            // Get object info
            GeneralizedInfo info = CMSObjectHelper.GetReadOnlyObject(ObjectType);
            if (info != null)
            {
                plcGrid.Visible = true;
                codeNameColumnName = info.CodeNameColumn;
                displayNameColumnName = info.DisplayNameColumn;

                // Task fields
                TemplateField taskCheckBoxField = (TemplateField)gvTasks.Columns[0];
                taskCheckBoxField.HeaderText = GetString("General.Process");

                BoundField titleField = (BoundField)gvTasks.Columns[1];
                titleField.HeaderText = GetString("Export.TaskTitle");

                BoundField typeField = (BoundField)gvTasks.Columns[2];
                typeField.HeaderText = GetString("general.type");

                BoundField timeField = (BoundField)gvTasks.Columns[3];
                timeField.HeaderText = GetString("Export.TaskTime");

                // Load tasks
                DataSet ds = DataSource;
                if (!DataHelper.DataSourceIsEmpty(ds) && !DataHelper.DataSourceIsEmpty(ds.Tables["Export_Task"]) && (ValidationHelper.GetBoolean(Settings.GetSettings(ImportExportHelper.SETTINGS_TASKS), true)))
                {
                    plcTasks.Visible = true;
                    gvTasks.DataSource = ds.Tables["Export_Task"];

                    // Set correct ID for direct page contol
                    pagerElem.DirectPageControlID = ((float)ds.Tables["Export_Task"].Rows.Count / pagerElem.CurrentPageSize > 20.0f) ? "txtPage" : "drpPage";

                    // Call page binding event
                    if (OnPageBinding != null)
                    {
                        OnPageBinding(this, null);
                    }

                    gvTasks.DataBind();
                }
                else
                {
                    plcTasks.Visible = false;
                }
            }
            else
            {
                plcGrid.Visible = false;
            }

            // Disable license selection
            bool enable = !((ObjectType == LicenseObjectType.LICENSEKEY) && Settings.IsOlderVersion);
            gvTasks.Enabled = enable;
            pnlTaskLinks.Enabled = enable;
        }
        else
        {
            pnlGrid.Visible = false;
        }
    }


    /// <summary>
    /// Ensure tasks preselection.
    /// </summary>
    /// <param name="taskId">Task ID</param>
    protected string IsTaskChecked(object taskId)
    {
        string value = "";
        int id = ValidationHelper.GetInteger(taskId, 0);
        if (Settings.IsTaskSelected(ObjectType, id, SiteObject))
        {
            value += " checked=\"checked\" ";
        }
        if (!selectionEnabled)
        {
            value += " disabled=\"disabled\" ";
        }

        return value;
    }



    protected void btnAll_Click(object sender, EventArgs e)
    {
        // Load all selection
        this.Settings.LoadDefaultSelection(ObjectType, SiteObject, ImportTypeEnum.All, false, true);
    }


    protected void btnNone_Click(object sender, EventArgs e)
    {
        // Load none selection
        this.Settings.LoadDefaultSelection(ObjectType, SiteObject, ImportTypeEnum.None, false, true);
    }


    #region "IUniPageable Members"

    /// <summary>
    /// Pager data item.
    /// </summary>
    public object PagerDataItem
    {
        get
        {
            return this.gvTasks.DataSource;
        }
        set
        {
            this.gvTasks.DataSource = value;
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
