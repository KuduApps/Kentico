<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvancedExport.ascx.cs"
    Inherits="CMSAdminControls_UI_UniGrid_Controls_AdvancedExport" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Basic/OrderByControl.ascx" TagName="OrderByControl"
    TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
    <ContentTemplate>
        <cms:ModalPopupDialog ID="mdlAdvancedExport" runat="server" BackgroundCssClass="ModalBackground"
            CssClass="ModalPopupDialog AdvancedExport">
            <asp:Panel ID="pnlAdvancedExport" runat="server" Visible="false" CssClass="DialogPageBody">
                <div style="height: auto; min-height: 0px;">
                    <div class="PageHeader">
                        <cms:PageTitle ID="advancedExportTitle" runat="server" EnableViewState="false" GenerateFullWidth="false"
                            SetWindowTitle="false" />
                    </div>
                </div>
                <asp:Panel ID="pnlScrollable" runat="server" CssClass="DialogPageContent DialogScrollableContent">
                    <div class="PageBody">
                        <asp:PlaceHolder ID="plcAdvancedExport" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <cms:LocalizedLabel ID="lblExportTo" runat="server" ResourceString="export.exportto"
                                            DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="drpExportTo" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drpExportTo" runat="server" CssClass="DropDownField" OnSelectedIndexChanged="drpExportTo_SelectedIndexChanged"
                                            AutoPostBack="true" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plcDelimiter" runat="server">
                                    <tr>
                                        <td>
                                            <cms:LocalizedLabel ID="lblDelimiter" runat="server" ResourceString="export.delimiter"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="drpDelimiter" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpDelimiter" runat="server" CssClass="DropDownField" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="plcExportRawData" runat="server">
                                    <tr>
                                        <td>
                                            <cms:LocalizedLabel ID="lblExportRawData" runat="server" ResourceString="export.exportrawdata"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="chkExportRawData" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkExportRawData" runat="server" OnCheckedChanged="chkExportRawData_CheckedChanged"
                                                AutoPostBack="true" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td>
                                        <cms:LocalizedLabel ID="lblCurrentPageOnly" runat="server" ResourceString="export.currentpage"
                                            DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="chkCurrentPageOnly" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkCurrentPageOnly" runat="server" Checked="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <cms:LocalizedLabel ID="lblRecords" runat="server" ResourceString="export.records"
                                            DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="txtRecords" />
                                    </td>
                                    <td>
                                        <cms:CMSTextBox ID="txtRecords" runat="server" CssClass="TextBoxField" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <cms:CMSRegularExpressionValidator ID="revRecords" runat="server" Display="Dynamic"
                                            ControlToValidate="txtRecords" EnableClientScript="true" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plcExportHeader" runat="server">
                                    <tr>
                                        <td>
                                            <cms:LocalizedLabel ID="lblExportHeader" runat="server" ResourceString="export.columnheader"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="chkExportHeader" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkExportHeader" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td style="vertical-align: top;">
                                        <cms:LocalizedLabel ID="lblOrderBy" runat="server" ResourceString="export.orderby"
                                            DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" />
                                    </td>
                                    <td>
                                        <cms:OrderByControl ID="orderByElem" runat="server" ShortID="o" DelayedReload="True"
                                            Mode="DropDownList" />
                                        <cms:ExtendedTextArea ID="txtOrderBy" runat="server" EditorMode="Basic" Language="SQL"
                                            CssClass="TextAreaField" />
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="plcWhere" runat="server">
                                    <tr>
                                        <td>
                                            <cms:LocalizedLabel ID="lblUseGridFilter" runat="server" ResourceString="export.usegridfilter"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="chkUseGridFilter" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkUseGridFilter" runat="server" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">
                                            <cms:LocalizedLabel ID="lblWhereCondition" runat="server" ResourceString="export.where"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="txtWhereCondition" />
                                        </td>
                                        <td>
                                            <cms:ExtendedTextArea ID="txtWhereCondition" runat="server" EditorMode="Basic" Language="SQL"
                                                CssClass="TextAreaField" />
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="pnlColumns" runat="server">
                                            <div class="ColumnControl">
                                                <cms:LocalizedLinkButton ID="lnkSelectAll" runat="server" ResourceString="export.selectall"
                                                    EnableViewState="false" />
                                                <cms:LocalizedLinkButton ID="lnkDeselectAll" runat="server" ResourceString="export.deselectall"
                                                    EnableViewState="false" />
                                                <cms:LocalizedLinkButton ID="lnkDefaultSelection" runat="server" ResourceString="export.defaultselection"
                                                    EnableViewState="false" />
                                            </div>
                                            <div class="ColumnValidator">
                                                <asp:CustomValidator ID="cvColumns" runat="server" Display="Dynamic" EnableClientScript="true" />
                                            </div>
                                            <asp:CheckBoxList ID="chlColumns" runat="server" CssClass="ColumnsCheckBoxList" />
                                        </asp:Panel>
                                        <div class="ColumnIndent">
                                            &nbsp;</div>
                                    </td>
                                </tr>
                            </table>
                        </asp:PlaceHolder>
                    </div>
                </asp:Panel>
                <div class="PageFooterLine">
                    <div class="LeftAlign">
                        <cms:LocalizedButton ID="btnPreview" runat="server" EnableViewState="false" ResourceString="general.preview"
                            CssClass="ContentButton" OnClick="btnPreview_Click" />
                    </div>
                    <div class="Buttons">
                        <cms:LocalizedButton ID="btnExport" runat="server" EnableViewState="false" ResourceString="general.export"
                            CssClass="SubmitButton" OnClick="btnExport_Click" />
                        <cms:LocalizedButton ID="btnClose" runat="server" EnableViewState="false" ResourceString="general.close"
                            CssClass="SubmitButton" OnClick="btnClose_Click" CausesValidation="false" /></div>
                    <div class="ClearBoth">
                        &nbsp;</div>
                </div>
            </asp:Panel>
        </cms:ModalPopupDialog>
        <asp:HiddenField ID="hdnDefaultSelection" runat="server" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
<asp:HiddenField ID="hdnParameter" runat="server" />
<asp:Button ID="btnFullPostback" runat="server" CssClass="HiddenButton" OnClick="btnFullPostback_Click"
    Style="display: none;" />
