<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_DiscountLevels_DiscountLevel_Edit" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Discount level - edit" CodeFile="DiscountLevel_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblDiscountLevelDisplayName" EnableViewState="false"
                    ResourceString="general.displayname" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDiscountLevelDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="200" EnableViewState="false" />&nbsp;
                <cms:CMSRequiredFieldValidator ID="rfvDiscountLevelDisplayName" runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="txtDiscountLevelDisplayName:textbox" Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblDiscountLevelName" EnableViewState="false"
                    ResourceString="general.codename" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDiscountLevelName" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />&nbsp;
                <cms:CMSRequiredFieldValidator ID="rfvDiscountLevelName" runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="txtDiscountLevelName" Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblDiscountLevelValue" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDiscountLevelValue" runat="server" CssClass="TextBoxField" MaxLength="10"
                    EnableViewState="false" />&nbsp;%&nbsp;
                <cms:CMSRequiredFieldValidator ID="rfvDiscountLevelValue" runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="txtDiscountLevelValue" Display="Dynamic" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblDiscountLevelValidFrom" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerDiscountLevelValidFrom" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblDiscountLevelValidTo" EnableViewState="false" />
            </td>
            <td>
                <cms:DateTimePicker ID="dtPickerDiscountLevelValidTo" runat="server" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblDiscountLevelEnabled" EnableViewState="false"
                    ResourceString="general.enabled" DisplayColon="true" />
            </td>
            <td>
                <asp:CheckBox ID="chkDiscountLevelEnabled" runat="server" CssClass="CheckBoxMovedLeft"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
