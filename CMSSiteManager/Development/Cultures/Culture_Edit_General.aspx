<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Culture_Edit_General.aspx.cs"
    Inherits="CMSSiteManager_Development_Cultures_Culture_Edit_General" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Culture Edit - General" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">    
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCultureName" runat="server" ResourceString="Culture_Edit_General.CultureName" 
                    EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtCultureName" runat="server" CssClass="TextBoxField" MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCultureName" runat="server" ControlToValidate="txtCultureName:textbox"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCultureCode" runat="server" ResourceString="Culture_Edit_General.CultureCode" 
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCultureCode" runat="server" CssClass="TextBoxField" MaxLength="10" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCultureCode" runat="server" ControlToValidate="txtCultureCode"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCultureShortName" runat="server" ResourceString="Culture_Edit_General.CultureShortName" 
                    EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCultureShortName" runat="server" CssClass="TextBoxField" MaxLength="200" EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvCultureShortName" runat="server" ControlToValidate="txtCultureShortName"
                    EnableViewState="false" />
            </td>
        </tr>
         <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="LocalizedLabel1" DisplayColon="true" runat="server" EnableViewState="false"
                    ResourceString="CultureEdit.CultureAlias" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCultureAlias" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton" 
                    ResourceString="general.ok" EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>