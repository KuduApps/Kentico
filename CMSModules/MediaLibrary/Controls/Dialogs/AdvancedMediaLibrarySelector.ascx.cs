using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.SettingsProvider;
using CMS.UIControls;


public partial class CMSModules_MediaLibrary_Controls_Dialogs_AdvancedMediaLibrarySelector : CMSUserControl
{
    #region "Events & delegates"

    /// <summary>
    /// Event fired when library selection changed.
    /// </summary>
    public event EventHandler LibraryChanged;

    #endregion


    #region "Private variables"

    private AvailableGroupsEnum mGroups = AvailableGroupsEnum.None;
    private AvailableLibrariesEnum mGlobalLibraries = AvailableLibrariesEnum.None;
    private AvailableLibrariesEnum mGroupLibraries = AvailableLibrariesEnum.None;
    private AvailableSitesEnum mSites = AvailableSitesEnum.All;

    private string mGlobalLibaryName = "";
    private int mSelectGroupID = 0;
    private int mSelectedLibraryID = 0;
    private string mGroupName = "";
    private string mGroupLibraryName = "";
    private string mSiteToSelect = "";

    private string mLibraryName = "";

    // Group selector
    private const string controlPath = "~/CMSModules/Groups/FormControls/CommunityGroupSelector.ascx";
    private FormEngineUserControl groupsSelector = null;
    private bool disableGroupSelector = false;

    #endregion


    #region "Public properties"

    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            siteSelector.IsLiveSite = value;
            librarySelector.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Indicates what groups should be available.
    /// </summary>
    public AvailableGroupsEnum Groups
    {
        get
        {
            return mGroups;
        }
        set
        {
            mGroups = value;
        }
    }


    /// <summary>
    /// Indicates what libraries should be available.
    /// </summary>
    public AvailableLibrariesEnum GlobalLibraries
    {
        get
        {
            return mGlobalLibraries;
        }
        set
        {
            mGlobalLibraries = value;
        }
    }


    /// <summary>
    /// Indicates what group libraries should be available.
    /// </summary>
    public AvailableLibrariesEnum GroupLibraries
    {
        get
        {
            return mGroupLibraries;
        }
        set
        {
            mGroupLibraries = value;
        }
    }


    /// <summary>
    /// Available sites.
    /// </summary>
    public AvailableSitesEnum Sites
    {
        get
        {
            return mSites;
        }
        set
        {
            mSites = value;
        }
    }


    /// <summary>
    /// Name of the global library.
    /// </summary>
    public string GlobalLibaryName
    {
        get
        {
            return mGlobalLibaryName;
        }
        set
        {
            mGlobalLibaryName = value;
            mLibraryName = value;
        }
    }


    /// <summary>
    /// Name of the group.
    /// </summary>
    public string GroupName
    {
        get
        {
            return mGroupName;
        }
        set
        {
            mGroupName = value;
        }
    }


    /// <summary>
    /// ID of the group to select.
    /// </summary>
    public int SelectedGroupID
    {
        get
        {
            return mSelectGroupID;
        }
        set
        {
            mSelectGroupID = value;
        }
    }


    /// <summary>
    /// ID of the library to select.
    /// </summary>
    public int SelectedLibraryID
    {
        get
        {
            return mSelectedLibraryID;
        }
        set
        {
            mSelectedLibraryID = value;
        }
    }


    /// <summary>
    /// Name of the group library.
    /// </summary>
    public string GroupLibraryName
    {
        get
        {
            return mGroupLibraryName;
        }
        set
        {
            mGroupLibraryName = value;
            mLibraryName = value;
        }
    }


    /// <summary>
    /// Current library ID.
    /// </summary>
    public int LibraryID
    {
        get
        {
            return librarySelector.MediaLibraryID;
        }
        set
        {
            librarySelector.MediaLibraryID = value;
        }
    }


    /// <summary>
    /// Name of the library to pre-select.
    /// </summary>
    public string LibraryName
    {
        get
        {
            return librarySelector.MediaLibraryName;
        }
        set
        {
            librarySelector.MediaLibraryName = value;
        }
    }


    /// <summary>
    /// Currently selected site name.
    /// </summary>
    public string SelectedSiteName
    {
        get
        {
            if (String.IsNullOrEmpty(siteSelector.SiteName))
            {
                return mSiteToSelect;
            }
            return siteSelector.SiteName;
        }
        set
        {
            siteSelector.SiteName = value;
            mSiteToSelect = value;
        }
    }


    /// <summary>
    /// ID of the currently selected site.
    /// </summary>
    public int SiteID
    {
        get
        {
            int siteId = ValidationHelper.GetInteger(siteSelector.DropDownSingleSelect.SelectedValue, 0);
            return (siteId > 0 ? siteId : -1);
        }
        set
        {
            siteSelector.SiteID = value;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        siteSelector.StopProcessing = false;
        siteSelector.UniSelector.AllowEmpty = false;

        InitializeGroupSelector();
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            // Display group selector only when group module is present
            if (ModuleEntry.IsModuleRegistered(ModuleEntry.COMMUNITY) && ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY) && (groupsSelector != null))
            {
                HandleGroupsSelection();
            }
            else
            {
                // Reload libraries
                LoadLibrarySelection();

                // Pre-select library
                PreselectLibrary();

                RaiseOnLibraryChanged();
            }
        }

        HandleSiteEmpty();

        HandleGroupEmpty();

        if (librarySelector.MediaLibraryID == 0)
        {
            SetLibrariesEmpty();
        }
        else
        {
            SetLibraries();
        }

        base.OnPreRender(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            siteSelector.StopProcessing = true;
            librarySelector.StopProcessing = true;
            Visible = false;
        }
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
    }


    /// <summary>
    /// Loads selector data.
    /// </summary>
    public void LoadData()
    {
        // Initialize controls
        SetupControls();
    }


    /// <summary>
    /// Reloads content of site selector.
    /// </summary>
    public void ReloadSites()
    {
        siteSelector.Reload(true);
    }


    #region "Private methods"

    /// <summary>
    /// Initializes all the inner controls.
    /// </summary>
    private void SetupControls()
    {
        // Initialize site selector
        siteSelector.IsLiveSite = IsLiveSite;
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.UniSelector.WhereCondition = GetSitesWhere();
        siteSelector.Reload(true);

        librarySelector.IsLiveSite = IsLiveSite;
    }


    /// <summary>
    /// Gets WHERE condition to retrieve available sites according specified type.
    /// </summary>
    private string GetSitesWhere()
    {
        string where = "SiteStatus = 'RUNNING'"; ;

        // If not global admin display only related sites
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SiteID IN (SELECT SiteID FROM CMS_UserSite WHERE UserID={0})", CMSContext.CurrentUser.UserID));
        }

        switch (Sites)
        {
            case AvailableSitesEnum.OnlySingleSite:
                if (!string.IsNullOrEmpty(SelectedSiteName))
                {
                    where = SqlHelperClass.AddWhereCondition(where, String.Format("SiteName LIKE N'{0}'", SelectedSiteName));
                }
                break;

            case AvailableSitesEnum.OnlyCurrentSite:
                where = SqlHelperClass.AddWhereCondition(where, String.Format("SiteName LIKE N'{0}'", CMSContext.CurrentSiteName));
                break;

            case AvailableSitesEnum.All:
            default:
                break;
        }

        return where;
    }


    /// <summary>
    /// Disables libraries drop-down list when empty.
    /// </summary>
    private void SetLibrariesEmpty()
    {
        LibraryID = 0;
        librarySelector.Enabled = false;

        ScriptHelper.RegisterStartupScript(Page, typeof(Page), "DialogsDisableMenuActions", ScriptHelper.GetScript("if(DisableNewFileBtn){ DisableNewFileBtn(); } if(DisableNewFolderBtn){ DisableNewFolderBtn(); }"));
    }


    /// <summary>
    /// Enables libraries drop-down list.
    /// </summary>
    private void SetLibraries()
    {
        librarySelector.Enabled = true;
    }


    /// <summary>
    /// Disables sites drop-down list when empty.
    /// </summary>
    private void HandleSiteEmpty()
    {
        if (!siteSelector.UniSelector.HasData || siteSelector.SiteID == 0)
        {
            siteSelector.Enabled = false;
            disableGroupSelector = true;
        }
    }


    /// <summary>
    /// Disables groups drop-down list when empty.
    /// </summary>
    private void HandleGroupEmpty()
    {
        if (disableGroupSelector && ModuleEntry.IsModuleRegistered(ModuleEntry.COMMUNITY) && ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY) && (groupsSelector != null))
        {
            groupsSelector.Enabled = false;
        }
    }

    #endregion


    #region "Selection handler methods"

    /// <summary>
    /// Initializes group selector.
    /// </summary>
    private void HandleGroupsSelection()
    {
        // Display group selector only when group module is present
        if (ModuleEntry.IsModuleRegistered(ModuleEntry.COMMUNITY) && ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY) && (groupsSelector != null))
        {
            // Global libraries item into group selector
            if (GlobalLibraries != AvailableLibrariesEnum.None)
            {
                // Add special item
                groupsSelector.SetValue("UseDisplayNames", true);
                groupsSelector.SetValue("DisplayGlobalValue", true);
                groupsSelector.SetValue("SiteID", SiteID);
                groupsSelector.SetValue("GlobalValueText", GetString("dialogs.media.globallibraries"));
                groupsSelector.IsLiveSite = IsLiveSite;
            }
            else
            {
                groupsSelector.SetValue("DisplayGlobalValue", false);
            }

            // If all groups should be displayed
            switch (Groups)
            {
                case AvailableGroupsEnum.All:
                    // Set condition to load only groups realted to the current site
                    groupsSelector.SetValue("WhereCondition", String.Format("(GroupSiteID={0})", SiteID));
                    break;

                case AvailableGroupsEnum.OnlyCurrentGroup:
                    // Load only current group and disable control
                    int groupId = ModuleCommands.CommunityGetCurrentGroupID();
                    groupsSelector.SetValue("WhereCondition", String.Format("(GroupID={0})", groupId));
                    break;

                case AvailableGroupsEnum.OnlySingleGroup:
                    if (!string.IsNullOrEmpty(GroupName))
                    {
                        groupsSelector.SetValue("WhereCondition", String.Format("(GroupName = N'{0}' AND GroupSiteID={1})", SqlHelperClass.GetSafeQueryString(GroupName, false), SiteID));
                    }
                    break;

                case AvailableGroupsEnum.None:
                    // Just '(none)' displayed in the selection
                    if (GlobalLibraries == AvailableLibrariesEnum.None)
                    {
                        groupsSelector.SetValue("DisplayNoneWhenEmpty", true);
                        groupsSelector.SetValue("EnabledGroups", false);
                    }
                    groupsSelector.SetValue("WhereCondition", String.Format("({0})", SqlHelperClass.NO_DATA_WHERE));
                    break;
            }

            // Reload group selector based on recently passed settings
            ((IDataUserControl)groupsSelector).ReloadData(true);
        }
        else
        {
            plcGroupSelector.Visible = false;
        }
    }


    /// <summary>
    /// Initializes group selector.
    /// </summary>
    private void InitializeGroupSelector()
    {
        if (groupsSelector == null)
        {
            try
            {
                groupsSelector = LoadControl(controlPath) as FormEngineUserControl;

                // Set up selector
                groupsSelector.ID = "dialogsGroupSelector";
                groupsSelector.SetValue("DisplayCurrentGroup", false);
                groupsSelector.SetValue("SiteID", 0);
                groupsSelector.IsLiveSite = IsLiveSite;
                groupsSelector.Changed += groupSelector_Changed;

                // Get uniselector and set it up
                UniSelector us = groupsSelector.GetValue("CurrentSelector") as UniSelector;
                if (us != null)
                {
                    us.ReturnColumnName = "GroupID";
                }

                // Get dropdownlist and set it up
                DropDownList drpGroups = groupsSelector.GetValue("CurrentDropDown") as DropDownList;
                if (drpGroups != null)
                {
                    drpGroups.AutoPostBack = true;
                }

                // Add control to panel
                pnlGroupSelector.Controls.Add(groupsSelector);
            }
            catch(HttpException)
            {
                // Couldn't load the control
                plcGroupSelector.Visible = false;
            }
        }
    }


    /// <summary>
    /// Initializes library selector.
    /// </summary>
    public void LoadLibrarySelection()
    {
        int groupId = (groupsSelector != null ? ValidationHelper.GetInteger(groupsSelector.GetValue("GroupID"), -1) : 0);

        // Decide what type of libraries to display
        if (groupId == -1)
        {
            librarySelector.Where = String.Format("({0})", SqlHelperClass.NO_DATA_WHERE);
            SetLibrariesEmpty();
        }
        else
        {
            librarySelector.Where = groupId == 0 ? GetGlobalLibrariesCondition() : GetGroupLibrariesCondition();
        }

        librarySelector.GroupID = groupId;
        librarySelector.SiteID = SiteID;
        librarySelector.ReloadData();
    }


    /// <summary>
    /// Returns WHERE condition when group libraries are being displayed.
    /// </summary>
    private string GetGroupLibrariesCondition()
    {
        string result = "";

        // Get currently selected group ID
        int groupId = ValidationHelper.GetInteger(groupsSelector.GetValue("GroupID"), 0);
        if (groupId > 0)
        {
            librarySelector.GroupID = groupId;

            switch (GroupLibraries)
            {
                case AvailableLibrariesEnum.OnlySingleLibrary:
                    librarySelector.SiteID = SiteID;
                    result = String.Format("(LibraryName= N'{0}')", SqlHelperClass.GetSafeQueryString(GroupLibraryName, false));
                    break;

                case AvailableLibrariesEnum.OnlyCurrentLibrary:
                    result = String.Format("(LibraryID={0})", ((MediaLibraryContext.CurrentMediaLibrary != null) ? MediaLibraryContext.CurrentMediaLibrary.LibraryID : 0));
                    break;

                case AvailableLibrariesEnum.All:
                default:
                    librarySelector.SiteID = SiteID;
                    break;
            }
        }

        return result;
    }


    /// <summary>
    /// Returns WHERE condition when global libraries are being displayed.
    /// </summary>
    private string GetGlobalLibrariesCondition()
    {
        // Create WHERE condition according global libraries
        switch (GlobalLibraries)
        {
            case AvailableLibrariesEnum.OnlyCurrentLibrary:
                int libraryId = ((MediaLibraryContext.CurrentMediaLibrary != null) ? MediaLibraryContext.CurrentMediaLibrary.LibraryID : 0);
                return String.Format("(LibraryID={0})", libraryId);

            case AvailableLibrariesEnum.OnlySingleLibrary:
                librarySelector.SiteID = SiteID;
                return String.Format("(LibraryName= N'{0}')", SqlHelperClass.GetSafeQueryString(GlobalLibaryName, false));

            case AvailableLibrariesEnum.All:
            default:
                librarySelector.SiteID = SiteID;
                return "";
        }
    }


    /// <summary>
    /// Ensures right library is selected in the list.
    /// </summary>
    private void PreselectLibrary()
    {
        if ((SelectedLibraryID > 0) && siteSelector.UniSelector.HasData)
        {
            librarySelector.MediaLibraryID = SelectedLibraryID;
        }
    }


    /// <summary>
    /// Ensures right group is selected in the list.
    /// </summary>
    private void PreselectGroup()
    {
        if (SelectedGroupID >= 0)
        {
            // Get dropdownlist and set it up
            DropDownList drpGroups = groupsSelector.GetValue("CurrentDropDown") as DropDownList;
            if (drpGroups != null)
            {
                try
                {
                    drpGroups.SelectedValue = SelectedGroupID.ToString();
                }
                catch { }
            }
        }
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Handler of the event occurring when the site in selector has changed.
    /// </summary>
    void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SiteID = (int)siteSelector.Value;

        // Reload groups selector
        HandleGroupsSelection();

        // Pre-select group
        PreselectGroup();

        // Pre-select library
        PreselectLibrary();

        // Reload libraries
        LoadLibrarySelection();

        RaiseOnLibraryChanged();
    }


    /// <summary>
    /// Handler of the event occurring when the group selector has changed.
    /// </summary>
    protected void groupSelector_Changed(object sender, EventArgs e)
    {
        if (!URLHelper.IsPostback())
        {
            // Pre-select group
            PreselectGroup();
        }

        if (!URLHelper.IsPostback())
        {
            // Pre-select library
            PreselectLibrary();
        }

        // Reload libraries
        LoadLibrarySelection();

        RaiseOnLibraryChanged();
    }


    protected void librarySelector_SelectedLibraryChanged()
    {
        // Let the parent now about library selection change
        RaiseOnLibraryChanged();
    }


    /// <summary>
    /// Fires event when library changed.
    /// </summary>
    private void RaiseOnLibraryChanged()
    {
        // Fire event
        if (LibraryChanged != null)
        {
            LibraryChanged(this, null);
        }
    }

    #endregion
}
