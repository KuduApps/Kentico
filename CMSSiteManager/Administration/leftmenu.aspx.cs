using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Administration_leftmenu : SiteManagerPage
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
        rootNode.Text = "<span class=\"ContentTreeSelectedItem\" name=\"treeNode\" onclick=\"ShowDesktopContent('administration.aspx', this);\"><img src=\"" + GetImageUrl("General/DefaultRoot.png") + "\" style=\"border:none;height:10px;width:1px;\" /><span class=\"Name\">" + GetString("Administration-LeftMenu.Administration") + "</span></span>";
        rootNode.Expanded = true;
        rootNode.NavigateUrl = "#";
        this.treeElem.Nodes.Add(rootNode);

        ArrayList items = new ArrayList();

        // Get the UIElements
        DataSet ds = UIElementInfoProvider.GetUIMenuElements("CMS.Administration");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                UIElementInfo element = new UIElementInfo(dr);
                bool add = CMSAdministrationPage.IsAdministrationUIElementAvailable(element);
                if (add)
                {
                    object[] itemProperties = new object[3];

                    // Ensure target URL
                    string targetUrl = URLHelper.ResolveUrl(element.ElementTargetURL);
                    targetUrl = URLHelper.RemoveParameterFromUrl(targetUrl, "siteid");

                    // Ensure icon URL
                    string iconUrl = element.ElementIconPath;
                    if (!String.IsNullOrEmpty(iconUrl))
                    {
                        if (!ValidationHelper.IsURL(iconUrl))
                        {
                            iconUrl = UIHelper.GetImagePath(this.Page, iconUrl, false, false);

                            // Try to get default icon if requested icon not found
                            if (!FileHelper.FileExists(iconUrl))
                            {
                                iconUrl = GetImageUrl("CMSModules/list.png");
                            }
                        }
                    }
                    else
                    {
                        iconUrl = GetImageUrl("CMSModules/list.png");
                    }

                    // Initialize and add element to collection
                    itemProperties[0] = targetUrl;
                    itemProperties[1] = ResHelper.LocalizeString(element.ElementCaption);
                    itemProperties[2] = URLHelper.ResolveUrl(iconUrl);

                    items.Add(itemProperties);
                }
            }
        }

        // Add permanent modules
        AddToCollection(
            GetString("Administration-LeftMenu.Avatars"),
            "~/CMSModules/Avatars/Avatar_list.aspx",
            "Objects/CMS_Avatar/list.png", items);

        AddToCollection(
            GetString("Administration-LeftMenu.Badges"),
            "~/CMSModules/Badges/Badges_List.aspx",
            "Objects/CMS_Badge/list.png", items);

        AddToCollection(
            GetString("Administration-LeftMenu.BadWords"),
            "~/CMSModules/BadWords/BadWords_List.aspx",
            "Objects/Badwords_Word/list.png", items);

        AddToCollection(
             GetString("Administration-LeftMenu.EmailQueue"),
             "~/CMSModules/EmailQueue/EmailQueue_Frameset.aspx",
             "CMSModules/CMS_EmailQueue/list.png", items);

        AddToCollection(
             GetString("Administration-LeftMenu.RecycleBin"),
             "~/CMSModules/RecycleBin/Pages/default.aspx",
             "CMSModules/CMS_RecycleBin/list.png", items);

        AddToCollection(
             GetString("srch.index.title"),
             "~/CMSModules/SmartSearch/SearchIndex_List.aspx",
             "Objects/CMS_SearchIndex/list.png", items);

        AddToCollection(
            GetString("Administration-LeftMenu.SMTPServers"),
            "~/CMSModules/SMTPServers/Pages/Administration/List.aspx",
            "Objects/CMS_SMTPServer/list.png", items);

        AddToCollection(
             GetString("Administration-LeftMenu.System"),
             "~/CMSModules/System/System_Frameset.aspx",
             "CMSModules/CMS_System/list.png", items);

        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.IntegrationBus))
        {
            AddToCollection(
                 GetString("integration.integration"),
                 "~/CMSModules/Integration/Pages/Administration/Frameset.aspx",
                 "CMSModules/CMS_Integration/list.png", items);
        }

        if (LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Webfarm))
        {
            AddToCollection(
                 GetString("Administration-LeftMenu.WebFarm"),
                 "~/CMSModules/WebFarm/Pages/WebFarm_Frameset.aspx",
                 "Objects/CMS_WebFarmServer/list.png", items);
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
