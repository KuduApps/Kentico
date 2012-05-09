using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.Newsletter;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.LicenseProvider;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_Subscribers : CMSNewsletterNewslettersPage
{
    #region "Variables"

    protected int newsletterId;


    private int mBounceLimit;


    private bool mBounceInfoAvailable;


    /// <summary>
    /// Contact group selector
    /// </summary>
    private FormEngineUserControl cgSelector = null;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.CurrentMaster.ActionsViewstateEnabled = true;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterDialogScript(this);

        CurrentMaster.DisplayActionsPanel = true;
        chkRequireOptIn.CheckedChanged += chkRequireOptIn_CheckedChanged;

        string siteName = CMSContext.CurrentSiteName;
        mBounceLimit = SettingsKeyProvider.GetIntValue(siteName + ".CMSBouncedEmailsLimit");
        mBounceInfoAvailable = SettingsKeyProvider.GetBoolValue(siteName + ".CMSMonitorBouncedEmails") &&
                               NewsletterProvider.OnlineMarketingEnabled(siteName);

        newsletterId = QueryHelper.GetInteger("newsletterid", 0);
        Newsletter newsletter = NewsletterProvider.GetNewsletter(newsletterId);
        EditedObject = newsletter;

        // Check if newsletter enables double opt-in
        if ((newsletter != null) && !newsletter.NewsletterEnableOptIn)
        {
            chkRequireOptIn.Visible = false;
        }

        if (!RequestHelper.IsPostBack())
        {
            chkSendConfirmation.Checked = false;
        }

        // Initialize unigrid
        UniGridSubscribers.WhereCondition = "NewsletterID = " + newsletterId;
        UniGridSubscribers.OnAction += UniGridSubscribers_OnAction;
        UniGridSubscribers.OnExternalDataBound += UniGridSubscribers_OnExternalDataBound;

        CurrentMaster.DisplayControlsPanel = true;

        // Initialize selectors and mass actions
        SetupSelectors();
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Hide columns with bounced emails if bounce info is not available
        UniGridSubscribers.NamedColumns["blocked"].Visible =
            UniGridSubscribers.NamedColumns["bounces"].Visible = mBounceInfoAvailable;

        pnlActions.Visible = !DataHelper.DataSourceIsEmpty(UniGridSubscribers.GridView.DataSource);
    }


    /// <summary>
    /// Configures selectors.
    /// </summary>
    private void SetupSelectors()
    {
        // Setup role selector
        selectRole.CurrentSelector.SelectionMode = SelectionModeEnum.MultipleButton;
        selectRole.CurrentSelector.OnItemsSelected += RolesSelector_OnItemsSelected;
        selectRole.CurrentSelector.ReturnColumnName = "RoleID";
        selectRole.CurrentSelector.ButtonImage = GetImageUrl("Objects/CMS_Role/add.png");
        selectRole.ImageDialog.CssClass = "NewItemImage";
        selectRole.LinkDialog.CssClass = "NewItemLink";
        selectRole.ShowSiteFilter = false;
        selectRole.CurrentSelector.ResourcePrefix = "addroles";
        selectRole.DialogButton.CssClass = "LongButton";
        selectRole.IsLiveSite = false;
        selectRole.UseCodeNameForSelection = false;

        // Setup user selector
        selectUser.UniSelector.SelectionMode = SelectionModeEnum.MultipleButton;
        selectUser.UniSelector.OnItemsSelected += UserSelector_OnItemsSelected;
        selectUser.UniSelector.ReturnColumnName = "UserID";
        selectUser.UniSelector.DisplayNameFormat = "{%FullName%} ({%Email%})";
        selectUser.UniSelector.ButtonImage = GetImageUrl("Objects/CMS_User/add.png");
        selectUser.ImageDialog.CssClass = "NewItemImage";
        selectUser.LinkDialog.CssClass = "NewItemLink";
        selectUser.ShowSiteFilter = false;
        selectUser.ResourcePrefix = "addusers";
        selectUser.DialogButton.CssClass = "LongButton";
        selectUser.IsLiveSite = false;

        // Setup subscriber selector
        selectSubscriber.UniSelector.SelectionMode = SelectionModeEnum.MultipleButton;
        selectSubscriber.UniSelector.OnItemsSelected += SubscriberSelector_OnItemsSelected;
        selectSubscriber.UniSelector.ReturnColumnName = "SubscriberID";
        selectSubscriber.UniSelector.ButtonImage = GetImageUrl("Objects/Newsletter_Subscriber/add.png");
        selectSubscriber.ImageDialog.CssClass = "NewItemImage";
        selectSubscriber.LinkDialog.CssClass = "NewItemLink";
        selectSubscriber.ShowSiteFilter = false;
        selectSubscriber.DialogButton.CssClass = "LongButton";
        selectSubscriber.IsLiveSite = false;

        // Setup contact group selector
        if (ModuleEntry.IsModuleLoaded(ModuleEntry.ONLINEMARKETING) && CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ContactManagement", "ReadContactGroups") && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.ContactManagement))
        {
            // Load selector control and initialize it
            plcSelectCG.Controls.Clear();
            cgSelector = (FormEngineUserControl)Page.LoadControl("~/CMSModules/ContactManagement/FormControls/ContactGroupSelector.ascx");
            if (cgSelector != null)
            {
                cgSelector.ID = "selectCG";
                // Get inner uniselector control
                UniSelector selector = (UniSelector)cgSelector.GetValue("uniselector");
                if (selector != null)
                {
                    // Bind an event handler on 'items selected' event
                    selector.OnItemsSelected += new EventHandler(selector_OnItemsSelected);
                    selector.ResourcePrefix = "contactgroupsubscriber";
                }
                // Insert selector to the header
                plcSelectCG.Controls.Add(cgSelector);
            }
        }

        // Initialize mass actions
        if (this.drpActions.Items.Count == 0)
        {
            drpActions.Items.Add(new ListItem(GetString("general.selectaction"), "SELECT"));
            drpActions.Items.Add(new ListItem(GetString("general.approve"), "APPROVE"));
            drpActions.Items.Add(new ListItem(GetString("general.Reject"), "REJECT"));
            drpActions.Items.Add(new ListItem(GetString("general.remove"), "REMOVE"));
        }
    }


    /// <summary>
    /// Unigrid external databound event handler.
    /// </summary>
    protected object UniGridSubscribers_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        bool approved = false;
        switch (sourceName)
        {
            case "block":
                return SetBlockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            case "unblock":
                return SetUnblockAction(sender, ((parameter as GridViewRow).DataItem) as DataRowView);

            case "email":
                return GetEmail(parameter as DataRowView);

            case "subscriptionapproved":
                return GetSubscriptionApproved(parameter);

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

            case "blocked":
                return GetBlocked(parameter as DataRowView);

            case "bounces":
                return GetBounces(parameter as DataRowView);
        }

        return null;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridSubscribers_OnAction(string actionName, object actionArgument)
    {
        // Check 'manage subscribers' permission
        CheckAuthorization();

        int subscriberId = ValidationHelper.GetInteger(actionArgument, 0);

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

            // Block selected subscriber
            case "block":
                SubscriberProvider.BlockSubscriber(subscriberId);
                break;

            // Un-block selected subscriber
            case "unblock":
                SubscriberProvider.UnblockSubscriber(subscriberId);
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
    /// Returns colored yes/no according to subscriber's approval info.
    /// </summary>
    private string GetSubscriptionApproved(object parameter)
    {
        return UniGridFunctions.ColoredSpanYesNo(parameter);
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
    /// Checkbox 'Require double opt-in' state changed.
    /// </summary>
    protected void chkRequireOptIn_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRequireOptIn.Checked)
        {
            chkSendConfirmation.Enabled = false;
            chkSendConfirmation.Checked = false;
        }
        else
        {
            chkSendConfirmation.Enabled = true;
        }
    }


    /// <summary>
    /// Roles control items changed event.
    /// </summary>
    protected void RolesSelector_OnItemsSelected(object sender, EventArgs e)
    {
        // Check permissions
        CheckAuthorization();

        int roleID = 0;
        string[] newItems = null;
        Subscriber sb = null;
        int siteId = CMSContext.CurrentSiteID;

        // Get new items from selector
        string newValues = ValidationHelper.GetString(selectRole.Value, null);

        // Get added items
        newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (newItems != null)
        {
            foreach (string item in newItems)
            {
                roleID = ValidationHelper.GetInteger(item, 0);

                // Get subscriber
                sb = SubscriberProvider.GetSubscriber(SiteObjectType.ROLE, roleID, siteId);
                if (sb == null)
                {
                    // Get role info and copy display name to new subscriber
                    RoleInfo ri = RoleInfoProvider.GetRoleInfo(roleID);
                    if (ri == null)
                    {
                        break;
                    }

                    // Check limited number of subscribers
                    if (!SubscriberProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Subscribers, VersionActionEnum.Insert))
                    {
                        lblError.Text = GetString("licenselimitations.subscribers.errormultiple");
                        lblError.Visible = true;
                        break;
                    }

                    // Create new subscriber of role type
                    sb = new Subscriber();
                    sb.SubscriberFirstName = ri.DisplayName;
                    // Full name consists of "role " and role display name
                    sb.SubscriberFullName = string.Concat("Role '", ri.DisplayName, "'");
                    sb.SubscriberSiteID = siteId;
                    sb.SubscriberType = SiteObjectType.ROLE;
                    sb.SubscriberRelatedID = roleID;
                    SubscriberProvider.SetSubscriber(sb);
                }

                // If subscriber exists and is not subscribed, subscribe him
                if ((sb != null) && (!SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId)))
                {
                    try
                    {
                        SubscriberProvider.Subscribe(sb.SubscriberID, newsletterId, DateTime.Now, chkSendConfirmation.Checked, false);
                    }
                    catch
                    {
                    }
                }
            }
        }

        selectRole.Value = null;
        UniGridSubscribers.ReloadData();
        pnlUpdate.Update();
    }


    /// <summary>
    /// User control items changed event.
    /// </summary>
    protected void UserSelector_OnItemsSelected(object sender, EventArgs e)
    {
        // Check permissions
        CheckAuthorization();

        int userID = 0;
        string[] newItems = null;
        Subscriber sb = null;
        int siteId = CMSContext.CurrentSiteID;

        // Get new items from selector
        string newValues = ValidationHelper.GetString(selectUser.Value, null);
        UserInfo ui = null;

        newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (newItems != null)
        {
            foreach (string item in newItems)
            {
                userID = ValidationHelper.GetInteger(item, 0);

                // Get subscriber
                sb = SubscriberProvider.GetSubscriber(SiteObjectType.USER, userID, siteId);
                if (sb == null)
                {
                    // Get user info
                    ui = UserInfoProvider.GetUserInfo(userID);
                    if (ui == null)
                    {
                        break;
                    }

                    // Check limited number of subscribers
                    if (!SubscriberProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Subscribers, VersionActionEnum.Insert))
                    {
                        lblError.Text = GetString("licenselimitations.subscribers.errormultiple");
                        lblError.Visible = true;
                        break;
                    }

                    // Create new subscriber of user type
                    sb = new Subscriber();
                    sb.SubscriberFirstName = ui.FullName;
                    sb.SubscriberFullName = "User '" + ui.FullName + "'";
                    sb.SubscriberSiteID = siteId;
                    sb.SubscriberType = SiteObjectType.USER;
                    sb.SubscriberRelatedID = userID;
                    SubscriberProvider.SetSubscriber(sb);
                }

                // If subscriber exists and is not subscribed, subscribe him
                if ((sb != null) && (!SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId)))
                {
                    try
                    {
                        SubscriberProvider.Subscribe(sb.SubscriberID, newsletterId, DateTime.Now, chkSendConfirmation.Checked, chkRequireOptIn.Checked);
                    }
                    catch
                    {
                    }
                }
            }
        }

        selectUser.Value = null;
        UniGridSubscribers.ReloadData();
        pnlUpdate.Update();
    }


    /// <summary>
    /// Subscriber control items changed event.
    /// </summary>
    protected void SubscriberSelector_OnItemsSelected(object sender, EventArgs e)
    {
        // Check permissions
        CheckAuthorization();

        int subscriberID = 0;
        string[] newItems = null;
        Subscriber sb = null;

        // Get new items from selector
        string newValues = ValidationHelper.GetString(selectSubscriber.Value, null);

        newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (newItems != null)
        {
            // Add all new items to site
            foreach (string item in newItems)
            {
                subscriberID = ValidationHelper.GetInteger(item, 0);

                // Get subscriber
                sb = SubscriberProvider.GetSubscriber(subscriberID);

                // If subscriber exists and is not subscribed, subscribe him
                if ((sb != null) && (!SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId)))
                {
                    try
                    {
                        SubscriberProvider.Subscribe(sb.SubscriberID, newsletterId, DateTime.Now, chkSendConfirmation.Checked, chkRequireOptIn.Checked);
                    }
                    catch
                    {
                    }
                }
            }
        }

        selectSubscriber.Value = null;
        UniGridSubscribers.ReloadData();
        pnlUpdate.Update();
    }


    /// <summary>
    /// Contact group items selected event handler.
    /// </summary>
    protected void selector_OnItemsSelected(object sender, EventArgs e)
    {
        // Check permissions
        CheckAuthorization();

        if (cgSelector == null)
        {
            return;
        }
        int groupID = 0;
        string[] newItems = null;
        Subscriber sb = null;
        int siteId = CMSContext.CurrentSiteID;
        string cgType = "om.contactgroup";
        
        // Get new items from selector
        string newValues = ValidationHelper.GetString(cgSelector.Value, null);

        // Get added items
        newItems = newValues.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (newItems != null)
        {
            foreach (string item in newItems)
            {
                groupID = ValidationHelper.GetInteger(item, 0);

                // Get subscriber
                sb = SubscriberProvider.GetSubscriber(cgType, groupID, siteId);
                if (sb == null)
                {
                    // Get contact group display name
                    string cgName = ModuleCommands.OnlineMarketingGetContactGroupName(groupID);
                    if (string.IsNullOrEmpty(cgName))
                    {
                        break;
                    }

                    // Check limited number of subscribers
                    if (!SubscriberProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Subscribers, VersionActionEnum.Insert))
                    {
                        lblError.Text = GetString("licenselimitations.subscribers.errormultiple");
                        lblError.Visible = true;
                        break;
                    }

                    // Create new subscriber of contact group type
                    sb = new Subscriber();
                    sb.SubscriberFirstName = cgName;
                    // Full name consists of "contact group " and display name
                    sb.SubscriberFullName = string.Concat("Contact group '", cgName, "'");
                    sb.SubscriberSiteID = siteId;
                    sb.SubscriberType = cgType;
                    sb.SubscriberRelatedID = groupID;
                    SubscriberProvider.SetSubscriber(sb);
                }

                // If subscriber exists and is not subscribed, subscribe him
                if ((sb != null) && (!SubscriberProvider.IsSubscribed(sb.SubscriberID, newsletterId)))
                {
                    try
                    {
                        SubscriberProvider.Subscribe(sb.SubscriberID, newsletterId, DateTime.Now, chkSendConfirmation.Checked, false);
                    }
                    catch
                    {
                    }
                }
            }
        }

        cgSelector.Value = null;
        UniGridSubscribers.ReloadData();
        pnlUpdate.Update();
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


    /// <summary>
    /// Handles multiple selector actions.
    /// </summary>
    protected void btnOk_Clicked(object sender, EventArgs e)
    {
        // Check permissions
        CheckAuthorization();

        if (drpActions.SelectedValue != "SELECT")
        {
            // Go through all selected items
            ArrayList list = UniGridSubscribers.SelectedItems;
            if (list.Count > 0)
            {
                SubscriberNewsletterInfo sni = null;
                foreach (object subscriberId in list)
                {
                    sni = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(ValidationHelper.GetInteger(subscriberId, 0), newsletterId);
                    if (sni != null)
                    {
                        switch (drpActions.SelectedValue)
                        {
                            // Remove subscription
                            case "REMOVE":
                                SubscriberProvider.Unsubscribe(sni, chkSendConfirmation.Checked);
                                break;

                            // Approve subscription
                            case "APPROVE":
                                if (!sni.SubscriptionApproved)
                                {
                                    sni.SubscriptionApproved = true;
                                    sni.SubscriptionApprovedWhen = DateTime.Now;
                                    SubscriberNewsletterInfoProvider.SetSubscriberNewsletterInfo(sni);
                                }
                                break;

                            // Reject subscription
                            case "REJECT":
                                if (sni.SubscriptionApproved)
                                {
                                    sni.SubscriptionApproved = false;
                                    sni.SubscriptionApprovedWhen = DateTime.MinValue;
                                    SubscriberNewsletterInfoProvider.SetSubscriberNewsletterInfo(sni);
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                ltlScript.Text += ScriptHelper.GetAlertScript(GetString("general.noitems"));
            }
        }
        UniGridSubscribers.ResetSelection();
        UniGridSubscribers.ReloadData();
    }

    #endregion
}