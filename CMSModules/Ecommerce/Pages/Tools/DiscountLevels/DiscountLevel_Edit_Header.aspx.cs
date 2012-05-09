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
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_DiscountLevels_DiscountLevel_Edit_Header : CMSDiscountLevelsPage
{
    protected int mDiscountLevelId = 0;
    protected int editedSiteId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        string currentDiscount = "";

        // Get discountlLevel id from querystring		
        mDiscountLevelId = QueryHelper.GetInteger("discountLevelId", 0);
        if (mDiscountLevelId > 0)
        {
            DiscountLevelInfo di = DiscountLevelInfoProvider.GetDiscountLevelInfo(mDiscountLevelId);

            if (di != null)
            {
                editedSiteId = di.DiscountLevelSiteID;
                // Check if edited object belongs to configured site
                CheckEditedObjectSiteID(editedSiteId);

                currentDiscount = ResHelper.LocalizeString(di.DiscountLevelDisplayName);
            }
        }

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        int hideBreadcrumbs = QueryHelper.GetInteger("hidebreadcrumbs", 0);
        if (hideBreadcrumbs == 0)
        {
            // Initializes page title breadcrumbs control		
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("DiscountLevel_Edit.ItemListLink");
            breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/DiscountLevels/DiscountLevel_List.aspx?siteId=" + SiteID;
            breadcrumbs[0, 2] = "ecommerceContent";
            breadcrumbs[1, 0] = FormatBreadcrumbObjectName(currentDiscount, editedSiteId);
            breadcrumbs[1, 1] = "";
            breadcrumbs[1, 2] = "";
            master.Title.Breadcrumbs = breadcrumbs;
        }

        // Page title
        master.Title.HelpTopicName = "new_levelgeneral_tab";
        master.Title.HelpName = "helpTopic";

        // Tabs
        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "DiscountLevels";
        master.Tabs.UrlTarget = "DiscountLevelContent";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);

        // Set master title
        master.Title.TitleText = GetString("com.discountlevel.edit");
        master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_DiscountLevel/object.png");
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
