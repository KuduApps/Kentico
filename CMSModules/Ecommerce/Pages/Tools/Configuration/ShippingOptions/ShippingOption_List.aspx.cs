using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ShippingOptions_ShippingOption_List : CMSShippingOptionsPage
{
    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Init Unigrid
        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");

        // Init site selector
        SelectSite.Selector.SelectedIndexChanged += new EventHandler(Selector_SelectedIndexChanged);

        if (!RequestHelper.IsPostBack())
        {
            // Init site selector
            SelectSite.SiteID = SiteFilterStartupValue;
        }

        if (this.AllowGlobalObjects && ExchangeTableInfoProvider.IsExchangeRateFromGlobalMainCurrencyMissing(CMSContext.CurrentSiteID))
        {
            lblMissingRate.Visible = true;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("COM_ShippingOption_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_ShippingOption/object.png");
        this.CurrentMaster.Title.HelpTopicName = "shipping_options_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.DisplaySiteSelectorPanel = AllowGlobalObjects;

        // Prepare the new shipping option header element
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("COM_ShippingOption_List.NewItemCaption");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/ShippingOptions/ShippingOption_New.aspx?siteId=" + SelectSite.SiteID;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_ShippingOption/add.png");

        this.hdrActions.Actions = actions;

        InitWhereCondition();
    }


    protected override void OnPreRender(EventArgs e)
    {
        bool both = (SelectSite.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD);

        // Hide header actions if (both) selected
        hdrActions.Enabled = !both;
        lblWarnNew.Visible = both;

        base.OnPreRender(e);
        UniGrid.NamedColumns["ShippingOptionSiteID"].Visible = AllowGlobalObjects;
    }

    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("ShippingOption_Edit_Frameset.aspx?shippingOptionID=" + Convert.ToString(actionArgument) + "&siteId=" + SelectSite.SiteID);
        }
        else if (actionName == "delete")
        {
            ShippingOptionInfo shippingInfoObj = ShippingOptionInfoProvider.GetShippingOptionInfo(ValidationHelper.GetInteger(actionArgument, 0));
            // Nothing to delete
            if (shippingInfoObj == null) return;

            // Check permissions
            CheckConfigurationModification(shippingInfoObj.ShippingOptionSiteID);

            // Check dependencies
            if (ShippingOptionInfoProvider.CheckDependencies(shippingInfoObj.ShippingOptionID))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }
            // Delete ShippingOptionInfo object from database
            ShippingOptionInfoProvider.DeleteShippingOptionInfo(shippingInfoObj);
        }
    }


    object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "shippoptenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
            case "shippingoptionsiteid":
                return UniGridFunctions.ColoredSpanYesNo(parameter == DBNull.Value);
            case "shippoptcharge":
                DataRowView row = (DataRowView)parameter;
                double value = ValidationHelper.GetDouble(row["ShippingOptionCharge"], 0);
                int siteId = ValidationHelper.GetInteger(row["ShippingOptionSiteID"], 0);

                return CurrencyInfoProvider.GetFormattedPrice(value, siteId);
        }

        return parameter;
    }


    /// <summary>
    /// Handles the SiteSelector's selection changed event.
    /// </summary>
    protected void Selector_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWhereCondition();
        UniGrid.ReloadData();

        // Save selected value
        StoreSiteFilterValue(SelectSite.SiteID);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Creates where condition for UniGrid and reloads it.
    /// </summary>
    private void InitWhereCondition()
    {
        UniGrid.WhereCondition = SelectSite.GetSiteWhereCondition("ShippingOptionSiteID");
    }

    #endregion
}
