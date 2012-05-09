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

public partial class CMSModules_Settings_FormControls_SelectDocumentOrder : FormEngineUserControl
{
    private string order = "";


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
            this.drpSelectOrder.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(drpSelectOrder.SelectedValue, "");
        }
        set
        {
            order = ValidationHelper.GetString(value, "");
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
    /// Returns ClientID of the DropDownList with order.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.drpSelectOrder.ClientID;
        }
    }


    /// <summary>
    /// Loads drop down list with data.
    /// </summary>
    private void ReloadData()
    {
        if (this.drpSelectOrder.Items.Count == 0)
        {
            this.drpSelectOrder.Items.Add(new ListItem(GetString("settings.alphabetical"), "ALPHABETICAL"));
            this.drpSelectOrder.Items.Add(new ListItem(GetString("settings.first"), "FIRST"));
            this.drpSelectOrder.Items.Add(new ListItem(GetString("settings.last"), "LAST"));
        }

        // Preselect value
        ListItem selectedItem = drpSelectOrder.Items.FindByValue(order);
        if (selectedItem != null)
        {
            this.drpSelectOrder.ClearSelection();
            selectedItem.Selected = true;
        }
        else
        {
            this.drpSelectOrder.SelectedIndex = 0;
        }
    }
}
