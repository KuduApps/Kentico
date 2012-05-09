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

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Newsletter;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Newsletters_Tools_Templates_NewsletterTemplate_New : CMSNewsletterTemplatesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set title
        this.CurrentMaster.Title.HelpTopicName = "new_newsletter2";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Check 'Manage templates' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managetemplates"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "managetemplates");
        }

        rfvTemplateDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvTemplateName.ErrorMessage = GetString("NewsletterTemplate_Edit.ErrorEmptyName");
        lblTemplateType.Text = GetString("NewsletterTemplate_Edit.TemplateType");
        btnOk.Text = GetString("General.OK");

        string currentEmailTemplate = GetString("NewsletterTemplate_Edit.NewItemCaption");

        // Initializes page title control		
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("NewsletterTemplate_Edit.ItemListLink");
        tabs[0, 1] = "~/CMSModules/Newsletters/Tools/Templates/NewsletterTemplate_List.aspx";
        tabs[0, 2] = "newslettersContent";
        tabs[1, 0] = currentEmailTemplate;
        tabs[1, 1] = string.Empty;
        tabs[1, 2] = string.Empty;
        this.CurrentMaster.Title.Breadcrumbs = tabs;

        if (!RequestHelper.IsPostBack())
        {
            // Fill drop down list with newsletter types
            ListItem newItem = new ListItem(GetString("NewsletterTemplate_Edit.DrpOptIn"), "0");
            drpTemplateType.Items.Add(newItem);
            newItem = new ListItem(GetString("NewsletterTemplate_Edit.DrpIssue"), "1");
            drpTemplateType.Items.Add(newItem);
            newItem = new ListItem(GetString("NewsletterTemplate_Edit.DrpSubscription"), "2");
            drpTemplateType.Items.Add(newItem);
            newItem = new ListItem(GetString("NewsletterTemplate_Edit.DrpUnsubscription"), "3");
            drpTemplateType.Items.Add(newItem);
            drpTemplateType.SelectedIndex = 0;
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string errorMessage = new Validator().NotEmpty(txtTemplateDisplayName.Text.Trim(), GetString("general.requiresdisplayname"))
            .NotEmpty(txtTemplateName.Text.Trim(), GetString("NewsletterTemplate_Edit.ErrorEmptyName"))
            .IsCodeName(txtTemplateName.Text.Trim(), GetString("General.ErrorCodeNameInIdentificatorFormat")).Result;

        if (String.IsNullOrEmpty(errorMessage))
        {
            int siteId = CMSContext.CurrentSiteID;
            // TemplateName must to be unique
            EmailTemplate emailTemplateObj = EmailTemplateProvider.GetEmailTemplate(txtTemplateName.Text.Trim(), siteId);

            // If templateName value is unique														
            if (emailTemplateObj == null)
            {
                string type;
                int typeValue = Convert.ToInt32(drpTemplateType.SelectedValue);
                switch (typeValue)
                {
                    default:
                    case 0:
                        type = "D"; // Double opt-in
                        break;

                    case 1:
                        type = "I"; // Issue
                        break;

                    case 2:
                        type = "S"; // Subscription
                        break;

                    case 3:
                        type = "U"; // Unsubscription
                        break;
                }

                // Create new item -> insert
                emailTemplateObj = new EmailTemplate();
                emailTemplateObj.TemplateType = type;
                emailTemplateObj.TemplateBody = string.Empty;
                emailTemplateObj.TemplateName = txtTemplateName.Text.Trim();
                emailTemplateObj.TemplateHeader = "<html>\n<head>\n</head>\n<body>";
                emailTemplateObj.TemplateFooter = "</body>\n</html>";
                emailTemplateObj.TemplateDisplayName = txtTemplateDisplayName.Text.Trim();
                emailTemplateObj.TemplateSiteID = siteId;

                EmailTemplateProvider.SetEmailTemplate(emailTemplateObj);

                URLHelper.Redirect("NewsletterTemplate_Edit.aspx?templateid=" + Convert.ToString(emailTemplateObj.TemplateID) + "&saved=1");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("NewsletterTemplate_Edit.TemplateNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }
}
