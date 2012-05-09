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

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_List : SiteManagerPage
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
        string script = @"function RefreshAdditionalContent(){
                            if (parent.parent.frames['pt_tree'])
                            {
                               parent.parent.frames['pt_tree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx?categoryid=" + categoryId) + @"';
                            }
                         }";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshTree", ScriptHelper.GetScript(script));

        // Configure the UniGrid
        PageTemplateCategoryInfo categoryInfo = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(categoryId);
        if (categoryInfo != null)
        {
            string categoryPath = categoryInfo.CategoryPath;
            // Add the slash character at the end of the categoryPath
            if (!categoryPath.EndsWith("/"))
            {
                categoryPath += "/";
            }
            pageTemplatesGrid.WhereCondition = "ObjectPath LIKE '" + SqlHelperClass.GetSafeQueryString(categoryPath, false) + "%' AND ObjectType = 'pagetemplate'";
        }
        pageTemplatesGrid.OnAction += pageTemplatesGrid_OnAction;
        pageTemplatesGrid.ZeroRowsText = GetString("general.nodatafound");

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
        switch (actionName)
        {
            case "delete":
                // Check 'Modify' permission
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.pagetemplate", "Modify"))
                {
                    RedirectToAccessDenied("cms.pagetemplate", "Modify");
                }

                // Delete PageTemplateInfo object from database
                PageTemplateInfoProvider.DeletePageTemplate(Convert.ToInt32(actionArgument));

                ltlScript.Text += ScriptHelper.GetScript("RefreshAdditionalContent();");
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
        Title = "Page template list";
    }

    #endregion
}
