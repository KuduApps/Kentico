<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Category"
    Theme="Default" CodeFile="WebPart_Category.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblInfo" runat="server" /><asp:Label EnableViewState="false"
        ForeColor="red" ID="lblError" runat="server" Visible="false" />
    <asp:Literal ID="ltlScript" runat="server" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lblCategoryDisplayName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtCategoryDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvCategoryDisplayName" runat="server" ErrorMessage="RequiredFieldValidator"
                    ControlToValidate="txtCategoryDisplayName:textbox"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcCategoryName">
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblCategoryName" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCategoryName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <cms:CMSRequiredFieldValidator ID="rfvCategoryName" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="txtCategoryName"></cms:CMSRequiredFieldValidator>
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCategoryImagePath" runat="server" EnableViewState="false" ResourceString="webpartcategory.imagepath" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCategoryImagePath" runat="server" CssClass="TextBoxField" MaxLength="450" />                
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
