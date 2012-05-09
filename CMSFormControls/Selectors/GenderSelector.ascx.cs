using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSFormControls_Selectors_GenderSelector : FormEngineUserControl
{
    #region "Variables"

    private string selectedValue = null;
    private DisplayType displayType = DisplayType.DropDownList;

    /// <summary>
    /// Indicates what control should be used.
    /// </summary>
    private enum DisplayType
    {
        DropDownList = 0,
        RadioButtonsHorizontal = 1,
        RadioButtonsVertical = 2
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return drpGender.Enabled;
        }
        set
        {
            drpGender.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (displayType == DisplayType.DropDownList)
            {
                return drpGender.SelectedValue;
            }
            else
            {
                return radGender.SelectedValue;
            }
        }
        set
        {
            if (ContainsColumn("DisplayType"))
            {
                displayType = (DisplayType)this.Form.Data.GetValue("DisplayType");
            }

            selectedValue = ValidationHelper.GetString(value, null);
            if (displayType == DisplayType.DropDownList)
            {
                drpGender.SelectedValue = selectedValue;
            }
            else
            {
                radGender.SelectedValue = selectedValue;
            }
        }
    }


    /// <summary>
    /// Returns value converted to int.
    /// </summary>
    public int Gender
    {
        get
        {
            return ValidationHelper.GetInteger(this.Value, 0);
        }
        set
        {
            this.Value = value;
        }
    }


    /// <summary>
    /// Returns current control - RadioButtonList or DropDownList.
    /// </summary>
    public Control CurrentSelector
    {
        get
        {
            if (drpGender.Visible)
            {
                return drpGender;
            }
            else
            {
                return radGender;
            }
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Return type
        if (ContainsColumn("DisplayType"))
        {
            displayType = (DisplayType)this.Form.Data.GetValue("DisplayType");
        }

        if (displayType == DisplayType.DropDownList)
        {
            if (drpGender.Items.Count == 0)
            {
                drpGender.Items.Add(new ListItem(GetString("general.selectunknown"), ((int)UserGenderEnum.Unknown).ToString()));
                drpGender.Items.Add(new ListItem(GetString("general.male"), ((int)UserGenderEnum.Male).ToString()));
                drpGender.Items.Add(new ListItem(GetString("general.female"), ((int)UserGenderEnum.Female).ToString()));
                drpGender.SelectedValue = selectedValue;
                drpGender.CssClass = this.CssClass;
            }
        }
        else
        {
            if (radGender.Items.Count == 0)
            {
                radGender.Items.Add(new ListItem(GetString("general.unknown"), ((int)UserGenderEnum.Unknown).ToString()));
                radGender.Items.Add(new ListItem(GetString("general.male"), ((int)UserGenderEnum.Male).ToString()));
                radGender.Items.Add(new ListItem(GetString("general.female"), ((int)UserGenderEnum.Female).ToString()));
                radGender.SelectedValue = selectedValue;
                radGender.Enabled = this.Enabled;
                radGender.CssClass = this.CssClass;
            }
            radGender.RepeatDirection = displayType == DisplayType.RadioButtonsHorizontal ? RepeatDirection.Horizontal : RepeatDirection.Vertical;
        }
    }

    #endregion
}