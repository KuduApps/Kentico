using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.EventLog;
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FolderTree : CMSAdminControl
{
    /// <summary>
    /// Occurs when selected folder changed.
    /// </summary>
    public delegate void OnFolderSelectedHandler();

    #region "Variables"

    private string mRootFolderPath = null;
    private string mSelectedPath = null;
    private string mExpandedPath = null;
    private string mImageFolderPath = null;
    private string mMediaLibraryFolder = null;
    private string mMediaLibraryPath = null;
    private bool mDisplayFilesCount = false;
    private bool mDisplayFolderIcon = true;
    private bool mIgnoreAccessDenied = false;
    private bool mCloseListing = false;
    private bool mRootHasMore = false;
    private string mCustomSelectFunction = null;
    private string mCustomClickForMoreFunction = null;
    private int mMaxSubFolders = -1;

    public event OnFolderSelectedHandler OnFolderSelected;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Returns maximal number of subfolders displayed per parent object 
    /// (if more objects are present, then "cick here for more" is displayed).
    /// </summary>
    private int MaxSubFolders
    {
        get
        {
            if (this.mMaxSubFolders < 0)
            {
                this.mMaxSubFolders = SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSMediaLibraryMaxSubFolders");

                // Handle negative value
                if (this.mMaxSubFolders < 0)
                {
                    this.mMaxSubFolders = 0;
                }
            }

            return this.mMaxSubFolders;
        }
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the value which determines whether to display icon of folder in tree.
    /// </summary>
    public bool DisplayFolderIcon
    {
        get
        {
            return this.mDisplayFolderIcon;
        }
        set
        {
            this.mDisplayFolderIcon = value;
        }
    }


    /// <summary>
    /// Indicates whether the control should try to bind as much folders as available ignoring access denied exceptions.
    /// </summary>
    public bool IgnoreAccessDenied
    {
        get
        {
            return this.mIgnoreAccessDenied;
        }
        set
        {
            this.mIgnoreAccessDenied = value;
        }
    }


    /// <summary>
    /// Indicates whether the control propagates information when max number of root child nodes is exceeded.
    /// </summary>
    public bool CloseListing
    {
        get
        {
            return this.mCloseListing;
        }
        set
        {
            this.mCloseListing = value;
        }
    }


    /// <summary>
    /// Root folder physical path.
    /// </summary>
    public string RootFolderPath
    {
        get
        {
            return this.mRootFolderPath;
        }
        set
        {
            this.mRootFolderPath = value;
        }
    }


    /// <summary>
    /// Path to the trees images.
    /// </summary>
    public string ImageFolderPath
    {
        get
        {
            if (this.mImageFolderPath == null)
            {
                if (CultureHelper.IsUICultureRTL())
                {
                    this.mImageFolderPath = GetImageUrl("RTL/Design/Controls/Tree", IsLiveSite, true);
                }
                else
                {
                    this.mImageFolderPath = GetImageUrl("Design/Controls/Tree", IsLiveSite, true);
                }
            }
            return this.mImageFolderPath;
        }
        set
        {
            this.mImageFolderPath = value;
        }
    }


    /// <summary>
    /// Selected path in treeview.
    /// </summary>
    public string SelectedPath
    {
        get
        {
            if (this.treeElem.SelectedNode != null)
            {
                // If not selected root node
                if (this.treeElem.SelectedNode != this.treeElem.Nodes[0])
                {
                    // Return path without library folder and \
                    this.mSelectedPath = this.treeElem.SelectedNode.ValuePath.Substring(this.treeElem.Nodes[0].Value.Length + 1);
                }
                else
                {
                    this.mSelectedPath = "";
                }
            }
            return this.mSelectedPath;
        }
        set
        {
            this.mSelectedPath = value;
        }
    }


    /// <summary>
    /// Expand path in treeview.
    /// </summary>
    public string ExpandedPath
    {
        get
        {
            return this.mExpandedPath;
        }
        set
        {
            ExpandPath(value);
            this.mExpandedPath = value;
        }
    }


    /// <summary>
    /// Media library folder in root of treeview.
    /// </summary>
    public string MediaLibraryFolder
    {
        get
        {
            return this.mMediaLibraryFolder;
        }
        set
        {
            this.mMediaLibraryFolder = value;
        }
    }


    /// <summary>
    /// Media library path for root of tree within library.
    /// </summary>
    public string MediaLibraryPath
    {
        get
        {
            return this.mMediaLibraryPath;
        }
        set
        {
            this.mMediaLibraryPath = value;
        }
    }


    /// <summary>
    /// Indicates if file count should be displayed in folder tree.
    /// </summary>
    public bool DisplayFilesCount
    {
        get
        {
            return mDisplayFilesCount;
        }
        set
        {
            mDisplayFilesCount = value;
        }
    }


    /// <summary>
    /// Javascript function for custom select handling.
    /// </summary>
    public string CustomSelectFunction
    {
        get
        {
            return this.mCustomSelectFunction;
        }
        set
        {
            this.mCustomSelectFunction = value;
        }
    }


    /// <summary>
    /// Javascript function for custom "click here for more" item handling.
    /// </summary>
    public string CustomClickForMoreFunction
    {
        get
        {
            return this.mCustomClickForMoreFunction;
        }
        set
        {
            this.mCustomClickForMoreFunction = value;
        }
    }


    /// <summary>
    /// Path separator.
    /// </summary>
    public char PathSeparator
    {
        get
        {
            return this.treeElem.PathSeparator;
        }
    }


    /// <summary>
    /// Indicates whether there is more items than MaxSubFolders under the root node.
    /// </summary>
    public bool RootHasMore
    {
        get
        {
            return this.mRootHasMore;
        }
        set
        {
            this.mRootHasMore = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            this.treeElem.LineImagesFolder = this.ImageFolderPath;
            this.treeElem.ImageSet = TreeViewImageSet.Custom;
            this.treeElem.ExpandDepth = 1;
            this.treeElem.PathSeparator = '\\';
            this.treeElem.SelectedNodeChanged += new EventHandler(treeElem_SelectedNodeChanged);

            if (!RequestHelper.IsPostBack())
            {
                ReloadData();
            }
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        try
        {
            this.treeElem.Nodes.Clear();

            // Get root files
            string rootDir = this.RootFolderPath;
            if (!String.IsNullOrEmpty(this.MediaLibraryPath))
            {
                rootDir = DirectoryHelper.CombinePath(rootDir, this.MediaLibraryPath);
            }
            else
            {
                rootDir = DirectoryHelper.CombinePath(rootDir, this.MediaLibraryFolder);
            }

            // Get the file and directories count
            int dirCount = 0;
            int fileCount = 0;

            string[] files = null;
            string[] directories = null;
            if ((rootDir != null) && (Directory.Exists(rootDir)))
            {
                files = Directory.GetFiles(rootDir);
                directories = Directory.GetDirectories(rootDir);
            }

            if (files != null)
            {
                fileCount = files.Length;
            }

            if (directories != null)
            {
                // Each directory contains directory for thumbnails
                dirCount = directories.Length - 1;
            }

            // Create root tree node
            TreeNode root = null;
            if (DisplayFilesCount)
            {
                root = CreateNode("<span class=\"Name\">" + this.MediaLibraryFolder + " (" + fileCount + ")</span>", this.MediaLibraryFolder, null, dirCount, 0);
            }
            else
            {
                root = CreateNode("<span class=\"Name\">" + this.MediaLibraryFolder + "</span>", this.MediaLibraryFolder, null, dirCount, 0);
            }

            // Keep root expanded
            root.Expand();
            // Add root node
            this.treeElem.Nodes.Add(root);

            // Bind tree nodes
            if (String.IsNullOrEmpty(this.MediaLibraryPath))
            {
                BindTreeView(this.RootFolderPath + this.MediaLibraryFolder, root, true);
            }
            else
            {
                BindTreeView(DirectoryHelper.CombinePath(this.RootFolderPath, this.MediaLibraryPath), root, true);
            }
        }
        catch (Exception ex)
        {
            if (!this.IgnoreAccessDenied)
            {
                this.lblError.Text = GetString("FolderTree.FailedLoad") + ": " + ex.Message;
                this.lblError.ToolTip = EventLogProvider.GetExceptionLogMessage(ex);
            }
        }
    }


    /// <summary>
    /// Selects given path in tree view.
    /// </summary>
    /// <param name="path">Path to select</param>
    public void SelectPath(string path)
    {
        SelectPath(path, true);
    }


    /// <summary>
    /// Selects given path in tree view.
    /// </summary>
    /// <param name="path">Path to select</param>
    /// <param name="folderSelect">Indicates if OnFolderSelect event should be executed</param>
    public void SelectPath(string path, bool folderSelect)
    {
        if (path != null)
        {
            TreeNode node = this.treeElem.FindNode(path.ToLower());
            if (node != null)
            {
                ExpandParent(node);
                node.Select();
                if ((folderSelect) && (OnFolderSelected != null))
                {
                    OnFolderSelected();
                }
            }
        }
    }


    /// <summary>
    /// Expand given path in tree view.
    /// </summary>
    /// <param name="path">Path to expand</param>
    public void ExpandPath(string path)
    {
        TreeNode node = this.treeElem.FindNode(path);
        if (node != null)
        {
            ExpandParent(node);
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Bind tree view.
    /// </summary>
    /// <param name="dirPath">Directory path</param>
    /// <param name="parentNode">Parent node</param>
    private void BindTreeView(string dirPath, TreeNode parentNode)
    {
        BindTreeView(dirPath, parentNode, false);
    }


    /// <summary>
    /// Bind tree view.
    /// </summary>
    /// <param name="dirPath">Directory path</param>
    /// <param name="parentNode">Parent node</param>
    private void BindTreeView(string dirPath, TreeNode parentNode, bool isRoot)
    {
        if (Directory.Exists(dirPath))
        {
            string folderImageTag = (this.DisplayFolderIcon ? "<img src=\"" + GetImageUrl("Design/Controls/Tree/folder.gif") + "\" alt=\"\" style=\"border:0px;vertical-align:middle;\" />" : "");
            string hidenFolder = "\\" + MediaLibraryHelper.GetMediaFileHiddenFolder(CMSContext.CurrentSiteName);

            // Get directories
            string[] dirs = null;
            try
            {
                dirs = Directory.GetDirectories(dirPath);
            }
            catch (Exception ex)
            {
                if (!this.IgnoreAccessDenied)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
            if (dirs != null)
            {
                int index = 1;
                foreach (string dir in dirs)
                {
                    if (!dir.EndsWith(hidenFolder))
                    {
                        // Add node
                        string text = dir.Substring(dir.LastIndexOf('\\')).Trim('\\');
                        string[] files = null;
                        int dirCount = 0;

                        // Get the files and directories
                        try
                        {
                            files = Directory.GetFiles(dir);

                            string[] directories = Directory.GetDirectories(dir);
                            if (directories != null)
                            {
                                dirCount = directories.Length;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!this.IgnoreAccessDenied)
                            {
                                throw new Exception(ex.Message, ex);
                            }
                        }

                        TreeNode node = null;
                        if (index <= this.MaxSubFolders)
                        {
                            if (this.DisplayFilesCount && (files != null))
                            {
                                node = CreateNode(folderImageTag + "<span class=\"Name\">" + text + " (" + files.Length + ")</span>", text, parentNode, files.Length + dirCount, index);
                            }
                            else
                            {
                                node = CreateNode(folderImageTag + "<span class=\"Name\">" + text + "</span>", text, parentNode, dirCount, index);
                            }
                            parentNode.ChildNodes.Add(node);

                            // Recursive bind
                            BindTreeView(dir, node);
                        }
                        else if (!IsLiveSite)
                        {
                            // Render 'more' node only if not LiveSite
                            node = CreateNode("<span class=\"Name\">" + GetString("contenttree.seelisting") + "</span>", "", parentNode, 0, index);
                            parentNode.ChildNodes.Add(node);

                            this.RootHasMore = (isRoot && !this.CloseListing);
                        }

                        if (index <= this.MaxSubFolders)
                        {
                            index++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Expand parent node.
    /// </summary>
    /// <param name="node">Tree node</param>
    private void ExpandParent(TreeNode node)
    {
        if (node.Parent != null)
        {
            node.Parent.Expand();
            ExpandParent(node.Parent);
        }
        else
        {
            node.Expand();
        }
    }


    private TreeNode CreateNode(string nodeText, string nodeValue, TreeNode parrentNode, int childCount, int index)
    {
        TreeNode node = new TreeNode();

        if (!String.IsNullOrEmpty(this.CustomSelectFunction))
        {
            string output = "<span ";
            string val = nodeValue;
            if (parrentNode != null)
            {
                val = parrentNode.ValuePath.ToLower() + treeElem.PathSeparator + nodeValue.ToLower();
            }
            node.SelectAction = TreeNodeSelectAction.None;
            if (index <= this.MaxSubFolders)
            {
                output += "onClick=\" document.getElementById('" + this.hdnPath.ClientID + "').value = '" + val.Replace("\\", "\\\\") + "'; ";
                if ((childCount > this.MaxSubFolders) && !String.IsNullOrEmpty(this.CustomClickForMoreFunction))
                {
                    output += this.CustomClickForMoreFunction.Replace("##NODEVALUE##", val.Replace('\\', '|')) + " return false;\" ";
                }
                else
                {
                    output += this.CustomSelectFunction.Replace("##NODEVALUE##", val.Replace('\\', '|')) + " return false;\" ";
                }
            }
            else if (!string.IsNullOrEmpty(this.CustomClickForMoreFunction))
            {
                output += "onClick=\" document.getElementById('" + this.hdnPath.ClientID + "').value = '" + parrentNode.ValuePath.Replace("\\", "\\\\") + "'; ";
                output += this.CustomClickForMoreFunction.Replace("##NODEVALUE##", parrentNode.ValuePath.Replace('\\', '|')) + " return false;\" ";
            }

            output += ">";
            output += nodeText;
            output += "</span>";
            node.Text = output;
        }
        else
        {
            node.Text = nodeText;
        }
        node.Value = nodeValue.ToLower();

        return node;
    }

    #endregion


    #region "TreeView events"

    protected void treeElem_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (OnFolderSelected != null)
        {
            OnFolderSelected();
        }
    }

    #endregion
}
