using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.FormEngine;

public partial class CMSFormControls_System_UserControlTypeSelector : FormEngineUserControl
{
    #region "Variables"

    private string mSelectedValue = String.Empty;
    private bool mIncludeAllItem = false;
    private string mDataType = null;
    private FieldEditorControlsEnum mFieldEditorControls = FieldEditorControlsEnum.All;
    private bool mIsPrimary = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.drpType.SelectedValue;
        }
        set
        {
            mSelectedValue = (string)value;
            if (this.drpType.Items.FindByValue(mSelectedValue) != null)
            {
                this.drpType.SelectedValue = mSelectedValue;
            }
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public FormUserControlTypeEnum ControlType
    {
        get
        {
            return FormUserControlInfoProvider.GetTypeEnum(ValidationHelper.GetInteger(this.Value, 0));
        }
        set
        {
            this.Value = Convert.ToString((int)value);
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return drpType.Enabled;
        }
        set
        {
            drpType.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets whether "all" item is present in the list.
    /// </summary>
    public bool IncludeAllItem
    {
        get
        {
            return mIncludeAllItem;
        }
        set
        {
            mIncludeAllItem = value;
        }
    }


    /// <summary>
    /// Gets or sets value indicating if drop-down is set to cause AutoPostBack.
    /// </summary>
    public bool AutoPostBack
    {
        get
        {
            return drpType.AutoPostBack;
        }
        set
        {
            drpType.AutoPostBack = value;
        }
    }


    /// <summary>
    /// Column data type to limit form control types.
    /// </summary>
    public string DataType
    {
        get
        {
            return mDataType;
        }
        set
        {
            mDataType = value;
        }
    }


    /// <summary>
    /// FieldEditorControlsEnum to limit form control types.
    /// </summary>
    public FieldEditorControlsEnum FieldEditorControls
    {
        get
        {
            return mFieldEditorControls;
        }
        set
        {
            mFieldEditorControls = value;
        }
    }


    /// <summary>
    /// Primary field to limit form control types.
    /// </summary>
    public bool IsPrimary
    {
        get
        {
            return mIsPrimary;
        }
        set
        {
            mIsPrimary = value;
        }
    }


    /// <summary>
    /// Indicates if field is External.
    /// </summary>
    public bool External
    {
        get;
        set;
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize filter dropdownlist
        if (drpType.Items.Count <= 0)
        {
            ReloadControl();
        }
        if (drpType.Items.FindByValue(mSelectedValue) != null)
        {
            this.drpType.SelectedValue = mSelectedValue;
        }

        if (!String.IsNullOrEmpty(this.CssClass))
        {
            drpType.CssClass = this.CssClass;
            this.CssClass = null;
        }

        drpType.SelectedIndexChanged += new EventHandler(drpType_SelectedIndexChanged);
    }


    /// <summary>
    /// Selected item changed.
    /// </summary>
    void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        RaiseOnChanged();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Clears selection of drop-down list.
    /// </summary>
    public void ClearSelection()
    {
        drpType.ClearSelection();
    }


    /// <summary>
    /// Loads control with items.
    /// </summary>
    public void ReloadControl()
    {
        drpType.Items.Clear();
        drpType.SelectedValue = null;
        drpType.ClearSelection();

        // Load control dynamicaly with limited set of available types
        if (!String.IsNullOrEmpty(this.DataType))
        {
            drpType.DataTextField = "text";
            drpType.DataValueField = "value";
            DataSet ds = FormHelper.GetAvailableControlTypes(this.DataType, this.FieldEditorControls, (this.IsPrimary && !this.External));
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                // Sort result by 'text' column
                ds.Tables[0].DefaultView.Sort = "text";
                drpType.DataSource = ds.Tables[0].DefaultView;
                drpType.DataBind();
            }
        }
        // Load control with all types
        else
        {
            foreach (FormUserControlTypeEnum en in new FormUserControlTypeEnum[] {
                    FormUserControlTypeEnum.Captcha, FormUserControlTypeEnum.Filter, FormUserControlTypeEnum.Input, FormUserControlTypeEnum.Multifield,
                    FormUserControlTypeEnum.Selector, FormUserControlTypeEnum.Uploader, FormUserControlTypeEnum.Viewer, FormUserControlTypeEnum.Visibility })
            {
                drpType.Items.Add(new ListItem(GetString("formcontrolstype." + en.ToString()), Convert.ToInt32(en).ToString()));
            }
        }

        // Include 'all' item in first position
        if (IncludeAllItem)
        {
            drpType.Items.Insert(0, new ListItem(GetString("general.selectall"), Convert.ToInt32(FormUserControlTypeEnum.Unspecified).ToString()));
        }
    }


    /// <summary>
    /// Loads control with items.
    /// </summary>
    /// <param name="controlTypes">Specified control types</param>
    public void ReloadControl(List<FormUserControlTypeEnum> controlTypes)
    {
        drpType.Items.Clear();

        foreach (FormUserControlTypeEnum controlType in controlTypes)
        {
            drpType.Items.Add(new ListItem(GetString("formcontrolstype." + controlType.ToString()), Convert.ToInt32(controlType).ToString()));
        }

        // Include 'all' item in first position
        if (IncludeAllItem)
        {
            drpType.Items.Insert(0, new ListItem(GetString("general.selectall"), Convert.ToInt32(FormUserControlTypeEnum.Unspecified).ToString()));
        }
    }

    #endregion
}
