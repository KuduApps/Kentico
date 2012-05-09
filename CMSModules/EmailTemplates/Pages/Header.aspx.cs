using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.EmailEngine;
using CMS.UIControls;
using CMS.CMSHelper;

// Set title
[Title("Objects/CMS_EmailTemplate/object.png", "EmailTemplate_Edit.Title", "newedit_e_mail_template")]

// Set edited object
[EditedObject("cms.emailtemplate", "templateid")]

// Set Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(1, Text = "{%EditedObject.EmailTemplateDisplayName%}")]

// Set tabs
[Tabs(1, "content")]
[Tab(0, "general.general", "Edit.aspx?templateid={%EditedObject.EmailTemplateID%}&tabmode=1", "SetHelpTopic('helpTopic', 'newedit_e_mail_template')")]

public partial class CMSModules_EmailTemplates_Pages_Header : CMSAdministrationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "Modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EmailTemplates", "Modify"))
        {
            RedirectToAccessDenied("CMS.EmailTemplates", "Modify");
        }

        // Prepare site query
        string siteQuery = null;
        if (SiteID > 0)
        {
            siteQuery = "?siteid=" + SiteID;
        }

        if (SelectedSiteID > 0)
        {
            siteQuery = "?selectedsiteid=" + SelectedSiteID;
        }

        EmailTemplateInfo templateInfo = (EmailTemplateInfo)EditedObject;
        // Check that edited template belongs to selected site
        if ((SiteID > 0) && (templateInfo != null) && (templateInfo.TemplateSiteID != SiteID))
        {
            RedirectToAccessDenied(null);
        }

        SetBreadcrumb(0, GetString("EmailTemplate_Edit.EmailTemplates"), ResolveUrl("List.aspx" + siteQuery), "_parent", null);
    }
}
