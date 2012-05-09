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
using CMS.CMSHelper;

public partial class CMSModules_Categories_Controls_SelectCategory : CMSUserControl
{
    #region "Variables"

    private int mRootCategoryId = 0;
    private int mCategoryId = 0;
    private int mExcludeCategoryId = 0;
    private int mUserId = 0;
    private int mSiteId = -1;
    private bool mAddNoneRecord = false;
    private string mSubItemPrefix = "--";
    private string mWhereCondition = null;
    private bool mDisableSiteCategories = false;
    private bool dataLoaded = false;
    private bool mAllowDisabledCategories = false;

    private Hashtable disabledCats = null;

    GroupedDataSource gds = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public bool Enabled
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
    /// Gets or sets the category ID.
    /// </summary>
    public int CategoryID
    {
        get
        {
            return ValidationHelper.GetInteger(this.drpCategories.SelectedValue, 0);
        }
        set
        {
            mCategoryId = value;
            try
            {
                this.drpCategories.SelectedValue = mCategoryId.ToString();
            }
            catch { }
        }
    }


    /// <summary>
    /// Gets or sets the string which will be used as a prefix in order to achieve tree structure.
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
    /// Indicates whether (none) should be dispalyed. Default value is false.
    /// </summary>
    public bool AddNoneRecord
    {
        get
        {
            return mAddNoneRecord;
        }
        set
        {
            mAddNoneRecord = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the category which will be the root of the tree structure.
    /// </summary>
    public int RootCategoryID
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
    /// Category which (with its subtree) should be excluded from the list.
    /// </summary>
    public int ExcludeCategoryID
    {
        get
        {
            return mExcludeCategoryId;
        }
        set
        {
            mExcludeCategoryId = value;
        }
    }


    /// <summary>
    /// ID of the site to offer categories for. Default value is ID of the current site.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (mSiteId < 0)
            {
                mSiteId = CMSContext.CurrentSiteID;
            }

            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }


    /// <summary>
    /// ID of the user to offer categories for.
    /// </summary>
    public int UserID
    {
        get
        {
            return mUserId;
        }
        set
        {
            mUserId = value;
        }
    }


    /// <summary>
    /// Additional where condition.
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
    /// Indicates whether site categories are to be disabled.
    /// </summary>
    public bool DisableSiteCategories
    {
        get
        {
            return mDisableSiteCategories;
        }
        set
        {
            mDisableSiteCategories = value;
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


    /// <summary>
    /// Indicates if disabled categories are allowed in category selector. Default value is false.
    /// </summary>
    public bool AllowDisabledCategories
    {
        get
        {
            return mAllowDisabledCategories;
        }
        set
        {
            mAllowDisabledCategories = value;
        }
    }

    #endregion


    #region "Page events"

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
                    DisableItem(this.drpCategories.Items[i]);
                }
            }
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads data.
    /// </summary>
    public void ReloadData(bool force)
    {
        if (!dataLoaded || force)
        {
            DisabledItems = string.Empty;
            disabledCats = new Hashtable();

            int shift = 0;

            this.drpCategories.Items.Clear();

            string where = GetWhereCondition();

            if (AddNoneRecord)
            {
                ListItem item = new ListItem(GetString("general.root"), "0");
                this.drpCategories.Items.Add(item);
            }

            // Get the data
            DataSet ds = CategoryInfoProvider.GetCategories(where, "CategoryUserID, CategorySiteID, CategoryOrder", 0, "CategoryID, CategoryParentID, CategoryDisplayName, CategoryOrder, CategoryLevel, CategorySiteID, CategoryEnabled", SiteID);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                gds = new GroupedDataSource(ds, "CategoryParentID");

                FillDropDownList(shift, 0);
            }

            // Ensure selected category
            if (CategoryID != mCategoryId)
            {
                CategoryID = mCategoryId;
            }

            dataLoaded = true;
        }
    }


    /// <summary>
    /// Reloads data if items are not already loaded.
    /// </summary>
    public void ReloadData()
    {
        ReloadData(false);
    }


    /// <summary>
    /// Fills existing categories in the drop down list as a tree structure.
    /// </summary>
    /// <param name="shift">Subcategory offset in the DDL</param>
    /// <param name="parentCategoryId">ID of the parent category</param>
    private void FillDropDownList(int shift, int parentCategoryId)
    {
        ArrayList items = null;
        if (parentCategoryId > 0)
        {
            items = gds.GetGroupView(parentCategoryId);
        }
        else
        {
            items = gds.GetGroupView(DBNull.Value);
        }
        if (items != null)
        {
            shift++;

            string prefix = "";
            // Prepare prefix
            for (int i = 0; i < shift; i++)
            {
                prefix += this.SubItemPrefix;
            }

            foreach (DataRowView dr in items)
            {
                ListItem item = new ListItem();

                // Prepend prefix
                item.Text = prefix;

                int catId = ValidationHelper.GetInteger(dr["CategoryID"], 0);
                int catParentId = ValidationHelper.GetInteger(dr["CategoryParentID"], 0);
                string catDisplayName = ValidationHelper.GetString(dr["CategoryDisplayName"], "");
                bool catIsSite = ValidationHelper.GetInteger(dr["CategorySiteID"], 0) > 0;
                bool catIsEnabled = ValidationHelper.GetBoolean(dr["CategoryEnabled"], true);

                if (!AllowDisabledCategories)
                {
                    // Category stays enabled only if its parent is enabled
                    if (catIsEnabled)
                    {
                        catIsEnabled = !disabledCats.ContainsKey(catParentId);
                    }

                    // Add disabled category to disabled list
                    if (!catIsEnabled && !disabledCats.ContainsKey(catId))
                    {
                        disabledCats.Add(catId, null);
                    }
                }
                
                item.Text += ResHelper.LocalizeString(catDisplayName);
                item.Value = catId.ToString();

                // Add item to the DLL
                this.drpCategories.Items.Add(item);
                
                // Disable unwanted site categories and unwanted disabled categories
                if ((!AllowDisabledCategories && !catIsEnabled) || (DisableSiteCategories && catIsSite))
                {
                    DisableItem(item);
                }

                // Call to add the subitems
                FillDropDownList(shift, catId);
            }
        }
    }


    private string GetWhereCondition()
    {
        string where = "ISNULL(CategoryUserID, 0) = " + UserID;

        if (ExcludeCategoryID > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "CategoryIDPath NOT LIKE (SELECT N'' + CategoryIDPath + '%' FROM CMS_Category WHERE CategoryID = " + ExcludeCategoryID + ")");
        }

        return where;
    }


    /// <summary>
    /// Disables list item and change its color if the current item is not group and selector is in keyEdit mode.
    /// Have to be called after item has been added to drpCategories not before!
    /// </summary>
    /// <param name="item"></param>
    private void DisableItem(ListItem item)
    {
        item.Attributes.Add("style", "color:gray");
        item.Attributes.Add("disabled", "disabled");

        DisabledItems += drpCategories.Items.IndexOf(item) + "|";
    }

    #endregion
}
