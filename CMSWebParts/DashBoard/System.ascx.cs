using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSWebParts_DashBoard_System : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the timer interval (seconds) for the page refresh.
    /// </summary>
    public int RefreshInterval
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("RefreshInterval"), 2);
        }
        set
        {
            SetValue("RefreshInterval", value);
            sysInfo.RefreshInterval = value;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Content loaded event handler
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do not process
        }
        else
        {
            // System control properties
            sysInfo.RefreshInterval = RefreshInterval;
            sysInfo.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(sysInfo_OnCheckPermissions);
        }
    }


    /// <summary>
    /// Reloads the control data
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// OnCheckPermission event handler
    /// </summary>
    /// <param name="permissionType">Type of the permission.</param>
    /// <param name="sender">The sender.</param>
    private void sysInfo_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if ((CMSContext.CurrentUser == null) || !CMSContext.CurrentUser.UserSiteManagerAdmin)
        {
            sender.StopProcessing = true;
            sysInfo.Visible = false;
            messageElem.Visible = true;
            messageElem.ErrorMessage = GetString("general.nopermission");
        }
    }

    #endregion
}
