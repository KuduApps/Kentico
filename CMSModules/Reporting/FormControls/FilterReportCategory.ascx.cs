using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.SettingsProvider;


public partial class CMSModules_Reporting_FormControls_FilterReportCategory : CMSAbstractBaseFilterControl
{
    #region "Properties"

    /// <summary>
    /// Sets where condition for filter depending on selected category.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            // Load selected category
            int categoryID = ValidationHelper.GetInteger(usCategories.Value, 0);
            ReportCategoryInfo rci = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryID);
            if (rci != null)
            {
                // Add slash if not root 
                string path = rci.CategoryPath;
                if (rci.CategoryPath != "/")
                {
                    path += "/";
                }
                // OR condition is to prevent wrong selection of similar categories. f.e. /A and /AAA in same directory
                base.WhereCondition = "ReportCategoryID IN (SELECT CategoryID from Reporting_ReportCategory WHERE Reporting_ReportCategory.CategoryPath LIKE '" + SqlHelperClass.GetSafeQueryString(path) + "%' OR Reporting_ReportCategory.CategoryPath LIKE '" + SqlHelperClass.GetSafeQueryString(rci.CategoryPath) + "' )";             
            }
            return base.WhereCondition;            
        }
        set
        {
            base.WhereCondition = value;            
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        usCategories.DropDownList.SelectedIndexChanged += new EventHandler(DropDownList_SelectedIndexChanged);
    }

    void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        RaiseOnFilterChanged();
    }

    #endregion
}
