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
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Reporting_Tools_Category_Frameset : CMSReportingPage
{
    protected string categoryContentUrl = "Report_List.aspx?categoryid=";
    protected string categoryHeaderUrl = "Category_Header.aspx?categoryid=";

    protected void Page_Load(object sender, EventArgs e)
    {
        int categoryId = ValidationHelper.GetInteger(Request.QueryString["categoryid"], 0);
        if (ValidationHelper.GetInteger(Request.QueryString["saved"], 0) > 0)
        {
            categoryContentUrl +=  categoryId.ToString() + "&saved=1";
            categoryHeaderUrl += categoryId.ToString() + "&saved=1";
        }
        else
        {
            categoryContentUrl += categoryId.ToString();
            categoryHeaderUrl += categoryId.ToString();
        }
    }
}
