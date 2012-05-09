<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Newsletters_Controls_SubscriberFilter" CodeFile="SubscriberFilter.ascx.cs" %>
<asp:Panel runat="server" ID="pnlSearch">
    <asp:DropDownList ID="filter" runat="server" CssClass="ContentDropdown" />
    <cms:CMSTextBox ID="txtEmail" runat="server" style="margin-left:1px" />
</asp:Panel>
