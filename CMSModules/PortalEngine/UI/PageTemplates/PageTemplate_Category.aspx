<%@ Page Language="C#" AutoEventWireup="true" 
    Inherits="CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Category" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Development - Page template category" CodeFile="PageTemplate_Category.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" Visible="false" />
    <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" Visible="false" CssClass="ItemsNotAvailable" />
    
    <asp:Literal ID="ltlScript" runat="server" />
    <table>
    <tr>
        <td class="FieldLabel">
            <asp:Label ID="lblCategoryDisplayName" runat="server" EnableViewState="false" /> 
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtCategoryDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            <cms:CMSRequiredFieldValidator ID="rfvCategoryDisplayName" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCategoryDisplayName:textbox" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcCategoryName">
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblCategoryName" runat="server" EnableViewState="false" /> 
            </td>
            <td>
                <cms:CMSTextBox ID="txtCategoryName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvCategoryName" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCategoryName" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblCategoryImagePath" runat="server" EnableViewState="false" ResourceString="pagetemplatecategory.imagepath" DisplayColon="true" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtCategoryImagePath" runat="server" CssClass="TextBoxField" MaxLength="450" />                
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton" />
        </td>
    </tr>   
    </table>
</asp:Content>
