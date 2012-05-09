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
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Polls_Polls_New : CMSGroupPollsPage
{
    protected int groupId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get GroupID
        groupId = QueryHelper.GetInteger("groupid", 0);

        CheckGroupPermissions(groupId, "Read");

        // Breadcrumbs		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("group.polls.title");
        breadcrumbs[0, 1] = "~/CMSModules/Groups/Tools/Polls/Polls_List.aspx?groupId=" + groupId;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("polls_new.newitemcaption");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpTopicName = "new_poll";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // PollNew control initialization
        PollNew.OnSaved += new EventHandler(PollNew_OnSaved);
        PollNew.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(PollNew_OnCheckPermissions);
        PollNew.GroupID = groupId;
        PollNew.SiteID = CMSContext.CurrentSiteID;
    }


    void PollNew_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        CheckPermissions(groupId, CMSAdminControl.PERMISSION_MANAGE);
    }


    /// <summary>
    /// Saved event handler. 
    /// </summary>
    void PollNew_OnSaved(object sender, EventArgs e)
    {
        string error = null;
        // Show possible license limitation error
        if (!String.IsNullOrEmpty(PollNew.LicenseError))
        {
            error = "&error=" + PollNew.LicenseError;
        }

        URLHelper.Redirect("Polls_Edit.aspx?pollId=" + PollNew.ItemID.ToString() + "&groupid=" + groupId.ToString() + "&saved=1" + error);
    }
}
