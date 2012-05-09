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

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_TaxClasses_TaxClass_List : CMSTaxClassesPage
{
    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (this.UseGlobalObjects && ExchangeTableInfoProvider.IsExchangeRateFromGlobalMainCurrencyMissing(CMSContext.CurrentSiteID))
        {
            lblMissingRate.Visible = true;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("TaxClass_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_TaxClass/object.png");
        this.CurrentMaster.Title.HelpTopicName = "tax_classes_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Prepare the new payment option header element
        string[,] actions = new string[2, 9];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("TaxClass_List.NewItemCaption");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Configuration/TaxClasses/TaxClass_New.aspx?siteId=" + SiteID;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_TaxClass/add.png");

        // Show copy from global link when not configuring global tax classes
        if (ConfiguredSiteID != 0)
        {
            // Show "Copy from global" link only if there is at least one global tax class
            DataSet ds = TaxClassInfoProvider.GetTaxClasses("TaxClassSiteID IS NULL", null, 1, "TaxClassID");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
                actions[1, 1] = GetString("general.copyfromglobal");
                actions[1, 2] = "return ConfirmCopyFromGlobal();";
                actions[1, 3] = null;
                actions[1, 4] = null;
                actions[1, 5] = GetImageUrl("Objects/Ecommerce_TaxClass/fromglobal.png");
                actions[1, 6] = "copyFromGlobal";
                actions[1, 7] = String.Empty;
                actions[1, 8] = true.ToString();

                // Register javascript to confirm action 
                string script = "function ConfirmCopyFromGlobal() {return confirm(" + ScriptHelper.GetString(GetString("com.ConfirmTaxClassFromGlobal")) + ");}";
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ConfirmCopyFromGlobal", ScriptHelper.GetScript(script));
            }
        }

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);

        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");

        // Configuring global records
        if (ConfiguredSiteID == 0)
        {
            // Select only global records
            UniGrid.WhereCondition = "TaxClassSiteID IS NULL";
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
            UniGrid.WhereCondition = "TaxClassSiteID = " + ConfiguredSiteID;
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
                UniGrid.ReloadData();
                break;
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("TaxClass_Frameset.aspx?taxclassid=" + Convert.ToString(actionArgument) + "&siteId=" + SiteID);
        }
        else if (actionName == "delete")
        {
            // Check permissions
            CheckConfigurationModification();

            TaxClassInfoProvider.DeleteTaxClassInfo(ValidationHelper.GetInteger(actionArgument, 0));
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    ///  Copies site-specific tax classes from global list.
    /// </summary>
    protected void CopyFromGlobal()
    {
        CheckConfigurationModification();
        TaxClassInfoProvider.CopyFromGlobal(ConfiguredSiteID);
    }

    #endregion
}
