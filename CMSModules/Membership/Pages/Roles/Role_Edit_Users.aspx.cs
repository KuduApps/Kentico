using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using System.Data;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Membership_Pages_Roles_Role_Edit_Users : CMSRolesPage
{
    private string currentValues = String.Empty;
    private int roleID = 0;

    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {

        // Check permissions
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (user != null)
        {
            if (!user.IsAuthorizedPerUIElement("CMS.Administration", "Users"))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Administration", "Users");
            }

            // Check "read" permissions
            if (!user.IsAuthorizedPerResource("CMS.Users", "Read"))
            {
                RedirectToAccessDenied("CMS.Users", "Read");
            }
        }

        lblAvialable.Text = GetString("roleusers.available");

        usUsers.AdditionalColumns = "UserID,ValidTo";
        usUsers.GridName = "~/CMSModules/Membership/Pages/Users/UsersValidTo.xml";
        roleID = QueryHelper.GetInteger("roleid", 0);
        usUsers.IsLiveSite = false;

        // Show only user belonging to role's site
        RoleInfo ri = RoleInfoProvider.GetRoleInfo(roleID);
        if (ri != null)
        {
            usUsers.WhereCondition = (ri.SiteID > 0) ? "UserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID = " + ri.SiteID + ")" : String.Empty; ; ;
            usUsers.ListingWhereCondition = "RoleID = " + ri.RoleID;
        }

        usUsers.DynamicColumnName = false;
        usUsers.SelectItemPageUrl = "~/CMSModules/Membership/Pages/Users/User_Edit_Add_Item_Dialog.aspx";

        currentValues = GetRoleUsers();
        if (!RequestHelper.IsPostBack())
        {
            usUsers.Value = currentValues;
        }

        if (!String.IsNullOrEmpty(currentValues))
        {
            usUsers.WhereCondition = SqlHelperClass.AddWhereCondition(usUsers.WhereCondition, "UserID NOT IN (" + currentValues.Replace(';', ',') + ")");
        }

        usUsers.OnSelectionChanged += UniSelector_OnSelectionChanged;
        usUsers.OnAdditionalDataBound += new CMSAdminControls_UI_UniSelector_UniSelector.AdditionalDataBoundEventHandler(usUsers_OnAdditionalDataBound);

        string script = "function setNewDateTime(date) {$j('#" + hdnDate.ClientID + "').val(date);}";
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "key", ScriptHelper.GetScript(script));

        string eventTarget = Request["__EVENTTARGET"];
        string eventArgument = Request["__EVENTARGUMENT"];
        if (eventTarget == ucCalendar.DateTimeTextBox.UniqueID)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "ManageUserRoles"))
            {
                RedirectToCMSDeskAccessDenied("CMS.Users", "ManageUserRoles");
            }

            int id = ValidationHelper.GetInteger(hdnDate.Value, 0);
            if (id != 0)
            {
                DateTime dt = ValidationHelper.GetDateTime(eventArgument, DateTimeHelper.ZERO_TIME);
                UserRoleInfo uri = UserRoleInfoProvider.GetUserRoleInfo(id, ri.RoleID);
                if (uri != null)
                {
                    uri.ValidTo = dt;
                    UserRoleInfoProvider.SetUserRoleInfo(uri);

                    // Invalidate user
                    UserInfo.TYPEINFO.ObjectInvalidated(id);

                    this.lblInfo.Visible = true;
                    this.lblInfo.Text = GetString("General.ChangesSaved");
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
    object usUsers_OnAdditionalDataBound(object sender, string sourceName, object parameter, string val)
    {
        DataRowView drv = null;
        switch (sourceName.ToLowerInvariant())
        {
            case "calendar":
                drv = (parameter as DataRowView);
                string itemID = drv[usUsers.ReturnColumnName].ToString();
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

                val = "<img class=\"CalendarIcon\" Alt=\"Calendar icon\" ID= \"" + imageID + "\" src=\"" + ResolveUrl("~/CMSAdminControls/ModalCalendar/Themes/calendar.png") + "\"" + onClick + ">";
                break;

            // Resolve User name
            case "name":
                drv = (parameter as DataRowView);
                string name = ValidationHelper.GetString(drv["UserName"], String.Empty);
                string fullName = ValidationHelper.GetString(drv["FullName"], String.Empty);

                val = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(name, fullName, String.Empty, false));
                break;
        }

        return val;
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveUsers();
    }


    private string GetRoleUsers()
    {
        DataSet ds = UserRoleInfoProvider.GetUserRoles("RoleID = " + roleID, null, 0, "UserID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "UserID"));
        }

        return String.Empty;
    }


    private void SaveUsers()
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "ManageUserRoles"))
        {
            RedirectToCMSDeskAccessDenied("CMS.Users", "ManageUserRoles");
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
                        UserRoleInfoProvider.RemoveUserFromRole(userId, roleID);
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
                DateTime dt = ValidationHelper.GetDateTime(hdnDate.Value, DateTimeHelper.ZERO_TIME);

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
                        UserRoleInfoProvider.AddUserToRole(userId, roleID, dt);
                        saved = true;
                    }
                }
            }
        }

        if (falseValues)
        {
            currentValues = GetRoleUsers();
            usUsers.Value = currentValues;
        }

        if (saved)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }

        usUsers.Reload(true);
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
    /// Page PreRender.
    /// </summary>
    /// <param name="e"></param>
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

