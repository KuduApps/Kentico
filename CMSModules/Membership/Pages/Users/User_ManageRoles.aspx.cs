using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;


public partial class CMSModules_Membership_Pages_Users_User_ManageRoles : CMSModalPage
{
    #region "Variables"

    private int userId = 0;
    private int siteId = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check administration
        CheckAdministration();

        // Check permissions
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (user != null)
        {
            // Check UI elements
            if (!user.IsAuthorizedPerUIElement("CMS.Administration", "Users"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Administration", "Users");
            }

            if (!user.IsAuthorizedPerUIElement("CMS.Administration", "Roles"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Administration", "Roles");
            }

            // Check "read" permissions
            if (!user.IsAuthorizedPerResource("CMS.Users", "Read"))
            {
                RedirectToAccessDenied("CMS.Users", "Read");
            }

            if (!user.IsAuthorizedPerResource("CMS.Roles", "Read"))
            {
                RedirectToAccessDenied("CMS.Roles", "Read");
            }
        }

        userId = QueryHelper.GetInteger("userId", 0);

        // Check that only global administrator can edit global administrator's accouns
        UserInfo ui = UserInfoProvider.GetUserInfo(userId);
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        CheckUserAvaibleOnSite(ui);
        if ((!currentUser.IsGlobalAdministrator || (currentUser.IsGlobalAdministrator && currentUser.UserSiteManagerDisabled)) && ui != null && ui.UserIsGlobalAdministrator && (ui.UserID != currentUser.UserID))
        {
            plcTable.Visible = false;
            lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
            lblError.Visible = true;
            return;
        }

        // Only global admin can choose the site
        if ((CMSContext.CurrentUser != null && CMSContext.CurrentUser.IsGlobalAdministrator))
        {
            // Show site selector
            CurrentMaster.DisplaySiteSelectorPanel = true;

            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.AllowAll = false;

            if (currentUser.UserSiteManagerAdmin)
            {
                siteSelector.AllowGlobal = true;
            }

            siteSelector.UserId = userId;
            siteSelector.OnlyRunningSites = false;
            siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

            if (!RequestHelper.IsPostBack())
            {
                if (ui.IsInSite (CMSContext.CurrentSiteName))
                {
                    siteSelector.Value = CMSContext.CurrentSiteID;
                }
                else 
                {
                    siteSelector.Value = siteSelector.GlobalRecordValue;                    
                }
            }

            siteId = siteSelector.SiteID;
        }
        else
        {
            siteSelector.StopProcessing = true;
            siteId = CMSContext.CurrentSiteID;
        }

        // Init UI
        itemSelectionElem.LeftColumnLabel.Text = GetString("user.manageroles.availableroles");
        itemSelectionElem.RightColumnLabel.Text = GetString("user.manageroles.userinroles");
        btnClose.Text = GetString("general.close");
        CurrentMaster.Title.TitleText = GetString("user.manageroles.header");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Role/object.png");


        // Register the events
        itemSelectionElem.OnMoveLeft += itemSelectionElem_OnMoveLeft;
        itemSelectionElem.OnMoveRight += itemSelectionElem_OnMoveRight;
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Load the roles into ItemSelection Control
        if (!RequestHelper.IsPostBack())
        {
            LoadRoles();
            itemSelectionElem.fill();
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Test if edited user belongs to current site
    /// </summary>
    /// <param name="ui">User info object</param>
    public void CheckUserAvaibleOnSite(UserInfo ui)
    {
        if (ui != null)
        {
            if (!CMSContext.CurrentUser.IsGlobalAdministrator && !ui.IsInSite(CMSContext.CurrentSiteName))
            {
                RedirectToInformation(GetString("user.notinsite"));
            }
        }
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        LoadRoles();
        itemSelectionElem.fill();
        pnlUpdate.Update();
    }


    #region "ItemSelection events"

    protected void itemSelectionElem_OnMoveRight(object sender, CommandEventArgs e)
    {
        // Permissions check
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "ManageUserRoles"))
        {
            RedirectToAccessDenied("CMS.Users", "Manage user roles");
        }

        string argument = ValidationHelper.GetString(e.CommandArgument, "");
        if (!string.IsNullOrEmpty(argument))
        {
            string[] ids = argument.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string id in ids)
            {
                UserInfoProvider.AddUserToRole(userId, ValidationHelper.GetInteger(id, 0));
            }
        }

    }


    protected void itemSelectionElem_OnMoveLeft(object sender, CommandEventArgs e)
    {
        // Permissions check
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "ManageUserRoles"))
        {
            RedirectToAccessDenied("CMS.Users", "Manage user roles");
        }

        string argument = ValidationHelper.GetString(e.CommandArgument, "");
        if (!string.IsNullOrEmpty(argument))
        {
            string[] ids = argument.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string id in ids)
            {
                UserInfoProvider.RemoveUserFromRole(userId, ValidationHelper.GetInteger(id, 0));
            }
        }
    }

    #endregion


    #region "Private helper functions"

    private void LoadRoles()
    {
        itemSelectionElem.LeftItems = GetSelectionControlItems(GetRoleDataset(false));
        itemSelectionElem.RightItems = GetSelectionControlItems(GetRoleDataset(true));
    }


    /// <summary>
    /// Returns the dataset with roles for specified site.
    /// </summary>
    /// <param name="users">Determines whether to return only roles to which the user is assigned</param>
    private DataSet GetRoleDataset(bool users)
    {
        string siteWhere = (siteId.ToString() == siteSelector.GlobalRecordValue) ? "SiteID IS NULL" : "SiteID = " + siteId;
        string where = "(RoleID " + (users ? "" : "NOT ") + "IN (SELECT RoleID FROM CMS_UserRole WHERE UserID = " + userId + ")) AND " + siteWhere + " AND RoleGroupID IS NULL";

        // Exclude generic roles
        string genericWhere = null;
        ArrayList genericRoles = RoleInfoProvider.GetGenericRoles();
        if (genericRoles.Count != 0)
        {
            foreach (string role in genericRoles)
            {
                genericWhere += "'" + SqlHelperClass.GetSafeQueryString(role, false) + "',";
            }

            if (genericWhere != null)
            {
                genericWhere = genericWhere.TrimEnd(',');
            }
            where += " AND ( RoleName NOT IN (" + genericWhere + ") )";
        }

        return RoleInfoProvider.GetRoles("RoleDisplayName, RoleID", where, "RoleDisplayName", 0);
    }


    /// <summary>
    /// Returns the 2 dimensional array which can be used in ItemSelection control.
    /// </summary>
    private static string[,] GetSelectionControlItems(DataSet ds)
    {
        string[,] retval = null;
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            retval = new string[ds.Tables[0].Rows.Count, 2];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                retval[i, 1] = Convert.ToString(dr["RoleDisplayName"]);
                retval[i, 0] = Convert.ToString(dr["RoleID"]);
            }
        }
        return retval;
    }

    #endregion
}
