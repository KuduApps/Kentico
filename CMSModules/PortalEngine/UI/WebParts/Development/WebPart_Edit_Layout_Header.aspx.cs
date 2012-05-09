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
[EditedObject("cms.webpartlayout", "layoutid")]

// Add breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "WebParts.Layout", "WebPart_Edit_Layout.aspx?webpartid={%EditedObject.WebPartLayoutWebPartID%}", "_parent")]
[Breadcrumb(1, Text = "{%EditedObject.WebPartLayoutDisplayName%}")]

// Set page title
[Help("newedit_webpart_layout", "helpTopic")]

// Set tabs number and ensure additional tab
[Tabs(2, "Content")]
[Tab(0, "general.general", "WebPart_Edit_Layout_Edit.aspx?layoutid={%EditedObject.WebPartLayoutID%}&webpartid={%EditedObject.WebPartLayoutWebPartID%}", "SetHelpTopic('helpTopic', 'newedit_webpart_layout');")]
public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Layout_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StorageHelper.IsExternalStorage)
        {
            SetTab(1, GetString("stylesheet.theme"), "WebPart_Edit_Layout_Theme.aspx" + URLHelper.Url.Query, "SetHelpTopic('helpTopic', 'webpart_layout_theme');");
        }
    }
}
