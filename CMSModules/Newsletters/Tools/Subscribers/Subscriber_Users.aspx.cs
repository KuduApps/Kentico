using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Subscribers_Subscriber_Users : CMSNewsletterSubscribersPage
{
    #region "Variables"

    private int mBounceLimit;


    private bool mBounceInfoAvailable;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        string siteName = CMSContext.CurrentSiteName;
        // Get bounce limit
        mBounceLimit = SettingsKeyProvider.GetIntValue(siteName + ".CMSBouncedEmailsLimit");
        // Get info if bounced e-mail tracking is available
        mBounceInfoAvailable = SettingsKeyProvider.GetBoolValue(siteName + ".CMSMonitorBouncedEmails") &&
                               NewsletterProvider.OnlineMarketingEnabled(siteName);

        CurrentMaster.HeaderActions.HelpTopicName = "subscriberusers_tab";
        CurrentMaster.HeaderActions.HelpName = "helpTopic";

        // Check if parent object exist
        Subscriber sb = SubscriberProvider.GetSubscriber(QueryHelper.GetInteger("subscriberid", 0));
        EditedObject = sb;

        // Initialize unigrid
        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += uniGrid_OnExternalDataBound;
        UniGrid.WhereCondition = "((RoleID = " + QueryHelper.GetInteger("roleid", 0) + ") AND (SiteID = " + CMSContext.CurrentSiteID + "))";
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Hide columns with bounced emails if bounce info is not available
        UniGrid.GridView.Columns[0].Visible =
            UniGrid.GridView.Columns[3].Visible =
                UniGrid.GridView.Columns[4].Visible = mBounceInfoAvailable;
    }


    /// <summary>
    /// Unigrid external databound event handler.
    /// </summary>
    protected object uniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "block": 
                return SetBlockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            case "unblock": 
                return SetUnblockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            case "blocked": 
                return GetBlocked(parameter);

            case "bounces": 
                return GetBounces(parameter);

            default: 
                return null;
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "block": Block(actionArgument);
                break;

            case "unblock": Unblock(actionArgument);
                break;
        }
    }


    private object SetBlockAction(object sender, DataRowView rowView)
    {
        (sender as ImageButton).Visible =
            (GetBouncesFromRow(rowView) < mBounceLimit) && mBounceInfoAvailable;

        return null;
    }


    private object SetUnblockAction(object sender, DataRowView rowView)
    {
        (sender as ImageButton).Visible =
            (GetBouncesFromRow(rowView) >= mBounceLimit) && mBounceInfoAvailable;

        return null;
    }


    private string GetBlocked(object parameter)
    {
        // Do not handle if bounce email monitoring is not available
        if (!mBounceInfoAvailable)
        {
            return null;
        }

        // If bounce limit is not a natural number, then the feature is considered disabled
        if (mBounceLimit <= 0)
        {
            return UniGridFunctions.ColoredSpanYesNoReversed(false);
        }

        return UniGridFunctions.ColoredSpanYesNoReversed(ValidationHelper.GetInteger(parameter, 0) >= mBounceLimit);
    }


    private string GetBounces(object parameter)
    {
        // Do not handle if bounce email monitoring is not available
        if (!mBounceInfoAvailable)
        {
            return null;
        }

        int bounces = ValidationHelper.GetInteger(parameter, 0);

        if (bounces == 0 || bounces == int.MaxValue)
        {
            return string.Empty;
        }

        return bounces.ToString();
    }


    private void Block(object actionArgument)
    {
        CheckAuthorization();
        SubscriberProvider.BlockUser(ValidationHelper.GetInteger(actionArgument, 0));
    }


    private void Unblock(object actionArgument)
    {
        CheckAuthorization();
        SubscriberProvider.UnblockUser(ValidationHelper.GetInteger(actionArgument, 0));
    }


    private static void CheckAuthorization()
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managesubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "managesubscribers");
        }
    }


    private static int GetBouncesFromRow(DataRowView rowView)
    {
        return ValidationHelper.GetInteger(DataHelper.GetDataRowValue(rowView.Row, "UserBounces"), 0);
    }

    #endregion
}