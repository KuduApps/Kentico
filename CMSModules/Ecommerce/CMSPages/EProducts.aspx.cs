using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_CMSPages_EProducts : CMSLiveModalPage
{
    #region "Variables"

    private int orderId = 0;

    #endregion


    #region "Page methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Get order ID from URL
        this.orderId = QueryHelper.GetInteger("orderid", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get order
        OrderInfo oi = OrderInfoProvider.GetOrderInfo(this.orderId);

        if (oi != null)
        {
            // Get customer for current user
            CustomerInfo customer = CustomerInfoProvider.GetCustomerInfoByUserID(CMSContext.CurrentUser.UserID);

            // If order does not belong to current user 
            if ((customer == null) || ((customer != null) && (oi.OrderCustomerID != customer.CustomerID)))
            {
                // Redirect to access denied page
                URLHelper.Redirect("~/CMSMessages/AccessDeniedToPage.aspx");
            }
        }
        else
        {
            string title = this.GetString("com.downloadsdialog.ordernotfoundtitle");
            string text = this.GetString("com.downloadsdialog.ordernotfoundtext");

            // Redirect to error page
            URLHelper.Redirect(String.Format("~/CMSMessages/Error.aspx?title={0}&text={1}", title, text));
        }

        // Initialize dialog title
        this.CurrentMaster.Title.TitleText = this.GetString("com.downloadsdialog.title");
        this.CurrentMaster.Title.TitleImage = this.GetImageUrl("Objects/Ecommerce_Order/download.png");

        // Initialize close button
        btnClose.Text = GetString("general.close");

        // Initialize unigrid
        this.downloadsGridElem.ZeroRowsText = this.GetString("com.downloadsdialog.nodownloadsfound");
        this.downloadsGridElem.OnDataReload += new OnDataReloadEventHandler(downloadsGridElem_OnDataReload);
        this.downloadsGridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(downloadsGridElem_OnExternalDataBound);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.downloadsGridElem.IsEmpty)
        {
            this.pnlDownloads.CssClass = "PageContent";
        }
    }

    #endregion


    #region "Methods"

    private DataSet downloadsGridElem_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        return OrderItemSKUFileInfoProvider.GetOrderItemSKUFiles(this.orderId);
    }


    object downloadsGridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DateTime orderItemValidTo = DateTimeHelper.ZERO_TIME;

        switch (sourceName.ToLower())
        {
            case "file":
                DataRowView row = (parameter as DataRowView);

                // Get values from parameter
                int orderSiteId = ValidationHelper.GetInteger(row["OrderSiteID"], 0);
                int fileId = ValidationHelper.GetInteger(row["OrderItemSKUFileID"], 0);
                string productName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(ValidationHelper.GetString(row["OrderItemSKUName"], null)));
                string fileName = ValidationHelper.GetString(row["FileName"], null);
                Guid token = ValidationHelper.GetGuid(row["Token"], Guid.Empty);
                string fileUrl = URLHelper.ResolveUrl(OrderItemSKUFileInfoProvider.GetOrderItemSKUFileUrl(token, fileName, orderSiteId));
                orderItemValidTo = ValidationHelper.GetDateTime(row["OrderItemValidTo"], DateTimeHelper.ZERO_TIME);

                // If download is not expired
                if ((orderItemValidTo.CompareTo(DateTimeHelper.ZERO_TIME) == 0) || (orderItemValidTo.CompareTo(DateTime.Now) > 0))
                {
                    // Return download link
                    return String.Format("{0} (<a href=\"{1}\" target=\"_blank\">{2}</a>)", productName, fileUrl, HTMLHelper.HTMLEncode(fileName));
                }
                else
                {
                    // Return file name
                    return String.Format("{0} ({1})", productName, HTMLHelper.HTMLEncode(fileName));
                }

            case "expiration":
                orderItemValidTo = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);

                // If download never expires
                if (orderItemValidTo.CompareTo(DateTimeHelper.ZERO_TIME) == 0)
                {
                    // Return dash
                    return "-";
                }
                else
                {
                    // Return expiration date and time
                    return orderItemValidTo.ToString();
                }
        }

        return parameter;
    }

    #endregion
}
