using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Users_User_Edit_CustomFields : CMSUsersPage
{
    protected int userId;


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get user ID from query string
        userId = ValidationHelper.GetInteger(Request.QueryString["userID"], 0);

        if (userId > 0)
        {            
            // Check that only global administrator can edit global administrator's accouns
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            CheckUserAvaibleOnSite(ui); 
            EditedObject = ui;

            if (!CheckGlobalAdminEdit(ui))
            {
                plcUserCustomFields.Visible = false;
                plcUserSettingsCustomFields.Visible = false;
                lblError.Visible = true;
                return;
            }
        }

        // Setup user info for user custom fields dataform
        formUserCustomFields.Info = UserInfoProvider.GetUserInfo(userId);        

        // If table has not any custom field hide custom field placeholder
        if ((formUserCustomFields.Info == null) || (formUserCustomFields.BasicForm.FormInformation.GetFormElements(true, false, true).Count <= 0))
        {
            plcUserCustomFields.Visible = false;
        }
        else
        {
            // Setup the User DataForm
            formUserCustomFields.BasicForm.HideSystemFields = true;
            formUserCustomFields.BasicForm.CssClass = "ContentDataFormButton";
            formUserCustomFields.BasicForm.SubmitButton.Visible = false;
            formUserCustomFields.OnAfterSave += formCustomFields_OnAfterSave;
            formUserCustomFields.OnBeforeSave += formFields_OnBeforeSave;
        }

        // Setup user settings info for user settings custom fields dataform
        formUserSettingsCustomFields.Info = UserSettingsInfoProvider.GetUserSettingsInfoByUser(userId);

        if ((formUserSettingsCustomFields.Info == null) || (formUserSettingsCustomFields.BasicForm.FormInformation.GetFormElements(true, false, true).Count <= 0))
        {
            plcUserSettingsCustomFields.Visible = false;

            // If user settings has no custom fields show OK button of custom user dataform
            if (plcUserCustomFields.Visible)
            {
                formUserCustomFields.BasicForm.SubmitButton.Visible = true;
            }
        }
        else
        {
            // Setup the UserSettings DataForm
            formUserSettingsCustomFields.OnAfterSave += formCustomFields_OnAfterSave;
            formUserSettingsCustomFields.OnBeforeSave += formCustomFields_OnBeforeSave;
            formUserSettingsCustomFields.BasicForm.HideSystemFields = true;
            formUserSettingsCustomFields.BasicForm.CssClass = "ContentDataFormButton";
        }
    }


    /// <summary>
    /// Custom fields before save.
    /// </summary>
    protected void formFields_OnBeforeSave(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (formUserCustomFields.BasicForm != null)
        {
            formUserCustomFields.BasicForm.SubmitButton.CssClass = "SubmitButton";
        }
        if (formUserSettingsCustomFields.BasicForm != null)
        {
            formUserSettingsCustomFields.BasicForm.SubmitButton.CssClass = "SubmitButton";
        }
    }


    /// <summary>
    /// UserSettings dataform OnBeforeSave.
    /// </summary>
    void formCustomFields_OnBeforeSave(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        // If user plcUserCustomFields some user custom fields are present - save them too.
        if (plcUserCustomFields.Visible)
        {
            formUserCustomFields.Save();
        }
    }


    /// <summary>
    /// On after save (called from both dataforms).
    /// </summary>
    void formCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        this.lblInfo.Visible = true;
    }
}
