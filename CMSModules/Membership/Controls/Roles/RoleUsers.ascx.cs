using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Membership_Controls_Roles_RoleUsers : CMSAdminEditControl
{
    #region "Variables"

    private string currentValues = String.Empty;
    private int mRoleId = 0;
    bool reloadData = false;

    #endregion


    #region "Properties" 

    /// <summary>
    /// Gets or sets the role ID for which the users should be displayed.
    /// </summary>
    public int RoleID
    {
        get
        {
            return mRoleId;
        }
        set
        {
            mRoleId = value;
        }
    }


    /// <summary>
    /// Gets or sets the group user filter.
    /// </summary>
    public int GroupID
    {
        get
        {
            return usUsers.GroupID;
        }
        set
        {
            usUsers.GroupID = value;
        }
    }

    #endregion


    #region "Methods" 

    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvialable.Text = GetString("roleusers.available");

        // Hide site selector
        usUsers.ShowSiteFilter = false;

        // Set selector live site and hide special users
        usUsers.IsLiveSite = IsLiveSite;
        if (IsLiveSite)
        {
            usUsers.HideDisabledUsers = true;
            usUsers.HideHiddenUsers = true;
        }

        // Show only user belonging to role's site
        RoleInfo ri = RoleInfoProvider.GetRoleInfo(RoleID);
        if (ri != null)
        {
            usUsers.SiteID = ri.SiteID == 0 ? -1 : ri.SiteID;
        }

        // Load data in administration
        if (!IsLiveSite)
        {
            currentValues = GetRoleUsers();

            if (!RequestHelper.IsPostBack())
            {
                usUsers.Value = currentValues;
            }
        }

        usUsers.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveUsers();
    }


    private string GetRoleUsers()
    {
        DataSet ds = UserRoleInfoProvider.GetUserRoles("RoleID = " + RoleID, null, 0, "UserID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "UserID"));
        }

        return String.Empty;
    }


    private void SaveUsers()
    {
        if (!CheckPermissions("cms.roles", PERMISSION_MODIFY))
        {
            return;
        }

        bool falseValues = false;        
        bool saved = false;
        
        // Remove old items
        string newValues = ValidationHelper.GetString(usUsers.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int userId = ValidationHelper.GetInteger(item, 0);

                    // Check permissions
                    string result = ValidateGlobalAndDeskAdmin(userId);
                    if (result != String.Empty)
                    {
                        lblError.Visible = true;
                        lblError.Text += result;
                        falseValues = true;
                        continue;
                    }
                    else
                    {
                        UserRoleInfoProvider.RemoveUserFromRole(userId, RoleID);
                        saved = true;
                    }
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int userId = ValidationHelper.GetInteger(item, 0);

                    // Check permissions
                    string result = ValidateGlobalAndDeskAdmin(userId);
                    if (result != String.Empty)
                    {
                        lblError.Visible = true;
                        lblError.Text += result;
                        falseValues = true;
                        continue;
                    }
                    else
                    {
                        UserRoleInfoProvider.AddUserToRole(userId, RoleID);
                        saved = true;
                    }
                }
            }
        }

        if (falseValues)
        {
            currentValues = GetRoleUsers();
            usUsers.Value = currentValues;
            usUsers.Reload();
        }

        if (saved)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }              
    }
   

    /// <summary>
    /// Check whether current user is allowed to modify another user. Return "" or error message.
    /// </summary>
    /// <param name="userId">Modified user</param>
    protected string ValidateGlobalAndDeskAdmin(int userId)
    {
        string result = String.Empty;

        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            return result;
        }

        UserInfo userInfo = UserInfoProvider.GetUserInfo(userId);
        if (userInfo == null)
        {
            result = GetString("Administration-User.WrongUserId");
        }
        else
        {
            if (userInfo.IsGlobalAdministrator)
            {
                result = String.Format(GetString("Administration-User.NotAllowedToModifySpecific"), userInfo.FullName + " (" + userInfo.UserName + ")");
            }
        }
        return result;
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        reloadData = true;
        currentValues = GetRoleUsers();
    }


    /// <summary>
    /// Page PreRender.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        if (reloadData)
        {
            currentValues = GetRoleUsers();
            usUsers.Value = currentValues;
            usUsers.ReloadData();
        }

        if (RequestHelper.IsPostBack())
        {
            pnlBasic.Update();
        }

        base.OnPreRender(e);
    }

    #endregion
}
