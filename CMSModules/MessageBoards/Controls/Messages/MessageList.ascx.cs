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
using CMS.CMSHelper;
using CMS.MessageBoard;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.TreeEngine;

public partial class CMSModules_MessageBoards_Controls_Messages_MessageList : CMSAdminListControl
{
    #region "Private variables"

    private int mBoardId = 0;
    private int mGroupId = 0;
    private string mEditPageUrl = "";
    protected string mPostBackRefference = "";
    private int siteId = 0;
    private bool mHideWhenGroupIsNotSupplied = false;
    private bool mShowFilter = true;
    private string mIsApproved = "no";
    private string mIsSpam = "all";
    private string mSiteName = String.Empty;
    private string mItemsPerPage = String.Empty;
    private string mOrderBy = String.Empty;
    private bool mAllowMassActions = true;
    private bool mShowPermissionMessage = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Determines whether to hide the content of the control when group ID is not supplied.
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
    /// If true show permission error.
    /// </summary>
    public bool ShowPermissionMessage
    {
        get
        {
            return this.mShowPermissionMessage;
        }
        set
        {
            this.mShowPermissionMessage = value;
        }
    }


    /// <summary>
    /// Site name of blogs.
    /// </summary>
    public string SiteName
    {
        get
        {
            return mSiteName;
        }
        set
        {
            mSiteName = value;
        }
    }


    /// <summary>
    /// If true mass actions are allowed.
    /// </summary>
    public bool AllowMassActions
    {
        get
        {
            return mAllowMassActions;
        }
        set
        {
            mAllowMassActions = value;
        }
    }


    /// <summary>
    /// Items per page.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return mItemsPerPage;
        }
        set
        {
            mItemsPerPage = value;
        }
    }


    /// <summary>
    /// ID of the current board.
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
    /// Order by.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return mOrderBy;
        }
        set
        {
            mOrderBy = value;
        }
    }


    /// <summary>
    /// Indicates whether shown comments are approved.
    /// </summary>
    public string IsApproved
    {
        get
        {
            return mIsApproved;
        }
        set
        {
            mIsApproved = value;
        }
    }



    /// <summary>
    /// Indicates wheter show spam marked comments.
    /// </summary>
    public string IsSpam
    {
        get
        {
            return mIsSpam;
        }
        set
        {
            mIsSpam = value;
        }
    }


    /// <summary>
    /// ID of the current group.
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
            this.boardSelector.GroupID = value;
        }
    }


    /// <summary>
    /// Target URL for the modal dialog message is editied in.
    /// </summary>
    public string EditPageUrl
    {
        get
        {
            return this.mEditPageUrl;
        }
        set
        {
            this.mEditPageUrl = value;
        }
    }

    /// <summary>
    /// If true filter is shown.
    /// </summary>
    public bool ShowFilter
    {
        get
        {
            return mShowFilter;
        }
        set
        {
            mShowFilter = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the group was supplied and hide control if required
        if ((this.GroupID == 0) && (this.HideWhenGroupIsNotSupplied))
        {
            this.Visible = false;
        }

        // If control should be hidden save view state memory
        if (this.StopProcessing || !this.Visible)
        {
            this.EnableViewState = false;
        }

        if (!ShowFilter)
        {
            RowFilter.Visible = false;
        }

        this.SetContext();

        // Initializes the controls
        SetupControls();

        this.ReleaseContext();
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource))
        {
            this.lblActions.Visible = false;
            this.drpActions.Visible = false;
            this.btnOk.Visible = false;
        }
        else
        {
            // Hide column containing board name when reviewing specific board
            if (this.BoardID > 0)
            {
                this.gridElem.GridView.Columns[6].Visible = false;
            }

            if (ShowPermissionMessage)
            {
                messageElem.Visible = true;
                messageElem.ErrorMessage = GetString("general.nopermission");
            }

            this.lblActions.Visible = true;
            this.drpActions.Visible = true;
            this.btnOk.Visible = true;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControls()
    {
        btnFilter.Text = GetString("general.show");
        btnOk.Text = GetString("general.ok");

        // Mass actions
        this.gridElem.GridOptions.ShowSelection = this.AllowMassActions;
        this.rowActions.Visible = this.AllowMassActions;

        lblSiteName.AssociatedControlClientID = siteSelector.DropDownSingleSelect.ClientID;
        lblBoardName.AssociatedControlClientID = boardSelector.DropDownSingleSelect.ClientID;

        gridElem.IsLiveSite = this.IsLiveSite;
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.ZeroRowsText = GetString("general.nodatafound");

        this.btnOk.OnClientClick += "return MassConfirm('" + this.drpActions.ClientID + "'," + ScriptHelper.GetString(GetString("General.ConfirmGlobalDelete")) + ");";

        ScriptHelper.RegisterDialogScript(this.Page);

        if (this.GroupID == 0)
        {
            this.GroupID = QueryHelper.GetInteger("groupid", 0);
        }

        ReloadFilter();

        if (!RequestHelper.IsPostBack())
        {
            // Preselect filter data
            PreselectFilter(";;" + this.GroupID + ";;;NO;;");
        }

        if (this.GroupID > 0)
        {
            // Hide site selection
            this.plcSite.Visible = false;
        }

        if (this.BoardID > 0)
        {
            // Hide board selection
            this.plcBoard.Visible = false;

            // Hide site selection
            this.plcSite.Visible = false;

            if ((this.GroupID > 0) && this.IsLiveSite)
            {
                InitializeGroupNewMessage();
            }
        }

        siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

        // Reload message list script        
        string board = (this.BoardID > 0 ? this.BoardID.ToString() : this.boardSelector.Value.ToString());
        string group = this.GroupID.ToString();
        string user = HTMLHelper.HTMLEncode(this.txtUserName.Text);
        string message = HTMLHelper.HTMLEncode(this.txtMessage.Text);
        string approved = this.drpApproved.SelectedItem.Value;
        string spam = this.drpSpam.SelectedItem.Value;
        bool changemaster = QueryHelper.GetBoolean("changemaster", false);

        // Set site selector
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.AllowAll = true;
        siteSelector.IsLiveSite = this.IsLiveSite;

        boardSelector.IsLiveSite = this.IsLiveSite;
        boardSelector.GroupID = this.GroupID;

        if (!ShowFilter)
        {
            SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteName);
            if (si != null)
            {
                siteId = si.SiteID;
            }
            if (SiteName == TreeProvider.ALL_SITES)
            {
                siteId = -1;
            }

        }
        else
        {
            siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
            IsApproved = drpApproved.SelectedValue.ToLower();
            IsSpam = drpSpam.SelectedValue.ToLower();
        }


        if (siteId == 0)
        {
            siteId = CMSContext.CurrentSiteID;
            siteSelector.Value = siteId;
        }

        string cmdArg = siteId + ";" + board + ";" + group + ";" + user.Replace(";", "#sc#") + ";" +
            message.Replace(";", "#sc#") + ";" + approved + ";" + spam + ";" + changemaster;

        this.btnRefreshHdn.CommandArgument = cmdArg;
        this.mPostBackRefference = ControlsHelper.GetPostBackEventReference(this.btnRefreshHdn, null);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshBoardList", ScriptHelper.GetScript("function RefreshBoardList(){" +
            mPostBackRefference + "}"));

        this.siteSelector.OnlyRunningSites = this.IsLiveSite;

        string where = "";

        // Sites dropdownlist
        if (siteId > 0)
        {
            where += "BoardSiteID = " + siteId + " AND";
        }

        // Approved dropdownlist
        switch (IsApproved.ToLower())
        {
            case "yes":
                where += " MessageApproved = 1 AND";
                break;

            case "no":
                where += " MessageApproved = 0 AND";
                break;
        }

        // Spam dropdownlist
        switch (IsSpam.ToLower())
        {
            case "yes":
                where += " MessageIsSpam = 1 AND";
                break;

            case "no":
                where += " MessageIsSpam = 0 AND";
                break;
        }

        int selectedBoardId = 0;
        if (mBoardId > 0)
        {
            where += " MessageBoardID = " + mBoardId.ToString() + " AND";
            selectedBoardId = mBoardId;
        }
        else
        {
            // Board dropdownlist
            selectedBoardId = ValidationHelper.GetInteger(boardSelector.Value, 0);
            if (selectedBoardId > 0)
            {
                where += " MessageBoardID = " + selectedBoardId + " AND";
            }
        }

        if (txtUserName.Text.Trim() != "")
        {
            where += " MessageUserName LIKE '%" + txtUserName.Text.Trim().Replace("'", "''") + "%' AND";
        }

        if (txtMessage.Text.Trim() != "")
        {
            where += " MessageText LIKE '%" + txtMessage.Text.Trim().Replace("'", "''") + "%' AND";
        }

        bool isAuthorized = false;

        if (selectedBoardId > 0)
        {
            BoardInfo selectedBoard = BoardInfoProvider.GetBoardInfo(selectedBoardId);
            if (selectedBoard != null)
            {
                isAuthorized = BoardInfoProvider.IsUserAuthorizedToManageMessages(selectedBoard);
            }
        }

        // Show messages to boards only where user is moderator
        if (!isAuthorized && (!(CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.MessageBoards", "Modify") || CMSContext.CurrentUser.IsGroupAdministrator(this.mGroupId))))
        {
            where += " BoardID IN (SELECT BoardID FROM Board_Moderator WHERE Board_Moderator.UserID = " + CMSContext.CurrentUser.UserID + " ) AND ";
        }

        if (this.mGroupId > 0)
        {
            where += " BoardGroupID =" + this.mGroupId;
        }
        else
        {
            where += "(BoardGroupID =0 OR BoardGroupID IS NULL)";
        }

        gridElem.WhereCondition = where;

        if ((!RequestHelper.IsPostBack()) && (!string.IsNullOrEmpty(ItemsPerPage)))
        {
            gridElem.Pager.DefaultPageSize = ValidationHelper.GetInteger(ItemsPerPage, -1);
        }

        if (!String.IsNullOrEmpty(OrderBy))
        {
            gridElem.OrderBy = OrderBy;
        }
    }


    /// <summary>
    /// Initializes New message action for group message board.
    /// </summary>
    private void InitializeGroupNewMessage()
    {
        this.plcNewMessageGroups.Visible = true;

        // New message link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Board.MessageList.NewMessage");
        actions[0, 2] = "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSModules/Groups/CMSPages/Message_Edit.aspx") + "?boardId=" + this.BoardID + "&groupid=" + this.GroupID + "&changemaster=" + QueryHelper.GetBoolean("changemaster", false) + "', 'MessageEdit', 500, 400); return false;";
        actions[0, 3] = "#";
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Board_Message/add.png");

        this.headerActions.Actions = actions;
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        this.boardSelector.SiteID = ValidationHelper.GetInteger(this.siteSelector.Value, 0);
        this.boardSelector.ReloadData(true);

        ReloadData();
    }


    /// <summary>
    /// Gets the information on last selected filter configuration and pre-selects the actual values.
    /// </summary>
    private void PreselectFilter(string filter)
    {
        string site = "";
        string board = "";
        string username = "";
        string message = "";
        string approved = "no";
        string isspam = "";

        string filterParams = HttpUtility.HtmlDecode(filter);

        if (!string.IsNullOrEmpty(filterParams))
        {
            string[] paramsArr = filterParams.Split(';');
            site = paramsArr[0];
            board = paramsArr[1];
            this.GroupID = ValidationHelper.GetInteger(paramsArr[2], 0);
            username = paramsArr[3].Replace("#sc#", ";");
            message = paramsArr[4].Replace("#sc#", ";");
            approved = paramsArr[5];
            isspam = paramsArr[6];
        }

        // Get filter values from the query string by default 
        // or ViewState if the control is used as part of GroupProfile on Live site
        site = QueryHelper.GetString("site", site);
        board = QueryHelper.GetString("board", board);
        username = QueryHelper.GetString("username", username);
        message = QueryHelper.GetString("message", message);
        approved = QueryHelper.GetString("approved", approved);
        isspam = QueryHelper.GetString("isspam", isspam);

        if (site != "")
        {
            siteId = ValidationHelper.GetInteger(site, 0);
            siteSelector.Value = siteId;
        }
        else
        {
            if (this.BoardID == 0)
            {
                siteId = CMSContext.CurrentSiteID;
                siteSelector.Value = siteId;
            }
        }

        if (board != "")
        {
            if (this.boardSelector.UniSelector.HasData)
            {
                this.boardSelector.Value = board;
            }
        }

        if (username != "")
        {
            this.txtUserName.Text = username;
        }

        if (message != "")
        {
            this.txtMessage.Text = message;
        }

        if (approved != "")
        {
            if (this.drpApproved.Items.Count > 0)
            {
                this.drpApproved.SelectedValue = approved;
            }
        }

        if (isspam != "")
        {
            if (this.drpSpam.Items.Count > 0)
            {
                this.drpSpam.SelectedValue = isspam;
            }
        }
    }


    /// <summary>
    /// Load data according to filter setings.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        gridElem.ReloadData();
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Sets the filter default values.
    /// </summary>
    public void ReloadFilter()
    {
        if (this.GroupID == 0)
        {
            this.plcSite.Visible = CMSContext.CurrentUser.IsGlobalAdministrator;
        }

        if (this.BoardID == 0)
        {
            this.boardSelector.IsLiveSite = this.IsLiveSite;
            this.boardSelector.SiteID = CMSContext.CurrentSiteID;
            this.boardSelector.GroupID = this.GroupID;
            this.boardSelector.ReloadData(false);
        }

        if (this.drpApproved.Items.Count == 0)
        {
            drpApproved.Items.Add(new ListItem(GetString("general.selectall"), "ALL"));
            drpApproved.Items.Add(new ListItem(GetString("general.yes"), "YES"));
            drpApproved.Items.Add(new ListItem(GetString("general.no"), "NO"));
            this.drpApproved.SelectedIndex = 0;
        }

        if (this.drpSpam.Items.Count == 0)
        {
            drpSpam.Items.Add(new ListItem(GetString("general.selectall"), "ALL"));
            drpSpam.Items.Add(new ListItem(GetString("general.yes"), "YES"));
            drpSpam.Items.Add(new ListItem(GetString("general.no"), "NO"));
            this.drpSpam.SelectedIndex = 0;
        }

        if (this.drpActions.Items.Count == 0)
        {
            drpActions.Items.Add(new ListItem(GetString("Board.MessageList.Action.Select"), "SELECT"));
            drpActions.Items.Add(new ListItem(GetString("Board.MessageList.Action.Approve"), "APPROVE"));
            drpActions.Items.Add(new ListItem(GetString("Board.MessageList.Action.Reject"), "REJECT"));
            drpActions.Items.Add(new ListItem(GetString("Board.MessageList.Action.Spam"), "SPAM"));
            drpActions.Items.Add(new ListItem(GetString("Board.MessageList.Action.NoSpam"), "NOSPAM"));
            drpActions.Items.Add(new ListItem(GetString("Board.MessageList.Action.Delete"), "DELETE"));
        }
    }

    #endregion


    #region "Event handlers"

    protected void btnRefreshHdn_Command(object sender, CommandEventArgs e)
    {
        PreselectFilter(Convert.ToString(e.CommandArgument));
        ReloadData();
    }


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        bool approve = false;
        switch (sourceName.ToLower())
        {
            case "messageapproved":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "messageisspam":
                return UniGridFunctions.ColoredSpanYesNoReversed(parameter);

            case "messagetext":
                string text = parameter.ToString();
                if (text.Length > 30)
                {
                    text = text.Substring(0, 30) + "...";
                }
                return HTMLHelper.HTMLEncode(text);

            case "messagetooltip":
                return HTMLHelper.HTMLEncodeLineBreaks(parameter.ToString());

            case "edit":
                ImageButton editButton = ((ImageButton)sender);
                int boardID = ValidationHelper.GetInteger(((DataRowView)((GridViewRow)parameter).DataItem).Row["BoardID"], 0);

                string url = "~/CMSModules/MessageBoards/Tools/Messages/Message_Edit.aspx";
                if (this.IsLiveSite)
                {
                    url = "~/CMSModules/MessageBoards/CMSPages/Message_Edit.aspx";
                }

                editButton.OnClientClick = "modalDialog('" + CMSContext.ResolveDialogUrl(((this.EditPageUrl == "") ? url : this.EditPageUrl)) +
                    "?messageboardid=" + boardID + "&messageId=" + editButton.CommandArgument + "', 'MessageEdit', 500, 400); return false;";
                break;

            case "approve":
                approve = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["MessageApproved"], false);
                if (!approve)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Approve.png");
                    button.ToolTip = GetString("general.approve");
                }
                else
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Reject.png");
                    button.ToolTip = GetString("general.reject");
                }
                break;

            case "messageinserted":
                return CMSContext.ConvertDateTime(ValidationHelper.GetDateTime(parameter, DataHelper.DATETIME_NOT_SELECTED), this).ToString();

        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        BoardMessageInfo message = BoardMessageInfoProvider.GetBoardMessageInfo(Convert.ToInt32(actionArgument));
        BoardInfo bi = BoardInfoProvider.GetBoardInfo(message.MessageBoardID);
        string[] argument = null;

        switch (actionName)
        {
            case "delete":
            case "approve":
                // Check whether user is board moderator first 
                if (!BoardInfoProvider.IsUserAuthorizedToManageMessages(bi))
                {
                    // Then check modify to messageboards
                    if (!CheckPermissions("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY))
                    {
                        return;
                    }
                }
                break;
        }

        switch (actionName)
        {
            case "delete":
                if (message != null)
                {
                    BoardMessageInfoProvider.DeleteBoardMessageInfo(message);
                }
                break;

            case "approve":
                if (message != null)
                {
                    if (message.MessageApproved)
                    {
                        // Reject message
                        message.MessageApproved = false;
                        message.MessageApprovedByUserID = 0;
                    }
                    else
                    {
                        // Approve message
                        message.MessageApproved = true;
                        message.MessageApprovedByUserID = CMSContext.CurrentUser.UserID;
                    }
                    BoardMessageInfoProvider.SetBoardMessageInfo(message);
                }
                break;

            default:
                break;
        }

        this.RaiseOnAction(actionName, ((argument == null) ? actionArgument : argument));
    }


    protected void btnOk_Clicked(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        if (drpActions.SelectedValue != "SELECT")
        {
            ArrayList list = gridElem.SelectedItems;
            if (list.Count > 0)
            {
                foreach (string messageId in list)
                {
                    BoardMessageInfo message = BoardMessageInfoProvider.GetBoardMessageInfo(Convert.ToInt32(messageId));
                    switch (drpActions.SelectedValue)
                    {
                        case "DELETE":
                            BoardMessageInfoProvider.DeleteBoardMessageInfo(message);
                            break;

                        case "APPROVE":
                            if (!message.MessageApproved)
                            {
                                message.MessageApproved = true;
                                message.MessageApprovedByUserID = CMSContext.CurrentUser.UserID;
                                BoardMessageInfoProvider.SetBoardMessageInfo(message);
                            }
                            break;

                        case "REJECT":
                            if (message.MessageApproved)
                            {
                                message.MessageApproved = false;
                                message.MessageApprovedByUserID = 0;
                                BoardMessageInfoProvider.SetBoardMessageInfo(message);
                            }
                            break;

                        case "SPAM":
                            if (!message.MessageIsSpam)
                            {
                                message.MessageIsSpam = true;
                                BoardMessageInfoProvider.SetBoardMessageInfo(message);
                            }
                            break;

                        case "NOSPAM":
                            if (message.MessageIsSpam)
                            {
                                message.MessageIsSpam = false;
                                BoardMessageInfoProvider.SetBoardMessageInfo(message);
                            }
                            break;
                    }
                }
            }
            else
            {
                ltlScript.Text += ScriptHelper.GetAlertScript(GetString("general.noitems"));
            }
        }

        this.gridElem.ResetSelection();

        this.ReloadData();
    }

    #endregion
}
