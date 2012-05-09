<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MessageBoards_Controls_LiveControls_Subscriptions" CodeFile="Subscriptions.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" tagname="HeaderActions" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/MessageBoards/Controls/Boards/BoardSubscription.ascx" TagName="BoardSubscriptionEdit" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/MessageBoards/Controls/Boards/BoardSubscriptions.ascx" TagName="BoardSubscriptionList" TagPrefix="cms" %>
<asp:PlaceHolder ID="plcSubscriptionList" runat="server">
    <asp:Panel ID="pnlSubscriptionActions" runat="server" CssClass="PageHeaderLine">
        <cms:HeaderActions ID="newSubscription" runat="server" />
    </asp:Panel>
    <cms:BoardSubscriptionList ID="boardSubscriptionList" runat="server" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcEditSubscriptions" runat="server" Visible="false">
    <asp:Panel ID="pnlEditHeader" runat="server" CssClass="Actions" EnableViewState="false">
        <asp:LinkButton ID="lnkEditSubscriptionBack" runat="server" CausesValidation="false" />
        <asp:Label ID="lblEditSubscriptionBack" runat="server" />
    </asp:Panel>
    <cms:BoardSubscriptionEdit ID="boardSubscriptionEdit" runat="server" />
</asp:PlaceHolder>
