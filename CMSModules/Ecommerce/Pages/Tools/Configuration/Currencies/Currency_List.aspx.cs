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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_Currencies_Currency_List : CMSCurrenciesPage
{
    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Page title
        this.CurrentMaster.Title.TitleText = GetString("Currency_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Currency/object.png");
        this.CurrentMaster.Title.HelpTopicName = "currencies_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[2, 9];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Currency_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Currency_Edit.aspx?siteId=" + SiteID);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_Currency/add.png");

        // Show copy from global link when not configuring global currencies.
        if (ConfiguredSiteID != 0)
        {
            // Show "Copy from global" link only if there is at least one global currency
            DataSet ds = CurrencyInfoProvider.GetCurrencies("CurrencySiteID IS NULL", null, 1, "CurrencyID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
                actions[1, 1] = GetString("general.copyfromglobal");
                actions[1, 2] = "return ConfirmCopyFromGlobal();";
                actions[1, 3] = null;
                actions[1, 4] = null;
                actions[1, 5] = GetImageUrl("Objects/Ecommerce_Currency/fromglobal.png");
                actions[1, 6] = "copyFromGlobal";
                actions[1, 7] = String.Empty;
                actions[1, 8] = true.ToString();

                // Register javascript to confirm generate 
                string script = "function ConfirmCopyFromGlobal() {return confirm(" + ScriptHelper.GetString(GetString("com.ConfirmCurrencyFromGlobal")) + ");}";
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ConfirmCopyFromGlobal", ScriptHelper.GetScript(script));
            }
        }

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);

        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.ZeroRowsText = GetString("general.nodatafound");

        // Configuring global records
        if (ConfiguredSiteID == 0)
        {
            // Select only global records
            gridElem.WhereCondition = "CurrencySiteID IS NULL";
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
            gridElem.WhereCondition = "CurrencySiteID = " + ConfiguredSiteID;
        }
    }

    #endregion


    #region "Event Handlers"

    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "copyfromglobal":
                CopyFromGlobal();
                gridElem.ReloadData();
                break;
        }
    }


    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "currismain":
                return UniGridFunctions.ColoredSpanYes(parameter);
            case "currenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
        }
        return "";
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("Currency_Edit.aspx?currencyid=" + Convert.ToString(actionArgument) + "&siteId=" + SiteID);
        }
        else if (actionName == "delete")
        {
            // Check permissions
            CheckConfigurationModification();

            int currencyId = ValidationHelper.GetInteger(actionArgument, 0);

            if (CurrencyInfoProvider.CheckDependencies(currencyId))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }

            CurrencyInfo ci = CurrencyInfoProvider.GetCurrencyInfo(currencyId);

            if (ci != null)
            {
                // An attempt to delete main currency
                if (ci.CurrencyIsMain)
                {
                    lblError.Visible = true;
                    lblError.Text = string.Format(GetString("com.deletemaincurrencyerror"), ci.CurrencyDisplayName);
                    return;
                }

                // Delete CurrencyInfo object from database
                CurrencyInfoProvider.DeleteCurrencyInfo(ci);
            }
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    ///  Copies site-specific currencies from global currency list.
    /// </summary>
    protected void CopyFromGlobal()
    {
        CheckConfigurationModification();
        CurrencyInfoProvider.CopyFromGlobal(ConfiguredSiteID);
    }

    #endregion
}
