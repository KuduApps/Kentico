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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_Membership_Controls_Roles_Role : CMSAdminEditControl
{
    private int mGroupID = 0;
    private Guid mGroupGUID = Guid.Empty;

    #region "Public properties"

    public int SelectedTab
    {
        get
        {
            return ValidationHelper.GetInteger(this.ViewState["selectedtab"], 0);
        }
        set
        {
            this.ViewState["selectedtab"] = (object)value;
        }
    }


    public int SiteID
    {
        get
        {
            return ValidationHelper.GetInteger(this.ViewState["siteid"], -1);
        }
        set
        {
            this.ViewState["siteid"] = (object)value;
        }
    }


    /// <summary>
    /// Gets or sets the community group id.
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
        }
    }


    /// <summary>
    /// Gets or sets the community group GUID.
    /// </summary>
    public Guid GroupGUID
    {
        get
        {
            return mGroupGUID;
        }
        set
        {
            mGroupGUID = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        #region "security"

        RoleEdit.OnCheckPermissions += new CheckPermissionsEventHandler(RoleEdit_OnCheckPermissions);
        RoleUsers.OnCheckPermissions += new CheckPermissionsEventHandler(RoleUsers_OnCheckPermissions);
        
        #endregion


        this.ltlScript.Text = ScriptHelper.GetScript("function UpdateForm(){ " + this.Page.ClientScript.GetPostBackEventReference(btnUpdate, "") + "; } \n");

        // Menu initialization
        tabMenu.UrlTarget = "_self";
        tabMenu.Tabs = new string[2, 5];
        tabMenu.Tabs[0, 0] = GetString("general.general");
        tabMenu.Tabs[1, 0] = GetString("general.users");
        tabMenu.UsePostback = true;
        tabMenu.UseClientScript = true;
        tabMenu.OnTabClicked += new EventHandler(tabMenu_OnTabChanged);
        tabMenu.TabControlIdPrefix = this.ClientID;

        btnUpdate.Attributes.Add("style", "display:none;");


        this.ReloadData(false);
    }


    #region "Security handlers"

    void RoleUsers_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    void RoleEdit_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        RaiseOnCheckPermissions(permissionType, sender);
    }

    #endregion


    /// <summary>
    /// Reloads and displays appropriate controls.
    /// </summary>
    public override void ReloadData(bool forceReload)
    {
        RoleEdit.ItemID = RoleUsers.RoleID = this.ItemID;
        RoleEdit.SiteID = this.SiteID;
        RoleEdit.GroupID = this.GroupID;
        RoleEdit.GroupGUID = this.GroupGUID;
        RoleEdit.DisplayMode = this.DisplayMode;
        RoleEdit.ReloadData(forceReload);

        RoleEdit.Visible = false;
        RoleUsers.Visible = false;
        RoleUsers.GroupID = this.GroupID;

        tabMenu.SelectedTab = this.SelectedTab;
        
        switch (this.SelectedTab)
        {
            case 0:
            default:
                RoleEdit.Visible = true;
                break;

            case 1:
                RoleUsers.Visible = true;
                RoleUsers.ReloadData();
                break;
        }
    }


    /// <summary>
    /// Tab change event handler.
    /// </summary>
    void tabMenu_OnTabChanged(object sender, EventArgs e)
    {
        this.SelectedTab = tabMenu.SelectedTab;
        this.ReloadData(false);
    }


    /// <summary>
    /// This function is executed by callback iniciated by 'Select roles' dialog after its closing.
    /// </summary>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ReloadData(false);
    }
}
