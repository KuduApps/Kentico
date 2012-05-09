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
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_SearchIndex_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("srch.index.newindex");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("SearchIndex_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_SearchIndex/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;
        
        // Set the page title
        this.CurrentMaster.Title.TitleText = GetString("srch.index.indexes");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_SearchIndex/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "searchindex_list";

        this.indexListElem.OnEdit += new EventHandler(indexListElem_OnEdit);
    }


    /// <summary>
    /// Item selected event handler.
    /// </summary>
    void indexListElem_OnEdit(object sender, EventArgs e)
    {
        URLHelper.Redirect("SearchIndex_Frameset.aspx?indexId=" + indexListElem.SelectedItemID);
    }
}
