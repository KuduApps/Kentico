using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.UIControls;

public partial class CMSFormControls_SimpleCountrySelector : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets state enable.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return this.countrySelector.Enabled;
        }
        set
        {
            this.countrySelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which indicates whether the selector should load all data to DDL.
    /// </summary>
    public bool DisplayAllItems
    {
        get
        {
            return this.countrySelector.DisplayAllItems;
        }
        set
        {
            this.countrySelector.DisplayAllItems = value;
        }
    }


    /// <summary>
    /// Add '(none)' record to the dropdownlist.
    /// </summary>
    public bool AddNoneRecord
    {
        get
        {
            return this.countrySelector.AddNoneRecord;
        }
        set
        {
            this.countrySelector.AddNoneRecord = value;
        }
    }


    /// <summary>
    /// Add '(select country)' record to the dropdownlist.
    /// </summary>
    public bool AddSelectCountryRecord
    {
        get
        {
            return this.countrySelector.AddSelectCountryRecord;
        }
        set
        {
            this.countrySelector.AddSelectCountryRecord = value;
        }
    }


    /// <summary>
    /// Set/get Value property in the form 'CountryName;StateName' or 'CountryID;StateID'
    /// </summary>
    public bool UseCodeNameForSelection
    {
        get
        {
            return this.countrySelector.UseCodeNameForSelection;
        }
        set
        {
            this.countrySelector.UseCodeNameForSelection = value;
        }
    }


    /// <summary>
    /// Selected country ID.
    /// </summary>
    public int CountryID
    {
        get
        {
            return this.countrySelector.CountryID;
        }
        set
        {
            this.countrySelector.CountryID = value;
        }
    }


    /// <summary>
    /// Selected Country name.
    /// </summary>
    public string CountryName
    {
        get
        {
            return this.countrySelector.CountryName;
        }
        set
        {
            this.countrySelector.CountryName = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.countrySelector.Value;
        }
        set
        {
            this.countrySelector.Value = value;
        }
    }


    /// <summary>
    /// Returns the DDL with countries.
    /// </summary>
    public DropDownList CountryDropDown
    {
        get
        {
            return this.countrySelector.CountryDropDown;
        }
    }


    /// <summary>
    /// Gets client ID of the country drop down list.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.countrySelector.ValueElementID;
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion
}
