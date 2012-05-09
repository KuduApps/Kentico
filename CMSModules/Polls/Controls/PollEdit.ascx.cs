using System;

using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;

public partial class CMSModules_Polls_Controls_PollEdit : CMSAdminEditControl
{
    #region "Variables"

    private int mSiteID = 0;
    private int mGroupID = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or Sets site ID of the poll.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;
            PollProperties.SiteID = mSiteID;
        }
    }


    /// <summary>
    /// Gets or sets the group ID for which the poll should be created.
    /// </summary>
    public int GroupID
    {
        get
        {
            return mGroupID;
        }
        set
        {
            mGroupID = value;
            PollProperties.GroupID = mGroupID;
        }
    }


    /// <summary>
    /// Gets or sets answer edit property.
    /// </summary>
    public bool AnswerEditSelected
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["answeredit"], false);
        }
        set
        {
            ViewState["answeredit"] = value;
        }
    }

    #endregion


    #region "Security handlers"

    /// <summary>
    /// Polls security - check permission event handler.
    /// </summary>
    void PollSecurity_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }


    /// <summary>
    /// Answer edit - check permission event handler.
    /// </summary>
    void AnswerEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }


    /// <summary>
    /// Answer list - check permission event handler.
    /// </summary>
    void AnswerList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }


    /// <summary>
    /// Poll properties - check permission event handler.
    /// </summary>
    void PollProperties_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        PollProperties.DisplayMode = DisplayMode;
        PollProperties.IsLiveSite = IsLiveSite;
        AnswerList.IsLiveSite = IsLiveSite;
        AnswerEdit.IsLiveSite = IsLiveSite;
        PollSecurity.IsLiveSite = IsLiveSite;
        PollView.IsLiveSite = IsLiveSite;
        PollView.CheckOpen = false;

        PollSecurity.OnCheckPermissions += new CheckPermissionsEventHandler(PollSecurity_OnCheckPermissions);
        AnswerEdit.OnCheckPermissions += new CheckPermissionsEventHandler(AnswerEdit_OnCheckPermissions);
        AnswerList.OnCheckPermissions += new CheckPermissionsEventHandler(AnswerList_OnCheckPermissions);
        PollProperties.OnCheckPermissions += new CheckPermissionsEventHandler(PollProperties_OnCheckPermissions);

        // Setup buttons
        btnNewAnswer.ResourceString = "Polls_Answer_List.NewItemCaption";
        imgNewAnswer.AlternateText = GetString("Polls_Answer_List.NewItemCaption");
        imgNewAnswer.ImageUrl = GetImageUrl("Objects/Polls_PollAnswer/add.png");
        btnResetAnswers.ResourceString = "Polls_Answer_List.ResetButton";
        btnResetAnswers.OnClientClick = "return confirm(" + ScriptHelper.GetString(GetString("Polls_Answer_List.ResetConfirmation")) + ");";
        imgResetAnswers.ImageUrl = GetImageUrl("CMSModules/CMS_Polls/resetanswers.png");
        imgResetAnswers.AlternateText = GetString("Polls_Answer_List.ResetButton");

        // Menu initialization
        tabMenu.UrlTarget = "_self";
        tabMenu.Tabs = new string[4, 5];
        tabMenu.Tabs[0, 0] = GetString("general.general");
        tabMenu.Tabs[1, 0] = GetString("Polls_Edit.Answers");
        tabMenu.Tabs[2, 0] = GetString("general.security");
        tabMenu.Tabs[3, 0] = GetString("general.view");

        tabMenu.UsePostback = true;
        tabMenu.UseClientScript = true;
        tabMenu.TabControlIdPrefix = ClientID;

        // BreadCrumbs setup
        btnBreadCrumbs.ResourceString = "Polls_Answer_Edit.ItemListLink";
        btnBreadCrumbs.Click += new EventHandler(btnBreadCrumbs_Click);

        // Register event handlers
        btnNewAnswer.Click += new EventHandler(btnNewAnswer_Click);
        btnResetAnswers.Click += new EventHandler(btnResetAnswers_Click);
        tabMenu.OnTabClicked += new EventHandler(tabMenu_OnTabClicked);
        AnswerList.OnEdit += new EventHandler(AnswerList_OnEdit);
        AnswerEdit.OnSaved += new EventHandler(AnswerEdit_OnSaved);

        if (!RequestHelper.IsPostBack() && !IsLiveSite)
        {
            ReloadData(false);
        }
    }


    /// <summary>
    /// Reloads data in controls.
    /// </summary>
    /// <param name="forceReload">Forces to reload all data</param>
    public override void ReloadData(bool forceReload)
    {
        base.ReloadData(forceReload);
        DisplayControls(tabMenu.SelectedTab.ToString(), forceReload);
    }


    /// <summary>
    /// Tab menu clicked event handler.
    /// </summary>
    void tabMenu_OnTabClicked(object sender, EventArgs e)
    {
        DisplayControls(tabMenu.SelectedTab.ToString(), false);
    }


    /// <summary>
    /// Displays appropriate controls regarding set properties.
    /// </summary>
    void DisplayControls(string selectedPage, bool forceReload)
    {
        PollProperties.Visible = false;
        AnswerList.Visible = false;
        PollSecurity.Visible = false;
        PollView.Visible = false;
        PollView.StopProcessing = true;
        headerLinks.Visible = false;
        pnlPollsBreadcrumbs.Visible = false;
        pnlPollsLinks.Visible = false;
        AnswerEdit.Visible = false;
        btnResetAnswers.Visible = true;
        imgResetAnswers.Visible = true;

        if (forceReload)
        {
            selectedPage = "0";
            tabMenu.SelectedTab = 0;
        }

        // Display appropriate tab
        switch (selectedPage)
        {
            // Poll properties
            case "0":
            default:
                PollProperties.Visible = true;
                PollProperties.ItemID = this.ItemID;
                
                this.PollProperties.ReloadData();
                break;

            // Answer list
            case "1":
                AnswerList.Visible = true;
                AnswerList.PollId = this.ItemID;
                AnswerList.ReloadData(true);
                headerLinks.Visible = true;
                pnlPollsLinks.Visible = true;
                break;

            // Answer edit
            case "answersedit":
                headerLinks.Visible = true;
                pnlPollsBreadcrumbs.Visible = true;
                pnlPollsLinks.Visible = true;
                AnswerEdit.Visible = true;
                AnswerEdit.PollId = this.ItemID;
                AnswerEdit.ReloadData();
                btnResetAnswers.Visible = false;
                imgResetAnswers.Visible = false;
                this.AnswerEditSelected = true;

                // Initialize breadcrumbs
                string currentPollAnswer = GetString("Polls_Answer_Edit.NewItemCaption");
                if (AnswerEdit.ItemID > 0)
                {
                    PollAnswerInfo pollAnswerObj = PollAnswerInfoProvider.GetPollAnswerInfo(AnswerEdit.ItemID);
                    if (pollAnswerObj != null)
                    {
                        currentPollAnswer = GetString("Polls_Answer_Edit.AnswerLabel") + " " + pollAnswerObj.AnswerOrder.ToString();
                    }
                }
                lblAnswer.Text = currentPollAnswer;
                break;

            // Poll security
            case "2":
                PollSecurity.Visible = true;
                PollSecurity.ItemID = this.ItemID;
                this.PollSecurity.ReloadData();
                break;

            // Poll view
            case "3":
                PollView.Visible = true;
                this.InitPollView(this.ItemID);
                this.PollView.StopProcessing = false;
                this.PollView.ReloadData(false);
                break;
        }
    }


    /// <summary>
    /// Answer list edit action event handler.
    /// </summary>
    void AnswerList_OnEdit(object sender, EventArgs e)
    {
        // Handle events from visible controls only
        if (AnswerList.Visible)
        {
            AnswerEdit.ItemID = AnswerList.SelectedItemID;
            AnswerEdit.PollId = this.ItemID;
            DisplayControls("answersedit", false);
        }
    }


    /// <summary>
    /// Answer Edit OnSave handler.
    /// </summary>
    void AnswerEdit_OnSaved(object sender, EventArgs e)
    {
        // Handle events from visible controls only
        if (AnswerEdit.Visible)
        {
            AnswerEdit.PollId = this.ItemID;
            DisplayControls("answersedit", false);
            AnswerEdit.LoadData();
        }
    }


    /// <summary>
    /// New answer button click.
    /// </summary>
    void btnNewAnswer_Click(object sender, EventArgs e)
    {
        if (CheckModifyPermission(this.ItemID))
        {
            AnswerEdit.ItemID = 0;
            AnswerEdit.PollId = this.ItemID;
            DisplayControls("answersedit", false);
            AnswerEdit.LoadData();
        }
    }


    /// <summary>
    /// Breadcrumbs click handler.
    /// </summary>
    void btnBreadCrumbs_Click(object sender, EventArgs e)
    {
        DisplayControls("1", false);
    }


    /// <summary>
    /// Reset answers button handler.
    /// </summary>
    void btnResetAnswers_Click(object sender, EventArgs e)
    {
        if (CheckModifyPermission(this.ItemID))
        {
            PollAnswerInfoProvider.ResetAnswers(this.ItemID);
            AnswerList.ReloadData();
        }
    }


    /// <summary>
    /// Initializes PollView control.
    /// </summary>
    /// <param name="pollId">ID of current Poll</param>
    protected void InitPollView(int pollId)
    {
        PollInfo pi = PollInfoProvider.GetPollInfo(pollId);

        if (pi != null)
        {
            PollView.PollCodeName = pi.PollCodeName;
            PollView.PollSiteID = pi.PollSiteID;
            PollView.PollGroupID = pi.PollGroupID;
            PollView.CountType = CountTypeEnum.Percentage;
            PollView.ShowGraph = true;
            PollView.ShowResultsAfterVote = true;
            PollView.CheckPermissions = false;
            PollView.CheckVoted = false;
            PollView.HideWhenNotAuthorized = false;
            PollView.CheckOpen = false;
            PollView.Visible = false;
        }
    }


    /// <summary>
    /// Checks modify permission. Returns false if checking failed.
    /// </summary>
    /// <param name="pollId">Poll ID</param>
    private bool CheckModifyPermission(int pollId)
    {
        PollInfo pi = PollInfoProvider.GetPollInfo(pollId);
        if (pi != null)
        {
            return (pi.PollSiteID > 0) && CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_MODIFY) ||
                (pi.PollSiteID <= 0) && CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_GLOBALMODIFY);
        }
        return false;
    }

    #endregion
}
