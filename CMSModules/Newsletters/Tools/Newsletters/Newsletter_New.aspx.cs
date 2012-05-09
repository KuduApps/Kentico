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
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Newsletter;
using CMS.Scheduler;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Newsletters_Tools_Newsletters_Newsletter_New : CMSNewsletterNewslettersPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set title
        this.CurrentMaster.Title.HelpTopicName = "new_newsletter";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Set required field validators
        rfvNewsletterDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvNewsletterName.ErrorMessage = GetString("Newsletter_Edit.ErrorEmptyName");
        rfvNewsletterSenderName.ErrorMessage = GetString("Newsletter_Edit.ErrorEmptySenderName");
        rfvNewsletterSenderEmail.ErrorMessage = GetString("Newsletter_Edit.ErrorEmptySenderEmail");

        // Initialize page title control		
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("Newsletter_Edit.ItemListLink");
        tabs[0, 1] = "~/CMSModules/Newsletters/Tools/Newsletters/Newsletter_List.aspx";
        tabs[0, 2] = "newslettersContent";
        tabs[1, 0] = GetString("Newsletter_Edit.NewItemCaption");
        tabs[1, 1] = string.Empty;
        tabs[1, 2] = string.Empty;

        this.CurrentMaster.Title.Breadcrumbs = tabs;

        if (!RequestHelper.IsPostBack())
        {
            radDynamic.Checked = false;
            radTemplateBased.Checked = true;
            SetTypeToTemplateBased();
        }

        // Configure newsletter template selectors
        int siteId = CMSContext.CurrentSiteID;
        subscriptionTemplate.WhereCondition = "TemplateType='S' AND TemplateSiteID=" + siteId;
        unsubscriptionTemplate.WhereCondition = "TemplateType='U' AND TemplateSiteID=" + siteId;
        issueTemplate.WhereCondition = "TemplateType='I' AND TemplateSiteID=" + siteId;
    }


    /// <summary>
    /// Enables and displays dialog for template based newsletter.
    /// </summary>
    protected void SetTypeToTemplateBased()
    {
        issueTemplate.Enabled = true;
        txtNewsletterDynamicURL.Enabled = false;
        lblSchedule.Visible = false;
        plcInterval.Visible = false;
    }


    /// <summary>
    /// Enables and displays dialog for template based newsletter.
    /// </summary>
    protected void SetTypeToDynamic()
    {
        issueTemplate.Enabled = false;
        txtNewsletterDynamicURL.Enabled = true;
        lblSchedule.Visible = true;
        plcInterval.Visible = true;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check 'configure' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "configure"))
        {
            RedirectToCMSDeskAccessDenied("cms.newsletter", "configure");
        }

        // Validate base inputs (newsletter display name, code name, sender name, sender e-mail)
        string errorMessage = new Validator().NotEmpty(txtNewsletterDisplayName.Text.Trim(), GetString("general.requiresdisplayname")).NotEmpty(txtNewsletterName.Text.Trim(), GetString("Newsletter_Edit.ErrorEmptyName")).NotEmpty(txtNewsletterSenderName.Text.Trim(), GetString("Newsletter_Edit.ErrorEmptySenderName")).NotEmpty(txtNewsletterSenderEmail.Text.Trim(), GetString("Newsletter_Edit.ErrorEmptySenderEmail")).IsEmail(txtNewsletterSenderEmail.Text.Trim(), GetString("Newsletter_Edit.ErrorEmailFormat")).IsCodeName(txtNewsletterName.Text.Trim(), GetString("general.invalidcodename")).Result;

        if (String.IsNullOrEmpty(errorMessage))
        {
            if (String.IsNullOrEmpty((string)subscriptionTemplate.Value))
            {
                // No subscription template selected
                lblSubscriptionError.Visible = true;
                return;
            }

            if (String.IsNullOrEmpty((string)unsubscriptionTemplate.Value))
            {
                // No unsubscription template selected
                lblUnsubscriptionError.Visible = true;
                return;
            }

            if ((radTemplateBased.Checked) && (String.IsNullOrEmpty((string)issueTemplate.Value)))
            {
                // No issue template selected for template based newsletter
                lblIssueError.Visible = true;
                return;
            }

            if ((radDynamic.Checked) && (!ScheduleInterval.CheckOneDayMinimum()))
            {
                // Problem occurred while setting schedule interval for dynamic newsletter
                lblError.Visible = true;
                lblError.Text = GetString("Newsletter_Edit.NoDaySelected");
                return;
            }

            // Newsletter code name must to be unique per site...
            Newsletter newsletterObj = NewsletterProvider.GetNewsletter(txtNewsletterName.Text.Trim(), CMSContext.CurrentSiteID);

            // If newsletter code name is unique...
            if (newsletterObj == null)
            {
                // Create new item -> insert
                newsletterObj = new Newsletter();

                newsletterObj.NewsletterDisplayName = txtNewsletterDisplayName.Text.Trim();
                newsletterObj.NewsletterName = txtNewsletterName.Text.Trim();

                int selectedValue = ValidationHelper.GetInteger(subscriptionTemplate.Value, 0);
                if (selectedValue != 0)
                {
                    newsletterObj.NewsletterSubscriptionTemplateID = selectedValue;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Newsletter_Edit.NoSubscriptionTemplateSelected");
                    return;
                }

                selectedValue = ValidationHelper.GetInteger(unsubscriptionTemplate.Value, 0);
                if (selectedValue != 0)
                {
                    newsletterObj.NewsletterUnsubscriptionTemplateID = selectedValue;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Newsletter_Edit.NoUnsubscriptionTemplateSelected");
                    return;
                }
                newsletterObj.NewsletterSenderName = txtNewsletterSenderName.Text.Trim();
                newsletterObj.NewsletterSenderEmail = txtNewsletterSenderEmail.Text.Trim();
                if (radDynamic.Checked)
                {
                    newsletterObj.NewsletterDynamicURL = txtNewsletterDynamicURL.Text.Trim();
                    newsletterObj.NewsletterType = NewsletterType.Dynamic;
                }
                else
                {
                    selectedValue = ValidationHelper.GetInteger(issueTemplate.Value, 0);
                    if (selectedValue != 0)
                    {
                        newsletterObj.NewsletterTemplateID = selectedValue;
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("Newsletter_Edit.NoEmailTemplateSelected");
                        return;
                    }
                    newsletterObj.NewsletterType = NewsletterType.TemplateBased;
                }
                newsletterObj.NewsletterSiteID = CMSContext.CurrentSiteID;
                newsletterObj.NewsletterGUID = Guid.NewGuid();
                newsletterObj.NewsletterLogActivity = true;

                if (NewsletterProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Newsletters, VersionActionEnum.Insert))
                {
                    // Create new newsletter
                    NewsletterProvider.SetNewsletter(newsletterObj);

                    if (radDynamic.Checked)
                    {
                        if (chkSchedule.Checked)
                        {
                            // Create new task to schedule the dynamic newsletter
                            TaskInfo task = new TaskInfo();
                            task.TaskAssemblyName = "CMS.Newsletter";
                            task.TaskClass = "CMS.Newsletter.DynamicNewsletterSender";
                            task.TaskDisplayName = GetString("DynamicNewsletter.TaskName") + newsletterObj.NewsletterDisplayName;
                            task.TaskEnabled = true;
                            task.TaskInterval = ScheduleInterval.ScheduleInterval;
                            task.TaskLastResult = string.Empty;
                            task.TaskName = "DynamicNewsletter." + ValidationHelper.GetCodeName(newsletterObj.NewsletterName, "_");
                            task.TaskSiteID = CMSContext.CurrentSiteID;
                            task.TaskNextRunTime = SchedulingHelper.GetNextTime(task.TaskInterval, new DateTime(), new DateTime());
                            task.TaskData = newsletterObj.NewsletterGUID.ToString();
                            // Set task for processing in external service
                            task.TaskAllowExternalService = true;
                            task.TaskUseExternalService = (SchedulingHelper.UseExternalService && NewsletterProvider.UseExternalServiceForDynamicNewsletters(CMSContext.CurrentSiteName));

                            TaskInfoProvider.SetTaskInfo(task);

                            newsletterObj.NewsletterDynamicScheduledTaskID = task.TaskID;
                            NewsletterProvider.SetNewsletter(newsletterObj);
                        }
                    }
                    URLHelper.Redirect("Newsletter_Frameset.aspx?newsletterid=" + ValidationHelper.GetString(newsletterObj.NewsletterID, null) + "&saved=1");
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("LicenseVersionCheck.Newsletter");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Newsletter_Edit.NewsletterNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    protected void rad_CheckedChanged(object sender, EventArgs e)
    {
        if (radDynamic.Checked)
        {
            // Display dialog for dynamic newsletter
            SetTypeToDynamic();
        }
        else
        {
            // Display dialog for template based newsletter
            SetTypeToTemplateBased();
        }
    }


    protected void chkSchedule_CheckedChanged(object sender, EventArgs e)
    {
        // Display/hide schedule interval control for dynamic newsletter
        ScheduleInterval.Visible = chkSchedule.Checked;
    }
}
