using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Ecommerce;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_List : CMSOrdersPage
{
    #region "Variables"

    protected int customerId;

    #endregion


    #region "Page Events"

    protected override void OnPreInit(EventArgs e)
    {
        customerId = QueryHelper.GetInteger("customerid", 0);
        this.CustomerID = customerId;      

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string[,] actions = new string[1, 6];

        // New item link
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Order_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Order_New.aspx") + "?customerid=" + customerId;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_Order/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        // Set master title
        if (this.customerId <= 0)
        {
            this.CurrentMaster.Title.TitleText = GetString("Order_New.Orders");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Order/object.png");
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.HelpTopicName = "helpTopic";

            AddMenuButtonSelectScript("Orders", "");
        }
        else
        {
            AddMenuButtonSelectScript("Customers", "");
        }
    }

    #endregion
}
