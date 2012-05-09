using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.URLRewritingEngine;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_Trees_UniTree : FormEngineUserControl, IPostBackEventHandler
{
    #region "Variables"

    private bool mEnableRootAction = true;
    private bool mCollapseAll = false;
    private bool mExpandAll = false;
    private bool mUsePostBack = true;

    private string mSelectedItem = null;

    private string mExpandPath = string.Empty;
    private string mSelectPath = string.Empty;

    private string mNodeTemplate = "##ICON####NODENAME##";
    private string mSelectedNodeTemplate = "##ICON####NODENAME##";
    private string mDefaultItemTemplate = "##ICON####NODENAME##";
    private string mSelectedDefaultItemTemplate = "##ICON####NODENAME##";

    private UniTreeProvider mProviderObject = null;
    private TreeNode mCustomRootNode = null;
    private TreeNode mRootNode = null;

    private bool mLocalize = false;
    private bool mDisplayPopulatingindicator = true;

    private string selectedPath = string.Empty;
    private string mCollapseTooltip = null;
    private string mExpandTooltip = null;
    private string mLineImagesFolder = string.Empty;
    private string mDefaultImagePath = string.Empty;
    private int mExpandLevel = 0;

    private ArrayList defaultItems = new ArrayList();

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value which indicates whether pupulating indicator should be displayed or not.
    /// </summary>
    public bool DisplayPopulatingIndicator
    {
        get
        {
            return mDisplayPopulatingindicator;
        }
        set
        {
            mDisplayPopulatingindicator = value;
        }
    }

    /// <summary>
    /// Indicates if the root element action should be None or Select.
    /// </summary>
    public bool EnableRootAction
    {
        get
        {
            return this.mEnableRootAction;
        }
        set
        {
            this.mEnableRootAction = value;
        }
    }


    /// <summary>
    /// Indicates if all nodes should be expanded.
    /// </summary>
    public bool ExpandAll
    {
        get
        {
            return this.mExpandAll;
        }
        set
        {
            this.mExpandAll = value;
        }
    }


    /// <summary>
    /// Indicates if all nodes should be collapsed.
    /// </summary>
    public bool CollapseAll
    {
        get
        {
            return this.mCollapseAll;
        }
        set
        {
            this.mCollapseAll = value;
        }
    }


    /// <summary>
    /// Gets or sets the ToolTip for the image that is displayed for the expandable node indicator.
    /// </summary>
    public string ExpandTooltip
    {
        get
        {
            return mExpandTooltip;
        }
        set
        {
            mExpandTooltip = value;
            treeElem.ExpandImageToolTip = value;
        }
    }


    /// <summary>
    /// Gets or sets the ToolTip for the image that is displayed for the collapsible node indicator.
    /// </summary>
    public string CollapseTooltip
    {
        get
        {
            return mCollapseTooltip;
        }
        set
        {
            treeElem.CollapseImageToolTip = value;
            mCollapseTooltip = value;
        }
    }


    /// <summary>
    /// Gets or sets the path to a folder that contains the line images that are used to connect child nodes to parent nodes.   
    /// </summary>
    public string LineImagesFolder
    {
        get
        {
            if (string.IsNullOrEmpty(mLineImagesFolder))
            {
                if ((this.IsLiveSite && CultureHelper.IsPreferredCultureRTL()) || (!this.IsLiveSite && CultureHelper.IsUICultureRTL()))
                {
                    mLineImagesFolder = "~" + URLHelper.CurrentRelativePath + "?cmsimg=/rt"; // GetImageUrl("RTL/Design/Controls/Tree", IsLiveSite, true);
                }
                else
                {
                    mLineImagesFolder = "~" + URLHelper.CurrentRelativePath + "?cmsimg=/t"; // GetImageUrl("Design/Controls/Tree", IsLiveSite, true);
                }
            }
            return mLineImagesFolder;
        }
        set
        {
            mLineImagesFolder = value;
            treeElem.LineImagesFolder = value;
        }
    }


    /// <summary>
    /// Indicates number of expanded levels.
    /// </summary>
    public int ExpandLevel
    {
        get
        {
            return this.mExpandLevel;
        }
        set
        {
            this.mExpandLevel = value;
        }
    }


    /// <summary>
    /// Indicates if ##NODENAME## should be localized.
    /// </summary>
    public bool Localize
    {
        get
        {
            return this.mLocalize;
        }
        set
        {
            this.mLocalize = value;
        }
    }


    /// <summary>
    /// Gets ors sets the value which determines whether the tree will generate postbacks on node click.
    /// </summary>
    public bool UsePostBack
    {
        get
        {
            return this.mUsePostBack;
        }
        set
        {
            this.mUsePostBack = value;
        }
    }


    /// <summary>
    /// Gets or sets selected item.
    /// </summary>
    public string SelectedItem
    {
        get
        {
            if (mSelectedItem == null)
            {
                mSelectedItem = hdnSelectedItem.Value;
            }

            return mSelectedItem;
        }
        set
        {
            hdnSelectedItem.Value = value;
            mSelectedItem = value;
        }
    }


    /// <summary>
    /// Gets the client ID of hidden field with selected item value.
    /// </summary>
    public string SelectedItemFieldId
    {
        get
        {
            return this.hdnSelectedItem.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets expand path.
    /// </summary>
    public string ExpandPath
    {
        get
        {
            return mExpandPath;
        }
        set
        {
            mExpandPath = value;
        }
    }


    /// <summary>
    /// Gets or sets select path.
    /// </summary>
    public string SelectPath
    {
        get
        {
            return mSelectPath;
        }
        set
        {
            mSelectPath = value;
        }
    }


    /// <summary>
    /// Gets or sets node template. You can use following macros:
    /// ##NODEID## (ID of item),##PARENTNODEID## (ID of parent), ##NODEJAVA## (encoded item name for using in javascript), 
    /// ##NODECHILDNODESCOUNT## (count of childs of node), ##NODENAME## (name of item), ##ICON## (image), ##OBJECTTYPE## (object type).
    /// </summary>
    public string NodeTemplate
    {
        get
        {
            return mNodeTemplate;
        }
        set
        {
            mNodeTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets node template. You can use following macros:
    /// ##NODEID## (ID of item), ##PARENTNODEID## (ID of parent), ##NODEJAVA## (encoded item name for using in javascript), 
    /// ##NODECHILDNODESCOUNT## (count of childs of node), ##NODENAME## (name of item), ##ICON## (image), ##OBJECTTYPE## (object type). 
    /// </summary>
    public string SelectedNodeTemplate
    {
        get
        {
            return mSelectedNodeTemplate;
        }
        set
        {
            mSelectedNodeTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets node template. You can use following macros:
    /// ##NODEJAVA## (encoded item name for using in javascript), 
    /// ##NODENAME## (name of item), ##ICON## (image) 
    /// </summary>
    public string DefaultItemTemplate
    {
        get
        {
            return mDefaultItemTemplate;
        }
        set
        {
            mDefaultItemTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets node template. You can use following macros:
    /// ##NODEJAVA## (encoded item name for using in javascript), 
    /// ##NODENAME## (name of item), ##ICON## (image) 
    /// </summary>
    public string SelectedDefaultItemTemplate
    {
        get
        {
            return mSelectedDefaultItemTemplate;
        }
        set
        {
            mSelectedDefaultItemTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets TreeProvider object.
    /// </summary>
    public UniTreeProvider ProviderObject
    {
        get
        {
            return mProviderObject;
        }
        set
        {
            mProviderObject = value;
        }
    }


    /// <summary>
    /// Gets or sets default image path.
    /// </summary>
    public string DefaultImagePath
    {
        get
        {
            return mDefaultImagePath;
        }
        set
        {
            mDefaultImagePath = value;
        }
    }


    /// <summary>
    /// Gets custom root node.
    /// </summary>
    public TreeNode CustomRootNode
    {
        get
        {
            return mCustomRootNode;
        }
    }


    /// <summary>
    /// Gets root node from provider object.
    /// </summary>
    public TreeNode RootNode
    {
        get
        {
            if (mRootNode == null)
            {
                return CustomRootNode;
            }

            return mRootNode;
        }
    }

    #endregion


    #region "Custom events"

    /// <summary>
    /// Image delegate.
    /// </summary>
    public delegate string GetImageEventHandler(UniTreeNode node);


    /// <summary>
    /// Gets image Url.
    /// </summary>
    public event GetImageEventHandler OnGetImage;


    /// <summary>
    /// On selected item event handler.
    /// </summary>    
    public delegate void ItemSelectedEventHandler(string selectedValue);


    /// <summary>
    /// On selected item event handler.
    /// </summary>
    public event ItemSelectedEventHandler OnItemSelected;


    /// <summary>
    /// Node created delegate.
    /// </summary>
    public delegate TreeNode NodeCreatedEventHandler(DataRow itemData, TreeNode defaultNode);


    /// <summary>
    /// Node created event handler.
    /// </summary>
    public event NodeCreatedEventHandler OnNodeCreated;

    #endregion


    #region "Events"

    /// <summary>
    /// Page load event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        mCollapseTooltip = GetString("general.collapse");
        mExpandTooltip = GetString("general.expand");

        treeElem.ExpandImageToolTip = ExpandTooltip;
        treeElem.CollapseImageToolTip = CollapseTooltip;

        treeElem.LineImagesFolder = LineImagesFolder;

        btnItemSelected.Style.Add("display", "none");
    }


    /// <summary>
    /// Tree populate.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Arguments</param>
    protected void treeElem_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        OnNodePopulate(e.Node);
    }


    /// <summary>
    /// Page PreRender.
    /// </summary>
    /// <param name="e">Arguments</param>
    protected override void OnPreRender(EventArgs e)
    {
        int index = 0;

        foreach (object item in defaultItems)
        {
            string[] defaultItem = (string[])item;

            if (defaultItem != null)
            {
                // Generate link HTML tag
                string link = null;
                string selectedItem = ValidationHelper.GetString(SelectedItem, "").ToLower();
                if (selectedItem == defaultItem[2].ToLower())
                {
                    link = ReplaceMacros(SelectedDefaultItemTemplate, 0, 0, defaultItem[0], defaultItem[1], 0, "", "");
                }
                else
                {
                    link = ReplaceMacros(DefaultItemTemplate, 0, 0, defaultItem[0], defaultItem[1], 0, "", "");
                }

                // Add complete HTML code to page
                if (this.UsePostBack)
                {
                    link = "<span onclick=\"" + ControlsHelper.GetPostBackEventReference(this, HTMLHelper.HTMLEncode(defaultItem[2])) + "\">" + link + "</span>";
                }

                TreeNode tn = new TreeNode();
                tn.Text = link;
                tn.NavigateUrl = URLRewriter.CurrentURL + "#";
                treeElem.Nodes.AddAt(index, tn);
                index++;
            }
        }

        if (DisplayPopulatingIndicator && !Page.IsCallback)
        {
            // Register tree progress icon
            ScriptHelper.RegisterTreeProgress(this.Page);
        }

        base.OnPreRender(e);
    }


    #endregion


    #region "Methods"

    /// <summary>
    /// Adds default item to control (link over the tree).
    /// </summary>
    /// <param name="itemName">Item name</param>
    /// <param name="value">Value</param>
    /// <param name="imagePath">Image path</param>
    public void AddDefaultItem(string itemName, string value, string imagePath)
    {
        string imgTag = "";

        // Generate image HTML tag
        if (!string.IsNullOrEmpty(imagePath))
        {
            imgTag = "<img class=\"TreeItemImage\" alt=\"" + HTMLHelper.HTMLEncode(ResHelper.LocalizeString(itemName)) + "\" src=\"" + HTMLHelper.HTMLEncode(imagePath) + "\" />";
        }

        // Insert default item to arraylist
        string[] defaultItem = new string[3];
        defaultItem[0] = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(itemName));
        defaultItem[1] = imgTag;
        defaultItem[2] = value;

        defaultItems.Add(defaultItem);
    }


    /// <summary>
    /// Adds root node (allows customization of the root item).
    /// </summary>
    /// <param name="rootText">Root text</param>
    /// <param name="value">Root value</param>
    /// <param name="imagePath">Image path</param>
    public void SetRoot(string rootText, string value, string imagePath)
    {
        mCustomRootNode = new TreeNode(rootText, value, imagePath);
    }


    /// <summary>
    /// Adds root node (allows customization of the root item).
    /// </summary>
    /// <param name="rootText">Root text</param>
    /// <param name="value">Root value</param>
    /// <param name="imagePath">Image path</param>
    /// <param name="navigateUrl">Navigate URL</param>
    /// <param name="target">Target</param>
    public void SetRoot(string rootText, string value, string imagePath, string navigateUrl, string target)
    {
        mCustomRootNode = new TreeNode(rootText, value, imagePath, navigateUrl, target);
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public void ReloadData()
    {
        if (!this.StopProcessing && (ProviderObject != null))
        {
            treeElem.Nodes.Clear();
            this.treeElem.EnableViewState = false;

            // Add custom root node
            if (mCustomRootNode != null)
            {
                if (!this.EnableRootAction)
                {
                    mCustomRootNode.SelectAction = TreeNodeSelectAction.None;
                }

                treeElem.Nodes.Add(mCustomRootNode);
                RaiseOnPopulateRootNode();
            }
            else
            {
                // Add root node from provider
                if (ProviderObject.RootNode != null)
                {
                    mRootNode = CreateNode((UniTreeNode)ProviderObject.RootNode);
                    treeElem.Nodes.Add(mRootNode);
                }
            }
        }
    }


    /// <summary>
    /// Creates node.
    /// </summary>
    /// <param name="uniNode">Node to create</param>
    protected TreeNode CreateNode(UniTreeNode uniNode)
    {
        DataRow dr = (DataRow)uniNode.ItemData;
        if (dr != null)
        {
            TreeNode node = new TreeNode();

            // Get data
            int childNodesCount = 0;
            if (!string.IsNullOrEmpty(ProviderObject.ChildCountColumn))
            {
                childNodesCount = ValidationHelper.GetInteger(dr[ProviderObject.ChildCountColumn], 0);
            }

            // Node ID
            int nodeID = 0;
            if (!string.IsNullOrEmpty(ProviderObject.IDColumn))
            {
                nodeID = ValidationHelper.GetInteger(dr[ProviderObject.IDColumn], 0);
            }

            // Node value
            string nodeValue = string.Empty;
            if (!string.IsNullOrEmpty(ProviderObject.ValueColumn))
            {
                nodeValue = nodeID + "_" + ValidationHelper.GetString(dr[ProviderObject.ValueColumn], "");
            }

            string objectType = string.Empty;
            if (!string.IsNullOrEmpty(ProviderObject.ObjectTypeColumn))
            {
                objectType = ValidationHelper.GetString(dr[ProviderObject.ObjectTypeColumn], "");

                // Add object type to value
                nodeValue += "_" + objectType;
            }

            // Display name
            string displayName = string.Empty;
            if (!string.IsNullOrEmpty(ProviderObject.DisplayNameColumn))
            {
                displayName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(ValidationHelper.GetString(dr[ProviderObject.DisplayNameColumn], "")));
            }

            // Path
            string nodePath = string.Empty;
            if (!string.IsNullOrEmpty(ProviderObject.PathColumn))
            {
                nodePath = HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr[ProviderObject.PathColumn], "")).ToLower();
            }

            // Parent ID
            int parentID = 0;
            if (!string.IsNullOrEmpty(ProviderObject.ParentIDColumn))
            {
                parentID = ValidationHelper.GetInteger(dr[ProviderObject.ParentIDColumn], 0);
            }

            // Parameter
            string parameter = string.Empty;
            if (!string.IsNullOrEmpty(ProviderObject.ParameterColumn))
            {
                parameter = HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr[ProviderObject.ParameterColumn], "")).ToLower();
            }

            int nodeLevel = 0;
            if (!String.IsNullOrEmpty(ProviderObject.LevelColumn))
            {
                nodeLevel = ValidationHelper.GetInteger(dr[ProviderObject.LevelColumn], 0);
            }

            node.NavigateUrl = URLRewriter.CurrentURL + "#";

            // Set value
            node.Value = nodeValue;

            // Get image html tag
            string imgPath = string.Empty;

            if (!string.IsNullOrEmpty(ProviderObject.ImageColumn))
            {
                imgPath = ResolveUrl(ValidationHelper.GetString(dr[ProviderObject.ImageColumn], ""));
            }

            if (string.IsNullOrEmpty(imgPath))
            {
                imgPath = ResolveUrl(DefaultImagePath);
            }

            if (OnGetImage != null)
            {
                imgPath = ResolveUrl(OnGetImage(uniNode));
            }

            string imgTag = string.Empty;
            if (!string.IsNullOrEmpty(imgPath))
            {
                imgTag = "<img class=\"TreeItemImage\" alt=\"" + ResHelper.LocalizeString(displayName) + "\" src=\"" + HTMLHelper.HTMLEncode(imgPath) + "\"/>";
            }

            // Set text
            string text = null;

            string selectedItem = ValidationHelper.GetString(SelectedItem, "");
            string selectPathLowered = SelectPath.ToLower();

            if (nodeValue.Equals(selectedItem, StringComparison.InvariantCultureIgnoreCase) || ((selectPathLowered == nodePath) && string.IsNullOrEmpty(selectedItem)))
            {
                text = ReplaceMacros(SelectedNodeTemplate, nodeID, childNodesCount, displayName, imgTag, parentID, objectType, parameter);
            }
            else
            {
                text = ReplaceMacros(NodeTemplate, nodeID, childNodesCount, displayName, imgTag, parentID, objectType, parameter);
            }

            if (this.UsePostBack)
            {
                text = "<span onclick=\"" + ControlsHelper.GetPostBackEventReference(this, nodeValue + ";" + nodePath) + "\">" + text + "</span>";
            }

            node.Text = text;

            // Set populate node automatically
            if (childNodesCount != 0)
            {
                node.PopulateOnDemand = true;
            }

            // Expand tree            
            if (this.ExpandAll)
            {
                node.Expanded = true;
            }
            else if (this.CollapseAll)
            {
                node.Expanded = false;
            }
            else
            {
                //// Handle expand path
                if (nodePath != "/")
                {
                    nodePath += "/";
                }

                string expandPathLowered = ExpandPath.ToLower();
                // SelectPath property
                if (selectPathLowered.StartsWith(nodePath) && (selectPathLowered != nodePath))
                {
                    node.Expanded = true;
                }
                // ExpandPath property
                else if ((expandPathLowered.StartsWith(nodePath)))
                {
                    node.Expanded = true;
                }
                // Path expanded by user
                else if (selectedPath.StartsWith(nodePath) && (selectedPath != nodePath))
                {
                    node.Expanded = true;
                }
                else
                {
                    node.Expanded = false;
                }
            }

            // Expand level
            if ((this.ExpandLevel != 0) && !this.CollapseAll)
            {
                node.Expanded = nodeLevel <= this.ExpandLevel;
            }

            return node;
        }
        return null;
    }


    /// <summary>
    /// Replaces all macros in template by values.
    /// </summary>
    /// <param name="template">Template with macros</param>
    /// <param name="itemID">Item ID</param>
    /// <param name="childCount">Child count</param>
    /// <param name="nodeName">Node name</param>
    /// <param name="parentNodeID">Parent item ID</param>
    /// <param name="icon">Icon</param>  
    /// <param name="objectType">Object type</param>
    /// <param name="objectType">Additional parameter</param>
    public string ReplaceMacros(string template, int itemID, int childCount, string nodeName, string icon, int parentNodeID, string objectType, string parameter)
    {
        template = template.Replace("##NODEID##", itemID.ToString());
        template = template.Replace("##NODEJAVA##", ScriptHelper.GetString(nodeName));
        template = template.Replace("##NODECHILDNODESCOUNT##", childCount.ToString());
        if (Localize)
        {
            template = template.Replace("##NODENAME##", ResHelper.LocalizeString(nodeName));
        }
        else
        {
            template = template.Replace("##NODENAME##", nodeName);
        }
        template = template.Replace("##ICON##", icon);
        template = template.Replace("##PARENTNODEID##", parentNodeID.ToString());
        template = template.Replace("##OBJECTTYPE##", objectType);
        template = template.Replace("##PARAMETER##", parameter);

        return template;
    }


    /// <summary>
    /// Returns javascript code raising postback and OnItemSelected event.
    /// </summary>
    /// <param name="argument">Postback parameter</param>    
    public string GetOnSelectedItemBackEventReference()
    {
        return ControlsHelper.GetPostBackEventReference(this.btnItemSelected, null, false);
    }


    /// <summary>
    /// Handles simulated hidden button click and raises OnItemSelected event with value from hidden field.
    /// </summary>
    protected void btnItemSelected_Click(object sender, EventArgs e)
    {
        RaiseOnItemSelected(hdnSelectedItem.Value);
    }


    /// <summary>
    /// Raises on selected item event.
    /// </summary>
    /// <param name="selectedValue">Selected value</param>    
    public void RaiseOnItemSelected(string selectedValue)
    {
        SelectedItem = selectedValue;

        if (OnItemSelected != null)
        {
            OnItemSelected(selectedValue);
        }
    }


    /// <summary>
    /// Populates root node.
    /// </summary>
    public void RaiseOnPopulateRootNode()
    {
        TreeNode node = treeElem.Nodes[0];
        if (node != null)
        {
            if (!string.IsNullOrEmpty(ExpandPath))
            {
                node.Expanded = true;
                OnNodePopulate(node);
            }
        }
    }


    /// <summary>
    /// Handle node is populated.
    /// </summary>
    private void OnNodePopulate(TreeNode node)
    {
        if ((ProviderObject != null) && (node != null))
        {
            string[] splitted = node.Value.Split('_');
            int nodeID = ValidationHelper.GetInteger(splitted[0], 0);

            // Get node object type
            string nodeObjectType = string.Empty;
            if (splitted.Length == 2)
            {
                nodeObjectType = splitted[1];
            }

            // Get child nodes
            SiteMapNodeCollection childNodes = ProviderObject.GetChildNodes(node.Value, node.Depth + 1);

            // Add to treeview
            foreach (UniTreeNode childNode in childNodes)
            {
                // Get ID
                int childNodeId = (int)(((DataRow)childNode.ItemData)[ProviderObject.IDColumn]);

                // Get object type
                string childNodeType = string.Empty;
                if (!string.IsNullOrEmpty(ProviderObject.ObjectTypeColumn))
                {
                    childNodeType = ValidationHelper.GetString((((DataRow)childNode.ItemData)[ProviderObject.ObjectTypeColumn]), "");
                }

                // Don't insert one object more than once
                if ((childNodeId != nodeID) || (nodeObjectType != childNodeType))
                {
                    TreeNode createdNode = CreateNode(childNode);
                    if (OnNodeCreated != null)
                    {
                        createdNode = OnNodeCreated((DataRow)childNode.ItemData, createdNode);
                    }
                    if (createdNode != null)
                    {
                        node.ChildNodes.Add(createdNode);
                    }
                }
            }
        }
    }


    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Raises event postback event.
    /// </summary>
    /// <param name="eventArgument"></param>
    public void RaisePostBackEvent(string eventArgument)
    {
        // Get argument
        string arg = HttpUtility.HtmlDecode(eventArgument);

        // Get value
        string[] selectedItem = arg.Split(';');
        string value = selectedItem[0];

        // Get path
        if (selectedItem.Length >= 2)
        {
            selectedPath = selectedItem[1].ToLower();
        }

        // Raise event
        RaiseOnItemSelected(value);
    }

    #endregion
}
