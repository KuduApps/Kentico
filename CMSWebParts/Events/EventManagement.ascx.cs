using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;


public partial class CMSWebParts_Events_EventManagement : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the site name. 
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("SiteName"), String.Empty).Replace("##currentsite##", CMSContext.CurrentSiteName);
        }
        set
        {
            SetValue("SiteName", value);
        }

    }


    /// <summary>
    /// Gets or sets the order by condition.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(GetValue("OrderBy"), "eventdate");
        }
        set
        {
            SetValue("OrderBy", value);
        }
    }


    /// <summary>
    /// Gets or sets the sorting direction.
    /// </summary>
    public string Sorting
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Sorting"), "ASC");
        }
        set
        {
            SetValue("Sorting", value);
        }
    }


    /// <summary>
    /// Gets or sets date filter for events.
    /// </summary>
    public string EventScope
    {
        get
        {
            return ValidationHelper.GetString(GetValue("EventScope"), "all");
        }
        set
        {
            SetValue("EventScope", value);
        }
    }


    /// <summary>
    /// Gets or sets the value of items per page.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ItemsPerPage"), "25");
        }
        set
        {
            SetValue("ItemsPerPage", value);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            this.EventManager.StopProcessing = true;
        }
        else
        {
            EventManager.SiteName = SiteName;
            EventManager.OrderBy = OrderBy + " " + Sorting;
            EventManager.ItemsPerPage = ItemsPerPage;
            EventManager.EventScope = EventScope;
            EventManager.OnCheckPermissions += new CMS.UIControls.CMSAdminControl.CheckPermissionsEventHandler(EventManager_OnCheckPermissions);
        }
    }


    /// <summary>
    /// Check permissions.
    /// </summary>
    /// <param name="permissionType">Permission type</param>
    /// <param name="sender">Sender</param>
    void EventManager_OnCheckPermissions(string permissionType, CMS.UIControls.CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.eventmanager", permissionType))
        {
            EventManager.Visible = false;
            messageElem.Visible = true;
            sender.StopProcessing = true;            
            messageElem.ErrorMessage = GetString("general.nopermission");
        }
    }

    #endregion
}
