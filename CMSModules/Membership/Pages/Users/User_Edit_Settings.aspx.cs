using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Users_User_Edit_Settings : CMSUsersPage
{
    private UserInfo ui;


    /// <summary>
    /// Handles onInit, fill timezone dropdownlist.
    /// </summary>    
    protected override void OnInit(EventArgs e)
    {
        timeZone.ReloadData();
        base.OnInit(e);
    }


    /// <summary>
    /// Handles Page Load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Load labels
        btnOk.Text = GetString("general.ok");
        lblActivatedByUser.Text = GetString("adm.user.lblActivatedByUser");
        lblActivationDate.Text = GetString("adm.user.lblActivationDate");
        lblCampaign.Text = GetString("adm.user.lblCampaign");
        lblMessageNotifEmail.Text = GetString("adm.user.lblMessageNotifEmail");
        lblNickName.Text = GetString("adm.user.lblNickName");
        lblRegInfo.Text = GetString("adm.user.lblRegInfo");
        lblTimeZone.Text = GetString("adm.user.lblTimeZone");
        lblURLReferrer.Text = GetString("adm.user.lblURLReferrer");
        lblUserPicture.Text = GetString("adm.user.lblUserPicture");
        lblUserSignature.Text = GetString("adm.user.lblUserSignature");
        lblWaitingForActivation.Text = GetString("adm.user.lblWaitingForActivation");
        lblUserLiveID.Text = GetString("adm.user.lblUserLiveID");
        lblUserOpenID.ResourceString = "adm.user.lblOpenID";
        lblLinkedInUserID.Text = GetString("adm.user.lblUserLinkedInID");

        lblBadge.Text = GetString("adm.user.lblBadge");
        lblUserShowSplashScreen.Text = GetString("adm.user.lblUserShowSplashScreen");
        lblUserActivityPoints.Text = GetString("adm.user.lblUserActivityPoints");
        lblUserForumPosts.Text = GetString("adm.user.lblUserForumPosts");
        lblUserBlogPosts.Text = GetString("adm.user.lblUserBlogPosts");
        lblUserBlogComments.Text = GetString("adm.user.lblUserBlogComments");
        lblUserGender.Text = GetString("adm.user.lblUserGender");
        lblUserDateOfBirth.Text = GetString("adm.user.lblUserDateOfBirth");
        lblUserMessageBoardPosts.Text = GetString("adm.user.lblUserMessageBoardPosts");
        lblPosition.Text = ResHelper.GetString("adm.user.lblUserPosition");
        lblUserSkype.Text = ResHelper.GetString("adm.user.lblUserSkype");
        lblUserIM.Text = ResHelper.GetString("adm.user.lblUserIM");
        lblUserPhone.Text = ResHelper.GetString("adm.user.lblUserPhone");

        if (!RequestHelper.IsPostBack())
        {
            rbtnlGender.Items.Add(new ListItem(GetString("general.unknown"), "0"));
            rbtnlGender.Items.Add(new ListItem(GetString("general.male"), "1"));
            rbtnlGender.Items.Add(new ListItem(GetString("general.female"), "2"));
        }

        // Get userid from query string
        int userId = QueryHelper.GetInteger("userID", 0);

        // Check that only global administrator can edit global administrator's accouns
        if (userId > 0)
        {
            ui = UserInfoProvider.GetUserInfo(userId);
            CheckUserAvaibleOnSite(ui); 
            EditedObject = ui;

            if (!CheckGlobalAdminEdit(ui))
            {
                plcTable.Visible = false;
                lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                lblError.Visible = true;
            }
        }

        UserPictureFormControl.IsLiveSite = false;

        //Load user data
        LoadData();
    }


    /// <summary>
    /// Loads data of edited user from DB.
    /// </summary>
    protected void LoadData()
    {
        //Check if user exists
        if (ui != null)
        {
            if (!RequestHelper.IsPostBack())
            {
                if ((ui.UserSettings != null) && (ui.UserSettings.UserActivatedByUserID > 0))
                {
                    UserInfo user = UserInfoProvider.GetUserInfo(ui.UserSettings.UserActivatedByUserID);
                    if (user != null)
                    {
                        lblUserFullName.Text = HTMLHelper.HTMLEncode(user.FullName);
                    }
                }

                if (String.IsNullOrEmpty(lblUserFullName.Text))
                {
                    lblUserFullName.Text = GetString("general.na");
                }

                activationDate.SelectedDateTime = ui.UserSettings.UserActivationDate;
                txtCampaign.Text = ui.UserCampaign;
                txtMessageNotifEmail.Text = ui.UserMessagingNotificationEmail;
                txtNickName.Text = ui.UserNickName;
                //txtCustomData.Text = ui.UserCustomData.GetData();
                //txtPreferences.Text = ui.UserSettings.UserPreferences.Value;
                LoadRegInfo(ui.UserSettings);
                timeZone.Value = ui.UserSettings.UserTimeZoneID;
                txtURLReferrer.Text = ui.UserURLReferrer;
                txtUserSignature.Text = ui.UserSignature;
                txtUserDescription.Text = ui.UserSettings.UserDescription;
                chkWaitingForActivation.Checked = ui.UserSettings.UserWaitingForApproval;
                chkLogActivities.Checked = ui.UserSettings.UserLogActivities;
                badgeSelector.Value = ui.UserSettings.UserBadgeID;
                txtUserLiveID.Text = ui.UserSettings.WindowsLiveID;
                txtFacebookUserID.Text = ui.UserSettings.UserFacebookID;
                txtOpenID.Text = OpenIDUserInfoProvider.GetOpenIDByUserID(ui.UserID);
                txtLinkedInID.Text = ui.UserSettings.UserLinkedInID;
                chkUserShowSplashScreen.Checked = ui.UserSettings.UserShowSplashScreen;
                txtUserActivityPoints.Text = ui.UserSettings.UserActivityPoints.ToString();
                lblUserForumPostsValue.Text = ui.UserSettings.UserForumPosts.ToString();
                lblUserBlogPostsValue.Text = ui.UserSettings.UserBlogPosts.ToString();
                lblUserBlogCommentsValue.Text = ui.UserSettings.UserBlogComments.ToString();
                rbtnlGender.SelectedValue = ui.UserSettings.UserGender.ToString();
                dtUserDateOfBirth.SelectedDateTime = ui.UserSettings.UserDateOfBirth;
                lblUserMessageBoardPostsValue.Text = ui.UserSettings.UserMessageBoardPosts.ToString();
                txtUserSkype.Text = ValidationHelper.GetString(ui.UserSettings.GetValue("UserSkype"), "");
                txtUserIM.Text = ValidationHelper.GetString(ui.UserSettings.GetValue("UserIM"), "");
                txtPhone.Text = ValidationHelper.GetString(ui.UserSettings.GetValue("UserPhone"), "");
                txtPosition.Text = ValidationHelper.GetString(ui.UserSettings.GetValue("UserPosition"), "");
            }

            // Load user picture, even for postback
            SetUserPictureArea(ui);
        }
    }


    /// <summary>
    /// Displays user's registration information.
    /// </summary>
    /// <param name="ui">User info</param>
    protected void LoadRegInfo(UserSettingsInfo usi)
    {
        if ((usi.UserRegistrationInfo != null) && (usi.UserRegistrationInfo.ColumnNames != null))
        {
            plcUserLastLogonInfo.Controls.Add(new LiteralControl("<br />"));
            foreach (string column in usi.UserRegistrationInfo.ColumnNames)
            {
                Label lbl = new Label();
                lbl.Text = HTMLHelper.HTMLEncode(TextHelper.LimitLength((string)usi.UserRegistrationInfo[column], 80, "...")) + "<br />";
                lbl.ToolTip = HTMLHelper.HTMLEncode(column + " - " + (string)usi.UserRegistrationInfo[column]);
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
    /// Saves data of edited user from TextBoxes into DB.
    /// </summary>
    protected void ButtonOK_Click(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        string errorMessage = null;

        // If some email address is set check the format
        if (!string.IsNullOrEmpty(txtMessageNotifEmail.Text))
        {
            errorMessage = new Validator().IsEmail(txtMessageNotifEmail.Text, GetString("adm.user.messageemailincorrect")).Result;
        }

        if (!UserPictureFormControl.IsValid())
        {
            errorMessage = UserPictureFormControl.ErrorMessage;
        }

        if (!dtUserDateOfBirth.IsValidRange() || !activationDate.IsValidRange ())
        {
            errorMessage = GetString("general.errorinvaliddatetimerange");
        }

        // Clean from empty strings
        txtNickName.Text = txtNickName.Text.Trim();
        txtUserSignature.Text = txtUserSignature.Text.Trim();
        txtUserDescription.Text = txtUserDescription.Text.Trim();
        txtMessageNotifEmail.Text = txtMessageNotifEmail.Text.Trim();

        if (string.IsNullOrEmpty(errorMessage) && (ui != null))
        {
            ui.UserSettings.UserActivationDate = activationDate.SelectedDateTime;
            ui.UserCampaign = txtCampaign.Text;
            ui.UserMessagingNotificationEmail = txtMessageNotifEmail.Text;
            ui.UserNickName = txtNickName.Text;

            // Check that Windows Live ID is not already registered to some user
            string windowsID = txtUserLiveID.Text.Trim();
            UserInfo uiUpdated = UserInfoProvider.GetUserInfoByWindowsLiveID(windowsID);
            // Windows Live ID is not assigned
            if ((uiUpdated == null) || (uiUpdated.UserID == ui.UserID))
            {
                ui.UserSettings.WindowsLiveID = windowsID;
            }
            // Windows Live ID is already assigned
            else
            {
                lblError.Text = GetString("mem.liveid.idalreadyregistered") + uiUpdated.UserName;
                lblError.Visible = true;
                return;
            }

            // Check that FacebookID is not already registered to some user
            string facebookID = txtFacebookUserID.Text.Trim();
            uiUpdated = UserInfoProvider.GetUserInfoByFacebookConnectID(facebookID);
            // Facebook ID not assigned to a user
            if ((uiUpdated == null) || (uiUpdated.UserID == ui.UserID))
            {
                ui.UserSettings.UserFacebookID = facebookID;
            }
            // Facebook ID is already registered
            else
            {
                lblError.Text = GetString("mem.facebook.idregistered") + uiUpdated.UserName;
                lblError.Visible = true;
                return;
            }

            // Check that LinkedIn member ID is not already registered to some user
            string linkedInID = txtLinkedInID.Text.Trim();
            uiUpdated = UserInfoProvider.GetUserInfoByLinkedInID(linkedInID);
            // LinkedIn member ID is not assigned
            if ((uiUpdated == null) || (uiUpdated.UserID == ui.UserID))
            {
                ui.UserSettings.UserLinkedInID = linkedInID;
            }
            // LinkedIn member ID is already assigned
            else
            {
                lblError.Text = GetString("mem.linkedin.idalreadyregistered") + uiUpdated.UserName;
                lblError.Visible = true;
                return;
            }

            ui.UserSettings.UserTimeZoneID = ValidationHelper.GetInteger(timeZone.Value, 0);
            ui.UserURLReferrer = txtURLReferrer.Text;
            ui.UserSignature = txtUserSignature.Text;
            ui.UserSettings.UserDescription = txtUserDescription.Text;

            ui.UserSettings.UserWaitingForApproval = chkWaitingForActivation.Checked;
            ui.UserSettings.UserLogActivities = chkLogActivities.Checked;
            ui.UserSettings.UserBadgeID = ValidationHelper.GetInteger(badgeSelector.Value, 0);

            ui.UserSettings.UserShowSplashScreen = chkUserShowSplashScreen.Checked;

            ui.UserSettings.UserActivityPoints = ValidationHelper.GetInteger(txtUserActivityPoints.Text, 0);
            ui.UserSettings.UserGender = ValidationHelper.GetInteger(rbtnlGender.SelectedValue, 0);
            ui.UserSettings.UserDateOfBirth = dtUserDateOfBirth.SelectedDateTime;

            ui.UserSettings.SetValue("UserPosition", txtPosition.Text);
            ui.UserSettings.SetValue("UserSkype", txtUserSkype.Text);
            ui.UserSettings.SetValue("UserIM", txtUserIM.Text);
            ui.UserSettings.SetValue("UserPhone", txtPhone.Text);

            //Set user picture to DB
            UserPictureFormControl.UpdateUserPicture(ui);
            UserInfoProvider.SetUserInfo(ui);

            // Update OpenID value
            UpdateOpenID(ui);

            // Display info label only if no error occured
            if (!lblError.Visible)
            {
                //Update user picture on the page
                SetUserPictureArea(ui);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Updates OpenID for given user.
    /// </summary>
    private void UpdateOpenID(UserInfo ui)
    {
        if (ui != null)
        {
            string oldOpenID = OpenIDUserInfoProvider.GetOpenIDByUserID(ui.UserID);
            string newOpenID = txtOpenID.Text.Trim();
            UserInfo uiUpdated = OpenIDUserInfoProvider.GetUserInfoByOpenID(newOpenID);

            // Make sure that only non-existing OpenID identificator can be saved
            if ((uiUpdated == null) || (uiUpdated.UserID == ui.UserID))
            {
                // Update or delete given OpenID related to user
                OpenIDUserInfoProvider.UpdateOpenIDUserInfo(oldOpenID, newOpenID, ui.UserID);
            }
            else
            {
                lblError.Text = GetString("mem.openid.idassignedto") + uiUpdated.UserName;
                lblError.Visible = true;
            }
        }
    }


    /// <summary>
    /// Sets user picture control.
    /// </summary>    
    private void SetUserPictureArea(UserInfo ui)
    {
        UserPictureFormControl.UserInfo = ui;
        UserPictureFormControl.MaxSideSize = 100;
    }
}
