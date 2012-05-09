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
using CMS.UIControls;
using CMS.FormControls;
using CMS.TreeEngine;

public partial class CMSFormControls_Cultures_SelectCulture : FormEngineUserControl
{
    #region "Variables"

    private bool mDisplayClearButton = true;
    private int mSiteID = 0;
    private string mReturnColumnName = "CultureCode";
    private bool mDisplayAllValue = false;

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
            if (this.uniSelector != null)
            {
                this.uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Returns ClientID of the textbox with culture.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            EnsureChildControls();
            return this.uniSelector.TextBoxSelect.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.uniSelector.Value;
        }
        set
        {
            EnsureChildControls();
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }
            this.uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to display Clear button.
    /// </summary>
    public bool DisplayClearButton
    {
        get
        {
            return this.mDisplayClearButton;
        }
        set
        {
            EnsureChildControls();
            this.mDisplayClearButton = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.AllowEmpty = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the ID of the site for which the cultures should be returned. 0 means current site. -1 all sites = all cultures.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteID;
        }
        set
        {
            EnsureChildControls();
            this.mSiteID = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.WhereCondition = GetWhereConditionInternal();
            }
        }
    }


    /// <summary>
    /// Gets current uniselector control.
    /// </summary>
    public UniSelector CurrentSelector
    {
        get
        {
            EnsureChildControls();
            return this.uniSelector;
        }
    }


    /// <summary>
    /// Gets or sets column name, default value is culture name.
    /// </summary>
    public string ReturnColumnName
    {
        get
        {
            return mReturnColumnName;
        }
        set
        {
            mReturnColumnName = value;
        }
    }


    /// <summary>
    /// Gets or sets whether (all) field is in dropdownlist.
    /// </summary>
    public bool DisplayAllValue
    {
        get
        {
            return mDisplayAllValue;
        }
        set
        {
            mDisplayAllValue = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            EnsureChildControls();
            this.uniSelector.StopProcessing = true;
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
        if (DisplayAllValue)
        {
            string[,] specialFields = new string[1, 2];
            specialFields[0, 0] = GetString("general.selectall");
            specialFields[0, 1] = "0";
            this.uniSelector.SpecialFields = specialFields;
        }

        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.DisplayNameFormat = "{%CultureName%} ({%CultureCode%})";
        
        // Set default where condition
        string where = GetWhereConditionInternal();
        if (!String.IsNullOrEmpty(where))
        {
            this.uniSelector.WhereCondition = where;
        }

        this.uniSelector.ReturnColumnName = this.ReturnColumnName;
        this.uniSelector.AllowEmpty = this.DisplayClearButton;
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        // If macro or special value, do not validate
        string value = this.uniSelector.TextBoxSelect.Text.Trim();
        if (!ContextResolver.ContainsMacro(value) && (value != "") && (value != TreeProvider.ALL_CULTURES))
        {
            // Check if culture exists
            CultureInfo ci = CultureInfoProvider.GetCultureInfo(value);
            if (ci == null)
            {
                this.ValidationError = GetString("formcontrols_selectculture.notexist").Replace("%%code%%", value);
                return false;
            }
            else
            {
                return true;
            }
        }
        return true;
    }


    /// <summary>
    /// Returns WHERE condition for given site.
    /// </summary>
    private string GetWhereConditionInternal()
    {
        // If site id is -1 return all cultures
        if (this.SiteID < 0)
        {
            return string.Empty;
        }

        string retval = "CultureID IN (SELECT CultureID FROM CMS_SiteCulture WHERE SiteID = ";
        if (this.SiteID > 0)
        {
            retval += this.SiteID;
        }
        else
        {
            retval += CMSContext.CurrentSiteID;
        }
        retval += ")";

        return retval;
    }


    /// <summary>
    /// Creates child controls and loads update panel container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (uniSelector == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }
}
