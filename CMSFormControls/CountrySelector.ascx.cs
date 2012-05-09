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
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.UIControls;

public partial class CMSFormControls_CountrySelector : FormEngineUserControl
{
    #region "Variables"

    private bool mDisplayAllItems = true;
    private bool mAddNoneRecord = false;
    private bool mUseCodeNameForSelection = true;
    private bool mAddSelectCountryRecord = true;
    private bool mEnableStateSelection = true;
    private ReturnType returnWhat = ReturnType.Both;

    /// <summary>
    /// Indicates what return value should be submited by the control.
    /// </summary>
    enum ReturnType
    {
        /// <summary>
        /// Default value. Returns string value with both countryID and stateID separated by semicolumn.
        /// </summary>
        Both = 0,

        /// <summary>
        /// Returns integer value containing only countryID.
        /// </summary>
        CountryID = 1,

        /// <summary>
        /// Returns integer value containing only stateID.
        /// </summary>
        StateID = 2
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets client ID of the country drop down list.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.CountryDropDown.ClientID;
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
            if (this.uniSelectorCountry != null)
            {
                this.uniSelectorCountry.Enabled = value;
            }
            if (this.uniSelectorState != null)
            {
                this.uniSelectorState.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the value which indicates whether the selector should load all data to DDL.
    /// </summary>
    public bool DisplayAllItems
    {
        get
        {
            return this.mDisplayAllItems;
        }
        set
        {
            this.mDisplayAllItems = value;
            if ((this.uniSelectorCountry != null) && (this.uniSelectorState != null))
            {
                this.uniSelectorCountry.MaxDisplayedItems = (value ? 300 : UniSelector.DefaultMaxDisplayedItems);
                this.uniSelectorState.MaxDisplayedItems = (value ? 100 : UniSelector.DefaultMaxDisplayedItems);

                this.uniSelectorCountry.MaxDisplayedTotalItems = this.uniSelectorCountry.MaxDisplayedItems + 50;
                this.uniSelectorState.MaxDisplayedTotalItems = this.uniSelectorState.MaxDisplayedItems + 50;
            }
        }
    }


    /// <summary>
    /// Add '(none)' record to the dropdownlist.
    /// </summary>
    public bool AddNoneRecord
    {
        get
        {
            return this.mAddNoneRecord;
        }
        set
        {
            this.mAddNoneRecord = value;
        }
    }


    /// <summary>
    /// Add '(select country)' record to the dropdownlist.
    /// </summary>
    public bool AddSelectCountryRecord
    {
        get
        {
            return this.mAddSelectCountryRecord;
        }
        set
        {
            this.mAddSelectCountryRecord = value;
        }
    }


    /// <summary>
    /// If set to true also state selection will be available in the control.
    /// </summary>
    public bool EnableStateSelection
    {
        get
        {
            return this.mEnableStateSelection;
        }
        set
        {
            this.mEnableStateSelection = value;
        }
    }


    /// <summary>
    /// Set/get Value property in the form 'CountryName;StateName' or 'CountryID;StateID'
    /// </summary>
    public bool UseCodeNameForSelection
    {
        get
        {
            return this.mUseCodeNameForSelection;
        }
        set
        {
            this.mUseCodeNameForSelection = value;
        }
    }


    /// <summary>
    /// Selected country ID.
    /// </summary>
    public int CountryID
    {
        get
        {
            if (this.UseCodeNameForSelection)
            {
                CountryInfo ci = CountryInfoProvider.GetCountryInfo(ValidationHelper.GetString(this.uniSelectorCountry.Value, String.Empty));
                if (ci != null)
                {
                    return ci.CountryID;
                }
            }
            else
            {
                return ValidationHelper.GetInteger(this.uniSelectorCountry.Value, 0);
            }

            return 0;
        }
        set
        {
            if (value > 0)
            {
                if (this.UseCodeNameForSelection)
                {
                    CountryInfo ci = CountryInfoProvider.GetCountryInfo(value);
                    if (ci != null)
                    {
                        this.uniSelectorCountry.Value = ci.CountryName;
                    }
                }
                else
                {
                    this.uniSelectorCountry.Value = value;
                }

                this.uniSelectorState.WhereCondition = "CountryID = " + value;
            }
        }
    }


    /// <summary>
    /// Selected State ID. Zero if not available.
    /// </summary>
    public int StateID
    {
        get
        {
            if (this.plcStates.Visible)
            {
                if (this.UseCodeNameForSelection)
                {
                    StateInfo si = StateInfoProvider.GetStateInfo(ValidationHelper.GetString(this.uniSelectorState.Value, String.Empty));
                    if (si != null)
                    {
                        return si.StateID;
                    }
                }
                else
                {
                    return ValidationHelper.GetInteger(this.uniSelectorState.Value, 0);
                }
            }
            return 0;
        }
        set
        {
            if (value > 0)
            {
                if (this.UseCodeNameForSelection)
                {
                    StateInfo si = StateInfoProvider.GetStateInfo(value);
                    if (si != null)
                    {
                        this.uniSelectorState.Value = si.StateName;
                    }
                }
                else
                {
                    this.uniSelectorState.Value = value;
                }
            }
        }
    }


    /// <summary>
    /// Selected Country name.
    /// </summary>
    public string CountryName
    {
        get
        {
            if (this.UseCodeNameForSelection)
            {
                return ValidationHelper.GetString(this.uniSelectorCountry.Value, String.Empty);
            }
            else
            {
                CountryInfo ci = CountryInfoProvider.GetCountryInfo(ValidationHelper.GetInteger(this.uniSelectorCountry.Value, 0));
                if (ci != null)
                {
                    return ci.CountryName;
                }
            }

            return String.Empty;
        }
        set
        {
            if (this.UseCodeNameForSelection)
            {
                this.uniSelectorCountry.Value = value;
            }
            else
            {
                CountryInfo ci = CountryInfoProvider.GetCountryInfo(value);
                if (ci != null)
                {
                    this.uniSelectorCountry.Value = ci.CountryID;
                }
            }
        }
    }


    /// <summary>
    /// Selected State name.
    /// </summary>
    public string StateName
    {
        get
        {
            if (this.plcStates.Visible)
            {
                if (this.UseCodeNameForSelection)
                {
                    return ValidationHelper.GetString(this.uniSelectorState.Value, String.Empty);
                }
                else
                {
                    StateInfo si = StateInfoProvider.GetStateInfo(ValidationHelper.GetInteger(this.uniSelectorState.Value, 0));
                    if (si != null)
                    {
                        return si.StateName;
                    }
                }
            }
            return String.Empty;
        }
        set
        {
            if (this.UseCodeNameForSelection)
            {
                this.uniSelectorState.Value = value;
            }
            else
            {
                StateInfo si = StateInfoProvider.GetStateInfo(value);
                if (si != null)
                {
                    this.uniSelectorState.Value = si.StateID;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            returnWhat = (ReturnType)ValidationHelper.GetInteger(GetValue("ReturnType"), 0);

            // Return only country ID
            if (returnWhat == ReturnType.CountryID)
            {
                return this.CountryID;
            }
            // Return only state ID
            else if (returnWhat == ReturnType.StateID)
            {
                return this.StateID;
            }
            // Return string with country and state IDs
            else
            {
                string val = null;
                if (this.UseCodeNameForSelection)
                {
                    val = (!string.IsNullOrEmpty(this.StateName)) ? this.CountryName + ";" + this.StateName : this.CountryName;
                }
                else
                {
                    val = (this.StateID > 0) ? this.CountryID.ToString() + ";" + this.StateID.ToString() : this.CountryID.ToString();
                }
                return (val == ";") ? null : val;
            }
        }
        set
        {
            // Return type
            returnWhat = (ReturnType)ValidationHelper.GetInteger(GetValue("ReturnType"), 0);

            // Load panel
            if ((this.uniSelectorCountry == null) || (this.uniSelectorState == null))
            {
                this.pnlUpdate.LoadContainer();
            }

            // Get only country ID
            if (returnWhat == ReturnType.CountryID)
            {
                this.CountryID = ValidationHelper.GetInteger(value, 0);
            }
            // Get only stateID
            else if (returnWhat == ReturnType.StateID)
            {
                this.StateID = ValidationHelper.GetInteger(value, 0);

                // Find country from state info
                StateInfo state = StateInfoProvider.GetStateInfo(this.StateID);
                if (state != null)
                {
                    this.CountryID = state.CountryID;
                }
            }
            // Get both country and state IDs
            else
            {
                string[] ids = ValidationHelper.GetString(value, "").Split(';');

                if (ids.Length >= 1)
                {
                    if (this.UseCodeNameForSelection)
                    {
                        this.CountryName = ValidationHelper.GetString(ids[0], "");
                    }
                    else
                    {
                        this.CountryID = ValidationHelper.GetInteger(ids[0], 0);
                    }
                    if (ids.Length == 2)
                    {
                        if (this.UseCodeNameForSelection)
                        {
                            this.StateName = ValidationHelper.GetString(ids[1], "");
                        }
                        else
                        {
                            this.StateID = ValidationHelper.GetInteger(ids[1], 0);
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Returns the DDL with countries.
    /// </summary>
    public DropDownList CountryDropDown
    {
        get
        {
            return this.uniSelectorCountry.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Returns the DDL with states.
    /// </summary>
    public DropDownList StateDropDown
    {
        get
        {
            return this.uniSelectorState.DropDownSingleSelect;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelectorCountry.StopProcessing = true;
            this.uniSelectorState.StopProcessing = true;
        }
        else
        {
            this.uniSelectorCountry.IsLiveSite = this.IsLiveSite;
            this.uniSelectorCountry.OnSelectionChanged += new EventHandler(uniSelectorCountry_OnSelectionChanged);
            this.uniSelectorCountry.DropDownSingleSelect.AutoPostBack = true;
            this.uniSelectorCountry.SelectionMode = SelectionModeEnum.SingleDropDownList;
            this.uniSelectorCountry.ReturnColumnName = (this.UseCodeNameForSelection ? "CountryName" : "CountryID");
            this.uniSelectorCountry.MaxDisplayedItems = (this.DisplayAllItems ? 300 : UniSelector.DefaultMaxDisplayedItems);
            this.uniSelectorCountry.MaxDisplayedTotalItems = this.uniSelectorCountry.MaxDisplayedItems + 50;

            string[,] fields = null;
            if (this.AddSelectCountryRecord && this.AddNoneRecord)
            {
                fields = new string[,] { 
                { GetString("countryselector.selectcountryrecord"), "" }, 
                { GetString("general.selectnone"), "" } 
            };
            }
            else
            {
                if (this.AddNoneRecord)
                {
                    fields = new string[,] { { GetString("general.selectnone"), "" } };
                }
                else if (this.AddSelectCountryRecord)
                {
                    fields = new string[,] { { GetString("countryselector.selectcountryrecord"), "" } };
                }
            }

            this.uniSelectorCountry.SpecialFields = fields;

            this.uniSelectorState.IsLiveSite = this.IsLiveSite;
            this.uniSelectorState.SelectionMode = SelectionModeEnum.SingleDropDownList;
            this.uniSelectorState.DropDownSingleSelect.AutoPostBack = true;
            this.uniSelectorState.ReturnColumnName = (this.UseCodeNameForSelection ? "StateName" : "StateID");
            this.uniSelectorState.MaxDisplayedItems = (this.DisplayAllItems ? 100 : UniSelector.DefaultMaxDisplayedItems);
            this.uniSelectorState.MaxDisplayedTotalItems = this.uniSelectorState.MaxDisplayedItems + 50;
            this.uniSelectorState.WhereCondition = "CountryID = " + CountryID;

            if (this.UseCodeNameForSelection)
            {
                this.uniSelectorState.AllRecordValue = String.Empty;
                this.uniSelectorState.NoneRecordValue = String.Empty;
                this.uniSelectorCountry.AllRecordValue = String.Empty;
                this.uniSelectorCountry.NoneRecordValue = String.Empty;
            }
        }
    }


    /// <summary>
    /// Hide States DDL if there is no state for selected country.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.CountryID > 0)
        {
            this.uniSelectorState.StopProcessing = false;
            this.plcStates.Visible = this.mEnableStateSelection && this.uniSelectorState.HasData;
        }
        else
        {
            this.plcStates.Visible = false;
            this.uniSelectorState.StopProcessing = true;
        }
    }


    /// <summary>
    /// Country DropDownList Selection change.
    /// </summary>
    protected void uniSelectorCountry_OnSelectionChanged(object sender, EventArgs e)
    {
        if (this.CountryID > 0)
        {
            this.uniSelectorState.WhereCondition = "CountryID = " + CountryID;

            this.uniSelectorState.StopProcessing = false;
            this.uniSelectorState.Reload(true);

            // Raise change event
            RaiseOnChanged();
        }
        else
        {
            this.uniSelectorState.StopProcessing = true;
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
    /// <param name="forceReload">If true uniselectors are reloaded</param>
    public void ReloadData(bool forceReload)
    {
        this.uniSelectorCountry.Reload(forceReload);

        int countryId = 0;
        if (this.UseCodeNameForSelection)
        {
            CountryInfo ci = CountryInfoProvider.GetCountryInfo(ValidationHelper.GetString(this.uniSelectorCountry.Value, ""));
            if (ci != null)
            {
                countryId = ci.CountryID;
            }
        }
        else
        {
            countryId = ValidationHelper.GetInteger(this.uniSelectorCountry.Value, 0);
        }
        this.uniSelectorState.WhereCondition = "CountryID = " + countryId;

        this.uniSelectorState.Reload(forceReload);
    }

    #endregion
}