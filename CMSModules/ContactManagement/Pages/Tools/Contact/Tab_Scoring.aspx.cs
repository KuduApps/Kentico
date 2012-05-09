using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;

[EditedObject(OnlineMarketingObjectType.CONTACT, "contactID")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Tab_Scoring : CMSScorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set up control
        ContactInfo ci = (ContactInfo)CMSPage.EditedObject;
        cScoring.ContactID = ci.ContactID;
        cScoring.SiteID = ci.ContactSiteID;
        cScoring.UniGrid.ZeroRowsText = GetString("om.score.notfound");
    }


    /// <summary>
    /// Check read permissions.
    /// </summary>
    protected override void CheckReadPermission()
    {
        // Check read permission for score or contact
        int siteID = ContactHelper.ObjectSiteID(EditedObject);
        CurrentUserInfo user = CMSContext.CurrentUser;
        if (!ContactHelper.AuthorizedReadContact(siteID, false) && !user.IsAuthorizedPerResource("CMS.Scoring", "Read"))
        {
            RedirectToCMSDeskAccessDenied("CMS.ContactManagement", "ReadContacts");
        }
    }
}