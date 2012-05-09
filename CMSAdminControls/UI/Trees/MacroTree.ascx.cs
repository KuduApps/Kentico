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

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.IO;

using TreeNode = System.Web.UI.WebControls.TreeNode;

public partial class CMSAdminControls_UI_Trees_MacroTree : CMSUserControl
{
    #region "Variables"

    private ContextResolver mContextResolver;

    private string mOnNodeClickHandler = null;
    private string mMacroExpression;
    private object mRoot;
    private bool mDisplayValues = true;

    private int mMaxDepth = 10;

    #endregion


    #region "Properties"

    /// <summary>
    /// Tree view control.
    /// </summary>
    public TreeView TreeControl
    {
        get
        {
            return treeElem;
        }
    }


    /// <summary>
    /// Gets or sets ContextResolver object which is used to evaluate macro expression. If nothing is specified, CMSContext.CurrentResolver is used.
    /// </summary>
    public ContextResolver ContextResolver
    {
        get
        {
            if (this.mContextResolver == null)
            {
                this.mContextResolver = CMSContext.CurrentResolver.CreateContextChild();
            }

            return this.mContextResolver;
        }
        set
        {
            this.mContextResolver = value;
        }
    }


    /// <summary>
    /// Gets or sets macro expression to evaluate. The result of the evaluation is used as a root of the tree structure.
    /// </summary>
    public string MacroExpression
    {
        get
        {
            return this.mMacroExpression;
        }
        set
        {
            this.mMacroExpression = value;
        }
    }


    /// <summary>
    /// Gets or sets JavaScript function which will be called when a node in the tree is clicked. Macro of the node is passed as a parameter to this function.
    /// </summary>
    public string OnNodeClickHandler
    {
        get
        {
            return this.mOnNodeClickHandler;
        }
        set
        {
            this.mOnNodeClickHandler = value;
        }
    }


    /// <summary>
    /// Gets or sets maximal tree depth.
    /// </summary>
    public int MaxDepth
    {
        get
        {
            return this.mMaxDepth;
        }
        set
        {
            this.mMaxDepth = value;
        }
    }


    /// <summary>
    /// Indicates whether to display values of the properties.
    /// </summary>
    public bool DisplayValues
    {
        get
        {
            return this.mDisplayValues;
        }
        set
        {
            this.mDisplayValues = value;
        }
    }


    /// <summary>
    /// Gets a root of the tree structure (result of the evauation of given macro).
    /// </summary>
    public object Root
    {
        get
        {
            if (mRoot == null)
            {
                bool match = false;
                this.ContextResolver.ResolveDataMacro(this.MacroExpression, ref mRoot, out match, true);
            }
            return this.mRoot;
        }
    }


    /// <summary>
    /// Gets an macro expression specifying currently selected node.
    /// </summary>
    public string SelectedMacroExpression
    {
        get
        {
            if (treeElem.SelectedNode != null)
            {
                return treeElem.SelectedNode.Value.Replace('/', '.').Replace(".[", "[");
            }
            else
            {
                return this.MacroExpression;
            }
        }
    }


    /// <summary>
    /// Gets an object specifying currently selected node.
    /// </summary>
    public object SelectedObject
    {
        get
        {
            object currentNodeObj = null;
            bool match = false;
            this.ContextResolver.ResolveDataMacro(this.SelectedMacroExpression, ref currentNodeObj, out match, true);
            return currentNodeObj;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the design
        bool isRTL = CultureHelper.IsUICultureRTL();
        if (IsLiveSite)
        {
            isRTL = CultureHelper.IsPreferredCultureRTL();
        }

        if (isRTL)
        {
            treeElem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", IsLiveSite, true);
        }
        else
        {
            treeElem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", IsLiveSite, true);
        }
        treeElem.ImageSet = TreeViewImageSet.Custom;
        treeElem.ExpandImageToolTip = GetString("ContentTree.Expand");
        treeElem.CollapseImageToolTip = GetString("ContentTree.Collapse");

        ContextResolver.UserName = CMSContext.CurrentUser.UserName;
        ContextResolver.CheckIntegrity = false;

        if (!Page.IsCallback)
        {
            // Register tree progress icon
            ScriptHelper.RegisterTreeProgress(this.Page);
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads the tree.
    /// </summary>
    public void ReloadData()
    {
        // Setup the root
        if (!String.IsNullOrEmpty(this.MacroExpression))
        {
            TreeNode root = new TreeNode("{% " + this.MacroExpression.Trim() + " %}");
            root.NavigateUrl = "javascript: " + OnNodeClickHandler + "('');";
            root.Value = HttpUtility.UrlEncode(this.MacroExpression);
            if (IsSimpleValue(this.Root))
            {
                // Macro result is only simple value, no children
                root.ToolTip = GetValueString(this.Root, true);
            }
            else
            {
                root.ToolTip = GetValueString(this.Root, false);
                root.PopulateOnDemand = true;
            }
            this.treeElem.Nodes.Add(root);

            root.Expanded = true;
        }
    }


    protected void treeElem_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        // Get the macro expression and evaluate it
        string macro = HttpUtility.UrlDecode(e.Node.ValuePath.Replace('/', '.')).Replace(".[", "[");

        object currentNodeObj = null;
        bool match = false;
        bool securityCheckPassed = false;
        this.ContextResolver.ResolveDataMacro(macro, ref currentNodeObj, out match, true, out securityCheckPassed);

        if (securityCheckPassed)
        {
            BindObject(currentNodeObj, e.Node);
        }
        else
        {
            TreeNode child = new TreeNode(GetString("macrodesigner.accessdenied"));
            child.Value = "";
            e.Node.ChildNodes.Add(child);
        }
    }


    private void BindObject(object obj, TreeNode root)
    {
        int index = 0;

        if (obj is DataRow)
        {
            // DataRow source, bind column names
            DataRow dr = (DataRow)obj;

            // Create tree structure
            foreach (DataColumn col in dr.Table.Columns)
            {
                // Stop on max nodes
                if (index++ >= MacroResolver.MaxMacroNodes)
                {
                    AppendMore(root);
                    break;
                }

                // Add the column
                object childObj = dr[col.ColumnName];
                AppendChild(root, col.ColumnName, childObj, false);
            }
        }
        else if (obj is DataRowView)
        {
            // DataRowView source, bind column names
            DataRowView dr = (DataRowView)obj;

            // Create tree structure
            foreach (DataColumn col in dr.DataView.Table.Columns)
            {
                // Stop on max nodes
                if (index++ >= MacroResolver.MaxMacroNodes)
                {
                    AppendMore(root);
                    break;
                }

                // Add the column
                object childObj = dr[col.ColumnName];
                AppendChild(root, col.ColumnName, childObj, false);
            }
        }
        else if (obj is AbstractContext)
        {
            AbstractContext context = (AbstractContext)obj;

            // Create tree structure
            foreach (string col in context.Properties)
            {
                // Stop on max nodes
                if (index >= MacroResolver.MaxMacroNodes)
                {
                    AppendMore(root);
                    break;
                }

                // Add the property
                object childObj = context.GetProperty(col);
                AppendChild(root, col, childObj, false);
            }
        }
        else if (obj is IHierarchicalObject)
        {
            // Data container source
            IHierarchicalObject hc = (IHierarchicalObject)obj;

            // Create tree structure
            foreach (string col in hc.Properties)
            {
                // Stop on max nodes
                if (index++ >= MacroResolver.MaxMacroNodes)
                {
                    AppendMore(root);
                    break;
                }

                // Add the property
                object childObj = null;
                try
                {
                    hc.TryGetProperty(col, out childObj);
                }
                catch
                {
                }

                AppendChild(root, col, childObj, false);
            }
        }
        else if (obj is IDataContainer)
        {
            // Data container source
            IDataContainer dc = (IDataContainer)obj;

            // Create tree structure
            foreach (string col in dc.ColumnNames)
            {
                // Stop on max nodes
                if (index++ >= MacroResolver.MaxMacroNodes)
                {
                    AppendMore(root);
                    break;
                }

                // Add the column
                object childObj = null;
                dc.TryGetValue(col, out childObj);

                AppendChild(root, col, childObj, false);
            }
        }

        // Enumerable objects
        if ((obj is IEnumerable) && !(obj is string))
        {
            IEnumerable collection = (IEnumerable)obj;
            IEnumerator enumerator = null;

            bool indexByName = false;

            INamedEnumerable namedCol = null;
            if (obj is INamedEnumerable)
            {
                // Collection with name enumerator
                namedCol = (INamedEnumerable)collection;
                if (namedCol.ItemsHaveNames)
                {
                    enumerator = namedCol.GetNamedEnumerator();
                    indexByName = true;
                }
            }

            if (!indexByName)
            {
                // Standard collection
                enumerator = collection.GetEnumerator();
            }

            int i = 0;

            while (enumerator.MoveNext())
            {
                // Stop on max nodes
                if (index++ >= MacroResolver.MaxMacroNodes)
                {
                    AppendMore(root);
                    break;
                }

                // Add the item
                object item = SqlHelperClass.EncapsulateObject(enumerator.Current);
                if (indexByName)
                {
                    // Convert the name with dot to indexer
                    string name = namedCol.GetObjectName(item);
                    if (!ValidationHelper.IsIdentifier(name))
                    {
                        name = "[\"" + name + "\"]";
                    }

                    AppendChild(root, name, item, false);
                }
                else
                {
                    // Indexed item
                    AppendChild(root, i.ToString(), item, true);
                }

                i++;
            }
        }
    }


    #region "Helper methods"

    /// <summary>
    /// Appends the "more" node (for case when there is more items than max nodes
    /// </summary>
    /// <param name="root">Root node</param>
    private void AppendMore(TreeNode root)
    {
        string iconUrl = GetImagePath("Design/Controls/MacroEditor/info.png"); ;
        string icon = "<img src=\"" + URLHelper.ResolveUrl(iconUrl) + "\" alt=\"\" style=\"border:0px;vertical-align:middle;\" class=\"Image16\" />";

        string text = "<span class=\"ContentTreeItem\" onclick=\"return false;\">" + icon + "<span class=\"Name\">" + GetString("MacroTree.More") + "</span></span>";

        TreeNode child = new TreeNode(text);

        child.ToolTip = GetString("MacroTree.MoreInfo");
        child.Value = "";
        child.PopulateOnDemand = false;

        root.ChildNodes.Add(child);
    }


    /// <summary>
    /// Appends given child object to the root node.
    /// </summary>
    /// <param name="root">Root node</param>
    /// <param name="colName">Name of the column</param>
    /// <param name="childObj">Child object to append</param>
    /// <param name="enumerate">Indicates if it's enumeration item</param>
    private void AppendChild(TreeNode root, string colName, object childObj, bool enumerate)
    {
        // Prepare the property name
        string col = colName;
        string defaultIconUrl = null;

        if (enumerate)
        {
            if (childObj is InfoObjectCollection)
            {
                col = ((InfoObjectCollection)childObj).Name;
            }
            else if (childObj != null)
            {
                col = "[" + col + "]" + (this.DisplayValues ? " " + HTMLHelper.HTMLEncode(childObj.ToString()) : "");
            }
        }

        string name = col;

        // Prepare the icon
        string iconUrl = GetImagePath("CMSModules/list.png");

        if (childObj is SettingsCategoryContainer)
        {
            // Get list icon of Settings category
            iconUrl = GetObjectIconPath(SiteObjectType.SETTINGSCATEGORY, "list.png");
        }
        else if (childObj is InfoObjectCollection)
        {
            var colObj = (InfoObjectCollection)childObj;

            // Collection
            iconUrl = GetObjectIconPath(colObj.ObjectType, "list.png");

            defaultIconUrl = GetDefaultIconUrl(colObj.TypeInfo);
        }
        else if (childObj is BaseInfo)
        {
            // Info object
            BaseInfo infoObj = (BaseInfo)childObj;

            iconUrl = GetObjectIconPath(infoObj.ObjectType, "list.png");

            defaultIconUrl = GetDefaultIconUrl(infoObj.TypeInfo);

            string objName = infoObj.Generalized.ObjectDisplayName;
            if (!String.IsNullOrEmpty(objName))
            {
                if (enumerate)
                {
                    name = "[" + colName + "] " + (this.DisplayValues ? " " + HTMLHelper.HTMLEncode(objName) : "");
                }
                else
                {
                    if (DisplayValues)
                    {
                        name += " <span class=\"Info\">(" + HTMLHelper.HTMLEncode(objName) + ")</span>";
                    }
                }
            }
        }
        else if (childObj is CMS.TreeEngine.TreeNode)
        {
            // Document
            CMS.TreeEngine.TreeNode node = (CMS.TreeEngine.TreeNode)childObj;

            iconUrl = GetDocumentTypeIconPath(node.NodeClassName, null, true);

            string docName = node.DocumentName;
            if (String.IsNullOrEmpty(docName))
            {
                docName = "/";
            }

            if (enumerate)
            {
                name = "[" + colName + "] " + HTMLHelper.HTMLEncode(docName);
            }
            else
            {
                if (DisplayValues)
                {
                    name += " <span class=\"Info\">(" + HTMLHelper.HTMLEncode(docName) + ")</span>"; ;
                }
            }
        }
        else if (childObj is PageInfo)
        {
            // Page info
            PageInfo pi = (PageInfo)childObj;
            iconUrl = GetDocumentTypeIconUrl(pi.ClassName);
        }
        else if (childObj is AbstractContext)
        {
            // Context
            iconUrl = GetImagePath("Design/Controls/MacroEditor/context.png");
        }
        else
        {
            switch (col.ToLower())
            {
                case "children":
                    // Children
                    iconUrl = GetImagePath("Design/Controls/MacroEditor/children.png");
                    break;

                case "parent":
                    // Parent
                    iconUrl = GetImagePath("Design/Controls/MacroEditor/parent.png");
                    break;

                default:
                    if (childObj == null)
                    {
                        // Null
                        iconUrl = GetImagePath("Design/Controls/MacroEditor/null.png"); ;
                    }
                    else if (!IsSimpleValue(childObj))
                    {
                        // Complex object
                        iconUrl = GetImagePath("Design/Controls/MacroEditor/repository.png"); ;
                    }
                    else if (childObj is string)
                    {
                        // Simple value
                        iconUrl = GetImagePath("Design/Controls/MacroEditor/string.png"); ;
                    }
                    else if ((childObj is int) || (childObj is double) || (childObj is long))
                    {
                        // Simple value
                        iconUrl = GetImagePath("Design/Controls/MacroEditor/number.png"); ;
                    }
                    else if (childObj is bool)
                    {
                        if ((bool)childObj)
                        {
                            // True
                            iconUrl = GetImagePath("Design/Controls/MacroEditor/true.png"); ;
                        }
                        else
                        {
                            // False
                            iconUrl = GetImagePath("Design/Controls/MacroEditor/false.png"); ;
                        }
                    }
                    else if (childObj is DateTime)
                    {
                        // Simple value
                        iconUrl = GetImagePath("Design/Controls/MacroEditor/datetime.png"); ;
                    }
                    else
                    {
                        // Simple value
                        iconUrl = GetImagePath("Design/Controls/MacroEditor/value.png"); ;
                    }
                    break;
            }
        }

        string icon = null;
        if (!String.IsNullOrEmpty(iconUrl))
        {
            // Check the existence of the file
            if (!File.Exists(Server.MapPath(iconUrl)))
            {
                if (defaultIconUrl == null)
                {
                    iconUrl = GetImagePath("CMSModules/list.png");
                }
                else
                {
                    iconUrl = defaultIconUrl;
                }
            }

            icon = "<img src=\"" + URLHelper.ResolveUrl(iconUrl) + "\" alt=\"\" style=\"border:0px;vertical-align:middle;\" class=\"Image16\" />";
        }

        bool isSimple = IsSimpleValue(childObj);

        // Append JS onclick action if defined, pass macro expression to the handler
        string jsHandler = "";
        if (!string.IsNullOrEmpty(OnNodeClickHandler))
        {
            string macroArg = root.ValuePath.Replace('/', '.') + (enumerate ? "[" + colName + "]" : "." + col);
            macroArg = HttpUtility.UrlDecode(macroArg);
            macroArg = macroArg.Replace(".[", "[");

            jsHandler = OnNodeClickHandler + "(" + ScriptHelper.GetString(macroArg.Substring(this.MacroExpression.Length + 1), true).Replace("\\\"", "&quot;") + ");";
        }

        // Create the node
        name = TextHelper.LimitLength(ResHelper.LocalizeString(name), 150, null, true);

        string text = "<span class=\"ContentTreeItem\" onclick=\"" + jsHandler + "return false;\">" + icon + "<span class=\"Name\">" + name + "</span></span>";

        TreeNode child = new TreeNode(text);

        child.ToolTip = GetValueString(childObj, isSimple);
        child.Value = HttpUtility.UrlEncode(enumerate ? "[" + colName + "]" : col);
        child.PopulateOnDemand = !isSimple;
        child.Expanded = false;

        root.ChildNodes.Add(child);
    }


    /// <summary>
    /// Gets the default icon URL for the given type info
    /// </summary>
    /// <param name="ti">Type information</param>
    protected string GetDefaultIconUrl(TypeInfo ti)
    {
        string defaultIconUrl = null;

        // Default icon per object category
        if (ti.IsSiteBinding)
        {
            defaultIconUrl = GetImagePath("Design/Controls/MacroEditor/sitebinding.png");
        }
        else if (ti.IsBinding)
        {
            defaultIconUrl = GetImagePath("Design/Controls/MacroEditor/binding.png");
        }

        return defaultIconUrl;
    }


    /// <summary>
    /// Returns formated value of given value.
    /// </summary>
    /// <param name="obj">Value</param>
    /// <param name="isSimple">Indicates if the value is simple object without children</param>
    private string GetValueString(object obj, bool isSimple)
    {
        if (isSimple)
        {
            if (obj != null)
            {
                string stringValue = TextHelper.LimitLength(ValidationHelper.GetString(obj, ""), 200, null, true);

                return "(" + obj.GetType() + "): " + stringValue;
            }
        }
        else
        {
            return "(" + obj.GetType() + ")" + (obj is IList ? " Count: " + ((IList)obj).Count : "");
        }

        return "null";
    }


    /// <summary>
    /// Returns true if the object is simple value (value which has no children).
    /// </summary>
    /// <param name="obj">Object to check</param>
    private bool IsSimpleValue(object obj)
    {
        if (obj == null)
        {
            return true;
        }

        if ((obj is AbstractContext) || (obj is IDataContainer) || (obj is IHierarchicalObject) || (obj is DataRowView) || (obj is DataRow))
        {
            return false;
        }

        if ((obj is IEnumerable) && !(obj is string) && !(obj is byte[]))
        {
            //// If it's enumeration, return false only if there are some items
            //IEnumerable collection = (IEnumerable)obj;
            //IEnumerator enumerator = collection.GetEnumerator();
            //if (enumerator.MoveNext())
            //{
            //    return false;
            //}
            return false;
        }

        return true;
    }

    #endregion
}
