using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_Captcha_CaptchaSelector : FormEngineUserControl
{
    #region "Variables"

    private string mSelectCaptcha = string.Empty;
    private bool mReloadDataOnPostback = true;

    #endregion


    #region "Properties"

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
            drpSelectCaptcha.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(drpSelectCaptcha.SelectedValue, string.Empty);
        }
        set
        {
            mSelectCaptcha = ValidationHelper.GetString(value, string.Empty);
            ReloadData();
        }
    }


    /// <summary>
    /// Indicates whether enable dropdown list autopostback.
    /// </summary>
    public bool AllowAutoPostBack
    {
        get
        {
            return drpSelectCaptcha.AutoPostBack;
        }
        set
        {
            drpSelectCaptcha.AutoPostBack = value;
        }
    }


    /// <summary>
    /// Indicates whether data should be reloaded on PostBack.
    /// </summary>
    public bool ReloadDataOnPostback
    {
        get
        {
            return mReloadDataOnPostback;
        }
        set
        {
            mReloadDataOnPostback = value;
        }
    }


    /// <summary>
    /// Returns ClientID of the DropDownList with badword action.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.drpSelectCaptcha.ClientID;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        return true;
    }


    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        if ((RequestHelper.IsPostBack() && ReloadDataOnPostback) || !RequestHelper.IsPostBack())
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Loads drop down list with data.
    /// </summary>
    public void ReloadData()
    {
        // Reload dropdown list
        drpSelectCaptcha.ClearSelection();
        if (drpSelectCaptcha.Items.Count == 0)
        {
            drpSelectCaptcha.Items.Add(new ListItem(GetString("captcha.simple"), (((int)CaptchaEnum.Default)).ToString()));
            drpSelectCaptcha.Items.Add(new ListItem(GetString("captcha.logic"), (((int)CaptchaEnum.Logic)).ToString()));
            drpSelectCaptcha.Items.Add(new ListItem(GetString("captcha.text"), (((int)CaptchaEnum.Text)).ToString()));
        }

        // Preselect value
        if (drpSelectCaptcha.Items.Count != 0)
        {
            ListItem selectedItem = drpSelectCaptcha.Items.FindByValue(mSelectCaptcha);
            if (selectedItem != null)
            {
                selectedItem.Selected = true;
            }
            else
            {
                drpSelectCaptcha.SelectedIndex = 0;
            }
        }
    }

    #endregion
}
