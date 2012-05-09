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

public partial class CMSAdminControls_Debug_SecurityLog : SecurityLog
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
                // Check the duplicity
                if (!dt.Columns.Contains("Duplicit"))
                {
                    DataHelper.MarkDuplicitRows(dt, "Indent <= 0", "Duplicit", "UserName", "SecurityOperation", "Resource", "Name", "SiteName");
                }

                this.Visible = true;

                int index = 1;
                gridSec.Columns[index++].HeaderText = GetString("SecurityLog.UserName");
                gridSec.Columns[index++].HeaderText = GetString("SecurityLog.Operation");
                gridSec.Columns[index++].HeaderText = GetString("SecurityLog.Result");
                gridSec.Columns[index++].HeaderText = GetString("SecurityLog.Resource");
                gridSec.Columns[index++].HeaderText = GetString("SecurityLog.Name");
                gridSec.Columns[index++].HeaderText = GetString("SecurityLog.Site");
                gridSec.Columns[index++].HeaderText = GetString("General.Context");

                if (LogStyle != "")
                {
                    this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("SecurityLog.Info") + "</div>";
                }

                gridSec.DataSource = dt;
                gridSec.DataBind();
            }
        }
    }


    protected string GetIndex(object ind)
    {
        int indent = ValidationHelper.GetInteger(ind, 0);
        if (indent == 0)
        {
            return (++index).ToString();
        }
        else
        {
            return null;
        }
    }


    protected object GetUserName(object userName, object indent)
    {
        if (ValidationHelper.GetInteger(indent, 0) > 0)
        {
            return null;
        }

        return userName;
    }


    protected string GetBeginIndent(object ind)
    {
        int indent = ValidationHelper.GetInteger(ind, 0);
        string result = "";
        for (int i = 0; i < indent; i++)
        {
            result += "&gt;"; //"<div style=\"padding-left: 10px;\">";
        }

        if (indent > 0)
        {
            result += "&nbsp;";
        }

        return result;
    }


    protected string GetEndIndent(object ind)
    {
        int indent = ValidationHelper.GetInteger(ind, 0);
        string result = "";
        /*
        for (int i = 0; i < indent; i++)
        {
            result += "</div>";
        }*/

        return result;
    }
}
