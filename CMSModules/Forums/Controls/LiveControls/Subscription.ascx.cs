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

public partial class CMSModules_Forums_Controls_LiveControls_Subscription : CMSAdminItemsControl
{
    #region "Variables"

    private int mForumId = 0;
    private const string breadCrumbsSeparator = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";
    private bool displayControlPerformed = false;
    private bool listVisible = false;

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

        listVisible = plcList.Visible;
        plcList.Visible = true;
        subscriptionList.Visible = true;

        #region "Security"

        subscriptionList.OnCheckPermissions += new CheckPermissionsEventHandler(subscriptionList_OnCheckPermissions);
        subscriptionEdit.OnCheckPermissions += new CheckPermissionsEventHandler(subscriptionEdit_OnCheckPermissions);
        subscriptionNew.OnCheckPermissions += new CheckPermissionsEventHandler(subscriptionNew_OnCheckPermissions);

        #endregion

        this.subscriptionNew.ForumID = this.mForumId;
        this.subscriptionEdit.ForumID = this.mForumId;
        this.subscriptionList.ForumID = this.mForumId;

        this.subscriptionList.OnAction += new CommandEventHandler(subscriptionList_OnAction);

        int subscriptionId = ValidationHelper.GetInteger(ViewState["SubscriptionID"], 0);
        this.subscriptionEdit.SubscriptionID = subscriptionId;

        // New item link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("ForumSubscription_List.NewItemCaption");
        actions[0, 5] = GetImageUrl("Objects/Forums_ForumSubscription/add.png");
        actions[0, 6] = "newsubscription";
        this.actionsElem.Actions = actions;
        this.actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);

        this.subscriptionNew.OnSaved += new EventHandler(subscriptionNew_OnSaved);

        InitializeBreadcrumbs();

        // Default show listing
        if (!RequestHelper.IsPostBack())
        {
            DisplayControl("list");
        }

    }
        
    #region "Security handlers"

    void subscriptionList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void subscriptionNew_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void subscriptionEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion

    protected void subscriptionNew_OnSaved(object sender, EventArgs e)
    {
        int subscriptionId = this.subscriptionNew.SubscriptionID;
        ViewState["SubscriptionID"] = subscriptionId;

        ForumSubscriptionInfo fsi = ForumSubscriptionInfoProvider.GetForumSubscriptionInfo(subscriptionId);
        if (fsi != null)
        {
            this.lblEditBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(fsi.SubscriptionEmail);
        }

        this.subscriptionEdit.SubscriptionID = subscriptionId;

        DisplayControl("edit");

    }


    /// <summary>
    /// New subscription link handler.
    /// </summary>
    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "newsubscription":
                DisplayControl("new");
                break;
        }
    }


    /// <summary>
    /// Edit subscription handler.
    /// </summary>
    protected void subscriptionList_OnAction(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "edit":
                 
                int subscriptionId = ValidationHelper.GetInteger(e.CommandArgument, 0);
                this.subscriptionEdit.SubscriptionID = subscriptionId;
                ViewState["SubscriptionID"] = subscriptionId;

                ForumSubscriptionInfo fsi = ForumSubscriptionInfoProvider.GetForumSubscriptionInfo(subscriptionId);
                if (fsi != null)
                {
                    this.lblEditBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(fsi.SubscriptionEmail);
                }

                DisplayControl("edit");

                break;
        }
    }


    /// <summary>
    /// Reloads the form data.
    /// </summary>
    public override void ReloadData()
    {
        this.subscriptionNew.ForumID = this.mForumId;
        this.subscriptionEdit.ForumID = this.mForumId;
        this.subscriptionList.ForumID = this.mForumId;
        DisplayControl("list");
    }
    

    /// <summary>
    /// Initializes the breadcrumbs.
    /// </summary>
    private void InitializeBreadcrumbs()
    {
        this.lblNewBack.Text = breadCrumbsSeparator + GetString("forum_list.subscription.newsubscription");

        this.lnkEditBack.Text = GetString("forum_list.subscription.headeractions");
        this.lnkNewBack.Text = GetString("forum_list.subscription.headeractions");

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
        // Hide all controls
        plcList.Visible = false;
        plcEdit.Visible = false;
        plcNew.Visible = false;

        displayControlPerformed = true;

        switch (selectedControl.ToLower())
        {
            // Show edit control
            case "edit":
                plcEdit.Visible = true;
                subscriptionEdit.Visible = true;
                subscriptionEdit.ReloadData();
                break;

            // Show new control
            case "new":
                plcNew.Visible = true;
                subscriptionNew.Visible = true;
                subscriptionNew.ReloadData();
                break;

            // Show list control
            default:
                plcList.Visible = true;
                subscriptionList.Visible = true;
                subscriptionList.ReloadData();
                break;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        if (!displayControlPerformed)
        {
            plcList.Visible = listVisible;
            subscriptionList.Visible = listVisible;
            if (listVisible)
            {
                subscriptionList.ReloadData();
            }
        }

        base.OnPreRender(e);
    }

}
