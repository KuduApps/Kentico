<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SMTPServers_Pages_Administration_Tab_General"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="SMTP Server Edit - General"
    CodeFile="Tab_General.aspx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Passwords/EncryptedPassword.ascx" TagPrefix="cms" TagName="EncryptedPassword" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">  
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblServerName" ResourceString="SMTPServer_Edit.ServerNameLabel" 
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="txtServerName" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtServerName" MaxLength="200" CssClass="TextBoxField" />                
                <cms:CMSRequiredFieldValidator runat="server" ID="rfvServerName" ControlToValidate="txtServerName" />
            </td>            
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblUserName" ResourceString="SMTPServer_Edit.ServerUserNameLabel" 
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="txtUserName" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtUserName" MaxLength="50" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td>               
                <cms:LocalizedLabel runat="server" ID="lblPassword" ResourceString="SMTPServer_Edit.ServerPasswordLabel"
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="encryptedPassword" />
            </td>
            <td>
                <cms:EncryptedPassword runat="server" ID="encryptedPassword" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblUseSSL" ResourceString="SMTPServer_Edit.ServerUseSSLLabel" 
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="chkUseSSL" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkUseSSL" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblPriorities" runat="server" ResourceString="SMTPServer_Edit.ServerPriorityLabel"
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="ddlPriorities" />
            </td>
            <td>
                <cms:LocalizedDropDownList runat="server" ID="ddlPriorities" UseResourceStrings="true"
                    CssClass="DropDownField"/>
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblEnabled" runat="server" ResourceString="general.enabled"
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="chkEnabled" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkEnabled" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <cms:FormSubmitButton runat="server" ID="btnOk" OnClick="btnOk_Click" CausesValidation="true" />
            </td>
        </tr>
    </table>   
</asp:Content>