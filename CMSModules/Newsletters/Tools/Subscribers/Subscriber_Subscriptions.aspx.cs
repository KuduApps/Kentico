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
using System.Collections.Generic;

using CMS.Newsletter;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Newsletters_Tools_Subscribers_Subscriber_Subscriptions : CMSNewsletterSubscribersPage
{
    protected int subscriberId = 0;
    protected bool isRoleORContactGroup = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        subscriberId = QueryHelper.GetInteger("subscriberid", 0);
        // Get subscriber by its ID and check its existence
        Subscriber subscriberObj = SubscriberProvider.GetSubscriber(subscriberId);
        EditedObject = subscriberObj;

        // Check if it is role or contact group subscriber
        isRoleORContactGroup = (subscriberObj.SubscriberRelatedID > 0) && (subscriberObj.SubscriberType != null) &&
            (subscriberObj.SubscriberType.Equals(SiteObjectType.ROLE, StringComparison.InvariantCultureIgnoreCase) ||
            subscriberObj.SubscriberType.Equals(PredefinedObjectType.CONTACTGROUP, StringComparison.InvariantCultureIgnoreCase));

        // Initialize page controls
        CurrentMaster.DisplayActionsPanel = true;
        CurrentMaster.DisplayControlsPanel = true;
        chkRequireOptIn.TextAlign = TextAlign.Right;
        chkSendConfirmation.TextAlign = TextAlign.Right;

        // Initialize uniselector for newsletters
        selectNewsletter.UniSelector.SelectionMode = SelectionModeEnum.MultipleButton;
        selectNewsletter.UniSelector.OnItemsSelected += new EventHandler(UniSelector_OnItemsSelected);
        selectNewsletter.UniSelector.ReturnColumnName = "NewsletterID";
        selectNewsletter.UniSelector.ButtonImage = GetImageUrl("Objects/Newsletter_Newsletter/add.png");
        selectNewsletter.ImageDialog.CssClass = "NewItemImage";
        selectNewsletter.LinkDialog.CssClass = "NewItemLink";
        selectNewsletter.ShowSiteFilter = false;
        selectNewsletter.ResourcePrefix = "newsletterselect";
        selectNewsletter.DialogButton.CssClass = "LongButton";
        selectNewsletter.IsLiveSite = false;

        // Initialize unigrid
        unigridNewsletters.WhereCondition = "SubscriberID = " + subscriberId.ToString();
        unigridNewsletters.OnAction += new OnActionEventHandler(unigridNewsletters_OnAction);
        unigridNewsletters.OnExternalDataBound += new OnExternalDataBoundEventHandler(unigridNewsletters_OnExternalDataBound);

        // Initialize mass actions
        if (drpActions.Items.Count == 0)
        {
            drpActions.Items.Add(new ListItem(GetString("general.selectaction"), "SELECT"));
            drpActions.Items.Add(new ListItem(GetString("general.approve"), "APPROVE"));
            drpActions.Items.Add(new ListItem(GetString("general.reject"), "REJECT"));
            drpActions.Items.Add(new ListItem(GetString("general.remove"), "REMOVE"));
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Display/hide mass action dialog under the unigrid
        pnlActions.Visible = !DataHelper.DataSourceIsEmpty(unigridNewsletters.GridView.DataSource);
        // Display/hide double opt-in option
        plcRequireOptIn.Visible = !isRoleORContactGroup;
    }


    /// <summary>
    /// Uniselector item selected event handler.
    /// </summary>
    protected void UniSelector_OnItemsSelected(object sender, EventArgs e)
    {
        // Check permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "ManageSubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "ManageSubscribers");
        }

        // Get new items from selector
        string newValues = ValidationHelper.GetString(selectNewsletter.Value, null);
        string[] newItems = null;
        SubscriberNewsletterInfo subscription = null;
        Newsletter newsletter = null;
        int newsletterId = 0;

        newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (newItems != null)
        {
            // Get all selected newsletters
            foreach (string item in newItems)
            {
                newsletterId = ValidationHelper.GetInteger(item, 0);

                // Get subscription
                subscription = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriberId, newsletterId);

                // If not already subscribed
                if (subscription == null)
                {
                    newsletter = NewsletterProvider.GetNewsletter(newsletterId);

                    // Subscribe role
                    if (isRoleORContactGroup)
                    {
                        SubscriberProvider.Subscribe(subscriberId, newsletterId, DateTime.Now, chkSendConfirmation.Checked, false);
                    }
                    // Subscribe users and subscribers
                    else
                    {
                        SubscriberProvider.Subscribe(subscriberId, newsletterId, DateTime.Now, chkSendConfirmation.Checked, chkRequireOptIn.Checked);
                    }
                }
            }
        }

        selectNewsletter.Value = null;
        unigridNewsletters.ReloadData();
        pnlUpdate.Update();
    }


    /// <summary>
    /// Unigrid databound handler.
    /// </summary>
    protected object unigridNewsletters_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        bool approved = false;
        switch (sourceName.ToLower())
        {
            case "subscriptionapproved":
                return UniGridFunctions.ColoredSpanYesNo(ValidationHelper.GetBoolean(parameter, false));

            case "approve":
                approved = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["SubscriptionApproved"], false);
                if (approved)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.Visible = false;
                }
                break;

            case "reject":
                approved = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["SubscriptionApproved"], false);
                if (!approved)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.Visible = false;
                }
                break;
        }

        return null;
    }


    /// <summary>
    /// Unigrid newsletters action handler.
    /// </summary>
    protected void unigridNewsletters_OnAction(string actionName, object actionArgument)
    {
        // Check 'configure' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "ManageSubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "ManageSubscribers");
        }
        
        int newsletterId = ValidationHelper.GetInteger(actionArgument, 0);

        switch (actionName.ToLower())
        {
            // Unsubscribe selected subscriber
            case "remove":
                SubscriberProvider.Unsubscribe(subscriberId, newsletterId, chkSendConfirmation.Checked);                                    
                break;

            // Approve selected subscription
            case "approve":
                {                    
                    SubscriberNewsletterInfo subscriptionInfo = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriberId, newsletterId);
                    if ((subscriptionInfo != null) && (!subscriptionInfo.SubscriptionApproved))
                    {
                        subscriptionInfo.SubscriptionApproved = true;
                        subscriptionInfo.SubscriptionApprovedWhen = DateTime.Now;
                        SubscriberNewsletterInfoProvider.SetSubscriberNewsletterInfo(subscriptionInfo);
                    }
                }
                break;

            // Reject selected subscription
            case "reject":
                {                    
                    SubscriberNewsletterInfo subscriptionInfo = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriberId, newsletterId);
                    if ((subscriptionInfo != null) && (subscriptionInfo.SubscriptionApproved))
                    {
                        subscriptionInfo.SubscriptionApproved = false;
                        subscriptionInfo.SubscriptionApprovedWhen = DateTime.MinValue;
                        SubscriberNewsletterInfoProvider.SetSubscriberNewsletterInfo(subscriptionInfo);
                    }
                }
                break;
        }
    }


    /// <summary>
    /// Handles multiple selector actions.
    /// </summary>
    protected void btnOk_Clicked(object sender, EventArgs e)
    {
        // Check permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "ManageSubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "ManageSubscribers");
        }
        // Return if no action was selected
        if (drpActions.SelectedValue.Equals("SELECT", StringComparison.InvariantCultureIgnoreCase))
        {
            return;
        }

        // Get selected items
        ArrayList list = unigridNewsletters.SelectedItems;
        if (list.Count == 0)
        {
            ltlScript.Text += ScriptHelper.GetAlertScript(GetString("general.noitems"));
            return;
        }

        int newsletterId;
        SubscriberNewsletterInfo subscriptionInfo;
        foreach (object id in list)
        {
            newsletterId = ValidationHelper.GetInteger(id, 0);

            switch (drpActions.SelectedValue)
            {
                // Remove subscription
                case "REMOVE":
                    SubscriberProvider.Unsubscribe(subscriberId, newsletterId, chkSendConfirmation.Checked);
                    break;

                // Approve subscription
                case "APPROVE":
                    subscriptionInfo = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriberId, newsletterId);
                    if ((subscriptionInfo != null) && (!subscriptionInfo.SubscriptionApproved))
                    {
                        subscriptionInfo.SubscriptionApproved = true;
                        subscriptionInfo.SubscriptionApprovedWhen = DateTime.Now;
                        SubscriberNewsletterInfoProvider.SetSubscriberNewsletterInfo(subscriptionInfo);
                    }
                    break;

                // Reject subscription
                case "REJECT":
                    subscriptionInfo = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(subscriberId, newsletterId);
                    if ((subscriptionInfo != null) && (subscriptionInfo.SubscriptionApproved))
                    {
                        subscriptionInfo.SubscriptionApproved = false;
                        subscriptionInfo.SubscriptionApprovedWhen = DateTime.MinValue;
                        SubscriberNewsletterInfoProvider.SetSubscriberNewsletterInfo(subscriptionInfo);
                    }
                    break;
            }
        }        

        unigridNewsletters.ResetSelection();
        unigridNewsletters.ReloadData();
    }
}