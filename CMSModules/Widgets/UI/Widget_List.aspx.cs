using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.PortalEngine;
using CMS.SettingsProvider;

public partial class CMSModules_Widgets_UI_Widget_List : SiteManagerPage
{
    #region "Variables"

    protected int categoryId = 0;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        categoryId = QueryHelper.GetInteger("categoryid", 0);
        
        // Register script for refresh tree after delete/destroy
        string script = @"function RefreshAdditionalContent() {
                            if (parent.parent.frames['widgettree'])
                            {
                                parent.parent.frames['widgettree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/Widgets/UI/WidgetTree.aspx?categoryid=" + categoryId) + @"';
                            }
                        }";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshTree", ScriptHelper.GetScript(script));

        // Used for delete calls
        int widgetId = QueryHelper.GetInteger("widgetid", 0);

        // Configure the UniGrid
        WidgetCategoryInfo categoryInfo = WidgetCategoryInfoProvider.GetWidgetCategoryInfo(categoryId);
        if (categoryInfo != null)
        {
            string categoryPath = categoryInfo.WidgetCategoryPath;
            // Add the slash character at the end of the categoryPath
            if (!categoryPath.EndsWith("/"))
            {
                categoryPath += "/";
            }
            widgetGrid.WhereCondition = "ObjectPath LIKE '" + SqlHelperClass.GetSafeQueryString(categoryPath, false) + "%' AND ObjectType = 'widget'";
        }
        widgetGrid.OnAction += pageTemplatesGrid_OnAction;
        widgetGrid.ZeroRowsText = GetString("general.nodatafound");

        InitializeMasterPage();
    }

    #endregion


    #region "UniGrid events"

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void pageTemplatesGrid_OnAction(string actionName, object actionArgument)
    {
        int widgetId = Convert.ToInt32(actionArgument);

        switch (actionName)
        {
            case "delete":
                // Check 'Modify' permission
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.widget", "Modify"))
                {
                    RedirectToAccessDenied("cms.widget", "Modify");
                }

                // delete PageTemplateInfo object from database
                WidgetInfoProvider.DeleteWidgetInfo(widgetId);

                // Refresh tree
                ltlScript.Text = ScriptHelper.GetScript("RefreshAdditionalContent();");
                break;

            case "clone":
                string scriptDialog = "modalDialog('" + URLHelper.ResolveUrl("~/CMSModules/Widgets/Dialogs/Widget_Clone.aspx") + "?widgetid=" + widgetId + "&reloadAll=0','WidgetClone', 500, 250);";
                ltlScript.Text = ScriptHelper.GetScript(scriptDialog);
                break;
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        Title = "Widget list";
    }

    #endregion
}
