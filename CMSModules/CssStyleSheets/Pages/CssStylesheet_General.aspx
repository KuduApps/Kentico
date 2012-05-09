<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CssStylesheets_Pages_CssStylesheet_General"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Edit CSS Stylesheet"
    CodeFile="CssStylesheet_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntBeforeBody" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel ID="pnlCheckOutInfo" runat="server" CssClass="PageContentLine">
        <asp:Label runat="server" EnableViewState="false" ID="lblCheckOutInfo" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlContainer" runat="server">
        <table style="width: 100%">
            <tbody>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCssStylesheetDisplayName" runat="server" EnableViewState="False" />
                    </td>
                    <td style="width: 100%">
                        <cms:LocalizableTextBox ID="txtCssStylesheetDisplayName" runat="server" CssClass="TextBoxField"
                            MaxLength="200" AutomaticMode="true" />
                        <br />
                        <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" EnableViewState="false"
                            Display="dynamic" ControlToValidate="txtCssStylesheetDisplayName:textbox"></cms:CMSRequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCssStylesheetName" runat="server" EnableViewState="False" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCssStylesheetName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                        <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" EnableViewState="false" Display="dynamic"
                            ControlToValidate="txtCssStylesheetName"></cms:CMSRequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <asp:Label ID="lblCssStylesheetText" runat="server" />
                    </td>
                    <td>
                        <cms:MacroEditor ID="txtCssStylesheetText" runat="server" EnableViewState="false"
                            Editor-EditorMode="Advanced" Editor-Language="CSS" Editor-ShowBookmarks="true" Editor-Width="100%" Editor-Height="385px"
                            Editor-RegularExpression="\s*/\*\s*#\s*([a-zA-Z_0-9-/\+\*.=~\!@\$%\^&\(\[\]\);:<>\?\s]*)\s*#\s*\*/"
                            Editor-EnablePositionMember="true" Editor-EnableSections="true" />
                    </td>
                </tr>
            </tbody>
        </table>
    </asp:Panel>
</asp:Content>
