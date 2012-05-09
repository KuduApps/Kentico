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
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Group_New : CMSGroupPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.TitleText = GetString("Group.NewHeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Community_Group/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_group";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Pagetitle breadcrumbs		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Group.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Groups/Tools/Group_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = GetString("Group.NewItemCaption");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        if (CMSContext.CurrentSite != null)
        {
            this.groupEditElem.SiteID = CMSContext.CurrentSite.SiteID;
        }

        this.groupEditElem.OnSaved += new EventHandler(groupEditElem_OnSaved);
        this.groupEditElem.DisplayAdvanceOptions = true;
        this.groupEditElem.IsLiveSite = false;
    }

    void groupEditElem_OnSaved(object sender, EventArgs e)
    {
        // Redirect to edit page
        URLHelper.Redirect("~/CMSModules/Groups/Tools/Group_Edit.aspx?groupId=" + this.groupEditElem.GroupID);
    }
}
