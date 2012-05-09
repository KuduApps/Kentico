using System;
using System.Data;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.UIControls;

public partial class CMSModules_ContactManagement_FormControls_ActivityTypeSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mShowAll = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Return null if "(all)" item is selected
            if (ValidationHelper.GetInteger(ucType.Value, Int32.MinValue) == UniSelector.US_ALL_RECORDS)
            {
                return null;
            }
            return ucType.Value;
        }
        set
        {
            ucType.Value = ValidationHelper.GetString(value, null);
        }
    }


    /// <summary>
    /// Gets selected value from dropdown list.
    /// </summary>
    public string SelectedValue
    {
        get
        {
            return ValidationHelper.GetString(this.Value, null);
        }
    }


    /// <summary>
    /// If set to true, only custom activities will be shown.
    /// </summary>
    public bool ShowCustomActivitiesOnly
    {
        get;
        set;
    }


    /// <summary>
    /// If set to true, only enabled activities will be shown.
    /// </summary>
    public bool ShowEnabledActivitiesOnly
    {
        get;
        set;
    }


    /// <summary>
    /// If set to true, only manually creatable activities are shown.
    /// </summary>
    public bool ShowManuallyCreatableActivities
    {
        get;
        set;
    }


    /// <summary>
    /// If set to true, "(all)" item will be included in the list.
    /// </summary>
    public bool ShowAll
    {
        get { return mShowAll; }
        set { mShowAll = value; }
    }


    /// <summary>
    /// Gets or sets AutoPostBack property of internal dropdown list.
    /// </summary>
    public bool AutoPostBack
    {
        get { return ucType.DropDownSingleSelect.AutoPostBack; }
        set { ucType.DropDownSingleSelect.AutoPostBack = value; }
    }

    #endregion


    #region "Events"

    public event EventHandler OnSelectedIndexChanged;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            ucType.Visible = false;
            return;
        }

        if (!String.IsNullOrEmpty(this.CssClass))
        {
            ucType.DropDownSingleSelect.CssClass = this.CssClass;
        }
        ucType.AllowAll = ShowAll;

        string where = null;
        if (ShowCustomActivitiesOnly)
        {
            where = "ActivityTypeIsCustom=1";
        }
        if (ShowManuallyCreatableActivities)
        {
            if (!String.IsNullOrEmpty(where))
            {
                where += " AND ";
            }
            where += "ActivityTypeManualCreationAllowed=1";
        }
        if (ShowEnabledActivitiesOnly)
        {
            if (!String.IsNullOrEmpty(where))
            {
                where += " AND ";
            }
            where += "ActivityTypeEnabled=1";
        }

        ucType.WhereCondition = where;
        ucType.DropDownSingleSelect.SelectedIndexChanged += new EventHandler(DropDownSingleSelect_SelectedIndexChanged);
    }


    protected void DropDownSingleSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnSelectedIndexChanged != null)
        {
            OnSelectedIndexChanged(this, EventArgs.Empty);
        }
    }

    #endregion
}
