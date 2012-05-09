using System;
using System.Text;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Edit_EditMenu : CMSContentPage
{
    #region "Variables"

    private bool mEnableMenu = true;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        string mode = QueryHelper.GetString("mode", string.Empty).ToLower();
        string elementName = null;

        if (mode == "editform")
        {
            elementName = "EditForm";
        }
        else if (mode == "edit")
        {
            elementName = "Page";
        }

        if (elementName != null)
        {
            // Check UIProfile
            if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", elementName))
            {
                mEnableMenu = false;
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register scripts
        ScriptHelper.RegisterProgress(Page);

        // Get the document ID
        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        string action = QueryHelper.GetString("action", string.Empty).ToLower();

        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);

        if (node != null)
        {
            if (node.NodeClassName.ToLower() == "cms.root")
            {
                menuElem.AllowSave = false;
            }
        }
        else
        {
            menuElem.Visible = false;
        }

        if (action == "new")
        {
            // Show the context help button for the CMS.MenuItem doc type and any other which has the attribute "ShowTemplateSelection" set.
            int classId = QueryHelper.GetInteger("classid", 0);
            DataClassInfo dataClassObj = DataClassInfoProvider.GetDataClass(classId);
            if ((dataClassObj != null) && (!string.IsNullOrEmpty(dataClassObj.ClassName)))
            {
                if (dataClassObj.ClassName.Equals("cms.menuitem", StringComparison.InvariantCultureIgnoreCase)
                    || dataClassObj.ClassShowTemplateSelection)
                {
                    menuElem.HelpTopicName = "CMS_Content_Templates";
                }
            }
        }

        // Context menu help for new A/B test variant
        if (action == "newvariant")
        {
            menuElem.HelpTopicName = "CMS_Content_New_ABvariant";
        }

        StringBuilder script = new StringBuilder();
        script.AppendLine("function PassiveRefresh(nodeId) {");
        script.AppendLine("    if (parent.frames['editview'] != null) {");
        script.AppendLine("        if (parent.frames['editview'].NotChanged) {");
        script.AppendLine("            parent.frames['editview'].NotChanged();");
        script.AppendLine("        }");
        script.AppendLine("        parent.frames['editview'].location.replace(parent.frames['editview'].location);");
        script.AppendLine("    }");
        script.AppendLine("}");

        script.AppendLine("function RefreshTree(expandNodeId, selectNodeId) {");
        script.AppendLine("    parent.RefreshTree(expandNodeId, selectNodeId);");
        script.AppendLine("}");

        script.AppendLine("var isSideWindow = true;");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "passiveRefresh", ScriptHelper.GetScript(script.ToString()));
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Enable/disable menu
        pnlMenu.Enabled = mEnableMenu;
    }

    #endregion
}
