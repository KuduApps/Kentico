<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_CustomTables_Controls_CustomTableForm" CodeFile="CustomTableForm.ascx.cs" %>
<asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:CustomTableForm ID="customTableForm" runat="server" />
