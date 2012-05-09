using System;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;

using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSAdminControls_UI_Trees_ObjectTree : CMSUserControl
{
    #region "Events"

    /// <summary>
    /// Fires when new node is created, returns true if the object should be created.
    /// </summary>
    public event ObjectTypeTreeNode.OnBeforeCreateNodeHandler OnBeforeCreateNode;

    #endregion


    #region "Variables"

    private string mNodeTextTemplate = "##ICON####NODENAME##";
    private string mSelectedNodeTextTemplate = "##ICON####NODENAME##";

    private string mValueTextTemplate = null;
    private string mPreselectObjectType = string.Empty;

    private ObjectTypeTreeNode mRootNode = null;
    private int mSiteId = 0;

    private bool mUsePostback = false;
    private bool mUseImages = false;
    private bool mIsPreselectedObjectTypeSiteObject = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Tree definition.
    /// </summary>
    public ObjectTypeTreeNode RootNode
    {
        get
        {
            return mRootNode;
        }
        set
        {
            mRootNode = value;
        }
    }


    /// <summary>
    /// Template of the node text.
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
    /// Template of the selected node text.
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
    /// Template of the node Value.
    /// </summary>
    public string ValueTextTemplate
    {
        get
        {
            return mValueTextTemplate;
        }
        set
        {
            mValueTextTemplate = value;
        }
    }


    /// <summary>
    /// Site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }


    /// <summary>
    /// Tree view control.
    /// </summary>
    public TreeView TreeView
    {
        get
        {
            return treeElem;
        }
    }


    /// <summary>
    /// Use postback on node selection.
    /// </summary>
    public bool UsePostback
    {
        get
        {
            return mUsePostback;
        }
        set
        {
            mUsePostback = value;
        }
    }


    /// <summary>
    /// Use images on node tree.
    /// </summary>
    public bool UseImages
    {
        get
        {
            return mUseImages;
        }
        set
        {
            mUseImages = value;
        }
    }


    public override bool EnableViewState
    {
        get
        {
            return base.EnableViewState;
        }
        set
        {
            base.EnableViewState = value;
            treeElem.EnableViewState = value;
        }
    }


    /// <summary>
    /// Defines whether some specific object type should be preselected.
    /// </summary>
    public string PreselectObjectType
    {
        get
        {
            return mPreselectObjectType;
        }
        set
        {
            mPreselectObjectType = value;
        }
    }


    /// <summary>
    /// Defines whether preselected object type is site-related object type.
    /// </summary>
    public bool IsPreselectedObjectTypeSiteObject
    {
        get
        {
            return mIsPreselectedObjectTypeSiteObject;
        }
        set
        {
            mIsPreselectedObjectTypeSiteObject = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        treeElem.ExpandImageToolTip = GetString("contenttree.expand");
        treeElem.CollapseImageToolTip = GetString("contenttree.collapse");

        string imagePath = "Design/Controls/Tree/";
        if (CultureHelper.IsUICultureRTL())
        {
            imagePath = "RTL/" + imagePath;
        }

        treeElem.LineImagesFolder = GetImageUrl(imagePath, IsLiveSite, false);

        if (!RequestHelper.IsPostBack())
        {
            ReloadData();
        }
    }

    #endregion


    #region "Other methods"

    public void ReloadData()
    {
        treeElem.Nodes.Clear();

        if (RootNode != null)
        {
            TreeNode rootNode = CreateNode(RootNode);
            rootNode.Selected = true;

            treeElem.Nodes.Add(rootNode);

            // Expand node structure
            TreeNode nodeToExpand = treeElem.SelectedNode.Parent;
            while (nodeToExpand != null)
            {
                nodeToExpand.Expanded = true;
                nodeToExpand = nodeToExpand.Parent;
            }
        }
    }


    /// <summary>
    /// Defines whether preselected object type is contained in the structure.
    /// </summary>
    public bool ContainsObjectType(string type)
    {
        if (treeElem.Nodes.Count > 0)
        {
            return ContainsObjectType(treeElem.Nodes[0], type);
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Defines whether preselected object type is contained in the structure.
    /// </summary>
    public bool ContainsObjectType(TreeNode nodeToExpand, string type)
    {
        if (nodeToExpand.Value == type)
        {
            return true;
        }

        if (nodeToExpand.ChildNodes.Count > 0)
        {
            foreach (TreeNode node in nodeToExpand.ChildNodes)
            {
                if (ContainsObjectType(node, type))
                {
                    return true;
                }
            }
        }
        return false;
    }


    public TreeNode CreateNode(ObjectTypeTreeNode source)
    {
        // Create new node
        TreeNode newNode = new TreeNode();
        newNode.Expanded = source.Expand;

        // Get the image
        string objectType = source.ObjectType ?? source.Group;

        if (!UsePostback)
        {
            newNode.NavigateUrl = "#";
        }

        string template = NodeTextTemplate;

        // Site
        int siteId = 0;
        if (source.Site)
        {
            siteId = SiteID;
        }

        // Title
        string name = GetString("ObjectTasks." + objectType.Replace(".", "_").Replace("#", "_"));

        // Image
        string imageUrl = GetObjectIconUrl(objectType, "list.png");
        string imageTag = "<img src=\"" + imageUrl + "\" alt=\"\" style=\"border:0px;vertical-align:middle;\" onclick=\"return false;\"/>";
        if ((objectType == String.Empty) || (objectType == CMSObjectHelper.GROUP_OBJECTS))
        {
            imageUrl = GetImageUrl("General/DefaultRoot.png");
            imageTag = "<img src=\"" + imageUrl + "\" alt=\"\" style=\"border:none;height:10px;width:1px;\" onclick=\"return false;\"/>";
            template = imageTag + SelectedNodeTextTemplate;
            siteId = -1;
            name = GetString("ObjectTasks.Root");
        }

        if (source.Main)
        {
            name = "<strong>" + name + "</strong>";
        }

        newNode.Text = template.Replace("##ICON##", imageTag).Replace("##NODENAME##", name).Replace("##SITEID##", siteId.ToString()).Replace("##OBJECTTYPE##", HttpUtility.UrlEncode(objectType));

        if (ValueTextTemplate != null)
        {
            newNode.Value = ValueTextTemplate.Replace("##SITEID##", siteId.ToString()).Replace("##OBJECTTYPE##", objectType);
        }

        // Add image to the node
        if (UseImages && (imageUrl != ""))
        {
            newNode.ImageUrl = imageUrl;
        }

        // Disable if not active node
        if (UsePostback && !source.Active)
        {
            newNode.SelectAction = TreeNodeSelectAction.None;
        }

        if (source.Group != null && !source.Active)
        {
            newNode.SelectAction = TreeNodeSelectAction.Expand;
        }

        // Add child nodes
        if (source.ChildNodes.Count > 0)
        {
            foreach (ObjectTypeTreeNode child in source.ChildNodes)
            {
                if ((SiteID > 0) || !child.Site)
                {
                    if ((OnBeforeCreateNode == null) || OnBeforeCreateNode(child))
                    {
                        // Create child node
                        TreeNode childNode = CreateNode(child);
                        if ((child.ObjectType != null) || (childNode.ChildNodes.Count > 0))
                        {
                            newNode.ChildNodes.Add(childNode);
                            // Preselect node
                            if ((child.ObjectType == PreselectObjectType) && (child.Site == IsPreselectedObjectTypeSiteObject))
                            {
                                childNode.Selected = true;
                            }
                        }
                    }
                }
            }
        }

        return newNode;
    }

    #endregion
}
