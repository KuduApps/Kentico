using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSModules_PortalEngine_Controls_PageTemplates_SelectPageTemplate : FormEngineUserControl
{
    #region "Variables"

    private DataSet ds = null;
    private bool mShowRoot = false;
    private bool mShowEmptyCategories = false;
    private bool mShowTemplates = true;
    private bool mShowAdHocCategory = false;
    private bool mEnableCategorySelection = false;

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
            return drpTemplate.SelectedValue;
        }
        set
        {
            EnsureChildControls();
            drpTemplate.SelectedValue = ValidationHelper.GetString(value, String.Empty);
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
    /// If enabled, page templates are shown in dropdownlist.
    /// </summary>
    public bool ShowTemplates
    {
        get
        {
            return mShowTemplates;
        }
        set
        {
            mShowTemplates = value;
        }
    }


    /// <summary>
    /// Enables or disables ad-hoc catgegory in dropdown.
    /// </summary>
    public bool ShowAdHocCategory
    {
        get
        {
            return mShowAdHocCategory;
        }
        set
        {
            mShowAdHocCategory = value;
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


    /// <summary>
    /// Gets inner drop down list control.
    /// </summary>
    public DropDownList DropDownList
    {
        get
        {
            return drpTemplate;
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

        drpTemplate.Items.Clear();

        // Do not retrieve page templates
        string whereCond = null;
        if (!this.ShowTemplates)
        {
            whereCond = "ObjectType = 'pagetemplatecategory'";
        }

        // Filter out adhoc category and adhoc templates
        if (!this.mShowAdHocCategory)
        {
            whereCond = SqlHelperClass.AddWhereCondition(whereCond, "(ObjectType != 'pagetemplatecategory' OR CodeName != 'AdHoc') AND (ObjectType != 'pagetemplate' OR ParentID NOT IN (SELECT CategoryID FROM CMS_PageTemplateCategory WHERE CategoryName = 'AdHoc'))");
        }

        ds = ConnectionHelper.ExecuteQuery("cms.pagetemplatecategory.selectallview", null, whereCond, "DisplayName", 0, "ObjectID, DisplayName, ParentID, ObjectType");

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

                // Skip page template, take only PageTemplateCategory
                if (type == "pagetemplate")
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

                        drpTemplate.Items.Add(listItem);
                    }

                    if (this.ShowTemplates)
                    {
                        // Add templates under category
                        int templates = AddTemplates(indentation += "\xA0\xA0\xA0", Convert.ToInt32(category.Value[0]));

                        // Remove empty categories
                        if (!this.ShowEmptyCategories && (templates == 0) && (listItem != null))
                        {
                            drpTemplate.Items.Remove(listItem);
                        }
                    }

                    // Recursion
                    AddSubCategories(categories, Convert.ToInt32(category.Value[0]), level + 1);
                }
            }
        }
    }


    /// <summary>
    /// Add page templates under current category.
    /// </summary>
    /// <param name="indentation">Indentation of items</param>
    /// <param name="parentCategory">Parent category</param>
    /// <returns>Number of added page templates</returns>
    private int AddTemplates(string indentation, int parentCategory)
    {
        if (ds != null)
        {
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "ParentID = " + parentCategory + " AND ObjectType = 'pagetemplate'";
            foreach (DataRowView drv in dv)
            {
                // Create and add list item
                ListItem listItem = new ListItem(indentation + drv["DisplayName"], drv["ObjectID"].ToString());
                drpTemplate.Items.Add(listItem);
            }

            // Return number of Templates
            return dv.Count;
        }

        return 0;
    }

    #endregion
}
