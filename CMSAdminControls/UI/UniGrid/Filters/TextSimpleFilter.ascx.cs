using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.Controls;
using System.Text;

public partial class CMSAdminControls_UI_UniGrid_Filters_TextSimpleFilter : CMSAbstractBaseFilterControl
{
    #region "Private variables"

    private bool mIncludeNULLCondition = true;
    private string mColumn = null;
    private string[] mColumns = null;
    private int mSize = 1000;
    private string[] mOperators = new string[] { "LIKE", "NOT LIKE", "=", "<>" };

    #endregion


    #region "Public properties"

    /// <summary>
    /// Where condition.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            base.WhereCondition = GetCondition();
            return base.WhereCondition;
        }
        set
        {
            base.WhereCondition = value;
        }
    }


    /// <summary>
    /// Name of the column.
    /// </summary>
    public string Column
    {
        get
        {
            return mColumn;
        }
        set
        {
            mColumn = value;
        }
    }


    /// <summary>
    /// Use this property to search within multiple columns.
    /// </summary>
    public string[] Columns
    {
        get
        {
            return mColumns;
        }
        set
        {
            mColumns = value;
        }
    }


    /// <summary>
    /// Maximum length of the entered text.
    /// </summary>
    public int Size
    {
        get
        {
            return mSize;
        }
        set
        {
            mSize = value;
        }
    }


    /// <summary>
    /// Determines whether condition for NULL values is added when 'NOT LIKE'
    /// or '<>' operand is selected.
    /// </summary>
    public bool IncludeNULLCondition
    {
        get
        {
            return mIncludeNULLCondition;
        }
        set
        {
            mIncludeNULLCondition = value;
        }
    }


    /// <summary>
    /// Gets/sets current text in filter.
    /// </summary>
    public string FilterText
    {
        get
        {
            return txtText.Text;
        }
        set
        {
            txtText.Text = value;
        }
    }


    /// <summary>
    /// Gets current operator.
    /// </summary>
    public string FilterOperator
    {
        get
        {
            // Check that selected operator was not modified
            if (mOperators.Contains<string>(drpCondition.SelectedValue))
            {
                return drpCondition.SelectedValue;
            }
            else
            {
                return null;
            }
        }
    }


    /// <summary>
    /// Determines whether the filter is set.
    /// </summary>
    public bool FilterIsSet
    {
        get
        {
            return !string.IsNullOrEmpty(txtText.Text);
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        InitFilterDropDown(drpCondition);
        txtText.MaxLength = Size;
    }

    #endregion


    #region "Public methods"

    public string GetCondition()
    {
        // Avoid SQL injection
        string tempVal = txtText.Text.Trim().Replace("'", "''");

        string op = this.FilterOperator;

        // No condition
        if (String.IsNullOrEmpty(tempVal) || (String.IsNullOrEmpty(this.Column) && (this.Columns == null)) || String.IsNullOrEmpty(op))
        {
            return null;
        }

        // Support for exact phrase search
        if (op.Contains("LIKE"))
        {
            tempVal = "%" + tempVal + "%";
        }

        StringBuilder where = new StringBuilder();
        string additionalForNull = string.Empty;

        // Get where condition for multiple columns
        if (this.Columns != null)
        {
            foreach (string column in this.Columns)
            {
                // Handling "NOT LIKE" and "<>" operators
                if (this.IncludeNULLCondition)
                {
                    if (op.Contains("<") || op.Contains("NOT"))
                    {
                        additionalForNull = " OR " + column + " IS NULL";
                    }
                }

                if (!String.IsNullOrEmpty(where.ToString()))
                {
                    where.Append(" OR ");
                }
                where.Append("(" + column + " " + op + " N'" + tempVal + "' " + additionalForNull + ")");
            }
            return "(" + where.ToString() + ")";
        }
        // Get where condition for single column
        else
        {
            // Handling "NOT LIKE" and "<>" operators
            if (this.IncludeNULLCondition)
            {
                if (op.Contains("<") || op.Contains("NOT"))
                {
                    additionalForNull = " OR " + this.Column + " IS NULL";
                }
            }
            return "(" + this.Column + " " + op + " N'" + tempVal + "' " + additionalForNull + ")";
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes standard filter dropdown.
    /// </summary>
    /// <param name="drp">Dropdown to init</param>
    private void InitFilterDropDown(DropDownList drp)
    {
        if ((drp != null) && (drp.Items.Count <= 0))
        {
            // Add 'LIKE'
            drp.Items.Add(new ListItem(mOperators[0], mOperators[0]));
            // Add 'NOT LIKE'
            drp.Items.Add(new ListItem(mOperators[1], mOperators[1]));
            // Add '='
            drp.Items.Add(new ListItem(mOperators[2], mOperators[2]));
            // Add '<>'
            drp.Items.Add(new ListItem(mOperators[3], mOperators[3]));
        }
    }

    #endregion
}
