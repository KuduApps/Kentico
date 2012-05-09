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
using CMS.URLRewritingEngine;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Membership_Controls_Roles_Roles : CMSAdminEditControl
{
    #region "Variables"

    private bool mHideWhenGroupIsNotSupplied = false;

    #endregion


    #region "Properties"

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
    /// Gets and sets current group ID.
    /// </summary>
    public int GroupID
    {
        get
        {
            int groupId = ValidationHelper.GetInteger(this.ViewState["groupid"], 0);

            if (groupId <= 0)
            {
                groupId = ValidationHelper.GetInteger(this.GetValue("GroupID"), 0);
            }

            return groupId;
        }
        set
        {
            this.ViewState["groupid"] = value;
        }
    }


    /// <summary>
    /// Gets and sets current group GUID.
    /// </summary>
    public Guid GroupGUID
    {
        get
        {
            Guid groupGuid = ValidationHelper.GetGuid(this.ViewState["groupguid"], Guid.Empty);

            if (groupGuid == Guid.Empty)
            {
                groupGuid = ValidationHelper.GetGuid(this.GetValue("GroupGUID"), Guid.Empty);
            }

            return groupGuid;
        }
        set
        {
            this.ViewState["groupguid"] = value;
        }
    }
    

    /// <summary>
    /// Gets and sets current site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            int siteId = ValidationHelper.GetInteger(this.ViewState["siteid"], 0);

            if (siteId <= 0)
            {
                siteId = ValidationHelper.GetInteger(this.GetValue("SiteID"), 0);
            }

            return siteId;
        }
        set
        {
            this.ViewState["siteid"] = value;
        }
    }


    /// <summary>
    /// Gets and sets current role ID.
    /// </summary>
    public int RoleID
    {
        get
        {
            return ValidationHelper.GetInteger(this.ViewState["roleid"], 0);
        }
        set
        {
            this.ViewState["roleid"] = value;
        }
    }


    /// <summary>
    /// Gets and sets control to be displayed.
    /// </summary>
    public string SelectedControl
    {
        get
        {
            return ValidationHelper.GetString(this.ViewState["selectedcontrol"], "general");
        }
        set
        {
            this.ViewState["selectedcontrol"] = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        #region "Security"

        RoleList.OnCheckPermissions += new CheckPermissionsEventHandler(RoleList_OnCheckPermissions);
        RoleEdit.OnCheckPermissions += new CheckPermissionsEventHandler(RoleEdit_OnCheckPermissions);
        Role.OnCheckPermissions += new CheckPermissionsEventHandler(Role_OnCheckPermissions);

        #endregion
        
        if (!this.Visible)
        {
            this.EnableViewState = false;
        }

        if (this.StopProcessing)
        {
            this.Role.StopProcessing = true;
            this.RoleList.StopProcessing = true;
            this.RoleEdit.StopProcessing = true;
        }
        else
        {
            if ((this.GroupID == 0) && this.HideWhenGroupIsNotSupplied)
            {
                this.Visible = false;
                return;
            }

            // Is live site
            Role.IsLiveSite = this.IsLiveSite;

            RoleList.SiteID = this.SiteID;
            RoleList.GroupID = this.GroupID;

            RoleEdit.GroupID = this.GroupID;
            RoleEdit.GroupGUID = this.GroupGUID;
            RoleEdit.SiteID = this.SiteID;
            RoleEdit.DisplayMode = this.DisplayMode;

            Role.GroupID = this.GroupID;
            Role.GroupGUID = this.GroupGUID;
            Role.SiteID = this.SiteID;
            Role.DisplayMode = this.DisplayMode;

            // Setup new role button            
            btnNewRole.Click += new EventHandler(btnNewRole_Click);
            imgNewRole.ImageUrl = GetImageUrl("Objects/CMS_Role/add.png");
            imgNewRole.AlternateText = GetString("Administration-Role_New.NewRole");

            // BreadCrumbs setup            
            btnBreadCrumbs.Click += new EventHandler(btnBreadCrumbs_Click);
            lblRole.ResourceString = "Administration-Role_New.NewRole";

            this.RoleList.OnEdit += new EventHandler(RoleList_OnEdit);
            this.RoleEdit.OnSaved += new EventHandler(RoleEdit_OnSaved);

            this.DisplayControls(this.SelectedControl);
        }
    }

    
    #region "Security handlers"

    void Role_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void RoleEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void RoleList_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    /// <summary>
    /// Displays appropriate controls.
    /// </summary>
    public void DisplayControls(string control)
    {
        // Hide all controls first and set all IDs
        Role.Visible = false;        
        RoleList.Visible = false;
        RoleEdit.Visible = false;
        headerLinks.Visible = false;
        pnlRolesBreadcrumbs.Visible = false;       

        // Display edit control
        if (this.RoleID > 0)
        {
            Role.Visible = true;
            pnlRolesBreadcrumbs.Visible = true;
            RoleInfo role = RoleInfoProvider.GetRoleInfo(this.RoleID);
            if (role != null)
            {
                lblRole.ResourceString = HTMLHelper.HTMLEncode(role.DisplayName);
            }

            Role.SiteID = this.SiteID;
            Role.ItemID = this.RoleID;            
            Role.ReloadData(false);
        }
        else
        {
            switch (control)
            {
                // Display list control
                case "general":
                default:                    
                    RoleList.Visible = true;
                    headerLinks.Visible = true;
                    RoleList.SiteID = this.SiteID;
                    RoleEdit.SiteID = this.SiteID;
                    RoleEdit.ReloadData(false);
                    break;
                // Display new control
                case "newrole":
                    RoleEdit.Visible = true;
                    pnlRolesBreadcrumbs.Visible = true;
                    lblRole.ResourceString = "Administration-Role_New.Title";
                    break;
            }
        }
    }


    /// <summary>
    /// Edit action delegate handler.
    /// </summary>
    void RoleList_OnEdit(object sender, EventArgs e)
    {
        this.RoleID = Role.ItemID = this.RoleList.SelectedItemID;
        Role.ReloadData(true);
        this.DisplayControls("");
    }


    /// <summary>
    /// New role click handler.
    /// </summary>
    void btnNewRole_Click(object sender, EventArgs e)
    {
        this.RoleID = this.RoleEdit.ItemID = this.Role.ItemID = 0;
        this.RoleEdit.ReloadData(true);
        this.SelectedControl = "newrole";
        this.DisplayControls(this.SelectedControl);
    }


    /// <summary>
    /// Breadcrumbs click handler.
    /// </summary>
    void btnBreadCrumbs_Click(object sender, EventArgs e)
    {
        this.RoleID = 0;
        this.SelectedControl = "general";
        this.DisplayControls(this.SelectedControl);
        Role.SelectedTab = 0;
    }


    /// <summary>
    /// OnSave event handler.
    /// </summary>
    void RoleEdit_OnSaved(object sender, EventArgs e)
    {
        this.Role.ItemID = this.RoleID = RoleEdit.ItemID;
        this.Role.ReloadData(true);
        this.SelectedControl = "general";
        this.DisplayControls(this.SelectedControl);
    }
}