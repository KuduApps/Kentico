using System;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Community;
using CMS.UIControls;

public partial class CMSWebParts_Community_Membership_GroupMembers : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets group name to specify group members.
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
            membersElem.HideWhenGroupIsNotSupplied = true;
            membersElem.OnCheckPermissions += membersElem_OnCheckPermissions;
            GroupInfo gi = GroupInfoProvider.GetGroupInfo(GroupName, CMSContext.CurrentSiteName);
            if (gi != null)
            {
                membersElem.GroupID = gi.GroupID;
            }
        }
    }


    /// <summary>
    /// Group members - check permissions.
    /// </summary>
    protected void membersElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!(CMSContext.CurrentUser.IsGroupAdministrator(membersElem.GroupID) | CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", "Manage")))
        {
            if (sender != null)
            {
                sender.StopProcessing = true;
            }
            membersElem.StopProcessing = true;
            membersElem.Visible = false;
            messageElem.ErrorMessage = NoPermissionMessage;
            messageElem.Visible = true;
        }
    }
}
