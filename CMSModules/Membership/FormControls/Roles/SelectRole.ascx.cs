using System;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Membership_FormControls_Roles_SelectRole : FormEngineUserControl
{
    #region "Variables"

    private int mSiteId = 0;
    private int mGroupId = 0;
    private bool mUseFriendlyMode = false;
    private bool mUseCodeNameForSelection = true;
    private bool mShowSiteFilter = true;
    private bool mSimpleMode = false;
    private bool mAllowEmpty = true;
    private bool mGlobalRoles = true;
    private bool mSiteRoles = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether global objects have suffix "(global)" in the grid.
    /// </summary>
    public bool AddGlobalObjectSuffix 
    {
        get
        {
            EnsureChildControls();
            return usRoles.AddGlobalObjectSuffix;
        }
        set
        {
            EnsureChildControls();
            usRoles.AddGlobalObjectSuffix = value;
        }
    }


    /// <summary>
    /// Indicates if role selector allow empty selection.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return this.mAllowEmpty;
        }
        set
        {
            this.mAllowEmpty = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the group
    /// (search will be only among group roles if this property is nonzero) 
    /// </summary>
    public int GroupID
    {
        get
        {
            return mGroupId;
        }
        set
        {
            mGroupId = value;
        }
    }


    /// <summary>
    /// If true site roles are selected.
    /// </summary>
    public bool SiteRoles
    {
        get
        {
            return mSiteRoles;
        }
        set
        {
            mSiteRoles = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the site to display:
    /// (-1 means CurrentSite, 0 means you can choose, > 0 means specific site)
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
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
            this.usRoles.Enabled = value;
        }
    }


    /// <summary>
    /// Gets the current UniSelector instance.
    /// </summary>
    public UniSelector CurrentSelector
    {
        get
        {
            EnsureChildControls();
            return this.usRoles;
        }
    }


    /// <summary>
    /// Gets dialog button.
    /// </summary>
    public Button DialogButton
    {
        get
        {
            EnsureChildControls();
            return this.usRoles.ButtonDialog;
        }
    }


    /// <summary>
    /// Gets or sets role name.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return Convert.ToString(usRoles.Value);
        }
        set
        {
            EnsureChildControls();
            usRoles.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether role should be edited in friendly format.
    /// </summary>
    public bool UseFriendlyMode
    {
        get
        {
            return mUseFriendlyMode;
        }
        set
        {
            EnsureChildControls();
            mUseFriendlyMode = value;

            // Change the selector settings for (un)friendly mode
            if (value)
            {
                usRoles.AllowEditTextBox = false;
            }
            else
            {
                usRoles.AllowEditTextBox = true;
            }

        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether to use code name as return value.
    /// </summary>
    public bool UseCodeNameForSelection
    {
        get
        {
            return mUseCodeNameForSelection;
        }
        set
        {
            mUseCodeNameForSelection = value;
        }
    }


    /// <summary>
    /// Gets or sets if site filter should be shown or not.
    /// </summary>
    public bool ShowSiteFilter
    {
        get
        {
            return mShowSiteFilter;
        }
        set
        {
            mShowSiteFilter = value;
        }
    }


    /// <summary>
    /// Gets or sets if control works in simple mode (ignores SiteID, GroupID parameters).
    /// </summary>
    public bool SimpleMode
    {
        get
        {
            return mSimpleMode;
        }
        set
        {
            mSimpleMode = value;
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
            return base.IsLiveSite;
        }
        set
        {
            EnsureChildControls();
            base.IsLiveSite = value;
            usRoles.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets image dialog.
    /// </summary>
    public Image ImageDialog
    {
        get
        {
            EnsureChildControls();
            return this.usRoles.ImageDialog;
        }
    }


    /// <summary>
    /// Gets hyperlink dialog.
    /// </summary>
    public HyperLink LinkDialog
    {
        get
        {
            EnsureChildControls();
            return this.usRoles.LinkDialog;
        }
    }


    /// <summary>
    /// Gets the drop down control.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            EnsureChildControls();
            return this.usRoles.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Returns ClientID of the textboxselect.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.usRoles.TextBoxSelect.ClientID;
        }
    }


    /// <summary>
    /// If true global roles are selected.
    /// </summary>
    public bool GlobalRoles
    {
        get
        {
            return mGlobalRoles;
        }
        set
        {
            mGlobalRoles = value;
        }
    }


    /// <summary>
    /// Indicates if generic role 'Everyone' should be displayed even if other generic roles are hidden.
    /// </summary>
    public bool DisplayEveryone
    {
        get;
        set;
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Page_Load event.
    /// </summary>    
    protected void Page_Load(object sender, EventArgs e)
    {
        // If current control context is widget or livesite hide site selector
        if (ControlsHelper.CheckControlContext(this, ControlContext.WIDGET_PROPERTIES) || ControlsHelper.CheckControlContext(this, ControlContext.LIVE_SITE))
        {
            this.ShowSiteFilter = false;
        }

        Reload(false);
    }


    /// <summary>
    /// Reloads the selector's data.
    /// </summary>
    /// <param name="forceReload">Indicates whether data should be forcibly reloaded</param>
    public void Reload(bool forceReload)
    {
        // Set allow empty
        usRoles.AllowEmpty = this.AllowEmpty;

        // Get siteID
        if (this.SiteID == 0)
        {
            this.SiteID = CMSContext.CurrentSiteID;
        }

        // Add sites filter
        if (ShowSiteFilter)
        {
            this.usRoles.FilterControl = "~/CMSFormControls/Filters/SiteFilter.ascx";
            this.usRoles.SetValue("DefaultFilterValue", this.SiteID);
            this.usRoles.SetValue("FilterMode", "role");
        }


        // Build where condition
        string whereCondition = (this.GroupID > 0) ? "(RoleGroupID = " + GroupID.ToString() + ") " : "(RoleGroupID IS NULL)";

        if (this.UseFriendlyMode)
        {
            if (this.DisplayEveryone)
            {
                whereCondition += " AND RoleName != '" + RoleInfoProvider.AUTHENTICATED + "' AND RoleName != '" + RoleInfoProvider.NOTAUTHENTICATED + "' ";
            }
            else
            {
                whereCondition += " AND RoleName != '" + RoleInfoProvider.EVERYONE + "' AND RoleName != '" + RoleInfoProvider.AUTHENTICATED + "' AND RoleName != '" + RoleInfoProvider.NOTAUTHENTICATED + "' ";
            }
        }

        if (!ShowSiteFilter)
        {
            if (SiteRoles && GlobalRoles)
            {
                whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "SiteID IS NULL OR  SiteID =" + SiteID.ToString());
            }
            else if (SiteRoles)
            {
                whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "SiteID =" + SiteID.ToString());
            }
            else if (GlobalRoles)
            {
                whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, "SiteID IS NULL");
            }
        }

        usRoles.ReturnColumnName = (this.UseCodeNameForSelection ? "RoleName" : "RoleID");
        usRoles.WhereCondition = whereCondition;

        if (SiteRoles && GlobalRoles && this.UseCodeNameForSelection)
        {
            usRoles.AddGlobalObjectNamePrefix = true;
        }

        if (forceReload)
        {
            usRoles.Reload(forceReload);
        }
    }



    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (usRoles == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }

    #endregion
}
