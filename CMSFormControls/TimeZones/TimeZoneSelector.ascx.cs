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
using CMS.CMSHelper;
using CMS.SiteProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSFormControls_TimeZones_TimeZoneSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Variables"

    private bool mUseZoneNameForSelection = true;
    private bool mAddNoneItemsRecord = true;
    private string mTimeZoneName = "";
    private int mTimeZoneId = 0;

    #endregion


    #region "Methods"

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
            this.drpTimeZoneSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (this.UseZoneNameForSelection)
            {
                return this.TimeZoneName;
            }
            else
            {
                if (TimeZoneID == 0)
                {
                    return null;
                }

                return this.TimeZoneID;
            }
        }
        set
        {
            if (this.UseZoneNameForSelection)
            {
                this.TimeZoneName = ValidationHelper.GetString(value, "");
            }
            else
            {
                this.TimeZoneID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }

    
    /// <summary>
    /// Gets or sets the TagGroup code name.
    /// </summary>
    public string TimeZoneName
    {
        get
        {
            if (this.UseZoneNameForSelection)
            {
                return ValidationHelper.GetString(this.drpTimeZoneSelector.SelectedValue, "");
            }
            else
            {
                TimeZoneInfo tzi = TimeZoneInfoProvider.GetTimeZoneInfo(ValidationHelper.GetInteger(this.drpTimeZoneSelector.SelectedValue, 0));
                if (tzi != null)
                {
                    return tzi.TimeZoneName;
                }
                return "";
            }
        }
        set
        {
            if (this.UseZoneNameForSelection)
            {
                SelectValue(value);
                this.mTimeZoneName = value;
            }
            else
            {
                TimeZoneInfo tzi = TimeZoneInfoProvider.GetTimeZoneInfo(value);
                if (tzi != null)
                {
                    SelectValue(tzi.TimeZoneID.ToString());
                    this.mTimeZoneId = tzi.TimeZoneID;
                }
            }
        }
    }


    /// <summary>
    /// Gets or sets the TagGroup ID.
    /// </summary>
    public int TimeZoneID
    {
        get
        {
            if (this.UseZoneNameForSelection)
            {
                string name = ValidationHelper.GetString(this.drpTimeZoneSelector.SelectedValue, "");
                TimeZoneInfo tzi = TimeZoneInfoProvider.GetTimeZoneInfo(name);
                if (tzi != null)
                {
                    return tzi.TimeZoneID;
                }
                return 0;
            }
            else
            {
                return ValidationHelper.GetInteger(this.drpTimeZoneSelector.SelectedValue, 0);
            }
        }
        set
        {
            if (this.UseZoneNameForSelection)
            {
                TimeZoneInfo tzi = TimeZoneInfoProvider.GetTimeZoneInfo(value);
                if (tzi != null)
                {
                    SelectValue(tzi.TimeZoneName);
                    this.mTimeZoneName = tzi.TimeZoneName;
                }
            }
            else
            {
                SelectValue(value.ToString());
                this.mTimeZoneId = value;
            }
        }
    }


    /// <summary>
    ///  If true, selected value is TimeZoneName, if false, selected value is TimeZoneID.
    /// </summary>
    public bool UseZoneNameForSelection
    {
        get
        {
            if ((FieldInfo != null)&&(FieldInfo.DataType == CMS.FormEngine.FormFieldDataTypeEnum.Integer))
            {
                mUseZoneNameForSelection = false;
            }
            
            return mUseZoneNameForSelection;
        }
        set
        {
            mUseZoneNameForSelection = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add none item record to the dropdownlist.
    /// </summary>
    public bool AddNoneItemsRecord
    {
        get
        {
            return mAddNoneItemsRecord;
        }
        set
        {
            mAddNoneItemsRecord = value;
        }
    }


    /// <summary>
    /// Returns ClientID of the DropDownList with timezone.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.drpTimeZoneSelector.ClientID;
        }
    }

    #endregion


    #region "Events"
    
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    

    /// <summary>
    /// CreateChildControls() override.
    /// </summary>
    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Loads public status according to the control settings.
    /// </summary>
    public void ReloadData()
    {
        // Cleanup
        this.drpTimeZoneSelector.ClearSelection();
        this.drpTimeZoneSelector.SelectedValue = null;

        // Populate the dropdownlist
        if (this.UseZoneNameForSelection)
        {
            FillDropdown("TimeZoneName");
        }
        else
        {
            FillDropdown("TimeZoneID");
        }

        // Try to preselect the value
        if (this.UseZoneNameForSelection)
        {
            SelectValue(this.mTimeZoneName);
        }
        else
        {
            SelectValue(this.mTimeZoneId.ToString());
        }
    }


    /// <summary>
    /// Tries to select the specified value in drpTimeZoneSelector.
    /// </summary>
    private void SelectValue(string val)
    {
        try
        {
            this.drpTimeZoneSelector.SelectedValue = val;
        }
        catch { }
    }


    /// <summary>
    /// Fill drop down list.
    /// </summary>
    /// <param name="ds">Data set with time zones</param>
    /// <param name="dataValue">Data value field</param>
    private void FillDropdown(string dataValue)
    {
        if (drpTimeZoneSelector.Items.Count == 0)
        {
            DataSet ds = TimeZoneInfoProvider.GetTimeZones(null, "TimeZoneGMT", -1, "TimeZoneID ,TimeZoneGMT, TimeZoneName, TimeZoneDisplayName");

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                string text = null;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    text = String.Format("(GMT{0:+00.00;-00.00}) {1}", row["TimeZoneGMT"], row["TimeZoneDisplayName"]);
                    this.drpTimeZoneSelector.Items.Add(new ListItem(text, row[dataValue].ToString()));
                }
            }

            // Add none record if needed
            if (this.AddNoneItemsRecord)
            {
                this.drpTimeZoneSelector.Items.Insert(0, new ListItem(GetString("General.SelectNone"), ""));
            }
        }
    }

    #endregion
}
