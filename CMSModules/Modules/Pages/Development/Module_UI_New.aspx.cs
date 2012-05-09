using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Modules_Pages_Development_Module_UI_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("resource.ui.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_UIProfiles/module.png");

        this.CurrentMaster.Title.HelpName = "helpTopic";

        int parentId = QueryHelper.GetInteger("parentId", 0);
        int moduleId = QueryHelper.GetInteger("moduleid", 0);

        if (parentId > 0)
        {
            this.editElem.ParentID = parentId;
            this.editElem.ResourceID = moduleId;
            this.CurrentMaster.Title.HelpTopicName = "new_ui_profile";

            // Setup breadcrumbs
            string[,] pageTitleTabs = new string[2, 3];
            pageTitleTabs[0, 0] = GetString("resource.ui.element");
            pageTitleTabs[0, 1] = "~/CMSModules/Modules/Pages/Development/Module_UI_Frameset.aspx?moduleid=" + moduleId;
            pageTitleTabs[0, 2] = "content";
            pageTitleTabs[1, 0] = GetString("resource.ui.newelement");
            pageTitleTabs[1, 1] = "";
            pageTitleTabs[1, 2] = "";
            this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        }
        else
        {
            this.editElem.Visible = false;
            this.lblInfo.Visible = true;
            this.lblInfo.Text = GetString("resource.ui.rootelement");
            this.CurrentMaster.Title.HelpTopicName = "root_ui_profile";
        }

    }
}
