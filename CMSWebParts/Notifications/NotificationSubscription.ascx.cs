using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.Notifications;
using CMS.CMSHelper;

public partial class CMSWebParts_Notifications_NotificationSubscription : CMSAbstractWebPart
{
    #region "Properties"

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
    /// Event data field 1.
    /// </summary>
    public string EventData1
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EventData1"), "");
        }
        set
        {
            this.SetValue("EventData1", value);
        }
    }


    /// <summary>
    /// Event data field 2.
    /// </summary>
    public string EventData2
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EventData2"), "");
        }
        set
        {
            this.SetValue("EventData2", value);
        }
    }


    /// <summary>
    /// Gets or sets the text which will be displayed above the notification gateway forms.
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
    /// Gets or sets the code names of the notification gateways separated with semicolon.
    /// </summary>
    public string GatewayNames
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("GatewayNames"), "");
        }
        set
        {
            this.SetValue("GatewayNames", value);
        }
    }


    /// <summary>
    /// Gets or sets the notification template code name.
    /// </summary>
    public string NotificationTemplateName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NotificationTemplateName"), "");
        }
        set
        {
            this.SetValue("NotificationTemplateName", value);
        }
    }


    /// <summary>
    /// Gets or sets the event source.
    /// </summary>
    public string EventSource
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EventSource"), "");
        }
        set
        {
            this.SetValue("EventSource", value);
        }
    }


    /// <summary>
    /// Gets or sets the event code.
    /// </summary>
    public string EventCode
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EventCode"), "");
        }
        set
        {
            this.SetValue("EventCode", value);
        }
    }


    /// <summary>
    /// Gets or sets the event object ID.
    /// </summary>
    public int EventObjectID
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("EventObjectID"), -1);
        }
        set
        {
            this.SetValue("EventObjectID", value);
        }
    }


    /// <summary>
    /// Gets or sets localizable string or plain text which describes event and which is visible to the users.
    /// </summary>
    public string EventDisplayName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("EventDisplayName"), "");
        }
        set
        {
            this.SetValue("EventDisplayName", value);
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
            // Inititalize subscriptionElem control
            this.subscriptionElem.Subscriptions = new NotificationSubscriptionInfo[] { new NotificationSubscriptionInfo() };
            this.subscriptionElem.EventDescription = this.EventDescription;
            this.subscriptionElem.GatewayNames = this.GatewayNames;
            this.subscriptionElem.NotificationTemplateName = this.NotificationTemplateName;
            this.subscriptionElem.EventCode = this.EventCode;
            this.subscriptionElem.EventSource = this.EventSource;
            this.subscriptionElem.EventDisplayName = this.EventDisplayName;
            this.subscriptionElem.EventObjectID = this.EventObjectID;
            this.subscriptionElem.EventData1 = this.EventData1;
            this.subscriptionElem.EventData2 = this.EventData2;
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
        }
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
