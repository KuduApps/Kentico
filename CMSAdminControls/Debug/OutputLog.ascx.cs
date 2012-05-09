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

public partial class CMSAdminControls_Debug_OutputLog : OutputLog
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
            // Get the output
            string output = ValidationHelper.GetString(this.Log.Value, null);
            if (!String.IsNullOrEmpty(output))
            {
                this.Visible = true;

                StringBuilder sb = new StringBuilder(output.Length + 200);
                int size = output.Length;
                
                if (LogStyle != "")
                {
                    sb.Append("<div style=\"padding: 2px; font-weight: bold; background-color: #eecccc; border-bottom: solid 1px #ff0000;\">");
                    sb.Append(GetString("OutputLog.Info"));
                    sb.Append("</div>");
                }

                // Size chart
                this.MaxSize = 102400;
                if (size > this.MaxSize)
                {
                    this.MaxSize = size;
                }

                sb.Append("<table><tr><td><strong>");
                sb.Append(GetSizeChart(size, 0, 0, 0));
                sb.Append("</td><td><strong>");
                sb.Append(DataHelper.GetSizeString(size));
                sb.Append("</strong></td></tr></table>");

                sb.Append("<div><textarea style=\"width: 100%; height: 530px;\" readonly=\"true\">");
                sb.Append(HTMLHelper.HTMLEncode(output));
                sb.Append("</textarea></div>");

                this.TotalSize = size;

                this.ltlLog.Text = sb.ToString();
            }
        }
    }
}
