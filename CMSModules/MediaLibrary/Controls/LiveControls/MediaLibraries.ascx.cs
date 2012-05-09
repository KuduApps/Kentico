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

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.MediaLibrary;

public partial class CMSModules_MediaLibrary_Controls_LiveControls_MediaLibraries : CMSAdminItemsControl, IPostBackEventHandler
{
    #region "Private variables"

    protected int mGroupID = 0;
    protected Guid mGroupGUID = Guid.Empty;
    protected bool mHideWhenGroupIsNotSupplied = false;
    protected bool isNewLibrary = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the group library belongs to.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (this.mGroupID <= 0)
            {
                mGroupID = ValidationHelper.GetInteger(GetValue("GroupID"), 0);
            }
            return this.mGroupID;
        }
        set
        {
            this.mGroupID = value;
        }
    }


    /// <summary>
    /// GUID of the group library belongs to.
    /// </summary>
    public Guid GroupGUID
    {
        get
        {
            if (this.mGroupGUID == Guid.Empty)
            {
                mGroupGUID = ValidationHelper.GetGuid(GetValue("GroupGUID"), Guid.Empty);
            }
            return this.mGroupGUID;
        }
        set
        {
            this.mGroupGUID = value;
        }
    }


    /// <summary>
    /// ID of the media library.
    /// </summary>
    protected int LibraryID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["LibraryID"], 0);
        }
        set
        {
            ViewState.Add("LibraryID", value);
        }
    }


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

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        #region "Security"

        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        #endregion


        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        if (this.StopProcessing)
        {
            this.libraryList.StopProcessing = true;
            this.libraryFiles.StopProcessing = true;
            this.libraryEdit.StopProcessing = true;
            this.librarySecurity.StopProcessing = true;
        }
        else
        {
            // Check if the group was supplied and hide control if necessary
            if ((this.GroupID == 0) && (this.HideWhenGroupIsNotSupplied))
            {
                this.Visible = false;
            }

            // Initialize controls
            SetupControls();
        }
    }


    #region "Private methods"

    /// <summary>
    /// Initializes all the controls used on live control.
    /// </summary>
    private void SetupControls()
    {
        // Set display mode
        this.libraryList.DisplayMode = this.DisplayMode;
        this.libraryFiles.DisplayMode = this.DisplayMode;
        this.libraryEdit.DisplayMode = this.DisplayMode;
        this.librarySecurity.DisplayMode = this.DisplayMode;

        // Initialize tabs & header actions
        InitializeTabs();
        InitializeHeaderActions();

        this.tabElem.TabControlIdPrefix = "libraries";
        this.tabElem.OnTabClicked += new EventHandler(tabElem_OnTabChanged);

        this.lnkEditBack.Text = GetString("Group_General.MediaLibrary.BackToList");
        this.lnkEditBack.Click += new EventHandler(lnkEditBack_Click);

        this.libraryList.GroupID = this.GroupID;
        this.libraryList.OnAction += new CommandEventHandler(libraryList_OnAction);

        // Initialize library edit tab controls
        this.libraryEdit.MediaLibraryID = this.LibraryID;
        this.libraryEdit.MediaLibraryGroupID = this.GroupID;
        this.libraryEdit.MediaLibraryGroupGUID = this.GroupGUID;
        this.libraryEdit.OnSaved += new EventHandler(libraryEdit_OnSaved);

        // Initialize library security tab controls
        this.librarySecurity.MediaLibraryID = this.LibraryID;

        // Initialize library files list tab controls
        this.libraryFiles.LibraryID = this.LibraryID;
    }


    /// <summary>
    /// Initializes used tab menu control.
    /// </summary>
    private void InitializeTabs()
    {
        string[,] tabs = new string[3, 4];
        tabs[0, 0] = GetString("general.files");        
        tabs[1, 0] = GetString("General.general");
        tabs[2, 0] = GetString("general.security");

        this.tabElem.Tabs = tabs;
    }


    /// <summary>
    /// Initializes the header action element.
    /// </summary>
    private void InitializeHeaderActions()
    {
        // New subscription link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("Group_General.MediaLibrary.NewLibrary");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_MediaLibrary/add.png");
        actions[0, 6] = "newmedialibrary";

        this.newLibrary.Actions = actions;
        this.newLibrary.ActionPerformed += new CommandEventHandler(newLibrary_ActionPerformed);
    }


    /// <summary>
    /// Initializes the breadcrumbs controls.
    /// </summary>
    private void InitializeBreadcrumbs()
    {
        if (this.LibraryID > 0)
        {
            MediaLibraryInfo library = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.LibraryID);
            if (library != null)
            {
                this.lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> " + HTMLHelper.HTMLEncode(library.LibraryDisplayName);
            }
        }
        else
        {
            this.lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> " + GetString("Group_General.MediaLibrary.NewLibrary");
        }
    }


    /// <summary>
    /// Handles displaying of library edit form.
    /// </summary>
    private void DisplayLibraryNew()
    {
        isNewLibrary = true;

        this.plcTabsHeader.Visible = true;
        this.pnlTabsMain.Visible = false;
        this.plcHeaderActions.Visible = false;

        this.plcList.Visible = false;

        this.plcTabs.Visible = true;
        this.tabEdit.Visible = true;
        this.tabFiles.Visible = false;
        this.tabSecurity.Visible = false;

        this.libraryEdit.MediaLibraryID = this.LibraryID;
        this.libraryEdit.MediaLibraryGroupID = this.GroupID;
        this.libraryEdit.MediaLibraryGroupGUID = this.GroupGUID;
        this.libraryEdit.ReloadData();
    }


    /// <summary>
    /// Handles displaying of library edit form.
    /// </summary>
    private void DisplayLibraryEdit()
    {
        this.plcTabsHeader.Visible = true;
        this.pnlTabsMain.Visible = true;

        this.plcHeaderActions.Visible = false;

        this.plcList.Visible = false;

        this.plcTabs.Visible = true;
        this.tabEdit.Visible = false;
        this.tabFiles.Visible = true;
        this.tabSecurity.Visible = false;

        this.libraryFiles.LibraryID = this.LibraryID;
        this.libraryFiles.ReloadData();
    }


    /// <summary>
    /// Displays default media library tab content.
    /// </summary>
    private void SetDefault()
    {
        this.plcTabsHeader.Visible = false;
        this.plcHeaderActions.Visible = true;
        this.plcList.Visible = true;
        this.plcTabs.Visible = false;
    }

    #endregion


    #region "Event handlers"

    protected void libraryList_OnAction(object sender, CommandEventArgs e)
    {
        string commandName = e.CommandName.ToLower();
        switch (commandName)
        {
            case "edit":
                this.LibraryID = ValidationHelper.GetInteger(e.CommandArgument, 0);

                this.plcTabs.Visible = true;

                InitializeBreadcrumbs();

                this.tabElem.SelectedTab = 0;

                // Load library data
                DisplayLibraryEdit();
                break;

            case "delete":
                this.LibraryID = 0;

                SetupControls();
                break;

            default:
                break;
        }
    }


    void lnkEditBack_Click(object sender, EventArgs e)
    {
        this.libraryFiles.ShouldProcess = false;

        SetDefault();
    }


    protected void libraryEdit_OnSaved(object sender, EventArgs e)
    {
        isNewLibrary = (this.LibraryID == 0);

        this.LibraryID = this.libraryEdit.MediaLibraryID;

        InitializeBreadcrumbs();

        // If brand new library created
        if (isNewLibrary)
        {
            // Reload library data
            DisplayLibraryEdit();
        }
    }


    protected void newLibrary_ActionPerformed(object sender, CommandEventArgs e)
    {
        this.libraryFiles.ShouldProcess = false;

        // Clear the editing form
        this.libraryEdit.ClearForm();

        // Reset library info in view state and refresh breadcrumbs info
        this.LibraryID = 0;

        InitializeBreadcrumbs();
        DisplayLibraryNew();
    }


    void tabElem_OnTabChanged(object sender, EventArgs e)
    {
        this.plcList.Visible = false;
        this.plcHeaderActions.Visible = false;

        int tabIndex = this.tabElem.SelectedTab;

        this.tabFiles.Visible = (tabIndex == 0);
        if (this.tabFiles.Visible)
        {
            this.plcTabs.Visible = true;
            this.libraryFiles.LibraryID = this.LibraryID;
            this.libraryFiles.ReloadData();
        }
        this.libraryFiles.ShouldProcess = this.tabFiles.Visible;

        this.tabEdit.Visible = (tabIndex == 1);
        if (this.tabEdit.Visible)
        {
            this.libraryEdit.ReloadData();
        }

        this.tabSecurity.Visible = (tabIndex == 2);
    }

    #endregion


    #region "IPostBackEventHandler Members"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument.ToLower() == "reloadtree")
        {
            this.libraryFiles.ReloadControl();
        }
    }

    #endregion
}
