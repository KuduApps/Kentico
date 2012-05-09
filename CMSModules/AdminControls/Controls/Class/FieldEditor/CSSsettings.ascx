<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CSSsettings.ascx.cs" Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_CSSsettings" %>
<asp:Panel ID="pnlCss" runat="server" CssClass="FieldPanel">
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblCaptionStyle" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.CaptionStyle" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCaptionStyle" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblInputStyle" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.InputStyle" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtInputStyle" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblControlCssClass" runat="server" EnableViewState="false"
                    ResourceString="TemplateDesigner.ControlCssClass" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtControlCssClass" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
    </table>
</asp:Panel>
