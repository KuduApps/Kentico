using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_SystemTables_Pages_Development_Views_ViewEdit_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            return;
        }

        // Init edit area
        string objName = QueryHelper.GetString("objname", null);
        int objType = QueryHelper.GetInteger("objtype", -1);

        // Initializes page title
        string[,] breadcrumbs = new string[2, 3];

        breadcrumbs[0, 0] = GetString("systbl.views.header.listlink");
        breadcrumbs[0, 1] = "~/CMSModules/SystemTables/Pages/Development/Views/Views_List.aspx";
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[1, 0] = objName;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "systemtables_views_edit";

        if (!RequestHelper.IsPostBack())
        {
            // Initialize menu
            InitializeMenu();
        }
    }


    /// <summary>
    /// Initializes edit header.
    /// </summary>
    /// <param name="docTypeId">DocumentTypeID</param>
    protected void InitializeMenu()
    {
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'systemtables_views_edit');";
        tabs[0, 2] = "View_Edit.aspx" + URLHelper.Url.Query;
        tabs[1, 0] = GetString("general.preview");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'systemtables_views_preview');";
        tabs[1, 2] = "ViewEdit_Preview.aspx" + URLHelper.Url.Query;

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "viewscontent";
    }
}
