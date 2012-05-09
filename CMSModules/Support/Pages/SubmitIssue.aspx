<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Support_Pages_SubmitIssue"
    Theme="Default" ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Support - Submit issue" CodeFile="SubmitIssue.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">    
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblEmail" EnableViewState="false" ResourceString="Support.SubmiIssue.Email" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtEmail" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSubject" ResourceString="general.subject" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtSubject" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblHtmlIssue" EnableViewState="false" ResourceString="Support.SubmiIssue.CkIssue" />
            </td>
            <td>
                <cms:CMSHtmlEditor ID="htmlTemplateBody" runat="server" Width="600px" Height="250px" AutoDetectLanguage="false" ToolbarSet="Basic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSysInfo" EnableViewState="false" ResourceString="Support.SubmiIssue.SysInfo" /><br />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtSysInfo" TextMode="MultiLine" Width="600px" Height="100px" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTemplate" EnableViewState="false" ResourceString="Support.SubmiIssue.Template" />
            </td>
            <td>                
                <cms:LocalizedRadioButton ID="radDontKnow" runat="server" GroupName="TemplateGroup" ResourceString="Support.SubmiIssue.DontKnow" Checked="true" />
                <cms:LocalizedRadioButton ID="radPortal" runat="server" GroupName="TemplateGroup" ResourceString="Support.SubmiIssue.Portal" />
                <cms:LocalizedRadioButton ID="radAspx" runat="server" GroupName="TemplateGroup" ResourceString="Support.SubmiIssue.Aspx" />
                <cms:LocalizedRadioButton ID="radMix" runat="server" GroupName="TemplateGroup" ResourceString="Support.SubmiIssue.Mix" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblSettings" EnableViewState="false" ResourceString="Support.SubmiIssue.Settings" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkSettings" Checked="true" /><br />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblAttachment" EnableViewState="false" ResourceString="Support.SubmiIssue.Attachment" />
            </td>
            <td>
                <cms:CMSFileUpload ID="fileUpload" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnSend" runat="server" OnClick="btnSend_Click" CssClass="SubmitButton" EnableViewState="false" ResourceString="Support.SubmiIssue.Send" />
            </td>
        </tr>
    </table>
</asp:Content>
