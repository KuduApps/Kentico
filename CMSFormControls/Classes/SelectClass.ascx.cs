using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.FormControls;
using CMS.UIControls;

public partial class CMSFormControls_Classes_SelectClass : FormEngineUserControl
{
    #region "Variables"

    private bool mOnlyCustomTables = false;
    private bool mOnlyDocumentTypes = true;

    private string mWhereCondition = String.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the selector where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return mWhereCondition;
        }
        set
        {
            mWhereCondition = value;
        }
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
    /// Gets the inner DDL control.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// If true, only Custom tables are loaded.
    /// </summary>
    public bool OnlyCustomTables
    {
        get
        {
            return this.mOnlyCustomTables;
        }
        set
        {
            this.mOnlyCustomTables = value;
            this.mOnlyDocumentTypes = !value;
        }
    }


    /// <summary>
    /// If true, only Document types are loaded.
    /// </summary>
    public bool OnlyDocumentTypes
    {
        get
        {
            return this.mOnlyDocumentTypes;
        }
        set
        {
            this.mOnlyDocumentTypes = value;
            this.mOnlyCustomTables = !value;
        }
    }


    /// <summary>
    /// Gets or sets whether (all) field is in dropdownlist.
    /// </summary>
    public bool DisplayAllValue
    {
        get
        {
            return this.uniSelector.AllowAll;
        }
        set
        {
            this.uniSelector.AllowAll = value;
        }
    }


    /// <summary>
    /// Gets or sets whether (none) field is in dropdownlist.
    /// </summary>
    public bool DisplayNoneValue
    {
        get
        {
            return this.uniSelector.AllowEmpty;
        }
        set
        {
            this.uniSelector.AllowEmpty = value;
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
            UniSelector.IsLiveSite = value;
        }
    }

    #endregion


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


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        ReloadData(false);
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    /// <param name="reloadUniSelector">If true, UniSelector is also reloaded</param>
    public void ReloadData(bool reloadUniSelector)
    {
        uniSelector.IsLiveSite = IsLiveSite;
        string whereCondition = String.Empty;

        if (this.OnlyDocumentTypes)
        {
            whereCondition = "(ClassIsDocumentType = 1)";
        }
        else if (this.OnlyCustomTables)
        {
            whereCondition = "(ClassIsCustomTable = 1)";
        }

        // Combine default where condition with external
        if (!String.IsNullOrEmpty(this.WhereCondition))
        {
            whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, this.WhereCondition);
        }

        uniSelector.WhereCondition = whereCondition;

        if (reloadUniSelector)
        {
            this.uniSelector.Reload(true);
        }
    }
}
