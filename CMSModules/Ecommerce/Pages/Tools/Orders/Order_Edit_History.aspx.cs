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
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_History : CMSOrdersPage
{
    protected override void OnPreInit(EventArgs e)
    {
        this.CustomerID = QueryHelper.GetInteger("customerid", 0);
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Orders.History"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Orders.History");
        }

        int orderId = QueryHelper.GetInteger("orderid", 0);

        OrderInfo oi = OrderInfoProvider.GetOrderInfo(orderId);
        if (oi != null)
        {
            // Check order site ID
            CheckOrderSiteID(oi.OrderSiteID);

            this.gridElem.DataSource = OrderStatusUserInfoProvider.GetOrderStatusHistoryList(orderId);
            this.gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
            this.gridElem.GridView.RowDataBound += new GridViewRowEventHandler(gridElem_RowDataBound);
        }
    }


    protected void gridElem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int orderStatId = ValidationHelper.GetInteger(((DataRowView)(e.Row.DataItem)).Row["ToStatusID"], 0);

            OrderStatusInfo osi = OrderStatusInfoProvider.GetOrderStatusInfo(orderStatId);
            if (osi != null)
            {
                e.Row.Style.Add("background-color", osi.StatusColor);
            }
        }
    }


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        // Localize status display name
        if (sourceName.ToLower() == "tostatusdisplayname")
        {
            return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(Convert.ToString(parameter)));
        }
        else if (sourceName.ToLower() == "formattedusername")
        {
            return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(Convert.ToString(parameter)));
        }

        return HTMLHelper.HTMLEncode(parameter.ToString());
    }
}
