using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_List : CMSProductOptionCategoriesPage
{
    #region "Page Events"


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Init Unigrid
        OptionCategoryGrid.OnAction += new OnActionEventHandler(OptionCategoryGrid_OnAction);
        OptionCategoryGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(OptionCategoryGrid_OnExternalDataBound);
        OptionCategoryGrid.ZeroRowsText = GetString("general.nodatafound");
        OptionCategoryGrid.OrderBy = "CategoryDisplayName ASC";

        // Init site selector
        SelectSite.Selector.SelectedIndexChanged += new EventHandler(Selector_SelectedIndexChanged);

        if (!RequestHelper.IsPostBack())
        {
            // Init site selector
            SelectSite.SiteID = SiteFilterStartupValue;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("OptionCategory_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("OptionCategory_New.aspx?siteId=" + SelectSite.SiteID);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_OptionCategory/add.png");
        this.hdrActions.Actions = actions;

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("optioncategory_edit.itemlistlink");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_OptionCategory/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "list_categories";
        this.CurrentMaster.DisplaySiteSelectorPanel = AllowGlobalObjects;

        OptionCategoryGrid.WhereCondition = SelectSite.GetSiteWhereCondition("CategorySiteID");

        AddMenuButtonSelectScript("ProductOptions", "");
    }


    protected override void OnPreRender(EventArgs e)
    {
        bool both = (SelectSite.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD);

        // Hide header actions if (both) selected
        hdrActions.Enabled = !both;
        lblWarnNew.Visible = both;

        base.OnPreRender(e);
        OptionCategoryGrid.NamedColumns["CategorySiteID"].Visible = AllowGlobalObjects;
    }

    #endregion


    #region "Event Handlers"

    protected object OptionCategoryGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "categoryselectiontype":
                switch (ValidationHelper.GetString(parameter, ""))
                {
                    case "dropdown":
                        return GetString("OptionCategory_List.DropDownList");

                    case "checkboxhorizontal":
                        return GetString("OptionCategory_List.checkboxhorizontal");

                    case "checkboxvertical":
                        return GetString("OptionCategory_List.checkboxvertical");

                    case "radiobuttonhorizontal":
                        return GetString("OptionCategory_List.radiobuttonhorizontal");

                    case "radiobuttonvertical":
                        return GetString("OptionCategory_List.radiobuttonvertical");

                    case "textbox":
                        return GetString("optioncategory_selectiontype.textbox");

                    case "textarea":
                        return GetString("optioncategory_selectiontype.textarea");

                }
                break;

            case "categoryenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "categorysiteid":
                return UniGridFunctions.ColoredSpanYesNo(parameter == DBNull.Value);
        }

        return parameter;
    }


    /// <summary>
    /// Handles the OptionCategoryGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void OptionCategoryGrid_OnAction(string actionName, object actionArgument)
    {
        int categoryId = ValidationHelper.GetInteger(actionArgument, 0);

        // Set actions
        if (actionName == "edit")
        {
            URLHelper.Redirect("OptionCategory_Edit.aspx?CategoryID=" + categoryId + "&siteId=" + SelectSite.SiteID);
        }
        else if (actionName == "delete")
        {
            OptionCategoryInfo categoryObj = OptionCategoryInfoProvider.GetOptionCategoryInfo(categoryId);

            if (!ECommerceContext.IsUserAuthorizedToModifyOptionCategory(categoryObj))
            {
                // Check module permissions
                if (categoryObj.CategoryIsGlobal)
                {
                    RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
                }
                else
                {
                    RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyProducts");
                }
            }

            // Check dependencies
            if (OptionCategoryInfoProvider.CheckDependencies(categoryId))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }

            // Delete option category from database
            OptionCategoryInfoProvider.DeleteOptionCategoryInfo(categoryObj);
        }
    }


    /// <summary>
    /// Handles the SiteSelector's selection changed event.
    /// </summary>
    void Selector_SelectedIndexChanged(object sender, EventArgs e)
    {
        OptionCategoryGrid.WhereCondition = SelectSite.GetSiteWhereCondition("CategorySiteID");
        OptionCategoryGrid.ReloadData();

        // Save selected value
        StoreSiteFilterValue(SelectSite.SiteID);
    }

    #endregion
}
