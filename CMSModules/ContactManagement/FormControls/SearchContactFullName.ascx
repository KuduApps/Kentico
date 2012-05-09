<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchContactFullName.ascx.cs"
    Inherits="CMSModules_ContactManagement_FormControls_SearchContactFullName" %>
<asp:Panel CssClass="Filter" runat="server" ID="pnlSearch">
    <cms:LocalizedLabel ID="lblSearch" runat="server" EnableViewState="false" ResourceString="om.contact.name"
        DisplayColon="true" />
    <asp:DropDownList ID="drpCondition" runat="server" />
    <cms:CMSTextBox ID="txtSearch" runat="server" /><cms:LocalizedButton runat="server"
        ID="btnSelect" OnClick="btnSelect_Click" EnableViewState="false" ResourceString="general.search"
        CssClass="ContentButton" />
</asp:Panel>
