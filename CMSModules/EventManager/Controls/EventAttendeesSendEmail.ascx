<%@ Control Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/EventManager/Controls/EventAttendeesSendEmail.ascx.cs"
    Inherits="CMSModules_EventManager_Controls_EventAttendeesSendEmail" %>
<cms:LocalizedLabel runat="server" ID="lblTitle" CssClass="SectionTitle" ResourceString="Events_SendEmail.lblTitle"
    EnableViewState="false" /><br />
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:PlaceHolder runat="server" ID="plcSend">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSenderName" runat="server" ResourceString="Events_SendEmail.lblSenderName"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSenderName" runat="server" CssClass="TextBoxField" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSenderEmail" runat="server" ResourceString="Events_SendEmail.lblSenderEmail"
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSenderEmail" runat="server" CssClass="TextBoxField" MaxLength="250" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblSubject" runat="server" ResourceString="general.subject"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextBoxField" MaxLength="450" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSHtmlEditor ID="htmlEmail" runat="server" Width="600" Height="435px" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnSend" runat="server" ResourceString="Events_SendEmail.btnSend"
                    CssClass="SubmitButton" OnClick="btnSend_Click" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:PlaceHolder>
