using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.LicenseProvider;

// Edited object
[EditedObject("cms.class", "classid")]

public partial class CMSModules_SystemTables_Pages_Development_SystemTable_Edit_OnlineMarketing : SiteManagerPage
{
    CMSUserControl mapControl = null;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.ContactManagement);

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

        // Get data class info
        DataClassInfo classInfo = (DataClassInfo)EditedObject;
        if (classInfo == null)
        {
            return;
        }

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
    }


    /// <summary>
    /// Actions handler - saves the changes.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Update the class object
        DataClassInfo classInfo = (DataClassInfo)EditedObject;
        if ((classInfo != null) && (mapControl != null))
        {
            classInfo.ClassContactOverwriteEnabled = ValidationHelper.GetBoolean(mapControl.GetValue("allowoverwrite"), false);
            classInfo.ClassContactMapping = ValidationHelper.GetString(mapControl.GetValue("mappingdefinition"), string.Empty);
            DataClassInfoProvider.SetDataClass(classInfo);

            // Show save information
            ShowInformation(GetString("general.changessaved"));
        }
    }
}