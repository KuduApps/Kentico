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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_Header : CMSShippingOptionsPage
{
    #region "Variables"

    protected int mShippingOptionId = 0;
    protected int editedSiteId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        mShippingOptionId = QueryHelper.GetInteger("shippingOptionID", 0);

        string shippingOptionName = "";
        ShippingOptionInfo soi = ShippingOptionInfoProvider.GetShippingOptionInfo(mShippingOptionId);
        if (soi != null)
        {
            editedSiteId = soi.ShippingOptionSiteID;

            // Check site ID
            CheckEditedObjectSiteID(editedSiteId);
            shippingOptionName = ResHelper.LocalizeString(soi.ShippingOptionDisplayName);
        }

        // Initializes page title and breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("ShippingOption_EditHeader.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/ShippingOptions/ShippingOption_List.aspx?siteId=" + SiteID;
        breadcrumbs[0, 2] = "configEdit";
        breadcrumbs[1, 0] = FormatBreadcrumbObjectName(shippingOptionName, editedSiteId);
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        master.Title.Breadcrumbs = breadcrumbs;
        master.Title.TitleText = GetString("ShippingOption_EditHeader.TitleText");
        master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_ShippingOption/object.png");
        master.Title.HelpTopicName = "newgeneral_tab2";
        master.Title.HelpName = "helpTopic";

        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "Configuration.ShippingOptions";
        master.Tabs.UrlTarget = "shippingOptionContent";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
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

    #endregion
}
