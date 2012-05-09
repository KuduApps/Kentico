using System;

using CMS.OnlineMarketing;
using CMS.UIControls;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACTSTATUS, "statusid")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "om.contactstatus.list", "~/CMSModules/ContactManagement/Pages/Tools/Configuration/ContactStatus/List.aspx", null)]
[Breadcrumb(1, ResourceString = "om.contactstatus.new", NewObject = true)]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}", ExistingObject = true)]

// Title
[Title(ImageUrl = "Objects/OM_ContactStatus/new.png", ResourceString = "om.contactstatus.new", HelpTopic = "onlinemarketing_contactstatus_new", NewObject = true)]
[Title(ImageUrl = "Objects/OM_ContactStatus/object.png", ResourceString = "om.contactstatus.edit", HelpTopic = "onlinemarketing_contactstatus_edit", ExistingObject = true)]

public partial class CMSModules_ContactManagement_Pages_Tools_Configuration_ContactStatus_Edit : CMSContactManagementContactStatusPage
{
    #region "Variables"

    private CurrentUserInfo currentUser;
    private ContactStatusInfo currentObject;
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
        currentObject = (ContactStatusInfo)EditedObject;

        // Check read permission
        currentObjectSiteId = currentObject != null ? currentObject.ContactStatusSiteID : siteID;
        this.CheckReadPermission(currentObjectSiteId);

        // Preserve site info passed in query        
        CurrentMaster.Title.Breadcrumbs[0, 1] = AddSiteQuery(CurrentMaster.Title.Breadcrumbs[0, 1], siteID);
        EditForm.RedirectUrlAfterSave = AddSiteQuery(EditForm.RedirectUrlAfterSave, siteID);

        // Set new site ID for new object
        if (EditedObject == null)
        {
            if ((siteID == UniSelector.US_GLOBAL_RECORD) && ModifyGlobalConfiguration)
            {
                EditForm.Data["ContactStatusSiteID"] = DBNull.Value;
            }
            else if (this.IsSiteManager && currentUser.UserSiteManagerAdmin)
            {
                EditForm.Data["ContactStatusSiteID"] = siteID;
            }
            else
            {
                EditForm.Data["ContactStatusSiteID"] = CMSContext.CurrentSiteID;
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