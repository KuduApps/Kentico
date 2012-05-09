using System;

using CMS.OnlineMarketing;
using CMS.UIControls;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACTROLE, "roleid")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "om.contactrole.list", "~/CMSModules/ContactManagement/Pages/Tools/Configuration/ContactRole/List.aspx", null)]
[Breadcrumb(1, ResourceString = "om.contactrole.new", NewObject = true)]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}", ExistingObject = true)]

// Title
[Title(ImageUrl = "Objects/OM_ContactRole/new.png", ResourceString = "om.contactrole.new", HelpTopic = "onlinemarketing_contactrole_new", NewObject = true)]
[Title(ImageUrl = "Objects/OM_ContactRole/object.png", ResourceString = "om.contactrole.edit", HelpTopic = "onlinemarketing_contactrole_edit", ExistingObject = true)]

public partial class CMSModules_ContactManagement_Pages_Tools_Configuration_ContactRole_Edit : CMSContactManagementContactRolePage
{
    #region "Variables"

    private ContactRoleInfo currentObject;
    private CurrentUserInfo currentUser;
    private int siteID;
    private int currentObjectSiteId = CMSContext.CurrentSiteID;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        siteID = QueryHelper.GetInteger("siteid", CMSContext.CurrentSiteID);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = CMSContext.CurrentUser;
        currentObject = (ContactRoleInfo)EditedObject;

        // Check read permission
        currentObjectSiteId = currentObject != null ? currentObject.ContactRoleSiteID : siteID;
        this.CheckReadPermission(currentObjectSiteId);

        // Preserve site info passed in query        
        CurrentMaster.Title.Breadcrumbs[0, 1] = AddSiteQuery(CurrentMaster.Title.Breadcrumbs[0, 1], siteID);
        EditForm.RedirectUrlAfterSave = AddSiteQuery(EditForm.RedirectUrlAfterSave, siteID);

        // Set new site ID for new object
        if (EditedObject == null)
        {
            if ((siteID == UniSelector.US_GLOBAL_RECORD) && ModifyGlobalConfiguration)
            {
                EditForm.Data["ContactRoleSiteID"] = DBNull.Value;
            }
            else if (this.IsSiteManager && currentUser.UserSiteManagerAdmin)
            {
                EditForm.Data["ContactRoleSiteID"] = siteID;
            }
            else
            {
                EditForm.Data["ContactRoleSiteID"] = CMSContext.CurrentSiteID;
            }
        }
    }


    /// <summary>
    /// OnBeforeSave event handler.
    /// </summary>
    private void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        // Check permissions
        ConfigurationHelper.AuthorizedModifyConfiguration(currentObjectSiteId, true);
    }

    #endregion
}