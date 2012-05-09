using System;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.Notifications;
using CMS.CMSHelper;

public partial class CMSWebParts_Notifications_ContentSubscription : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the code names of the notification gateways separated with semicolon.
    /// </summary>
    public string GatewayNames
    {
        get
        {
            string names = ValidationHelper.GetString(this.GetValue("GatewayNames"), "");
            if (names == "")
            {
                names = "CMS.EmailGateway";
            }
            return names;
        }
        set
        {
            this.SetValue("GatewayNames", value);
        }
    }


    /// <summary>
    /// Indicates if notification e-mail is sent when specified document is created.
    /// </summary>
    public bool CreateEventEnabled
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CreateEventEnabled"), false);
        }
        set
        {
            this.SetValue("CreateEventEnabled", value);
        }
    }


    /// <summary>
    /// Gets or sets localizable string or plain text which describes CREATE event and which is visible to the users.
    /// </summary>
    public string CreateEventDisplayName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CreateEventDisplayName"), "");
        }
        set
        {
            this.SetValue("CreateEventDisplayName", value);
        }
    }


    /// <summary>
    /// Indicates if notification e-mail is sent when specified document is deleted.
    /// </summary>
    public bool DeleteEventEnabled
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DeleteEventEnabled"), false);
        }
        set
        {
            this.SetValue("DeleteEventEnabled", value);
        }
    }


    /// <summary>
    /// Gets or sets localizable string or plain text which describes DELETE event and which is visible to the users.
    /// </summary>
    public string DeleteEventDisplayName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DeleteEventDisplayName"), "");
        }
        set
        {
            this.SetValue("DeleteEventDisplayName", value);
        }
    }


    /// <summary>
    /// Indicates if notification e-mail is sent when specified document is updated.
    /// </summary>
    public bool UpdateEventEnabled
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UpdateEventEnabled"), false);
        }
        set
        {
            this.SetValue("UpdateEventEnabled", value);
        }
    }


    /// <summary>
    /// Gets or sets localizable string or plain text which describes UPDATE event and which is visible to the users.
    /// </summary>
    public string UpdateEventDisplayName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("UpdateEventDisplayName"), "");
        }
        set
        {
            this.SetValue("UpdateEventDisplayName", value);
        }
    }


    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public string EventDescription
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EventDescription"), "");
        }
        set
        {
            this.SetValue("EventDescription", value);
        }
    }


    /// <summary>
    /// Determines whether the users are subscribed to site specific event or global event.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), "-");
        }
        set
        {
            this.SetValue("SiteName", value);
        }
    }


    /// <summary>
    /// Gets or sets the format of the subscription (HTML/Plaintext)
    /// </summary>
    public bool SubscriptionUseHTML
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SubscriptionUseHTML"), false);
        }
        set
        {
            this.SetValue("SubscriptionUseHTML", value);
        }
    }


    /// <summary>
    /// Gets or sets the path to the document.
    /// </summary>
    public string Path
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Path"), "");
        }
        set
        {
            this.SetValue("Path", value);
        }
    }


    /// <summary>
    /// Gets or sets the notification template code name for CREATE event.
    /// </summary>
    public string CreateEventTemplateName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CreateEventTemplateName"), "");
        }
        set
        {
            this.SetValue("CreateEventTemplateName", value);
        }
    }


    /// <summary>
    /// Gets or sets the notification template code name for DELETE event.
    /// </summary>
    public string DeleteEventTemplateName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DeleteEventTemplateName"), "");
        }
        set
        {
            this.SetValue("DeleteEventTemplateName", value);
        }
    }


    /// <summary>
    /// Gets or sets the notification template code name for UPDATE event.
    /// </summary>
    public string UpdateEventTemplateName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("UpdateEventTemplateName"), "");
        }
        set
        {
            this.SetValue("UpdateEventTemplateName", value);
        }
    }


    /// <summary>
    /// Gets or sets the document types.
    /// </summary>
    public string DocumentTypes
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DocumentTypes"), "");
        }
        set
        {
            this.SetValue("DocumentTypes", value);
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
            this.subscriptionElem.StopProcessing = true;
        }
        else
        {

            // Build the actions string
            string actionsString = (CreateEventEnabled ? "|CREATEDOC" : "") +
                (DeleteEventEnabled ? "|DELETEDOC" : "") +
                (UpdateEventEnabled ? "|UPDATEDOC" : "");

            actionsString = actionsString.TrimStart('|');

            // Get the actions
            string[] actions = actionsString.Split(new char[] { '|' });
            if (actions.Length > 0)
            {
                // Inititalize subscriptionElem control
                this.subscriptionElem.GatewayNames = this.GatewayNames;
                this.subscriptionElem.EventSource = "Content";
                this.subscriptionElem.EventDescription = this.EventDescription;
                this.subscriptionElem.EventObjectID = 0;
                this.subscriptionElem.EventData1 = (String.IsNullOrEmpty(this.Path) ? "/%" : this.Path);
                this.subscriptionElem.EventData2 = this.DocumentTypes;
                this.subscriptionElem.SubscriptionUseHTML = this.SubscriptionUseHTML;

                // If "#current#" is set, then get current site ID
                if (this.SiteName == "#current#")
                {
                    this.subscriptionElem.SubscriptionSiteID = CMSContext.CurrentSiteID;
                }
                // If "-" as global is not set, then try to find the site
                else if (this.SiteName != "-")
                {
                    // Try to find given site
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(this.SiteName);
                    if (si != null)
                    {
                        this.subscriptionElem.SubscriptionSiteID = si.SiteID;
                    }
                }

                // Initialize SubscriptionInfo objects
                NotificationSubscriptionInfo[] subscriptions = new NotificationSubscriptionInfo[actions.Length];
                for (int i = 0; i < actions.Length; i++)
                {
                    NotificationSubscriptionInfo nsi = new NotificationSubscriptionInfo();
                    nsi.SubscriptionEventCode = actions[i];

                    // Get correct template name and event display name
                    string currentDisplayName = "";
                    string currentTemplateName = "";
                    switch (actions[i].ToLower())
                    {
                        case "createdoc":
                            currentDisplayName = this.CreateEventDisplayName;
                            currentTemplateName = this.CreateEventTemplateName;
                            break;
                        case "deletedoc":
                            currentDisplayName = this.DeleteEventDisplayName;
                            currentTemplateName = this.DeleteEventTemplateName;
                            break;
                        case "updatedoc":
                            currentDisplayName = this.UpdateEventDisplayName;
                            currentTemplateName = this.UpdateEventTemplateName;
                            break;
                    }

                    // Get correct template
                    NotificationTemplateInfo nti = GetTemplateInfo(currentTemplateName);
                    if (nti != null)
                    {
                        nsi.SubscriptionTemplateID = nti.TemplateID;
                    }

                    if (String.IsNullOrEmpty(currentDisplayName))
                    {
                        nsi.SubscriptionEventDisplayName = String.Format(GetString("notifications.contentsubscription.name"),
                            (String.IsNullOrEmpty(this.Path) ? "/%" : this.Path),
                            (String.IsNullOrEmpty(this.DocumentTypes) ? GetString("notifications.contentsubscription.alldoctypes") : this.DocumentTypes),
                            actions[i]);
                    }
                    else
                    {
                        nsi.SubscriptionEventDisplayName = currentDisplayName;
                    }
                    subscriptions[i] = nsi;

                }
                this.subscriptionElem.Subscriptions = subscriptions;
            }
        }
    }


    /// <summary>
    /// Parses the notification template site and name and returns proper Info object.
    /// </summary>
    private NotificationTemplateInfo GetTemplateInfo(string templateName)
    {
        if (templateName != null)
        {
            string[] temp = templateName.Split(new char[] { '.' });
            if (temp.Length == 2)
            {
                // Site template
                SiteInfo tempSite = SiteInfoProvider.GetSiteInfo(temp[0]);
                if (tempSite != null)
                {
                    return NotificationTemplateInfoProvider.GetNotificationTemplateInfo(temp[1], tempSite.SiteID);
                }
            }
            else
            {
                // Global template
                return NotificationTemplateInfoProvider.GetNotificationTemplateInfo(templateName);
            }
        }

        return null;
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }
}
