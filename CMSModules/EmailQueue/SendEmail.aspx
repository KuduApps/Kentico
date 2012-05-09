<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_EmailQueue_SendEmail"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="SendEmail.aspx.cs" %>
    
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileUploader.ascx" TagName="FileUploader" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">    
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblFrom" runat="server" CssClass="FieldLabel" EnableViewState="false"
                ResourceString="general.fromemail" DisplayColon="true" ShowRequiredMark="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtFrom" runat="server" CssClass="TextBoxField" MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvFrom" runat="server" ControlToValidate="txtFrom"
                    Display="dynamic" EnableViewState="false" />
                <cms:CMSRegularExpressionValidator ID="revFrom" runat="server" ControlToValidate="txtFrom"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblTo" runat="server" CssClass="FieldLabel" EnableViewState="false"
                ResourceString="general.toemail" DisplayColon="true" ShowRequiredMark="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTo" runat="server" CssClass="TextBoxField" MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvTo" runat="server" ControlToValidate="txtTo" Display="dynamic"
                    EnableViewState="false" />
                <cms:CMSRegularExpressionValidator ID="revTo" runat="server" ControlToValidate="txtTo"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblCc" runat="server" CssClass="FieldLabel" EnableViewState="false"
                ResourceString="general.cc" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCc" runat="server" CssClass="TextBoxField" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblBcc" runat="server" CssClass="FieldLabel" EnableViewState="false"
                ResourceString="general.bcc" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtBcc" runat="server" CssClass="TextBoxField" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSubject" runat="server" CssClass="FieldLabel" EnableViewState="false"
                ResourceString="general.subject" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextBoxField" MaxLength="450" />
            </td>
        </tr>
    </table>
    <table>
        <asp:PlaceHolder runat="server" ID="plcText">
            <tr>
                <td colspan="2">
                    <cms:LocalizedLabel ID="lblText" runat="server" CssClass="FieldLabel" EnableViewState="false"
                    ResourceString="general.text" DisplayColon="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:CMSHtmlEditor ID="htmlText" runat="server" Width="625" Height="400px" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcPlainText">
            <tr>
                <td colspan="2">
                    <cms:LocalizedLabel ID="lblPlainText" runat="server" CssClass="FieldLabel" EnableViewState="false"
                    ResourceString="general.plaintext" DisplayColon="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:CMSTextBox ID="txtPlainText" runat="server" CssClass="TextAreaLarge" TextMode="MultiLine" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlAttachments" runat="server">
                    <cms:FileUploader ID="uploader" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <br />
                <cms:LocalizedButton ID="btnSend" runat="server" CssClass="LongSubmitButton" OnClick="btnSend_Click" 
                EnableViewState="false" ResourceString="general.send" />
            </td>
        </tr>
    </table>
</asp:Content>