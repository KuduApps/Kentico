using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.PortalEngine;

public partial class CMSModules_PortalEngine_Controls_WebParts_WebPartTree : CMSAdminControl
{
    #region "Variables"

    private int mMaxTreeNodes = -1;
    private bool mUseMaxNodeLimit = true;
    bool mSelectWebParts = false;
    bool mShowRecentlyUsed = false;
    private bool mShowWidgetOnlyWebparts = false;
    private bool mUseGlobalSettings = false;

    /// <summary>
    /// Index used for item count under one node.
    /// </summary>
    private int indexMaxTreeNodes = -1;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether webpart of type "Widget only" will be displayed in selector.
    /// </summary>
    public bool ShowWidgetOnlyWebparts
    {
        get
        {
            return mShowWidgetOnlyWebparts;
        }
        set
        {
            mShowWidgetOnlyWebparts = value;
        }
    }


    /// <summary>
    /// Indicates whether use max node limit stored in settings.
    /// </summary>
    public bool UseMaxNodeLimit
    {
        get
        {
            return mUseMaxNodeLimit;
        }
        set
        {
            mUseMaxNodeLimit = value;
        }
    }


    /// <summary>
    /// If true, only settings are used.
    /// </summary>
    public bool UseGlobalSettings
    {
        get
        {
            return mUseGlobalSettings;
        }
        set
        {
            mUseGlobalSettings = value;
        }
    }


    /// <summary>
    /// Maximum tree nodes shown under parent node - this value can be ignored if UseMaxNodeLimit set to false.
    /// </summary>
    public int MaxTreeNodes
    {
        get
        {
            if (mMaxTreeNodes < 0)
            {
                mMaxTreeNodes = SettingsKeyProvider.GetIntValue((UseGlobalSettings ? "" : CMSContext.CurrentSiteName + ".") + "CMSMaxUITreeNodes");
            }
            return mMaxTreeNodes;
        }
        set
        {
            mMaxTreeNodes = value;
        }
    }

    /// <summary>
    /// Gets or sets whether webparts are shown in tree or not.
    /// </summary>
    public bool SelectWebParts
    {
        get
        {
            return this.mSelectWebParts;
        }
        set
        {
            this.mSelectWebParts = value;
        }
    }


    /// <summary>
    /// Gets or sets whether recently used link is shown or not.
    /// </summary>
    public bool ShowRecentlyUsed
    {
        get
        {
            return mShowRecentlyUsed;
        }
        set
        {
            mShowRecentlyUsed = value;
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
    /// Gets or sets if use postback.
    /// </summary>
    public bool UsePostBack
    {
        get
        {
            return treeElem.UsePostBack;
        }
        set
        {
            treeElem.UsePostBack = value;
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
            treeElem.ExpandPath = value;
        }
    }

    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            treeElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            treeElem.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Root path for the items in the tree.
    /// </summary>
    public virtual string RootPath
    {
        get;
        set;
    }

    #endregion


    #region "Custom events"

    /// <summary>
    /// On selected item event handler.
    /// </summary>    
    public delegate void ItemSelectedEventHandler(string selectedValue);

    /// <summary>
    /// On selected item event handler.
    /// </summary>
    public event ItemSelectedEventHandler OnItemSelected;

    #endregion


    #region "Page and other events"

    /// <summary>
    /// Page_Load event.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        // Create and set category provider
        UniTreeProvider categoryProvider = new UniTreeProvider();
        categoryProvider.DisplayNameColumn = "DisplayName";
        categoryProvider.IDColumn = "ObjectID";
        categoryProvider.LevelColumn = "ObjectLevel";
        categoryProvider.OrderColumn = "CategoryOrder";
        categoryProvider.ParentIDColumn = "ParentID";
        categoryProvider.PathColumn = "ObjectPath";
        categoryProvider.ValueColumn = "ObjectID";
        categoryProvider.ChildCountColumn = "CompleteChildCount";
        categoryProvider.QueryName = "cms.webpartcategory.selectallview";
        categoryProvider.ObjectTypeColumn = "ObjectType";
        categoryProvider.Columns = "DisplayName, ObjectID, ObjectLevel,CategoryOrder,ParentID, ObjectPath, CompleteChildCount,ObjectType,CategoryChildCount, CategoryImagePath";
        categoryProvider.ImageColumn = "CategoryImagePath";

        string where = null;

        if (!SelectWebParts)
        {
            // Select only categories
            where = "ObjectType = N'webpartcategory'";

            categoryProvider.ChildCountColumn = "CategoryChildCount";
            categoryProvider.ObjectTypeColumn = "";
            treeElem.DefaultImagePath = GetImageUrl("Objects/CMS_WebPartCategory/list.png");
        }
        else
        {
            categoryProvider.OrderBy = "ObjectType DESC, DisplayName ASC";
            treeElem.OnGetImage += new CMSAdminControls_UI_Trees_UniTree.GetImageEventHandler(treeElem_OnGetImage);
        }

        if (!ShowWidgetOnlyWebparts)
        {
            // Hide categories with "widget only" web parts
            where = SqlHelperClass.AddWhereCondition(where, "ObjectPath = '/' OR (SELECT Count(*) FROM View_CMS_WebPartCategoryWebpart_Joined AS X WHERE X.ObjectType = N'webpart' AND (X.WebPartType IS NULL OR X.WebPartType != " + Convert.ToInt32(WebPartTypeEnum.WidgetOnly) + ") AND X.ObjectPath LIKE View_CMS_WebPartCategoryWebpart_Joined.ObjectPath + '/%') > 0");
        }

        // Add custom where condition
        if (!string.IsNullOrEmpty(RootPath))
        {
            where = SqlHelperClass.AddWhereCondition(where, "ObjectPath = '" + SqlHelperClass.GetSafeQueryString(RootPath, false) + "' OR ObjectPath LIKE '" + SqlHelperClass.GetSafeQueryString(RootPath, false) + "/%'");
            categoryProvider.RootLevelOffset = RootPath.Split('/').Length - 1;

            treeElem.ExpandPath = RootPath + "/";
        }

        categoryProvider.WhereCondition = where;

        // Set up tree 
        treeElem.ProviderObject = categoryProvider;

        if (SelectWebParts)
        {
            treeElem.NodeTemplate = "<span id=\"##OBJECTTYPE##_##NODEID##\" onclick=\"SelectNode(##NODEID##,'##OBJECTTYPE##', ##PARENTNODEID##);\" name=\"treeNode\" class=\"ContentTreeItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";
            treeElem.SelectedNodeTemplate = "<span id=\"##OBJECTTYPE##_##NODEID##\" onclick=\"SelectNode(##NODEID##,'##OBJECTTYPE##', ##PARENTNODEID##);\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";
        }
        else
        {
            treeElem.NodeTemplate = "<span onclick=\"SelectNode(##NODEID##, this);\" class=\"ContentTreeItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";
            treeElem.DefaultItemTemplate = "<span onclick=\"SelectNode('recentlyused', this);\" class=\"ContentTreeItem\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
            treeElem.SelectedDefaultItemTemplate = "<span onclick=\"SelectNode('recentlyused', this);\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
            treeElem.SelectedNodeTemplate = "<span onclick=\"SelectNode(##NODEID##, this);\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";

            // Register jquery
            ScriptHelper.RegisterJQuery(this.Page);

            string js = "var selectedItem = $j('.ContentTreeSelectedItem');" +
                "function SelectNode(nodeid, sender){" +
                "selectedItem.removeClass('ContentTreeSelectedItem'); " +
                "selectedItem.addClass('ContentTreeItem');" +
                "selectedItem = $j(sender);" +
                "selectedItem.removeClass('ContentTreeItem'); " +
                "selectedItem.addClass('ContentTreeSelectedItem'); " +
                "document.getElementById('" + this.treeElem.SelectedItemFieldId + "').value = nodeid;" +
                treeElem.GetOnSelectedItemBackEventReference() +
                "}";

            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "SelectTreeNode", ScriptHelper.GetScript(js));
        }

        // Add last recently used
        if (ShowRecentlyUsed)
        {
            treeElem.AddDefaultItem(GetString("webparts.recentlyused"), "recentlyused", ResolveUrl(GetImageUrl("Objects/CMS_WebPartCategory/recentlyused.png")));
        }

        // Setup event handler
        treeElem.OnItemSelected += new CMSAdminControls_UI_Trees_UniTree.ItemSelectedEventHandler(treeElem_OnItemSelected);
        treeElem.OnNodeCreated += new CMSAdminControls_UI_Trees_UniTree.NodeCreatedEventHandler(treeElem_OnNodeCreated);
    }


    /// <summary>
    /// Page PreRender.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        // Load data
        if (!RequestHelper.IsPostBack())
        {
            treeElem.ReloadData();
        }
    }


    /// <summary>
    /// Used for maxnodes in collapsed node.
    /// </summary>
    /// <param name="itemData">The data row to check</param>
    /// <param name="defaultNode">The defaul node</param>
    protected TreeNode treeElem_OnNodeCreated(DataRow itemData, TreeNode defaultNode)
    {
        if (UseMaxNodeLimit)
        {
            // Get parentID from data row
            int parentID = ValidationHelper.GetInteger(itemData["ParentID"], 0);
            string objectType = ValidationHelper.GetString(itemData["ObjectType"], String.Empty);

            // Don't use maxnodes limitation for categories
            if (objectType.ToLower() == "webpartcategory")
            {
                return defaultNode;
            }

            // Increment index count in collapsing
            indexMaxTreeNodes++;
            if (indexMaxTreeNodes == MaxTreeNodes)
            {
                // Load parentid
                int parentParentID = 0;

                WebPartCategoryInfo category = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(parentID);
                if (category != null)
                {
                    parentParentID = category.CategoryParentID;
                }

                System.Web.UI.WebControls.TreeNode node = new System.Web.UI.WebControls.TreeNode();
                node.Text = "<span class=\"ContentTreeItem\" onclick=\"SelectNode(" + parentID + " ,'webpartcategory'," + parentParentID + ",true ); return false;\"><span class=\"Name\" style=\"font-style: italic;\">" + GetString("general.seelisting") + "</span></span>";
                return node;
            }
            if (indexMaxTreeNodes > MaxTreeNodes)
            {
                return null;
            }
        }
        return defaultNode;
    }


    /// <summary>
    /// On selected item event.
    /// </summary>
    /// <param name="selectedValue">Selected value</param>
    protected void treeElem_OnItemSelected(string selectedValue)
    {
        if (OnItemSelected != null)
        {
            OnItemSelected(selectedValue);
        }
    }


    /// <summary>
    /// On get image event.
    /// </summary>
    /// <param name="node">Current node</param>
    string treeElem_OnGetImage(UniTreeNode node)
    {
        if ((node != null) && (node.ItemData != null))
        {
            string objectType = string.Empty;

            DataRow dr = (DataRow)node.ItemData;
            if (dr != null)
            {
                objectType = ValidationHelper.GetString(dr["ObjectType"], "").ToLower();
            }

            // Return image path
            if (objectType == "webpart")
            {
                return GetImageUrl("Objects/CMS_WebPart/tree.png");
            }
            else if (objectType == "webpartcategory")
            {
                return GetImageUrl("Objects/CMS_WebPartCategory/list.png");
            }
        }
        return String.Empty;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads the tree data.
    /// </summary>
    public override void ReloadData()
    {
        treeElem.ReloadData();
        base.ReloadData();
    }

    #endregion
}
