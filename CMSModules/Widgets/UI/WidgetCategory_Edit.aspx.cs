using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_Widgets_UI_WidgetCategory_Edit : SiteManagerPage
{
    #region "Variables"

    // indicates if the page is used as a "new category" or "edit existing category"
    private bool isNew = false;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        categoryEdit.ItemID = QueryHelper.GetInteger("categoryid", 0);
        
        int parentCategoryId = QueryHelper.GetInteger("parentid", 0);
        categoryEdit.ParentCategoryID = parentCategoryId;

        // Edit cateogry
        if (categoryEdit.ItemID > 0)
        {
            WidgetCategoryInfo wci = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(categoryEdit.ItemID);
            if (wci != null)
            {
                // Set already loaded object to inner control
                categoryEdit.CategoryInfo = wci;

                // Set masterpage
                this.CurrentMaster.Title.TitleText = "";
                this.CurrentMaster.Title.Breadcrumbs = null;
            }
        }
        // New category
        else
        {
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.HelpTopicName = "widget_category_general";

            isNew = true;

            WidgetCategoryInfo parentCategoryInfo = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(parentCategoryId);

            string parentCategoryName = GetString("development.pagetemplates");
            if (parentCategoryInfo != null)
            {
                parentCategoryName = parentCategoryInfo.WidgetCategoryDisplayName;
            }

            string[,] pageTitleTabs = new string[3, 4];

            pageTitleTabs[0, 0] = GetString("widgets.title");
            pageTitleTabs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Category_Frameset.aspx");
            pageTitleTabs[0, 2] = "";
            pageTitleTabs[0, 3] = "if (parent.frames['widgettree']) { parent.frames['widgettree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetTree.aspx") + "'; }";

            pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(parentCategoryName);
            pageTitleTabs[1, 1] = URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/Category_Frameset.aspx?categoryid=" + parentCategoryId);
            pageTitleTabs[1, 2] = "";

            pageTitleTabs[2, 0] = GetString("widgets.category.titlenew");
            pageTitleTabs[2, 1] = "";
            pageTitleTabs[2, 2] = "";

            // Set masterpage
            this.CurrentMaster.Title.TitleText = GetString("widget.category");
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WidgetCategory/new.png");
            this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        }

        categoryEdit.OnSaved += new EventHandler(categoryEdit_OnSaved);
    }


    protected void categoryEdit_OnSaved(object sender, EventArgs e)
    {
        if (categoryEdit.CategoryInfo != null)
        {
            URLHelper.Redirect("~/CMSModules/Widgets/UI/WidgetCategory_Edit.aspx?categoryid=" + categoryEdit.CategoryInfo.WidgetCategoryID + "&parentid=" + categoryEdit.CategoryInfo.WidgetCategoryParentID + "&" + (isNew ? "new=1" : "saved=1"));
        }
    }

    #endregion
}
