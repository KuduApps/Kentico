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

public partial class CMSModules_MessageBoards_Controls_LiveControls_MessageBoards : CMSAdminItemsControl
{
    #region "Private variables"

    private int mBoardId = 0;
    private int mGroupId = 0;
    private bool mHideWhenGroupIsNotSupplied = false;

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
    /// Current board ID.
    /// </summary>
    public int BoardID
    {
        get
        {
            return this.mBoardId;
        }
        set
        {
            this.mBoardId = value;
        }
    }


    /// <summary>
    /// Current group ID.
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

    #endregion

     protected void Page_Load(object sender, EventArgs e)
    {
        #region "Security"

        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        this.boards.OnCheckPermissions += new CheckPermissionsEventHandler(boards_OnCheckPermissions);
        this.messages.OnCheckPermissions += new CheckPermissionsEventHandler(messages_OnCheckPermissions);

        #endregion

        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        if (this.StopProcessing)
        {
            this.messages.StopProcessing = true;
            this.boards.StopProcessing = true;
        }
        else
        {
            // Hide controls if the control should be hidden
            if ((this.GroupID == 0) && this.HideWhenGroupIsNotSupplied)
            {
                this.Visible = false;
                return;
            }

            this.tabElem.TabControlIdPrefix = "messageboards";

            this.boards.IsLiveSite = this.IsLiveSite;
            this.boards.GroupID = this.GroupID;
            this.boards.DisplayMode = this.DisplayMode;
            this.boards.HideWhenGroupIsNotSupplied = true;

            this.messages.IsLiveSite = this.IsLiveSite;
            this.messages.BoardID = this.BoardID;
            this.messages.GroupID = this.GroupID;
            this.messages.HideWhenGroupIsNotSupplied = true;

            // Initialize the tab control
            InitializeTabs();
        }
    }


    /// <summary>
    /// Initializes the tabs.
    /// </summary>
    private void InitializeTabs()
    {
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("Group_General.Boards.Messages");
        tabs[1, 0] = GetString("Group_General.Boards.Boards");
        
        this.tabElem.Tabs = tabs;
        this.tabElem.OnTabClicked += new EventHandler(tabElem_OnTabChanged);
    }


    protected void tabElem_OnTabChanged(object sender, EventArgs e)
    {
        int tabIndex = this.tabElem.SelectedTab;

        // Handle message list control setting
        this.tabMessages.Visible = (tabIndex == 0);
        if (this.tabMessages.Visible) 
        {
            this.messages.ReloadData(); 
        }

        this.tabBoards.Visible = (tabIndex == 1);
        if (this.tabBoards.Visible) 
        {
            this.boards.ReloadData();
        }
    }


    #region "Private methods"

    /// <summary>
    /// Sets the message boards section to the default state.
    /// </summary>
    private void ResetState() 
    {
        this.tabElem.SelectedTab = 0;
        
        this.tabMessages.Visible = true;
        this.tabBoards.Visible = false;
    }

    #endregion


    #region "Security handlers"

    protected void messages_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    protected void boards_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion 
}
