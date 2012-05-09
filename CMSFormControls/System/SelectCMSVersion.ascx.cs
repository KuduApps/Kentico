using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSFormControls_System_SelectCMSVersion : FormEngineUserControl
{
    #region "Variables"

    bool mAllowEmpty = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates if value of form control could be empty.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return mAllowEmpty;
        }
        set
        {
            mAllowEmpty = value;
        }
    }


    /// <summary>
    /// Selected version (e.g. '5.5', '5.5R2',...).
    /// </summary>
    public override object Value
    {
        get
        {
            if (drpVersion.SelectedItem == null)
            {
                return "";
            }

            return drpVersion.SelectedItem.Value;
        }
        set
        {
            if (value == null)
            {
                drpVersion.SelectedValue = "";
            }
            else
            {
                drpVersion.SelectedValue = value.ToString();
            }
        }
    }

    #endregion


    #region "Control methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!URLHelper.IsPostback())
        {
            // Fill the combo with versions
            drpVersion.Items.Add(new ListItem("(none)", ""));
            drpVersion.Items.Add(new ListItem("CMS 3.0", "3.0"));
            drpVersion.Items.Add(new ListItem("CMS 3.1", "3.1"));
            drpVersion.Items.Add(new ListItem("CMS 3.1a", "3.1a"));
            drpVersion.Items.Add(new ListItem("CMS 4.0", "4.0"));
            drpVersion.Items.Add(new ListItem("CMS 4.1", "4.1"));
            drpVersion.Items.Add(new ListItem("CMS 5.0", "5.0"));
            drpVersion.Items.Add(new ListItem("CMS 5.5", "5.5"));
            drpVersion.Items.Add(new ListItem("CMS 5.5R2", "5.5R2"));
            drpVersion.Items.Add(new ListItem("CMS 6.0", "6.0"));

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }


    /// <summary>
    /// Validates the return value of form control.
    /// </summary>
    public override bool IsValid()
    {
        if (!AllowEmpty)
        {
            if (drpVersion.SelectedValue == "")
            {
                this.ValidationError = ResHelper.GetString("general.requirescmsversion");
                return false;
            }
        }

        return true;
    }

    #endregion
}