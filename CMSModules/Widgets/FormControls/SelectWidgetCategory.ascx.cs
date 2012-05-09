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

public partial class CMSModules_Widgets_FormControls_SelectWidgetCategory : FormEngineUserControl
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return drpWidgetCategory.SelectedValue;
        }
        set
        {
            EnsureChildControls();
            drpWidgetCategory.SelectedValue = ValidationHelper.GetString(value, String.Empty);
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

        drpWidgetCategory.Items.Clear();

        // Get categories
        DataSet ds = WidgetCategoryInfoProvider.GetWidgetCategories(null, "WidgetCategoryDisplayName", 0, "WidgetCategoryID, WidgetCategoryDisplayName, WidgetCategoryParentID");

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            int counter = 0;

            // Make special collection for "tree mapping"
            Dictionary<int, SortedList<string, object[]>> categories = new Dictionary<int, SortedList<string, object[]>>();

            // Fill collection from dataset
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                int parentId = ValidationHelper.GetInteger(dr["WidgetCategoryParentID"], 0);
                int id = ValidationHelper.GetInteger(dr["WidgetCategoryID"], 0);
                string name = ResHelper.LocalizeString(ValidationHelper.GetString(dr["WidgetCategoryDisplayName"], String.Empty));

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
                    for (int i = 0; i < level; i++)
                    {
                        indentation += "\xA0\xA0\xA0";
                    }

                    // Create and add list item
                    ListItem listItem = new ListItem(indentation + category.Value[1], category.Value[0].ToString());
                    drpWidgetCategory.Items.Add(listItem);

                    // Recursion
                    AddSubCategories(categories, Convert.ToInt32(category.Value[0]), level + 1);
                }
            }
        }
    }

    #endregion
}
