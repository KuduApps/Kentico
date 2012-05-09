using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_ContextMenus_ReportContextMenu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string menuId = ContextMenu.MenuID;
        string parentElemId = ContextMenu.ParentElementClientID;

        string parameterScript = "GetContextMenuParameter('" + menuId + "')";
        string hideProgress = "if(window.HideIndicator) {HideIndicator(false); CM_Close('" + menuId + "', 0); }";
        string actionPattern = "Report_Export_" + parentElemId + "('{0}', " + parameterScript + ");";

        // Initialize menu
        imgExcel.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/excelexport.png");
        lblExcel.Text = imgExcel.AlternateText = ResHelper.GetString("export.exporttoexcel");
        pnlExcel.Attributes.Add("onclick", string.Format(actionPattern, DataExportFormatEnum.XLSX) + hideProgress);

        imgCSV.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/csvexport.png");
        lblCSV.Text = imgCSV.AlternateText = ResHelper.GetString("export.exporttocsv");
        pnlCSV.Attributes.Add("onclick", string.Format(actionPattern, DataExportFormatEnum.CSV) + hideProgress);

        imgXML.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/xmlexport.png");
        lblXML.Text = imgXML.AlternateText = ResHelper.GetString("export.exporttoxml");
        pnlXML.Attributes.Add("onclick", string.Format(actionPattern, DataExportFormatEnum.XML) + hideProgress);
    }
}
