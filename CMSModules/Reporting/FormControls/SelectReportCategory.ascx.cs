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
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.FormControls;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.Reporting;


public partial class CMSModules_Reporting_FormControls_SelectReportCategory : FormEngineUserControl
{
    #region "Variables" 

    bool mShowRootCategory = false;
    bool mUseAutoPostBack = false;

    #endregion 


    #region "Public properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            EnsureChildControls();

            base.Enabled = value;
            drpCategories.Enabled = value;
        }
    }


    ///<summary>
    /// Gets or sets field value.
    ///</summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return drpCategories.SelectedValue;
        }
        set
        {
            EnsureChildControls();
            drpCategories.SelectedValue = ValidationHelper.GetString(value, String.Empty) ;
        }
    }


    /// <summary>
    /// Gets the inner DropDownList control.
    /// </summary>
    public DropDownList DropDownList
    {
        get
        {
            return drpCategories;
        }
    }


    /// <summary>
    /// If true show root category.
    /// </summary>
    public bool ShowRootCategory
    {
        get
        {
            return mShowRootCategory;
        }
        set
        {
            mShowRootCategory = value;
        }
    }


    /// <summary>
    /// Indicates if use autopostback.
    /// </summary>
    public bool UseAutoPostBack
    {
        get
        {
            return mUseAutoPostBack;
        }
        set
        {
            mUseAutoPostBack = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// Creates child controls.
    /// </summary>      
    protected override void CreateChildControls()
    {
        if (StopProcessing)
        {
            return;
        }
        drpCategories.AutoPostBack = UseAutoPostBack;
        drpCategories.Items.Clear();
       
        //// Get categories
        DataSet ds = ReportCategoryInfoProvider.GetCategories(String.Empty, String.Empty);

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            int counter = 0;

            // Make special collection for "tree mapping"
            Dictionary<int, SortedList<string, object[]>> categories = new Dictionary<int, SortedList<string, object[]>>();

            // Fill collection from dataset
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                int parentId = ValidationHelper.GetInteger(dr["CategoryParentID"], 0);
                int id = ValidationHelper.GetInteger(dr["CategoryID"], 0);
                string name = ResHelper.LocalizeString(ValidationHelper.GetString(dr["CategoryDisplayName"], String.Empty));

                SortedList<string, object[]> list;
                categories.TryGetValue(parentId, out list);

                try
                {
                    // Sub categories list not created yet
                    if (list == null)
                    {
                        list = new SortedList<string, object[]>();
                        categories.Add(parentId, list);
                    }

                    list.Add(name + "_" + counter, new object[2] { id, name });
                }
                catch
                {
                }
                counter++;
            }

                // Start filling the dropdown from the root(parentId = 0)
                AddSubCategories(categories, 0, 0);
        }
    }


    /// <summary>
    /// Add subcategories list items to drop down.
    /// </summary>
    /// <param name="categories">Special "tree" collection</param>
    /// <param name="parentId">Category parent ID</param>
    /// <param name="level">Category level(recursion)</param>
    private void AddSubCategories(Dictionary<int, SortedList<string, object[]>> categories, int parentId, int level)
    {
        if (categories != null)
        {
            SortedList<string, object[]> categoryList;
            categories.TryGetValue(parentId, out categoryList);
            if (categoryList != null)
            {
                foreach (KeyValuePair<string, object[]> category in categoryList)
                {
                    // Make indentation for sub categories
                    string indentation = String.Empty;
                    //No root displayed - show one level less
                    int levelIndentation = level - 1;
                    if (ShowRootCategory)
                    {
                        levelIndentation = level;
                    }
                    for (int i = 0; i < levelIndentation; i++)
                    {
                        indentation += "\xA0\xA0\xA0";
                    }

                    //If item is not root
                    if ((parentId != 0) || (ShowRootCategory))
                    {
                        // Create and add list item
                        ListItem listItem = new ListItem(indentation + category.Value[1], category.Value[0].ToString());
                        drpCategories.Items.Add(listItem);
                    }

                    // Recursion
                    AddSubCategories(categories, Convert.ToInt32(category.Value[0]), level + 1);
                }
            }
        }
    }

    #endregion
}
