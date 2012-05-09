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

public partial class CMSModules_Ecommerce_Pages_Tools_Customers_Customer_List : CMSCustomersPage
{
    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Init Unigrid
        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");
        
        string anoWhere = "(CustomerUserID IS NULL) AND CustomerSiteID = " + CMSContext.CurrentSiteID;
        string regWhere = string.Format("(CustomerUserID IS NOT NULL) AND (CustomerUserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID = {0}))", CMSContext.CurrentSiteID);
        UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(anoWhere, regWhere, "OR");

        // Check if user is global administrator
        if (CurrentUser.IsGlobalAdministrator)
        {
            // Display customers without site binding
            UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(UniGrid.WhereCondition, "(CustomerUserID IS NULL) AND (CustomerSiteID IS NULL)", "OR");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeMasterPage();

        // Set master title
        this.CurrentMaster.Title.TitleText = GetString("Customers_Edit.ItemListLink");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Ecommerce_Customer/object.png");
        this.CurrentMaster.Title.HelpTopicName = "customers_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        AddMenuButtonSelectScript("Customers", "");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Initializes properties of the master page.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Initialize the new customer element
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Customers_List.NewItemCaption");
        actions[0, 3] = "~/CMSModules/Ecommerce/Pages/Tools/Customers/Customer_New.aspx";
        actions[0, 5] = GetImageUrl("Objects/Ecommerce_Customer/add.png");

        this.hdrActions.Actions = actions;
    }

    #endregion


    #region "Events"

    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "custenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "statedisplayname":
            case "countrydisplayname":
                return ResHelper.LocalizeString(Convert.ToString(parameter));

            case "customeruserid":
                return UniGridFunctions.ColoredSpanYesNo(parameter != DBNull.Value);

            case "hassitebinding":
                // Has site binding action
                {
                    ImageButton button = ((ImageButton)sender);

                    if (!CurrentUser.IsGlobalAdministrator)
                    {
                        button.Visible = false;
                    }
                    else
                    {
                        button.OnClientClick = "return false;";
                        button.Style.Add("cursor", "default");

                        // Show warning for customers not bound to site
                        int userId = ValidationHelper.GetInteger(((DataRowView)((GridViewRow)parameter).DataItem).Row["CustomerUserID"], 0);
                        int siteId = ValidationHelper.GetInteger(((DataRowView)((GridViewRow)parameter).DataItem).Row["CustomerSiteID"], 0);
                        button.Visible = (userId == 0) && (siteId == 0);
                    }
                }
                break;
        }

        return parameter;
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
            URLHelper.Redirect("Customer_Edit_Frameset.aspx?customerid=" + Convert.ToString(actionArgument));
        }
        else if (actionName == "delete")
        {
            int customerId = ValidationHelper.GetInteger(actionArgument, 0);

            // Check module permissions
            if (!ECommerceContext.IsUserAuthorizedToModifyCustomer())
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyCustomers");
                return;
            }

            // Check customers dependencies
            if (CustomerInfoProvider.CheckDependencies(customerId))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Ecommerce.DeleteDisabled");
                return;
            }

            // Delete CustomerInfo object from database
            CustomerInfoProvider.DeleteCustomerInfo(customerId);

            UniGrid.ReloadData();
        }
    }

    #endregion
}
