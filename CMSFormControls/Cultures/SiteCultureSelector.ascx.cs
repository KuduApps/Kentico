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

using CMS.FormControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.PortalControls;
using CMS.SettingsProvider;

public partial class CMSFormControls_Cultures_SiteCultureSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mDisplayAllCultures = false;
    private bool mUseCultureCode = true;
    private bool mAddDefaultRecord = true;
    private bool mAddAllRecord = false;
    private int mSiteID = 0;
    private bool mPostbackOnChange = false;

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
            if (this.uniSelector != null)
            {
                this.uniSelector.Enabled = value;
            }
        }
    }


    /// <summary>
    /// Returns ClientID of the dropdown with cultures.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect.ClientID;
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
            if (uniSelector == null)
            {
                this.pnlUpdate.LoadContainer();
            }
            this.uniSelector.Value = value;
        }
    }


    /// <summary>
    /// Gets the inner UniSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return uniSelector;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to display all cultures 
    /// (if you set this to True, SiteID property is ignored).
    /// </summary>
    public bool DisplayAllCultures
    {
        get
        {
            return this.mDisplayAllCultures;
        }
        set
        {
            this.mDisplayAllCultures = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.WhereCondition = (this.DisplayAllCultures ? "" : GetWhereConditionInternal());
            }
        }
    }


    /// <summary>
    ///  If true, returned value is CultureCode, if false, selected value is CultureID.
    /// </summary>
    public bool UseCultureCode
    {
        get
        {
            return mUseCultureCode;
        }
        set
        {
            mUseCultureCode = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.ReturnColumnName = (value ? "CultureCode" : "CultureID");
            }
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add (default) item record to the dropdownlist.
    /// </summary>
    public bool AddDefaultRecord
    {
        get
        {
            return mAddDefaultRecord;
        }
        set
        {
            mAddDefaultRecord = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.AllowEmpty = value;
            }
        }
    }


    /// <summary>
    /// Gets/Sets all record value
    /// </summary>
    public string AllRecordValue
    {
        get
        {
            return uniSelector.AllRecordValue;
        }
        set
        {
            uniSelector.AllRecordValue = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines, whether to add (all) item record to the dropdownlist.
    /// </summary>
    public bool AddAllRecord
    {
        get
        {
            return mAddAllRecord;
        }
        set
        {
            mAddAllRecord = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.AllowAll = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the ID of the site for which the cultures should be returned. 0 means current site.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteID;
        }
        set
        {
            this.mSiteID = value;
            if (this.uniSelector != null)
            {
                this.uniSelector.WhereCondition = GetWhereConditionInternal();
            }
        }
    }


    /// <summary>
    /// Returns UpdatePanel of selector.
    /// </summary>
    public CMSUpdatePanel UpdatePanel
    {
        get
        {
            return pnlUpdate;
        }
    }


    /// <summary>
    /// Returns DropDownList with cultures.
    /// </summary>
    public DropDownList DropDownCultures
    {
        get
        {
            return this.uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets or sets special fields for UniSelector dropdown. 
    /// (Set AddDefaultRecord to false if you use this property)
    /// </summary>
    public string[,] SpecialFields
    {
        get
        {
            return this.uniSelector.SpecialFields;
        }
        set
        {
            if (this.uniSelector != null)
            {
                this.uniSelector.SpecialFields = value;
            }
        }
    }


    /// <summary>
    /// If true, full postback is raised when item changed.
    /// </summary>
    public bool PostbackOnChange
    {
        get
        {
            return mPostbackOnChange;
        }
        set
        {
            mPostbackOnChange = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            if (PostbackOnChange)
            {
                uniSelector.DropDownSingleSelect.AutoPostBack = true;
                ScriptManager scr = ScriptManager.GetCurrent(Page);
                scr.RegisterPostBackControl(uniSelector.DropDownSingleSelect);
            }

            ReloadData();
        }
    }

    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        this.uniSelector.IsLiveSite = this.IsLiveSite;

        if (!this.DisplayAllCultures)
        {
            uniSelector.WhereCondition = GetWhereConditionInternal();
        }

        this.uniSelector.ReturnColumnName = (this.UseCultureCode ? "CultureCode" : "CultureID");


        this.uniSelector.AllowAll = this.AddAllRecord;

        if (this.AddDefaultRecord)
        {
            this.uniSelector.SpecialFields = new string[,] { { GetString("general.defaultchoice"), "" } };
        }
    }


    /// <summary>
    /// Reloads uniselector.
    /// </summary>
    /// <param name="forceReload"></param>
    public void Reload(bool forceReload)
    {
        uniSelector.Reload(forceReload);
    }


    /// <summary>
    /// Returns WHERE condition for given site.
    /// </summary>
    private string GetWhereConditionInternal()
    {
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
}
