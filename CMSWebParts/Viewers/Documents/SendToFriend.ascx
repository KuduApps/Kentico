<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_Viewers_Documents_SendToFriend" CodeFile="~/CMSWebParts/Viewers/Documents/SendToFriend.ascx.cs" %>
<cms:CMSRepeater ID="repItems" runat="server" EnableViewState="false" StopProcessing="true" />
<asp:Label ID="lblHeader" runat="server" CssClass="sendToFriendHeader" EnableViewState="false" />
<asp:Panel ID="pnlSendToFriend" runat="server" DefaultButton="btnSend" CssClass="sendToFriendPanel">
    <div style="padding-bottom: 3px;">
        <asp:Label ID="lblInfo" runat="server" EnableViewState="false" />
        <asp:Label ID="lblError" runat="server" ForeColor="Red" EnableViewState="false" />
    </div>
    <div>
        <asp:Label ID="lblEmailTo" runat="server" EnableViewState="false" />
        <cms:CMSTextBox ID="txtEmailTo" runat="server" CssClass="sendToFriendEmailTextbox" />
        <cms:CMSButton ID="btnSend" runat="server" OnClick="btnSend_Click" ValidationGroup="sendToFriend" EnableViewState="false" />
        <cms:CMSRequiredFieldValidator ID="rfvEmailTo" runat="server" ControlToValidate="txtEmailTo"
            ValidationGroup="sendToFriend" Display="Dynamic" EnableViewState="false" />
    </div>
    <asp:Label ID="lblYourMessage" runat="server" CssClass="sendToFriendYourMessage" EnableViewState="false" />
    <asp:Panel ID="pnlMessageText" runat="server">
        <asp:Label ID="lblMessageText" runat="server" EnableViewState="false" />
        <cms:CMSTextBox ID="txtMessageText" runat="server" TextMode="MultiLine" Width="400"
            Height="100" CssClass="sendToFriendMessage" />
    </asp:Panel>
</asp:Panel>
