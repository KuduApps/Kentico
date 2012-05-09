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
using System.Text.RegularExpressions;

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.FormControls;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Subscribers_Subscriber_General : CMSNewsletterSubscribersPage
{
    protected int subscriberId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize dataform
        formCustomFields.OnAfterValidate += formCustomFields_OnAfterValidate;
        formCustomFields.OnAfterSave += formCustomFields_OnAfterSave;
        formCustomFields.OnBeforeValidate += formCustomFields_OnBeforeValidate;

        // Get subscriber ID from querystring
        subscriberId = QueryHelper.GetInteger("subscriberid", 0);
        if (subscriberId > 0)
        {
            if (!RequestHelper.IsPostBack())
            {
                // Get subscriber info and check its existence
                Subscriber subscriberObj = SubscriberProvider.GetSubscriber(subscriberId);
                EditedObject = subscriberObj;

                // Fill editing form only if subscriber is of standard type
                if (DataHelper.IsEmpty(subscriberObj.SubscriberType) && subscriberObj.SubscriberRelatedID == 0)
                {
                    formCustomFields.ItemID = subscriberObj.SubscriberID;

                    // Show that the subscriber was created or updated successfully
                    if (QueryHelper.GetBoolean("saved", false))
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
                else
                {
                    formCustomFields.Visible = false;
                }
            }
        }
        else
        {
            formCustomFields.Visible = false;
        }
    }


    /// <summary>
    /// DataForm's OnBeforeValidate event handler.
    /// </summary>
    protected void formCustomFields_OnBeforeValidate(object sender, EventArgs e)
    {
        // Check permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managesubscribers"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "managesubscribers");
        }
    }


    /// <summary>
    /// DataForm's OnAfterValidate event handler.
    /// </summary>
    protected void formCustomFields_OnAfterValidate(object sender, EventArgs e)
    {
        // Get subscriber's email
        FormEngineUserControl control = (FormEngineUserControl)formCustomFields.BasicForm.FieldControls["SubscriberEmail"];
        if (control != null)
        {
            string mail = ValidationHelper.GetString(control.Value, String.Empty).Trim();
            // Get original subscriber
            Subscriber subscriberObj = SubscriberProvider.GetSubscriber(subscriberId);
            if (subscriberObj != null)
            {
                // Check the email changes
                if ((subscriberObj.SubscriberEmail.ToLower() != mail.ToLower()) && (SubscriberProvider.EmailExists(mail)))
                {
                    // Show error message if the email address is already used by another subscriber
                    formCustomFields.BasicForm.StopProcessing = true;
                    lblError.Visible = true;
                    lblError.Text = GetString("Subscriber_Edit.EmailAlreadyExists");
                }
            }
            else
            {
                formCustomFields.BasicForm.StopProcessing = true;
                lblError.Visible = true;
                lblError.Text = GetString("Subscriber_Edit.SubscriberDoesNotExists");
            }
        }
    }


    /// <summary>
    /// DataForm's OnAfterSave event handler.
    /// </summary>
    protected void formCustomFields_OnAfterSave(object sender, EventArgs e)
    {
        if (!formCustomFields.BasicForm.StopProcessing)
        {
            // Save with provider to log synchronization
            Subscriber subscriberObj = SubscriberProvider.GetSubscriber(subscriberId);
            EditedObject = subscriberObj;

            subscriberObj.SubscriberFullName = (subscriberObj.SubscriberFirstName + " " + subscriberObj.SubscriberLastName).Trim();
            SubscriberProvider.SetSubscriber(subscriberObj);
            
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");

            // Refresh header with display name
            ScriptHelper.RefreshTabHeader(Page, null);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (formCustomFields.BasicForm != null)
        {
            // Set submit button's CSS class
            formCustomFields.BasicForm.SubmitButton.CssClass = "SubmitButton";
        }
    }
}
