using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSModules_PortalEngine_FormControls_PageTemplates_LevelTree : FormEngineUserControl
{
    #region "Private variables"

    private string mTreePath = string.Empty;
    private int mLevel = 0;
    private string mValue = string.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets tree path - if set is created from TreePath.
    /// </summary>
    public string TreePath
    {
        get
        {
            return mTreePath;
        }
        set
        {
            mTreePath = value;
        }
    }


    /// <summary>
    /// Gets or sets Level, levels are rendered only if TreePath is not set. 
    /// </summary>
    public int Level
    {
        get
        {
            return mLevel;
        }
        set
        {
            mLevel = value;
        }
    }


    /// <summary>
    /// Gets or sets value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (string.IsNullOrEmpty(mValue))
            {
                if (treeElem.Nodes.Count > 0)
                {                    
                    TreeNode currentNode = treeElem.Nodes[0];
                    while (currentNode != null)
                    {
                        if (currentNode.Checked)
                        {
                            mValue += "/{" + currentNode.Value + "}";
                        }

                        if (currentNode.ChildNodes.Count == 0)
                        {
                            break;
                        }
                        currentNode = currentNode.ChildNodes[0];
                    }
                }
            }

            return mValue;            
        }
        set
        {
            mValue = ValidationHelper.GetString(value, string.Empty);
        }
    }   

    #endregion


    #region "Methods and events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check culture
        if (CultureHelper.IsUICultureRTL())
        {
            this.treeElem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", IsLiveSite, true);
        }
        else
        {
            this.treeElem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", IsLiveSite, true);
        }

        // Set tree elements
        this.treeElem.ToolTip = string.Empty;
        this.treeElem.ExpandImageToolTip = string.Empty;
        this.treeElem.ImageSet = TreeViewImageSet.Custom;
        this.treeElem.ExpandImageToolTip = GetString("ContentTree.Expand");
        this.treeElem.CollapseImageToolTip = GetString("ContentTree.Collapse");
        
        // Fill tree and select data
        if (!RequestHelper.IsPostBack())
        {            
            FillTree();
            Select();
        }        
    }


    /// <summary>
    /// Creates tree content.
    /// </summary>
    public void FillTree()
    {
        this.treeElem.Nodes.Clear();

        if (string.IsNullOrEmpty(ValidationHelper.GetString(this.TreePath, string.Empty)) && (this.Level == 0))
        {
            return;
        }
        // Create tree with Level0 to LevelN 
        else if (string.IsNullOrEmpty(TreePath))
        {
            CreateLevelTree();    
        }        

        // Split leafs in tree path
        string[] levels = this.TreePath.Split('/');

        if (levels.Length > 0)
        {
            string rootName = GetString("InheritLevels.RootName");

            // Own user ID
            if (levels.Length > 1)
            {
                // Begin from index 1, first item is empty string
                if (!string.IsNullOrEmpty(levels[1]))
                {
                    rootName = levels[1];
                }
            }

            TreeNode root = new TreeNode("<span onclick=\"return false;\" class=\"ContentTreeItem\"><span class=\"Name\"><strong>" + HTMLHelper.HTMLEncode(rootName) + "</strong></span></span>", "0");
            root.ToolTip = rootName;
            root.ImageToolTip = string.Empty;
            TreeNode LastNode = root;

            // Insert other nodes
            for (int i = 2; i < levels.Length; i++)
            {
                string currentLevel = levels[i];
                if (ValidationHelper.GetString(currentLevel, "") != "")
                {
                    TreeNode currentNode = new TreeNode("<span onclick=\"return false;\" class=\"ContentTreeItem\"><span class=\"Name\">" + HTMLHelper.HTMLEncode(currentLevel) + "</span></span>", (i-1).ToString());
                    currentNode.ToolTip = currentLevel;
                    currentNode.ImageToolTip = "";
                    LastNode.ChildNodes.Add(currentNode);
                    LastNode = currentNode;
                }
            }

            this.treeElem.Nodes.Add(root);
            this.treeElem.ExpandAll();
        }        
    }


    /// <summary>
    /// Select specified documents.
    /// </summary>
    public void Select()
    {
        // Get value
        string selected = ValidationHelper.GetString(mValue, string.Empty);
        if (string.IsNullOrEmpty(selected))
        {
            return;
        }

        // Split value
        string[] levels = selected.Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
        if (levels.Length == 0)
        {
            return;
        }

        foreach (string currentLevel in levels)
        {
            int numberLevel = ValidationHelper.GetInteger(currentLevel.Replace("{", "").Replace("}", ""), -1);
            if (numberLevel >= 0)
            {
                TreeNode currentNode = treeElem.Nodes[0];
                while (currentNode != null)
                {
                    if (currentNode.Value == numberLevel.ToString())
                    {
                        currentNode.Checked = true;
                        break;
                    }
                    else
                    {
                        if (currentNode.ChildNodes.Count == 0)
                        {
                            break;
                        }
                        currentNode = currentNode.ChildNodes[0];
                    }
                }
            }
        }
    }


    /// <summary>
    /// Creates path to level tree.
    /// </summary>
    public void CreateLevelTree()
    {
        string levelName = GetString("inheritlevels.levelname");
        string tmpTree = "/" + levelName + "0";

        for (int i = 1; i < this.Level; i++)
        {
            tmpTree += "/" + levelName + i.ToString();
        }

        this.TreePath = tmpTree;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Hide tooltips of checkboxes in the treeview
        ltlScript.Text += ScriptHelper.GetScript("hideCheckBoxToolTips('" + treeElem.ClientID + "');");
    }

    #endregion
}
