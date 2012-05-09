using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Objects_Tree : CMSStagingObjectsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        objectTree.RootNode = TaskInfoProvider.ObjectTree;
        objectTree.NodeTextTemplate = "<span class=\"ContentTreeItem\" onclick=\"SelectNode('##OBJECTTYPE##', ##SITEID##, this); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        objectTree.SelectedNodeTextTemplate = "<span id=\"treeSelectedNode\" class=\"ContentTreeSelectedItem\" onclick=\"SelectNode('##OBJECTTYPE##', ##SITEID##, this); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        objectTree.SiteID = CMSContext.CurrentSite.SiteID;

        // Check 'Manage object tasks' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageObjectsTasks"))
        {
            RedirectToAccessDenied("cms.staging", "ManageObjectsTasks");
        }

        ltlScript.Text = ScriptHelper.GetScript("treeUrl = '" + ResolveUrl("~/CMSModules/Staging/Tools/Objects/Tree.aspx") + "';");
    }
}
