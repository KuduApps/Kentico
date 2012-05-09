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

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;


public partial class CMSModules_SmartSearch_SearchIndex_Header : SiteManagerPage
{
    private int indexId = 0;
    private SearchIndexInfo index = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "read" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.SearchIndex", "Read"))
        {
            RedirectToAccessDenied("CMS.SearchIndex", "Read");
        }

        indexId = QueryHelper.GetInteger("indexId", 0);

        string indexListUr = "~/CMSModules/SmartSearch/SearchIndex_List.aspx";


        string currentIndex = "";
        index = SearchIndexInfoProvider.GetSearchIndexInfo(indexId);
        if (index != null)
        {
            currentIndex = index.IndexDisplayName;
        }

        // Initialize PageTitle breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("srch.index.indexes");
        pageTitleTabs[0, 1] = indexListUr;
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentIndex;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.Title.TitleText = GetString("srch.index.indexes");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_SearchIndex/object.png");
        CurrentMaster.Title.HelpTopicName = "searchindex_general";
        CurrentMaster.Title.HelpName = "title";

        // Tabs
        InitalizeTabs();
    }


    /// <summary>
    /// Initializes tabs.
    /// </summary>
    protected void InitalizeTabs()
    {
        string indexType = PredefinedObjectType.DOCUMENT;
        if (index != null)
        {
            indexType = index.IndexType;
        }
        int i = 0;
        string[,] tabs = new string[5, 4];

        tabs[i, 0] = GetString("general.general");
        tabs[i, 1] = "SetHelpTopic('title', 'searchindex_general');";
        tabs[i, 2] = "SearchIndex_General.aspx?indexId=" + indexId;
        i++;

        tabs[i, 0] = GetString("general.index");

        switch (indexType)
        {
            case PredefinedObjectType.USER:
                tabs[i, 1] = "SetHelpTopic('title', 'searchindex_user_index');";
                break;

            case SettingsObjectType.CUSTOMTABLE:
                tabs[i, 1] = "SetHelpTopic('title', 'searchindex_customtable_index');";
                break;

            case SearchHelper.GENERALINDEX:
                tabs[i, 1] = "SetHelpTopic('title', 'searchindex_genera_index');";
                break;

            case SearchHelper.CUSTOM_SEARCH_INDEX:
                tabs[i, 1] = "SetHelpTopic('title', 'searchindex_custom_index');";
                break;

            case PredefinedObjectType.FORUM:
                tabs[i, 1] = "SetHelpTopic('title', 'searchindex_forum_index');";
                break;

            default:
                tabs[i, 1] = "SetHelpTopic('title', 'searchindex_index');";
                break;
        }
       
        tabs[i, 2] = "SearchIndex_Content_List.aspx?indexId=" + indexId;
        i++;

        tabs[i, 0] = GetString("general.sites");
        tabs[i, 1] = "SetHelpTopic('title', 'searchindex_sites');";
        tabs[i, 2] = "SearchIndex_Sites.aspx?indexId=" + indexId;
        i++;

        if (indexType == SearchHelper.GENERALINDEX)
        {
            tabs[i, 0] = GetString("srch.searchFields");
            tabs[i, 1] = "SetHelpTopic('title', 'searchindex_fields');";
            tabs[i, 2] = "SearchIndex_Fields.aspx?indexId=" + indexId;
            i++;
        }

        if ((indexType == PredefinedObjectType.DOCUMENT) || (indexType == SearchHelper.DOCUMENTS_CRAWLER_INDEX))
        {
            tabs[i, 0] = GetString("general.cultures");
            tabs[i, 1] = "SetHelpTopic('title', 'searchindex_cultures');";
            tabs[i, 2] = "SearchIndex_Cultures.aspx?indexId=" + indexId;
            i++;
        }

        // Search example
        tabs[i, 0] = GetString("srch.searchTab");
        tabs[i, 1] = "SetHelpTopic('title', 'searchindex_search');";
        tabs[i, 2] = "SearchIndex_search.aspx?indexId=" + indexId;

        CurrentMaster.Tabs.UrlTarget = "content";
        CurrentMaster.Tabs.Tabs = tabs;
    }
}
