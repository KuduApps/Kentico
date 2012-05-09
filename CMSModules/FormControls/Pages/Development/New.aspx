<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs"
    Inherits="CMSModules_FormControls_Pages_Development_New"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Form Controls - New form control" Theme="Default" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSFormControls/System/UserControlSelector.ascx" TagPrefix="cms"
    TagName="FileSystemSelector" %>
<%@ Register Src="~/CMSFormControls/System/UserControlTypeSelector.ascx" TagPrefix="cms"
    TagName="TypeSelector" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel runat="server" ID="lblError" ForeColor="red" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDisplayName" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="general.displayname" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtControlDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvControlDisplayName" runat="server" ControlToValidate="txtControlDisplayName:textbox"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCodeName" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="general.codename" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtControlName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvControlName" runat="server" ControlToValidate="txtControlName"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblType" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="formcontrols.type" DisplayColon="true" />
            </td>
            <td>
                <cms:TypeSelector ID="drpTypeSelector" runat="server" CssClass="DropDownField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblFileName" runat="server" CssClass="ContentLabel" EnableViewState="false"
                    ResourceString="general.filename" DisplayColon="true" />
            </td>
            <td>
                <cms:FileSystemSelector ID="tbFileName" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>
