using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Community;
using CMS.UIControls;

public partial class CMSWebParts_Community_Profile_GroupRoles : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Group name which profile should be displayed.
    /// </summary>
    public string GroupName
    {
        get
        {
            string groupName = ValidationHelper.GetString(GetValue("GroupName"), "");
            if ((string.IsNullOrEmpty(groupName) || groupName == GroupInfoProvider.CURRENT_GROUP) && (CommunityContext.CurrentGroup != null))
            {
                return CommunityContext.CurrentGroup.GroupName;
            }
            return groupName;
        }
        set
        {
            SetValue("GroupName", value);
        }
    }


    /// <summary>
    /// Gets or sets message which should be displayed if user hasn't permissions.
    /// </summary>
    public string NoPermissionMessage
    {
        get
        {
            return ValidationHelper.GetString(GetValue("NoPermissionMessage"), "");
        }
        set
        {
            SetValue("NoPermissionMessage", value);
            messageElem.ErrorMessage = value;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads data.
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
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            rolesElem.HideWhenGroupIsNotSupplied = true;
            rolesElem.OnCheckPermissions += rolesElem_OnCheckPermissions;
            GroupInfo gi = GroupInfoProvider.GetGroupInfo(GroupName, CMSContext.CurrentSiteName);
            if (gi != null)
            {
                rolesElem.GroupID = gi.GroupID;
                rolesElem.SiteID = CMSContext.CurrentSiteID;
            }
            else
            {
                Visible = false;
            }
        }
    }


    /// <summary>
    /// Group roles - check permissions.
    /// </summary>
    void rolesElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!(CMSContext.CurrentUser.IsGroupAdministrator(rolesElem.GroupID) | CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", CMSAdminControl.PERMISSION_MANAGE)))
        {
            if (sender != null)
            {
                sender.StopProcessing = true;
            }
            rolesElem.StopProcessing = true;
            rolesElem.Visible = false;
            messageElem.ErrorMessage = NoPermissionMessage;
            messageElem.Visible = true;
        }
    }
}
