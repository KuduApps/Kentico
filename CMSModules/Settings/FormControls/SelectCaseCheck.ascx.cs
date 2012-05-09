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

public partial class CMSModules_Settings_FormControls_SelectCaseCheck : FormEngineUserControl
{
    private string caseCheck = "";


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
            this.drpSelectCaseCheck.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(drpSelectCaseCheck.SelectedValue, "");
        }
        set
        {
            caseCheck = ValidationHelper.GetString(value, "");
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
    /// Returns ClientID of the DropDownList with case check.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.drpSelectCaseCheck.ClientID;
        }
    }


    /// <summary>
    /// Loads drop down list with data.
    /// </summary>
    private void ReloadData()
    {
        if (this.drpSelectCaseCheck.Items.Count == 0)
        {
            this.drpSelectCaseCheck.Items.Add(new ListItem(GetString("urlcasecheck.none"), "NONE"));
            this.drpSelectCaseCheck.Items.Add(new ListItem(GetString("urlcasecheck.exact"), "EXACT"));
            this.drpSelectCaseCheck.Items.Add(new ListItem(GetString("urlcasecheck.lowercase"), "LOWERCASE"));
            this.drpSelectCaseCheck.Items.Add(new ListItem(GetString("urlcasecheck.uppercase"), "UPPERCASE"));
        }

        // Preselect value
        ListItem selectedItem = drpSelectCaseCheck.Items.FindByValue(caseCheck);
        if (selectedItem != null)
        {
            drpSelectCaseCheck.ClearSelection();
            selectedItem.Selected = true;
        }
        else
        {
            this.drpSelectCaseCheck.SelectedIndex = 0;
        }
    }
}