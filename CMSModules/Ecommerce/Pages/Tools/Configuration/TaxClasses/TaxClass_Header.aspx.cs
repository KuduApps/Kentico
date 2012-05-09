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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_Header : CMSTaxClassesPage
{
    protected int taxClassId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        taxClassId = QueryHelper.GetInteger("taxclassid", 0);
        bool hideBreadcrumbs = QueryHelper.GetBoolean("hidebreadcrumbs", false);

        string taxClassName = "";
        TaxClassInfo tc = TaxClassInfoProvider.GetTaxClassInfo(taxClassId);
        if (tc != null)
        {
            CheckEditedObjectSiteID(tc.TaxClassSiteID);
            taxClassName = ResHelper.LocalizeString(tc.TaxClassDisplayName);
        }

        // initializes page title
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("TaxClass_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/TaxClasses/TaxClass_List.aspx?siteId=" + SiteID;
        pageTitleTabs[0, 2] = "configEdit";
        pageTitleTabs[1, 0] = taxClassName;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        if (!hideBreadcrumbs)
        {
            master.Title.Breadcrumbs = pageTitleTabs;
        }
        master.Title.TitleText = GetString("TaxClass_Edit.HeaderCaption-Properties");
        master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_TaxClass/object.png");
        master.Title.HelpTopicName = "new_classgeneral_tab";
        master.Title.HelpName = "helpTopic";

        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "Configuration.TaxClasses";
        master.Tabs.UrlTarget = "taxClassContent";
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
}
