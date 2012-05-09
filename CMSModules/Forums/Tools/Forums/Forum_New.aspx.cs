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

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Tools_Forums_Forum_New : CMSForumsPage
{
    private int groupId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        groupId = QueryHelper.GetInteger("groupid", 0);

        ForumContext.CheckSite(groupId, 0, 0);
   
        this.forumNew.GroupID = groupId;
        this.forumNew.OnSaved += new EventHandler(forumNew_OnSaved);
        this.forumNew.IsLiveSite = false;

        InitializeMasterPage();
    }

    
    protected void forumNew_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Forum_Frameset.aspx?forumid=" + this.forumNew.ForumID);
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        // Initialize help 
        this.Title = "Forums - New forum";
        this.CurrentMaster.Title.HelpTopicName = "add_forum";
        this.CurrentMaster.Title.HelpName = "helpTopic";        

        // Initialize page breadcrumbs
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("forum_list.headercaption");
        tabs[0, 1] = "~/CMSModules/Forums/Tools/Forums/Forum_List.aspx?groupid=" + groupId;
        tabs[0, 2] = "";
        tabs[1, 0] = GetString("Forum_Edit.NewForum");
        tabs[1, 1] = "";
        tabs[1, 2] = "";
        
        this.CurrentMaster.Title.Breadcrumbs = tabs;
    }
}
