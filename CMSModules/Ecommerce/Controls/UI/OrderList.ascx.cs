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
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Controls_UI_OrderList : CMSAdminListControl
{
    #region "Variables"

    private int customerId = 0;
    private bool isAdmin = false;
    private int siteId;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        isAdmin = (CMSContext.CurrentUser != null) && (CMSContext.CurrentUser.IsGlobalAdministrator);

        if (!isAdmin)
        {
            this.plcSiteFilter.Visible = false;
            siteSelector.StopProcessing = true;
            siteId = CMSContext.CurrentSiteID;
        }
        else
        {
            // Set site selector
            siteSelector.AllowAll = true;

            if (!RequestHelper.IsPostBack())
            {
                siteId = CMSContext.CurrentSiteID;
                siteSelector.Value = siteId;
            }
            else
            {
                siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
            }
        }

        // Set filter controls
        lblStatus.Text = GetString("OrderList.StatusLabel");
        lblSites.Text = GetString("OrderList.SitesLabel");
        lblOrderID.Text = GetString("OrderList.OrderIdLabel");
        lblCustomerFirstName.Text = GetString("OrderList.CustomerFirstName");
        lblCustomerLastName.Text = GetString("OrderList.CustomerLastName");

        btnFilter.Text = GetString("general.show");

        customerId = QueryHelper.GetInteger("customerId", 0);

        gridElem.IsLiveSite = this.IsLiveSite;
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.GridView.RowDataBound += new GridViewRowEventHandler(GridView_RowDataBound);
        gridElem.OnBeforeDataReload += gridElem_OnBeforeDataReload;
        gridElem.ImageDirectoryPath = GetImageUrl("Design/Controls/UniGrid/Actions/", IsLiveSite, true);
        gridElem.ZeroRowsText = GetString("general.nodatafound");
        gridElem.WhereCondition = GetWhereCondition();

        if (customerId > 0)
        {
            this.plcCustomerFilter.Visible = false;
            CustomerInfo customer = CustomerInfoProvider.GetCustomerInfo(customerId);
            if ((customer != null) && (customer.CustomerUserID > 0))
            {
                siteSelector.UserId = customer.CustomerUserID;
            }
            else
            {
                this.plcSiteFilter.Visible = false;
                siteSelector.StopProcessing = true;
                siteId = CMSContext.CurrentSiteID;
            }
        }

        statusElem.SiteID = siteId;

        // Initialize order is paid filter
        if (!RequestHelper.IsPostBack())
        {
            this.drpOrderIsPaid.Items.Add(new ListItem("general.selectall", "ALL"));
            this.drpOrderIsPaid.Items.Add(new ListItem("general.yes", "YES"));
            this.drpOrderIsPaid.Items.Add(new ListItem("general.no", "NO"));
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (customerId > 0)
        {
            gridElem.GridView.Columns[2].Visible = false;
            gridElem.GridView.Columns[3].Visible = false;
        }

        // Hide orders site name column, when only one site records are filtered out
        gridElem.NamedColumns["OrderSiteID"].Visible = (siteId <= 0);
    }

    #endregion


    #region "Event handlers"

    void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string color = ValidationHelper.GetString(((DataRowView)(e.Row.DataItem)).Row["StatusColor"], "");
            if (color != "")
            {
                e.Row.Style.Add("background-color", color);
            }
        }
    }


    protected void gridElem_OnBeforeDataReload()
    {
        gridElem.WhereClause = GetFilterWhereCondition();
    }


    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView dr = null;
        switch (sourceName.ToLower())
        {
            case "fullname":
                dr = (DataRowView)parameter;
                return HTMLHelper.HTMLEncode(dr["CustomerFirstName"] + " " + dr["CustomerLastName"]);

            case "totalprice":
                dr = (DataRowView)parameter;
                return HTMLHelper.HTMLEncode(String.Format(dr["CurrencyFormatString"].ToString(), dr["OrderTotalPrice"]));

            case "statusdisplayname":
                return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(Convert.ToString(parameter)));
            //break;
        }
        return parameter;
    }


    /// <summary>
    /// Handles the gridElem's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        int orderId = ValidationHelper.GetInteger(actionArgument, 0);
        OrderInfo oi = null;
        OrderStatusInfo osi = null;

        switch (actionName.ToLower())
        {
            case "edit":
                string redirectToUrl = "Order_Edit.aspx?orderID=" + orderId.ToString();
                if (customerId > 0)
                {
                    redirectToUrl += "&customerId=" + customerId.ToString();
                }
                URLHelper.Redirect(redirectToUrl);
                break;


            case "delete":
                // Check 'ModifyOrders' and 'EcommerceModify' permission
                if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
                {
                    AccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
                }

                // delete OrderInfo object from database
                OrderInfoProvider.DeleteOrderInfo(orderId);
                break;

            case "previous":
                // Check 'ModifyOrders' and 'EcommerceModify' permission
                if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
                {
                    AccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
                }

                oi = OrderInfoProvider.GetOrderInfo(orderId);
                if (oi != null)
                {
                    osi = OrderStatusInfoProvider.GetPreviousEnabledStatus(oi.OrderStatusID);
                    if (osi != null)
                    {
                        oi.OrderStatusID = osi.StatusID;
                        // Save order status changes
                        OrderInfoProvider.SetOrderInfo(oi);
                    }
                }
                break;

            case "next":
                // Check 'ModifyOrders' and 'EcommerceModify' permission
                if (!ECommerceContext.IsUserAuthorizedForPermission("ModifyOrders"))
                {
                    AccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyOrders");
                }

                oi = OrderInfoProvider.GetOrderInfo(orderId);
                if (oi != null)
                {
                    osi = OrderStatusInfoProvider.GetNextEnabledStatus(oi.OrderStatusID);
                    if (osi != null)
                    {
                        oi.OrderStatusID = osi.StatusID;
                        // Save order status changes
                        OrderInfoProvider.SetOrderInfo(oi);
                    }
                }
                break;
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Creates where condition based on query string.
    /// </summary>
    private string GetWhereCondition()
    {
        if (customerId > 0)
        {
            return "CustomerID = " + customerId;
        }
        return null;
    }


    /// <summary>
    /// Creates where condition corresponding to values selected in filter.
    /// </summary>
    private string GetFilterWhereCondition()
    {
        string where = string.Empty;

        // Status dropdownlist
        if (statusElem.OrderStatusID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "OrderStatusID = " + statusElem.OrderStatusID);
        }

        // Site filter
        if (siteId > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "OrderSiteID = " + siteId);
        }

        if (txtOrderId.Text.Trim() != "")
        {
            where = SqlHelperClass.AddWhereCondition(where, "OrderID = " + ValidationHelper.GetInteger(txtOrderId.Text.Trim(), 0));
        }

        if (customerId <= 0)
        {
            if (txtCustomerFirstName.Text.Trim() != "")
            {
                where = SqlHelperClass.AddWhereCondition(where, "CustomerFirstName LIKE N'%" + SqlHelperClass.GetSafeQueryString(txtCustomerFirstName.Text, false).Trim() + "%'");
            }

            if (txtCustomerLastName.Text.Trim() != "")
            {
                where = SqlHelperClass.AddWhereCondition(where, " CustomerLastName LIKE N'%" + SqlHelperClass.GetSafeQueryString(txtCustomerLastName.Text, false).Trim() + "%'");
            }
        }

        // Get only paid orders
        if (this.drpOrderIsPaid.SelectedValue == "YES")
        {
            where = SqlHelperClass.AddWhereCondition(where, "ISNULL(OrderIsPaid, 0) = 1");
        }
        // Get only not paid orders
        else if (this.drpOrderIsPaid.SelectedValue == "NO")
        {
            where = SqlHelperClass.AddWhereCondition(where, "ISNULL(OrderIsPaid, 0) = 0");
        }

        return where;
    }

    #endregion
}
