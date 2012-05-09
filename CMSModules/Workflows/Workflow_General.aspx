<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Workflows_Workflow_General" Theme="Default"
    CodeFile="Workflow_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDisplayName" runat="server" EnableViewState="false" ResourceString="general.displayname"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="TextBoxWorkflowDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="450" />
                <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorDisplayName" runat="server"
                    EnableViewState="false" ControlToValidate="TextBoxWorkflowDisplayName:textbox" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCodeName" runat="server" EnableViewState="false" ResourceString="general.codename"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="450" />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" EnableViewState="false"
                    ControlToValidate="txtCodeName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAutoPublish" runat="server" EnableViewState="false" ResourceString="development-workflow_general.autopublish"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkAutoPublish" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblUseCheckInCheckOut" runat="server" EnableViewState="false"
                    ResourceString="development-workflow_general.usecheckincheckout" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizedRadioButton ID="radSiteSettings" runat="server" GroupName="UseCheckInCheckOut"
                    EnableViewState="false" ResourceString="development-workflow_general.sitesettings" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedRadioButton ID="radYes" runat="server" GroupName="UseCheckInCheckOut"
                    EnableViewState="false" ResourceString="general.yes" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedRadioButton ID="radNo" runat="server" GroupName="UseCheckInCheckOut"
                    EnableViewState="false" ResourceString="general.no" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" OnClick="ButtonOK_Click" CssClass="SubmitButton"
                    ResourceString="general.ok" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
