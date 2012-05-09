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
using CMS.IO;

public partial class CMSWebParts_Community_Profile_GroupProfile : CMSAbstractWebPart
{
    #region "Private variables"

    bool mDisplayMessage = false;

    #endregion


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
    /// If true group display name change allowed on live site.
    /// </summary>
    public bool AllowChangeGroupDisplayName
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowChangeGroupDisplayName"), false);
        }
        set
        {
            groupProfileElem.AllowChangeGroupDisplayName = value;
            this.SetValue("AllowChangeGroupDisplayName", value);
        }
    }


    /// <summary>
    /// Gets or sets message which should be displayed if user hasn't permissions.
    /// </summary>
    public string NoPermissionMessage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NoPermissionMessage"), "");
        }
        set
        {
            this.SetValue("NoPermissionMessage", value);
            this.messageElem.ErrorMessage = value;
        }
    }


    /// <summary>
    /// If true, changing theme for group page is enabled.
    /// </summary>
    public bool AllowSelectTheme
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowSelectTheme"), false);
        }
        set
        {
            this.SetValue("AllowSelectTheme", value);
            groupProfileElem.AllowSelectTheme = value;
        }
    }


    /// <summary>
    /// If true, the general tab is enabled.
    /// </summary>
    public bool DisplayGeneral
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayGeneral"), groupProfileElem.ShowGeneralTab);
        }
        set
        {
            this.SetValue("DisplayGeneral", value);
            groupProfileElem.ShowGeneralTab = value;
        }
    }


    /// <summary>
    /// If true, the security tab is enabled.
    /// </summary>
    public bool DisplaySecurity
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplaySecurity"), groupProfileElem.ShowSecurityTab);
        }
        set
        {
            this.SetValue("DisplaySecurity", value);
            groupProfileElem.ShowSecurityTab = value;
        }
    }


    /// <summary>
    /// If true, the members tab is enabled.
    /// </summary>
    public bool DisplayMembers
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayMembers"), groupProfileElem.ShowMembersTab);
        }
        set
        {
            this.SetValue("DisplayMembers", value);
            groupProfileElem.ShowMembersTab = value;
        }
    }


    /// <summary>
    /// If true, the roles tab is enabled.
    /// </summary>
    public bool DisplayRoles
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayRoles"), groupProfileElem.ShowRolesTab);
        }
        set
        {
            this.SetValue("DisplayRoles", value);
            groupProfileElem.ShowRolesTab = value;
        }
    }


    /// <summary>
    /// If true, the forums tab is enabled.
    /// </summary>
    public bool DisplayForums
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayForums"), groupProfileElem.ShowForumsTab);
        }
        set
        {
            this.SetValue("DisplayForums", value);
            groupProfileElem.ShowForumsTab = value;
        }
    }


    /// <summary>
    /// If true, the media library tab is enabled.
    /// </summary>
    public bool DisplayMediaLibrary
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayMediaLibrary"), groupProfileElem.ShowMediaTab);
        }
        set
        {
            this.SetValue("DisplayMediaLibrary", value);
            groupProfileElem.ShowMediaTab = value;
        }
    }


    /// <summary>
    /// If true, the message boards tab is enabled.
    /// </summary>
    public bool DisplayMessageBoards
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayMessageBoards"), groupProfileElem.ShowMessageBoardsTab);
        }
        set
        {
            this.SetValue("DisplayMessageBoards", value);
            groupProfileElem.ShowMessageBoardsTab = value;
        }
    }


    /// <summary>
    /// If true, the polls tab is enabled.
    /// </summary>
    public bool DisplayPolls
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayPolls"), groupProfileElem.ShowPollsTab);
        }
        set
        {
            this.SetValue("DisplayPolls", value);
            groupProfileElem.ShowPollsTab = value;
        }
    }


    /// <summary>
    /// If true, the polls tab is enabled.
    /// </summary>
    public bool DisplayProjects
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayProjects"), groupProfileElem.ShowProjectsTab);
        }
        set
        {
            this.SetValue("DisplayProjects", value);
            groupProfileElem.ShowProjectsTab = value;
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
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            this.SetContext();

            this.messageElem.ErrorMessage = this.NoPermissionMessage;
            this.messageElem.IsLiveSite = true;
            this.groupProfileElem.IsLiveSite = true;
            this.groupProfileElem.RedirectToAccessDeniedPage = false;
            this.groupProfileElem.OnCheckPermissions += new CMS.UIControls.CMSAdminControl.CheckPermissionsEventHandler(groupProfileElem_OnCheckPermissions);

            this.groupProfileElem.HideWhenGroupIsNotSupplied = true;

            GroupInfo gi = GroupInfoProvider.GetGroupInfo(this.GroupName, CMSContext.CurrentSiteName); 

            if (gi != null)
            {
                this.groupProfileElem.GroupID = gi.GroupID;
                this.groupProfileElem.AllowChangeGroupDisplayName = AllowChangeGroupDisplayName;
                this.groupProfileElem.AllowSelectTheme = AllowSelectTheme;
                this.groupProfileElem.ShowGeneralTab = DisplayGeneral;
                this.groupProfileElem.ShowSecurityTab = DisplaySecurity;
                this.groupProfileElem.ShowMembersTab = DisplayMembers;
                this.groupProfileElem.ShowRolesTab = DisplayRoles;
                this.groupProfileElem.ShowForumsTab = DisplayForums;
                this.groupProfileElem.ShowMediaTab = DisplayMediaLibrary;
                this.groupProfileElem.ShowMessageBoardsTab = DisplayMessageBoards;
                this.groupProfileElem.ShowPollsTab = DisplayPolls;
                this.groupProfileElem.ShowProjectsTab = DisplayProjects;
            }
            else
            {
                groupProfileElem.StopProcessing = true;
                groupProfileElem.Visible = false;
                mDisplayMessage = true;
            }

            this.ReleaseContext();
        }
    }


   
    /// <summary>
    /// Group profile - check permissions.
    /// </summary>
    void groupProfileElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!(CMSContext.CurrentUser.IsGroupAdministrator(this.groupProfileElem.GroupID) || CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", permissionType)))
        {
            if (sender != null)
            {
                sender.StopProcessing = true;
            }
            groupProfileElem.StopProcessing = true;
            groupProfileElem.Visible = false;
            messageElem.ErrorMessage = NoPermissionMessage;
            mDisplayMessage = true;
        }
    }

    
    /// <summary>
    /// Render override.
    /// </summary>
    /// <param name="writer">Writer</param>
    protected override void Render(HtmlTextWriter writer)
    {
        if (!mDisplayMessage)
        {
            messageElem.Visible = false;
        }
        
        base.Render(writer);
    }
}
