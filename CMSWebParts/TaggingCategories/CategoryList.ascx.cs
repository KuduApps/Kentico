using System;
using System.Data;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using CMS.CMSHelper;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.SettingsProvider;

public partial class CMSWebParts_TaggingCategories_CategoryList : CMSAbstractWebPart
{
    #region "Private variables"

    private bool mUseCompleteWhere = false;
    private bool? mAllowGlobalCategories = null;
    private CategoryInfo mStartingCategoryObj = null;
    private DataSet data = null;

    private CategoryNode generalRoot = null;
    private CategoryNode personalRoot = null;
    protected ContextResolver currentResolver = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Document list url.
    /// </summary>
    public string DocumentListUrl
    {
        get
        {
            return ValidationHelper.GetString(GetValue("DocumentListUrl"), "");
        }
        set
        {
            SetValue("DocumentListUrl", value);
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(GetValue("TransformationName"), "");
        }
        set
        {
            SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return ValidationHelper.GetString(GetValue("WhereCondition"), "");
        }
        set
        {
            SetValue("WhereCondition", value);
        }
    }


    /// <summary>
    /// Gets or sets the order by clause.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(GetValue("OrderBy"), "");
        }
        set
        {
            SetValue("OrderBy", value);
        }
    }


    /// <summary>
    /// Gets or sets the TOP N value.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("SelectTopN"), 0);
        }
        set
        {
            SetValue("SelectTopN", value);
        }
    }


    /// <summary>
    ///  Gets or sets the value that indicates whether control is not visible for empty datasource.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("HideControlForZeroRows"), true);
        }
        set
        {
            SetValue("HideControlForZeroRows", value);
        }
    }


    /// <summary>
    /// Gets or sets the text value which is displayed for zero rows result.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ZeroRowsText"), "");
        }
        set
        {
            SetValue("ZeroRowsText", value);
        }
    }


    /// <summary>
    /// Gets or sets if the global categories should be displayed.
    /// </summary>
    public bool DisplayGlobalCategories
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayGlobalCategories"), true);
        }
        set
        {
            SetValue("DisplayGlobalCategories", value);
        }
    }


    /// <summary>
    /// Display Site Categories
    /// </summary>
    public bool DisplaySiteCategories
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplaySiteCategories"), true);
        }
        set
        {
            this.SetValue("DisplaySiteCategories", value);
        }
    }


    /// <summary>
    /// Gets or sets if the user categories should be displayed.
    /// </summary>
    public bool DisplayCustomCategories
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayCustomCategories"), true);
        }
        set
        {
            SetValue("DisplayCustomCategories", value);
        }
    }


    /// <summary>
    /// Gets or sets the alias path.
    /// </summary>
    public string AliasPath
    {
        get
        {
            string path = ValidationHelper.GetString(GetValue("AliasPath"), "");

            if (CMSContext.CurrentDocument != null)
            {
                if (path == "")
                {
                    path = CMSContext.CurrentDocument.NodeAliasPath.TrimEnd('/') + "/%";
                }

                path = ((path == ".") && (CMSContext.CurrentDocument != null)) ? CMSContext.CurrentDocument.NodeAliasPath : path;
            }

            return path;
        }
        set
        {
            SetValue("AliasPath", value);
        }
    }


    /// <summary>
    /// Gets or sets the culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return ValidationHelper.GetString(GetValue("CultureCode"), "");
        }
        set
        {
            SetValue("CultureCode", value);
        }
    }


    /// <summary>
    /// Gets or sets combine with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CombineWithDefaultCulture"), true);
        }
        set
        {
            SetValue("CombineWithDefaultCulture", value);
        }
    }


    /// <summary>
    /// Gets or sets select only published.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SelectOnlyPublished"), true);
        }
        set
        {
            SetValue("SelectOnlyPublished", value);
        }
    }


    /// <summary>
    /// Gets or sets max relative level.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(GetValue("MaxRelativeLevel"), -1);
        }
        set
        {
            SetValue("MaxRelativeLevel", value);
        }
    }


    /// <summary>
    /// Categories root.
    /// </summary>
    public string CategoriesRoot
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CategoriesRoot"), "");
        }
        set
        {
            this.SetValue("CategoriesRoot", value);
        }
    }


    /// <summary>
    /// Personal categories root.
    /// </summary>
    public string PersonalCategoriesRoot
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PersonalCategoriesRoot"), "");
        }
        set
        {
            this.SetValue("PersonalCategoriesRoot", value);
        }
    }


    /// <summary>
    /// Starting category.
    /// </summary>
    public string StartingCategory
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("StartingCategory"), "");
        }
        set
        {
            this.SetValue("StartingCategory", value);
        }
    }


    /// <summary>
    /// Use document filter.
    /// </summary>
    public bool UseDocumentFilter
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseDocumentFilter"), true);
        }
        set
        {
            this.SetValue("UseDocumentFilter", value);
        }
    }


    /// <summary>
    /// Category separator.
    /// </summary>
    public string CategorySeparator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CategorySeparator"), "/");
        }
        set
        {
            this.SetValue("CategorySeparator", value);
        }
    }


    /// <summary>
    /// Category separator RTL.
    /// </summary>
    public string CategorySeparatorRTL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CategorySeparatorRTL"), "\\");
        }
        set
        {
            this.SetValue("CategorySeparatorRTL", value);
        }
    }


    /// <summary>
    /// Document list target.
    /// </summary>
    public string DocumentListTarget
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DocumentListTarget"), "");
        }
        set
        {
            this.SetValue("DocumentListTarget", value);
        }
    }


    /// <summary>
    /// Render link title.
    /// </summary>
    public bool RenderLinkTitle
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderLinkTitle"), false);
        }
        set
        {
            this.SetValue("RenderLinkTitle", value);
        }
    }


    /// <summary>
    /// Render as tree.
    /// </summary>
    public bool RenderAsTree
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderAsTree"), false);
        }
        set
        {
            this.SetValue("RenderAsTree", value);
        }
    }


    /// <summary>
    /// Category content before.
    /// </summary>
    public string CategoryContentBefore
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CategoryContentBefore"), "");
        }
        set
        {
            this.SetValue("CategoryContentBefore", value);
        }
    }


    /// <summary>
    /// Category content after.
    /// </summary>
    public string CategoryContentAfter
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CategoryContentAfter"), "");
        }
        set
        {
            this.SetValue("CategoryContentAfter", value);
        }
    }


    /// <summary>
    /// Indicates if global categories are allowed for selected site in settings.
    /// </summary>
    private bool AllowGlobalCategories
    {
        get
        {
            if (!mAllowGlobalCategories.HasValue)
            {
                mAllowGlobalCategories = SettingsKeyProvider.GetBoolValue(SettingsKeyProvider.GetFullKeyName(CMSContext.CurrentSiteName, "CMSAllowGlobalCategories"));
            }

            return mAllowGlobalCategories ?? false;
        }
    }


    /// <summary>
    /// Starting category info object.
    /// </summary>
    private CategoryInfo StartingCategoryObj
    {
        get
        {
            if ((mStartingCategoryObj == null) && !string.IsNullOrEmpty(StartingCategory))
            {
                mStartingCategoryObj = CategoryInfoProvider.GetCategoryInfo(StartingCategory, CMSContext.CurrentSiteName);
                if (mStartingCategoryObj != null)
                {
                    if (mStartingCategoryObj.CategoryIsPersonal ||
                        (!mStartingCategoryObj.CategoryIsGlobal && !DisplaySiteCategories) ||
                        (mStartingCategoryObj.CategoryIsGlobal && !DisplayGlobalCategories))
                    {
                        mStartingCategoryObj = null;
                    }
                }
            }

            return mStartingCategoryObj;
        }
    }

    #endregion


    #region "Overridden methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        rptCategoryList.ReloadData(true);
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if ((HideControlForZeroRows) && ((DataHelper.DataSourceIsEmpty(data)) || (lblInfo.Visible)))
        {
            Visible = false;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        // Handle stop processing
        if (!this.StopProcessing)
        {
            // If displaying something
            if ((DisplayGlobalCategories) || (DisplayCustomCategories) || (DisplaySiteCategories))
            {
                // Get data set
                data = GetDataSet();

                if (RenderAsTree)
                {
                    CreateCategoryTrees(data);
                    StringBuilder sb = new StringBuilder();

                    // Render trees
                    sb.Append("<ul style=\"margin:0;\" class=\"CategoryListList\">");
                    RenderTree(sb, generalRoot);
                    RenderTree(sb, personalRoot);
                    sb.Append("</ul>");

                    ltlList.Text = sb.ToString();

                    rptCategoryList.Visible = false;
                }
                else
                {
                    rptCategoryList.ItemDataBound += rptCategoryList_ItemDataBound;
                    rptCategoryList.DataSource = data;

                    if (String.IsNullOrEmpty(TransformationName))
                    {
                        rptCategoryList.ItemTemplate = CMSDataProperties.LoadTransformation(this, "[]", false);
                    }
                    else
                    {
                        rptCategoryList.ItemTemplate = CMSDataProperties.LoadTransformation(this, TransformationName, false);
                    }
                    rptCategoryList.HideControlForZeroRows = HideControlForZeroRows;
                    rptCategoryList.ZeroRowsText = ZeroRowsText;
                    rptCategoryList.DataBindByDefault = false;
                    rptCategoryList.DataBind();

                    ltlList.Visible = false;
                }
            }
            // Else show zero rows text
            else
            {
                lblInfo.Text = ZeroRowsText;
                lblInfo.Visible = true;
            }
        }
    }


    /// <summary>
    /// Item databound handler.
    /// </summary>
    protected void rptCategoryList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // Create category entries as text to the placeholder control in repeater transformation
        if (e.Item.Controls.Count > 0)
        {
            // Get processed category paths
            string idPath = ((DataRowView)(e.Item.DataItem)).Row["CategoryIDPath"].ToString();
            string namePath = ((DataRowView)(e.Item.DataItem)).Row["CategoryNamePath"].ToString();
            int userId = ValidationHelper.GetInteger(((DataRowView)(e.Item.DataItem)).Row["CategoryUserID"], 0);

            // Handle custom starting category
            if (!string.IsNullOrEmpty(StartingCategory) && (StartingCategoryObj != null) && (namePath.StartsWith(StartingCategoryObj.CategoryNamePath)))
            {
                // Shorten paths
                namePath = namePath.Substring(StartingCategoryObj.CategoryNamePath.Length);
                idPath = idPath.Replace(StartingCategoryObj.CategoryIDPath, "");
            }

            // Prepare category entry text
            string rootText = (userId > 0) ? PersonalCategoriesRoot : CategoriesRoot;
            string text = CreateCategoryLine(idPath, namePath, rootText);

            // Try find placeholder control with id 'plcCategoryList'
            PlaceHolder plcCategoryList = e.Item.Controls[0].FindControl("plcCategoryList") as PlaceHolder;

            if (plcCategoryList != null)
            {
                // Append it to placeholder
                Literal ltlCategory = new Literal();
                ltlCategory.Text = text;
                plcCategoryList.Controls.Add(ltlCategory);
            }
            else
            {
                LiteralControl ltl = e.Item.Controls[0].Controls[0] as LiteralControl;
                if(ltl != null)
                {
                    ltl.Text += text + "<br />";
                }
            }
        }
    }


    /// <summary>
    /// Returns dataset with categories.
    /// </summary>
    protected DataSet GetDataSet()
    {
        DataSet ds = null;

        // Prepare user id
        int userId = 0;
        if ((CMSContext.CurrentUser != null) && (CMSContext.CurrentUser.UserID > 0))
        {
            userId = CMSContext.CurrentUser.UserID;
        }

        // Try to get data from cache
        using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref ds, this.CacheMinutes, true, this.CacheItemName, "categorylist", CMSContext.CurrentSiteName, DisplayGlobalCategories, AllowGlobalCategories, DisplaySiteCategories, DisplayCustomCategories, userId, UseDocumentFilter, AliasPath, CultureCode, MaxRelativeLevel, CombineWithDefaultCulture, SelectOnlyPublished, WhereCondition, StartingCategory))
        {
            if (cs.LoadData)
            {
                string where = GetCompleteWhereCondition();
                if (mUseCompleteWhere)
                {
                    ds = CategoryInfoProvider.GetDocumentCategories(where, OrderBy, SelectTopN);
                }
                else
                {
                    ds = CategoryInfoProvider.GetCategories(where, OrderBy, SelectTopN, null);
                }

                // Save the result to the cache
                if (cs.Cached)
                {
                    cs.CacheDependency = GetCacheDependency();
                    cs.Data = ds;
                }
            }
        }

        return ds;
    }


    /// <summary>
    /// Creates html entry for one category.
    /// </summary>
    /// <param name="categoryIdPath">ID path of rendered category.</param>
    /// <param name="categoryNamePath">Name path of rendered category</param>
    /// <param name="rootText">Text to be used as </param>
    protected string CreateCategoryLine(string categoryIdPath, string categoryNamePath, string rootText)
    {
        // Split paths
        string[] idSplits = categoryIdPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        string[] nameSplits = categoryNamePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

        // Need rtl rendering
        bool jrtl = CultureHelper.IsPreferredCultureRTL();

        StringBuilder line = new StringBuilder();

        int count = idSplits.Length;
        if ((count <= nameSplits.Length) && (count > 0))
        {
            // Convert string to ints
            int[] intIdSplits = ValidationHelper.GetIntegers(idSplits, -1);

            if (!jrtl)
            {
                // Append root if any
                if (!string.IsNullOrEmpty(rootText))
                {
                    line.Append(FormatCategoryDisplayName(rootText));
                }

                // Append whole category path
                for (int i = 0; i < count; i++)
                {
                    // Do not append the separator at the beginning
                    if (line.Length > 0)
                    {
                        line.Append(CategorySeparator);
                    }

                    // Append display name part
                    line.Append(CreateCategoryPartLink(nameSplits[i], intIdSplits[i]));
                }
            }
            else
            {
                // Append whole category path
                for (int i = count - 1; i >= 0; i--)
                {
                    // Do not append the separator at the beginning
                    if (line.Length > 0)
                    {
                        line.Append(CategorySeparatorRTL);
                    }

                    // Append display name part
                    line.Append(CreateCategoryPartLink(nameSplits[i], intIdSplits[i]));
                }

                // Append root if any
                if (!string.IsNullOrEmpty(rootText))
                {
                    line.Append(CategorySeparatorRTL);
                    line.Append(FormatCategoryDisplayName(rootText));
                }
            }
        }

        return line.ToString();
    }


    /// <summary>
    /// Creates HTML code for category link.
    /// </summary>
    /// <param name="categoryDisplayName">Category display name.</param>
    /// <param name="categoryId">ID of the category.</param>
    protected string CreateCategoryPartLink(string categoryDisplayName, int categoryId)
    {
        // Get target url
        string url = (String.IsNullOrEmpty(DocumentListUrl) ? URLHelper.CurrentURL : CMSContext.GetUrl(DocumentListUrl));

        // Append category parameter
        url = URLHelper.AddParameterToUrl(url, "categoryid", categoryId.ToString());

        StringBuilder attrs = new StringBuilder();

        // Append target attribute
        if (!string.IsNullOrEmpty(DocumentListTarget))
        {
            attrs.Append(" target=\"").Append(DocumentListTarget).Append("\"");
        }

        // Append title attribute
        if (RenderLinkTitle)
        {
            attrs.Append(" title=\"").Append(HTMLHelper.HTMLEncode(categoryDisplayName)).Append("\"");
        }

        return string.Format("<a href=\"{0}\"{1}>{2}</a>", url, attrs, FormatCategoryDisplayName(categoryDisplayName));
    }


    /// <summary>
    /// Formats category display name. Applies CategoryContentBefore and CategoryContentAfter.
    /// </summary>
    /// <param name="categoryDisplayName">Category display name.</param>
    protected string FormatCategoryDisplayName(string categoryDisplayName)
    {
        // Localize category display name
        categoryDisplayName = ResHelper.LocalizeString(categoryDisplayName, CultureCode);

        // Encode category display name
        categoryDisplayName = HTMLHelper.HTMLEncode(categoryDisplayName);

        // Format name
        return string.Format("{0}{1}{2}", CategoryContentBefore, categoryDisplayName, CategoryContentAfter);
    }


    /// <summary>
    /// Returns complete WHERE condition.
    /// </summary>
    protected string GetCompleteWhereCondition()
    {
        string where = "";
        string comleteWhere = "";

        // Global where condition
        if (DisplayGlobalCategories && AllowGlobalCategories)
        {
            where = " ((CategoryUserID IS NULL) AND (CategorySiteID IS NULL)) ";
        }

        // Site where condition
        if (DisplaySiteCategories)
        {
            where = SqlHelperClass.AddWhereCondition(where, "CategorySiteID = " + CMSContext.CurrentSiteID, "OR");
        }

        // User where condition
        if (DisplayCustomCategories)
        {
            if ((CMSContext.CurrentUser != null) && (CMSContext.CurrentUser.UserID > 0))
            {
                where = SqlHelperClass.AddWhereCondition(where, "CategoryUserID = " + CMSContext.CurrentUser.UserID, "OR");
            }
        }

        // Nothing to display
        if (string.IsNullOrEmpty(where))
        {
            where = "(1=0)";
        }

        // Get complete where condition
        if (UseDocumentFilter && (!String.IsNullOrEmpty(AliasPath) || !String.IsNullOrEmpty(CultureCode) || (MaxRelativeLevel > -1)))
        {
            comleteWhere = TreeProvider.GetCompleteWhereCondition(CMSContext.CurrentSiteName, AliasPath, CultureCode, CombineWithDefaultCulture, null, SelectOnlyPublished, MaxRelativeLevel);
            comleteWhere += "))";
            mUseCompleteWhere = true;

            // Add complete where condition
            where = SqlHelperClass.AddWhereCondition(where, comleteWhere);
        }

        // Add custom where condition if specified
        where = SqlHelperClass.AddWhereCondition(where, WhereCondition);

        // Display only enabled categories under enabled predecesors
        where = SqlHelperClass.AddWhereCondition(where, "CategoryEnabled = 1 AND (NOT EXISTS(SELECT CategoryID FROM CMS_Category AS pc WHERE (pc.CategoryEnabled = 0) AND (CMS_Category.CategoryIDPath like pc.CategoryIDPath+'/%')))");

        // Filter non-personal categories by starting category
        if (!string.IsNullOrEmpty(StartingCategory))
        {
            string startingIdPath = (StartingCategoryObj != null) ? StartingCategoryObj.CategoryIDPath : "";

            where = SqlHelperClass.AddWhereCondition(where, "(CategoryUserID IS NOT NULL) OR (CategoryIDPath LIKE N'" + SqlHelperClass.GetSafeQueryString(startingIdPath) + "/%')");
        }

        return where;
    }


    /// <summary>
    /// Builds category tree from given dataset and returns root node object.
    /// </summary>
    /// <param name="data"></param>
    private void CreateCategoryTrees(DataSet data)
    {
        generalRoot = new CategoryNode(CategoriesRoot);
        personalRoot = new CategoryNode(PersonalCategoriesRoot);

        if (!SqlHelperClass.DataSourceIsEmpty(data))
        {
            // Handle custom starting category
            string prefixToRemove = "";
            if (!string.IsNullOrEmpty(StartingCategory) && (StartingCategoryObj != null))
            {
                prefixToRemove = StartingCategoryObj.CategoryIDPath;
            }

            foreach (DataRow dr in data.Tables[0].Rows)
            {
                // Get processed category path
                string idPath = dr["CategoryIDPath"].ToString();

                // Shorten ID path when starting category entered
                if (!string.IsNullOrEmpty(prefixToRemove))
                {
                    idPath = idPath.Replace(prefixToRemove, "");
                }

                // Split path
                string[] idSplits = idPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                int[] ids = ValidationHelper.GetIntegers(idSplits, -1);

                // Add categories from path to tree
                foreach (int id in ids)
                {
                    CategoryInfo category = CategoryInfoProvider.GetCategoryInfo(id);

                    if (category != null)
                    {
                        if (category.CategoryIsPersonal)
                        {
                            personalRoot.AddCategory(category);
                        }
                        else
                        {
                            generalRoot.AddCategory(category);
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// Renders tree specified by root node into supplied string builder. 
    /// </summary>
    /// <param name="builder">String builder to render to.</param>
    /// <param name="root">Root of the rendered subtree.</param>
    private void RenderTree(StringBuilder builder, CategoryNode root)
    {
        bool itemStarted = false;

        // Render node title
        if ((root.Category != null) || (!string.IsNullOrEmpty(root.AlternativeText)))
        {
            builder.Append("<li class=\"CategoryListItem\">");
            itemStarted = true;

            if (root.Category != null)
            {
                builder.Append(CreateCategoryPartLink(root.Category.CategoryDisplayName, root.Category.CategoryID));
            }
            else
            {
                builder.Append(FormatCategoryDisplayName(root.AlternativeText));
            }
        }

        // Recursively render child nodes
        if (root.Nodes.Count > 0)
        {
            if (itemStarted)
            {
                builder.AppendLine("<ul class=\"CategoryListList\">");
            }

            foreach (CategoryNode child in root.Nodes)
            {
                RenderTree(builder, child);
            }

            if (itemStarted)
            {
                builder.AppendLine("</ul>");
            }
        }

        if (itemStarted)
        {
            builder.Append("</li>");
        }
    }

    #endregion


    #region "Category node class"

    /// <summary>
    /// Class representing node of the category tree.
    /// </summary>
    private class CategoryNode
    {
        private List<CategoryNode> mNodes = new List<CategoryNode>();

        /// <summary>
        /// ID of the category which is represented by this node.
        /// </summary>
        public int ID
        {
            get;
            set;
        }


        /// <summary>
        /// Category info object represented by this node.
        /// </summary>
        public CategoryInfo Category
        {
            get;
            set;
        }


        /// <summary>
        /// Text to be used when no category specified.
        /// </summary>
        public string AlternativeText
        {
            get;
            set;
        }


        /// <summary>
        /// Child nodes.
        /// </summary>
        public List<CategoryNode> Nodes
        {
            get
            {
                return mNodes;
            }
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        public CategoryNode()
        {
        }


        /// <summary>
        /// Constructor with alternative text.
        /// </summary>
        /// <param name="altText">Alternative text.</param>
        public CategoryNode(string altText)
        {
            AlternativeText = altText;
        }


        /// <summary>
        /// Contructor for specified category object.
        /// </summary>
        /// <param name="categoryObj">Category info object to create node for.</param>
        public CategoryNode(CategoryInfo categoryObj)
        {
            ID = categoryObj.CategoryID;
            Category = categoryObj;
        }


        /// <summary>
        /// Checks whether this node contains child with given ID.
        /// </summary>
        /// <param name="categoryId">ID of the category to look for.</param>
        public bool Contains(int categoryId)
        {
            return Nodes.Exists(c => c.ID == categoryId);
        }


        /// <summary>
        /// Creates and appends child node for given category if not exists. Return true when succesfully added.
        /// </summary>
        /// <param name="category"></param>
        public bool AddChild(CategoryInfo category)
        {
            if (!Contains(category.CategoryID))
            {
                Nodes.Add(new CategoryNode(category));

                return true;
            }

            return false;
        }


        /// <summary>
        /// Finds place for given category in subtree and creates node. Category is placed under root when no suitable place found.
        /// </summary>
        /// <param name="category"></param>
        public bool AddCategory(CategoryInfo category)
        {
            // Add to child list when suitable parent found
            if (category.CategoryParentID == ID)
            {
                AddChild(category);

                return true;
            }

            // Else look for place in all subtrees recursively
            foreach (CategoryNode node in Nodes)
            {
                if (node.AddCategory(category))
                {
                    return true;
                }
            }

            // When not found and current node is the root, add to child list
            if (ID == 0)
            {
                AddChild(category);

                return true;
            }

            // No suitable place found
            return false;
        }
    }

    #endregion
}
