using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Newsletters_Controls_SubscriberBlockedFilter : CMSAbstractBaseFilterControl
{
    #region "Constants"

    private const string All = "ALL";


    private const string Active = "ACTIVE";


    private const string Blocked = "BLOCKED";

    #endregion


    #region "Variables"

    private int mBounceLimit;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            base.WhereCondition = GenerateWhereCondition();
            return base.WhereCondition;
        }
        set
        {
            base.WhereCondition = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        mBounceLimit = SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSBouncedEmailsLimit");

        if (!RequestHelper.IsPostBack())
        {
            Reload();
        }
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    protected void Reload()
    {
        ddlBounceFilter.Items.Clear();
        ddlBounceFilter.Items.Add(new ListItem(ResHelper.GetString("general.selectall"), All));
        ddlBounceFilter.Items.Add(new ListItem(ResHelper.GetString("newsletter.filter.bouncedemails.active"), Active));
        ddlBounceFilter.Items.Add(new ListItem(ResHelper.GetString("newsletter.filter.bouncedemails.blocked"), Blocked));
    }


    /// <summary>
    /// Generates WHERE condition.
    /// </summary>
    private string GenerateWhereCondition()
    {
        if (mBounceLimit <= 0)
        {
            return null;
        }

        switch (ddlBounceFilter.SelectedValue)
        {
            case All:
                return null;

            case Active:
                return string.Format("(((SubscriberType = '{0}' OR SubscriberType IS NULL) AND (SubscriberBounces IS NULL OR SubscriberBounces < {1})) OR (SubscriberType = '{2}') OR (SubscriberType = '{3}'))",
                    SiteObjectType.USER, mBounceLimit, SiteObjectType.ROLE, PredefinedObjectType.CONTACTGROUP);

            case Blocked:
                return string.Format("((SubscriberType IS NULL OR SubscriberType = '{0}') AND (SubscriberBounces >= {1}))", SiteObjectType.USER, mBounceLimit);

            default:
                return null;
        }
    }

    #endregion
}