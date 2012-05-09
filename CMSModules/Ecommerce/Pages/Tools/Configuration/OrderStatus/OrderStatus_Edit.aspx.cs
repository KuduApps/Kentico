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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_OrderStatus_OrderStatus_Edit : CMSOrderStatusesPage
{
    #region "Variables"

    protected int mOrderStatusId = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvStatusDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvStatusName.ErrorMessage = GetString("general.requirescodename");

        // Control initializations				
        lblStatusEmail.Text = GetString("OrderStatus_Edit.StatusSendEmail");
        lblStatusDisplayName.Text = GetString("OrderStatus_Edit.StatusDisplayNameLabel");
        lblStatusName.Text = GetString("OrderStatus_Edit.StatusNameLabel");
        lblStatusColor.Text = GetString("OrderStatus_Edit.StatusColor");
        clrPicker.SupportFolder = "~/CMSAdminControls/ColorPicker";

        btnOk.Text = GetString("General.OK");

        string currentOrderStatus = GetString("OrderStatus_Edit.NewItemCaption");

        // Get order status id from querystring		
        mOrderStatusId = QueryHelper.GetInteger("orderStatusId", 0);
        if (mOrderStatusId > 0)
        {
            OrderStatusInfo orderStatusObj = OrderStatusInfoProvider.GetOrderStatusInfo(mOrderStatusId);
            EditedObject = orderStatusObj;

            if (orderStatusObj != null)
            {
                // Check edited object site ID
                CheckEditedObjectSiteID(orderStatusObj.StatusSiteID);

                currentOrderStatus = ResHelper.LocalizeString(orderStatusObj.StatusDisplayName);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(orderStatusObj);

                    // Show that the orderStatus was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }

            this.CurrentMaster.Title.TitleText = GetString("OrderStatus_Edit.HeaderCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_OrderStatus/object.png");
        }
        else
        {
            this.CurrentMaster.Title.TitleText = GetString("OrderStatus_Edit.NewItemCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_OrderStatus/new.png");
        }
        this.CurrentMaster.Title.HelpTopicName = "newedit_order";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title breadcrumbs control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("OrderStatus_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/OrderStatus/OrderStatus_List.aspx?siteId=" + SiteID;
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = currentOrderStatus;
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// Load data of editing orderStatus.
    /// </summary>
    /// <param name="orderStatusObj">OrderStatus object</param>
    protected void LoadData(OrderStatusInfo orderStatusObj)
    {
        chkStatusEnabled.Checked = orderStatusObj.StatusEnabled;
        txtStatusDisplayName.Text = orderStatusObj.StatusDisplayName;
        txtStatusName.Text = orderStatusObj.StatusName;
        clrPicker.SelectedColor = orderStatusObj.StatusColor;
        chkStatusEmail.Checked = orderStatusObj.StatusSendNotification;
        chkMarkOrderAsPaid.Checked = orderStatusObj.StatusOrderIsPaid;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check permissions
        CheckConfigurationModification();

        string errorMessage = new Validator()
            .NotEmpty(txtStatusName.Text.Trim(), GetString("general.requirescodename"))
            .NotEmpty(txtStatusDisplayName.Text.Trim(), GetString("general.requiresdisplayname")).Result;

        if (!ValidationHelper.IsCodeName(txtStatusName.Text.Trim()))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }

        if (errorMessage == "")
        {
            OrderStatusInfo orderStatusObj = OrderStatusInfoProvider.GetOrderStatusInfo(txtStatusName.Text.Trim(), SiteInfoProvider.GetSiteName(ConfiguredSiteID));

            // If statusName value is unique for site OR edit
            if ((orderStatusObj == null) || (orderStatusObj.StatusID == mOrderStatusId))
            {
                // If statusName value is unique for site -> determine whether it is update or insert 
                if ((orderStatusObj == null))
                {
                    // Get OrderStatusInfo object by primary key
                    orderStatusObj = OrderStatusInfoProvider.GetOrderStatusInfo(mOrderStatusId);
                    if (orderStatusObj == null)
                    {
                        // Create new item -> insert
                        orderStatusObj = new OrderStatusInfo();
                        orderStatusObj.StatusSiteID = ConfiguredSiteID;
                    }
                }

                orderStatusObj.StatusSendNotification = chkStatusEmail.Checked;
                orderStatusObj.StatusEnabled = chkStatusEnabled.Checked;
                orderStatusObj.StatusDisplayName = txtStatusDisplayName.Text.Trim();
                orderStatusObj.StatusName = txtStatusName.Text.Trim();
                orderStatusObj.StatusColor = clrPicker.SelectedColor;
                orderStatusObj.StatusOrderIsPaid = chkMarkOrderAsPaid.Checked;

                if (orderStatusObj.StatusOrder == 0)
                {
                    orderStatusObj.StatusOrder = OrderStatusInfoProvider.GetLastStatusOrder(orderStatusObj.StatusSiteID) + 1;
                }

                // Save
                OrderStatusInfoProvider.SetOrderStatusInfo(orderStatusObj);

                URLHelper.Redirect("OrderStatus_Edit.aspx?orderStatusId=" + orderStatusObj.StatusID + "&saved=1&siteId=" + SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("OrderStatus_Edit.OrderStatusExist");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
