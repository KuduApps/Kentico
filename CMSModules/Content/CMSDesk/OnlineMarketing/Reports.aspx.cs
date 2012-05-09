using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;


[RegisterTitle("general.reports")]
public partial class CMSModules_Content_CMSDesk_OnlineMarketing_Reports : CMSAnalyticsContentPage
{
    private IDisplayReport ucDisplayReport;

    protected override void OnPreRender(EventArgs e)
    {
        CurrentUserInfo ui = CMSContext.CurrentUser;
        if (!ui.IsAuthorizedPerUIElement("CMS.Content", "Reports"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Reports");
        }

        // Check read for web analytics
        if (!ui.IsAuthorizedPerResource("cms.webanalytics", "Read"))
        {
            RedirectToAccessDenied(String.Format(GetString("general.permissionresource"), "Read", "Web analytics"));
        }

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName))
        {
            this.pnlWarning.Visible = true;
            this.lblWarning.Text = ResHelper.GetString("WebAnalytics.Disabled");
        }

        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlContent.Controls.Add((Control)ucDisplayReport);


        // Check read permission for node
        int nodeID = QueryHelper.GetInteger("nodeid", 0);
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeID, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture);

        if (ui.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
        {
            RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
        }

        ucGraphType.ProcessChartSelectors(false);
        CurrentMaster.PanelContent.CssClass = String.Empty;
        UIHelper.AllowUpdateProgress = false;

        // General report data
        ucDisplayReport.ReportName = rbContent.Checked ? "pagereports.content" : "pagereports.Traffic";
        ucDisplayReport.LoadFormParameters = false;
        ucDisplayReport.DisplayFilter = false;
        ucDisplayReport.GraphImageWidth = 100;
        ucDisplayReport.IgnoreWasInit = true;
        ucDisplayReport.TableFirstColumnWidth = Unit.Percentage(30);
        ucDisplayReport.UseExternalReload = true;
        ucDisplayReport.UseProgressIndicator = true;

        ucDisplayReport.SetDefaultDynamicMacros((int)ucGraphType.SelectedInterval);

        // Resolve report macros 
        DataTable dtp = new DataTable();
        dtp.Columns.Add("FromDate", typeof(DateTime));
        dtp.Columns.Add("ToDate", typeof(DateTime));
        dtp.Columns.Add("CodeName", typeof(string));
        dtp.Columns.Add("NodeID", typeof(int));
        dtp.Columns.Add("CultureCode", typeof(string));

        object[] parameters = new object[5];
        parameters[0] = ucGraphType.From;
        parameters[1] = ucGraphType.To;
        parameters[2] = "pageviews";
        parameters[3] = nodeID;
        parameters[4] = CMSContext.PreferredCultureCode;

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();
        ucDisplayReport.ReportParameters = dtp.Rows[0];

        ucDisplayReport.ReloadData(true);

        base.OnPreRender(e);
    }
}

