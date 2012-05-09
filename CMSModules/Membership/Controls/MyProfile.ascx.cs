using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.FormControls;
using CMS.ExtendedControls;

public partial class CMSModules_Membership_Controls_MyProfile : CMSUserControl
{
    // Default form name
    private string mAlternativeFormName = "";

    /// <summary>
    /// Indicates if field visibility could be edited.
    /// </summary>
    private bool mAllowEditVisibility = false;

    #region "Public properties"

    /// <summary>
    /// Gets or sets the alternative form name(displays or edits user profile).
    /// </summary>
    public string AlternativeFormName
    {
        get
        {
            return mAlternativeFormName;
        }
        set
        {
            mAlternativeFormName = value;
            editProfileForm.AlternativeFormFullName = value;
            editProfileForm.VisibilityFormName = value;
        }
    }


    /// <summary>
    /// Indicates if field visibility could be edited on user form.
    /// </summary>
    public bool AllowEditVisibility
    {
        get
        {
            return mAllowEditVisibility;
        }
        set
        {
            mAllowEditVisibility = value;
            editProfileForm.AllowEditVisibility = value;
        }
    }

    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            editProfileForm.IsLiveSite = value;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        // Show only to authenticated users
        if ((CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated())
        {
            // Register handler for special actions
            editProfileForm.OnBeforeSave += editProfileForm_OnBeforeSave;
            editProfileForm.OnAfterSave += editProfileForm_OnAfterSave;

            // Get up-todate info of current user and use it for the form
            int userId = CMSContext.CurrentUser.UserID;
            editProfileForm.Info = UserInfoProvider.GetUserInfo(userId);
            editProfileForm.AlternativeFormFullName = AlternativeFormName;
            editProfileForm.VisibilityFormName = AlternativeFormName;
            editProfileForm.AllowEditVisibility = AllowEditVisibility;
            editProfileForm.IsLiveSite = IsLiveSite;            
        }
        else
        {
            Visible = false;
        }
    }


    /// <summary>
    /// OnAfterSave handler.
    /// </summary>
    protected void editProfileForm_OnAfterSave(object sender, EventArgs e)
    {
        lblInfo.Text = GetString("general.changessaved");
        lblInfo.Visible = true;
    }


    /// <summary>
    /// OnBeforeSave handler.
    /// </summary>
    protected void editProfileForm_OnBeforeSave(object sender, EventArgs e)
    {
        // If avatarUser id colunm is set
        if (editProfileForm.BasicForm.Data.GetValue("UserAvatarID") != DBNull.Value)
        {
            // If Avatar not set, rewrite to null value
            if (ValidationHelper.GetInteger(editProfileForm.BasicForm.Data.GetValue("UserAvatarID"), 0) == 0)
            {
                editProfileForm.BasicForm.Data.SetValue("UserAvatarID", DBNull.Value);
            }
        }

        // Set full name as first name + last name
        if (CMSContext.CurrentUser != null)
        {
            string firstName = ValidationHelper.GetString(editProfileForm.BasicForm.Data.GetValue("FirstName"), "");
            string lastName = ValidationHelper.GetString(editProfileForm.BasicForm.Data.GetValue("LastName"), "");

            // Change full name only if full name wasn't changed(=set) and first/last name is not empty
            if ((CMSContext.CurrentUser.FullName == String.Empty) && (CMSContext.CurrentUser.FullName == ValidationHelper.GetString(editProfileForm.BasicForm.Data.GetValue("FullName"), ""))
                && ((firstName != String.Empty) || (lastName != String.Empty)))
            {
                editProfileForm.BasicForm.Data.SetValue("FullName", (firstName + " " + lastName).Trim());
            }
        }

        // Ensure unique user email
        string email = ValidationHelper.GetString(editProfileForm.BasicForm.Data.GetValue("Email"), "").Trim();

        // Get current user info
        UserInfo ui = editProfileForm.Info as UserInfo;

        // Check if user email is unique in all sites where he belongs
        if (!UserInfoProvider.IsEmailUnique(email, ui))
        {
            lblError.Visible = true;
            lblError.Text = GetString("UserInfo.EmailAlreadyExist");
            editProfileForm.StopProcessing = true;
        }


        #region "Reserved names - nickname"

        // Don't check reserved names for global administrator
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            // Check for reserved user names like administrator, sysadmin, ...
            string nickName = ValidationHelper.GetString(editProfileForm.BasicForm.Data.GetValue("UserNickName"), "");

            if (UserInfoProvider.NameIsReserved(CMSContext.CurrentSiteName, nickName))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Webparts_Membership_RegistrationForm.UserNameReserved").Replace("%%name%%", HTMLHelper.HTMLEncode(nickName));
                editProfileForm.StopProcessing = true;
            }
        }

        #endregion

    }


    /// <summary>
    /// PreRender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        editProfileForm.BasicForm.SubmitButton.CssClass = "SubmitButton";
        ControlsHelper.RegisterPostbackControl(editProfileForm.BasicForm.SubmitButton);

        // Alter username according to GetFormattedUserName function
        if ((editProfileForm.BasicForm != null) && (editProfileForm.BasicForm.FieldEditingControls != null))
        {
            EditingFormControl userControl = editProfileForm.BasicForm.FieldEditingControls["UserName"] as EditingFormControl;
            if (userControl != null)
            {
                string userName = ValidationHelper.GetString(userControl.Value, String.Empty);

                // Set back formatted username
                userControl.Value = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(userName, IsLiveSite));
            }
        }
    }
}
