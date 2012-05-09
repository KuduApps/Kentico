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
using CMS.Community;
using CMS.CMSHelper;
using CMS.EventLog;

public partial class CMSModules_Groups_Tools_Group_Delete : CMSGroupPage
{
    #region "Private variables"

    private string mGroupListUrl = "~/CMSModules/Groups/Tools/Group_List.aspx";
    private GroupInfo gi = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Only community manager can delete group
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Groups", CMSAdminControl.PERMISSION_MANAGE))
        {
            RedirectToCMSDeskAccessDenied("CMS.Groups", CMSAdminControl.PERMISSION_MANAGE);
        }

        int groupId = QueryHelper.GetInteger("groupid", 0);
        gi = GroupInfoProvider.GetGroupInfo(groupId);

        if (gi != null)
        {
            lblMsg.Style.Add("font-weight", "bold");
            mGroupListUrl = ResolveUrl(mGroupListUrl);

            // Pagetitle
            this.CurrentMaster.Title.TitleText = GetString("group.deletegroup") + " \"" + HTMLHelper.HTMLEncode(gi.GroupDisplayName) + "\"";
            this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Groups/groupdelete.png");

            // Initializes breadcrumbs
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("group.deletegroup.listlink");
            breadcrumbs[0, 1] = mGroupListUrl;
            breadcrumbs[0, 2] = "";
            breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(gi.GroupDisplayName);
            breadcrumbs[1, 1] = "";
            breadcrumbs[1, 2] = "";
            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

            btnDelete.Click += new EventHandler(btnDelete_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }
    }


    void btnDelete_Click(object sender, EventArgs e)
    {
        // Only community manager can delete group
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Groups", CMSAdminControl.PERMISSION_MANAGE))
        {
            RedirectToCMSDeskAccessDenied("CMS.Groups", CMSAdminControl.PERMISSION_MANAGE);
        }

        if (gi != null)
        {
            try
            {
                GroupInfoProvider.DeleteGroupInfo(gi, chkDeleteAll.Checked);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
                lblError.Visible = true;
                return;
            }
        }
        URLHelper.Redirect(mGroupListUrl);
    }


    void btnCancel_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(mGroupListUrl);
    }

    #endregion
}
