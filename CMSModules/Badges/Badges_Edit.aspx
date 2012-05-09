<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Badges_Badges_Edit"
    ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="Badges_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <div>
        <cms:LocalizedLabel ID="lblSaved" runat="server" ResourceString="general.changessaved"
            Visible="false" CssClass="InfoLabel" EnableViewState="false" />
        <cms:LocalizedLabel ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
    </div>
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDisplayName" runat="server" ResourceString="general.displayName"
                    DisplayColon="true" EnableViewState="false" /></td>
            <td class="FieldValue">
                <cms:LocalizableTextBox runat="server" ID="txtDisplayName" MaxLength="200" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                    ValidationGroup="validateOK" EnableViewState="false" Display="Dynamic" ></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblName" runat="server" ResourceString="general.codename" DisplayColon="true" EnableViewState="false" /></td>
            <td class="FieldValue">
                <cms:CMSTextBox runat="server" ID="txtName" MaxLength="100" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                    ValidationGroup="validateOK" EnableViewState="false" Display="Dynamic" ></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblImageURL" runat="server" ResourceString="badge.imageurl"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td class="FieldValue">
                <cms:CMSTextBox runat="server" ID="txtImageURL" MaxLength="100" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblIsAutomatic" runat="server" ResourceString="badge.isautomatic"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td class="FieldValue">
                <asp:CheckBox ID="chkIsAutomatic" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblTopLimit" runat="server" ResourceString="badge.toplimit"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td class="FieldValue">
                <cms:CMSTextBox runat="server" ID="txtTopLimit" MaxLength="9" CssClass="TextBoxField" />
                <cms:CMSRangeValidator ID="rvtxtTopLimit" ControlToValidate="txtTopLimit" runat="server"
                    MaximumValue="999999999" MinimumValue="0" ValidationGroup="validateOK" EnableViewState="false" Display="Dynamic" ></cms:CMSRangeValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
            </td>
            <td class="FieldValue">
                <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.OK" ValidationGroup="validateOK"
                    CssClass="SubmitButton" OnClick="btnOK_Click" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
