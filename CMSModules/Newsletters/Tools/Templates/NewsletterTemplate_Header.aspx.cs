using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;

// Set edited object
[EditedObject("newsletter.emailtemplate", "templateid")]

// Set breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "NewsletterTemplate_Edit.ItemListLink", "~/CMSModules/Newsletters/Tools/Templates/NewsletterTemplate_List.aspx", "_parent")]
[Breadcrumb(1, Text = "{%EditedObject.TemplateDisplayName%}")]

// Set help
[Help("newsletter_edit", "helpTopic")]

// Set tabs
[Tabs(1, "Content")]
[Tab(0, "general.general", "NewsletterTemplate_Edit.aspx?templateid={%EditedObject.TemplateID%}&tabmode={?tabmode?}", "SetHelpTopic('helpTopic','newsletter_edit')")]

public partial class CMSModules_Newsletters_Tools_Templates_NewsletterTemplate_Header : CMSNewsletterTemplatesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
