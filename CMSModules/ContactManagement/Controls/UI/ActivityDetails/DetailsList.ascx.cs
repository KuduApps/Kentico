using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_ContactManagement_Controls_UI_ActivityDetails_DetailsList : CMSUserControl
{
    #region "Variables"

    private Table tbl = new Table();
    private bool mHideEmptyValues = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// If true, empty values is hidden/ignored.
    /// </summary>
    public bool HideEmptyValues
    {
        get { return mHideEmptyValues; }
        set { mHideEmptyValues = value; }
    }


    /// <summary>
    /// Returns true, if detilas list is not empty.
    /// </summary>
    public bool IsDataLoaded
    {
        get { return (tbl.Rows.Count > 0); }
    }

    #endregion 


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        tbl.ID = this.ID + "tbl";
        plc.Controls.Add(tbl);
    }


    /// <summary>
    /// Creates/adds new row to table.
    /// </summary>
    /// <param name="label">Label text (resource string)</param>
    /// <param name="value">Value (will be encoded)</param>
    public void AddRow(string label, string value)
    {
        AddRow(label, value, true);
    }


    /// <summary>
    /// Creates/adds new row to table.
    /// </summary>
    /// <param name="label">Label text (resource string)</param>
    /// <param name="value">Value</param>
    /// <param name="encode">Set tu true if value should be encoded</param>
    public void AddRow(string label, string value, bool encode)
    {
        if (encode)
        {
            value = HTMLHelper.HTMLEncode(value);
        }

        AddRow(label, value, "ActivityDetailsLabel", null, true);
    }


    /// <summary>
    /// Creates/adds new row to table.
    /// </summary>
    /// <param name="label">Label text (resource string)</param>
    /// <param name="value">Value (will be encoded)</param>
    /// <param name="labelCssClass">CSS class for label cell</param>
    /// <param name="valueCssClass">CSS class for value cell</param>
    public void AddRow(string label, string value, string labelCssClass, string valueCssClass, bool DisplayColon)
    {
        if (HideEmptyValues && String.IsNullOrEmpty(value))
        {
            return;
        }

        label = GetString(label);
        if (DisplayColon)
        {
            label += GetString("general.colon");
        }


        int i = tbl.Rows.Count;
        TableRow tr = new TableRow();
        tr.Cells.Add(CreateTableCell("cell0" + i, label, labelCssClass));
        tr.Cells.Add(CreateTableCell("cell1" + i, value, valueCssClass));
        tbl.Rows.Add(tr);
    }


    /// <summary>
    /// Creates new cell.
    /// </summary>
    /// <param name="id">Cell ID</param>
    /// <param name="text">Cell text</param>
    /// <param name="cssClass">CSS class</param>
    private TableCell CreateTableCell(string id, string text, string cssClass)
    {
        TableCell tc = new TableCell();
        tc.ID = id;
        tc.CssClass = cssClass;
        tc.Text = text;
        return tc;
    }

    #endregion
}

