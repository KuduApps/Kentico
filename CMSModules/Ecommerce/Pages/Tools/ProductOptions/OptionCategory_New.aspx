<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_New" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Option Category - New" CodeFile="OptionCategory_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblDisplayName" EnableViewState="false" ResourceString="general.displayname"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" ControlToValidate="txtDisplayName:textbox"
                    runat="server" Display="Dynamic" ValidationGroup="OptionCategories" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCategoryName" EnableViewState="false" ResourceString="general.codename"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCategoryName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCategoryName" ControlToValidate="txtCategoryName"
                    runat="server" Display="Dynamic" ValidationGroup="OptionCategories" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCategoryType" EnableViewState="false" ResourceString="general.type"
                    DisplayColon="true" />
            </td>
            <td>
                <asp:RadioButton runat="server" ID="radSelection" GroupName="radCategoryType" Checked="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
            </td>
            <td>
                <asp:RadioButton runat="server" ID="radText" GroupName="radCategoryType" Checked="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="OptionCategories" />
            </td>
        </tr>
    </table>
</asp:Content>
