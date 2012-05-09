<%@ Page Title="Localize string" Language="C#" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    AutoEventWireup="true" EnableEventValidation="false" Inherits="CMSFormControls_Selectors_LocalizableTextBox_LocalizeString"
    CodeFile="LocalizeString.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="content" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlContent" CssClass="PageContent" runat="server">
        <asp:HiddenField ID="hdnID" runat="server" />
        <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" Visible="false"
            CssClass="InfoLabel" ResourceString="general.changessaved" />
        <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" Visible="false"
            CssClass="ErrorLabel" />
        <asp:Panel ID="pnlControls" runat="server">
            <table>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblStringKey" runat="server" DisplayColon="true" ResourceString="localizable.resourcekey"
                            EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtStringKey" runat="server" CssClass="TextBoxField" MaxLength="200" />
                        <cms:CMSRequiredFieldValidator ID="rfvKey" runat="server" ControlToValidate="txtStringKey"
                            Display="Dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcIsCustom" runat="server" Visible="false">
                    <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblIsCustom" runat="server" DisplayColon="true" ResourceString="localizable.iscustom"
                                EnableViewState="false" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsCustom" runat="server" Checked="true" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <cms:LocalizedLabel ID="lblTranlsations" runat="server" EnableViewState="false" DisplayColon="true"
                            ResourceString="localizable.translations" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <cms:LocalizedCheckBox ID="chkDefaultCulture" runat="server" ResourceString="localizable.defaultculture"
                            Checked="true" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <cms:LocalizedCheckBox ID="chkSiteCultures" runat="server" ResourceString="localizable.sitecultures"
                            Checked="true" AutoPostBack="true" />
                    </td>
                </tr>
            </table>
            <br />
            <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Table ID="tblGrid" runat="server" GridLines="Horizontal" CssClass="UniGridGrid"
                        CellSpacing="0" CellPadding="3" Width="95%">
                        <asp:TableHeaderRow CssClass="UniGridHead" ID="tblHeaderRow" runat="server" HorizontalAlign="Left">
                            <asp:TableHeaderCell HorizontalAlign="Left" Scope="Column" ID="tblHeaderCellFilter"
                                runat="server">
                                <asp:Panel ID="pnlHeaderCell" runat="server" class="UniMatrixFilter">
                                    <cms:CMSTextBox runat="server" ID="txtFilter" CssClass="ShortTextBox" />
                                    <cms:LocalizedButton runat="server" ID="btnFilter" OnClick="btnFilter_Click" ResourceString="general.search" />
                                </asp:Panel>
                                <cms:LocalizedLabel ID="lblHeaderCell" runat="server" EnableViewState="false" ResourceString="general.language" />
                            </asp:TableHeaderCell>
                            <asp:TableHeaderCell HorizontalAlign="Center" Scope="Column" ID="tblHeaderCellLabel"
                                runat="server" EnableViewState="false"></asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="chkDefaultCulture" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chkSiteCultures" EventName="CheckedChanged" />
                </Triggers>
            </cms:CMSUpdatePanel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="footer" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" ResourceString="general.ok"
            EnableViewState="false" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.cancel"
            EnableViewState="false" OnClientClick="window.close(); return false;" />
        <cms:LocalizedButton ID="btnApply" runat="server" CssClass="SubmitButton" ResourceString="general.apply"
            EnableViewState="false" />
    </div>
</asp:Content>
