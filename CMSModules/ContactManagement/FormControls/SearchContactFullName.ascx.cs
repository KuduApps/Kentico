using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;

public partial class CMSModules_ContactManagement_FormControls_SearchContactFullName : CMSAbstractBaseFilterControl
{
    /// <summary>
    /// OnLoad override - check wheter filter is set.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        LoadDropDown();
        base.OnLoad(e);
    }


    /// <summary>
    /// Loads dropdown list.
    /// </summary>
    private void LoadDropDown()
    {
        if (!RequestHelper.IsPostBack())
        {
            drpCondition.Items.Clear();
            drpCondition.Items.Add(new ListItem("LIKE", "0"));
            drpCondition.Items.Add(new ListItem("NOT LIKE", "1"));
            drpCondition.Items.Add(new ListItem("=", "2"));
            drpCondition.Items.Add(new ListItem("<>", "3"));
        }
    }


    /// <summary>
    /// Generates where condition.
    /// </summary>
    protected static string GenerateWhereCondition(string text, string value)
    {
        string searchPhrase = SqlHelperClass.GetSafeQueryString(text, false);
        string prefix = "ISNULL(ContactFirstName, '') + CASE ContactFirstName WHEN '' THEN '' WHEN NULL THEN '' ELSE ' ' END + ISNULL(ContactMiddleName, '') + CASE ContactMiddleName WHEN '' THEN '' WHEN NULL THEN '' ELSE ' ' END + ISNULL(ContactLastName, '')";
        switch (value)
        {
            // LIKE
            default:
            case "0": return prefix + " LIKE N'%" + searchPhrase + "%'";
            case "1": return prefix + " NOT LIKE N'%" + searchPhrase + "%'";
            case "2": return prefix + " = N'" + searchPhrase + "'";
            case "3": return prefix + " <> N'" + searchPhrase + "'";
        }
    }


    /// <summary>
    /// Select button handler.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">EventArgs</param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        // Set where condition
        WhereCondition = GenerateWhereCondition(txtSearch.Text, drpCondition.SelectedValue);
        //Raise OnFilterChange event
        RaiseOnFilterChanged();
    }
}