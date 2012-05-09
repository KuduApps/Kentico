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

public partial class CMSModules_Groups_Tools_Polls_Polls_Edit_General : CMSGroupPollsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PollProperties.ItemID = QueryHelper.GetInteger("pollid", 0);
        PollProperties.SiteID = CMSContext.CurrentSiteID;
        PollProperties.GroupID = QueryHelper.GetInteger("groupid", 0);
        PollProperties.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(PollProperties_OnCheckPermissions);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!RequestHelper.IsPostBack())
        {
            PollProperties.ReloadData();
        }
    }


    void PollProperties_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        CheckPermissions(PollProperties.GroupID, CMSAdminControl.PERMISSION_MANAGE);
    }
}
