using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Newsletters_Tools_Subscribers_Subscriber_List : CMSNewsletterSubscribersPage
{
    #region "Variables"

    private int mBounceLimit;


    private bool mBounceInfoAvailable;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        SiteInfo currentSite = CMSContext.CurrentSite;
        mBounceLimit = SettingsKeyProvider.GetIntValue(currentSite.SiteName + ".CMSBouncedEmailsLimit");
        mBounceInfoAvailable = SettingsKeyProvider.GetBoolValue(currentSite.SiteName + ".CMSMonitorBouncedEmails") &&
                               NewsletterProvider.OnlineMarketingEnabled(currentSite.SiteName);                               

        // Add subscriber link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Subscriber_List.NewItemCaption");
        actions[0, 3] = ResolveUrl("Subscriber_New.aspx");
        actions[0, 5] = GetImageUrl("Objects/Newsletter_Subscriber/add.png");

        CurrentMaster.HeaderActions.Actions = actions;

        // Initialize unigrid
        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += uniGrid_OnExternalDataBound;
        UniGrid.WhereCondition = "SubscriberSiteID = " + currentSite.SiteID;        
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Hide columns with bounced emails if bounce info is not available
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
            case "block":
                return SetBlockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            case "unblock":
                return SetUnblockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            case "email":
                return GetEmail(parameter as DataRowView);

            case "blocked":
                return GetBlocked(parameter as DataRowView);

            case "bounces":
                return GetBounces(parameter as DataRowView);

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
            // Edit subscriber
            case "edit":
                Edit(actionArgument);
                break;

            // Delete subscriber
            case "delete":
                Delete(actionArgument);
                break;

            // Block subscriber
            case "block":
                Block(actionArgument);
                break;

            // Un-block subscriber
            case "unblock":
                Unblock(actionArgument);
                break;
        }
    }


    /// <summary>
    /// Displays/hides block action button in unigrid.
    /// </summary>
    private object SetBlockAction(object sender, DataRowView rowView)
    {        
        (sender as ImageButton).Visible =
            (GetBouncesFromRow(rowView) < mBounceLimit) && mBounceInfoAvailable && !IsRoleORContactGroupSubscriber(rowView);

        return null;
    }


    /// <summary>
    /// Displays/hides un-block action button in unigrid.
    /// </summary>
    private object SetUnblockAction(object sender, DataRowView rowView)
    {
        (sender as ImageButton).Visible =
            (GetBouncesFromRow(rowView) >= mBounceLimit) && mBounceInfoAvailable && !IsRoleORContactGroupSubscriber(rowView);

        return null;
    }


    /// <summary>
    /// Returns subscriber's e-mail address.
    /// </summary>
    private string GetEmail(DataRowView rowView)
    {        
        string email = ValidationHelper.GetString(DataHelper.GetDataRowValue(rowView.Row, "SubscriberEmail"), string.Empty);

        if (!string.IsNullOrEmpty(email))
        {
            return email;
        }
        else
        {
            return ValidationHelper.GetString(DataHelper.GetDataRowValue(rowView.Row, "Email"), string.Empty);
        }
    }


    /// <summary>
    /// Returns colored yes/no or nothing according to subscriber's blocked info.
    /// </summary>
    private string GetBlocked(DataRowView rowView)
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

        if (IsRoleORContactGroupSubscriber(rowView))
        {
            return null;
        }

        return UniGridFunctions.ColoredSpanYesNoReversed(GetBouncesFromRow(rowView) >= mBounceLimit);
    }


    /// <summary>
    /// Returns number of bounces or nothing according to subscriber's bounce info.
    /// </summary>
    private string GetBounces(DataRowView rowView)
    {
        // Do not handle if bounce email monitoring is not available
        if (!mBounceInfoAvailable)
        {
            return null;
        }

        int bounces = GetBouncesFromRow(rowView);

        if (bounces == 0 || bounces == int.MaxValue || IsRoleORContactGroupSubscriber(rowView))
        {
            return null;
        }

        return bounces.ToString();
    }


    /// <summary>
    /// Edit selected subscriber.
    /// </summary>
    private void Edit(object actionArgument)
    {
        URLHelper.Redirect("Subscriber_Frameset.aspx?subscriberid=" + ValidationHelper.GetString(actionArgument, string.Empty));
    }


    /// <summary>
    /// Delete selected subscriber.
    /// </summary>
    private void Delete(object actionArgument)
    {
        CheckAuthorization();        
        SubscriberProvider.DeleteSubscriber(ValidationHelper.GetInteger(actionArgument, 0));
    }


    /// <summary>
    /// Block selected subscriber.
    /// </summary>
    private void Block(object actionArgument)
    {
        CheckAuthorization();
        SubscriberProvider.BlockSubscriber(ValidationHelper.GetInteger(actionArgument, 0));        
    }


    /// <summary>
    /// Un-block selected subscriber.
    /// </summary>
    private void Unblock(object actionArgument)
    {
        CheckAuthorization();
        SubscriberProvider.UnblockSubscriber(ValidationHelper.GetInteger(actionArgument, 0));
    }


    /// <summary>
    /// Checks if the user has permission to manage subscribers.
    /// </summary>
    private static void CheckAuthorization()
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managesubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "managesubscribers");
        }
    }


    /// <summary>
    /// Returns if type of the subscriber is "cms.role" or "om.contactgroup".
    /// </summary>
    private static bool IsRoleORContactGroupSubscriber(DataRowView rowView)
    {
        string type = ValidationHelper.GetString(DataHelper.GetDataRowValue(rowView.Row, "SubscriberType"), string.Empty);
        return (type.Equals(SiteObjectType.ROLE, StringComparison.InvariantCultureIgnoreCase) || type.Equals("om.contactgroup", StringComparison.InvariantCultureIgnoreCase));
    }


    /// <summary>
    /// Returns number of bounces of the subscriber.
    /// </summary>
    private static int GetBouncesFromRow(DataRowView rowView)
    {
        return ValidationHelper.GetInteger(DataHelper.GetDataRowValue(rowView.Row, "SubscriberBounces"), 0);
    }

    #endregion
}