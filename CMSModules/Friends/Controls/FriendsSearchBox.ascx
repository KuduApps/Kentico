<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Friends_Controls_FriendsSearchBox" CodeFile="FriendsSearchBox.ascx.cs" %>
<asp:Panel ID="pnlFilter" runat="server" EnableViewState="false">
    <cms:CMSTextBox ID="txtSearch" runat="server" EnableViewState="false" CssClass="TextBoxField" />
    <cms:LocalizedButton ID="btnSearch" runat="server" ResourceString="general.search"
        EnableViewState="false" CssClass="ContentButton" OnClick="btnSearch_OnClick" />
    <br />
    <br />
</asp:Panel>
