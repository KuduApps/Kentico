using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Membership_Controls_MyMemberships : CMSAdminControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public int UserID
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets the URL of 'buy membership' page.
    /// </summary>
    public string BuyMembershipURL
    {
        get
        {
            return this.btnBuyMembership.PostBackUrl;
        }
        set
        {
            this.btnBuyMembership.PostBackUrl = URLHelper.ResolveUrl(value);
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.membershipsGrid.StopProcessing = this.StopProcessing;

        // Setup where condition
        string where = String.Empty;

        // Get memberships for current user
        where = SqlHelperClass.AddWhereCondition(where, String.Format("UserID = {0}", this.UserID));

        // Set where condition
        this.membershipsGrid.WhereCondition = where;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show buy membership button if it has URL
        this.btnBuyMembership.Visible = !String.IsNullOrEmpty(this.btnBuyMembership.PostBackUrl);
    }

    #endregion


    #region "Methods"

    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        switch (propertyName.ToLower())
        {
            case "userid":
                this.UserID = ValidationHelper.GetInteger(value, 0);
                break;

            case "buymembershipurl":
                this.BuyMembershipURL = ValidationHelper.GetString(value, null);
                break;
        }
    }


    protected object membershipsUniGridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "membershipname":
                MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(ValidationHelper.GetInteger(parameter, 0));

                if (mi != null)
                {
                    return mi.MembershipDisplayName;
                }
                break;

            case "validto":
                DateTime validTo = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);

                // Format unlimited membership
                if (validTo.CompareTo(DateTimeHelper.ZERO_TIME) == 0)
                {
                    return "-";
                }
                break;
        }

        return parameter;
    }

    #endregion
}
