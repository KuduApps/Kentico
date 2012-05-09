using System;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.IO;

public partial class CMSSiteManager_Development_leftmenu : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(this.Page);

        if (CultureHelper.IsUICultureRTL())
        {
            treeElem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            treeElem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }
        this.treeElem.ImageSet = TreeViewImageSet.Custom;
        this.treeElem.ExpandImageToolTip = GetString("General.Expand");
        this.treeElem.CollapseImageToolTip = GetString("General.Collapse");

        // Fill in the tree
        treeElem.Nodes.Clear();

        String imagesUrl = GetImageUrl("CMSModules/CMS_SystemDevelopment/", false, true);

        TreeNode rootNode = new TreeNode();
        rootNode.Text = "<span class=\"ContentTreeSelectedItem\" name=\"treeNode\" onclick=\"ShowDesktopContent('development.aspx', this); \"><img src=\"" + GetImageUrl("General/DefaultRoot.png") + "\" style=\"border:none;height:10px;width:1px;\" /><span class=\"Name\">" + GetString("Development.Root") + "</span></span>";
        rootNode.Expanded = true;
        rootNode.NavigateUrl = "#";
        treeElem.Nodes.Add(rootNode);

        TreeNode newNode = null;

        ArrayList al = FillCollectionWithModules(DevelopmentItems, "ShowInDevelopment = 1", true, true);

        // Display default development items
        foreach (object[] itemProperties in al)
        {
            FeatureEnum feature = (FeatureEnum)itemProperties[5];

            // Check whether the notification module is loaded
            if ((feature == FeatureEnum.Notifications) && !ModuleEntry.IsModuleLoaded(ModuleEntry.NOTIFICATIONS))
            {
                continue;
            }

            bool showItem = (feature == FeatureEnum.Unknown) ? true : LicenseHelper.IsFeatureAvailableInUI(feature);
            if (showItem)
            {
                string codeName = ValidationHelper.GetCodeName(itemProperties[1]);

                newNode = new TreeNode();
                newNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"ShowDesktopContent(" + ScriptHelper.GetString(itemProperties[0].ToString()) + ", this); \"><img class=\"TreeItemImage\" src=\"" + itemProperties[3].ToString() + "\" alt=\"\" /><span class=\"Name\">" + HTMLHelper.HTMLEncode(GetString(itemProperties[1].ToString())) + "</span></span>";
                newNode.NavigateUrl = "#";
                rootNode.ChildNodes.Add(newNode);
            }
        }


        // Handle 'System development' section displaying
        if (SettingsKeyProvider.DevelopmentMode)
        {
            newNode = new TreeNode();
            newNode.Text = "<span class=\"ContentTreeItem\"\"><img class=\"TreeItemImage\" src=\"" + imagesUrl + "sysdev.png" + "\" alt=\"\" /><span class=\"Name\">" + GetString("Development.SysDev") + "</span></span>";

            TreeNode newSubNode = new TreeNode();
            newSubNode.Text = "<span class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('../../CMSModules/Settings/Development/CustomSettings/Default.aspx?treeroot=settings', this); \"><img class=\"TreeItemImage\" src=\"" + imagesUrl + "settings.png" + "\" alt=\"\" /><span class=\"Name\">" + GetString("Development.SysDev.Settings") + "</span></span>";
            newSubNode.NavigateUrl = "#";
            newNode.ChildNodes.Add(newSubNode);

            newSubNode = new TreeNode();
            newSubNode.Text = "<span class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('../../CMSModules/SystemDevelopment/Development/Classes/Class_List.aspx', this); \"><img class=\"TreeItemImage\" src=\"" + imagesUrl + "classes.png" + "\" alt=\"\" /><span class=\"Name\">" + GetString("Development.SysDev.Classes") + "</span></span>";
            newSubNode.NavigateUrl = "#";
            newNode.ChildNodes.Add(newSubNode);

            newSubNode = new TreeNode();
            newSubNode.Text = "<span class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('../../CMSModules/SystemDevelopment/Development/Resources/UICulture_StringsDefault_List.aspx', this); \"><img class=\"TreeItemImage\" src=\"" + imagesUrl + "resources.png" + "\" alt=\"\" /><span class=\"Name\">" + GetString("Development.SysDev.Resources") + "</span></span>";
            newSubNode.NavigateUrl = "#";
            newNode.ChildNodes.Add(newSubNode);

            rootNode.ChildNodes.Add(newNode);
        }
    }
}
