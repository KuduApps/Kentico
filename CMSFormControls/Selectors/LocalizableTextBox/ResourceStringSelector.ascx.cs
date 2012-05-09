using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSFormControls_Selectors_LocalizableTextBox_ResourceStringSelector : FormEngineUserControl
{
    #region "Public properties"

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
            if (uniSelector != null)
            {
                uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return uniSelector.Value;
        }
        set
        {
            if (uniSelector == null)
            {
                pnlUpdate.LoadContainer();
            }
            uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets inner uniselector.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return uniSelector;
        }
    }


    /// <summary>
    /// Gets textbox.
    /// </summary>
    public TextBox TextBox
    {
        get
        {
            EnsureChildControls();
            return uniSelector.TextBoxSelect;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }


    protected override void EnsureChildControls()
    {
        if (uniSelector == null)
        {
            pnlUpdate.LoadContainer();
        }
        base.EnsureChildControls();
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        UICultureInfo ui;
        try
        {
            ui = UICultureInfoProvider.GetUICultureInfo(CMSContext.CurrentUser.PreferredUICultureCode);
        }
        catch
        {
            ui = UICultureInfoProvider.GetUICultureInfo(CultureHelper.DefaultUICulture);
        }
        if (ui != null)
        {
            uniSelector.WhereCondition = SqlHelperClass.AddWhereCondition(uniSelector.WhereCondition, "UICultureID = " + ui.UICultureID);
            uniSelector.ReturnColumnName = "StringKey";
            uniSelector.AdditionalColumns = "StringKey, TranslationText";
            uniSelector.DialogGridName = "~/CMSFormControls/Selectors/LocalizableTextBox/ResourceStringSelector.xml";
            uniSelector.IsLiveSite = IsLiveSite;
            uniSelector.Reload(false);
            uniSelector.DialogWindowWidth = 850;
            uniSelector.ResourcePrefix = "resourcestring";
            uniSelector.UseDefaultNameFilter = false;
        }
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        this.ValidationError = new Validator().NotEmpty(uniSelector.Value, GetString("Administration-UICulture_String_New.EmptyKey")).IsCodeName(uniSelector.Value, GetString("Administration-UICulture_String_New.InvalidCodeName")).Result;
        if (!String.IsNullOrEmpty(this.ValidationError))
        {
            return false;
        }
        return true;
    }

    #endregion
}
