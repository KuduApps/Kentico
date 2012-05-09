using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.EmailEngine;
using CMS.EventLog;
using CMS.SiteProvider;

public partial class CMSWebParts_Newsletters_UnsubscriptionRequest : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets submit button text.
    /// </summary>
    public string ButtonText {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ButtonText"), String.Empty);
        }
        set
        {
            this.SetValue("ButtonText", value);
        }
    }


    /// <summary>
    /// Gets or sets newsletter name.
    /// </summary>
    public string NewsletterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NewsletterName"), null);
        }
        set
        {
            this.SetValue("NewsletterName", value);
        }
    }


    /// <summary>
    /// Gets or sets info message.
    /// </summary>
    public string InformationText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("InformationText"), null);
        }
        set
        {
            this.SetValue("InformationText", value);
        }
    }


    /// <summary>
    /// Gets or sets error message.
    /// </summary>
    public string ErrorText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ErrorText"), null);
        }
        set
        {
            this.SetValue("ErrorText", value);
        }
    }


    /// <summary>
    /// Gets or sets message that will be shown after successful unsubscription.
    /// </summary>
    public string ResultText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ResultText"), null);
        }
        set
        {
            this.SetValue("ResultText", value);
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
    /// Reloads data for partial caching.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
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
        }
        else
        {
            if (!String.IsNullOrEmpty(this.InformationText))
            {
                lblInfo.Text = this.InformationText;
                lblInfo.Visible = true;
            }
            else
            {
                lblInfo.Visible = false;
            }

            if (!String.IsNullOrEmpty(this.ButtonText))
            {
                btnSubmit.Text = this.ButtonText;
            }
            else
            {
                btnSubmit.Text = GetString("general.ok");
            }
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Check email format
        string email = txtEmail.Text.Trim();
        string result = new Validator().IsEmail(email, GetString("unsubscribe.invalidemailformat")).Result;

        if (String.IsNullOrEmpty(result))
        {
            bool requestSent = false;
            int siteId = 0;
            if (CMSContext.CurrentSite != null)
            {
                siteId = CMSContext.CurrentSiteID;
            }

            // Try to get all subscriber infos with given e-mail
            DataSet ds = SubscriberProvider.GetSubscribersFromView(email, siteId);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Subscriber sb = new Subscriber(dr);
                    if ((sb != null) && ((sb.SubscriberType == null) || (sb.SubscriberRelatedID > 0) && (sb.SubscriberType == SiteObjectType.USER)))
                    {
                        // Get newsletter information
                        Newsletter news = NewsletterProvider.GetNewsletter(this.NewsletterName, siteId);
                        if (news != null)
                        {
                            // Get subscription info
                            SubscriberNewsletterInfo sni = SubscriberNewsletterInfoProvider.GetSubscriberNewsletterInfo(sb.SubscriberID, news.NewsletterID);
                            // Send e-mail to subscribed user only
                            if (sni != null)
                            {
                                SendUnsubscriptionRequest(sb, news, sni, CMSContext.CurrentSiteName);
                                requestSent = true;  // At least one request was sent
                            }
                        }
                    }
                }
            }

            // Unsubscription failed if none confirmation e-mail was sent
            if (!requestSent)
            {
                // Use default error message if none is specified
                if (String.IsNullOrEmpty(this.ErrorText))
                {
                    result = GetString("unsubscribe.notsubscribed");
                }
                else
                {
                    result = this.ErrorText;
                }
            }
        }

        // Display error message if set
        if (!string.IsNullOrEmpty(result))
        {
            lblError.Text = result;
            lblError.Visible = true;
        }
        else
        {
            // Display unsubscription confirmation
            lblInfo.Visible = true;
            if (String.IsNullOrEmpty(this.ResultText))
            {
                // Use default message if none was specified
                lblInfo.Text = GetString("unsubscribe.confirmtext");
            }
            else
            {
                lblInfo.Text = this.ResultText;
            }
            lblError.Visible = false;
            txtEmail.Visible = false;
            btnSubmit.Visible = false;
        }
    }


    /// <summary>
    /// Creates and sends unsubscription e-mail.
    /// </summary>
    /// <param name="subscriber">Subscriber object</param>
    /// <param name="news">Newsletter object</param>
    /// <param name="subscription">Subscription object</param>
    /// <param name="siteName">Site name</param>
    protected void SendUnsubscriptionRequest(Subscriber subscriber, Newsletter news, SubscriberNewsletterInfo subscription, string siteName)
    {
        // Get global e-mail template with unsubscription request
        EmailTemplateInfo et = CMS.EmailEngine.EmailTemplateProvider.GetEmailTemplate("newsletter.unsubscriptionrequest", siteName);
        if (et != null)
        {
            // Get subscriber member
            SortedDictionary<int, Subscriber> subscribers = SubscriberProvider.GetSubscribers(subscriber, 1, 0);
            foreach (KeyValuePair<int, Subscriber> item in subscribers)
            {
                // Get 1st subscriber's member
                Subscriber sb = item.Value;

                string body = et.TemplateText;
                string plainBody = et.TemplatePlainText;

                // Resolve newsletter macros (first name, last name etc.)
                IssueHelper ih = new IssueHelper();
                if (ih.LoadDynamicFields(sb, news, subscription, null, false, siteName, null, null, null))
                {
                    body = ih.ResolveDynamicFieldMacros(body);
                    plainBody = ih.ResolveDynamicFieldMacros(plainBody);
                }

                // Create e-mail
                EmailMessage msg = new EmailMessage();
                msg.EmailFormat = EmailFormatEnum.Default;
                msg.From = EmailHelper.GetSender(et, news.NewsletterSenderEmail);
                msg.Recipients = sb.SubscriberEmail;
                msg.BccRecipients = et.TemplateBcc;
                msg.CcRecipients = et.TemplateCc;
                msg.Subject = ResHelper.LocalizeString(et.TemplateSubject);
                msg.Body = URLHelper.MakeLinksAbsolute(body);
                msg.PlainTextBody = URLHelper.MakeLinksAbsolute(plainBody);

                // Add attachments and send e-mail
                MetaFileInfoProvider.ResolveMetaFileImages(msg, et.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);

                EmailSender.SendEmail(siteName, msg);
            }
        }
        else
        {
            // Log missing template
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "UnsubscriptionRequest", "Unsubscription request e-mail template is missing.");
        }
    }
}
