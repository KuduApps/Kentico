<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="BizForms - New BizForm" Inherits="CMSModules_BizForms_Tools_BizForm_New"
    Theme="Default" CodeFile="BizForm_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblFormDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtFormDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="100" />
            </td>
            <td>
                <cms:CMSRequiredFieldValidator ID="rfvFormDisplayName" runat="server" ControlToValidate="txtFormDisplayName:textbox" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblFormName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtFormName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
            <td>
                <cms:CMSRequiredFieldValidator ID="rfvFormName" runat="server" ControlToValidate="txtFormName" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblTableName" EnableViewState="false" />
            </td>
            <td>
                <asp:Label ID="lblPrefix" runat="server" EnableViewState="false" /><cms:CMSTextBox ID="txtTableName"
                    runat="server" CssClass="SmallTextBox" />
            </td>
            <td>
                <cms:CMSRequiredFieldValidator ID="rfvTableName" runat="server" ControlToValidate="txtTableName" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
