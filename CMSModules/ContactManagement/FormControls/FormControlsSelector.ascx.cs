using System;
using System.Data;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_ContactManagement_FormControls_FormControlsSelector : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // "none" selected
            if (ucType.DropDownSingleSelect.SelectedIndex <= 0)
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
    /// Gets selected value as string.
    /// </summary>
    public string SelectedValue
    {
        get
        {
            return ValidationHelper.GetString(Value, null);
        }
    }

    /// <summary>
    /// If set, only controls of this type are shown.
    /// </summary>
    public int? ShowControlsOfType
    {
        get;
        set;
    }


    /// <summary>
    /// If set, only form controls for integer are shown.
    /// </summary>
    public bool ShowControlsForInteger
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets additional list of controls that will be shown.
    /// List must contain form control code names separated by semicolon ("formcontrol1;formcontrol2;formcontrol3").
    /// </summary>
    public string ShowAdditionalControls
    {
        get;
        set;
    }

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

        // Additional item for selecting default control
        string[,] specFields = new string[,] {{ GetString("general.defaultchoice"), "##default##" }};
        ucType.SpecialFields = specFields;

        string where = null;
        if (ShowControlsForInteger)
        {
            where = SqlHelperClass.AddWhereCondition(where, "UserControlForInteger=1");
        }
        if (ShowControlsOfType == null)
        {
            where = SqlHelperClass.AddWhereCondition(where, "UserControlType IS NULL");
        }
        else if (ShowControlsOfType >= 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "UserControlType=" + ShowControlsOfType.ToString());
        }
        // Show additional controls
        if (!String.IsNullOrEmpty(ShowAdditionalControls))
        {
            if (!String.IsNullOrEmpty(where))
            {
                where = "(" + where + ") OR ";
            }
            where += "UserControlCodeName IN ('" + SqlHelperClass.GetSafeQueryString(ShowAdditionalControls).Replace(";", "','") + "')";
        }

        ucType.WhereCondition = where;
    }

    #endregion
}
