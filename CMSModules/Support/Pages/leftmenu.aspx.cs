using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Support_Pages_leftmenu : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(this.Page);

        if (CultureHelper.IsUICultureRTL())
        {
            TreeViewAdministration.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            TreeViewAdministration.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }
        TreeViewAdministration.ImageSet = TreeViewImageSet.Custom;

        TreeViewAdministration.Nodes.Clear();

        string iconUrl = null;
        string codeName = "";

        codeName = "Support";
        TreeNode rootNode = new TreeNode();
        rootNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeSelectedItem\" onclick=\"ShowDesktopContent('support.aspx', this);\"><span class=\"Name\">" + GetString("Support-LeftMenu.Support") + "</span></span>";
        rootNode.Expanded = true;
        rootNode.NavigateUrl = "#";
        TreeViewAdministration.Nodes.Add(rootNode);

        codeName = "DevNet";
        iconUrl = GetImageUrl("CMSModules/CMS_Support/Devnet.png");
        TreeNode newNode = new TreeNode();
        newNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('http://devnet.kentico.com', this);\"><img class=\"TreeItemImage\" src=\"" + iconUrl + "\" alt=\"\" /><span class=\"Name\">" + GetString("Support-LeftMenu.DevNet") + "</span></span>";
        newNode.NavigateUrl = "#";
        rootNode.ChildNodes.Add(newNode);

        codeName = "SubmitIssue";
        iconUrl = GetImageUrl("CMSModules/CMS_Support/SubmitIssue.png");
        newNode = new TreeNode();
        newNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('SubmitIssue.aspx', this);\"><img class=\"TreeItemImage\" src=\"" + iconUrl + "\" alt=\"\" /><span class=\"Name\">" + GetString("Support-LeftMenu.SubmitIssue") + "</span></span>";
        newNode.NavigateUrl = "#";
        rootNode.ChildNodes.Add(newNode);

        codeName = "Help";
        iconUrl = GetImageUrl("CMSModules/CMS_Support/Help.png");
        newNode = new TreeNode();
        newNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('http://devnet.kentico.com/Documentation/" + CMSContext.SYSTEM_VERSION.Replace(".", "_") + ".aspx', this);\"><img class=\"TreeItemImage\" src=\"" + iconUrl + "\" alt=\"\" /><span class=\"Name\">" + GetString("General.Documentation") + "</span></span>";
        newNode.NavigateUrl = "#";
        rootNode.ChildNodes.Add(newNode);

        try
        {
            if (CMS.IO.File.Exists(Server.MapPath("~/CMSAPIExamples/Default.aspx")))
            {
                codeName = "APIExamples";
                iconUrl = GetImageUrl("CMSModules/CMS_Support/Code.png");
                newNode = new TreeNode();
                newNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('" + ResolveUrl("~/CMSAPIExamples/Default.aspx") + "', this, true);\"><img class=\"TreeItemImage\" src=\"" + iconUrl + "\" alt=\"\" /><span class=\"Name\">" + GetString("Support-LeftMenu.ApiExamples") + "</span></span>";
                newNode.NavigateUrl = "#";
                rootNode.ChildNodes.Add(newNode);
            }

            // Uncomment this section to display link to controls examples
            //if (CMS.IO.File.Exists(Server.MapPath("~/CMSControlsExamples/Default.aspx")))
            //{
            //    codeName = "ControlExamples";
            //    iconUrl = GetImageUrl("CMSModules/CMS_Support/Control.png");
            //    newNode = new TreeNode();
            //    newNode.Text = "<span id=\"node_" + codeName + "\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"ShowDesktopContent('" + ResolveUrl("~/CMSControlsExamples/Default.aspx") + "', this, true);\"><img class=\"TreeItemImage\" src=\"" + iconUrl + "\" alt=\"\" /><span class=\"Name\">" + GetString("Support-LeftMenu.ControlsExamples") + "</span></span>";
            //    newNode.NavigateUrl = "#";
            //    rootNode.ChildNodes.Add(newNode);
            //}
        }
        catch { }
    }
}
