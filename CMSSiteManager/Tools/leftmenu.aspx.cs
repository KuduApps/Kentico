using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Tools_leftmenu : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(this.Page);

        // Initialize TreeView
        this.treeElem.ImageSet = TreeViewImageSet.Custom;
        this.treeElem.ExpandImageToolTip = GetString("General.Expand");
        this.treeElem.CollapseImageToolTip = GetString("General.Collapse");
        if (CultureHelper.IsUICultureRTL())
        {
            this.treeElem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            this.treeElem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }
        this.treeElem.Nodes.Clear();

        // Create root
        TreeNode rootNode = new TreeNode();
        rootNode.Text = "<span class=\"ContentTreeSelectedItem\" name=\"treeNode\" onclick=\"ShowDesktopContent('tools.aspx', this);\"><img src=\"" + GetImageUrl("General/DefaultRoot.png") + "\" style=\"border:none;height:10px;width:1px;\" /><span class=\"Name\">" + GetString("Administration-LeftMenu.Tools") + "</span></span>";
        rootNode.Expanded = true;
        rootNode.NavigateUrl = "#";
        this.treeElem.Nodes.Add(rootNode);

        ArrayList items = new ArrayList();

        // Add permanent modules
        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Ecommerce, ModuleEntry.ECOMMERCE))
        {
            AddToCollection(
                GetString("Administration-LeftMenu.Ecommerce"),
                "~/CMSModules/Ecommerce/Pages/SiteManager/Configuration_Frameset.aspx?siteId=0",
                "CMSModules/CMS_Ecommerce/list.png", items);
        }

        // Add permanent modules
        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.ContactManagement, ModuleEntry.ONLINEMARKETING))
        {
            AddToCollection(
                GetString("om.contactmanagement"),
                "~/CMSModules/ContactManagement/Pages/Tools/Frameset.aspx?isSiteManager=1",
                "Objects/OM_ContactManagement/list.png", items);
        }

        // Sort the collection
        ObjectArrayComparer comparer = new ObjectArrayComparer();
        items.Sort(comparer);

        // Build the tree
        foreach (object node in items)
        {
            object[] nodeArray = (object[])node;
            AddNodeToTree(Convert.ToString(nodeArray[1]), Convert.ToString(nodeArray[0]), Convert.ToString(nodeArray[2]), rootNode);
        }
    }


    /// <summary>
    /// Add module to tree menu.
    /// </summary>
    /// <param name="caption">Module code name</param>
    /// <param name="url">Resource string of tree item</param>
    /// <param name="icon">URL to module</param>
    /// <param name="collection">Collection to fill</param>
    protected void AddToCollection(string caption, string url, string icon, ArrayList collection)
    {
        object[] itemProperties = new object[3];

        itemProperties[0] = url;
        itemProperties[1] = ResHelper.LocalizeString(caption);
        itemProperties[2] = GetImageUrl(icon);

        collection.Add(itemProperties);
    }


    /// <summary>
    /// Add module to tree menu.
    /// </summary>
    /// <param name="caption">Module code name</param>
    /// <param name="url">Resource string of tree item</param>
    /// <param name="icon">URL to module</param>
    /// <param name="rootNode">Parent node</param>
    protected void AddNodeToTree(string caption, string url, string icon, TreeNode rootNode)
    {
        TreeNode newNode = new TreeNode();

        string codeName = ValidationHelper.GetCodeName(caption);

        newNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('" + URLHelper.ResolveUrl(url) + "', this, '" + codeName + "');\"><img class=\"TreeItemImage\" src=\"" + icon + "\" alt=\"\" /><span class=\"Name\">" + caption + "</span></span>";
        newNode.NavigateUrl = "#";
        rootNode.ChildNodes.Add(newNode);
    }
}
