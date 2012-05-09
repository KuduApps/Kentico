using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Modules_Pages_Development_Module_UI_Tree : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int moduleId = QueryHelper.GetInteger("moduleid", 0);
        int parentId = QueryHelper.GetInteger("parentId", 0);

        ScriptHelper.RegisterJQuery(this.Page);

        // Use images according to culture
        if (CultureHelper.IsUICultureRTL())
        {
            this.uniTree.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            this.uniTree.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }

        if (!RequestHelper.IsPostBack())
        {
            // Get module root element
            UIElementInfo elemInfo = UIElementInfoProvider.GetRootUIElementInfo(moduleId);
            if (elemInfo != null)
            {
                this.uniTree.SelectPath = elemInfo.ElementIDPath;
                this.uniTree.ExpandPath = elemInfo.ElementIDPath + "/";
                this.menuElem.Value = elemInfo.ElementID + "|" + elemInfo.ElementParentID;
            }
            else
            {
                // Get current resource
                ResourceInfo resInfo = ResourceInfoProvider.GetResourceInfo(moduleId);
                if (resInfo != null)
                {
                    // Create new UI element 
                    elemInfo = new UIElementInfo();
                    elemInfo.ElementResourceID = moduleId;
                    elemInfo.ElementDisplayName = resInfo.ResourceDisplayName;
                    elemInfo.ElementName = resInfo.ResourceName.ToLower().Replace(".", "");
                    elemInfo.ElementIsCustom = false;

                    UIElementInfoProvider.SetUIElementInfo(elemInfo);
                    this.uniTree.SelectPath = elemInfo.ElementIDPath;
                    this.uniTree.ExpandPath = elemInfo.ElementIDPath;
                    this.menuElem.Value = elemInfo.ElementID + "|0";
                }
            }
        }

        this.menuElem.ResourceID = moduleId;
        this.menuElem.AfterAction += new OnActionEventHandler(menuElem_AfterAction);

        // Create and set UIElements provider
        UniTreeProvider elementProvider = new UniTreeProvider();
        elementProvider.ObjectType = "CMS.UIElement";
        elementProvider.DisplayNameColumn = "ElementDisplayName";
        elementProvider.IDColumn = "ElementID";
        elementProvider.LevelColumn = "ElementLevel";
        elementProvider.OrderColumn = "ElementOrder";
        elementProvider.ParentIDColumn = "ElementParentID";
        elementProvider.PathColumn = "ElementIDPath";
        elementProvider.ValueColumn = "ElementID";
        elementProvider.ChildCountColumn = "ElementChildCount";
        elementProvider.WhereCondition = "ElementResourceID = " + moduleId;
        elementProvider.Columns = "ElementID,ElementLevel,ElementOrder,ElementParentID,ElementIDPath,ElementChildCount,ElementDisplayName";

        this.uniTree.UsePostBack = false;
        this.uniTree.ProviderObject = elementProvider;
        this.uniTree.ExpandTooltip = GetString("general.expand");
        this.uniTree.CollapseTooltip = GetString("general.collapse");

        this.uniTree.NodeTemplate = "<span id=\"node_##NODEID##\" onclick=\"SelectNode(##NODEID##,##PARENTNODEID##," + moduleId + "); return false;\" name=\"treeNode\" class=\"ContentTreeItem\"><span class=\"Name\">##NODENAME##</span></span>";
        this.uniTree.SelectedNodeTemplate = "<span id=\"node_##NODEID##\" onclick=\"SelectNode(##NODEID##,##PARENTNODEID##," + moduleId + "); return false;\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\"><span class=\"Name\">##NODENAME##</span></span>";

        if (!RequestHelper.IsPostBack())
        {
            string selectedPath = QueryHelper.GetString("path", String.Empty);
            int elementId = QueryHelper.GetInteger("elementId", 0);

            if (!string.IsNullOrEmpty(selectedPath))
            {
                this.uniTree.SelectPath = selectedPath;
            }

            if (elementId > 0)
            {
                this.menuElem.ElementID = elementId;
                this.menuElem.ParentID = parentId;
                this.menuElem.Value = elementId + "|" + parentId;
            }
        }

        // Load data
        this.uniTree.ReloadData();

        string script = "var frameURL = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_EditFrameset.aspx") + "';";
        script += "var newURL = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_New.aspx") + "';";
        script += "var postParentId = " + parentId + ";";

        this.ltlScript.Text = ScriptHelper.GetScript(script);
    }


    protected void menuElem_AfterAction(string actionName, object actionArgument)
    {
        string[] split = actionArgument.ToString().Split('|');
        int elementId = ValidationHelper.GetInteger(split[0], 0);

        UIElementInfo elemInfo = UIElementInfoProvider.GetUIElementInfo(elementId);
        if (elemInfo != null)
        {
            this.uniTree.SelectPath = elemInfo.ElementIDPath;
            switch (actionName.ToLower())
            {
                case "delete":
                    this.uniTree.ExpandPath = elemInfo.ElementIDPath + "/";
                    // Reload header and content after save
                    StringBuilder sb = new StringBuilder();

                    sb.Append("if (window.parent != null) {");
                    sb.Append("if (window.parent.frames['uicontent'] != null) {");
                    if (elemInfo.ElementParentID > 0)
                    {
                        // If not root element load edit frameset
                        sb.Append("window.parent.frames['uicontent'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_EditFrameset.aspx") + "?moduleID=" + elemInfo.ElementResourceID + "&elementId=" + elemInfo.ElementID + "&parentId=" + elemInfo.ElementParentID + "';");
                    }
                    else
                    {
                        // Else load root info page
                        sb.Append("window.parent.frames['uicontent'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_New.aspx") + "?moduleID=" + elemInfo.ElementResourceID + "&parentId=0';");
                    }
                    sb.Append("}");
                    sb.Append("}");
                    this.ltlScript.Text += ScriptHelper.GetScript(sb.ToString());
                    // Update menu actions parameters
                    this.menuElem.Value = elemInfo.ElementID + "|" + elemInfo.ElementParentID;
                    break;
                case "moveup":
                case "movedown":
                    if (split.Length == 2)
                    {
                        this.ltlScript.Text += ScriptHelper.GetScript("window.tabIndex = " + split[1] + ";");
                    }
                    break;
            }
            this.ltlScript.Text += ScriptHelper.GetScript("var postParentId = " + elemInfo.ElementParentID + ";");
            // Load data
            this.uniTree.ReloadData();
        }
    }
}
