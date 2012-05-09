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
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_Products_Product_List : CMSProductsPage
{
    #region "Variables"

    protected string editToolTip = null;
    protected string deleteToolTip = null;
    protected bool isAdvancedMode = false;

    #endregion


    #region "Event Handlers"

    object gridData_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "publicstatusdisplayname":
                return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(Convert.ToString(parameter)));

            case "skuenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "skuprice":
                DataRowView row = (DataRowView)parameter;
                double value = ValidationHelper.GetDouble(row["SKUPrice"], 0);
                int siteId = ValidationHelper.GetInteger(row["SKUSiteID"], 0);

                return CurrencyInfoProvider.GetFormattedPrice(value, siteId);

            case "skusiteid":
                return UniGridFunctions.ColoredSpanYesNo(parameter == DBNull.Value);
        }

        return parameter;
    }


    void gridData_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("Product_Edit_Frameset.aspx?productid=" + ValidationHelper.GetInteger(actionArgument, 0) + "&siteId=" + SelectSite.SiteID);
                break;

            case "delete":
                int skuId = ValidationHelper.GetInteger(actionArgument, 0);
                SKUInfo skuObj = SKUInfoProvider.GetSKUInfo(skuId);

                // Check module permissions
                if (!ECommerceContext.IsUserAuthorizedToModifySKU(skuObj))
                {
                    if (skuObj.IsGlobal)
                    {
                        RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
                    }
                    else
                    {
                        RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyProducts");
                    }
                }

                // Check dependencies
                if (SKUInfoProvider.CheckDependencies(skuId))
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Ecommerce.DeleteDisabled");
                    return;
                }

                SKUInfoProvider.DeleteSKUInfo(skuObj);

                break;
        }
    }


    /// <summary>
    /// Site selection changed event handler.
    /// </summary>
    void Selector_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridData.WhereCondition = GetWhereCondition();
        gridData.ReloadData();

        // Save selected value
        StoreSiteFilterValue(SelectSite.SiteID);
    }


    /// <summary>
    /// Sets the advanced mode.
    /// </summary>
    protected void lnkShowAdvancedFilter_Click(object sender, EventArgs e)
    {
        isAdvancedMode = true;
        ViewState["IsAdvancedMode"] = isAdvancedMode;
        this.plcSimpleFilter.Visible = !isAdvancedMode;
        this.plcAdvancedFilter.Visible = isAdvancedMode;
        this.plcAdvancedFilterNumber.Visible = isAdvancedMode;
        this.plcAdvancedFilterStatuses.Visible = isAdvancedMode;
        this.plcAdvancedFilterAssignedToDocument.Visible = isAdvancedMode;
    }


    /// <summary>
    /// Sets the simple mode.
    /// </summary>
    protected void lnkShowSimpleFilter_Click(object sender, EventArgs e)
    {
        isAdvancedMode = false;
        ViewState["IsAdvancedMode"] = isAdvancedMode;
        this.plcSimpleFilter.Visible = !isAdvancedMode;
        this.plcAdvancedFilter.Visible = isAdvancedMode;
        this.plcAdvancedFilterNumber.Visible = isAdvancedMode;
        this.plcAdvancedFilterStatuses.Visible = isAdvancedMode;
        this.plcAdvancedFilterAssignedToDocument.Visible = isAdvancedMode;
    }

    #endregion


    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Init Unigrid
        gridData.OnAction += new OnActionEventHandler(gridData_OnAction);
        gridData.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridData_OnExternalDataBound);
        gridData.ZeroRowsText = GetString("Product_List.NoProductAvailable");

        // Init site selector
        SelectSite.Selector.SelectedIndexChanged += new EventHandler(Selector_SelectedIndexChanged);

        if (this.AllowGlobalObjects && ExchangeTableInfoProvider.IsExchangeRateFromGlobalMainCurrencyMissing(CMSContext.CurrentSiteID))
        {
            lblMissingRate.Visible = true;
        }

        if (!RequestHelper.IsPostBack())
        {
            // Init site selector
            SelectSite.SiteID = SiteFilterStartupValue;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        editToolTip = GetString("Product_List.EditToolTip");
        deleteToolTip = GetString("Product_List.DeleteToolTip");

        lblStoreStatus.Text = GetString("Product_List.StoreStatus");
        lblInternalStatus.Text = GetString("Product_List.InternalStatus");
        lblDepartment.Text = GetString("Product_List.Department");
        lblName.Text = GetString("Product_List.Name");
        lblNumber.Text = GetString("Product_List.Number");
        btnFilter.Text = GetString("general.show");

        // General UI
        this.lnkShowAdvancedFilter.Text = GetString("general.displayadvancedfilter");
        this.imgShowAdvancedFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        this.lnkShowSimpleFilter.Text = GetString("general.displaysimplefilter");
        this.imgShowSimpleFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortUp.png");

        EnsureFilterMode();

        this.plcSimpleFilter.Visible = !isAdvancedMode;
        this.plcAdvancedFilter.Visible = isAdvancedMode;
        this.plcAdvancedFilterNumber.Visible = isAdvancedMode;
        this.plcAdvancedFilterStatuses.Visible = isAdvancedMode;
        this.plcAdvancedFilterAssignedToDocument.Visible = isAdvancedMode;

        if (!RequestHelper.IsPostBack())
        {
            this.drpAssignedToDocument.Items.Add(new ListItem(this.GetString("general.selectall"), ""));
            this.drpAssignedToDocument.Items.Add(new ListItem(this.GetString("general.yes"), "1"));
            this.drpAssignedToDocument.Items.Add(new ListItem(this.GetString("general.no"), "0"));
        }

        // Fill department dropdownlist
        if ((CMSContext.CurrentUser != null) && !CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            departmentElem.UserID = CMSContext.CurrentUser.UserID;
        }

        // When global SKUs can be included in listing
        if (SelectSite.SiteID <= 0)
        {
            // Display global departments and global statuses too
            departmentElem.DisplayGlobalItems = true;
            publicStatusElem.AppendGlobalItems = true;
            internalStatusElem.AppendGlobalItems = true;
        }

        gridData.WhereCondition = GetWhereCondition();

        InitializeMasterPage();

        AddMenuButtonSelectScript("Products", "");
    }


    protected override void OnPreRender(EventArgs e)
    {
        bool both = (SelectSite.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD);

        // Hide header actions if (both) selected
        hdrActions.Enabled = !both;
        lblWarnNew.Visible = both;

        base.OnPreRender(e);
        gridData.NamedColumns["SKUSiteID"].Visible = AllowGlobalObjects;
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Initializes master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Set the master page actions element        
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Product_List.NewItem");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Products/Product_New.aspx?siteId=" + SelectSite.SiteID;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_SKU/add.png");

        this.hdrActions.Actions = actions;

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("Product_Edit_Header.ItemListLink");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_SKU/object.png");
        this.CurrentMaster.Title.HelpTopicName = "product_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.DisplaySiteSelectorPanel = AllowGlobalObjects;
    }


    private string GetWhereCondition()
    {
        string where = "";

        // Display ONLY products - not product options
        where += " (SKUOptionCategoryID IS NULL) AND ";

        // Value from txtName
        if (txtName.Text.Trim() != "")
        {
            where += "SKUName LIKE '%" + txtName.Text.Trim().Replace("'", "''") + "%' AND ";
        }

        // Value from txtNumber
        if (isAdvancedMode && (txtNumber.Text != ""))
        {
            where += "SKUNumber LIKE '%" + txtNumber.Text.Trim().Replace("'", "''") + "%' AND ";
        }

        // Product type
        if ((string)this.selectProductTypeElem.Value != "ALL")
        {
            // If standard product is selected
            if ((string)this.selectProductTypeElem.Value == SKUInfoProvider.GetSKUProductTypeString(SKUProductTypeEnum.Product))
            {
                where += String.Format("ISNULL(SKUProductType, 'PRODUCT') = '{0}' AND ", this.selectProductTypeElem.Value);
            }
            else
            {
                where += String.Format("SKUProductType = '{0}' AND ", this.selectProductTypeElem.Value);
            }
        }

        // Value from departmentElem
        if (departmentElem.DepartmentID > 0)
        {
            where += "(SKUDepartmentID=" + departmentElem.DepartmentID + ") AND ";
        }
        else if (departmentElem.DepartmentID == -5)
        {
            where += "(SKUDepartmentID IS NULL) AND ";
        }


        // Internal status value
        if (isAdvancedMode && (internalStatusElem.InternalStatusID > 0))
        {
            where += "InternalStatusID = " + internalStatusElem.InternalStatusID + " AND ";
        }

        // Store status value
        if (isAdvancedMode && (publicStatusElem.PublicStatusID > 0))
        {
            where += "PublicStatusID = " + publicStatusElem.PublicStatusID + " AND ";
        }

        // Assigned to document
        switch (this.drpAssignedToDocument.SelectedValue)
        {
            case "0":
                where += "SKUID NOT IN (SELECT NodeSKUID FROM View_CMS_Tree_Joined WHERE NodeSKUID IS NOT NULL) AND ";
                break;

            case "1":
                where += "SKUID IN (SELECT NodeSKUID FROM View_CMS_Tree_Joined WHERE NodeSiteID = " + CMSContext.CurrentSiteID + " AND NodeSKUID IS NOT NULL) AND ";
                break;
        }

        CurrentUserInfo cui = CMSContext.CurrentUser;
        if (cui != null)
        {
            if (cui.IsGlobalAdministrator || cui.IsAuthorizedPerResource("CMS.Ecommerce", "AccessAllDepartments"))
            {
                // Display products from all departments to Global administrator
                if (where != "")
                {
                    // Trim ending ' AND'
                    where = where.Remove(where.Length - 4);
                }
            }
            else
            {
                // Complete where
                where += "((SKUDepartmentID IS NULL) OR SKUDepartmentID IN (SELECT DepartmentID FROM COM_UserDepartment WHERE UserID=" + CMSContext.CurrentUser.UserID + "))";
            }

            where = "(" + where + ") AND (" + SelectSite.GetSiteWhereCondition("SKUSiteID") + ")";

            return where;
        }
        return null;
    }


    /// <summary>
    /// Ensures correct filter mode flag if filter mode was just changed.
    /// </summary>
    private void EnsureFilterMode()
    {
        if (URLHelper.IsPostback())
        {
            // Get current event target
            string uniqieId = ValidationHelper.GetString(Request.Params["__EVENTTARGET"], String.Empty);
            uniqieId = uniqieId.Replace("$", "_");

            // If postback was fired by mode switch, update isAdvancedMode variable
            if (uniqieId == lnkShowAdvancedFilter.ClientID)
            {
                isAdvancedMode = true;
            }
            else if (uniqieId == lnkShowSimpleFilter.ClientID)
            {
                isAdvancedMode = false;
            }
            else
            {
                isAdvancedMode = ValidationHelper.GetBoolean(ViewState["IsAdvancedMode"], false);
            }
        }
    }

    #endregion
}
