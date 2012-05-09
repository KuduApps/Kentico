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

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_List : SiteManagerPage
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
        
        // Used for delete calls
        int webpartId = QueryHelper.GetInteger("webpartid", 0);

        string script = @"function RefreshAdditionalContent() {
                            if (parent.parent.frames['webparttree'])
                            {
                                parent.parent.frames['webparttree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Tree.aspx?categoryid=" + categoryId) + @"';
                            }
                        }";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshTree", ScriptHelper.GetScript(script));

        // Configure the UniGrid
        WebPartCategoryInfo categoryInfo = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(categoryId);
        if (categoryInfo != null)
        {
            string categoryPath = categoryInfo.CategoryPath;
            // Add the slash character at the end of the categoryPath
            if (!categoryPath.EndsWith("/"))
            {
                categoryPath += "/";
            }
            webpartGrid.WhereCondition = "ObjectPath LIKE '" + SqlHelperClass.GetSafeQueryString(categoryPath, false) + "%' AND ObjectType = 'webpart'";
        }
        webpartGrid.OnAction += webpartGrid_OnAction;
        webpartGrid.ZeroRowsText = GetString("general.nodatafound");

        InitializeMasterPage();
    }

    #endregion


    #region "UniGrid events" 

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void webpartGrid_OnAction(string actionName, object actionArgument)
    {
        int webpartId = Convert.ToInt32(actionArgument);

        switch (actionName)
        {
            case "delete":
                // Check 'Modify' permission
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.webpart", "Modify"))
                {
                    RedirectToAccessDenied("cms.webpart", "Modify");
                }

                // Delete PageTemplateInfo object from database
                WebPartInfoProvider.DeleteWebPartInfo(webpartId);

                // Refresh tree
                ltlScript.Text = ScriptHelper.GetScript("RefreshAdditionalContent();");
                break;

            case "clone":
                string scriptDialog = "modalDialog('" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Clone.aspx") + "?webpartid=" + webpartId + "&reloadAll=1','WebPartClone', 500, 250);";
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
        Title = "Web part list";
    }

    #endregion
}
