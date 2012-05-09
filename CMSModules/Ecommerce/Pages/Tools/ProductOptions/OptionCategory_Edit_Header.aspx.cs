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
using CMS.Controls;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_Edit_Header : CMSProductOptionCategoriesPage
{
    protected int categoryId = 0;
    protected int editedSiteId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        bool hideBreadcrumbs = QueryHelper.GetBoolean("hidebreadcrumbs", false);
        // Get option category ID from querystring
        categoryId = QueryHelper.GetInteger("categoryID", 0);

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        // Get localized option category name
        string categName = "";
        OptionCategoryInfo categObj = OptionCategoryInfoProvider.GetOptionCategoryInfo(categoryId);
        if (categObj != null)
        {
            categName = ResHelper.LocalizeString(categObj.CategoryDisplayName);
            editedSiteId = categObj.CategorySiteID;

            // Check if edited object belongs to configured site
            CheckEditedObjectSiteID(editedSiteId);
        }

        if (!hideBreadcrumbs)
        {
            // Initializes page title control
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("optioncategory_edit.itemlistlink");
            breadcrumbs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/ProductOptions/OptionCategory_List.aspx?siteId=" + SiteID;
            breadcrumbs[0, 2] = "ecommerceContent";
            breadcrumbs[1, 0] = FormatBreadcrumbObjectName(categName, editedSiteId);
            breadcrumbs[1, 1] = "";
            breadcrumbs[1, 2] = "";
            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        }

        master.Title.HelpTopicName = "edit_option_category___general";
        master.Title.HelpName = "helpTopic";

        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "ProductOptions";
        master.Tabs.UrlTarget = "OptionCategoryEdit";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
        master.Tabs.OpenTabContentAfterLoad = false;

        // Set master title
        master.Title.TitleText = GetString("com.optioncategory.edit");
        master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_OptionCategory/object.png");
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        // Add SiteId parameter to each tab
        if (parameters.Length > 2)
        {
            parameters[2] = URLHelper.AddParameterToUrl(parameters[2], "siteId", SiteID.ToString());
        }

        return parameters;
    }
}
