<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    EnableEventValidation="false" CodeFile="ReportHtmlGraph_Edit.aspx.cs" Inherits="CMSModules_Reporting_Tools_ReportHtmlGraph_Edit" %>

<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Reporting/Controls/HtmlBarGraph.ascx" TagName="ReportGraph"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Objects/Controls/ObjectVersionList.ascx" TagName="VersionList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:HiddenField runat="server" ID="hdnSelectedTab" />
    <div class="WebpartProperties LightTabs">
        <asp:Panel runat="server" ID="pnlFullTabsLeft" CssClass="FullTabsLeft">
        </asp:Panel>
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
        <div id="WebPartForm_Properties" class="WebPartForm">
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
                                Display="Dynamic"></cms:CMSRequiredFieldValidator>
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="DefaultCodeNameRow" runat="server" IsRequired="true" LabelTitle="general.codename">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtDefaultCodeName" MaxLength="50"
                                name="txtDefaultCodeName" />
                            <br />
                            <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtDefaultCodeName"
                                Display="Dynamic"></cms:CMSRequiredFieldValidator>
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
                    <cms:CategoryPanel ID="TitlePanel" runat="server" Text="rep.title">
                        <cms:CategoryPanelRow ID="TitleRow" runat="server" IsRequired="false" LabelTitle="Reporting_ReportGraph_Edit.GraphTitle">
                            <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtGraphTitle" MaxLength="50"
                                name="txtGraphTitle" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="LegendPanel" runat="server" Text="rep.legend">
                        <cms:CategoryPanelRow ID="LegendTitle" runat="server" IsRequired="false" LabelTitle="rep.graph.legendtitle">
                            <cms:CMSTextBox runat="server" ID="txtLegendTitle" CssClass="TextBoxField" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="DisplayLegend" runat="server" IsRequired="false" LabelTitle="rep.graph.displaylegend">
                            <asp:CheckBox runat="server" ID="chkDisplayLegend" name="chkDisplayLegend" />
                        </cms:CategoryPanelRow>
                    </cms:CategoryPanel>
                    <cms:CategoryPanel ID="SeriesPanel" runat="server" Text="rep.series">
                        <cms:CategoryPanelRow ID="SeriesItemNameFormat" runat="server" LabelTitle="rep.graph.itemnameformat"
                            IsRequired="false">
                            <cms:CMSTextBox runat="server" ID="txtItemNameFormat" CssClass="TextBoxField" />
                        </cms:CategoryPanelRow>
                        <cms:CategoryPanelRow ID="SeriesItemValueFormat" runat="server" LabelTitle="rep.graph.itemvalueformat"
                            IsRequired="false">
                            <cms:CMSTextBox runat="server" ID="txtItemValueFormat" CssClass="TextBoxField" />
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
                    </cms:CategoryPanel>
                </div>
                <asp:HiddenField ID="txtNewGraphHidden" runat="server" />
                <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
                <asp:Panel runat="server" ID="pnlPreview" Style="height: 98%" Visible="false">
                    <table id="Table1" runat="server" style="height: 98%; width: 100%">
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
            <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" />
            <cms:CMSButton ID="btnCancel" runat="server" OnClientClick="window.close(); return false;"
                CssClass="SubmitButton" />
            <cms:CMSButton ID="btnApply" runat="server" CssClass="SubmitButton" OnClick="btnApply_Click" />
        </div>
    </div>
</asp:Content>
