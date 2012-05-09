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
using CMS.UIControls;

public partial class CMSModules_Forums_Controls_LiveControls_Groups : CMSAdminItemsControl
{
    #region "Variables"

    private int mGroupId = 0;
    private Guid mCommunityGroupGUID = Guid.Empty;
    private bool mHideWhenGroupIsNotSupplied = false;
    private bool displayControlPerformed = false;
    private bool listVisible = false;

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
    /// Gets or sets the Community group GUID.
    /// </summary>
    public Guid CommunityGroupGUID
    {
        get
        {
            if (mCommunityGroupGUID == Guid.Empty)
            {
                mCommunityGroupGUID = ValidationHelper.GetGuid(this.GetValue("CommunityGroupGUID"), Guid.Empty);
            }

            return mCommunityGroupGUID;
        }
        set
        {
            mCommunityGroupGUID = value;
        }
    }


    /// <summary>
    /// Gets or sets the Community group ID.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (mGroupId <= 0)
            {
                this.mGroupId = ValidationHelper.GetInteger(this.GetValue("GroupID") , 0);
            }

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
        listVisible = plcList.Visible;
        plcList.Visible = true;
        groupList.Visible = true;


        #region "Security"

        groupList.OnCheckPermissions += new CheckPermissionsEventHandler(groupList_OnCheckPermissions);
        groupEdit.OnCheckPermissions += new CheckPermissionsEventHandler(groupEdit_OnCheckPermissions);
        groupNew.OnCheckPermissions += new CheckPermissionsEventHandler(groupNew_OnCheckPermissions);

        #endregion
        
        if (!this.Visible || StopProcessing)
        {
            //this.groupList.Visible = false;
            //this.groupEdit.Visible = false;
            //this.groupNew.Visible = false;
        }
        else
        {
            groupNew.IsLiveSite = true;
            groupList.IsLiveSite = true;
            groupEdit.IsLiveSite = true;

            this.groupList.OnAction += new CommandEventHandler(subscriptionList_OnAction);

            if (this.GroupID > 0)
            {
                this.groupList.CommunityGroupId = this.GroupID;
            }
            else
            {
                // Hide controls if the control should be hidden
                if (this.HideWhenGroupIsNotSupplied)
                {
                    this.Visible = false;
                    return;
                }
            }

            this.groupNew.CommunityGroupID = this.GroupID;

            int groupId = ValidationHelper.GetInteger(ViewState["GroupID"], 0);
            this.groupEdit.GroupID = groupId;

            // New item link
            string[,] actions = new string[1, 7];
            actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[0, 1] = GetString("Group_List.NewItemCaption");
            actions[0, 5] = GetImageUrl("Objects/Forums_ForumGroup/add.png");
            actions[0, 6] = "newgroup";
            this.actionsElem.Actions = actions;
            this.actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);

            this.groupNew.OnSaved += new EventHandler(groupNew_OnSaved);

            // Set display mode
            this.groupEdit.DisplayMode = this.DisplayMode;
            this.groupEdit.CommunityGroupGUID = this.CommunityGroupGUID;

            this.groupNew.DisplayMode = this.DisplayMode;
            this.groupNew.CommunityGroupGUID = this.CommunityGroupGUID;

            InitializeBreadcrumbs();            
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            DisplayControl("list");
        }
        else if (!displayControlPerformed)
        {
            plcList.Visible = listVisible;
            groupList.Visible = listVisible;
            if (listVisible)
            {
                groupList.ReloadData();
            }
        }
    }

    #region "Security handlers"

    void groupNew_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void groupEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void groupList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    protected void groupNew_OnSaved(object sender, EventArgs e)
    {
        int groupId = this.groupNew.GroupID;
        ViewState["GroupID"] = groupId;

        ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(groupId);
        if (fgi != null)
        {
            this.lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> " + HTMLHelper.HTMLEncode(fgi.GroupDisplayName);
        }

        this.groupEdit.GroupID = groupId;
        DisplayControl("edit");
    }

    
    /// <summary>
    /// New subscription link handler.
    /// </summary>
    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "newgroup":
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

                DisplayControl("edit");

                int groupId = ValidationHelper.GetInteger(e.CommandArgument, 0);
                ViewState["GroupID"] = groupId;

                this.groupEdit.GroupID = groupId;
                this.groupEdit.ReloadData();

                ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(groupId);
                if (fgi != null)
                {
                    this.lblEditBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> " + HTMLHelper.HTMLEncode(fgi.GroupDisplayName);
                }

                break;

            default:
                DisplayControl("list");
                break;
        }
    }


    /// <summary>
    /// Reloads the form data.
    /// </summary>
    public override void ReloadData()
    {
        DisplayControl("list");
    }


    /// <summary>
    /// Initializes the breadcrumbs.
    /// </summary>
    private void InitializeBreadcrumbs()
    {
        this.lblNewBack.Text = " <span class=\"TitleBreadCrumbSeparator\">&nbsp;</span> " + GetString("Group_General.NewGroup");

        this.lnkEditBack.Text = GetString("Group_General.GroupList");
        this.lnkNewBack.Text = GetString("Group_General.GroupList");

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


    /// <summary>
    /// Displays only specified control. Other controls hides.
    /// </summary>
    /// <param name="selectedControl">Selected control</param>
    private void DisplayControl(string selectedControl)
    {
        displayControlPerformed = true;

        // Disable all controls
        plcEdit.Visible = false;
        plcList.Visible = false;
        plcNew.Visible = false;

        switch (selectedControl.ToLower())
        {
            // Show group edit control
            case "edit":
                plcEdit.Visible = true;
                groupEdit.Visible = true;
                groupEdit.ReloadData();
                break;

            // Show group new control
            case "new" :
                plcNew.Visible = true;
                groupNew.Visible = true;
                groupNew.ReloadData();
                break;

            // Show group list control
            default:
                plcList.Visible = true;
                groupList.Visible = true;
                groupList.ReloadData();
                break;
        }
    }

    


}
