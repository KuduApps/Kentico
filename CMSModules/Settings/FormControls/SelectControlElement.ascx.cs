using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.FormEngine;
using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSModules_Settings_FormControls_SelectControlElement : FormEngineUserControl
{
    private string controlElement = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }


    /// <summary>
    /// Gets ClientID of the dropdownlist with stylesheets.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return drpControlElement.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.drpControlElement.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(drpControlElement.SelectedValue, "");
        }
        set
        {
            controlElement = ValidationHelper.GetString(value, "");
            ReloadData();
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        return true;
    }


    /// <summary>
    /// Loads drop down list with data.
    /// </summary>
    private void ReloadData()
    {
        if (this.drpControlElement.Items.Count == 0)
        {
            this.drpControlElement.Items.Add(new ListItem("span", "span"));
            this.drpControlElement.Items.Add(new ListItem("div", "div"));
        }

        ListItem selectedItem = drpControlElement.Items.FindByValue(controlElement);
        if (selectedItem != null)
        {
            drpControlElement.ClearSelection();
            selectedItem.Selected = true;
        }
        else
        {
            this.drpControlElement.SelectedIndex = 0;
        }
    }
}
