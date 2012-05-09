using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.PortalEngine;
using CMS.WorkflowEngine;
using CMS.TreeEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_PortalEngine_Controls_Layout_PageTemplateTree : CMSAdminControl
{
    #region "Variables"

    private int mMaxTreeNodes = -1;
    private bool mUseMaxNodeLimit = true;
    private bool mSelectPageTemplates = false;
    private bool mShowAdHocCategory = true;
    private bool mShowEmptyCategories = true;
    private int mDocumentID = 0;
    private bool mIsNewPage = false;
    private bool mShowOnlySiteTemplates = true;
    private bool mUseGlobalSettings = false;

    /// <summary>
    /// Index used for item count under one node.
    /// </summary>
    private int indexMaxTreeNodes = -1;

    #endregion


    #region "Public properties"

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
    /// If true, only global settings are used.
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
    /// Gets or sets whether page templates are shown in tree or not.
    /// </summary>
    public bool SelectPageTemplates
    {
        get
        {
            return this.mSelectPageTemplates;
        }
        set
        {
            this.mSelectPageTemplates = value;
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
    /// Shows or hides AdHoc category in tree.
    /// </summary>
    public bool ShowAdHocCategory
    {
        get
        {
            return mShowAdHocCategory;
        }
        set
        {
            mShowAdHocCategory = value;
        }
    }


    /// <summary>
    /// Shows or hides empty categories in tree.
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


    /// <summary>
    /// Gets or sets a value indicating whether to show site page templates only.
    /// </summary>
    public bool ShowOnlySiteTemplates
    {
        get
        {
            return mShowOnlySiteTemplates;
        }
        set
        {
            mShowOnlySiteTemplates = value;
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
    /// Gets or sets document id.
    /// </summary>
    public int DocumentID
    {
        get
        {
            return mDocumentID;
        }
        set
        {
            mDocumentID = value;
        }
    }


    /// <summary>
    /// Whether selecting new page.
    /// </summary>
    public bool IsNewPage
    {
        get
        {
            return mIsNewPage;
        }
        set
        {
            mIsNewPage = value;
        }
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
        categoryProvider.QueryName = "cms.pagetemplatecategory.selectallview";
        categoryProvider.ObjectTypeColumn = "ObjectType";
        categoryProvider.Columns = "DisplayName, CodeName, ObjectID, ObjectLevel, CategoryOrder, ParentID, ObjectPath, CompleteChildCount, ObjectType, CategoryChildCount, CategoryImagePath, Parameter";
        categoryProvider.ImageColumn = "CategoryImagePath";
        categoryProvider.ParameterColumn = "Parameter";

        if (!SelectPageTemplates)
        {
            categoryProvider.WhereCondition = "ObjectType = 'pagetemplatecategory'";
            categoryProvider.ChildCountColumn = "CategoryChildCount";
            categoryProvider.ObjectTypeColumn = "";
            treeElem.DefaultImagePath = GetImageUrl("Objects/CMS_PageTemplateCategory/list.png");
        }
        else
        {
            categoryProvider.OrderBy = "ObjectType DESC, DisplayName ASC";
            treeElem.OnGetImage += new CMSAdminControls_UI_Trees_UniTree.GetImageEventHandler(treeElem_OnGetImage);
        }

        // Do not show AdHoc category
        if (!this.ShowAdHocCategory)
        {
            categoryProvider.WhereCondition = SqlHelperClass.AddWhereCondition(categoryProvider.WhereCondition, "CodeName <> 'AdHoc'");
        }

        // Do not show empty categories
        if (!this.ShowEmptyCategories)
        {
            categoryProvider.WhereCondition = SqlHelperClass.AddWhereCondition(categoryProvider.WhereCondition, "CategoryTemplateChildCount > 0 OR CategoryChildCount > 0");

            TreeProvider tp = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = DocumentHelper.GetDocument(DocumentID, tp);
            string culture = CMSContext.PreferredCultureCode;
            int level = 0;
            string path = string.Empty;

            if (node != null)
            {
                level = node.NodeLevel;
                path = node.NodeAliasPath;
                if (IsNewPage)
                {
                    level++;
                    path = path + "/%";
                }
                else
                {
                    culture = node.DocumentCulture;
                }
            }

            // Add where condition for scopes
            categoryProvider.WhereCondition += " AND (ObjectLevel = 0 OR (SELECT TOP 1 ObjectID FROM View_CMS_PageTemplateCategoryPageTemplate_Joined AS X WHERE X.ObjectType = 'pagetemplate' ";

            categoryProvider.WhereCondition += " AND (X.PageTemplateType IS NULL OR X.PageTemplateType <> N'" + PageTemplateInfoProvider.GetPageTemplateTypeCode(PageTemplateTypeEnum.Dashboard) + "')";


            if (ShowOnlySiteTemplates)
            {
                categoryProvider.WhereCondition += " AND X.ObjectID IN (SELECT PageTemplateID FROM CMS_PageTemplateSite WHERE SiteID = " + CMSContext.CurrentSiteID + ") ";
            }

            if (node != null)
            {
                categoryProvider.WhereCondition += " AND (" + PageTemplateScopeInfoProvider.GetScopeWhereCondition(path, culture, node.NodeClassName, level, CMSContext.CurrentSiteName, "X", "ObjectID") + ") ";
            }

            categoryProvider.WhereCondition += " AND (X.ObjectPath LIKE View_CMS_PageTemplateCategoryPageTemplate_Joined.ObjectPath + '/%')) IS NOT NULL)";

            // Add column count column - minimal number of childs
            categoryProvider.Columns += @", (SELECT TOP 1 Count(*) FROM View_CMS_PageTemplateCategoryPageTemplate_Joined AS Y WHERE 
            (Y.ObjectID = View_CMS_PageTemplateCategoryPageTemplate_Joined.ObjectID AND Y.ObjectLevel = 0)
            OR ( View_CMS_PageTemplateCategoryPageTemplate_Joined.ObjectType = 'PageTemplateCategory' 
            AND View_CMS_PageTemplateCategoryPageTemplate_Joined.CategoryChildCount > 0 
            AND Y.ObjectType = 'PageTemplate' AND Y.ObjectLevel > View_CMS_PageTemplateCategoryPageTemplate_Joined.ObjectLevel + 1 ";

            if (ShowOnlySiteTemplates)
            {
                categoryProvider.Columns += "AND Y.ObjectID IN (SELECT PageTemplateID FROM CMS_PageTemplateSite WHERE SiteID = " + CMSContext.CurrentSiteID + ") ";
            }

            if (node != null)
            {
                categoryProvider.Columns += " AND ( " + PageTemplateScopeInfoProvider.GetScopeWhereCondition(path, culture, node.NodeClassName, level, CMSContext.CurrentSiteName, "Y", "ObjectID") + " ) ";
            }

            categoryProvider.Columns += " AND Y.ObjectPath LIKE  View_CMS_PageTemplateCategoryPageTemplate_Joined.ObjectPath + '/%')) AS MinNumberOfChilds";
            categoryProvider.ChildCountColumn = "MinNumberOfChilds";
        }

        // Set up tree 
        treeElem.ProviderObject = categoryProvider;

        if (SelectPageTemplates)
        {
            treeElem.NodeTemplate = "<span id=\"##OBJECTTYPE##_##NODEID##\" onclick=\"SelectNode(##NODEID##,'##OBJECTTYPE##', ##PARENTNODEID##, '##PARAMETER##');\" name=\"treeNode\" class=\"ContentTreeItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";
            treeElem.SelectedNodeTemplate = "<span id=\"##OBJECTTYPE##_##NODEID##\" onclick=\"SelectNode(##NODEID##,'##OBJECTTYPE##', ##PARENTNODEID##, '##PARAMETER##');\" name=\"treeNode\"  class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";
        }
        else
        {
            treeElem.NodeTemplate = "<span onclick=\"SelectNode(##NODEID##, this);\" class=\"ContentTreeItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";
            treeElem.DefaultItemTemplate = "<span onclick=\"SelectNode('recentlyused', this);\" class=\"ContentTreeItem\">##ICON##<span class=\"Name\">##NODENAME##</span></span><div style=\"clear:both\"></div>";
            treeElem.SelectedDefaultItemTemplate = "<span onclick=\"SelectNode('recentlyused', this);\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON##<span class=\"Name\">##NODENAME##</span></span><div style=\"clear:both\"></div>";
            treeElem.SelectedNodeTemplate = "<span onclick=\"SelectNode(##NODEID##, this);\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON## <span class=\"Name\">##NODENAME##</span></span>";

            // Register jquery
            ScriptHelper.RegisterJQuery(this.Page);

            string js =
                "function SelectNode(nodeid, sender){ " +
                "var selectedItem = $j('.ContentTreeSelectedItem'); " +
                "selectedItem.removeClass('ContentTreeSelectedItem'); " +
                "selectedItem.addClass('ContentTreeItem');" +
                "selectedItem = $j(sender);" +
                "selectedItem.removeClass('ContentTreeItem'); " +
                "selectedItem.addClass('ContentTreeSelectedItem'); " +
                "document.getElementById('" + this.treeElem.SelectedItemFieldId + "').value = nodeid;" +
                treeElem.GetOnSelectedItemBackEventReference() +
                "}";

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectTreeNode", ScriptHelper.GetScript(js));
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
    /// <param name="itemData">The item data</param>
    /// <param name="defaultNode">The default node</param>
    protected System.Web.UI.WebControls.TreeNode treeElem_OnNodeCreated(DataRow itemData, System.Web.UI.WebControls.TreeNode defaultNode)
    {
        if (UseMaxNodeLimit)
        {
            // Get parentID from data row
            int parentID = ValidationHelper.GetInteger(itemData["ParentID"], 0);
            string objectType = ValidationHelper.GetString(itemData["ObjectType"], String.Empty);

            // Don't use maxnodes limitation for categories
            if (objectType.ToLower() == "pagetemplatecategory")
            {
                return defaultNode;
            }

            // Increment index count in collapsing
            indexMaxTreeNodes++;
            if (indexMaxTreeNodes == MaxTreeNodes)
            {
                // Load parentid
                int parentParentID = 0;
                PageTemplateCategoryInfo parentParent = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(parentID);
                if (parentParent != null)
                {
                    parentParentID = parentParent.ParentId;
                }

                System.Web.UI.WebControls.TreeNode node = new System.Web.UI.WebControls.TreeNode();
                node.Text = "<span class=\"ContentTreeItem\" onclick=\"SelectNode(" + parentID + " ,'pagetemplatecategory'," + parentParentID + ",true ); return false;\"><span class=\"Name\" style=\"font-style: italic;\">" + GetString("general.seelisting") + "</span></span>";
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
    ///  On selected item event.
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
    protected string treeElem_OnGetImage(UniTreeNode node)
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
            if (objectType == "pagetemplate")
            {
                // Set special icon for ad-hoc page template
                if (!ValidationHelper.GetBoolean(dr["Parameter"], true))
                {
                    return GetImageUrl("Objects/CMS_PageTemplate/adhoc.png");
                }

                return GetImageUrl("Objects/CMS_PageTemplate/tree.png");
            }
            else if (objectType == "pagetemplatecategory")
            {
                return GetImageUrl("Objects/CMS_PageTemplateCategory/list.png");
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
