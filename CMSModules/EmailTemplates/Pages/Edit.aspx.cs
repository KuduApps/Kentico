using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.SiteProvider;
using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.URLRewritingEngine;

public partial class CMSModules_EmailTemplates_Pages_Edit : CMSEmailTemplatesPage
{

    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "Modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EmailTemplates", "Modify"))
        {
            RedirectToAccessDenied("CMS.EmailTemplates", "Modify");
        }

        // Register save button
        string[,] actions = new string[1, 9];
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("general.save");
        actions[0, 2] = null;
        actions[0, 3] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "lnksave_click";
        actions[0, 7] = null;
        actions[0, 8] = "true";

        this.CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        this.CurrentMaster.HeaderActions.Actions = actions;

        int templateId = QueryHelper.GetInteger("templateid", 0);
        string emailTemplateListUrl = "~/CMSModules/EmailTemplates/Pages/List.aspx";

        // Initialize the editing control
        emailTemplateEditElem.EmailTemplateID = templateId;

        int siteId = SiteID;

        if (siteId != 0)
        {
            emailTemplateListUrl += "?siteid=" + siteId;
        }

        if (siteId == 0)
        {
            siteId = SelectedSiteID;
            emailTemplateListUrl += "?selectedsiteid=" + SelectedSiteID;
        }

        if ((siteId == 0) && CMSContext.CurrentUser.UserSiteManagerAdmin)
        {
            emailTemplateEditElem.GlobalTemplate = true;
        }

        emailTemplateEditElem.SiteID = siteId;

        // Initializes page title breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("EmailTemplate_Edit.EmailTemplates");
        pageTitleTabs[0, 1] = emailTemplateListUrl;

        if (templateId > 0)
        {
            // Get email template info
            EmailTemplateInfo templateInfo = EmailTemplateProvider.GetEmailTemplate(templateId);

            // Check that edited template belongs to selected site
            if ((siteId > 0) && (templateInfo != null) && (templateInfo.TemplateSiteID != siteId))
            {
                RedirectToAccessDenied(null);
            }

            SetEditedObject(templateInfo, "Frameset.aspx");

            pageTitleTabs[1, 0] = templateInfo.TemplateDisplayName;
            this.CurrentMaster.Title.TitleText = GetString("EmailTemplate_Edit.Title");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_EmailTemplate/object.png");
        }
        else
        {
            pageTitleTabs[1, 0] = GetString("EmailTemplate_Edit.CurrentTemplate");
            this.CurrentMaster.Title.TitleText = GetString("EmailTemplate_Edit.TitleNew");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_EmailTemplate/new.png");
        }
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        // Pagetitle
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.HelpTopicName = "newedit_e_mail_template";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }

    #endregion

}
