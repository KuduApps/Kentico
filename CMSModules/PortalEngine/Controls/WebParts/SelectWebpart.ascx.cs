using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSModules_PortalEngine_Controls_WebParts_SelectWebpart : FormEngineUserControl
{
    #region "Variables"

    private DataSet ds = null;
    private bool mShowRoot = false;
    private bool mShowEmptyCategories = false;
    private bool mShowWebparts = true;
    private bool mEnableCategorySelection = false;
    private bool mShowInheritedWebparts = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return drpWebpart.SelectedValue;
        }
        set
        {
            EnsureChildControls();
            drpWebpart.SelectedValue = ValidationHelper.GetString(value, String.Empty);
        }
    }


    /// <summary>
    /// Enables or disables root category in drop down list.
    /// </summary>
    public bool ShowRoot
    {
        get
        {
            return mShowRoot;
        }
        set
        {
            mShowRoot = value;
        }
    }


    /// <summary>
    /// Shows or hide inherited webparts.
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
    /// Enables or disables hiding of empty categories in drop down list.
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
    /// If enabled, webparts are shown in dropdownlist.
    /// </summary>
    public bool ShowWebparts
    {
        get
        {
            return mShowWebparts;
        }
        set
        {
            mShowWebparts = value;
        }
    }


    /// <summary>
    /// If enabled, category can be selected, otherwise categories are disabled.
    /// </summary>
    public bool EnableCategorySelection
    {
        get
        {
            return mEnableCategorySelection;
        }
        set
        {
            mEnableCategorySelection = value;
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

        drpWebpart.Items.Clear();

        // Do not retrieve webparts
        string whereCond = null;
        if (!this.ShowWebparts)
        {
            whereCond = "ObjectType = 'webpartcategory'";
        }

        if (!this.ShowInheritedWebparts)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "WebPartParentID IS NULL");
        }

        ds = ConnectionHelper.ExecuteQuery("cms.webpartcategory.selectallview", null, whereCond, "DisplayName", 0, "ObjectID, DisplayName, ParentID, ObjectType");

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            int counter = 0;

            // Make special collection for "tree mapping"
            Dictionary<int, SortedList<string, object[]>> categories = new Dictionary<int, SortedList<string, object[]>>();

            // Fill collection from dataset
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int parentId = ValidationHelper.GetInteger(dr["ParentID"], 0);
                int id = ValidationHelper.GetInteger(dr["ObjectID"], 0);
                string name = ResHelper.LocalizeString(ValidationHelper.GetString(dr["DisplayName"], String.Empty));
                string type = ValidationHelper.GetString(dr["ObjectType"], String.Empty);

                // Skip webpart, take only WebpartCategory
                if (type == "webpart")
                {
                    continue;
                }

                SortedList<string, object[]> list;
                categories.TryGetValue(parentId, out list);

                // Sub categories list not created yet
                if (list == null)
                {
                    list = new SortedList<string, object[]>();
                    categories.Add(parentId, list);
                }

                list.Add(name + "_" + counter, new object[2] { id, name });

                counter++;
            }

            // Start filling the dropdown from the root(parentId = 0)
            int level = 0;

            // Root is not shown, start indentation later
            if (!this.ShowRoot)
            {
                level = -1;
            }

            AddSubCategories(categories, 0, level);
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
                    for (int i = 0; i < level; i++)
                    {
                        indentation += "\xA0\xA0\xA0";
                    }

                    // Create and add list item
                    ListItem listItem = null;

                    // Hide root
                    if (((parentId == 0) && this.ShowRoot) || (parentId > 0))
                    {
                        if (this.EnableCategorySelection)
                        {
                            listItem = new ListItem(indentation + category.Value[1], category.Value[0].ToString());
                        }
                        else
                        {
                            listItem = new ListItem(indentation + category.Value[1], "-10");
                            listItem.Attributes.Add("style", "background-color: #DDDDDD; color: #000000;");
                            listItem.Attributes.Add("disabled", "disabled");
                        }

                        drpWebpart.Items.Add(listItem);
                    }

                    if (this.ShowWebparts)
                    {
                        // Add webparts under category
                        int webparts = AddWebparts(indentation += "\xA0\xA0\xA0", Convert.ToInt32(category.Value[0]));

                        // Remove empty categories
                        if (!this.ShowEmptyCategories && (webparts == 0) && (listItem != null))
                        {
                            drpWebpart.Items.Remove(listItem);
                        }
                    }

                    // Recursion
                    AddSubCategories(categories, Convert.ToInt32(category.Value[0]), level + 1);
                }
            }
        }
    }


    /// <summary>
    /// Add webparts under current category.
    /// </summary>
    /// <param name="indentation">Indentation of items</param>
    /// <param name="parentCategory">Parent category</param>
    /// <returns>Number of added webparts</returns>
    private int AddWebparts(string indentation, int parentCategory)
    {
        if (ds != null)
        {
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "ParentID = " + parentCategory + " AND ObjectType = 'webpart'";
            foreach (DataRowView drv in dv)
            {
                // Create and add list item
                ListItem listItem = new ListItem(indentation + drv["DisplayName"], drv["ObjectID"].ToString());
                drpWebpart.Items.Add(listItem);
            }

            // Return number of webparts
            return dv.Count;
        }

        return 0;
    }

    #endregion
}
