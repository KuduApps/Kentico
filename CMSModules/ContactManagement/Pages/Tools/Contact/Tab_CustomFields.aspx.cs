using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;

// Edited object
[EditedObject(OnlineMarketingObjectType.CONTACT, "contactId")]

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Tab_CustomFields : CMSContactManagementContactsPage
{
    private int siteId = 0;

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (EditedObject != null)
        {
            // Get edited contact object
            ContactInfo ci = (ContactInfo)EditedObject;
            siteId = ci.ContactSiteID;

            this.CheckReadPermission(siteId);
            // Initialize dataform
            formCustomFields.Info = ci;
            formCustomFields.BasicForm.HideSystemFields = true;
            formCustomFields.OnBeforeSave += formCustomFields_OnBeforeSave;
            formCustomFields.OnAfterSave += formCustomFields_OnAfterSave;
        }
        else
        {
            // Disable dataform
            formCustomFields.Enabled = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (formCustomFields.BasicForm != null)
        {
            // Set submit button's css class
            formCustomFields.BasicForm.SubmitButton.CssClass = "ContentButton";
        }
    }


    protected void formCustomFields_OnBeforeSave(object sender, EventArgs e)
    {
        // Check modify permissions
        ContactHelper.AuthorizedModifyContact(siteId, true);
    }


    protected void formCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        // Display 'changes saved' information
        ShowInformation(GetString("general.changessaved"));
    }
}