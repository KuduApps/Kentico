<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryEdit.ascx.cs"
    Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_CategoryEdit" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Panel ID="pnlCategory" runat="server">
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCategoryName" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.CategoryName" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtCategoryName" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
    </table>
</asp:Panel>
