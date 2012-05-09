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

public partial class CMSModules_Ecommerce_Pages_Tools_Suppliers_Supplier_List : CMSSuppliersPage
{
    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Init Unigrid
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.ZeroRowsText = GetString("general.nodatafound");

        // Init site selector
        SelectSite.Selector.SelectedIndexChanged += new EventHandler(Selector_SelectedIndexChanged);

        if (!RequestHelper.IsPostBack())
        {
            SelectSite.SiteID = SiteFilterStartupValue;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // New item link
        string[,] actions = new string[1, 7];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("supplier_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Supplier_Edit.aspx?siteId=" + SelectSite.SiteID);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_Supplier/add.png");
        this.hdrActions.Actions = actions;

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("supplier_Edit.ItemListLink");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Supplier/object.png");
        this.CurrentMaster.Title.HelpTopicName = "suppliers_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.DisplaySiteSelectorPanel = AllowGlobalObjects;

        InitWhereCondition();

        // Try to select button in menu
        AddMenuButtonSelectScript("Suppliers", "");
    }


    protected override void OnPreRender(EventArgs e)
    {
        bool both = (SelectSite.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD);

        // Hide header actions if (both) selected
        hdrActions.Enabled = !both;
        lblWarnNew.Visible = both;

        base.OnPreRender(e);

        gridElem.NamedColumns["SupplierSiteID"].Visible = AllowGlobalObjects;
    }

    #endregion


    #region "Event Handlers"

    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "suppenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "suppsiteid":
                return UniGridFunctions.ColoredSpanYesNo(parameter == DBNull.Value);
        }
        return parameter;
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
            URLHelper.Redirect("Supplier_Edit.aspx?supplierid=" + Convert.ToString(actionArgument) + "&siteId=" + SelectSite.SiteID);
        }
        else if (actionName == "delete")
        {
            int supplierId = ValidationHelper.GetInteger(actionArgument, 0);
            SupplierInfo supplierObj = SupplierInfoProvider.GetSupplierInfo(supplierId);

            // Check module permissions
            if (!ECommerceContext.IsUserAuthorizedToModifySupplier(supplierObj))
            {
                if (supplierObj.IsGlobal)
                {
                    RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
                }
                else
                {
                    RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifySuppliers");
                }
            }

            if (SupplierInfoProvider.CheckDependencies(supplierId))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }

            // Delete SupplierInfo object from database
            SupplierInfoProvider.DeleteSupplierInfo(supplierObj);
            InitWhereCondition();
        }
    }


    /// <summary>
    /// Selection changed event.
    /// </summary>
    void Selector_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWhereCondition();
        gridElem.ReloadData();

        // Save selected value
        StoreSiteFilterValue(SelectSite.SiteID);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Creates where condition for UniGrid.
    /// </summary>
    private void InitWhereCondition()
    {
        gridElem.WhereCondition = SelectSite.GetSiteWhereCondition("SupplierSiteID");
    }

    #endregion
}
