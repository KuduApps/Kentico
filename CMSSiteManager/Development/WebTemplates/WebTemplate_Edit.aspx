<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSSiteManager_Development_WebTemplates_WebTemplate_Edit"
    Theme="Default" Title="Web Template Edit" CodeFile="WebTemplate_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" TagName="File" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/LicenseSelector.ascx" TagName="LicenseSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/LicensePackageSelector.ascx" TagName="LicensePackageSelector"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" Visible="false" CssClass="InfoLabel" />
    <asp:Label runat="server" ID="lblError" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblWebTemplateDisplayName" runat="server" EnableViewState="False"
                    ResourceString="Administration-WebTemplate_New.WebTemplateDisplayName" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtWebTemplateDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="100" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvWebTemplateDisplayName" runat="server" EnableViewState="false"
                    ControlToValidate="txtWebTemplateDisplayName:textbox" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblWebTemplateName" runat="server" EnableViewState="False"
                    ResourceString="Administration-WebTemplate_New.WebTemplateName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtWebTemplateName" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvWebTemplateName" runat="server" EnableViewState="false"
                    ControlToValidate="txtWebTemplateName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <cms:LocalizedLabel ID="lblWebTemplateFileName" runat="server" EnableViewState="False"
                    ResourceString="Administration-WebTemplate_New.WebTemplateFileName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtWebTemplateFileName" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvWebTemplateFileName" runat="server" EnableViewState="false"
                    ControlToValidate="txtWebTemplateFileName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <cms:LocalizedLabel ID="lblWebTemplateDescription" runat="server" EnableViewState="False"
                    ResourceString="Administration-WebTemplate_New.WebTemplateDescription" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtWebTemplateDescription" runat="server" MaxLength="100" CssClass="TextAreaField"
                    TextMode="MultiLine" EnableViewState="false" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvWebTemplateDescription" runat="server" ControlToValidate="txtWebTemplateDescription:textbox"
                    Display="Dynamic" EnableViewState="False" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblUploadFile" runat="server" EnableViewState="false" ResourceString="Administration-PageLayout_New.lblUpload" />
            </td>
            <td>
                <cms:File ID="attachmentFile" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblEditions" runat="server" EnableViewState="false" ResourceString="Administration-PageLayout_New.lblEditions" />
            </td>
            <td>
                <cms:LicenseSelector ID="ucLicenseSelector" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblPackages" runat="server" EnableViewState="false" ResourceString="Administration-PageLayout_New.lblPackages" />
            </td>
            <td>
                <cms:LicensePackageSelector ID="ucLicensePackageSelector" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>
