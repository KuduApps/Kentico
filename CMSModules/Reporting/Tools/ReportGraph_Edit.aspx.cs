using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.Synchronization;
using CMS.ExtendedControls;

public partial class CMSModules_Reporting_Tools_ReportGraph_Edit : CMSReportingModalPage
{
    #region "Variables"

    protected ReportInfo reportInfo = null;
    protected ReportGraphInfo graphInfo = null;
    protected int graphId;
    private const string colorPickerPath = "~/CMSAdminControls/ColorPicker";
    bool newReport = false;

    #endregion


    #region "Properties"

    /// <summary>
    ///  Tab state (edit, preview, version)
    /// </summary>
    private int SelectedTab
    {
        get
        {
            return ValidationHelper.GetInteger(hdnTabState.Value, 0);
        }
        set
        {
            hdnTabState.Value = value.ToString();
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Test permission for query
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Reporting", "EditSQLQueries"))
        {
            txtQueryQuery.Enabled = false;
        }
        else
        {
            txtQueryQuery.Enabled = true;
        }

        versionList.OnAfterRollback += new EventHandler(versionList_onAfterRollback);

        // Own javascript tab change handling -> because tab control raises changetab after prerender - too late
        RegisterTabScript(hdnTabState.ClientID, tabControlElem);

        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'report_graph_properties')";
        tabs[1, 0] = GetString("general.preview");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'report_graph_properties');";

        tabControlElem.Tabs = tabs;
        tabControlElem.UsePostback = true;

        CurrentMaster.Title.TitleCssClass = "PageTitleHeader";
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "report_graph_properties";
        Title = "ReportGraph Edit";

        RegisterClientScript();

        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        btnOk.Text = GetString("General.OK");
        btnCancel.Text = GetString("General.Cancel");
        btnApply.Text = GetString("General.apply");

        int reportId = QueryHelper.GetInteger("reportid", 0);
        bool isPreview = QueryHelper.GetBoolean("preview", false);

        // If preview by URL -> select preview tab
        if (isPreview && !RequestHelper.IsPostBack())
        {
            SelectedTab = 1;
        }

        if (reportId > 0)
        {
            reportInfo = ReportInfoProvider.GetReportInfo(reportId);
        }

        // Must be valid reportid parameter
        if (PersistentEditedObject == null)
        {
            if (reportInfo != null)
            {
                string graphName = QueryHelper.GetString("itemname", "");

                // Try to load graphname from hidden field (adding new graph & preview)
                if (graphName == String.Empty)
                {
                    graphName = txtNewGraphHidden.Value;
                }

                if (ValidationHelper.IsIdentifier(graphName))
                {
                    PersistentEditedObject = ReportGraphInfoProvider.GetReportGraphInfo(reportInfo.ReportName + "." + graphName);
                    graphInfo = PersistentEditedObject as ReportGraphInfo;
                }
            }
        }
        else
        {
            graphInfo = PersistentEditedObject as ReportGraphInfo;
        }

        if (graphInfo != null)
        {
            CurrentMaster.Title.TitleText = GetString("Reporting_ReportGraph_Edit.TitleText");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportGraph/object.png");

            graphId = graphInfo.GraphID;

            if (ObjectVersionManager.DisplayVersionsTab(graphInfo))
            {
                tabs[2, 0] = GetString("objectversioning.tabtitle");
                tabs[2, 1] = "SetHelpTopic('helpTopic', 'objectversioning_general');";
                versionList.Object = graphInfo;
                versionList.IsLiveSite = false;
            }
        }
        else
        {
            CurrentMaster.Title.TitleText = GetString("Reporting_ReportGraph_Edit.NewItemTitleText");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportGraph/new.png");

            newReport = true;
        }

        if (!RequestHelper.IsPostBack())
        {
            // Fill border styles
            FillBorderStyle(drpLegendBorderStyle);
            FillBorderStyle(drpChartAreaBorderStyle);
            FillBorderStyle(drpPlotAreaBorderStyle);
            FillBorderStyle(drpSeriesBorderStyle);
            FillBorderStyle(drpSeriesLineBorderStyle);

            // Fill gradient styles
            FillGradientStyle(drpChartAreaGradient);
            FillGradientStyle(drpPlotAreaGradient);
            FillGradientStyle(drpSeriesGradient);

            // Fill axis's position
            FillPosition(drpYAxisTitlePosition);
            FillPosition(drpXAxisTitlePosition);
            FillTitlePosition(drpTitlePosition);

            // Fill legend's position
            FillLegendPosition(drpLegendPosition);

            // Fill font type
            FillChartType(drpChartType);

            // Fill Chart type controls
            FillBarType(drpBarDrawingStyle);
            FillStackedBarType(drpStackedBarDrawingStyle);
            FillPieType(drpPieDrawingStyle);
            FillDrawingDesign(drpPieDrawingDesign);
            FillLabelStyle(drpPieLabelStyle);
            FillPieRadius(drpPieDoughnutRadius);
            FillLineDrawingStyle(drpLineDrawingStyle);
            FillOrientation(drpBarOrientation);
            FillOrientation(drpBarStackedOrientation);
            FillBorderSkinStyle();

            // FillSymbos
            FillSymbols(drpSeriesSymbols);
            if (!newReport)
            {
                LoadData();
            }
            // Load default data
            else
            {
                chkExportEnable.Checked = true;

                // Set default values for some controls
                txtQueryNoRecordText.Text = GetString("attachmentswebpart.nodatafound");
                drpPieLabelStyle.SelectedValue = "Outside";
                drpPieDoughnutRadius.SelectedValue = "60";
                drpSeriesBorderStyle.SelectedValue = "Solid";
                drpSeriesLineBorderStyle.SelectedValue = "Solid";
                drpLegendBorderStyle.SelectedValue = "Solid";
                drpChartAreaBorderStyle.SelectedValue = "Solid";
                drpPlotAreaBorderStyle.SelectedValue = "None";

                txtSeriesLineBorderSize.Text = "2";
                txtSeriesBorderSize.Text = "1";
                txtChartAreaBorderSize.Text = "1";
                txtPlotAreaBorderSize.Text = "";
                txtLegendBorderSize.Text = "1";

                ucPlotAreaBorderColor.SelectedColor = "#bbbbbb";
                ucChartAreaBorderColor.SelectedColor = "#bbbbbb";
                ucLegendBorderColor.SelectedColor = "#000000";
                ucSeriesBorderColor.SelectedColor = "#000000";

                ucChartAreaPrBgColor.SelectedColor = "#FFFFFF";
                ucChartAreaSecBgColor.SelectedColor = "#d3dde7";
                drpChartAreaGradient.SelectedValue = "BottomTop";
                ucTitleColor.SelectedColor = "#000000";
                drpBorderSkinStyle.SelectedValue = "None";
                txtChartWidth.Text = "600";
                txtChartHeight.Text = "400";
                chkShowGrid.Checked = true;
                chkYAxisUseXSettings.Checked = true;
                ucXAxisTitleColor.SelectedColor = "#000000";
                ucYAxisTitleColor.SelectedColor = "#000000";
                drpSeriesSymbols.SelectedValue = "Circle";

                drpBarDrawingStyle.SelectedValue = "Cylinder";
                chkSeriesDisplayItemValue.Checked = true;
                txtXAxisInterval.Text = "1";
                chkXAxisSort.Checked = true;
                drpPieDrawingDesign.SelectedValue = "SoftEdge";
                drpStackedBarDrawingStyle.SelectedValue = "Cylinder";
            }
        }

        HideChartTypeControls(drpChartType.SelectedIndex);
        ucXAxisTitleFont.SetOnChangeAttribute("xAxixTitleFontChanged ()");

        ucLegendBgColor.SupportFolder = colorPickerPath;
        ucLegendBorderColor.SupportFolder = colorPickerPath;
        ucChartAreaPrBgColor.SupportFolder = colorPickerPath;
        ucChartAreaSecBgColor.SupportFolder = colorPickerPath;
        ucChartAreaBorderColor.SupportFolder = colorPickerPath;
        ucPlotAreSecBgColor.SupportFolder = colorPickerPath;
        ucSeriesPrBgColor.SupportFolder = colorPickerPath;
        ucSeriesLineColor.SupportFolder = colorPickerPath;
        ucSeriesSecBgColor.SupportFolder = colorPickerPath;
        ucPlotAreaPrBgColor.SupportFolder = colorPickerPath;
        ucPlotAreaBorderColor.SupportFolder = colorPickerPath;
        ucSeriesBorderColor.SupportFolder = colorPickerPath;
        ucTitleColor.SupportFolder = colorPickerPath;
        ucXAxisTitleColor.SupportFolder = colorPickerPath;
        ucYAxisTitleColor.SupportFolder = colorPickerPath;

        ucTitleFont.DefaultSize = 14;
        ucTitleFont.DefaultStyle = "bold";
        ucXAxisTitleFont.DefaultSize = 11;
        ucXAxisTitleFont.DefaultStyle = "bold";
        ucYAxisTitleFont.DefaultSize = 11;
        ucYAxisTitleFont.DefaultStyle = "bold";

        txtRotateX.Enabled = chkShowAs3D.Checked;
        txtRotateY.Enabled = chkShowAs3D.Checked;
        chkBarOverlay.Enabled = chkShowAs3D.Checked;

        if (drpPieDrawingStyle.SelectedValue != "Doughnut")
        {
            drpPieDoughnutRadius.Enabled = false;
        }
        else
        {
            drpPieDoughnutRadius.Enabled = true;
        }
    }


    /// <summary>
    /// PreRender event handler
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        ShowActionButtons();
        // State
        switch (SelectedTab)
        {
            // Edit
            case 0:
                pnlPreview.Visible = false;
                pnlVersions.Visible = false;
                DisplayEditControls(true);
                categoryList.Visible = true;
                break;

            // Preview
            case 1:
                if (Save(false))
                {
                    ShowPreview();
                }
                else
                {
                    SelectedTab = 0;
                }
                break;

            // Version
            case 2:
                // Color picker preview issue
                DisplayEditControls(false);
                categoryList.Visible = false;
                pnlPreview.Visible = false;
                if (graphInfo != null)
                {
                    pnlVersions.Visible = true;
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "SetTabHelpTopic", ScriptHelper.GetScript("SetHelpTopic('helpTopic', 'objectversioning_general');"));
                }
                HideActionButtons();
                break;
        }

        tabControlElem.SelectedTab = SelectedTab;

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);
        AddNoCacheTag();
        RegisterModalPageScripts();

        base.OnPreRender(e);
    }


    protected void tabControlElem_clicked(object sender, EventArgs e)
    {
        // Set tab's selectedtab not by Request param, but from property
        tabControlElem.SelectedTab = SelectedTab;
    }


    /// <summary>
    /// Delete all chart controls except the one set
    /// </summary>
    /// <param name="index"></param>
    private void HideChartTypeControls(int index)
    {
        // Use x axis
        if (chkYAxisUseXSettings.Checked)
        {
            YAxisTitleFontRow.HideRow = true;
            YAxisTitlePositionRow.HideRow = true;
            YAxisLabelFont.HideRow = true;
        }

        // Bar
        if (index != 0)
        {
            BarDrawingStyleRow.HideRow = true;
            BarOverlayRow.HideRow = true;
            BarOrientationRow.HideRow = true;
        }

        // Stacked bar
        if (index != 1)
        {
            StackedBarDrawingStyleRow.HideRow = true;
            StackedBar100ProcStacked.HideRow = true;
            BarStackedOrientationRow.HideRow = true;
        }

        // Pie bar
        if (index != 2)
        {
            PieDrawingStyleRow.HideRow = true;
            PieDrawingDesign.HideRow = true;
            PieLabelStyleRow.HideRow = true;
            PieDoughnutRadiusRow.HideRow = true;
            PieOtherValue.HideRow = true;
        }
        else
        {
            SeriesValuesAsPercent.HideRow = true;
        }

        // Line bar
        if (index != 3)
        {
            LineDrawingStyleRow.HideRow = true;
            SeriesLineBorderSizeRow.HideRow = true;
            SeriesLineBorderStyleRow.HideRow = true;
            SeriesLineColorRow.HideRow = true;
            SeriesSymbols.HideRow = true;
        }
        else
        {
            SeriesBorderColorRow.HideRow = true;
            SeriesBorderSizeRow.HideRow = true;
            SeriesGradientRow.HideRow = true;
            SeriesPrBgColorRow.HideRow = true;
            SeriesSecBgColorRow.HideRow = true;
            SeriesBorderStyleRow.HideRow = true;
        }

        if (index == 2)
        {
            ChartShowGridRow.HideRow = true;
        }
    }


    /// <summary>
    /// Convert old graph data to new one (if any)
    /// </summary>
    /// <param name="settings"></param>
    private void Convert(ReportCustomData settings)
    {
        // Smoothcurves
        bool smoothCurves = ValidationHelper.GetBoolean(settings["SmoothCurves"], false);
        if (smoothCurves == true)
        {
            drpLineDrawingStyle.SelectedValue = "SpLine";
        }

        // Fillcurves
        bool fillCurves = ValidationHelper.GetBoolean(settings["FillCurves"], false);
        if (fillCurves == true)
        {
            drpStackedBarDrawingStyle.SelectedValue = "Area";
        }

        // Verticalbars
        if (settings.ContainsColumn("VerticalBars"))
        {
            bool verticalBars = ValidationHelper.GetBoolean(settings["VerticalBars"], false);
            if (verticalBars)
            {
                drpBarOrientation.SelectedValue = "Vertical";
            }
            else
            {
                drpBarOrientation.SelectedValue = "Horizontal";
            }
        }

        // Some types of graph type
        string graphType = graphInfo.GraphType;
        if (graphType == "baroverlay")
        {
            chkBarOverlay.Checked = true;
            drpChartType.SelectedValue = "bar";
        }

        if (graphType == "barpercentage")
        {
            chkStacked.Checked = true;
            drpChartType.SelectedValue = "barstacked";
        }

        if (graphType == "barstacked")
        {
            chkStacked.Checked = false;
            drpChartType.SelectedValue = "barstacked";
        }

        // Legend
        int value = ValidationHelper.GetInteger(graphInfo.GraphLegendPosition, 100);
        string position = String.Empty;
        if (value != 100)
        {
            switch (value)
            {
                case 0:
                    position = "TopLeft";
                    break;

                case 1:
                    position = "TopLeft";
                    break;

                case 2:
                    position = "TopRight";
                    break;

                case 3:
                    position = "BottomLeft";
                    break;

                case 4:
                    position = "TopLeft";
                    chkLegendInside.Checked = true;
                    break;

                case 5:
                    position = "TopRight";
                    chkLegendInside.Checked = true;
                    break;

                case 6:
                    position = "BottomLeft";
                    chkLegendInside.Checked = true;
                    break;

                case 7:
                    position = "BottomRight";
                    chkLegendInside.Checked = true;
                    break;

                case 8:
                    position = "TopLeft";
                    break;

                case 9:
                    position = "Top";
                    break;

                case 10:
                    position = "Bottom";
                    break;

                case 11:
                    position = "TopLeft";
                    break;

                case 12:
                    position = "BottomLeft";
                    break;

                case -1:
                    position = "None";
                    break;

                default:
                    position = "None";
                    break;
                    ;

            }
            drpLegendPosition.SelectedValue = position;
        }
        // If old x axis font defined set same y axis
        if (settings.ContainsColumn("axisFont"))
        {
            chkYAxisUseXSettings.Checked = true;
            ucYAxisTitleFont.Value = ucXAxisTitleFont.Value;
            drpYAxisTitlePosition.SelectedValue = drpYAxisTitlePosition.SelectedValue;
        }

        // Convert displayItemValue        
        if (settings.ContainsColumn("DisplayItemValue"))
        {
            // Conversion - if old graph was bar type set this value to true otherwise set it to false
            string chartType = graphInfo.GraphType.ToLower();
            if ((chartType == "bar") || (chartType == "barstacked") || (chartType == "barpercentage") || (chartType == "baroverlay"))
            {

                chkSeriesDisplayItemValue.Checked = true;
            }
            else
            {
                chkSeriesDisplayItemValue.Checked = false;
            }
        }
    }


    /// <summary>
    /// Loads data from graph storage
    /// </summary>
    private void LoadData()
    {
        if (graphInfo != null)
        {
            txtDefaultName.Text = graphInfo.GraphDisplayName;
            txtDefaultCodeName.Text = graphInfo.GraphName;
            txtQueryQuery.Text = graphInfo.GraphQuery;
            chkIsStoredProcedure.Checked = graphInfo.GraphQueryIsStoredProcedure;
            drpChartType.SelectedValue = graphInfo.GraphType;

            if (ValidationHelper.GetInteger(graphInfo.GraphHeight, 0) == 0)
            {
                txtChartHeight.Text = "";
            }
            else
            {
                txtChartHeight.Text = ValidationHelper.GetString(graphInfo.GraphHeight.ToString(), "");
            }
            if (ValidationHelper.GetInteger(graphInfo.GraphWidth, 0) == 0)
            {
                txtChartWidth.Text = "";
            }
            else
            {
                txtChartWidth.Text = ValidationHelper.GetString(graphInfo.GraphWidth.ToString(), "");
            }
            txtGraphTitle.Text = graphInfo.GraphTitle;
            txtXAxisTitle.Text = graphInfo.GraphXAxisTitle;
            txtYAxisTitle.Text = graphInfo.GraphYAxisTitle;

            // Load Custom settings
            ReportCustomData settings = graphInfo.GraphSettings;

            // Export
            chkExportEnable.Checked = ValidationHelper.GetBoolean(settings["ExportEnabled"], false);

            // Chart Type
            chkShowGrid.Checked = ValidationHelper.GetBoolean(settings["ShowMajorGrid"], false);

            drpBarDrawingStyle.SelectedValue = settings["BarDrawingStyle"];

            chkBarOverlay.Checked = ValidationHelper.GetBoolean(settings["BarOverlay"], false);
            drpBarOrientation.SelectedValue = settings["BarOrientation"];
            drpBarStackedOrientation.SelectedValue = settings["BarOrientation"];

            drpStackedBarDrawingStyle.SelectedValue = settings["StackedBarDrawingStyle"];
            chkStacked.Checked = ValidationHelper.GetBoolean(settings["StackedBarMaxStacked"], false);

            drpPieDrawingStyle.SelectedValue = settings["PieDrawingStyle"];
            drpPieDrawingDesign.SelectedValue = settings["PieDrawingDesign"];
            drpPieLabelStyle.SelectedValue = settings["PieLabelStyle"];
            drpPieDoughnutRadius.SelectedValue = ValidationHelper.GetString(settings["PieDoughnutRadius"], "60");
            txtPieOtherValue.Text = ValidationHelper.GetString(settings["PieOtherValue"], String.Empty);

            drpLineDrawingStyle.SelectedValue = settings["LineDrawinStyle"];

            chkStacked.Checked = ValidationHelper.GetBoolean(settings["StackedBarMaxStacked"], false);
            chkShowAs3D.Checked = ValidationHelper.GetBoolean(settings["ShowAs3D"], false);

            txtRotateX.Text = settings["RotateX"];
            txtRotateY.Text = settings["RotateY"];

            // Title
            ucTitleFont.Value = settings["TitleFontNew"];
            drpTitlePosition.SelectedValue = settings["TitlePosition"];
            ucTitleColor.SelectedColor = settings["TitleColor"];

            // Legend
            ucLegendBgColor.SelectedColor = settings["LegendBgColor"];
            ucLegendBorderColor.SelectedColor = ValidationHelper.GetString(settings["LegendBorderColor"], "#000000");
            txtLegendBorderSize.Text = ValidationHelper.GetString(settings["LegendBorderSize"], "1");
            drpLegendBorderStyle.SelectedValue = ValidationHelper.GetString(settings["LegendBorderStyle"], "Solid");
            drpLegendPosition.SelectedValue = settings["LegendPosition"];
            chkLegendInside.Checked = ValidationHelper.GetBoolean(settings["LegendInside"], false);
            txtLegendTitle.Text = settings["LegendTitle"];

            // XAxis
            ucXAxisTitleFont.Value = settings["XAxisFont"];
            drpXAxisTitlePosition.SelectedValue = settings["XAxisTitlePosition"];
            txtXAxisAngle.Text = settings["XAxisAngle"];
            txtXAxisInterval.Text = settings["XAxisInterval"];
            chkXAxisSort.Checked = ValidationHelper.GetBoolean(settings["xaxissort"], false);
            ucXAxisLabelFont.Value = settings["XAxisLabelFont"];
            ucXAxisTitleColor.SelectedColor = settings["XAxisTitleColor"];
            txtXAxisFormat.Text = settings["XAxisFormat"];

            // YAxis
            chkYAxisUseXSettings.Checked = ValidationHelper.GetBoolean(settings["YAxisUseXAxisSettings"], false);
            txtYAxisAngle.Text = settings["YAxisAngle"];
            ucYAxisTitleColor.SelectedColor = settings["YAxisTitleColor"];
            txtYAxisFormat.Text = settings["YAxisFormat"];

            if (chkYAxisUseXSettings.Checked)
            {
                ucYAxisTitleFont.Value = ucXAxisTitleFont.Value;
                ucYAxisLabelFont.Value = ucXAxisLabelFont.Value;
                drpYAxisTitlePosition.SelectedValue = drpXAxisTitlePosition.SelectedValue;
            }
            else
            {
                ucYAxisTitleFont.Value = graphInfo.GraphSettings["YAxisFont"];
                ucYAxisLabelFont.Value = graphInfo.GraphSettings["YAxisLabelFont"];
                drpYAxisTitlePosition.SelectedValue = graphInfo.GraphSettings["YAxisTitlePosition"];
            }

            // Chart Area
            ucChartAreaPrBgColor.SelectedColor = settings["ChartAreaPrBgColor"];
            ucChartAreaSecBgColor.SelectedColor = settings["ChartAreaSecBgColor"];
            drpChartAreaGradient.SelectedValue = settings["ChartAreaGradient"];
            ucChartAreaBorderColor.SelectedColor = ValidationHelper.GetString(settings["ChartAreaBorderColor"], "#000000");
            txtChartAreaBorderSize.Text = ValidationHelper.GetString(settings["ChartAreaBorderSize"], "1");
            drpChartAreaBorderStyle.SelectedValue = ValidationHelper.GetString(settings["ChartAreaBorderStyle"], "Solid");
            txtChartAreaBorderSize.Text = settings["ChartAreaBorderSize"];
            txtScaleMin.Text = settings["ScaleMin"];
            txtScaleMax.Text = settings["ScaleMax"];
            chkTenPowers.Checked = ValidationHelper.GetBoolean(settings["TenPowers"], false);
            chkReverseY.Checked = ValidationHelper.GetBoolean(settings["ReverseYAxis"], false);
            drpBorderSkinStyle.SelectedValue = ValidationHelper.GetString(settings["BorderSkinStyle"], "None");
            txtQueryNoRecordText.Text = settings["QueryNoRecordText"];

            // Plot area
            drpPlotAreaGradient.SelectedValue = settings["PlotAreaGradient"];
            ucPlotAreaPrBgColor.SelectedColor = settings["PlotAreaPrBgColor"];
            ucPlotAreSecBgColor.SelectedColor = settings["PlotAreaSecBgColor"];
            ucPlotAreaBorderColor.SelectedColor = ValidationHelper.GetString(settings["PlotAreaBorderColor"], "#000000");
            txtPlotAreaBorderSize.Text = ValidationHelper.GetString(settings["PlotAreaBorderSize"], "1");
            drpPlotAreaBorderStyle.SelectedValue = ValidationHelper.GetString(settings["PlotAreaBorderStyle"], "Solid");

            // Series 
            ucSeriesPrBgColor.SelectedColor = settings["SeriesPrBgColor"];
            ucSeriesSecBgColor.SelectedColor = settings["SeriesSecBgColor"];
            drpSeriesGradient.SelectedValue = settings["SeriesGradient"];
            drpSeriesSymbols.SelectedValue = settings["SeriesSymbols"];
            ucSeriesBorderColor.SelectedColor = ValidationHelper.GetString(settings["SeriesBorderColor"], "#000000");
            txtSeriesBorderSize.Text = ValidationHelper.GetString(settings["SeriesBorderSize"], "1");
            drpSeriesBorderStyle.SelectedValue = ValidationHelper.GetString(settings["SeriesBorderStyle"], "Solid");
            chkSeriesDisplayItemValue.Checked = ValidationHelper.GetBoolean(settings["DisplayItemValue"], false);
            txtItemValueFormat.Text = settings["ItemValueFormat"];
            txtSeriesItemTooltip.Text = settings["SeriesItemToolTip"];
            txtSeriesItemLink.Text = settings["SeriesItemLink"];
            chkValuesAsPercent.Checked = ValidationHelper.GetBoolean(settings["ValuesAsPercent"], false);

            ucSeriesLineColor.SelectedColor = settings["SeriesPrBgColor"];
            txtSeriesLineBorderSize.Text = ValidationHelper.GetString(settings["SeriesBorderSize"], "1");
            drpSeriesLineBorderStyle.SelectedValue = ValidationHelper.GetString(settings["SeriesBorderStyle"], "Solid");

            // Try to convert old data
            Convert(settings);
        }
    }




    /// <summary>
    /// Fills drop down list with border style enum
    /// </summary>
    /// <param name="drp">Data drop down list</param>
    private void FillBorderStyle(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.notset"), "NotSet"));
        drp.Items.Add(new ListItem(GetString("rep.graph.solid"), "Solid"));
        drp.Items.Add(new ListItem(GetString("rep.graph.dash"), "Dash"));
        drp.Items.Add(new ListItem(GetString("rep.graph.dashdot"), "DashDot"));
        drp.Items.Add(new ListItem(GetString("rep.graph.dashdotdot"), "DashDotDot"));
        drp.Items.Add(new ListItem(GetString("rep.graph.dot"), "Dot"));
    }


    /// <summary>
    /// Fills drop down list with usable gradient style
    /// </summary>
    /// <param name="drp">Drop down list</param>
    private void FillGradientStyle(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.none"), "None"));
        drp.Items.Add(new ListItem(GetString("rep.graph.leftright"), "LeftRight"));
        drp.Items.Add(new ListItem(GetString("rep.graph.diagonalleft"), "DiagonalLeft"));
        drp.Items.Add(new ListItem(GetString("rep.graph.topbottom"), "TopBottom"));
        drp.Items.Add(new ListItem(GetString("rep.graph.diagonalright"), "DiagonalRight"));
        drp.Items.Add(new ListItem(GetString("rep.graph.rightleft"), "RightLeft"));
        drp.Items.Add(new ListItem(GetString("rep.graph.leftdiagonal"), "LeftDiagonal"));
        drp.Items.Add(new ListItem(GetString("rep.graph.bottomtop"), "BottomTop"));
        drp.Items.Add(new ListItem(GetString("rep.graph.rightdiagonal"), "RightDiagonal"));
    }


    /// <summary>
    /// Fills drop down list with usable position for axe titles
    /// </summary>
    /// <param name="drp">Drop down list</param>
    private void FillPosition(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.center"), "Center"));
        drp.Items.Add(new ListItem(GetString("rep.graph.near"), "Near"));
        drp.Items.Add(new ListItem(GetString("rep.graph.far"), "Far"));
    }


    /// <summary>
    /// Fills title position
    /// </summary>
    /// <param name="drp">Title position drop down</param>
    private void FillTitlePosition(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.center"), "Center"));
        drp.Items.Add(new ListItem(GetString("rep.graph.right"), "Right"));
        drp.Items.Add(new ListItem(GetString("rep.graph.left"), "Left"));
    }


    /// <summary>
    /// Fills legend position 
    /// </summary>
    private void FillLegendPosition(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.none"), "None"));
        drp.Items.Add(new ListItem(GetString("rep.graph.top"), "Top"));
        drp.Items.Add(new ListItem(GetString("rep.graph.left"), "Left"));
        drp.Items.Add(new ListItem(GetString("rep.graph.right"), "Right"));
        drp.Items.Add(new ListItem(GetString("rep.graph.bottom"), "Bottom"));
        drp.Items.Add(new ListItem(GetString("rep.graph.topleft"), "TopLeft"));
        drp.Items.Add(new ListItem(GetString("rep.graph.topright"), "TopRight"));
        drp.Items.Add(new ListItem(GetString("rep.graph.bottomleft"), "BottomLeft"));
        drp.Items.Add(new ListItem(GetString("rep.graph.bottomright"), "BottomRight"));
    }

    /// <summary>
    /// Fills chart type drop down 
    /// </summary>
    /// <param name="drp">Data drop down list</param>
    public void FillChartType(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.barchart"), "bar"));
        drp.Items.Add(new ListItem(GetString("rep.graph.barstackedchart"), "barstacked"));
        drp.Items.Add(new ListItem(GetString("rep.graph.piechart"), "pie"));
        drp.Items.Add(new ListItem(GetString("rep.graph.linechart"), "line"));
    }


    /// <summary>
    /// Fills drawing style for bar charts
    /// </summary>
    /// <param name="drp"></param>
    private void FillBarType(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.bar"), "Bar"));
        drp.Items.Add(new ListItem(GetString("rep.graph.cylinder"), "Cylinder"));
    }


    /// <summary>
    /// Fills drawing style for stacked bar charts
    /// </summary>
    /// <param name="drp">Stacked bar data drop down list</param>
    private void FillStackedBarType(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.bar"), "Bar"));
        drp.Items.Add(new ListItem(GetString("rep.graph.cylinder"), "Cylinder"));
        drp.Items.Add(new ListItem(GetString("rep.graph.area"), "Area"));
    }


    /// <summary>
    /// Fills drawing style for pie charts
    /// </summary>
    /// <param name="drp">Pie type data drop down list</param>
    private void FillPieType(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.pie"), "Pie"));
        drp.Items.Add(new ListItem(GetString("rep.graph.doughnut"), "Doughnut"));
    }


    /// <summary>
    /// Fills drawing design for pie charts
    /// </summary>
    /// <param name="drp">Drawing design drop down list</param>
    private void FillDrawingDesign(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.none"), "Default"));
        drp.Items.Add(new ListItem(GetString("rep.graph.softedge"), "SoftEdge"));
        drp.Items.Add(new ListItem(GetString("rep.graph.concave"), "Concave"));
    }


    /// <summary>
    /// Fills label style for pie charts
    /// </summary>
    /// <param name="drp">Label style drop down list</param>
    private void FillLabelStyle(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.inside"), "Inside"));
        drp.Items.Add(new ListItem(GetString("rep.graph.outside"), "Outside"));
        drp.Items.Add(new ListItem(GetString("rep.graph.none"), "Disabled"));
    }


    /// <summary>
    /// Fills drawing style for line charts
    /// </summary>
    /// <param name="drp">Line drawing style drop down list</param>
    private void FillLineDrawingStyle(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.line"), "Line"));
        drp.Items.Add(new ListItem(GetString("rep.graph.spline"), "SpLine"));
    }


    /// <summary>
    /// Fills drawing style for pie charts
    /// </summary>
    /// <param name="drp">Pie radius drop down list</param>
    private void FillPieRadius(DropDownList drp)
    {
        drp.Items.Add(new ListItem("20", "20"));
        drp.Items.Add(new ListItem("30", "30"));
        drp.Items.Add(new ListItem("40", "40"));
        drp.Items.Add(new ListItem("50", "50"));
        drp.Items.Add(new ListItem("60", "60"));
        drp.Items.Add(new ListItem("70", "70"));
    }


    /// <summary>
    /// Fill bar chart orientation
    /// </summary>
    /// <param name="drp">Orientation drop down list</param>
    private void FillOrientation(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.vertical"), "Vertical"));
        drp.Items.Add(new ListItem(GetString("rep.graph.horizontal"), "Horizontal"));
    }


    /// <summary>
    /// Fills border skin style
    /// </summary>
    private void FillBorderSkinStyle()
    {
        drpBorderSkinStyle.Items.Add(new ListItem(GetString("general.none"), "None"));
        drpBorderSkinStyle.Items.Add(new ListItem(GetString("rep.graph.emboss"), "Emboss"));
        drpBorderSkinStyle.Items.Add(new ListItem(GetString("rep.graph.raised"), "Raised"));
        drpBorderSkinStyle.Items.Add(new ListItem(GetString("rep.graph.sunken"), "Sunken"));
    }


    /// <summary>
    /// Fills symbols for charts
    /// </summary>
    /// <param name="drp">Symbols drop down list</param>
    private void FillSymbols(DropDownList drp)
    {
        drp.Items.Add(new ListItem(GetString("rep.graph.none"), "None"));
        drp.Items.Add(new ListItem(GetString("rep.graph.square"), "Square"));
        drp.Items.Add(new ListItem(GetString("rep.graph.diamond"), "Diamond"));
        drp.Items.Add(new ListItem(GetString("rep.graph.triangle"), "Triangle"));
        drp.Items.Add(new ListItem(GetString("rep.graph.circle"), "Circle"));
        drp.Items.Add(new ListItem(GetString("rep.graph.cross"), "Cross"));
        drp.Items.Add(new ListItem(GetString("rep.graph.star4"), "Star4"));
    }


    /// <summary>
    /// Register javascript
    /// </summary>
    private void RegisterClientScript()
    {
        RegisterResizeAndRollbackScript(divFooter.ClientID, divScrolable.ClientID);

        string script = @"

            function checkXAxisSettings () {  
            if (document.getElementById ('" + chkYAxisUseXSettings.ClientID + @"').checked) { 
            document.getElementById('" + YAxisTitleFontRow.RowClientId + @"').style.display ='none'; 
            document.getElementById('" + YAxisTitlePositionRow.RowClientId + @"').style.display ='none'; 
            document.getElementById('" + YAxisLabelFont.RowClientId + @"').style.display ='none'; 
                    document.getElementById('" + drpYAxisTitlePosition.ClientID + @"').value =  
                    document.getElementById('" + drpXAxisTitlePosition.ClientID + @"').value;  
                    document.getElementById('" + ucYAxisLabelFont.FontTypeTextBoxClientId + @"').value =  document.getElementById('" + ucXAxisLabelFont.FontTypeTextBoxClientId + @"').value;  
                    document.getElementById('" + ucYAxisTitleFont.FontTypeTextBoxClientId + @"').value = document.getElementById('" + ucXAxisTitleFont.FontTypeTextBoxClientId + @"').value;}
                else  {   document.getElementById('" + YAxisTitleFontRow.RowClientId + @"').style.display =''; 
                          document.getElementById('" + YAxisTitlePositionRow.RowClientId + @"').style.display ='';
                          document.getElementById('" + YAxisLabelFont.RowClientId + @"').style.display ='';            
            } 
      }";

        script += @"function hideAllChartTypeControls () {
                   $j('.Bar').css('display','none');
                   $j('.StackedBar').css('display','none');
                   $j('.Pie').css('display','none');
                   $j('.Line').css('display','none');
                   $j('.Common').css('display','none');
                   $j('.Grid').css('display','none');
                }

                function showAs3DClicked () {
                    if (document.getElementById ('" + chkShowAs3D.ClientID + @"').checked == false) {
                        document.getElementById ('" + txtRotateX.ClientID + @"').disabled = true;
                        document.getElementById ('" + txtRotateY.ClientID + @"').disabled = true;
                        document.getElementById ('" + chkBarOverlay.ClientID + @"').disabled = true;
                    }
                    else {
                        document.getElementById ('" + txtRotateX.ClientID + @"').disabled = false;
                        document.getElementById ('" + txtRotateY.ClientID + @"').disabled = false;
                        document.getElementById ('" + chkBarOverlay.ClientID + @"').disabled = false;
}
                }

            function pieStyleChanged () { 
                    if (document.getElementById ('" + drpPieDrawingStyle.ClientID + @"').value == 'Doughnut') {
                        document.getElementById ('" + drpPieDoughnutRadius.ClientID + @"').disabled = false;    
                    }
                    else {
                        document.getElementById ('" + drpPieDoughnutRadius.ClientID + @"').disabled = true;    
                    }
            }
                   function typeChanged () {
                            var value=document.getElementById ('" + drpChartType.ClientID + @"').selectedIndex;
                            hideAllChartTypeControls ();
                            switch (value) {
                                case 0: $j('.Bar').css('display','');
                                        $j('.Common').css('display','');
                                        $j('.Grid').css('display','');
                                break;
                                case 1: $j('.StackedBar').css('display','');
                                        $j('.Common').css('display','');
                                        $j('.Grid').css('display','');
                                    break;
                                case 2: $j('.Pie').css('display','');
                                        $j('.Common').css('display','');                                        
                                    break;
                                case 3: $j('.Line').css('display','');
                                        $j('.Grid').css('display','');
                                    break;
                            }                                          
                }
                
        ";

        drpChartType.Attributes["onChange"] = "typeChanged()";
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(Page), "YAxisScript", ScriptHelper.GetScript(script));
    }


    /// <summary>
    /// Check if control contains valid non negative integer
    /// </summary>
    /// <param name="txtControl">Text control</param>
    /// <param name="errorMessage">Error message text from previous controls</param>
    /// <param name="row">Row with textbox control</param>    
    private string ValidateNonNegativeIntegerValue(TextBox txtControl, string errorMessage, CategoryPanelRow row)
    {
        if ((txtControl.Text.Trim() != String.Empty) && ((!ValidationHelper.IsInteger(txtControl.Text.Trim())) || (Int32.Parse(txtControl.Text.Trim()) < 0)))
        {
            row.ErrorMessage = "rep.invalidnonnegativeinteger";
            return GetString("rep.invaliddata");
        }
        return errorMessage;
    }


    /// <summary>
    /// Check if control contains valid integer number
    /// </summary>
    /// <param name="txtControl">Text control</param>
    /// <param name="errorMessage">Error message text from previous controls</param>
    /// <param name="row">Row with textbox control</param> 
    private string ValidateIntegerValue(TextBox txtControl, string errorMessage, CategoryPanelRow row)
    {
        if ((txtControl.Text.Trim() != String.Empty) && (!ValidationHelper.IsInteger(txtControl.Text.Trim())))
        {
            row.ErrorMessage = "rep.invalidinteger";
            return GetString("rep.invaliddata");
        }
        return errorMessage;
    }


    /// <summary>
    /// Save the changes to DB
    /// </summary>
    /// <param name="save">If false, data is not stored to DB, only info object is filled</param>
    private bool Save(bool save)
    {
        string errorMessage = String.Empty;
        if (save)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
            {
                RedirectToAccessDenied("cms.reporting", "Modify");
            }

            errorMessage = new Validator()
                .NotEmpty(txtDefaultName.Text, rfvDisplayName.ErrorMessage)
                .NotEmpty(txtDefaultCodeName.Text, rfvCodeName.ErrorMessage)
                .NotEmpty(txtQueryQuery.Text, GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;

            if ((errorMessage == "") && (!ValidationHelper.IsIdentifier(txtDefaultCodeName.Text.Trim())))
            {
                errorMessage = GetString("general.erroridentificatorformat");
            }

            string fullName = reportInfo.ReportName + "." + txtDefaultCodeName.Text.Trim();
            ReportGraphInfo codeNameCheck = ReportGraphInfoProvider.GetReportGraphInfo(fullName);
            if ((errorMessage == "") && (codeNameCheck != null) && (codeNameCheck.GraphID != graphId))
            {
                errorMessage = GetString("Reporting_ReportGraph_Edit.ErrorCodeNameExist");
            }
        }

        if (errorMessage == String.Empty)
        {
            // Test query in all cases        
            errorMessage = new Validator().NotEmpty(txtQueryQuery.Text.Trim(), GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;

            // Test valid integers 
            errorMessage = ValidateNonNegativeIntegerValue(txtChartWidth, errorMessage, ChartWidthRow);
            errorMessage = ValidateNonNegativeIntegerValue(txtChartHeight, errorMessage, ChartHeightRow);

            errorMessage = ValidateIntegerValue(txtRotateX, errorMessage, RotateXRow);
            errorMessage = ValidateIntegerValue(txtRotateY, errorMessage, RotateYRow);

            errorMessage = ValidateIntegerValue(txtXAxisAngle, errorMessage, XAxisAngleRow);
            errorMessage = ValidateIntegerValue(txtYAxisAngle, errorMessage, YAxisAngleRow);
            errorMessage = ValidateNonNegativeIntegerValue(txtXAxisInterval, errorMessage, XAxisInterval);

            errorMessage = ValidateIntegerValue(txtScaleMin, errorMessage, ChartAreaScaleMin);
            errorMessage = ValidateIntegerValue(txtScaleMax, errorMessage, ChartAreaScaleMax);

            errorMessage = ValidateNonNegativeIntegerValue(txtChartAreaBorderSize, errorMessage, ChartAreaBorderSizeRow);
            errorMessage = ValidateNonNegativeIntegerValue(txtSeriesBorderSize, errorMessage, SeriesBorderSizeRow);
            errorMessage = ValidateNonNegativeIntegerValue(txtLegendBorderSize, errorMessage, LegendBorderSizeRow);
            errorMessage = ValidateNonNegativeIntegerValue(txtPlotAreaBorderSize, errorMessage, PlotAreaBorderSizeRow);
            errorMessage = ValidateNonNegativeIntegerValue(txtSeriesLineBorderSize, errorMessage, SeriesLineBorderSizeRow);
        }

        if (errorMessage == String.Empty)
        {
            // New graph
            if (graphInfo == null)
            {
                graphInfo = new ReportGraphInfo();
            }

            graphInfo.GraphDisplayName = txtDefaultName.Text.Trim();
            graphInfo.GraphName = txtDefaultCodeName.Text.Trim();

            if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Reporting", "EditSQLQueries"))
            {
                graphInfo.GraphQuery = txtQueryQuery.Text;
            }

            graphInfo.GraphQueryIsStoredProcedure = chkIsStoredProcedure.Checked;

            graphInfo.GraphType = drpChartType.SelectedValue;
            graphInfo.GraphReportID = reportInfo.ReportID;
            graphInfo.GraphWidth = ValidationHelper.GetInteger(txtChartWidth.Text, 0);
            graphInfo.GraphHeight = ValidationHelper.GetInteger(txtChartHeight.Text, 0);
            graphInfo.GraphTitle = txtGraphTitle.Text;

            graphInfo.GraphXAxisTitle = txtXAxisTitle.Text;
            graphInfo.GraphYAxisTitle = txtYAxisTitle.Text;

            ReportCustomData settings = (ReportCustomData)graphInfo.GraphSettings;

            // Export
            settings["ExportEnabled"] = chkExportEnable.Checked.ToString();

            // ChartType                        
            settings["BarDrawingStyle"] = drpBarDrawingStyle.SelectedValue;
            settings["BarOverlay"] = chkBarOverlay.Checked.ToString();
            settings["BarOrientation"] = drpChartType.SelectedValue.ToLower() == "bar" ? drpBarOrientation.SelectedValue : drpBarStackedOrientation.SelectedValue;

            settings["StackedBarDrawingStyle"] = drpStackedBarDrawingStyle.SelectedValue;
            settings["StackedBarMaxStacked"] = chkStacked.Checked.ToString();

            settings["PieDrawingStyle"] = drpPieDrawingStyle.SelectedValue;
            settings["PieDrawingDesign"] = drpPieDrawingDesign.SelectedValue;
            settings["PieLabelStyle"] = drpPieLabelStyle.SelectedValue;
            settings["PieDoughnutRadius"] = drpPieDoughnutRadius.SelectedValue;
            settings["PieOtherValue"] = txtPieOtherValue.Text.ToString();

            settings["LineDrawinStyle"] = drpLineDrawingStyle.SelectedValue;

            settings["ShowAs3D"] = chkShowAs3D.Checked.ToString();
            settings["RotateX"] = txtRotateX.Text;
            settings["RotateY"] = txtRotateY.Text;

            // Title
            settings["ShowMajorGrid"] = chkShowGrid.Checked.ToString();
            settings["TitleFontNew"] = (string)ucTitleFont.Value;
            settings["TitlePosition"] = drpTitlePosition.SelectedValue;
            settings["TitleColor"] = ucTitleColor.SelectedColor;

            // Legend
            settings["LegendBgColor"] = ucLegendBgColor.SelectedColor;
            settings["LegendBorderColor"] = ucLegendBorderColor.SelectedColor;
            settings["LegendBorderSize"] = txtLegendBorderSize.Text;
            settings["LegendBorderStyle"] = drpLegendBorderStyle.SelectedValue;
            settings["LegendPosition"] = drpLegendPosition.SelectedValue;
            settings["LegendInside"] = chkLegendInside.Checked.ToString();
            settings["LegendTitle"] = txtLegendTitle.Text;

            // XAxis
            settings["XAxisFont"] = (string)ucXAxisTitleFont.Value;
            settings["XAxisTitlePosition"] = drpXAxisTitlePosition.SelectedValue;
            settings["XAxisAngle"] = txtXAxisAngle.Text;
            settings["XAxisInterval"] = txtXAxisInterval.Text;
            settings["xaxissort"] = chkXAxisSort.Checked.ToString();
            settings["XAxisLabelFont"] = (string)ucXAxisLabelFont.Value;
            settings["XAxisTitleColor"] = ucXAxisTitleColor.SelectedColor;
            settings["XAxisFormat"] = txtXAxisFormat.Text;

            // YAxis             
            settings["YAxisUseXAxisSettings"] = chkYAxisUseXSettings.Checked.ToString();
            settings["YAxisAngle"] = txtYAxisAngle.Text;
            settings["YAxisTitleColor"] = ucYAxisTitleColor.SelectedColor;
            settings["YAxisFormat"] = txtYAxisFormat.Text;

            if (chkYAxisUseXSettings.Checked)
            {
                settings["YAxisFont"] = (string)ucXAxisTitleFont.Value;
                settings["YAxisTitlePosition"] = drpXAxisTitlePosition.SelectedValue;
                settings["YAxisLabelFont"] = (string)ucXAxisLabelFont.Value;
            }
            else
            {
                settings["YAxisFont"] = (string)ucYAxisTitleFont.Value;
                settings["YAxisLabelFont"] = (string)ucYAxisLabelFont.Value;
                settings["YAxisTitlePosition"] = drpYAxisTitlePosition.SelectedValue;
            }

            // Chart Area
            settings["ChartAreaPrBgColor"] = ucChartAreaPrBgColor.SelectedColor;
            settings["ChartAreaSecBgColor"] = ucChartAreaSecBgColor.SelectedColor;
            settings["ChartAreaBorderColor"] = ucChartAreaBorderColor.SelectedColor;
            settings["ChartAreaGradient"] = drpChartAreaGradient.SelectedValue;
            settings["ChartAreaBorderSize"] = txtChartAreaBorderSize.Text;
            settings["ChartAreaBorderStyle"] = drpChartAreaBorderStyle.SelectedValue;
            settings["ScaleMin"] = txtScaleMin.Text;
            settings["ScaleMax"] = txtScaleMax.Text;
            settings["TenPowers"] = chkTenPowers.Checked.ToString();
            settings["ReverseYAxis"] = chkReverseY.Checked.ToString();
            settings["BorderSkinStyle"] = drpBorderSkinStyle.SelectedValue;
            settings["QueryNoRecordText"] = txtQueryNoRecordText.Text;

            // Plot Area
            settings["PlotAreaPrBgColor"] = ucPlotAreaPrBgColor.SelectedColor;
            settings["PlotAreaSecBgColor"] = ucPlotAreSecBgColor.SelectedColor;
            settings["PlotAreaBorderColor"] = ucPlotAreaBorderColor.SelectedColor;
            settings["PlotAreaGradient"] = drpPlotAreaGradient.SelectedValue;
            settings["PlotAreaBorderSize"] = txtPlotAreaBorderSize.Text;
            settings["PlotAreaBorderStyle"] = drpPlotAreaBorderStyle.SelectedValue;

            // Series 
            settings["SeriesPrBgColor"] = ucSeriesPrBgColor.SelectedColor;
            settings["SeriesSecBgColor"] = ucSeriesSecBgColor.SelectedColor;
            settings["SeriesBorderColor"] = ucSeriesBorderColor.SelectedColor;
            settings["SeriesGradient"] = drpSeriesGradient.SelectedValue;
            settings["SeriesBorderSize"] = txtSeriesBorderSize.Text;
            settings["SeriesBorderStyle"] = drpSeriesBorderStyle.SelectedValue;
            settings["SeriesSymbols"] = drpSeriesSymbols.SelectedValue;
            settings["DisplayItemValue"] = chkSeriesDisplayItemValue.Checked.ToString();
            settings["SeriesItemToolTip"] = txtSeriesItemTooltip.Text;
            settings["SeriesItemLink"] = txtSeriesItemLink.Text;
            settings["ItemValueFormat"] = txtItemValueFormat.Text;
            settings["ValuesAsPercent"] = chkValuesAsPercent.Checked.ToString();

            if (drpChartType.SelectedValue.ToLower() == "line")
            {
                settings["SeriesBorderSize"] = txtSeriesLineBorderSize.Text;
                settings["SeriesBorderStyle"] = drpSeriesLineBorderStyle.SelectedValue;
                settings["SeriesPrBgColor"] = ucSeriesLineColor.SelectedColor;
            }

            // Delete old settings
            settings["axisFont"] = null;
            settings["colors"] = null;
            settings["graphGradient"] = null;
            settings["chartGradient"] = null;
            settings["itemGradient"] = null;
            settings["symbols"] = null;
            settings["titleFont"] = null;
            settings["VerticalBars"] = null;
            settings["FillCurves"] = null;
            settings["SmoothCurves"] = null;
            graphInfo.GraphLegendPosition = 100;
            if (save)
            {
                ReportGraphInfoProvider.SetReportGraphInfo(graphInfo);
            }
            else
            {
                PersistentEditedObject = graphInfo;
            }

            return true;
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return false;
        }
    }


    /// <summary>
    /// Save data and close editor
    /// </summary>
    /// <param name="sender">Button save object</param>
    /// <param name="e">Event arguments</param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (Save(true))
        {
            ltlScript.Text += ScriptHelper.GetScript("window.RefreshContent();window.close();");
        }
        else
        {
            SelectedTab = 0;
        }
    }


    /// <summary>
    /// Apply changes
    /// </summary>
    /// <param name="sender">Button apply object</param>
    /// <param name="e">Event arguments</param>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (!Save(true))
        {
            SelectedTab = 0;
        }
        else
        {
            // Redirect for new reports
            if (newReport)
            {
                Response.Redirect(ResolveUrl("ReportGraph_Edit.aspx?reportId=" + graphInfo.GraphReportID + "&itemName=" + graphInfo.GraphName));
            }
        }
    }


    /// <summary>
    /// Show graph
    /// </summary>
    private void ShowPreview()
    {
        // Color picker preview issue
        DisplayEditControls(false);

        categoryList.Visible = false;
        pnlVersions.Visible = false;
        if (reportInfo != null)
        {
            pnlPreview.Visible = true;

            FormInfo fi = new FormInfo(reportInfo.ReportParameters);
            // Get datarow with required columns
            DataRow dr = fi.GetDataRow();

            fi.LoadDefaultValues(dr, true);

            ctrlReportGraph.ReportParameters = dr;

            ctrlReportGraph.Visible = true;

            // Prepare fully qualified graph name = with reportname
            string fullReportGraphName = reportInfo.ReportName + "." + graphInfo.GraphName;
            ctrlReportGraph.GraphInfo = graphInfo;
            ctrlReportGraph.Parameter = fullReportGraphName;

            ctrlReportGraph.ReloadData(true);
        }
    }


    /// <summary>
    /// Hide buttons that are connected with reporting object
    /// </summary>
    private void HideActionButtons()
    {
        btnApply.Visible = false;
        btnOk.Visible = false;
        btnCancel.Text = GetString("general.close");
    }


    /// <summary>
    /// Show buttons that are connected with reporting object
    /// </summary>
    private void ShowActionButtons()
    {
        btnApply.Visible = true;
        btnOk.Visible = true;
        btnCancel.Text = GetString("general.cancel");
    }


    /// <summary>
    /// Show/hide edit forms (tab 0)
    /// </summary>
    private void DisplayEditControls(bool show)
    {
        // Color picker preview issue  (need to hide validators when saving in preview mode (javascript ruins validation))
        if (show)
        {
            FormPanelHolder.Attributes.Remove("style");
        }
        else
        {
            FormPanelHolder.Attributes.Add("style", "display:none");
        }

        rfvDisplayName.Visible = show;
        rfvCodeName.Visible = show;
    }


    /// <summary>
    /// Get info from PersistentEditedObject and reload data
    /// </summary>
    private void ReloadDataAfrterRollback()
    {
        // Load rollbacked info
        GeneralizedInfo gi = PersistentEditedObject as GeneralizedInfo;
        if (gi != null)
        {
            graphInfo = gi.MainObject as ReportGraphInfo;
        }
        LoadData();
    }


    protected void versionList_onAfterRollback(object sender, EventArgs e)
    {
        ReloadDataAfrterRollback();
    }

    #endregion
}

