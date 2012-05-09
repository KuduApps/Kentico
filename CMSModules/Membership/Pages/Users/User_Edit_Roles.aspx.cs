using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;
using System.Threading;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Membership_Pages_Users_User_Edit_Roles : CMSUsersPage
{
    #region "Protected variables"

    protected int siteId = 0;
    protected int userId = 0;
    protected UserInfo ui = null;
    protected string currentValues = string.Empty;

    #endregion


    #region "Events"

    /// <summary>
    /// Page_load event.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions and UI elements
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (user != null)
        {
            if (!user.IsAuthorizedPerUIElement("CMS.Administration", "Roles"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Administration", "Roles");
            }

            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Roles", "Read"))
            {
                RedirectToAccessDenied("CMS.Roles", "Read");
            }
        }

        ScriptHelper.RegisterJQuery(Page);

        // Get user id and site Id from query
        userId = QueryHelper.GetInteger("userid", 0);

        // Show contentplaceholder where site selector can be shown
        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        if ((SiteID > 0) && !CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            this.plcSites.Visible = false;
            this.CurrentMaster.DisplaySiteSelectorPanel = false;
        }

        if (userId > 0)
        {
            // Check that only global administrator can edit global administrator's accouns
            ui = UserInfoProvider.GetUserInfo(userId);
            CheckUserAvaibleOnSite(ui); 
            EditedObject = ui;

            if (!CheckGlobalAdminEdit(ui))
            {
                plcTable.Visible = false;
                lblErrorDeskAdmin.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                lblErrorDeskAdmin.Visible = true;
                return;
            }


            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.AllowAll = false;
            siteSelector.AllowEmpty = false;

            // Global roles only for global admin
            if (CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                siteSelector.AllowGlobal = true;
            }

            // Only sites assigned to user
            siteSelector.UserId = userId;
            siteSelector.OnlyRunningSites = false;
            siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

            if (!RequestHelper.IsPostBack())
            {
                siteId = CMSContext.CurrentSiteID;

                // If user is member of current site
                if (UserSiteInfoProvider.GetUserSiteInfo(userId, siteId) != null)
                {
                    // Force uniselector to preselect current site
                    siteSelector.Value = siteId;
                }

                // Force to load data
                siteSelector.UpdateWhereCondition();
                siteSelector.Reload(true);
            }

            // Get truly selected item
            siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        }

        usRoles.OnSelectionChanged += new EventHandler(usRoles_OnSelectionChanged);
        string siteIDWhere = (siteId <= 0) ? " SiteID IS NULL " : " SiteID =" + siteId;
        usRoles.WhereCondition = siteIDWhere + " AND RoleGroupID IS NULL";

        usRoles.SelectItemPageUrl = "~/CMSModules/Membership/Pages/Users/User_Edit_Add_Item_Dialog.aspx";
        usRoles.ListingWhereCondition = siteIDWhere + " AND RoleGroupID IS NULL AND UserID=" + userId;
        usRoles.ReturnColumnName = "RoleID";
        usRoles.DynamicColumnName = false;
        usRoles.GridName = "User_Role_List.xml";
        usRoles.AdditionalColumns = "ValidTo";
        usRoles.OnAdditionalDataBound += new CMSAdminControls_UI_UniSelector_UniSelector.AdditionalDataBoundEventHandler(usMemberships_OnAdditionalDataBound);

        // Exclude generic roles
        string genericWhere = null;
        ArrayList genericRoles = RoleInfoProvider.GetGenericRoles();
        if (genericRoles.Count != 0)
        {
            foreach (string role in genericRoles)
            {
                genericWhere += "'" + role.Replace("'", "''") + "',";
            }

            genericWhere = genericWhere.TrimEnd(',');
            usRoles.WhereCondition += " AND ( RoleName NOT IN (" + genericWhere + ") )";
        }

        // Get the active roles for this site
        DataSet ds = UserRoleInfoProvider.GetUserRoles("UserID = " + userId + " AND RoleID IN (SELECT RoleID FROM CMS_Role WHERE SiteID IS NULL OR SiteID = " + siteId + ")", null, 0, "RoleID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "RoleID"));
        }

        if (!String.IsNullOrEmpty(currentValues))
        {
            usRoles.WhereCondition += " AND RoleID NOT IN (" + currentValues.Replace(';', ',') + ") ";
        }


        // If not postback or site selection changed
        if (!RequestHelper.IsPostBack() || (siteId != Convert.ToInt32(ViewState["rolesOldSiteId"])))
        {
            // Set values
            usRoles.Value = currentValues;
        }

        // Store selected site id
        ViewState["rolesOldSiteId"] = siteId;

        string script = "function setNewDateTime(date) {$j('#" + hdnDate.ClientID + "').val(date);}";
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "key", ScriptHelper.GetScript(script));

        string eventTarget = Request["__EVENTTARGET"];
        string eventArgument = Request["__EVENTARGUMENT"];
        if (eventTarget == ucCalendar.DateTimeTextBox.UniqueID)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "ManageUserRoles"))
            {
                RedirectToAccessDenied("CMS.Users", "Manage user roles");
            }

            int id = ValidationHelper.GetInteger(hdnDate.Value, 0);
            if (id != 0)
            {
                DateTime dt = ValidationHelper.GetDateTime(eventArgument, DateTimeHelper.ZERO_TIME);
                UserRoleInfo uri = UserRoleInfoProvider.GetUserRoleInfo(userId, id);
                if (uri != null)
                {
                    uri.ValidTo = dt;
                    UserRoleInfoProvider.SetUserRoleInfo(uri);

                    // Invalidate user
                    UserInfo ui = UserInfoProvider.GetUserInfo(userId);
                    if (ui != null)
                    {
                        ui.Invalidate();
                    }

                    this.lblInfo.Visible = true;
                }
            }
        }
    }


    /// <summary>
    /// Callback event for create calendar icon.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="sourceName">Event source name</param>
    /// <param name="parameter">Event parameter</param>
    /// <param name="val">Value from basic external data bound event</param>
    object usMemberships_OnAdditionalDataBound(object sender, string sourceName, object parameter, string val)
    {
        switch (sourceName.ToLowerInvariant())
        {
            case "calendar":
                DataRowView drv = (parameter as DataRowView);
                string itemID = drv[usRoles.ReturnColumnName].ToString();
                string imageID = "img_" + itemID;
                string date = drv["ValidTo"].ToString();
                string postback = ControlsHelper.GetPostBackEventReference(ucCalendar.DateTimeTextBox, "#").Replace("'#'", "$j('#" + ucCalendar.DateTimeTextBox.ClientID + "').val()");
                string onClick = String.Empty;

                ucCalendar.DateTimeTextBox.Attributes["OnChange"] = postback;

                if (!ucCalendar.UseCustomCalendar)
                {
                    onClick = " onClick=\" $j('#" + hdnDate.ClientID + "').val('" + itemID + "');" + ucCalendar.GenerateNonCustomCalendarImageEvent() + "\"";
                }
                else
                {
                    onClick = " onClick=\" $j('#" + hdnDate.ClientID + "').val('" + itemID + "'); var dt = $j('#" + ucCalendar.DateTimeTextBox.ClientID + "'); dt.val('" + date + "'); dt.datepicker('setLocation','" + imageID + "'); dt.datepicker ('show');\"";
                }

                val = "<img class=\"CalendarIcon\" Title=\"" + GetString("membership.changevalidity") + "\" Alt=\"" + GetString("membership.changevalidity") + "\"  ID= \"" + imageID + "\" src=\"" + ResolveUrl("~/CMSAdminControls/ModalCalendar/Themes/calendar.png") + "\"" + onClick + ">";
                break;
        }

        return val;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Display message if user has no site
        if ((!siteSelector.UniSelector.HasData) && (!CMSContext.CurrentUser.IsGlobalAdministrator))
        {
            lblError.Visible = true;
            lblError.Text = GetString("Administration-User_Edit_Roles.UserWithNoSites");
        }
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        this.lblInfo.Visible = false;
        this.pnlUpdate.Update();
    }


    protected void usRoles_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveData();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Check whether current user is allowed to modify another user.
    /// </summary>
    /// <param name="userId">Modified user</param>
    /// <returns>"" or error message.</returns>
    protected static string ValidateGlobalAndDeskAdmin(UserInfo ui)
    {
        string result = String.Empty;

        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            return result;
        }

        if (ui == null)
        {
            result = ResHelper.GetString("Administration-User.WrongUserId");
        }
        else
        {
            if (ui.IsGlobalAdministrator)
            {
                result = ResHelper.GetString("Administration-User.NotAllowedToModify");
            }
        }
        return result;
    }


    /// <summary>
    /// Saves data.
    /// </summary>
    private void SaveData()
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "ManageUserRoles"))
        {
            RedirectToAccessDenied("CMS.Users", "Manage user roles");
        }

        bool saved = false;
        string result = ValidateGlobalAndDeskAdmin(ui);
        if (result != String.Empty)
        {
            lblErrorDeskAdmin.Visible = true;
            lblErrorDeskAdmin.Text = result;
            return;
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usRoles.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        this.lblInfo.Visible = false;

        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);
                    UserRoleInfoProvider.RemoveUserFromRole(userId, roleID);
                }

                saved = true;
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                DateTime dt = ValidationHelper.GetDateTime(hdnDate.Value, DateTimeHelper.ZERO_TIME);

                // Add all new items to site
                foreach (string item in newItems)
                {
                    int roleID = ValidationHelper.GetInteger(item, 0);
                    UserRoleInfoProvider.AddUserToRole(userId, roleID, dt);                    
                }

                saved = true;
            }
        }

        if (saved)
        {
            lblInfo.Visible = true;
            usRoles.Reload(true);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (RequestHelper.IsPostBack())
        {
            pnlBasic.Update();
        }

        base.OnPreRender(e);
    }

    #endregion
}
