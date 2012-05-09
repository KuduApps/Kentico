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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_WebFarm_Pages_WebFarm_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set the page title
        this.CurrentMaster.Title.TitleText = GetString("webfarm_header.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebfarmServer/object.png");
        this.CurrentMaster.Title.HelpTopicName = "webfarm_server_list";
        this.CurrentMaster.Title.HelpName = "title";

        string[,] tabs = new string[3, 4];

        tabs[0, 0] = GetString("WebFarm_Header.Servers");
        tabs[0, 1] = "SetHelpTopic('title', 'webfarm_server_list');";
        tabs[0, 2] = "WebFarm_Server_List.aspx";
        tabs[1, 0] = GetString("WebFarm_Header.Tasks");
        tabs[1, 1] = "SetHelpTopic('title', 'webfarm_tasks');";
        tabs[1, 2] = "WebFarm_Task_List.aspx";
        tabs[2, 0] = GetString("WebFarm_Header.AnonymousTasks");
        tabs[2, 1] = "SetHelpTopic('title', 'webfarm_anonymous_tasks');";
        tabs[2, 2] = "WebFarm_AnonymousTask_List.aspx";
        
        CurrentMaster.Tabs.UrlTarget = "content";
        CurrentMaster.Tabs.Tabs = tabs;
    }
}
