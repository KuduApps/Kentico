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
using CMS.MessageBoard;

public partial class CMSModules_MessageBoards_Controls_LiveControls_Boards : CMSAdminItemsControl
{
    #region "Selected tab enumeration"

    private enum SelectedControlEnum { Listing, Messages, General, Subscriptions, Moderators, Security };

    #endregion


    #region "Private fields"

    private BoardInfo board = null;

    private int mGroupID = 0;
    private bool mHideWhenGroupIsNotSupplied = false;
    private const string breadCrumbsSeparator = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns currently selected tab.
    /// </summary>
    private SelectedControlEnum SelectedControl
    {
        get
        {
            int selectedTab = ValidationHelper.GetInteger(ViewState["SelectedControl"], 0);
            switch (selectedTab)
            {
                case 1:
                    return SelectedControlEnum.General;
                case 2:
                    return SelectedControlEnum.Moderators;
                case 3:
                    return SelectedControlEnum.Security;
                case 4:
                    return SelectedControlEnum.Subscriptions;
                default:
                    return SelectedControlEnum.Messages;
            }
        }
        set
        {
            switch (value)
            {
                case SelectedControlEnum.General:
                    this.tabElem.SelectedTab = 1;
                    break;

                case SelectedControlEnum.Moderators:
                    this.tabElem.SelectedTab = 2;
                    break;

                case SelectedControlEnum.Security:
                    this.tabElem.SelectedTab = 3;
                    break;

                case SelectedControlEnum.Subscriptions:
                    this.tabElem.SelectedTab = 4;
                    break;

                case SelectedControlEnum.Messages:
                    this.tabElem.SelectedTab = 0;
                    break;
            }
        }
    }


    /// <summary>
    /// Current board ID for internal use.
    /// </summary>
    private int BoardID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["BoardID"], 0);
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Current group ID.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (this.mGroupID == 0)
            {
                this.mGroupID = ValidationHelper.GetInteger(this.GetValue("GroupID"), 0);
            }

            if (this.mGroupID == 0)
            {
                this.mGroupID = QueryHelper.GetInteger("groupid", 0);
            }
            return this.mGroupID;
        }
        set
        {
            this.mGroupID = value;
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
        // Check if the group was supplied and hide control if necessary
        if ((this.GroupID == 0) && (this.HideWhenGroupIsNotSupplied))
        {
            this.Visible = false;
        }

        if (this.StopProcessing || !this.Visible)
        {
            this.EnableViewState = false;
            this.boardMessages.StopProcessing = true;
            this.boardList.StopProcessing = true;
            this.boardEdit.StopProcessing = true;
            this.boardModerators.StopProcessing = true;
            this.boardSecurity.StopProcessing = true;
            this.boardSubscriptions.StopProcessing = true;
            return;
        }

        // Initializes the controls
        SetupControls();

        // Display current control
        if (ViewState["SelectedControl"] != null)
        {
            DisplayControl(this.SelectedControl, false);
        }

        // Reload data if necessary
        if (!URLHelper.IsPostback() && !this.IsLiveSite)
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    private void SetupControls()
    {
        this.tabElem.TabControlIdPrefix = "boards";
        this.tabElem.OnTabClicked += new EventHandler(tabElem_OnTabChanged);

        this.lnkEditBack.Text = GetString("Group_General.Boards.Boards.BackToList");
        this.lnkEditBack.Click += new EventHandler(lnkEditBack_Click);

        // Register for the security events
        this.boardList.OnCheckPermissions += new CheckPermissionsEventHandler(boardList_OnCheckPermissions);
        this.boardEdit.OnCheckPermissions += new CheckPermissionsEventHandler(boardEdit_OnCheckPermissions);
        this.boardModerators.OnCheckPermissions += new CheckPermissionsEventHandler(boardModerators_OnCheckPermissions);
        this.boardSecurity.OnCheckPermissions += new CheckPermissionsEventHandler(boardSecurity_OnCheckPermissions);

        // Setup controls        
        this.boardList.IsLiveSite = this.IsLiveSite;
        this.boardList.GroupID = this.GroupID;
        this.boardList.OnAction += new CommandEventHandler(boardList_OnAction);

        this.boardMessages.IsLiveSite = this.IsLiveSite;
        this.boardMessages.OnCheckPermissions += new CheckPermissionsEventHandler(boardMessages_OnCheckPermissions);
        this.boardMessages.BoardID = this.BoardID;
        this.boardMessages.GroupID = this.GroupID;
        this.boardMessages.EditPageUrl = (this.GroupID > 0) ? "~/CMSModules/Groups/CMSPages/Message_Edit.aspx" : "~/CMSModules/MessageBoards/CMSPages/Message_Edit.aspx";

        this.boardEdit.IsLiveSite = this.IsLiveSite;
        this.boardEdit.BoardID = this.BoardID;
        this.boardEdit.DisplayMode = this.DisplayMode;

        this.boardModerators.IsLiveSite = this.IsLiveSite;
        this.boardModerators.BoardID = this.BoardID;

        this.boardSecurity.IsLiveSite = this.IsLiveSite;
        this.boardSecurity.BoardID = this.BoardID;
        this.boardSecurity.GroupID = this.GroupID;

        this.boardSubscriptions.IsLiveSite = this.IsLiveSite;
        this.boardSubscriptions.BoardID = this.BoardID;
        this.boardSubscriptions.GroupID = this.GroupID;

        // Initialize tab control
        string[,] tabs = new string[5, 4];
        tabs[0, 0] = GetString("Group_General.Boards.Boards.Messages");
        tabs[1, 0] = GetString("Group_General.Boards.Boards.Edit");
        tabs[2, 0] = GetString("Group_General.Boards.Boards.Moderators");
        tabs[3, 0] = GetString("Group_General.Boards.Boards.Security");
        tabs[4, 0] = GetString("Group_General.Boards.Boards.SubsList");
        this.tabElem.Tabs = tabs;

        // Initialize breadcrubms
        if (this.BoardID > 0)
        {
            board = BoardInfoProvider.GetBoardInfo(this.BoardID);
            if (board != null)
            {
                this.lblEditBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(board.BoardDisplayName);
            }
        }
    }


    /// <summary>
    /// Displays specified control.
    /// </summary>
    /// <param name="selectedControl">Control to be displayed</param>
    /// <param name="reload">If True, ReloadData on child control is called</param>
    private void DisplayControl(SelectedControlEnum selectedControl, bool reload)
    {
        // First hide and stop all elements
        this.plcList.Visible = false;
        this.boardList.StopProcessing = true;

        this.plcTabs.Visible = true;
        this.plcTabsHeader.Visible = true;

        this.tabEdit.Visible = false;
        this.boardEdit.StopProcessing = true;

        this.tabMessages.Visible = false;
        this.boardMessages.StopProcessing = true;

        this.tabModerators.Visible = false;
        this.boardModerators.StopProcessing = true;

        this.tabSecurity.Visible = false;
        this.boardSecurity.StopProcessing = true;

        this.tabSubscriptions.Visible = false;
        this.boardSubscriptions.StopProcessing = true;

        // Set correct tab
        this.SelectedControl = selectedControl;
        this.pnlContent.CssClass = "TabBody";

        // Enable currently selected element
        switch (selectedControl)
        {
            case SelectedControlEnum.Listing:
                this.pnlContent.CssClass = "";
                this.plcTabs.Visible = false;
                this.plcTabsHeader.Visible = false;
                this.plcList.Visible = true;
                this.boardList.StopProcessing = false;
                if (reload)
                {
                    this.boardList.ReloadData();
                }
                break;

            case SelectedControlEnum.General:
                this.tabEdit.Visible = true;
                this.boardEdit.StopProcessing = false;
                if (reload)
                {
                    this.boardEdit.ReloadData();
                }
                break;

            case SelectedControlEnum.Messages:
                this.tabMessages.Visible = true;
                this.boardMessages.StopProcessing = false;
                if (reload)
                {
                    this.boardMessages.IsLiveSite = this.IsLiveSite;
                    this.boardMessages.BoardID = ValidationHelper.GetInteger(ViewState["BoardID"], 0);
                    this.boardMessages.ReloadData();
                }

                // Breadcrumbs
                if (board == null)
                {
                    board = BoardInfoProvider.GetBoardInfo(this.BoardID);
                    if (board != null)
                    {
                        this.lblEditBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(board.BoardDisplayName);
                    }
                }

                break;

            case SelectedControlEnum.Moderators:
                this.tabModerators.Visible = true;
                this.boardModerators.StopProcessing = false;
                if (reload)
                {
                    this.boardModerators.ReloadData(true);
                }
                break;

            case SelectedControlEnum.Security:
                this.tabSecurity.Visible = true;
                this.boardSecurity.StopProcessing = false;
                if (reload)
                {
                    this.boardSecurity.ReloadData();
                }
                break;

            case SelectedControlEnum.Subscriptions:
                this.tabSubscriptions.Visible = true;
                this.boardSubscriptions.StopProcessing = false;
                if (reload)
                {
                    this.boardSubscriptions.BoardID = this.BoardID;
                    this.boardSubscriptions.ReloadData();
                }                                

                break;
        }
    }


    /// <summary>
    /// Reloads the contol data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControls();

        DisplayControl(SelectedControlEnum.Listing, true);
    }


    /// <summary>
    /// Displays the default contol.
    /// </summary>
    public void SetDefault()
    {
        DisplayControl(SelectedControlEnum.Listing, true);
    }


    /// <summary>
    /// Clears the boards filter up.
    /// </summary>
    public void ClearFilter()
    {
        this.boardList.ClearFilter();
    }


    #region "Event handlers"
    
    protected void tabElem_OnTabChanged(object sender, EventArgs e)
    {
        ViewState.Add("SelectedControl", this.tabElem.SelectedTab);
        DisplayControl(this.SelectedControl, true);
    }


    protected void boardList_OnAction(object sender, CommandEventArgs e)
    {
        if ((e.CommandName.ToLower() == "edit") && this.plcList.Visible)
        {
            ViewState["BoardID"] = e.CommandArgument;
            DisplayControl(SelectedControlEnum.Messages, true);

        }
    }


    protected void lnkEditBack_Click(object sender, EventArgs e)
    {
        ViewState.Add("SelectedControl", null);
        DisplayControl(SelectedControlEnum.Listing, true);
    }

    #endregion


    #region "Security event handlers"

    protected void boardList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        this.RaiseOnCheckPermissions(permissionType, sender);
    }


    protected void boardMessages_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        this.RaiseOnCheckPermissions(permissionType, sender);
    }


    protected void boardSecurity_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        this.RaiseOnCheckPermissions(permissionType, sender);
    }


    protected void boardModerators_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        this.RaiseOnCheckPermissions(permissionType, sender);
    }


    protected void boardEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        this.RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion
}
