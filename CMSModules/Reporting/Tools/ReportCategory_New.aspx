<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Reporting_Tools_ReportCategory_New" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" CodeFile="ReportCategory_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
        <asp:Literal runat="server" ID="ltlScript" />       
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCategoryDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtCategoryDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtCategoryDisplayName:textbox" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblCategoryCodeName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCategoryCodeName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCategoryCodeName" />
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
