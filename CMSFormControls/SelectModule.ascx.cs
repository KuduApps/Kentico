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

public partial class CMSFormControls_SelectModule : FormEngineUserControl
{
    #region "Variables"

    private int mSiteID = 0;

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
    /// Returns ClientID of the DLL with module.
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
    /// Gets or sets the ID of the site for which the modules should be returned. 0 means current site.
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
                this.uniSelector.WhereCondition = GetProperWhereCondition();
            }
        }
    }


    /// <summary>
    /// Gets the inner UniSelector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return this.uniSelector;
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
    /// If true, displays only modules which are assigned to the given site.
    /// </summary>
    public bool DisplayOnlyForGivenSite
    {
        get;
        set;
    }


    /// <summary>
    /// If true, displays only modules which have some permissions to be displayed in permission matrix.
    /// </summary>
    public bool DisplayOnlyWithPermission
    {
        get;
        set;
    }


    /// <summary>
    /// If true, displays all modules. If false, displayes only modules with some UI Elements.
    /// </summary>
    public bool DisplayAllModules
    {
        get;
        set;
    }


    /// <summary>
    /// If true, displays the none option, returning 0 as the value.
    /// </summary>
    public bool DisplayNone
    {
        get;
        set;
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
        if (this.StopProcessing)
        {
            this.uniSelector.StopProcessing = true;
        }
        else
        {
            ReloadData(false);
        }
    }


    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    /// <param name="forcedReload">Indicates whether the UniSelector should be reloaded</param>
    public void ReloadData(bool forcedReload)
    {
        this.uniSelector.IsLiveSite = this.IsLiveSite;
        this.uniSelector.WhereCondition = GetProperWhereCondition();
        this.uniSelector.OrderBy = "ResourceDisplayName";

        if (DisplayNone)
        {
            this.uniSelector.AllowEmpty = true;
        }

        if (forcedReload)
        {
            this.uniSelector.Reload(true);
        }
    }


    /// <summary>
    /// Returns proper where condition.
    /// </summary>
    private string GetProperWhereCondition()
    {
        string where = "";

        if (!this.DisplayAllModules)
        {
            if (CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                where = "(ResourceID IN (SELECT ElementResourceID FROM CMS_UIElement WHERE ElementParentID IS NULL AND ElementChildCount > 0)) AND NOT ResourceName = 'CMS.WYSIWYGEditor'";
            }
            else
            {
                where = "(ResourceID IN (SELECT ResourceID FROM CMS_ResourceSite WHERE SiteID = " + (this.SiteID == 0 ? CMSContext.CurrentSiteID : this.SiteID) + ")) AND (ResourceID IN (SELECT ElementResourceID FROM CMS_UIElement WHERE ElementParentID IS NULL AND ElementChildCount > 0)) AND NOT ResourceName = 'CMS.WYSIWYGEditor'";
            }
        }

        if (this.DisplayOnlyForGivenSite)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "(ResourceID IN (SELECT ResourceID FROM CMS_ResourceSite WHERE SiteID = " + (this.SiteID == 0 ? CMSContext.CurrentSiteID : this.SiteID) + "))";
        }

        if (this.DisplayOnlyWithPermission)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "EXISTS (SELECT PermissionID FROM CMS_Permission WHERE CMS_Permission.ResourceID = CMS_Resource.ResourceID AND PermissionDisplayInMatrix = 1)";
        }

        return where;
    }

}
