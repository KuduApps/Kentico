<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DocumentSource.ascx.cs"
    Inherits="CMSModules_AdminControls_Controls_Class_FieldEditor_DocumentSource" %>
<asp:Panel ID="pnlSourceField" runat="server" Visible="false">
    <cms:LocalizedLabel ID="lblSourceField" runat="server" EnableViewState="false" ResourceString="TemplateDesigner.SourceField" /><br />
    <asp:DropDownList ID="drpSourceField" runat="server" CssClass="SourceFieldDropDown"
        AutoPostBack="true" OnSelectedIndexChanged="drpSourceField_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <cms:LocalizedLabel ID="lblSourceAliasField" runat="server" EnableViewState="false"
        ResourceString="TemplateDesigner.SourceAliasField" /><br />
    <asp:DropDownList ID="drpSourceAliasField" runat="server" CssClass="SourceFieldDropDown"
        AutoPostBack="true" OnSelectedIndexChanged="drpSourceField_SelectedIndexChanged">
    </asp:DropDownList>
</asp:Panel>
