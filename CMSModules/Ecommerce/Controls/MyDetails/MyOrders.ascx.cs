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
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_MyDetails_MyOrders : CMSAdminControl
{
    #region "Variables"

    private int mCustomerId = 0;
    private bool mShowOrderTrackingNumber = false;
    private bool downloadLinksColumnVisible = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Customer ID.
    /// </summary>
    public int CustomerId
    {
        get
        {
            return mCustomerId;
        }
        set
        {
            mCustomerId = value;
        }
    }


    /// <summary>
    /// Indcicates if order tracking number should be displayed.
    /// </summary>
    public bool ShowOrderTrackingNumber
    {
        get
        {
            return mShowOrderTrackingNumber;
        }
        set
        {
            mShowOrderTrackingNumber = value;
        }
    }


    /// <summary>
    /// If true, control does not process the data.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["StopProcessing"], false);
        }
        set
        {
            ViewState["StopProcessing"] = value;
        }
    }

    #endregion


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                gridOrders.IsLiveSite = this.IsLiveSite;
                gridOrders.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridOrders_OnExternalDataBound);
                gridOrders.WhereCondition = "(CustomerID = " + this.CustomerId + ") AND (OrderSiteID = " + CMSContext.CurrentSiteID + ")";
            }
            else
            {
                // Hide if user is not authenticated
                this.Visible = false;
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Set visibility of order traking number column
        this.gridOrders.GridView.Columns[4].Visible = this.ShowOrderTrackingNumber;

        // Set visibility of download links column
        this.gridOrders.GridView.Columns[6].Visible = this.downloadLinksColumnVisible;

        ScriptHelper.RegisterDialogScript(this.Page);
    }


    protected object gridOrders_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView dr = null;
        switch (sourceName.ToLower())
        {
            case "totalprice":
                dr = (DataRowView)parameter;
                return HTMLHelper.HTMLEncode(String.Format(dr["CurrencyFormatString"].ToString(), dr["OrderTotalPrice"]));

            case "statusdisplayname":
                return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(Convert.ToString(parameter)));

            case "invoice":
                return "<a target=\"_blank\" href=\"" + URLHelper.ResolveUrl("~/CMSModules/Ecommerce/CMSPages/GetInvoice.aspx?orderid=" + ValidationHelper.GetInteger(parameter, 0)) + "\">" + GetString("general.view") + "</a>";

            case "downloads":
                int orderId = ValidationHelper.GetInteger(parameter, 0);

                string where = String.Format("COM_OrderItemSKUFile.OrderItemID IN (SELECT OrderItemID FROM COM_OrderItem WHERE OrderItemOrderID = {0})", orderId);

                // Get order item SKU files for the order
                DataSet orderItemSkuFiles = SqlHelperClass.ExecuteQuery("ecommerce.orderitemskufile.selectallwithdetails", null, where, null);

                // If there are some downloads available for the order
                if (!DataHelper.DataSourceIsEmpty(orderItemSkuFiles))
                {
                    // Make download links column visible
                    this.downloadLinksColumnVisible = true;

                    // Show view action for this record
                    string url = URLHelper.ResolveUrl("~/CMSModules/Ecommerce/CMSPages/EProducts.aspx?orderid=" + orderId);
                    return String.Format("<a href=\"#\" onclick=\"modalDialog('{0}', 'DownloadLinks', 700, 420); return false;\">{1}</a>", url, this.GetString("general.view"));
                }

                return String.Empty;
        }
        return parameter;
    }


    /// <summary>
    /// Overriden SetValue - because of MyAccount webpart.
    /// </summary>
    /// <param name="propertyName">Name of the property to set</param>
    /// <param name="value">Value to set</param>
    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        switch (propertyName.ToLower())
        {
            case "customerid":
                this.CustomerId = ValidationHelper.GetInteger(value, 0);
                break;
            case "showordertrackingnumber":
                this.ShowOrderTrackingNumber = ValidationHelper.GetBoolean(value, false);
                break;
        }
    }
}