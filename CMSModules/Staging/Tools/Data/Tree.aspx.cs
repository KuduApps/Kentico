using System;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Data_Tree : CMSStagingDataPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Manage data tasks' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageDataTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageDataTasks");
        }

        if (CultureHelper.IsUICultureRTL())
        {
            objectTree.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            objectTree.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }
        objectTree.ImageSet = TreeViewImageSet.Custom;
        objectTree.ExpandImageToolTip = GetString("General.Expand");
        objectTree.CollapseImageToolTip = GetString("General.Collapse");

        CreateTreeView();
    }


    /// <summary>
    /// Creates tree view.
    /// </summary>
    private void CreateTreeView()
    {
        objectTree.Nodes.Clear();

        // Fill in the custom tables
        TreeNode rootNode = new TreeNode();
        rootNode.Text = "<span class=\"ContentTreeSelectedItem\" id=\"treeSelectedNode\" onclick=\"SelectNode('##ALL##', this); \"><span class=\"Name\">" + GetString("DataStaging.RootNodeText") + "</span></span>";
        rootNode.Expanded = true;
        rootNode.NavigateUrl = "#";
        objectTree.Nodes.Add(rootNode);

        string objectType = null;
        if (Request.Params["objectType"] != null)
        {
            objectType = ValidationHelper.GetString(Request.Params["objectType"], null);
        }
        // Initialize tree view with custom tables
        bool tableSelected = false;
        DataSet dsTables = DataClassInfoProvider.GetCustomTableClasses(CMSContext.CurrentSiteID, null, null, 0, "ClassID,ClassDisplayName,ClassName");
        if (!DataHelper.DataSourceIsEmpty(dsTables))
        {
            DataTable table = dsTables.Tables[0];

            foreach (DataRow dr in table.Rows)
            {
                string tableDisplayName = ResHelper.LocalizeString(dr["ClassDisplayName"].ToString());

                TreeNode tableNode = new TreeNode();
                tableNode.ImageUrl = GetImageUrl("Objects/CMS_CustomTable/list.png");

                string currentObjectType = CustomTableItemProvider.GetObjectType(dr["ClassName"].ToString());
                if (currentObjectType == objectType)
                {
                    tableNode.Text = "<span class=\"ContentTreeSelectedItem\" id=\"treeSelectedNode\" onclick=\"SelectNode('" + currentObjectType + "', this); \"><span class=\"Name\">" + HTMLHelper.HTMLEncode(tableDisplayName) + "</span></span>";
                    tableSelected = true;
                }
                else
                {
                    tableNode.Text = "<span class=\"ContentTreeItem\" onclick=\"SelectNode('" + currentObjectType + "', this); \"><span class=\"Name\">" + HTMLHelper.HTMLEncode(tableDisplayName) + "</span></span>";
                }
                tableNode.Value = currentObjectType;
                tableNode.NavigateUrl = "#";

                objectTree.Nodes[0].ChildNodes.Add(tableNode);
            }
        }

        string script = "var currentNode = document.getElementById('treeSelectedNode');\n";

        if ((objectType != null) && tableSelected)
        {
            script += " \t SelectNode('" + objectType + "');\n";
        }

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "RefreshScript", ScriptHelper.GetScript(script));
    }
}
