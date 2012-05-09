<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Configuration_PublicStatus_PublicStatus_Edit" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Public status - Edit" CodeFile="PublicStatus_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPublicStatusDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtPublicStatusDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" ControlToValidate="txtPublicStatusDisplayName:textbox"
                    runat="server" Display="Dynamic" ValidationGroup="PublicStatus" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPublicStatusName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtPublicStatusName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" ControlToValidate="txtPublicStatusName"
                    runat="server" Display="Dynamic" ValidationGroup="PublicStatus" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblPublicStatusEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkPublicStatusEnabled" runat="server" CssClass="CheckBoxMovedLeft"
                    Checked="true" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="PublicStatus" />
            </td>
        </tr>
    </table>
</asp:Content>
