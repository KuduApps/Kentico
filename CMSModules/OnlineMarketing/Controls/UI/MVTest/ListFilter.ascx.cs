using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;

public partial class CMSModules_OnlineMarketing_Controls_UI_MVTest_ListFilter : CMSAdminListControl
{
    #region "Variables"

    private QueryDataParameters mParameters = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return GetWhereCondition();
        }
    }


    /// <summary>
    /// Parameters used for from and to filter.
    /// </summary>
    public QueryDataParameters Parameters
    {
        get
        {
            return mParameters;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns where condition.
    /// </summary>
    private string GetWhereCondition()
    {
        String where = String.Empty;

        if (!dtpFrom.IsValidRange() || !dtpTo.IsValidRange())
        {
            lblError.Text = GetString("general.errorinvaliddatetimerange");
            lblError.Visible = true;
            return String.Empty;
        }

        where = SqlHelperClass.AddWhereCondition(where, tsfFrom.GetCondition());
        where = SqlHelperClass.AddWhereCondition(where, tsfSource.GetCondition());        

        // Create date time condition
        mParameters = new QueryDataParameters();
        
        DateTime from = dtpFrom.SelectedDateTime;
        DateTime to = dtpTo.SelectedDateTime;

        if (from != DateTimeHelper.ZERO_TIME)
        {
            where = SqlHelperClass.AddWhereCondition(where, "MVTestOpenFrom > @from");

            mParameters.Add("@from", from);
        }

        if (to != DateTimeHelper.ZERO_TIME)
        {
            where = SqlHelperClass.AddWhereCondition(where, "MVTestOpenTo < @to");

            mParameters.Add("@to", to);
        }  

        return where;
    }

    #endregion
}
