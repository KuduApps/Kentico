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

public partial class CMSModules_Settings_FormControls_SelectPermissions : FormEngineUserControl
{
    private string permissions = "";


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
            this.drpSelectPermissions.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(drpSelectPermissions.SelectedValue, "");
        }
        set
        {
            permissions = ValidationHelper.GetString(value, "");
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
    /// Returns ClientID of the DropDownList with permissions.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.drpSelectPermissions.ClientID;
        }
    }


    /// <summary>
    /// Loads drop down list with data.
    /// </summary>
    private void ReloadData()
    {
        if (this.drpSelectPermissions.Items.Count == 0)
        {
            this.drpSelectPermissions.Items.Add(new ListItem(GetString("settings.allpages"), "ALL"));
            this.drpSelectPermissions.Items.Add(new ListItem(GetString("settings.nopage"), "NO"));
            this.drpSelectPermissions.Items.Add(new ListItem(GetString("settings.securedareas"), "SECUREDAREAS"));
        }

        // Preselect value
        ListItem selectedItem = drpSelectPermissions.Items.FindByValue(permissions);
        if (selectedItem != null)
        {
            drpSelectPermissions.ClearSelection();
            selectedItem.Selected = true;
        }
        else
        {
            this.drpSelectPermissions.SelectedIndex = 0;
        }
    }
}