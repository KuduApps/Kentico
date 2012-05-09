<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_ReportGraph_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" CodeFile="~/CMSModules/Reporting/Tools/ReportGraph_Edit.aspx.cs" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSFormControls/Selectors/FontSelector.ascx" TagPrefix="cms"
    TagName="FontSelector" %>
<%@ Register Src="~/CMSModules/Reporting/Controls/ReportGraph.ascx" TagName="ReportGraph"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Objects/Controls/ObjectVersionList.ascx" TagName="VersionList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:HiddenField ID="hdnTabState" runat="server" />
    <div class="WebpartProperties LightTabs">
        <asp:Panel runat="server" ID="pnlFullTabsLeft" CssClass="FullTabsLeft" />
        <asp:Panel runat="server" ID="pnlTabsContainer" CssClass="FullTabsRight">
            <asp:Panel runat="server" ID="pnlTabs" CssClass="TabsTabs">
                <asp:Panel runat="server" ID="pnlWhite" CssClass="Tabs">
                    <cms:UITabs ID="tabControlElem" runat="server" UseClientScript="true" OnOnTabClicked="tabControlElem_clicked" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
        <div class="HeaderSeparatorEnvelope">
            <div class="HeaderSeparator">
                &nbsp;</div>
        </div>
        <div id="WebPartForm_Properties" class="WebPartForm ReportGraph">
            <cms:CategoryListPanel ID="categoryList" runat="server" />
            <div id="divScrolable" runat="server" class="ScrollableContent">
                <div id="FormPanelHolder" class="ReportFormPanel" runat="server" style="overflow: hidden">
                    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                        Visible="false" />
                    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                        Visible="false" />
                    <cms:CategoryPanel ID="DefaultPanel" runat="server" Text="rep.default">
                        <cms:CategoryPanelRow ID="DefaultNameRow" runat="server" IsRequired="true" LabelTitle="general.displayname">
                            <cms:LocalizableTextBox runat="server" CssClass="TextBoxField" ID="txtDefaultName"
                                MaxLength="50" name="txtDefaultName" />
                            <br />
                            <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDefaultName"
                                Display="Dynamic" ValidationGroup="Basic"></cms:CMSRequiredFieldValidator>
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="DefaultCodeNameRow" runat="server" IsRequired="true" LabelTitle="general.codename">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtDefaultCodeName" MaxLength="50"
                                name="txtDefaultCodeName" />
                            <br />
                            <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtDefaultCodeName"
                                Display="Dynamic" ValidationGroup="Basic"></cms:CMSRequiredFieldValidator>
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="DefaultEnableExportRow" runat="server" IsRequired="false"
                            LabelTitle="rep.enableexport">
                            <asp:CheckBox runat="server" ID="chkExportEnable" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="QueryPanel" runat="server" Text="rep.query">
                        <cms:CategoryPanelRow ID="QueryQueryRow" runat="server" IsRequired="true" LabelTitle="Reporting_ReportGraph_Edit.Query">
                            <cms:ExtendedTextArea runat="server" ID="txtQueryQuery" Name="txtQueryQuery" EditorMode="Advanced"
                                Language="SQL" Width="560px" Height="240px" />
                            <br />
                            <cms:LocalizedLabel runat="server" ID="lblQueryHelp" ResourceString="rep.queryhelp"
                                CssClass="ContentLabelItalic" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="IsStoredProcedureRow" runat="server" LabelTitle="rep.isstoredprocedure">
                            <asp:CheckBox runat="server" ID="chkIsStoredProcedure" name="chkQueryIsQuery" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="QueryNoRecordText" runat="server" IsRequired="false" LabelTitle="rep.graph.norecordtext">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" name="txtNoRecordText" ID="txtQueryNoRecordText" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel runat="server" ID="ChartTypePanel" Text="rep.charttype">
                        <cms:CategoryPanelRow ID="ChartTypeRow" runat="server" IsRequired="false" LabelTitle="Reporting_ReportGraph_Edit.GraphType">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpChartType" name="drpChartType" />
                        </cms:CategoryPanelRow>
                        <%--BAR--%>
                        <cms:CategoryPanelRow ID="BarDrawingStyleRow" runat="server" ItemGroup="Bar" LabelTitle="rep.graph.drawingstyle"
                            IsRequired="false">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpBarDrawingStyle"
                                name="drpBarDrawingStyle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="BarOverlayRow" runat="server" ItemGroup="Bar" IsRequired="false"
                            LabelTitle="rep.graph.overlay">
                            <asp:CheckBox runat="server" CssClass="DropDownField" ID="chkBarOverlay" name="chkBarOverlay" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="BarOrientationRow" runat="server" ItemGroup="Bar" IsRequired="false"
                            LabelTitle="rep.graph.orientation">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpBarOrientation"
                                name="drpBarOrientation" />
                        </cms:CategoryPanelRow>
                        <%--BAR STACKED--%>
                        <cms:CategoryPanelRow ID="StackedBarDrawingStyleRow" runat="server" ItemGroup="StackedBar"
                            LabelTitle="rep.graph.drawingstyle" IsRequired="false">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpStackedBarDrawingStyle"
                                name="drpStackedBarDrawingStyle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="StackedBar100ProcStacked" runat="server" ItemGroup="StackedBar"
                            LabelTitle="rep.graph.stacked" IsRequired="false">
                            <asp:CheckBox runat="server" ID="chkStacked" name="chkStacked" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="BarStackedOrientationRow" runat="server" ItemGroup="StackedBar"
                            LabelTitle="rep.graph.orientation" IsRequired="false">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpBarStackedOrientation"
                                name="drpBarStackedOrientation" />
                        </cms:CategoryPanelRow>
                        <!--  PIE CHARTS  //-->
                        <cms:CategoryPanelRow ID="PieDrawingStyleRow" runat="server" ItemGroup="Pie" IsRequired="false"
                            LabelTitle="rep.graph.drawingstyle">
                            <asp:DropDownList runat="server" CssClass="DropDownField" onChange="pieStyleChanged()"
                                ID="drpPieDrawingStyle" name="drpPieDrawingStyle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PieDrawingDesign" runat="server" ItemGroup="Pie" IsRequired="false"
                            LabelTitle="rep.graph.drawingdesign">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpPieDrawingDesign"
                                name="drpPieDrawingDesign" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PieLabelStyleRow" runat="server" ItemGroup="Pie" IsRequired="false"
                            LabelTitle="rep.graph.labelstyle">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpPieLabelStyle" name="drpPieLabelStyle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PieDoughnutRadiusRow" runat="server" ItemGroup="Pie" IsRequired="false"
                            LabelTitle="rep.graph.doughnutradius">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpPieDoughnutRadius"
                                name="drpPieDoughnutRadius" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PieOtherValue" runat="server" ItemGroup="Pie" IsRequired="false"
                            LabelTitle="rep.graph.collectpieslices">
                            <cms:CMSTextBox runat="server" ID="txtPieOtherValue" name="txtPieOtherValue" CssClass="TextBoxField" />
                        </cms:CategoryPanelRow>
                        <!--  Line CHARTS  //-->
                        <cms:CategoryPanelRow ID="LineDrawingStyleRow" runat="server" ItemGroup="Line" IsRequired="false"
                            LabelTitle="rep.graph.drawingstyle">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpLineDrawingStyle"
                                name="drpLineDrawingStyle" />
                        </cms:CategoryPanelRow>
                        <!--  General column type  //-->
                        <cms:CategoryPanelRow ID="ShowAs3DRow" runat="server" IsRequired="false" LabelTitle="rep.graph.showas3D">
                            <asp:CheckBox runat="server" ID="chkShowAs3D" onclick="showAs3DClicked()" name="chkShowAs3D" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="RotateXRow" runat="server" IsRequired="false" LabelTitle="rep.graph.rotatex">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtRotateX" name="txtRotateX" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="RotateYRow" runat="server" IsRequired="false" LabelTitle="rep.graph.rotatey">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtRotateY" name="txtRotateY" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartWidthRow" runat="server" IsRequired="false" LabelTitle="Reporting_ReportGraph_Edit.GraphWidth">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtChartWidth" MaxLength="50"
                                name="txtChartWidth" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartHeightRow" runat="server" IsRequired="false" LabelTitle="Reporting_ReportGraph_Edit.GraphHeight">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtChartHeight" MaxLength="50"
                                name="txtChartHeight" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartShowGridRow" runat="server" ItemGroup="Grid" IsRequired="false"
                            LabelTitle="rep.graph.showgrid">
                            <asp:CheckBox runat="server" ID="chkShowGrid" name="chkShowGrid" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="TitlePanel" runat="server" Text="rep.title">
                        <cms:CategoryPanelRow ID="TitleRow" runat="server" IsRequired="false" LabelTitle="Reporting_ReportGraph_Edit.GraphTitle">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtGraphTitle" MaxLength="50"
                                name="txtGraphTitle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="TitleFontRow" runat="server" IsRequired="false" LabelTitle="rep.graph.titlefont">
                            <cms:FontSelector runat="server" ID="ucTitleFont" name="ucTitleFont" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="TitleColorRow" runat="server" IsRequired="false" LabelTitle="rep.graph.titlecolor">
                            <cms:ColorPicker runat="server" ID="ucTitleColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="TitlePositionRow" runat="server" IsRequired="false" LabelTitle="rep.graph.titleposition">
                            <asp:DropDownList runat="server" ID="drpTitlePosition" CssClass="DropDownField" MaxLength="50"
                                name="drpTitlePosition" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="LegendPanel" runat="server" Text="rep.legend">
                        <cms:CategoryPanelRow ID="LegendBgColorRow" runat="server" IsRequired="false" LabelTitle="rep.graph.legendbgcolor">
                            <cms:ColorPicker runat="server" ID="ucLegendBgColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="LegendBorderColorRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.bordercolor">
                            <cms:ColorPicker runat="server" ID="ucLegendBorderColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="LegendBorderSizeRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.bordersize">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtLegendBorderSize" MaxLength="50"
                                name="txtLegendBorderSize" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="LegendBorderStyleRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.borderstyle">
                            <asp:DropDownList runat="server" CssClass="DropDownField" name="drpLegendBorderStyle"
                                ID="drpLegendBorderStyle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="LegendPositionRow" runat="server" IsRequired="false" LabelTitle="rep.graph.position">
                            <asp:DropDownList runat="server" CssClass="DropDownField" ID="drpLegendPosition"
                                MaxLength="50" name="drpLegendPosition" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="LegendInsideRow" runat="server" IsRequired="false" LabelTitle="rep.graph.legendinside">
                            <asp:CheckBox runat="server" ID="chkLegendInside" name="chkLegendInside" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="LegendTitle" runat="server" IsRequired="false" LabelTitle="rep.graph.legendtitle">
                            <cms:CMSTextBox runat="server" ID="txtLegendTitle" CssClass="TextBoxField" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="XAxisPanel" runat="server" Text="rep.xaxis">
                        <cms:CategoryPanelRow ID="XAxisTitleRow" runat="server" IsRequired="false" LabelTitle="Reporting_ReportGraph_Edit.GraphXAxisTitle">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtXAxisTitle" MaxLength="50"
                                name="txtXAxisTitle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisTitleColorRow" runat="server" IsRequired="false" LabelTitle="rep.graph.titlecolor">
                            <cms:ColorPicker runat="server" ID="ucXAxisTitleColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisAngleRow" runat="server" IsRequired="false" LabelTitle="rep.graph.xaxisangle">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtXAxisAngle" MaxLength="50"
                                name="txtXAxisAngle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisFormatRow" runat="server" IsRequired="false" LabelTitle="rep.graph.xaxisformat">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" MaxLength="150" ID="txtXAxisFormat"
                                name="txtXAxisFormat" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisTitleFontRow" runat="server" IsRequired="false" LabelTitle="rep.graph.titlefont">
                            <cms:FontSelector runat="server" ID="ucXAxisTitleFont" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisTitlePositionRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.position">
                            <asp:DropDownList runat="server" CssClass="DropDownField" name="drpXAxisTitlePosition"
                                ID="drpXAxisTitlePosition" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisLabelFont" runat="server" IsRequired="false" LabelTitle="rep.graph.axislabelfont">
                            <cms:FontSelector runat="server" name="ucXAxisLabelFont" ID="ucXAxisLabelFont" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisInterval" runat="server" IsRequired="false" LabelTitle="rep.graph.xaxisinterval">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtXAxisInterval" MaxLength="50"
                                name="txtXAxisInterval" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="XAxisUseSort" runat="server" IsRequired="false" LabelTitle="rep.graph.usexsort">
                            <asp:CheckBox runat="server" ID="chkXAxisSort" name="chkXAxisSort" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="YAxisPanel" runat="server" Text="rep.yaxis">
                        <cms:CategoryPanelRow ID="YAxisTitleRow" runat="server" IsRequired="false" LabelTitle="Reporting_ReportGraph_Edit.GraphYAxisTitle">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtYAxisTitle" MaxLength="50"
                                name="txtYAxisTitle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="YAxisTitleColorRow" runat="server" IsRequired="false" LabelTitle="rep.graph.titlecolor">
                            <cms:ColorPicker runat="server" ID="ucYAxisTitleColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="YAxisAngleRow" runat="server" IsRequired="false" LabelTitle="rep.graph.yaxisangle">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtYAxisAngle" MaxLength="50"
                                name="txtYAxisAngle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="YAxisFormatRow" runat="server" IsRequired="false" LabelTitle="rep.graph.yaxisformat">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtYAxisFormat" MaxLength="150"
                                name="txtYAxisFormat" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="YAxisUseXSettingsRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.usexsettings">
                            <asp:CheckBox runat="server" ID="chkYAxisUseXSettings" name="chkYAxisUseXSettings"
                                onClick="checkXAxisSettings()" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="YAxisTitleFontRow" runat="server" IsRequired="false" LabelTitle="rep.graph.titlefont">
                            <cms:FontSelector runat="server" ID="ucYAxisTitleFont" name="ucYAxisTitleFont" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="YAxisTitlePositionRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.position">
                            <asp:DropDownList runat="server" CssClass="DropDownField" name="drpYAxisTitlePosition"
                                ID="drpYAxisTitlePosition" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="YAxisLabelFont" runat="server" IsRequired="false" LabelTitle="rep.graph.axislabelfont">
                            <cms:FontSelector runat="server" name="ucYAxisLabelFont" ID="ucYAxisLabelFont" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="SeriesPanel" runat="server" Text="rep.series">
                        <cms:CategoryPanelRow ID="SeriesPrBgColorRow" ItemGroup="Common" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.primarybgcolor">
                            <cms:ColorPicker runat="server" ID="ucSeriesPrBgColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesSecBgColorRow" ItemGroup="Common" runat="server"
                            LabelTitle="rep.graph.secondarybgcolor" IsRequired="false">
                            <cms:ColorPicker runat="server" ID="ucSeriesSecBgColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesGradientRow" ItemGroup="Common" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.gradient">
                            <asp:DropDownList class="DropDownField" name="drpSeriesGradient" ID="drpSeriesGradient"
                                runat="server" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesBorderColorRow" ItemGroup="Common" runat="server"
                            LabelTitle="rep.graph.bordercolor" IsRequired="false">
                            <cms:ColorPicker runat="server" ID="ucSeriesBorderColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesBorderSizeRow" ItemGroup="Common" runat="server"
                            LabelTitle="rep.graph.bordersize" IsRequired="false">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtSeriesBorderSize" MaxLength="50"
                                name="txtSeriesBorderSize" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesBorderStyleRow" ItemGroup="Common" runat="server"
                            LabelTitle="rep.graph.borderstyle" IsRequired="false">
                            <asp:DropDownList class="DropDownField" name="drpSeriesBorderStyle" ID="drpSeriesBorderStyle"
                                runat="server" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesDisplayItemValue" runat="server" LabelTitle="rep.graph.displayitemvalue"
                            IsRequired="false">
                            <asp:CheckBox name="chkSeriesDisplayItemValue" ID="chkSeriesDisplayItemValue" runat="server" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesItemValueFormat" runat="server" LabelTitle="rep.graph.itemvalueformat"
                            IsRequired="false">
                            <cms:CMSTextBox runat="server" ID="txtItemValueFormat" CssClass="TextBoxField" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesLineColorRow" runat="server" ItemGroup="Line" IsRequired="false"
                            LabelTitle="rep.graph.serieslinecolor">
                            <cms:ColorPicker runat="server" ID="ucSeriesLineColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesLineBorderSizeRow" runat="server" ItemGroup="Line"
                            LabelTitle="rep.graph.serieslinesbordersize" IsRequired="false">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtSeriesLineBorderSize"
                                MaxLength="50" name="txtSeriesLineBorderSize" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesLineBorderStyleRow" ItemGroup="Line" runat="server"
                            LabelTitle="rep.graph.serieslineborderstyle" IsRequired="false">
                            <asp:DropDownList class="DropDownField" name="drpSeriesLineBorderStyle" ID="drpSeriesLineBorderStyle"
                                runat="server" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesSymbols" ItemGroup="Line" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.symbols">
                            <asp:DropDownList class="DropDownField" name="drpSeriesSymbols" ID="drpSeriesSymbols"
                                runat="server" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesItemTooltip" runat="server" LabelTitle="rep.graph.itemtooltip"
                            IsRequired="false">
                            <cms:LargeTextArea runat="server" CssClass="TextBoxField" ID="txtSeriesItemTooltip"
                                name="txtSeriesItemTooltip" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesItemLink" runat="server" LabelTitle="rep.graph.itemlink"
                            IsRequired="false">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtSeriesItemLink" MaxLength="100"
                                name="txtSeriesItemLink" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesValuesAsPercent" runat="server" ItemGroup="StackedBar Bar Line"
                            LabelTitle="rep.graph.valuesaspercent" IsRequired="false">
                            <asp:CheckBox name="chkValuesAsPercent" ID="chkValuesAsPercent" runat="server" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="ChartAreaPanel" runat="server" Text="rep.chartarea">
                        <cms:CategoryPanelRow ID="ChartAreaPrBgColorRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.primarybgcolor">
                            <cms:ColorPicker runat="server" ID="ucChartAreaPrBgColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaSecBgColorRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.secondarybgcolor">
                            <cms:ColorPicker runat="server" ID="ucChartAreaSecBgColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaGradientRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.gradient">
                            <asp:DropDownList runat="server" CssClass="DropDownField" name="drpChartAreaGradient"
                                ID="drpChartAreaGradient" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaBorderColorRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.bordercolor">
                            <cms:ColorPicker runat="server" ID="ucChartAreaBorderColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaBorderSizeRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.bordersize">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtChartAreaBorderSize"
                                MaxLength="50" name="txtChartAreaBorderSize" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaBorderStyleRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.borderstyle">
                            <asp:DropDownList runat="server" CssClass="DropDownField" name="drpChartAreaBorderStyle"
                                ID="drpChartAreaBorderStyle" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaScaleMin" runat="server" IsRequired="false" LabelTitle="rep.graph.scalemin">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" name="txtScaleMin" ID="txtScaleMin" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaScaleMax" runat="server" IsRequired="false" LabelTitle="rep.graph.scalemax">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" name="txtScaleMax" ID="txtScaleMax" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaTenPowers" runat="server" LabelTitle="rep.graph.tenpowers">
                            <asp:CheckBox runat="server" ID="chkTenPowers" name="chkTenPowers" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaReverseYRow" runat="server" LabelTitle="rep.graph.reversey">
                            <asp:CheckBox runat="server" ID="chkReverseY" name="chkReverseY" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="ChartAreaBorderSkinStyle" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.borderskinstyle">
                            <asp:DropDownList ID="drpBorderSkinStyle" runat="server" CssClass="DropDownField" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="PlotAreaPanel" runat="server" Text="rep.plotarea">
                        <cms:CategoryPanelRow ID="PlotAreaPrBgColorRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.primarybgcolor">
                            <cms:ColorPicker runat="server" ID="ucPlotAreaPrBgColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PlotAreaSecBgColorRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.secondarybgcolor">
                            <cms:ColorPicker runat="server" ID="ucPlotAreSecBgColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PlotAreaGradientRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.gradient">
                            <asp:DropDownList runat="server" CssClass="DropDownField" name="drpPlotAreaGradient"
                                ID="drpPlotAreaGradient" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PlotAreaBorderColorRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.bordercolor">
                            <cms:ColorPicker runat="server" ID="ucPlotAreaBorderColor" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PlotAreaBorderSizeRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.bordersize">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtPlotAreaBorderSize"
                                MaxLength="50" name="txtPlotAreaBorderSize" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="PlotAreaBorderStyleRow" runat="server" IsRequired="false"
                            LabelTitle="rep.graph.borderstyle">
                            <asp:DropDownList class="DropDownField" name="drpPlotAreaBorderStyle" ID="drpPlotAreaBorderStyle"
                                runat="server" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                </div>
                <asp:HiddenField ID="txtNewGraphHidden" runat="server" />
                <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
                <asp:Panel runat="server" ID="pnlPreview" Style="height: 98%" Visible="false">
                    <table runat="server" style="height: 98%; width: 100%">
                        <tr>
                            <td style="text-align: center; vertical-align: middle">
                                <cms:ReportGraph ID="ctrlReportGraph" runat="server" Visible="false" RenderCssClasses="true" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlVersions" Style="margin: 20px 12px 15px;" CssClass="VersionTab"
                    Visible="false">
                    <cms:VersionList ID="versionList" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div id="divFooter" runat="server" style="position: absolute; width: 100%">
        <div class="ReportEditButtons">
            <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                ValidationGroup="Basic" />
            <cms:CMSButton ID="btnCancel" runat="server" OnClientClick="window.close(); return false;"
                CssClass="SubmitButton" />
            <cms:CMSButton ID="btnApply" runat="server" CssClass="SubmitButton" OnClick="btnApply_Click"
                ValidationGroup="Basic" />
        </div>
    </div>
</asp:Content>
