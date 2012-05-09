using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_UI_PanelMenu_PanelMenu : CMSUserControl
{
    #region "Variables"

    private int columnsCount = 1;

    #endregion


    #region "Properties"

    /// <summary>
    /// Categories data {[n, 0] - Category title, [n, 1] - Category title, [n, 2] - Category URL, [n, 3] - Category image URL, [n, 4] - Category tooltip, [n, 5] - Category actions}.
    /// </summary>
    public object[,] Categories { get; set; }


    /// <summary>
    /// Number of columns in which categories will be displayed.
    /// </summary>
    public int ColumnsCount
    {
        get
        {
            return this.columnsCount;
        }
        set
        {
            if (value <= 0)
            {
                this.columnsCount = 1;
            }
            else
            {
                this.columnsCount = value;
            }
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadCategories();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Creates columns and populates them with controls for categories.
    /// </summary>
    public void LoadCategories()
    {
        if (this.Categories != null)
        {
            // Prepare columns data
            List<object[]>[] columnsData = new List<object[]>[this.ColumnsCount];

            int categoriesCount = this.Categories.GetLength(0);
            int categoriesPerColumn = (int)Math.Ceiling((double)categoriesCount / (double)ColumnsCount);

            // Distribute categories over columns
            int columnCategoriesCount = 0;
            int columnIndex = 0;
            for (int i = 0; i < categoriesCount; i++)
            {
                if (columnCategoriesCount < categoriesPerColumn)
                {
                    columnCategoriesCount++;
                }
                else
                {
                    columnCategoriesCount = 1;
                    columnIndex++;
                }

                if (columnsData[columnIndex] == null)
                {
                    columnsData[columnIndex] = new List<object[]>();
                }

                columnsData[columnIndex].Add(new object[] { this.Categories[i, 0], this.Categories[i, 1], this.Categories[i, 2], this.Categories[i, 3], this.Categories[i, 4], this.Categories[i, 5] });
            }

            // Bind columns data
            this.rptColumns.DataSource = columnsData;
            this.rptColumns.DataBind();
        }
    }

    #endregion
}
