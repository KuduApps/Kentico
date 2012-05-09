using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Controls;
using CMS.SettingsProvider;
using CMS.GlobalHelper;

public partial class CMSModules_WebFarm_Controls_WebFarm_Task_Filter : CMSAbstractBaseFilterControl
{
    #region "Properties"

    /// <summary>
    /// Where condition.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            base.WhereCondition = GenerateWhereCondition();
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
        if (!URLHelper.IsPostback())
        {
            drpTaskStatus.Items.Add(new ListItem(ResHelper.GetString("general.selectall"), "all"));
            drpTaskStatus.Items.Add(new ListItem(ResHelper.GetString("webfarm.notprocessed"), "notprocessed"));
            drpTaskStatus.Items.Add(new ListItem(ResHelper.GetString("general.failed"), "failed"));
        }
    }


    /// <summary>
    /// Generates where condition.
    /// </summary>
    protected string GenerateWhereCondition()
    {
        string where = String.Empty;
        string status = drpTaskStatus.SelectedValue;

        switch (status)
        {
            case "notprocessed" :
                where = SqlHelperClass.AddWhereCondition(where, "ErrorMessage IS NULL");
                break;

            case "failed" :
                where = SqlHelperClass.AddWhereCondition(where, "ErrorMessage IS NOT NULL");
                break;
        }
        return where;
    }

    #endregion
}
