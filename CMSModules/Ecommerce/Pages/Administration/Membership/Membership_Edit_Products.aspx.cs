using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.Ecommerce;
using System.Data;

public partial class CMSModules_Ecommerce_Pages_Administration_Membership_Membership_Edit_Products : CMSMembershipPage
{
    #region "Variables"

    private int membershipID;
    private bool isSiteManager;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query parameters
        this.membershipID = QueryHelper.GetInteger("membershipid", 0);
        this.isSiteManager = (QueryHelper.GetInteger("siteid", 0) == 0);

        // Get membership
        MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(this.membershipID);

        EditedObject = mi;

        // Check permissions
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            if (mi != null)
            {
                if (mi.MembershipSiteID != CMSContext.CurrentSiteID)
                {
                    this.RedirectToAccessDenied(GetString("general.actiondenied"));
                }
            }
        }

        // Setup where condition
        string where = String.Empty;

        // Products associated with this membership
        where = SqlHelperClass.AddWhereCondition(where, String.Format("SKUMembershipGUID = '{0}'", mi.MembershipGUID));

        // Set where condition
        this.productsUniGridElem.WhereCondition = where;
    }

    #endregion


    #region "Methods"

    protected object productsUniGridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView row;

        switch (sourceName.ToLower())
        {
            case "skuprice":
                row = (DataRowView)parameter;

                // Get information needed to format SKU price
                double value = ValidationHelper.GetDouble(row["SKUPrice"], 0);
                int siteId = ValidationHelper.GetInteger(row["SKUSiteID"], 0);

                // Return formatted SKU price
                return CurrencyInfoProvider.GetFormattedPrice(value, siteId);

            case "skuvalidity":
                row = (DataRowView)parameter;

                // Get information needed to format SKU validity
                ValidityEnum validity = DateTimeHelper.GetValidityEnum(ValidationHelper.GetString(row["SKUValidity"], null));
                int validFor = ValidationHelper.GetInteger(row["SKUValidFor"], 0);
                DateTime validUntil = ValidationHelper.GetDateTime(row["SKUValidUntil"], DateTimeHelper.ZERO_TIME);

                // Return formatted SKU validity
                return DateTimeHelper.GetFormattedValidity(validity, validFor, validUntil);
        }
        return null;
    }

    #endregion
}

