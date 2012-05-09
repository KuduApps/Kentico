using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSWebParts_Ecommerce_Donations : CMSAbstractWebPart
{
    #region "Variables"

    private string mDonations = "ALL";
    private bool mDisplayOnlyPaidDonations = true;

    private int mTopN = 0;
    private string mColumns = "COM_SKU.SKUName, COM_OrderItem.OrderItemTotalPriceInMainCurrency, COM_Customer.CustomerFirstName, COM_Customer.CustomerLastName, COM_Customer.CustomerCompany, COM_Order.OrderDate";
    private string mOrderBy = "COM_OrderItem.OrderItemTotalPriceInMainCurrency DESC";
    private string mWhereCondition = null;

    private string mTransformationName = "ecommerce.transformations.donationslistitem";
    private string mItemSeparator = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Type of donations to be displayed. Possible values: 'ALL' - both public and private donations, 'PUBLIC' - public donations, 'PRIVATE' - private donations.
    /// </summary>
    public string Donations
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Donations"), this.mDonations);
        }
        set
        {
            this.SetValue("Donations", value);
            this.mDonations = value;
        }
    }


    /// <summary>
    /// Indicates if only donations from already paid orders are being displayed.
    /// </summary>
    public bool DisplayOnlyPaidDonations
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayOnlyPaidDonations"), this.mDisplayOnlyPaidDonations);
        }
        set
        {
            this.SetValue("DisplayOnlyPaidDonations", value);
            this.mDisplayOnlyPaidDonations = value;
        }
    }


    /// <summary>
    /// Number of records to select.
    /// </summary>
    public int TopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("TopN"), this.mTopN);
        }
        set
        {
            this.SetValue("TopN", value);
            this.mTopN = value;
        }
    }


    /// <summary>
    /// Columns to select.
    /// </summary>
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), this.mColumns);
        }
        set
        {
            this.SetValue("Columns", value);
            this.mColumns = value;
        }
    }


    /// <summary>
    /// Order by expression.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OrderBy"), this.mOrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.mOrderBy = value;
        }
    }


    /// <summary>
    /// Where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WhereCondition"), this.mWhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.mWhereCondition = value;
        }
    }


    /// <summary>
    /// Transformation name.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), this.mTransformationName);
        }
        set
        {
            this.SetValue("TransformationName", value);
            this.mTransformationName = value;
            this.repeater.TransformationName = value;
        }
    }


    /// <summary>
    /// Item separator.
    /// </summary>
    public string ItemSeparator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemSeparator"), this.mItemSeparator);
        }
        set
        {
            this.SetValue("ItemSeparator", value);
            this.mItemSeparator = value;
            this.repeater.ItemSeparator = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();

        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            this.repeater.StopProcessing = true;
            return;
        }

        // Initialize repeater
        this.repeater.TransformationName = this.TransformationName;
        this.repeater.ItemSeparator = this.ItemSeparator;
        this.ReloadRepeaterData();
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        this.SetupControl();
    }


    /// <summary>
    /// Reloads repeater data.
    /// </summary>
    private void ReloadRepeaterData()
    {
        this.repeater.DataSource = OrderItemInfoProvider.GetDonations(this.GetCompleteWhereCondition(), this.OrderBy, this.TopN, this.Columns);
        this.repeater.DataBind();
    }


    /// <summary>
    /// Returns complete where condition according to current property values.
    /// </summary>
    private string GetCompleteWhereCondition()
    {
        string where = null;

        // Get only order items for current site
        where = SqlHelperClass.AddWhereCondition(where, "COM_Order.OrderSiteID = " + CMSContext.CurrentSiteID);

        // Get only order items from paid orders
        if (this.DisplayOnlyPaidDonations)
        {
            where = SqlHelperClass.AddWhereCondition(where, "COM_Order.OrderIsPaid = 1");
        }

        // Get only public/private donations
        switch (this.Donations.ToUpper())
        {
            case "PUBLIC":
                where = SqlHelperClass.AddWhereCondition(where, "COM_OrderItem.OrderItemIsPrivate = 0");
                break;

            case "PRIVATE":
                where = SqlHelperClass.AddWhereCondition(where, "COM_OrderItem.OrderItemIsPrivate = 1");
                break;
        }

        // Add additional where condition
        if (!String.IsNullOrEmpty(this.WhereCondition))
        {
            where = SqlHelperClass.AddWhereCondition(where, this.WhereCondition);
        }

        return where;
    }

    #endregion
}
