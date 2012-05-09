using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;

public partial class CMSModules_Polls_Controls_Polls : CMSAdminEditControl
{
    #region "Variables"

    private int mGroupID = 0;
    private int mSiteID = 0;
    private Guid mGroupGUID = Guid.Empty;
    private bool mHideWhenGroupIsNotSupplied = false;
    private bool dataLoaded = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets ID of site.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteID <= 0)
            {
                mSiteID = ValidationHelper.GetInteger(this.GetValue("SiteID"), CMSContext.CurrentSiteID);
            }
            return mSiteID;
        }
        set
        {
            this.mSiteID = value;
            this.PollNew.SiteID = this.mSiteID;
            this.PollEdit.SiteID = this.mSiteID;
        }
    }


    /// <summary>
    /// Gets or sets ID of group.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (mGroupID <= 0)
            {
                mGroupID = ValidationHelper.GetInteger(this.GetValue("GroupID"), 0);
            }

            return mGroupID;
        }
        set
        {
            this.mGroupID = value;
        }
    }


    /// <summary>
    /// Gets or sets GUID of group.
    /// </summary>
    public Guid GroupGUID
    {
        get
        {
            if (mGroupGUID == Guid.Empty)
            {
                mGroupGUID = ValidationHelper.GetGuid(this.GetValue("GroupGUID"), Guid.Empty);
            }

            return mGroupGUID;
        }
        set
        {
            this.mGroupGUID = value;
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


    /// <summary>
    /// Gets or sets switch to display appropriate controls.
    /// </summary>
    public string SelectedControl
    {
        get
        {
            return ValidationHelper.GetString(this.ViewState["selectedcontrol" + this.ClientID], "list");
        }
        set
        {
            ViewState["selectedcontrol" + this.ClientID] = (object)value;
        }
    }


    public bool DelayedReload
    {
        get
        {
            return PollsList.DelayedReload;
        }
        set
        {
            PollsList.DelayedReload = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        #region "Security"

        if (!CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_READ))
        {
            return;
        }

        PollEdit.OnCheckPermissions += new CheckPermissionsEventHandler(PollEdit_OnCheckPermissions);
        PollNew.OnCheckPermissions += new CheckPermissionsEventHandler(PollNew_OnCheckPermissions);
        PollsList.OnCheckPermissions += new CheckPermissionsEventHandler(PollsList_OnCheckPermissions);

        #endregion

        PollsList.IsLiveSite = IsLiveSite;
        PollEdit.IsLiveSite = IsLiveSite;
        PollNew.IsLiveSite = IsLiveSite;

        PollNew.SiteID = SiteID;
        PollEdit.SiteID = SiteID;

        PollsList.GroupId = GroupID;
        PollNew.GroupID = GroupID;
        PollNew.GroupGUID = GroupGUID;
        PollEdit.GroupID = GroupID;

        // Set display mode
        PollNew.DisplayMode = DisplayMode;
        PollEdit.DisplayMode = DisplayMode;

        if ((GroupID == 0) && HideWhenGroupIsNotSupplied)
        {
            Visible = false;
            return;
        }

        PollsList.OnEdit += new EventHandler(PollsList_OnEdit);
        PollNew.OnSaved += new EventHandler(PollNew_OnSaved);
        btnNewPoll.Click += new EventHandler(btnNewPoll_Click);
        btnBreadCrumbs.Click += new EventHandler(btnBreadCrumbs_Click);

        if (!RequestHelper.IsPostBack() && (!IsLiveSite))
        {
            ReloadData(false);
        }
    }


    #region "Security handlers"

    void PollsList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void PollNew_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void PollEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    /// <summary>
    /// Displays controls in dependence on properties.
    /// </summary>
    public override void ReloadData(bool forceReload)
    {
        base.ReloadData(forceReload);

        // Setup button
        imgNewPoll.ImageUrl = GetImageUrl("Objects/Polls_Poll/add.png");
        imgNewPoll.AlternateText = GetString("polls_new.newitemcaption");
        btnNewPoll.ResourceString = "Polls_List.NewItemCaption";

        // Setup breadcrumbs
        btnBreadCrumbs.ResourceString = "Polls_Edit.itemlistlink";

        // Setup panels
        pnlPollsHeaderLinks.Visible = false;
        pnlPollsHeaderBreadCrumbs.Visible = false;
        pnlList.Visible = false;
        pnlEdit.Visible = false;
        pnlPollNew.Visible = false;

        // Display appropriate poll controls
        switch (this.SelectedControl)
        {
            case "new":
                {
                    pnlPollsHeaderBreadCrumbs.Visible = true;
                    pnlPollNew.Visible = true;
                    PollNew.ReloadData();
                    lblPoll.ResourceString = "polls_new.newitemcaption";
                    break;
                }
            case "edit":
                {
                    pnlPollsHeaderBreadCrumbs.Visible = true;
                    pnlEdit.Visible = true;
                    PollEdit.ReloadData(true);
                    PollInfo pi = PollInfoProvider.GetPollInfo(this.ItemID);
                    if (pi != null)
                    {
                        lblPoll.ResourceString = null;
                        lblPoll.Text = HTMLHelper.HTMLEncode(pi.PollDisplayName);
                    }
                    break;
                }
            case "list":
            default:
                {
                    if (!dataLoaded || forceReload)
                    {
                        pnlPollsHeaderLinks.Visible = true;
                        pnlList.Visible = true;
                        PollsList.GroupId = this.GroupID;
                        PollsList.ReloadData();
                        dataLoaded = true;
                    }
                    break;
                }
        }
    }


    /// <summary>
    /// New poll saved event handler.
    /// </summary>
    void PollNew_OnSaved(object sender, EventArgs e)
    {
        // Handle events only from visible controls
        if (PollNew.Visible)
        {
            this.ItemID = PollNew.ItemID;
            PollEdit.ItemID = PollNew.ItemID;
            PollEdit.ReloadData(true);
            this.SelectedControl = "edit";
            ReloadData();
        }
    }


    /// <summary>
    /// Edit poll click event handler.
    /// </summary>
    void PollsList_OnEdit(object sender, EventArgs e)
    {
        // Handle events only from visible controls
        if (PollsList.Visible)
        {
            this.ItemID = PollsList.SelectedItemID;
            PollEdit.ItemID = PollsList.SelectedItemID;
            this.SelectedControl = "edit";
            this.ReloadData();
        }
    }


    /// <summary>
    /// New poll click handler.
    /// </summary>
    void btnNewPoll_Click(object sender, EventArgs e)
    {
        this.ItemID = 0;
        PollEdit.ItemID = 0;
        PollEdit.ReloadData(true);
        PollNew.ItemID = 0;
        PollNew.ClearForm();
        this.SelectedControl = "new";
        ReloadData();
    }


    /// <summary>
    /// Breadcrumbs click event handler.
    /// </summary>
    void btnBreadCrumbs_Click(object sender, EventArgs e)
    {
        this.SelectedControl = "list";
        ReloadData(true);
    }
}
