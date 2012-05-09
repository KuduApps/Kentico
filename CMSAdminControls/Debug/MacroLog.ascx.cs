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

public partial class CMSAdminControls_Debug_MacroLog : MacroLog
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

                gridMacros.Columns[1].HeaderText = GetString("MacroLog.Expression");
                gridMacros.Columns[2].HeaderText = GetString("MacroLog.Result");
                gridMacros.Columns[3].HeaderText = GetString("General.Context");

                if (LogStyle != "")
                {
                    this.ltlInfo.Text = "<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">" + GetString("MacroLog.Info") + "</div>";
                }

                gridMacros.DataSource = dt;
                gridMacros.DataBind();
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


    protected string GetBeginIndent(object ind)
    {
        int indent = ValidationHelper.GetInteger(ind, 0);
        string result = "";
        for (int i = 0; i < indent; i++)
        {
            result += "&gt;"; //"<div style=\"padding-left: 10px;\">";
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


    protected string GetExpression(object indent, object expression)
    {
        string result = null;

        bool main = (ValidationHelper.GetInteger(indent, 0) <= 0);
        if (main)
        {
            result += "<strong>";
        }
        else
        {
            result += "<span style=\"color: #888888;\">";
        }

        result += GetBeginIndent(indent);
        result += HttpUtility.HtmlEncode(ValidationHelper.GetString(expression, ""));

        if (main)
        {
            result += "</strong>";
        }
        else
        {
            result += "</span>";
        }

        result += GetEndIndent(indent);

        return result;
    }


    protected string GetContext(object indent, object context)
    {
        if (ValidationHelper.GetInteger(indent, 0) <= 0)
        {
            return GetContext(context);
        }

        return "";
    }


    protected string GetResult(object result, object ind)
    {
        if ((result == null) || (result == DBNull.Value))
        {
            return null;
        }

        string stringResult = "";

        bool main = ValidationHelper.GetInteger(ind, 0) <= 0;
        if (main)
        {
            stringResult = "<strong>";
        }
        else
        {
            stringResult += "<span style=\"color: #888888;\">";
        }

        stringResult += HttpUtility.HtmlEncode(ValidationHelper.GetString(result, ""));

        if (main)
        {
            stringResult += "</strong>";
        }
        else
        {
            stringResult += "</span>";
        }

        return stringResult;
    }
}
