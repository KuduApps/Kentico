<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_UICultures_Pages_Development_UICulture_New"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" CodeFile="New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel"
        Visible="false" />
    <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblUICultureName" runat="server" EnableViewState="false"
                    ResourceString="UICultures_New.CultureName" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtUICultureName" runat="server" MaxLength="200" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvUICultureName" runat="server" ControlToValidate="txtUICultureName:textbox"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblUICultureCode" runat="server" EnableViewState="false"
                    ResourceString="UICultures_New.CultureCode" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtUICultureCode" runat="server" MaxLength="10" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvUICultureCode" runat="server" ControlToValidate="txtUICultureCode"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>
