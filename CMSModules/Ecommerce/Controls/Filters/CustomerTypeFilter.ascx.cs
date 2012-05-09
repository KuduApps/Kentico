using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_Filters_CustomerTypeFilter : CMSAbstractDataFilterControl
{
    #region "Constants"

    protected const string CUSTOMERS_ALL = "all";
    protected const string CUSTOMERS_ANONYMOUS = "ano";
    protected const string CUSTOMERS_REGISTERED = "reg";

    #endregion


    #region "Properties"

    public override string WhereCondition
    {
        get
        {
            return GetWhereCondition();
        }
    }

    #endregion


    #region "Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Fill customer type selector
        ddlCustomerType.Items.Add(new ListItem(ResHelper.GetString("general.selectall"), CUSTOMERS_ALL));
        ddlCustomerType.Items.Add(new ListItem(ResHelper.GetString("general.yes"), CUSTOMERS_REGISTERED));
        ddlCustomerType.Items.Add(new ListItem(ResHelper.GetString("general.no"), CUSTOMERS_ANONYMOUS));
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion


    #region "Methods"

    public string GetWhereCondition()
    {
        string where = "";

        string anoWhere = "(CustomerUserID IS NULL) AND CustomerSiteID = " + CMSContext.CurrentSiteID;
        string regWhere = string.Format("(CustomerUserID IS NOT NULL) AND (CustomerUserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID = {0}))", CMSContext.CurrentSiteID);
        switch (ddlCustomerType.SelectedValue)
        {
            case CUSTOMERS_ANONYMOUS:
                where = anoWhere;
                break;

            case CUSTOMERS_ALL:
                where = "(" + SqlHelperClass.AddWhereCondition(regWhere, anoWhere, "OR") + ")";

                // Check if user is global administrator
                if (CMSContext.CurrentUser.IsGlobalAdministrator)
                {
                    // Display customers without site binding
                    where = SqlHelperClass.AddWhereCondition(where, "(CustomerUserID IS NULL) AND (CustomerSiteID IS NULL)", "OR");
                }

                break;

            case CUSTOMERS_REGISTERED:
                where = regWhere;
                break;

            default:
                where = "(1=0)";
                break;
        }

        return "(" + where + ")";
    }

    #endregion
}
