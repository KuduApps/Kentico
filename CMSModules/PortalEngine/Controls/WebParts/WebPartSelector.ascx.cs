using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_Controls_WebParts_WebPartSelector : CMSAdminControl
{
    #region "Variables"

    private bool mShowInheritedWebparts = true;
    private bool mShowWidgetOnlyWebparts = false;

    #endregion


    #region "Webpart selector properties"

    /// <summary>
    /// If true, only wireframe web parts are offered.
    /// </summary>
    public bool ShowWireframeOnlyWebparts
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates whether inherited webpart will be displayed in selector.
    /// </summary>
    public bool ShowInheritedWebparts
    {
        get
        {
            return mShowInheritedWebparts;
        }
        set
        {
            mShowInheritedWebparts = value;
        }
    }


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
            flatElem.ShowWidgetOnlyWebparts = value;
            treeElem.ShowWidgetOnlyWebparts = value;
        }
    }

    #endregion


    #region "Selector properties"

    /// <summary>
    /// Gets or set the flat panel selected item.
    /// </summary>
    public string SelectedItem
    {
        get
        {
            return flatElem.SelectedItem;
        }
        set
        {
            flatElem.SelectedItem = value;
        }
    }


    /// <summary>
    /// Gets or sets name of javascript function used for passing selected value from flat selector.
    /// </summary>
    public string SelectFunction
    {
        get
        {
            return flatElem.UniFlatSelector.SelectFunction;
        }
        set
        {
            flatElem.UniFlatSelector.SelectFunction = value;
        }
    }


    /// <summary>
    /// If enabled, flat selector remembers selected item trough postbacks.
    /// </summary>
    public bool RememberSelectedItem
    {
        get
        {
            return flatElem.UniFlatSelector.RememberSelectedItem;
        }
        set
        {
            flatElem.UniFlatSelector.RememberSelectedItem = value;
        }
    }


    /// <summary>
    /// Enables  or disables stop processing.
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
            flatElem.StopProcessing = value;
            treeElem.StopProcessing = value;
            this.EnableViewState = !value;
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
            flatElem.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page methods and events"

    /// <summary>
    /// OnInit.
    /// </summary>    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        treeElem.SelectPath = "/";
    }

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        treeElem.OnItemSelected += new CMSModules_PortalEngine_Controls_WebParts_WebPartTree.ItemSelectedEventHandler(treeElem_OnItemSelected);

        string where = null;

        // Filter for inherited webparts
        if (!ShowInheritedWebparts)
        {
            where = SqlHelperClass.AddWhereCondition(flatElem.UniFlatSelector.WhereCondition, "WebPartParentID IS NULL");
        }

        // Where conditions for wireframe 
        if (ShowWireframeOnlyWebparts)
        {
            where = "(0 == 1)";// SqlHelperClass.AddWhereCondition(where, "WebPartType = " + Convert.ToInt32(WebPartTypeEnum.Wireframe));

            treeElem.RootPath = "/Wireframes";
            treeElem.SelectPath = "/Wireframes";
        }

        flatElem.UniFlatSelector.WhereCondition = where;
        
        // Preselect root category
        if (!RequestHelper.IsPostBack())
        {
            ResetToDefault();
        }
    }


    /// <summary>
    /// Page prerender.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        // Pass currently selected category to flat selector
        if (RequestHelper.IsPostBack())
        {
            flatElem.TreeSelectedItem = treeElem.SelectedItem;
        }
    }


    /// <summary>
    /// On tree element item selected.
    /// </summary>
    /// <param name="selectedValue">Selected value</param> 
    protected void treeElem_OnItemSelected(string selectedValue)
    {
        flatElem.TreeSelectedItem = selectedValue;

        // Clear search box and pager
        flatElem.UniFlatSelector.ResetToDefault();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads data.
    /// </summary>
    /// <param name="reloadFlat">If true, flat selector is reloaded</param>
    public override void ReloadData(bool reloadFlat)
    {
        treeElem.ReloadData();
        if (reloadFlat)
        {
            flatElem.ReloadData();
        }
    }


    /// <summary>
    /// Selects root category in tree, clears search condition and resets pager to first page.
    /// </summary>
    public void ResetToDefault()
    {
        // Get root webpart category
        WebPartCategoryInfo wci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName("/");
        if (wci != null)
        {
            flatElem.SelectedCategory = wci;

            // Expand root node
            treeElem.SelectedItem = wci.CategoryID.ToString();
            treeElem.SelectPath = "/";
        }

        // Clear search condition and resets pager to first page
        flatElem.UniFlatSelector.ResetToDefault();
    }

    #endregion
}
