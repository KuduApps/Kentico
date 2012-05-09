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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;

public partial class CMSWebParts_Navigation_cmstreeview : CMSAbstractWebPart
{
    #region "Document properties"
    
    /// <summary>
    /// Gets or sets the cache minutes.
    /// </summary>
    public override int CacheMinutes
    {
        get
        {
            return base.CacheMinutes;
        }
        set
        {
            base.CacheMinutes = value;
            treeView.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the cache item dependencies.
    /// </summary>
    public override string CacheDependencies
    {
        get
        {
            return base.CacheDependencies;
        }
        set
        {
            base.CacheDependencies = value;
            treeView.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the cache item. If not explicitly specified, the name is automatically 
    /// created based on the control unique ID
    /// </summary>
    public override string CacheItemName
    {
        get
        {
            return base.CacheItemName;
        }
        set
        {
            base.CacheItemName = value;
            treeView.CacheItemName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), this.treeView.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            treeView.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the class names.
    /// </summary>
    public string ClassNames
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Classnames"), this.treeView.ClassNames), this.treeView.ClassNames);
        }
        set
        {
            this.SetValue("ClassNames", value);
            this.treeView.ClassNames = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents are combined with default culture.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CombineWithDefaultCulture"), this.treeView.CombineWithDefaultCulture);
        }
        set
        {
            this.SetValue("CombineWithDefaultCulture", value);
            this.treeView.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Gets or sets the culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CultureCode"), this.treeView.CultureCode), this.treeView.CultureCode);
        }
        set
        {
            this.SetValue("CultureCode", value);
            this.treeView.CultureCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the maximal relative level.
    /// </summary>
    public int MaxRelativeLevel
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRelativeLevel"), this.treeView.MaxRelativeLevel);
        }
        set
        {
            this.SetValue("MaxRelativeLevel", value);
            this.treeView.MaxRelativeLevel = value;
        }
    }


    /// <summary>
    /// Gets or sets the order by clause.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("OrderBy"), this.treeView.OrderBy), this.treeView.OrderBy);
        }
        set
        {
            this.SetValue("OrderBy", value);
            this.treeView.OrderBy = value;
        }
    }


    /// <summary>
    /// Gets or sets the nodes path.
    /// </summary>
    public string Path
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("Path"), this.treeView.Path), this.treeView.Path);
        }
        set
        {
            this.SetValue("Path", value);
            this.treeView.Path = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected documents must be published.
    /// </summary>
    public bool SelectOnlyPublished
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SelectOnlyPublished"), this.treeView.SelectOnlyPublished);
        }
        set
        {
            this.SetValue("SelctOnlyPublished", value);
            this.treeView.SelectOnlyPublished = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("SiteName"), this.treeView.SiteName), this.treeView.SiteName);
        }
        set
        {
            this.SetValue("SiteName", value);
            this.treeView.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets the where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("WhereCondition"), this.treeView.WhereCondition);
        }
        set
        {
            this.SetValue("WhereCondition", value);
            this.treeView.WhereCondition = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether text can be wrapped or space is replaced with non breakable space.
    /// </summary>
    public bool WordWrap
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("WordWrap"), this.treeView.WordWrap);
        }
        set
        {
            this.SetValue("WordWrap", value);
            this.treeView.WordWrap = value;
        }
    }


    /// <summary>
    /// Gets or sets the CSS prefix.
    /// </summary>
    public string CSSPrefix
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("CSSPrefix"), this.treeView.CSSPrefix), this.treeView.CSSPrefix);
        }
        set
        {
            this.SetValue("CSSPrefix", value);
            this.treeView.CSSPrefix = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.treeView.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            treeView.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows results.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.treeView.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.treeView.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Filter name.
    /// </summary>
    public string FilterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FilterName"), this.treeView.FilterName);
        }
        set
        {
            this.SetValue("FilterName", value);
            this.treeView.FilterName = value;
        }
    }

    #endregion


    #region "CMSTreeView properties"

    /// <summary>
    /// Gets or sets the value that indicates whether treeview try fix broken lines.
    /// </summary>
    public bool FixBrokenLines
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("FixBrokenLines"), this.treeView.FixBrokenLines);
        }
        set
        {
            this.SetValue("FixBrokenLines", value);
            this.treeView.FixBrokenLines = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether all nodes should be expanded on startup.
    /// </summary>
    public bool ExpandAllOnStartup
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ExpandAllOnStartup"), this.treeView.ExpandAllOnStartup);
        }
        set
        {
            this.SetValue("ExpandAllOnStartup", value);
            this.treeView.ExpandAllOnStartup = value;
        }
    }


    /// <summary>
    /// Gets or sets the root node text (by default is root text selected from document caption or document name).
    /// </summary>
    public string RootText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RootText"), this.treeView.RootText);
        }
        set
        {
            this.SetValue("RootText", value);
            this.treeView.RootText = value;
        }
    }


    /// <summary>
    ///  Gets or sets the URL to the image which will be displayed for root node.
    /// </summary>
    public string RootImageUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RootImageUrl"), this.treeView.RootImageUrl);
        }
        set
        {
            this.SetValue("RootImageUrl", value);
            this.treeView.RootImageUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the URL to the image which will be displayed fro all nodes.
    /// </summary>
    public string NodeImageUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NodeImageUrl"), this.treeView.NodeImageUrl);
        }
        set
        {
            this.SetValue("NodeImageUrl", value);
            this.treeView.NodeImageUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the javascript action which is proceeded on OnClick action.
    /// </summary>
    public string OnClickAction
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("OnClickAction"), this.treeView.OnClickAction);
        }
        set
        {
            this.SetValue("OnClickAction", value);
            this.treeView.OnClickAction = ResolveOnClickMacros(value);
        }
    }


    /// <summary>
    /// Gets or sets the css style which is applied to the selected item.
    /// </summary>
    public string SelectedItemStyle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemStyle"), this.treeView.SelectedItemStyle);
        }
        set
        {
            this.SetValue("SelectedItemStyle", value);
            this.treeView.SelectedItemStyle = value;
        }
    }


    /// <summary>
    /// Gets or sets the CSS class which is applied to the selected item.
    /// </summary>
    public string SelectedItemClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemClass"), this.treeView.SelectedItemClass);
        }
        set
        {
            this.SetValue("SelectedItemClass", value);
            this.treeView.SelectedItemClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the css style for all items.
    /// </summary>
    public string ItemStyle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemStyle"), this.treeView.ItemStyle);
        }
        set
        {
            this.SetValue("ItemStyle", value);
            this.treeView.ItemStyle = value;
        }
    }


    /// <summary>
    /// Gets or sets the CSS class for all items.
    /// </summary>
    public string ItemClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ItemClass"), this.treeView.ItemClass);
        }
        set
        {
            this.SetValue("ItemClass", value);
            this.treeView.ItemClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether root node is not active (do not provide any action).
    /// </summary>
    public bool InactiveRoot
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("InactiveRoot"), this.treeView.InactiveRoot);
        }
        set
        {
            this.SetValue("InactiveRoot", value);
            this.treeView.InactiveRoot = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether sub items are loaded dynamically (populate on demand).
    /// </summary>
    public bool DynamicBehavior
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DynamicBehavior"), this.treeView.DynamicBehavior);
        }
        set
        {
            this.SetValue("DynamicBehavior", value);
            this.treeView.DynamicBehavior = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether document type images will be displayed.
    /// </summary>
    public bool DisplayDocumentTypeImages
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayDocumentTypeImages"), this.treeView.DisplayDocumentTypeImages);
        }
        set
        {
            this.SetValue("DisplayDocumentTypeImages", value);
            this.treeView.DisplayDocumentTypeImages = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether nide image isn't active (do not provide any action).
    /// </summary>
    public bool InactiveNodeImage
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("InactiveNodeImage"), this.treeView.InactiveNodeImage);
        }
        set
        {
            this.SetValue("InactiveNodeImage", value);
            this.treeView.InactiveNodeImage = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether path to the current item will be expanded.
    /// </summary>
    public bool ExpandCurrentPath
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ExpandCurrentPath"), this.treeView.ExpandCurrentPath);
        }
        set
        {
            this.SetValue("ExpandCurrentPath", value);
            this.treeView.ExpandCurrentPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected item will be inactivated (do not provide any action).
    /// </summary>
    public bool InactivateSelectedItem
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("InactivateSelectedItem"), this.treeView.InactivateSelectedItem);
        }
        set
        {
            this.SetValue("InactivateSelectedItem", value);
            this.treeView.InactivateSelectedItem = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether all items in path will be inactivated.
    /// </summary>
    public bool InactivateAllItemsInPath
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("InactivateAllItemsInPath"), this.treeView.InactivateAllItemsInPath);
        }
        set
        {
            this.SetValue("InactivateAllItemsInPath", value);
            this.treeView.InactivateAllItemsInPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether selected item is highlighted.
    /// </summary>
    public bool HiglightSelectedItem
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HiglightSelectedItem"), this.treeView.HiglightSelectedItem);
        }
        set
        {
            this.SetValue("HiglightSelectedItem", value);
            this.treeView.HiglightSelectedItem = value;
        }
    }


    /// <summary>
    /// Gets or sets property which indicates if menu caption should be HTML encoded.
    /// </summary>
    public bool EncodeMenuCaption
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("EncodeMenuCaption"), this.treeView.EncodeMenuCaption);
        }
        set
        {
            this.SetValue("EncodeMenuCaption", value);
            this.treeView.EncodeMenuCaption = value;
        }
    }


    /// <summary>
    /// Gets or sets the columns to be retrieved from database.
    /// </summary>  
    public string Columns
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Columns"), this.treeView.Columns);
        }
        set
        {
            this.SetValue("Columns", value);
            this.treeView.Columns = value;
        }
    }

    #endregion


    #region "CMSTreeView base properties"

    /// <summary>
    /// Gets or sets the URL to a custom image for the collapsible node indicator.
    /// </summary>
    public string CollapseImageUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CollapseImageUrl"), this.treeView.CollapseImageUrl);
        }
        set
        {
            this.SetValue("CollapseImageUrl", value);
            this.treeView.CollapseImageUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the URL to a custom image for the expandable node indicator.
    /// </summary>
    public string ExpandImageUrl
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ExpandImageUrl"), this.treeView.ExpandImageUrl);
        }
        set
        {
            this.SetValue("ExpandImageUrl", value);
            this.treeView.ExpandImageUrl = value;
        }
    }


    /// <summary>
    /// Gets or sets the target window or frame in which to display the Web page content that is associated with a node.
    /// </summary>
    public string Target
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Target"), this.treeView.Target);
        }
        set
        {
            this.SetValue("Target", value);
            this.treeView.Target = value;
        }
    }


    /// <summary>
    /// Gets or sets the css style which is applied to the inactive item.
    /// </summary>
    public string InactiveItemStyle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("InactiveItemStyle"), this.treeView.InactiveItemStyle);
        }
        set
        {
            this.SetValue("InactiveItemStyle", value);
            this.treeView.InactiveItemStyle = value;
        }
    }


    /// <summary>
    /// Gets or sets the CSS class which is applied to the inactive item.
    /// </summary>
    public string InactiveItemClass
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("InactiveItemClass"), this.treeView.InactiveItemClass);
        }
        set
        {
            this.SetValue("InactiveItemClass", value);
            this.treeView.InactiveItemClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether document menu action will be ignored.
    /// </summary>
    public bool IgnoreDocumentMenuAction
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("IgnoreDocumentMenuAction"), this.treeView.IgnoreDocumentMenuAction);
        }
        set
        {
            this.SetValue("IgnoreDocumentMenuAction", value);
            this.treeView.IgnoreDocumentMenuAction = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether root node is hidden.
    /// </summary>
    public bool HideRootNode
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideRootNode"), this.treeView.HideRootNode);
        }
        set
        {
            this.SetValue("HideRootNode", value);
            this.treeView.HideRootNode = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether subtree under current item is expanded.
    /// </summary>
    public bool ExpandSubTree
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ExpandSubTree"), this.treeView.ExpandSubTree);
        }
        set
        {
            this.SetValue("ExpandSubTree", value);
            this.treeView.ExpandSubTree = value;
        }
    }


    /// <summary>
    /// Gets or sets the SkinID which should be used.
    /// </summary>
    public override string SkinID
    {
        get
        {
            return base.SkinID;
        }
        set
        {
            base.SkinID = value;

            // Set SkinID
            if (!this.StandAlone && (this.PageCycle < PageCycleEnum.Initialized) && (ValidationHelper.GetString(this.Page.StyleSheetTheme, "") == ""))
            {
                this.treeView.SkinID = this.SkinID;
            }
        }
    }


    /// <summary>
    /// Gets or sets the value that indicating whether lines connecting  child nodes to parent nodes are displayed.
    /// </summary>
    public bool ShowLines
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowLines"), true);
        }
        set
        {
            this.SetValue("ShowLines", value);
            this.treeView.ShowLines = value;
        }
    }


    /// <summary>
    /// Gets or sets ToolTip for the image that is displayed for the expandable node indicator.
    /// </summary>
    public string ExpandImageToolTip
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ExpandImageToolTip"), this.treeView.ExpandImageToolTip);
        }
        set
        {
            this.SetValue("ExpandImageToolTip", value);
            this.treeView.ExpandImageToolTip = value;
        }
    }


    /// <summary>
    /// Gets or sets ToolTip for the image that is displayed for the collapsible node indicator.
    /// </summary>
    public string CollapseImageToolTip
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CollapseImageToolTip"), this.treeView.CollapseImageToolTip);
        }
        set
        {
            this.SetValue("CollapseImageToolTip", value);
            this.treeView.CollapseImageToolTip = value;
        }
    }


    /// <summary>
    /// Gets or sets the path to a folder that contains the line images that are used to connect child nodes to parent nodes.
    /// </summary>
    public string LineImagesFolder
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LineImagesFolder"), this.treeView.LineImagesFolder);
        }
        set
        {
            if (!CultureHelper.IsCultureRTL(CMSContext.CurrentDocumentCulture.CultureCode))
            {
                this.SetValue("LineImagesFolder", value);
                this.treeView.LineImagesFolder = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the path to a folder that contains the line images that are used to connect child nodes to parent nodes when the current culture is a RTL culture.
    /// </summary>
    public string RTLLineImagesFolder
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RTLLineImagesFolder"), this.treeView.LineImagesFolder);
        }
        set
        {
            if (CultureHelper.IsCultureRTL(CMSContext.CurrentDocumentCulture.CultureCode))
            {
                this.SetValue("RTLLineImagesFolder", value);
                this.treeView.LineImagesFolder = value;
            }
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            this.treeView.StopProcessing = true;
        }
        else
        {
            // Do not resolve macros in OnClickAction field
            this.NotResolveProperties = "onclickaction";

            this.treeView.ControlContext = this.ControlContext;

            // Set properties from Webpart form        
            this.treeView.FixBrokenLines = this.FixBrokenLines;
            this.treeView.CacheItemName = this.CacheItemName;
            this.treeView.CacheDependencies = this.CacheDependencies;
            this.treeView.CacheMinutes = this.CacheMinutes;
            this.treeView.CheckPermissions = this.CheckPermissions;
            this.treeView.ClassNames = this.ClassNames;
            this.treeView.CombineWithDefaultCulture = this.CombineWithDefaultCulture;
            this.treeView.CSSPrefix = this.CSSPrefix;
            this.treeView.CultureCode = this.CultureCode;
            this.treeView.MaxRelativeLevel = this.MaxRelativeLevel;
            this.treeView.OrderBy = this.OrderBy;
            this.treeView.Path = this.Path;
            this.treeView.SelectOnlyPublished = this.SelectOnlyPublished;
            this.treeView.SiteName = this.SiteName;
            this.treeView.WhereCondition = this.WhereCondition;
            this.treeView.EncodeMenuCaption = this.EncodeMenuCaption;
            this.treeView.Columns = this.Columns;

            this.treeView.CollapseImageUrl = this.CollapseImageUrl;
            this.treeView.ExpandImageUrl = this.ExpandImageUrl;

            this.treeView.ExpandImageToolTip = this.ExpandImageToolTip;
            this.treeView.CollapseImageToolTip = this.CollapseImageToolTip;

            if (CultureHelper.IsCultureRTL(CMSContext.CurrentDocumentCulture.CultureCode))
            {
                this.treeView.LineImagesFolder = this.RTLLineImagesFolder;
            }
            else
            {
                this.treeView.LineImagesFolder = this.LineImagesFolder;
            }

            this.treeView.ShowLines = this.ShowLines;

            if (((this.treeView.CollapseImageUrl != null) && (this.treeView.CollapseImageUrl != "")) ||
                ((this.treeView.ExpandImageUrl != null) && (this.treeView.ExpandImageUrl != "")))
            {
                this.treeView.ShowLines = false;
            }

            this.treeView.WordWrap = this.WordWrap;
            this.treeView.ExpandAllOnStartup = this.ExpandAllOnStartup;

            this.treeView.RootText = this.RootText;
            this.treeView.RootImageUrl = this.RootImageUrl;
            this.treeView.NodeImageUrl = this.NodeImageUrl;
            this.treeView.OnClickAction = ResolveOnClickMacros(this.OnClickAction);
            this.treeView.SelectedItemStyle = this.SelectedItemStyle;
            this.treeView.SelectedItemClass = this.SelectedItemClass;
            this.treeView.ItemStyle = this.ItemStyle;
            this.treeView.ItemClass = this.ItemClass;

            this.treeView.InactiveRoot = this.InactiveRoot;
            this.treeView.DynamicBehavior = this.DynamicBehavior;
            this.treeView.DisplayDocumentTypeImages = this.DisplayDocumentTypeImages;
            this.treeView.InactiveNodeImage = this.InactiveNodeImage;
            this.treeView.ExpandCurrentPath = this.ExpandCurrentPath;
            this.treeView.InactivateSelectedItem = this.InactivateSelectedItem;
            this.treeView.InactivateAllItemsInPath = this.InactivateAllItemsInPath;
            this.treeView.HiglightSelectedItem = this.HiglightSelectedItem;

            this.treeView.Target = this.Target;
            this.treeView.InactiveItemStyle = this.InactiveItemStyle;
            this.treeView.InactiveItemClass = this.InactiveItemClass;

            this.treeView.ExpandSubTree = this.ExpandSubTree;
            this.treeView.HideRootNode = this.HideRootNode;

            this.treeView.HideControlForZeroRows = this.HideControlForZeroRows;
            this.treeView.ZeroRowsText = this.ZeroRowsText;

            this.treeView.FilterName = this.FilterName;
        }
    }


    /// <summary>
    /// Resolves the macros within current WebPart context, with special handling for onclickaction field.
    /// </summary>
    /// <param name="inputText">Input text to resolve</param>
    public string ResolveOnClickMacros(string inputText)
    {
        // Special "macro" with two '%' will be resolveed later
        if (!String.IsNullOrEmpty(inputText) && !inputText.Contains("%%"))
        {
            ContextResolver resolver = ContextResolver.CreateContextChild();
            resolver.KeepUnresolvedMacros = true;
            return resolver.ResolveMacros(inputText, false);
        }
        return inputText;
    }


    /// <summary>
    /// Applies given stylesheet skin.
    /// </summary>
    public override void ApplyStyleSheetSkin(Page page)
    {
        this.treeView.SkinID = this.SkinID;
        base.ApplyStyleSheetSkin(page);
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        this.treeView.ReloadData(true);
        base.ReloadData();
    }


    /// <summary>
    /// OnPrerender override (Set visibility).
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = this.treeView.Visible;

        if (DataHelper.DataSourceIsEmpty(this.treeView.DataSource) && (this.treeView.HideControlForZeroRows))
        {
            this.Visible = false;
        }
    }
}
