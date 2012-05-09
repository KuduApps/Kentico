using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_FormControls_SelectSettingsCategory : FormEngineUserControl
{
    #region "Variables"

    private string mSubItemPrefix = "--";
    private bool mDisplayOnlyCategories = true;
    private int mCurrentCategoryId = 0;
    private int mRootCategoryId = 0;
    private bool mIncludeRootCategory = false;
    private string mWhereCondition = null;
    private GroupedDataSource groupedDS = null;
    private bool mIsKeyEdit = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets SubItemPrefix.
    /// </summary>
    public string SubItemPrefix
    {
        get 
        { 
            return this.mSubItemPrefix; 
        }
        set 
        { 
            this.mSubItemPrefix = value; 
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return this.drpCategories.Enabled;
        }
        set
        {
            this.drpCategories.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets DisplayOnlyCategories property. 
    /// If set to <c>false</c> groups will be displayed as well.
    /// </summary>
    public bool DisplayOnlyCategories
    {
        get 
        { 
            return this.mDisplayOnlyCategories; 
        }
        set 
        { 
            this.mDisplayOnlyCategories = value; 
        }
    }
    
    
    /// <summary>
    /// If set to true selector is used in settings key edit form, so all items representing categories (not group) will be
    /// disabled and with different color.
    /// </summary>
    public bool IsKeyEdit
    {
        get 
        {
            return this.mIsKeyEdit; 
        }
        set 
        {
            this.mIsKeyEdit = value; 
        }
    }

    
    /// <summary>
    /// Gets the value of the selected Category in the list control, or selects the Category
    /// in the list control that contains the specified value.
    /// </summary>
    public int SelectedCategory
    {
        get
        {
            return ValidationHelper.GetInteger(this.drpCategories.SelectedValue, 0);
        }
        set
        {
            this.drpCategories.SelectedValue = value.ToString();
        }
    }


    /// <summary>
    /// Gets the collection of items in the list control.
    /// </summary>
    public ListItemCollection Items
    {
        get
        {
            return this.drpCategories.Items;
        }
    }


    /// <summary>
    /// Gets or sets ID of the Category which will be the root of the tree structure.
    /// </summary>
    public int RootCategoryId
    {
        get
        {
            return this.mRootCategoryId;
        }
        set
        {
            this.mRootCategoryId = value;
        }
    }


    /// <summary>
    /// Gets or sets enabled state of inclusion of RootCategory. Default false.
    /// </summary>
    public bool IncludeRootCategory
    {
        get
        {
            return this.mIncludeRootCategory;
        }
        set
        {
            this.mIncludeRootCategory = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the Category which will be not included in the list control so its children.
    /// </summary>
    public int CurrentCategoryId
    {
        get
        {
            return this.mCurrentCategoryId;
        }
        set
        {
            this.mCurrentCategoryId = value;
        }
    }


    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return this.mWhereCondition;
        }
        set
        {
            this.mWhereCondition = value;
        }
    }


    /// <summary>
    /// Indexes of categories, which have to be disabled e.g. '0|1|5|9|'.
    /// </summary>
    protected string DisabledItems
    {
        get
        {
            return ValidationHelper.GetString(ViewState["DisabledItems"], string.Empty);
        }
        set
        {
            ViewState["DisabledItems"] = value;
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing && !URLHelper.IsPostback())
        {
            ReloadData();
        }
        else
        {
            // Get disabled item indexes and disable them
            string[] indexs = DisabledItems.TrimEnd('|').Split('|');
            DisabledItems = string.Empty;
            int cnt = this.drpCategories.Items.Count;
            foreach (string index in indexs)
            {
                int i = ValidationHelper.GetInteger(index, -1);
                if (i >= 0 && i < cnt)
                {
                    DisableItemInKeyEdit(this.drpCategories.Items[i], false);
                }
            }
        }
    }

    #endregion


    #region "Public Methods"

    /// <summary>
    /// DataBinds the control.
    /// </summary>
    public void ReloadData()
    {
        DisabledItems = string.Empty;
        this.drpCategories.Items.Clear();
        int shift = -1;
        string where = string.Empty;
        if (DisplayOnlyCategories)
        {
            // Only categories that are not marked as groups will be displayed
            where += "ISNULL([CategoryIsGroup], 0) = 0";
        }
        if (!string.IsNullOrEmpty(WhereCondition))
        {
            // Append additional WHERE condition
            where += " AND " + WhereCondition;
        }

        // Add root category item if needed
        if (this.IncludeRootCategory)
        {
            SettingsCategoryInfo rootCat = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(this.RootCategoryId);
            if (rootCat != null)
            {
                ListItem item = new ListItem();
                item.Text = GetPrefix(shift) + ResHelper.LocalizeString(rootCat.CategoryDisplayName);
                item.Value = this.RootCategoryId.ToString();
                this.drpCategories.Items.Add(item);
                DisableItemInKeyEdit(item, rootCat.CategoryIsGroup);
                

                // Increase indent
                shift++;
            }
        }
        DataSet ds = SettingsCategoryInfoProvider.GetSettingsCategories(where, "CategoryOrder", 0, "CategoryID, CategoryParentID, CategoryName, CategoryDisplayName, CategoryOrder, CategoryIsGroup");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            this.groupedDS = new GroupedDataSource(ds, "CategoryParentID");
            FillDropDownList(shift, this.RootCategoryId);
        }
    }

    #endregion


    #region "Private Methods"

    /// <summary>
    /// Fills existing SettingsCategories in the DropDownList as a tree structure.
    /// </summary>
    /// <param name="shift">Subelement offset in the DropDownList</param>
    /// <param name="parentCategoryID">ID of the parent Category</param>
    private void FillDropDownList(int shift, int parentCategoryID)
    {
        ArrayList items = null;
        if (parentCategoryID > 0)
        {
            items = groupedDS.GetGroupView(parentCategoryID);
        }
        else
        {
            items = groupedDS.GetGroupView(DBNull.Value);
        }
        if (items != null)
        {
            shift++;

            foreach (DataRowView dr in items)
            {
                int cID = ValidationHelper.GetInteger(dr["CategoryID"], 0);

                // Skip processing for current category, current category and its children should not be displayed
                // in the DropDownList
                bool skipCurrent = ((this.CurrentCategoryId > 0) && (this.CurrentCategoryId == cID));
                if (skipCurrent)
                {
                    continue;
                }
                string cName = ValidationHelper.GetString(dr["CategoryName"], "");
                string cDisplayName = ValidationHelper.GetString(dr["CategoryDisplayName"], "");
                bool cIsGroup = ValidationHelper.GetBoolean(dr["CategoryIsGroup"], false);

                // Init item
                ListItem item = new ListItem();
                item.Text = GetPrefix(shift) + ResHelper.LocalizeString(cDisplayName);
                item.Value = cID.ToString();
                // Add item to the DropDownList
                this.drpCategories.Items.Add(item);
                // Disable item if in key-edit mode and item represents category
                DisableItemInKeyEdit(item, cIsGroup);

                // Call to add the subitems
                FillDropDownList(shift, cID);
            }
        }
    }


    /// <summary>
    /// Disables list item and change its color if the current item is not group and selector is in keyEdit mode.
    /// Have to be called after item has been added to drpCategories not before!
    /// </summary>
    /// <param name="item"></param>
    /// <param name="isGroup"></param>
    private void DisableItemInKeyEdit(ListItem item, bool isGroup)
    {
        if ((this.mIsKeyEdit) && (!isGroup))
        {
            item.Attributes.Add("style", "color:gray");
            item.Attributes.Add("disabled", "disabled");

            DisabledItems += drpCategories.Items.IndexOf(item) + "|";
        }
    }


    /// <summary>
    /// Gets item prefix.
    /// </summary>
    /// <param name="length">Prefix length</param>
    /// <returns>Text representing prefix.</returns>
    private string GetPrefix(int length)
    {
        string text = string.Empty;
        for (int i = 0; i < length; i++)
        {
            text += this.SubItemPrefix;
        }
        return text;
    }
   
    #endregion
}
