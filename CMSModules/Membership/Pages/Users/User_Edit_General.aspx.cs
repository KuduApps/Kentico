using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.EventLog;

public partial class CMSModules_Membership_Pages_Users_User_Edit_General : CMSUsersPage
{
    #region "Protected variables"

    protected int userId = 0;
    protected string password;
    protected string myCulture = string.Empty;
    protected string myUICulture = string.Empty;
    private UserInfo ui = null;
    private CurrentUserInfo currentUser = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;

        rfvFullName.ErrorMessage = GetString("Administration-User_New.RequiresFullName");
        ucUserName.UseDefaultValidationGroup = false;

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "PictDelConfirm",
            ScriptHelper.GetScript("function DeleteConfirmation(){ return confirm(" + ScriptHelper.GetString(GetString("MyProfile.PictDeleteConfirm")) + ");}"));

        CheckBoxLabelIsGlobalAdministrator.Enabled = currentUser.IsGlobalAdministrator;
        userId = QueryHelper.GetInteger("userid", 0);

        cultureSelector.DisplayAllCultures = true;

        if (userId > 0)
        {
            ui = UserInfoProvider.GetUserInfo(userId);
            CheckUserAvaibleOnSite(ui);
            EditedObject = ui;

            // Check that only global administrator can edit global administrator's accouns            
            if (!CheckGlobalAdminEdit(ui))
            {
                plcTable.Visible = false;
                lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                lblError.Visible = true;
            }

            if (!RequestHelper.IsPostBack())
            {
                LoadData();
            }
        }

        // Register help variable for user is external confirmation
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "IsExternal", ScriptHelper.GetScript("var isExternal = " + chkIsExternal.Checked.ToString().ToLower() + ";"));

        // Javascript code for "Is external user" confirmation 
        string javascript = ScriptHelper.GetScript(
                            @"function CheckExternal() {
                            var checkbox = document.getElementById('" + chkIsExternal.ClientID + @"')
                            if(checkbox.checked && !isExternal) {                                                                    
                                if(!confirm('" + GetString("user.confirmexternal") + @"')) {                                                                       
                                    checkbox.checked = false ;                                    
                                }
                            }}");

        // Register script to the page
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), ClientID + "CheckExternal", javascript);

        // Assing to ok button
        if (!chkIsExternal.Checked)
        {
            btnOk.OnClientClick = "CheckExternal()";
        }

        // Display impersonation link if current user is global administrator and target user is not
        if (currentUser.IsGlobalAdministrator && RequestHelper.IsFormsAuthentication() && (ui != null) && (ui.UserID != currentUser.UserID) && !ui.IsPublic() && !ui.IsGlobalAdministrator)
        {
            string message = GetImpersonalMessage(ui);

            // Header actions
            string[,] actions = new string[1, 11];

            actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
            actions[0, 1] = GetString("Membership.Impersonate");
            actions[0, 2] = "if (!confirm('" + message + "')) { return false; }";
            actions[0, 4] = GetString("Membership.Impersonate");
            actions[0, 5] = GetImageUrl("Objects/CMS_User/Impersonate.png");
            actions[0, 6] = "impersonate";
            actions[0, 8] = "true";

            CurrentMaster.HeaderActions.Actions = actions;
            CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (ui != null)
        {
            // Only admin who have currently access to site manager can disable access to sitemanager to others
            if (!ui.IsGlobalAdministrator || (currentUser.UserID == ui.UserID) || currentUser.UserSiteManagerDisabled)
            {
                // Only admin who have access to site manager can create another global admin
                if (currentUser.UserSiteManagerDisabled)
                {
                    plcGlobalAdmin.Visible = false;
                }

                plcSiteManagerDisabled.Visible = false;
            }
            else
            {
                plcSiteManagerDisabled.Visible = true;
            }
        }
    }


    /// <summary>
    /// Users actions.
    /// </summary>
    void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "impersonate":
                if (currentUser.IsGlobalAdministrator)
                {
                    UserInfo ui = UserInfoProvider.GetUserInfo(userId);
                    currentUser.UserImpersonate(ui);
                }
                break;
        }
    }


    /// <summary>
    /// Saves data of edited user from TextBoxes into DB.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!currentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        string result = ValidateGlobalAndDeskAdmin(userId);

        // Find whether user name is valid
        if (result == String.Empty)
        {
            if (!ucUserName.IsValid())
            {
                result = ucUserName.ValidationError;
            }
        }

        String userName = ValidationHelper.GetString(ucUserName.Value, String.Empty);
        if (result == String.Empty)
        {
            // Finds whether required fields are not empty
            result = new Validator().NotEmpty(txtFullName.Text, GetString("Administration-User_New.RequiresFullName")).Result;
        }

        if ((result == String.Empty) && (ui != null))
        {
            // If site prefixed allowed - ad site prefix
            if ((SiteID != 0) && UserInfoProvider.UserNameSitePrefixEnabled(CMSContext.CurrentSiteName))
            {
                if (!UserInfoProvider.IsSitePrefixedUser(userName))
                {
                    userName = UserInfoProvider.EnsureSitePrefixUserName(userName, CMSContext.CurrentSite);
                }
            }

            // Validation for site prefixed users
            if (!UserInfoProvider.IsUserNamePrefixUnique(userName, ui.UserID))
            {
                result = GetString("Administration-User_New.siteprefixeduserexists");
            }

            // Ensure same password
            password = ui.GetValue("UserPassword").ToString();

            // Test for unique username
            UserInfo uiTest = UserInfoProvider.GetUserInfo(userName);
            if ((uiTest == null) || (uiTest.UserID == userId))
            {
                if (ui == null)
                {
                    ui = new UserInfo();
                }

                bool globAdmin = ui.IsGlobalAdministrator;
                bool editor = ui.IsEditor;

                // Email format validation
                string email = txtEmail.Text.Trim();
                if ((email != string.Empty) && (!ValidationHelper.IsEmail(email)))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Administration-User_New.WrongEmailFormat");
                    return;
                }

                bool oldGlobal = ui.IsGlobalAdministrator;
                bool oldEditor = ui.IsEditor;

                if (ui.UserName != userName)
                {
                    // Refresh the breadcrumb
                    ScriptHelper.RefreshTabHeader(Page, null);
                }

                ui.Email = email;
                ui.FirstName = txtFirstName.Text.Trim();
                ui.FullName = txtFullName.Text.Trim();
                ui.LastName = txtLastName.Text.Trim();
                ui.MiddleName = txtMiddleName.Text.Trim();
                ui.UserName = userName;
                ui.Enabled = CheckBoxEnabled.Checked;
                ui.IsEditor = CheckBoxIsEditor.Checked;
                ui.UserIsHidden = chkIsHidden.Checked;

                // Only admins who have access to site manager can set this 
                if (!currentUser.UserSiteManagerDisabled)
                {
                    ui.UserSiteManagerDisabled = chkSiteManagerDisabled.Checked;

                    // Only admin who have access to site manager can create another global admin
                    ui.IsGlobalAdministrator = currentUser.IsGlobalAdministrator && CheckBoxLabelIsGlobalAdministrator.Checked;
                }

                ui.IsExternal = chkIsExternal.Checked;
                ui.UserIsDomain = chkIsDomain.Checked;
                ui.SetValue("UserPassword", password);
                ui.UserID = userId;
                ui.UserStartingAliasPath = txtUserStartingPath.Text.Trim();

                LoadUserLogon(ui);

                // Set values of cultures.
                string culture = ValidationHelper.GetString(cultureSelector.Value, "");
                ui.PreferredCultureCode = culture;

                if (lstUICulture.SelectedValue == "0")
                    ui.PreferredUICultureCode = "";
                else
                {
                    UICultureInfo ciUI = UICultureInfoProvider.GetUICultureInfo(Convert.ToInt32(lstUICulture.SelectedValue));
                    ui.PreferredUICultureCode = ciUI.UICultureCode;
                }

                // Define domain variable
                string domains = null;

                // Get all user sites
                DataTable ds = UserInfoProvider.GetUserSites(ui.UserID, null, null, 0, "SiteDomainName");
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    foreach (DataRow dr in ds.Rows)
                    {
                        domains += ValidationHelper.GetString(dr["SiteDomainName"], string.Empty) + ";";
                    }

                    // Remove  ";" at the end
                    if (domains != null)
                    {
                        domains = domains.Remove(domains.Length - 1);
                    }
                }
                else
                {
                    DataSet siteDs = SiteInfoProvider.GetSites(null, null, "SiteDomainName");
                    if (!DataHelper.DataSourceIsEmpty(siteDs))
                    {
                        domains = TextHelper.Join(";", SqlHelperClass.GetStringValues(siteDs.Tables[0], "SiteDomainName"));
                    }
                }

                // Check limitations for Global administrator
                if (ui.IsGlobalAdministrator && !oldGlobal)
                {
                    if (!UserInfoProvider.LicenseVersionCheck(domains, FeatureEnum.GlobalAdmininistrators, VersionActionEnum.Insert, globAdmin))
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("License.MaxItemsReachedGlobal");
                    }
                }

                // Check limitations for editors
                if (ui.IsEditor && !oldEditor)
                {
                    if (!UserInfoProvider.LicenseVersionCheck(domains, FeatureEnum.Editors, VersionActionEnum.Insert, editor))
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("License.MaxItemsReachedEditor");
                    }
                }

                // Check whether email is unique if it is required
                if (!UserInfoProvider.IsEmailUnique(email, ui))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("UserInfo.EmailAlreadyExist");
                    return;
                }

                if (!lblError.Visible)
                {
                    // Check whether the username of the currently logged user has been changed
                    if ((currentUser != null) && (currentUser.UserID == ui.UserID) && (currentUser.UserName != ui.UserName))
                    {
                        // Ensure that an update search task will be created but NOT executed when updating the user
                        CMSActionContext.EnableSearchIndexer = false;
                    }

                    // Update the user
                    UserInfoProvider.SetUserInfo(ui);

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Administration-User_New.UserExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }

        // Display impersonation link if current user is global administrator
        if (CurrentMaster.HeaderActions.Actions != null)
        {
            if (currentUser.IsGlobalAdministrator && RequestHelper.IsFormsAuthentication() && (ui != null) && (ui.UserID != currentUser.UserID))
            {
                if (!currentUser.UserSiteManagerDisabled || !ui.UserIsGlobalAdministrator || ui.UserSiteManagerDisabled)
                {
                    string message = GetImpersonalMessage(ui);
                    CurrentMaster.HeaderActions.Actions[0, 2] = "if (!confirm('" + message + "')) { return false; }";
                    CurrentMaster.HeaderActions.ReloadData();
                }
            }
        }
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Returns the impersonalization message for current user.
    /// </summary>
    /// <param name="ui">User info</param>
    protected string GetImpersonalMessage(UserInfo ui)
    {
        string message = String.Empty;

        // Global admin message
        if (ui.IsGlobalAdministrator)
        {
            message = GetString("Membership.ImperConfirmGlobal");
        }
        // Editor message
        else if (ui.IsEditor)
        {
            message = GetString("Membership.ImperConfirmEditor");
        }
        // Default user message
        else
        {
            message = GetString("Membership.ImperConfirmDefault");
        }

        return message;
    }


    /// <summary>
    /// Loads data of edited user from DB into TextBoxes.
    /// </summary>
    protected void LoadData()
    {
        // Fill lstUICulture (loop over and localize them first)
        DataSet uiCultures = UICultureInfoProvider.GetUICultures(null, "UICultureName ASC");
        LocalizeCultureNames(uiCultures);
        lstUICulture.DataSource = uiCultures.Tables[0].DefaultView;
        lstUICulture.DataTextField = "UICultureName";
        lstUICulture.DataValueField = "UICultureID";
        lstUICulture.DataBind();

        lstUICulture.Items.Insert(0, GetString("Administration-User_Edit.Default"));
        lstUICulture.Items[0].Value = "0";

        if (ui != null)
        {
            txtEmail.Text = ui.Email;
            txtFirstName.Text = ui.FirstName;
            txtFullName.Text = ui.FullName;
            txtLastName.Text = ui.LastName;
            txtMiddleName.Text = ui.MiddleName;
            ucUserName.Value = ui.UserName;

            CheckBoxEnabled.Checked = ui.Enabled;
            CheckBoxIsEditor.Checked = ui.IsEditor;
            CheckBoxLabelIsGlobalAdministrator.Checked = ui.IsGlobalAdministrator;
            chkIsExternal.Checked = ui.IsExternal;
            chkIsDomain.Checked = ui.UserIsDomain;
            chkIsHidden.Checked = ui.UserIsHidden;
            chkSiteManagerDisabled.Checked = ui.UserSiteManagerDisabled;

            password = ui.GetValue("UserPassword").ToString();

            if (ui.IsPublic())
            {
                ucUserName.Enabled = false;
            }

            myCulture = ui.PreferredCultureCode;
            myUICulture = ui.PreferredUICultureCode;

            txtUserStartingPath.Text = ui.UserStartingAliasPath;
        }

        cultureSelector.Value = myCulture;

        if (!string.IsNullOrEmpty(myUICulture))
        {
            try
            {
                UICultureInfo ciUI = UICultureInfoProvider.GetUICultureInfo(myUICulture);
                lstUICulture.SelectedIndex = lstUICulture.Items.IndexOf(lstUICulture.Items.FindByValue(ciUI.UICultureID.ToString()));
            }
            catch
            {
                lstUICulture.SelectedIndex = lstUICulture.Items.IndexOf(lstUICulture.Items.FindByValue("0"));
            }
        }
        else
        {
            lstUICulture.SelectedIndex = lstUICulture.Items.IndexOf(lstUICulture.Items.FindByValue("0"));
        }

        if (ui != null)
        {
            lblCreatedInfo.Text = ui.UserCreated.ToString();
            lblLastLogonTime.Text = ui.LastLogon.ToString();

            LoadUserLogon(ui);

            if (ui.UserCreated == DataHelper.DATETIME_NOT_SELECTED)
            {
                lblCreatedInfo.Text = GetString("general.na");
            }

            if (ui.LastLogon == DataHelper.DATETIME_NOT_SELECTED)
            {
                lblLastLogonTime.Text = GetString("general.na");
            }
        }
    }


    /// <summary>
    /// Displays user's last logon information.
    /// </summary>
    /// <param name="ui">User info</param>
    protected void LoadUserLogon(UserInfo ui)
    {
        if ((ui.UserLastLogonInfo != null) && (ui.UserLastLogonInfo.ColumnNames != null))
        {
            plcUserLastLogonInfo.Controls.Add(new LiteralControl("<br />"));
            foreach (string column in ui.UserLastLogonInfo.ColumnNames)
            {
                Label lbl = new Label();
                lbl.Text = HTMLHelper.HTMLEncode(TextHelper.LimitLength((string)ui.UserLastLogonInfo[column], 80, "...")) + "<br />";
                lbl.ToolTip = HTMLHelper.HTMLEncode(column + " - " + (string)ui.UserLastLogonInfo[column]);
                plcUserLastLogonInfo.Controls.Add(lbl);
            }

            plcUserLastLogonInfo.Controls.Add(new LiteralControl("<br />"));
        }
        else
        {
            plcUserLastLogonInfo.Controls.Add(new LiteralControl(GetString("general.na") + "<br />"));
        }
    }


    /// <summary>
    /// Check whether current user is allowed to modify another user. Return "" or error message.
    /// </summary>
    /// <param name="userId">Modified user</param>
    protected string ValidateGlobalAndDeskAdmin(int userId)
    {
        string result = String.Empty;

        if (currentUser.IsGlobalAdministrator)
        {
            return result;
        }

        UserInfo userInfo = UserInfoProvider.GetUserInfo(userId);
        if (userInfo == null)
        {
            result = GetString("Administration-User.WrongUserId");
        }
        else if (userInfo.IsGlobalAdministrator)
        {
            result = GetString("Administration-User.NotAllowedToModify");
        }
        return result;
    }


    /// <summary>
    /// Localizes culture names and sorts them in ascending order.
    /// </summary>
    /// <param name="uiCultures">DataSet containing the UI cultures</param>
    private void LocalizeCultureNames(DataSet uiCultures)
    {
        // Localize all available UI cultures
        if (!DataHelper.DataSourceIsEmpty(uiCultures))
        {
            for (int i = 0; i < uiCultures.Tables[0].Rows.Count; i++)
            {
                uiCultures.Tables[0].Rows[i]["UICultureName"] = ResHelper.LocalizeString(uiCultures.Tables[0].Rows[i]["UICultureName"].ToString());
            }
        }

        uiCultures.Tables[0].DefaultView.Sort = "UICultureName ASC";
    }

    #endregion
}