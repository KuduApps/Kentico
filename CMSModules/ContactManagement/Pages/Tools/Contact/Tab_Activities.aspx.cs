using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.WebAnalytics;
using CMS.SiteProvider;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Tab_Activities : CMSContactManagementContactsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CMSPage.EditedObject != null)
        {
            ContactInfo ci = (ContactInfo)CMSPage.EditedObject;

            // Check permission
            this.CheckReadPermission(ci.ContactSiteID);

            bool isGlobal = (ci.ContactSiteID == 0);
            bool isMerged = (ci.ContactMergedWithContactID > 0);

            // Show warning if activity logging is disabled
            string siteName = SiteInfoProvider.GetSiteName(ci.ContactSiteID);
            if (!ActivitySettingsHelper.OnlineMarketingEnabled(siteName))
            {
                lblDis.ResourceString = "om.onlinemarketing.disabled";
            }
            pnlDis.Visible = !isGlobal && !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName);

            // Show IP addresses if enabled
            fltElem.ShowIPFilter = ActivitySettingsHelper.IPLoggingEnabled(siteName);
            fltElem.ShowSiteFilter = this.IsSiteManager && isGlobal;
            listElem.ShowIPAddressColumn = fltElem.ShowIPFilter;
 

            // Restrict WHERE condition for activities of current site (if not in site manager)
            if (!this.IsSiteManager)
            {
                fltElem.SiteID = CMSContext.CurrentSiteID;
            }

            listElem.ContactID = ci.ContactID;
            listElem.IsMergedContact = isMerged;
            listElem.IsGlobalContact = isGlobal;

            fltElem.ShowContactSelector = isGlobal;
            listElem.ShowContactNameColumn = isGlobal;
            listElem.ShowSiteNameColumn = this.IsSiteManager && isGlobal;
            listElem.ShowRemoveButton = !isMerged && !isGlobal;
            listElem.OrderBy = "ActivityCreated DESC";
            listElem.WhereCondition = fltElem.WhereCondition;
        }
    }
}