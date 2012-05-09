<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_Departments_Department_New" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="Department_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDepartmentDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDepartmentDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" ControlToValidate="txtDepartmentDisplayName:textbox"
                    runat="server" Display="Dynamic" ValidationGroup="Departments" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDepartmentName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDepartmentName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" ControlToValidate="txtDepartmentName"
                    runat="server" Display="Dynamic" ValidationGroup="Departments" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="Departments" />
            </td>
        </tr>
    </table>
</asp:Content>
