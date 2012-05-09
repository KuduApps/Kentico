using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Widgets_UI_Widget_Edit_Header : SiteManagerPage
{
    #region "Variables"

    private int widgetId = 0;
    private WidgetInfo widget = null;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "read" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Widget", "Read"))
        {
            RedirectToAccessDenied("CMS.Widget", "Read");
        }

        widgetId = QueryHelper.GetInteger("widgetId", 0);
        widget = WidgetInfoProvider.GetWidgetInfo(widgetId);

        if (widget != null)
        {
            string currentWidget = widget.WidgetDisplayName;
            WidgetCategoryInfo categoryInfo = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(widget.WidgetCategoryID);

            // Initialize Master Page
            string[,] pageTitleTabs = new string[3, 4];

            pageTitleTabs[0, 0] = GetString("widgets.title");
            pageTitleTabs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Category_Frameset.aspx");
            pageTitleTabs[0, 2] = "_parent";
            pageTitleTabs[0, 3] = "if (parent.parent.frames['widgettree']) { parent.parent.frames['widgettree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetTree.aspx") + "'; }";

            if (categoryInfo != null)
            {
                pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(categoryInfo.WidgetCategoryDisplayName);
                pageTitleTabs[1, 1] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Category_Frameset.aspx?categoryid=" + widget.WidgetCategoryID);
                pageTitleTabs[1, 2] = "_parent";
                pageTitleTabs[1, 3] = "if (parent.parent.frames['widgettree']) { parent.parent.frames['widgettree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetTree.aspx?categoryid=" + widget.WidgetCategoryID) + "'; }";

                pageTitleTabs[2, 0] = HTMLHelper.HTMLEncode(currentWidget);
                pageTitleTabs[2, 1] = "";
                pageTitleTabs[2, 2] = "";
            }

            // Set masterpage        
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Widget/object.png");
            this.CurrentMaster.Title.HelpTopicName = "widget_general";
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

            // Tabs
            InitalizeTabs();
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Initializes tabs.
    /// </summary>
    protected void InitalizeTabs()
    {
        string[,] tabs = new string[5, 4];

        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'widget_general');";
        tabs[0, 2] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_General.aspx?widgetid=" + widgetId);

        tabs[1, 0] = GetString("general.properties");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'widget_properties');";
        tabs[1, 2] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_Properties.aspx?widgetid=" + widgetId);

        tabs[2, 0] = GetString("WebParts.SystemProperties");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'widget_systemproperties');";
        tabs[2, 2] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_SystemProperties.aspx?widgetid=" + widgetId);

        tabs[3, 0] = GetString("webparts.documentation");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'widget_documentation');";
        tabs[3, 2] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_Documentation.aspx?widgetid=" + widgetId);

        tabs[4, 0] = GetString("general.security");
        tabs[4, 1] = "SetHelpTopic('helpTopic', 'widget_security');";
        tabs[4, 2] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_Edit_Security.aspx?widgetid=" + widgetId);

        CurrentMaster.Tabs.UrlTarget = "widgeteditcontent";
        CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
