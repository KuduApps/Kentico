using System;

using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.IO;

// Set edited object
[EditedObject("cms.pagetemplate", "templateid")]

// Set tabs number and ensure additional tab
[Tabs(10, "pt_edit_content")]
public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Header : CMSEditTemplatePage
{
    protected bool isDialog = false;


    #region "Page events"

    /// <summary>
    /// PreInit event handler.
    /// </summary>
    protected override void OnPreInit(EventArgs e)
    {
        isDialog = QueryHelper.GetBoolean("dialog", false);

        // Change master page for dialog
        if (isDialog)
        {
            this.MasterPageFile = "~/CMSMasterPages/UI/Dialogs/TabsHeader.master";
        }

        base.OnPreInit(e);
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        int pageTemplateId = QueryHelper.GetInteger("templateid", 0);
        if (pageTemplateId != 0)
        {
            // Initialize page title
            string templates = GetString("Administration-PageTemplate_Header.Templates");
            string title = GetString("Administration-PageTemplate_Header.TemplateProperties");
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplateId);

            // Check if the object exists - if not->redirect and inform the user
            EditedObject = pti;

            PageTemplateCategoryInfo categoryInfo = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(pti.CategoryID);
            string currentPageTemplate = HTMLHelper.HTMLEncode(pti.DisplayName);
            string[,] breadcrumbs;

            if (!isDialog)
            {
                breadcrumbs = new string[3, 4];

                breadcrumbs[0, 0] = GetString("development.pagetemplates");
                breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx");
                breadcrumbs[0, 2] = "_parent";
                breadcrumbs[0, 3] = "if (parent.parent.frames['pt_tree']) { parent.parent.frames['pt_tree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx") + "'; }";

                if (categoryInfo != null)
                {
                    breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(categoryInfo.DisplayName);
                    breadcrumbs[1, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx?categoryid=" + pti.CategoryID);
                    breadcrumbs[1, 2] = "_parent";
                    breadcrumbs[1, 3] = "if (parent.parent.frames['pt_tree']) { parent.parent.frames['pt_tree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx?categoryid=" + pti.CategoryID) + "'; }";

                    breadcrumbs[2, 0] = HTMLHelper.HTMLEncode(currentPageTemplate);
                    breadcrumbs[2, 1] = "";
                    breadcrumbs[2, 2] = "";
                }
            }
            else
            {
                breadcrumbs = new string[2, 3];

                breadcrumbs[0, 0] = GetString("development.pagetemplates"); ;
                breadcrumbs[0, 1] = "";
                breadcrumbs[0, 2] = "";

                breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(currentPageTemplate);
                breadcrumbs[1, 1] = "";
                breadcrumbs[1, 2] = "";

                if (pti.IsReusable)
                {
                    this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_PageTemplates/pagetemplate.png");
                }
                else
                {
                    this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_PageTemplates/adhoc.png");
                }
                this.CurrentMaster.Title.TitleText = title;
            }

            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
            this.CurrentMaster.Title.HelpTopicName = "general_tab12";
            this.CurrentMaster.Title.HelpName = "helpTopic";

            // Initialize menu
            int i = 0;

            string dialog = String.Empty;
            if (isDialog)
            {
                dialog = "&dialog=1";
            }

            // General tab
            SetTab(i, GetString("general.general"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_General.aspx?templateid=" + pageTemplateId + dialog), "SetHelpTopic('helpTopic', 'general_tab12');");
            i++;

            bool showDesign = ((pti.PageTemplateType == PageTemplateTypeEnum.Portal) || (pti.PageTemplateType == PageTemplateTypeEnum.Dashboard));

            // Design tab
            if (!isDialog && showDesign)
            {
                SetTab(i, GetString("edittabs.design"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Design.aspx?templateid=" + pageTemplateId), "SetHelpTopic('helpTopic', 'design_tab2');");
                i++;
            }

            CurrentUserInfo user = CMSContext.CurrentUser;
            if (!isDialog && (pti.IsReusable) && user.UserSiteManagerAdmin)
            {
                // Sites tab
                SetTab(i, GetString("general.sites"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Sites.aspx?templateid=" + pageTemplateId), "SetHelpTopic('helpTopic', 'sites_tab2');");
                i++;

                if (pti.PageTemplateType != PageTemplateTypeEnum.Dashboard)
                {
                    // Scopes tab
                    SetTab(i, GetString("pagetemplate.edit.scopes"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Scopes/PageTemplateScopes_List.aspx?templateid=" + pageTemplateId), "SetHelpTopic('helpTopic', 'page_templates_scopes');");
                    i++;
                }
            }

            if (showDesign)
            {
                // Layouts tab
                SetTab(i, GetString("Administration-PageTemplate_Header.Layouts"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Layouts.aspx?templateid=" + pageTemplateId + dialog), "SetHelpTopic('helpTopic', 'layout');");
                i++;

                if (pti.IsReusable && !StorageHelper.IsExternalStorage && user.UserSiteManagerAdmin)
                {
                    // Theme tab
                    SetTab(i, GetString("Stylesheet.Theme"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Theme.aspx?templateid=" + pageTemplateId + dialog), "SetHelpTopic('helpTopic', 'page_templates_theme');");
                    i++;
                }

                // Web parts tab
                if ((!isDialog || SettingsKeyProvider.DevelopmentMode) && CurrentUser.IsGlobalAdministrator)
                {
                    SetTab(i, GetString("Administration-PageTemplate_Header.WebParts"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_WebParts.aspx?templateid=" + pageTemplateId), "SetHelpTopic('helpTopic', 'web_parts');");
                    i++;

                    if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSShowTemplateASPXTab"], false))
                    {
                        // ASPX tab
                        SetTab(i, GetString("Administration-PageTemplate_Header.ASPX"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_ASPX.aspx?templateid=" + pageTemplateId), "SetHelpTopic('helpTopic', 'page_templates_aspx_code');");
                        i++;
                    }
                }
            }

            if (pti.PageTemplateType != PageTemplateTypeEnum.Dashboard)
            {
                // Header tab
                SetTab(i, GetString("Administration-PageTemplate_Header.Header"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_HeaderTab.aspx?templateid=" + pageTemplateId + dialog), "SetHelpTopic('helpTopic', 'header');");
                i++;

                // Documents tab
                SetTab(i, GetString("general.documents"), URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Documents.aspx?templateid=" + pageTemplateId + dialog), "SetHelpTopic('helpTopic', 'page_templates_documents');");
                i++;
            }
        }
    }

    #endregion
}
