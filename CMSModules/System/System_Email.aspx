<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_System_Email"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" 
    Title="Administration - System - Email" CodeFile="System_Email.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" TagName="File" TagPrefix="uc1" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">            
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblServer" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.SMTPServer" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtServer" runat="server" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvServer" runat="server" ControlToValidate="txtServer"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblUserName" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.SMTPUserName" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtUserName" runat="server" CssClass="TextBoxField" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblPassword" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.SMTPPassword" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPassword" runat="server" CssClass="TextBoxField" TextMode="Password" />
            </td>
        </tr>

        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblSSL" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.SSL" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkSSL" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblFrom" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.From" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtFrom" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvFrom" runat="server" ControlToValidate="txtFrom"
                    Display="dynamic" EnableViewState="false" />
                <cms:CMSRegularExpressionValidator ID="revFrom" runat="server" ControlToValidate="txtFrom"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblTo" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.To" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTo" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvTo" runat="server" ControlToValidate="txtTo" Display="dynamic"
                    EnableViewState="false" />
                <cms:CMSRegularExpressionValidator ID="revTo" runat="server" ControlToValidate="txtTo"
                    Display="dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblSubject" runat="server" EnableViewState="false" 
                    ResourceString="general.subject" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="TextBoxField" MaxLength="450" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblText" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.Text" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtText" runat="server" CssClass="TextAreaField" TextMode="MultiLine" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAttachment" runat="server" EnableViewState="false" 
                    ResourceString="System_Email.Attachment" DisplayColon="true" />
            </td>
            <td>
                <cms:Uploader ID="FileUploader" runat="server" BorderStyle="none" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>            
                <cms:LocalizedButton ID="btnSend" runat="server" CssClass="SubmitButton" OnClick="btnSend_Click"
                    ResourceString="System_Email.Send" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>