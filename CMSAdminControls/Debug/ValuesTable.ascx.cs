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
using System.Drawing;

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSAdminControls_Debug_ValuesTable : ValuesTable
{
    #region "Variables"

    protected int index = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        this.EnableViewState = false;
        this.Visible = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = false;

        if (this.Table != null)
        {
            // Get the log table
            DataTable dt = this.Table;
            if (!DataHelper.DataSourceIsEmpty(dt))
            {
                this.Visible = true;

                // Set the column names
                foreach (DataColumn dc in dt.Columns)
                {
                    BoundField col = new BoundField();
                    col.DataField = dc.ColumnName;
                    col.HeaderText = GetString(ResourcePrefix + dc.ColumnName);

                    // Set style
                    col.ItemStyle.BorderColor = Color.FromArgb(204, 204, 204);
                    col.ItemStyle.BorderStyle = BorderStyle.Solid;
                    col.ItemStyle.BorderWidth = 1;
                    col.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                    if (dc.DataType != typeof(string))
                    {
                        col.ItemStyle.Wrap = false;
                    }

                    col.HeaderStyle.CopyFrom(col.ItemStyle);

                    gridValues.Columns.Add(col);
                }

                // Bind the data
                gridValues.DataSource = dt;
                gridValues.DataBind();

                if (!String.IsNullOrEmpty(this.Title))
                {
                    this.ltlInfo.Text = "<div style=\"padding: 5px 2px 2px 2px;\"><strong>" + this.Title + "</strong></div>";
                }
            }
        }
    }


    /// <summary>
    /// Gets the item index.
    /// </summary>
    protected int GetIndex()
    {
        return ++index;
    }
}
