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

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.UIControls;

public partial class CMSFormControls_Sites_SelectSite : FormEngineUserControl
{
    #region "Variables"

    private bool mAllowMultipleSelection = false;

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
            EnsureChildControls();
            base.Enabled = value;
            usSites.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return usSites.Value;
        }
        set
        {
            EnsureChildControls();
            usSites.Value = value;
        }
    }


    /// <summary>
    /// Enables or disables multiple site selection.
    /// </summary>
    public bool AllowMultipleSelection
    {
        get
        {
            return mAllowMultipleSelection;
        }
        set
        {
            EnsureChildControls();
            mAllowMultipleSelection = value;
            if (mAllowMultipleSelection)
            {
                this.usSites.SelectionMode = SelectionModeEnum.MultipleTextBox;
            }
            else
            {
                this.usSites.SelectionMode = SelectionModeEnum.SingleTextBox;
            }
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            EnsureChildControls();
            base.IsLiveSite = value;
            usSites.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set resource strings for current mode
        usSites.ResourcePrefix = (this.AllowMultipleSelection) ? "sitesselect" : "siteselect";

        // Use sitenames as values
        if ((this.FieldInfo != null) && (this.FieldInfo.DataType == CMS.FormEngine.FormFieldDataTypeEnum.Integer))
        {
            usSites.ReturnColumnName = "SiteID";
            usSites.SelectionMode = SelectionModeEnum.SingleDropDownList;
        }
        else
        {
            usSites.ReturnColumnName = "SiteName";
        }

        if (this.HasDependingFields)
        {
            usSites.OnSelectionChanged += new EventHandler(usSites_OnSelectionChanged);
        }
    }


    /// <summary>
    /// Handles the OnSelectionChanged event of the usSites control.
    /// </summary>
    void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        this.RaiseOnChanged();
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (usSites == null)
        {
            this.pnlUpdate.LoadContainer();
        }

        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Gets where condition.
    /// </summary>
    public override string GetWhereCondition()
    {
        // Return where condition for integer
        if ((this.FieldInfo != null) && (FieldInfo.DataType == CMS.FormEngine.FormFieldDataTypeEnum.Integer))
        {
            if (ValidationHelper.GetInteger(usSites.Value, UniSelector.US_NONE_RECORD) == UniSelector.US_NONE_RECORD)
            {
                return FieldInfo.Name + " IS NULL";
            }
            else
            {
                return FieldInfo.Name + " = " + usSites.Value;
            }
        }
        // Return where condition for string
        else
        {
            if (String.IsNullOrEmpty(ValidationHelper.GetString(usSites.Value, null)))
            {
                return FieldInfo.Name + " IS NULL OR " + FieldInfo.Name + " = N''";
            }
            else
            {
                return FieldInfo.Name + " = N'" + usSites.Value + "'";
            }
        }
    }

    #endregion
}
