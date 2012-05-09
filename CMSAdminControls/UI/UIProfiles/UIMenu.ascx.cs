using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSAdminControls_UI_UIProfiles_UIMenu : CMSUserControl
{
    #region "Variables"

    private string mRootTargetURL = String.Empty;

    private bool mModuleAvailabilityForSiteRequired = false;
    private bool mDisplayRootIcon = false;

    private UIElementInfo root = null;
    private CurrentUserInfo currentUser = null;

    private int totalNodes = 0;
    private int mMaxRelativeLevel = 1;
    private bool mEnableRootSelect = true;

    private string preselectedItem = "";

    #endregion


    #region "Properties"


    /// <summary>
    /// Gets the root node of the tree.
    /// </summary>
    public TreeNode RootNode
    {
        get
        {
            return this.treeElem.CustomRootNode;
        }
    }


    /// <summary>
    /// Indicates if the root element is clickable.
    /// </summary>
    public bool EnableRootSelect
    {
        get
        {
            return mEnableRootSelect;
        }
        set
        {
            this.mEnableRootSelect = value;
        }
    }


    /// <summary>
    /// Code name of the UIElement.
    /// </summary>
    public string ElementName
    {
        get;
        set;
    }


    /// <summary>
    /// Code name of the module.
    /// </summary>
    public string ModuleName
    {
        get;
        set;
    }


    /// <summary>
    /// Name of the javascript function which is called when specified tab (UI element) is clicked. 
    /// UI element code name is passed as parameter.
    /// </summary>
    public string JavaScriptHandler
    {
        get;
        set;
    }


    /// <summary>
    /// Query parameter name for the preselection of the item.
    /// </summary>
    public string QueryParameterName
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if all nodes should be expanded.
    /// </summary>
    public bool ExpandAll
    {
        get
        {
            return this.treeElem.ExpandAll;
        }
        set
        {
            this.treeElem.ExpandAll = value;
        }
    }


    /// <summary>
    /// Indicates number of expanded levels.
    /// </summary>
    public int ExpandLevel
    {
        get
        {
            return this.treeElem.ExpandLevel;
        }
        set
        {
            this.treeElem.ExpandLevel = value;
        }
    }


    /// <summary>
    /// Gets the value which indicates whether there is some tab displayed or not.
    /// </summary>
    public bool MenuEmpty
    {
        get
        {
            if (this.treeElem.CustomRootNode != null)
            {
                return (this.treeElem.CustomRootNode.ChildNodes.Count == 0);
            }
            return true;
        }
    }


    /// <summary>
    /// Root node target URL.
    /// </summary>
    public string RootTargetURL
    {
        get
        {
            return this.mRootTargetURL;
        }
        set
        {
            this.mRootTargetURL = value;
        }
    }


    /// <summary>
    /// Indicates if site availability of the corresponding module (module with name in format "cms.[ElementName]") is required for each UI element in the menu. Takes effect only when corresponding module exists.
    /// </summary>
    public bool ModuleAvailabilityForSiteRequired
    {
        get
        {
            return this.mModuleAvailabilityForSiteRequired;
        }
        set
        {
            this.mModuleAvailabilityForSiteRequired = value;
        }
    }


    /// <summary>
    /// Gets or sets maximal relative level displayed (depth of the tree to load).
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return this.mMaxRelativeLevel;
        }
        set
        {
            this.mMaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Indicates if the icon should be displayed in the root of the tree.
    /// </summary>
    public bool DisplayRootIcon
    {
        get
        {
            return this.mDisplayRootIcon;
        }
        set
        {
            this.mDisplayRootIcon = value;
        }
    }

    #endregion


    #region "Custom events"

    /// <summary>
    /// Node created delegate.
    /// </summary>
    public delegate TreeNode NodeCreatedEventHandler(UIElementInfo uiElement, TreeNode defaultNode);


    /// <summary>
    /// Node created event handler.
    /// </summary>
    public event NodeCreatedEventHandler OnNodeCreated;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Handle the preselection
        preselectedItem = QueryHelper.GetString(this.QueryParameterName, "");
        if (preselectedItem.StartsWith("cms.", StringComparison.InvariantCultureIgnoreCase))
        {
            preselectedItem = preselectedItem.Substring(4);
        }

        this.treeElem.SelectedItem = preselectedItem;

        // Use images according to culture
        if (CultureHelper.IsUICultureRTL())
        {
            this.treeElem.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            this.treeElem.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }

        // Register JQuery
        ScriptHelper.RegisterJQuery(this.Page);

        // Get the info
        if (String.IsNullOrEmpty(this.ElementName))
        {
            // Get the root UI element
            root = UIElementInfoProvider.GetRootUIElementInfo(this.ModuleName);
        }
        else
        {
            // Get the specified element
            root = UIElementInfoProvider.GetUIElementInfo(this.ModuleName, this.ElementName);
        }

        if (root != null)
        {
            string levelWhere = (this.MaxRelativeLevel <= 0 ? "" : " AND (ElementLevel <= " + (root.ElementLevel + this.MaxRelativeLevel) + ")");
            string levelColumn = "CASE ElementLevel WHEN " + (root.ElementLevel + this.MaxRelativeLevel) + " THEN 0 ELSE ElementChildCount END AS ElementChildCount";

            // Create and set category provider
            UniTreeProvider provider = new UniTreeProvider();
            provider.RootLevelOffset = root.ElementLevel;
            provider.ObjectType = "cms.uielement";
            provider.DisplayNameColumn = "ElementDisplayName";
            provider.IDColumn = "ElementID";
            provider.LevelColumn = "ElementLevel";
            provider.OrderColumn = "ElementOrder";
            provider.ParentIDColumn = "ElementParentID";
            provider.PathColumn = "ElementIDPath";
            provider.ValueColumn = "ElementName";
            provider.ChildCountColumn = "ElementChildCount";
            provider.WhereCondition = "((ElementLevel = 0) OR ((ElementCaption IS NOT NULL) AND NOT (ElementCaption = '')))" + levelWhere;
            provider.Columns = "ElementID, ElementName, ElementDisplayName, ElementLevel, ElementOrder, ElementParentID, ElementIDPath, ElementCaption, ElementIconPath, ElementTargetURL, ElementResourceID, " + levelColumn;

            string redirectScript = ScriptHelper.GetScript("function redirectUrl(node,url,nodeCodeName) {SelectNode(nodeCodeName);if (" + this.JavaScriptHandler + ") { " + this.JavaScriptHandler + "(node, url); } return false;}");
            ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "redirect", redirectScript);

            if (String.IsNullOrEmpty(this.JavaScriptHandler))
            {
                if (EnableRootSelect)
                {
                    this.treeElem.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\" onclick=\"SelectNode('##NODECODENAME##'); return false;\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
                }
                else
                {
                    this.treeElem.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
                }
                this.treeElem.NodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"SelectNode('##NODECODENAME##'); return false;\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
            }
            else
            {
                if (EnableRootSelect)
                {
                    this.treeElem.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\" onclick=\"return redirectUrl(##NODEJAVA##, '##NODETARGETURL##','##NODECODENAME##'); \">##ICON##<a class=\"Name\" href=\"##NODETARGETURL##\">##NODECUSTOMNAME##</a></span>";
                }
                else
                {
                    this.treeElem.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
                }
                this.treeElem.NodeTemplate = "<span id=\"node_##NODECODENAME##\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"return redirectUrl(##NODEJAVA##, '##NODETARGETURL##','##NODECODENAME##'); \">##ICON##<a class=\"Name\" href=\"##NODETARGETURL##\">##NODECUSTOMNAME##</a></span>";
            }
            this.treeElem.UsePostBack = false;
            this.treeElem.ProviderObject = provider;
            this.treeElem.ExpandPath = root.ElementIDPath;

            this.treeElem.OnGetImage += new CMSAdminControls_UI_Trees_UniTree.GetImageEventHandler(treeElem_OnGetImage);
            this.treeElem.OnNodeCreated += new CMSAdminControls_UI_Trees_UniTree.NodeCreatedEventHandler(treeElem_OnNodeCreated);

            // Create root node
            string rootName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(String.IsNullOrEmpty(root.ElementCaption) ? root.ElementDisplayName : root.ElementCaption));
            string rootIcon = "<img src=\"" + URLHelper.ResolveUrl(GetImageUrl("/General/DefaultRoot.png")) + "\" style=\"border:none;height:10px;width:1px;\" />";
            string rootText = this.treeElem.ReplaceMacros(this.treeElem.SelectedNodeTemplate, root.ElementID, root.ElementChildCount, rootName, rootIcon, root.ElementParentID, null, null);

            rootText = rootText.Replace("##NODETARGETURL##", ScriptHelper.GetString(this.RootTargetURL, false));
            rootText = rootText.Replace("##NODECUSTOMNAME##", rootName);
            rootText = rootText.Replace("##NODECODENAME##", root.ElementName);

            this.treeElem.SetRoot(rootText, root.ElementID.ToString(), (this.DisplayRootIcon ? UIHelper.GetImageUrl(this.Page, root.ElementIconPath) : ""), URLHelper.Url + "#", null);
            if (EnableRootSelect)
            {
                this.treeElem.CustomRootNode.SelectAction = TreeNodeSelectAction.None;
            }

            currentUser = CMSContext.CurrentUser;
        }

        // Reserve log item
        DataRow sdr = SecurityHelper.ReserveSecurityLogItem("LoadUIMenu");

        this.treeElem.ReloadData();

        // Log the security
        if (sdr != null)
        {
            SecurityHelper.SetLogItemData(sdr, currentUser.UserName, this.ModuleName, this.ElementName, totalNodes, CMSContext.CurrentSiteName);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Add editing icon in development mode
        if (SettingsKeyProvider.DevelopmentMode && CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(this.ModuleName);
            if (ri != null)
            {
                treeElem.RootNode.Text = UIHelper.GetResourceUIElementsLink(this.Page, ri.ResourceId) + treeElem.RootNode.Text;
            }
        }
    }


    protected TreeNode treeElem_OnNodeCreated(DataRow itemData, TreeNode defaultNode)
    {
        if (itemData != null)
        {
            CurrentUserInfo currentUser = CMSContext.CurrentUser;
            if (currentUser != null)
            {
                // Check permissions
                string elemName = ValidationHelper.GetString(itemData["ElementName"], "");
                if (currentUser.IsAuthorizedPerUIElement(this.ModuleName, elemName, this.ModuleAvailabilityForSiteRequired))
                {
                    // Ensure element caption
                    string caption = ValidationHelper.GetString(itemData["ElementCaption"], "");
                    if (String.IsNullOrEmpty(caption))
                    {
                        caption = ValidationHelper.GetString(itemData["ElementDisplayName"], "");
                    }

                    // Set caption
                    string text = defaultNode.Text;
                    text = text.Replace("##NODECUSTOMNAME##", ResHelper.LocalizeString(caption));
                    text = text.Replace("##NODECODENAME##", elemName);

                    if (!String.IsNullOrEmpty(JavaScriptHandler))
                    {
                        defaultNode.SelectAction = TreeNodeSelectAction.None;
                    }

                    // Set URL
                    string url = CMSContext.ResolveMacros(ValidationHelper.GetString(itemData["ElementTargetURL"], ""));
                    url = URLHelper.EnsureHashToQueryParameters(url);

                    if (String.IsNullOrEmpty(url))
                    {
                        return null;
                    }
                    url = ScriptHelper.GetString(URLHelper.ResolveUrl(url), false);

                    text = text.Replace("##NODETARGETURL##", url);

                    defaultNode.Text = text;

                    totalNodes++;

                    // Raise the node created handler
                    if (OnNodeCreated != null)
                    {
                        defaultNode = OnNodeCreated(new UIElementInfo(itemData), defaultNode);
                    }

                    // Handle the preselection
                    if (defaultNode != null)
                    {
                        if (preselectedItem.Equals(elemName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ScriptHelper.RegisterStartupScript(this, typeof(string), "UIMenu_Preselection",
                                ScriptHelper.GetScript("redirectUrl('" + elemName + "','" + url + "'," + ScriptHelper.GetString(elemName) + ")"));
                        }

                        string targetURL = ValidationHelper.GetString(itemData["ElementTargetURL"], "");

                        // If url is '@' dont redirect, only collapse children
                        if (targetURL == "@")
                        {
                            // Set text manualy, dont't use template                        
                            string imageUrl = ValidationHelper.GetString(itemData["ElementIconPath"], "");

                            // Get image URL
                            imageUrl = GetImageUrl(imageUrl);

                            // Try to get default icon if requested icon not found
                            if (!FileHelper.FileExists(imageUrl))
                            {
                                imageUrl = GetImageUrl("CMSModules/list.png");
                            }
                            
                            // Onclick simulates click on '+' or '-' button
                            string onClick = "onClick=\"var js = $j(this).parents('tr').find('a').attr('href');eval(js); \"";
                            
                            // Insert image manually - (some IE issues)
                            defaultNode.Text = "<table class=\"TreeNodeTable\" cellspacing=\"0\" cellpadding=\"0\"><tr><td><img src='" + imageUrl + "'/></td><td><span id=\"node_" + elemName + "\" class=\"ContentTreeItem \" name=\"treeNode\" " + onClick + " ><span class=\"NodeName\">" + ResHelper.LocalizeString(caption) + "</span></span></td></tr></table>";                            
                        }
                    }

                    return defaultNode;
                }
            }
        }

        return null;
    }


    /// <summary>
    /// Gets image handler.
    /// </summary>
    /// <param name="node">Tree node</param>
    protected string treeElem_OnGetImage(UniTreeNode node)
    {
        DataRow dr = node.ItemData as DataRow;
        if (dr != null)
        {
            // Get icon path
            string imgUrl = ValidationHelper.GetString(dr["ElementIconPath"], "");
            if (!String.IsNullOrEmpty(imgUrl))
            {
                if (!ValidationHelper.IsURL(imgUrl))
                {
                    imgUrl = UIHelper.GetImagePath(this.Page, imgUrl, false, false);

                    // Try to get default icon if requested icon not found
                    if (!FileHelper.FileExists(imgUrl))
                    {
                        imgUrl = GetImageUrl("CMSModules/list.png");
                    }
                }

                return imgUrl;
            }
        }

        return GetImageUrl("CMSModules/list.png");
    }
}
