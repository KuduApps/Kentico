using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.LicenseProvider;

public partial class CMSModules_SystemTables_Pages_Development_SystemTable_Header : SiteManagerPage
{
    DataClassInfo dci = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        int classId = QueryHelper.GetInteger("classid", 0);

        if (classId > 0)
        {
            dci = DataClassInfoProvider.GetDataClass(classId);
        }

        // Initializes page title
        string[,] breadcrumbs = new string[2, 3];

        breadcrumbs[0, 0] = GetString("systbl.header.listlink");
        breadcrumbs[0, 1] = "~/CMSModules/SystemTables/Pages/Development/SystemTable/List.aspx";
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[1, 0] = (dci != null) ? dci.ClassDisplayName : String.Empty;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpTopicName = "system_tables_fields";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        if (!RequestHelper.IsPostBack())
        {
            // Initialize menu
            InitializeMenu(classId);
        }
    }


    /// <summary>
    /// Initializes edit header.
    /// </summary>
    /// <param name="docTypeId">DocumentTypeID</param>
    protected void InitializeMenu(int objId)
    {
        string[,] tabs = new string[5, 4];
        tabs[0, 0] = GetString("general.fields");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'system_tables_fields');";
        tabs[0, 2] = "Edit_Fields.aspx?classid=" + objId;
        tabs[1, 0] = GetString("systbl.header.query");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'system_tables_queries');";
        tabs[1, 2] = "Edit_Query_List.aspx?classid=" + objId;
        tabs[2, 0] = GetString("systbl.header.altforms");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'alternative_forms');";
        tabs[2, 2] = ResolveUrl("~/CMSModules/SystemTables/Pages/Development/AlternativeForms/List.aspx?classid=" + objId);

        if (dci != null)
        {
            switch (dci.ClassName.ToLower())
            {
                case "ecommerce.sku":
                    tabs[3, 0] = GetString("systbl.header.search");
                    tabs[3, 1] = "SetHelpTopic('helpTopic', 'system_tables_search_fields_tab');";
                    tabs[3, 2] = URLHelper.EncodeQueryString("Edit_Search.aspx?classname=cms.document");
                    break;

                case "cms.user":
                    tabs[3, 0] = GetString("systbl.header.search");
                    tabs[3, 1] = "SetHelpTopic('helpTopic', 'system_tables_search_full_fields_tab');";
                    tabs[3, 2] = URLHelper.EncodeQueryString("Edit_SearchFields.aspx?classname=cms.user");
                    if (ModuleEntry.IsModuleLoaded(ModuleEntry.ONLINEMARKETING) && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ContactManagement))
                    {
                        tabs[4, 0] = GetString("onlinemarketing.title");
                        tabs[4, 1] = "SetHelpTopic('helpTopic', 'system_tables_onlinemarketing');";
                        tabs[4, 2] = "Edit_OnlineMarketing.aspx?classid=" + objId;
                    }
                    break;

                case "newsletter.subscriber":
                case "ecommerce.customer":
                    if (ModuleEntry.IsModuleLoaded(ModuleEntry.ONLINEMARKETING) && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ContactManagement))
                    {
                        tabs[3, 0] = GetString("onlinemarketing.title");
                        tabs[3, 1] = "SetHelpTopic('helpTopic', 'system_tables_onlinemarketing');";
                        tabs[3, 2] = "Edit_OnlineMarketing.aspx?classid=" + objId;
                    }
                    break;
            }
        }
        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "content";
    }
}
