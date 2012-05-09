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
using System.Text;

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.URLRewritingEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.IO;


public partial class CMSModules_Forums_Controls_PostTree : ForumViewer
{
    #region "Variables"

    /// <summary>
    /// Forum Post tree provider.
    /// </summary>
    private ForumPostTreeProvider mForumPostTreeProvider = null;

    private bool mAllowReply = true;
    private string mPostPath = "/%";    
    private bool mAdministrationMode = false;
    private int mSelected = 0;
    private bool mSelectOnlyApproved = false;
    private bool mRegularLoad = false;
    private int mMaxPostNodes = 0;
    private bool mUseMaxPostNodes = false;
    private ForumPostInfo mSelectedPost = null;
    private string mMaxTreeNodeText = null;
    private bool? mCanBeModerated = null;
    private bool? mUserIsModerator = null;


    /// <summary>
    /// If is true unapproved post is higlighted.
    /// </summary>
    protected bool mHighlightUnApproved = false;

    /// <summary>
    /// Item CSS class.
    /// </summary>
    protected string mItemCssClass = "ThreadPost";

    /// <summary>
    /// Selcted item CSS class.
    /// </summary>
    protected string mSelectedItemCssClass = "ThreadPostSelected";

    #endregion


    #region "Public events and delegates"

    /// <summary>
    /// Image delegate.
    /// </summary>
    public delegate string GetIconEventHandler(ForumPostTreeNode node);

    /// <summary>
    /// Gets image Url.
    /// </summary>
    public event GetIconEventHandler OnGetPostIconUrl;

    #endregion


    #region "Public properties"

    /// <summary>
    /// If is true unapproved post is higlighted.
    /// </summary>
    public bool HighlightUnApprove
    {
        get
        {
            return mHighlightUnApproved;
        }
        set
        {
            mHighlightUnApproved = value;
        }
    }


    /// <summary>
    /// Enable subscription.
    /// </summary>
    public bool AllowReply
    {
        get
        {
            return mAllowReply;
        }
        set
        {
            mAllowReply = value;
        }
    }


    /// <summary>
    /// Path.
    /// </summary>
    public string PostPath
    {
        get
        {
            return mPostPath;
        }
        set
        {
            mPostPath = value;
        }
    }


    /// <summary>
    /// Select only approved.
    /// </summary>
    public bool SelectOnlyApproved
    {
        get
        {
            return mSelectOnlyApproved;
        }
        set
        {
            mSelectOnlyApproved = value;
        }
    }


    /// <summary>
    /// Selected post id.
    /// </summary>
    public int Selected
    {
        get
        {
            return mSelected;
        }
        set
        {
            mSelected = value;
        }
    }


    /// <summary>
    /// Selected post info.
    /// </summary>
    public ForumPostInfo SelectedPost
    {
        get
        {
            if ((mSelectedPost == null) && (this.Selected > 0))
            {
                mSelectedPost = ForumPostInfoProvider.GetForumPostInfo(this.Selected);
            }

            return mSelectedPost;
        }
        set
        {
            mSelectedPost = value;
        }
    }


    /// <summary>
    /// Maximum number of forum post nodes displayed within one level of the tree.
    /// Setting CMSForumMaxPostNode is used by default.
    /// Note: UseMaxPostNodes property can suppress use of MaxPostNodes
    /// </summary>
    public int MaxPostNodes
    {
        get
        {
            if (mMaxPostNodes <= 0)
            {
                mMaxPostNodes = SettingsKeyProvider.GetIntValue(this.SiteName + ".CMSForumMaxPostNode");
            }
            return mMaxPostNodes;
        }
        set
        {
            mMaxPostNodes = value;
            MapProvider.MaxPostNodes = value + 1;
        }
    }


    /// <summary>
    /// Enables or disables use of MaxPostNodes property, which limits maximum number of posts 
    /// displayed in tree.
    /// </summary>
    public bool UseMaxPostNodes
    {
        get
        {
            return mUseMaxPostNodes;
        }
        set
        {
            mUseMaxPostNodes = value;
        }
    }


    /// <summary> 
    /// Enables or disables administration mode.
    /// </summary>
    public bool AdministrationMode
    {
        get
        {
            return mAdministrationMode;
        }
        set
        {
            mAdministrationMode = value;
        }
    }


    /// <summary>
    /// Post tree provider that the tree uses.
    /// </summary>
    public ForumPostTreeProvider MapProvider
    {
        get
        {
            if (mForumPostTreeProvider == null)
            {
                mForumPostTreeProvider = new ForumPostTreeProvider();

                // Set Map provider values
                mForumPostTreeProvider.BindNodeData = true;
                mForumPostTreeProvider.Path = this.PostPath;

                if (this.ExpandTree || this.DetailModeIE)
                {
                    // Load all posts
                    mForumPostTreeProvider.MaxRelativeLevel = ForumPostTreeProvider.ALL_LEVELS;
                }
                else
                {
                    // Load only first level
                    mForumPostTreeProvider.MaxRelativeLevel = 1;
                }

                mForumPostTreeProvider.SelectOnlyApproved = this.SelectOnlyApproved;
                mForumPostTreeProvider.ForumID = this.ForumID;
                mForumPostTreeProvider.WhereCondition = this.WhereCondition;
                mForumPostTreeProvider.Columns = GetColumns();

                // Limit number of displayed posts, usually in CMSDesk
                if (this.UseMaxPostNodes)
                {
                    mForumPostTreeProvider.MaxPostNodes = MaxPostNodes + 1;
                }
            }

            return mForumPostTreeProvider;
        }
    }


    /// <summary>
    /// Provides acces to treeElem property.
    /// </summary>
    public TreeView TreeView
    {
        get
        {
            return treeElem;
        }
    }


    /// <summary>
    /// Item CSS class.
    /// </summary>
    public string ItemCssClass
    {
        get
        {
            return mItemCssClass;
        }
        set
        {
            mItemCssClass = value;
        }
    }


    /// <summary>
    /// Selected item CSS class.
    /// </summary>
    public string SelectedItemCssClass
    {
        get
        {
            return mSelectedItemCssClass;
        }
        set
        {
            mSelectedItemCssClass = value;
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
                mMaxTreeNodeText = GetString("general.seelisting");
            }
            return mMaxTreeNodeText;
        }
        set
        {
            mMaxTreeNodeText = value;
        }
    }


    /// <summary>
    /// Indicates whether posts can be moderated in current context, 
    /// therefore unapproved post could be displayed.
    /// </summary>
    public bool CanBeModerated
    {
        get
        {
            if (!mCanBeModerated.HasValue)
            {
                ForumInfo fi = ForumContext.CurrentForum;
                mCanBeModerated = ((fi != null) && this.EnableOnSiteManagement && fi.ForumModerated && UserIsModerator);
            }

            return mCanBeModerated.Value;
        }
    }


    /// <summary>
    /// Indicates whether the current user is a moderator. That means: forum moderator or group admin or global admin.
    /// </summary>
    private bool UserIsModerator
    {
        get
        {
            if (!mUserIsModerator.HasValue)
            {
                bool result = false;
                ForumInfo fi = ForumContext.CurrentForum;
                result = (fi != null) && ForumContext.UserIsModerator(fi.ForumID, this.CommunityGroupID);

                if (!result && (this.CommunityGroupID > 0))
                {
                    result = CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", CMSAdminControl.PERMISSION_MANAGE);
                }

                mUserIsModerator = result;
            }

            return mUserIsModerator.Value;
        }
    }


    /// <summary>
    /// Indicates if special mode for treating (dynamic) detail mode in IE should be used. 
    /// The cause is that, IE can't handle too long parameters of TreeView_PopulateNode javascript function
    /// </summary>
    private bool DetailModeIE
    {
        get
        {
            return (((this.ShowMode == ShowModeEnum.DetailMode) || (this.ShowMode == ShowModeEnum.DynamicDetailMode)) && BrowserHelper.IsIE());
        }
    }

    #endregion


    #region "Helper methods and properties"

    /// <summary>
    /// Gets or sets the value that indicates whether data has been loaded or current load is 
    /// called on demand
    /// </summary>
    protected bool RegularLoad
    {
        get
        {
            return mRegularLoad;
        }
        set
        {
            mRegularLoad = value;
        }
    }


    /// <summary>
    /// Returns list of required columns if current mode is tree, otherwise returns null => load all columns
    /// </summary>
    /// <returns></returns>
    protected string GetColumns()
    {
        if (ShowMode == ShowModeEnum.TreeMode)
        {
            return "PostSubject, PostIDPath, PostParentID, PostApproved, PostID, PostThreadPosts, PostThreadPostsAbsolute, PostLevel";
        }

        return null;
    }


    /// <summary>
    /// Returns node text for dynamic mode.
    /// </summary>
    /// <param name="postRow">Forum post data row</param>
    private string CreateDetailModeNode(DataRow postRow)
    {
        StringBuilder sbRendered = null;

        // Render forum post control
        string forumPostControlId = DynamicForumPostRender(postRow, out sbRendered);

        string nodeText = String.Format("<div ID=\"Selected{0}\" style=\"display: block;\" class=\"TreeSelectedPost\">{1}</div>",
            forumPostControlId, sbRendered.ToString());

        return nodeText;
    }


    /// <summary>
    /// Returns node text for dynamic details mode.
    /// </summary>
    /// <param name="postRow">Forum post data row</param>
    /// <param name="cssClass">CSS class</param>
    /// <param name="imageTag">Image tag</param>
    /// <param name="postSubject">Post subject</param>    
    private string CreateDynamicDetailModeNode(DataRow postRow, string cssClass, string imageTag, string postSubject)
    {
        StringBuilder sbRendered = null;

        // Render forum post control
        string forumPostControlId = DynamicForumPostRender(postRow, out sbRendered);

        string nodeText = String.Format(
            "<span style=\"display:block;\" ID=\"{0}\" class=\"{1}\" onclick=\" SelectForumNode('Selected{0}','{0}'); return false;\">"
            + "{2}{3}" + //imagetag + postsubject
            "</span><div ID=\"Selected{0}\" style=\"display: none;\" class=\"TreeSelectedPost\">{4}</div>",
            forumPostControlId, cssClass, imageTag, HTMLHelper.HTMLEncode(postSubject), sbRendered.ToString());
        return nodeText;
    }


    /// <summary>
    /// Renders ForumPost control for specified node.
    /// </summary>
    /// <param name="postRow">Forum post data row</param>
    /// <param name="sbRendered">String builder instance containing rendered text of control</param>
    /// <returns>Id of the created forumpost control</returns>
    private string DynamicForumPostRender(DataRow postRow, out StringBuilder sbRendered)
    {
        // Create detail of post to string
        sbRendered = new StringBuilder();
        string mId = "";
        ForumPostInfo fpi = new ForumPostInfo(postRow);

        if (this.ShowMode != ShowModeEnum.TreeMode)
        {
            StringWriter sw = new StringWriter(sbRendered);
            Html32TextWriter writer = new Html32TextWriter(sw);
            CMSModules_Forums_Controls_Posts_ForumPost post = (CMSModules_Forums_Controls_Posts_ForumPost)this.Page.LoadControl("~/CMSModules/Forums/Controls/Posts/ForumPost.ascx");
            post.ID = "forumPost" + fpi.PostId;

            CopyValues(post);

            post.PostInfo = fpi;

            post.ReloadData();
            post.RenderControl(writer);
            mId = this.ClientID + fpi.PostId;
        }

        return mId;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ForumContext.ForumID = this.ForumID;
        
        treeElem.ImageSet = TreeViewImageSet.Custom;
        if ((this.IsLiveSite && CultureHelper.IsPreferredCultureRTL()) || (!this.IsLiveSite && CultureHelper.IsUICultureRTL()))
        {
            treeElem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", IsLiveSite, false);
        }
        else
        {
            treeElem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", IsLiveSite, false);
        }

        // Loading image script
        string loadingScript = @"
            if (TreeView_PopulateNode) { base_TreeView_PopulateNode = TreeView_PopulateNode };
            TreeView_PopulateNode = function(data, index, node, selectNode, selectImageNode, lineType, text, path, databound, datapath, parentIsLast) {
            if (!data) { return; }
            if (!node.blur) { node = node[0]; }
            node.blur();
            node.firstChild.src = '" + GetImageUrl("Design/Preloaders/preload16pad.gif") + "';" + @"
            if (base_TreeView_PopulateNode) {
                base_TreeView_PopulateNode(data, index, node, selectNode, selectImageNode, lineType, text, path, databound, datapath, parentIsLast); } }";

        ltlScript.Text = ScriptHelper.GetScript(loadingScript);

        treeElem.ExpandImageToolTip = GetString("ContentTree.Expand");
        treeElem.CollapseImageToolTip = GetString("ContentTree.Collapse");
    }


    /// <summary>
    /// OnPreRender.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!RequestHelper.IsCallback())
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        if (StopProcessing)
        {
            return;
        }

        mForumPostTreeProvider = null;

        this.RegularLoad = true;

        // Some post should be preselected => load all posts leading to it in one query
        if (this.SelectedPost != null)
        {
            MapProvider.SelectPostPath = this.SelectedPost.PostIDPath;
        }

        if (MapProvider.RootNode != null)
        {
            TreeNode rootNode = CreateNode((ForumPostTreeNode)MapProvider.RootNode, 0);
            // Add root node
            treeElem.Nodes.Add(rootNode);
            treeElem.EnableViewState = false;
        }
    }


    /// <summary>
    /// Creates tree node.
    /// </summary>
    /// <param name="sourceNode">Node with source data</param>
    protected TreeNode CreateNode(ForumPostTreeNode sourceNode, int index)
    {
        if (sourceNode == null)
        {
            return null;
        }

        // Create tree node
        TreeNode newNode = new TreeNode();

        DataRow dr = (DataRow)sourceNode.ItemData;

        // Check whether item data are defined, if not it is root node
        if (dr != null)
        {
            int sourceNodeId = (int)dr["PostID"];
            int nodeLevel = (int)dr["PostLevel"];

            // Check on maximum post in tree
            if (!this.UseMaxPostNodes || (index < MaxPostNodes))
            {
                #region "Set node values and appearance"

                newNode.Value = sourceNodeId.ToString();
                newNode.SelectAction = TreeNodeSelectAction.None;

                bool isApproved = ValidationHelper.GetBoolean(dr["PostApproved"], false);
                string postSubject = (string)dr["PostSubject"];

                string cssClass = this.ItemCssClass;

                // Add CSS class for unapproved posts
                if (HighlightUnApprove && !isApproved)
                {
                    cssClass += " PostUnApproved";
                }

                string imageTag = "";
                if (OnGetPostIconUrl != null)
                {
                    string imageUrl = OnGetPostIconUrl(sourceNode);
                    imageTag = "<img src=\"" + imageUrl + "\" alt=\"post\" style=\"border:0px;vertical-align:middle;\" />&nbsp;";
                }


                // Set by display mode
                switch (this.ShowMode)
                {
                    // Dynamic detail mode
                    case ShowModeEnum.DynamicDetailMode:
                        newNode.Text = CreateDynamicDetailModeNode(dr, cssClass, imageTag, postSubject);
                        break;

                    // Detail mode
                    case ShowModeEnum.DetailMode:
                        newNode.Text = CreateDetailModeNode(dr);
                        break;

                    // Tree mode
                    default:

                        if (this.Selected == sourceNodeId)
                        {
                            cssClass = this.SelectedItemCssClass;

                            string spanId = String.Empty;
                            if (this.AdministrationMode)
                            {
                                spanId = "id=\"treeSelectedNode\"";
                            }

                            newNode.Text = String.Format("<span {0} class=\"{1}\" onclick=\"ShowPost({2}); SelectForumNode(this);\">{3}<span class=\"Name\">{4}</span></span>",
                                spanId, cssClass, newNode.Value, imageTag, HTMLHelper.HTMLEncode(postSubject));
                        }
                        else
                        {
                            newNode.Text = String.Format("<span class=\"{0}\" onclick=\"ShowPost({1}); SelectForumNode(this);\">{2}<span class=\"Name\">{3}</span></span>",
                                cssClass, newNode.Value, imageTag, HTMLHelper.HTMLEncode(postSubject));
                        }
                        break;
                }

                #endregion

                if (!this.ExpandTree)
                {
                    #region "Populate deeper levels on demand"

                    // Check if can expand
                    string childCountColumn = "PostThreadPosts";

                    // Check if unapproved posts can be included
                    if (this.AdministrationMode || this.UserIsModerator)
                    {
                        childCountColumn = "PostThreadPostsAbsolute";
                    }

                    int childNodesCount = ValidationHelper.GetInteger(dr[childCountColumn], 0);

                    // If the post is thread(level = 0) then childnodes count 1 means no real child-post
                    if ((childNodesCount == 0) || ((childNodesCount == 1) && (nodeLevel == 0)))
                    {
                        newNode.PopulateOnDemand = false;

                        // No childs -> expand
                        newNode.Expanded = true;
                    }
                    else
                    {
                        if (!sourceNode.ChildNodesLoaded)
                        {
                            newNode.PopulateOnDemand = true;
                            newNode.Expanded = false;
                        }
                    }

                    #endregion

                    #region "Expand nodes on the current path"

                    // If preselect is set = first load
                    if (this.RegularLoad)
                    {
                        string currentNodePath = (string)dr["PostIDPath"];
                        string currentSelectedPath = String.Empty;

                        if (this.SelectedPost != null)
                        {
                            currentSelectedPath = this.SelectedPost.PostIDPath;
                        }

                        // Expand if node is on the path
                        if (currentSelectedPath.StartsWith(currentNodePath))
                        {
                            // Raise OnTreeNodePopulate
                            newNode.PopulateOnDemand = true;
                            newNode.Expanded = true;
                        }
                        else
                        {
                            newNode.Expanded = false;
                        }
                    }

                    #endregion
                }
                else
                {
                    // Populate will be called on each node
                    newNode.PopulateOnDemand = true;
                    newNode.Expanded = true;
                }
            }
            else
            {
                string parentNodeId = ValidationHelper.GetString(dr["PostParentID"], "");
                newNode.Value = sourceNodeId.ToString();
                newNode.Text = MaxTreeNodeText.Replace("##PARENTNODEID##", parentNodeId);
                newNode.SelectAction = TreeNodeSelectAction.None;
            }
        }
        // Root node populate by default
        else
        {
            // Root node as forum display name
            ForumInfo fi = ForumInfoProvider.GetForumInfo(this.ForumID);

            if (fi != null)
            {
                newNode.Text = "<span class=\"" + this.ItemCssClass + "\" onclick=\"ShowPost('-1'); SelectForumNode(this); \"><span class=\"Name\">" + HTMLHelper.HTMLEncode(fi.ForumDisplayName) + "</span></span>";
                newNode.Value = "0";
                newNode.SelectAction = TreeNodeSelectAction.None;
            }

            newNode.PopulateOnDemand = true;
            newNode.Expanded = true;
        }

        return newNode;
    }


    /// <summary>
    /// On populate create child nodes.
    /// </summary>
    protected void treeElem_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        e.Node.ChildNodes.Clear();
        e.Node.PopulateOnDemand = false;

        int postId = ValidationHelper.GetInteger(e.Node.Value, 0);

        // Set the ForumID if not set already
        if (ForumContext.ForumID == 0)
        {
            ForumPostInfo postInfo = ForumPostInfoProvider.GetForumPostInfo(postId);
            if (postInfo != null)
            {
                ForumContext.ForumID = postInfo.PostForumID;
                this.ForumID = postInfo.PostForumID;
            }
        }

        // Get child nodes
        SiteMapNodeCollection childNodes = MapProvider.GetChildNodes(postId, this.RegularLoad);

        int index = 0;
        foreach (ForumPostTreeNode childNode in childNodes)
        {
            int childNodeId = (int)((DataRow)childNode.ItemData)["PostID"];
            if (childNodeId != postId)
            {
                TreeNode newNode = CreateNode(childNode, index);
                bool? originalExpanded = newNode.Expanded;

                // Force node to expand and load child posts
                if (this.DetailModeIE)
                {
                    newNode.PopulateOnDemand = true;
                    newNode.Expanded = true;
                }

                e.Node.ChildNodes.Add(newNode);

                // Restore original expanded state
                if (this.DetailModeIE)
                {
                    newNode.Expanded = originalExpanded;
                }

                index++;
            }

            // Ensure there is only one 'click here for more' item
            if (this.UseMaxPostNodes && (index > this.MaxPostNodes))
            {
                break;
            }
        }
    }


    /// <summary> 
    /// Render 
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        if (!this.StopProcessing)
        {
            base.Render(writer);
        }
    }

    #endregion
}
