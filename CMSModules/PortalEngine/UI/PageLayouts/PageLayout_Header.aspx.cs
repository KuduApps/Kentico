using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.IO;

// Set edited object
[EditedObject("cms.layout", "layoutid")]

// Add breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "Administration-PageLayout_New.NewLayout", "PageLayout_List.aspx", "_parent")]
[Breadcrumb(1, Text = "{%EditedObject.LayoutDisplayName%}")]

// Set page title
[Title("Objects/CMS_Layout/object.png", "Administration-PageLayout_New.EditLayout", "newedit_page_layout")]

// Set tabs number and ensure additional tab
[Tabs(2, "pl_edit_content")]
[Tab(0, "general.general", "~/CMSModules/PortalEngine/UI/PageLayouts/PageLayout_Edit.aspx?layoutid={%EditedObject.LayoutID%}", "SetHelpTopic('helpTopic', 'newedit_page_layout');")]

public partial class CMSModules_PortalEngine_UI_PageLayouts_PageLayout_Header : SiteManagerPage
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StorageHelper.IsExternalStorage)
        {
            SetTab(1, GetString("Stylesheet.Theme"), ResolveUrl("~/CMSModules/PortalEngine/UI/PageLayouts/PageLayout_Edit_Theme.aspx") + URLHelper.Url.Query, "SetHelpTopic('helpTopic', 'pagelayout_theme_tab');");
        }
    }
}
