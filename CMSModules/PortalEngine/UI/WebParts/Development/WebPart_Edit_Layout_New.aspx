<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Layout_New"
    Theme="Default" CodeFile="WebPart_Edit_Layout_New.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Content ID="cnt" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCodeName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDescription" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                    MaxLength="450" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCode" EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextArea ID="tbCode" runat="server" EnableViewState="false" EditorMode="Advanced"
                    Width="800px" Height="600px" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcCssLink">
            <tr id="cssLink">
                <td class="FieldLabel">
                </td>
                <td>
                    <cms:LocalizedLinkButton runat="server" ID="lnkStyles" EnableViewState="false" ResourceString="general.addcss"
                        OnClientClick="document.getElementById('editCss').style.display = 'table-row'; document.getElementById('cssLink').style.display = 'none'; return false;" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr id="editCss" style="<%= (plcCssLink.Visible ? "display: none": "") %>">
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCSS" ResourceString="Container_Edit.ContainerCSS"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextArea ID="tbCSS" runat="server" EditorMode="Advanced" Language="CSS"
                    Width="800px" Height="200px" />
            </td>
        </tr>
    </table>
</asp:Content>
