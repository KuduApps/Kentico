using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Membership_New : CMSMembershipPage
{
    #region "Variables"

    string siteParam = String.Empty;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int siteID = SiteID;
        if (siteID == 0)
        {
            siteID = SelectedSiteID;
        }

        editElem.SiteID = siteID;

        siteParam = URLHelper.RemoveUrlParameter(URLHelper.GetQuery(URLHelper.Url.ToString()), "membershipID");
        // Prepare the breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("membership.membership.list");
        breadcrumbs[0, 1] = "~/CMSModules/Membership/Pages/Membership/List.aspx" + siteParam;
        breadcrumbs[1, 0] = GetString("membership.membership.new");

        // Set the title
        PageTitle title = this.CurrentMaster.Title;
        title.Breadcrumbs = breadcrumbs;
        title.TitleText = GetString("membership.membership.new");
        title.TitleImage = GetImageUrl("Objects/CMS_Membership/new.png");
        title.HelpTopicName = "membership_general";

        this.editElem.OnSaved += new EventHandler(editElem_OnSaved);

        editElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(membershipEditElem_OnCheckPermissions);

    }


    protected void membershipEditElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Membership", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.Membership", permissionType);
        }
    }


    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        if (siteParam != String.Empty)
        {
            siteParam = siteParam.Replace('?', '&');
        }

        URLHelper.Redirect("Frameset.aspx?saved=1&membershipId=" + editElem.ItemID + siteParam);
    }

    #endregion
}
