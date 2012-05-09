<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_Workflows_Workflow_Step_New"
    Theme="Default" Title="Workflows - New workflow step" CodeFile="Workflow_Step_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblError" ForeColor="red" EnableViewState="false" Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblWorkflowDisplayName" runat="server" EnableViewState="false"
                    ResourceString="general.displayname" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtWorkflowDisplayName" runat="server" CssClass="TextBoxField" MaxLength="450" />
                <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorDisplayName" runat="server"
                    EnableViewState="false" ControlToValidate="txtWorkflowDisplayName:textbox" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblWorkflowCodeName" runat="server" EnableViewState="false"
                    ResourceString="general.codename" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtWorkflowCodeName" runat="server" CssClass="TextBoxField" MaxLength="440" />
                <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorCodeName" runat="server" EnableViewState="false"
                    ControlToValidate="txtWorkflowCodeName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="SubmitButton"
                    EnableViewState="false" ResourceString="General.OK" />
            </td>
        </tr>
    </table>
</asp:Content>
