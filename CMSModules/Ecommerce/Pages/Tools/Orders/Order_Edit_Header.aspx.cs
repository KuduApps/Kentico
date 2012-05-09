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
using CMS.FormEngine;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_Header : CMSOrdersPage
{
    #region "Variables"

    private int orderId;
    private int customerId;

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        customerId = QueryHelper.GetInteger("customerid", 0);
        this.CustomerID = customerId;
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the parameters from querystring
        orderId = QueryHelper.GetInteger("orderid", 0);
        customerId = QueryHelper.GetInteger("customerId", 0);

        // Initializes page title control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Order_Edit.Orders");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Orders/Order_List.aspx";
        if (customerId > 0)
        {
            pageTitleTabs[0, 1] += "?customerId=" + customerId.ToString();
            pageTitleTabs[0, 2] = "CustomerContent";
        }
        else
        {
            pageTitleTabs[0, 2] = "ecommerceContent";
        }
        pageTitleTabs[1, 0] = orderId.ToString();
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        master.Title.Breadcrumbs = pageTitleTabs;
        master.Title.HelpTopicName = "general_tab11";
        master.Title.HelpName = "helpTopic";

        master.Tabs.ModuleName = "CMS.Ecommerce";
        master.Tabs.ElementName = "Orders";
        master.Tabs.UrlTarget = "orderContent";
        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);


        // Set master title
        if (this.customerId <= 0)
        {
            master.Title.TitleText = GetString("com.order.edit");
            master.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Order/object.png");

            AddMenuButtonSelectScript("Orders", "");
        }
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        if (element.ElementName.ToLower() == "orders.customfields")
        {
            // Check if order has any custom fields
            FormInfo formInfo = FormHelper.GetFormInfo("ecommerce.order", false);
            if (formInfo.GetFormElements(true, false, true).Count <= 0)
            {
                return null;
            }
        }

        return parameters;
    }
}

