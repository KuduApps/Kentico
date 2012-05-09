<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tab_General.aspx.cs"
    Inherits="CMSModules_UICultures_Pages_Development_UICulture_Tab_General"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" Title="UI Culture Edit" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">    
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblUICultureName" runat="server" EnableViewState="false"
                    ResourceString="Development-UICulture_Edit.UICultureName" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtUICultureName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvUICultureName" runat="server" ControlToValidate="txtUICultureName:textbox"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblUICultureCode" runat="server" EnableViewState="false"
                    ResourceString="Development-UICulture_Edit.UICultureCode" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtUICultureCode" runat="server" CssClass="TextBoxField" MaxLength="10" />
                <cms:CMSRequiredFieldValidator ID="rfvUICultureCode" runat="server" ControlToValidate="txtUICultureCode"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                    EnableViewState="false" ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>