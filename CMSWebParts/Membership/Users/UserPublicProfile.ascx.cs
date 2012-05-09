using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.FormControls;

public partial class CMSWebParts_Membership_Users_UserPublicProfile : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Name of the alternative form (ClassName.AlternativeFormName)
    /// Default value is CMS.User.DisplayProfile
    /// </summary>
    public string AlternativeFormName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AlternativeFormName"), "CMS.User.DisplayProfile");
        }
        set
        {
            this.SetValue("AlternativeFormName", value);
        }
    }


    /// <summary>
    /// User name whose profile should be displayed.
    /// </summary>
    public string UserName
    {
        get
        {
            string userName = ValidationHelper.GetString(GetValue("UserName"), String.Empty);
            if (userName != String.Empty)
            {
                return userName;
            }

            // Back compatibility
            int userID = ValidationHelper.GetInteger(GetValue("UserID"), 0);
            if (userID != 0)
            {
                if (userID == CMSContext.CurrentUser.UserID)
                {
                    return CMSContext.CurrentUser.UserName;
                }

                UserInfo ui = UserInfoProvider.GetUserInfo(userID);
                if (ui != null)
                {
                    return ui.UserName;
                }
            }

            return String.Empty;
        }  
        set
        {
            this.SetValue("UserName", value);
        }
    }


    /// <summary>
    /// User id whose profile should be displayed.
    /// </summary>
    [Obsolete ("Use UserName instead")]
    public int UserID
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("UserID"), 0);
        }
        set
        {
            this.SetValue("UserID", value);
        }
    }


    /// <summary>
    /// No profile text.
    /// </summary>
    public string NoProfileText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NoProfileText"), "");
        }
        set
        {
            this.SetValue("NoProfileText", value);
        }
    }


    /// <summary>
    /// Indicates if field visibility should be applied on user form.
    /// </summary>
    public bool ApplyVisibility
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ApplyVisibility"), false);
        }
        set
        {
            this.SetValue("ApplyVisibility", value);
        }
    }


    /// <summary>
    /// This name is used if ApplyVisibility is 'true' to get visibility definition of current user.
    /// </summary>
    public string VisibilityFormName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("VisibilityFormName"), "");
        }
        set
        {
            this.SetValue("VisibilityFormName", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Get user info
            UserInfo ui = GetUser();
            if (ui != null)
            {
                // Get alternative form info
                AlternativeFormInfo afi = AlternativeFormInfoProvider.GetAlternativeFormInfo(AlternativeFormName);
                if (afi != null)
                {
                    // Initialize data form
                    formElem.Visible = true;
                    formElem.Info = ui;
                    formElem.AlternativeFormFullName = this.AlternativeFormName;
                    formElem.IsLiveSite = true;
                    formElem.ApplyVisibility = this.ApplyVisibility;
                    formElem.VisibilityFormName = this.VisibilityFormName;
                    formElem.BasicForm.SubmitButton.Visible = false;
                }
                else
                {
                    lblError.Text = String.Format(GetString("altform.formdoesntexists"), AlternativeFormName);
                    lblError.Visible = true;
                    plcContent.Visible = false;
                }
            }
            else
            {
                // Hide data form
                formElem.Visible = false;
                lblNoProfile.Visible = true;
                lblNoProfile.Text = this.NoProfileText;
            }
        }
    }


    // Get user
    private UserInfo GetUser()
    {
        UserInfo ui = null;

        if (!String.IsNullOrEmpty(this.UserName))
        {
            ui = UserInfoProvider.GetUserInfo(this.UserName);
        }
        // Otherwise select current user
        else
        {
            ui = SiteContext.CurrentUser;
        }

        return ui;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Alter username according to GetFormattedUserName function
        if ((formElem.BasicForm != null) && (formElem.BasicForm.FieldEditingControls != null))
        {
            EditingFormControl userControl = formElem.BasicForm.FieldEditingControls["UserName"] as EditingFormControl;
            if (userControl != null)
            {
                string userName = ValidationHelper.GetString(userControl.Value, String.Empty);

                // Set back formatted username
                userControl.Value = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(userName, true));
            }
        }
    }
}
