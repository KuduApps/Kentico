using System;
using System.Collections.Generic;
using System.Data;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Newsletters_Tools_Templates_NewsletterTemplate_List : CMSNewsletterTemplatesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Add subscriber link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("NewsletterTemplate_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("NewsletterTemplate_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Newsletter_EmailTemplate/add.png");
        CurrentMaster.HeaderActions.Actions = actions;

        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.WhereCondition = "(TemplateSiteID = " + CMSContext.CurrentSiteID + ")";
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
    }


    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            // Initialize template type column
            case "templatetype":
                switch (parameter.ToString().ToLower())
                {
                    case "u":
                        return GetString("NewsletterTemplate_List.Unsubscription");

                    case "s":
                        return GetString("NewsletterTemplate_List.Subscription");

                    case "d":
                        return GetString("NewsletterTemplate_List.OptIn");

                    default:
                        return GetString("NewsletterTemplate_List.Issue");
                }
        }
        return parameter;
    }


    /// <summary>
    /// Increment counter at the end of string.
    /// </summary>
    /// <param name="s">String</param>
    /// <param name="lpar">Left parathenses</param>
    /// <param name="rpar">Right parathenses</param>
    string Increment(string s, string lpar, string rpar)
    {
        int i = 1;
        s = s.Trim();
        if ((rpar == String.Empty) || s.EndsWith(rpar))
        {
            int leftpar = s.LastIndexOf(lpar);
            if (lpar == rpar)
            {
                leftpar = s.LastIndexOf(lpar, leftpar - 1);
            }

            if (leftpar >= 0)
            {
                i = ValidationHelper.GetSafeInteger(s.Substring(leftpar + lpar.Length, s.Length - leftpar - lpar.Length - rpar.Length), 0);
                // Remove parathenses only if parathenses found
                if (i > 0)
                {
                    s = s.Remove(leftpar);
                }
                i++;
            }
        }

        s += lpar + i + rpar;
        return s;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        string templateId = actionArgument.ToString();

        switch (actionName.ToLower())
        {
            // Edit the template
            case "edit":
                URLHelper.Redirect("NewsletterTemplate_Edit.aspx?templateid=" + templateId);
                break;

            // Delete the template
            case "delete":
                // Check 'Manage templates' permission
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managetemplates"))
                {
                    RedirectToCMSDeskAccessDenied("cms.newsletter", "managetemplates");
                }

                // Check if the template is used in a newsletter
                string where = string.Format("(NewsletterTemplateID={0}) OR (NewsletterSubscriptionTemplateID={0}) OR (NewsletterUnsubscriptionTemplateID={0}) OR (NewsletterOptInTemplateID={0})", templateId);

                DataSet newsByEmailtempl = NewsletterProvider.GetNewsletters(where, null, 1, "NewsletterID");
                if (DataHelper.DataSourceIsEmpty(newsByEmailtempl))
                {
                    // Check if the template is used in an issue
                    DataSet newsletterIssues = IssueProvider.GetIssues("IssueTemplateID = " + templateId, null, 1, "IssueID");
                    if (DataHelper.DataSourceIsEmpty(newsletterIssues))
                    {
                        // Delete EmailTemplate object from database
                        EmailTemplateProvider.DeleteEmailTemplate(ValidationHelper.GetInteger(templateId, 0));
                    }
                    else
                    {
                        ShowError(GetString("NewsletterTemplate_List.TemplateInUseByNewsletterIssue"));
                    }
                }
                else
                {
                    ShowError(GetString("NewsletterTemplate_List.TemplateInUseByNewsletter"));
                }
                break;

            // Clone the template
            case "clone":
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.newsletter", "managetemplates"))
                {
                    RedirectToCMSDeskAccessDenied("cms.newsletter", "managetemplates");
                }

                int tmpId = ValidationHelper.GetInteger(templateId, 0);
                EmailTemplate oldet = EmailTemplateProvider.GetEmailTemplate(tmpId);
                if (oldet != null)
                {
                    EmailTemplate et = new EmailTemplate();
                    et.TemplateBody = oldet.TemplateBody;
                    et.TemplateDisplayName = oldet.TemplateDisplayName;
                    et.TemplateSubject = oldet.TemplateSubject;
                    et.TemplateFooter = oldet.TemplateFooter;
                    et.TemplateHeader = oldet.TemplateHeader;
                    et.TemplateID = 0;
                    et.TemplateName = oldet.TemplateName;
                    et.TemplateSiteID = oldet.TemplateSiteID;
                    et.TemplateStylesheetText = oldet.TemplateStylesheetText;
                    et.TemplateType = oldet.TemplateType;

                    string templateName = et.TemplateName;
                    string templateDisplayName = et.TemplateDisplayName;

                    while (EmailTemplateProvider.GetEmailTemplate(templateName, et.TemplateSiteID) != null)
                    {
                        templateName = Increment(templateName, "_", "");
                        templateDisplayName = Increment(templateDisplayName, "(", ")");
                    }

                    et.TemplateName = templateName;
                    et.TemplateDisplayName = templateDisplayName;

                    // Get new ID
                    using (CMSActionContext context = new CMSActionContext())
                    {
                        // Disable versioning to prevent creating new version on first set
                        context.CreateVersion = false;

                        EmailTemplateProvider.SetEmailTemplate(et);
                    }

                    List<Guid> convTable = new List<Guid>();

                    try
                    {
                        MetaFileInfoProvider.CopyMetaFiles(tmpId, et.TemplateID, NewsletterObjectType.NEWSLETTERTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE, convTable);
                    }
                    catch (Exception e)
                    {
                        ShowError(e.Message);
                        EmailTemplateProvider.DeleteEmailTemplate(et);
                        return;
                    }

                    for (int i = 0; i < convTable.Count; i += 2)
                    {
                        et.TemplateBody = et.TemplateBody.Replace(convTable[i].ToString(), convTable[i + 1].ToString());
                    }

                    EmailTemplateProvider.SetEmailTemplate(et);
                }
                break;
        }
    }
}