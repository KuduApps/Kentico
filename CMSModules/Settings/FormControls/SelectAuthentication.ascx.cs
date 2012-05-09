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
using CMS.Synchronization;

public partial class CMSModules_Settings_FormControls_SelectAuthentication : FormEngineUserControl
{
    private string authentication = "";


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
            this.drpSelectAuthentication.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(drpSelectAuthentication.SelectedValue, "");
        }
        set
        {
            authentication = ValidationHelper.GetString(value, "");
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
    /// Returns ClientID of the DropDownList with authentication.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.drpSelectAuthentication.ClientID;
        }
    }


    /// <summary>
    /// Loads drop down list with data.
    /// </summary>
    private void ReloadData()
    {
        if (this.drpSelectAuthentication.Items.Count == 0)
        {
            this.drpSelectAuthentication.Items.Add(new ListItem(GetString("settings.username"), "USERNAME"));
            this.drpSelectAuthentication.Items.Add(new ListItem(GetString("settings.x509"), "X509"));
        }

        // Preselect value
        ListItem selectedItem = drpSelectAuthentication.Items.FindByValue(authentication);
        if (selectedItem != null)
        {
            drpSelectAuthentication.ClearSelection();
            selectedItem.Selected = true;
        }
        else
        {
            this.drpSelectAuthentication.SelectedIndex = 0;
        }
    }
}