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
using CMS.UIControls;

public partial class CMSModules_Forums_Controls_LiveControls_Forum : CMSAdminItemsControl
{
    #region "Variables"

    private int mForumId = 0;
    private bool displayControlPerformed = false;
    private bool tabVisible = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the Forum ID.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Visible || StopProcessing)
        {
            this.EnableViewState = false;
        }

        // Show controls
        if (!displayControlPerformed)
        {
            tabVisible = tabSubscriptions.Visible;
            tabSubscriptions.Visible = true;
            subscriptionElem.Visible = true;
        }

        #region "Security"

        // Add permission handlers
        postEdit.OnCheckPermissions += new CheckPermissionsEventHandler(postEdit_OnCheckPermissions);
        forumEditElem.OnCheckPermissions += new CheckPermissionsEventHandler(forumEditElem_OnCheckPermissions);
        subscriptionElem.OnCheckPermissions += new CheckPermissionsEventHandler(subscriptionElem_OnCheckPermissions);
        moderatorEdit.OnCheckPermissions += new CheckPermissionsEventHandler(moderatorEdit_OnCheckPermissions);
        securityElem.OnCheckPermissions += new CheckPermissionsEventHandler(securityElem_OnCheckPermissions);

        #endregion

        // Set properties
        this.tabElem.TabControlIdPrefix = "forum";
        this.forumEditElem.ForumID = this.mForumId;
        this.forumEditElem.DisplayMode = this.DisplayMode;

        this.moderatorEdit.ForumID = this.mForumId;

        this.securityElem.ForumID = this.mForumId;
        this.subscriptionElem.ForumID = this.mForumId;
        this.postEdit.ForumID = this.mForumId;
        this.securityElem.IsGroupForum = true;

        InitializeTabs();
    }

    #region "Security"

    // Security handlers
    void securityElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void moderatorEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void subscriptionElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void forumEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void postEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    /// <summary>
    /// Show correct tab.
    /// </summary>
    protected void forumTabElem_OnTabChanged(object sender, EventArgs e)
    {
        int tab = this.tabElem.SelectedTab;

        // Switch tab content
        if (tab == 0)
        {
            DisplayControl("posts");
        }
        else if (tab == 1)
        {
            DisplayControl("general");
        }
        else if (tab == 2)
        {
            DisplayControl("subscriptions");
        }
        else if (tab == 3)
        {
            DisplayControl("moderators");
        }
        else if (tab == 4)
        {
            DisplayControl("security");
        }

    }


    /// <summary>
    /// Reloads the form data.
    /// </summary>
    public override void ReloadData()
    {
        // Reload properties in control
        this.forumEditElem.ForumID = this.mForumId;
        this.moderatorEdit.ForumID = this.mForumId;

        this.securityElem.ForumID = this.mForumId;
        this.subscriptionElem.ForumID = this.mForumId;
        this.postEdit.ForumID = this.mForumId;

        DisplayControl("post");
    }


    /// <summary>
    /// Initializes the tabs.
    /// </summary>
    private void InitializeTabs()
    {
        // Initialize forum tabs
        string[,] tabs = new string[5, 4];
        tabs[0, 0] = GetString("Forum_Edit.Posts");
        tabs[1, 0] = GetString("general.general");
        tabs[2, 0] = GetString("Forum_Edit.Subscriptions");
        tabs[3, 0] = GetString("Forum_Edit.Moderating");
        tabs[4, 0] = GetString("general.Security");
        this.tabElem.Tabs = tabs;
        this.tabElem.OnTabClicked += new EventHandler(forumTabElem_OnTabChanged);
    }

    private void DisplayControl(string selectedControl)
    {
        displayControlPerformed = true;

        // Hide all tabs
        this.tabGeneral.Visible = false;
        this.tabModerators.Visible = false;
        this.tabPosts.Visible = false;
        this.tabSecurity.Visible = false;
        this.tabSubscriptions.Visible = false;
        this.tabElem.SelectedTab = 0;

        switch (selectedControl.ToLower())
        {
            // Show general tab
            case "general":
                forumEditElem.ReloadData();
                tabGeneral.Visible = true;
                tabElem.SelectedTab = 1;
                break;

            // Show moderators tab
            case "moderators":
                moderatorEdit.ReloadData(true);
                tabModerators.Visible = true;
                tabElem.SelectedTab = 3;
                break;

            // Show security tab
            case "security":
                securityElem.ReloadData();
                tabSecurity.Visible = true;
                tabElem.SelectedTab = 4;
                break;


            // Show subscriptions tab
            case "subscriptions":
                subscriptionElem.ReloadData();
                tabSubscriptions.Visible = true;
                tabElem.SelectedTab = 2;
                break;

            // Show posts tab
            default:
                postEdit.ReloadData();
                tabPosts.Visible = true;
                break;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if (!displayControlPerformed)
        {
            // Set visibility of forum tabs
            tabSubscriptions.Visible = tabVisible;
        }

        base.OnPreRender(e);
    }

}
