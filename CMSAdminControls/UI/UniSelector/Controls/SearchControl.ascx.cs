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

using CMS.Controls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSAdminControls_UI_UniSelector_Controls_SearchControl : CMSAbstractBaseFilterControl
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
        drpCondition.Items.Clear();
        drpCondition.Items.Add(new ListItem("LIKE", "0"));
        drpCondition.Items.Add(new ListItem("NOT LIKE", "1"));
        drpCondition.Items.Add(new ListItem("=", "2"));
        drpCondition.Items.Add(new ListItem("<>", "3"));
    }


    /// <summary>
    /// Generates where condition.
    /// </summary>
    protected static string GenerateWhereCondition(string text, string value)
    {
        string searchPhrase = SqlHelperClass.GetSafeQueryString(text, false);

        switch (value)
        {
            // LIKE
            case "0": return "LIKE N'%" + searchPhrase + "%'";
            case "1": return "NOT LIKE N'%" + searchPhrase + "%'";
            case "2": return "= N'" + searchPhrase + "'";
            case "3": return "<> N'" + searchPhrase + "'";
            default: return "LIKE N'%%'";
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
