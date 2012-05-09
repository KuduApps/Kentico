using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSModules_ContactManagement_Filters_ContactFilter : CMSAbstractBaseFilterControl
{
    #region "Methods"

    /// <summary>
    /// Create WHERE condition.
    /// </summary>
    private string CreateWhereCondition()
    {
        string text = txtContact.Text.Trim();
        return "(ContactFirstName LIKE '%" + SqlHelperClass.GetSafeQueryString(text) + "%' OR ContactMiddleName LIKE '%" + SqlHelperClass.GetSafeQueryString(text) + "%' OR ContactLastName LIKE '%" + SqlHelperClass.GetSafeQueryString(text) + "%')";
    }


    /// <summary>
    /// Button search click.
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.WhereCondition = CreateWhereCondition();
        RaiseOnFilterChanged();
    }

    #endregion
}
