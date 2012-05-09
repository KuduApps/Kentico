<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Messaging_Controls_MessageUserButtons" CodeFile="MessageUserButtons.ascx.cs" %>
<asp:Panel ID="pnlButtons" runat="server" CssClass="MessageUserButtons" EnableViewState="false">
    <asp:LinkButton ID="btnAddToContactList" runat="server" EnableViewState="false" OnClick="btnAddToContactList_Click">
        <asp:Image ID="imgAddToContactList" runat="server" EnableViewState="false" ImageAlign="AbsBottom" />
    </asp:LinkButton>
    <asp:LinkButton ID="btnAddToIgnoreList" runat="server" EnableViewState="false" OnClick="btnAddToIgnoreList_Click">
        <asp:Image ID="imgAddToIgnoreList" runat="server" EnableViewState="false" ImageAlign="AbsBottom" />
    </asp:LinkButton>
</asp:Panel>
