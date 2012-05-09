using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Tab_IPs : CMSContactManagementContactsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ContactInfo ci = (ContactInfo)CMSPage.EditedObject;

        // Check permission read
        int siteID = ContactHelper.ObjectSiteID(EditedObject);
        this.CheckReadPermission(siteID);

        bool globalContact = (ci.ContactSiteID == 0);
        bool mergedContact = (ci.ContactMergedWithContactID > 0);

        fltElem.ShowContactNameFilter = globalContact;
        fltElem.ShowSiteFilter = this.IsSiteManager && globalContact;

        listElem.ShowContactNameColumn = globalContact;
        listElem.ShowSiteNameColumn = this.IsSiteManager && globalContact;
        listElem.ShowRemoveButton = !mergedContact && !globalContact && ContactHelper.AuthorizedModifyContact(ci.ContactSiteID, false);
        listElem.IsGlobalContact = globalContact;
        listElem.IsMergedContact = mergedContact;
        listElem.ContactID = ci.ContactID;

        // Restrict site IDs in CMSDesk
        if (!IsSiteManager)
        {
            listElem.WhereCondition = SqlHelperClass.AddWhereCondition(fltElem.WhereCondition, "ContactSiteID = " + CMSContext.CurrentSiteID);
        }
        else
        {
            listElem.WhereCondition = fltElem.WhereCondition;
        }
    }
}