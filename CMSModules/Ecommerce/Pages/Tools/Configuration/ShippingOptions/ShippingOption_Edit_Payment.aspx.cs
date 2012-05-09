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
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_Payment : CMSShippingOptionsPage
{
    protected int mShippingOptionId = 0;
    protected string mCurrentValues = string.Empty;
    protected ShippingOptionInfo mShippingOptionInfoObj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.ShippingOptions.PaymentMethods"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.ShippingOptions.PaymentMethods");
        }

        bool offerGlobalPaymentMethods = false;

        lblAvialable.Text = GetString("com.shippingoption.payments");
        mShippingOptionId = QueryHelper.GetInteger("shippingoptionid", 0);
        if (mShippingOptionId > 0)
        {
            mShippingOptionInfoObj = ShippingOptionInfoProvider.GetShippingOptionInfo(mShippingOptionId);
            EditedObject = mShippingOptionInfoObj;

            if (mShippingOptionInfoObj != null)
            {
                int editedSiteId = mShippingOptionInfoObj.ShippingOptionSiteID;
                // Check object's site id
                CheckEditedObjectSiteID(editedSiteId);

                // Offer global payment methods when allowed or configuring global shipping option
                if (editedSiteId != 0)
                {
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(editedSiteId);
                    if (si != null)
                    {
                        offerGlobalPaymentMethods = ECommerceSettings.AllowGlobalPaymentMethods(si.SiteName);
                    }
                }
                // Configuring global shipping option
                else
                {
                    offerGlobalPaymentMethods = true;
                }

                DataSet ds = PaymentOptionInfoProvider.GetPaymentOptionsForShipping(mShippingOptionId, false);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    mCurrentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "PaymentOptionID"));
                }

                if (!RequestHelper.IsPostBack())
                {
                    uniSelector.Value = mCurrentValues;
                }
            }
        }

        uniSelector.IconPath = GetObjectIconUrl("ecommerce.paymentoption", "object.png");
        uniSelector.OnSelectionChanged += uniSelector_OnSelectionChanged;
        uniSelector.WhereCondition = GetSelectorWhereCondition(offerGlobalPaymentMethods);
    }


    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveItems();
    }


    protected void SaveItems()
    {
        if (mShippingOptionInfoObj == null)
        {
            return;
        }

        // Check permissions
        CheckConfigurationModification(mShippingOptionInfoObj.ShippingOptionSiteID);

        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, mCurrentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int paymentId = ValidationHelper.GetInteger(item, 0);
                    PaymentShippingInfoProvider.RemovePaymentFromShipping(paymentId, mShippingOptionId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(mCurrentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int paymentId = ValidationHelper.GetInteger(item, 0);
                    PaymentShippingInfoProvider.AddPaymentToShipping(paymentId, mShippingOptionId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }


    /// <summary>
    /// Returns where condition for uniselector. This condition filters records contained in currently selected values,
    /// global records according to offerGlobalObjects paramter and site-specific records according to edited objects 
    /// site Id.
    /// </summary>
    /// <param name="offerGlobalObjects">Indicates if global records will be selected</param>
    protected string GetSelectorWhereCondition(bool offerGlobalObjects)
    {
        // Select nothing
        string where = "(1=0)";

        // Add global records
        if (offerGlobalObjects)
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionSiteID IS NULL", "OR");
        }

        // Add site specific records
        if ((mShippingOptionInfoObj != null) && (mShippingOptionInfoObj.ShippingOptionSiteID != 0))
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionSiteID = " + mShippingOptionInfoObj.ShippingOptionSiteID, "OR");
        }

        where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionEnabled = 1");

        // Add records which are used by parent object
        if (!string.IsNullOrEmpty(mCurrentValues))
        {
            where = SqlHelperClass.AddWhereCondition(where, "PaymentOptionID IN (" + mCurrentValues.Replace(';', ',') + ")", "OR");
        }

        return where;
    }
}
