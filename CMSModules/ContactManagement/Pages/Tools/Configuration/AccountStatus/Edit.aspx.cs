using System;

using CMS.OnlineMarketing;
using CMS.UIControls;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;

// Edited object
[EditedObject(OnlineMarketingObjectType.ACCOUNTSTATUS, "statusid")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "om.accountstatus.list", "~/CMSModules/ContactManagement/Pages/Tools/Configuration/AccountStatus/List.aspx", null)]
[Breadcrumb(1, ResourceString = "om.accountstatus.new", NewObject = true)]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}", ExistingObject = true)]

// Title
[Title(ImageUrl = "Objects/OM_AccountStatus/new.png", ResourceString = "om.accountstatus.new", HelpTopic = "onlinemarketing_accountstatus_new", NewObject = true)]
[Title(ImageUrl = "Objects/OM_AccountStatus/object.png", ResourceString = "om.accountstatus.edit", HelpTopic = "onlinemarketing_accountstatus_edit", ExistingObject = true)]

public partial class CMSModules_ContactManagement_Pages_Tools_Configuration_AccountStatus_Edit : CMSContactManagementAccountStatusPage
{
    #region "Variables"

    private CurrentUserInfo currentUser;
    private AccountStatusInfo currentObject;
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
        currentObject = (AccountStatusInfo)EditedObject;

        currentObjectSiteId = currentObject != null ? currentObject.AccountStatusSiteID : siteID;
        this.CheckReadPermission(currentObjectSiteId);

        // Preserve site info passed in query        
        CurrentMaster.Title.Breadcrumbs[0, 1] = AddSiteQuery(CurrentMaster.Title.Breadcrumbs[0, 1], siteID);
        EditForm.RedirectUrlAfterSave = AddSiteQuery(EditForm.RedirectUrlAfterSave, siteID);
        
        // Set new site ID for new object
        if (EditedObject == null)
        {
            if ((siteID == UniSelector.US_GLOBAL_RECORD) && ModifyGlobalConfiguration)
            {
                EditForm.Data["AccountStatusSiteID"] = DBNull.Value;
            }
            else if (this.IsSiteManager && currentUser.UserSiteManagerAdmin)
            {
                EditForm.Data["AccountStatusSiteID"] = siteID;
            }
            else
            {
                EditForm.Data["AccountStatusSiteID"] = CMSContext.CurrentSiteID;
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