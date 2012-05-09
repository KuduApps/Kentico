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

using CMS.GlobalHelper;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Forums_Controls_LiveControls_Group : CMSAdminItemsControl
{
    #region "Variables"

    private int mGroupId = 0;
    private Guid mCommunityGroupGUID = Guid.Empty;
    private const string breadCrumbsSeparator = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";
    private bool displayControlPerformed = false;
    private bool listVisible = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the Group ID.
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
    /// Gets or sets the Group GUID.
    /// </summary>
    public Guid CommunityGroupGUID
    {
        get
        {
            return this.mCommunityGroupGUID;
        }
        set
        {
            this.mCommunityGroupGUID = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        #region "Security"

        forumList.OnCheckPermissions += new CheckPermissionsEventHandler(forumList_OnCheckPermissions);
        forumEdit.OnCheckPermissions += new CheckPermissionsEventHandler(forumEdit_OnCheckPermissions);
        forumNew.OnCheckPermissions += new CheckPermissionsEventHandler(forumNew_OnCheckPermissions);
        groupEdit.OnCheckPermissions += new CheckPermissionsEventHandler(groupEdit_OnCheckPermissions);

        #endregion

        listVisible = plcForumList.Visible;
        tabForums.Visible = true;
        plcForumList.Visible = true;
        forumList.Visible = true;
        

        this.tabElem.TabControlIdPrefix = "group";
        this.groupEdit.GroupID = this.mGroupId;
        this.groupEdit.DisplayMode = this.DisplayMode;

        this.forumNew.GroupID = this.mGroupId;
        this.forumNew.CommunityGroupGUID = this.CommunityGroupGUID;
        this.forumNew.DisplayMode = this.DisplayMode;

        this.forumNew.DisplayMode = this.DisplayMode;

        this.forumList.GroupID = this.mGroupId;

        int forumId = ValidationHelper.GetInteger(ViewState["ForumID"], 0);
        this.forumEdit.ForumID = forumId;
        this.forumEdit.DisplayMode = this.DisplayMode;

        this.forumList.OnAction += new CommandEventHandler(forumList_OnAction);
        this.forumNew.OnSaved += new EventHandler(forumNew_OnSaved);

        // New item link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("Forum_List.NewItemCaption");
        actions[0, 5] = GetImageUrl("Objects/Forums_Forum/add.png");
        actions[0, 6] = "newforum";
        this.actionsElem.Actions = actions;
        this.actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);

        InitializeTabs();
        InitializeBreadcrumbs();

        this.titleElemEdit.TitleText = GetString("forum_edit.headercaption");
        this.titleElemEdit.TitleImage = GetImageUrl("Objects/Forums_Forum/object.png");
    }

    #region "Security handlers"

    void groupEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void forumNew_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void forumEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void forumList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    /// <summary>
    /// New forum saved handler.
    /// </summary>
    protected void forumNew_OnSaved(object sender, EventArgs e)
    {
        this.plcForumEdit.Visible = true;
        this.plcForumList.Visible = false;
        this.tabNewForum.Visible = false;
        this.tabForums.Visible = true;

        int forumId = this.forumNew.ForumID;
        ViewState["ForumID"] = forumId;

        ForumInfo fi = ForumInfoProvider.GetForumInfo(forumId);
        if (fi != null)
        {
            this.lblEditBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(fi.ForumDisplayName);
        }

        this.forumEdit.ForumID = forumId;
        this.forumEdit.ReloadData();
    }


    /// <summary>
    /// New forum click handler.
    /// </summary>
    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "newforum":
                DisplayControl("new");
                break;
        }
    }


    /// <summary>
    /// Edit forum action.
    /// </summary>
    protected void forumList_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToString())
        {
            case "edit":
                
                int forumId = ValidationHelper.GetInteger(e.CommandArgument, 0);
                ViewState["ForumID"] = forumId;

                ForumInfo fi = ForumInfoProvider.GetForumInfo(forumId);
                if (fi != null)
                {
                    this.lblEditBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(fi.ForumDisplayName);
                }

                this.forumEdit.ForumID = forumId;
                DisplayControl("edit");

                break;

            default:
                DisplayControl("list");
                break;
        }
    }


    /// <summary>
    /// Show correct tab.
    /// </summary>
    protected void forumTabElem_OnTabChanged(object sender, EventArgs e)
    {
        int tab = this.tabElem.SelectedTab;
        if (tab == 0)
        {
            DisplayControl("list");
        }
        else if (tab == 1)
        {
            DisplayControl("group");
        }
    }


    /// <summary>
    /// Reloads the form data.
    /// </summary>
    public override void ReloadData()
    {
        this.groupEdit.GroupID = this.mGroupId;
        this.groupEdit.DisplayMode = this.DisplayMode;

        this.forumNew.GroupID = this.mGroupId;
        this.forumNew.DisplayMode = this.DisplayMode;

        this.forumList.GroupID = this.mGroupId;

        DisplayControl("list");        
    }   


    /// <summary>
    /// Initializes the tabs.
    /// </summary>
    private void InitializeTabs()
    {
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("Group_General.Forums");
        tabs[1, 0] = GetString("general.general");
        this.tabElem.Tabs = tabs;
        this.tabElem.OnTabClicked += new EventHandler(forumTabElem_OnTabChanged);
    }


    /// <summary>
    /// Initializes the breadcrumbs.
    /// </summary>
    private void InitializeBreadcrumbs()
    {
        this.lblNewBack.Text = breadCrumbsSeparator + GetString("Forum_Edit.NewForum");

        this.lnkEditBack.Text = GetString("forum_list.headercaption");
        this.lnkNewBack.Text = GetString("forum_list.headercaption");

        this.lnkEditBack.Click += new EventHandler(lnkEditBack_Click);
        this.lnkNewBack.Click += new EventHandler(lnkNewBack_Click);
    }


    protected void lnkNewBack_Click(object sender, EventArgs e)
    {
        DisplayControl("list");
    }


    protected void lnkEditBack_Click(object sender, EventArgs e)
    {
        DisplayControl("list");
    }


    private void DisplayControl(string selectedControl)
    {
        tabElem.SelectedTab = 0;
        plcForumList.Visible = false;
        plcForumEdit.Visible = false;
        tabForums.Visible = false;
        tabGeneral.Visible = false;
        tabNewForum.Visible = false;

        displayControlPerformed = true;

        switch (selectedControl.ToLower())
        {
            // New forum
            case "new":
                forumNew.ReloadData();
                tabNewForum.Visible = true;              
                break;

            // Edit forum
            case "edit":
                forumEdit.ReloadData();
                plcForumEdit.Visible = true;
                tabForums.Visible = true;
                break;

            // Edit forum group
            case "group":
                groupEdit.ReloadData();
                tabGeneral.Visible = true;
                tabElem.SelectedTab = 1;
                break;

            // Forum list
            default:
                plcForumList.Visible = true;
                tabForums.Visible = true;
                forumList.ReloadData();
                break;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (!displayControlPerformed)
        {
            plcForumList.Visible = listVisible;
            if (listVisible)
            {
                forumList.ReloadData();
            }
        }
        
        base.OnPreRender(e);
    }

}
