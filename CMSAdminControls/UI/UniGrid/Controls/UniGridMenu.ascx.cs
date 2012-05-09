using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_UniGrid_Controls_UniGridMenu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string menuId = ContextMenu.MenuID;
        string parentElemId = ContextMenu.ParentElementClientID;

        string parameterScript = "GetContextMenuParameter('" + menuId + "')";
        string hideProgress = "window.noProgress = true; if (window.HideIndicator) { HideIndicator(false); }";

        string actionPattern = "UG_Export_" + parentElemId + "('{0}', " + parameterScript + ");";

        // Initialize menu
        imgExcel.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/excelexport.png");
        lblExcel.Text = ResHelper.GetString("export.exporttoexcel");
        pnlExcel.Attributes.Add("onclick", hideProgress + string.Format(actionPattern, DataExportFormatEnum.XLSX));

        imgCSV.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/csvexport.png");
        lblCSV.Text = ResHelper.GetString("export.exporttocsv");
        pnlCSV.Attributes.Add("onclick", hideProgress + string.Format(actionPattern, DataExportFormatEnum.CSV));

        imgXML.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/xmlexport.png");
        lblXML.Text = ResHelper.GetString("export.exporttoxml");
        pnlXML.Attributes.Add("onclick", hideProgress + string.Format(actionPattern, DataExportFormatEnum.XML));

        imgAdvancedExport.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/advancedexport.png");
        lblAdvancedExport.Text = ResHelper.GetString("export.advancedexport");
        pnlAdvancedExport.Attributes.Add("onclick", string.Format(actionPattern, "advancedexport"));
    }
}