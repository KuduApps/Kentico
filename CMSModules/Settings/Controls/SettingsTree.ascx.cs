using System;
using System.Web.UI.WebControls;
using System.Data;

using CMS.SiteProvider;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.CMSStorage;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_Controls_SettingsTree : CMSUserControl
{
    #region "Variables"

    protected string mCategoryName = null;
    protected string mModuleName = null;
    protected string mJavaScriptHandler = null;
    protected int mSiteId = 0;

    private SettingsCategoryInfo mRoot = null;
    private CurrentUserInfo currentUser = null;

    private int mMaxRelativeLevel = 1;
    private bool mRootIsClickable = true;
    private bool mShowEmptyCategories = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the root node of the tree.
    /// </summary>
    public TreeNode RootNode
    {
        get
        {
            return treeElem.CustomRootNode;
        }
    }


    /// <summary>
    /// Gets or sets select path.
    /// </summary>
    public string SelectPath
    {
        get
        {
            return treeElem.SelectPath;
        }
        set
        {
            treeElem.SelectPath = value;
        }
    }


    /// <summary>
    /// Gets or sets expanded path.
    /// </summary>
    public string ExpandPath
    {
        get
        {
            return treeElem.ExpandPath;
        }
        set
        {
            treeElem.ExpandPath = value;
        }
    }


    /// <summary>
    /// Gets or sets selected item.
    /// </summary>
    public string SelectedItem
    {
        get
        {
            return treeElem.SelectedItem;
        }
        set
        {
            treeElem.SelectedItem = value;
        }
    }


    /// <summary>
    /// Code name of the Category.
    /// </summary>
    public string CategoryName
    {
        get
        {
            return mCategoryName;
        }
        set
        {
            mCategoryName = value;
            mRoot = null;
        }
    }


    /// <summary>
    /// Name of the javascript function which is called when specified tab (Category) is clicked. 
    /// Category code name is passed as parameter.
    /// </summary>
    public string JavaScriptHandler
    {
        get
        {
            return mJavaScriptHandler;
        }
        set
        {
            mJavaScriptHandler = value;
        }
    }


    /// <summary>
    /// Gets the value which indicates whether there is some tab displayed or not.
    /// </summary>
    public bool MenuEmpty
    {
        get
        {
            if ((treeElem.ProviderObject != null) && (treeElem.ProviderObject.RootNode != null))
            {
                return treeElem.ProviderObject.RootNode.HasChildNodes;
            }
            return false;
        }
    }


    /// <summary>
    /// Gets the value which indicates whether root node is clickable or not.
    /// </summary>
    public bool RootIsClickable
    {
        get
        {
            return (mRootIsClickable);
        }
        set
        {
            mRootIsClickable = value;
        }
    }


    /// <summary>
    /// Gets or sets maximal relative level displayed (depth of the tree to load).
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return mMaxRelativeLevel;
        }
        set
        {
            mMaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Id of the site. Used for generating JavaScriptHandler second argument.
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
    /// Category, which should be used as root category.
    /// </summary>
    public SettingsCategoryInfo RootCategory
    {
        get
        {
            if (mRoot == null)
            {
                // Get the info
                if (String.IsNullOrEmpty(CategoryName))
                {
                    // Get the root category
                    mRoot = SettingsCategoryInfoProvider.GetRootSettingsCategoryInfo();
                }
                else
                {
                    // Get the specified category
                    mRoot = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(CategoryName);
                }
            }

            return mRoot;
        }
        set
        {
            mRoot = value;
            if (mRoot != null)
            {
                mCategoryName = mRoot.CategoryName;
            }
        }
    }


    /// <summary>
    /// Indicates whether categories without displayable keys are to be shown. Default value is true;
    /// </summary>
    public bool ShowEmptyCategories
    {
        get
        {
            return mShowEmptyCategories;
        }
        set
        {
            mShowEmptyCategories = value;
        }
    }

    #endregion


    #region "Custom events"

    /// <summary>
    /// Node created delegate.
    /// </summary>
    public delegate TreeNode NodeCreatedEventHandler(SettingsCategoryInfo category, TreeNode defaultNode);


    /// <summary>
    /// Node created event handler.
    /// </summary>
    public event NodeCreatedEventHandler OnNodeCreated;

    #endregion


    #region "Public methods"

    /// <summary>
    /// Reloads tree data.
    /// </summary>
    public void ReloadData()
    {
        treeElem.ReloadData();
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register JQuery
        ScriptHelper.RegisterJQuery(Page);

        // Use images according to culture
        if (CultureHelper.IsUICultureRTL())
        {
            this.treeElem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            this.treeElem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }

        if (RootCategory != null)
        {
            string levelWhere = (MaxRelativeLevel <= 0 ? "" : " AND (CategoryLevel <= " + (RootCategory.CategoryLevel + MaxRelativeLevel) + ")");
            // Restrict CategoryChildCount to MaxRelativeLevel. If level < MaxRelativeLevel, use count of non-group childs.
            string levelColumn = "CASE CategoryLevel WHEN " + MaxRelativeLevel + " THEN 0 ELSE  (SELECT COUNT(*) AS CountNonGroup FROM CMS_SettingsCategory AS sc WHERE (sc.CategoryParentID = CMS_SettingsCategory.CategoryID) AND (sc.CategoryIsGroup = 0)) END AS CategoryChildCount";

            // Create and set category provider
            UniTreeProvider provider = new UniTreeProvider();
            provider.RootLevelOffset = RootCategory.CategoryLevel;
            provider.ObjectType = "CMS.SettingsCategory";
            provider.DisplayNameColumn = "CategoryDisplayName";
            provider.IDColumn = "CategoryID";
            provider.LevelColumn = "CategoryLevel";
            provider.OrderColumn = "CategoryOrder";
            provider.ParentIDColumn = "CategoryParentID";
            provider.PathColumn = "CategoryIDPath";
            provider.ValueColumn = "CategoryID";
            provider.ChildCountColumn = "CategoryChildCount";

            provider.WhereCondition = " ((CategoryIsGroup IS NULL) OR (CategoryIsGroup = 0)) " + levelWhere;
            if (!ShowEmptyCategories)
            {
                provider.WhereCondition = SqlHelperClass.AddWhereCondition(provider.WhereCondition, "CategoryID IN (SELECT CategoryParentID FROM CMS_SettingsCategory WHERE (CategoryIsGroup = 0) OR (CategoryIsGroup = 1 AND CategoryID IN (SELECT KeyCategoryID FROM CMS_SettingsKey WHERE ISNULL(SiteID, 0) = " + SiteID + ")))");
            }
            provider.Columns = "CategoryID, CategoryName, CategoryDisplayName, CategoryLevel, CategoryOrder, CategoryParentID, CategoryIDPath, CategoryIconPath, " + levelColumn;

            if (String.IsNullOrEmpty(JavaScriptHandler))
            {
                treeElem.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\" onclick=\"SelectNode('##NODECODENAME##');\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
                treeElem.NodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"SelectNode('##NODECODENAME##');\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
            }
            else
            {
                treeElem.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\" onclick=\"SelectNode('##NODECODENAME##'); if (" + JavaScriptHandler + ") { " + JavaScriptHandler + "('##NODECODENAME##',##NODEID##, ##SITEID##, ##PARENTID##); }\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
                treeElem.NodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"SelectNode('##NODECODENAME##'); if (" + JavaScriptHandler + ") { " + JavaScriptHandler + "('##NODECODENAME##',##NODEID##, ##SITEID##, ##PARENTID##); }\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
            }

            treeElem.UsePostBack = false;
            treeElem.ProviderObject = provider;
            treeElem.ExpandPath = RootCategory.CategoryIDPath;

            treeElem.OnGetImage += treeElem_OnGetImage;
            treeElem.OnNodeCreated += treeElem_OnNodeCreated;

            // Create root node
            string rootName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(String.IsNullOrEmpty(RootCategory.CategoryDisplayName) ? RootCategory.CategoryName : RootCategory.CategoryDisplayName));
            string rootIcon = "<img src=\"" + URLHelper.ResolveUrl(GetImageUrl("/General/DefaultRoot.png")) + "\" style=\"border:none;height:10px;width:1px;\" />";
            string rootText = "";
            if (!RootIsClickable)
            {
                rootText = treeElem.ReplaceMacros("<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>", RootCategory.CategoryID, RootCategory.CategoryChildCount, HTMLHelper.HTMLEncode(rootName), rootIcon, RootCategory.CategoryParentID, null, null);
            }
            else
            {
                rootText = treeElem.ReplaceMacros(treeElem.SelectedNodeTemplate, RootCategory.CategoryID, RootCategory.CategoryChildCount, HTMLHelper.HTMLEncode(rootName), rootIcon, RootCategory.CategoryParentID, null, null);
            }

            rootText = rootText.Replace("##NODECUSTOMNAME##", HTMLHelper.HTMLEncode(rootName));
            rootText = rootText.Replace("##NODECODENAME##", HTMLHelper.HTMLEncode(RootCategory.CategoryName));
            rootText = rootText.Replace("##SITEID##", mSiteId.ToString());
            rootText = rootText.Replace("##PARENTID##", RootCategory.CategoryParentID.ToString());


            treeElem.SetRoot(rootText, RootCategory.CategoryID.ToString(), ResolveUrl(RootCategory.CategoryIconPath), URLHelper.Url + "#", null);

            currentUser = CMSContext.CurrentUser;
        }

        if (!RequestHelper.IsPostBack())
        {
            treeElem.ReloadData();
        }
    }


    protected TreeNode treeElem_OnNodeCreated(DataRow itemData, TreeNode defaultNode)
    {
        defaultNode.Selected = false;
        if (itemData != null)
        {
            if (currentUser != null)
            {
                // Ensure name.
                string catName = ValidationHelper.GetString(itemData["CategoryName"], "");
                // Ensure caption.
                string caption = ValidationHelper.GetString(itemData["CategoryDisplayName"], "");

                int catParentId = ValidationHelper.GetInteger(itemData["CategoryParentID"], 0);

                if (String.IsNullOrEmpty(caption))
                {
                    caption = catName;
                }

                // Set caption
                defaultNode.Text = defaultNode.Text.Replace("##NODECUSTOMNAME##", HTMLHelper.HTMLEncode(ResHelper.LocalizeString(caption)));
                defaultNode.Text = defaultNode.Text.Replace("##NODECODENAME##", HTMLHelper.HTMLEncode(catName));
                defaultNode.Text = defaultNode.Text.Replace("##SITEID##", mSiteId.ToString());
                defaultNode.Text = defaultNode.Text.Replace("##PARENTID##", catParentId.ToString());

                if (OnNodeCreated != null)
                {
                    return OnNodeCreated(new SettingsCategoryInfo(itemData), defaultNode);
                }
                else
                {
                    return defaultNode;
                }
            }
        }

        return null;
    }


    /// <summary>
    /// Method for obtaining image url for given tree node (category).
    /// </summary>
    /// <param name="node">Node (category)</param>
    /// <returns>Image URL</returns>
    protected string treeElem_OnGetImage(UniTreeNode node)
    {
        File f = new File();
        DataRow dr = node.ItemData as DataRow;
        string imgUrl = string.Empty;

        try
        {
            if (dr != null)
            {
                // Get icon path
                imgUrl = ValidationHelper.GetString(dr["CategoryIconPath"], "");
                if (!String.IsNullOrEmpty(imgUrl))
                {
                    if (CMS.IO.Path.IsPathRooted(imgUrl))
                    {
                        imgUrl = GetImagePath("CMSModules/CMS_Settings/Categories/list.png");
                    }
                    else if (!ValidationHelper.IsURL(imgUrl))
                    {
                        imgUrl = GetImagePath(imgUrl);
                    }
                    else
                    {
                        return imgUrl;
                    }
                }
                else
                {
                    // If requested icon not found try to get icon as previous version did
                    string categoryName = ValidationHelper.GetString(dr["CategoryName"], "");
                    imgUrl = GetImagePath("CMSModules/CMS_Settings/Categories/" + categoryName.Replace(".", "_") + "/list.png");
                }
            }
            // Get default icon if requested icon not found
            if (!f.Exists(Server.MapPath(imgUrl)))
            {
                imgUrl = GetImagePath("CMSModules/CMS_Settings/Categories/list.png");
            }
        }
        catch
        {
            imgUrl = GetImagePath("CMSModules/CMS_Settings/Categories/list.png");
        }

        return URLHelper.ResolveUrl(imgUrl);
    }
}
