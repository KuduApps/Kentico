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
using System.Text;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Community;
using CMS.UIControls;

public partial class CMSWebParts_Community_Profile_GroupForums : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets group name to specify group members.
    /// </summary>
    public string GroupName
    {
        get
        {
            string groupName = ValidationHelper.GetString(this.GetValue("GroupName"), "");
            if ((string.IsNullOrEmpty(groupName) || groupName == GroupInfoProvider.CURRENT_GROUP) && (CommunityContext.CurrentGroup != null))
            {
                return CommunityContext.CurrentGroup.GroupName;
            }
            return groupName;
        }
        set
        {
            this.SetValue("GroupName", value);
        }
    }


    /// <summary>
    /// Gets or sets message which should be displayed if user hasn't permissions.
    /// </summary>
    public string NoPermissionMessage
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("NoPermissionMessage"), messageElem.ErrorMessage), messageElem.ErrorMessage);
        }
        set
        {
            this.SetValue("NoPermissionMessage", value);
            this.messageElem.ErrorMessage = value;
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
    /// Reloads the control data.
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
            // Do nothing
        }
        else
        {
            this.forumsElem.HideWhenGroupIsNotSupplied = true;
            this.forumsElem.OnCheckPermissions += new CMS.UIControls.CMSAdminControl.CheckPermissionsEventHandler(forumsElem_OnCheckPermissions);

            GroupInfo gi = GroupInfoProvider.GetGroupInfo(this.GroupName, CMSContext.CurrentSiteName);
            if (gi != null)
            {
                this.forumsElem.GroupID = gi.GroupID;
            }
        }
    }


    /// <summary>
    /// Group forums - check permissions.
    /// </summary>
    void forumsElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!(CMSContext.CurrentUser.IsGroupAdministrator(this.forumsElem.GroupID) | CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", "Manage")))
        {
            if (sender != null)
            {
                sender.StopProcessing = true;
            }
            forumsElem.StopProcessing = true;
            forumsElem.Visible = false;
            messageElem.Visible = true;
            messageElem.ErrorMessage = String.Format(this.NoPermissionMessage, permissionType, "CMS.Groups");
        }
    }
}
