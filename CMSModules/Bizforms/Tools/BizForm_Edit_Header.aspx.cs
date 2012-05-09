using System;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.LicenseProvider;

// Set edited object
[EditedObject("cms.form", "formid")]

// Set title
[Title("Objects/CMS_Form/object.png", "BizFormHeader.Title", "general_tab")]

public partial class CMSModules_BizForms_Tools_BizForm_Edit_Header : CMSBizFormPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string bizFormName = String.Empty;

        BizFormInfo bizForm = EditedObject as BizFormInfo;

        // Init tabs
        InitalizeMenu(bizForm.FormID);

        // Initializes breadcrumbs
        InitBreadcrumbs(2);
        SetBreadcrumb(0, GetString("BizForm.BizForms"), ResolveUrl("~/CMSModules/BizForms/Tools/BizForm_List.aspx"), "_parent", null);
        SetBreadcrumb(1, bizForm.FormDisplayName, null, null, null);

        if (CMSContext.CurrentUser.IsAuthorizedPerResource("cms.form", "ReadData"))
        {
            SetHelp("data_tab", "helpTopic");
        }
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitalizeMenu(int formId)
    {
        CurrentUserInfo user = CMSContext.CurrentUser;
        // Learn if new form was created
        bool newForm = QueryHelper.GetBoolean("newform", false);

        int i = 0;
        InitTabs(9, "BizFormContent");

        // Check 'ReadData' permission
        if (user.IsAuthorizedPerResource("cms.form", "ReadData"))
        {
            SetTab(i++, GetString("BizForm.Data"), "BizForm_Edit_Data.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'data_tab');");
        }

        // Check 'ReadForm' permission
        if (user.IsAuthorizedPerResource("cms.form", "ReadForm"))
        {
            SetTab(i++, GetString("general.general"), "BizForm_Edit_General.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'general_tab');");
            SetTab(i++, GetString("general.fields"), "BizForm_Edit_Fields.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'fields_tab');");
            SetTab(i++, GetString("general.form"), "BizForm_Edit_Layout.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'form_tab2');");
            SetTab(i++, GetString("biz.notificationemail"), "BizForm_Edit_NotificationEmail.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'notification_mail_tab');");
            SetTab(i++, GetString("biz.autoresponder"), "BizForm_Edit_Autoresponder.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'autoresponder_tab');");
            SetTab(i++, GetString("general.security"), "BizForm_Edit_Security.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'security_tab2');");
            SetTab(i++, GetString("biz.header.altforms"), "AlternativeForms/AlternativeForms_List.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'alternative_forms');");
            if (ModuleEntry.IsModuleLoaded(ModuleEntry.ONLINEMARKETING) && (user.IsAuthorizedPerResource("CMS.ContactManagement", "ModifyContacts")) && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ContactManagement))
            {
                SetTab(i++, GetString("onlinemarketing.title"), "BizForm_Edit_OnlineMarketing.aspx?formid=" + formId, "SetHelpTopic('helpTopic', 'onlinemarketing_tab');");
            }
        }

        // Select second (General) tab if new form was created
        if (newForm)
        {
            this.CurrentMaster.Tabs.SelectedTab = 1;
        }
    }
}
