using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Text;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.FormEngine;


public partial class CMSFormControls_Basic_MultipleChoiceControl : FormEngineUserControl
{
    #region "Variables"

    private string[] selectedValues = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return list.Enabled;
        }
        set
        {
            list.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            StringBuilder text = new StringBuilder();
            foreach (ListItem item in list.Items)
            {
                if (item.Selected)
                {
                    text.Append(item.Value + "|");
                }
            }
            return text.ToString().TrimEnd('|');
        }
        set
        {
            selectedValues = ValidationHelper.GetString(value, "").Split('|');
            list.ClearSelection();
            if (selectedValues != null)
            {
                foreach (string val in selectedValues)
                {
                    if (list.Items.FindByValue(val) != null)
                    {
                        list.Items.FindByValue(val).Selected = true;
                    }
                }
            }
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        LoadAndSelectList();

        // Set control styles
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            list.CssClass = this.CssClass;
            this.CssClass = null;
        }
        else if (String.IsNullOrEmpty(list.CssClass))
        {
            list.CssClass = "CheckBoxListField";
        }
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            list.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        this.CheckRegularExpression = true;
        this.CheckFieldEmptiness = true;
    }


    /// <summary>
    /// Loads and selects control.
    /// </summary>
    private void LoadAndSelectList()
    {
        if (list.Items.Count == 0)
        {
            // Set control direction
            string direction = ValidationHelper.GetString(this.GetValue("repeatdirection"), "");
            if (direction.Equals("horizontal",StringComparison.InvariantCultureIgnoreCase))
            {
                list.RepeatDirection = RepeatDirection.Horizontal;
            }
            else
            {
                list.RepeatDirection = RepeatDirection.Vertical;
            }

            string options = ValidationHelper.GetString(this.GetValue("options"), null);
            string query = ValidationHelper.GetString(this.GetValue("query"), null);

            try
            {
                FormHelper.LoadItemsIntoList(options, query, list.Items, this.FieldInfo);
            }
            catch (Exception ex)
            {
                DisplayException(ex);
            }

            FormHelper.SelectMultipleValues(selectedValues, list.Items,  ListSelectionMode.Multiple);
        }
    }
    

    /// <summary>
    /// Displays exception control with current error.
    /// </summary>
    /// <param name="ex">Thrown exception</param>
    private void DisplayException(Exception ex)
    {
        FormControlError ctrlError = new FormControlError();
        ctrlError.InnerException = ex;
        this.Controls.Add(ctrlError);
        list.Visible = false;
    }

    #endregion
}
