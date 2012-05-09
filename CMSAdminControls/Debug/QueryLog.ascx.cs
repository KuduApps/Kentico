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
using System.Text;

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSAdminControls_Debug_QueryLog : QueryLog
{
    /// <summary>
    /// Total parameters size.
    /// </summary>
    public virtual long TotalParamSize
    {
        get;
        set;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.EnableViewState = false;
        this.Visible = true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.Visible = false;

        if (this.Log != null)
        {
            // Get the log table
            DataTable dt = this.Log.LogTable;
            if (!DataHelper.DataSourceIsEmpty(dt))
            {
                // Check the duplicity
                if (!dt.Columns.Contains("Duplicit"))
                {
                    DataHelper.MarkDuplicitRows(dt, null, "Duplicit", "QueryText", "QueryName", "QueryParameters", "QueryResults");
                }

                this.Visible = true;

                gridQueries.Columns[1].HeaderText = GetString("QueryLog.QueryText");
                //gridQueries.Columns[2].HeaderText = GetString("QueryLog.QueryParameters");
                gridQueries.Columns[2].HeaderText = GetString("General.Context");
                gridQueries.Columns[3].HeaderText = GetString("QueryLog.QueryDuration");

                if (LogStyle != "")
                {
                    this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("QueryLog.Info") + "</div>";
                }

                this.MaxDuration = DataHelper.GetMaximumValue<double>(dt, "QueryDuration");

                // Prepare maximum size
                this.MaxSize = DataHelper.GetMaximumValue<int>(dt, "QueryResultsSize");

                int paramSize = DataHelper.GetMaximumValue<int>(dt, "QueryParametersSize");
                if (paramSize > this.MaxSize)
                {
                    this.MaxSize = paramSize;
                }

                gridQueries.DataSource = dt;
                gridQueries.DataBind();
            }
        }
    }


    protected object GetIndex(object resultsSize, object parametersSize, object queryText)
    {
        if (queryText == DBNull.Value)
        {
            return null;
        }

        this.TotalSize += ValidationHelper.GetInteger(resultsSize, 0);
        this.TotalParamSize += ValidationHelper.GetInteger(parametersSize, 0);

        return ++index;
    }


    protected object GetConnectionString(object connectionString)
    {
        return String.Format("<img src=\"{0}\" title=\"{1}\" />", GetImageUrl("CMSModules/CMS_System/data.png"), connectionString);
    }
}
