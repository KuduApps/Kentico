<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MediaLibrary_Controls_UI_MediaLibraryEdit" CodeFile="MediaLibraryEdit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" TagName="File" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:PlaceHolder ID="plcProperties" runat="server">
        <table style="vertical-align: top">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblDisplayName" EnableViewState="false" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                        ValidationGroup="Required" EnableViewState="false" Display="Dynamic" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcCodeName" runat="server">
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ID="lblCodeName" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                        <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                            ValidationGroup="Required" EnableViewState="false" Display="Dynamic" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblDescription" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblTeaser" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:File ID="ucMetaFile" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblFolder" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtFolder" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvFolder" runat="server" ControlToValidate="txtFolder"
                        ValidationGroup="Required" EnableViewState="false" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                        ValidationGroup="Required" OnClick="btnOK_Click" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
</asp:Panel>
