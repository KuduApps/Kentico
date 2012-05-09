<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_Controls_WidgetPropertiesFieldEditor"
    CodeFile="WidgetPropertiesFieldEditor.ascx.cs" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/FieldEditor/FieldEditor.ascx"
    TagName="FieldEditor" TagPrefix="cms" %>
<asp:Panel ID="pnlError" runat="server" CssClass="FieldPanelError" Visible="false"
    EnableViewState="false">
    <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" ResourceString="general.invalidid"
        CssClass="ErrorLabel" />
</asp:Panel>
<cms:FieldEditor ID="fieldEditor" runat="server" />
