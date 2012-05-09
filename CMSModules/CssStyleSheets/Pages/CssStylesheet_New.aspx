<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CssStylesheets_Pages_CssStylesheet_New"
         Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="New CSS stylesheet"
         CodeFile="CssStylesheet_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlContainer" runat="server" EnableViewState="false">                    
        <table style="width: 100%">
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblCssStylesheetDisplayName" runat="server" EnableViewState="False" />
                </td>
                <td style="width: 100%">
                    <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                    <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" EnableViewState="false" ControlToValidate="txtDisplayName:textbox" Display="dynamic" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblCssStylesheetName" runat="server" EnableViewState="False" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                    <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" EnableViewState="false" ControlToValidate="txtName" Display="dynamic" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblCssStylesheetText" runat="server" Text="Label" />
                </td>
                <td>
                    <cms:ExtendedTextArea ID="txtText" runat="server" EnableViewState="false"
                        EditorMode="Advanced" Language="CSS" Height="385px" Width="99%" />                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:CheckBox runat="server" ID="chkAssign" Visible="false" Checked="true" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="false" OnClick="btnOK_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
