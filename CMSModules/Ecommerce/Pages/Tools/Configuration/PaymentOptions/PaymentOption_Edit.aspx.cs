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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_PaymentOptions_PaymentOption_Edit : CMSPaymentMethodsPage
{
    protected int mPaymentOptionId = 0;
    protected int mSiteId = -1;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Field validator error messages initialization
        rfvDisplayName.ErrorMessage = GetString("paymentoption_edit.errorEmptyDisplayName");
        rfvCodeName.ErrorMessage = GetString("paymentoption_edit.errorEmptyCodeName");

        // Control initializations				
        lblPaymentOptionName.Text = GetString("PaymentOption_Edit.PaymentOptionNameLabel");
        lblPaymentOptionDisplayName.Text = GetString("PaymentOption_Edit.PaymentOptionDisplayNameLabel");
        // Gateway
        lblPaymentGateway.Text = GetString("PaymentOption_Edit.GatewaySettings");
        lblGateUrl.Text = GetString("PaymentOption_Edit.GateUrl");
        lblPaymentAssemblyName.Text = GetString("PaymentOption_Edit.PaymentAssemblyName");
        lblPaymentClassName.Text = GetString("PaymentOption_Edit.PaymentClassName");
        // Statuses
        lblStatusFailed.Text = GetString("PaymentOption_Edit.PaymentFailedStatus");
        lblStatusSucceed.Text = GetString("PaymentOption_Edit.PaymentSucceedStatus");

        btnOk.Text = GetString("General.OK");

        string currentPaymentOption = GetString("PaymentOption_Edit.NewItemCaption");

        // Get paymentOption id from querystring		
        mPaymentOptionId = QueryHelper.GetInteger("paymentOptionId", 0);
        mSiteId = ConfiguredSiteID;
        if (mPaymentOptionId > 0)
        {
            PaymentOptionInfo paymentOptionObj = PaymentOptionInfoProvider.GetPaymentOptionInfo(mPaymentOptionId);
            EditedObject = paymentOptionObj;

            if (paymentOptionObj != null)
            {
                currentPaymentOption = ResHelper.LocalizeString(paymentOptionObj.PaymentOptionDisplayName);
                mSiteId = paymentOptionObj.PaymentOptionSiteID;
                CheckEditedObjectSiteID(mSiteId);

                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(paymentOptionObj);

                    // Show that the paymentOption was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }

            this.CurrentMaster.Title.TitleText = GetString("PaymentOption_Edit.HeaderCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_PaymentOption/object.png");
        }
        else // Add new
        {
            this.CurrentMaster.Title.TitleText = GetString("PaymentOption_New.HeaderCaption");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_PaymentOption/new.png");
        }

        this.succeededElem.SiteID = mSiteId;
        this.failedElem.SiteID = mSiteId;

        // Initializes page title control		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("PaymentOption_Edit.ItemListLink");
        pageTitleTabs[0, 1] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/PaymentOptions/PaymentOption_List.aspx?siteId=" + SiteID;
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = FormatBreadcrumbObjectName(currentPaymentOption, mSiteId);
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.HelpTopicName = "newedit_payment_method";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Load data of editing paymentOption.
    /// </summary>
    /// <param name="paymentOptionObj">PaymentOption object</param>
    protected void LoadData(PaymentOptionInfo paymentOptionObj)
    {
        txtPaymentOptionName.Text = paymentOptionObj.PaymentOptionName;
        chkPaymentOptionEnabled.Checked = paymentOptionObj.PaymentOptionEnabled;
        txtPaymentOptionDisplayName.Text = paymentOptionObj.PaymentOptionDisplayName;
        txtGateUrl.Text = paymentOptionObj.PaymentOptionPaymentGateUrl;
        txtPaymentAssemblyName.Text = paymentOptionObj.PaymentOptionAssemblyName;
        txtPaymentClassName.Text = paymentOptionObj.PaymentOptionClassName;
        succeededElem.OrderStatusID = paymentOptionObj.PaymentOptionSucceededOrderStatusID;
        failedElem.OrderStatusID = paymentOptionObj.PaymentOptionFailedOrderStatusID;
        this.chkAllowIfNoShipping.Checked = paymentOptionObj.PaymentOptionAllowIfNoShipping;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check input values from textboxes and other contrlos
        string errorMessage = new Validator()
            .NotEmpty(txtPaymentOptionDisplayName.Text.Trim(), GetString("paymentoption_edit.errorEmptyDisplayName"))
            .NotEmpty(txtPaymentOptionName.Text.Trim(), GetString("paymentoption_edit.errorEmptyCodeName")).Result;

        if (!ValidationHelper.IsCodeName(txtPaymentOptionName.Text.Trim()))
        {
            errorMessage = GetString("General.ErrorCodeNameInIdentificatorFormat");
        }

        if (errorMessage == "")
        {
            // PaymentOptionName must be unique
            PaymentOptionInfo paymentOptionObj = null;
            string siteWhere = (mSiteId > 0) ? " AND (PaymentOptionSiteID = " + mSiteId + " OR PaymentOptionSiteID IS NULL)" : "";
            DataSet ds = PaymentOptionInfoProvider.GetPaymentOptions("PaymentOptionName = '" + txtPaymentOptionName.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                paymentOptionObj = new PaymentOptionInfo(ds.Tables[0].Rows[0]);
            }

            if ((paymentOptionObj == null) || (paymentOptionObj.PaymentOptionID == mPaymentOptionId))
            {
                // Get the object
                if (paymentOptionObj == null)
                {
                    paymentOptionObj = PaymentOptionInfoProvider.GetPaymentOptionInfo(mPaymentOptionId);
                    if (paymentOptionObj == null)
                    {
                        paymentOptionObj = new PaymentOptionInfo();
                        paymentOptionObj.PaymentOptionSiteID = mSiteId;
                    }
                }

                paymentOptionObj.PaymentOptionID = mPaymentOptionId;
                paymentOptionObj.PaymentOptionDisplayName = txtPaymentOptionDisplayName.Text.Trim();
                paymentOptionObj.PaymentOptionName = txtPaymentOptionName.Text.Trim();
                paymentOptionObj.PaymentOptionEnabled = chkPaymentOptionEnabled.Checked;
                paymentOptionObj.PaymentOptionPaymentGateUrl = txtGateUrl.Text.Trim();
                paymentOptionObj.PaymentOptionClassName = txtPaymentClassName.Text.Trim();
                paymentOptionObj.PaymentOptionAssemblyName = txtPaymentAssemblyName.Text.Trim();
                paymentOptionObj.PaymentOptionSucceededOrderStatusID = succeededElem.OrderStatusID;
                paymentOptionObj.PaymentOptionFailedOrderStatusID = failedElem.OrderStatusID;
                paymentOptionObj.PaymentOptionAllowIfNoShipping = this.chkAllowIfNoShipping.Checked;

                CheckConfigurationModification(paymentOptionObj.PaymentOptionSiteID);

                PaymentOptionInfoProvider.SetPaymentOptionInfo(paymentOptionObj);

                URLHelper.Redirect("PaymentOption_Edit.aspx?paymentOptionId=" + Convert.ToString(paymentOptionObj.PaymentOptionID) + "&saved=1&siteId=" + SiteID);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("PaymentOption_Edit.PaymentOptionNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
