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

using CMS.Community;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Groups_Controls_GroupProfile : CMSAdminEditControl
{
    #region "Variables"

    private int mGroupId = 0;

    private bool mShowContentTab = true;
    private bool mShowGeneralTab = true;
    private bool mShowSecurityTab = true;
    private bool mShowMembersTab = true;
    private bool mShowRolesTab = true;
    private bool mShowForumsTab = true;
    private bool mShowMediaTab = true;
    private bool mShowMessageBoardsTab = true;
    private bool mShowPollsTab = true;
    private bool mShowProjectTab = false;

    private int generalTabIndex = 0;
    private int securityTabIndex = 0;
    // Set -1 it there is a check for a presence of a module
    private int membersTabIndex = -1;
    private int rolesTabIndex = -1;
    private int forumsTabIndex = -1;
    private int mediaTabIndex = -1;
    private int messageBoardsTabIndex = -1;
    private int pollsTabIndex = -1;
    private int projectTabIndex = -1;

    private bool mHideWhenGroupIsNotSupplied = false;
    private GroupInfo gi = null;
    private CMSAdminControl ctrl = null;
    private bool mAllowChangeGroupDisplayName = false;
    private bool mAllowSelectTheme = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Determines whether to hide the content of the control when GroupID is not supplied.
    /// </summary>
    public bool HideWhenGroupIsNotSupplied
    {
        get
        {
            return this.mHideWhenGroupIsNotSupplied;
        }
        set
        {
            this.mHideWhenGroupIsNotSupplied = value;
        }
    }


    /// <summary>
    /// If true group display name change allowed on live site.
    /// </summary>
    public bool AllowChangeGroupDisplayName
    {
        get
        {
            return mAllowChangeGroupDisplayName;
        }
        set
        {
            mAllowChangeGroupDisplayName = value;
        }
    }


    /// <summary>
    /// If true changing theme for group page is enabled.
    /// </summary>
    public bool AllowSelectTheme
    {
        get
        {
            return mAllowSelectTheme;
        }
        set
        {
            mAllowSelectTheme = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the group to be edited.
    /// </summary>
    public int GroupID
    {
        get
        {
            return this.mGroupId;
        }
        set
        {
            this.mGroupId = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the content tab.
    /// </summary>
    public bool ShowContentTab
    {
        get
        {
            return this.mShowContentTab;
        }
        set
        {
            this.mShowContentTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the general tab.
    /// </summary>
    public bool ShowGeneralTab
    {
        get
        {
            return this.mShowGeneralTab;
        }
        set
        {
            this.mShowGeneralTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the security tab.
    /// </summary>
    public bool ShowSecurityTab
    {
        get
        {
            return this.mShowSecurityTab;
        }
        set
        {
            this.mShowSecurityTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the members tab.
    /// </summary>
    public bool ShowMembersTab
    {
        get
        {
            return this.mShowMembersTab;
        }
        set
        {
            this.mShowMembersTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the roles tab.
    /// </summary>
    public bool ShowRolesTab
    {
        get
        {
            return this.mShowRolesTab;
        }
        set
        {
            this.mShowRolesTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the forums tab.
    /// </summary>
    public bool ShowForumsTab
    {
        get
        {
            return this.mShowForumsTab;
        }
        set
        {
            this.mShowForumsTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the media tab.
    /// </summary>
    public bool ShowMediaTab
    {
        get
        {
            return this.mShowMediaTab;
        }
        set
        {
            this.mShowMediaTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the message boards tab.
    /// </summary>
    public bool ShowMessageBoardsTab
    {
        get
        {
            return this.mShowMessageBoardsTab;
        }
        set
        {
            this.mShowMessageBoardsTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the polls tab.
    /// </summary>
    public bool ShowPollsTab
    {
        get
        {
            return this.mShowPollsTab;
        }
        set
        {
            this.mShowPollsTab = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to show the projects tab.
    /// </summary>
    public bool ShowProjectsTab
    {
        get
        {
            return this.mShowProjectTab;
        }
        set
        {
            this.mShowProjectTab = value;
        }
    }


    /// <summary>
    /// Gets or sets switch to display appropriate controls.
    /// </summary>
    public string SelectedPage
    {
        get
        {
            return ValidationHelper.GetString(this.ViewState[this.ClientID + "SelectedPage"], "");
        }
        set
        {
            ViewState[this.ClientID + "SelectedPage"] = (object)value;
        }
    }


    #endregion


    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        // Get page url
        string page = QueryHelper.GetText("tab", this.SelectedPage);
        if (!String.IsNullOrEmpty(page))
        {
            page = page.ToLower();
        }

        // Check MANAGE permission
        if (RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_MANAGE, this))
        {
            if (this.StopProcessing)
            {
                return;
            }
        }

        if ((this.GroupID == 0) && this.HideWhenGroupIsNotSupplied)
        {
            // Hide if groupID == 0
            this.Visible = false;
            return;
        }

        gi = GroupInfoProvider.GetGroupInfo(this.GroupID);

        // If no group, display the info and return
        if (gi == null)
        {
            this.lblInfo.Text = GetString("group.groupprofile.nogroup");
            this.lblInfo.Visible = true;
            this.tabMenu.Visible = false;
            this.pnlContent.Visible = false;
            return;
        }

        // Get current URL
        string absoluteUri = URLHelper.CurrentURL;

        // Menu initialization
        tabMenu.TabControlIdPrefix = "GroupProfile";
        tabMenu.UrlTarget = "_self";
        tabMenu.Tabs = new string[9, 5];

        tabMenu.UsePostback = true;

        int i = 0;
        string defaultTab = null;

        #region "Show/hide tabs"

        // Show general tab
        if (ShowGeneralTab)
        {
            tabMenu.Tabs[i, 0] = GetString("General.General");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "general"));
            defaultTab = "general";
            generalTabIndex = i;
            i++;
        }

        // Show security tab
        if (ShowSecurityTab)
        {
            tabMenu.Tabs[i, 0] = GetString("General.Security");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "security"));
            if (String.IsNullOrEmpty(defaultTab))
            {
                defaultTab = "security";
            }

            securityTabIndex = i;
            i++;
        }

        // Show members tab
        if (ShowMembersTab)
        {
            tabMenu.Tabs[i, 0] = GetString("Group.Members");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "members"));
            if (String.IsNullOrEmpty(defaultTab))
            {
                defaultTab = "members";
            }

            membersTabIndex = i;
            i++;
        }

        // Show roles tab
        if (ShowRolesTab && ResourceSiteInfoProvider.IsResourceOnSite("CMS.Roles", CMSContext.CurrentSiteName))
        {
            tabMenu.Tabs[i, 0] = GetString("general.roles");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "roles"));
            if (String.IsNullOrEmpty(defaultTab))
            {
                defaultTab = "roles";
            }

            rolesTabIndex = i;
            i++;
        }

        // Show forums tab
        if (ShowForumsTab && ResourceSiteInfoProvider.IsResourceOnSite("CMS.Forums", CMSContext.CurrentSiteName))
        {
            tabMenu.Tabs[i, 0] = GetString("Group.Forums");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "forums"));
            if (String.IsNullOrEmpty(defaultTab))
            {
                defaultTab = "forums";
            }

            forumsTabIndex = i;
            i++;
        }

        // Show media tab
        if (ShowMediaTab && ResourceSiteInfoProvider.IsResourceOnSite("CMS.MediaLibrary", CMSContext.CurrentSiteName))
        {
            tabMenu.Tabs[i, 0] = GetString("Group.MediaLibrary");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "medialibrary"));
            if (String.IsNullOrEmpty(defaultTab))
            {
                defaultTab = "medialibrary";
            }

            mediaTabIndex = i;
            i++;
        }

        // Show message boards tab
        if (ShowMessageBoardsTab && ResourceSiteInfoProvider.IsResourceOnSite("CMS.MessageBoards", CMSContext.CurrentSiteName))
        {
            tabMenu.Tabs[i, 0] = GetString("Group.MessageBoards");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "messageboards"));
            if (String.IsNullOrEmpty(defaultTab))
            {
                defaultTab = "messageboards";
            }

            messageBoardsTabIndex = i;
            i++;
        }

        // Show polls tab
        if (ShowPollsTab && ResourceSiteInfoProvider.IsResourceOnSite("CMS.Polls", CMSContext.CurrentSiteName))
        {
            tabMenu.Tabs[i, 0] = GetString("Group.Polls");
            tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "polls"));
            if (String.IsNullOrEmpty(defaultTab))
            {
                defaultTab = "polls";
            }

            pollsTabIndex = i;
            i++;
        }

        // Show projects tab
        if (ShowProjectsTab)
        {
            // Check whether license for project management is available
            // if no hide project management tab
            if (LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ProjectManagement))
            {
                // Check site availability
                if (ResourceSiteInfoProvider.IsResourceOnSite("CMS.ProjectManagement", CMSContext.CurrentSiteName))
                {
                    tabMenu.Tabs[i, 0] = ResHelper.GetString("pm.project.list");
                    tabMenu.Tabs[i, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, "tab", "projects"));
                    if (String.IsNullOrEmpty(defaultTab))
                    {
                        defaultTab = "projects";
                    }

                    projectTabIndex = i;
                }
            }
        }

        #endregion

        if (string.IsNullOrEmpty(page))
        {
            page = defaultTab;
            tabMenu.SelectedTab = 0;
        }

        // Select current page
        switch (page)
        {
            case "general":
                tabMenu.SelectedTab = generalTabIndex;
                
                // Show general content
                if (ShowGeneralTab)
                {
                    ctrl = LoadControl("~/CMSModules/Groups/Controls/GroupEdit.ascx") as CMSAdminControl;
                    ctrl.ID = "groupEditElem";

                    if (ctrl != null)
                    {
                        ctrl.SetValue("GroupID", gi.GroupID);
                        ctrl.SetValue("SiteID", CMSContext.CurrentSiteID);
                        ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                        ctrl.SetValue("AllowChangeGroupDisplayName", AllowChangeGroupDisplayName);
                        ctrl.SetValue("AllowSelectTheme", AllowSelectTheme);
                        ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);
                        pnlContent.Controls.Add(ctrl);
                    }
                }
                break;

            case "security":
                tabMenu.SelectedTab = securityTabIndex;
                
                // Show security content
                if (ShowSecurityTab)
                {
                    ctrl = LoadControl("~/CMSModules/Groups/Controls/Security/GroupSecurity.ascx") as CMSAdminControl;
                    ctrl.ID = "securityElem";

                    if (ctrl != null)
                    {
                        ctrl.SetValue("GroupID", gi.GroupID);
                        ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                        ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);
                        pnlContent.Controls.Add(ctrl);
                    }
                }
                break;

            case "members":
                if (membersTabIndex >= 0)
                {
                    tabMenu.SelectedTab = membersTabIndex;

                    // Show members content
                    if (ShowMembersTab)
                    {
                        ctrl = LoadControl("~/CMSModules/Groups/Controls/Members/Members.ascx") as CMSAdminControl;
                        ctrl.ID = "securityElem";

                        if (ctrl != null)
                        {
                            ctrl.SetValue("GroupID", gi.GroupID);
                            ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                            ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);
                            pnlContent.Controls.Add(ctrl);
                        }
                    }
                }
                break;

            case "forums":
                if (forumsTabIndex >= 0)
                {
                    tabMenu.SelectedTab = forumsTabIndex;

                    // Show forums content
                    if (ShowForumsTab)
                    {
                        ctrl = LoadControl("~/CMSModules/Forums/Controls/LiveControls/Groups.ascx") as CMSAdminControl;
                        ctrl.ID = "forumElem";

                        if (ctrl != null)
                        {
                            ctrl.SetValue("GroupID", gi.GroupID);
                            ctrl.SetValue("CommunityGroupGUID", gi.GroupGUID);
                            ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                            ctrl.DisplayMode = ControlDisplayModeEnum.Simple;
                            ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);

                            pnlContent.Controls.Add(ctrl);
                        }
                    }
                }
                break;

            case "roles":
                if (rolesTabIndex >= 0)
                {
                    tabMenu.SelectedTab = rolesTabIndex;
                    // Show roles content
                    if (ShowRolesTab)
                    {
                        ctrl = LoadControl("~/CMSModules/Membership/Controls/Roles/Roles.ascx") as CMSAdminControl;
                        ctrl.ID = "rolesElem";

                        if (ctrl != null)
                        {
                            ctrl.SetValue("GroupID", gi.GroupID);
                            ctrl.SetValue("GroupGUID", gi.GroupGUID);
                            ctrl.SetValue("SiteID", CMSContext.CurrentSiteID);
                            ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                            ctrl.DisplayMode = ControlDisplayModeEnum.Simple;
                            ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);

                            pnlContent.Controls.Add(ctrl);
                        }
                    }
                }
                break;

            case "polls":
                if (pollsTabIndex >= 0)
                {
                    tabMenu.SelectedTab = pollsTabIndex;

                    // Show polls content
                    if (ShowPollsTab)
                    {
                        ctrl = LoadControl("~/CMSModules/Polls/Controls/Polls.ascx") as CMSAdminControl;
                        ctrl.ID = "pollsElem";

                        if (ctrl != null)
                        {
                            ctrl.SetValue("GroupID", gi.GroupID);
                            ctrl.SetValue("GroupGUID", gi.GroupGUID);
                            ctrl.SetValue("SiteID", CMSContext.CurrentSiteID);
                            ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                            ctrl.DisplayMode = ControlDisplayModeEnum.Simple;
                            ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);
                            pnlContent.Controls.Add(ctrl);
                        }
                    }
                }
                break;

            case "messageboards":
                if (messageBoardsTabIndex >= 0)
                {
                    tabMenu.SelectedTab = messageBoardsTabIndex;
                    
                    // Show message boards content
                    if (ShowMessageBoardsTab)
                    {
                        ctrl = LoadControl("~/CMSModules/MessageBoards/Controls/LiveControls/Boards.ascx") as CMSAdminControl;
                        ctrl.ID = "boardElem";

                        if (ctrl != null)
                        {
                            ctrl.SetValue("GroupID", gi.GroupID);
                            ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                            ctrl.DisplayMode = ControlDisplayModeEnum.Simple;
                            ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);

                            pnlContent.Controls.Add(ctrl);
                        }
                    }
                }
                break;

            case "medialibrary":
                if (mediaTabIndex >= 0)
                {
                    tabMenu.SelectedTab = mediaTabIndex;
                    
                    // Show media content
                    if (ShowMediaTab)
                    {
                        ctrl = LoadControl("~/CMSModules/MediaLibrary/Controls/LiveControls/MediaLibraries.ascx") as CMSAdminControl;
                        ctrl.ID = "libraryElem";

                        if (ctrl != null)
                        {
                            ctrl.SetValue("GroupGUID", gi.GroupID);
                            ctrl.SetValue("GroupID", gi.GroupID);
                            ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                            ctrl.DisplayMode = ControlDisplayModeEnum.Simple;
                            ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);

                            pnlContent.Controls.Add(ctrl);
                        }
                    }
                }
                break;

            case "projects":
                if (projectTabIndex >= 0)
                {
                    tabMenu.SelectedTab = projectTabIndex;
                    
                    // Show projects content
                    if (ShowProjectsTab)
                    {
                        ctrl = LoadControl("~/CMSModules/ProjectManagement/Controls/LiveControls/GroupProjects.ascx") as CMSAdminControl;
                        ctrl.ID = "projectElem";
                        if (ctrl != null)
                        {
                            ctrl.SetValue("CommunityGroupID", gi.GroupID);
                            ctrl.SetValue("IsLiveSite", this.IsLiveSite);
                            ctrl.DisplayMode = ControlDisplayModeEnum.Simple;
                            ctrl.OnCheckPermissions += new CheckPermissionsEventHandler(ctrl_OnCheckPermissions);
                            pnlContent.Controls.Add(ctrl);
                        }
                    }
                }
                break;


            default:
                break;
        }

        if (!RequestHelper.IsPostBack())
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    public override void ReloadData()
    {
        if (ctrl != null)
        {
            // Reload data
            ctrl.ReloadData();
        }
    }


    #region "Security handlers"

    /// <summary>
    /// Control - Check permission event handler.
    /// </summary>
    /// <param name="permissionType"></param>
    void ctrl_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!RaiseOnCheckPermissions(permissionType, sender))
        {
            // Check if user is allowed to create or modify the module records
            if ((!CMSContext.CurrentUser.IsGroupAdministrator(this.GroupID)) && (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Groups", CMSAdminControl.PERMISSION_MANAGE)))
            {
                AccessDenied("CMS.Groups", CMSAdminControl.PERMISSION_MANAGE);
            }

        }
    }

    #endregion
}
