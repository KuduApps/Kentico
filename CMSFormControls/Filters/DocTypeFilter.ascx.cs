using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Controls;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSFormControls_Filters_DocTypeFilter : CMSAbstractBaseFilterControl
{
    #region "Variables"

    private CMSUserControl filteredControl;


    private bool mRenderTableTag = true;


    private DataClassInfo mSelectedClass;


    private bool mIsLiveSite = true;
    private bool mShowCustomTableClasses = true;
    private bool? mIsSiteManager = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates, if wrapping table should be rendered.
    /// </summary>
    public bool RenderTableTag
    {
        get
        {
            return mRenderTableTag;
        }
        set
        {
            mRenderTableTag = value;
        }
    }


    /// <summary>
    /// If false, custom table in drop down is never shown.
    /// </summary>
    public bool ShowCustomTableClasses
    {
        get
        {
            return mShowCustomTableClasses;
        }
        set
        {
            mShowCustomTableClasses = value;
        }
    }


    /// <summary>
    /// Selected class id.
    /// </summary>
    public int ClassId
    {
        get
        {
            return ValidationHelper.GetInteger(uniSelector.Value, 0);
        }
    }


    /// <summary>
    /// Indicates if filter is used on live site.
    /// </summary>
    public bool IsLiveSite
    {
        get
        {
            return mIsLiveSite;
        }
        set
        {
            mIsLiveSite = value;
        }
    }


    /// <summary>
    /// If true, filter is in site manager.
    /// </summary>
    public bool IsSiteManager
    {
        get
        {
            if (mIsSiteManager == null)
            {
                CMSUserControl parent = ControlsHelper.GetParentControl(this, typeof(CMSUserControl)) as CMSUserControl;
                if (parent != null)
                {
                    UserInfo ui = CMSContext.CurrentUser;
                    mIsSiteManager = ValidationHelper.GetBoolean(parent.GetValue("IsSiteManager"), false) && ui.UserSiteManagerAdmin;
                }
                else
                {
                    mIsSiteManager = false;
                }
            }

            return mIsSiteManager.Value;
        }
        set
        {
            mIsSiteManager = value;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Load Class type DDL
        if (!URLHelper.IsPostback())
        {
            drpClassType.Items.Clear();
            drpClassType.Items.Add(new ListItem(ResHelper.GetString("general.documenttype"), "doctype"));
            if (ShowCustomTableClasses && !DataHelper.DataSourceIsEmpty(DataClassInfoProvider.GetCustomTableClasses(CMSContext.CurrentSiteID, null, null, -1, "ClassID")))
            {
                drpClassType.Items.Add(new ListItem(ResHelper.GetString("queryselection.classtype.customtables"), "customtables"));
            }

            lblDocType.Text = ResHelper.GetString("queryselection.lbldoctypes");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            uniSelector.StopProcessing = true;
        }
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                Initialize();
            }

            ReloadData();

            WhereCondition = GenerateWhereCondition();

        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        filteredControl = FilteredControl as CMSUserControl;

        // Initialize UniSelector with document types
        uniSelector.DisplayNameFormat = "{%ClassDisplayName%} ({%ClassName%})";
        uniSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;

        if (drpClassType.SelectedValue == "doctype")
        {
            uniSelector.WhereCondition = "ClassIsDocumentType = 1";
        }
        else
        {
            uniSelector.WhereCondition = "ClassIsCustomTable = 1";
        }

        if (!IsSiteManager)
        {
            uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(uniSelector.WhereCondition, "ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + CMSContext.CurrentSiteID + ")");
        }

        uniSelector.ReturnColumnName = "ClassID";
        uniSelector.ObjectType = SettingsObjectType.CLASS;
        uniSelector.ResourcePrefix = "allowedclasscontrol";
        uniSelector.AllowAll = false;
        uniSelector.AllowEmpty = false;
        uniSelector.DropDownSingleSelect.AutoPostBack = true;
        uniSelector.OnSelectionChanged += uniSelector_OnSelectionChanged;
        uniSelector.IsLiveSite = IsLiveSite && (filteredControl != null ? filteredControl.IsLiveSite : true);
        uniSelector.DialogWindowName = "DocumentTypeSelectionDialog";

    }


    /// <summary>
    /// Initialize filter.
    /// </summary>
    public void Initialize()
    {
        if (!String.IsNullOrEmpty(SelectedValue))
        {
            switch (FilterMode)
            {
                case SettingsObjectType.TRANSFORMATION:
                    TransformationInfo ti = TransformationInfoProvider.GetTransformation(SelectedValue);
                    if (ti != null)
                    {
                        mSelectedClass = DataClassInfoProvider.GetDataClass(ti.TransformationClassID);
                    }
                    break;

                case SettingsObjectType.QUERY:
                    Query q = QueryProvider.GetQuery(SelectedValue, false);
                    if (q != null)
                    {
                        mSelectedClass = DataClassInfoProvider.GetDataClass(q.QueryClassId);
                    }
                    break;
            }

            // If selected object is under custom class, change selected class type
            if (mSelectedClass != null)
            {
                if (mSelectedClass.ClassIsCustomTable)
                {
                    lblDocType.Text = ResHelper.GetString("queryselection.customtable");
                    ListItem selectedItem = ControlsHelper.FindItemByValue(drpClassType, "customtables", false);

                    // Select item which is already loaded in drop-down list
                    if (selectedItem != null)
                    {
                        drpClassType.SelectedValue = selectedItem.Value;
                    }
                }
                uniSelector.Value = mSelectedClass.ClassID;
            }

        }
    }


    /// <summary>
    /// Main filter action (document name changed). Raises the filter event.
    /// </summary>
    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Set where condition
        WhereCondition = GenerateWhereCondition();

        // Raise OnFilterChange event
        RaiseOnFilterChanged();
    }


    /// <summary>
    /// Class type changed event.
    /// </summary>
    protected void drpClassType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpClassType.SelectedValue == "doctype")
        {
            lblDocType.Text = ResHelper.GetString("queryselection.lbldoctypes");
            uniSelector.WhereCondition = "(ClassIsDocumentType = 1)";
        }
        else
        {
            lblDocType.Text = ResHelper.GetString("queryselection.customtable");
            uniSelector.WhereCondition = "(ClassIsCustomTable = 1)";
        }

        if (!IsSiteManager)
        {
            uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(uniSelector.WhereCondition, "ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + CMSContext.CurrentSiteID + ")");
        }

        uniSelector.Reload(true);

        WhereCondition = GenerateWhereCondition();
        RaiseOnFilterChanged();
    }


    /// <summary>
    /// Generates where condition.
    /// </summary>
    private string GenerateWhereCondition()
    {
        if (!uniSelector.HasData)
        {
            return "0=1";
        }

        // Get the class ID
        int classId = ValidationHelper.GetInteger(uniSelector.Value, 0);
        if (classId <= 0)
        {
            // No results
            return "0=1";
        }

        // Only results for specific class
        string mode = string.Empty;
        if (filteredControl != null)
        {
            mode = ValidationHelper.GetString(filteredControl.GetValue("FilterMode"), "");
            // Set the prefix for the item
            DataClassInfo ci = DataClassInfoProvider.GetDataClass(classId);
            filteredControl.SetValue("ItemPrefix", ci.ClassName + ".");
        }

        switch (mode.ToLower())
        {
            case SettingsObjectType.TRANSFORMATION:
                return string.Concat("(TransformationClassID = ", ValidationHelper.GetInteger(uniSelector.Value, 0), ")");
            default:
                return string.Concat("(ClassID = ", ValidationHelper.GetInteger(uniSelector.Value, 0), ")");
        }
    }


    /// <summary>
    /// Renders control.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        if (RenderTableTag)
        {
            writer.Write("<table>");
        }
        base.Render(writer);

        if (RenderTableTag)
        {
            writer.Write("</table>");
        }
    }

    #endregion
}