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

public partial class CMSModules_MessageBoards_Controls_LiveControls_Subscriptions : CMSAdminItemsControl
{
    #region "Selected control enumeration"

    private enum SelectedControlEnum { Listing, Edit };

    #endregion


    #region "Private fields"

    private int mGroupID = 0;
    private const string breadCrumbsSeparator = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> ";

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns currently selected control.
    /// </summary>
    private SelectedControlEnum SelectedControl
    {
        get
        {
            if (this.plcEditSubscriptions.Visible)
            {
                return SelectedControlEnum.Edit;
            }
            else
            {
                return SelectedControlEnum.Listing;
            }
        }
        set
        {
            switch (value)
            {
                case SelectedControlEnum.Listing:
                    this.boardSubscriptionList.StopProcessing = false;
                    this.boardSubscriptionEdit.StopProcessing = true;
                    this.plcEditSubscriptions.Visible = false;
                    this.plcSubscriptionList.Visible = true;
                    break;

                case SelectedControlEnum.Edit:
                    this.boardSubscriptionList.StopProcessing = true;
                    this.boardSubscriptionEdit.StopProcessing = false;
                    this.plcEditSubscriptions.Visible = true;
                    this.plcSubscriptionList.Visible = false;
                    break;
            }
        }
    }


    /// <summary>
    /// Current subscription ID for internal use.
    /// </summary>
    private int SubscriptionID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["SubscriptionID"], 0);
        }
    }

    #endregion


    #region "Public properties"


    /// <summary>
    /// Current board ID.
    /// </summary>
    public int BoardID
    {
        get
        {
            return this.boardSubscriptionEdit.BoardID;
        }
        set
        {
            this.boardSubscriptionEdit.BoardID = value;
            this.boardSubscriptionList.BoardID = value;
        }
    }


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

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // If control should be hidden save view state memory
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        // Initializes the controls
        SetupControls();

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
        this.boardSubscriptionEdit.OnCheckPermissions += new CheckPermissionsEventHandler(boardSubscriptionEdit_OnCheckPermissions);
        this.boardSubscriptionList.OnCheckPermissions += new CheckPermissionsEventHandler(boardSubscriptionList_OnCheckPermissions);
        this.boardSubscriptionList.IsLiveSite = this.IsLiveSite;
        this.boardSubscriptionEdit.IsLiveSite = this.IsLiveSite;
        this.boardSubscriptionList.OnAction += new CommandEventHandler(boardSubscriptionList_OnAction);
        this.boardSubscriptionEdit.GroupID = this.GroupID;
        this.boardSubscriptionEdit.SubscriptionID = this.SubscriptionID;
        this.boardSubscriptionEdit.OnSaved += new EventHandler(boardSubscriptionEdit_OnSaved);

        this.lnkEditSubscriptionBack.Text = GetString("Group_General.Boards.Boards.BackToSubscriptions");
        this.lnkEditSubscriptionBack.Click += new EventHandler(lnkEditSubscriptionBack_Click);

        // New subscription link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("Group_General.Boards.Boards.NewSubscription");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_MessageBoards/newsubscription.png");
        actions[0, 6] = "newsubscription";

        this.newSubscription.Actions = actions;
        this.newSubscription.ActionPerformed += new CommandEventHandler(newSubscription_ActionPerformed);

        InitBreadCrumbs();
    }


    /// <summary>
    /// Displays specified control.
    /// </summary>
    /// <param name="selectedControl">Control to be displayed</param>
    /// <param name="reload">If True, ReloadData on child control is called</param>
    private void DisplayControl(SelectedControlEnum selectedControl, bool reload)
    {
        // Set correct tab
        this.SelectedControl = selectedControl;

        // Enable currently selected element
        switch (selectedControl)
        {
            case SelectedControlEnum.Listing:
                if (reload)
                {
                    this.boardSubscriptionList.BoardID = this.BoardID;
                    this.boardSubscriptionList.ReloadData();
                }                                

                break;

            case SelectedControlEnum.Edit:
                if (reload)
                {
                    this.boardSubscriptionEdit.GroupID = this.GroupID;
                    this.boardSubscriptionEdit.SubscriptionID = this.SubscriptionID;
                    this.boardSubscriptionEdit.ReloadData();
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

        this.newSubscription.ReloadData();
    }


    private void InitBreadCrumbs()
    {
        // Initialize subscription breadcrumbs
        if (this.SubscriptionID > 0)
        {
            BoardSubscriptionInfo subscription = BoardSubscriptionInfoProvider.GetBoardSubscriptionInfo(this.SubscriptionID);
            if (subscription != null)
            {
                this.lblEditSubscriptionBack.Text = breadCrumbsSeparator + HTMLHelper.HTMLEncode(subscription.SubscriptionEmail);
            }
        }
        else
        {
            this.lblEditSubscriptionBack.Text = breadCrumbsSeparator + GetString("Group_General.Boards.Boards.NewSubscription");
        }
    }


    #region "Event handlers"

    protected void newSubscription_ActionPerformed(object sender, CommandEventArgs e)
    {
        ViewState.Add("SubscriptionID", 0);
        DisplayControl(SelectedControlEnum.Edit, true);
        InitBreadCrumbs();
    }


    protected void boardSubscriptionList_OnAction(object sender, CommandEventArgs e)
    {
        if ((e.CommandName.ToLower() == "edit") && this.boardSubscriptionList.Visible)
        {
            ViewState.Add("SubscriptionID", e.CommandArgument);
            DisplayControl(SelectedControlEnum.Edit, true);
            InitBreadCrumbs();
        }
    }


    protected void boardSubscriptionEdit_OnSaved(object sender, EventArgs e)
    {
        ViewState.Add("SubscriptionID", this.boardSubscriptionEdit.SubscriptionID);
        DisplayControl(SelectedControlEnum.Edit, true);
        InitBreadCrumbs();
    }


    protected void lnkEditSubscriptionBack_Click(object sender, EventArgs e)
    {
        DisplayControl(SelectedControlEnum.Listing, true);
    }

    #endregion


    #region "Security event handlers"

    protected void boardSubscriptionList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        this.RaiseOnCheckPermissions(permissionType, sender);
    }


    protected void boardSubscriptionEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        this.RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion
}
