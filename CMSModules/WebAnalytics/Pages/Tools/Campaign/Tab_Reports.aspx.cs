using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.SettingsProvider;

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_Tab_Reports : CMSWebAnalyticsPage
{
    #region "Variables"

    protected string mSave = null;
    protected string mPrint = null;
    protected string mDeleteData = null;
    private bool isSaved = false;
    private bool reportLoaded = false;
    string campaignName = String.Empty;
    CampaignInfo campaignInfo;

    private IDisplayReport ucDisplayReport = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ucDisplayReport = LoadControl("~/CMSModules/Reporting/Controls/DisplayReport.ascx") as IDisplayReport;
        pnlContent.Controls.Add((Control)ucDisplayReport);

        CurrentMaster.PanelContent.CssClass = "";
        UIHelper.AllowUpdateProgress = false;
        ScriptHelper.RegisterDialogScript(Page);

        // Campaign Info
        int campaignID = QueryHelper.GetInteger("campaignID", 0);
        campaignInfo = CampaignInfoProvider.GetCampaignInfo(campaignID);
        if (campaignInfo == null)
        {
            return;
        }

        // Validate SiteID for non administrators
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            if (campaignInfo.CampaignSiteID != CMSContext.CurrentSiteID)
            {
                RedirectToAccessDenied(GetString("cmsmessages.accessdenied"));
            }
        }

        campaignName = campaignInfo.CampaignName;

        // Text for menu
        mSave = GetString("general.save");
        mPrint = GetString("Analytics_Report.Print");
        mDeleteData = GetString("Analytics_Report.ManageData");

        // Images for menu buttons
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save_small.png");
        imgPrint.ImageUrl = GetImageUrl("General/printSmall.png");
        imgManageData.ImageUrl = GetImageUrl("CMSModules/CMS_Reporting/managedataSmall.png");

        // Check 'ManageData' permission
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageData"))
        {
            this.lnkDeleteData.OnClientClick = "modalDialog('" +
                ResolveUrl("~/CMSModules/Reporting/WebAnalytics/Analytics_ManageData.aspx") +
                    "?statcodename=singlecampaign;" + campaignName + "', 'AnalyticsManageData', " + AnalyticsHelper.MANAGE_WINDOW_WIDTH + ", " + AnalyticsHelper.MANAGE_WINDOW_HEIGHT + "); return false; ";
            this.lnkDeleteData.Visible = true;
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "myModalDialog",
        ScriptHelper.GetScript("function myModalDialog(url, name, width, height) { " +
            "win = window; " +
            "var dHeight = height; var dWidth = width; " +
            "if (( document.all )&&(navigator.appName != 'Opera')) { " +
            "try { win = wopener.window; } catch (e) {} " +
            "if ( parseInt(navigator.appVersion.substr(22, 1)) < 7 ) { dWidth += 4; dHeight += 58; }; " +
            "dialog = win.showModalDialog(url, this, 'dialogWidth:' + dWidth + 'px;dialogHeight:' + dHeight + 'px;resizable:yes;scroll:yes'); " +
            "} else { " +
            "oWindow = win.open(url, name, 'height=' + dHeight + ',width=' + dWidth + ',toolbar=no,directories=no,menubar=no,modal=yes,dependent=yes,resizable=yes,scroll=yes,scrollbars=yes'); oWindow.opener = this; oWindow.focus(); } } "));

        ucGraphType.ProcessChartSelectors(false);

        // Enables/disables radio buttons (based on UI elements)
        CurrentUserInfo ui = CMSContext.CurrentUser;
        bool checkedButton = false;

        if (!RequestHelper.IsPostBack())
        {
            if (!ui.IsGlobalAdministrator)
            {
                rbCount.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "CampaignConversionCount");
                rbDetail.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "CampaignDetails");
                rbGoalCount.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "goals.numberofconversions");
                rbGoalValue.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "goals.totalvalueofconversions");
                rbGoalView.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "goals.numberofvisitors");
                rbValue.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "campaignsConversionValue");
                rbValuePerVisitor.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "valuepervisitor");
                rbViews.Enabled = ui.IsAuthorizedPerUIElement("cms.WebAnalytics", "campaign.overview");

                // Check first enabled button 
                foreach (Control ctrl in pnlRadios.Controls)
                {
                    RadioButton rb = ctrl as RadioButton;
                    if (rb != null)
                    {
                        if (rb.Enabled)
                        {
                            rb.Checked = true;
                            checkedButton = true;
                            break;
                        }
                    }
                }

                // No report avaible -> redirect to access denied
                if (!checkedButton)
                {
                    RedirectToAccessDenied(GetString("campaign.noreportavaible"));
                }
            }
            else
            {
                // Admin check first radio button
                rbViews.Checked = true;
            }
        }
    }


    /// <summary>
    /// Display report
    /// </summary>
    /// <param name="reload">If true, display report control is reloaded</param>
    private void DisplayReport(bool reload)
    {
        if (reportLoaded)
        {
            return;
        }

        ucGraphType.ProcessChartSelectors(false);

        String siteName = CMSContext.CurrentSiteName;

        // Set repors name
        String views = "campaign.yearreport;campaign.monthreport;campaign.weekreport;campaign.dayreport;campaign.hourreport";
        String conversionCount = "campaignconversioncount.yearreport;campaignconversioncount.monthreport;campaignconversioncount.weekreport;campaignconversioncount.dayreport;campaignconversioncount.hourreport";
        String conversionValue = "campaignconversionvalue.yearreport;campaignconversionvalue.monthreport;campaignconversionvalue.weekreport;campaignconversionvalue.dayreport;campaignconversionvalue.hourreport";
        String details = "campaigns.singledetails";
        String visitorsGoal = "goalsnumberofvisitors.yearreport;goalsnumberofvisitors.monthreport;goalsnumberofvisitors.weekreport;goalsnumberofvisitors.dayreport;goalsnumberofvisitors.hourreport";
        String valuePerVisitor = "goalsvaluepervisit.yearreport;goalsvaluepervisit.monthreport;goalsvaluepervisit.weekreport;goalsvaluepervisit.dayreport;goalsvaluepervisit.hourreport";
        String valueGoal = "goalsvalueofconversions.yearreport;goalsvalueofconversions.monthreport;goalsvalueofconversions.weekreport;goalsvalueofconversions.dayreport;goalsvalueofconversions.hourreport";
        String countGoal = "goalsnumberofconversions.yearreport;goalsnumberofconversions.monthreport;goalsnumberofconversions.weekreport;goalsnumberofconversions.dayreport;goalsnumberofconversions.hourreport";

        String codeName = String.Empty;
        pnlConversions.Visible = false;
        ucGraphType.EnableDateTimePicker = true;

        if (rbViews.Checked)
        {
            CheckWebAnalyticsUI("campaign.overview");
            codeName = "campaign";
            ucDisplayReport.ReportName = ucGraphType.GetReportName(views);
        }

        if (rbCount.Checked)
        {
            CheckWebAnalyticsUI("CampaignConversionCount");
            pnlConversions.Visible = true;
            codeName = "campconversion";
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionCount);
        }

        if (rbValue.Checked)
        {
            CheckWebAnalyticsUI("campaignsConversionValue");
            pnlConversions.Visible = true;
            codeName = "campconversion";
            ucDisplayReport.ReportName = ucGraphType.GetReportName(conversionValue);
        }

        if (rbDetail.Checked)
        {
            CheckWebAnalyticsUI("CampaignDetails");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(details);
            ucGraphType.EnableDateTimePicker = false;
        }

        if (rbGoalView.Checked)
        {
            CheckWebAnalyticsUI("goals.numberofvisitors");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(visitorsGoal);
        }

        if (rbGoalCount.Checked)
        {
            CheckWebAnalyticsUI("goals.numberofconversions");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(countGoal);
        }

        if (rbGoalValue.Checked)
        {
            CheckWebAnalyticsUI("goals.totalvalueofconversions");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(valueGoal);
        }

        if (rbValuePerVisitor.Checked)
        {
            CheckWebAnalyticsUI("goals.valuepervisitor");
            ucDisplayReport.ReportName = ucGraphType.GetReportName(valuePerVisitor);
        }

        // Load conversions fit to campaign
        if ((pnlConversions.Visible) && (campaignInfo != null))
        {
            ucConversions.PostbackOnDropDownChange = true;
            if (!campaignInfo.CampaignUseAllConversions)
            {
                ucConversions.WhereCondition = "ConversionID  IN (SELECT ConversionID FROM Analytics_ConversionCampaign WHERE CampaignID =" + campaignInfo.CampaignID + ")";
            }

            ucConversions.WhereCondition = SqlHelperClass.AddWhereCondition(ucConversions.WhereCondition, "ConversionSiteID =" + CMSContext.CurrentSiteID);
            ucConversions.ReloadData(true);
        }

        String conversion = ValidationHelper.GetString(ucConversions.Value, String.Empty);
        if (conversion == ucConversions.AllRecordValue)
        {
            conversion = String.Empty;
        }

        // General report data
        ucDisplayReport.LoadFormParameters = false;
        ucDisplayReport.DisplayFilter = false;
        ucDisplayReport.GraphImageWidth = 100;
        ucDisplayReport.IgnoreWasInit = true;
        ucDisplayReport.TableFirstColumnWidth = Unit.Percentage(30);
        ucDisplayReport.UseExternalReload = true;
        ucDisplayReport.UseProgressIndicator = true;

        // Resolve report macros 
        DataTable dtp = new DataTable();
        dtp.Columns.Add("FromDate", typeof(DateTime));
        dtp.Columns.Add("ToDate", typeof(DateTime));
        dtp.Columns.Add("CodeName", typeof(string));
        dtp.Columns.Add("CampaignName", typeof(string));
        dtp.Columns.Add("ConversionName", typeof(string));

        object[] parameters = new object[5];
        parameters[0] = ucGraphType.From;
        parameters[1] = ucGraphType.To;
        parameters[2] = codeName;
        parameters[3] = campaignName;
        parameters[4] = conversion;

        dtp.Rows.Add(parameters);
        dtp.AcceptChanges();
        ucDisplayReport.ReportParameters = dtp.Rows[0];

        if (reload)
        {
            ucDisplayReport.ReloadData(true);
        }

        reportLoaded = true;
    }


    /// <summary>
    /// Handles lnkSave click event.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        Save();
    }


    protected override void OnPreRender(EventArgs e)
    {
        DisplayReport(true);
        lnkPrint.OnClientClick = "myModalDialog('" + ResolveUrl("~/CMSModules/Reporting/Tools/Analytics_Print.aspx") + "?reportname=" + ucDisplayReport.ReportName + "&parameters=" + HttpUtility.HtmlEncode(AnalyticsHelper.GetQueryStringParameters(ucDisplayReport.ReportParameters)) + "&UILang=" + System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag + "', 'PrintReport" + ucDisplayReport.ReportName + "', 800, 700); return false;";
        base.OnPreRender(e);
    }


    /// <summary>
    /// VerifyRenderingInServerForm.
    /// </summary>
    /// <param name="control">Control</param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        if (!isSaved)
        {
            base.VerifyRenderingInServerForm(control);
        }
    }


    /// <summary>
    /// Saves the graph report.
    /// </summary>
    private void Save()
    {
        // Check web analytics save permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "SaveReports"))
        {
            RedirectToCMSDeskAccessDenied("CMS.WebAnalytics", "SaveReports");
        }

        DisplayReport(false);

        // Saves the report        
        isSaved = true;

        if (ucDisplayReport.SaveReport() > 0)
        {
            lblInfo.Visible = true;
            lblInfo.Text = String.Format(GetString("Analytics_Report.ReportSavedTo"), ucDisplayReport.ReportDisplayName + " - " + DateTime.Now.ToString());
        }

        isSaved = false;
    }

    #endregion
}

