using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_ContactManagement_Pages_Tools_Contact_Subscriber_Contacts : CMSNewsletterSubscribersPage
{
    #region "Variables"

    private int mBounceLimit;
    private bool mBounceInfoAvailable;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {        
        // Check read permission
        ContactHelper.AuthorizedReadContact(CMSContext.CurrentSiteID, true);
        
        string siteName = CMSContext.CurrentSiteName;
        // Get bounce limit
        mBounceLimit = SettingsKeyProvider.GetIntValue(siteName + ".CMSBouncedEmailsLimit");
        // Get info if bounced e-mail tracking is available
        mBounceInfoAvailable = SettingsKeyProvider.GetBoolValue(siteName + ".CMSMonitorBouncedEmails") &&
                               NewsletterProvider.OnlineMarketingEnabled(siteName);

        CurrentMaster.HeaderActions.HelpTopicName = "subscribercontacts_tab";
        CurrentMaster.HeaderActions.HelpName = "helpTopic";

        // Check if parent object exist
        Subscriber sb = SubscriberProvider.GetSubscriber(QueryHelper.GetInteger("subscriberid", 0));
        EditedObject = sb;

        // Initialize unigrid
        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += uniGrid_OnExternalDataBound;
        UniGrid.WhereCondition = "ContactID IN (SELECT ContactGroupMemberRelatedID FROM OM_ContactGroupMember WHERE ContactGroupMemberContactGroupID = "
            + QueryHelper.GetInteger("groupid", 0) + " AND ContactGroupMemberType = 0) AND ContactSiteID = " + CMSContext.CurrentSiteID
            + " AND ContactMergedWithContactID IS NULL";
        UniGrid.ShowObjectMenu = false;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Hide columns with bounced emails if bounce info is not available
        UniGrid.GridView.Columns[0].Visible =
            UniGrid.NamedColumns["blocked"].Visible =
                UniGrid.NamedColumns["bounces"].Visible = mBounceInfoAvailable;
    }


    /// <summary>
    /// Unigrid external databound event handler.
    /// </summary>
    protected object uniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            // Show/hide block action button
            case "block":
                return SetBlockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            // Show/hide un-block action button
            case "unblock":
                return SetUnblockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            // Get if the contact is blocked
            case "blocked":
                return GetBlocked(parameter);

            // Get number of bounces
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
            // Block selected contact
            case "block": Block(actionArgument);
                break;

            // Un-block selected contact
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


    /// <summary>
    /// Block contact.
    /// </summary>
    private void Block(object actionArgument)
    {
        CheckAuthorization();
        SubscriberProvider.BlockContact(ValidationHelper.GetInteger(actionArgument, 0));
    }


    /// <summary>
    /// Un-block contact.
    /// </summary>
    private void Unblock(object actionArgument)
    {
        CheckAuthorization();
        SubscriberProvider.UnblockContact(ValidationHelper.GetInteger(actionArgument, 0));
    }


    /// <summary>
    /// Check manage permission.
    /// </summary>
    private static void CheckAuthorization()
    {
        // Check manage subscribers permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managesubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "managesubscribers");
        }

        // Check manage contacts permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.contactmanagement", "modifycontacts"))
        {
            RedirectToCMSDeskAccessDenied("cms.contactmanagement", "modifycontacts");
        }
    }


    /// <summary>
    /// Returns number of bounces from data row with contact data.
    /// </summary>
    private static int GetBouncesFromRow(DataRowView rowView)
    {
        return ValidationHelper.GetInteger(DataHelper.GetDataRowValue(rowView.Row, "ContactBounces"), 0);
    }

    #endregion
}