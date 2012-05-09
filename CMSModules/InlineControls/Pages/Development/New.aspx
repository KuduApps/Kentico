<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_InlineControls_Pages_Development_New" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Inline control - edit" CodeFile="New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/UserControlSelector.ascx" TagPrefix="cms"
    TagName="FileSystemSelector" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <table style="vertical-align: top">
        <tr>
            <td style="vertical-align: top; padding-top: 5px" class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblControlDisplayName" EnableViewState="false" ResourceString="InlineControl_Edit.ControlDisplayNameLabel" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtControlDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtControlDisplayName:textbox"
                    Display="Dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblControlName" EnableViewState="false" ResourceString="InlineControl_Edit.ControlNameLabel" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtControlName" runat="server" CssClass="TextBoxField" MaxLength="200" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtControlName"
                    Display="Dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblControlFileName" DisplayColon="true" EnableViewState="false" ResourceString="InlineControl_Edit.ControlFileNameLabel" />
            </td>
            <td>
                <cms:FileSystemSelector ID="FileSystemSelector" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblControlParameterName" EnableViewState="false" ResourceString="InlineControl_Edit.ControlParameterNameLabel" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtControlParameterName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblDescription" EnableViewState="false" ResourceString="general.description"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtControlDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:CheckBox runat="server" ID="chkAssign" Visible="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ResourceString="General.OK" />
            </td>
        </tr>
    </table>
</asp:Content>
