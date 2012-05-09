<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Reporting_Tools_ReportTable_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="ReportTable Edit" ValidateRequest="false" EnableEventValidation="false"
    CodeFile="~/CMSModules/Reporting/Tools/ReportTable_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.UIControls" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSFormControls/Selectors/FontSelector.ascx" TagPrefix="cms"
    TagName="FontSelector" %>
<%@ Register Src="~/CMSModules/Reporting/Controls/ReportTable.ascx" TagName="ReportTable"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Objects/Controls/ObjectVersionList.ascx" TagName="VersionList"
    TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="server">
    <asp:HiddenField runat="server" ID="hdnSelectedTab" />
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
        <div id="WebPartForm_Properties" class="WebPartForm">
            <cms:CategoryListPanel ID="categoryList" runat="server" />
            <div id="divScrolable" runat="server" class="ScrollableContent">
                <div id="divPanelHolder" runat="server" style="overflow: hidden">
                    <div id="FormPanelHolder" class="ReportFormPanel">
                        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                            Visible="false" />
                        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                            Visible="false" />
                        <cms:CategoryPanel ID="DefaultPanel" runat="server" Text="rep.default">
                            <cms:CategoryPanelRow ID="DisplayNameRow" runat="server" IsRequired="true" LabelTitle="general.displayname">
                                <cms:LocalizableTextBox runat="server" CssClass="TextBoxField" ID="txtDisplayName"
                                    MaxLength="50" name="txtDefaultName" />
                                <br />
                                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                                    ErrorMessage="general.requiresdisplayname" Display="Dynamic"></cms:CMSRequiredFieldValidator>
                            </cms:CategoryPanelRow>
                            <cms:CategoryPanelRow ID="CodeNameRow" runat="server" IsRequired="true" LabelTitle="general.codename">
                                <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtCodeName" MaxLength="50"
                                    name="DefaultCodeName" />
                                <br />
                                <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                                    ErrorMessage="general.requirescodename" Display="Dynamic"></cms:CMSRequiredFieldValidator>
                            </cms:CategoryPanelRow>
                            <cms:CategoryPanelRow ID="DefaultEnableExportRow" runat="server" IsRequired="false"
                                LabelTitle="rep.enableexport">
                                <asp:CheckBox runat="server" ID="chkExportEnable" />
                            </cms:CategoryPanelRow>
                        </cms:CategoryPanel>
                        <cms:CategoryPanel ID="QueryPanel" runat="server" Text="rep.query">
                            <cms:CategoryPanelRow ID="QueryRow" runat="server" IsRequired="true" LabelTitle="rep.query">
                                <cms:ExtendedTextArea runat="server" ID="txtQuery" Name="txtQuery" EditorMode="Advanced"
                                    Language="SQL" Width="560px" Height="240px" />
                            </cms:CategoryPanelRow>
                            <cms:CategoryPanelRow ID="IsProcedureRow" runat="server" LabelTitle="rep.isstoredprocedure">
                                <asp:CheckBox runat="server" ID="chkIsProcedure" name="chkIsProcedure" />
                            </cms:CategoryPanelRow>
                            <cms:CategoryPanelRow ID="QueryNoRecordText" runat="server" IsRequired="false" LabelTitle="rep.graph.norecordtext">
                                <cms:CMSTextBox runat="server" CssClass="TextBoxField" name="txtNoRecordText" ID="txtQueryNoRecordText" />
                            </cms:CategoryPanelRow>
                        </cms:CategoryPanel>
                        <cms:CategoryPanel ID="SkinPanel" runat="server" Text="rep.table.skin">
                            <cms:CategoryPanelRow ID="SkinSkinIdRow" runat="server" LabelTitle="rep.table.skinid">
                                <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtSkinID" MaxLength="50"
                                    name="txtSkinID" />
                            </cms:CategoryPanelRow>
                        </cms:CategoryPanel>
                        <cms:CategoryPanel ID="PagingPanel" runat="server" Text="rep.table.paging">
                            <cms:CategoryPanelRow ID="PagingEnableRow" runat="server" LabelTitle="rep.table.enablepaging">
                                <asp:CheckBox runat="server" ID="chkEnablePaging" name="chkEnablePaging" />
                            </cms:CategoryPanelRow>
                            <cms:CategoryPanelRow ID="PagingPageSizeRow" runat="server" LabelTitle="rep.table.pagesize">
                                <cms:CMSTextBox runat="server" CssClass="TextBoxField" ID="txtPageSize" MaxLength="50"
                                    name="txt PageSize" />
                            </cms:CategoryPanelRow>
                            <cms:CategoryPanelRow ID="PagingPageModeRow" runat="server" LabelTitle="rep.table.pagingmode">
                                <asp:DropDownList runat="server" class="DropDownField" name="drpPageMode" ID="drpPageMode" />
                            </cms:CategoryPanelRow>
                        </cms:CategoryPanel>
                    </div>
                </div>
                <asp:Panel runat="server" ID="pnlPreview" Visible="false" Style="padding: 10px">
                    <cms:ReportTable ID="ctrlReportTable" runat="server" RenderCssClasses="true" />
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlVersions" Style="margin: 20px 12px 15px;" CssClass="VersionTab"
                    Visible="false">
                    <cms:VersionList ID="versionList" runat="server" />
                </asp:Panel>
            </div>
        </div>
        <asp:HiddenField ID="txtNewTableHidden" runat="server" />
        <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
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
