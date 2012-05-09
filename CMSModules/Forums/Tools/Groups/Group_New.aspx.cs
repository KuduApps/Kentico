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
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Tools_Groups_Group_New : CMSForumsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.groupNewElem.OnSaved += new EventHandler(groupNewElem_OnSaved);
        this.groupNewElem.IsLiveSite = false;
        this.InitializeMasterPage();
    }


    protected void groupNewElem_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Group_Frameset.aspx?groupId=" + groupNewElem.GroupID);
    }


    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        // Set title and help
        this.Title = "Forum - New group";
        this.CurrentMaster.Title.HelpTopicName = "new_forum_group";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title control		
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("Group_General.GroupList");
        tabs[0, 1] = "~/CMSModules/Forums/Tools/Groups/Group_List.aspx";
        tabs[0, 2] = "";
        tabs[1, 0] = GetString("Group_General.NewGroup");
        tabs[1, 1] = "";
        tabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = tabs;

        // Set title label
        this.CurrentMaster.Title.TitleText = GetString("Group_General.NewGroup");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Forums_ForumGroup/new.png");
    }
}
