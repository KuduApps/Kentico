using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Community;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.FormEngine;

public partial class CMSModules_Groups_FormControls_CommunityGroupSelector : FormEngineUserControl, IDataUserControl
{
    #region "Private variables"

    private string mGroupCodeName = String.Empty;
    private bool mDisplayCurrentGroup = true;
    private bool mDisplayNoneWhenEmpty = false;
    private bool mDisplayNoneValue = false;
    private bool mUseDisplayNames = false;
    private string mWhereCondition = "";
    private string mNoneValueText = "";

    private bool specialFieldsAdded = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the current site.
    /// </summary>
    public int SiteID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["SiteID"], CMSContext.CurrentSiteID);
        }
        set
        {
            ViewState["SiteID"] = value;
        }
    }


    /// <summary>
    /// ID of the currently selected group.
    /// </summary>
    public int GroupID
    {
        get
        {
            return ValidationHelper.GetInteger(usGroups.Value, 0);
        }
        set
        {
            usGroups.Value = value.ToString();
        }
    }


    /// <summary>
    /// Indicates if '(global)' value should be displayed.
    /// </summary>
    public bool DisplayGlobalValue
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["DisplayGlobalValue"], false);
        }
        set
        {
            ViewState["DisplayGlobalValue"] = value;
        }
    }


    /// <summary>
    /// Gets or sets text of 'none' item in the selector.
    /// </summary>
    public string NoneValueText
    {
        get
        {
            if (this.mNoneValueText == "")
            {
                this.mNoneValueText = GetString("general.empty");
            }
            return this.mNoneValueText;
        }
        set
        {
            this.mNoneValueText = value;
        }
    }


    /// <summary>
    /// Gets or sets text of global item in the selector.
    /// </summary>
    public string GlobalValueText
    {
        get
        {
            if (string.IsNullOrEmpty(ValidationHelper.GetString(this.GetValue("GlobalValueText"), String.Empty)))
            {
                this.SetValue("GlobalValueText", GetString("general.global"));
            }

            return ValidationHelper.GetString(this.GetValue("GlobalValueText"), String.Empty);
        }
        set
        {
            this.SetValue("GlobalValueText", value);
        }
    }


    /// <summary>
    /// Indicates whether show display or code names of groups.
    /// </summary>
    public bool UseDisplayNames
    {
        get
        {
            return mUseDisplayNames;
        }
        set
        {
            mUseDisplayNames = value;
        }
    }


    /// <summary>
    /// Gets or sets if groups drop-down list should be enabled.
    /// </summary>
    public bool EnabledGroups
    {
        get
        {
            return usGroups.Enabled;
        }
        set
        {
            usGroups.Enabled = value;
        }
    }


    /// <summary>
    /// Indicates whether the '(none)' value should be displayed.
    /// </summary>
    public bool DisplayNoneValue
    {
        get
        {
            return this.mDisplayNoneValue;
        }
        set
        {
            this.mDisplayNoneValue = value;
        }
    }


    /// <summary>
    /// Indicates whether 'none' item should be added automatically when no groups exist for selector.
    /// </summary>
    public bool DisplayNoneWhenEmpty
    {
        get
        {
            return this.mDisplayNoneWhenEmpty;
        }
        set
        {
            this.mDisplayNoneWhenEmpty = value;
        }
    }


    /// <summary>
    /// Gets or sets WHERE condition to filter data.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return this.mWhereCondition;
        }
        set
        {
            this.mWhereCondition = value;
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
            EnsureChildControls();
            base.Enabled = value;
            this.usGroups.Enabled = value;
        }

    }

    /// <summary>
    /// Gets the currene UniSelector instance.
    /// </summary>
    public UniSelector CurrentSelector
    {
        get
        {
            EnsureChildControls();
            return this.usGroups;
        }
    }


    /// <summary>
    /// Group code name.
    /// </summary>
    public string GroupCodeName
    {
        get
        {
            EnsureChildControls();
            return Convert.ToString(usGroups.Value);
        }
        set
        {
            EnsureChildControls();
            usGroups.Value = value;
        }
    }


    /// <summary>
    /// Gets the current drop down control.
    /// </summary>
    public DropDownList CurrentDropDown
    {
        get
        {
            EnsureChildControls();
            return usGroups.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets or sets value name - default groupname.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return Convert.ToString(usGroups.Value);
        }
        set
        {
            EnsureChildControls();
            usGroups.Value = value;
        }
    }


    /// <summary>
    /// Gets ClientID of the dropdownlist with stylesheets.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            EnsureChildControls();
            return usGroups.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets if live iste property.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            EnsureChildControls();
            return ValidationHelper.GetBoolean(ViewState["IsLiveSite"], true);
        }
        set
        {
            EnsureChildControls();
            ViewState["IsLiveSite"] = value;
            usGroups.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates whether the current group option should be added to the list.
    /// </summary>
    public bool DisplayCurrentGroup
    {
        get
        {
            return this.mDisplayCurrentGroup;
        }
        set
        {
            this.mDisplayCurrentGroup = value;
        }
    }

    #endregion


    #region "SetValue/GetValue overrides"

    /// <summary>
    /// Sets the property value of the control, setting the value affects only local property value.
    /// </summary>
    /// <param name="propertyName">Property name to set</param>
    /// <param name="value">New property value</param>
    public override void SetValue(string propertyName, object value)
    {
        switch (propertyName.ToLower())
        {
            case "groupid":
                this.EnsureChildControls();
                usGroups.Value = value;
                break;
            case "reloaddata":
                ReloadData();
                break;
            case "enabledgroups":
                this.EnabledGroups = ValidationHelper.GetBoolean(value, false);
                break;
            case "usedisplaynames":
                this.UseDisplayNames = ValidationHelper.GetBoolean(value, false);
                break;
            case "wherecondition":
                this.WhereCondition = ValidationHelper.GetString(value, "");
                break;
            case "displaynonevalue":
                this.DisplayNoneValue = ValidationHelper.GetBoolean(value, false);
                break;
            case "nonevaluetext":
                this.NoneValueText = ValidationHelper.GetString(value, "");
                break;
            case "displaynonewhenempty":
                this.DisplayNoneWhenEmpty = ValidationHelper.GetBoolean(value, false);
                break;
            case "displayglobalvalue":
                this.DisplayGlobalValue = ValidationHelper.GetBoolean(value, false);
                break;
            case "displaycurrentgroup":
                this.DisplayCurrentGroup = ValidationHelper.GetBoolean(value, false);
                break;
            case "siteid":
                this.SiteID = ValidationHelper.GetInteger(value, 0);
                break;
        }

        base.SetValue(propertyName, value);
    }


    /// <summary>
    /// Returns the value of the given webpart property property.
    /// </summary>
    /// <param name="propertyName">Property name</param>
    public override object GetValue(string propertyName)
    {
        switch (propertyName.ToLower())
        {
            case "groupid":
                return ValidationHelper.GetInteger(usGroups.Value, 0);
            case "enabledgroups":
                return this.EnabledGroups;
            case "usedisplaynames":
                return this.UseDisplayNames;
            case "wherecondition":
                return this.WhereCondition;
            case "displaynonevalue":
                return this.DisplayNoneValue;
            case "nonevaluetext":
                return this.NoneValueText;
            case "displaynonewhenempty":
                return this.DisplayNoneWhenEmpty;
            case "displayglobalvalue":
                return this.DisplayGlobalValue;
            case "currentselector":
                return this.CurrentSelector;
            case "currentdropdown":
                return this.CurrentDropDown;
            case "displaycurrentgroup":
                return this.DisplayCurrentGroup;
            case "siteid":
                return this.SiteID;
        }

        return base.GetValue(propertyName);
    }

    #endregion


    #region "Other methods/events"

    /// <summary>
    /// Page_Init event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected override void OnInit(EventArgs e)
    {
        this.SetValue("CurrentSelector", this.CurrentSelector);
    }


    /// <summary>
    /// Page_load event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Return GroupID if field type is integer
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            usGroups.ReturnColumnName = "GroupID";
            usGroups.AllowEmpty = true;
            DisplayCurrentGroup = DisplayNoneValue = DisplayNoneWhenEmpty = false;
        }

        if (StopProcessing)
        {
            usGroups.StopProcessing = true;
        }
        else
        {
            EnsureChildControls();
            AddSpecialFields();

            // Ensure correct livesite setting for uniselector
            IsLiveSite = IsLiveSite;

            usGroups.WhereCondition = BuildWhereCondition();
            usGroups.OnSelectionChanged += new EventHandler(usGroups_OnSelectionChanged);
        }
    }

    /// <summary>
    /// Selection changed.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    void usGroups_OnSelectionChanged(object sender, EventArgs e)
    {
        RaiseOnChanged();
    }


    /// <summary>
    /// Creates child controls and loads update panel container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (usGroups == null)
        {
            this.pnlUpdate.LoadContainer();
        }

        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Reloads groups for the specified site.
    /// </summary>
    /// <param name="forceLoad">Indicates whether the load should be forced</param>
    public void ReloadData(bool forceLoad)
    {
        ReloadData();
    }


    /// <summary>
    /// Reloads groups for the specified site.
    /// </summary>
    public void ReloadData()
    {
        string[,] specialFields = new string[3, 2];
        int index = 0;

        // Add "current group" option
        if (this.DisplayCurrentGroup && usGroups.SpecialFields == null)
        {
            specialFields[index, 0] = GetString("group.current");
            specialFields[index, 1] = GroupInfoProvider.CURRENT_GROUP;
            index++;
        }

        // Add default item
        if (DisplayGlobalValue)
        {
            specialFields[index, 0] = this.GlobalValueText;
            specialFields[index, 1] = "0";
            index++;
        }

        // Add none item if required
        if (this.DisplayNoneValue || (this.DisplayNoneWhenEmpty && !usGroups.HasData))
        {
            specialFields[index, 0] = this.NoneValueText;
            specialFields[index, 1] = "-1";
        }

        // Handle on selection changed
        usGroups.SpecialFields = specialFields;

        usGroups.DropDownSingleSelect.ClearSelection();
        usGroups.DropDownSingleSelect.Items.Clear();

        AddSpecialFields();
        usGroups.WhereCondition = BuildWhereCondition();
        usGroups.Reload(true);

        if (!RequestHelper.IsPostBack())
        {
            RaiseOnChanged();
        }
    }


    /// <summary>
    /// Builds wehre condition.
    /// </summary>
    /// <returns></returns>
    public string BuildWhereCondition()
    {
        string where = string.Empty;

        if (SiteID > 0)
        {
            where = "GroupSiteID=" + SiteID + " AND GroupApproved = 1";
        }

        // If the user is global admin display all the site groups otherwise display just user's groups
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            if (where != null)
            {
                if (!string.IsNullOrEmpty(where))
                {
                    where += " AND ";
                }
                where += "(GroupID IN (SELECT MemberGroupID FROM Community_GroupMember WHERE MemberUserID=" +
                    CMSContext.CurrentUser.UserID + "))";
            }
        }

        // Include custom WHERE condition if specified
        if (!string.IsNullOrEmpty(this.WhereCondition))
        {
            if (!string.IsNullOrEmpty(where))
            {
                where += " AND ";
            }
            where += "(" + this.WhereCondition + ")";
        }

        return where;
    }


    /// <summary>
    /// Adds special fields to selector.
    /// </summary>
    private void AddSpecialFields()
    {
        if (!specialFieldsAdded)
        {
            string[,] specialFields = new string[3, 2];
            int index = 0;

            // Add "current group" option
            if (this.DisplayCurrentGroup && usGroups.SpecialFields == null)
            {
                specialFields[index, 0] = GetString("group.current");
                specialFields[index, 1] = GroupInfoProvider.CURRENT_GROUP;
                index++;
            }

            // Add default item
            if (DisplayGlobalValue)
            {
                specialFields[index, 0] = this.GlobalValueText;
                specialFields[index, 1] = "0";
                index++;
            }

            // Add none item if required
            if (this.DisplayNoneValue || (this.DisplayNoneWhenEmpty && !usGroups.HasData))
            {
                specialFields[index, 0] = this.NoneValueText;
                specialFields[index, 1] = "-1";
            }

            usGroups.SpecialFields = specialFields;
            specialFieldsAdded = true;
        }
    }

    /// <summary>
    /// Returns WHERE condition for selected form.
    /// </summary>
    public override string GetWhereCondition()
    {
        // Return correct WHERE condition for integer if none value is selected
        if ((this.FieldInfo != null) && ((this.FieldInfo.DataType == FormFieldDataTypeEnum.Integer) || (this.FieldInfo.DataType == FormFieldDataTypeEnum.LongInteger)))
        {
            int id = ValidationHelper.GetInteger(usGroups.Value, 0);
            if (id > 0)
            {
                return base.GetWhereCondition();
            }
        }
        return null;
    }

    #endregion
}
