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
using CMS.Reporting;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.UIControls;

public partial class CMSModules_Reporting_Tools_Report_Parameters : CMSReportingPage
{
    protected int reportId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Report General";
        CurrentMaster.BodyClass += " FieldEditorBody";

        // Get report ID from url
        reportId = ValidationHelper.GetInteger(Request.QueryString["reportID"], 0);

        // Get report from database
        ReportInfo ri = ReportInfoProvider.GetReportInfo(reportId);
        if (ri != null)
        {
            editor.FormDefinition = ri.ReportParameters;
        }

        // Initialize FieldEditor
        editor.OnAfterDefinitionUpdate += editor_OnAfterDefinitionUpdate;
        editor.Mode = FieldEditorModeEnum.General;
        editor.DisplayedControls = FieldEditorControlsEnum.Reports;
        
    }

    void editor_OnAfterDefinitionUpdate(object sender, EventArgs e)
    {
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }

        ReportInfo ri = ReportInfoProvider.GetReportInfo(reportId);
        if (ri != null)
        {
            // Get new report parameters changed by fieldeditor
            ri.ReportParameters = editor.FormDefinition;

            // Update report parameters in database
            ReportInfoProvider.SetReportInfo(ri);
        }
    }
}
