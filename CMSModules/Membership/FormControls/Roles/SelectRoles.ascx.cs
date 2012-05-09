using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.CMSHelper;

public partial class CMSModules_Membership_FormControls_Roles_SelectRoles : FormEngineUserControl
{
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
            selectRole.Enabled = value;
        }
    }


    /// <summary>
    /// Determines whether use displaynames instead of codenames.
    /// </summary>
    public bool UseFriendlyNames
    {
        get
        {
            return selectRole.UseFriendlyMode;
        }
        set
        {
            selectRole.UseFriendlyMode = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return selectRole.Value;
        }
        set
        {
            selectRole.Value = value;
        }

    }


    /// <summary>
    /// Gets or sets if live iste property.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return selectRole.IsLiveSite;
        }
        set
        {
            selectRole.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates if role selector allow empty selection.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return selectRole.AllowEmpty;
        }
        set
        {
            selectRole.AllowEmpty = value;
        }
    }


    #endregion


    #region "Events"

    /// <summary>
    /// Page_Load event.
    /// </summary>    
    protected void Page_Load(object sender, EventArgs e)
    {
        selectRole.IsLiveSite = this.IsLiveSite;
        selectRole.ShowSiteFilter = false;
        selectRole.CurrentSelector.ResourcePrefix = "addroles";
        selectRole.SiteID = CMSContext.CurrentSiteID;
    }

    #endregion
}
