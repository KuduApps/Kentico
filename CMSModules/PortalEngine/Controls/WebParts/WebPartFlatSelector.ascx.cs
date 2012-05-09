using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_Controls_WebParts_WebPartFlatSelector : CMSAdminControl
{
    #region "Variables"

    private string mSelectedItem = string.Empty;
    private WebPartCategoryInfo mSelectedCategory = null;
    private string mTreeSelectedItem = null;
    private bool mShowWidgetOnlyWebparts = false;

    #endregion


    #region "Flat selector properties"

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
    /// Retruns inner instance of UniFlatSelector control.
    /// </summary>
    public UniFlatSelector UniFlatSelector
    {
        get
        {
            return flatElem;
        }
    }


    /// <summary>
    /// Gets or sets selected item in flat selector.
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
    /// Gets or sets the current webpart category.
    /// </summary>
    public WebPartCategoryInfo SelectedCategory
    {
        get
        {
            // If not loaded yet
            if (mSelectedCategory == null)
            {
                int categoryId = ValidationHelper.GetInteger(this.TreeSelectedItem, 0);
                if (categoryId > 0)
                {
                    mSelectedCategory = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(categoryId);
                }
            }

            return mSelectedCategory;
        }
        set
        {
            mSelectedCategory = value;
            // Update ID
            if (mSelectedCategory != null)
            {
                mTreeSelectedItem = mSelectedCategory.CategoryID.ToString();
            }
        }
    }


    /// <summary>
    /// Gets or sets the selected item in tree, ususaly the category id.
    /// </summary>
    public string TreeSelectedItem
    {
        get
        {
            return mTreeSelectedItem;
        }
        set
        {
            // Clear loaded category if change
            if (value != mTreeSelectedItem)
            {
                mSelectedCategory = null;
            }
            mTreeSelectedItem = value;
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
            flatElem.StopProcessing = value;
            this.EnableViewState = !value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        // Setup flat selector
        flatElem.QueryName = "cms.webpart.selectallview";
        flatElem.ValueColumn = "WebPartID";
        flatElem.SearchLabelResourceString = "webpart.webpartname";
        flatElem.SearchColumn = "WebPartDisplayName";
        flatElem.SelectedColumns = "WebPartName, MetafileGUID, WebPartDisplayName, WebPartID";
        flatElem.OrderBy = "WebPartDisplayName";
        flatElem.NotAvailableImageUrl = GetImageUrl("Objects/CMS_WebPart/notavailable.png");
        flatElem.NoRecordsMessage = "webparts.norecordsincategory";
        flatElem.NoRecordsSearchMessage = "webparts.norecords";
        flatElem.SearchCheckBox.Visible = true;
        flatElem.SearchCheckBox.Text = GetString("webparts.searchindescription");

        if (!URLHelper.IsPostback())
        {
            flatElem.SearchCheckBox.Checked = true;
        }

        if (flatElem.SearchCheckBox.Checked)
        {
            flatElem.SearchColumn += ";WebPartDescription";
        }

        flatElem.OnItemSelected += new UniFlatSelector.ItemSelectedEventHandler(flatElem_OnItemSelected);
    }


    /// <summary>
    /// On PreRender.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreRender(EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        if (!ShowWidgetOnlyWebparts)
        {
            // Do not display widget only webparts
            flatElem.WhereCondition = SqlHelperClass.AddWhereCondition(flatElem.WhereCondition, "WebPartType IS NULL OR WebPartType != " + Convert.ToInt32(WebPartTypeEnum.WidgetOnly));
        }

        // Restrict to items in selected category (if not root)
        if ((this.SelectedCategory != null) && (this.SelectedCategory.CategoryParentID > 0))
        {
            flatElem.WhereCondition = SqlHelperClass.AddWhereCondition(flatElem.WhereCondition, "WebPartCategoryID = " + this.SelectedCategory.CategoryID + " OR WebPartCategoryID IN (SELECT CategoryID FROM CMS_WebPartCategory WHERE CategoryPath LIKE N'" + SqlHelperClass.GetSafeQueryString(this.SelectedCategory.CategoryPath, false) + "/%')");
        }

        // Recently used
        if (this.TreeSelectedItem.ToLowerInvariant() == "recentlyused")
        {
            flatElem.WhereCondition = SqlHelperClass.AddWhereCondition(flatElem.WhereCondition, SqlHelperClass.GetWhereCondition("WebPartName", CMSContext.CurrentUser.UserSettings.UserUsedWebParts.Split(';')));
        }

        // Description area and recently used
        litCategory.Text = ShowInDescriptionArea(this.SelectedItem);

        base.OnPreRender(e);
    }

    #endregion


    #region "Event handling"

    /// <summary>
    /// Updates description after item is selected in flat selector.
    /// </summary>
    protected string flatElem_OnItemSelected(string selectedValue)
    {
        return ShowInDescriptionArea(selectedValue);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        flatElem.ReloadData();
        flatElem.ResetToDefault();
        pnlUpdate.Update();
    }


    /// <summary>
    /// Generates HTML text to be used in description area.
    /// </summary>
    ///<param name="selectedValue">Selected item for which generate description</param>
    private string ShowInDescriptionArea(string selectedValue)
    {
        string name = String.Empty;
        string description = String.Empty;

        if (!String.IsNullOrEmpty(selectedValue))
        {
            int webpartId = ValidationHelper.GetInteger(selectedValue, 0);
            WebPartInfo wi = WebPartInfoProvider.GetWebPartInfo(webpartId);
            if (wi != null)
            {
                name = wi.WebPartDisplayName;
                description = wi.WebPartDescription;
            }
        }
        // No selection show selected category
        else if (this.SelectedCategory != null)
        {
            name = this.SelectedCategory.CategoryDisplayName;
        }
        // Recently used
        else
        {
            name = GetString("webparts.recentlyused");
        }


        string text = "<div class=\"ItemName\">" + HTMLHelper.HTMLEncode(ResHelper.LocalizeString(name)) + "</div>";
        if (description != null)
        {
            text += "<div class=\"Description\">" + HTMLHelper.HTMLEncode(ResHelper.LocalizeString(description)) + "</div>";
        }

        return text;
    }

    #endregion
}
