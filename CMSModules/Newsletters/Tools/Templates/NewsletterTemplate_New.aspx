<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Tools_Templates_NewsletterTemplate_New" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Tools - Newsletter template edit" CodeFile="NewsletterTemplate_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top" cellspacing="5">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTemplateDisplayName" EnableViewState="false"
                    ResourceString="general.displayname" DisplayColon="true" /></td>
            <td>
                <cms:LocalizableTextBox ID="txtTemplateDisplayName" runat="server" CssClass="TextBoxField" MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvTemplateDisplayName" runat="server" ControlToValidate="txtTemplateDisplayName:textbox"
                    Display="dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTemplateName" EnableViewState="false" ResourceString="general.codename"
                    DisplayColon="true" /></td>
            <td>
                <cms:CMSTextBox ID="txtTemplateName" runat="server" CssClass="TextBoxField" MaxLength="250" />
                <cms:CMSRequiredFieldValidator ID="rfvTemplateName" runat="server" ControlToValidate="txtTemplateName"
                    Display="dynamic"  EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblTemplateType" EnableViewState="false" /></td>
            <td>
                <asp:DropDownList ID="drpTemplateType" runat="server" CssClass="DropDownField" /></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" /></td>
        </tr>
    </table>
</asp:Content>
