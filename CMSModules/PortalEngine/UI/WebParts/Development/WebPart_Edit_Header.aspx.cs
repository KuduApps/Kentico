using System;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.IO;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Header : SiteManagerPage
{
    #region "Variables"

    protected int webpartid;
    protected WebPartInfo wpi = null;
    bool isInherited = false;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "read" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Webpart", "Read"))
        {
            RedirectToAccessDenied("CMS.Webpart", "Read");
        }

        webpartid = QueryHelper.GetInteger("webpartid", 0);
        wpi = WebPartInfoProvider.GetWebPartInfo(webpartid);

        if (wpi != null)
        {
            isInherited = (wpi.WebPartParentID > 0);

            // Initialize master page
            InitializeMasterPage();
        }

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Initialize master page.
    /// </summary>
    private void InitializeMasterPage()
    {
        this.Title = "WebParts - List";

        // Set the master page title
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Webpart/object.png");
        this.CurrentMaster.Title.HelpTopicName = "webpart_general";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes page title
        string[,] breadcrumbs = new string[3, 4];
        breadcrumbs[0, 0] = GetString("Development-WebPart_Edit.WebParts");
        breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Frameset.aspx");
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[0, 3] = "if (parent.parent.frames['webparttree']) { parent.parent.frames['webparttree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Tree.aspx") + "'; }";


        if (wpi != null)
        {
            if (isInherited)
            {
                this.CurrentMaster.Title.HelpTopicName = "webpart_general_inherited";
            }

            WebPartCategoryInfo categoryInfo = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(wpi.WebPartCategoryID);

            if (categoryInfo != null)
            {
                breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(categoryInfo.CategoryDisplayName);
                breadcrumbs[1, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Frameset.aspx?categoryid=" + wpi.WebPartCategoryID);
                breadcrumbs[1, 2] = "_parent";
                breadcrumbs[1, 3] = "if (parent.parent.frames['webparttree']) { parent.parent.frames['webparttree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Tree.aspx?categoryid=" + wpi.WebPartCategoryID) + "'; }";


                breadcrumbs[2, 0] = HTMLHelper.HTMLEncode(wpi.WebPartDisplayName);
                breadcrumbs[2, 1] = "";
                breadcrumbs[2, 2] = "";
            }
        }

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[8, 4];
        int i = 0;

        tabs[i, 0] = GetString("General.General");
        tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_general');";
        tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_General.aspx?webpartid=" + webpartid);
        if (isInherited)
        {
            tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_general_inherited');";
        }
        i++;

        tabs[i, 0] = GetString("WebParts.Properties");
        tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_properties');";
        tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_Properties.aspx?webpartid=" + webpartid);
        if (isInherited)
        {
            tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_properties_inherited');";
        }
        i++;

        tabs[i, 0] = GetString("WebParts.SystemProperties");
        tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_systemproperties');";
        tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_SystemProperties.aspx?webpartid=" + webpartid);
        i++;

        // Layout tab
        tabs[i, 0] = GetString("WebParts.Layout");
        tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_layout');";
        tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_Layout.aspx?webpartid=" + webpartid);
        i++;

        // CSS tab
        tabs[i, 0] = GetString("WebParts.CSS");
        tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_css');";
        tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_CSS.aspx?webpartid=" + webpartid);
        i++;

        if (!StorageHelper.IsExternalStorage)
        {
            // Theme tab
            tabs[i, 0] = GetString("Stylesheet.Theme");
            tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_theme');";
            tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_Theme.aspx?webpartid=" + webpartid);
            i++;
        }

        // Documentation tab
        tabs[i, 0] = GetString("WebParts.Documentation");
        tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_documentation');";
        tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_Documentation.aspx?webpartid=" + webpartid);
        i++;

        if (ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSDevelopmentMode"], false))
        {
            // Code tab
            tabs[i, 0] = GetString("WebParts.Code");
            tabs[i, 1] = "SetHelpTopic('helpTopic', 'webpart_code');";
            tabs[i, 2] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_Code.aspx?webpartid=" + webpartid);
            i++;
        }

        this.CurrentMaster.Tabs.UrlTarget = "webparteditcontent";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
