<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Workflows_Workflow_New"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="Workflow_New.aspx.cs" %>

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
                <cms:LocalizableTextBox ID="txtWorkflowDisplayName" runat="server" CssClass="TextBoxField" MaxLength="450" />
                <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorDisplayName" runat="server"
                    EnableViewState="false" ControlToValidate="txtWorkflowDisplayName:textbox" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCodeName" runat="server" EnableViewState="false" ResourceString="general.codename"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtWorkflowCodeName" runat="server" CssClass="TextBoxField" MaxLength="450" />
                <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorCodeName" runat="server" EnableViewState="false"
                    ControlToValidate="txtWorkflowCodeName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
