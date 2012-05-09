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
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Reporting_Tools_Report_Edit : CMSReportingPage
{
    protected string reportContentUrl = "Report_View.aspx?reportid=";
    protected string reportHeaderUrl = "Report_Header.aspx?reportid=";

    protected int categoryId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int reportId = ValidationHelper.GetInteger(Request.QueryString["reportid"], 0);
        categoryId = ValidationHelper.GetInteger(Request.QueryString["categoryId"], 0);

        if (ValidationHelper.GetInteger(Request.QueryString["saved"], 0) > 0)
        {
            reportContentUrl = "Report_General.aspx?reportid=" + reportId + "&saved=1&categoryId=" + categoryId;
            reportHeaderUrl += reportId.ToString() + "&saved=1&categoryId=" + categoryId;
        }
        else
        {
            reportContentUrl += reportId.ToString() + "&categoryId=" + categoryId;
            reportHeaderUrl += reportId.ToString() + "&categoryId=" + categoryId;
        }

        string script = @"function Refresh() {
  if (parent.frames['reportcategorytree']) {
     parent.frames['reportcategorytree'].location.href = '" + ResolveUrl(@"~\CMSModules\Reporting\Tools\ReportCategory_tree.aspx?reportid=" + reportId) + @"';
  }
}";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "refreshCategoryTree", ScriptHelper.GetScript(script));
    }
}
