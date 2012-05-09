<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_Controls_NotificationSubscription_NotificationSubscription" CodeFile="NotificationSubscription.ascx.cs" %>
<div class="NotificationSubscriptionHeader">
    <cms:LocalizedLabel ID="lblDescription" runat="server" EnableViewState="false" />
</div>
<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:Panel ID="pnlGateways" runat="server" CssClass="NotificationSubscription" />
<asp:Panel runat="server" ID="pnlSubscribe" CssClass="NotificationSubscriptionButton" EnableViewState="false">
    <cms:CMSButton ID="btnSubscribe" runat="server" OnClick="btnSubscribe_Click" EnableViewState="false" />
</asp:Panel>
