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
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_DiscountCoupons_DiscountCoupon_Edit_Header : CMSDiscountCouponsPage
{
    protected int mDiscountId = 0;
    protected int editedSiteId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        string currentDiscountCoupon = "";
        mDiscountId = QueryHelper.GetInteger("discountid", 0);
        if (mDiscountId > 0)
        {
            DiscountCouponInfo discountCouponObj = DiscountCouponInfoProvider.GetDiscountCouponInfo(mDiscountId);
            if (discountCouponObj != null)
            {
                editedSiteId = discountCouponObj.DiscountCouponSiteID;

                // Check if edited object belongs to configured site
                CheckEditedObjectSiteID(editedSiteId);

                currentDiscountCoupon = ResHelper.LocalizeString(discountCouponObj.DiscountCouponDisplayName);
            }
        }

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        // Page title
        master.Title.HelpTopicName = "new_coupongeneral_tab";
        master.Title.HelpName = "helpTopic";

        // Initializes page title breadcrumbs control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("DiscounCoupon_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/DiscountCoupons/DiscountCoupon_List.aspx?siteId=" + SiteID;
        breadcrumbs[0, 2] = "ecommerceContent";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(currentDiscountCoupon, editedSiteId);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        // Tabs
        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "DiscountCoupons";
        master.Tabs.UrlTarget = "DiscountContent";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);

        // Set master title
        master.Title.TitleText = GetString("com.discountcoupon.edit");
        master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_DiscountCoupon/object.png");
    }


    string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        // Add SiteId parameter to each tab
        if (parameters.Length > 2)
        {
            parameters[2] = URLHelper.AddParameterToUrl(parameters[2], "siteId", SiteID.ToString());
        }

        return parameters;
    }
}
