using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Membership_Pages_Users_User_New : CMSUsersPage
{
    String userName = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        ucUserName.UseDefaultValidationGroup = false;

        LabelConfirmPassword.Text = GetString("Administration-User_New.ConfirmPassword");
        LabelIsEditor.Text = GetString("Administration-User_New.IsEditor");
        LabelFullName.Text = GetString("Administration-User_New.FullName");
        LabelPassword.Text = GetString("Administration-User_New.Password");
        ButtonOK.Text = GetString("general.ok");
        RequiredFieldValidatorFullName.ErrorMessage = GetString("Administration-User_New.RequiresFullName");

        if (!RequestHelper.IsPostBack())
        {
            CheckBoxEnabled.Checked = true;
            CheckBoxIsEditor.Checked = true;
        }

        string users = GetString("general.users");
        string currentUser = GetString("Administration-User_New.CurrentUser");
        string title = GetString("Administration-User_New.Title");

        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = users;
        pageTitleTabs[0, 1] = "~/CMSModules/Membership/Pages/Users/Users_Frameset.aspx?siteid=" + SiteID;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentUser;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.HelpTopicName = "new_user";
    }


    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        // Email format validation
        if ((TextBoxEmail.Text.Trim() != "") && (!ValidationHelper.IsEmail(TextBoxEmail.Text)))
        {
            lblError.Visible = true;
            lblError.Text = GetString("Administration-User_New.WrongEmailFormat");
            return;
        }

        // Find whether user name is valid
        string result = null;
        if (!ucUserName.IsValid())
        {
            result = ucUserName.ValidationError;
        }

        // Additional validation
        if (String.IsNullOrEmpty(result))
        {
            result = new Validator().NotEmpty(TextBoxFullName.Text, GetString("Administration-User_New.RequiresFullName")).Result;
        }

        userName = ValidationHelper.GetString(ucUserName.Value, String.Empty);

        // If site prefixed allowed - add site prefix
        if ((SiteID != 0) && UserInfoProvider.UserNameSitePrefixEnabled(CMSContext.CurrentSiteName))
        {
            if (!UserInfoProvider.IsSitePrefixedUser(userName))
            {
                userName = UserInfoProvider.EnsureSitePrefixUserName(userName, CMSContext.CurrentSite);
            }
        }

        // Validation for site prefixed users
        if (!UserInfoProvider.IsUserNamePrefixUnique(userName, 0))
        {
            result = GetString("Administration-User_New.siteprefixeduserexists");
        }

        if (result == "")
        {
            if (TextBoxConfirmPassword.Text == passStrength.Text)
            {
                // Check wheter password is valid according to policy
                if (passStrength.IsValid())
                {
                    if (UserInfoProvider.GetUserInfo(userName) == null)
                    {
                        int userId = SaveNewUser();
                        if (userId != -1)
                        {
                            URLHelper.Redirect("User_Edit_Frameset.aspx?userId=" + userId + "&siteid=" + SiteID);
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
                    lblError.Text = UserInfoProvider.GetPolicyViolationMessage(CMSContext.CurrentSiteName);
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Administration-User_Edit_Password.PasswordsDoNotMatch");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Saves new user's data into DB.
    /// </summary>
    /// <returns>Returns ID of created user</returns>
    protected int SaveNewUser()
    {
        UserInfo ui = new UserInfo();        

        // Load default values
        FormHelper.LoadDefaultValues("cms.user", ui);

        ui.PreferredCultureCode = "";
        ui.Email = TextBoxEmail.Text;
        ui.FirstName = "";
        ui.FullName = TextBoxFullName.Text;
        ui.LastName = "";
        ui.MiddleName = "";
        ui.UserName = userName;
        ui.Enabled = CheckBoxEnabled.Checked;
        ui.IsEditor = CheckBoxIsEditor.Checked;
        ui.IsExternal = false;
        ui.IsGlobalAdministrator = false;

        // Check license limitations only in cmsdesk
        if (SiteID > 0)
        {
            // Check limitations for Global administrator
            if (ui.IsGlobalAdministrator)
            {
                if (!UserInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.GlobalAdmininistrators, VersionActionEnum.Insert, false))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("License.MaxItemsReachedGlobal");
                }
            }

            // Check limitations for editors
            if (ui.IsEditor)
            {
                if (!UserInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Editors, VersionActionEnum.Insert, false))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("License.MaxItemsReachedEditor");
                }
            }

            // Check limitations for site members
            if (!UserInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.SiteMembers, VersionActionEnum.Insert, false))
            {
                lblError.Visible = true;
                lblError.Text = GetString("License.MaxItemsReachedSiteMember");
            }
        }


        // Check whether email is unique if it is required
        if (!UserInfoProvider.IsEmailUnique(TextBoxEmail.Text.Trim(), SiteName, 0))
        {
            lblError.Visible = true;
            lblError.Text = GetString("UserInfo.EmailAlreadyExist");
            return -1;
        }

        if (!lblError.Visible)
        {
            // Set password and save object
            UserInfoProvider.SetPassword(ui, passStrength.Text);

            // Add user to current site
            if (SiteID > 0)
            {
                UserInfoProvider.AddUserToSite(ui.UserName, SiteName);
            }

            return ui.UserID;
        }

        return -1;
    }
}
