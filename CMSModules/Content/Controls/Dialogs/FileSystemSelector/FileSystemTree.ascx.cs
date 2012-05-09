using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.IO;

public partial class CMSModules_Content_Controls_Dialogs_FileSystemSelector_FileSystemTree : CMSUserControl
{
    #region "Variables"

    private string mStartingPath = "";
    private string mDefaultPath = "";
    private string mAllowedFolders = "";
    private string mExcludedFolders = "";

    private string mNodeTextTemplate = "##ICON####NODENAME##";
    private string mSelectedNodeTextTemplate = "##ICON####NODENAME##";
    private string mNodeValue = "";
    private string mBasePath = null;
    private int mMaxTreeNodes = 0;
    private string mMaxTreeNodeText = null;
    private bool mDeniedNodePostback = true;
    private bool mAllowMarks = true;
    private bool mExpandDefaultPath = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Starting path of the tree.
    /// </summary>
    public string StartingPath
    {
        get
        {
            return mStartingPath;
        }
        set
        {
            mStartingPath = value;
        }
    }


    /// <summary>
    /// Path to selected tree node in tree.
    /// </summary>
    public string DefaultPath
    {
        get
        {
            mDefaultPath = ValidationHelper.GetString(ViewState["TreeDefaultPath"], "");
            return mDefaultPath;
        }
        set
        {
            mDefaultPath = value.Replace('/', '\\');
            ViewState["TreeDefaultPath"] = mDefaultPath;
        }
    }


    /// <summary>
    /// Determines if default path node should be expanded.
    /// </summary>
    public bool ExpandDefaultPath
    {
        get
        {
            return mExpandDefaultPath;
        }
        set
        {
            mExpandDefaultPath = value;
        }
    }

    /// <summary>
    /// List of folders which should be displayed, separated with semicolon.
    /// </summary>
    public string AllowedFolders
    {
        get
        {
            return mAllowedFolders.ToLower();
        }
        set
        {
            mAllowedFolders = value;
        }
    }


    /// <summary>
    /// List of files excluded from tree, separated with semicolon.
    /// </summary>
    public string ExcludedFolders
    {
        get
        {
            return mExcludedFolders.ToLower();
        }
        set
        {
            mExcludedFolders = value;
        }
    }


    /// <summary>
    /// Maximum number of tree nodes displayed within the tree.
    /// </summary>
    public int MaxTreeNodes
    {
        get
        {
            if (mMaxTreeNodes <= 0)
            {
                mMaxTreeNodes = SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSMaxTreeNodes");
            }
            return mMaxTreeNodes;
        }
        set
        {
            mMaxTreeNodes = value;
        }
    }


    /// <summary>
    /// Text to appear within the latest node when max tree nodes applied.
    /// </summary>
    public string MaxTreeNodeText
    {
        get
        {
            if (mMaxTreeNodeText == null)
            {
                mMaxTreeNodeText = GetString("ContentTree.SeeListing");
            }
            return mMaxTreeNodeText;
        }
        set
        {
            mMaxTreeNodeText = value;
        }
    }


    /// <summary>
    /// Gets or sets the current node value.
    /// </summary>
    public string NodeValue
    {
        get
        {
            return mNodeValue;
        }
        set
        {
            mNodeValue = value;
        }
    }


    /// <summary>
    /// Template of the node text, use {0} to insert the original node text, {1} to insert the Node ID.
    /// </summary>
    public string NodeTextTemplate
    {
        get
        {
            return mNodeTextTemplate;
        }
        set
        {
            mNodeTextTemplate = value;
        }
    }


    /// <summary>
    /// Template of the SelectedNode text, use {0} to insert the original SelectedNode text, {1} to insert the SelectedNode ID.
    /// </summary>
    public string SelectedNodeTextTemplate
    {
        get
        {
            return mSelectedNodeTextTemplate;
        }
        set
        {
            mSelectedNodeTextTemplate = value;
        }
    }


    /// <summary>
    /// True if the special marks (NOTTRANSLATED, REDIRECTION, ...) should be rendered.
    /// </summary>
    public bool AllowMarks
    {
        get
        {
            return mAllowMarks;
        }
        set
        {
            mAllowMarks = value;
        }
    }


    /// <summary>
    /// Indicates whether access denied node causes postback.
    /// </summary>
    public bool DeniedNodePostback
    {
        get
        {
            return this.mDeniedNodePostback;
        }
        set
        {
            this.mDeniedNodePostback = value;
        }
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Gets number of childs under specified item.
    /// </summary>
    /// <param name="dirInfo">Directory info of processed folder</param>
    /// <returns></returns>
    private int GetAllowedChildNumber(DirectoryInfo dirInfo)
    {
        int counter = 0;
        try
        {
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                if ((IsAllowed(dir.FullName.ToLower())) && (!IsExcluded(dir.FullName.ToLower())))
                {
                    counter++;
                }
            }
            return counter;
        }
        catch (Exception)
        {
            return 0;
        }
    }


    /// <summary>
    /// Gets full starting path inspecting possible relative path specification.
    /// </summary>
    private string FullStartingPath
    {
        get
        {
            if (UsingRelativeURL())
            {
                return Server.MapPath(this.StartingPath).TrimEnd('\\');
            }
            else
            {
                if (this.StartingPath.EndsWith(":\\"))
                {
                    return this.StartingPath;
                }
            }
            return StartingPath.TrimEnd('\\');
        }
    }


    /// <summary>
    /// Check if relative paths are used.
    /// </summary>
    /// <returns>True if relative paths are used</returns>
    public bool UsingRelativeURL()
    {
        if (this.StartingPath.StartsWith("~"))
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// Determines if specified folder under root is allowed or not.
    /// </summary>
    /// <param name="folderName">Path to folder</param>
    /// <returns>True if folder is allowed</returns>
    private bool IsAllowed(string folderName)
    {

        if (String.IsNullOrEmpty(AllowedFolders) || (folderName.ToLower() == FullStartingPath.ToLower()))
        {
            return true;
        }
        else
        {
            foreach (string folder in AllowedFolders.Split(';'))
            {
                if (folderName.StartsWith(DirectoryHelper.CombinePath(FullStartingPath.ToLower(), folder)))
                {
                    return true;
                }
            }
            return false;
        }
    }


    /// <summary>
    /// Determines if specified folder under root is excluded or not.
    /// </summary>
    /// <param name="folderName">Path to folder</param>
    /// <returns>True if folder is excluded</returns>
    private bool IsExcluded(string folderName)
    {

        if (String.IsNullOrEmpty(ExcludedFolders))
        {
            return false;
        }
        else
        {
            foreach (string folder in ExcludedFolders.Split(';'))
            {
                if (folderName.ToLower().Equals(Path.Combine(FullStartingPath.ToLower(), folder)))
                {
                    return true;
                }
            }
            return false;
        }
    }

    #endregion


    #region "Control events"

    protected void Page_Load(object sender, EventArgs e)
    {
        mBasePath = URLHelper.Url.LocalPath;

        if (CultureHelper.IsUICultureRTL())
        {
            treeFileSystem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", IsLiveSite, true);
        }
        else
        {
            treeFileSystem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", IsLiveSite, true);
        }
        treeFileSystem.ImageSet = TreeViewImageSet.Custom;
        treeFileSystem.ExpandImageToolTip = GetString("ContentTree.Expand");
        treeFileSystem.CollapseImageToolTip = GetString("ContentTree.Collapse");
        treeFileSystem.ShowLines = true;


        if (!RequestHelper.IsCallback())
        {
            ScriptHelper.RegisterTreeProgress(Page);
        }
    }


    /// <summary>
    /// Pre render.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if ((!RequestHelper.IsCallback() && !RequestHelper.IsPostBack()))
        {
            ReloadData();
        }
    }

    #endregion


    #region "Control methods"

    /// <summary>
    /// Reload control data.
    /// </summary>
    public void ReloadData()
    {
        try
        {
            this.treeFileSystem.Nodes.Clear();
            InitializeTree();

            // Expand current node parent
            if (!String.IsNullOrEmpty(DefaultPath))
            {
                if (!this.DefaultPath.ToLower().StartsWith(this.FullStartingPath.ToLower().TrimEnd('\\')))
                {
                    this.DefaultPath = DirectoryHelper.CombinePath(this.FullStartingPath, this.DefaultPath);
                }

                if (!String.IsNullOrEmpty(ExcludedFolders))
                {
                    foreach (string excludedFolder in ExcludedFolders.Split(';'))
                    {
                        if (DefaultPath.ToLower().StartsWith((DirectoryHelper.CombinePath(FullStartingPath, excludedFolder)).ToLower()))
                        {
                            this.DefaultPath = this.FullStartingPath;
                            break;
                        }
                    }
                }

                string preselectedPath = this.DefaultPath;
                string rootPath = this.treeFileSystem.Nodes[0].Value;

                if (preselectedPath.ToLower().StartsWith(rootPath.ToLower()))
                {
                    TreeNode parent = this.treeFileSystem.Nodes[0];

                    string[] folders = preselectedPath.ToLower().Substring(rootPath.Length).Split('\\');
                    int index = 0;
                    string path = rootPath.ToLower() + folders[index];


                    foreach (string folder in folders)
                    {
                        foreach (TreeNode node in parent.ChildNodes)
                        {
                            if (node.Value.ToLower() == path)
                            {
                                parent = node;
                                break;
                            }
                        }
                        if (index < folders.Length - 1)
                        {
                            parent.Expand();
                            path += '\\' + folders[index + 1];
                        }
                        else
                        {
                            if (ExpandDefaultPath)
                            {
                                parent.Expand();
                            }
                        }
                        index++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = GetString("ContentTree.FailedLoad") + ": " + ex.Message;
            lblError.ToolTip = ex.StackTrace;
        }
    }


    /// <summary>
    /// Tree initialization.
    /// </summary>
    private void InitializeTree()
    {
        // Create root element

        DirectoryInfo di = null;
        if (Directory.Exists(this.FullStartingPath))
        {
            di = DirectoryInfo.New(this.FullStartingPath);
        }
        TreeNode root = CreateNode(di, 0);
        root.Expand();
        root.PopulateOnDemand = false;

        treeFileSystem.Nodes.Add(root);

    }


    /// <summary>
    /// Creation of new tree folder node.
    /// </summary>
    /// <param name="dirInfo">Folder information</param>
    /// <param name="index">Index in tree to check if max number of item isn't exceeded</param>
    /// <returns></returns>
    protected TreeNode CreateNode(DirectoryInfo dirInfo, int index)
    {
        if ((dirInfo != null) && (this.IsAllowed(dirInfo.FullName.ToLower())) && (!this.IsExcluded(dirInfo.FullName.ToLower())))
        {
            System.Web.UI.WebControls.TreeNode newNode = new System.Web.UI.WebControls.TreeNode();

            // Check if node is part of preselected path
            string preselectedPath = this.DefaultPath;
            if (!this.DefaultPath.ToLower().StartsWith(this.FullStartingPath.ToLower().TrimEnd('\\')))
            {
                preselectedPath = DirectoryHelper.CombinePath(this.FullStartingPath, this.DefaultPath);
            }

            if (index == MaxTreeNodes)
            {
                newNode.Value = "";
                newNode.Text = MaxTreeNodeText.Replace("##PARENTNODEID##", dirInfo.Parent == null ? "" : dirInfo.Parent.FullName.Replace("\\", "\\\\").Replace("'", "\\'"));
                newNode.NavigateUrl = mBasePath + "#";
            }
            else if ((index < MaxTreeNodes) || (preselectedPath.ToLower().StartsWith(dirInfo.FullName.ToLower())))
            {
                newNode.Value = dirInfo.FullName;
                newNode.NavigateUrl = mBasePath + "#";

                string imageUrl = "";
                string tooltip = "";

                imageUrl = treeFileSystem.LineImagesFolder + "/folder.gif";
                string imageTag = "<img src=\"" + imageUrl + "\" alt=\"\" style=\"border:0px;vertical-align:middle;\" onclick=\"return false;\"" + tooltip + "/>";
                string nodeName = HttpUtility.HtmlEncode(dirInfo.Name);
                string nodeNameJava = ScriptHelper.GetString(nodeName);

                string preSel = this.FullStartingPath.TrimEnd('\\').ToLower();
                if (this.DefaultPath.ToLower().StartsWith(this.FullStartingPath.ToLower().TrimEnd('\\')))
                {
                    preSel = this.DefaultPath.ToLower();
                }
                else if (!String.IsNullOrEmpty(this.DefaultPath))
                {
                    preSel = DirectoryHelper.CombinePath(preSel, this.DefaultPath.ToLower());
                }


                if ((preSel != "") && (newNode.Value.ToLower() == preSel))
                {
                    newNode.Text = SelectedNodeTextTemplate.Replace("##NODENAMEJAVA##", nodeNameJava).Replace("##NODENAME##", nodeName).Replace("##ICON##", imageTag).Replace("##NODEID##", newNode.Value.Replace("\\", "\\\\").Replace("'", "\\'"));
                }
                else
                {
                    newNode.Text = NodeTextTemplate.Replace("##NODENAMEJAVA##", nodeNameJava).Replace("##NODENAME##", nodeName).Replace("##ICON##", imageTag).Replace("##NODEID##", newNode.Value.Replace("\\", "\\\\").Replace("'", "\\'"));
                }

                int childNodesCount = 0;
                try
                {
                    childNodesCount = ValidationHelper.GetInteger(GetAllowedChildNumber(dirInfo), 0);
                    if (childNodesCount == 0)
                    {
                        newNode.PopulateOnDemand = false;
                        newNode.Expanded = true;
                    }
                    else
                    {
                        newNode.PopulateOnDemand = true;
                        newNode.Expanded = false;
                    }
                }
                catch
                {
                    // Access error
                    newNode.PopulateOnDemand = false;
                    newNode.Expanded = true;
                }
                finally
                {
                    newNode.Text = newNode.Text.Replace("##NODECHILDNODESCOUNT##", childNodesCount.ToString());
                }
            }

            else
            {
                return null;
            }

            return newNode;
        }
        return null;
    }


    /// <summary>
    /// Node populating.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Particular node arguments</param>
    protected void treeFileSystem_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        e.Node.ChildNodes.Clear();
        e.Node.PopulateOnDemand = false;

        try
        {
            DirectoryInfo dirInfo;
            dirInfo = DirectoryInfo.New(e.Node.Value);

            DirectoryInfo[] childDirs = dirInfo.GetDirectories();

            for (int i = 0, index = 0; i < childDirs.Length; i++)
            {
                System.Web.UI.WebControls.TreeNode newNode = CreateNode(childDirs[i], index);
                if (newNode != null)
                {
                    e.Node.ChildNodes.Add(newNode);
                    // More content node was inserted
                    if (newNode.Value == "")
                    {
                        i--;
                    }
                    index++;
                }
            }
        }
        catch
        {
        }
    }

    #endregion
}
