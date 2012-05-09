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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_ExchangeRates_ExchangeTable_List : CMSExchangeRatesPage
{
    #region "Variables"

    int mCurrentTableId = 0;

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.TitleText = GetString("ExchangeTable_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_ExchangeTable/object.png");
        this.CurrentMaster.Title.HelpTopicName = "exchange_rates_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 9];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("ExchangeTable_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("ExchangeTable_Edit.aspx?siteId=" + SiteID);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_ExchangeTable/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;

        FindCurrentTableID();

        gridElem.GridView.RowDataBound += new GridViewRowEventHandler(GridView_RowDataBound);
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.ZeroRowsText = GetString("general.nodatafound");

        // Configuring global records
        if (ConfiguredSiteID == 0)
        {
            // Select only global records
            gridElem.WhereCondition = "ExchangeTableSiteID IS NULL";
            // Show "using global settings" info message only if showing global store settings
            if (SiteID != 0)
            {
                lblGlobalInfo.Visible = true;
                lblGlobalInfo.Text = GetString("com.UsingGlobalSettings");
            }
        }
        else
        {
            // Select only site-specific records
            gridElem.WhereCondition = "ExchangeTableSiteID = " + ConfiguredSiteID;
        }
    }

    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("ExchangeTable_Edit.aspx?exchangeid=" + Convert.ToString(actionArgument) + "&siteId=" + SiteID);
        }
        else if (actionName == "delete")
        {
            CheckConfigurationModification();

            int tableId = ValidationHelper.GetInteger(actionArgument, 0);
            // Delete ExchangeTableInfo object from database
            ExchangeTableInfoProvider.DeleteExchangeTableInfo(tableId);

            // If current table deleted
            if (mCurrentTableId == tableId)
            {
                // Find new current table
                FindCurrentTableID();
            }
        }
    }


    /// <summary>
    /// Handles the databoud event (for coloring valid/invalid exchange rates)
    /// </summary>
    void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView drv = e.Row.DataItem as DataRowView;
        if (drv != null)
        {
            int tableId = ValidationHelper.GetInteger(drv.Row["ExchangeTableID"], 0);
            if (tableId == mCurrentTableId)
            {
                // Exchange rate which will be used for all operations is highlighted
                e.Row.Style.Add("background-color", "rgb(221, 250, 222)");
            }
        }
    }

    #endregion


    #region "Private methods"

    private void FindCurrentTableID()
    {
        mCurrentTableId = 0;

        // Get current table (it will be highlighted in the listing)
        ExchangeTableInfo eti = ExchangeTableInfoProvider.GetLastExchangeTableInfo(SiteID);
        if (eti != null)
        {
            mCurrentTableId = eti.ExchangeTableID;
        }
    }

    #endregion
}
