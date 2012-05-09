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

using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSAdminControls_Debug_CacheLog : CacheLog
{
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
                this.Visible = true;

                gridCache.Columns[1].HeaderText = GetString("CacheLog.Operation");
                gridCache.Columns[2].HeaderText = GetString("CacheLog.Data");
                gridCache.Columns[3].HeaderText = GetString("General.Context");

                if (LogStyle != "")
                {
                    this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("CacheLog.Info") + "</div>";
                }

                gridCache.DataSource = dt;
                gridCache.DataBind();
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
