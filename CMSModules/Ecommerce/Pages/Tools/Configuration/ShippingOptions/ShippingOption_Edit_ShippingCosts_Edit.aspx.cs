using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_Edit_ShippingCosts_Edit : CMSShippingOptionsPage
{
    #region "Variables"

    private int mShippingCostId = -1;
    private int mShippingOptionId = -1;
    protected ShippingOptionInfo mShippingOptionInfoObj = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.ShippingOptions.ShippingCosts"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.ShippingOptions.ShippingCosts");
        }

        // Init labels
        lblShippingCostValue.Text = GetString("com.shippingcost.costlabel");
        lblShippingCostMinWeight.Text = GetString("com.shippingcost.minweightlabel");
        rfvMinWeight.ErrorMessage = GetString("com.shippingcost.minweightempty");
        rvMinWeight.ErrorMessage = GetString("com.shippingcost.minweightrange");
        txtShippingCostCharge.EmptyErrorMessage = GetString("com.shippingcost.costempty");
        txtShippingCostCharge.ValidationErrorMessage = GetString("com.shippingcost.costrange");

        btnOk.Text = GetString("General.OK");

        // Get parameters from querystring		
        mShippingCostId = QueryHelper.GetInteger("shippingCostId", 0);
        mShippingOptionId = QueryHelper.GetInteger("shippingCostShippingOptionId", 0);

        // Check if configured shipping option record belongs to correct site
        mShippingOptionInfoObj = ShippingOptionInfoProvider.GetShippingOptionInfo(mShippingOptionId);
        if (mShippingOptionInfoObj != null)
        {
            CheckEditedObjectSiteID(mShippingOptionInfoObj.ShippingOptionSiteID);
            txtShippingCostCharge.CurrencySiteID = mShippingOptionInfoObj.ShippingOptionSiteID;

            // Check presence of main currency
            string currencyErr = CheckMainCurrency(mShippingOptionInfoObj.ShippingOptionSiteID);
            if (!string.IsNullOrEmpty(currencyErr))
            {
                // Show message
                lblError.Text = currencyErr;
                lblError.Visible = true;
            }
        }

        string[,] breadcrumbs = new string[2, 4];
        breadcrumbs[0, 0] = GetString("com.ui.shippingcost");
        breadcrumbs[0, 1] = ResolveUrl("ShippingOption_Edit_ShippingCosts.aspx?shippingoptionid=" + mShippingOptionId + "&siteId=" + SiteID);

        // If true, then we will edit existing record                
        if (mShippingCostId > 0)
        {
            // Check if there is already ShippingCostInfo with this shippingCostID 
            ShippingCostInfo shippingCostInfo = ShippingCostInfoProvider.GetShippingCostInfo(mShippingCostId);
            
            // Check if shipping cost belongs to edited shipping option
            if ((shippingCostInfo != null) && (shippingCostInfo.ShippingCostShippingOptionID != mShippingOptionId))
            {
                shippingCostInfo = null;
            }

            EditedObject = shippingCostInfo;

            if (shippingCostInfo != null)
            {
                // Fill editing form with existing data when not postback
                if (!RequestHelper.IsPostBack())
                {
                    LoadData(shippingCostInfo);

                    // Show that the shippingCost was created or updated successfully
                    if (QueryHelper.GetString("saved", "") == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
            // Set title to "shipping cost properties"
            breadcrumbs[1, 0] = GetString("com.ui.shippingcost.edit");
            breadcrumbs[1, 1] = "";
        }
        // Do this when creating new cost
        else
        {
            // Set header to "new item"		
            breadcrumbs[1, 0] = GetString("com.ui.shippingcost.edit_new");
        }

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Load data of editing ShippingCost.
    /// </summary>
    /// <param name="shippingCostObj">ShippingCost object</param>
    protected void LoadData(ShippingCostInfo shippingCostObj)
    {
        // Load data from database
        txtShippingCostCharge.Value = shippingCostObj.ShippingCostValue;
        txtShippingCostMinWeight.Text = Convert.ToString(shippingCostObj.ShippingCostMinWeight);
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // No option to save under
        if (mShippingOptionInfoObj == null)
        {
            return;
        }

        // Check permissions
        CheckConfigurationModification(mShippingOptionInfoObj.ShippingOptionSiteID);

        // True if there is already same min count;
        bool isMinCountUnique = false;
        // Server side validation of user input 
        string errorMessage = new Validator().NotEmpty(txtShippingCostMinWeight.Text.Trim(), "com.shippingcost.minweightempty").Result;

        if (errorMessage == "")
        {
            errorMessage = txtShippingCostCharge.ValidatePrice(false);
        }

        double minWeight = ValidationHelper.GetDouble(txtShippingCostMinWeight.Text.Trim(), 0);

        if ((errorMessage == "") && (minWeight <= 0))
        {
            errorMessage = GetString("com.shippingcost.minweightrange");
        }

        if (errorMessage == "")
        {
            ShippingCostInfo shippingCostObj = ShippingCostInfoProvider.GetShippingCostInfo(mShippingCostId);
            // If ShippingCost doesnt already exist, create new one
            if (shippingCostObj == null)
            {
                // Create new shipping cost
                shippingCostObj = new ShippingCostInfo();
                shippingCostObj.ShippingCostShippingOptionID = mShippingOptionId;
            }

            // Look for record with same minimum weight
            DataSet ds = ShippingCostInfoProvider.GetShippingCosts("ShippingCostMinWeight = " + minWeight + " AND ShippingCostShippingOptionID = " + mShippingOptionId, null, 1, "ShippingCostID");
            if (DataHelper.DataSourceIsEmpty(ds))
            {
                isMinCountUnique = true;
            }

            // Check if min weight is unique or it is update of existing item
            if ((isMinCountUnique) || (SqlHelperClass.GetInteger(ds.Tables[0].Rows[0]["ShippingCostID"], -1) == shippingCostObj.ShippingCostID))
            {
                // Set ShippingCostObj values
                shippingCostObj.ShippingCostValue = txtShippingCostCharge.Value;
                shippingCostObj.ShippingCostMinWeight = minWeight;

                // Set data to database
                ShippingCostInfoProvider.SetShippingCostInfo(shippingCostObj);
                string redirectUrl = "ShippingOption_Edit_ShippingCosts_Edit.aspx?shippingCostId=" + Convert.ToString(shippingCostObj.ShippingCostID) + "&saved=1&shippingCostShippingOptionId=" + mShippingOptionId + "&siteId=" + SiteID;
                URLHelper.Redirect(redirectUrl);
            }
            else
            {
                lblError.Text = GetString("com.ui.shippingcost.edit_costexists");
                lblError.Visible = true;
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion
}

