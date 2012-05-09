using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;


public partial class CMSModules_Reporting_Tools_ReportCategory_Edit_Frameset : CMSReportingPage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string categoryID = Request.QueryString["CategoryID"];
        frmHeader.Attributes.Add("src", "ReportCategoryEdit_Header.aspx?CategoryID=" + categoryID);
        frmGeneral.Attributes.Add("src", "Report_List.aspx?CategoryID=" + categoryID);
    }
}

