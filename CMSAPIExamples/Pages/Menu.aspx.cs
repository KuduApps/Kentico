using System;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.SettingsProvider;

public partial class CMSAPIExamples_Pages_Menu : CMSAPIExamplePage
{
    private string selectOnStartupScript = "";
    private string reqId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        reqId = QueryHelper.GetString("module", "").ToLower();

        SetupControls();
        LoadMenu();

        if (!string.IsNullOrEmpty(selectOnStartupScript))
        {
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "StartupSelect", ScriptHelper.GetScript(selectOnStartupScript));
        }

        ScriptHelper.RegisterTitleScript(this, "CMS API Examples");
    }


    protected void SetupControls()
    {
        ScriptHelper.RegisterJQuery(this.Page);

        // Initialize TreeView
        this.treeView.ImageSet = TreeViewImageSet.Custom;
        this.treeView.ExpandImageToolTip = "Expand";
        this.treeView.CollapseImageToolTip = "Collapse";
        if (CultureHelper.IsUICultureRTL())
        {
            this.treeView.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            this.treeView.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }

        StringBuilder script = new StringBuilder();
        script.AppendLine("function ShowBlank(elem) {");
        script.Append(" var url = '");
        script.Append(ResolveUrl("~/CMSPages/blank.htm"));
        script.AppendLine("';");
        script.AppendLine(" DisplayPage(url, elem);");
        script.AppendLine("}");
        script.AppendLine("function ShowExample(url,elem) {");
        script.Append(" var mainUrl = '");
        script.Append(ResolveUrl("~/CMSAPIExamples/Code/"));
        script.AppendLine("';");
        script.AppendLine(" DisplayPage(mainUrl + url, elem);");
        script.AppendLine("}");

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "MenuScript", ScriptHelper.GetScript(script.ToString()));
    }


    protected void CreateRoot()
    {
        // Create root
        TreeNode rootNode = new TreeNode();
        rootNode.Text = String.Format("<span class=\"ContentTreeSelectedItem\" name=\"treeNode\" id=\"rootNode\" onclick=\"ShowBlank(this);\"><img src=\"{0}\" style=\"border:none;height:10px;width:1px;\" /><span class=\"Name\">Modules</span></span>", GetImageUrl("General/DefaultRoot.png"));
        rootNode.Expanded = true;
        rootNode.SelectAction = TreeNodeSelectAction.None;
        this.treeView.Nodes.Add(rootNode);
    }


    protected void LoadMenu()
    {
        if (!RequestHelper.IsPostBack())
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("menu.xml"));

                treeView.Nodes.Clear();
                CreateRoot();

                addTreeNode(xmlDoc.DocumentElement, treeView.Nodes[0]);

            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
                lblError.ToolTip = ex.StackTrace;
            }
        }
    }


    private void addTreeNode(XmlNode xmlNode, TreeNode treeNode)
    {
        if (xmlNode.HasChildNodes)
        {
            XmlNodeList nodeList = xmlNode.ChildNodes;
            //Loop through the child nodes
            foreach (XmlNode node in nodeList)
            {
                TreeNode newTreeNode = CreateNode(node);
                if (newTreeNode != null)
                {
                    treeNode.ChildNodes.Add(newTreeNode);
                    addTreeNode(node, newTreeNode);
                }
            }
        }
    }


    private TreeNode CreateNode(XmlNode xmlNode)
    {
        string module = xmlNode.Attributes["module"].InnerText;
        string moduleName = "CMS." + module;
        bool loadModule = true;
        // Check if module is loaded
        if (ModuleEntry.IsModuleRegistered(moduleName) && !ModuleEntry.IsModuleLoaded(moduleName))
        {
            loadModule = false;
        }

        if (loadModule)
        {
            // Get node atributes
            string text = xmlNode.Attributes["text"].InnerText;
            string url = (xmlNode.Attributes["url"] != null) ? xmlNode.Attributes["url"].InnerText : null;
            string img = (xmlNode.Attributes["img"] != null) ? xmlNode.Attributes["img"].InnerText : null;
            string id = (xmlNode.Attributes["id"] != null) ? xmlNode.Attributes["id"].InnerText : null;

            // Get default icon
            if (string.IsNullOrEmpty(img))
            {
                img = string.Format("CMSModules/CMS_{0}/list.png", module.Replace(" ", ""));
            }

            string imgUrl = GetImageUrl(img);

            if (string.IsNullOrEmpty(id))
            {
                id = module;
            }

            string onClickScript = "";

            // Create new node
            TreeNode node = new TreeNode();
            node.Collapse();


            if (!string.IsNullOrEmpty(url))
            {
                string urlStr = ScriptHelper.GetString(url);

                if (reqId.ToLower() == id.ToLower())
                {
                    selectOnStartupScript = string.Format("var node = document.getElementById('node_{0}'); if(node != null){{ShowExample({1}, node);}}", id, urlStr);
                }

                node.SelectAction = TreeNodeSelectAction.None;

                // Prepare selection script
                onClickScript = string.Format("ShowExample({0}, this);", urlStr);
            }
            else
            {
                // Expand category node when clicked
                node.SelectAction = TreeNodeSelectAction.Expand;
            }

            // Create node text
            node.Text = String.Format("<span class=\"ContentTreeItem\" name=\"treeNode\" id=\"node_{3}\" onclick=\"{1}\"><img src=\"{2}\" class=\"TreeItemImage\" alt=\"{0}\" /><span class=\"Name\">{0}</span></span>", text, onClickScript, imgUrl, id);

            return node;
        }
        return null;
    }
}