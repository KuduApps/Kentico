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
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_SearchIndex_Content_List : SiteManagerPage
{
    // Index id
    private int indexId = QueryHelper.GetInteger("indexid", 0);
    private bool displayHeaderActions = true;
    private bool displayCustomTableActions = false;


    protected void Page_Load(object sender, EventArgs e)
    {
        string addAllowed = GetString("srch.index.addcontent");
        string addExcluded = GetString("srch.index.addexcluded");

        this.contentList.StopProcessing = true;
        this.forumList.StopProcessing = true;

        // Get search index info
        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(indexId);

        if (sii != null)
        {
            // Swirch by index type
            switch (sii.IndexType)
            {
                // Documents
                case PredefinedObjectType.DOCUMENT:
                case SearchHelper.DOCUMENTS_CRAWLER_INDEX:
                    contentList.StopProcessing = false;
                    contentList.Visible = true;
                    this.contentList.OnAction += new CommandEventHandler(contentList_OnAction);
                    break;

                // Forums
                case PredefinedObjectType.FORUM:
                    forumList.StopProcessing = false;
                    forumList.Visible = true;
                    this.forumList.OnAction += new CommandEventHandler(contentList_OnAction);

                    addAllowed = GetString("srch.index.addforum");
                    addExcluded = GetString("srch.index.addforumexcluded");
                    break;

                // Users
                case PredefinedObjectType.USER:
                    userList.StopProcessing = false;
                    userList.Visible = true;
                    displayHeaderActions = false;
                    break;

                // Custom tables
                case SettingsObjectType.CUSTOMTABLE:
                    customTableList.Visible = true;
                    customTableList.StopProcessing = false;
                    customTableList.OnAction += new CommandEventHandler(contentList_OnAction);
                    displayHeaderActions = false;
                    displayCustomTableActions = true;
                    break;

                // General index
                case SearchHelper.GENERALINDEX:
                    generalList.Visible = true;
                    generalList.StopProcessing = false;
                    displayHeaderActions = false;
                    break;

                // Custom search
                case SearchHelper.CUSTOM_SEARCH_INDEX:
                    customList.Visible = true;
                    customList.StopProcessing = false;
                    displayHeaderActions = false;
                    break;
            }
        }

        // Display header action if it is required
        if (displayHeaderActions)
        {
            // New item link
            string[,] actions = new string[2, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = addAllowed;
            actions[0, 2] = null;
            actions[0, 3] = ResolveUrl("SearchIndex_Content_Edit.aspx?indexid=" + indexId + "&itemtype=" + SearchIndexSettingsInfo.TYPE_ALLOWED);
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("CMSModules/CMS_SMartSearch/addcontent.png");
            actions[1, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[1, 1] = addExcluded;
            actions[1, 2] = null;
            actions[1, 3] = ResolveUrl("SearchIndex_Content_Edit.aspx?indexid=" + indexId + "&itemtype=" + SearchIndexSettingsInfo.TYPE_EXLUDED);
            actions[1, 4] = null;
            actions[1, 5] = GetImageUrl("CMSModules/CMS_SMartSearch/addexcludedcontent.png");
            this.CurrentMaster.HeaderActions.Actions = actions;
        }

        if (displayCustomTableActions)
        {
            // New item link
            string[,] actions = new string[1, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = GetString("srch.index.addcustomtable"); ;
            actions[0, 2] = null;
            actions[0, 3] = ResolveUrl("SearchIndex_Content_Edit.aspx?indexid=" + indexId + "&itemtype=" + SearchIndexSettingsInfo.TYPE_ALLOWED);
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("CMSModules/CMS_SMartSearch/addcontent.png");
            this.CurrentMaster.HeaderActions.Actions = actions;
        }
    }


    /// <summary>
    /// Action event handler.
    /// </summary>
    void contentList_OnAction(object sender, CommandEventArgs e)
    {
        URLHelper.Redirect("SearchIndex_Content_Edit.aspx?indexid=" + indexId + "&guid=" + e.CommandArgument);
    }
}
