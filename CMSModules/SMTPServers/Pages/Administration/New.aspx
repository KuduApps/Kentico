<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_SMTPServers_Pages_Administration_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="New SMTP server"
    CodeFile="New.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ResourceString="SMTPServer_Edit.ServerNameLabel" 
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="txtServerName" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtServerName" MaxLength="200" CssClass="TextBoxField" />                
                <cms:CMSRequiredFieldValidator runat="server" ID="rfvServerName" ControlToValidate="txtServerName" />
            </td>            
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ResourceString="SMTPServer_Edit.ServerUserNameLabel" 
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="txtUserName" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtUserName" MaxLength="50" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td>               
                <cms:LocalizedLabel runat="server" ResourceString="SMTPServer_Edit.ServerPasswordLabel"
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="txtPassword" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtPassword" MaxLength="50" CssClass="TextBoxField"
                    TextMode="Password" />
            </td>    
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ResourceString="SMTPServer_Edit.ServerUseSSLLabel"
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="chkUseSSL" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkUseSSL" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ResourceString="SMTPServer_Edit.ServerPriorityLabel"
                    DisplayColon="true" EnableViewState="false" AssociatedControlID="ddlPriorities" />
            </td>
            <td>
                <cms:LocalizedDropDownList runat="server" ID="ddlPriorities" UseResourceStrings="true"
                    CssClass="DropDownField"/>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <cms:LocalizedCheckBox runat="server" ID="chkAssign" Checked="true" Visible="false" />
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