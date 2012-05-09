<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_LinkMediaSelector_Search" CodeFile="Search.ascx.cs" %>
<asp:Literal ID="ltlScript" runat="server"></asp:Literal>
<asp:Panel ID="pnlDialogSearch" runat="server" CssClass="DialogSearchBox">
    <div class="DialogSearchLabel">
        <cms:LocalizedLabel ID="lblSearchByName" runat="server" ResourceString="dialogs.view.searchbyname"
            DisplayColon="true" EnableViewState="false"></cms:LocalizedLabel>
    </div>
    <div class="DialogSearch">
        <cms:CMSTextBox ID="txtSearchByName" CssClass="TextBoxField" runat="server"></cms:CMSTextBox >
    </div>
    <div class="DialogSearch">
        <cms:LocalizedButton ID="btnSearch" CssClass="ContentButton" ResourceString="general.search"
            EnableViewState="false" runat="server" />
    </div>
</asp:Panel>
