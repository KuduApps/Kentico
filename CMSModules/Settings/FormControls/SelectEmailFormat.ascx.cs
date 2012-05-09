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

public partial class CMSModules_Settings_FormControls_SelectEmailFormat : FormEngineUserControl
{
    private string format = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        //ReloadData();
    }


    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        ReloadData();
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
            this.drpSelectFormat.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(drpSelectFormat.SelectedValue, "");
        }
        set
        {
            format = ValidationHelper.GetString(value, "");
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


    public override string ValueElementID
    {
        get
        {
            return this.drpSelectFormat.ClientID;
        }
    }


    /// <summary>
    /// Loads drop down list with data.
    /// </summary>
    private void ReloadData()
    {
        if (this.drpSelectFormat.Items.Count == 0)
        {
            this.drpSelectFormat.Items.Add(new ListItem(GetString("general.plaintext"), "plaintext"));
            this.drpSelectFormat.Items.Add(new ListItem(GetString("general.html"), "html"));
            this.drpSelectFormat.Items.Add(new ListItem(GetString("settings.both"), "both"));
        }

        // Preselect value
        ListItem selectedItem = drpSelectFormat.Items.FindByValue(format);
        if (selectedItem != null)
        {
            drpSelectFormat.ClearSelection();
            selectedItem.Selected = true;
        }
        else
        {
            this.drpSelectFormat.SelectedIndex = 0;
        }
    }
}