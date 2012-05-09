using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.LicenseProvider;

public partial class CMSModules_Newsletters_Tools_Subscribers_Subscriber_Header : CMSNewsletterSubscribersPage
{
    #region "Variables"

    protected int subscriberId;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        string currentSubscriber = GetString("Subscriber_Edit.NewItemCaption");

        // Get subscriber id from querystring        
        subscriberId = QueryHelper.GetInteger("subscriberid", 0);

        // Get subscriber object
        Subscriber subscriberObj = SubscriberProvider.GetSubscriber(subscriberId);
        if (subscriberObj != null)
        {
            if (!DataHelper.IsEmpty(subscriberObj.SubscriberEmail))
            {
                // Get e-mail for breadcrumbs
                currentSubscriber = subscriberObj.SubscriberEmail;
            }
            else
            {
                string type = null;
                switch (subscriberObj.SubscriberType)
                {
                    case SiteObjectType.USER:
                        type = GetString("objecttype.cms_user");
                        break;
                    case SiteObjectType.ROLE:
                        type = GetString("objecttype.cms_role");
                        break;
                    case PredefinedObjectType.CONTACTGROUP:
                        type = GetString("objecttype.om_contactgroup");
                        break;
                }

                // Get first, last names and type for breadcrumbs
                currentSubscriber = string.Format("{0} ({1})", string.Concat(subscriberObj.SubscriberFirstName, " ", subscriberObj.SubscriberLastName), type.ToLower());
            }
        }

        // Initializes page title
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Subscriber_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Newsletters/Tools/Subscribers/Subscriber_List.aspx";
        breadcrumbs[0, 2] = "newslettersContent";
        breadcrumbs[1, 0] = currentSubscriber;

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        CurrentMaster.Title.HelpTopicName = "general_tab2";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Initialize tabs
        InitalizeMenu(subscriberObj);
    }


    /// <summary>
    /// Initializes user edit menu.
    /// </summary>
    protected void InitalizeMenu(Subscriber subscriber)
    {
        if (subscriber == null)
        {
            return;
        }

        string[,] tabs = new string[2, 4];
        // Display General tab only to standard subscribers ...
        if (string.IsNullOrEmpty(subscriber.SubscriberType) && subscriber.SubscriberRelatedID == 0)
        {
            tabs[0, 0] = GetString("general.general");
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_tab2');";
            tabs[0, 2] = "Subscriber_General.aspx?subscriberid=" + subscriberId;
            tabs[1, 0] = GetString("Subscriber_Edit.Subscription");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'subscriptions_tab');";
            tabs[1, 2] = "Subscriber_Subscriptions.aspx?subscriberid=" + subscriberId;
        }
        else
        {
            // ... other subscriber types have Subscriptions tab as the first one
            CurrentMaster.Title.HelpTopicName = "subscriptions_tab";
            tabs[0, 0] = GetString("Subscriber_Edit.Subscription");
            tabs[0, 1] = "SetHelpTopic('helpTopic', 'subscriptions_tab');";
            tabs[0, 2] = "Subscriber_Subscriptions.aspx?subscriberid=" + subscriberId;

            ScriptHelper.RegisterStartupScript(this, typeof(string), "SelectSubscriptionTab", ScriptHelper.GetScript("SelTab(0,'" + tabs[0, 2] + "','subscriberContent');"));
        }

        // Display Users tab for role subscribers
        if (subscriber.SubscriberType == SiteObjectType.ROLE)
        {
            tabs[1, 0] = GetString("Subscriber_Edit.Users");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'subscriberusers_tab');";
            tabs[1, 2] = "Subscriber_Users.aspx?roleid=" + subscriber.SubscriberRelatedID + "&subscriberid=" + subscriberId;
        }
        // Display Contacts tab for contact group subscribers
        else if (subscriber.SubscriberType == PredefinedObjectType.CONTACTGROUP && ModuleEntry.IsModuleLoaded(ModuleEntry.ONLINEMARKETING) && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ContactManagement))
        {
            tabs[1, 0] = GetString("om.contact.list");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'subscribercontacts_tab');";
            tabs[1, 2] = "../../../ContactManagement/Pages/Tools/Contact/Subscriber_Contacts.aspx?groupid=" + subscriber.SubscriberRelatedID + "&subscriberid=" + subscriberId;
        }

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "subscriberContent";
    }

    #endregion
}