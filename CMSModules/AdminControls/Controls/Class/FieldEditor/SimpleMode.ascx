<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SimpleMode.ascx.cs" Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_SimpleMode" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/System/UserControlTypeSelector.ascx" TagPrefix="cms"
    TagName="TypeSelector" %>
<asp:Panel ID="pnlSimple" runat="server">
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblColumnName" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.ColumName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtColumnName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
        </tr>
        <asp:PlaceHolder ID="pnlPublicField" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblPublicField" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.PublicField" />
                </td>
                <td>
                    <asp:CheckBox ID="chkPublicField" runat="server" CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblFieldCaption" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.FieldCaption" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtFieldCaption" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblTypeSelector" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.FieldType"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:TypeSelector ID="drpControlType" runat="server" CssClass="DropDownField" AutoPostBack="true"
                    IncludeAllItem="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
            </td>
            <td>
                <asp:DropDownList ID="drpFieldType" runat="server" AutoPostBack="True" DataTextField="Text"
                    DataValueField="Value" CssClass="DropDownField" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcSimpleTextBox" runat="server" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblSimpleTextBoxMaxLength" runat="server" EnableViewState="false"
                        ResourceString="templatedesigner.textboxlength" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtSimpleTextBoxMaxLength" runat="server" CssClass="TextBoxField" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAllowEmpty" runat="server" EnableViewState="false" ResourceString="templatedesigner.attributeallowempty" />
            </td>
            <td>
                <asp:CheckBox ID="chkAllowEmpty" runat="server" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDefaultValue" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.ColumnDefaultValue" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDefaultValue" runat="server" CssClass="TextBoxField" />
                <cms:LargeTextArea ID="txtLargeDefaultValue" runat="server" />
                <asp:CheckBox ID="chkDefaultValue" runat="server" CssClass="CheckBoxMovedLeft" />
                <cms:DateTimePicker ID="datetimeDefaultValue" runat="server" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcIsSystem" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblIsSystem" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.AttributeIsSystem" />
                </td>
                <td>
                    <asp:CheckBox ID="chkIsSystem" runat="server" CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</asp:Panel>
<asp:Panel ID="pnlSettings" runat="server" CssClass="FieldPanel">
    <cms:BasicForm ID="form" runat="server" />
</asp:Panel>
