using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;

// Set edited object
[EditedObject("cms.query", "queryid")]

// Add breadcrumbs
[Breadcrumbs(2)]

// Set page help
[Help("customtable_edit_newedit_query", "helpTopic")]

// Set tabs number
[Tabs(1, "ctq_edit_content")]

public partial class CMSModules_CustomTables_CustomTable_Edit_Query_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Query qi = EditedObject as Query;

        // Set breadcrumbs
        SetBreadcrumb(0, GetString("general.queries"), ResolveUrl("CustomTable_Edit_Query_List.aspx?customtableid=" + qi.QueryClassId), "_parent", null);
        SetBreadcrumb(1, qi.QueryName, null, null, null);

        // Set general tab
        SetTab(0, GetString("general.general"), URLHelper.RemoveParameterFromUrl("CustomTable_Edit_Query_Edit.aspx" + URLHelper.Url.Query, "saved"), "SetHelpTopic('helpTopic', 'customtable_edit_newedit_query');");
    }
}
