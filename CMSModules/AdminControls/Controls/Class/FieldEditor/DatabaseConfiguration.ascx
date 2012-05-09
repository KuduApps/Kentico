<%@ Control Language="C#" AutoEventWireup="True" CodeFile="DatabaseConfiguration.ascx.cs"
    Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_DatabaseConfiguration" %>
<%@ Register Src="~/CMSFormControls/Inputs/LargeTextArea.ascx" TagName="LargeTextArea"
    TagPrefix="cms" %>
<asp:Panel ID="pnlDatabase" runat="server" CssClass="FieldPanel">
    <table>
        <asp:PlaceHolder ID="plcGuid" runat="server" Visible="false">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblGuid" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.Guid" />
                </td>
                <td>
                    <asp:Label ID="lblGuidValue" runat="server" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcGroup" runat="server">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblGroup" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.Group" />
                </td>
                <td>
                    <asp:DropDownList ID="drpGroup" runat="server" CssClass="DropDownField" OnSelectedIndexChanged="drpGroup_SelectedIndexChanged"
                        AutoPostBack="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAttributeName" runat="server" EnableViewState="false"
                    ResourceString="TemplateDesigner.ColumName" />
            </td>
            <td>
                <asp:DropDownList ID="drpSystemFields" runat="server" CssClass="DropDownField" OnSelectedIndexChanged="drpSystemFields_SelectedIndexChanged"
                    AutoPostBack="true" DataValueField="column_name" DataTextField="column_name" />
                <cms:CMSTextBox ID="txtAttributeName" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAttributeType" runat="server" EnableViewState="false"
                    ResourceString="TemplateDesigner.AttributeType" />
            </td>
            <td>
                <asp:DropDownList ID="drpAttributeType" runat="server" DataTextField="Text" DataValueField="Value"
                    CssClass="DropDownField" AutoPostBack="True" OnSelectedIndexChanged="drpAttributeType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAttributeSize" runat="server" EnableViewState="false"
                    ResourceString="TemplateDesigner.AttributeSize" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtAttributeSize" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAllowEmpty" runat="server" EnableViewState="false" ResourceString="templatedesigner.attributeallowempty" />
            </td>
            <td>
                <asp:CheckBox ID="chkAllowEmpty" runat="server" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcDefaultValue" runat="server">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDefaultValue" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.ColumnDefaultValue" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDefaultValue" runat="server" CssClass="TextBoxField" Visible="false" />
                <cms:LargeTextArea ID="txtLargeDefaultValue" runat="server" Visible="false" />
                <asp:CheckBox ID="chkDefaultValue" runat="server" CssClass="CheckBoxMovedLeft" Visible="false" />
                <cms:DateTimePicker ID="datetimeDefaultValue" runat="server" Visible="false" />
            </td>
        </tr>
        </asp:PlaceHolder>
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
