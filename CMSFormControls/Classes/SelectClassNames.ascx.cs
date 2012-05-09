using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.FormControls;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSFormControls_Classes_SelectClassNames : FormEngineUserControl
{
    #region "Variables"

    private bool mDisplayClearButton = true;

    #endregion


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
    /// Returns ClientID of the textbox with classnames.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return uniSelector.TextBoxSelect.ClientID;
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
    /// Gets dropdown list.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            EnsureChildControls();
            return uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to display Clear button.
    /// </summary>
    public bool DisplayClearButton
    {
        get
        {
            return mDisplayClearButton;
        }
        set
        {
            mDisplayClearButton = value;
            if (uniSelector != null)
            {
                uniSelector.AllowEmpty = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the SiteID value to filter classnames.
    /// </summary>
    public int SiteID
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("SiteID"), 0);
        }
        set
        {
            SetValue("SiteID", value);
        }
    }


    /// <summary>
    /// Indicates if should be shown only document types.
    /// </summary>
    public bool ShowOnlyCoupled
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowOnlyCoupled"), false);
        }
        set
        {
            SetValue("ShowOnlyCoupled", value);
        }
    }

    #endregion


    #region "Constructors"

    public CMSFormControls_Classes_SelectClassNames()
    {
        SiteID = CMSContext.CurrentSiteID;
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
            ReloadData(false);
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
    /// <param name="forceReload">Indicates if data should be loaded from DB</param>
    public void ReloadData(bool forceReload)
    {
        uniSelector.IsLiveSite = IsLiveSite;
        uniSelector.AllowEmpty = DisplayClearButton;
        // Where condition
        string where = null;

        // Show only document types
        if (ShowOnlyCoupled)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ClassIsCoupledClass = 1");
        }

        // Filter using Site ID
        if (SiteID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + SiteID + ")"); ;
        }

        uniSelector.WhereCondition = where;
        uniSelector.Reload(forceReload);
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        string[] values = ValidationHelper.GetString(uniSelector.Value, "").Split(new char[] { ';' });
        foreach (string className in values)
        {
            if ((className != "") && !MacroResolver.ContainsMacro(className) && !className.Contains("*"))
            {
                DataClassInfo di = DataClassInfoProvider.GetDataClass(className);
                if (di == null)
                {
                    ValidationError = GetString("formcontrols_selectclassnames.notexist").Replace("%%code%%", className);
                    return false;
                }
            }
        }
        return true;
    }

    #endregion
}
