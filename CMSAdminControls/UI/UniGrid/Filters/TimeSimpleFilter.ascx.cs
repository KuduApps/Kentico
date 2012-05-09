using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_UniGrid_Filters_TimeSimpleFilter : CMSUserControl
{
    #region "Private variables"

    private string mColumn = null;
    private bool mShowTime = true;    

    #endregion


    #region "Public properties"


    /// <summary>
    /// Gets or sets the name of the column that is used to create filtering condition.
    /// </summary>
    [Browsable(true)]
    [Category("Data")]
    [Description("Determines the name of the column that is used to create filtering condition")]
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
    /// Enables/disables showing time.
    /// </summary>
    public bool ShowTime
    {
        get
        {
            return mShowTime;
        }
        set
        {
            mShowTime = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        dtmTimeFrom.EditTime = this.ShowTime;
        dtmTimeTo.EditTime = this.ShowTime;
    }


    #region "Public methods"

    public string GetCondition()
    {
        string resultCondition = null;

        // Make sure that fromTime precedes toTime
        DateTime fromTime = dtmTimeFrom.SelectedDateTime;
        DateTime toTime = dtmTimeTo.SelectedDateTime;
        if ((fromTime != DataHelper.DATETIME_NOT_SELECTED) && (toTime != DataHelper.DATETIME_NOT_SELECTED))
        {
            if (fromTime > toTime)
            {
                DateTime tmp = fromTime;
                fromTime = toTime;
                toTime = tmp;
            }
        }

        // Apply fromTime limit
        if (fromTime != DataHelper.DATETIME_NOT_SELECTED)
        {
            resultCondition = mColumn + " >= '" + fromTime.ToString("s") + "'";
        }


        // Apply toTime limit
        if (toTime != DataHelper.DATETIME_NOT_SELECTED)
        {
            if (!String.IsNullOrEmpty(resultCondition))
            {
                resultCondition += " AND ";
            }
            resultCondition += mColumn + " <= '" + toTime.ToString("s") + "'";
        }

        return resultCondition;
    }

    #endregion
}
