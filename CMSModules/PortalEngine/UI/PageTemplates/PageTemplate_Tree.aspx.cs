using System;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Tree : SiteManagerPage, IPostBackEventHandler
{
    #region "Variables"

    protected bool nodeIsSelected = false;
    protected int pageTemplateId = 0;
    protected int categoryId = 0;
    protected bool expand = false;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        treeElem.UseGlobalSettings = true;

        // Register scripts
        ScriptHelper.RegisterJQuery(Page);
        RegisterExportScript();

        // Images
        imgNewCategory.ImageUrl = GetImageUrl("Objects/CMS_PageTemplateCategory/new.png");
        imgNewTemplate.ImageUrl = GetImageUrl("CMSModules/CMS_PageTemplates/addpagetemplate.png");
        imgDeleteItem.ImageUrl = GetImageUrl("CMSModules/CMS_PageTemplates/delete.png");
        imgExportObject.ImageUrl = GetImageUrl("CMSModules/CMS_PageTemplates/exportobject.png");

        // Resource strings
        lnkDeleteItem.Text = GetString("Development-PageTemplates_Tree.DeleteSelectedItem");
        lnkNewCategory.Text = GetString("Development-PageTemplates_Tree.NewCategory");
        lnkNewTemplate.Text = GetString("Development-PageTemplates_Tree.NewTemplate");
        lnkExportObject.Text = GetString("Development-PageTemplates_Tree.ExportObject");

        // Setup menu action scripts
        lnkNewTemplate.Attributes.Add("onclick", "NewItem('pagetemplate');");
        lnkNewCategory.Attributes.Add("onclick", "NewItem('pagetemplatecategory');");
        lnkDeleteItem.Attributes.Add("onclick", "DeleteItem();");
        lnkExportObject.Attributes.Add("onclick", "ExportObject();");

        // Tooltips
        lnkDeleteItem.ToolTip = GetString("Development-PageTemplates_Tree.DeleteSelectedItem");
        lnkNewCategory.ToolTip = GetString("Development-PageTemplates_Tree.NewCategory");
        lnkNewTemplate.ToolTip = GetString("Development-PageTemplates_Tree.NewTemplate");
        lnkExportObject.ToolTip = GetString("Development-PageTemplates_Tree.ExportObject");

        string script = "var doNotReloadContent = false;\n";

        // URLs for menu actions
        script += " var pageTemplateCategoryURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx") + "';";
        script += " var pageTemplateCategoryNewURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Category.aspx") + "';";
        script += " var pageTemplateURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Edit.aspx") + "';";
        script += " var pageTemplateNewURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_New.aspx") + "';";
        script += " var pageTemplateTreeURL = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx") + "';";

        // Setup tree element        
        treeElem.SelectPageTemplates = true;
        treeElem.UsePostBack = false;

        // Script for deleting widget or category
        string delPostback = ControlsHelper.GetPostBackEventReference(Page, "##");
        string deleteScript = "function DeleteItem() { \n" +
                                " if ((selectedItemId > 0) && (selectedItemParent > 0) && " +
                                " confirm('" + GetString("general.deleteconfirmation") + "')) {\n " +
                                    delPostback.Replace("'##'", "selectedItemType+';'+selectedItemId+';'+selectedItemParent") + ";\n" +
                                "}\n" +
                              "}\n";
        script += deleteScript;

        // Preselect tree item
        if (!RequestHelper.IsPostBack())
        {
            // If template was edited select this template
            pageTemplateId = QueryHelper.GetInteger("templateid", 0);
            // Category
            categoryId = QueryHelper.GetInteger("categoryid", 0);
            // Parent category
            categoryId = QueryHelper.GetInteger("parentcategoryid", categoryId);

            bool reload = QueryHelper.GetBoolean("reload", false);

            // Select category
            if (categoryId > 0)
            {
                PageTemplateCategoryInfo ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(categoryId);
                if (ptci != null)
                {
                    // If not set explicitly stop reloading of right frame
                    if (!reload)
                    {
                        script += "doNotReloadContent = true;";
                    }
                    script += SelectAtferLoad(ptci.CategoryPath, categoryId, "pagetemplatecategory", ptci.ParentId, true);
                }
            }
            // Select widget
            else if (pageTemplateId > 0)
            {
                PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplateId);
                if (pti != null)
                {
                    PageTemplateCategoryInfo ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(pti.CategoryID);
                    if (ptci != null)
                    {
                        // If not set explicitly stop reloading of right frame
                        if (!reload)
                        {
                            script += "doNotReloadContent = true;";
                        }
                        string path = ptci.CategoryPath + "/" + pti.CodeName;
                        script += SelectAtferLoad(path, pageTemplateId, "pagetemplate", pti.CategoryID, pti.IsReusable);
                    }
                }
            }
            // Select root by default
            else
            {
                PageTemplateCategoryInfo ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfoByCodeName("/");
                if (ptci != null)
                {
                    script += SelectAtferLoad("/", ptci.CategoryId, "pagetemplatecategory", 0, true);
                }
            }
        }

        ltlScript.Text += ScriptHelper.GetScript(script);

        // Special browser class for RTL scrollbars correction
        pnlSubBox.CssClass = BrowserHelper.GetBrowserClass();
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Expands tree at specified path and selects tree item by javascript.
    /// </summary>
    /// <param name="path">Selected path</param>
    /// <param name="itemId">ID of selected tree item</param>
    /// <param name="type">Type of tree item</param>
    /// <param name="parentId">ID of parent</param>
    /// <param name="isReusable">Idnicates if page template is reusable</param>
    private string SelectAtferLoad(string path, int itemId, string type, int parentId, bool isReusable)
    {
        treeElem.SelectPath = path;
        string script = String.Format("SelectNode({0},'{1}',{2},'{3}');", itemId, type, parentId, isReusable ? "1" : "0");
        return script;
    }


    public void RaisePostBackEvent(string eventArgument)
    {
        string[] values = eventArgument.Split(';');
        if ((values != null) && (values.Length == 3))
        {
            int id = ValidationHelper.GetInteger(values[1], 0);
            int parentId = ValidationHelper.GetInteger(values[2], 0);
            string script = String.Empty;

            switch (values[0])
            {
                case "pagetemplate":
                    PageTemplateInfoProvider.DeletePageTemplate(id);
                    break;

                case "pagetemplatecategory":
                    // Recursively delete template category and all its descendants
                    PageTemplateCategoryInfoProvider.DeletePageTemplateCategory(id);
                    break;
            }

            // Select parent node after delete
            PageTemplateCategoryInfo ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(parentId);
            if (ptci != null)
            {
                script = SelectAtferLoad(ptci.CategoryPath, parentId, "pagetemplatecategory", ptci.ParentId, true) + script;
                ltlScript.Text += ScriptHelper.GetScript(script);
            }

            treeElem.ReloadData();
        }
    }

    #endregion
}
