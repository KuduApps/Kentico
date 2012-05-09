using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.LicenseProvider;

public partial class CMSModules_Membership_Pages_Membership_Header : CMSMembershipPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        int siteId = QueryHelper.GetInteger("siteID", 0);
        int membershipId = QueryHelper.GetInteger("membershipID", 0);

        MembershipInfo membership = MembershipInfoProvider.GetMembershipInfo(membershipId);

        if (membership != null)
        {
            // Check E-commerce module availability
            bool ecommerceAvailable = false;

            if (CMSContext.CurrentSiteName != null)
            {
                ecommerceAvailable = LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Ecommerce, "CMS.Ecommerce") && ResourceSiteInfoProvider.IsResourceOnSite("CMS.Ecommerce", CMSContext.CurrentSiteName);
            }

            // Prepare the tabs
            string[,] tabs = new string[ecommerceAvailable ? 4 : 3, 4];
            string siteIDParam = (siteId != 0) ? "&SiteID=" + siteId : String.Empty;

            tabs[0, 0] = GetString("general.general");
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'membership_general');";
            tabs[0, 2] = "Tab_General.aspx?membershipID=" + membershipId + siteIDParam;

            tabs[1, 0] = GetString("general.roles");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'membership_roles');";
            tabs[1, 2] = "Tab_Roles.aspx?membershipID=" + membershipId + siteIDParam;

            tabs[2, 0] = GetString("general.users");
            tabs[2, 1] = "SetHelpTopic('helpTopic', 'membership_users');";
            tabs[2, 2] = "Tab_Users.aspx?membershipID=" + membershipId + siteIDParam;

            // Show Ecommerce tab if available
            if (ecommerceAvailable)
            {
                tabs[3, 0] = this.GetString("ecommerce.products");
                tabs[3, 1] = "SetHelpTopic('helpTopic', 'membership_products');";
                string url = this.ResolveUrl("~/CMSModules/Ecommerce/Pages/Administration/Membership/Membership_Edit_Products.aspx");
                if (siteId > 0)
                {
                    url = URLHelper.AddParameterToUrl(url, "siteID", siteId.ToString());
                }
                url = URLHelper.AddParameterToUrl(url, "membershipID", membershipId.ToString());
                tabs[3, 2] = url;
            }

            string query = URLHelper.GetQuery(URLHelper.Url.ToString());
            string bcQuery = URLHelper.RemoveUrlParameter(query, "membershipID");

            // Prepare the breadcrumbs
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("membership.membership.list");
            breadcrumbs[0, 1] = "~/CMSModules/Membership/Pages/Membership/List.aspx" + bcQuery;
            breadcrumbs[0, 2] = "_parent";
            breadcrumbs[1, 0] = membership.MembershipDisplayName;

            // Set the tabs
            ICMSMasterPage master = this.CurrentMaster;
            master.Tabs.Tabs = tabs;
            master.Tabs.UrlTarget = "content";

            // Set the title
            PageTitle title = this.CurrentMaster.Title;
            title.Breadcrumbs = breadcrumbs;
            title.TitleText = GetString("membership.membership.edit");
            title.TitleImage = GetImageUrl("Objects/CMS_Membership/object.png");
            title.HelpTopicName = "membership_general";
            title.HelpName = "helpTopic";
        }
    }

    #endregion
}

