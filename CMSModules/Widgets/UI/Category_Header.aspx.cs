using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.PortalEngine;

public partial class CMSModules_Widgets_UI_Category_Header : SiteManagerPage
{
    #region "Variables"

    protected int categoryId;
    protected string currentWidgetCategory = "";

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.Contains("categoryid"))
        {
            categoryId = QueryHelper.GetInteger("categoryid", 0);
            WidgetCategoryInfo categoryInfo = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(categoryId);
            if (categoryInfo != null)
            {
                currentWidgetCategory = HTMLHelper.HTMLEncode(categoryInfo.WidgetCategoryDisplayName);
            }
            else
            {
                // Set root category
                WidgetCategoryInfo rootCategory = WidgetCategoryInfoProvider.GetWidgetCategoryInfo("/");
                if (rootCategory != null)
                {
                    currentWidgetCategory = rootCategory.WidgetCategoryDisplayName;
                }
            }
        }

        if (QueryHelper.Contains("saved"))
        {
            this.CurrentMaster.Tabs.SelectedTab = 1;
        }

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        this.InitializeMasterPage();
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Initializes master page.
    /// </summary>
    protected void InitializeMasterPage()
    {        
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "widget_list";

        // initializes page title control		
        string[,] tabs = new string[2, 4];

        tabs[0, 0] = GetString("widgets.title");
        tabs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Category_Frameset.aspx");
        tabs[0, 2] = "_parent";
        tabs[0, 3] = "if (parent.parent.frames['widgettree']) { parent.parent.frames['widgettree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetTree.aspx") + "'; }";

        tabs[1, 0] = HTMLHelper.HTMLEncode(currentWidgetCategory);
        tabs[1, 1] = "";
        tabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = tabs;
        this.CurrentMaster.Title.TitleText = "";
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Widget/object.png");

    }


    /// <summary>
    /// Initializes user edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string generalString = GetString("general.general");
        string widgetsString = GetString("widgets.title");

        string[,] tabs = new string[2, 4];

        tabs[0, 0] = widgetsString;
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'widget_list');";
        tabs[0, 2] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Widget_List.aspx?categoryid=" + categoryId);

        tabs[1, 0] = generalString;
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'widget_category_general');";
        tabs[1, 2] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetCategory_Edit.aspx?categoryid=" + categoryId);

        this.CurrentMaster.Tabs.UrlTarget = "categoryContent";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
