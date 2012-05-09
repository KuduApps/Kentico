using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.LicenseProvider;

// Edited object
[EditedObject("cms.form", "formid")]

public partial class CMSModules_BizForms_Tools_BizForm_Edit_OnlineMarketing : CMSBizFormPage
{
    CMSUserControl mapControl = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.ContactManagement);

        // Check if current user is authorised to read either site or global contacts
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ContactManagement", "ModifyContacts"))
        {
            RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ModifyContacts");
        }

        // Init header actions
        string[,] actions = new string[1, 11];
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";
        CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        CurrentMaster.HeaderActions.Actions = actions;

        // Get form info
        BizFormInfo formInfo = (BizFormInfo)EditedObject;
        if (formInfo == null)
        {
            return;
        }

        // Get class of the form
        DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(formInfo.FormClassID);

        // Load mapping dialog control and initialize it
        plcMapping.Controls.Clear();
        mapControl = (CMSUserControl)Page.LoadControl("~/CMSModules/ContactManagement/Controls/UI/Contact/MappingDialog.ascx");
        if (mapControl != null)
        {
            mapControl.ID = "ctrlMapping";
            mapControl.SetValue("classname", classInfo.ClassName);
            mapControl.SetValue("allowoverwrite", classInfo.ClassContactOverwriteEnabled);
            plcMapping.Controls.Add(mapControl);
        }

        if (!RequestHelper.IsPostBack())
        {
            // Initialize checkbox value and mapping dialog visibility
            plcMapping.Visible = chkLogActivity.Checked = formInfo.FormLogActivity;
        }
    }


    /// <summary>
    /// Checkbox checked changed event handler.
    /// </summary>
    protected void chkLogActivity_CheckedChanged(object sender, EventArgs e)
    {
        // Show/hide mapping dialog
        plcMapping.Visible = chkLogActivity.Checked;
    }


    /// <summary>
    /// Actions handler - saves the changes.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Update the form object and its class
        BizFormInfo form = (BizFormInfo)EditedObject;
        if ((form != null) && (mapControl != null))
        {
            if (plcMapping.Visible)
            {
                // Update mapping of the form class only if mapping dialog is visible
                DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(form.FormClassID);
                if (classInfo != null)
                {
                    classInfo.ClassContactOverwriteEnabled = ValidationHelper.GetBoolean(mapControl.GetValue("allowoverwrite"), false);
                    classInfo.ClassContactMapping = ValidationHelper.GetString(mapControl.GetValue("mappingdefinition"), string.Empty);
                    DataClassInfoProvider.SetDataClass(classInfo);
                }
            }

            // Update the form
            form.FormLogActivity = chkLogActivity.Checked;
            BizFormInfoProvider.SetBizFormInfo(form);

            // Show save information
            ShowInformation(GetString("general.changessaved"));
        }
    }
}